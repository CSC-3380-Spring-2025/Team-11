using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class MoveToTarget : BehaviorNode
{
	[Export]
	CharacterBody3D movementNode;
	[Export]
	public float speed = 5.0f;
	public Player target = null;
	NavigationAgent3D navAgent;

	bool isRunning = false;

    public override void _Ready()
    {
        navAgent = (NavigationAgent3D)movementNode.FindChild("NavigationAgent3D");
    }


    public override void _PhysicsProcess(double delta)
    {
        if(isRunning)
		{
		UpdateTargetLocation(target.GlobalPosition);
		Vector3 currentLocation = movementNode.GlobalTransform.Origin;
		Vector3 nextLocation = navAgent.GetNextPathPosition();
		Vector3 newVelocity = (nextLocation - currentLocation).Normalized() * speed;

		movementNode.Velocity = newVelocity;
		movementNode.MoveAndSlide();
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

		foreach (var element in context)
		{
			if(element.Key == "Player")
			{
				target = (Player)element.Value;
			}
		}

		if(!(target is Player))
		{
			GD.Print("Target is not Player, got %s", target);
			return BehaviorNode.Status.ERROR;
		}
		isRunning = true;
		return BehaviorNode.Status.RUNNING;
	}
}
