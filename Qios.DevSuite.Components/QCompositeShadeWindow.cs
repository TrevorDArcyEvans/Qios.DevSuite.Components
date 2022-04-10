// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeShadeWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QCompositeShadeWindow : QControlShadeWindow
  {
    private QShape m_oShape;
    private Rectangle m_oShapeBounds;

    public QCompositeShadeWindow(Control control, IWin32Window ownerWindow)
      : base(control, ownerWindow)
    {
    }

    internal QShape Shape
    {
      get => this.m_oShape;
      set => this.m_oShape = value;
    }

    internal Rectangle ShapeBounds
    {
      get => this.m_oShapeBounds;
      set => this.m_oShapeBounds = value;
    }

    internal QCompositeWindow FloatingCompositeWindow => this.Control as QCompositeWindow;

    protected override void PaintShade(PaintEventArgs pevent)
    {
      if (this.m_oShape == null)
      {
        base.PaintShade(pevent);
      }
      else
      {
        QControlPaint.DrawShapeShade((IQShadedShapeAppearance) new QAppearanceWrapper((IQAppearance) null)
        {
          ShadeOffset = new Point(this.ShadeOffsetTopLeft.X + this.ShadeOffsetBottomRight.X, this.ShadeOffsetTopLeft.Y + this.ShadeOffsetBottomRight.Y),
          ShadeVisible = true,
          ShadeGradientSize = this.GradientLength,
          Shape = this.Shape
        }, this.m_oShapeBounds, DockStyle.None, Color.FromArgb(98, (int) this.ShadeColor.R, (int) this.ShadeColor.G, (int) this.ShadeColor.B), pevent.Graphics);
        QCompositeWindow floatingCompositeWindow = this.FloatingCompositeWindow;
        if (floatingCompositeWindow == null || !floatingCompositeWindow.Configuration.ShapeWindow)
          return;
        QPartPaintContext fromControl = QPartPaintContext.CreateFromControl((Control) floatingCompositeWindow, pevent.Graphics);
        fromControl.RecursiveLevels = floatingCompositeWindow.LevelsToPaintOnShadeWindow;
        this.FloatingCompositeWindow.Composite.PaintComposite(fromControl);
        fromControl.Dispose();
      }
    }

    protected override void UpdateShadeBounds(int setWindowPosFlags)
    {
      if (this.Shape != null)
      {
        NativeMethods.SetWindowPos(this.Handle, this.Control.Handle, this.Control.Left, this.Control.Top, this.Control.Width + this.ShadeOffsetBottomRight.X + this.ShadeOffsetTopLeft.X, this.Control.Height + this.ShadeOffsetBottomRight.Y + this.ShadeOffsetTopLeft.Y, (uint) setWindowPosFlags);
        this.UpdateBounds();
      }
      else
        base.UpdateShadeBounds(setWindowPosFlags);
    }
  }
}
