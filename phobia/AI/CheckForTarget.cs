using Godot;
using System;
using System.Collections.Generic;


public partial class CheckForTarget : BehaviorNode
{
	[Export]
	Area3D los;

	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
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
	// private void OnVisionTimerTimeOut()
	// {
	// 	Area3D los = GetNode<Area3D>("LOS");
	// 	RayCast3D visionRaycast = GetNode<RayCast3D>("LOS/VisionRayCast");

	// 	Godot.Collections.Array<Node3D> overlaps = los.GetOverlappingBodies();
		
	// 	if (overlaps.Count > 0)
	// 	{
	// 		foreach (Node3D overlap in overlaps)
	// 		{
	// 			if(overlap is Player && !isChasing)
	// 			{
	// 				Vector3 playerPosition = overlap.GlobalPosition;
	// 				visionRaycast.LookAt(playerPosition, Vector3.Up);
	// 				visionRaycast.ForceRaycastUpdate();

	// 				if(visionRaycast.IsColliding())
	// 				{
	// 					GodotObject collider = visionRaycast.GetCollider();
	// 					if (collider is Player)
	// 					{
	// 						visionRaycast.DebugShapeCustomColor = Color.Color8(174, 0, 0);
	// 						GD.Print("I see " + overlap.Name);							
	// 					}
	// 					else
	// 					{
	// 						visionRaycast.DebugShapeCustomColor = Color.Color8(0, 255, 0);
	// 						GD.Print("I don't see " + overlap.Name);
	// 					}
	// 				}
					
	// 			}
	// 		}
	// 	} 
		
	// }

