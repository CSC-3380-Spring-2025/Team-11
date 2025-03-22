using Godot;
using System;

public partial class LevelSwitch : Node3D
{
	[Export]
	public PackedScene nextLevel;
	[Export]
	private ExitDoor exitDoor;
	public override void _Ready()
	{
		exitDoor.ExitDoorOpened += OnExitDoorOpened;
	}

	private void OnExitDoorOpened()
	{
		GetTree().ChangeSceneToPacked(nextLevel);
	}
}
