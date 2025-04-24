using Godot;
using System;
using System.Collections.Generic;
public partial class RepeaterNode : BehaviorNode
{
	[Export]
	BehaviorNode node;


	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		node.Evaluate(context);
		return BehaviorNode.Status.RUNNING;
	}
}
