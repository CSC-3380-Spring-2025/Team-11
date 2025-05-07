using Godot;
using System;
using System.Collections.Generic;


/// <summary>
/// Basic behavior tree. Takes a repeater node which will repeat through every action of the tree indefinately.
/// </summary>
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
