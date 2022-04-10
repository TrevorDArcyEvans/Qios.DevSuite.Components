// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlStyles
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QControlStyles
  {
    None = 0,
    DoNotCopyBitsOnUserSize = 1,
    DoNotNotifyChangeOnUserSize = 2,
    DoNotRedrawOnUserSize = 4,
    DoNothingOnUserSize = DoNotRedrawOnUserSize | DoNotNotifyChangeOnUserSize | DoNotCopyBitsOnUserSize, // 0x00000007
    NeverCopyBitsOnBoundsChange = 8,
    NeverRedrawOnBoundsChange = 16, // 0x00000010
    DoNotInvalidateOnScroll = 32, // 0x00000020
  }
}
