using Godot;
using System;

public partial class ItemObject : Node3D
{
	// Called when the node enters the scene tree for the first time.

	[Export]
	public Sprite3D spriteNode;
	[Export]
	public float spinSpeed = 0.05f;
	[Export]
	public Area3D itemHitbox;



	public override void _Ready()
	{
		itemHitbox.BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float spinWeight = 1f - Mathf.Exp(-spinSpeed * (float)delta);
		spriteNode.Rotation = spriteNode.Rotation.Lerp(new Vector3(spriteNode.Rotation.X, spriteNode.Rotation.Y + 90f, spriteNode.Rotation.Z), spinWeight);
		Transform3D transform = Transform;
        transform.Basis = Basis.Identity;
        Transform = transform;
	}

	private void OnBodyEntered(Node3D body)
	{
		if(body is Player)
		{
			GD.Print("Item Picked Up by " + body.GetType());
			QueueFree();
		}
	}
}
