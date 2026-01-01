using System;

public static class Func
{
  public static int SignNotZero(this float value)
  {
    int sign = Math.Sign(value);
    return sign == 0 ? 1 : sign;
  }
}