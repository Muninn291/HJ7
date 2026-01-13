using Godot;
using System;

public partial class Timemite : Enemy
{
  private static readonly float MOVE_SPEED = 50f;
  // public int direction;
  private static readonly Random tmRand = new();

  public override void _Ready()
  {
    base._Ready();
    direction = tmRand.Next(0, 2) * 2 - 1;
    // Velocity = new Vector2((tmRand.Next(0, 2) * 2 - 1) * MOVE_SPEED, 0);
    Velocity = new Vector2(MOVE_SPEED * direction, Velocity.Y);
  }

  // public override void _Process(double delta)
  // {
  //   base._Process(delta);

  // }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
    MoveAndSlide();
    if (IsOnWall())
    {
      direction *= -1;
      Velocity = new Vector2(MOVE_SPEED * direction, Velocity.Y);
    }
  }
}