using Godot;
using System;

public partial class MainScene : Node3D
{
	private grid gridNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gridNode = GetNode<grid>("Grid");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		gridNode.RotateY((float)(Mathf.Pi / 10 * delta));
	}
}
