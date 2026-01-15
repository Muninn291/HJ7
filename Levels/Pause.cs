using Godot;
using System;

public partial class Pause : CanvasLayer
{
  public Control pauseMenu;
  public Control victory;
  public Control defeat;
  public bool gameEnd = false;

  public override void _Ready()
  {
    base._Ready();
    pauseMenu = (Control)FindChild("PauseMenu");
    victory = (Control)FindChild("Victory");
    defeat = (Control)FindChild("Defeat");
  }


  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("pause") && !gameEnd)
    {
      TogglePause();
    }
  }

  public void TogglePause()
  {
    Global.Instance.GetTree().Paused = !Global.Instance.GetTree().Paused;
    pauseMenu.Visible = Global.Instance.GetTree().Paused;
  }

  public void ShowVictory()
  {
    Global.Instance.GetTree().Paused = true;
    victory.Visible = true;
    gameEnd = true;
  }

  public void ShowDefeat()
  {
    Global.Instance.GetTree().Paused = true;
    defeat.Visible = true;
    gameEnd = true;
  }
}
