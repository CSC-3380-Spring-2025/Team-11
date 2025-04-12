using Godot;
using System;

public partial class Player : CharacterBody3D
{

	[Signal]
	public delegate void BatteryDepletedEventHandler();
	[Signal]
	public delegate void PlayerReadyEventHandler();
	public const float baseSpeed = 4.0f;
	public const float sprintModifier = 0.6f;
	public float currentSpeed = baseSpeed;
	public const float jumpVelocity = 4.5f;
	public const float camSensitivity = 0.006f;
	public int flashlightBattery = 100;

	private Node3D head;
	private Camera3D cam;
	private Node3D hand;
	private SpotLight3D flashlight;
	private Timer batteryTimer;
	private int batteryDepletionRate = 2;
	private double timeLeft;
	private bool flashlightOn = true;

	private Area3D hurtBox;

	
	public override void _Ready(){
		Input.MouseMode = Input.MouseModeEnum.Captured;
		head = GetNode<Node3D>("Head");
		cam = GetNode<Camera3D>("Head/Camera3D");
		hand = GetNode<Node3D>("Hand");
		flashlight = GetNode<SpotLight3D>("Hand/SpotLight3D");

		batteryTimer = GetNode<Timer>("BatteryTimer");
		batteryTimer.Timeout += OnBatteryTimerTimeOut;
		timeLeft = batteryTimer.WaitTime;
		hurtBox = (Area3D)FindChild("Hurtbox").FindChild("Area3D");
		hurtBox.BodyEntered += OnBodyEnteredHurtbox;
		EmitSignal(SignalName.PlayerReady);

	}
	
	public override void _Input(InputEvent @event){
		if(@event is InputEventMouseMotion m){
			head.RotateY(-m.Relative.X * camSensitivity);
			cam.RotateX(-m.Relative.Y * camSensitivity);
			hand.RotateY(-m.Relative.X * camSensitivity);
			flashlight.RotateX(-m.Relative.Y * camSensitivity);
			
			Vector3 camRot = cam.Rotation;
			camRot.X = Mathf.Clamp(camRot.X,
				Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
			cam.Rotation = camRot;
			
			Vector3 handRot = hand.Rotation;
			handRot.X = Mathf.Clamp(handRot.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
			hand.Rotation = handRot;
			
			Vector3 flashlightRot = flashlight.Rotation;
			flashlightRot.X = Mathf.Clamp(flashlightRot.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
			flashlight.Rotation = flashlightRot;
			
		}
		else if (@event is InputEventKey k && k.Keycode == Key.Escape){
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = jumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backwards");
		Vector3 direction = (head.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();


		if(Input.IsActionPressed("sprint"))
		{
			currentSpeed = baseSpeed + (baseSpeed * sprintModifier);
		}
		else
		{
			currentSpeed = baseSpeed;
		}

		if (direction != Vector3.Zero)
		{	
			velocity.X = direction.X * currentSpeed;
			velocity.Z = direction.Z * currentSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, currentSpeed);
		}
		


		Velocity = velocity;
		MoveAndSlide();
		
	}
	public override void _Process(double delta)
	{
		HandleFlashlightBattery();
	}
	private void HandleFlashlightBattery()
	{
		if(flashlightBattery > 0)
		{
			if(Input.IsActionJustPressed("toggle_flashlight") && flashlightOn)
			{
				flashlight.Visible = false;
				flashlightOn = false;
				batteryTimer.Stop();
			}
			else if(Input.IsActionJustPressed("toggle_flashlight") && !flashlightOn)
			{
				flashlight.Visible = true;
				flashlightOn = true;
				batteryTimer.Start(batteryTimer.TimeLeft);
			}
		}
		else
		{
			flashlight.Visible = false;
		}	

	}
	private void OnBatteryTimerTimeOut()
	{		
		flashlightBattery = Math.Max(0, flashlightBattery - batteryDepletionRate);
		batteryTimer.Start();
		EmitSignal(SignalName.BatteryDepleted);
	}

	private void OnBodyEnteredHurtbox(Node3D body)
	{
		PhysicsBody3D bodyEntered = (PhysicsBody3D)body;

		//Currently detects if collision eleent is an enemy using col layer but when
		//an enemy class is created it will check for enemy class
		if(bodyEntered.CollisionLayer == 4)
		{
			Globals.Instance.previousScene = GetTree().CurrentScene.SceneFilePath;
			CallDeferred(MethodName.ChangeToGameOver);
		}
	}

	private void ChangeToGameOver()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
		GetTree().ChangeSceneToFile("res://Scenes/game_over_screen.tscn");
	}
}
