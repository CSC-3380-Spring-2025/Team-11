using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MoveToPlayer : BehaviorNode
{
	[Export]
	CharacterBody3D movementNode;
	[Export]
	public float speed = 5.0f;
	public Player target = null;
	private NavigationAgent3D navAgent;
	private Area3D los;
	bool isRunning = false;

    public override void _Ready()
    {
        navAgent = (NavigationAgent3D)movementNode.FindChild("NavigationAgent3D");
		los = (Area3D)movementNode.FindChild("LOS");
    }


    public override void _PhysicsProcess(double delta)
    {
        if(isRunning)
		{
		UpdateTargetLocation(target.GlobalPosition);
		//GD.Print("Finding Target " + target.Name + ": ");
		//GD.Print("Current Target Position: " + target.GlobalPosition.X + ", " + target.GlobalPosition.Y);
		Vector3 currentLocation = movementNode.GlobalTransform.Origin;
		Vector3 nextLocation = navAgent.GetNextPathPosition();
		Vector3 newVelocity = (nextLocation - currentLocation).Normalized() * speed;

		movementNode.Velocity = newVelocity;
		movementNode.MoveAndSlide();

		los.LookAt(nextLocation);
		los.Rotation = new Vector3(0, los.Rotation.Y, 0); 
		}
    }

	public void UpdateTargetLocation(Vector3 targetLocation)
	{
		navAgent.TargetPosition = targetLocation;
	}
	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		
		if(!context.ContainsKey("Player"))
		{
			GD.Print("Player not in context");
			return BehaviorNode.Status.ERROR;
		}

		target = (Player)context["Player"];

		if(!(target is Player))
		{
			GD.Print("Target is not Player, got %s", target);
			return BehaviorNode.Status.ERROR;
		}

		isRunning = true;
		return BehaviorNode.Status.RUNNING;
	}
}
