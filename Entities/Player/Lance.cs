using Godot;
using System;

public partial class Lance : Node2D
{
	public override void _Ready()
	{
		LookAt(GetGlobalMousePosition());

		AnimationPlayer thrustAnimation = (AnimationPlayer)FindChild("ThrustArea").FindChild("ThrustAnim");
		thrustAnimation.CurrentAnimationChanged += AnimEnd;
		thrustAnimation.Play("thrust");
	}

	public void AnimEnd(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			QueueFree();
		}
	}
}
