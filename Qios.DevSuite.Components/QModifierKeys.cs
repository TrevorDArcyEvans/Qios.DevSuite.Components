// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QModifierKeys
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QModifierKeys
  {
    None = 0,
    Shift = 65536, // 0x00010000
    Alt = 262144, // 0x00040000
    Control = 131072, // 0x00020000
  }
}
