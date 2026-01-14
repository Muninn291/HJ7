using Godot;
using System;
using static Func;

public partial class Quit : Button
{
  public override void _Pressed()
  {
    base._Pressed();
    Global.Instance.GetTree().Quit();
  }
}
