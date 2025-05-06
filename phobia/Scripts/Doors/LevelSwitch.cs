using Godot;
using System;



public partial class LevelSwitch : Node3D
{
	[Export]
	public PackedScene nextLevel;
	private ExitDoor exitDoor;
	private Godot.Collections.Array<Node> saveNodes;
	public override void _Ready()
	{

		saveNodes = GetTree().GetNodesInGroup("Persist");
		exitDoor = (ExitDoor)FindChild("StaticBody3D");
		exitDoor.ExitDoorOpened += OnExitDoorOpened;
	}

	private void OnExitDoorOpened()
	{
		foreach (Node saveNode in saveNodes)
		{
			if (saveNode is Player)
			{
				Player playerNode = (Player)saveNode;
				playerNode.Save();
			}
		}
		GetTree().ChangeSceneToPacked(nextLevel);
	}
}
