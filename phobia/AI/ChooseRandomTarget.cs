using Godot;
using System;
using System.Collections.Generic;

public partial class ChooseRandomTarget : BehaviorNode
{
	public override BehaviorNode.Status Evaluate(Dictionary<StringName, Node> context)
	{
		Node randomTargets = Globals.Instance.root.FindChild("RandomTargets");

		if(randomTargets == null || randomTargets.GetChildCount() == 0)
		{
			GD.Print("No Random Targets");
			return BehaviorNode.Status.SUCCESS;
		}
		else
		{
			Random rand = new Random();
			GD.Print("Number of Random Targets :" + randomTargets.GetChildCount());
			int targetNum = rand.Next(randomTargets.GetChildCount());
			GD.Print("Target chosen : RandomTarget" + targetNum);
			if(context.ContainsKey("target"))
			{
				context["target"] = randomTargets.FindChild("RandomTarget" + targetNum);
			}
			else
			{
				context.Add("target", randomTargets.FindChild("RandomTarget" + targetNum));
			}

			return BehaviorNode.Status.SUCCESS;
		}
	}
}
