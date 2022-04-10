// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QPartApplicationButtonPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QPartApplicationButtonPainter : QPartObjectPainter
  {
    private const int ShadeDistance = 2;
    private const int Padding = 2;
    private QColorSet m_oColorSet;
    private Size m_oSize = Size.Empty;
    private QAppearance m_oAppearance;
    private QAppearanceFillerProperties m_oProperties;

    public QPartApplicationButtonPainter()
    {
      this.m_oAppearance = new QAppearance();
      this.m_oAppearance.GradientAngle = 90;
      this.m_oAppearance.MetallicOffset = 50;
      this.m_oAppearance.BorderWidth = 1;
      this.m_oProperties = new QAppearanceFillerProperties();
      this.m_oProperties.UseAlternativeShineColor = true;
    }

    public Size Size
    {
      get => this.m_oSize;
      set => this.m_oSize = value;
    }

    public QColorSet ColorSet
    {
      get => this.m_oColorSet;
      set => this.m_oColorSet = value;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled || part == null)
        return;
      Rectangle bounds = part.CalculatedProperties.GetBounds(QPartBoundsType.Bounds);
      if (!this.Size.IsEmpty)
        bounds.Size = this.Size;
      bounds.X += 2;
      bounds.Y += 2;
      bounds.Width -= 2;
      bounds.Height -= 2;
      SmoothingMode smoothingMode = paintContext.Graphics.SmoothingMode;
      paintContext.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 2, bounds.Height - 2);
      GraphicsPath path1 = new GraphicsPath();
      path1.AddEllipse(rectangle);
      PathGradientBrush pathGradientBrush1 = new PathGradientBrush(path1);
      pathGradientBrush1.CenterColor = this.ColorSet.Border;
      pathGradientBrush1.SurroundColors = new Color[1]
      {
        Color.Empty
      };
      pathGradientBrush1.FocusScales = new PointF(0.85f, 0.85f);
      paintContext.Graphics.FillPath((Brush) pathGradientBrush1, path1);
      pathGradientBrush1.Dispose();
      rectangle = new Rectangle(bounds.X, bounds.Y, bounds.Width - 2, bounds.Height - 2);
      path1.Reset();
      path1.AddEllipse(rectangle);
      QColorSet colors = new QColorSet(Color.White, this.ColorSet.Foreground, this.ColorSet.Border);
      QGradientAppearanceFiller appearanceFiller = new QGradientAppearanceFiller();
      appearanceFiller.FillPathBackground(rectangle, path1, (IQAppearance) this.m_oAppearance, colors, this.m_oProperties, paintContext.Graphics);
      appearanceFiller.FillPathForeground(rectangle, path1, (IQAppearance) this.m_oAppearance, colors, this.m_oProperties, paintContext.Graphics);
      rectangle = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 6, bounds.Height - 6);
      path1.Reset();
      path1.AddEllipse(rectangle);
      ++rectangle.Width;
      this.m_oProperties.AlternativeShineColor = this.ColorSet.Foreground;
      new QMetallicAppearanceFiller().FillPathBackground(rectangle, path1, (IQAppearance) this.m_oAppearance, this.ColorSet, this.m_oProperties, paintContext.Graphics);
      --rectangle.Width;
      GraphicsPath path2 = new GraphicsPath();
      rectangle.Offset(-1, rectangle.Height / 2);
      path2.AddEllipse(rectangle);
      PathGradientBrush pathGradientBrush2 = new PathGradientBrush(path2);
      pathGradientBrush2.InterpolationColors = new ColorBlend(3)
      {
        Positions = new float[3]{ 0.0f, 0.6f, 1f },
        Colors = new Color[3]
        {
          Color.Transparent,
          Color.Transparent,
          Color.White
        }
      };
      paintContext.Graphics.FillPath((Brush) pathGradientBrush2, path1);
      pathGradientBrush2.Dispose();
      pathGradientBrush2.Dispose();
      path1.Dispose();
      paintContext.Graphics.SmoothingMode = smoothingMode;
    }
  }
}
