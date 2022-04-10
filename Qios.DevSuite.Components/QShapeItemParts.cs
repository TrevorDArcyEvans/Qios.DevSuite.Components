// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeItemParts
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QShapeItemParts
  {
    None = 0,
    Location = 1,
    ControlPoint1 = 2,
    ControlPoint2 = 4,
    Line = 8,
    AllPoints = ControlPoint2 | ControlPoint1 | Location, // 0x00000007
    All = AllPoints | Line, // 0x0000000F
  }
}
