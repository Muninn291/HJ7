using Godot;
using System;

public partial class AimPointer : Node2D
{
  public override void _Process(double delta)
  {
    LookAt(GetGlobalMousePosition());
  }
}
