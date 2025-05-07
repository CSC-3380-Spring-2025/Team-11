using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// This node keeps repeating until the node under it returns ERROR.
/// </summary>
public partial class RepeatUntilFail : BehaviorNode
{
	[Export]
	BehaviorNode node;


	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		BehaviorNode.Status result = node.Evaluate(context);

		if(result == BehaviorNode.Status.ERROR)
		{
			return BehaviorNode.Status.SUCCESS;
		}
		else
		{
			return BehaviorNode.Status.RUNNING;
		}

	}
}
