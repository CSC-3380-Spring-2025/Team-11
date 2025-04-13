using Godot;
using System;

public partial class pathfinding : CharacterBody3D
{

	public float Speed = 2f;
	public float Accel = 10f;
	
	private NavigationAgent3D nav;
	private Vector3 velocity = Vector3.Zero;
	
	public override void _Ready()
	{
		nav = GetNode<NavigationAgent3D>("NavigationAgent3D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 direction = Vector3.Zero;

		// Replace this with the actual path to your target node or use an AutoLoad
		Node3D target = GetNode<Node3D>("Target"); // Adjust path as needed
		nav.TargetPosition = target.GlobalPosition;
		

		direction = nav.GetNextPathPosition() - GlobalPosition;
		direction = direction.Normalized();

		velocity = velocity.Lerp(direction * Speed, (float)(Accel * delta));

		MoveAndSlide();
	}
}
