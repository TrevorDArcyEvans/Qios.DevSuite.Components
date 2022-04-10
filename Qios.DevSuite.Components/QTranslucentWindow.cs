// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTranslucentWindow
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
  [ToolboxItem(false)]
  [Designer(typeof (QTranslucentWindowDesigner), typeof (IRootDesigner))]
  public class QTranslucentWindow : Control, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bResizeToBackgroundImage = true;
    private QTranslucentWindowOptions m_eOptions;
    private bool m_bIsLayered;
    private bool m_bLayeredStyleSet;
    private bool m_bLayeredAttributeSet;
    private IWin32Window m_oOwnerWindow;
    private Size m_oMinimumSize = Size.Empty;
    private Size m_oMaximumSize = Size.Empty;
    private double m_dOpacity = 1.0;
    private bool m_bTopMost;
    private bool m_bCreateControlCalled;
    private bool m_bSetVisibleCoreCalled;
    private bool m_bOnLoadCalled;
    private QWeakDelegate m_oActivatedDelegate;
    private QWeakDelegate m_oLoadDelegate;
    private QWeakDelegate m_oClosedDelegate;
    private QWeakDelegate m_oClosingDelegate;

    [Description("Gets raised when the window is activated.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler Activated
    {
      add => this.m_oActivatedDelegate = QWeakDelegate.Combine(this.m_oActivatedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oActivatedDelegate = QWeakDelegate.Remove(this.m_oActivatedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the window is loaded.")]
    public event EventHandler Load
    {
      add => this.m_oLoadDelegate = QWeakDelegate.Combine(this.m_oLoadDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oLoadDelegate = QWeakDelegate.Remove(this.m_oLoadDelegate, (Delegate) value);
    }

    [Description("Gets raised when the window is closed.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler Closed
    {
      add => this.m_oClosedDelegate = QWeakDelegate.Combine(this.m_oClosedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oClosedDelegate = QWeakDelegate.Remove(this.m_oClosedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the window is about to close.")]
    [Category("QEvents")]
    public event CancelEventHandler Closing
    {
      add => this.m_oClosingDelegate = QWeakDelegate.Combine(this.m_oClosingDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oClosingDelegate = QWeakDelegate.Remove(this.m_oClosingDelegate, (Delegate) value);
    }

    public QTranslucentWindow(QTranslucentWindowOptions options)
    {
      this.SuspendLayout();
      base.SetVisibleCore(false);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetTopLevel(true);
      this.SetLayered(true, false);
      this.m_eOptions = options;
      this.ResumeLayout(false);
    }

    public QTranslucentWindow()
      : this(QTranslucentWindowOptions.PaintMask)
    {
    }

    [DefaultValue(1.0)]
    [Category("QAppearance")]
    [Description("Contains the Opacity of the Window. This can be between 0 and 1.")]
    public virtual double Opacity
    {
      get => this.m_dOpacity;
      set => this.SetOpacity(value, this.m_bIsLayered);
    }

    internal byte OpacityAsByte
    {
      get => (byte) (this.m_dOpacity * (double) byte.MaxValue);
      set => this.SetOpacity((double) value / (double) byte.MaxValue, this.m_bIsLayered);
    }

    protected virtual void SetLayered(bool value, bool refresh)
    {
      this.m_bIsLayered = value;
      this.SetLayeredCore(false);
      if (this.m_bIsLayered && this.IsHandleCreated)
      {
        NativeMethods.SetWindowRgn(this.Handle, IntPtr.Zero, 0);
        this.Region = (Region) null;
      }
      if (!refresh)
        return;
      this.Refresh();
    }

    protected virtual void SetOpacity(double value, bool refresh)
    {
      if (value < 0.0)
        value = 0.0;
      if (value > 1.0)
        value = 1.0;
      this.m_dOpacity = value;
      this.SetLayeredCore(false);
      if (!refresh)
        return;
      this.Refresh();
    }

    [Description("Gets or sets the MinimumSize of the Window")]
    [Localizable(true)]
    [Category("QBehavior")]
    [DefaultValue(typeof (Size), "0,0")]
    public new virtual Size MinimumSize
    {
      get => this.m_oMinimumSize;
      set
      {
        if (this.m_oMinimumSize == value)
          return;
        this.m_oMinimumSize = value;
        this.SetBounds(0, 0, Math.Max(this.Width, this.m_oMinimumSize.Width), Math.Max(this.Height, this.m_oMinimumSize.Height), BoundsSpecified.Size);
      }
    }

    [DefaultValue(typeof (Size), "0,0")]
    [Category("QBehavior")]
    [Localizable(true)]
    [Description("Gets or sets the MaximumSize of the Window")]
    public new virtual Size MaximumSize
    {
      get => this.m_oMaximumSize;
      set
      {
        if (this.m_oMaximumSize == value)
          return;
        this.m_oMaximumSize = value;
        BoundsSpecified specified = BoundsSpecified.None;
        if (this.m_oMaximumSize.Width > 0)
          specified = BoundsSpecified.Width;
        if (this.m_oMaximumSize.Height > 0)
          specified = BoundsSpecified.Height;
        this.SetBounds(0, 0, Math.Min(this.Width, this.m_oMaximumSize.Width), Math.Min(this.Height, this.m_oMaximumSize.Height), specified);
      }
    }

    [Description("Gets or sets whether this Window is TopMost")]
    [DefaultValue(false)]
    [Category("QBehavior")]
    public virtual bool TopMost
    {
      get => this.m_bTopMost;
      set
      {
        if (this.m_bTopMost == value)
          return;
        this.m_bTopMost = value;
        this.SetTopMostCore();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Form Owner
    {
      get => this.m_oOwnerWindow as Form;
      set => this.OwnerWindow = (IWin32Window) value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public override Color BackColor
    {
      get => base.BackColor;
      set
      {
        if (value.A < byte.MaxValue)
          this.SetLayered(true, false);
        base.BackColor = value;
        if (!this.IsLayered)
          return;
        this.Refresh();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    [Description("Gets or sets if the window should resize to the backgroundImage")]
    [DefaultValue(true)]
    [Category("QBehavior")]
    protected bool ResizeToBackgroundImage
    {
      get => this.m_bResizeToBackgroundImage;
      set
      {
        this.m_bResizeToBackgroundImage = value;
        this.PerformLayout();
      }
    }

    public override ISite Site
    {
      get => base.Site;
      set
      {
        base.Site = value;
        if (value == null || !value.DesignMode)
          return;
        this.SetTopLevel(false);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWin32Window OwnerWindow
    {
      get => this.m_oOwnerWindow;
      set => this.SetOwnerWindow(value, false);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control OwnerControl
    {
      get => this.m_oOwnerWindow as Control;
      set => this.OwnerWindow = (IWin32Window) value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QTranslucentWindowOptions Options
    {
      get => this.m_eOptions;
      set => this.m_eOptions = value;
    }

    [Browsable(false)]
    public bool IsLayered => this.m_bIsLayered;

    public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set
      {
        base.BackgroundImage = value;
        this.PerformLayout();
      }
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        if (!this.DesignMode)
        {
          createParams.Style = int.MinValue;
          createParams.ExStyle |= 128;
          if (this.m_bTopMost)
            createParams.ExStyle |= 8;
          if (this.m_oOwnerWindow != null)
            createParams.Parent = QControlHelper.GetUndisposedHandle(this.m_oOwnerWindow);
          if (this.IsLayered || this.OpacityAsByte < byte.MaxValue)
            createParams.ExStyle |= 524288;
          else
            createParams.ExStyle &= -524289;
        }
        return createParams;
      }
    }

    public override void Refresh()
    {
      if (this.IsLayered && !this.DesignMode)
        this.DrawLayeredWindow();
      else
        base.Refresh();
    }

    public void CenterToScreen()
    {
      Point point = new Point();
      Rectangle workingArea = (this.OwnerControl == null ? Screen.FromPoint(Control.MousePosition) : Screen.FromControl(this.OwnerControl)).WorkingArea;
      point.X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2);
      point.Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2);
      this.Location = point;
    }

    public void ShowCenteredOnScreen()
    {
      this.CenterToScreen();
      this.Show();
    }

    public void Activate()
    {
      if (!this.Visible || !this.IsHandleCreated)
        return;
      NativeMethods.SetForegroundWindow(this.Handle);
    }

    public void Close()
    {
      if (!this.IsHandleCreated)
        return;
      NativeMethods.SendMessage(this.Handle, 16, IntPtr.Zero, IntPtr.Zero);
    }

    internal virtual void DrawLayeredWindow() => this.DrawLayeredWindow(Point.Empty, Size.Empty);

    private void SetLayeredCore(bool force)
    {
      if (!this.IsHandleCreated)
        return;
      bool flag = this.m_bIsLayered || this.OpacityAsByte < byte.MaxValue;
      if (force || flag != this.m_bLayeredStyleSet)
      {
        this.m_bLayeredStyleSet = flag;
        NativeMethods.SetWindowLong(this.Handle, -20, this.CreateParams.ExStyle);
      }
      if ((this.OpacityAsByte >= byte.MaxValue || this.IsLayered) && !this.DesignMode)
      {
        if (!this.m_bLayeredAttributeSet)
          return;
        this.m_bLayeredAttributeSet = false;
        int dwNewLong = this.CreateParams.ExStyle & -524289;
        NativeMethods.SetWindowLong(this.Handle, -20, dwNewLong);
        if (!this.m_bLayeredStyleSet)
          return;
        NativeMethods.SetWindowLong(this.Handle, -20, dwNewLong | 524288);
      }
      else
      {
        NativeMethods.SetLayeredWindowAttributes(this.Handle, ColorTranslator.ToWin32(Color.Empty), this.OpacityAsByte, 2);
        this.m_bLayeredAttributeSet = true;
      }
    }

    private void SetTopMostCore()
    {
      if (!this.IsHandleCreated || this.DesignMode)
        return;
      if (this.m_bTopMost)
      {
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 19U);
      }
      else
      {
        if (this.m_oOwnerWindow == null || NativeHelper.IsTopMost(this.m_oOwnerWindow.Handle))
          return;
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-2), 0, 0, 0, 0, 19U);
      }
    }

    public void SetOwnerWindow(IWin32Window window, bool force)
    {
      if (!force && this.m_oOwnerWindow == window)
        return;
      this.m_oOwnerWindow = window;
      this.SetOwnerWindowCore();
    }

    private void SetOwnerWindowCore()
    {
      if (!this.IsHandleCreated || this.DesignMode)
        return;
      NativeMethods.SetWindowLong(this.Handle, -8, this.m_oOwnerWindow != null ? this.m_oOwnerWindow.Handle.ToInt32() : IntPtr.Zero.ToInt32());
      if (this.m_oOwnerWindow == null || !NativeHelper.IsTopMost(this.m_oOwnerWindow.Handle))
        return;
      NativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 19U);
    }

    private void ClearHdc(IntPtr hdc, Rectangle rect, Color color)
    {
      IntPtr solidBrush = NativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(color));
      IntPtr hObject = NativeMethods.SelectObject(hdc, solidBrush);
      NativeMethods.Rectangle(hdc, rect.X, rect.Y, rect.Width, rect.Height);
      NativeMethods.SelectObject(hdc, hObject);
      NativeMethods.DeleteObject(solidBrush);
    }

    private void ClearHdc(IntPtr hdc, Rectangle rect) => this.ClearHdc(hdc, rect, Color.FromArgb(0));

    internal virtual void DrawLayeredWindow(Point location, Size size)
    {
      if (this.DesignMode || !this.IsHandleCreated)
        return;
      bool flag = (this.m_eOptions & QTranslucentWindowOptions.PaintMask) == QTranslucentWindowOptions.PaintMask;
      Rectangle clipRect = new Rectangle(0, 0, size.IsEmpty ? this.Width : size.Width, size.IsEmpty ? this.Height : size.Height);
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      QOffscreenBitmapSet bitmapSet = (QOffscreenBitmapSet) null;
      IntPtr hObject1 = IntPtr.Zero;
      try
      {
        bitmapSet = QOffscreenBitmapsManager.GetFreeBitmapSet();
        num1 = NativeMethods.GetWindowDC(IntPtr.Zero);
        num2 = NativeMethods.CreateCompatibleDC(num1);
        IntPtr hObject2 = bitmapSet.SecureOffscreen32BitNativeBitmap(clipRect.Size);
        hObject1 = NativeMethods.SelectObject(num2, hObject2);
        this.ClearHdc(num2, new Rectangle(0, 0, clipRect.Width, clipRect.Height));
        using (Graphics graphics = Graphics.FromHdc(num2))
        {
          PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, clipRect);
          this.OnPaintBackground(paintEventArgs);
          this.OnPaint(paintEventArgs);
        }
        if (flag)
        {
          IntPtr num3 = IntPtr.Zero;
          IntPtr hObject3 = IntPtr.Zero;
          try
          {
            IntPtr num4 = bitmapSet.SecureOffscreenMaskBitmap(clipRect.Size);
            num3 = NativeMethods.CreateCompatibleDC(num1);
            hObject3 = NativeMethods.SelectObject(num3, num4);
            NativeMethods.SetBkColor(num1, ColorTranslator.ToWin32(Color.Black));
            this.ClearHdc(num3, new Rectangle(Point.Empty, clipRect.Size));
            using (Graphics graphics = Graphics.FromHdc(num3))
              this.OnPaintMask(new PaintEventArgs(graphics, clipRect));
            this.MakeOpaque(num2, clipRect.Size, num4);
          }
          finally
          {
            if (num3 != IntPtr.Zero && hObject3 != IntPtr.Zero)
              NativeMethods.SelectObject(num3, hObject3);
            if (num3 != IntPtr.Zero)
              NativeMethods.DeleteDC(num3);
          }
        }
        NativeMethods.BLENDFUNCTION pblend = new NativeMethods.BLENDFUNCTION();
        pblend.BlendOp = (byte) 0;
        pblend.BlendFlags = (byte) 0;
        pblend.SourceConstantAlpha = this.OpacityAsByte;
        pblend.AlphaFormat = (byte) 1;
        NativeMethods.POINT pprSrc = new NativeMethods.POINT(0, 0);
        NativeMethods.POINT point = NativeHelper.CreatePoint(location.IsEmpty ? this.Location : location);
        NativeMethods.SIZE size1 = NativeHelper.CreateSize(size.IsEmpty ? clipRect.Size : size);
        NativeMethods.UpdateLayeredWindow(this.Handle, num1, ref point, ref size1, num2, ref pprSrc, 0, ref pblend, 2);
      }
      finally
      {
        if (num1 != IntPtr.Zero)
          NativeMethods.ReleaseDC(IntPtr.Zero, num1);
        if (num2 != IntPtr.Zero && hObject1 != IntPtr.Zero)
          NativeMethods.SelectObject(num2, hObject1);
        if (num2 != IntPtr.Zero)
          NativeMethods.DeleteDC(num2);
        if (bitmapSet != null)
          QOffscreenBitmapsManager.FreeBitmapSet(bitmapSet);
      }
    }

    private void MakeOpaque(IntPtr hdc, Rectangle rectangle) => this.MakeOpaque(hdc, rectangle, IntPtr.Zero);

    private void MakeOpaque(IntPtr hdc, Size size, IntPtr maskBitmap) => this.MakeOpaque(hdc, new Rectangle(Point.Empty, size), maskBitmap);

    private void MakeOpaque(IntPtr hdc, Rectangle rectangle, IntPtr maskBitmap)
    {
      IntPtr hDC = IntPtr.Zero;
      IntPtr hObject1 = IntPtr.Zero;
      IntPtr num = IntPtr.Zero;
      QOffscreenBitmapSet bitmapSet = (QOffscreenBitmapSet) null;
      try
      {
        hDC = NativeMethods.GetDC(IntPtr.Zero);
        bitmapSet = QOffscreenBitmapsManager.GetFreeBitmapSet();
        IntPtr hObject2 = bitmapSet.SecureOffscreenDesktopBitmap(rectangle.Size);
        num = NativeMethods.CreateCompatibleDC(hDC);
        hObject1 = NativeMethods.SelectObject(num, hObject2);
        this.ClearHdc(num, new Rectangle(-1, -1, rectangle.Width + 2, rectangle.Height + 2), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
        if (maskBitmap != IntPtr.Zero)
          NativeMethods.MaskBlt(hdc, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, num, 0, 0, maskBitmap, 0, 0, 2864382502U);
        else
          NativeMethods.BitBlt(hdc, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, num, 0, 0, 12255782);
      }
      finally
      {
        if (num != IntPtr.Zero && hObject1 != IntPtr.Zero)
          NativeMethods.SelectObject(num, hObject1);
        if (num != IntPtr.Zero)
          NativeMethods.DeleteDC(num);
        if (hDC != IntPtr.Zero)
          NativeMethods.ReleaseDC(IntPtr.Zero, hDC);
        if (bitmapSet != null)
          QOffscreenBitmapsManager.FreeBitmapSet(bitmapSet);
      }
    }

    protected override void OnCreateControl() => base.OnCreateControl();

    protected override void CreateHandle()
    {
      base.CreateHandle();
      this.SetLayeredCore(true);
      this.m_bCreateControlCalled = true;
      if (this.m_bOnLoadCalled || !this.m_bSetVisibleCoreCalled)
        return;
      this.m_bOnLoadCalled = true;
      this.OnLoad(EventArgs.Empty);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 6)
      {
        this.OnActivated(EventArgs.Empty);
        base.WndProc(ref m);
      }
      else if (m.Msg == 16)
      {
        CancelEventArgs e = new CancelEventArgs(false);
        this.OnClosing(e);
        if (!e.Cancel)
        {
          this.OnClosed(EventArgs.Empty);
          this.Dispose();
        }
        m.Result = e.Cancel ? IntPtr.Zero : new IntPtr(1);
      }
      else if (m.Msg == 36)
      {
        NativeMethods.MINMAXINFO lparam = (NativeMethods.MINMAXINFO) m.GetLParam(typeof (NativeMethods.MINMAXINFO));
        if (this.m_oMinimumSize.Width > 0)
          lparam.ptMinTrackSize.x = this.m_oMinimumSize.Width;
        if (this.m_oMinimumSize.Height > 0)
          lparam.ptMinTrackSize.y = this.m_oMinimumSize.Height;
        if (this.m_oMaximumSize.Width > 0)
          lparam.ptMaxTrackSize.x = this.m_oMaximumSize.Width;
        if (this.m_oMaximumSize.Height > 0)
          lparam.ptMaxTrackSize.y = this.m_oMaximumSize.Height;
        Marshal.StructureToPtr((object) lparam, m.LParam, true);
        m.Result = IntPtr.Zero;
      }
      else if (m.Msg == 70)
      {
        NativeMethods.WINDOWPOS valueType = (NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.WINDOWPOS));
        if (((int) valueType.flags & 1) != 1)
        {
          if (this.MinimumSize.Width > 0 && this.MinimumSize.Width > valueType.cx)
            valueType.cx = this.MinimumSize.Width;
          if (this.MaximumSize.Width > 0 && this.MaximumSize.Width < valueType.cx)
            valueType.cx = this.MaximumSize.Width;
          if (this.MinimumSize.Height > 0 && this.MinimumSize.Height > valueType.cy)
            valueType.cy = this.MinimumSize.Height;
          if (this.MaximumSize.Height > 0 && this.MaximumSize.Height < valueType.cy)
            valueType.cy = this.MaximumSize.Height;
        }
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
        base.WndProc(ref m);
      }
      else
        base.WndProc(ref m);
    }

    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      this.m_bSetVisibleCoreCalled = true;
      if (!this.m_bOnLoadCalled && this.m_bCreateControlCalled)
      {
        this.m_bOnLoadCalled = true;
        this.OnLoad(EventArgs.Empty);
      }
      if (!value || !this.m_bIsLayered)
        return;
      this.Refresh();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.BackgroundImage != null && this.ResizeToBackgroundImage)
        this.SetBounds(0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height, BoundsSpecified.Size);
      if (!this.IsLayered && !this.DesignMode)
        return;
      this.Refresh();
    }

    protected virtual void OnPaintMask(PaintEventArgs pevent)
    {
    }

    protected virtual void OnClosing(CancelEventArgs e) => this.m_oClosingDelegate = QWeakDelegate.InvokeDelegate(this.m_oClosingDelegate, (object) this, (object) e);

    protected virtual void OnClosed(EventArgs e) => this.m_oClosedDelegate = QWeakDelegate.InvokeDelegate(this.m_oClosedDelegate, (object) this, (object) e);

    protected virtual void OnLoad(EventArgs e) => this.m_oLoadDelegate = QWeakDelegate.InvokeDelegate(this.m_oLoadDelegate, (object) this, (object) e);

    protected virtual void OnActivated(EventArgs e) => this.m_oActivatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oActivatedDelegate, (object) this, (object) e);

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.DesignMode)
      {
        Brush brush = (Brush) new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White);
        pevent.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
        brush.Dispose();
      }
      else if (!this.IsLayered)
        pevent.Graphics.Clear(this.BackColor);
      if (this.BackgroundImage == null)
        return;
      pevent.Graphics.DrawImage(this.BackgroundImage, 0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height);
    }

    protected override void Dispose(bool disposing) => base.Dispose(disposing);
  }
}
