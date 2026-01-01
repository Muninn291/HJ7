using Godot;
using System;

public abstract partial class Item : Touchable
{
  public abstract void OnPickUp();

  public override void TouchableEntered()
  {
    OnPickUp();
    MarkForDelete();
  }

  // public override void TouchableExited()
  // {
    
  // }
}