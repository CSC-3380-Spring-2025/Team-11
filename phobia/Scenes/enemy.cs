
using Godot;
using System;

public partial class enemy : CharacterBody3D
{
	public float Speed = 3.0f;
	public float gravity = 9.8f;


	private NavigationAgent3D navAgent;

	public override void _Ready()
	{
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
	}

	public override void _PhysicsProcess(double delta)
	{

		Vector3 currentLocation = GlobalTransform.Origin;
		Vector3 nextLocation = navAgent.GetNextPathPosition();
		Vector3 newVelocity = (nextLocation - currentLocation).Normalized() * Speed;

		Velocity = newVelocity;
		MoveAndSlide();
	}

	public void UpdateTargetLocation(Vector3 targetLocation)
	{
		navAgent.TargetPosition = targetLocation;
	}
}

	
