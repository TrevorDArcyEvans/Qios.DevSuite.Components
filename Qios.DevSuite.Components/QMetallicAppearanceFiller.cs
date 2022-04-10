// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMetallicAppearanceFiller
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QMetallicAppearanceFiller : QAppearanceFiller
  {
    private const float SHINE_OVERLAP = 0.1f;
    private const float SHINE_HUE = 1f;
    private const float SHINE_BRIGHTNESS = 1f;
    private const float SHINE_SATURATION = 1f;

    public override object FillRectangleBackground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (!(appearance is IQMetallicAppearance appearance1))
        return (object) null;
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return (object) null;
      if (colors.Background1 == colors.Background2)
        return base.FillRectangleBackground(bounds, appearance, colors, properties, graphics);
      Rectangle boundsToUseForBrushes = QAppearanceFiller.GetBoundsToUseForBrushes(properties, bounds);
      Color color = colors.Background1;
      Color color2 = colors.Background2;
      if (appearance1.MetallicAutomaticColorOrder && (double) color2.GetBrightness() > (double) color.GetBrightness())
      {
        color = colors.Background2;
        color2 = colors.Background1;
      }
      float metallicOffsetFactor;
      Rectangle rect1;
      Rectangle rect2;
      int angle;
      if (appearance1.MetallicDirection == QMetallicAppearanceDirection.Horizontal)
      {
        metallicOffsetFactor = this.GetMetallicOffsetFactor(appearance1, boundsToUseForBrushes.Height);
        int metallicOffset = this.GetMetallicOffset(appearance1, boundsToUseForBrushes.Height);
        rect1 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top, boundsToUseForBrushes.Width, metallicOffset);
        Rectangle rectangle = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top + metallicOffset - 1, boundsToUseForBrushes.Width, boundsToUseForBrushes.Height - metallicOffset + 1);
        int num = (int) (0.100000001490116 * (double) rectangle.Width);
        rect2 = new Rectangle(rectangle.Left - num, rectangle.Top, rectangle.Width + 2 * num, rectangle.Height * 2);
        angle = 90;
      }
      else
      {
        metallicOffsetFactor = this.GetMetallicOffsetFactor(appearance1, boundsToUseForBrushes.Width);
        int metallicOffset = this.GetMetallicOffset(appearance1, boundsToUseForBrushes.Width);
        rect1 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top, metallicOffset, boundsToUseForBrushes.Height);
        Rectangle rectangle = new Rectangle(boundsToUseForBrushes.Left + metallicOffset - 1, boundsToUseForBrushes.Top, boundsToUseForBrushes.Width - metallicOffset + 1, boundsToUseForBrushes.Height);
        int num = (int) (0.100000001490116 * (double) rectangle.Height);
        rect2 = new Rectangle(rectangle.Left, rectangle.Top - num, rectangle.Width * 2, rectangle.Height + 2 * num);
        angle = 0;
      }
      float num1 = appearance1.MetallicNearIntensity > 0 ? (float) appearance1.MetallicNearIntensity / 100f : 0.0f;
      float num2 = appearance1.MetallicFarIntensity > 0 ? (float) appearance1.MetallicFarIntensity / 100f : 0.0f;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(boundsToUseForBrushes.Left - 1, boundsToUseForBrushes.Top - 1, boundsToUseForBrushes.Width + 2, boundsToUseForBrushes.Height + 2), color, color2, (float) angle);
      linearGradientBrush.Blend = new Blend()
      {
        Factors = new float[3]{ 0.0f, num1, num1 },
        Positions = new float[3]
        {
          0.0f,
          metallicOffsetFactor,
          1f
        }
      };
      graphics.FillRectangle((Brush) linearGradientBrush, bounds);
      linearGradientBrush.Blend = new Blend()
      {
        Factors = new float[3]{ 1f, 1f, num2 },
        Positions = new float[3]{ 0.0f, num2, 1f }
      };
      Region savedRegion = QControlPaint.AdjustClip(graphics, new Region(rect1), CombineMode.Exclude);
      graphics.FillRectangle((Brush) linearGradientBrush, bounds);
      if (!rect2.IsEmpty && appearance1.MetallicShineIntensity > 0)
      {
        int alpha = appearance1.MetallicShineIntensity != 100 ? (int) ((double) appearance1.MetallicShineIntensity / 100.0 * (double) byte.MaxValue) : (int) byte.MaxValue;
        float num3 = (float) appearance1.MetallicShineSaturation / 100f;
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(rect2);
        PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
        pathGradientBrush.CenterColor = Color.FromArgb(alpha, QMath.ModifyColor(color, 1f, 1f, num3 + 1f));
        pathGradientBrush.SurroundColors = new Color[1]
        {
          Color.Transparent
        };
        graphics.FillRectangle((Brush) pathGradientBrush, bounds);
        pathGradientBrush.Dispose();
        path.Dispose();
      }
      QControlPaint.RestoreClip(graphics, savedRegion);
      if (appearance1.MetallicInnerGlowWidth > 0)
      {
        Rectangle rectangle;
        if ((properties.BackgroundOptions & QAppearanceBackgroundOptions.InsetDoesNotDependOnBorder) != QAppearanceBackgroundOptions.InsetDoesNotDependOnBorder)
        {
          int borderWidth = QAppearanceFiller.GetBorderWidth(appearance);
          QAppearanceForegroundOptions borderOptions = QAppearanceFiller.GetBorderOptions(appearance);
          rectangle = new QPadding((borderOptions & QAppearanceForegroundOptions.DrawLeftBorder) == QAppearanceForegroundOptions.DrawLeftBorder ? borderWidth : 0, (borderOptions & QAppearanceForegroundOptions.DrawTopBorder) == QAppearanceForegroundOptions.DrawTopBorder ? borderWidth : 0, (borderOptions & QAppearanceForegroundOptions.DrawBottomBorder) == QAppearanceForegroundOptions.DrawBottomBorder ? borderWidth : 0, (borderOptions & QAppearanceForegroundOptions.DrawRightBorder) == QAppearanceForegroundOptions.DrawRightBorder ? borderWidth : 0).InflateRectangleWithPadding(bounds, false, true);
        }
        else
          rectangle = bounds;
        Brush borderBrush = (Brush) new SolidBrush(!colors.InnerGlow.IsEmpty ? colors.InnerGlow : color);
        QAppearanceFiller.DrawRectangleBorders(rectangle, borderBrush, appearance1.MetallicInnerGlowWidth, QAppearanceForegroundOptions.DrawAllBorders, graphics);
        borderBrush.Dispose();
      }
      linearGradientBrush.Dispose();
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
      if (!(appearance is IQMetallicAppearance appearance1))
        return (object) null;
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return (object) null;
      if (colors.Background1 == colors.Background2)
        return base.FillPathBackground(bounds, path, appearance, colors, properties, graphics);
      Color color = colors.Background1;
      Color color2 = colors.Background2;
      if (appearance1.MetallicAutomaticColorOrder && (double) color2.GetBrightness() > (double) color.GetBrightness())
      {
        color = colors.Background2;
        color2 = colors.Background1;
      }
      bool flag = (properties.BackgroundOptions & QAppearanceBackgroundOptions.DontExtendPathBackgroundWithBorder) == QAppearanceBackgroundOptions.DontExtendPathBackgroundWithBorder;
      int borderWidth1 = QAppearanceFiller.GetBorderWidth(appearance);
      Rectangle boundsToUseForBrushes = QAppearanceFiller.GetBoundsToUseForBrushes(properties, bounds);
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      if (appearance is IQShapeAppearance qshapeAppearance)
        graphics.SmoothingMode = QMisc.GetSmoothingMode(qshapeAppearance.SmoothingMode);
      float metallicOffsetFactor;
      Rectangle rect1;
      Rectangle rect2;
      int angle;
      if (appearance1.MetallicDirection == QMetallicAppearanceDirection.Horizontal)
      {
        metallicOffsetFactor = this.GetMetallicOffsetFactor(appearance1, boundsToUseForBrushes.Height);
        int metallicOffset = this.GetMetallicOffset(appearance1, boundsToUseForBrushes.Height);
        rect1 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top, boundsToUseForBrushes.Width, metallicOffset);
        Rectangle rectangle = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top + metallicOffset - 1, boundsToUseForBrushes.Width, boundsToUseForBrushes.Height - metallicOffset + 1);
        int num = (int) (0.100000001490116 * (double) rectangle.Width);
        rect2 = new Rectangle(rectangle.Left - num, rectangle.Top, rectangle.Width + 2 * num, rectangle.Height * 2);
        angle = 90;
      }
      else
      {
        metallicOffsetFactor = this.GetMetallicOffsetFactor(appearance1, boundsToUseForBrushes.Width);
        int metallicOffset = this.GetMetallicOffset(appearance1, boundsToUseForBrushes.Width);
        rect1 = new Rectangle(boundsToUseForBrushes.Left, boundsToUseForBrushes.Top, metallicOffset, boundsToUseForBrushes.Height);
        Rectangle rectangle = new Rectangle(boundsToUseForBrushes.Left + metallicOffset - 1, boundsToUseForBrushes.Top, boundsToUseForBrushes.Width - metallicOffset + 1, boundsToUseForBrushes.Height);
        int num = (int) (0.100000001490116 * (double) rectangle.Height);
        rect2 = new Rectangle(rectangle.Left, rectangle.Top - num, rectangle.Width * 2, rectangle.Height + 2 * num);
        angle = 0;
      }
      float num1 = appearance1.MetallicNearIntensity > 0 ? (float) appearance1.MetallicNearIntensity / 100f : 0.0f;
      float num2 = appearance1.MetallicFarIntensity > 0 ? (float) appearance1.MetallicFarIntensity / 100f : 0.0f;
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(boundsToUseForBrushes.Left - 1, boundsToUseForBrushes.Top - 1, boundsToUseForBrushes.Width + 2, boundsToUseForBrushes.Height + 2), color, color2, (float) angle);
      linearGradientBrush.Blend = new Blend()
      {
        Factors = new float[3]{ 0.0f, num1, num1 },
        Positions = new float[3]
        {
          0.0f,
          metallicOffsetFactor,
          1f
        }
      };
      Pen pen1 = (Pen) null;
      if (!flag && borderWidth1 > 0)
      {
        pen1 = new Pen((Brush) linearGradientBrush, (float) borderWidth1);
        graphics.DrawPath(pen1, path);
      }
      graphics.FillPath((Brush) linearGradientBrush, path);
      linearGradientBrush.Blend = new Blend()
      {
        Factors = new float[3]{ 1f, 1f, num2 },
        Positions = new float[3]{ 0.0f, num2, 1f }
      };
      Region savedRegion1 = QControlPaint.AdjustClip(graphics, new Region(rect1), CombineMode.Exclude);
      graphics.FillPath((Brush) linearGradientBrush, path);
      if (pen1 != null)
      {
        pen1.Brush = (Brush) linearGradientBrush;
        graphics.DrawPath(pen1, path);
        pen1.Dispose();
      }
      if (!rect2.IsEmpty && appearance1.MetallicShineIntensity > 0)
      {
        int alpha = appearance1.MetallicShineIntensity != 100 ? (int) ((double) appearance1.MetallicShineIntensity / 100.0 * (double) byte.MaxValue) : (int) byte.MaxValue;
        float num3 = (float) appearance1.MetallicShineSaturation / 100f;
        GraphicsPath path1 = new GraphicsPath();
        path1.AddEllipse(rect2);
        PathGradientBrush pathGradientBrush = new PathGradientBrush(path1);
        pathGradientBrush.CenterColor = !properties.UseAlternativeShineColor ? Color.FromArgb(alpha, QMath.ModifyColor(color, 1f, 1f, num3 + 1f)) : properties.AlternativeShineColor;
        pathGradientBrush.SurroundColors = new Color[1]
        {
          Color.Transparent
        };
        graphics.FillPath((Brush) pathGradientBrush, path);
        pathGradientBrush.Dispose();
        path1.Dispose();
      }
      QControlPaint.RestoreClip(graphics, savedRegion1);
      if (appearance1.MetallicInnerGlowWidth > 0)
      {
        int borderWidth2 = QAppearanceFiller.GetBorderWidth(appearance);
        Region savedRegion2 = (Region) null;
        if (borderWidth2 > 0)
          savedRegion2 = QControlPaint.AdjustClip(graphics, new Region(path), CombineMode.Intersect);
        Pen pen2 = new Pen(!colors.InnerGlow.IsEmpty ? colors.InnerGlow : color, borderWidth2 > 0 ? (float) (borderWidth2 + appearance1.MetallicInnerGlowWidth * 2) : (float) appearance1.MetallicInnerGlowWidth);
        graphics.DrawPath(pen2, path);
        pen2.Dispose();
        if (borderWidth2 > 0)
          QControlPaint.RestoreClip(graphics, savedRegion2);
      }
      linearGradientBrush.Dispose();
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
