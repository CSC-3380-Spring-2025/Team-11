using Godot;
using System;

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
			concealed = true;
			interactable = !interactable;
			
			if (concealed == true) 
			{
				playerBody.Visible = false;
			}
			if (concealed && Input.IsActionJustPressed("interact"))
			{
				playerBody.Visible = true;
				interactable = true;
			}
		}
		
	}
}
