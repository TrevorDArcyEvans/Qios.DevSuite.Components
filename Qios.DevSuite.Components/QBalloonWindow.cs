// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QBalloonWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QBalloonWindow : QShapedWindow
  {
    private Point m_oLastMouseOverPoint = Point.Empty;
    private string m_sText;
    private Point m_oOwnerLocation;
    private bool m_bBalloonManaged;
    private bool m_bMarkupTextRendered;
    private QMarkupText m_oMarkupText;
    private Control m_oTargetControl;
    private bool m_bAnimateWindow;
    private int m_iAnimateTime;
    private QColorScheme m_oOriginalColorScheme;
    private Font m_oOriginalFont;
    private QWeakDelegate m_oElementMouseEnterDelegate;
    private QWeakDelegate m_oElementMouseLeaveDelegate;
    private QWeakDelegate m_oElementMouseDownDelegate;
    private QWeakDelegate m_oElementMouseUpDelegate;
    private QWeakDelegate m_oElementMouseClickDelegate;
    private QWeakDelegate m_oElementLinkClickDelegate;

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the mouse enters an element.")]
    public event QMarkupTextElementEventHandler ElementMouseEnter
    {
      add => this.m_oElementMouseEnterDelegate = QWeakDelegate.Combine(this.m_oElementMouseEnterDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseEnterDelegate = QWeakDelegate.Remove(this.m_oElementMouseEnterDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Occurs when the mouse leaves this element.")]
    [QWeakEvent]
    public event QMarkupTextElementEventHandler ElementMouseLeave
    {
      add => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oElementMouseLeaveDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oElementMouseLeaveDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user presses the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseDown
    {
      add => this.m_oElementMouseDownDelegate = QWeakDelegate.Combine(this.m_oElementMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseDownDelegate = QWeakDelegate.Remove(this.m_oElementMouseDownDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user releases the mousebutton on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseUp
    {
      add => this.m_oElementMouseUpDelegate = QWeakDelegate.Combine(this.m_oElementMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseUpDelegate = QWeakDelegate.Remove(this.m_oElementMouseUpDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user clicks on this element.")]
    public event QMarkupTextElementEventHandler ElementMouseClick
    {
      add => this.m_oElementMouseClickDelegate = QWeakDelegate.Combine(this.m_oElementMouseClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementMouseClickDelegate = QWeakDelegate.Remove(this.m_oElementMouseClickDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Occurs when the user clicks on a link.")]
    public event QMarkupTextElementEventHandler ElementLinkClick
    {
      add => this.m_oElementLinkClickDelegate = QWeakDelegate.Combine(this.m_oElementLinkClickDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oElementLinkClickDelegate = QWeakDelegate.Remove(this.m_oElementLinkClickDelegate, (Delegate) value);
    }

    public QBalloonWindow()
    {
      this.SuspendLayout();
      this.ResumeLayout(false);
    }

    internal Control TargetControl
    {
      get => this.m_oTargetControl;
      set => this.m_oTargetControl = value;
    }

    internal bool AnimateWindow
    {
      get => this.m_bAnimateWindow;
      set => this.m_bAnimateWindow = value;
    }

    internal int AnimateTime
    {
      get => this.m_iAnimateTime;
      set => this.m_iAnimateTime = value;
    }

    internal bool BalloonManaged
    {
      get => this.m_bBalloonManaged;
      set => this.m_bBalloonManaged = value;
    }

    private void SecureMarkupText()
    {
      if (this.m_oMarkupText != null)
        return;
      this.m_oMarkupText = new QMarkupText(this.ColorScheme, this.Font, (Color) this.ColorScheme.MarkupText);
      this.m_oMarkupText.UpdateRequested += new QCommandUIRequestEventHandler(this.m_oMarkupText_UpdateRequested);
      this.m_oMarkupText.Root.MouseEnter += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseEnter);
      this.m_oMarkupText.Root.MouseLeave += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseLeave);
      this.m_oMarkupText.Root.MouseDown += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseDown);
      this.m_oMarkupText.Root.MouseUp += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseUp);
      this.m_oMarkupText.Root.MouseClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_MouseClick);
      this.m_oMarkupText.Root.LinkClick += new QMarkupTextElementEventHandler(this.MarkupTextRoot_LinkClick);
    }

    private void UpdateMarkupText()
    {
      if (this.Shape == null || this.m_oMarkupText == null)
        return;
      this.m_oMarkupText.SuspendProcessing();
      Size shapeSize = this.Shape.CalculateShapeSize(Size.Empty, true);
      if (this.UsedShadeVisible)
      {
        shapeSize.Width += Math.Abs(this.Appearance.ShadeOffset.X);
        shapeSize.Height += Math.Abs(this.Appearance.ShadeOffset.Y);
      }
      Size size = new Size(this.Width - shapeSize.Width > 0 ? this.Width - shapeSize.Width : 0, this.Height - shapeSize.Height > 0 ? this.Height - shapeSize.Height : 0);
      Graphics graphics = this.CreateGraphics();
      this.SecureMarkupText();
      QMarkupTextParams markupParams = new QMarkupTextParams(graphics);
      this.m_oMarkupText.Configuration.AdjustHeightToTextSize = false;
      this.m_oMarkupText.Configuration.AdjustWidthToTextSize = false;
      this.m_oMarkupText.Configuration.MarkupPadding = this.Configuration.TextPadding;
      this.m_oMarkupText.Configuration.DrawTextOptions = this.Configuration.DrawTextOptions;
      this.m_oMarkupText.Configuration.TextAlign = this.Configuration.TextAlign;
      this.m_oMarkupText.Configuration.WrapText = this.Configuration.WrapText;
      this.m_oMarkupText.Configuration.MaximumSize = new Size(-1, -1);
      this.m_oMarkupText.Configuration.CanFocus = false;
      this.m_oMarkupText.Enabled = this.Configuration.MarkupTextEnabled;
      this.m_oMarkupText.PutSize(size);
      this.m_oMarkupText.PutLocation(this.ContentRectangle.Location);
      this.m_oMarkupText.ResumeProcessing(false, false);
      this.m_oMarkupText.RenderMarkupText(markupParams);
      graphics.Dispose();
      if (this.m_bMarkupTextRendered)
        return;
      this.m_bMarkupTextRendered = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.m_oMarkupText == null)
        return;
      this.m_oMarkupText.HandleMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.IsDisposed || this.m_oMarkupText == null)
        return;
      this.m_oMarkupText.HandleMouseUp(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.IsDisposed || this.m_oMarkupText == null)
        return;
      this.m_oMarkupText.HandleMouseLeave();
      if (this.IsMoving)
        this.Cursor = Cursors.SizeAll;
      else
        this.Cursor = Cursors.Default;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.IsDisposed || this.m_oMarkupText == null)
        return;
      Point point = new Point(e.X, e.Y);
      if (point == this.m_oLastMouseOverPoint)
        return;
      this.m_oLastMouseOverPoint = point;
      this.m_oMarkupText.HandleMouseMove(e);
      if (this.m_oMarkupText.HotElement != null && this.m_oMarkupText.HotElement.IsOrHasParentOfType(typeof (QMarkupTextElementAnchor)))
        this.Cursor = Cursors.Hand;
      else if (this.IsMoving)
        this.Cursor = Cursors.SizeAll;
      else
        this.Cursor = Cursors.Default;
    }

    protected override void OnColorsChanged(EventArgs e)
    {
      base.OnColorsChanged(e);
      if (this.m_oMarkupText == null || this.ColorScheme == null)
        return;
      this.m_oMarkupText.SetProperties(this.Font, this.ColorScheme, (Color) this.ColorScheme.MarkupText);
    }

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      if (this.m_oMarkupText == null)
        return;
      this.m_oMarkupText.SetProperties(this.Font, this.ColorScheme, (Color) this.ColorScheme.MarkupText);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.IsDisposed || this.m_oMarkupText == null || !this.Configuration.DrawText)
        return;
      if (this.DesignMode && !this.m_bMarkupTextRendered || !this.m_bBalloonManaged && !this.m_bMarkupTextRendered)
        this.UpdateMarkupText();
      this.m_oMarkupText.Draw(e.Graphics);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.IsDisposed || !this.Visible && !this.DesignMode)
        return;
      this.UpdateMarkupText();
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 33 && !this.Configuration.CanActivate)
      {
        m.Result = new IntPtr(3);
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(0), 0, 0, 0, 0, 19U);
      }
      else
        base.WndProc(ref m);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Rectangle ContentRectangle
    {
      get => base.ContentRectangle;
      set
      {
        base.ContentRectangle = value;
        if (this.m_oMarkupText == null)
          return;
        this.m_oMarkupText.PutLocation(value.Location);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override double Opacity
    {
      get => base.Opacity;
      set => base.Opacity = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets the QBalloonWindowConfiguration for the QBallooonWindow.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QBalloonWindowConfiguration Configuration
    {
      get => (QBalloonWindowConfiguration) base.Configuration;
      set => this.Configuration = (QShapedWindowConfiguration) value;
    }

    protected override void HandleConfigurationChanged(bool refresh)
    {
      base.HandleConfigurationChanged(false);
      this.UpdateMarkupText();
      if (!refresh)
        return;
      this.Refresh();
    }

    [Browsable(false)]
    public QMarkupText MarkupTextObject => this.m_oMarkupText;

    [DefaultValue("")]
    [Category("QAppearance")]
    [Description("Gets or sets the Markup text for this QBalloonWindow. The formatting happens in XML.")]
    [Localizable(true)]
    public string MarkupText
    {
      get => this.m_oMarkupText != null ? this.m_oMarkupText.MarkupText : "";
      set
      {
        string str = value == null ? "" : value;
        this.SecureMarkupText();
        if (!this.Visible)
          this.m_oMarkupText.SuspendProcessing();
        this.m_oMarkupText.MarkupText = str;
        this.m_sText = QMarkupText.RemoveTags(HttpUtility.HtmlDecode(QMarkupText.ReplaceBRWithEnter(this.m_oMarkupText.Root.MarkupNode.InnerXml)));
        if (!this.Visible)
          this.m_oMarkupText.ResumeProcessing(true, false);
        if (!this.DesignMode)
          return;
        this.UpdateMarkupText();
        this.Refresh();
        if (this.ControlContainer == null)
          return;
        this.ControlContainer.Refresh();
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the text for this QMarkupLabel. This property does not apply any formatting. To format the text, use MarkupText")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    public override string Text
    {
      get => this.m_sText;
      set
      {
        string str = value == null ? "" : value;
        this.m_sText = str;
        this.SecureMarkupText();
        this.m_oMarkupText.MarkupText = QMarkupText.ReplaceEnterWithBR(HttpUtility.HtmlEncode(QMarkupText.RemoveTags(str)));
        if (!this.DesignMode)
          return;
        this.UpdateMarkupText();
        this.Refresh();
      }
    }

    protected override string BorderColorPropertyName => "BalloonWindowBorder";

    protected override string ShadeColorPropertyName => "BalloonWindowShade";

    protected override string BackColor2PropertyName => "BalloonWindowBackground2";

    protected override string BackColorPropertyName => "BalloonWindowBackground1";

    protected override void OnLostFocus(EventArgs e) => base.OnLostFocus(e);

    protected virtual void OnElementMouseEnter(QMarkupTextElementEventArgs e) => this.m_oElementMouseEnterDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseEnterDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseLeave(QMarkupTextElementEventArgs e) => this.m_oElementMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseLeaveDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseDown(QMarkupTextElementEventArgs e) => this.m_oElementMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseUp(QMarkupTextElementEventArgs e) => this.m_oElementMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnElementMouseClick(QMarkupTextElementEventArgs e) => this.m_oElementMouseClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementMouseClickDelegate, (object) this, (object) e);

    protected virtual void OnElementLinkClick(QMarkupTextElementEventArgs e) => this.m_oElementLinkClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oElementLinkClickDelegate, (object) this, (object) e);

    protected override void SetVisibleCore(bool value)
    {
      if (value)
        NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 339U);
      base.SetVisibleCore(value);
      QControlHelper.UpdateControlRoot((Control) this);
      if (this.m_bBalloonManaged || this.ControlContainer == null)
        return;
      this.ControlContainer.Visible = value;
    }

    protected override void Initialize() => base.Initialize();

    protected override QShapedWindowConfiguration CreateShapedWindowConfigurationInstance() => (QShapedWindowConfiguration) new QBalloonWindowConfiguration();

    private Size CalculateMarkupTextSize(int maxWidth, int maxHeight, Size emptyShapeSize)
    {
      if (!this.IsHandleCreated)
        this.CreateHandle();
      Graphics graphics = this.CreateGraphics();
      this.SecureMarkupText();
      this.m_oMarkupText.SuspendProcessing();
      QMarkupTextParams markupParams = new QMarkupTextParams(graphics);
      this.m_oMarkupText.Configuration.AdjustHeightToTextSize = true;
      this.m_oMarkupText.Configuration.AdjustWidthToTextSize = true;
      this.m_oMarkupText.Configuration.MarkupPadding = this.Configuration.TextPadding;
      this.m_oMarkupText.Configuration.DrawTextOptions = this.Configuration.DrawTextOptions;
      this.m_oMarkupText.Configuration.TextAlign = this.Configuration.TextAlign;
      this.m_oMarkupText.Configuration.WrapText = this.Configuration.WrapText;
      this.m_oMarkupText.Enabled = this.Configuration.MarkupTextEnabled;
      this.m_oMarkupText.Configuration.MaximumSize = new Size(maxWidth, -1);
      this.m_oMarkupText.PutLocation(Point.Empty);
      this.m_oMarkupText.ResumeProcessing(false, false);
      this.m_oMarkupText.RenderMarkupText(markupParams);
      graphics.Dispose();
      return this.m_oMarkupText.Size;
    }

    private void ApplyAutoSize(Screen screen, int xLocation)
    {
      if (this.DesignMode || this.Shape == null)
        return;
      Size shapeSize1 = this.Shape.CalculateShapeSize(Size.Empty, true);
      if (this.UsedShadeVisible)
      {
        shapeSize1.Width += Math.Abs(this.Appearance.ShadeOffset.X);
        shapeSize1.Height += Math.Abs(this.Appearance.ShadeOffset.Y);
      }
      int num = SystemInformation.VirtualScreen.Width - shapeSize1.Width;
      int maxHeight = -1;
      if (this.MaximumSize.Width > 0)
        num = Math.Min(num, this.MaximumSize.Width - shapeSize1.Width);
      if (this.MaximumSize.Height > 0)
        maxHeight = this.MaximumSize.Height - shapeSize1.Height;
      Size shapeSize2 = this.Shape.CalculateShapeSize(this.CalculateMarkupTextSize(num, maxHeight, shapeSize1), true);
      if (this.UsedShadeVisible)
      {
        shapeSize2.Width += Math.Abs(this.Appearance.ShadeOffset.X) + this.Appearance.BorderWidth;
        shapeSize2.Height += Math.Abs(this.Appearance.ShadeOffset.Y) + this.Appearance.BorderWidth;
      }
      this.Size = shapeSize2;
    }

    internal void ResetTopMost()
    {
      if (this.TopMost == this.Configuration.TopMost)
        return;
      this.TopMost = this.Configuration.TopMost;
    }

    internal void OverrideFont(Font font)
    {
      if (this.m_oOriginalFont == null)
        this.m_oOriginalFont = this.Font;
      this.Font = font;
    }

    internal void RestoreFont()
    {
      if (this.m_oOriginalFont == null)
        return;
      this.Font = this.m_oOriginalFont;
      this.m_oOriginalFont = (Font) null;
    }

    internal bool FontOverridden => this.m_oOriginalFont != null;

    internal void OverrideColorScheme(QColorScheme colorScheme)
    {
      if (this.m_oOriginalColorScheme == null)
        this.m_oOriginalColorScheme = this.ColorScheme;
      this.ColorScheme = colorScheme;
    }

    internal void RestoreColorScheme()
    {
      if (this.m_oOriginalColorScheme == null)
        return;
      this.ColorScheme = this.m_oOriginalColorScheme;
      this.m_oOriginalColorScheme = (QColorScheme) null;
    }

    internal bool ColorSchemeOverridden => this.m_oOriginalColorScheme != null;

    internal Point CalculateBounds(
      Point screenLocation,
      Control control,
      bool flipWindow,
      QBalloonWindowPositioning positioning,
      bool ignoreWorkingArea)
    {
      this.m_oTargetControl = control;
      if (this.Shape == null)
        return Point.Empty;
      this.FlipHorizontal = false;
      this.FlipVertical = false;
      bool flag1 = (this.Shape.FocusPointAnchor & AnchorStyles.Right) == AnchorStyles.Right;
      bool flag2 = (this.Shape.FocusPointAnchor & AnchorStyles.Bottom) == AnchorStyles.Bottom;
      int dx = this.Shape.FocusPoint.X;
      int dy = this.Shape.FocusPoint.Y;
      int num1 = 0;
      int y1 = 0;
      int y2 = 0;
      Point point = Point.Empty;
      Screen screen1 = Screen.FromPoint(screenLocation);
      Rectangle rectangle = ignoreWorkingArea ? screen1.Bounds : screen1.WorkingArea;
      switch (positioning)
      {
        case QBalloonWindowPositioning.CursorLocation:
          int num2 = 0;
          int num3 = 0;
          NativeMethods.ICONINFO iconInfo = new NativeMethods.ICONINFO();
          if (Cursor.Current != (Cursor) null)
          {
            NativeMethods.GetIconInfo(Cursor.Current.Handle, ref iconInfo);
            if (!iconInfo.fIcon)
            {
              num2 = iconInfo.yHotspot;
              num3 = Cursor.Current.Size.Height - iconInfo.yHotspot;
            }
          }
          Point mousePosition = Control.MousePosition;
          screen1 = Screen.FromPoint(mousePosition);
          rectangle = ignoreWorkingArea ? screen1.Bounds : screen1.WorkingArea;
          num1 = mousePosition.X;
          y1 = mousePosition.Y + num3;
          y2 = mousePosition.Y - num2;
          point = !flag2 ? new Point(num1, y1) : new Point(num1, y2);
          break;
        case QBalloonWindowPositioning.ControlBounds:
          Rectangle screen2 = control.RectangleToScreen(control.Bounds);
          screen2.Offset(-control.Left, -control.Top);
          num1 = screen2.Left;
          y1 = screen2.Bottom;
          y2 = screen2.Top;
          point = new Point(num1, y1);
          screen1 = Screen.FromPoint(point);
          rectangle = ignoreWorkingArea ? screen1.Bounds : screen1.WorkingArea;
          break;
        case QBalloonWindowPositioning.SpecifiedLocation:
          screen1 = Screen.FromPoint(screenLocation);
          rectangle = ignoreWorkingArea ? screen1.Bounds : screen1.WorkingArea;
          if (screenLocation.X < rectangle.X)
            screenLocation.X = rectangle.X;
          if (screenLocation.Y < rectangle.Y)
            screenLocation.Y = rectangle.Y;
          if (screenLocation.X > rectangle.Right)
            screenLocation.X = rectangle.Right;
          if (screenLocation.Y > rectangle.Bottom)
            screenLocation.Y = rectangle.Bottom;
          num1 = screenLocation.X;
          y1 = screenLocation.Y;
          y2 = screenLocation.Y;
          point = screenLocation;
          break;
      }
      if (this.Configuration.AutoSize)
        this.ApplyAutoSize(screen1, num1);
      this.UpdateMarkupText();
      if (flag1)
        dx = this.Width - (this.Shape.Size.Width - dx);
      if (flag2)
        dy = this.Height - (this.Shape.Size.Height - dy);
      if (flag2)
      {
        if (flipWindow && point.Y - dx < rectangle.Top)
        {
          point.Y = y1 - this.Height;
          this.FlipVertical = true;
        }
      }
      else if (y1 + this.Height - dy > rectangle.Bottom && y2 - this.Height - dy > rectangle.Top)
      {
        point.Y = y2 - this.Height;
        if (flipWindow)
          this.FlipVertical = true;
      }
      if (flag1)
      {
        if (flipWindow && point.X - dx < rectangle.Left)
        {
          point.X -= this.Width;
          this.FlipHorizontal = true;
        }
      }
      else if (point.X + this.Width - dx > rectangle.Right && flipWindow && point.X - this.Width - dx > rectangle.Left)
      {
        point.X -= this.Width;
        this.FlipHorizontal = true;
      }
      if (!this.FlipVertical)
        dy *= -1;
      if (!this.FlipHorizontal)
        dx *= -1;
      point.Offset(dx, dy);
      if (point.X + this.Width > rectangle.Right)
        point.X -= point.X + this.Width - rectangle.Right;
      else if (point.X < rectangle.Left)
        point.X = rectangle.Left;
      point.Offset(this.Configuration.LocationOffset.X, this.Configuration.LocationOffset.Y);
      this.Location = point;
      return point;
    }

    internal void RepositionToOwnerControl(bool reposition)
    {
      if (this.OwnerControl == null)
        return;
      if (reposition)
      {
        Point location = this.Location;
        location.Offset(this.OwnerControl.Location.X - this.m_oOwnerLocation.X, this.OwnerControl.Location.Y - this.m_oOwnerLocation.Y);
        NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, location.X, location.Y, 0, 0, 21U);
        this.UpdateBounds();
      }
      this.m_oOwnerLocation = this.OwnerControl.Location;
    }

    internal override string PressedButtonBackground1ColorPropertyName => "BalloonWindowPressedButtonBackground1";

    internal override string PressedButtonBackground2ColorPropertyName => "BalloonWindowPressedButtonBackground2";

    internal override string PressedButtonBorderColorPropertyName => "BalloonWindowPressedButtonBorder";

    internal override string HotButtonBackground1ColorPropertyName => "BalloonWindowHotButtonBackground1";

    internal override string HotButtonBackground2ColorPropertyName => "BalloonWindowHotButtonBackground2";

    internal override string HotButtonBorderColorPropertyName => "BalloonWindowHotButtonBorder";

    private void MarkupTextRoot_MouseEnter(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseEnter(e);

    private void MarkupTextRoot_MouseLeave(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseLeave(e);

    private void MarkupTextRoot_MouseDown(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseDown(e);

    private void MarkupTextRoot_MouseUp(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseUp(e);

    private void MarkupTextRoot_MouseClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementMouseClick(e);

    private void MarkupTextRoot_LinkClick(object sender, QMarkupTextElementEventArgs e) => this.OnElementLinkClick(e);

    private void m_oMarkupText_UpdateRequested(object sender, QCommandUIRequestEventArgs e)
    {
      if (e.Request == QCommandUIRequest.PerformLayout)
      {
        if (this.Configuration.AutoSize)
          this.ApplyAutoSize(Screen.FromPoint(this.Location), this.Location.X);
        this.UpdateMarkupText();
        this.Refresh();
      }
      else
        this.Refresh();
    }
  }
}
