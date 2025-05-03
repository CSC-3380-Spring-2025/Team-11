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
		if (Input.IsActionPressed("close_tutorial") && Visible && !GetTree().Paused)
		{
			Visible = false;
		}

		Godot.Collections.Array<InputEvent> flashlightToggle = InputMap.ActionGetEvents("toggle_flashlight");
		Godot.Collections.Array<InputEvent> sprintToggle = InputMap.ActionGetEvents("sprint");

		tutorialLabel.Text = "Welcome to the tutorial! You can use the flashlight to find your way through mazes.\n" + 
		"To use the flashlight press " +  flashlightToggle[0].AsText().TrimSuffix(" (Physical)").ToLower() + ", to move around use WASD or Arrow Keys.\n" +
		"To sprint press " + sprintToggle[0].AsText().TrimSuffix(" (Physical)").ToLower() + ". (Press Enter to Close)"; 

	}

}
