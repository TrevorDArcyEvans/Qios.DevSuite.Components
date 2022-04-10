// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDrawControlBackgroundOptions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QDrawControlBackgroundOptions
  {
    None = 0,
    DrawLeftBorder = 1,
    DrawRightBorder = 2,
    DrawTopBorder = 4,
    DrawBottomBorder = 8,
    DrawAllBorders = DrawBottomBorder | DrawTopBorder | DrawRightBorder | DrawLeftBorder, // 0x0000000F
    NeverDrawBorders = 32, // 0x00000020
  }
}
