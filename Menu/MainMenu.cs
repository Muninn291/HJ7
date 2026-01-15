using Godot;
using System;
using static Func;

public partial class MainMenu : Button
{
  public override void _Pressed()
  {
    base._Pressed();
    Global.Instance.GetTree().Paused = false;
    Node level = GD.Load<PackedScene>("res://Menu/main_menu.tscn").Instantiate();
    ChangeMainScene(level, true);
  }
}
