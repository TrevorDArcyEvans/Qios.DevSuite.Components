// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QContainerControlBase
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (ScrollableControlDesigner), typeof (IDesigner))]
  [Designer(typeof (DocumentDesigner), typeof (IRootDesigner))]
  [ToolboxItem(false)]
  public class QContainerControlBase : ContainerControl, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private IWin32Window m_oOwnerWindow;
    private ArrayList m_aSetTimers;
    private bool m_bWrapTabAround;
    private QControlStyles m_eStyles;
    private Rectangle m_oCachedCurrentBounds = Rectangle.Empty;
    private Rectangle m_oCachedLeftBorderArea = Rectangle.Empty;
    private Rectangle m_oCachedRightBorderArea = Rectangle.Empty;
    private Rectangle m_oCachedTopBorderArea = Rectangle.Empty;
    private Rectangle m_oCachedBottomBorderArea = Rectangle.Empty;
    private int m_iMousePositionMarginForCornerResize = 10;
    private Size m_oMinimumClientSize = Size.Empty;
    private bool m_bIsUserSizing;
    private bool m_bCanSizeLeft;
    private bool m_bCanSizeTop;
    private bool m_bCanSizeRight;
    private bool m_bCanSizeBottom;
    private bool m_bTrackingNonClientAreaMouse;
    private QWeakDelegate m_oPaintNonClientAreaDelegate;
    private QWeakDelegate m_oNonClientAreaMouseDownDelegate;
    private QWeakDelegate m_oNonClientAreaDoubleClickDelegate;
    private QWeakDelegate m_oNonClientAreaMouseUpDelegate;
    private QWeakDelegate m_oNonClientAreaMouseMoveDelegate;
    private QWeakDelegate m_oNonClientAreaMouseLeaveDelegate;
    private QWeakDelegate m_oUserStartsSizingDelegate;
    private QWeakDelegate m_oUserEndsSizingDelegate;
    private QWeakDelegate m_oUserSizingDelegate;
    private QWeakDelegate m_oUserSizedDelegate;
    private QWeakDelegate m_oWindowsXPThemeChangedDelegate;
    private QWeakDelegate m_oTimerElapsedDelegate;

    public QContainerControlBase() => this.InternalConstruct();

    protected QContainerControlBase(IWin32Window ownerWindow)
    {
      this.m_oOwnerWindow = ownerWindow;
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.SetStyle(ControlStyles.ContainerControl, true);
      this.MinimumClientSize = (Size) this.GetDefaultValueAsValueType("MinimumClientSize");
    }

    [Obsolete("Obsolete since version 1.0.7.20. If you have overridden this method, put the code in the constructor of your QContainerControlBase override.")]
    protected virtual void Initialize()
    {
    }

    protected void SetQControlStyles(QControlStyles styles, bool value)
    {
      if (value)
        this.m_eStyles |= styles;
      else
        this.m_eStyles &= ~styles;
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        if (this.m_oOwnerWindow != null)
          createParams.Parent = QControlHelper.GetUndisposedHandle(this.m_oOwnerWindow);
        return createParams;
      }
    }

    [Description("This event gets raised when the NonClientArea should be drawn")]
    [QWeakEvent]
    [Category("QEvents")]
    public event PaintEventHandler PaintNonClientArea
    {
      add => this.m_oPaintNonClientAreaDelegate = QWeakDelegate.Combine(this.m_oPaintNonClientAreaDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oPaintNonClientAreaDelegate = QWeakDelegate.Remove(this.m_oPaintNonClientAreaDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user clicks on the NonClientArea")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QNonClientAreaMouseEventHandler NonClientAreaMouseDown
    {
      add => this.m_oNonClientAreaMouseDownDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseDownDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseDownDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseDownDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user double clicks on the NonClientArea")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QNonClientAreaMouseEventHandler NonClientAreaDoubleClick
    {
      add => this.m_oNonClientAreaDoubleClickDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaDoubleClickDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaDoubleClickDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaDoubleClickDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user releases the button on the NonClientArea.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QNonClientAreaMouseEventHandler NonClientAreaMouseUp
    {
      add => this.m_oNonClientAreaMouseUpDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseUpDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseUpDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseUpDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user moves over the NonClientArea.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QNonClientAreaMouseEventHandler NonClientAreaMouseMove
    {
      add => this.m_oNonClientAreaMouseMoveDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseMoveDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseMoveDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseMoveDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user leaves the NonClientArea.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler NonClientAreaMouseLeave
    {
      add => this.m_oNonClientAreaMouseLeaveDelegate = QWeakDelegate.Combine(this.m_oNonClientAreaMouseLeaveDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oNonClientAreaMouseLeaveDelegate = QWeakDelegate.Remove(this.m_oNonClientAreaMouseLeaveDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user starts sizing the control")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler UserStartsSizing
    {
      add => this.m_oUserStartsSizingDelegate = QWeakDelegate.Combine(this.m_oUserStartsSizingDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oUserStartsSizingDelegate = QWeakDelegate.Remove(this.m_oUserStartsSizingDelegate, (Delegate) value);
    }

    [Description("This event gets raised when the user ends the Sizing of the control")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler UserEndsSizing
    {
      add => this.m_oUserEndsSizingDelegate = QWeakDelegate.Combine(this.m_oUserEndsSizingDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oUserEndsSizingDelegate = QWeakDelegate.Remove(this.m_oUserEndsSizingDelegate, (Delegate) value);
    }

    [Description("Is raised before every resize action performed by the user and can be canceled")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QUserSizingEventHandler UserSizing
    {
      add => this.m_oUserSizingDelegate = QWeakDelegate.Combine(this.m_oUserSizingDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oUserSizingDelegate = QWeakDelegate.Remove(this.m_oUserSizingDelegate, (Delegate) value);
    }

    [Description("Is raised after every resize action performed by the user.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QUserSizedEventHandler UserSized
    {
      add => this.m_oUserSizedDelegate = QWeakDelegate.Combine(this.m_oUserSizedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oUserSizedDelegate = QWeakDelegate.Remove(this.m_oUserSizedDelegate, (Delegate) value);
    }

    [Description("Is raised when the Windows XP theme is changed")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler WindowsXPThemeChanged
    {
      add => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Combine(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Remove(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when a timer that was set with the StartTimer method elapses")]
    [QWeakEvent]
    public event QControlTimerEventHandler TimerElapsed
    {
      add => this.m_oTimerElapsedDelegate = QWeakDelegate.Combine(this.m_oTimerElapsedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oTimerElapsedDelegate = QWeakDelegate.Remove(this.m_oTimerElapsedDelegate, (Delegate) value);
    }

    [DefaultValue(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Browsable(false)]
    public Rectangle LeftBorderArea
    {
      get
      {
        if (this.m_oCachedLeftBorderArea == Rectangle.Empty)
          this.m_oCachedLeftBorderArea = new Rectangle(0, 0, this.ResizeBorderWidth, this.CurrentBounds.Height);
        return this.m_oCachedLeftBorderArea;
      }
    }

    [Browsable(false)]
    public Rectangle RightBorderArea
    {
      get
      {
        if (this.m_oCachedRightBorderArea == Rectangle.Empty)
          this.m_oCachedRightBorderArea = new Rectangle(this.CurrentBounds.Width - this.ResizeBorderWidth, 0, this.ResizeBorderWidth, this.CurrentBounds.Height);
        return this.m_oCachedRightBorderArea;
      }
    }

    [Browsable(false)]
    public Rectangle TopBorderArea
    {
      get
      {
        if (this.m_oCachedTopBorderArea == Rectangle.Empty)
          this.m_oCachedTopBorderArea = new Rectangle(0, 0, this.CurrentBounds.Width, this.ResizeBorderWidth);
        return this.m_oCachedTopBorderArea;
      }
    }

    [Browsable(false)]
    public Rectangle BottomBorderArea
    {
      get
      {
        if (this.m_oCachedBottomBorderArea == Rectangle.Empty)
          this.m_oCachedBottomBorderArea = new Rectangle(0, this.CurrentBounds.Height - this.ResizeBorderWidth, this.CurrentBounds.Width, this.ResizeBorderWidth);
        return this.m_oCachedBottomBorderArea;
      }
    }

    [DefaultValue(typeof (Size), "50, 50")]
    [Description("Gets or sets the minimumsize of the client area of the control")]
    [Localizable(true)]
    [Category("QBehavior")]
    public virtual Size MinimumClientSize
    {
      get => this.m_oMinimumClientSize;
      set => this.m_oMinimumClientSize = value;
    }

    [DefaultValue(false)]
    [Description("Indicates if the focus of the Controls should wrap around in this control when TAB is pressed.")]
    [Category("QBehavior")]
    public virtual bool WrapTabAround
    {
      get => this.m_bWrapTabAround;
      set => this.m_bWrapTabAround = value;
    }

    [Browsable(false)]
    public override RightToLeft RightToLeft
    {
      get => base.RightToLeft;
      set => base.RightToLeft = value;
    }

    [Browsable(false)]
    public new virtual Size MinimumSize
    {
      get
      {
        Size minimumClientSize = this.MinimumClientSize;
        return new Size(minimumClientSize.Width + this.ClientAreaMarginLeft + this.ClientAreaMarginRight + this.ClientAreaPaddingLeft + this.ClientAreaPaddingRight, minimumClientSize.Height + this.ClientAreaMarginTop + this.ClientAreaMarginBottom + this.ClientAreaPaddingTop + this.ClientAreaPaddingBottom);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    protected virtual IWin32Window OwnerWindow
    {
      get => this.m_oOwnerWindow;
      set => this.SetOwnerWindow(value, false);
    }

    public void SetOwnerWindow(IWin32Window window, bool force)
    {
      if (!force && this.m_oOwnerWindow == window)
        return;
      this.m_oOwnerWindow = window;
      this.SetOwnerWindowCore();
    }

    protected virtual void SetOwnerWindowCore()
    {
      if (!this.IsHandleCreated)
        return;
      NativeMethods.SetWindowLong(this.Handle, -8, (this.m_oOwnerWindow != null ? this.m_oOwnerWindow.Handle : IntPtr.Zero).ToInt32());
    }

    [Browsable(false)]
    public bool IsFirstControl => this.Parent != null && this.Parent.Controls.IndexOf((Control) this) == 0;

    [Browsable(false)]
    public bool IsLastControl => this.Parent != null && this.Parent.Controls.IndexOf((Control) this) == this.Parent.Controls.Count - 1;

    [Browsable(false)]
    public bool IsSingleChildControl => this.Parent != null && this.Parent.Controls.Count == 1;

    [Browsable(false)]
    public QContainerControlBase NextSibling
    {
      get
      {
        int controlIndex = this.ControlIndex;
        if (controlIndex < 0)
          return (QContainerControlBase) null;
        return controlIndex >= this.Parent.Controls.Count - 1 ? (QContainerControlBase) null : this.Parent.Controls[controlIndex + 1] as QContainerControlBase;
      }
    }

    [Browsable(false)]
    public QContainerControlBase PreviousSibling
    {
      get
      {
        int controlIndex = this.ControlIndex;
        return controlIndex <= 0 ? (QContainerControlBase) null : this.Parent.Controls[controlIndex - 1] as QContainerControlBase;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual int ControlIndex
    {
      get => this.Parent == null ? -1 : this.Parent.Controls.IndexOf((Control) this);
      set
      {
        if (this.Parent == null)
          return;
        this.Parent.Controls.SetChildIndex((Control) this, value);
      }
    }

    public void StartTimer(int timerId, int interval)
    {
      if (this.IsHandleCreated)
        NativeMethods.SetTimer(this.Handle, new IntPtr(timerId), (uint) interval, (QTimerCallbackDelegate) null);
      QTimerDefinition timerWithId = this.FindTimerWithID(timerId);
      if (timerWithId != null)
      {
        timerWithId.TimerInteval = interval;
      }
      else
      {
        if (this.m_aSetTimers == null)
          this.m_aSetTimers = new ArrayList();
        this.m_aSetTimers.Add((object) new QTimerDefinition(timerId, interval));
      }
    }

    public void StopTimer(int timerId)
    {
      if (this.IsHandleCreated)
        NativeMethods.KillTimer(this.Handle, new IntPtr(timerId));
      QTimerDefinition timerWithId = this.FindTimerWithID(timerId);
      if (timerWithId == null)
        return;
      this.m_aSetTimers.Remove((object) timerWithId);
    }

    private QTimerDefinition FindTimerWithID(int timerID)
    {
      if (this.m_aSetTimers == null)
        return (QTimerDefinition) null;
      for (int index = 0; index < this.m_aSetTimers.Count; ++index)
      {
        QTimerDefinition aSetTimer = (QTimerDefinition) this.m_aSetTimers[index];
        if (aSetTimer.TimerID == timerID)
          return aSetTimer;
      }
      return (QTimerDefinition) null;
    }

    public virtual void RefreshNoClientArea(bool refreshChildren)
    {
      if (this.IsDisposed)
        return;
      NativeMethods.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 1121);
      if (!refreshChildren)
        return;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QContainerControlBase)
          ((QContainerControlBase) this.Controls[index]).RefreshNoClientArea();
      }
    }

    public virtual void RefreshNoClientArea() => this.RefreshNoClientArea(false);

    public virtual void PerformNonClientAreaLayout() => this.PerformNonClientAreaLayout(false);

    public virtual void PerformNonClientAreaLayout(bool layoutChildren)
    {
      NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 39U);
      if (!layoutChildren)
        return;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QContainerControlBase)
          ((QContainerControlBase) this.Controls[index]).PerformNonClientAreaLayout(layoutChildren);
      }
    }

    public Point PointToControl(Point clientPoint)
    {
      clientPoint.X += this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft;
      clientPoint.Y += this.ClientAreaMarginTop + this.ClientAreaPaddingTop;
      return clientPoint;
    }

    public Rectangle NonClientRectangleToClient(Rectangle nonClientRectangle)
    {
      nonClientRectangle.X -= this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft;
      nonClientRectangle.Y -= this.ClientAreaMarginTop + this.ClientAreaPaddingTop;
      return nonClientRectangle;
    }

    [Browsable(false)]
    public bool IsUserSizing => this.m_bIsUserSizing;

    [Browsable(false)]
    public virtual Rectangle CaptionBounds => Rectangle.Empty;

    [Browsable(false)]
    protected virtual int ClientAreaMarginTop => 0;

    [Browsable(false)]
    protected virtual int ClientAreaMarginLeft => 0;

    [Browsable(false)]
    protected virtual int ClientAreaMarginRight => 0;

    [Browsable(false)]
    protected virtual int ClientAreaMarginBottom => 0;

    [Browsable(false)]
    protected virtual int ClientAreaPaddingTop => 0;

    [Browsable(false)]
    protected virtual int ClientAreaPaddingLeft => 0;

    [Browsable(false)]
    protected virtual int ClientAreaPaddingRight => 0;

    [Browsable(false)]
    protected virtual int ClientAreaPaddingBottom => 0;

    [Browsable(false)]
    protected virtual int ResizeBorderWidth => 0;

    [Browsable(false)]
    protected virtual bool CanSizeTop
    {
      get => this.m_bCanSizeTop;
      set => this.m_bCanSizeTop = value;
    }

    [Browsable(false)]
    protected virtual bool CanSizeLeft
    {
      get => this.m_bCanSizeLeft;
      set => this.m_bCanSizeLeft = value;
    }

    [Browsable(false)]
    protected virtual bool CanSizeRight
    {
      get => this.m_bCanSizeRight;
      set => this.m_bCanSizeRight = value;
    }

    [Browsable(false)]
    protected virtual bool CanSizeBottom
    {
      get => this.m_bCanSizeBottom;
      set => this.m_bCanSizeBottom = value;
    }

    [Browsable(false)]
    public Rectangle CurrentBounds
    {
      get
      {
        if (this.m_oCachedCurrentBounds == Rectangle.Empty)
          this.m_oCachedCurrentBounds = NativeHelper.GetWindowBounds((Control) this);
        return this.m_oCachedCurrentBounds;
      }
    }

    protected void DefaultOnPaintBackground(PaintEventArgs e) => this.OnPaintBackground(e);

    protected virtual void OnPaintNonClientArea(PaintEventArgs e)
    {
      e.Graphics.Clear(this.BackColor);
      this.m_oPaintNonClientAreaDelegate = QWeakDelegate.InvokeDelegate(this.m_oPaintNonClientAreaDelegate, (object) this, (object) e);
    }

    protected virtual void OnNonClientAreaMouseDown(QNonClientAreaMouseEventArgs e) => this.m_oNonClientAreaMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnNonClientAreaDoubleClick(QNonClientAreaMouseEventArgs e) => this.m_oNonClientAreaDoubleClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaDoubleClickDelegate, (object) this, (object) e);

    protected virtual void OnNonClientAreaMouseUp(QNonClientAreaMouseEventArgs e) => this.m_oNonClientAreaMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnNonClientAreaMouseLeave(EventArgs e) => this.m_oNonClientAreaMouseLeaveDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseLeaveDelegate, (object) this, (object) e);

    protected virtual void OnNonClientAreaMouseMove(QNonClientAreaMouseEventArgs e) => this.m_oNonClientAreaMouseMoveDelegate = QWeakDelegate.InvokeDelegate(this.m_oNonClientAreaMouseMoveDelegate, (object) this, (object) e);

    protected virtual void OnWindowsXPThemeChanged(EventArgs e) => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowsXPThemeChangedDelegate, (object) this, (object) e);

    protected virtual void OnTimerElapsed(QControlTimerEventArgs e) => this.m_oTimerElapsedDelegate = QWeakDelegate.InvokeDelegate(this.m_oTimerElapsedDelegate, (object) this, (object) e);

    protected virtual void OnUserStartsSizing(EventArgs e) => this.m_oUserStartsSizingDelegate = QWeakDelegate.InvokeDelegate(this.m_oUserStartsSizingDelegate, (object) this, (object) e);

    protected virtual void OnUserEndsSizing(EventArgs e) => this.m_oUserEndsSizingDelegate = QWeakDelegate.InvokeDelegate(this.m_oUserEndsSizingDelegate, (object) this, (object) e);

    protected virtual void OnUserSizing(QUserSizingEventArgs e) => this.m_oUserSizingDelegate = QWeakDelegate.InvokeDelegate(this.m_oUserSizingDelegate, (object) this, (object) e);

    protected virtual void OnUserSized(QUserSizedEventArgs e) => this.m_oUserSizedDelegate = QWeakDelegate.InvokeDelegate(this.m_oUserSizedDelegate, (object) this, (object) e);

    internal void SetCanSizeProperties(bool left, bool top, bool right, bool bottom)
    {
      if (left == this.CanSizeLeft && top == this.CanSizeTop && right == this.CanSizeRight && bottom == this.CanSizeBottom)
        return;
      this.CanSizeLeft = left;
      this.CanSizeTop = top;
      this.CanSizeRight = right;
      this.CanSizeBottom = bottom;
      this.PerformNonClientAreaLayout();
      this.RefreshNoClientArea();
    }

    private QSizingAction GetSizingAction(int x, int y)
    {
      if (this.m_bCanSizeLeft && this.LeftBorderArea.Contains(x, y))
      {
        if (this.m_bCanSizeTop && y < this.m_iMousePositionMarginForCornerResize)
          return QSizingAction.SizingTopLeft;
        return this.m_bCanSizeBottom && y > this.Height - this.m_iMousePositionMarginForCornerResize ? QSizingAction.SizingBottomLeft : QSizingAction.SizingLeft;
      }
      if (this.m_bCanSizeRight && this.RightBorderArea.Contains(x, y))
      {
        if (this.m_bCanSizeTop && y < this.m_iMousePositionMarginForCornerResize)
          return QSizingAction.SizingTopRight;
        return this.m_bCanSizeBottom && y > this.Height - this.m_iMousePositionMarginForCornerResize ? QSizingAction.SizingBottomRight : QSizingAction.SizingRight;
      }
      if (this.m_bCanSizeTop && this.TopBorderArea.Contains(x, y))
      {
        if (this.m_bCanSizeLeft && x < this.m_iMousePositionMarginForCornerResize)
          return QSizingAction.SizingTopLeft;
        return this.m_bCanSizeRight && x > this.Width - this.m_iMousePositionMarginForCornerResize ? QSizingAction.SizingTopRight : QSizingAction.SizingTop;
      }
      if (!this.m_bCanSizeBottom || !this.BottomBorderArea.Contains(x, y))
        return QSizingAction.None;
      if (this.m_bCanSizeLeft && x < this.m_iMousePositionMarginForCornerResize)
        return QSizingAction.SizingBottomLeft;
      return this.m_bCanSizeRight && x > this.Width - this.m_iMousePositionMarginForCornerResize ? QSizingAction.SizingBottomRight : QSizingAction.SizingBottom;
    }

    private void ClearCachedObjects()
    {
      this.m_oCachedCurrentBounds = Rectangle.Empty;
      this.m_oCachedLeftBorderArea = Rectangle.Empty;
      this.m_oCachedRightBorderArea = Rectangle.Empty;
      this.m_oCachedBottomBorderArea = Rectangle.Empty;
      this.m_oCachedTopBorderArea = Rectangle.Empty;
    }

    protected object GetDefaultValue(string propertyName) => QMisc.GetDefaultValue((object) this, propertyName);

    protected ValueType GetDefaultValueAsValueType(string propertyName) => QMisc.GetDefaultValueAsValueType((object) this, propertyName);

    private QNonClientAreaMouseEventArgs GetNonClientMouseEventArgs(
      Message m)
    {
      Point forMouseMessages = this.GetControlPointForMouseMessages(m.LParam);
      QSizingAction sizingAction = this.GetSizingAction(forMouseMessages.X, forMouseMessages.Y);
      QNonClientAreaLocation location = QNonClientAreaLocation.Nowhere;
      MouseButtons buttons = MouseButtons.None;
      QMouseAction mouseAction = QMouseAction.None;
      if (sizingAction != QSizingAction.None)
        location = QNonClientAreaLocation.SizingArea;
      else if (this.CaptionBounds.Contains(forMouseMessages))
        location = QNonClientAreaLocation.Caption;
      if (m.Msg == 161)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.Left;
      }
      else if (m.Msg == 164)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.Right;
      }
      else if (m.Msg == 171)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.XButton1;
      }
      else if (m.Msg == 167)
      {
        mouseAction = QMouseAction.Down;
        buttons = MouseButtons.Middle;
      }
      else if (m.Msg == 162)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.Left;
      }
      else if (m.Msg == 165)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.Right;
      }
      else if (m.Msg == 172)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.XButton1;
      }
      else if (m.Msg == 168)
      {
        mouseAction = QMouseAction.Up;
        buttons = MouseButtons.Middle;
      }
      else if (m.Msg == 163)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.Left;
      }
      else if (m.Msg == 166)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.Right;
      }
      else if (m.Msg == 173)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.XButton1;
      }
      else if (m.Msg == 169)
      {
        mouseAction = QMouseAction.DoubleClick;
        buttons = MouseButtons.Middle;
      }
      else if (m.Msg == 160)
        mouseAction = QMouseAction.Move;
      return new QNonClientAreaMouseEventArgs(mouseAction, buttons, 1, forMouseMessages.X, forMouseMessages.Y, location, sizingAction);
    }

    private Point GetControlPointForMouseMessages(IntPtr param) => this.PointToControl(this.PointToClient(new Point(param.ToInt32())));

    private void TrackNonClientAreaMouse()
    {
      if (this.m_bTrackingNonClientAreaMouse)
        return;
      NativeMethods.TRACKMOUSEEVENT lpEventTrack = new NativeMethods.TRACKMOUSEEVENT();
      lpEventTrack.cbSize = (uint) Marshal.SizeOf((object) lpEventTrack);
      lpEventTrack.dwFlags = 18U;
      lpEventTrack.hwndTrack = this.Handle;
      this.m_bTrackingNonClientAreaMouse = NativeMethods.TrackMouseEvent(ref lpEventTrack);
    }

    protected override bool ProcessTabKey(bool forward) => this.SelectNextControl(this.ActiveControl, forward, true, true, this.m_bWrapTabAround);

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 794)
      {
        this.ClearCachedObjects();
        base.WndProc(ref m);
        this.OnWindowsXPThemeChanged(EventArgs.Empty);
      }
      else if (m.Msg == 275)
      {
        this.OnTimerElapsed(new QControlTimerEventArgs(m.WParam.ToInt32()));
        base.WndProc(ref m);
      }
      else if (m.Msg == 791)
      {
        if (((int) m.LParam & 2) == 2 && (((int) m.LParam & 1) != 1 || this.Visible))
        {
          Rectangle rectangle1 = new Rectangle(this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft, this.ClientAreaMarginTop + this.ClientAreaPaddingTop, this.CurrentBounds.Width - this.ClientAreaMarginLeft - this.ClientAreaMarginRight - this.ClientAreaPaddingLeft - this.ClientAreaPaddingRight, this.CurrentBounds.Height - this.ClientAreaMarginTop - this.ClientAreaMarginBottom - this.ClientAreaPaddingTop - this.ClientAreaPaddingBottom);
          Rectangle rectangle2 = new Rectangle(0, 0, this.CurrentBounds.Width, this.CurrentBounds.Height);
          Graphics graphics = Graphics.FromHdc(m.WParam);
          Region savedRegion = QControlPaint.AdjustClip(graphics, new Region(rectangle2), CombineMode.Replace);
          this.OnPaintNonClientArea(new PaintEventArgs(graphics, rectangle2));
          QControlPaint.RestoreClip(graphics, savedRegion);
          graphics.Dispose();
        }
        base.WndProc(ref m);
      }
      else if (m.Msg == 133)
      {
        this.ClearCachedObjects();
        base.WndProc(ref m);
        this.ClearCachedObjects();
        if (this.ClientAreaMarginLeft == 0 && this.ClientAreaMarginTop == 0 && this.ClientAreaMarginRight == 0 && this.ClientAreaMarginBottom == 0 && this.ClientAreaPaddingLeft == 0 && this.ClientAreaPaddingTop == 0 && this.ClientAreaPaddingRight == 0 && this.ClientAreaPaddingBottom == 0)
          return;
        Rectangle rect1 = new Rectangle(this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft, this.ClientAreaMarginTop + this.ClientAreaPaddingTop, this.CurrentBounds.Width - this.ClientAreaMarginLeft - this.ClientAreaMarginRight - this.ClientAreaPaddingLeft - this.ClientAreaPaddingRight, this.CurrentBounds.Height - this.ClientAreaMarginTop - this.ClientAreaMarginBottom - this.ClientAreaPaddingTop - this.ClientAreaPaddingBottom);
        Rectangle rectangle = new Rectangle(0, 0, this.CurrentBounds.Width, this.CurrentBounds.Height);
        Region region = new Region(rectangle);
        region.Exclude(rect1);
        if (this.HScroll && this.VScroll)
        {
          Rectangle rect2 = new Rectangle(rect1.Right - SystemInformation.VerticalScrollBarWidth, rect1.Bottom - SystemInformation.HorizontalScrollBarHeight, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
          region.Union(rect2);
        }
        IntPtr num1 = IntPtr.Zero;
        IntPtr num2 = IntPtr.Zero;
        QOffscreenBitmapSet bitmapSet = (QOffscreenBitmapSet) null;
        try
        {
          num1 = NativeMethods.GetWindowDC(m.HWnd);
          num2 = NativeMethods.CreateCompatibleDC(num1);
          bitmapSet = QOffscreenBitmapsManager.GetFreeBitmapSet();
          IntPtr hObject = bitmapSet.SecureOffscreenDesktopBitmap(rectangle.Size);
          NativeMethods.SelectObject(num2, hObject);
          using (Graphics graphics = Graphics.FromHdc(num2))
          {
            graphics.Clip = region;
            this.OnPaintNonClientArea(new PaintEventArgs(graphics, rectangle));
          }
          int num3 = 0;
          int num4 = 0;
          NativeMethods.BitBlt(num1, num3, num4, rectangle.Width, this.ClientAreaMarginTop + this.ClientAreaPaddingTop, num2, num3, num4, 13369376);
          int num5 = 0;
          int num6 = 0;
          NativeMethods.BitBlt(num1, num5, num6, this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft, rectangle.Height, num2, num5, num6, 13369376);
          int num7 = 0;
          int num8 = rectangle.Height - this.ClientAreaMarginBottom - this.ClientAreaPaddingBottom;
          NativeMethods.BitBlt(num1, num7, num8, rectangle.Width, this.ClientAreaMarginBottom + this.ClientAreaPaddingBottom, num2, num7, num8, 13369376);
          int num9 = rectangle.Width - this.ClientAreaMarginRight - this.ClientAreaPaddingRight;
          int num10 = 0;
          NativeMethods.BitBlt(num1, num9, num10, this.ClientAreaMarginRight + this.ClientAreaPaddingRight, rectangle.Height, num2, num9, num10, 13369376);
          if (!this.HScroll || !this.VScroll)
            return;
          int num11 = rectangle.Width - this.ClientAreaMarginRight - this.ClientAreaPaddingRight - SystemInformation.VerticalScrollBarWidth;
          int num12 = rectangle.Height - this.ClientAreaMarginBottom - this.ClientAreaPaddingBottom - SystemInformation.HorizontalScrollBarHeight;
          NativeMethods.BitBlt(num1, num11, num12, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight, num2, num11, num12, 13369376);
        }
        finally
        {
          if (num2 != IntPtr.Zero)
            NativeMethods.DeleteDC(num2);
          if (num1 != IntPtr.Zero)
            NativeMethods.ReleaseDC(m.HWnd, num1);
          if (bitmapSet != null)
            QOffscreenBitmapsManager.FreeBitmapSet(bitmapSet);
        }
      }
      else if (m.Msg == 131)
      {
        this.ClearCachedObjects();
        if (m.WParam != IntPtr.Zero)
        {
          NativeMethods.NCCALCSIZE_PARAMS valueType = (NativeMethods.NCCALCSIZE_PARAMS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.NCCALCSIZE_PARAMS));
          valueType.rgrc0.left += this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft;
          valueType.rgrc0.top += this.ClientAreaMarginTop + this.ClientAreaPaddingTop;
          valueType.rgrc0.bottom -= this.ClientAreaMarginBottom + this.ClientAreaPaddingBottom;
          valueType.rgrc0.right -= this.ClientAreaMarginRight + this.ClientAreaPaddingRight;
          valueType.rgrc1 = valueType.rgrc0;
          Marshal.StructureToPtr((object) valueType, m.LParam, false);
          base.WndProc(ref m);
          m.Result = IntPtr.Zero;
        }
        else
        {
          NativeMethods.RECT valueType = (NativeMethods.RECT) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.RECT));
          valueType.left += this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft;
          valueType.top += this.ClientAreaMarginTop + this.ClientAreaPaddingTop;
          valueType.bottom -= this.ClientAreaMarginBottom - this.ClientAreaPaddingBottom;
          valueType.right -= this.ClientAreaMarginRight - this.ClientAreaPaddingRight;
          Marshal.StructureToPtr((object) valueType, m.LParam, false);
          base.WndProc(ref m);
          m.Result = IntPtr.Zero;
        }
      }
      else if (m.Msg == 132)
      {
        base.WndProc(ref m);
        Point forMouseMessages = this.GetControlPointForMouseMessages(m.LParam);
        switch (this.GetSizingAction(forMouseMessages.X, forMouseMessages.Y))
        {
          case QSizingAction.None:
            if (this.CaptionBounds.Contains(forMouseMessages))
            {
              m.Result = new IntPtr(2);
              break;
            }
            if (forMouseMessages.X >= this.ClientAreaMarginLeft + this.ClientAreaPaddingLeft && forMouseMessages.X < this.CurrentBounds.Width - this.ClientAreaMarginRight - this.ClientAreaPaddingRight && forMouseMessages.Y >= this.ClientAreaMarginTop + this.ClientAreaPaddingTop && forMouseMessages.Y < this.CurrentBounds.Height - this.ClientAreaMarginBottom - this.ClientAreaPaddingBottom)
              break;
            m.Result = new IntPtr(18);
            break;
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
      else if ((m.Msg & 65520) == 160)
      {
        QNonClientAreaMouseEventArgs clientMouseEventArgs = this.GetNonClientMouseEventArgs(m);
        switch (clientMouseEventArgs.MouseAction)
        {
          case QMouseAction.Down:
            this.OnNonClientAreaMouseDown(clientMouseEventArgs);
            break;
          case QMouseAction.DoubleClick:
            this.OnNonClientAreaMouseDown(clientMouseEventArgs);
            this.OnNonClientAreaDoubleClick(clientMouseEventArgs);
            break;
          case QMouseAction.Up:
            this.OnNonClientAreaMouseUp(clientMouseEventArgs);
            break;
          case QMouseAction.Move:
            this.TrackNonClientAreaMouse();
            this.OnNonClientAreaMouseMove(clientMouseEventArgs);
            break;
        }
        if (clientMouseEventArgs.CancelDefaultAction)
          return;
        base.WndProc(ref m);
      }
      else if (m.Msg == 674)
      {
        this.m_bTrackingNonClientAreaMouse = false;
        this.OnNonClientAreaMouseLeave(EventArgs.Empty);
        base.WndProc(ref m);
      }
      else if (m.Msg == 561)
      {
        this.m_bIsUserSizing = true;
        this.OnUserStartsSizing(EventArgs.Empty);
        base.WndProc(ref m);
      }
      else if (m.Msg == 562)
      {
        this.m_bIsUserSizing = false;
        this.OnUserEndsSizing(EventArgs.Empty);
        base.WndProc(ref m);
      }
      else if (m.Msg == 70)
      {
        this.ClearCachedObjects();
        NativeMethods.WINDOWPOS valueType = (NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.WINDOWPOS));
        valueType.flags |= (this.m_eStyles & QControlStyles.NeverCopyBitsOnBoundsChange) == QControlStyles.NeverCopyBitsOnBoundsChange ? 256U : 0U;
        valueType.flags |= (this.m_eStyles & QControlStyles.NeverRedrawOnBoundsChange) == QControlStyles.NeverRedrawOnBoundsChange ? 8U : 0U;
        if (this.IsUserSizing)
        {
          valueType.flags |= 4U;
          valueType.flags |= (this.m_eStyles & QControlStyles.DoNotCopyBitsOnUserSize) == QControlStyles.DoNotCopyBitsOnUserSize ? 256U : 0U;
          valueType.flags |= (this.m_eStyles & QControlStyles.DoNotNotifyChangeOnUserSize) == QControlStyles.DoNotNotifyChangeOnUserSize ? 1024U : 0U;
          valueType.flags |= (this.m_eStyles & QControlStyles.DoNotRedrawOnUserSize) == QControlStyles.DoNotRedrawOnUserSize ? 8U : 0U;
          QUserSizingEventArgs e = new QUserSizingEventArgs(new Size(valueType.cx, valueType.cy));
          this.OnUserSizing(e);
          if (e.Cancel)
          {
            valueType.flags |= 3U;
          }
          else
          {
            valueType.cx = e.NewSize.Width;
            valueType.cy = e.NewSize.Height;
            Size minimumSize = this.MinimumSize;
            if (valueType.cx < minimumSize.Width)
            {
              if (valueType.x > this.Left)
                valueType.x = this.Right - minimumSize.Width;
              valueType.cx = minimumSize.Width;
            }
            if (valueType.cy < minimumSize.Height)
            {
              if (valueType.y > this.Top)
                valueType.y = this.Bottom - minimumSize.Height;
              valueType.cy = minimumSize.Height;
            }
          }
        }
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
        base.WndProc(ref m);
      }
      else if (m.Msg == 71)
      {
        this.ClearCachedObjects();
        if (this.IsUserSizing)
        {
          NativeMethods.WINDOWPOS valueType = (NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.WINDOWPOS));
          this.OnUserSized(new QUserSizedEventArgs(new Size(valueType.cx, valueType.cy)));
        }
        base.WndProc(ref m);
      }
      else if (m.Msg == 277)
      {
        base.WndProc(ref m);
        if ((this.m_eStyles & QControlStyles.DoNotInvalidateOnScroll) == QControlStyles.DoNotInvalidateOnScroll)
          return;
        this.Invalidate(false);
      }
      else if (m.Msg == 276)
      {
        base.WndProc(ref m);
        if ((this.m_eStyles & QControlStyles.DoNotInvalidateOnScroll) == QControlStyles.DoNotInvalidateOnScroll)
          return;
        this.Invalidate(false);
      }
      else
        base.WndProc(ref m);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (width == 0 || height == 0)
        base.SetBoundsCore(x, y, width, height, specified);
      else
        base.SetBoundsCore(x, y, Math.Max(width, this.MinimumSize.Width), Math.Max(height, this.MinimumSize.Height), specified);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.Invalidate();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      this.ClearCachedObjects();
      base.OnLayout(levent);
    }

    protected override void OnResize(EventArgs e)
    {
      if (this.Parent != null && this.IsUserSizing)
        this.Parent.PerformLayout((Control) this, (string) null);
      base.OnResize(e);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      if (this.m_aSetTimers == null)
        return;
      for (int index = 0; index < this.m_aSetTimers.Count; ++index)
      {
        QTimerDefinition aSetTimer = (QTimerDefinition) this.m_aSetTimers[index];
        NativeMethods.SetTimer(this.Handle, new IntPtr(aSetTimer.TimerID), (uint) aSetTimer.TimerInteval, (QTimerCallbackDelegate) null);
      }
    }

    protected override void Dispose(bool disposing) => base.Dispose(disposing);
  }
}
