using Godot;
using System;

public partial class Lance : Node2D
{
	public override void _Ready()
	{
		LookAt(GetGlobalMousePosition());

		AnimationPlayer thrustAnimation = (AnimationPlayer)FindChild("ThrustArea").FindChild("ThrustAnim");
		thrustAnimation.AnimationFinished += AnimEnd;
		thrustAnimation.Play("thrust");
	}

	public void AnimEnd(StringName animName)
	{
		if (animName == "thrust")
		{
			QueueFree();
		}
	}
}
