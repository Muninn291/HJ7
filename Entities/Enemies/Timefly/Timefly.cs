using Godot;
using System;

public partial class Timefly : Enemy
{
  private static readonly float MOVE_SPEED = 50f;
  private static readonly float OVERTIME_MOVE_SPEED = 125f;
  // private static readonly Random tmRand = new();

  public override void _Ready()
  {
    base._Ready();
    // direction = tmRand.Next(0, 2) * 2 - 1;
    Velocity = new Vector2(MOVE_SPEED, 0);
    // Velocity.
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    if (!level.Timeout)
    {
      MoveAndSlide();
      // if (IsOnWall())
      // {
      //   direction *= -1;
      // }
      Velocity = new Vector2((level.overtime ? OVERTIME_MOVE_SPEED : MOVE_SPEED) * direction, Velocity.Y);
    }
  }
}