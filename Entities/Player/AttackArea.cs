using Godot;
using System;

public partial class AttackArea : Area2D
{
  public override void _Ready()
  {
    BodyShapeEntered += OnBodyShapeEntered;
    BodyShapeExited += OnBodyShapeExited;
  }

  public static void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
  {
    if (body is Enemy enemy)
    {
      enemy.OnHit();
    }
  }

  public static void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
  {
  }
}
