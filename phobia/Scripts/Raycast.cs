using Godot;
using System;

/// <summary>
/// The Raycast class emits signals whenever it interacts with a door.
/// </summary>
public partial class Raycast : RayCast3D
{
	
	[Signal]
	public delegate void DoorHoveredEventHandler(Door doorObject);
	[Signal]
	public delegate void DoorNotHoveredEventHandler();
	[Signal]
	public delegate void ConcealmentObjectHoveredEventHandler(ConcealmentObject concealmentObject);
	[Signal]
	public delegate void ConcealmentObjectNotHoveredEventHandler();

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (IsColliding())
		{
			Object hitObj = GetCollider();
			if(hitObj is Door)
			{
				
				Door doorObj = (Door) hitObj;
				EmitSignal(SignalName.DoorHovered, doorObj);
				if(Input.IsActionJustPressed("interact"))
				{
					doorObj.Interact();
				}
				
			}
			else
			{
				EmitSignal(SignalName.DoorNotHovered);
			}
			
			if (hitObj is ConcealmentObject)
			{
				ConcealmentObject concealmentObj = (ConcealmentObject) hitObj;
				EmitSignal(SignalName.ConcealmentObjectHovered, concealmentObj);
				if(Input.IsActionJustPressed("interact"))
				{
					concealmentObj.Interaction();
				}
			}
		}
		else
		{
			EmitSignal(SignalName.ConcealmentObjectNotHovered);
		}
		
	}
}
