using Godot;
using System;



/// <summary>
/// Contains varibales that need to persist between scenes.
/// </summary>
public partial class Globals : Node
{
	public static Globals Instance {get; private set;}
	
	public String previousScene {get; set;}
	public Node root {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
