using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// The enemy moves toward the player.
/// </summary>
public partial class MoveToPlayer : BehaviorNode
{
	[Export]
	CharacterBody3D movementNode;
	[Export]
	public float speed = 5.0f;
	public Player target = null;
	private NavigationAgent3D navAgent;
	private Area3D los;
	private AnimatedSprite3D sprite;
	bool isRunning = false;

    public override void _Ready()
    {
        navAgent = (NavigationAgent3D)movementNode.FindChild("NavigationAgent3D");
		los = (Area3D)movementNode.FindChild("LOS");
		sprite = (AnimatedSprite3D)movementNode.FindChild("Sprite3D");
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
		sprite.LookAt(nextLocation);
		sprite.Rotation = new Vector3(0, sprite.Rotation.Y, 0); 
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
