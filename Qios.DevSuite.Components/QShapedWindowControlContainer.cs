// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindowControlContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QShapedWindowControlContainerDesigner), typeof (IDesigner))]
  [ToolboxItem(false)]
  internal sealed class QShapedWindowControlContainer : Control
  {
    private Bitmap m_oBackgroundImage;
    private bool m_bSettingRegion;
    private IWin32Window m_oOwnerWindow;
    private Bitmap m_oBitmap;
    private Container components;
    private bool m_bCreatingBitmap;

    public QShapedWindowControlContainer()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      this.SetTopLevel(true);
      this.Visible = false;
      this.BackColor = Color.Transparent;
    }

    internal Bitmap Bitmap => this.m_oBitmap;

    internal bool TopLevel
    {
      get => this.GetTopLevel();
      set => this.SetTopLevel(value);
    }

    internal void CreateBitmap()
    {
      if (this.Width < 1 || this.Height < 1)
        return;
      this.m_bCreatingBitmap = true;
      if (!this.Created)
        this.CreateControl();
      bool visible = this.Visible;
      Region region = (Region) null;
      if (this.Region != null)
        region = this.Region.Clone();
      if (!this.Visible)
      {
        this.Region = new Region(Rectangle.Empty);
        this.Visible = true;
      }
      Bitmap bitmap = new Bitmap(this.Width, this.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      this.PaintOnGraphics(graphics);
      graphics.Dispose();
      if (this.m_oBitmap != null)
        this.m_oBitmap.Dispose();
      this.m_oBitmap = bitmap;
      if (!visible)
      {
        this.Visible = false;
        this.Region = region;
      }
      this.m_bCreatingBitmap = false;
    }

    private void PaintOnGraphics(Graphics graphics)
    {
      IntPtr hdc = graphics.GetHdc();
      NativeMethods.SendMessage(this.Handle, 791, hdc, new IntPtr(20));
      graphics.ReleaseHdc(hdc);
    }

    internal void SetTransparent(bool value)
    {
      CreateParams createParams = this.CreateParams;
      if (value)
        createParams.ExStyle |= 32;
      else
        createParams.ExStyle &= -33;
      NativeMethods.SetWindowLong(this.Handle, -20, createParams.ExStyle);
    }

    private void SetRegion()
    {
      if (this.DesignMode || this.BackColor != Color.Transparent || this.m_bCreatingBitmap)
        return;
      this.m_bSettingRegion = true;
      Region region = new Region(Rectangle.Empty);
      for (int index = 0; index < this.Controls.Count; ++index)
        region.Union(this.Controls[index].Bounds);
      this.Region = region;
      this.m_bSettingRegion = false;
    }

    protected override void SetVisibleCore(bool value)
    {
      if (value)
      {
        this.SetRegion();
        NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 339U);
      }
      base.SetVisibleCore(value);
      if (!value && this.m_oBackgroundImage != null)
      {
        this.m_oBackgroundImage.Dispose();
        this.m_oBackgroundImage = (Bitmap) null;
      }
      QControlHelper.UpdateControlRoot((Control) this);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 70)
      {
        NativeMethods.WINDOWPOS valueType = (NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.WINDOWPOS));
        if (!this.m_bSettingRegion && !this.m_bCreatingBitmap)
          valueType.flags &= 4294967263U;
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
      }
      if (m.Msg == 33)
        m.Result = new IntPtr(3);
      else
        base.WndProc(ref m);
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
        if (this.m_oBackgroundImage != null)
        {
          this.m_oBackgroundImage.Dispose();
          this.m_oBackgroundImage = (Bitmap) null;
        }
        if (this.components != null)
          this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IWin32Window OwnerWindow
    {
      get => this.m_oOwnerWindow;
      set
      {
        this.m_oOwnerWindow = value;
        this.SetOwnerWindowCore();
      }
    }

    public override ISite Site
    {
      get => base.Site;
      set => base.Site = value;
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style |= int.MinValue;
        createParams.Style &= -268435457;
        if (this.OwnerWindow != null)
          createParams.Parent = QControlHelper.GetUndisposedHandle(this.OwnerWindow);
        return createParams;
      }
    }

    private void SetOwnerWindowCore()
    {
      if (!this.IsHandleCreated)
        return;
      NativeMethods.SetWindowLong(this.Handle, -8, this.OwnerWindow != null ? this.OwnerWindow.Handle.ToInt32() : IntPtr.Zero.ToInt32());
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.m_oBackgroundImage != null)
      {
        this.m_oBackgroundImage.Dispose();
        this.m_oBackgroundImage = (Bitmap) null;
      }
      this.SetRegion();
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index].BackColor == Color.Transparent)
          this.Controls[index].Invalidate();
      }
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.BackColor == Color.Transparent)
      {
        if (this.DesignMode && this.m_oBackgroundImage != null)
        {
          this.m_oBackgroundImage.Dispose();
          this.m_oBackgroundImage = (Bitmap) null;
        }
        if (this.OwnerWindow is QShapedWindow ownerWindow)
        {
          if (this.m_oBackgroundImage == null)
            this.m_oBackgroundImage = ownerWindow.GetBackgroundImage();
          if (this.m_oBackgroundImage != null)
            pevent.Graphics.DrawImage((Image) this.m_oBackgroundImage, 0, 0, ownerWindow.ContentRectangle, GraphicsUnit.Pixel);
        }
      }
      else
        base.OnPaintBackground(pevent);
      if (!this.DesignMode)
        return;
      Pen pen = new Pen(SystemColors.ControlDarkDark, 1f);
      pen.DashStyle = DashStyle.Dash;
      pevent.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
      pen.Dispose();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Point Location
    {
      get => base.Location;
      set => base.Location = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Size Size
    {
      get => base.Size;
      set => base.Size = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override AnchorStyles Anchor
    {
      get => base.Anchor;
      set
      {
      }
    }

    private void InitializeComponent()
    {
      this.ClientSize = new Size(120, 40);
      this.Name = nameof (QShapedWindowControlContainer);
      this.Text = nameof (QShapedWindowControlContainer);
    }
  }
}
