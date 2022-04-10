// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRoundedRectanglePainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QRoundedRectanglePainter
  {
    private static QRoundedRectanglePainter m_oDefault;

    public static QRoundedRectanglePainter Default
    {
      get
      {
        if (QRoundedRectanglePainter.m_oDefault == null)
          QRoundedRectanglePainter.m_oDefault = new QRoundedRectanglePainter();
        return QRoundedRectanglePainter.m_oDefault;
      }
    }

    internal GraphicsPath GetRoundedRectanglePath(
      Rectangle rectangle,
      int cornerSize,
      QAppearanceForegroundOptions foregroundOptions,
      QDrawRoundedRectangleOptions roundedRectangleOptions)
    {
      int num1 = Math.Min(Math.Min(rectangle.Width, cornerSize * 2), rectangle.Height);
      if (num1 % 2 != 0)
        --num1;
      int num2 = (roundedRectangleOptions & QDrawRoundedRectangleOptions.TopLeft) == QDrawRoundedRectangleOptions.TopLeft ? num1 / 2 : 0;
      int num3 = (roundedRectangleOptions & QDrawRoundedRectangleOptions.TopRight) == QDrawRoundedRectangleOptions.TopRight ? num1 / 2 : 0;
      int num4 = (roundedRectangleOptions & QDrawRoundedRectangleOptions.BottomLeft) == QDrawRoundedRectangleOptions.BottomLeft ? num1 / 2 : 0;
      int num5 = (roundedRectangleOptions & QDrawRoundedRectangleOptions.BottomRight) == QDrawRoundedRectangleOptions.BottomRight ? num1 / 2 : 0;
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawTopBorder) != QAppearanceForegroundOptions.DrawTopBorder)
      {
        num2 = 0;
        num3 = 0;
      }
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawRightBorder) != QAppearanceForegroundOptions.DrawRightBorder)
      {
        num3 = 0;
        num5 = 0;
      }
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawBottomBorder) != QAppearanceForegroundOptions.DrawBottomBorder)
      {
        num5 = 0;
        num4 = 0;
      }
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawLeftBorder) != QAppearanceForegroundOptions.DrawLeftBorder)
      {
        num4 = 0;
        num2 = 0;
      }
      QLine qline1 = new QLine(new Point(rectangle.Left + num2, rectangle.Top), new Point(rectangle.Right - num3, rectangle.Top));
      QLine qline2 = new QLine(new Point(rectangle.Right, rectangle.Top + num3), new Point(rectangle.Right, rectangle.Bottom - num5));
      QLine qline3 = new QLine(new Point(rectangle.Right - num5, rectangle.Bottom), new Point(rectangle.Left + num4, rectangle.Bottom));
      QLine qline4 = new QLine(new Point(rectangle.Left, rectangle.Bottom - num4), new Point(rectangle.Left, rectangle.Top + num2));
      GraphicsPath roundedRectanglePath = new GraphicsPath();
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawTopBorder) == QAppearanceForegroundOptions.DrawTopBorder)
        roundedRectanglePath.AddLine(qline1.P1, qline1.P2);
      if (num3 > 0)
        roundedRectanglePath.AddArc(new Rectangle(qline1.P2.X - num3, qline1.P2.Y, num1, num1), 270f, 90f);
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawRightBorder) == QAppearanceForegroundOptions.DrawRightBorder)
        roundedRectanglePath.AddLine(qline2.P1, qline2.P2);
      else
        roundedRectanglePath.StartFigure();
      if (num5 > 0)
        roundedRectanglePath.AddArc(new Rectangle(qline2.P2.X - num5 * 2, qline2.P2.Y - num5, num1, num1), 0.0f, 90f);
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawBottomBorder) == QAppearanceForegroundOptions.DrawBottomBorder)
        roundedRectanglePath.AddLine(qline3.P1, qline3.P2);
      else
        roundedRectanglePath.StartFigure();
      if (num4 > 0)
        roundedRectanglePath.AddArc(new Rectangle(qline3.P2.X - num4, qline3.P2.Y - num4 * 2, num1, num1), 90f, 90f);
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawLeftBorder) == QAppearanceForegroundOptions.DrawLeftBorder)
        roundedRectanglePath.AddLine(qline4.P1, qline4.P2);
      if (num2 > 0)
        roundedRectanglePath.AddArc(new Rectangle(qline4.P2.X, qline1.P1.Y, num1, num1), 180f, 90f);
      if ((foregroundOptions & QAppearanceForegroundOptions.DrawAllBorders) == QAppearanceForegroundOptions.DrawAllBorders)
        roundedRectanglePath.CloseFigure();
      return roundedRectanglePath;
    }

    public void FillBackground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QRoundedRectanglePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      SmoothingMode smoothingMode = graphics != null ? graphics.SmoothingMode : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle = new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 1, bounds.Height + 1);
      Region savedRegion = QControlPaint.AdjustClip(graphics, new Region(bounds), CombineMode.Intersect);
      GraphicsPath roundedRectanglePath = this.GetRoundedRectanglePath(rectangle, properties.CornerSize, QAppearanceForegroundOptions.DrawAllBorders, properties.Options);
      QAppearanceFiller.Fillers[appearance.BackgroundStyle].FillPathBackground(bounds, roundedRectanglePath, appearance, colors, fillerProperties, graphics);
      roundedRectanglePath.Dispose();
      QControlPaint.RestoreClip(graphics, savedRegion);
      graphics.SmoothingMode = smoothingMode;
    }

    public void FillForeground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QRoundedRectanglePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      SmoothingMode smoothingMode = graphics != null ? graphics.SmoothingMode : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      GraphicsPath roundedRectanglePath = this.GetRoundedRectanglePath(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 1, bounds.Height + 1), properties.CornerSize, QAppearanceFiller.GetBorderOptions(appearance), properties.Options);
      QAppearanceFiller.Fillers[appearance.BackgroundStyle].FillPathForeground(bounds, roundedRectanglePath, appearance, colors, fillerProperties, graphics);
      roundedRectanglePath.Dispose();
      graphics.SmoothingMode = smoothingMode;
    }

    public void Paint(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QRoundedRectanglePainterProperties properties,
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
