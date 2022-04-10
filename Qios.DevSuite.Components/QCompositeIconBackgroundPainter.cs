// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeIconBackgroundPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QCompositeIconBackgroundPainter : QPartObjectPainter
  {
    private QPartBoundsType m_eDrawOnBounds = QPartBoundsType.Bounds;
    private QColorSet m_oColorSet;
    private int m_iSize;
    private QMargin m_oMargin;
    private QAppearanceWrapper m_oAppearance;
    private QPartShapePainter m_oClipOnPainter;

    public QCompositeIconBackgroundPainter()
    {
      this.m_oAppearance = new QAppearanceWrapper((IQAppearance) null);
      this.m_oAppearance.GradientAngle = 0;
      this.m_oAppearance.BorderWidth = 1;
      this.m_oAppearance.ShowBorderLeft = false;
      this.m_oAppearance.ShowBorderBottom = false;
      this.m_oAppearance.ShowBorderTop = false;
    }

    public QPartBoundsType DrawOnBounds
    {
      get => this.m_eDrawOnBounds;
      set => this.m_eDrawOnBounds = value;
    }

    public QPartShapePainter ClipOnPainter
    {
      get => this.m_oClipOnPainter;
      set => this.m_oClipOnPainter = value;
    }

    public int Size
    {
      get => this.m_iSize;
      set => this.m_iSize = value;
    }

    public QMargin Margin
    {
      get => this.m_oMargin;
      set => this.m_oMargin = value;
    }

    public QColorSet ColorSet
    {
      get => this.m_oColorSet;
      set => this.m_oColorSet = value;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled || part == null || this.m_oColorSet == null)
        return;
      Rectangle rectangle = part.CalculatedProperties.GetBounds(this.m_eDrawOnBounds);
      rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Left + this.m_iSize, rectangle.Bottom);
      rectangle = this.m_oMargin.InflateRectangleWithMargin(rectangle, false, true) with
      {
        Width = this.m_iSize
      };
      Region region = (Region) null;
      if (this.m_oClipOnPainter != null && this.m_oClipOnPainter.LastDrawnGraphicsPath != null)
      {
        region = paintContext.Graphics.Clip.Clone();
        paintContext.Graphics.SetClip(new Region(this.m_oClipOnPainter.LastDrawnGraphicsPath), CombineMode.Intersect);
      }
      QRectanglePainter.Default.Paint(rectangle, (IQAppearance) this.m_oAppearance, this.m_oColorSet, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, paintContext.Graphics);
      if (region == null)
        return;
      paintContext.Graphics.SetClip(region, CombineMode.Replace);
    }
  }
}
