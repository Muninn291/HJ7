using Godot;
using System;
using System.Linq;

public partial class Level : Control
{
  [Export]
  public long grains = 60000;
  public int reverseGrains = -1;
  public RichTextLabel gCount;
  public RichTextLabel rgCount;
  public ProgressBar grainCount;
  public ulong prevTime = 0;
  public ulong newTime = 0;
  public bool timeout = false;

  public override void _Ready()
  {
    prevTime = Time.GetTicksMsec();
    newTime = prevTime;
    grainCount = (ProgressBar)FindChild("UI").FindChild("ProgressBar");
    grainCount.MaxValue = grains / 1000f;
    grainCount.Value = grainCount.MaxValue;
    gCount = (RichTextLabel)FindChild("UI").FindChild("ProgressBar").FindChild("GCounter").FindChild("GCount");
    rgCount = (RichTextLabel)FindChild("UI").FindChild("RGCounter").FindChild("RGCount");
    foreach (Entity entity in GetChildren().Where(c => c is Entity).Cast<Entity>())
    {
      entity.level = this;
    }
    UpdateRGCount();
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    newTime = Time.GetTicksMsec();
    if (grains > 0 && !timeout)
    {
      grains = Math.Max(grains - (long)(newTime - prevTime), 0);
    }
    UpdateGCount();
    prevTime = newTime;
  }

  public void UpdateGCount()
  {
    grainCount.Value = grains / 1000f;
    gCount.Text = $"{Math.Ceiling(grainCount.Value)}";
  }

  public void UpdateRGCount()
  {
    reverseGrains = GetChildren().Count(c => c is ReverseGrain rg && !rg.collected);
    rgCount.Text = $"{reverseGrains}";
  }
}
