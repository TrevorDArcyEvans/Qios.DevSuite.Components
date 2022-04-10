// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockingWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QDockingWindow : QDockControl
  {
    private const int SlidingTimerId = 17;
    private static Icon m_oUnpinnedIcon;
    private static Icon m_oPinnedIcon;
    private QWeakEventConsumerCollection m_oEventConsumers;
    private string m_sWindowGroupName;
    private bool m_bTimerRunning;
    private int m_iSlidingTimerInterval = 30;
    private bool m_bCanSlide = true;
    private int m_iSlidingTime = 500;
    private bool m_bSlideWithMotion = true;
    private QDockingWindow.WindowSlidingAction m_eSlidingAction;
    private QDockingWindow.WindowSlidedState m_eWindowSlidedState;
    private int m_iSlidingStep = 30;
    private int m_iSlidingCurrentStep;
    private Rectangle m_oCachedCaptionArea = Rectangle.Empty;
    private Rectangle m_oCachedCloseButtonArea = Rectangle.Empty;
    private Rectangle m_oCachedPinButtonArea = Rectangle.Empty;
    private QButtonState m_eCloseButtonStyle = QButtonState.Normal;
    private QButtonState m_ePinButtonStyle = QButtonState.Normal;
    private bool m_bShowInTaskbarUndocked;
    private QModifierKeys m_eDockModifierKeys;
    private FormBorderStyle m_oFormBorderStyleUndocked = FormBorderStyle.SizableToolWindow;
    private bool m_bWindowsXPThemeTried;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private QDockContainerAppearance m_oDockContainerAppearance;
    private EventHandler m_oOwnerSizeChangedEventHandler;
    private EventHandler m_oDockBarVisibleChangedEventHandler;
    private EventHandler m_oDockBarSizeChangedEventHandler;
    private EventHandler m_oDockContainerAppearanceChangedEventHandler;
    private QWeakDelegate m_oLoadDelegate;
    private QWeakDelegate m_oWindowDockDelegate;
    private QWeakDelegate m_oWindowSlideDelegate;
    private QWeakDelegate m_oWindowSlidedDelegate;

    public QDockingWindow(Form owner)
    {
      this.InternalConstruct();
      this.Owner = owner;
    }

    public QDockingWindow() => this.InternalConstruct();

    private void InternalConstruct()
    {
      this.SuspendLayout();
      if (QDockingWindow.m_oUnpinnedIcon == null)
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        QDockingWindow.m_oUnpinnedIcon = new Icon(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Icons.PinOut.ico"));
        QDockingWindow.m_oPinnedIcon = new Icon(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Icons.PinIn.ico"));
      }
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oDockContainerAppearanceChangedEventHandler = new EventHandler(this.DockContainerAppearance_AppearanceChanged);
      this.m_oDockContainerAppearance = new QDockContainerAppearance();
      this.m_oDockContainerAppearance.AppearanceChanged += this.m_oDockContainerAppearanceChangedEventHandler;
      this.SetStyle(ControlStyles.Selectable, true);
      this.Text = QResources.GetGeneral("QDockingWindow_Text");
      this.Width = 100;
      this.Height = 100;
      this.m_oOwnerSizeChangedEventHandler = new EventHandler(this.Owner_SizeChanged);
      this.m_oDockBarSizeChangedEventHandler = new EventHandler(this.DockBar_SizeChanged);
      this.m_oDockBarVisibleChangedEventHandler = new EventHandler(this.DockBar_VisibleChanged);
      this.ResumeLayout(false);
    }

    [QWeakEvent]
    [Description("Occurs before a QDockingWindow is displayed for the first time.")]
    [Category("QEvents")]
    public event EventHandler Load
    {
      add => this.m_oLoadDelegate = QWeakDelegate.Combine(this.m_oLoadDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oLoadDelegate = QWeakDelegate.Remove(this.m_oLoadDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Occurs before a QDockingWindow is docked or undocked.")]
    [Category("QEvents")]
    public event QDockEventHandler WindowDock
    {
      add => this.m_oWindowDockDelegate = QWeakDelegate.Combine(this.m_oWindowDockDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oWindowDockDelegate = QWeakDelegate.Remove(this.m_oWindowDockDelegate, (Delegate) value);
    }

    [Description("Occurs before a QDockingWindow is sliding in or sliding out.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QSlideEventHandler WindowSlide
    {
      add => this.m_oWindowSlideDelegate = QWeakDelegate.Combine(this.m_oWindowSlideDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oWindowSlideDelegate = QWeakDelegate.Remove(this.m_oWindowSlideDelegate, (Delegate) value);
    }

    [Description("Occurs after a QDockingWindow has slided in or slided out.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QSlideEventHandler WindowSlided
    {
      add => this.m_oWindowSlidedDelegate = QWeakDelegate.Combine(this.m_oWindowSlidedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oWindowSlidedDelegate = QWeakDelegate.Remove(this.m_oWindowSlidedDelegate, (Delegate) value);
    }

    public override QDockingWindow CurrentWindow => this;

    public override Form Owner
    {
      get => base.Owner;
      set
      {
        if (base.Owner == value)
          return;
        if (base.Owner != null)
          this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oOwnerSizeChangedEventHandler);
        base.Owner = value;
        if (base.Owner == null)
          return;
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oOwnerSizeChangedEventHandler, (object) base.Owner, "SizeChanged"));
      }
    }

    public override string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        if (this.TopLevelControl is QDockForm)
          (this.TopLevelControl as QDockForm).SetTextToActiveControl();
        if (this.DockBar == null || !this.IsInSlideMode)
          return;
        this.DockBar.PerformLayout();
        this.DockBar.Refresh();
      }
    }

    public bool ShouldSerializeDockContainerAppearance() => !this.m_oDockContainerAppearance.IsSetToDefaultValues();

    public void ResetDockContainerAppearance() => this.m_oDockContainerAppearance.SetToDefaultValues();

    [Description("Gets or sets the QContainerAppearance for the QDockContainer.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QDockContainerAppearance DockContainerAppearance => this.m_oDockContainerAppearance;

    [DefaultValue(QModifierKeys.None)]
    [Description("Gets or sets the modifier keys the user needs to press to allow the QDockingWindow to dock.")]
    [Category("QBehavior")]
    public QModifierKeys DockModifierKeys
    {
      get => this.m_eDockModifierKeys;
      set => this.m_eDockModifierKeys = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets a value indicating whether the QDockingWindow is displayed in the Windows taskbar when undocked.")]
    [DefaultValue(false)]
    public bool ShowInTaskbarUndocked
    {
      get => this.m_bShowInTaskbarUndocked;
      set => this.m_bShowInTaskbarUndocked = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets the border style of the QDockingWindow when undocked.")]
    [DefaultValue(FormBorderStyle.SizableToolWindow)]
    public FormBorderStyle FormBorderStyleUndocked
    {
      get => this.m_oFormBorderStyleUndocked;
      set
      {
        this.m_oFormBorderStyleUndocked = value;
        if (!(this.TopLevelControl is QDockForm))
          return;
        (this.TopLevelControl as QDockForm).SetTextToActiveControl();
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Contains a possible GroupName for the Window. Every window with the same GroupName will be docked in the same group on the QDockBar")]
    public string WindowGroupName
    {
      get => this.m_sWindowGroupName;
      set => this.m_sWindowGroupName = value;
    }

    [Category("QBehavior")]
    [DefaultValue(true)]
    [Description("Indicates if this control can slide")]
    public bool CanSlide
    {
      get => this.m_bCanSlide;
      set
      {
        if (this.m_bCanSlide == value)
          return;
        this.m_bCanSlide = value;
        this.RefreshNoClientArea();
      }
    }

    [Description("Contains the time it takes for the window to slide in or out (milliseconds)")]
    [DefaultValue(500)]
    [Category("QBehavior")]
    public int SlidingTime
    {
      get => this.m_iSlidingTime;
      set => this.m_iSlidingTime = value >= 1 && value <= 10000 ? value : throw new QDockingWindowException(QResources.GetException("QDockingWindow_SlidingTime_InvalidValue"));
    }

    [DefaultValue(true)]
    [Description("Indicates if the window should slide with motion or just collapse or expand")]
    [Category("QBehavior")]
    public bool SlideWithMotion
    {
      get => this.m_bSlideWithMotion;
      set => this.m_bSlideWithMotion = value;
    }

    [Browsable(false)]
    public override Color BorderColor
    {
      get => base.BorderColor;
      set => base.BorderColor = value;
    }

    public override Icon Icon
    {
      get => base.Icon;
      set
      {
        base.Icon = value;
        if (this.TopLevelControl is QDockForm)
          (this.TopLevelControl as QDockForm).SetTextToActiveControl();
        if (this.DockBar == null || !this.IsInSlideMode)
          return;
        this.DockBar.PerformLayout();
        this.DockBar.Invalidate();
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ParentSetToDockPoint(QDockPoint point)
    {
      if (point != null)
        this.OnWindowDock(new QDockEventArgs(point.DockBar, point.ControlToPlaceOnContainer));
      base.ParentSetToDockPoint(point);
    }

    [Browsable(false)]
    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void PutDockBar(QDockBar dockBar)
    {
      if (this.DockBar != null)
      {
        this.DockBar.RemoveWindow(this);
        this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oDockBarSizeChangedEventHandler);
        this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oDockBarVisibleChangedEventHandler);
      }
      base.PutDockBar(dockBar);
      if (this.DockBar == null)
        return;
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oDockBarSizeChangedEventHandler, (object) this.DockBar, "SizeChanged"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oDockBarVisibleChangedEventHandler, (object) this.DockBar, "VisibleChanged"));
    }

    internal bool ShowCaption
    {
      get
      {
        for (QDockContainer dockContainer = this.DockContainer; dockContainer != null && (dockContainer.VisibleChildControlsCount == 1 || dockContainer.IsTabbed); dockContainer = dockContainer.DockContainer)
        {
          if (dockContainer.Parent is QDockForm)
            return false;
        }
        return true;
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override bool NoControlCanClose() => !this.CanClose;

    [Browsable(false)]
    public bool IsSlideOut => this.IsInSlideMode && this.m_eWindowSlidedState == QDockingWindow.WindowSlidedState.SlidedOut;

    [Browsable(false)]
    public bool IsSlideIn => this.IsInSlideMode && this.m_eWindowSlidedState == QDockingWindow.WindowSlidedState.SlidedIn;

    [Browsable(false)]
    public bool IsSliding => this.IsInSlideMode && this.CurrentAction == QDockControl.WindowAction.Sliding;

    [Browsable(false)]
    public bool IsSlidingIn => this.IsSliding && this.m_eSlidingAction == QDockingWindow.WindowSlidingAction.SlidingIn;

    [Browsable(false)]
    public bool IsSlidingOut => this.IsSliding && this.m_eSlidingAction == QDockingWindow.WindowSlidingAction.SlidingOut;

    public bool ContainsScreenPoint(Point point) => this.RectangleToScreen(new Rectangle(-this.ClientAreaMarginLeft, -this.ClientAreaMarginTop, this.CurrentBounds.Width, this.CurrentBounds.Height)).Contains(point);

    public bool Close() => this.CloseControl(true);

    public override bool MustBePersistedAfter(IQPersistableObject persistableObject)
    {
      if (persistableObject is QDockControl qdockControl)
      {
        if (qdockControl.Parent == this.Parent)
        {
          int num1 = this.ControlIndex;
          int num2 = qdockControl.ControlIndex;
          if (this.DockContainer != null)
          {
            if (this.DockContainer.Orientation == QDockOrientation.Tabbed)
            {
              num1 = this.DockContainer.TabStrip.TabButtons.GetButtonIndexWithControl((Control) this);
              num2 = this.DockContainer.TabStrip.TabButtons.GetButtonIndexWithControl((Control) qdockControl);
            }
          }
          else if (this.IsInSlideMode && qdockControl.IsInSlideMode)
          {
            if (this.DockBar != qdockControl.DockBar)
              return false;
            QDockingWindow window = qdockControl as QDockingWindow;
            QDockingWindowItem windowItem1 = this.DockBar.DockingWindowItems.GetWindowItem(this);
            QDockingWindowItem windowItem2 = this.DockBar.DockingWindowItems.GetWindowItem(window);
            if (windowItem1.IsInGroup && windowItem1.Group == windowItem2.Group)
            {
              num1 = windowItem1.Group.Items.IndexOf((IQDockingWindowItem) windowItem1);
              num2 = windowItem2.Group.Items.IndexOf((IQDockingWindowItem) windowItem2);
            }
            else
            {
              num1 = this.DockBar.GetWindowGroupIndex(this);
              num2 = this.DockBar.GetWindowGroupIndex(window);
            }
          }
          return num1 - num2 > 0;
        }
        if (qdockControl.ThisOrChildControlContains((QDockControl) this))
          return true;
      }
      return false;
    }

    public override IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement)
    {
      IXPathNavigable parentNode = manager != null ? manager.CreatePersistableObjectElement((IQPersistableObject) this, parentElement) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      if (this.DockBar != null)
      {
        QXmlHelper.AddElement(parentNode, "dockBar", (object) this.DockBar.PersistGuid);
        QXmlHelper.AddElement(parentNode, "dockBarGroupIndex", (object) this.DockBar.GetWindowGroupIndex(this));
      }
      QXmlHelper.AddElement(parentNode, "dock", (object) this.Dock);
      QXmlHelper.AddElement(parentNode, "windowDockStyle", (object) this.WindowDockStyle);
      QXmlHelper.AddElement(parentNode, "dockPosition", (object) this.DockPosition);
      QXmlHelper.AddElement(parentNode, "controlIndex", (object) this.ControlIndex);
      QXmlHelper.AddElement(parentNode, "orientation", (object) (QDockOrientation) (this.DockContainer != null ? (int) this.DockContainer.Orientation : 0));
      QXmlHelper.AddElement(parentNode, "size", (object) this.Size);
      QXmlHelper.AddElement(parentNode, "dockedSize", (object) this.DockedSize);
      QXmlHelper.AddElement(parentNode, "visible", (object) this.Visible);
      QXmlHelper.AddElement(parentNode, "enabled", (object) this.Enabled);
      return parentNode;
    }

    public override bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      QDockPoint qdockPoint = new QDockPoint();
      qdockPoint.Parent = parentState as Control;
      int groupIndex = -1;
      if (!QMisc.IsEmpty((object) QXmlHelper.GetChildElementString(persistableObjectElement, "dockBar")))
      {
        Guid childElementGuid = QXmlHelper.GetChildElementGuid(persistableObjectElement, "dockBar");
        qdockPoint.DockBar = manager.GetPersistableHost(childElementGuid, (IQPersistableObject) this) as QDockBar;
        if (qdockPoint.DockBar == null)
          return false;
        groupIndex = QXmlHelper.GetChildElementInt(persistableObjectElement, "dockBarGroupIndex");
      }
      if (qdockPoint.Parent == null)
        qdockPoint.Parent = qdockPoint.DockBar == null ? manager.OwnerControl : qdockPoint.DockBar.Parent;
      object childElementEnum1 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "windowDockStyle", typeof (QDockControl.QWindowDockStyle));
      QDockControl.QWindowDockStyle qwindowDockStyle = childElementEnum1 != null ? (QDockControl.QWindowDockStyle) childElementEnum1 : QDockControl.QWindowDockStyle.None;
      object childElementEnum2 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "dockPosition", typeof (QDockPosition));
      qdockPoint.DockPosition = childElementEnum2 != null ? (QDockPosition) childElementEnum2 : QDockPosition.None;
      object childElementEnum3 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "orientation", typeof (QDockOrientation));
      qdockPoint.DockContainerOrientation = childElementEnum3 != null ? (QDockOrientation) childElementEnum3 : QDockOrientation.None;
      qdockPoint.InsertIndex = qdockPoint.DockContainer == null ? QXmlHelper.GetChildElementInt(persistableObjectElement, "controlIndex") : -1;
      object childElementEnum4 = (object) QXmlHelper.GetChildElementEnum(persistableObjectElement, "dock", typeof (DockStyle));
      this.Dock = childElementEnum4 != null ? (DockStyle) childElementEnum4 : DockStyle.None;
      this.Visible = QXmlHelper.GetChildElementBool(persistableObjectElement, "visible");
      this.Enabled = QXmlHelper.GetChildElementBool(persistableObjectElement, "enabled");
      this.PutWindowDockStyle(qwindowDockStyle);
      this.DockPosition = qdockPoint.DockPosition;
      this.PutDockBar(qdockPoint.DockBar);
      if (this.Owner == null)
        this.Owner = manager.OwnerForm;
      this.Size = QXmlHelper.GetChildElementSize(persistableObjectElement, "size");
      this.DockedSize = QXmlHelper.GetChildElementSize(persistableObjectElement, "dockedSize");
      switch (qwindowDockStyle)
      {
        case QDockControl.QWindowDockStyle.None:
        case QDockControl.QWindowDockStyle.Docked:
          if (qdockPoint.DockContainer != null)
            qdockPoint.DockContainer.SuspendLayout();
          this.SetParent(qdockPoint.Parent, qdockPoint.InsertIndex, this.Dock, this.Size, false);
          if (qdockPoint.DockContainer != null)
          {
            qdockPoint.DockContainer.ResumeLayout(false);
            break;
          }
          break;
        case QDockControl.QWindowDockStyle.SlideMode:
          this.DockBar.AddWindow(this, groupIndex, false);
          break;
      }
      return true;
    }

    public void DockWindow(QDockBar dockBar) => this.DockWindow(dockBar, false, false);

    public override int ControlIndex
    {
      get => this.DockContainer != null && this.DockContainer.IsTabbed && !this.DockContainer.IsLoadingPersistence ? this.TabButton.ButtonOrder : base.ControlIndex;
      set
      {
        if (this.DockContainer != null && this.DockContainer.IsTabbed)
          this.TabButton.ButtonOrder = value;
        else
          base.ControlIndex = value;
      }
    }

    public void DockWindow(QDockBar dockBar, bool slideMode, bool slideOut)
    {
      this.CheckConstraintsForDockOnDockBar(dockBar, slideMode);
      if (slideMode)
      {
        this.PutDockBar(dockBar);
        this.DockBar.AddWindow(this, false);
        if (!slideOut)
          return;
        this.SlideWindow(false, true);
      }
      else
        this.DockControl(new QDockPoint(Rectangle.Empty, QDockControl.FromDockStyle(dockBar.Dock), (Control) this.Owner, dockBar.ControlIndex, dockBar, QDockOrientation.None, (QDockControl) null));
    }

    public void DockWindow(
      QDockingWindow destinationWindow,
      QDockOrientation orientation,
      bool after)
    {
      if (destinationWindow == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (destinationWindow)));
      if (destinationWindow.IsInSlideMode)
      {
        this.CheckConstraintsForDockOnDockBar(destinationWindow.DockBar, true);
        this.CheckConstraintsForDockOnDockingWindow(orientation, after, true);
        this.PutDockBar(destinationWindow.DockBar);
        if (orientation == QDockOrientation.Tabbed)
          this.DockBar.AddWindowToGoup(destinationWindow, this, after, false);
        else
          this.DockBar.AddWindow(this, false);
      }
      else
      {
        this.CheckConstraintsForDockOnDockingWindow(orientation, after, false);
        int indexWithoutControl = destinationWindow.GetControlIndexWithoutControl((QDockControl) this, orientation == QDockOrientation.Tabbed);
        this.DockControl(new QDockPoint(Rectangle.Empty, destinationWindow.DockPosition, destinationWindow.Parent, after ? indexWithoutControl + 1 : indexWithoutControl, destinationWindow.DockBar, orientation, (QDockControl) destinationWindow));
      }
    }

    public void UndockWindow(int left, int top) => this.UndockControl(left, top);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void UndockControl(int x, int y)
    {
      this.PutWindowDockStyle(QDockControl.QWindowDockStyle.Docked);
      QDockContainer containerInstance = this.CreateDockContainerInstance((QDockControl) this, this.DockedSize, this.ControlIndex, this.DockPosition, (Control) null, QDockOrientation.None, false);
      containerInstance.SuspendLayout();
      this.SetParent((Control) containerInstance, 0, this.Dock, this.DockedSize, false);
      containerInstance.ResumeLayout(false);
      containerInstance.UndockControl(x, y);
    }

    private void CheckConstraintsForDockOnDockBar(QDockBar bar, bool slided)
    {
      if (slided && !this.CanSlide)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanSlideViolation"));
      if (bar.Dock == DockStyle.Top && !this.CanDockTop)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockTopViolation"));
      if (bar.Dock == DockStyle.Left && !this.CanDockLeft)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockLeftViolation"));
      if (bar.Dock == DockStyle.Right && !this.CanDockRight)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockRightViolation"));
      if (bar.Dock == DockStyle.Bottom && !this.CanDockBottom)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockBottomViolation"));
    }

    private void CheckConstraintsForDockOnDockingWindow(
      QDockOrientation orientation,
      bool after,
      bool slided)
    {
      if (slided && !this.CanSlide)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanSlideViolation"));
      if (orientation == QDockOrientation.Horizontal && !after && !this.CanDockOnOtherControlLeft)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockOnOtherControlLeftViolation"));
      if (orientation == QDockOrientation.Horizontal && after && !this.CanDockOnOtherControlRight)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockOnOtherControlRightViolation"));
      if (orientation == QDockOrientation.Vertical && !after && !this.CanDockOnOtherControlTop)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockOnOtherControlTopViolation"));
      if (orientation == QDockOrientation.Vertical && after && !this.CanDockOnOtherControlBottom)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockOnOtherControlBottomViolation"));
      if (orientation == QDockOrientation.Tabbed && !this.CanDockOnOtherControlTabbed)
        throw new QDockingWindowException(QResources.GetException("QDockingWindow_CanDockOnOtherControlTabbedViolation"));
    }

    internal void SetToSlidedState(bool useMotion)
    {
      Rectangle ofSlidedOutWindow = this.GetBoundsOfSlidedOutWindow();
      if (!useMotion)
      {
        ofSlidedOutWindow.Width = 0;
        ofSlidedOutWindow.Height = 0;
      }
      this.SetParent(this.DockBar.Parent, this.DockBar.ControlIndex, DockStyle.None, ofSlidedOutWindow, false);
      this.PutWindowDockStyle(QDockControl.QWindowDockStyle.SlideMode);
      this.DockPosition = QDockControl.FromDockStyle(this.DockBar.Dock);
      this.SetCanSizeProperties(this.DockPosition == QDockPosition.Right, this.DockPosition == QDockPosition.Bottom, this.DockPosition == QDockPosition.Left, this.DockPosition == QDockPosition.Top);
      this.m_eWindowSlidedState = QDockingWindow.WindowSlidedState.SlidedOut;
      this.SlideWindow(true, useMotion);
    }

    internal void SetToDockedState(QDockContainer container, QDockOrientation orientation)
    {
      if (!this.IsInSlideMode)
        return;
      if (!this.IsSlideOut)
        this.ClearRegion(true);
      QDockPoint point = container == null ? new QDockPoint(Rectangle.Empty, QDockControl.FromDockStyle(this.DockBar.Dock), this.DockBar.Parent, this.DockBar.ControlIndex, this.DockBar, orientation, (QDockControl) null) : new QDockPoint(Rectangle.Empty, QDockControl.FromDockStyle(this.DockBar.Dock), (Control) container, -1, this.DockBar, orientation, (QDockControl) null);
      this.m_eWindowSlidedState = QDockingWindow.WindowSlidedState.None;
      this.SetToDockPoint(point);
      this.DockBar.RemoveWindow(this);
    }

    public void SwapDockMode()
    {
      if (this.IsDockedOnDockBar)
      {
        if (this.IsOnDockContainer)
        {
          if (this.DockContainer.IsTabbed)
            this.DockContainer.AddWindowsToDockBar(this.DockBar, this);
          else
            this.DockBar.AddWindow(this);
        }
        else
          this.DockBar.AddWindow(this);
      }
      else
      {
        if (!this.IsInSlideMode)
          return;
        this.DockBar.SetWindowToDockedState(this);
      }
    }

    private void SlideWindow(bool slideIn, bool useMotion, bool raiseEvents)
    {
      bool useMotion1 = useMotion && this.SlideWithMotion;
      if (raiseEvents)
        this.OnWindowSlide(new QSlideEventArgs(slideIn, useMotion1));
      if (this.IsDockedOnDockBar)
        this.SwapDockMode();
      this.BringToFront();
      if (this.DockPosition == QDockPosition.Left || this.DockPosition == QDockPosition.Right)
        this.m_iSlidingStep = (int) Math.Ceiling((double) this.DockedSize.Width / ((double) this.SlidingTime / (double) this.m_iSlidingTimerInterval));
      else if (this.DockPosition == QDockPosition.Top || this.DockPosition == QDockPosition.Bottom)
        this.m_iSlidingStep = (int) Math.Ceiling((double) this.DockedSize.Height / ((double) this.SlidingTime / (double) this.m_iSlidingTimerInterval));
      if (!this.IsInSlideMode)
        return;
      if (slideIn)
      {
        if (useMotion1)
        {
          if (this.m_eWindowSlidedState == QDockingWindow.WindowSlidedState.SlidedIn)
            return;
          this.CurrentAction = QDockControl.WindowAction.Sliding;
          this.m_eSlidingAction = QDockingWindow.WindowSlidingAction.SlidingIn;
          this.m_iSlidingCurrentStep = 0;
          this.StartTimer();
        }
        else
        {
          this.CurrentAction = QDockControl.WindowAction.None;
          this.m_eWindowSlidedState = QDockingWindow.WindowSlidedState.SlidedIn;
          Region region = (Region) null;
          if (this.DockPosition == QDockPosition.Left || this.DockPosition == QDockPosition.Right)
            region = new Region(new Rectangle(0, 0, 0, this.Height));
          else if (this.DockPosition == QDockPosition.Top || this.DockPosition == QDockPosition.Bottom)
            region = new Region(new Rectangle(0, 0, this.Width, 0));
          this.SetRegion(region, true);
          this.OnWindowSlided(new QSlideEventArgs(true, false));
        }
      }
      else
      {
        if (useMotion1)
        {
          if (this.m_eWindowSlidedState == QDockingWindow.WindowSlidedState.SlidedOut)
            return;
          this.CurrentAction = QDockControl.WindowAction.Sliding;
          this.m_eSlidingAction = QDockingWindow.WindowSlidingAction.SlidingOut;
          this.m_iSlidingCurrentStep = 0;
          this.StartTimer();
        }
        else
        {
          this.SetBoundsToSlidedOutWindow();
          if (!this.Visible)
            this.Visible = true;
          this.ClearRegion(true);
          this.CurrentAction = QDockControl.WindowAction.None;
          this.m_eWindowSlidedState = QDockingWindow.WindowSlidedState.SlidedOut;
          this.PerformNonClientAreaLayout();
          this.PerformLayout();
          this.Invalidate();
          this.OnWindowSlided(new QSlideEventArgs(false, false));
        }
        this.BringToFront();
        this.Focus();
      }
      if (this.DockBar == null)
        return;
      this.DockBar.StartTimer();
    }

    public void SlideWindow(bool slideIn, bool useMotion) => this.SlideWindow(slideIn, useMotion, true);

    public void SwapWindowSlideState(bool useMotion)
    {
      if (this.IsDockedOnDockBar)
        this.SwapDockMode();
      this.SlideWindow(this.m_eWindowSlidedState == QDockingWindow.WindowSlidedState.SlidedOut, useMotion);
    }

    internal override bool DockModifierKeysPressed
    {
      get
      {
        if (this.DockModifierKeys == QModifierKeys.None)
          return true;
        Keys modifierKeys = Control.ModifierKeys;
        return ((this.DockModifierKeys & QModifierKeys.Control) != QModifierKeys.Control || (modifierKeys & Keys.Control) == Keys.Control) && ((this.DockModifierKeys & QModifierKeys.Alt) != QModifierKeys.Alt || (modifierKeys & Keys.Alt) == Keys.Alt) && ((this.DockModifierKeys & QModifierKeys.Shift) != QModifierKeys.Shift || (modifierKeys & Keys.Shift) == Keys.Shift);
      }
    }

    private int ParentsNonClientAreaMargin
    {
      get
      {
        if (!this.IsDockedOnDockBar)
          return 0;
        int clientAreaMargin = 0;
        for (QDockContainer dockContainer = this.DockContainer; dockContainer != null; dockContainer = dockContainer.DockContainer)
        {
          if (this.DockPosition == QDockPosition.Left || this.DockPosition == QDockPosition.Right)
            clientAreaMargin += dockContainer.NonClientAreaMarginWidth;
          else
            clientAreaMargin += dockContainer.NonClientAreaMarginHeight;
        }
        return clientAreaMargin;
      }
    }

    private Rectangle GetBoundsOfSlidedOutWindow()
    {
      if (this.IsDockedOnDockBar || this.IsInSlideMode)
      {
        switch (this.DockPosition)
        {
          case QDockPosition.Top:
            return new Rectangle(this.DockBar.Left, this.DockBar.Visible ? this.DockBar.Bottom : this.DockBar.Top, this.DockBar.Width, this.DockedSize.Height + this.ParentsNonClientAreaMargin);
          case QDockPosition.Left:
            return new Rectangle(this.DockBar.Visible ? this.DockBar.Right : this.DockBar.Left, this.DockBar.Top, this.DockedSize.Width + this.ParentsNonClientAreaMargin, this.DockBar.Height);
          case QDockPosition.Bottom:
            return new Rectangle(this.DockBar.Left, (this.DockBar.Visible ? this.DockBar.Top : this.DockBar.Bottom) - this.DockedSize.Height, this.DockBar.Width, this.DockedSize.Height + this.ParentsNonClientAreaMargin);
          case QDockPosition.Right:
            return new Rectangle((this.DockBar.Visible ? this.DockBar.Left : this.DockBar.Right) - this.DockedSize.Width, this.DockBar.Top, this.DockedSize.Width + this.ParentsNonClientAreaMargin, this.DockBar.Height);
        }
      }
      return this.Bounds;
    }

    internal void SetBoundsToSlidedOutWindow()
    {
      if (!this.IsDockedOnDockBar && !this.IsInSlideMode)
        return;
      Rectangle ofSlidedOutWindow = this.GetBoundsOfSlidedOutWindow();
      this.SetBounds(ofSlidedOutWindow.Left, ofSlidedOutWindow.Top, ofSlidedOutWindow.Width, ofSlidedOutWindow.Height);
    }

    private bool HasWindowsXPTheme
    {
      get
      {
        this.SecureWindowsXpTheme();
        return this.m_hWindowsXPTheme != IntPtr.Zero;
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ClearCachedMinimumClientSize()
    {
    }

    private void ClearCachedObjects()
    {
      this.m_oCachedCloseButtonArea = Rectangle.Empty;
      this.m_oCachedPinButtonArea = Rectangle.Empty;
      this.m_oCachedCaptionArea = Rectangle.Empty;
    }

    public override Rectangle CaptionBounds => this.CaptionArea;

    internal Rectangle CaptionArea
    {
      get
      {
        if (this.m_oCachedCaptionArea == Rectangle.Empty)
          this.m_oCachedCaptionArea = !this.ShowCaption ? Rectangle.Empty : new Rectangle(this.ClientAreaMarginLeft, this.TopBorderSize, this.CurrentBounds.Width - this.ClientAreaMarginLeft - this.ClientAreaMarginRight, SystemInformation.ToolWindowCaptionHeight);
        return this.m_oCachedCaptionArea;
      }
    }

    private Rectangle CloseButtonArea
    {
      get
      {
        if (this.m_oCachedCloseButtonArea == Rectangle.Empty)
        {
          if (this.ShowCaption)
          {
            Size captionButtonSize = NativeHelper.GetToolWindowCaptionButtonSize(this.HasWindowsXPTheme);
            int y = this.CaptionArea.Top + (int) Math.Ceiling((double) this.CaptionArea.Height / 2.0 - (double) captionButtonSize.Height / 2.0);
            int width = this.CanClose ? captionButtonSize.Width : 0;
            int height = captionButtonSize.Height;
            this.m_oCachedCloseButtonArea = new Rectangle(this.CaptionArea.Right - NativeHelper.ToolWindowCaptionButtonSpacing - width, y, width, height);
          }
          else
            this.m_oCachedCloseButtonArea = Rectangle.Empty;
        }
        return this.m_oCachedCloseButtonArea;
      }
    }

    private Rectangle PinButtonArea
    {
      get
      {
        if (this.m_oCachedPinButtonArea == Rectangle.Empty && this.CanSlide)
        {
          if (this.ShowCaption)
          {
            Size captionButtonSize = NativeHelper.GetToolWindowCaptionButtonSize(this.HasWindowsXPTheme);
            this.m_oCachedPinButtonArea = new Rectangle(this.CloseButtonArea.Left - NativeHelper.ToolWindowCaptionButtonSpacing - captionButtonSize.Width, this.CaptionArea.Top + (int) Math.Ceiling((double) this.CaptionArea.Height / 2.0 - (double) captionButtonSize.Height / 2.0), captionButtonSize.Width, captionButtonSize.Height);
          }
          else
            this.m_oCachedPinButtonArea = Rectangle.Empty;
        }
        return this.m_oCachedPinButtonArea;
      }
    }

    private int GetCaptionButtonStyle(QButtonState style)
    {
      switch (style)
      {
        case QButtonState.Inactive:
          return 4;
        case QButtonState.Normal:
          return !this.IsActivated ? 5 : 1;
        case QButtonState.Hot:
          return 2;
        case QButtonState.Pressed:
          return 3;
        default:
          return 0;
      }
    }

    private void HandleButtonMouseMove(int x, int y)
    {
      if (this.CloseButtonArea.Contains(x, y))
      {
        this.Cursor = Cursors.Default;
        if (this.m_eCloseButtonStyle == QButtonState.Normal)
        {
          this.m_eCloseButtonStyle = QButtonState.Hot;
          this.RefreshNoClientArea();
        }
      }
      else if (this.m_eCloseButtonStyle != QButtonState.Normal)
      {
        this.Cursor = Cursors.Default;
        this.m_eCloseButtonStyle = QButtonState.Normal;
        this.RefreshNoClientArea();
      }
      if (!this.CanSlide)
        return;
      if (this.PinButtonArea.Contains(x, y))
      {
        this.Cursor = Cursors.Default;
        if (this.m_ePinButtonStyle != QButtonState.Normal)
          return;
        this.m_ePinButtonStyle = QButtonState.Hot;
        this.RefreshNoClientArea();
      }
      else
      {
        if (this.m_ePinButtonStyle == QButtonState.Normal)
          return;
        this.Cursor = Cursors.Default;
        this.m_ePinButtonStyle = QButtonState.Normal;
        this.RefreshNoClientArea();
      }
    }

    private bool HandleButtonMouseDown(int x, int y)
    {
      if (this.CloseButtonArea.Contains(x, y))
      {
        this.Cursor = Cursors.Default;
        if (this.m_eCloseButtonStyle != QButtonState.Pressed)
        {
          this.m_eCloseButtonStyle = QButtonState.Pressed;
          this.RefreshNoClientArea();
        }
        return true;
      }
      if (!this.CanSlide || !this.PinButtonArea.Contains(x, y))
        return false;
      this.Cursor = Cursors.Default;
      if (this.m_ePinButtonStyle != QButtonState.Pressed)
      {
        this.m_ePinButtonStyle = QButtonState.Pressed;
        this.RefreshNoClientArea();
      }
      return true;
    }

    private void SecureWindowsXpTheme()
    {
      if (this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.Handle, "Window");
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

    private void DrawBorders(IntPtr hdc)
    {
      Rectangle windowBounds = NativeHelper.GetWindowBounds((Control) this);
      NativeMethods.RECT rect = NativeHelper.CreateRECT(0, 0, windowBounds.Width, windowBounds.Height);
      if (!this.IsInSlideMode || this.DockPosition == QDockPosition.Right)
        NativeMethods.DrawEdge(hdc, ref rect, 5, 1);
      if (!this.IsInSlideMode || this.DockPosition == QDockPosition.Left)
        NativeMethods.DrawEdge(hdc, ref rect, 5, 4);
      if (!this.IsInSlideMode || this.DockPosition == QDockPosition.Top)
        NativeMethods.DrawEdge(hdc, ref rect, 5, 8);
      if (this.IsInSlideMode && this.DockPosition != QDockPosition.Bottom)
        return;
      NativeMethods.DrawEdge(hdc, ref rect, 5, 2);
    }

    private void StartTimer()
    {
      if (this.m_bTimerRunning)
        return;
      this.m_bTimerRunning = true;
      this.StartTimer(17, this.m_iSlidingTimerInterval);
    }

    private void StopTimer()
    {
      if (!this.m_bTimerRunning)
        return;
      this.m_bTimerRunning = false;
      this.StopTimer(17);
    }

    protected virtual void OnLoad(EventArgs e) => this.m_oLoadDelegate = QWeakDelegate.InvokeDelegate(this.m_oLoadDelegate, (object) this, (object) e);

    protected virtual void OnWindowDock(QDockEventArgs e) => this.m_oWindowDockDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowDockDelegate, (object) this, (object) e);

    protected virtual void OnWindowSlide(QSlideEventArgs e) => this.m_oWindowSlideDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowSlideDelegate, (object) this, (object) e);

    protected virtual void OnWindowSlided(QSlideEventArgs e) => this.m_oWindowSlidedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowSlidedDelegate, (object) this, (object) e);

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.OnLoad(EventArgs.Empty);
    }

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (e.TimerId != 17)
        return;
      Rectangle empty = Rectangle.Empty;
      if (this.CurrentAction != QDockControl.WindowAction.Sliding)
        this.StopTimer();
      else if (this.m_eSlidingAction == QDockingWindow.WindowSlidingAction.SlidingIn)
      {
        bool flag = false;
        switch (this.DockPosition)
        {
          case QDockPosition.Top:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int height1 = Math.Max(0, this.Height - this.m_iSlidingCurrentStep);
            this.SetRegion(new Region(new Rectangle(0, this.m_iSlidingCurrentStep, this.DockBar.Width, height1)), false);
            Rectangle bounds1 = this.Bounds;
            int num1 = this.DockBar.Visible ? this.DockBar.Bottom : this.DockBar.Top;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, this.DockBar.Left, num1 - this.m_iSlidingCurrentStep, this.DockBar.Width, this.DockedSize.Height, 12U);
            this.Owner.Invalidate(bounds1, true);
            if (height1 == 0)
            {
              flag = true;
              break;
            }
            break;
          case QDockPosition.Left:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int width1 = Math.Max(0, this.DockedSize.Width - this.m_iSlidingCurrentStep);
            this.SetRegion(new Region(new Rectangle(this.m_iSlidingCurrentStep, 0, width1, this.DockBar.Height)), false);
            Rectangle bounds2 = this.Bounds;
            int num2 = this.DockBar.Visible ? this.DockBar.Right : this.DockBar.Left;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, num2 - this.m_iSlidingCurrentStep, this.DockBar.Top, this.DockedSize.Width, this.DockBar.Height, 12U);
            this.Owner.Invalidate(bounds2, true);
            if (width1 == 0)
            {
              flag = true;
              break;
            }
            break;
          case QDockPosition.Bottom:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int height2 = Math.Max(0, this.Height - this.m_iSlidingCurrentStep);
            this.SetRegion(new Region(new Rectangle(0, 0, this.Width, height2)), false);
            Rectangle bounds3 = this.Bounds;
            int num3 = (this.DockBar.Visible ? this.DockBar.Top : this.DockBar.Bottom) - this.DockedSize.Height;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, this.DockBar.Left, num3 + this.m_iSlidingCurrentStep, this.DockBar.Width, this.DockedSize.Height, 12U);
            this.Owner.Invalidate(bounds3, true);
            if (height2 == 0)
            {
              flag = true;
              break;
            }
            break;
          case QDockPosition.Right:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int width2 = Math.Max(0, this.Width - this.m_iSlidingCurrentStep);
            this.SetRegion(new Region(new Rectangle(0, 0, width2, this.DockBar.Height)), false);
            Rectangle bounds4 = this.Bounds;
            int num4 = (this.DockBar.Visible ? this.DockBar.Left : this.DockBar.Right) - this.DockedSize.Width;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, num4 + this.m_iSlidingCurrentStep, this.DockBar.Top, this.DockedSize.Width, this.DockBar.Height, 12U);
            this.Owner.Invalidate(bounds4, true);
            if (width2 == 0)
            {
              flag = true;
              break;
            }
            break;
        }
        if (!flag)
          return;
        this.StopTimer();
        this.CurrentAction = QDockControl.WindowAction.None;
        this.m_eWindowSlidedState = QDockingWindow.WindowSlidedState.SlidedIn;
        this.OnWindowSlided(new QSlideEventArgs(true, true));
      }
      else
      {
        if (this.m_eSlidingAction != QDockingWindow.WindowSlidingAction.SlidingOut)
          return;
        bool flag = false;
        switch (this.DockPosition)
        {
          case QDockPosition.Top:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int y = Math.Max(0, this.DockedSize.Height - this.m_iSlidingCurrentStep);
            int num5 = this.DockBar.Visible ? this.DockBar.Bottom : this.DockBar.Top;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, this.DockBar.Left, num5 - y, this.DockBar.Width, this.DockedSize.Height, 12U);
            this.SetRegion(new Region(new Rectangle(0, y, this.DockBar.Width, this.DockedSize.Height - y)), false);
            this.Owner.Invalidate(this.Bounds, true);
            if (!this.Visible)
              this.Visible = true;
            if (y == 0)
            {
              flag = true;
              break;
            }
            break;
          case QDockPosition.Left:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int x = Math.Max(0, this.DockedSize.Width - this.m_iSlidingCurrentStep);
            int num6 = this.DockBar.Visible ? this.DockBar.Right : this.DockBar.Left;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, num6 - x, this.DockBar.Top, this.DockedSize.Width, this.DockBar.Height, 12U);
            this.SetRegion(new Region(new Rectangle(x, 0, this.DockedSize.Width - x, this.Height)), false);
            this.Owner.Invalidate(this.Bounds, true);
            if (!this.Visible)
              this.Visible = true;
            if (x == 0)
            {
              flag = true;
              break;
            }
            break;
          case QDockPosition.Bottom:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int height = Math.Min(this.DockedSize.Height, this.m_iSlidingCurrentStep);
            int num7 = this.DockBar.Visible ? this.DockBar.Top : this.DockBar.Bottom;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, this.DockBar.Left, num7 - height, this.DockBar.Width, this.DockedSize.Height, 12U);
            this.SetRegion(new Region(new Rectangle(0, 0, this.Width, height)), false);
            this.Owner.Invalidate(this.Bounds, true);
            if (!this.Visible)
              this.Visible = true;
            if (height == this.DockedSize.Height)
            {
              flag = true;
              break;
            }
            break;
          case QDockPosition.Right:
            this.m_iSlidingCurrentStep += this.m_iSlidingStep;
            int width = Math.Min(this.DockedSize.Width, this.m_iSlidingCurrentStep);
            int iX = (this.DockBar.Visible ? this.DockBar.Left : this.DockBar.Right) - width;
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, iX, this.DockBar.Top, this.DockedSize.Width, this.DockBar.Height, 12U);
            this.SetRegion(new Region(new Rectangle(0, 0, width, this.Height)), false);
            this.Owner.Invalidate(this.Bounds, true);
            if (!this.Visible)
              this.Visible = true;
            if (width == this.DockedSize.Width)
            {
              flag = true;
              break;
            }
            break;
        }
        if (!flag)
          return;
        this.StopTimer();
        this.CurrentAction = QDockControl.WindowAction.None;
        this.m_eWindowSlidedState = QDockingWindow.WindowSlidedState.SlidedOut;
        this.ClearRegion(true);
        this.OnWindowSlided(new QSlideEventArgs(false, true));
      }
    }

    private int TopBorderSize
    {
      get
      {
        int topBorderSize = 0;
        if (this.IsDocked)
        {
          topBorderSize = 0;
          if (!this.IsOnDockContainer)
            topBorderSize += this.ResizeBorderWidth;
        }
        else if (this.IsInSlideMode)
        {
          topBorderSize = this.ResizeBorderWidth;
          if (this.DockPosition == QDockPosition.Bottom)
            topBorderSize += this.ResizeBorderWidth;
        }
        return topBorderSize;
      }
    }

    protected override int ClientAreaMarginTop
    {
      get
      {
        if (this.IsUndocked)
          return 0;
        return this.ShowCaption ? this.TopBorderSize + SystemInformation.ToolWindowCaptionHeight : this.TopBorderSize;
      }
    }

    protected override int ClientAreaMarginLeft
    {
      get
      {
        int clientAreaMarginLeft = 0;
        if (this.IsInSlideMode)
        {
          clientAreaMarginLeft = this.ResizeBorderWidth;
          if (this.DockPosition == QDockPosition.Right)
            clientAreaMarginLeft += this.ResizeBorderWidth;
        }
        return clientAreaMarginLeft;
      }
    }

    protected override int ClientAreaMarginRight
    {
      get
      {
        int clientAreaMarginRight = 0;
        if (this.IsDocked)
        {
          if (!this.IsOnDockContainerTabbed && this.IsOnDockContainerHorizontal && !this.IsLastControl)
            clientAreaMarginRight = this.ResizeBorderWidth;
        }
        else if (this.IsInSlideMode)
        {
          clientAreaMarginRight = this.ResizeBorderWidth;
          if (this.DockPosition == QDockPosition.Left)
            clientAreaMarginRight += this.ResizeBorderWidth;
        }
        return clientAreaMarginRight;
      }
    }

    protected override int ClientAreaMarginBottom
    {
      get
      {
        int areaMarginBottom = 0;
        if (this.IsDocked)
        {
          if (!this.IsOnDockContainerTabbed && this.IsOnDockContainerVertical && !this.IsLastControl)
            areaMarginBottom = this.ResizeBorderWidth;
        }
        else if (this.IsInSlideMode)
        {
          areaMarginBottom = this.ResizeBorderWidth;
          if (this.DockPosition == QDockPosition.Top)
            areaMarginBottom += this.ResizeBorderWidth;
        }
        return areaMarginBottom;
      }
    }

    protected override string BackColorPropertyName => "DockingWindowBackground1";

    protected override string BackColor2PropertyName => "DockingWindowBackground2";

    protected override string BorderColorPropertyName => (string) null;

    protected override void OnLayout(LayoutEventArgs levent)
    {
      this.ClearCachedObjects();
      if ((levent.AffectedControl == null || levent.AffectedControl == this) && levent.AffectedProperty == "Bounds" && !this.IsSliding && this.IsSlideOut)
      {
        Rectangle ofSlidedOutWindow = this.GetBoundsOfSlidedOutWindow();
        this.SetBounds(ofSlidedOutWindow.X, ofSlidedOutWindow.Y, ofSlidedOutWindow.Width, ofSlidedOutWindow.Height);
      }
      base.OnLayout(levent);
    }

    protected override void OnNonClientAreaMouseMove(QNonClientAreaMouseEventArgs e)
    {
      if (this.IsIdle)
        this.HandleButtonMouseMove(e.X, e.Y);
      base.OnNonClientAreaMouseMove(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Select();
      base.OnMouseDown(e);
    }

    protected override void OnNonClientAreaMouseDown(QNonClientAreaMouseEventArgs e)
    {
      Point controlMousePosition = new Point(e.X, e.Y);
      if (this.IsIdle && e.Button == MouseButtons.Left)
      {
        bool flag = this.DockContainer != null && (this.DockContainer.IsTabbed || this.DockContainer.Controls.Count > 1);
        if (this.HandleButtonMouseDown(controlMousePosition.X, controlMousePosition.Y) || !flag && !this.DockModifierKeysPressed || flag && !this.DockContainer.DockModifierKeysPressed)
          e.CancelDefaultAction = true;
        else if (this.CaptionArea.Contains(controlMousePosition.X, controlMousePosition.Y))
        {
          this.Select();
          if (this.DockContainer != null && (this.DockContainer.IsTabbed || this.DockContainer.Controls.Count == 1))
          {
            this.DockContainer.PrepareDragging(controlMousePosition);
            e.CancelDefaultAction = true;
          }
          else
          {
            this.PrepareDragging(controlMousePosition);
            e.CancelDefaultAction = true;
          }
        }
        else
          this.CurrentAction = QDockControl.WindowAction.None;
      }
      else if (this.CurrentAction == QDockControl.WindowAction.Sliding && e.Button == MouseButtons.Left)
        e.CancelDefaultAction = true;
      base.OnNonClientAreaMouseDown(e);
    }

    protected override void OnNonClientAreaMouseUp(QNonClientAreaMouseEventArgs e)
    {
      if (this.CurrentAction == QDockControl.WindowAction.Sliding && e.Button == MouseButtons.Left)
        e.CancelDefaultAction = true;
      else if (this.m_eCloseButtonStyle == QButtonState.Pressed)
        this.Close();
      else if (this.m_ePinButtonStyle == QButtonState.Pressed)
        this.SwapDockMode();
      base.OnNonClientAreaMouseUp(e);
    }

    protected override void OnNonClientAreaMouseLeave(EventArgs e)
    {
      base.OnNonClientAreaMouseLeave(e);
      this.HandleButtonMouseMove(-1, -1);
    }

    protected override void OnUserStartsSizing(EventArgs e)
    {
      this.CurrentAction = QDockControl.WindowAction.Sizing;
      base.OnUserStartsSizing(e);
    }

    protected override void OnUserEndsSizing(EventArgs e)
    {
      this.CurrentAction = QDockControl.WindowAction.None;
      base.OnUserEndsSizing(e);
    }

    protected override void OnPaintNonClientArea(PaintEventArgs e)
    {
      base.OnPaintNonClientArea(e);
      QRectanglePainter.Default.FillBackground(e.ClipRectangle, (IQAppearance) this.DockContainerAppearance, new QColorSet((Color) this.ColorScheme.DockContainerBackground1, (Color) this.ColorScheme.DockContainerBackground2), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
      this.SecureWindowsXpTheme();
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        IntPtr hdc = e.Graphics.GetHdc();
        if (this.IsInSlideMode)
          this.DrawBorders(hdc);
        if (this.ShowCaption)
        {
          int iStateId = this.IsActivated ? 1 : 2;
          NativeMethods.RECT rect = NativeHelper.CreateRECT(this.CaptionArea);
          NativeMethods.DrawThemeBackground(this.m_hWindowsXPTheme, hdc, 5, iStateId, ref rect, ref rect);
          if (this.CanClose)
          {
            rect = NativeHelper.CreateRECT(this.CloseButtonArea);
            NativeMethods.DrawThemeBackground(this.m_hWindowsXPTheme, hdc, 19, this.GetCaptionButtonStyle(this.m_eCloseButtonStyle), ref rect, ref rect);
          }
          e.Graphics.ReleaseHdc(hdc);
          if (this.CanSlide)
          {
            if (this.IsDockedOnDockBar)
              QControlPaint.DrawIcon(QDockingWindow.m_oPinnedIcon, this.PinButtonArea.Left + (this.PinButtonArea.Width - QDockingWindow.m_oPinnedIcon.Width) / 2, this.PinButtonArea.Top + (this.PinButtonArea.Height - QDockingWindow.m_oPinnedIcon.Height) / 2, e.Graphics);
            else if (this.IsInSlideMode)
              QControlPaint.DrawIcon(QDockingWindow.m_oUnpinnedIcon, this.PinButtonArea.Left + (this.PinButtonArea.Width - QDockingWindow.m_oPinnedIcon.Width) / 2, this.PinButtonArea.Top + (this.PinButtonArea.Height - QDockingWindow.m_oPinnedIcon.Height) / 2, e.Graphics);
          }
          RectangleF area = new RectangleF((PointF) this.CaptionArea.Location, (SizeF) this.CaptionArea.Size);
          area.Width -= (float) this.CloseButtonArea.Width;
          if (this.CanSlide && (this.IsDockedOnDockBar || this.IsInSlideMode))
            area.Width -= (float) (NativeHelper.ToolWindowCaptionButtonSpacing + this.PinButtonArea.Width);
          QControlPaint.DrawSmallCaptionText(this.m_hWindowsXPTheme, this.Text, this.IsActivated, area, e.Graphics);
        }
        else
          e.Graphics.ReleaseHdc(hdc);
      }
      else
      {
        if (this.ShowCaption)
          QControlPaint.DrawCaption(this.CaptionArea, e.Graphics, this.IsActivated);
        IntPtr hdc = e.Graphics.GetHdc();
        if (this.ShowCaption && this.CanClose)
        {
          NativeMethods.RECT rect = NativeHelper.CreateRECT(this.CloseButtonArea);
          int uState = this.m_eCloseButtonStyle == QButtonState.Pressed ? 512 : 0;
          NativeMethods.DrawFrameControl(hdc, ref rect, 1, uState);
        }
        if (this.IsInSlideMode)
          this.DrawBorders(hdc);
        e.Graphics.ReleaseHdc(hdc);
        if (!this.ShowCaption)
          return;
        RectangleF area = new RectangleF((PointF) this.CaptionArea.Location, (SizeF) this.CaptionArea.Size);
        area.Width -= (float) this.CloseButtonArea.Width;
        if (this.CanSlide && (this.IsDockedOnDockBar || this.IsInSlideMode))
          area.Width -= (float) (NativeHelper.ToolWindowCaptionButtonSpacing + this.PinButtonArea.Width);
        QControlPaint.DrawSmallCaptionText(IntPtr.Zero, this.Text, this.IsActivated, area, e.Graphics);
        if (!this.CanSlide)
          return;
        if (this.IsDockedOnDockBar)
        {
          QControlPaint.DrawIcon(QDockingWindow.m_oPinnedIcon, this.PinButtonArea.Left + (this.PinButtonArea.Width - QDockingWindow.m_oPinnedIcon.Width) / 2, this.PinButtonArea.Top + (this.PinButtonArea.Height - QDockingWindow.m_oPinnedIcon.Height) / 2, e.Graphics);
        }
        else
        {
          if (!this.IsInSlideMode)
            return;
          QControlPaint.DrawIcon(QDockingWindow.m_oUnpinnedIcon, this.PinButtonArea.Left + (this.PinButtonArea.Width - QDockingWindow.m_oPinnedIcon.Width) / 2, this.PinButtonArea.Top + (this.PinButtonArea.Height - QDockingWindow.m_oPinnedIcon.Height) / 2, e.Graphics);
        }
      }
    }

    private void Owner_SizeChanged(object sender, EventArgs e)
    {
      if (!this.IsSlideOut || !this.Visible)
        return;
      this.SlideWindow(false, false, false);
    }

    private void DockBar_VisibleChanged(object sender, EventArgs e)
    {
      if (!this.IsSlideOut || !this.Visible)
        return;
      this.SlideWindow(false, false, false);
    }

    private void DockBar_SizeChanged(object sender, EventArgs e)
    {
    }

    private void DockContainerAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.RefreshNoClientArea(true);
      this.OnAppearanceChanged(EventArgs.Empty);
    }

    protected override void OnEnter(EventArgs e)
    {
      base.OnEnter(e);
      this.RefreshNoClientArea();
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      this.RefreshNoClientArea();
    }

    protected override void OnColorsChanged(EventArgs e)
    {
      this.PerformNonClientAreaLayout();
      this.PerformLayout();
      this.Invalidate();
      base.OnColorsChanged(e);
    }

    protected override void OnWindowsXPThemeChanged(EventArgs e)
    {
      base.OnWindowsXPThemeChanged(e);
      this.CloseWindowsXpTheme();
    }

    protected override void Dispose(bool disposing)
    {
      if (!this.IsDisposed)
      {
        this.CloseWindowsXpTheme();
        int num = disposing ? 1 : 0;
      }
      base.Dispose(disposing);
    }

    private enum WindowSlidingAction
    {
      None,
      SlidingIn,
      SlidingOut,
    }

    private enum WindowSlidedState
    {
      None,
      SlidedIn,
      SlidedOut,
    }
  }
}
