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
		}
		else
		{
			EmitSignal(SignalName.DoorNotHovered);
		}
		
	}
}
