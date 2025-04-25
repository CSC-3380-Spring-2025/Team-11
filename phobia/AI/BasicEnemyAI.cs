using Godot;
using System;

public partial class BasicEnemyAI : CharacterBody3D
{
	public const float Speed = 3.0f;
	Timer visionTimer;

	bool isChasing = false;
	Player chaseTarget;
	NavigationAgent3D navAgent;
	public override void _Ready()
	{
		visionTimer = (Timer)FindChild("VisionTimer");
		visionTimer.Timeout += OnVisionTimerTimeOut;
		navAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
	}

    public override void _Process(double delta)
    {
        if(isChasing)
		{
			UpdateTargetLocation(chaseTarget.GlobalPosition);
			GD.Print(chaseTarget.GlobalPosition);
		}
    }

	public override void _PhysicsProcess(double delta)
	{
		if(isChasing)
		{
		Vector3 currentLocation = GlobalTransform.Origin;
		Vector3 nextLocation = navAgent.GetNextPathPosition();
		Vector3 newVelocity = (nextLocation - currentLocation).Normalized() * Speed;

		Velocity = newVelocity;
		MoveAndSlide();
		}
	}

	public void UpdateTargetLocation(Vector3 targetLocation)
	{
		navAgent.TargetPosition = targetLocation;
	}
	private void OnVisionTimerTimeOut()
	{
		Area3D los = GetNode<Area3D>("LOS");
		RayCast3D visionRaycast = GetNode<RayCast3D>("LOS/VisionRayCast");

		Godot.Collections.Array<Node3D> overlaps = los.GetOverlappingBodies();
		
		if (overlaps.Count > 0)
		{
			foreach (Node3D overlap in overlaps)
			{
				if(overlap is Player && !isChasing)
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
							chaseTarget = (Player)collider;
							isChasing = true;
							
						}
						else
						{
							visionRaycast.DebugShapeCustomColor = Color.Color8(0, 255, 0);
							//GD.Print("I don't see " + overlap.Name);
						}
					}
					
				}
			}
		} 
		
	}
}
