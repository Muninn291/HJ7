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
  private float MAX_MOVE_SPEED = 250f;
  [Export]
  private float MOVE_ACCEL = 1500f;
  [Export]
  private float MOVE_DECEL = 2500f;
  [Export]
  private float JUMP_SPEED = 400f;
  [Export]
  private float MAX_JUMP_TIME = 0.25f;
  private float MAX_DASH_TIME = 0.03f;
  private float DASH_DURATION = 0.15f;
  private float DASH_SPEED = 1700f;
  private float DASH_DECEL = 5500f;
  public float DASH_COOLDOWN = 0.55f;
  public MoveState moveState = MoveState.AIRBORNE;
  public float direction = 0f;
  public int facing = 1;
  public float jumpTime = 0f;
  public float dashTime = 0f;
  public bool dashReady = true;
  public bool attacking = false;
  public float ATTACK_COOLDOWN = 2f;

  public override void _Ready()
  {
    base._Ready();
    dashTime = DASH_COOLDOWN;
  }

  public override void _PhysicsProcess(double delta)
  {
    if (level.timeout)
    {
      return;
    }
    switch (moveState)
    {
      case GROUNDED:
        jumpTime = 0f;
        break;
      case AIRBORNE:
        jumpTime = 0f;
        break;
      case JUMPING:
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

    dashTime += (float)delta;
    if (moveState == DASHING)
    {
      if (dashTime <= MAX_DASH_TIME)
      {
        xSpeed = DASH_SPEED * facing;
      }
      else
      {
        xSpeed = Math.Max(Math.Abs(xSpeed) - DASH_DECEL * (float)delta, 0f) * xSpeed.SignNotZero();
      }
      ySpeed = 0f;
      if (dashTime > DASH_DURATION)
      {
        moveState = IsOnFloor() ? GROUNDED : AIRBORNE;
      }
    }
    else
    {
      if (direction != 0f)
      {
        facing = (int)direction;
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
    }
    Velocity = new(xSpeed, ySpeed);

    dashReady = dashReady || (moveState == GROUNDED && dashTime >= DASH_COOLDOWN);

    MoveAndSlide();
    if (moveState != DASHING)
    {
      if (moveState != JUMPING)
      {
        if (IsOnFloor())
        {
          if (moveState == AIRBORNE)
          {
            dashReady = true;
          }
          moveState = GROUNDED;
        }
        else
        {
          moveState = AIRBORNE;
        }
      }
      else if (Velocity.Y == 0f)
      {
        moveState = AIRBORNE;
      }
    }
  }
}