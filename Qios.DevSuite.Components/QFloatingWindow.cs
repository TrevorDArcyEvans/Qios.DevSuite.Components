// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QFloatingWindow : QContainerControl, IQMouseHookClient, IQHostedComponent
  {
    private IQComponentHost m_oCustomComponentHost;
    private ArrayList m_oAdditionalUnsuppressedMouseControls;
    private Control m_oKeepWindowBehind;
    private bool m_bAllowAnimation = true;
    private bool m_bHideCaret;
    private QMouseHooker m_oMouseHooker;
    private Color m_oShadeColor = Color.Black;
    private int m_iShadingWidth = 5;
    private bool m_bTopMost;
    private bool m_bCreatingControl;
    private QControlShadeWindow m_oShadeWindow;
    private QCommandDirections m_eAnimateDirection;
    private QFloatingWindowConfiguration m_oConfiguration;
    private EventHandler m_oConfigurationChangedEventHandler;
    private QWeakDelegate m_oClosed;
    private QWeakDelegate m_oClosing;

    public QFloatingWindow() => this.InternalConstruct();

    public QFloatingWindow(IWin32Window ownerWindow)
      : base(ownerWindow)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.SuspendLayout();
      this.m_bCreatingControl = true;
      this.SetTopLevel(true);
      this.Visible = false;
      this.m_bCreatingControl = false;
      this.m_oConfigurationChangedEventHandler = new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oConfiguration = this.CreateConfigurationInstance();
      this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
      this.ResumeLayout(false);
    }

    [Category("QEvents")]
    [Description("Gets raised when the QFloatingWindow is closing")]
    [QWeakEvent]
    public event CancelEventHandler Closing
    {
      add => this.m_oClosing = QWeakDelegate.Combine(this.m_oClosing, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oClosing = QWeakDelegate.Remove(this.m_oClosing, (Delegate) value);
    }

    [Description("Gets raised when the QFloatingWindow is closed")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler Closed
    {
      add => this.m_oClosed = QWeakDelegate.Combine(this.m_oClosed, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oClosed = QWeakDelegate.Remove(this.m_oClosed, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new IWin32Window OwnerWindow
    {
      get => base.OwnerWindow;
      set => base.OwnerWindow = value;
    }

    public override ISite Site
    {
      get => base.Site;
      set
      {
        base.Site = value;
        if (!this.DesignMode)
          return;
        this.SetTopLevel(false);
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QFloatingWindowConfiguration for this QFloatingWindow.")]
    public QFloatingWindowConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration == value)
          return;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= this.m_oConfigurationChangedEventHandler;
        this.m_oConfiguration = value;
        if (this.m_oConfiguration == null)
          return;
        this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
        this.Configuration_ConfigurationChanged((object) this.m_oConfiguration, EventArgs.Empty);
      }
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        if (!this.DesignMode)
        {
          createParams.Style |= int.MinValue;
          if (this.m_bCreatingControl)
            createParams.Style &= -268435457;
        }
        createParams.ExStyle = 0;
        createParams.ExStyle |= 134217728;
        createParams.ExStyle |= 128;
        return createParams;
      }
    }

    internal IQComponentHost CustomComponentHost => this.m_oCustomComponentHost;

    internal void SetCustomComponentHost(IQComponentHost value, bool removeFromCurrentHost)
    {
      if (removeFromCurrentHost && this.m_oCustomComponentHost != null)
        this.m_oCustomComponentHost.SetComponent((object) this, (object) null);
      this.m_oCustomComponentHost = value;
    }

    internal IQFloatingWindowEventRaiser CustomEventRaiser => this.CustomComponentHost as IQFloatingWindowEventRaiser;

    [Browsable(false)]
    internal QControlShadeWindow ShadeWindow
    {
      get => this.m_oShadeWindow;
      set => this.m_oShadeWindow = value;
    }

    private bool UseAnimation => !this.DesignMode && this.m_bAllowAnimation;

    internal bool AllowAnimation
    {
      get => this.m_bAllowAnimation;
      set => this.m_bAllowAnimation = value;
    }

    private QCommandDirections AnimateDirection => this.m_eAnimateDirection;

    private bool UsedShadeVisible => !this.DesignMode && this.Configuration.UsedShowBackgroundShade;

    private int ShadingWidth
    {
      get => this.m_iShadingWidth;
      set => this.m_iShadingWidth = value;
    }

    private int UsedShadingWidth => !this.UsedShadeVisible ? 0 : this.m_iShadingWidth;

    internal Control RealOwnerWindow => Control.FromHandle((IntPtr) NativeMethods.GetWindowLong(this.Handle, -8));

    internal bool TopMost
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

    public bool ContainsControl(Control control)
    {
      if (control == null)
        return false;
      Control parentOrOwner = QControlHelper.GetParentOrOwner(control);
      while (parentOrOwner != null && parentOrOwner != this)
        parentOrOwner = QControlHelper.GetParentOrOwner(parentOrOwner);
      return parentOrOwner == this;
    }

    public virtual Rectangle CalculateBounds(
      Rectangle openingItemZone,
      Rectangle openingItemBounds,
      ref QRelativePositions openingItemRelativePosition,
      ref QCommandDirections animateDirection)
    {
      this.PerformLayout();
      Rectangle empty = Rectangle.Empty with
      {
        Width = this.Width,
        Height = this.Height
      };
      openingItemBounds.Offset(openingItemZone.Location);
      Screen screen = Screen.FromRectangle(openingItemZone);
      if (screen != null)
      {
        Rectangle workingArea = screen.WorkingArea;
        if (openingItemRelativePosition == QRelativePositions.Above && workingArea.Bottom < openingItemZone.Bottom + empty.Height && openingItemZone.Top - workingArea.Top > workingArea.Bottom - openingItemZone.Bottom)
          this.FlipShowParams(ref openingItemRelativePosition, ref animateDirection);
        else if (openingItemRelativePosition == QRelativePositions.Below && workingArea.Top > openingItemZone.Top - empty.Height && workingArea.Bottom - openingItemZone.Bottom > openingItemZone.Top - workingArea.Top)
          this.FlipShowParams(ref openingItemRelativePosition, ref animateDirection);
        else if (openingItemRelativePosition == QRelativePositions.Left && workingArea.Right < openingItemZone.Right + empty.Width && openingItemZone.Left - workingArea.Left > workingArea.Right - openingItemZone.Right)
          this.FlipShowParams(ref openingItemRelativePosition, ref animateDirection);
        else if (openingItemRelativePosition == QRelativePositions.Right && workingArea.Left > openingItemZone.Left - empty.Width && workingArea.Right - openingItemZone.Right > openingItemZone.Left - workingArea.Left)
          this.FlipShowParams(ref openingItemRelativePosition, ref animateDirection);
        switch (openingItemRelativePosition)
        {
          case QRelativePositions.Above:
            empty.X = openingItemBounds.X;
            empty.Y = openingItemZone.Bottom;
            break;
          case QRelativePositions.Below:
            empty.X = openingItemBounds.X;
            empty.Y = openingItemZone.Top - empty.Height;
            break;
          case QRelativePositions.Left:
            empty.X = openingItemZone.Right;
            empty.Y = openingItemBounds.Y;
            break;
          case QRelativePositions.Right:
            empty.X = openingItemZone.X - empty.Width;
            empty.Y = openingItemBounds.Y;
            break;
        }
        if (!workingArea.Contains(empty))
        {
          if (workingArea.Top > empty.Top)
          {
            if ((openingItemRelativePosition & QRelativePositions.Vertical) == QRelativePositions.None)
            {
              empty.Y += workingArea.Y - empty.Y;
            }
            else
            {
              empty.Height = openingItemZone.Top - workingArea.Top;
              empty.Y = workingArea.Y;
            }
          }
          else if (workingArea.Bottom < empty.Bottom)
          {
            if ((openingItemRelativePosition & QRelativePositions.Vertical) == QRelativePositions.None)
              empty.Y -= empty.Bottom - workingArea.Bottom;
            else
              empty.Height = workingArea.Bottom - openingItemZone.Bottom;
          }
          if (workingArea.Left > empty.Left)
          {
            if ((openingItemRelativePosition & QRelativePositions.Horizontal) == QRelativePositions.None)
            {
              empty.X += workingArea.X - empty.X;
            }
            else
            {
              empty.Width = openingItemZone.Left - workingArea.Left;
              empty.X = workingArea.X;
            }
          }
          else if (workingArea.Right < empty.Right)
          {
            if ((openingItemRelativePosition & QRelativePositions.Horizontal) == QRelativePositions.None)
              empty.X -= empty.Right - workingArea.Right;
            else
              empty.Width = workingArea.Right - openingItemZone.Right;
          }
        }
      }
      return empty;
    }

    public virtual void Show(Rectangle bounds, QCommandDirections animateDirection)
    {
      this.PutAnimateDirection(animateDirection);
      this.SetBounds(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
      this.PerformLayout();
      this.Visible = true;
      NativeMethods.ReleaseCapture();
      this.m_bHideCaret = NativeMethods.HideCaret(IntPtr.Zero);
      this.MouseHooker.ExitOnMouseDown = this.Configuration.CloseOnMouseDown;
      this.MouseHooker.ExitOnMouseUp = this.Configuration.CloseOnMouseUp;
      this.MouseHooker.MouseHooked = true;
      this.MouseHooker.IgnoreNextMouseUpMessage = Control.MouseButtons != MouseButtons.None;
    }

    public virtual void Show(
      Rectangle openingItemZone,
      Rectangle openingItemBounds,
      QRelativePositions openingItemRelativePosition,
      QCommandDirections animateDirection)
    {
      this.Show(this.CalculateBounds(openingItemZone, openingItemBounds, ref openingItemRelativePosition, ref animateDirection), animateDirection);
    }

    protected virtual void OnClosed(EventArgs e)
    {
      if (this.CustomEventRaiser != null)
        this.CustomEventRaiser.RaiseClosed(e);
      this.m_oClosed = QWeakDelegate.InvokeDelegate(this.m_oClosed, (object) this, (object) e);
    }

    protected virtual void OnClosing(CancelEventArgs e)
    {
      if (this.CustomEventRaiser != null)
        this.CustomEventRaiser.RaiseClosing(e);
      this.m_oClosing = QWeakDelegate.InvokeDelegate(this.m_oClosing, (object) this, (object) e);
    }

    private void SetWindowOrder(bool makeVisible)
    {
      uint uFlags = 531;
      if (makeVisible)
        uFlags |= 64U;
      NativeMethods.SetWindowPos(this.Handle, this.m_oKeepWindowBehind != null ? this.m_oKeepWindowBehind.Handle : new IntPtr(0), 0, 0, 0, 0, uFlags);
    }

    protected override void SetVisibleCore(bool value)
    {
      if (!this.m_bCreatingControl && !this.DesignMode)
      {
        if (value)
        {
          QMenuAnimationType qmenuAnimationType = this.UseAnimation ? this.Configuration.UsedAnimateShowType : QMenuAnimationType.None;
          int flagsFromDirection = NativeHelper.GetAnimateWindowFlagsFromDirection(this.AnimateDirection);
          if (qmenuAnimationType != QMenuAnimationType.None && flagsFromDirection > 0)
          {
            QControlHelper.SecureAllControlHandles((Control) this, true);
            this.SetWindowOrder(false);
            NativeMethods.AnimateWindow(this.Handle, this.Configuration.AnimateShowTime, flagsFromDirection | (qmenuAnimationType == QMenuAnimationType.Slide ? 262144 : 524288));
            if (this.Controls.Count > 0)
              this.Invalidate(true);
          }
          else
            this.SetWindowOrder(true);
          if (this.UsedShadeVisible)
            this.SetShadeVisibleCore(true, true);
        }
        else
        {
          this.SetShadeVisibleCore(false, false);
          NativeMethods.ShowWindow(this.Handle, 0);
        }
      }
      base.SetVisibleCore(value);
      QControlHelper.UpdateControlRoot((Control) this);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 33)
        m.Result = (IntPtr) 3;
      else if (m.Msg == 791)
      {
        bool visibilityProperty = QControlHelper.GetControlInternalVisibilityProperty((Control) this);
        QControlHelper.ForceControlInternalVisibilityProperty((Control) this, true);
        base.WndProc(ref m);
        QControlHelper.ForceControlInternalVisibilityProperty((Control) this, visibilityProperty);
      }
      else if (m.Msg == 792)
        base.WndProc(ref m);
      else if (m.Msg == 28)
      {
        if (m.WParam == IntPtr.Zero && this.Visible)
          this.CloseAll(QFloatingWindowCloseType.ClickedOutsideWindow);
        base.WndProc(ref m);
      }
      else
        base.WndProc(ref m);
    }

    protected override void SetOwnerWindowCore()
    {
      base.SetOwnerWindowCore();
      if (!this.IsHandleCreated)
        return;
      if (this.m_bTopMost || this.OwnerWindow != null && NativeHelper.IsTopMost(this.OwnerWindow.Handle))
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 19U);
      if (this.m_oShadeWindow == null)
        return;
      this.m_oShadeWindow.SetOwnerWindow(this.OwnerWindow, true);
    }

    protected virtual void CreateShadeWindow() => this.m_oShadeWindow = new QControlShadeWindow((Control) this, this.OwnerWindow);

    protected virtual QFloatingWindowConfiguration CreateConfigurationInstance() => new QFloatingWindowConfiguration();

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this.m_oShadeWindow != null && this.m_oShadeWindow.Visible)
        this.SetShadeWindowRegion();
      base.OnSizeChanged(e);
    }

    internal Control KeepWindowBehind
    {
      get => this.m_oKeepWindowBehind;
      set => this.m_oKeepWindowBehind = value;
    }

    internal void AddAdditionalUnsuppressedMouseControl(Control control)
    {
      if (this.m_oAdditionalUnsuppressedMouseControls == null)
        this.m_oAdditionalUnsuppressedMouseControls = new ArrayList();
      if (this.m_oAdditionalUnsuppressedMouseControls.Contains((object) control))
        return;
      this.m_oAdditionalUnsuppressedMouseControls.Add((object) control);
    }

    internal void RemoveAdditionalUnsuppressedMouseControl(Control control)
    {
      if (this.m_oAdditionalUnsuppressedMouseControls == null)
        return;
      this.m_oAdditionalUnsuppressedMouseControls.Remove((object) control);
    }

    bool IQMouseHookClient.SuppressMessageToDestination(
      int code,
      ref NativeMethods.MOUSEHOOKSTRUCT mouseHookStruct)
    {
      Control controlFromHandle = QControlHelper.GetFirstControlFromHandle(mouseHookStruct.hWnd);
      if (controlFromHandle is QCompositeShadeWindow)
        return true;
      if (controlFromHandle == this || this.ContainsControl(controlFromHandle) || this.OwnerWindow is IQCompositeContainer && this.OwnerWindow == controlFromHandle)
        return false;
      if (this.OwnerWindow is QFloatingWindow)
        return ((IQMouseHookClient) this.OwnerWindow).SuppressMessageToDestination(code, ref mouseHookStruct);
      return this.m_oAdditionalUnsuppressedMouseControls == null || !this.m_oAdditionalUnsuppressedMouseControls.Contains((object) controlFromHandle);
    }

    void IQMouseHookClient.HandleMouseWheelMessage(
      ref bool cancelMessage,
      MouseEventArgs e)
    {
      for (IntPtr index = NativeMethods.WindowFromPoint(new NativeMethods.POINT(e.X, e.Y)); index != IntPtr.Zero; index = NativeMethods.GetParent(index))
      {
        if (Control.FromHandle(index) is QFloatingWindow qfloatingWindow)
        {
          Point client = qfloatingWindow.PointToClient(new Point(e.X, e.Y));
          qfloatingWindow.OnMouseWheel(new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta));
          cancelMessage = true;
          break;
        }
      }
    }

    void IQMouseHookClient.HandleExitMessage(ref bool cancelMessage)
    {
      if (!cancelMessage)
        return;
      this.CloseAll(QFloatingWindowCloseType.ClickedOutsideWindow);
    }

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
    }

    private void SetTopMostCore()
    {
      if (!this.IsHandleCreated)
        return;
      if (this.m_bTopMost)
      {
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 19U);
      }
      else
      {
        if (this.OwnerWindow == null || NativeHelper.IsTopMost(this.OwnerWindow.Handle))
          return;
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-2), 0, 0, 0, 0, 19U);
      }
    }

    private void SetShadeVisibleCore(bool value, bool refresh)
    {
      if (value && this.m_oShadeWindow == null)
        this.CreateShadeWindow();
      if (value)
      {
        this.SetShadeWindowProperties();
        this.m_oShadeWindow.Visible = true;
      }
      else
      {
        if (this.m_oShadeWindow == null)
          return;
        this.m_oShadeWindow.Visible = false;
      }
    }

    protected virtual void SetShadeWindowProperties()
    {
      if (this.m_oShadeWindow == null)
        return;
      this.m_oShadeWindow.ShadeColor = this.ShadeColor;
      this.m_oShadeWindow.ShadeOffsetTopLeft = new Point(this.UsedShadingWidth / 2, this.UsedShadingWidth / 2);
      this.m_oShadeWindow.ShadeOffsetBottomRight = new Point(this.UsedShadingWidth / 2, this.UsedShadingWidth / 2);
      this.m_oShadeWindow.GradientLength = this.UsedShadingWidth;
      this.m_oShadeWindow.CornerSize = 3;
      this.SetShadeWindowRegion();
    }

    protected virtual void SetShadeWindowRegion() => this.m_oShadeWindow.UpdateShadeBounds();

    private void FlipShowParams(
      ref QRelativePositions openingItemRelativePosition,
      ref QCommandDirections animateDirection)
    {
      if (openingItemRelativePosition == QRelativePositions.Left)
        openingItemRelativePosition = QRelativePositions.Right;
      else if (openingItemRelativePosition == QRelativePositions.Right)
        openingItemRelativePosition = QRelativePositions.Left;
      else if (openingItemRelativePosition == QRelativePositions.Above)
        openingItemRelativePosition = QRelativePositions.Below;
      else if (openingItemRelativePosition == QRelativePositions.Below)
        openingItemRelativePosition = QRelativePositions.Above;
      if (animateDirection == QCommandDirections.Left)
        animateDirection = QCommandDirections.Right;
      else if (animateDirection == QCommandDirections.Right)
        animateDirection = QCommandDirections.Left;
      else if (animateDirection == QCommandDirections.Up)
      {
        animateDirection = QCommandDirections.Down;
      }
      else
      {
        if (animateDirection != QCommandDirections.Down)
          return;
        animateDirection = QCommandDirections.Up;
      }
    }

    internal void CloseAll(QFloatingWindowCloseType closeType)
    {
      this.Close(closeType);
      if (!(this.OwnerWindow is QFloatingWindow ownerWindow))
        return;
      ownerWindow.CloseAll(closeType);
    }

    public virtual bool Close(QFloatingWindowCloseType closeType)
    {
      if (!this.Visible)
        return false;
      CancelEventArgs e = new CancelEventArgs(false);
      this.OnClosing(e);
      if (e.Cancel)
        return false;
      this.MouseHooker.MouseHooked = false;
      if (this.m_bHideCaret)
        NativeMethods.ShowCaret(IntPtr.Zero);
      this.Hide();
      this.OnClosed(EventArgs.Empty);
      return true;
    }

    internal void PutAnimateDirection(QCommandDirections direction) => this.m_eAnimateDirection = direction;

    [Browsable(false)]
    private Color ShadeColor
    {
      get => this.m_oShadeColor;
      set => this.m_oShadeColor = value;
    }

    internal QMouseHooker MouseHooker
    {
      get
      {
        if (this.m_oMouseHooker == null)
          this.m_oMouseHooker = new QMouseHooker((IQMouseHookClient) this);
        return this.m_oMouseHooker;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (this.m_oMouseHooker != null)
        this.m_oMouseHooker.Dispose();
      base.Dispose(disposing);
    }

    IQComponentHost IQHostedComponent.ComponentHost => this.CustomComponentHost;

    void IQHostedComponent.SetComponentHost(
      IQComponentHost value,
      bool removeFromCurrentHost)
    {
      this.SetCustomComponentHost(value, removeFromCurrentHost);
    }

    Point IQMouseHookClient.PointToClient([In] Point obj0) => this.PointToClient(obj0);
  }
}
