// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartCalculateSizedReturnOptions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  internal enum QPartCalculateSizedReturnOptions
  {
    None = 0,
    SumWidth = 1,
    MaximumWidth = 2,
    SumHeight = 4,
    MaximumHeight = 8,
    DefaultForHorizontalDirection = MaximumHeight | SumWidth, // 0x00000009
    DefaultForVerticalDirection = SumHeight | MaximumWidth, // 0x00000006
    OuterSize = 16, // 0x00000010
    InnerSize = 32, // 0x00000020
    MinimumSize = 64, // 0x00000040
    MaximumSize = 128, // 0x00000080
  }
}
