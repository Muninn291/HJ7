using Godot;
using System;

public partial class Player : MovingEntity
{
  public PackedScene thrust = GD.Load<PackedScene>("res://Entities/Player/thrust.tscn");

  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("rightClick"))
    {
      level.timeout = true;
    }
    else if (@event.IsActionReleased("rightClick"))
    {
      level.timeout = false;
    }
    else if (@event.IsActionPressed("attack"))
    {
      Node2D thrustInstance = (Node2D)thrust.Instantiate();
      AddChild(thrustInstance);
    }
    else if (@event.IsActionPressed("dash") && dashReady)
    {
      dashTime = 0f;
      dashReady = false;
      if (moveState == MoveState.AIRBORNE)
      {
        airDash = true;
      }
      moveState = MoveState.DASHING;
    }
    else if (moveState != MoveState.DASHING)
    {
      if (@event.IsActionPressed("jump") && moveState == MoveState.GROUNDED)
      {
        moveState = MoveState.JUMPING;
      }
      else if (@event.IsActionReleased("jump") && moveState == MoveState.JUMPING)
      {
        moveState = MoveState.AIRBORNE;
      }
    }
  }
  public override void _PhysicsProcess(double delta)
  {
    if (moveState != MoveState.DASHING)
    {
      direction = Math.Sign(Input.GetVector("left", "right", "up", "down").X);
    }
    base._PhysicsProcess(delta);
  }
}
