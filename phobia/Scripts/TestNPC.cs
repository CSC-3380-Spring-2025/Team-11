using Godot;

public partial class TestNPC : Area3D
{
	public override void _Ready()
	{
		GD.Print("TestNpc is ready.");
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	private void OnBodyEntered(Node body)
	{
		GD.Print("Collision detected! Reloading...");
		GetTree().ReloadCurrentScene();
	}
}
