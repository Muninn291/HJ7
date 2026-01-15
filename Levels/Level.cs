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
  private bool timeout;
  public bool Timeout
  {
    get => timeout;
    set
    {
      timeout = value;
      timeStopFilter.Visible = value;
    }
  }
  public ColorRect timeStopFilter;
  public Pause pause;
  public bool overtime = false;

  public override void _Ready()
  {
    prevTime = Time.GetTicksMsec();
    newTime = prevTime;
    grainCount = (ProgressBar)FindChild("UI").FindChild("ProgressBar");
    grainCount.MaxValue = grains / 1000f;
    grainCount.Value = grainCount.MaxValue;
    gCount = (RichTextLabel)FindChild("UI").FindChild("ProgressBar").FindChild("GCounter").FindChild("GCount");
    rgCount = (RichTextLabel)FindChild("UI").FindChild("RGCounter").FindChild("RGCount");
    timeStopFilter = (ColorRect)FindChild("UI").FindChild("TimeStopFilter");
    pause = (Pause)FindChild("Pause");
    foreach (Entity entity in GetChildren().Where(c => c is Entity).Cast<Entity>())
    {
      entity.level = this;
    }
    foreach (Entity entity in FindChild("AlwaysColor").GetChildren().Where(c => c is Entity).Cast<Entity>())
    {
      entity.level = this;
    }
    UpdateRGCount();
  }

  public override void _Process(double delta)
  {
    base._Process(delta);
    newTime = Time.GetTicksMsec();
    if (grains > 0 && !Timeout)
    {
      DecreaseGrains((long)(newTime - prevTime));
    }
    prevTime = newTime;
  }

  public void DecreaseGrains(long difference)
  {
    grains = Math.Max(grains - difference, 0);
    UpdateGCount();
    if (grains == 0)
    {
      Timeout = false;
      overtime = true;
    }
  }

  public void UpdateGCount()
  {
    grainCount.Value = grains / 1000f;
    if (grainCount.Value < 5 && grainCount.Value > 0)
    {
      gCount.Text = $"{Math.Round(grainCount.Value, 1, MidpointRounding.ToZero):N1}";
    }
    else
    {
      gCount.Text = $"{Math.Floor(grainCount.Value)}";
    }
  }

  public void UpdateRGCount()
  {
    reverseGrains = FindChild("AlwaysColor").GetChildren().Count(c => c is ReverseGrain rg && !rg.collected);
    if (reverseGrains <= 0)
    {
      pause.ShowVictory();
    }
    rgCount.Text = $"{reverseGrains}";
  }
}
