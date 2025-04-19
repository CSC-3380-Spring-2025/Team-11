using Godot;
using System;
using System.IO;

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
