// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QRectangleSide
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components.Design
{
  [Flags]
  internal enum QRectangleSide
  {
    None = 0,
    North = 1,
    South = 2,
    West = 4,
    East = 8,
    NorthEast = East | North, // 0x00000009
    SouthEast = East | South, // 0x0000000A
    NorthWest = West | North, // 0x00000005
    SouthWest = West | South, // 0x00000006
  }
}
