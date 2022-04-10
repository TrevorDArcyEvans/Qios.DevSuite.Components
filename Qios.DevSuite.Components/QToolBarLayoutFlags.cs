// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarLayoutFlags
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  internal enum QToolBarLayoutFlags
  {
    None = 0,
    FixedWidth = 1,
    FixedHeight = 2,
    MaximumWidth = 4,
    DoNotSetBounds = 16, // 0x00000010
    DoNotStretch = 32, // 0x00000020
  }
}
