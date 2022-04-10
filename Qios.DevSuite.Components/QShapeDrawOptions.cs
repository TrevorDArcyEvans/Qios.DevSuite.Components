// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeDrawOptions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QShapeDrawOptions
  {
    None = 0,
    FillBackground = 1,
    DrawBorder = 2,
    ExceedBackgroundWithBorder = 4,
    ReturnDrawnShape = 8,
    DefaultForFill = ExceedBackgroundWithBorder | FillBackground, // 0x00000005
    DefaultForDraw = DrawBorder, // 0x00000002
    Default = DefaultForDraw | DefaultForFill, // 0x00000007
  }
}
