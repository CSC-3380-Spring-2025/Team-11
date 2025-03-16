using Godot;
using System;


/// <summary>
/// The Door class takes an animation player and plays an animation when the player interacts with it.
/// </summary>
public partial class Door : StaticBody3D
{
	[Export]
	private AnimationPlayer doorAnimPlayer {get; set; }
	[Export]
	private Timer doorTimer {get; set; }
	private bool doorClosed = false;
	private bool interactable = true;
	private AudioStreamPlayer3D doorCloseSound;
	private AudioStreamPlayer3D doorOpenSound;

	public override void _Ready()
	{
		doorCloseSound = GetNode<AudioStreamPlayer3D>("DoorCloseSFX");
		doorOpenSound = GetNode<AudioStreamPlayer3D>("DoorOpenSFX");


		doorAnimPlayer.AnimationFinished += OnAnimFinished;
		doorAnimPlayer.AnimationStarted += OnAnimStarted;
		doorTimer.Timeout += OnDoorTimerTimeout;
	}
	public override void _Process(double delta)
	{
	}

	public void Interact()
	{
		if (interactable == true)
		{
			interactable = false;

			doorClosed = !doorClosed;
			if (doorClosed == false)
			{
				doorAnimPlayer.Play("close");
			}
			if (doorClosed == true)

			{
				doorAnimPlayer.Play("open");
			}
			
			doorTimer.Start();
		}
	}

	private void OnAnimFinished(StringName anim)
	{
		if(anim == "close")
		{
			doorCloseSound.Play();
		}
	}

	private void OnAnimStarted(StringName anim)
	{
		if(anim == "open")
		{
			doorOpenSound.Play();
		}
	}

	private void OnDoorTimerTimeout()
	{
		interactable = true;
	}
}