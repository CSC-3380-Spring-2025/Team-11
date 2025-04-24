using Godot;
using System;
using System.Collections.Generic;

public partial class SequenceNode : BehaviorNode
{
	[Export]
	BehaviorNode[] nodes;
	public int currentNode = 0;

	private void _ResetState()
	{
		currentNode = 0;
	}

	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		if (nodes.Length == 0)
		{
			_ResetState();
			return BehaviorNode.Status.SUCCESS;
		}
		

		BehaviorNode btNode = nodes[currentNode];
		BehaviorNode.Status result = btNode.Evaluate(context);


		if(result == BehaviorNode.Status.ERROR)
		{
			_ResetState();
			return BehaviorNode.Status.ERROR;
		}
		
		if(result == BehaviorNode.Status.SUCCESS)
		{
			if(currentNode == nodes.Length - 1)
			{
				_ResetState();
				return BehaviorNode.Status.SUCCESS;
			}
		}
		
		if(result != BehaviorNode.Status.RUNNING)
		{
			currentNode += 1;
		}

		return BehaviorNode.Status.RUNNING;
		
	}
}
