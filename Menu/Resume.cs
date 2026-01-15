using Godot;
using System;
using static Func;

public partial class Resume : Button
{
  public override void _Pressed()
  {
    base._Pressed();
    ((Pause)GetParent().GetParent().GetParent()).TogglePause();
  }
}
