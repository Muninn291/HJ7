using Godot;
using System;

public partial class Grain : Item
{
  public static readonly long grainGain = 3000;

  public override void _Process(double delta)
  {
    base._Process(delta);
    if (level.overtime)
    {
      QueueFree();
    }
  }


  public override void OnPickUp()
  {
    GD.Print("Picked up a grain!");
    level.grains += grainGain;
    level.UpdateGCount();
  }
}
