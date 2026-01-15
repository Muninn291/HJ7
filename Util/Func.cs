using Godot;
using System;

public static class Func
{
  public static int SignNotZero(this float value)
  {
    int sign = Math.Sign(value);
    return sign == 0 ? 1 : sign;
  }

  public static Node ChangeMainScene(Node newScene, bool deleteOld)
  {
    Node oldScene = Global.Instance.GetTree().Root.FindChild("Main_*", false, false);
    Callable.From(() =>
      {
        Global.Instance.GetTree().Root.AddChild(newScene);
        // if (newScene is MainLevel newOverworld) {
        // 	Global.Instance.overworld = newOverworld;
        // }
        Global.Instance.GetTree().Root.RemoveChild(oldScene);
        if (deleteOld)
        {
          oldScene.QueueFree();
        }
      }).CallDeferred();

    return deleteOld ? null : oldScene;
  }

  public static void RetryLevel()
  {
    Node oldScene = Global.Instance.GetTree().Root.FindChild("Main_*", false, false);
    Node newScene = GD.Load<PackedScene>($"res://Levels/{oldScene.Name}.tscn").Instantiate();
    Callable.From(() =>
      {
        Global.Instance.GetTree().Root.RemoveChild(oldScene);
        Global.Instance.GetTree().Root.AddChild(newScene);
        oldScene.QueueFree();
      }).CallDeferred();

    return;
  }
}