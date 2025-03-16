using Godot;
using System;

public partial class TutorialUI : Control
{

	public bool hidden = false;

	private Label tutorialLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tutorialLabel = GetNode<Label>("PanelContainer/Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("close_tutorial") && Visible)
		{
			Visible = false;
		}


	}
}
