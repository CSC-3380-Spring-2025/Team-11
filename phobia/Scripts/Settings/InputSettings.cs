using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Resolvers;

public partial class InputSettings : Control
{
	private PackedScene inputButtonScene;
	private VBoxContainer actionList;
	private Control settingsMenu;
	private bool isRemapping = false;

	private StringName actionToRemap = null;
	private Button remappingButton = null;
	
	private Button[] buttons = new Button[10];
	private StringName[] actions = new StringName[10];

	Dictionary<StringName, StringName> inputActions = new Dictionary<StringName, StringName>();


	public override void _Ready()
	{
		
		inputButtonScene = GD.Load<PackedScene>("res://Scenes/Settings/input_button.tscn");
		actionList = GetNode<VBoxContainer>("PanelContainer/MarginContainer/VBoxContainer/ScrollContainer/ActionList");

		inputActions.Add("interact", "Interact");
		inputActions.Add("toggle_flashlight", "Toggle Flashlight");
		CreateActionList();

		// for (int j = 0; j < buttons.Length; j++)
		// {
		// 	if (buttons[j] != null)
		// 	{
		// 		buttons[j].Pressed += () => OnInputButtonPressed(buttons[j], actions[j]);
		// 	}
		// }

		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void CreateActionList()
	{
		InputMap.LoadFromProjectSettings();

		foreach (Node item in actionList.GetChildren())
		{
			item.QueueFree();
		}

		foreach (StringName action in InputMap.GetActions())
		{
			Button button = (Button)inputButtonScene.Instantiate();
			Label actionLabel = (Label)button.FindChild("LabelAction");
			Label inputLabel = (Label)button.FindChild("LabelInput");

			if(inputActions.ContainsKey(action))
			{
				actionLabel.Text = inputActions[action];
				Godot.Collections.Array<InputEvent> events = InputMap.ActionGetEvents(action);

			if (events.Count > 0)
			{
				inputLabel.Text = events[0].AsText().TrimSuffix(" (Physical)");
			}
			else
			{
				inputLabel.Text = "";
			}

			actionList.AddChild(button);
			
			button.Pressed += () => OnInputButtonPressed(button, action);
			}

		}
	}

	private void OnInputButtonPressed(Button button, StringName action)
	{
		if (!isRemapping)
		{
			isRemapping = true;
			actionToRemap = action;
			remappingButton = button;
			Label actionL = (Label) button.FindChild("LabelInput");

			actionL.Text = "Press key to bind...";
		}

	}

public override void _Input(InputEvent @event)
{
	if (isRemapping)
	{
		if(@event is InputEventKey || (@event is InputEventMouseButton && @event.IsPressed()))
		{
			InputMap.ActionEraseEvents(actionToRemap);
			InputMap.ActionAddEvent(actionToRemap, @event);
			UpdateActionList(remappingButton, @event);
		}
	}
}

private void UpdateActionList(Button button, InputEvent @event)
{
	Label labelInput = (Label)button.FindChild("LabelInput");
	labelInput.Text = @event.AsText().TrimSuffix(" (Physical)");
	

}
}
