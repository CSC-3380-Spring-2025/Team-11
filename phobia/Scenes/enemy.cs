
using Godot;
using System;


/// <summary>
/// Prototype enemy used to test the pathfinding.
/// </summary>
public partial class enemy : CharacterBody3D
{
	public float speed = 3.0f;
	


	private NavigationAgent3D navAgent;

	public override void _Ready()
	{
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
	}

	public override void _PhysicsProcess(double delta)
	{

		Vector3 currentLocation = GlobalTransform.Origin;
		Vector3 nextLocation = navAgent.GetNextPathPosition();
		Vector3 newVelocity = (nextLocation - currentLocation).Normalized() * speed;

		Velocity = newVelocity;
		MoveAndSlide();
	}

	public void UpdateTargetLocation(Vector3 targetLocation)
	{
		navAgent.TargetPosition = targetLocation;
	}
}

	
