using Godot;
using System;
using System.IO;



/// <summary>
/// The main menu, it deletes the players stats when the start button is pressed to start a new game.
/// </summary>
public partial class MainMenu : Control
{
	private DirAccess dir = DirAccess.Open("user://");
	public void start(){
		if(dir.FileExists("player_vars.cfg"))
		{
			GD.Print("Removing player_vars.cfg");
			dir.Remove("player_vars.cfg");
		}
		GetTree().ChangeSceneToFile("res://Levels/LVL0-Tutorial.tscn");
	}
	
	public void _on_quit_button_pressed () {
		GetTree().Quit();
	}	
	
}
