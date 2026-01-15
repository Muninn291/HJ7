using Godot;
using System;

public partial class Detector : Area2D
{
  public override void _Ready()
  {
    BodyShapeEntered += OnBodyShapeEntered;
    BodyShapeExited += OnBodyShapeExited;
  }

  public static void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
  {
    if (body is Item item)
    {
      item.OnEnter();
    }
    else if (body is Enemy enemy)
    {
      if (enemy.level.grains > 0)
      {
        enemy.level.DecreaseGrains(1000);
      }
      else
      {
        enemy.level.pause.ShowDefeat();
      }
    }
  }

  public static void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
  {
    if (body is Item item)
    {
      item.OnExit();
    }
  }
}
