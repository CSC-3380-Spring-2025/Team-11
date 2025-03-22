using Godot;
using System;

public partial class ExitDoor : Door
{
	[Signal]
	public delegate void ExitDoorOpenedEventHandler();


	public override void _Ready()
	{
		connectSignals();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void Interact()
    {
        base.Interact();
		EmitSignal(SignalName.ExitDoorOpened);
    }
}
