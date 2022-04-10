// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartFocusRectanglePainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QPartFocusRectanglePainter : QPartObjectPainter
  {
    private QMargin m_oFocusRectangleMargin;
    private QPadding m_oFocusRectanglePadding;

    public QPadding FocusRectanglePadding
    {
      get => this.m_oFocusRectanglePadding;
      set => this.m_oFocusRectanglePadding = value;
    }

    public QMargin FocusRectangleMargin
    {
      get => this.m_oFocusRectangleMargin;
      set => this.m_oFocusRectangleMargin = value;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled)
        return;
      Rectangle rectangle = this.FocusRectanglePadding.InflateRectangleWithPadding(this.FocusRectangleMargin.InflateRectangleWithMargin(part.CalculatedProperties.GetBounds(QPartBoundsType.Bounds), true, true), false, true);
      ControlPaint.DrawFocusRectangle(paintContext.Graphics, rectangle);
    }
  }
}
