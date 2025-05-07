using Godot;
using System;

/// <summary>
/// Audio plays whenever an object in the area's collision mask is entered. 
/// </summary>
public partial class PlayAudioOnBodyEnter : Area3D
{

	[Export]
	public bool continuous;
	[Export]
	public Timer cooldownTimer;
	[Export]
	public AudioStreamPlayer3D audio;
	private bool audioPlaying = false;
	private bool bodiesInArea = false;
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cooldownTimer.Autostart = false;
		cooldownTimer.OneShot = true;
		BodyEntered += OnBodyEntered; 
		BodyExited += OnBodyExited;
		audio.Finished += OnAudioFinished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node3D body)
	{
		if(cooldownTimer.TimeLeft == 0 && !bodiesInArea)
		{
			if(continuous)
			{
				audioPlaying = true;
			}

			bodiesInArea = true;
			audio.Play();
			cooldownTimer.Start();
		}
	}

	private void OnBodyExited(Node3D body)
	{
		if(GetOverlappingBodies().Count <= 0)
		{
		audioPlaying = false;
		bodiesInArea = false;
		}


	}
	private void OnAudioFinished()
	{
		if(continuous && audioPlaying)
		{
			audio.Play();
		}
	}
}
