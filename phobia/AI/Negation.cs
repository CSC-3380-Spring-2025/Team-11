using Godot;
using System;
using System.Collections.Generic;

public partial class Negation : BehaviorNode
{
	[Export]
	BehaviorNode node;


	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		BehaviorNode.Status result = node.Evaluate(context);

		if(result == BehaviorNode.Status.SUCCESS)
		{
			return BehaviorNode.Status.ERROR;
		}
		if(result == BehaviorNode.Status.ERROR)
		{
			return BehaviorNode.Status.SUCCESS;
		}
		
		return BehaviorNode.Status.RUNNING;

	}
}
