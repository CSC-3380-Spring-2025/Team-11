using Godot;
using System;

public partial class BatteryPickup : Area3D
{
	[Export] public float SpinSpeed = 3f; 

	private Node3D spriteNode;

	public override void _Ready()
	{
		spriteNode = GetNode<Node3D>("Sprite3D"); // Change path if needed
		BodyEntered += OnBodyEntered;
	}

	public override void _Process(double delta)
	{
		if (spriteNode == null) return;

		float spinWeight = 1f - Mathf.Exp(-SpinSpeed * (float)delta);
		Vector3 currentRotation = spriteNode.Rotation;
		Vector3 targetRotation = new Vector3(currentRotation.X, currentRotation.Y 
		+ Mathf.DegToRad(90f), currentRotation.Z);
		spriteNode.Rotation = currentRotation.Lerp(targetRotation, spinWeight);

	}

	private void OnBodyEntered(Node3D body)
	{
		if (body is Player player)
		{
			GD.Print("Battery picked up!");
			QueueFree();
		}
	}
}
