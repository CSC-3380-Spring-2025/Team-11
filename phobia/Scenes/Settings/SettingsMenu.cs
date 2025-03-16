using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SettingsMenu : Control
{
	// Called when the node enters the scene tree for the first time.

	private Button remapButton;
	private Control inputSettings;
	public override void _Ready()
	{
		remapButton = GetNode<Button>("PanelContainer/MarginContainer/VBoxContainer/ScrollContainer/VBoxContainer/RemapButton");
		inputSettings = GetNode<Control>("InputSettings"); 
		remapButton.Pressed += OnRemapButtonPressed; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnRemapButtonPressed()
	{
		inputSettings.Visible = true;

	}
}
	