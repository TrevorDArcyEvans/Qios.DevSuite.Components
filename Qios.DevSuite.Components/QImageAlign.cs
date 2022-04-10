// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QImageAlign
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QImageAlignConverter))]
  public enum QImageAlign
  {
    RepeatedVertical,
    RepeatedHorizontal,
    RepeatedBoth,
    Stretched,
    Centered,
    TopLeft,
    CenterLeft,
    BottomLeft,
    TopRight,
    CenterRight,
    BottomRight,
    TopMiddle,
    BottomMiddle,
  }
}
