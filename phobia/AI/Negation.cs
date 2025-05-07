using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// This node reverses the status returned by the node under it. SUCCESS if ERROR / ERROE if SUCCESS
/// </summary>
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
