using Godot;
using System;


/// <summary>
/// Let's the enemies know where the player is in each level.
/// </summary>
public partial class lvl1 : Node3D
{

	private Node3D player;

	public override void _Ready()
	{
		player = GetNode<Node3D>("UI/Player");
		Globals.Instance.root = GetNode(this.GetPath());
	}

	public override void _PhysicsProcess(double delta)
	{
		GetTree().CallGroup("enemies", "UpdateTargetLocation", player.GlobalTransform.Origin);
	}
}
