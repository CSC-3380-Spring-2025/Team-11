using Godot;
using System;

public partial class MainMenu : Control
{
	public void start(){
		GetTree().ChangeSceneToFile("res://Levels/LVL0-Tutorial.tscn");
	}
	
	public void _on_quit_button_pressed () {
		GetTree().Quit();
	}	
	
}
