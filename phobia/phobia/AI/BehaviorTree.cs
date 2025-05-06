using Godot;
using System;
using System.Collections.Generic;

public partial class BehaviorTree : Node
{
	[Export]
	RepeaterNode root;
	Dictionary<StringName, Node> _context = new Dictionary<StringName, Node>();

    public override void _Process(double delta)
    {
    root.Evaluate(_context);  
    }


}
