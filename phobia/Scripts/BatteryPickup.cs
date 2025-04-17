using Godot;
using System;

public partial class BatteryPickup : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Enable collision detection
		this.Monitoring = true;

		// Connect the signal BodyEntered to the OnBodyEntered method
		this.BodyEntered += OnBodyEntered;
	}

	// This function will be called when a body enters the area.
	private void OnBodyEntered(Node3D body)
	{
		// Check if the body is the player (make sure your player is in the correct group)
		if (body is Player) // or `body.IsInGroup("Player")` if using groups
		{
			GD.Print("Battery picked up!");
			QueueFree(); // Remove the battery from the scene
		}
	}
}
