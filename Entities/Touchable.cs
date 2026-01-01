using Godot;
using System;

public abstract partial class Touchable : Entity
{
  bool disabled = false;
  public void OnEnter()
    {
        if (!disabled)
        {
            TouchableEntered();
        }
    }

    public void OnExit()
    {
        if (!disabled)
        {
            TouchableExited();
        }
    }

    public virtual void TouchableEntered()
    {

    }
    public virtual void TouchableExited()
    {

    }

    public void Disable()
    {
        disabled = true;
    }

    public void MarkForDelete()
    {
        Disable();
        QueueFree();
    }
}