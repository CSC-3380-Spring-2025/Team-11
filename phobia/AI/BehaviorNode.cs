using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// Basic behvaior node for the behavior tree. Used for the implementation of AI behavior. It returns three status RUNNING, SUCCESS, ERROR.
/// </summary>
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
