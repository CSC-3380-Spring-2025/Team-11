using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Takes the target from ChooseRandomTarget node and then go to that location.
/// </summary>
public partial class GoToTarget : BehaviorNode
{
	[Export]
	public CharacterBody3D moveNode;
	[Export]
	public float speed = 3.0f;
	//Timer visionTimer;
	private bool isRunning = false;
	private bool reachedTarget = false;
	private NavigationAgent3D navAgent;
	private Area3D los;
	private Node3D target;
	private AnimatedSprite3D sprite;
	public override void _Ready()
	{
		los  = (Area3D)moveNode.FindChild("LOS");
		navAgent = (NavigationAgent3D)moveNode.FindChild("NavigationAgent3D");
		sprite = (AnimatedSprite3D)moveNode.FindChild("Sprite3D");
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		if(isRunning)
		{
		UpdateTargetLocation(target.GlobalPosition);
		//GD.Print("Finding Target " + target.Name + ": ");
		//GD.Print("Current Target Position: " + target.GlobalPosition.X + ", " + target.GlobalPosition.Y);
		Vector3 currentLocation = moveNode.GlobalTransform.Origin;
		Vector3 nextLocation = navAgent.GetNextPathPosition();
		Vector3 newVelocity = (nextLocation - currentLocation).Normalized() * speed;

		moveNode.Velocity = newVelocity;
		moveNode.MoveAndSlide();

		los.LookAt(nextLocation);
		los.Rotation = new Vector3(0, los.Rotation.Y, 0); 
		sprite.LookAt(nextLocation);
		sprite.Rotation = new Vector3(0, sprite.Rotation.Y, 0);

		}
	}

	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		if(!context.ContainsKey("target"))
		{
			//GD.Print("Target Not Found");
			return BehaviorNode.Status.ERROR;
		}
		else
		{
			target = (Node3D)context["target"];
		}
		if(CheckVision() == BehaviorNode.Status.SUCCESS)
		{
			isRunning = false;
			return BehaviorNode.Status.SUCCESS;
		}

		if(moveNode.GlobalPosition.DistanceTo(target.GlobalPosition) <= navAgent.TargetDesiredDistance)
		{
			isRunning = false;
			return BehaviorNode.Status.ERROR;
		}

		isRunning = true;
		return BehaviorNode.Status.RUNNING;
		
	}

	public void UpdateTargetLocation(Vector3 targetLocation)
	{
		navAgent.TargetPosition = targetLocation;
	}


	private BehaviorNode.Status CheckVision()
	{
		RayCast3D visionRaycast = (RayCast3D)los.FindChild("VisionRayCast");

		Godot.Collections.Array<Node3D> overlaps = los.GetOverlappingBodies();
		
		if (overlaps.Count > 0)
		{
			foreach (Node3D overlap in overlaps)
			{
				if(overlap is Player)
				{
					Vector3 playerPosition = overlap.GlobalPosition;
					visionRaycast.LookAt(playerPosition, Vector3.Up);
					visionRaycast.ForceRaycastUpdate();

					if(visionRaycast.IsColliding())
					{
						GodotObject collider = visionRaycast.GetCollider();
						if (collider is Player)
						{
							visionRaycast.DebugShapeCustomColor = Color.Color8(174, 0, 0);
							//GD.Print("I see " + overlap.Name);
							return BehaviorNode.Status.SUCCESS;							
						}
						else
						{
							visionRaycast.DebugShapeCustomColor = Color.Color8(0, 255, 0);
							//GD.Print("I don't see " + overlap.Name);
							return BehaviorNode.Status.ERROR;
						}
					}
					
				}
			}
		} 
		return BehaviorNode.Status.RUNNING;
	}
}
