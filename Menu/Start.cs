using Godot;
using System;
using static Func;

public partial class Start : Button
{
  public override void _Pressed()
  {
    base._Pressed();
    Node level = GD.Load<PackedScene>("res://Levels/Main_Level1.tscn").Instantiate();
    ChangeMainScene(level, true);
  }
}
