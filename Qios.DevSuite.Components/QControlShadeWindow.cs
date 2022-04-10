// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlShadeWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QControlShadeWindow : QTranslucentWindow
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private Control m_oControl;
    private Color m_oShadeColor = Color.Empty;
    private Region m_oShadeRegion;
    private Point m_oShadeOffsetTopLeft = Point.Empty;
    private Point m_oShadeOffsetBottomRight = Point.Empty;
    private int m_iCornerSize;
    private int m_iGradientLength;
    private QWeakDelegate m_oShadePainting;
    private QWeakDelegate m_oShadePainted;

    public QControlShadeWindow(Control control, IWin32Window ownerWindow)
      : base(QTranslucentWindowOptions.None)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oControl = control;
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_SizeChanged), (object) this.m_oControl, "SizeChanged"));
      this.OwnerWindow = ownerWindow;
      this.SetLayered(true, false);
      this.UpdateShadeBounds();
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the shade is about to be painted")]
    public event PaintEventHandler ShadePainting
    {
      add => this.m_oShadePainting = QWeakDelegate.Combine(this.m_oShadePainting, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oShadePainting = QWeakDelegate.Remove(this.m_oShadePainting, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the shade is painted")]
    public event PaintEventHandler ShadePainted
    {
      add => this.m_oShadePainted = QWeakDelegate.Combine(this.m_oShadePainted, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oShadePainted = QWeakDelegate.Remove(this.m_oShadePainted, (Delegate) value);
    }

    protected virtual void OnShadePainting(PaintEventArgs e) => this.m_oShadePainting = QWeakDelegate.InvokeDelegate(this.m_oShadePainting, (object) this, (object) e);

    protected virtual void OnShadePainted(PaintEventArgs e) => this.m_oShadePainted = QWeakDelegate.InvokeDelegate(this.m_oShadePainted, (object) this, (object) e);

    protected Control Control => this.m_oControl;

    public override Color BackColor
    {
      get => Color.Transparent;
      set
      {
      }
    }

    public Point ShadeOffsetTopLeft
    {
      get => this.m_oShadeOffsetTopLeft;
      set
      {
        if (!(this.m_oShadeOffsetTopLeft != value))
          return;
        this.m_oShadeOffsetTopLeft = value;
        this.UpdateShadeBounds();
      }
    }

    public Point ShadeOffsetBottomRight
    {
      get => this.m_oShadeOffsetBottomRight;
      set
      {
        if (!(this.m_oShadeOffsetBottomRight != value))
          return;
        this.m_oShadeOffsetBottomRight = value;
        this.UpdateShadeBounds();
      }
    }

    public Region ShadeRegion
    {
      get => this.m_oShadeRegion;
      set
      {
        if (this.m_oShadeRegion != null && this.m_oShadeRegion != value)
          this.m_oShadeRegion.Dispose();
        this.m_oShadeRegion = value;
        if (!this.Visible)
          return;
        this.Refresh();
      }
    }

    public Color ShadeColor
    {
      get => this.m_oShadeColor;
      set
      {
        this.m_oShadeColor = value;
        if (!this.Visible)
          return;
        this.Refresh();
      }
    }

    public int GradientLength
    {
      get => this.m_iGradientLength;
      set
      {
        this.m_iGradientLength = value;
        if (!this.Visible)
          return;
        this.Refresh();
      }
    }

    public int CornerSize
    {
      get => this.m_iCornerSize;
      set
      {
        this.m_iCornerSize = value;
        if (!this.Visible)
          return;
        this.Refresh();
      }
    }

    protected override void SetVisibleCore(bool value)
    {
      if (value)
      {
        this.Refresh();
        this.UpdateShadeBounds(80);
      }
      else
        NativeMethods.ShowWindow(this.Handle, 0);
      base.SetVisibleCore(value);
      QControlHelper.UpdateControlRoot((Control) this);
    }

    internal void UpdateShadeBounds() => this.UpdateShadeBounds(20);

    protected virtual void UpdateShadeBounds(int setWindowPosFlags)
    {
      NativeMethods.SetWindowPos(this.Handle, this.m_oControl.Handle, this.m_oControl.Left + this.m_oShadeOffsetTopLeft.X, this.m_oControl.Top + this.m_oShadeOffsetTopLeft.Y, this.m_oControl.Width + this.m_oShadeOffsetBottomRight.X, this.m_oControl.Height + this.m_oShadeOffsetBottomRight.Y, (uint) setWindowPosFlags);
      this.UpdateBounds();
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      this.OnShadePainting(pevent);
      GraphicsState gstate = (GraphicsState) null;
      Region region = (Region) null;
      if (this.m_oShadeRegion != null)
      {
        gstate = pevent.Graphics.Save();
        region = pevent.Graphics.Clip.Clone();
        region.Intersect(this.m_oShadeRegion);
        pevent.Graphics.Clip = region;
      }
      this.PaintShade(pevent);
      if (gstate != null)
        pevent.Graphics.Restore(gstate);
      region?.Dispose();
      this.OnShadePainted(pevent);
    }

    protected virtual void PaintShade(PaintEventArgs pevent) => QControlPaint.DrawRoundedShade(this.ClientRectangle, this.m_iCornerSize, this.m_iGradientLength, Color.FromArgb(98, (int) this.m_oShadeColor.R, (int) this.m_oShadeColor.G, (int) this.m_oShadeColor.B), QDrawRoundedRectangleOptions.All, 0, 0, pevent.Graphics);

    private void Control_SizeChanged(object sender, EventArgs e) => this.UpdateShadeBounds();
  }
}
