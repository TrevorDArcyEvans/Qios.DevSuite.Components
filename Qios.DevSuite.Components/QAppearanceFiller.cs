// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppearanceFiller
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QAppearanceFiller
  {
    private static QAppearanceFillers m_oFillers;

    public static QAppearanceFillers Fillers
    {
      get
      {
        if (QAppearanceFiller.m_oFillers == null)
          QAppearanceFiller.m_oFillers = new QAppearanceFillers();
        return QAppearanceFiller.m_oFillers;
      }
    }

    internal static int GetBorderWidth(IQAppearance appearance) => appearance is IQBorderedAppearance qborderedAppearance ? qborderedAppearance.BorderWidth : 0;

    internal static QAppearanceForegroundOptions GetBorderOptions(
      IQAppearance appearance)
    {
      IQBorderedAdvancedAppearance advancedAppearance = appearance as IQBorderedAdvancedAppearance;
      IQBorderedAppearance qborderedAppearance = appearance as IQBorderedAppearance;
      if (advancedAppearance != null)
      {
        if (!advancedAppearance.ShowBorders)
          return QAppearanceForegroundOptions.None;
        QAppearanceForegroundOptions borderOptions = QAppearanceForegroundOptions.None;
        if (advancedAppearance.ShowBorderLeft)
          borderOptions |= QAppearanceForegroundOptions.DrawLeftBorder;
        if (advancedAppearance.ShowBorderTop)
          borderOptions |= QAppearanceForegroundOptions.DrawTopBorder;
        if (advancedAppearance.ShowBorderRight)
          borderOptions |= QAppearanceForegroundOptions.DrawRightBorder;
        if (advancedAppearance.ShowBorderBottom)
          borderOptions |= QAppearanceForegroundOptions.DrawBottomBorder;
        return borderOptions;
      }
      return qborderedAppearance != null ? QAppearanceForegroundOptions.DrawAllBorders : QAppearanceForegroundOptions.None;
    }

    internal float GetMetallicOffsetFactor(IQMetallicAppearance appearance, int size)
    {
      if (appearance.MetallicOffset > 0)
      {
        if (appearance.MetallicOffsetUnit == QAppearanceUnit.Percent)
          return (float) appearance.MetallicOffset / 100f;
        if (appearance.MetallicOffsetUnit == QAppearanceUnit.Pixel)
          return (float) appearance.MetallicOffset / (float) size;
      }
      return 0.0f;
    }

    internal int GetMetallicOffset(IQMetallicAppearance appearance, int size)
    {
      if (appearance.MetallicOffset > 0)
      {
        if (appearance.MetallicOffsetUnit == QAppearanceUnit.Percent)
          return (int) Math.Round((double) appearance.MetallicOffset / 100.0 * (double) size);
        if (appearance.MetallicOffsetUnit == QAppearanceUnit.Pixel)
          return appearance.MetallicOffset;
      }
      return 0;
    }

    public static Rectangle GetBoundsToUseForBrushes(
      QAppearanceFillerProperties properties,
      Rectangle bounds)
    {
      return properties.UseAlternativeBoundsForBrushCreation ? properties.AlternativeBoundsForBrushCreation : bounds;
    }

    internal static void DrawRectangleBorders(
      Rectangle rectangle,
      Brush borderBrush,
      int borderWidth,
      QAppearanceForegroundOptions options,
      Graphics graphics)
    {
      if (options == QAppearanceForegroundOptions.None)
        return;
      bool flag1 = (options & QAppearanceForegroundOptions.DrawLeftBorder) == QAppearanceForegroundOptions.DrawLeftBorder;
      bool flag2 = (options & QAppearanceForegroundOptions.DrawTopBorder) == QAppearanceForegroundOptions.DrawTopBorder;
      bool flag3 = (options & QAppearanceForegroundOptions.DrawBottomBorder) == QAppearanceForegroundOptions.DrawBottomBorder;
      bool flag4 = (options & QAppearanceForegroundOptions.DrawRightBorder) == QAppearanceForegroundOptions.DrawRightBorder;
      if (flag2)
        graphics.FillRectangle(borderBrush, rectangle.Left, rectangle.Top, rectangle.Width, borderWidth);
      if (flag3)
        graphics.FillRectangle(borderBrush, rectangle.Left, rectangle.Bottom - borderWidth, rectangle.Width, borderWidth);
      if (flag1)
        graphics.FillRectangle(borderBrush, rectangle.Left, rectangle.Top, borderWidth, rectangle.Height);
      if (!flag4)
        return;
      graphics.FillRectangle(borderBrush, rectangle.Right - borderWidth, rectangle.Top, borderWidth, rectangle.Height);
    }

    public virtual object FillRectangleBackground(
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
      if (colors.Background1 == Color.Empty)
        return (object) null;
      Brush brush = (Brush) new SolidBrush(colors.Background1);
      graphics.FillRectangle(brush, bounds);
      brush.Dispose();
      return (object) null;
    }

    public virtual object FillRectangleForeground(
      Rectangle bounds,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      QAppearanceForegroundOptions borderOptions = QAppearanceFiller.GetBorderOptions(appearance);
      int borderWidth = QAppearanceFiller.GetBorderWidth(appearance);
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return (object) null;
      if (borderWidth <= 0)
        return (object) null;
      if (colors.Border == Color.Empty)
        return (object) null;
      if (borderOptions == QAppearanceForegroundOptions.None)
        return (object) null;
      Brush borderBrush = (Brush) new SolidBrush(colors.Border);
      QAppearanceFiller.DrawRectangleBorders(bounds, borderBrush, borderWidth, borderOptions, graphics);
      borderBrush.Dispose();
      return (object) null;
    }

    public virtual object FillPathBackground(
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
      if (colors.Background1 == Color.Empty)
        return (object) null;
      bool flag = (properties.BackgroundOptions & QAppearanceBackgroundOptions.DontExtendPathBackgroundWithBorder) == QAppearanceBackgroundOptions.DontExtendPathBackgroundWithBorder;
      int borderWidth = QAppearanceFiller.GetBorderWidth(appearance);
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      if (appearance is IQShapeAppearance qshapeAppearance)
        graphics.SmoothingMode = QMisc.GetSmoothingMode(qshapeAppearance.SmoothingMode);
      Brush brush = (Brush) new SolidBrush(colors.Background1);
      graphics.FillPath(brush, path);
      if (!flag && borderWidth > 0)
      {
        Pen pen = new Pen(brush, (float) borderWidth);
        graphics.DrawPath(pen, path);
        pen.Dispose();
      }
      brush.Dispose();
      graphics.SmoothingMode = smoothingMode;
      return (object) null;
    }

    public virtual object FillPathForeground(
      Rectangle bounds,
      GraphicsPath path,
      IQAppearance appearance,
      QColorSet colors,
      QAppearanceFillerProperties properties,
      Graphics graphics)
    {
      int borderWidth = QAppearanceFiller.GetBorderWidth(appearance);
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return (object) null;
      if (borderWidth <= 0)
        return (object) null;
      if (colors.Border == Color.Empty)
        return (object) null;
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      if (appearance is IQShapeAppearance qshapeAppearance)
        graphics.SmoothingMode = QMisc.GetSmoothingMode(qshapeAppearance.SmoothingMode);
      Pen pen = new Pen(colors.Border, (float) borderWidth);
      graphics.DrawPath(pen, path);
      pen.Dispose();
      graphics.SmoothingMode = smoothingMode;
      return (object) null;
    }
  }
}
