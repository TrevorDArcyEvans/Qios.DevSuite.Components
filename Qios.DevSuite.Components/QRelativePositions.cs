// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRelativePositions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QRelativePositions
  {
    None = 0,
    Above = 1,
    Below = 2,
    Vertical = Below | Above, // 0x00000003
    Left = 4,
    Right = 8,
    Horizontal = Right | Left, // 0x0000000C
  }
}
