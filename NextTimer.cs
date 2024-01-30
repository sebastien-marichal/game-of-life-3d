using Godot;
using System;

public partial class NextTimer : Node
{
	private Timer next;

	[Signal]
	public delegate void NextStepEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		next = new Timer();
		next.Timeout += () =>
			{
			EmitSignal("NextStep");
			};
		next.Autostart = true;
		next.OneShot = false;
		AddChild(next);
	}
}
