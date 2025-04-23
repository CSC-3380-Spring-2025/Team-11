using Godot;
using System;
using System.Collections.Generic;

public partial class BehaviorNode : Node
{
	public enum Status {
		RUNNING,
		SUCCESS,
		ERROR
	}

	public virtual Status Evaluate(Dictionary<StringName, Node> context)
	{
		GD.Print("Not implemented evaluate in BT, %s", Name);
		return Status.ERROR;	
	}
}
