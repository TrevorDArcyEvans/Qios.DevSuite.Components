// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartShapePainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QPartShapePainter : QPartObjectPainter
  {
    private QPartBoundsType m_eDrawOnBounds = QPartBoundsType.Bounds;
    private QShapeAppearance m_oAppearance;
    private QShapePainterProperties m_oPainterProperties;
    private QAppearanceFillerProperties m_oFillerProperties;
    private QPainterOptions m_eOptions = QPainterOptions.Default;
    private GraphicsPath m_oLastDrawnGraphicsPath;
    private QColorSet m_oColorSet;

    public QPartShapePainter() => this.m_oColorSet = new QColorSet();

    public QPartBoundsType DrawOnBounds
    {
      get => this.m_eDrawOnBounds;
      set => this.m_eDrawOnBounds = value;
    }

    public QShapeAppearance Appearance
    {
      get => this.m_oAppearance;
      set => this.m_oAppearance = value;
    }

    public GraphicsPath LastDrawnGraphicsPath => this.m_oLastDrawnGraphicsPath;

    protected void PutLastDrawnGraphicsPath(GraphicsPath path) => this.m_oLastDrawnGraphicsPath = path;

    public QShapePainterProperties Properties
    {
      get => this.m_oPainterProperties;
      set => this.m_oPainterProperties = value;
    }

    public QAppearanceFillerProperties FillerProperties
    {
      get => this.m_oFillerProperties;
      set => this.m_oFillerProperties = value;
    }

    public QShapePainterProperties PropertiesToUse => this.m_oPainterProperties == null ? QShapePainterProperties.Default : this.m_oPainterProperties;

    public QAppearanceFillerProperties FillerPropertiesToUse => this.m_oFillerProperties == null ? QAppearanceFillerProperties.Default : this.m_oFillerProperties;

    public QPainterOptions Options
    {
      get => this.m_eOptions;
      set => this.m_eOptions = value;
    }

    public QColorSet ColorSet
    {
      get => this.m_oColorSet;
      set => this.m_oColorSet = value;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      this.m_oLastDrawnGraphicsPath = (GraphicsPath) null;
      if (!this.Enabled || this.m_oColorSet == null || this.m_oAppearance == null)
        return;
      this.m_oLastDrawnGraphicsPath = QShapePainter.Default.Paint(part.CalculatedProperties.GetBounds(this.m_eDrawOnBounds), this.m_oAppearance.Shape, (IQAppearance) this.m_oAppearance, this.m_oColorSet, this.PropertiesToUse, this.FillerPropertiesToUse, this.m_eOptions, paintContext.Graphics);
    }
  }
}
