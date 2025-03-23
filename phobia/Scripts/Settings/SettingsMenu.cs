using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SettingsMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	
	[Export]
	private Button settingsButton;
	private Button remapButton;
	private InputSettings inputSettings;


	public override void _Ready()
	{
		remapButton = GetNode<Button>("PanelContainer/MarginContainer/VBoxContainer/ScrollContainer/VBoxContainer/RemapButton");
		inputSettings = GetNode<InputSettings>("InputSettings"); 
		
		remapButton.Pressed += OnRemapButtonPressed;
		settingsButton.Pressed += OnSettingsButtonPressed; 
	}

	public override void _Process(double delta)
	{
		if (inputSettings.close == true)
		{
			Visible = false;
		}
	}
	private void OnRemapButtonPressed()
	{
		inputSettings.Visible = true;

	}

	private void OnSettingsButtonPressed()
	{
		inputSettings.close = false;
		Visible = true;
	}
}
	
