using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// This node repeats the node node under it forever
/// </summary>
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
