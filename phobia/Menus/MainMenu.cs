using Godot;
using System;

public partial class MainMenu : Control
{
	public void start(){
		GetTree().ChangeSceneToFile("res://Levels/LVL0-Tutorial.tscn");
	}
	
	
	
	
}
