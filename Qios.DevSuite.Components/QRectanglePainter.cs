// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRectanglePainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QRectanglePainter
  {
    private static QRectanglePainter m_oDefault;

    public static QRectanglePainter Default
    {
      get
      {
        if (QRectanglePainter.m_oDefault == null)
          QRectanglePainter.m_oDefault = new QRectanglePainter();
        return QRectanglePainter.m_oDefault;
      }
    }

    public void FillBackground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QRectanglePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      QAppearanceFiller.Fillers[appearance.BackgroundStyle].FillRectangleBackground(bounds, appearance, colors, fillerProperties, graphics);
    }

    public void FillForeground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QRectanglePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      QAppearanceFiller.Fillers[appearance.BackgroundStyle].FillRectangleForeground(bounds, appearance, colors, fillerProperties, graphics);
    }

    public void Paint(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QRectanglePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      if ((options & QPainterOptions.FillBackground) == QPainterOptions.FillBackground)
        this.FillBackground(bounds, appearance, colors, properties, fillerProperties, options, graphics);
      if ((options & QPainterOptions.FillForeground) != QPainterOptions.FillForeground)
        return;
      this.FillForeground(bounds, appearance, colors, properties, fillerProperties, options, graphics);
    }
  }
}
