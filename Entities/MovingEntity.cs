using Godot;
using System;
using static MoveState;

public partial class MovingEntity : Entity
{
  [Export]
  private float GRAVITY = 1700f;
  [Export]
  private float TERMINAL_VELOCITY = 750f;
  [Export]
  private float MAX_MOVE_SPEED = 500f;
  [Export]
  private float MOVE_ACCEL = 1500f;
  [Export]
  private float MOVE_DECEL = 2500f;
  [Export]
  private float JUMP_SPEED = 400f;
  [Export]
  private float MAX_JUMP_TIME = 0.25f;
  public MoveState moveState = MoveState.AIRBORNE;
  public float direction = 0f;
  public float jumpTime = 0f;


  public override void _PhysicsProcess(double delta)
  {
    switch(moveState)
    {
      case MoveState.GROUNDED:
      jumpTime = 0f;
      break;
      case MoveState.AIRBORNE:
      jumpTime = 0f;
      break;
      case MoveState.JUMPING:
      break;
      default:
      break;
    }
    float xSpeed = Velocity.X;
    float ySpeed = Velocity.Y;

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
      jumpTime += (float)delta;
      if (jumpTime >= MAX_JUMP_TIME)
      {
        moveState = AIRBORNE;
        jumpTime = 0f;
      }
      else
      {
        ySpeed = -1 * JUMP_SPEED;
      }
    }
    Velocity = new(xSpeed, ySpeed);


    MoveAndSlide();
    if (moveState != JUMPING)
    {
      if (Velocity.Y != 0f)
      {
        moveState = AIRBORNE;
      }
      else
      {
        moveState = GROUNDED;
      }
    }
  }
}