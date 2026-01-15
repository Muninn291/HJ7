using Godot;
using System;
using static Func;

public partial class Retry : Button
{
  public override void _Pressed()
  {
    base._Pressed();
    Global.Instance.GetTree().Paused = false;
    RetryLevel();
    // Node level = GD.Load<PackedScene>($"res://Menu/{GetParent().GetParent()}.tscn").Instantiate();
    // ChangeMainScene(level, true);
  }
}
