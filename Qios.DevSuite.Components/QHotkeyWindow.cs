// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QHotkeyWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QHotkeyWindow : QTranslucentWindow
  {
    private string m_sHotkeys;
    private QHotkeyWindowConfiguration m_oConfiguration;
    private QColorSet m_oColorSet;
    private IQHotkeyHandlerHost m_oNavigationHost;
    private bool m_bShouldBeVisible;

    public QColorSet ColorSet
    {
      get => this.m_oColorSet;
      set => this.m_oColorSet = value;
    }

    public QHotkeyWindowConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set => this.m_oConfiguration = value;
    }

    public IQHotkeyHandlerHost NavigationHost
    {
      get => this.m_oNavigationHost;
      set => this.m_oNavigationHost = value;
    }

    public string Hotkeys
    {
      get => this.m_sHotkeys;
      set => this.m_sHotkeys = value;
    }

    public bool ShouldBeVisible
    {
      get => this.m_bShouldBeVisible;
      set => this.m_bShouldBeVisible = value;
    }

    protected override void SetVisibleCore(bool value)
    {
      if (value)
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(0), 0, 0, 0, 0, 851U);
      base.SetVisibleCore(value);
      QControlHelper.UpdateControlRoot((Control) this);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 28)
      {
        int num = this.Visible ? 1 : 0;
        base.WndProc(ref m);
      }
      else if (m.Msg == 132)
        m.Result = new IntPtr(-1);
      else
        base.WndProc(ref m);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.m_oConfiguration == null)
        return;
      Size size = Size.Empty;
      using (Graphics graphics = this.CreateGraphics())
        size = graphics.MeasureString(this.Hotkeys, this.Font).ToSize();
      Size shapeSize = this.m_oConfiguration.Appearance.Shape.CalculateShapeSize(this.m_oConfiguration.Padding.InflateSizeWithPadding(size, true, true), true);
      this.SetBounds(0, 0, shapeSize.Width, shapeSize.Height, BoundsSpecified.Size);
      if (!this.IsLayered && !this.DesignMode)
        return;
      this.Refresh();
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.m_oConfiguration == null || this.m_oColorSet == null)
        return;
      QShapePainter.Default.Paint(this.ClientRectangle, this.m_oConfiguration.Appearance.Shape, (IQAppearance) this.m_oConfiguration.Appearance, this.m_oColorSet, new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds), QAppearanceFillerProperties.Default, QPainterOptions.Default, pevent.Graphics);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.m_oConfiguration == null || this.m_oColorSet == null)
        return;
      Rectangle rectangle = this.Configuration.Padding.InflateRectangleWithPadding(this.Configuration.Appearance.Shape.CalculateContentBounds(this.ClientRectangle, DockStyle.None), false, true);
      Brush brush = (Brush) new SolidBrush(this.m_oColorSet.Foreground);
      e.Graphics.DrawString(this.m_sHotkeys, this.Font, brush, (PointF) rectangle.Location);
      brush.Dispose();
    }
  }
}
