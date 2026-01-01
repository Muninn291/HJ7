using Godot;
using System;
using System.Linq;

public partial class Level : Control
{
  [Export]
  public ulong grains = 60000;
  public int reverseGrains = -1;
  public RichTextLabel gCount;
  public RichTextLabel rgCount;
  public ProgressBar grainCount;
  public ulong prevTime = 0;
  public ulong newTime = 0;

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
    grains -= newTime - prevTime;
    grainCount.Value = grains / 1000f;
    prevTime = newTime;
    gCount.Text = $"{Math.Ceiling(grainCount.Value)}";
  }


  public void UpdateRGCount()
  {
    reverseGrains = GetChildren().Count(c => c is ReverseGrain rg && !rg.collected);
    rgCount.Text = $"{reverseGrains}";
  }
}
