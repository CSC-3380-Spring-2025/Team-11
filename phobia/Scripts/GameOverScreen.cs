using Godot;
using System;


/// <summary>
/// This screen pops up when the player dies.
/// </summary>
public partial class GameOverScreen : Control
{
	// Called when the node enters the scene tree for the first time.
	
	private Button restart;
	private Button mainMenu;
	private Button quit;
	public override void _Ready()
	{
		restart = (Button)FindChild("Restart");
		mainMenu = (Button)FindChild("MainMenu");
		quit = (Button)FindChild("Quit");

		restart.Pressed += OnRestartPressed;
		mainMenu.Pressed += OnMainMenuPressed;
		quit.Pressed += OnQuitPressed;

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnRestartPressed()
	{
		GetTree().ChangeSceneToFile(Globals.Instance.previousScene);
	}
	private void OnMainMenuPressed()
	{
		GetTree().ChangeSceneToFile("res://Menus/MainMenu.tscn");
	}
	private void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
