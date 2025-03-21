using Godot;
using System;

public partial class TutorialUI : Control
{

	public bool hidden = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("close_tutorial") && Visible && !GetTree().Paused)
		{
			Visible = false;
		}

	}
}
