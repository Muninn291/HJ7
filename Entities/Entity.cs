using Godot;
using System;
using static MoveState;

// namespace HJ7
// {
public partial class Entity : CharacterBody2D
{
  [Export]
  private float GRAVITY = 750f;
  [Export]
  private float TERMINAL_VELOCITY = 750f;
  [Export]
  private float MAX_MOVE_SPEED = 500f;
  [Export]
  private float MOVE_ACCEL = 1250f;
  [Export]
  private float MOVE_DECEL = 1800f;
  [Export]
  private float JUMP_SPEED = 500f;
  [Export]
  private MoveState moveState = MoveState.AIRBORNE;
  private Vector2 xSpeed = Vector2.Zero;

  public override void _PhysicsProcess(double delta)
  {
    // switch(moveState)
    // {
    //   case MoveState.JUMPING:
    //   break;
    //   case MoveState.GROUNDED:
    //   break;
    //   default:
    //   break;
    // }
    float xSpeed = Velocity.X;
    float ySpeed = Velocity.Y;

    float direction = Input.GetVector("left", "right", "up", "down").X;
    if (direction == 0f || direction != xSpeed.SignNotZero())
    {
      xSpeed = Math.Max(Math.Abs(xSpeed) - MOVE_DECEL * (float)delta, 0f) * xSpeed.SignNotZero();
    }
    
    if (direction != 0f)
    {
      xSpeed += direction * MOVE_ACCEL * (float)delta;
      xSpeed = Math.Min(Math.Abs(xSpeed), MAX_MOVE_SPEED) * xSpeed.SignNotZero();
    }


    if (moveState != JUMPING)
    {
      ySpeed = Math.Min(ySpeed + GRAVITY * (float)delta, TERMINAL_VELOCITY);
    }
    else
    {
      ySpeed = Math.Min(ySpeed - JUMP_SPEED * (float)delta, TERMINAL_VELOCITY);
    }
    Velocity = new(xSpeed, ySpeed);


    // GD.Print(Velocity.Y);
    MoveAndSlide();
    if (moveState != JUMPING && Velocity.Y != 0f)
    {
      moveState = AIRBORNE;
    }
    else
    {
      moveState = GROUNDED;
    }
    // GD.Print(Velocity.Y);
    // GD.Print(Velocity.X);

  }
}
// }