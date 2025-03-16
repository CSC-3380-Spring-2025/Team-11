using Godot;
using System;

public partial class PauseUi : Control
{
	private bool paused = false;
	
	public override void _Ready() 
	{
			Visible = false;
	}
	
	public override void _Process(double delta) 
	{
		if (Input.IsActionJustPressed("toggle_pause"))
		{
			Visible = true;
			paused = true;
			GetTree().Paused = true;
		}
		if (Input.IsActionJustPressed("toggle_pause") && Visible) 
		{
			Visible = false;
			paused = false;
			GetTree().Paused = false;
		}
	}
	
	public void _on_resume_pressed() 
	{
		paused = false;
		Visible = false;
		GetTree().Paused = false;
	}
	public void _on_restart_pressed() 
	{
		GetTree().ReloadCurrentScene(); 
	}
	public void _on_settings_pressed() 
	{
		
	}
	public void _on_quit_pressed() 
	{
		
		GetTree().ChangeSceneToFile("res://Menus/MainMenu.tscn");
	}
}
