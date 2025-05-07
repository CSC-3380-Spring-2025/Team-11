using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// Get's the node of the player so that the other node's that follow the player can have it's location.
/// </summary>
public partial class FindTarget : BehaviorNode
{
	const String targetName = "Player";

	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		Node targetNode = Globals.Instance.root.FindChild(targetName);
		
		if(targetNode == null)
		{
			return BehaviorNode.Status.ERROR;
		}

		if(!context.ContainsValue(targetNode))
		{
			context.Add(targetName, targetNode);
		}

		return BehaviorNode.Status.SUCCESS;
	}
}
