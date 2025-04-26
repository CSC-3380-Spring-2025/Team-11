using Godot;
using System;
using System.Numerics;


/// <summary>
/// The UI method toogles between visible and invisible based on the signals it recieves from the Player Raycast.
/// </summary>
public partial class UI : Control
{
	private int batteryPercentage;

	private int staminaPercentage;
	private Player player;
	private Label battery;
	private Label doorOverlay;
	private ProgressBar staminaBar;
	private Label concealmentObjectOverlay;
	private TextureRect batteryTextureRect;
	private AtlasTexture batteryTexture;
	private Godot.Vector2 batteryTextureOffset = new Godot.Vector2(10, 10);
	private Godot.Vector2 batteryTextureSize = new Godot.Vector2(70, 32);
	public override void _Ready()
	{
		Raycast playerRaycast = GetNode<Raycast>("Player/Head/RayCast3D");
		player = GetNode<Player>("Player");
		battery = GetNode<Label>("Battery");
		doorOverlay = GetNode<Label>("DoorOverlay");
		concealmentObjectOverlay = GetNode<Label>("ObjectOverlay");
		staminaBar = GetNode<ProgressBar>("StaminaBar");
		batteryTextureRect = GetNode<TextureRect>("BatteryTexture");
		batteryTexture = (AtlasTexture)batteryTextureRect.Texture;
		batteryTexture.Region = new Rect2(batteryTextureOffset.X, batteryTextureOffset.Y, batteryTextureSize.X, batteryTextureSize.Y);

		playerRaycast.DoorHovered += OnDoorHovered;
		playerRaycast.DoorNotHovered += OnDoorNotHovered;
		playerRaycast.ConcealmentObjectHovered += OnConcealmentObjectHovered;
		playerRaycast.ConcealmentObjectNotHovered += OnConcealmentObjectNotHovered;
		player.BatteryUpdated += OnBatteryUpdated;
		player.StaminaUpdate += OnStaminaUpdate;
		player.Ready += OnPlayerReady;
		UpdateBatteryPercentage();
		UpdateStaminaPercentage();


	}
	public override void _Process(double delta)
	{


	}

	private void OnDoorHovered(Door doorObject)
	{
		Godot.Collections.Array<InputEvent> events = InputMap.ActionGetEvents("interact");

		if(doorObject is ExitDoor && events.Count > 0)
		{
			doorOverlay.Text = "Press " + events[0].AsText().TrimSuffix(" (Physical)") + " to go to next level.";
		}
		else if (!(doorObject is ExitDoor) && events.Count > 0)
		{
			doorOverlay.Text  = "Press " + events[0].AsText().TrimSuffix(" (Physical)") + " to open door.";
		}
		else
		{
			doorOverlay.Text = "";
		}
		
		doorOverlay.Visible = true;
	}

	private void OnDoorNotHovered()
	{

		doorOverlay.Visible = false;
	}
	
	private void OnConcealmentObjectHovered(ConcealmentObject concealmentObject)
	{
		concealmentObjectOverlay.Visible = true;
	}
	
	private void OnConcealmentObjectNotHovered()
	{
		concealmentObjectOverlay.Visible = false;
	}

	private void OnBatteryUpdated()
	{
		UpdateBatteryPercentage();
	}

	
	private void OnStaminaUpdate()
	{
		UpdateStaminaPercentage();
	}


	private void OnPlayerReady()
	{
		GD.Print("Player Ready");
		UpdateBatteryPercentage();
	}

	private void UpdateBatteryPercentage()
	{
		batteryPercentage = player.flashlightBattery;
		battery.Text = batteryPercentage + "%";
		
		if(batteryPercentage <= 100  && batteryPercentage >= 75)
		{
			//battery.SelfModulate = Colors.Green;
			batteryTexture.Region = new Rect2(batteryTextureOffset.X, batteryTextureOffset.Y, batteryTextureSize.X, batteryTextureSize.Y);
		}
		else if (batteryPercentage <= 75 && batteryPercentage >= 50)
		{
			//battery.SelfModulate = Colors.Yellow;
			batteryTexture.Region = new Rect2(batteryTextureOffset.X * 2 + batteryTextureSize.X, batteryTextureOffset.Y, batteryTextureSize.X, batteryTextureSize.Y);
		}
		else if (batteryPercentage <= 50 && batteryPercentage >= 25)
		{
			//battery.SelfModulate = Colors.Orange;
			batteryTexture.Region = new Rect2(batteryTextureOffset.X * 3 + batteryTextureSize.X * 2, batteryTextureOffset.Y, batteryTextureSize.X, batteryTextureSize.Y);
		}
		else if (batteryPercentage < 25 && batteryPercentage > 0)
		{
			//battery.SelfModulate = Colors.Red;
			batteryTexture.Region = new Rect2(batteryTextureOffset.X * 4 + batteryTextureSize.X * 3, batteryTextureOffset.Y, batteryTextureSize.X, batteryTextureSize.Y);
		}
		else
		{
			//battery.SelfModulate = Colors.Red;
			batteryTexture.Region = new Rect2(batteryTextureOffset.X * 5 + batteryTextureSize.X * 4, batteryTextureOffset.Y, batteryTextureSize.X, batteryTextureSize.Y);
		}

	}

	private void UpdateStaminaPercentage()
	{
		staminaPercentage = player.currentStamina;
		staminaBar.Value = staminaPercentage;
	}

}
