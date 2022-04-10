// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindow
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
  [DesignerCategory("UserControl")]
  [ToolboxItem(false)]
  [Designer(typeof (QShapedWindowDesigner), typeof (IRootDesigner))]
  [Designer(typeof (QShapedWindowDesigner), typeof (IRootDesigner))]
  public class QShapedWindow : QTranslucentWindow
  {
    private bool m_bCreatingBackgroundImage;
    private QButtonState m_oCloseButtonState = QButtonState.Normal;
    private bool m_bWindowsXPThemeTried;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private bool m_bPaintControlContainer;
    private Size m_oPaintSize;
    private bool m_bIsUserSizing;
    private Rectangle m_oContentRectangle;
    private bool m_bLoaded;
    private QShapedWindowConfiguration m_oShapedWindowConfiguration;
    private QShapedWindowAppearance m_oAppearance;
    private QColorScheme m_oColorScheme;
    private bool m_bIsMoving;
    private Cursor m_oMovingCursor;
    private Point m_oMovingStartPoint;
    private bool m_bFlipHorizontal;
    private bool m_bFlipVertical;
    private QShape m_oShape;
    private GraphicsPath m_oShapeGraphicsPath;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;
    private EventHandler m_oAppearanceChangedEventHandler;
    internal QShapedWindowControlContainer ControlContainer;
    private EventHandler m_oShapedWindowConfigurationChangedEventHandler;
    private QWeakDelegate m_oColorsChangedDelegate;
    private QWeakDelegate m_oWindowsXPThemeChangedDelegate;
    private QWeakDelegate m_oAppearanceChangedDelegate;

    public QShapedWindow()
    {
      this.SuspendLayout();
      this.InitializeComponent();
      this.ResizeToBackgroundImage = false;
      this.m_oAppearanceChangedEventHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.m_oAppearance = this.CreateAppearanceInstance();
      this.m_oAppearance.AppearanceChanged += this.m_oAppearanceChangedEventHandler;
      this.m_oColorSchemeColorsChangedEventHandler = new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oColorScheme = new QColorScheme();
      this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      this.Initialize();
      this.ResumeLayout(false);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the colors or the QColorScheme changes")]
    public event EventHandler ColorsChanged
    {
      add => this.m_oColorsChangedDelegate = QWeakDelegate.Combine(this.m_oColorsChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oColorsChangedDelegate = QWeakDelegate.Remove(this.m_oColorsChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the Windows XP theme is changed")]
    public event EventHandler WindowsXPThemeChanged
    {
      add => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Combine(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Remove(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the Appearance changes")]
    public event EventHandler AppearanceChanged
    {
      add => this.m_oAppearanceChangedDelegate = QWeakDelegate.Combine(this.m_oAppearanceChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oAppearanceChangedDelegate = QWeakDelegate.Remove(this.m_oAppearanceChangedDelegate, (Delegate) value);
    }

    public override ISite Site
    {
      get => base.Site;
      set => base.Site = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Size MinimumSize
    {
      get => base.MinimumSize;
      set => base.MinimumSize = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Size MaximumSize
    {
      get => base.MaximumSize;
      set => base.MaximumSize = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool TopMost
    {
      get => base.TopMost;
      set => base.TopMost = value;
    }

    public bool ShouldSerializeAppearance() => !this.Appearance.IsSetToDefaultValues();

    public void ResetAppearance() => this.Appearance.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance.")]
    public virtual QShapedWindowAppearance Appearance
    {
      get => this.m_oAppearance;
      set
      {
        if (this.m_oAppearance == value)
          return;
        if (this.m_oAppearance != null)
          this.m_oAppearance.AppearanceChanged -= this.m_oAppearanceChangedEventHandler;
        this.m_oAppearance = value;
        if (this.m_oAppearance == null)
          return;
        this.m_oAppearance.AppearanceChanged += this.m_oAppearanceChangedEventHandler;
        this.Appearance_AppearanceChanged((object) this, EventArgs.Empty);
      }
    }

    [Description("Gets or sets the QShapedWindowConfiguration for the QShapedWindow.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QShapedWindowConfiguration Configuration
    {
      get => this.m_oShapedWindowConfiguration;
      set
      {
        if (this.m_oShapedWindowConfiguration == value)
          return;
        if (this.m_oShapedWindowConfiguration != null)
          this.m_oShapedWindowConfiguration.ConfigurationChanged -= this.m_oShapedWindowConfigurationChangedEventHandler;
        this.m_oShapedWindowConfiguration = value;
        if (this.m_oShapedWindowConfiguration == null)
          return;
        this.m_oShapedWindowConfiguration.ConfigurationChanged += this.m_oShapedWindowConfigurationChangedEventHandler;
        this.ShapedWindow_ConfigurationChanged((object) this, EventArgs.Empty);
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    public virtual QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set
      {
        if (this.m_oColorScheme == value)
          return;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
        this.m_oColorScheme = value;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
        this.OnColorsChanged(EventArgs.Empty);
      }
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set => base.BackgroundImage = value;
    }

    public new bool ShouldSerializeBackColor() => false;

    public override void ResetBackColor() => this.ColorScheme[this.BackColorPropertyName].Reset();

    [Category("Appearance")]
    [Description("Gets or sets the backcolor of this Control")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public override Color BackColor
    {
      get => this.ColorScheme[this.BackColorPropertyName].Current;
      set => this.ColorScheme[this.BackColorPropertyName].Current = value;
    }

    public bool ShouldSerializeBackColor2() => false;

    public void ResetBackColor2() => this.ColorScheme[this.BackColor2PropertyName].Reset();

    [Category("Appearance")]
    [Description("Gets or sets the second background color of this QContainerControl. This color is used when the Appearance is set to Gradient.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual Color BackColor2
    {
      get => this.ColorScheme[this.BackColor2PropertyName].Current;
      set => this.ColorScheme[this.BackColor2PropertyName].Current = value;
    }

    public bool ShouldSerializeBorderColor() => false;

    public void ResetBorderColor() => this.ColorScheme[this.BorderColorPropertyName].Reset();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description(" Gets or sets the border color of this QContainerControl. ")]
    public virtual Color BorderColor
    {
      get => this.ColorScheme[this.BorderColorPropertyName].Current;
      set => this.ColorScheme[this.BorderColorPropertyName].Current = value;
    }

    public bool ShouldSerializeShadeColor() => false;

    public void ResetShadeColor() => this.ColorScheme[this.ShadeColorPropertyName].Reset();

    [Description("Gets or sets the shade color of this QContainerControl. ")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual Color ShadeColor
    {
      get => this.ColorScheme[this.ShadeColorPropertyName].Current;
      set => this.ColorScheme[this.ShadeColorPropertyName].Current = value;
    }

    internal bool IsMoving => this.m_bIsMoving;

    internal Cursor MovingCursor => this.m_oMovingCursor;

    internal virtual string HotButtonBorderColorPropertyName => "ShapedWindowHotButtonBorder";

    internal virtual string HotButtonBackground1ColorPropertyName => "ShapedWindowHotButtonBackground1";

    internal virtual string HotButtonBackground2ColorPropertyName => "ShapedWindowHotButtonBackground2";

    internal virtual string PressedButtonBorderColorPropertyName => "ShapedWindowPressedButtonBorder";

    internal virtual string PressedButtonBackground1ColorPropertyName => "ShapedWindowPressedButtonBackground1";

    internal virtual string PressedButtonBackground2ColorPropertyName => "ShapedWindowPressedButtonBackground2";

    [Description("Gets or sets the shape of the window")]
    [Category("QAppearance")]
    [QShapeDesignVisible(QShapeType.ShapedWindow)]
    internal QShape Shape
    {
      get => this.m_oShape;
      set
      {
        if (this.m_oShape != null)
          this.m_oShape.ShapeChanged -= new EventHandler(this.m_oShape_ShapeChanged);
        this.m_oShape = value;
        if (this.m_oShape != null)
          this.m_oShape.ShapeChanged += new EventHandler(this.m_oShape_ShapeChanged);
        else
          this.ContentRectangle = Rectangle.Empty;
        this.UpdateMinimumSize();
        this.Invalidate();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.DesignMode)
      {
        this.PaintShape(e.Graphics, this.Size);
        this.SetControlContainerLocation();
        this.SetControlContainerSize();
      }
      this.PaintCloseButton(e.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.DesignMode)
      {
        Brush brush = (Brush) new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White);
        pevent.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
        brush.Dispose();
      }
      else
      {
        if (this.IsLayered)
          this.PaintShape(pevent.Graphics, this.m_oPaintSize.IsEmpty ? this.Size : this.m_oPaintSize);
        else
          this.PaintShape(pevent.Graphics, this.Size);
        if (!this.m_bPaintControlContainer || this.ControlContainer == null || this.ControlContainer.Bitmap == null || this.m_bCreatingBackgroundImage)
          return;
        pevent.Graphics.DrawImageUnscaled((Image) this.ControlContainer.Bitmap, this.ContentRectangle.X, this.ContentRectangle.Y);
      }
    }

    protected override void OnPaintMask(PaintEventArgs pevent)
    {
      base.OnPaintMask(pevent);
      if (this.ShapeGraphicsPath == null)
        return;
      pevent.Graphics.SmoothingMode = SmoothingMode.None;
      pevent.Graphics.Clip = new Region(this.ShapeGraphicsPath);
      pevent.Graphics.Clear(Color.White);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.m_bLoaded)
        return;
      this.m_bLoaded = true;
      if (this.ControlContainer != null && this.DesignMode)
      {
        this.ControlContainer.TopLevel = false;
        this.Controls.Add((Control) this.ControlContainer);
        this.ControlContainer.Visible = true;
      }
      if (this.ControlContainer == null)
        return;
      if (this.ControlContainer.Controls.Count == 0 && !this.DesignMode)
      {
        this.ControlContainer = (QShapedWindowControlContainer) null;
      }
      else
      {
        this.SetControlContainerLocation();
        this.SetControlContainerSize();
      }
    }

    protected override void OnLocationChanged(EventArgs e)
    {
      base.OnLocationChanged(e);
      this.SetControlContainerLocation();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.SetControlContainerSize();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.CloseButtonArea.Contains(new Point(e.X, e.Y)) && this.Configuration.CanClose)
      {
        if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left)
          return;
        this.CloseButtonState = QButtonState.Pressed;
      }
      else
      {
        if (!this.Configuration.CanMove || e.Button != MouseButtons.Left)
          return;
        this.m_bIsMoving = true;
        this.m_oMovingCursor = Cursor.Current;
        this.Cursor = Cursors.SizeAll;
        this.m_oMovingStartPoint = new Point(e.X, e.Y);
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.m_bIsMoving)
      {
        this.m_bIsMoving = false;
        if (this.m_oMovingCursor != (Cursor) null)
          this.Cursor = this.m_oMovingCursor;
        else
          this.Cursor = Cursors.Default;
      }
      else
      {
        if (!this.CloseButtonArea.Contains(new Point(e.X, e.Y)) || this.CloseButtonState != QButtonState.Pressed || !this.Configuration.CanClose)
          return;
        this.CloseButtonState = QButtonState.Hot;
        this.Close();
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.m_bIsMoving)
      {
        Point screen = this.PointToScreen(new Point(e.X, e.Y));
        this.Location = new Point(screen.X - this.m_oMovingStartPoint.X, screen.Y - this.m_oMovingStartPoint.Y);
      }
      else if (this.CloseButtonArea.Contains(new Point(e.X, e.Y)) && this.Configuration.CanClose)
      {
        if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
          this.CloseButtonState = QButtonState.Pressed;
        else
          this.CloseButtonState = QButtonState.Hot;
      }
      else
        this.CloseButtonState = QButtonState.Normal;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.CloseButtonState = QButtonState.Normal;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 794)
      {
        this.OnWindowsXPThemeChanged(EventArgs.Empty);
        base.WndProc(ref m);
      }
      else if (m.Msg == 132 && !this.Configuration.ReceivesMouseMessages)
        m.Result = new IntPtr(-1);
      else if (m.Msg == 132 && this.Configuration.CanSize && this.ShapeGraphicsPath != null)
      {
        base.WndProc(ref m);
        Point point = new Point(m.LParam.ToInt32());
        point = this.PointToClient(point);
        QSizingAction qsizingAction = QSizingAction.None;
        if (this.ShapeGraphicsPath.IsOutlineVisible(point, new Pen(Color.Black, 10f)))
        {
          if (point.X < this.ContentRectangle.X)
            qsizingAction = QSizingAction.SizingLeft;
          if (point.X > this.ContentRectangle.X + this.ContentRectangle.Width)
            qsizingAction = QSizingAction.SizingRight;
          if (point.Y < this.ContentRectangle.Y)
          {
            switch (qsizingAction)
            {
              case QSizingAction.SizingLeft:
                qsizingAction = QSizingAction.SizingTopLeft;
                break;
              case QSizingAction.SizingRight:
                qsizingAction = QSizingAction.SizingTopRight;
                break;
              default:
                qsizingAction = QSizingAction.SizingTop;
                break;
            }
          }
          if (point.Y > this.ContentRectangle.Y + this.ContentRectangle.Height)
          {
            switch (qsizingAction)
            {
              case QSizingAction.SizingLeft:
                qsizingAction = QSizingAction.SizingBottomLeft;
                break;
              case QSizingAction.SizingRight:
                qsizingAction = QSizingAction.SizingBottomRight;
                break;
              default:
                qsizingAction = QSizingAction.SizingBottom;
                break;
            }
          }
        }
        switch (qsizingAction)
        {
          case QSizingAction.SizingLeft:
            m.Result = new IntPtr(10);
            break;
          case QSizingAction.SizingRight:
            m.Result = new IntPtr(11);
            break;
          case QSizingAction.SizingTop:
            m.Result = new IntPtr(12);
            break;
          case QSizingAction.SizingBottom:
            m.Result = new IntPtr(15);
            break;
          case QSizingAction.SizingTopLeft:
            m.Result = new IntPtr(13);
            break;
          case QSizingAction.SizingBottomLeft:
            m.Result = new IntPtr(16);
            break;
          case QSizingAction.SizingTopRight:
            m.Result = new IntPtr(14);
            break;
          case QSizingAction.SizingBottomRight:
            m.Result = new IntPtr(17);
            break;
        }
      }
      else if (m.Msg == 70)
      {
        NativeMethods.WINDOWPOS valueType = (NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.WINDOWPOS));
        if (this.m_bIsUserSizing)
        {
          valueType.flags |= 4U;
          valueType.flags |= 256U;
          if (this.IsLayered)
            valueType.flags |= 8U;
          if (valueType.cx != 0 && valueType.cy != 0 && this.IsLayered)
          {
            this.DrawLayeredWindow(new Point(valueType.x, valueType.y), new Size(valueType.cx, valueType.cy));
            this.UpdateBounds();
          }
        }
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
        base.WndProc(ref m);
      }
      else if (m.Msg == 561)
      {
        this.m_bIsUserSizing = true;
        base.WndProc(ref m);
      }
      else if (m.Msg == 562)
      {
        this.m_bIsUserSizing = false;
        base.WndProc(ref m);
      }
      else
        base.WndProc(ref m);
    }

    protected override void Dispose(bool disposing)
    {
      this.CloseWindowsXpTheme();
      if (disposing)
      {
        if (this.m_oColorScheme != null && !this.m_oColorScheme.IsDisposed)
        {
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
          this.m_oColorScheme.Dispose();
        }
        this.Configuration = (QShapedWindowConfiguration) null;
        this.Appearance = (QShapedWindowAppearance) null;
        this.ColorScheme = (QColorScheme) null;
      }
      base.Dispose(disposing);
    }

    protected virtual void UpdateMaximumSize() => this.MaximumSize = this.Configuration.MaximumSize;

    protected virtual void UpdateMinimumSize()
    {
      if (this.Shape == null)
        return;
      if (this.Configuration.AutoMinimumSize)
      {
        Size shapeSize = this.Shape.CalculateShapeSize(Size.Empty, true);
        if (this.Configuration.CanClose)
        {
          shapeSize.Width += this.CloseButtonArea.Width;
          shapeSize.Height += this.CloseButtonArea.Height;
        }
        shapeSize.Width += Math.Abs(this.Appearance.ShadeOffset.X);
        shapeSize.Height += Math.Abs(this.Appearance.ShadeOffset.Y);
        this.MinimumSize = new Size(Math.Max(shapeSize.Width, this.Shape.MinimumSize.Width + Math.Abs(this.UsedShadeVisible ? this.Appearance.ShadeOffset.X : 0)), Math.Max(shapeSize.Height, this.Shape.MinimumSize.Height + Math.Abs(this.UsedShadeVisible ? this.Appearance.ShadeOffset.Y : 0)));
      }
      else
        this.MinimumSize = this.Configuration.MinimumSize;
    }

    protected virtual QShapedWindowConfiguration CreateShapedWindowConfigurationInstance() => new QShapedWindowConfiguration();

    protected virtual string BackColorPropertyName => "ShapedWindowBackground1";

    protected virtual string BackColor2PropertyName => "ShapedWindowBackground2";

    protected virtual string BorderColorPropertyName => "ShapedWindowBorder";

    protected virtual string ShadeColorPropertyName => "ShapedWindowShade";

    protected virtual void OnColorsChanged(EventArgs e) => this.m_oColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oColorsChangedDelegate, (object) this, (object) e);

    protected virtual void OnWindowsXPThemeChanged(EventArgs e)
    {
      this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowsXPThemeChangedDelegate, (object) this, (object) e);
      this.CloseWindowsXpTheme();
    }

    protected virtual void OnAppearanceChanged(EventArgs e) => this.m_oAppearanceChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oAppearanceChangedDelegate, (object) this, (object) e);

    protected virtual QShapedWindowAppearance CreateAppearanceInstance() => new QShapedWindowAppearance();

    public virtual Rectangle ContentRectangle
    {
      get => this.m_oContentRectangle;
      set
      {
        bool flag1 = false;
        bool flag2 = false;
        if (this.DesignMode)
        {
          if (value.Location != this.m_oContentRectangle.Location)
            flag1 = true;
          if (value.Size != this.m_oContentRectangle.Size)
            flag2 = true;
        }
        this.m_oContentRectangle = value;
        if (!this.DesignMode)
          return;
        if (flag1)
          this.SetControlContainerLocation();
        if (!flag2)
          return;
        this.SetControlContainerSize();
      }
    }

    private void SecureWindowsXpTheme()
    {
      if (this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.Handle, "Tooltip");
      this.m_bWindowsXPThemeTried = true;
    }

    private void CloseWindowsXpTheme()
    {
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        NativeMethods.CloseThemeData(this.m_hWindowsXPTheme);
        this.m_hWindowsXPTheme = IntPtr.Zero;
      }
      this.m_bWindowsXPThemeTried = false;
    }

    private bool HasWindowsXPTheme
    {
      get
      {
        this.SecureWindowsXpTheme();
        return this.m_hWindowsXPTheme != IntPtr.Zero;
      }
    }

    private Rectangle CloseButtonArea
    {
      get
      {
        this.SecureWindowsXpTheme();
        Size size = this.Configuration.CloseButtonStyle != QButtonStyle.Windows ? this.Configuration.CustomCloseButtonSize : (!(this.m_hWindowsXPTheme != IntPtr.Zero) ? NativeHelper.GetCaptionButtonSize(false) : new Size(SystemInformation.ToolWindowCaptionButtonSize.Width, SystemInformation.ToolWindowCaptionButtonSize.Height));
        Rectangle closeButtonArea = new Rectangle(this.ContentRectangle.Right - size.Width, this.ContentRectangle.Top, size.Width, size.Height);
        closeButtonArea.Offset(this.Configuration.CloseButtonOffset);
        return closeButtonArea;
      }
    }

    private GraphicsPath ShapeGraphicsPath
    {
      get => this.m_oShapeGraphicsPath;
      set
      {
        this.m_oShapeGraphicsPath = value;
        if (this.IsLayered)
          return;
        this.Region = new Region(this.m_oShapeGraphicsPath);
      }
    }

    private QButtonState CloseButtonState
    {
      get => this.m_oCloseButtonState;
      set
      {
        if (this.m_oCloseButtonState == value)
          return;
        this.m_oCloseButtonState = value;
        this.Refresh();
      }
    }

    private int GetCloseButtonStyle(QButtonState style)
    {
      switch (style)
      {
        case QButtonState.Inactive:
          return 1;
        case QButtonState.Normal:
          return 1;
        case QButtonState.Hot:
          return 2;
        case QButtonState.Pressed:
          return 3;
        default:
          return 0;
      }
    }

    private void PaintCloseButton(Graphics graphics)
    {
      if (!this.Configuration.CanClose || this.CloseButtonArea.IsEmpty)
        return;
      if (this.Configuration.CloseButtonStyle == QButtonStyle.Windows)
      {
        this.SecureWindowsXpTheme();
        if (this.m_hWindowsXPTheme != IntPtr.Zero)
        {
          IntPtr hdc = graphics.GetHdc();
          NativeMethods.RECT rect = NativeHelper.CreateRECT(this.CloseButtonArea);
          NativeMethods.DrawThemeBackground(this.m_hWindowsXPTheme, hdc, 5, this.GetCloseButtonStyle(this.CloseButtonState), ref rect, ref rect);
          graphics.ReleaseHdc(hdc);
        }
        else
        {
          IntPtr hdc = graphics.GetHdc();
          NativeMethods.RECT rect = NativeHelper.CreateRECT(this.CloseButtonArea);
          int num1 = 0;
          int num2 = this.CloseButtonState == QButtonState.Pressed ? 512 : 0;
          NativeMethods.DrawFrameControl(hdc, ref rect, 1, num1 | num2);
          graphics.ReleaseHdc(hdc);
        }
      }
      else
      {
        QColorSet colors = (QColorSet) null;
        if (this.CloseButtonState == QButtonState.Hot)
          colors = new QColorSet(this.ColorScheme[this.HotButtonBackground1ColorPropertyName].Current, this.ColorScheme[this.HotButtonBackground2ColorPropertyName].Current, this.ColorScheme[this.HotButtonBorderColorPropertyName].Current);
        else if (this.CloseButtonState == QButtonState.Pressed)
          colors = new QColorSet(this.ColorScheme[this.PressedButtonBackground1ColorPropertyName].Current, this.ColorScheme[this.PressedButtonBackground2ColorPropertyName].Current, this.ColorScheme[this.PressedButtonBorderColorPropertyName].Current);
        if (colors != null)
          QRectanglePainter.Default.Paint(this.CloseButtonArea, (IQAppearance) new QAppearanceWrapper((IQAppearance) this.Configuration.ButtonAppearance), colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        QControlPaint.DrawImage(QControlPaint.CreateColorizedImage(this.Configuration.UsedCustomCloseButtonMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.ColorScheme.MarkupText), QImageAlign.Centered, this.CloseButtonArea, this.CloseButtonArea.Size, graphics);
      }
    }

    private void SetControlContainerLocation()
    {
      if (this.ControlContainer == null)
        return;
      if (this.ControlContainer.TopLevel)
        NativeMethods.SetWindowPos(this.ControlContainer.Handle, IntPtr.Zero, this.Location.X + this.m_oContentRectangle.X, this.Location.Y + this.m_oContentRectangle.Y, 0, 0, 21U);
      else
        this.ControlContainer.Location = this.m_oContentRectangle.Location;
    }

    private void SetControlContainerSize()
    {
      if (this.ControlContainer == null)
        return;
      this.ControlContainer.Size = this.m_oContentRectangle.Size;
    }

    protected virtual void Initialize()
    {
      this.m_oShapedWindowConfigurationChangedEventHandler = new EventHandler(this.ShapedWindow_ConfigurationChanged);
      this.m_oShapedWindowConfiguration = this.CreateShapedWindowConfigurationInstance();
      this.m_oShapedWindowConfiguration.ConfigurationChanged += this.m_oShapedWindowConfigurationChangedEventHandler;
      this.Shape = this.m_oAppearance.Shape;
      this.HandleConfigurationChanged(false);
    }

    protected virtual void HandleConfigurationChanged(bool refresh)
    {
      this.UpdateMaximumSize();
      this.UpdateMinimumSize();
      if (this.Configuration.Layered != this.IsLayered)
        this.SetLayered(this.Configuration.Layered, false);
      this.TopMost = this.Configuration.TopMost;
      if (!refresh)
        return;
      this.Refresh();
    }

    private void ApplyObsoletePropertiesToAppearance()
    {
      this.Appearance.SuspendChangeNotification();
      try
      {
        this.Appearance.ShadeOffset = (Point) this.Configuration.Properties.GetPropertyAsValueType(2);
        this.Appearance.ShadeVisible = (bool) this.Configuration.Properties.GetPropertyAsValueType(1);
        this.Appearance.ShadeGradientSize = (int) this.Configuration.Properties.GetPropertyAsValueType(3);
      }
      finally
      {
        this.Appearance.ResumeChangeNotification(false);
      }
    }

    private void ApplyObsoletePropertiesFromAppearance()
    {
      this.Configuration.SuspendChangedEvent();
      try
      {
        this.Configuration.Properties.SetProperty(2, (object) this.Appearance.ShadeOffset);
        this.Configuration.Properties.SetProperty(1, (object) this.Appearance.ShadeVisible);
        this.Configuration.Properties.SetProperty(3, (object) this.Appearance.ShadeGradientSize);
      }
      finally
      {
        this.Configuration.ResumeChangedEvent();
      }
    }

    private void ShapedWindow_ConfigurationChanged(object sender, EventArgs e)
    {
      this.ApplyObsoletePropertiesToAppearance();
      this.HandleConfigurationChanged(true);
    }

    private void ColorScheme_ColorsChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.OnColorsChanged(EventArgs.Empty);
      this.Refresh();
      if (!this.DesignMode || this.ControlContainer == null)
        return;
      this.ControlContainer.Refresh();
    }

    private void Appearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.ApplyObsoletePropertiesFromAppearance();
      if (this.Shape != this.m_oAppearance.Shape)
        this.Shape = this.m_oAppearance.Shape;
      this.PerformLayout();
      this.OnAppearanceChanged(EventArgs.Empty);
      this.Refresh();
    }

    private void m_oShape_ShapeChanged(object sender, EventArgs e) => this.Refresh();

    private void PaintShape(Graphics graphics, Size size)
    {
      Matrix matrix = (Matrix) null;
      if (this.m_bFlipHorizontal)
        matrix = new Matrix(-1f, 0.0f, 0.0f, 1f, (float) size.Width, 0.0f);
      DockStyle dockStyle = DockStyle.Top;
      if (this.m_bFlipVertical)
        dockStyle = DockStyle.Bottom;
      if (this.m_oShape == null || this.m_oShape.Items.Count <= 0)
        return;
      size.Width -= this.Appearance.BorderWidth;
      size.Height -= this.Appearance.BorderWidth;
      int num1 = this.UsedShadeVisible ? this.Appearance.ShadeOffset.X : 0;
      int num2 = this.UsedShadeVisible ? this.Appearance.ShadeOffset.Y : 0;
      int num3 = 0;
      if (this.m_bFlipHorizontal)
        num3 = (num1 < 0 ? Math.Abs(num1) + 1 : num1) + 1;
      Rectangle rectangle = Rectangle.Empty;
      rectangle = new Rectangle(num1 < 0 ? Math.Abs(num1) : 0, num2 < 0 ? Math.Abs(num2) : 0, size.Width - Math.Abs(num1), size.Height - Math.Abs(num2));
      rectangle.Offset(-num3, 0);
      if (this.UsedShadeVisible)
        QControlPaint.DrawShapeShade((IQShadedShapeAppearance) this.Appearance, new Rectangle(Point.Empty, rectangle.Size), dockStyle, this.ShadeColor, graphics, matrix);
      QAppearanceWrapper appearance = new QAppearanceWrapper((IQAppearance) this.Appearance);
      appearance.SmoothingMode = this.IsLayered ? this.Appearance.SmoothingMode : QSmoothingMode.None;
      appearance.BorderWidth = this.IsLayered ? this.Appearance.BorderWidth : this.Appearance.BorderWidth + 1;
      QShapePainterProperties properties = new QShapePainterProperties(matrix, QShapePainterOptions.ReturnDrawnShape);
      GraphicsPath graphicsPath = QShapePainter.Default.Paint(rectangle, this.m_oShape, (IQAppearance) appearance, new QColorSet(this.BackColor, this.BackColor2, this.BorderColor), properties, new QAppearanceFillerProperties()
      {
        DockStyle = dockStyle
      }, QPainterOptions.Default, graphics);
      Rectangle contentBounds = this.m_oShape.CalculateContentBounds(rectangle, dockStyle);
      if (this.FlipHorizontal)
      {
        contentBounds.X = this.Size.Width - contentBounds.Right;
        if (num1 > 0)
          contentBounds.Offset(-(num3 * 2), 0);
        else if (num1 < 0)
          contentBounds.Offset(-1, 0);
      }
      this.ContentRectangle = contentBounds;
      matrix?.Dispose();
      if (this.ControlContainer != null)
        this.ControlContainer.Region = (Region) null;
      if (this.ShapeGraphicsPath != null)
        this.ShapeGraphicsPath.Dispose();
      this.ShapeGraphicsPath = graphicsPath;
    }

    private void InitializeComponent() => this.Name = nameof (QShapedWindow);

    [Browsable(false)]
    internal bool UsedShadeVisible
    {
      get
      {
        if (!this.Configuration.InheritWindowsSettings)
          return this.Appearance.ShadeVisible;
        return NativeHelper.ShowShadows;
      }
    }

    internal override void DrawLayeredWindow(Point location, Size size)
    {
      this.m_oPaintSize = !size.IsEmpty ? size : this.Size;
      base.DrawLayeredWindow(location, size);
    }

    internal Bitmap GetBackgroundImage()
    {
      if (this.Width < 0 || this.Height < 0)
        return (Bitmap) null;
      Bitmap backgroundImage = new Bitmap(this.Width, this.Height);
      Graphics graphics = Graphics.FromImage((Image) backgroundImage);
      this.m_bCreatingBackgroundImage = true;
      this.OnPaintBackground(new PaintEventArgs(graphics, this.ClientRectangle));
      this.OnPaint(new PaintEventArgs(graphics, this.ClientRectangle));
      this.m_bCreatingBackgroundImage = false;
      graphics.Dispose();
      return backgroundImage;
    }

    internal bool PaintControlContainer
    {
      get => this.m_bPaintControlContainer;
      set
      {
        this.m_bPaintControlContainer = value;
        if (this.ControlContainer == null || !value)
          return;
        this.ControlContainer.CreateBitmap();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FlipHorizontal
    {
      get => this.m_bFlipHorizontal;
      set
      {
        this.m_bFlipHorizontal = value;
        this.Refresh();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FlipVertical
    {
      get => this.m_bFlipVertical;
      set
      {
        this.m_bFlipVertical = value;
        this.Refresh();
      }
    }
  }
}
