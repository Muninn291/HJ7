using Godot;
using System;


public partial class Player : Entity
{
  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("jump") && moveState == MoveState.GROUNDED)
    {
      moveState = MoveState.JUMPING;
    }
    else if (@event.IsActionReleased("jump") && moveState == MoveState.JUMPING)
    {
      moveState = MoveState.AIRBORNE;
    }
  }
  public override void _PhysicsProcess(double delta)
  {
    direction = Input.GetVector("left", "right", "up", "down").X;
    base._PhysicsProcess(delta);
  }
}
