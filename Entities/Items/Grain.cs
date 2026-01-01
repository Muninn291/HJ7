using Godot;
using System;

public partial class Grain : Item
{
  public static readonly ulong grainGain = 3000;

  public override void OnPickUp()
  {
	GD.Print("Picked up a grain!");
	level.grains += grainGain;
  }
}
