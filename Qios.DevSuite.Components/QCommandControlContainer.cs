// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandControlContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCommandControlContainerDesigner), typeof (IDesigner))]
  [Designer(typeof (DocumentDesigner), typeof (IRootDesigner))]
  [ToolboxItem(false)]
  public class QCommandControlContainer : Control, IComponent, IDisposable
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private Size m_oPreferredSize = Size.Empty;
    private Size m_oPositionOffset;
    private Point m_oSetLocationOnVisible;
    private bool m_bCreatingBitmap;
    private bool m_bCreatingBackgroundBitmap;
    private Bitmap m_oBitmap;
    private Bitmap m_oBackgroundBitmap;
    private Graphics m_oBackgroundGraphics;

    public QCommandControlContainer()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.BackColor = Color.Transparent;
    }

    [Description("Gets or sets the backcolor of this Control")]
    [DefaultValue(typeof (Color), "Transparent")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public override Color BackColor
    {
      get => base.BackColor;
      set => base.BackColor = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AnchorStyles Anchor
    {
      get => base.Anchor;
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Visible
    {
      get => base.Visible;
      set => base.Visible = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Enabled
    {
      get => base.Enabled;
      set => base.Enabled = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new Point Location
    {
      get => base.Location;
      set => base.Location = value;
    }

    internal Bitmap Bitmap => this.m_oBitmap;

    internal Bitmap BackgroundBitmap => this.m_oBackgroundBitmap;

    internal bool CreatingBitmap => this.m_bCreatingBitmap;

    internal bool CreatingBackgroundBitmap => this.m_bCreatingBackgroundBitmap;

    internal Size PositionOffset
    {
      get => this.m_oPositionOffset;
      set
      {
        Point location = this.GetLocation();
        this.m_oPositionOffset = value;
        this.SetLocation(location);
      }
    }

    [Description("Gets or sets the preferred size of the QCommandControlContainer")]
    [Category("Layout")]
    public new Size PreferredSize
    {
      get => this.m_oPreferredSize;
      set
      {
        this.m_oPreferredSize = value;
        bool flag = false;
        if (this.Height != this.m_oPreferredSize.Height)
        {
          this.Height = this.m_oPreferredSize.Height;
          flag = true;
        }
        if (this.m_oPreferredSize != this.Size)
          flag = true;
        if (!flag || this.Parent == null || !(this.Parent is QMenuItemContainer))
          return;
        this.Parent.PerformLayout();
        this.Parent.Invalidate();
      }
    }

    internal void SetLocation(Point location) => this.SetLocation(location, false);

    internal void SetLocation(Point location, bool force)
    {
      if (this.Visible || force)
        this.Location = new Point(location.X - this.m_oPositionOffset.Width, location.Y - this.m_oPositionOffset.Height);
      else
        this.m_oSetLocationOnVisible = location;
    }

    internal Point GetLocation() => this.Visible || this.m_oSetLocationOnVisible.IsEmpty ? new Point(this.Location.X + this.m_oPositionOffset.Width, this.Location.Y + this.m_oPositionOffset.Height) : this.m_oSetLocationOnVisible;

    internal void CreateBitmap()
    {
      if (this.m_bCreatingBitmap || this.Width < 1 || this.Height < 1)
        return;
      this.m_bCreatingBitmap = true;
      QControlHelper.SecureAllControlHandles((Control) this, true);
      if (!this.m_oSetLocationOnVisible.IsEmpty)
      {
        this.SetLocation(this.m_oSetLocationOnVisible, true);
        this.m_oSetLocationOnVisible = Point.Empty;
      }
      bool flag = NativeHelper.IsWindowVisible((Control) this);
      if (!flag)
      {
        NativeHelper.SetWindowVisibleWithoutNotice((Control) this, true);
        this.Visible = true;
      }
      Bitmap bitmap = new Bitmap(this.Width, this.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      IntPtr hdc = graphics.GetHdc();
      NativeMethods.SendMessage(this.Handle, 791, hdc, new IntPtr(20));
      graphics.ReleaseHdc(hdc);
      graphics.Dispose();
      if (this.m_oBitmap != null)
        this.m_oBitmap.Dispose();
      this.m_oBitmap = bitmap;
      if (!flag)
      {
        NativeHelper.SetWindowVisibleWithoutNotice((Control) this, false);
        this.Visible = false;
      }
      this.m_bCreatingBitmap = false;
    }

    internal void PaintOnGraphics(Point location, Graphics graphics)
    {
      this.CreateBitmap();
      if (this.m_oBitmap == null || this.m_oBitmap.Size.IsEmpty)
        return;
      graphics.DrawImageUnscaled((Image) this.m_oBitmap, location.X, location.Y);
    }

    internal void ResetBackground()
    {
      if (this.m_bCreatingBackgroundBitmap)
        return;
      if (this.m_oBackgroundGraphics != null)
      {
        this.m_oBackgroundGraphics.Dispose();
        this.m_oBackgroundGraphics = (Graphics) null;
      }
      if (this.m_oBackgroundBitmap == null)
        return;
      this.m_oBackgroundBitmap.Dispose();
      this.m_oBackgroundBitmap = (Bitmap) null;
    }

    public new void ResumeLayout(bool performLayout)
    {
      base.ResumeLayout(performLayout);
      if (!(this.Parent is QMenuItemContainer parent))
        return;
      parent.PerformLayout();
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      if (!(this.Parent is QMenuItemContainer))
        return;
      this.m_oEventConsumers.Add((QWeakEventConsumer) new QWeakLayoutEventConsumer((Delegate) new LayoutEventHandler(this.QCommandControlContainer_Layout), (object) this.Parent, "Layout"));
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 791)
        base.WndProc(ref m);
      else if (m.Msg == 792)
      {
        Graphics graphics = Graphics.FromHdc(m.WParam);
        PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, this.ClientRectangle);
        this.OnPaintBackground(paintEventArgs);
        this.OnPaint(paintEventArgs);
        graphics.Dispose();
      }
      else
        base.WndProc(ref m);
    }

    internal void DisconnectFromParent()
    {
      if (!(this.Parent is QMenuItemContainer))
        return;
      this.m_oEventConsumers.DetachAndRemove((Delegate) new LayoutEventHandler(this.QCommandControlContainer_Layout));
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.ResetBackground();
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.DesignMode && this.BackColor == Color.Transparent && !(this.Parent is QMenuItemContainer))
        pevent.Graphics.Clear(SystemColors.Control);
      else if (this.BackColor == Color.Transparent)
      {
        if (this.m_oBackgroundBitmap == null && this.Width > 0 && this.Height > 0)
        {
          this.m_bCreatingBackgroundBitmap = true;
          this.m_oBackgroundBitmap = new Bitmap(this.Size.Width, this.Size.Height, PixelFormat.Format32bppPArgb);
          this.m_oBackgroundGraphics = Graphics.FromImage((Image) this.m_oBackgroundBitmap);
          QControlPaint.PaintTransparentBackground((Control) this, new PaintEventArgs(this.m_oBackgroundGraphics, pevent.ClipRectangle));
          this.m_bCreatingBackgroundBitmap = false;
          this.CreateBitmap();
        }
        if (this.m_oBackgroundBitmap != null)
          pevent.Graphics.DrawImageUnscaled((Image) this.m_oBackgroundBitmap, 0, 0);
        if (!this.DesignMode)
          return;
        Pen pen = new Pen(SystemColors.ControlDarkDark, 1f);
        pen.DashStyle = DashStyle.Dash;
        pevent.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
        pen.Dispose();
      }
      else
        base.OnPaintBackground(pevent);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_oBitmap != null)
        {
          this.m_oBitmap.Dispose();
          this.m_oBitmap = (Bitmap) null;
        }
        if (this.m_oBackgroundGraphics != null)
        {
          this.m_oBackgroundGraphics.Dispose();
          this.m_oBackgroundGraphics = (Graphics) null;
        }
        if (this.m_oBackgroundBitmap != null)
        {
          this.m_oBackgroundBitmap.Dispose();
          this.m_oBackgroundBitmap = (Bitmap) null;
        }
      }
      base.Dispose(disposing);
    }

    protected override void SetVisibleCore(bool value)
    {
      if (!this.m_oSetLocationOnVisible.IsEmpty && value)
      {
        this.SetLocation(this.m_oSetLocationOnVisible, true);
        this.m_oSetLocationOnVisible = Point.Empty;
      }
      base.SetVisibleCore(value);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      if (!this.m_oSetLocationOnVisible.IsEmpty && this.Visible)
      {
        this.SetLocation(this.m_oSetLocationOnVisible, true);
        this.m_oSetLocationOnVisible = Point.Empty;
      }
      base.OnVisibleChanged(e);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, width, height, specified);
      if (specified != BoundsSpecified.Size || !this.m_oPreferredSize.IsEmpty)
        return;
      this.m_oPreferredSize.Width = width;
      this.m_oPreferredSize.Height = height;
    }

    private void QCommandControlContainer_Layout(object sender, LayoutEventArgs e)
    {
      if (e.AffectedControl != null)
        return;
      this.ResetBackground();
    }
  }
}
