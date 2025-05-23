using Godot;
using System;



/// <summary>
/// Displays title menu when the game paused.
/// </summary>
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
		
	}
	private void _on_resume_pressed() 
	{
		paused = false;
		Visible = false;
		GetTree().Paused = false;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	private void _on_restart_pressed() 
	{
		paused = false;
		Visible = false;
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene(); 
	}

	private void _on_settings_pressed() 
	{
		//paused= false; 
		//Visible = false;
		//GetTree().Paused = false;
		//GetTree().ChangeSceneToFile("res://Scenes/Settings/settings_menu.tscn");
		
	}

	private void _on_quit_pressed() 
	{
		paused = false; 
		Visible = false;
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Menus/MainMenu.tscn");
	}
}
