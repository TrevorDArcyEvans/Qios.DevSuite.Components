// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDrawTextOptions
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [Flags]
  public enum QDrawTextOptions
  {
    None = 0,
    HidePrefix = 1048576, // 0x00100000
    IgnorePrefix = 2048, // 0x00000800
    PathEllipsis = 16384, // 0x00004000
    WordEllipsis = 262144, // 0x00040000
    EndEllipsis = 32768, // 0x00008000
  }
}
