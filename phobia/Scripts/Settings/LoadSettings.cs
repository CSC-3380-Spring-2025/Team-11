using Godot;
using System;

public partial class LoadSettings : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void LoadInputSettings(ConfigFile config, String settingsPath)
	{
		Error err = config.Load(settingsPath);
		
		if(err != Error.Ok)
		{
			InputMap.LoadFromProjectSettings();
		}
		else
		{
			InputMap.LoadFromProjectSettings();
			foreach (String action in config.GetSections())
			{
				if(InputMap.HasAction(action))
				{
					InputEvent actionEvent = (InputEvent) config.GetValue(action, action);
					InputMap.ActionEraseEvents(action);
					InputMap.ActionAddEvent(action, actionEvent);
				}
			}	
		} 
	}
}
