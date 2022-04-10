// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonForm
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(false)]
  public class QRibbonForm : Form, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bAppearanceSimple = true;
    private QColorScheme m_oColorScheme;
    private QShapeAppearance m_oAppearance;
    private QMargin m_oClientAreaMargin = QMargin.Empty;
    private Size m_oLastAppliedSize;
    private FormWindowState m_eLastAppliedWindowState;
    private GraphicsPath m_oLastAppliedPath;
    private QOffscreenBitmapSet m_oOffscreenBitmapSet;
    private Size m_oAppliedClientSize;
    private QImageAlign m_eBackgroundImageAlign = QImageAlign.Centered;
    private Point m_oBackgroundImageOffset = Point.Empty;
    private bool m_bActive;
    private QWeakDelegate m_oWindowsXPThemeChangedDelegate;
    private QWeakDelegate m_oPaintNonClientAreaDelegate;

    public QRibbonForm() => this.InternalConstruct();

    private void InternalConstruct()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
      this.m_oColorScheme = new QColorScheme();
      this.m_oColorScheme.ColorsChanged += new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oAppearance = this.CreateAppearanceInstance();
      this.m_oAppearance.AppearanceChanged += new EventHandler(this.Appearance_AppearanceChanged);
      this.AdjustLayoutToShape(this.Size);
      this.ResumeLayout();
    }

    protected virtual QShapeAppearance CreateAppearanceInstance() => (QShapeAppearance) new QRibbonFormAppearance();

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("The ColorScheme that is used.")]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonForm)]
    public QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set
      {
        if (this.m_oColorScheme == value)
          return;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged -= new EventHandler(this.ColorScheme_ColorsChanged);
        this.m_oColorScheme = value;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged += new EventHandler(this.ColorScheme_ColorsChanged);
        if (this.m_oColorScheme == null || this.IsDisposed)
          return;
        this.Invalidate();
      }
    }

    public bool ShouldSerializeAppearance() => !this.Appearance.IsSetToDefaultValues();

    public void ResetAppearance() => this.Appearance.SetToDefaultValues();

    [Description("Gets or the Apperance that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QShapeAppearance Appearance => this.m_oAppearance;

    [DefaultValue(true)]
    [Description("Gets or sets whether Simple appearance must be used. When this is true, no advanced gradients are drawn. Just a plain background color and a plain borderKeep this true for increased resizing performance.")]
    [Category("QAppearance")]
    public bool AppearanceSimple
    {
      get => this.m_bAppearanceSimple;
      set
      {
        this.m_bAppearanceSimple = value;
        this.RefreshNoClientArea();
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the alignment of the BackgroundImage for this QContainerControl")]
    [DefaultValue(QImageAlign.Centered)]
    public QImageAlign BackgroundImageAlign
    {
      get => this.m_eBackgroundImageAlign;
      set
      {
        this.m_eBackgroundImageAlign = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets a relative offset to add to the BackgroundImage position")]
    [Category("Appearance")]
    [DefaultValue(typeof (Point), "0,0")]
    public Point BackgroundImageOffset
    {
      get => this.m_oBackgroundImageOffset;
      set
      {
        this.m_oBackgroundImageOffset = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get => this.IsActive && this.Enabled ? (Color) this.ColorScheme.RibbonFormBackground1 : (Color) this.ColorScheme.RibbonFormInactiveBackground1;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoScroll
    {
      get => base.AutoScroll;
      set => base.AutoScroll = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Size AutoScrollMinSize
    {
      get => base.AutoScrollMinSize;
      set => base.AutoScrollMinSize = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Size AutoScrollMargin
    {
      get => base.AutoScrollMargin;
      set => base.AutoScrollMargin = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Point AutoScrollPosition
    {
      get => base.AutoScrollPosition;
      set => base.AutoScrollPosition = value;
    }

    [Browsable(false)]
    public bool IsActive => this.m_bActive || this.DesignMode;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Browsable(false)]
    public Rectangle CurrentBounds => NativeHelper.GetWindowBounds((Control) this);

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Is raised when the Windows XP theme is changed")]
    public event EventHandler WindowsXPThemeChanged
    {
      add => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Combine(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Remove(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the NonClientArea should be drawn")]
    [Category("QEvents")]
    [QWeakEvent]
    public event PaintEventHandler PaintNonClientArea
    {
      add => this.m_oPaintNonClientAreaDelegate = QWeakDelegate.Combine(this.m_oPaintNonClientAreaDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPaintNonClientAreaDelegate = QWeakDelegate.Remove(this.m_oPaintNonClientAreaDelegate, (Delegate) value);
    }

    public Point PointToControl(Point clientPoint)
    {
      clientPoint.X += this.m_oClientAreaMargin.Left;
      clientPoint.Y += this.m_oClientAreaMargin.Top;
      return clientPoint;
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        Size size = this.AdjustPossibleFullSize(createParams, createParams.Width, createParams.Height);
        createParams.Width = size.Width;
        createParams.Height = size.Height;
        return createParams;
      }
    }

    protected override void SetClientSizeCore(int x, int y)
    {
      this.m_oAppliedClientSize = new Size(x, y);
      base.SetClientSizeCore(x, y);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      this.m_oAppliedClientSize = this.ClientSize;
      base.OnSizeChanged(e);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      Size size1 = this.AdjustPossibleFullSize(base.CreateParams, width, height);
      width = size1.Width;
      height = size1.Height;
      base.SetBoundsCore(x, y, width, height, specified);
      Size size2 = this.m_oClientAreaMargin.InflateSizeWithMargin(this.Size, false, true);
      this.UpdateBounds(this.Left, this.Top, this.Width, this.Height, size2.Width, size2.Height);
    }

    private Size AdjustPossibleFullSize(CreateParams createParams, int width, int height)
    {
      Size size = QControlHelper.GetDefaultNonClientAreaMargin(createParams, (MenuStrip) null).InflateSizeWithMargin(this.m_oAppliedClientSize, true, true);
      if (size.Width == width && size.Height == height)
      {
        width = this.m_oAppliedClientSize.Width + this.m_oClientAreaMargin.Horizontal;
        height = this.m_oAppliedClientSize.Height + this.m_oClientAreaMargin.Vertical;
      }
      return new Size(width, height);
    }

    protected Rectangle GetRectangleToUseForRegion(Size size)
    {
      int num1 = (int) Math.Floor((double) this.Appearance.BorderWidth / 2.0);
      int num2 = (int) Math.Ceiling((double) this.Appearance.BorderWidth / 2.0);
      return new QMargin(num1, num1, num2, num2).InflateRectangleWithMargin(new Rectangle(Point.Empty, size), false, true);
    }

    protected QMargin GetDefaultFrameBorderMargin()
    {
      Size frameBorderSize = SystemInformation.FrameBorderSize;
      QMargin frameBorderMargin = new QMargin(frameBorderSize.Width, frameBorderSize.Height, frameBorderSize.Height, frameBorderSize.Width);
      if (!this.IsMdiChild)
        ++frameBorderMargin.Bottom;
      return frameBorderMargin;
    }

    private bool AdjustLayoutToShape(Size size)
    {
      FormWindowState currentFormState = NativeHelper.GetCurrentFormState((Form) this);
      if (this.m_oLastAppliedSize == Size.Empty || size != this.m_oLastAppliedSize || this.m_eLastAppliedWindowState != currentFormState)
      {
        this.m_eLastAppliedWindowState = currentFormState;
        this.m_oLastAppliedSize = size;
        if (this.m_eLastAppliedWindowState == FormWindowState.Maximized || this.m_eLastAppliedWindowState == FormWindowState.Minimized)
        {
          this.m_oClientAreaMargin = this.GetDefaultFrameBorderMargin();
          if (this.m_oLastAppliedPath != null)
            this.m_oLastAppliedPath.Dispose();
          this.m_oLastAppliedPath = (GraphicsPath) null;
          if (this.Region == null)
            return false;
          Region region = this.Region;
          this.Region = (Region) null;
          region.Dispose();
          return true;
        }
        this.m_oClientAreaMargin = this.Appearance.Shape.ToMargin();
        GraphicsPath graphicsPath = this.Appearance.Shape.CreateGraphicsPath(this.GetRectangleToUseForRegion(size), DockStyle.None, QShapePathOptions.AllLines, (Matrix) null);
        if (graphicsPath != null)
        {
          if (this.m_oLastAppliedPath != null)
            this.m_oLastAppliedPath.Dispose();
          this.m_oLastAppliedPath = graphicsPath;
          Pen pen = new Pen(Brushes.Black, (float) this.Appearance.BorderWidth);
          GraphicsPath path = (GraphicsPath) graphicsPath.Clone();
          path.Widen(pen);
          pen.Dispose();
          path.AddPath(graphicsPath, false);
          path.FillMode = FillMode.Winding;
          Region region1 = new Region(path);
          path.Dispose();
          Region region2 = this.Region;
          this.Region = region1;
          region2?.Dispose();
          return true;
        }
      }
      return false;
    }

    public void RefreshNoClientArea() => this.RefreshNoClientArea(false);

    public void RefreshNoClientArea(bool children)
    {
      if (!this.Visible || !this.IsHandleCreated || this.IsDisposed)
        return;
      int flags = 1057 | (children ? 128 : 64);
      Qios.DevSuite.Components.NativeMethods.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, flags);
    }

    protected override void SetVisibleCore(bool value)
    {
      if (value)
        this.AdjustLayoutToShape(this.CurrentBounds.Size);
      base.SetVisibleCore(value);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 794)
      {
        base.WndProc(ref m);
        this.OnWindowsXPThemeChanged(EventArgs.Empty);
      }
      else if (m.Msg == 133)
      {
        if (NativeHelper.GetCurrentFormState((Form) this) == FormWindowState.Minimized)
        {
          base.WndProc(ref m);
        }
        else
        {
          Rectangle currentBounds = this.CurrentBounds;
          if (this.AdjustLayoutToShape(currentBounds.Size))
          {
            m.Result = IntPtr.Zero;
          }
          else
          {
            Rectangle rectangle = new Rectangle(0, 0, currentBounds.Width, currentBounds.Height);
            this.m_oClientAreaMargin.InflateRectangleWithMargin(rectangle, false, true);
            IntPtr windowDc = Qios.DevSuite.Components.NativeMethods.GetWindowDC(m.HWnd);
            IntPtr compatibleDc = Qios.DevSuite.Components.NativeMethods.CreateCompatibleDC(windowDc);
            if (this.m_oOffscreenBitmapSet == null)
              this.m_oOffscreenBitmapSet = new QOffscreenBitmapSet();
            IntPtr hObject1 = this.m_oOffscreenBitmapSet.SecureOffscreenDesktopBitmap(rectangle.Size);
            IntPtr hObject2 = Qios.DevSuite.Components.NativeMethods.SelectObject(compatibleDc, hObject1);
            Graphics graphics = Graphics.FromHdc(compatibleDc);
            this.OnPaintNonClientArea(new PaintEventArgs(graphics, rectangle));
            graphics.Dispose();
            int num1 = 0;
            int num2 = 0;
            Qios.DevSuite.Components.NativeMethods.BitBlt(windowDc, num1, num2, rectangle.Width, this.m_oClientAreaMargin.Top, compatibleDc, num1, num2, 13369376);
            int num3 = 0;
            int num4 = 0;
            Qios.DevSuite.Components.NativeMethods.BitBlt(windowDc, num3, num4, this.m_oClientAreaMargin.Left, rectangle.Height, compatibleDc, num3, num4, 13369376);
            int num5 = 0;
            int num6 = rectangle.Height - this.m_oClientAreaMargin.Bottom;
            Qios.DevSuite.Components.NativeMethods.BitBlt(windowDc, num5, num6, rectangle.Width, this.m_oClientAreaMargin.Bottom, compatibleDc, num5, num6, 13369376);
            int num7 = rectangle.Width - this.m_oClientAreaMargin.Right;
            int num8 = 0;
            Qios.DevSuite.Components.NativeMethods.BitBlt(windowDc, num7, num8, this.m_oClientAreaMargin.Right, rectangle.Height, compatibleDc, num7, num8, 13369376);
            Qios.DevSuite.Components.NativeMethods.SelectObject(compatibleDc, hObject2);
            Qios.DevSuite.Components.NativeMethods.DeleteDC(compatibleDc);
            Qios.DevSuite.Components.NativeMethods.ReleaseDC(m.HWnd, windowDc);
            m.Result = IntPtr.Zero;
          }
        }
      }
      else if (m.Msg == 131)
      {
        if (NativeHelper.GetCurrentFormState((Form) this) == FormWindowState.Minimized)
          base.WndProc(ref m);
        else if (m.WParam != IntPtr.Zero)
        {
          Qios.DevSuite.Components.NativeMethods.NCCALCSIZE_PARAMS valueType1 = (Qios.DevSuite.Components.NativeMethods.NCCALCSIZE_PARAMS) QMisc.PtrToValueType(m.LParam, typeof (Qios.DevSuite.Components.NativeMethods.NCCALCSIZE_PARAMS));
          Qios.DevSuite.Components.NativeMethods.WINDOWPOS valueType2 = (Qios.DevSuite.Components.NativeMethods.WINDOWPOS) QMisc.PtrToValueType(valueType1.lppos, typeof (Qios.DevSuite.Components.NativeMethods.WINDOWPOS));
          Rectangle rectangle = new Rectangle(valueType2.x, valueType2.y, valueType2.cx, valueType2.cy);
          rectangle = this.m_oClientAreaMargin.InflateRectangleWithMargin(rectangle, false, true);
          valueType1.rgrc0 = NativeHelper.CreateRECT(rectangle);
          valueType1.rgrc1 = valueType1.rgrc0;
          Marshal.StructureToPtr((object) valueType1, m.LParam, false);
          m.Result = new IntPtr(0);
        }
        else
        {
          Rectangle currentBounds = this.CurrentBounds;
          Marshal.StructureToPtr((object) ((Qios.DevSuite.Components.NativeMethods.RECT) QMisc.PtrToValueType(m.LParam, typeof (Qios.DevSuite.Components.NativeMethods.RECT)) with
          {
            left = this.m_oClientAreaMargin.Left,
            top = this.m_oClientAreaMargin.Top,
            bottom = (currentBounds.Height - this.m_oClientAreaMargin.Bottom),
            right = (currentBounds.Width - this.m_oClientAreaMargin.Right)
          }), m.LParam, false);
          m.Result = IntPtr.Zero;
        }
      }
      else if (m.Msg == 132)
      {
        if (NativeHelper.GetCurrentFormState((Form) this) == FormWindowState.Minimized)
        {
          base.WndProc(ref m);
        }
        else
        {
          base.WndProc(ref m);
          switch (m.Result.ToInt32())
          {
            case 8:
            case 9:
            case 20:
              m.Result = new IntPtr(2);
              break;
          }
        }
      }
      else if (m.Msg == 12)
        this.DefWndProc(ref m);
      else if (m.Msg == 134)
      {
        m.Result = new IntPtr(1);
        bool bActive = this.m_bActive;
        this.m_bActive = m.WParam.ToInt32() != 0;
        if (bActive == this.m_bActive)
          return;
        this.RefreshNoClientArea(true);
      }
      else if (m.Msg == 6)
      {
        base.WndProc(ref m);
        bool bActive = this.m_bActive;
        int int32 = m.WParam.ToInt32();
        this.m_bActive = int32 == 1 || int32 == 2;
        if (bActive == this.m_bActive)
          return;
        this.RefreshNoClientArea(true);
      }
      else if (m.Msg == 70)
      {
        Qios.DevSuite.Components.NativeMethods.WINDOWPOS valueType = (Qios.DevSuite.Components.NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (Qios.DevSuite.Components.NativeMethods.WINDOWPOS));
        Rectangle currentBounds = this.CurrentBounds;
        if ((currentBounds.Width != valueType.cx || currentBounds.Height != valueType.cy) && ((int) valueType.flags & 1) == 0)
          valueType.flags |= 256U;
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
        base.WndProc(ref m);
      }
      else if (m.Msg == 71)
      {
        Qios.DevSuite.Components.NativeMethods.WINDOWPOS valueType = (Qios.DevSuite.Components.NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (Qios.DevSuite.Components.NativeMethods.WINDOWPOS));
        if (((int) valueType.flags & 1) == 0)
        {
          this.AdjustLayoutToShape(new Size(valueType.cx, valueType.cy));
          valueType.flags |= 256U;
        }
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
        base.WndProc(ref m);
      }
      else if (m.Msg == 5)
        base.WndProc(ref m);
      else if (m.Msg == 36)
      {
        base.WndProc(ref m);
        if (this.MdiParent != null)
          return;
        Rectangle rectangle = this.GetDefaultFrameBorderMargin().InflateRectangleWithMargin(Screen.PrimaryScreen.Bounds, true, true);
        Marshal.StructureToPtr((object) ((Qios.DevSuite.Components.NativeMethods.MINMAXINFO) m.GetLParam(typeof (Qios.DevSuite.Components.NativeMethods.MINMAXINFO)) with
        {
          ptMaxPosition = new Qios.DevSuite.Components.NativeMethods.POINT(rectangle.X, rectangle.Y),
          ptMaxSize = new Qios.DevSuite.Components.NativeMethods.POINT(rectangle.Width, rectangle.Height)
        }), m.LParam, true);
        m.Result = IntPtr.Zero;
      }
      else
        base.WndProc(ref m);
    }

    protected QColorSet GetColorSet() => this.IsActive && this.Enabled ? new QColorSet((Color) this.ColorScheme.RibbonFormBackground1, (Color) this.ColorScheme.RibbonFormBackground2, (Color) this.ColorScheme.RibbonFormBorder) : new QColorSet((Color) this.ColorScheme.RibbonFormInactiveBackground1, (Color) this.ColorScheme.RibbonFormInactiveBackground2, (Color) this.ColorScheme.RibbonFormInactiveBorder);

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      QColorSet colorSet = this.GetColorSet();
      if (!this.m_bAppearanceSimple)
      {
        if (this.m_oOffscreenBitmapSet != null)
        {
          if (!(this.m_oOffscreenBitmapSet.OffscreenDesktopBitmap == IntPtr.Zero))
          {
            Size size = this.m_oClientAreaMargin.InflateSizeWithMargin(this.CurrentBounds.Size, false, true);
            IntPtr hdc = pevent.Graphics.GetHdc();
            IntPtr compatibleDc = Qios.DevSuite.Components.NativeMethods.CreateCompatibleDC(hdc);
            IntPtr offscreenDesktopBitmap = this.m_oOffscreenBitmapSet.OffscreenDesktopBitmap;
            IntPtr hObject = Qios.DevSuite.Components.NativeMethods.SelectObject(compatibleDc, offscreenDesktopBitmap);
            Qios.DevSuite.Components.NativeMethods.BitBlt(hdc, 0, 0, size.Width, size.Height, compatibleDc, this.m_oClientAreaMargin.Left, this.m_oClientAreaMargin.Top, 13369376);
            Qios.DevSuite.Components.NativeMethods.SelectObject(compatibleDc, hObject);
            Qios.DevSuite.Components.NativeMethods.DeleteDC(compatibleDc);
            pevent.Graphics.ReleaseHdc(hdc);
            goto label_11;
          }
        }
      }
      try
      {
        pevent.Graphics.Clear(colorSet.Background1);
      }
      catch
      {
        Size size = this.CurrentBounds.Size;
        using (Brush brush = (Brush) new SolidBrush(colorSet.Background1))
          pevent.Graphics.FillRectangle(brush, 0, 0, size.Width, size.Height);
      }
label_11:
      if (this.BackgroundImage == null)
        return;
      QControlPaint.DrawImage(this.BackgroundImage, this.m_eBackgroundImageAlign, this.m_oBackgroundImageOffset, this.ClientRectangle, this.BackgroundImage.Size, pevent.Graphics, (ImageAttributes) null, false);
    }

    protected virtual void OnPaintNonClientArea(PaintEventArgs e)
    {
      Size size = this.CurrentBounds.Size;
      QColorSet colorSet = this.GetColorSet();
      if (this.m_bAppearanceSimple)
      {
        e.Graphics.Clear(colorSet.Background1);
        if (NativeHelper.GetCurrentFormState((Form) this) != FormWindowState.Maximized && this.m_oLastAppliedPath != null && this.m_oAppearance.BorderWidth > 0)
        {
          Pen pen = new Pen(colorSet.Border, (float) this.m_oAppearance.BorderWidth);
          e.Graphics.DrawPath(pen, this.m_oLastAppliedPath);
          pen.Dispose();
        }
      }
      else
      {
        Rectangle rectangleToUseForRegion = this.GetRectangleToUseForRegion(size);
        QPainterOptions options = this.WindowState == FormWindowState.Maximized ? QPainterOptions.FillBackground : QPainterOptions.Default;
        QShapePainter.Default.Paint(rectangleToUseForRegion, this.Appearance.Shape, (IQAppearance) this.Appearance, colorSet, QShapePainterProperties.Default, QAppearanceFillerProperties.Default, options, e.Graphics);
      }
      this.m_oPaintNonClientAreaDelegate = QWeakDelegate.InvokeDelegate(this.m_oPaintNonClientAreaDelegate, (object) this, (object) e);
    }

    protected virtual void OnWindowsXPThemeChanged(EventArgs e) => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowsXPThemeChangedDelegate, (object) this, (object) e);

    private void ColorScheme_ColorsChanged(object sender, EventArgs e)
    {
      this.Invalidate();
      this.RefreshNoClientArea();
    }

    private void Appearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.m_oLastAppliedSize = Size.Empty;
      this.AdjustLayoutToShape(this.CurrentBounds.Size);
      this.RefreshNoClientArea();
    }
  }
}
