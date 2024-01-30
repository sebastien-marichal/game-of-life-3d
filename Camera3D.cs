using Godot;
using System;

public partial class Camera3D : Godot.Camera3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LookAt(Vector3.Zero, Vector3.Up);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
