// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartLayoutFlags
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QPartLayoutFlags
  {
    None = 0,
    WidthConstraintApplied = 1,
    HeightConstraintApplied = 2,
    IsTableRow = 4,
    IsTableCell = 8,
    OverriddenVisible = 16, // 0x00000010
  }
}
