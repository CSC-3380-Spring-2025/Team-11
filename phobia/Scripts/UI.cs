using Godot;
using System;


/// <summary>
/// The UI method toogles between visible and invissible based on the signals it recieves from the Player Raycast.
/// </summary>
public partial class UI : Control
{
	private int batteryPercentage;

	private int staminaPercentage;
	private Player player;
	private Label battery;
	private Label doorOverlay;
	private ProgressBar staminaBar;

	public override void _Ready()
	{
		Raycast playerRaycast = GetNode<Raycast>("Player/Head/RayCast3D");
		player = GetNode<Player>("Player");
		battery = GetNode<Label>("Battery");
		doorOverlay = GetNode<Label>("DoorOverlay");

		staminaBar = GetNode<ProgressBar>("StaminaBar");
		playerRaycast.DoorHovered += OnDoorHovered;
		playerRaycast.DoorNotHovered += OnDoorNotHovered;
		player.BatteryDepleted += OnBatteryDepleted;
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

	private void OnBatteryDepleted()
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
		battery.Text = "Battery: " + batteryPercentage + "%";
		
		if(batteryPercentage <= 100  && batteryPercentage >= 75)
		{
			battery.SelfModulate = Colors.Green;
		}
		else if (batteryPercentage <= 75 && batteryPercentage >= 50)
		{
			battery.SelfModulate = Colors.Yellow;
		}
		else if (batteryPercentage <= 50 && batteryPercentage >= 25)
		{
			battery.SelfModulate = Colors.Orange;
		}
		else
		{
			battery.SelfModulate = Colors.Red;
		}

	}

	private void UpdateStaminaPercentage()
	{
		staminaPercentage = player.currentStamina;
		staminaBar.Value = staminaPercentage;
	}

}
