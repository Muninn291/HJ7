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
  private float DASH_SPEED = 1500f;
  private float DASH_DECEL = 5500f;
  public float DASH_COOLDOWN = 0.55f;
  private float SUPER_DASH_SPEED = 4700f;
  public MoveState moveState = MoveState.AIRBORNE;
  public float direction = 0f;
  public int facing = 1;
  public float jumpTime = 0f;
  public float dashTime = 0f;
  public bool dashReady = true;
  public bool airDash = false;
  public bool attackCoolingDown = false;
  public float ATTACK_COOLDOWN = 0.5f;
  public float attackTime = 0f;
  public Vector2 superDashTarget = Vector2.Zero;
  public CollisionShape2D dashBox;
  public Area2D dashDetector;
  public Vector2 superDashOrigin;
  public RectangleShape2D collisionShape;
  public AnimationPlayer anim;

  public override void _Ready()
  {
    base._Ready();
    dashTime = DASH_COOLDOWN;
    dashDetector = (Area2D)FindChild("DashDetector");
    dashBox = (CollisionShape2D)dashDetector?.FindChild("DashCollision");
    collisionShape = (RectangleShape2D)((CollisionShape2D)((Area2D)FindChild("Detector"))?.FindChild("Collision"))?.Shape;
    anim = (AnimationPlayer)FindChild("AnimationPlayer");
    anim.Play("idle");
  }

  public override void _PhysicsProcess(double delta)
  {
    if (level.Timeout && moveState != SUPER_DASHING)
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

    if (this is Player && (direction == 0f || direction != xSpeed.SignNotZero()))
    {
      xSpeed = Math.Max(Math.Abs(xSpeed) - MOVE_DECEL * (float)delta, 0f) * xSpeed.SignNotZero();
    }

    if (attackCoolingDown)
    {
      attackTime += (float)delta;
      if (attackTime >= ATTACK_COOLDOWN)
      {
        attackCoolingDown = false;
      }
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
    else if (moveState == SUPER_DASHING)
    {
      xSpeed = 0f;
      ySpeed = 0f;
      Vector2 result = GlobalPosition.MoveToward(superDashTarget, SUPER_DASH_SPEED * (float)delta) - GlobalPosition;
      if (MoveAndCollide(result) != null)
      {
        EndSuperdash();
      }
    }
    else
    {
      if (this is Player)
      {
        if (direction != 0f)
        {
          facing = (int)direction;
          xSpeed += direction * MOVE_ACCEL * (float)delta;
          xSpeed = Math.Min(Math.Abs(xSpeed), MAX_MOVE_SPEED) * xSpeed.SignNotZero();
        }
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

    dashReady = dashReady || (!airDash && dashTime >= DASH_COOLDOWN);

    if (moveState == SUPER_DASHING)
    {
      if (GlobalPosition.DistanceTo(superDashTarget) < 0.01f)
      {
        EndSuperdash();
      }
    }
    else
    {
      MoveAndSlide();

      if (moveState != DASHING && moveState != SUPER_DASHING)
      {
        if (moveState != JUMPING)
        {
          if (IsOnFloor())
          {
            if (airDash)
            {
              dashReady = true;
              airDash = false;
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

  public void EndSuperdash()
  {
    moveState = IsOnFloor() ? GROUNDED : AIRBORNE;
    var dashShape = new RectangleShape2D
    {
      Size = new Vector2((GlobalPosition - superDashOrigin).Length() + collisionShape.Size.X, collisionShape.Size.Y)
    };
    dashDetector.GlobalPosition = superDashOrigin;
    dashBox.Shape = dashShape;
    dashBox.Position = new Vector2(((GlobalPosition - superDashOrigin).Length()) / 2, 0);
    dashDetector.LookAt(GlobalPosition);
  }
}