using Godot;
using System;

public partial class Player : MovingEntity
{
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
    else if (@event.IsActionPressed("dash") && dashReady)
    {
      dashTime = 0f;
      dashReady = false;
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
      direction = Input.GetVector("left", "right", "up", "down").X;
    }
    base._PhysicsProcess(delta);
  }
}
