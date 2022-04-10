// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapePainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QShapePainter
  {
    private static QShapePainter m_oDefault;

    public static QShapePainter Default
    {
      get
      {
        if (QShapePainter.m_oDefault == null)
          QShapePainter.m_oDefault = new QShapePainter();
        return QShapePainter.m_oDefault;
      }
    }

    public GraphicsPath FillBackground(
      Rectangle bounds,
      QShape shape,
      IQAppearance appearance,
      QColorSet colors,
      QShapePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      bool flag = (properties.Options & QShapePainterOptions.ReturnDrawnShape) == QShapePainterOptions.ReturnDrawnShape;
      if (colors.Background1 == Color.Empty && colors.Background2 == Color.Empty && !flag)
        return (GraphicsPath) null;
      if (!bounds.Size.IsEmpty && properties != null && (properties.Options & QShapePainterOptions.StayWithinBounds) == QShapePainterOptions.StayWithinBounds)
      {
        --bounds.Width;
        --bounds.Height;
      }
      GraphicsPath graphicsPath = shape.CreateGraphicsPath(bounds, fillerProperties.DockStyle, QShapePathOptions.AllLines, properties.Matrix);
      if (graphicsPath == null)
        return (GraphicsPath) null;
      QAppearanceFiller.Fillers[appearance.BackgroundStyle].FillPathBackground(bounds, graphicsPath, appearance, colors, fillerProperties, graphics);
      if (flag)
        return graphicsPath;
      graphicsPath.Dispose();
      return (GraphicsPath) null;
    }

    public GraphicsPath FillForeground(
      Rectangle bounds,
      QShape shape,
      IQAppearance appearance,
      QColorSet colors,
      QShapePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      bool flag = (properties.Options & QShapePainterOptions.ReturnDrawnShape) == QShapePainterOptions.ReturnDrawnShape;
      if (colors.Border == Color.Empty && !flag)
        return (GraphicsPath) null;
      if (!bounds.Size.IsEmpty && properties != null && (properties.Options & QShapePainterOptions.StayWithinBounds) == QShapePainterOptions.StayWithinBounds)
      {
        --bounds.Width;
        --bounds.Height;
      }
      GraphicsPath graphicsPath = shape.CreateGraphicsPath(bounds, fillerProperties.DockStyle, QShapePathOptions.VisibleLines, properties.Matrix);
      if (graphicsPath == null)
        return (GraphicsPath) null;
      QAppearanceFiller.Fillers[appearance.BackgroundStyle].FillPathForeground(bounds, graphicsPath, appearance, colors, fillerProperties, graphics);
      if (flag)
        return graphicsPath;
      graphicsPath.Dispose();
      return (GraphicsPath) null;
    }

    public GraphicsPath Paint(
      Rectangle bounds,
      QShape shape,
      IQAppearance appearance,
      QColorSet colors,
      QShapePainterProperties properties,
      QAppearanceFillerProperties fillerProperties,
      QPainterOptions options,
      Graphics graphics)
    {
      GraphicsPath graphicsPath1 = (GraphicsPath) null;
      GraphicsPath graphicsPath2 = (GraphicsPath) null;
      if ((options & QPainterOptions.FillBackground) == QPainterOptions.FillBackground)
        graphicsPath1 = this.FillBackground(bounds, shape, appearance, colors, properties, fillerProperties, options, graphics);
      if ((options & QPainterOptions.FillForeground) == QPainterOptions.FillForeground)
      {
        graphicsPath2 = this.FillForeground(bounds, shape, appearance, colors, properties, fillerProperties, options, graphics);
        if (graphicsPath1 != null && graphicsPath2 != null)
        {
          graphicsPath2.Dispose();
          graphicsPath2 = (GraphicsPath) null;
        }
      }
      return graphicsPath1 ?? graphicsPath2;
    }
  }
}
