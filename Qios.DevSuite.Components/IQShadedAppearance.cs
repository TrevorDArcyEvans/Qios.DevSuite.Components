// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQShadedAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public interface IQShadedAppearance
  {
    Point ShadeOffset { get; set; }

    int ShadeGradientSize { get; set; }

    bool ShadeVisible { get; set; }

    bool ShadeClipToShapeBounds { get; set; }

    QMargin ShadeClipMargin { get; set; }

    QPadding ShadeGrowPadding { get; set; }
  }
}
