using Godot;
using System;
using System.Collections.Generic;



/// <summary>
/// This causes the enemy to wait for a specified number of time.
/// </summary>
public partial class IdleNode : BehaviorNode
{
	[Export]
	public float waitTime = 1;
	Timer timer;
	private bool timerStarted = false;
    public override void _Ready()
    {
        timer = GetNode<Timer>("IdleTimer");
		timer.WaitTime = waitTime;

    }


	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		if(timer.TimeLeft == 0 && !timerStarted)
		{
			timer.Start();
			timerStarted = true;
		}
		else if (timer.TimeLeft == 0 && timerStarted)
		{
			timerStarted = false;
			return BehaviorNode.Status.SUCCESS;
		}

		return BehaviorNode.Status.RUNNING; 
	}
}
