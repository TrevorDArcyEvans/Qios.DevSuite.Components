// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemActivationOptions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QCompositeItemActivationOptions
  {
    None = 0,
    Activate = 1,
    Expand = 2,
    ExpandAndActivate = Expand | Activate, // 0x00000003
    FocusControl = 4,
    Automatic = 8,
  }
}
