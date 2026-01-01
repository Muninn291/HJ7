using Godot;
using System;

public partial class ReverseGrain : Item
{
  public bool collected = false;
  public override void OnPickUp()
  {
    GD.Print("Picked up a reverse grain!");
    collected = true;
    level.UpdateRGCount();
  }
}