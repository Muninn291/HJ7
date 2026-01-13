using Godot;
using System;

public partial class Enemy : MovingEntity
{
  public int health = 1;

  public override void _Process(double delta)
  {
    if (level.Timeout && anim.IsPlaying())
    {
      anim.Pause();
    }
    else if (!anim.IsPlaying())
    {
      anim.Play();
    }
  }

  public void OnHit()
  {
    GD.Print("I've been HIT!");
    health -= 1;
    if (health <= 0)
    {
      QueueFree();
    }
  }
}