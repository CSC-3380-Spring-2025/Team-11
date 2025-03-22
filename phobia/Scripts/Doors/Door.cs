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
	[Export]
	private AudioStreamPlayer3D doorCloseSound;
	[Export]
	private AudioStreamPlayer3D doorOpenSound;
	private bool doorClosed = false;
	private bool interactable = true;



	
	public override void _Ready()
	{
		connectSignals();
	}

	public virtual void Interact()
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

	protected void connectSignals()
	{
		doorAnimPlayer.AnimationFinished += OnAnimFinished;
		doorAnimPlayer.AnimationStarted += OnAnimStarted;
		doorTimer.Timeout += OnDoorTimerTimeout;
	}
}