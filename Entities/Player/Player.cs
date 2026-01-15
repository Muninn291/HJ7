using Godot;
using System;
using System.Threading;

public partial class Player : MovingEntity
{
  public static readonly PackedScene thrust = GD.Load<PackedScene>("res://Entities/Player/thrust.tscn");

  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("rightClick"))
    {
      if (level.grains > 0)
      {
        level.Timeout = true;
      }
    }
    else if (@event.IsActionReleased("rightClick"))
    {
      level.Timeout = false;
    }
    else if (@event.IsActionPressed("attack") && !attackCoolingDown)
    {
      Node2D thrustInstance = (Node2D)thrust.Instantiate();
      AddChild(thrustInstance);
      attackCoolingDown = true;
      attackTime = 0f;
    }
    else if (@event.IsActionPressed("dash"))
    {
      if (level.Timeout)
      {
        if (level.grains > 1000)
        {
          superDashTarget = GetGlobalMousePosition();
          superDashOrigin = GlobalPosition;
          moveState = MoveState.SUPER_DASHING;
          level.DecreaseGrains(1000);
        }
      }
      else if (dashReady)
      {
        dashTime = 0f;
        dashReady = false;
        if (moveState == MoveState.AIRBORNE)
        {
          airDash = true;
        }
        moveState = MoveState.DASHING;
      }
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
    if (moveState == MoveState.SUPER_DASHING)
    {

    }
    else if (moveState != MoveState.DASHING)
    {
      direction = Math.Sign(Input.GetVector("left", "right", "up", "down").X);
    }
    base._PhysicsProcess(delta);
  }
}
