// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuItemContainerBehaviorFlags
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  internal enum QMenuItemContainerBehaviorFlags
  {
    None = 0,
    AlwaysAutoExpand = 1,
    HideExpandedItemOnClick = 2,
    DoNotDrawExpandedItemWhenHotItemShown = 4,
    StopAutoExpandWhenOverHotItem = 8,
    UseHasChildItemHotBoundsForExpension = 16, // 0x00000010
    ExpandItemOnMouseUp = 32, // 0x00000020
    CanIterateThroughHiddenItems = 64, // 0x00000040
    CanSimulateFocus = 128, // 0x00000080
    SimplePersonalizing = 256, // 0x00000100
  }
}
