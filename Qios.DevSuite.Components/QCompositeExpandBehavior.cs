// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeExpandBehavior
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QCompositeExpandBehavior
  {
    None = 0,
    AutoExpand = 1,
    NotAutoExpand = 2,
    AutoChangeExpand = 4,
    NotAutoChangeExpand = 8,
    PaintExpandedChildWhenHot = 16, // 0x00000010
    NotPaintExpandedChildWhenHot = 32, // 0x00000020
    ExpandOnNavigationKeys = 64, // 0x00000040
    NotExpandOnNavigationKeys = 128, // 0x00000080
    CloseOnNavigationKeys = 256, // 0x00000100
    NotCloseOnNavigationKeys = 512, // 0x00000200
    PositionOutsideComposite = 1024, // 0x00000400
    NotPositionOutsideComposite = 2048, // 0x00000800
    CloseExpandedItemOnClick = 4096, // 0x00001000
    NotCloseExpandedItemOnClick = 8192, // 0x00002000
  }
}
