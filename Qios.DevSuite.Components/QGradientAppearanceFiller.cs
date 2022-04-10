// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QGradientAppearanceFiller
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QGradientAppearanceFiller : QAppearanceFiller
  {
    protected Brush CreateGradientBrush(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties)
    {
      if (!(appearance is IQGradientAppearance qgradientAppearance))
        return (Brush) null;
      Rectangle boundsToUseForBrushes = QAppearanceFiller.GetBoundsToUseForBrushes(properties, bounds);
      LinearGradientBrush gradientBrush = new LinearGradientBrush(new Rectangle(boundsToUseForBrushes.Left - 1, boundsToUseForBrushes.Top - 1, boundsToUseForBrushes.Width + 2, boundsToUseForBrushes.Height + 2), colors.Background1, colors.Background2, (float) qgradientAppearance.GradientAngle, false);
      Blend blend = new Blend();
      float num1 = qgradientAppearance.GradientBlendFactor > 0 ? (float) qgradientAppearance.GradientBlendFactor / 100f : 0.0f;
      if ((double) num1 > 1.0)
        num1 = 1f;
      blend.Factors = new float[3]{ 0.0f, num1, 1f };
      float num2 = qgradientAppearance.GradientBlendPosition > 0 ? (float) qgradientAppearance.GradientBlendPosition / 100f : 0.0f;
      if ((double) num2 > 1.0)
        num2 = 1f;
      blend.Positions = new float[3]{ 0.0f, num2, 1f };
      gradientBrush.Blend = blend;
      return (Brush) gradientBrush;
    }

    public override object FillRectangleBackground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return (object) null;
      if (colors.Background1 == colors.Background2)
        return base.FillRectangleBackground(bounds, appearance, colors, properties, graphics);
      Brush gradientBrush = this.CreateGradientBrush(bounds, appearance, colors, properties);
      if (gradientBrush == null)
        return (object) null;
      graphics.FillRectangle(gradientBrush, bounds);
      gradientBrush.Dispose();
      return (object) null;
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
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return (object) null;
      if (colors.Background1 == colors.Background2)
        return base.FillPathBackground(bounds, path, appearance, colors, properties, graphics);
      bool flag = (properties.BackgroundOptions & QAppearanceBackgroundOptions.DontExtendPathBackgroundWithBorder) == QAppearanceBackgroundOptions.DontExtendPathBackgroundWithBorder;
      int borderWidth = QAppearanceFiller.GetBorderWidth(appearance);
      Brush gradientBrush = this.CreateGradientBrush(bounds, appearance, colors, properties);
      if (gradientBrush == null)
        return (object) null;
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      if (appearance is IQShapeAppearance qshapeAppearance)
        graphics.SmoothingMode = QMisc.GetSmoothingMode(qshapeAppearance.SmoothingMode);
      graphics.FillPath(gradientBrush, path);
      if (!flag && borderWidth > 0)
      {
        Pen pen = new Pen(gradientBrush, (float) borderWidth);
        graphics.DrawPath(pen, path);
        pen.Dispose();
      }
      gradientBrush.Dispose();
      graphics.SmoothingMode = smoothingMode;
      return (object) null;
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
