// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QSolidAppearanceFiller
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QSolidAppearanceFiller : QAppearanceFiller
  {
    public override object FillRectangleBackground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      return base.FillRectangleBackground(bounds, appearance, colors, properties, graphics);
    }

    public override object FillRectangleForeground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      return base.FillRectangleForeground(bounds, appearance, colors, properties, graphics);
    }

    public override object FillPathBackground(
      Rectangle bounds,
      GraphicsPath path,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      return base.FillPathBackground(bounds, path, appearance, colors, properties, graphics);
    }

    public override object FillPathForeground(
      Rectangle bounds,
      GraphicsPath path,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      return base.FillPathForeground(bounds, path, appearance, colors, properties, graphics);
    }
  }
}
