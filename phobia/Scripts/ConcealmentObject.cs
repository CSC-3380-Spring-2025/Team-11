using Godot;
using System;


/// <summary>
/// Hides the player when the player interacts with it.
/// </summary>
public partial class ConcealmentObject : StaticBody3D
{
	private bool interactable = true;
	private bool concealed = false;
	private Player playerBody;
	
	public override void _Ready()
	{
		playerBody = GetNode<Player>("Player");
	}
	
	public virtual void Interaction() 
	{
		if (interactable == true) 
		{
			interactable = false;
			playerBody.Visible = false;
			concealed = true;
			
			if (concealed && Input.IsActionJustPressed("interact"))
			{
				playerBody.Visible = true;
				interactable = true;
			}
		}
		
	}
}
