// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartSelectionTypes
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QPartSelectionTypes
  {
    None = 0,
    HorizontalNearAligned = 1,
    HorizontalCenterAligned = 2,
    HorizontalFarAligned = 4,
    VerticalNearAligned = 8,
    VerticalCenterAligned = 16, // 0x00000010
    VerticalFarAligned = 32, // 0x00000020
    StretchHorizontal = 64, // 0x00000040
    StretchVertical = 128, // 0x00000080
    ShrinkHorizontal = 256, // 0x00000100
    ShrinkVertical = 512, // 0x00000200
    SetToMaximumWidth = 1024, // 0x00000400
    SetToMinimumWidth = 2048, // 0x00000800
    SetToMaximumHeight = 4096, // 0x00001000
    SetToMinimumHeight = 8192, // 0x00002000
    NotSetToMaximumWidth = 16384, // 0x00004000
    NotSetToMinimumWidth = 32768, // 0x00008000
    NotSetToMaximumHeight = 65536, // 0x00010000
    NotSetToMinimumHeight = 131072, // 0x00020000
    NotStretchHorizontal = 262144, // 0x00040000
    NotStretchVertical = 524288, // 0x00080000
    NotShrinkHorizontal = 1048576, // 0x00100000
    NotShrinkVertical = 2097152, // 0x00200000
    AllHorizontalAligned = HorizontalFarAligned | HorizontalCenterAligned | HorizontalNearAligned, // 0x00000007
    AllVerticalAligned = VerticalFarAligned | VerticalCenterAligned | VerticalNearAligned, // 0x00000038
    All = -1, // 0xFFFFFFFF
  }
}
