// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuItemContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  public abstract class QMenuItemContainer : QCommandContainer, IQPersistableObject
  {
    internal const int TimerId = 17;
    private bool m_bHasSimulatedFocus;
    private Keys m_eOpenChildMenuArrowKey = Keys.Down;
    private Keys m_eOpenChildMenuArrowKey2 = Keys.Up;
    private Keys m_eCloseChildMenuArrowKey;
    private Keys m_eNextMenuItemArrowKey = Keys.Right;
    private Keys m_ePreviousMenuItemArrowKey = Keys.Left;
    private bool m_bHotkeyVisible;
    private QCommandConfiguration m_oConfigurationBase;
    private EventHandler m_oConfigurationChangedEventHander;
    private bool m_bTimerRunning;
    private Guid m_oPersistGuid = Guid.NewGuid();
    private bool m_bPersistObject = true;
    private bool m_bCreateNew;
    private bool m_bIsPersisted;
    private QMenuItem m_oDesignerMenuItem;
    private QFloatingMenuConfiguration m_oChildMenuConfigurationBase;
    private EventHandler m_oChildMenuConfigurationChangedEventHander;
    private QCommandContainerOrientation m_eOrientation = QCommandContainerOrientation.Horizontal;
    private QButtonState m_eDepersonalizeMenuItemState = QButtonState.Normal;
    private bool m_bAutoExpand;
    private QMenuItemContainerBehaviorFlags m_eBehaviorFlags;
    private int m_iTimerInterval = 50;
    private Point m_oLastMouseMovePoint = Point.Empty;
    private long m_lProposedExpandedItemTickStart;
    private long m_lExpandedItemMouseOffTickStart;
    private long m_lMouseHoverTickStart;
    private QMenuItem m_oProposedExpandedItem;
    private QMenuItem m_oHotItem;
    private QMenuItem m_oExpandedItem;
    private QMenuItem m_oMouseDownItem;
    private QMenuItem m_oMouseOverOuterBoundsItem;
    private QMenuItem m_oPreviousExpandedItem;
    private QMenuItem m_oLastMouseOverItem;
    private bool m_bPersonalized = true;
    private Rectangle m_oDepersonalizeMenuItemBounds = Rectangle.Empty;
    private IContainer m_oComponents;
    private bool m_bBubbleEventsToCustomParent = true;
    private QWeakDelegate m_oMenuItemMouseDownDelegate;
    private QWeakDelegate m_oMenuItemMouseUpDelegate;
    private QWeakDelegate m_oMenuItemSelectedDelegate;
    private QWeakDelegate m_oMenuItemActivatingDelegate;
    private QWeakDelegate m_oMenuItemActivatedDelegate;
    private QWeakDelegate m_oMenuShowingDelegate;
    private QWeakDelegate m_oMenuShowedDelegate;
    private QWeakDelegate m_oPaintMenuItemDelegate;
    private QWeakDelegate m_oMenuItemsRequestedDelegate;
    private QWeakDelegate m_oCustomizeMenuShowedDelegate;
    private QWeakDelegate m_oCustomizeMenuClosedDelegate;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    protected QMenuItemContainer() => this.InternalConstruct();

    protected QMenuItemContainer(IQCommandContainer customCommandContainer)
      : base(customCommandContainer)
    {
      this.InternalConstruct();
    }

    protected QMenuItemContainer(QCommand parentCommand, QMenuItemCollection menuItems)
      : base(parentCommand, (QCommandCollection) menuItems)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.SuspendLayout();
      this.m_oConfigurationChangedEventHander = new EventHandler(this.Configuration_Changed);
      this.m_oComponents = (IContainer) new Container();
      if (this.ParentCommandContainer != null)
        this.OwnerWindow = this.ParentCommandContainer.OwnerWindow;
      this.m_oChildMenuConfigurationChangedEventHander = new EventHandler(this.ChildMenuConfiguration_ConfigurationChanged);
      this.SetOrientation(QCommandContainerOrientation.Horizontal, false);
      this.ResumeLayout(false);
    }

    protected override QCommandCollection CreateCommandCollection() => (QCommandCollection) new QMenuItemCollection((IQCommandContainer) this, (QCommand) this.ParentMenuItem);

    [Category("QEvents")]
    [Description("Gets raised when the Configuration changed")]
    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event QMenuMouseEventHandler MenuItemMouseDown
    {
      add => this.m_oMenuItemMouseDownDelegate = QWeakDelegate.Combine(this.m_oMenuItemMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemMouseDownDelegate = QWeakDelegate.Remove(this.m_oMenuItemMouseDownDelegate, (Delegate) value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event QMenuMouseEventHandler MenuItemMouseUp
    {
      add => this.m_oMenuItemMouseUpDelegate = QWeakDelegate.Combine(this.m_oMenuItemMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemMouseUpDelegate = QWeakDelegate.Remove(this.m_oMenuItemMouseUpDelegate, (Delegate) value);
    }

    [Description("Gets raised when a user tries to expand a menuItem while the menuItem doens't have any child menuitems.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QMenuEventHandler MenuItemsRequested
    {
      add => this.m_oMenuItemsRequestedDelegate = QWeakDelegate.Combine(this.m_oMenuItemsRequestedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemsRequestedDelegate = QWeakDelegate.Remove(this.m_oMenuItemsRequestedDelegate, (Delegate) value);
    }

    [Description("Gets raised when a user selects a menuItem via the mouse or the Keyboard")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QMenuEventHandler MenuItemSelected
    {
      add => this.m_oMenuItemSelectedDelegate = QWeakDelegate.Combine(this.m_oMenuItemSelectedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemSelectedDelegate = QWeakDelegate.Remove(this.m_oMenuItemSelectedDelegate, (Delegate) value);
    }

    [Description("Gets raised when a user activates a MenuItem by clicking it, using a ShortCut, a HotKey or navigating to it with the keyboard. This event can be canceled.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QMenuCancelEventHandler MenuItemActivating
    {
      add => this.m_oMenuItemActivatingDelegate = QWeakDelegate.Combine(this.m_oMenuItemActivatingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemActivatingDelegate = QWeakDelegate.Remove(this.m_oMenuItemActivatingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when a user activates a menuItem by clicking it, using a ShortCut, a HotKey or navigating to it with the keyboard.")]
    [Category("QEvents")]
    public event QMenuEventHandler MenuItemActivated
    {
      add => this.m_oMenuItemActivatedDelegate = QWeakDelegate.Combine(this.m_oMenuItemActivatedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemActivatedDelegate = QWeakDelegate.Remove(this.m_oMenuItemActivatedDelegate, (Delegate) value);
    }

    [Description("Gets raised when a menuItem is activated and it has ChildMenuItems before the ChildMenu pops up. This event can be canceled.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QMenuCancelEventHandler MenuShowing
    {
      add => this.m_oMenuShowingDelegate = QWeakDelegate.Combine(this.m_oMenuShowingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuShowingDelegate = QWeakDelegate.Remove(this.m_oMenuShowingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when a menuItem is activated and it has ChildMenuItems when the ChildMenu is popped up.")]
    [Category("QEvents")]
    public event QMenuEventHandler MenuShowed
    {
      add => this.m_oMenuShowedDelegate = QWeakDelegate.Combine(this.m_oMenuShowedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuShowedDelegate = QWeakDelegate.Remove(this.m_oMenuShowedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when a MenuItem must be painted")]
    [QWeakEvent]
    public event QPaintMenuItemEventHandler PaintMenuItem
    {
      add => this.m_oPaintMenuItemDelegate = QWeakDelegate.Combine(this.m_oPaintMenuItemDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oPaintMenuItemDelegate = QWeakDelegate.Remove(this.m_oPaintMenuItemDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the customize menu is showed")]
    public event EventHandler CustomizeMenuShowed
    {
      add => this.m_oCustomizeMenuShowedDelegate = QWeakDelegate.Combine(this.m_oCustomizeMenuShowedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCustomizeMenuShowedDelegate = QWeakDelegate.Remove(this.m_oCustomizeMenuShowedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the customize menu is closed")]
    public event EventHandler CustomizeMenuClosed
    {
      add => this.m_oCustomizeMenuClosedDelegate = QWeakDelegate.Combine(this.m_oCustomizeMenuClosedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCustomizeMenuClosedDelegate = QWeakDelegate.Remove(this.m_oCustomizeMenuClosedDelegate, (Delegate) value);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string ToolTipText
    {
      get => base.ToolTipText;
      set => base.ToolTipText = value;
    }

    [Description("Gets or sets the PersistGuid. With this Guid the control is identified in the persistence files.")]
    [Category("QPersistence")]
    public Guid PersistGuid
    {
      get => this.m_oPersistGuid;
      set => this.m_oPersistGuid = value;
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether this object must be persisted.")]
    [Category("QPersistence")]
    public virtual bool PersistObject
    {
      get => this.m_bPersistObject;
      set => this.m_bPersistObject = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsPersisted
    {
      get => this.m_bIsPersisted;
      set => this.m_bIsPersisted = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool RequiresUnload => this is IQToolBar qtoolBar && (qtoolBar.IsFloating || qtoolBar.ToolBarHost != null);

    [Description("Gets or sets whether a new instance of this PersistableObject must be created when it is loaded from file. If this is false the persistableObject must match an existing persistableObject in the QPersistenceManager.PersistableObjects collection.")]
    [Category("QPersistence")]
    [DefaultValue(false)]
    public virtual bool CreateNew
    {
      get => this.m_bCreateNew;
      set => this.m_bCreateNew = value;
    }

    string IQPersistableObject.Name
    {
      get => base.Name;
      set => base.Name = value;
    }

    public new string Name
    {
      get => base.Name;
      set => base.Name = value;
    }

    [Browsable(false)]
    public QMenuItem ParentMenuItem => !(this.ParentCommand is QMenuItem) ? (QMenuItem) null : (QMenuItem) this.ParentCommand;

    internal QMenuItem DesignerMenuItem
    {
      get => this.m_oDesignerMenuItem;
      set
      {
        this.m_oDesignerMenuItem = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    internal Keys OpenChildMenuArrowKey
    {
      get => this.m_eOpenChildMenuArrowKey;
      set => this.m_eOpenChildMenuArrowKey = value;
    }

    [Browsable(false)]
    internal Keys OpenChildMenuArrowKey2
    {
      get => this.m_eOpenChildMenuArrowKey2;
      set => this.m_eOpenChildMenuArrowKey2 = value;
    }

    [Browsable(false)]
    internal Keys CloseChildMenuArrowKey
    {
      get => this.m_eCloseChildMenuArrowKey;
      set => this.m_eCloseChildMenuArrowKey = value;
    }

    [Browsable(false)]
    protected Keys NextMenuItemArrowKey
    {
      get => this.m_eNextMenuItemArrowKey;
      set => this.m_eNextMenuItemArrowKey = value;
    }

    [Browsable(false)]
    protected Keys PreviousMenuItemArrowKey
    {
      get => this.m_ePreviousMenuItemArrowKey;
      set => this.m_ePreviousMenuItemArrowKey = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal QCommandConfiguration ConfigurationBase
    {
      get => this.m_oConfigurationBase;
      set => this.SetConfigurationBase(value, true);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool HotkeyVisible
    {
      get
      {
        switch (this.ConfigurationBase.UsedHotkeyVisibilityType)
        {
          case QHotkeyVisibilityType.Always:
            return true;
          case QHotkeyVisibilityType.Never:
            return false;
          case QHotkeyVisibilityType.WhenAltIsPressed:
            return this.m_bHotkeyVisible;
          default:
            return false;
        }
      }
      set
      {
        if (this.m_bHotkeyVisible == value)
          return;
        bool hotkeyVisible = this.HotkeyVisible;
        this.m_bHotkeyVisible = value;
        if (this.HotkeyVisible == hotkeyVisible)
          return;
        this.PerformLayout((Control) null, (string) null);
        this.Refresh();
      }
    }

    internal void SetConfigurationBase(QCommandConfiguration configuration, bool raiseEvent)
    {
      if (this.m_oConfigurationBase == configuration)
        return;
      if (this.m_oConfigurationBase != null)
        this.m_oConfigurationBase.ConfigurationChanged -= this.m_oConfigurationChangedEventHander;
      this.m_oConfigurationBase = configuration;
      if (this.m_oConfigurationBase != null)
        this.m_oConfigurationBase.ConfigurationChanged += this.m_oConfigurationChangedEventHander;
      if (!raiseEvent)
        return;
      this.OnConfigurationChanged(EventArgs.Empty);
    }

    internal virtual QCommandContainerOrientation Orientation
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => this.m_eOrientation;
    }

    internal void SetOrientation(QCommandContainerOrientation orientation, bool refresh)
    {
      if (this.m_eOrientation == orientation)
        return;
      this.m_eOrientation = orientation;
      this.SetKeysToOrientation(orientation);
      if (!refresh)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
    }

    internal virtual void SetKeysToOrientation(QCommandContainerOrientation orientation)
    {
      switch (orientation)
      {
        case QCommandContainerOrientation.None:
        case QCommandContainerOrientation.Horizontal:
          this.m_eNextMenuItemArrowKey = Keys.Right;
          this.m_ePreviousMenuItemArrowKey = Keys.Left;
          this.m_eOpenChildMenuArrowKey = Keys.Down;
          this.m_eOpenChildMenuArrowKey2 = Keys.Up;
          this.m_eCloseChildMenuArrowKey = Keys.None;
          break;
        case QCommandContainerOrientation.Vertical:
          this.m_eNextMenuItemArrowKey = Keys.Down;
          this.m_ePreviousMenuItemArrowKey = Keys.Up;
          this.m_eOpenChildMenuArrowKey = Keys.Left;
          this.m_eOpenChildMenuArrowKey2 = Keys.Right;
          this.m_eCloseChildMenuArrowKey = Keys.Left;
          break;
      }
    }

    [Browsable(false)]
    public bool HasSimulatedFocus => this.m_bHasSimulatedFocus;

    internal void SimulateGotFocus()
    {
      if ((this.m_eBehaviorFlags & QMenuItemContainerBehaviorFlags.CanSimulateFocus) != QMenuItemContainerBehaviorFlags.CanSimulateFocus || this.HasSimulatedFocus)
        return;
      this.m_bHasSimulatedFocus = true;
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
      NativeMethods.HideCaret(IntPtr.Zero);
    }

    internal void SimulateLostFocus()
    {
      if ((this.m_eBehaviorFlags & QMenuItemContainerBehaviorFlags.CanSimulateFocus) != QMenuItemContainerBehaviorFlags.CanSimulateFocus || !this.m_bHasSimulatedFocus)
        return;
      this.m_bHasSimulatedFocus = false;
      this.HotkeyVisible = false;
      this.PutPersonalized(true);
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
      NativeMethods.ShowCaret(IntPtr.Zero);
    }

    internal virtual bool HandlePossibleHotKey(Keys keys)
    {
      int itemsWithHotkeyCount = this.GetAccessibleMenuItemsWithHotkeyCount(keys);
      if (itemsWithHotkeyCount <= 0)
        return false;
      QMenuItem menuItemWithHotkey = this.GetNextAccessibleMenuItemWithHotkey(this.HotItem, keys);
      if (itemsWithHotkeyCount > 1)
        this.SetHotItem(menuItemWithHotkey, QMenuItemActivationType.Keyboard);
      else if (menuItemWithHotkey.HasChildItems)
      {
        this.ExpandItem(menuItemWithHotkey, false, true, QMenuItemActivationType.Hotkey);
      }
      else
      {
        this.SetHotItem(menuItemWithHotkey, QMenuItemActivationType.Keyboard);
        this.ActivateItem(this.HotItem, false, true, this.HotkeyVisible, QMenuItemActivationType.Hotkey);
      }
      return true;
    }

    internal bool HandleDefaultNavigationKeys(Keys keys, bool useAutoExpand)
    {
      switch (keys)
      {
        case Keys.Return:
          if (this.HotItem != null && !this.HotItem.HasChildItems)
            this.RaiseMenuItemsRequested(new QMenuEventArgs(this.HotItem, QMenuItemActivationType.Mouse, true));
          if (this.HotItem != null && this.HotItem.HasChildItems)
            this.ExpandItem(this.HotItem, false, true, QMenuItemActivationType.Keyboard);
          else
            this.ActivateItem(this.HotItem, true, false, true, QMenuItemActivationType.Keyboard);
          return true;
        case Keys.Escape:
          if (this.ExpandedItem != null)
          {
            this.SetHotItem(this.ExpandedItem, QMenuItemActivationType.Keyboard);
            this.ResetExpandedItem();
            this.ProposedExpandedItem = (QMenuItem) null;
            this.AutoExpand = false;
            return true;
          }
          this.ResetState();
          return false;
        default:
          if (keys == this.CloseChildMenuArrowKey && this.ExpandedItem != null)
          {
            this.SetHotItem(this.ExpandedItem, QMenuItemActivationType.Keyboard);
            this.ResetExpandedItem();
            this.ProposedExpandedItem = (QMenuItem) null;
            this.AutoExpand = false;
            return true;
          }
          if (keys == this.OpenChildMenuArrowKey || keys == this.OpenChildMenuArrowKey2)
          {
            if (this.HotItem != null && !this.HotItem.HasChildItems)
              this.RaiseMenuItemsRequested(new QMenuEventArgs(this.HotItem, QMenuItemActivationType.Mouse, true));
            return this.ExpandItem(this.HotItem, false, true, QMenuItemActivationType.Keyboard);
          }
          if (keys == this.PreviousMenuItemArrowKey)
          {
            this.SelectPreviousItem();
            if (this.AutoExpand && useAutoExpand)
            {
              if (this.HotItem != null && !this.HotItem.HasChildItems)
                this.RaiseMenuItemsRequested(new QMenuEventArgs(this.HotItem, QMenuItemActivationType.Mouse, true));
              this.ExpandItem(this.HotItem, false, true, QMenuItemActivationType.Keyboard);
            }
            return true;
          }
          if (keys != this.NextMenuItemArrowKey)
            return false;
          this.SelectNextItem();
          if (this.AutoExpand && useAutoExpand)
          {
            if (this.HotItem != null && !this.HotItem.HasChildItems)
              this.RaiseMenuItemsRequested(new QMenuEventArgs(this.HotItem, QMenuItemActivationType.Mouse, true));
            this.ExpandItem(this.HotItem, false, true, QMenuItemActivationType.Keyboard);
          }
          return true;
      }
    }

    [Browsable(false)]
    internal QMenuItemContainer RootMenuItemContainer => this.ParentMenuItemContainer != null ? this.ParentMenuItemContainer.RootMenuItemContainer : this;

    [Browsable(false)]
    internal QMenuItemContainer ParentMenuItemContainer => this.ParentCommandContainer != null && this.ParentCommandContainer is QMenuItemContainer ? (QMenuItemContainer) this.ParentCommandContainer : (QMenuItemContainer) null;

    internal virtual Color ParentMenuIntersectColor
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => (Color) this.ColorScheme.MenuHotItemBackground2;
    }

    public virtual bool MustBePersistedAfter(IQPersistableObject persistableObject) => false;

    public virtual IXPathNavigable SavePersistableObject(
      QPersistenceManager manager,
      IXPathNavigable parentElement)
    {
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      IQToolBar qtoolBar = this as IQToolBar;
      IXPathNavigable parentNode = (IXPathNavigable) null;
      if (qtoolBar != null || this.CreateNew || this.Items.HasPersonalizedItems(true))
        parentNode = manager.CreatePersistableObjectElement((IQPersistableObject) this, parentElement);
      if (qtoolBar != null)
      {
        QXmlHelper.AddElement(parentNode, "isFloating", (object) qtoolBar.IsFloating);
        if (qtoolBar.IsFloating)
        {
          QXmlHelper.AddElement(parentNode, "floatingBounds", (object) qtoolBar.ToolBarForm.Bounds);
          QXmlHelper.AddElement(parentNode, "userRequestedToolBarWidth", (object) qtoolBar.ToolBarForm.UserRequestedToolBarWidth);
        }
        if (qtoolBar.ToolBarHost != null)
        {
          QXmlHelper.AddElement(parentNode, "toolBarHost", (object) qtoolBar.ToolBarHost.PersistGuid);
          QXmlHelper.AddElement(parentNode, "rowIndex", (object) qtoolBar.RowIndex);
          QXmlHelper.AddElement(parentNode, "toolBarIndex", (object) qtoolBar.ToolBarPositionIndex);
          QXmlHelper.AddElement(parentNode, "requestedPosition", (object) qtoolBar.UserRequestedPosition);
        }
        QXmlHelper.AddElement(parentNode, "visible", (object) this.Visible);
      }
      if (parentNode != null)
      {
        QMenuItemSaveType saveOptions = this.CreateNew ? QMenuItemSaveType.CompleteMenuItem : QMenuItemSaveType.PersonalizedStateOnly;
        this.Items.SaveToXml(parentNode, saveOptions);
      }
      return parentNode;
    }

    public virtual bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState)
    {
      IQToolBar qtoolBar = this as IQToolBar;
      if (manager == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (manager)));
      if (qtoolBar != null)
      {
        QToolBarHost host = (QToolBarHost) null;
        bool childElementBool = QXmlHelper.GetChildElementBool(persistableObjectElement, "isFloating");
        int childElementInt1 = QXmlHelper.GetChildElementInt(persistableObjectElement, "rowIndex");
        int childElementInt2 = QXmlHelper.GetChildElementInt(persistableObjectElement, "toolBarIndex");
        int childElementInt3 = QXmlHelper.GetChildElementInt(persistableObjectElement, "requestedPosition");
        if (!childElementBool && !QMisc.IsEmpty((object) QXmlHelper.GetChildElementString(persistableObjectElement, "toolBarHost")))
          host = manager.GetPersistableHost(QXmlHelper.GetChildElementGuid(persistableObjectElement, "toolBarHost"), (IQPersistableObject) this) as QToolBarHost;
        if (host != null)
          qtoolBar.DockToolBar(host, childElementInt1, childElementInt2, childElementInt3);
        else if (childElementBool)
        {
          Rectangle elementRectangle = QXmlHelper.GetChildElementRectangle(persistableObjectElement, "floatingBounds");
          qtoolBar.FloatToolBar(elementRectangle.Location);
          qtoolBar.ToolBarForm.Owner = manager.OwnerForm;
          qtoolBar.ToolBarForm.UserRequestedToolBarWidth = QXmlHelper.GetChildElementInt(persistableObjectElement, "userRequestedToolBarWidth");
        }
        this.Visible = QXmlHelper.GetChildElementBool(persistableObjectElement, "visible");
      }
      if (QXmlHelper.ContainsChildElement(persistableObjectElement, "menuItemCollection"))
      {
        QMenuItemLoadType loadOptions = this.CreateNew ? QMenuItemLoadType.CreateNewItems : QMenuItemLoadType.MatchByName;
        this.Items.LoadFromXml(QXmlHelper.SelectChildNavigable(persistableObjectElement, "menuItemCollection"), loadOptions);
      }
      return true;
    }

    public virtual void UnloadPersistableObject()
    {
      this.ResetState();
      this.Parent = (Control) null;
    }

    protected virtual QMenuItem CloneMenuItemForCustomizeMenu(QMenuItem oMenuItem)
    {
      QMenuItem qmenuItem = (QMenuItem) oMenuItem.Clone(false);
      if (oMenuItem.VisibleWhenPersonalized)
        qmenuItem.Checked = true;
      qmenuItem.Control = (QCommandControlContainer) null;
      qmenuItem.PutControlBounds(Rectangle.Empty);
      qmenuItem.CheckedIcon = (Icon) null;
      qmenuItem.VisibleWhenPersonalized = true;
      qmenuItem.Visible = true;
      qmenuItem.Enabled = true;
      qmenuItem.UserHasRightToExecute = true;
      qmenuItem.CloseMenuOnActivate = false;
      qmenuItem.ItemName = oMenuItem.ItemName + "_Customize";
      qmenuItem.SetAdditionalProperty(3, (object) QCommandCopyType.CustomizeOriginal);
      return qmenuItem;
    }

    public virtual QMenuItemCollection RetrieveCustomizeMenu()
    {
      QMenuItemCollection qmenuItemCollection = new QMenuItemCollection();
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index].UserHasRightToExecute || this.Items[index].UserRightBehavior == QCommandUserRightBehavior.DisableWhenNoRight)
        {
          QMenuItem menuItem = this.CloneMenuItemForCustomizeMenu(this.Items[index]);
          if (menuItem != null)
            qmenuItemCollection.Add(menuItem);
        }
      }
      return qmenuItemCollection;
    }

    public QMenuItem GetAccessibleMenuItemWithHotkey(Keys hotkey) => this.Items.GetNextMenuItemWithHotkey((QMenuItem) null, hotkey, false, false);

    internal QMenuItem GetNextAccessibleMenuItemWithHotkey(
      QMenuItem afterItem,
      Keys hotkey,
      int recursiveLevels)
    {
      return this.Items.GetNextMenuItemWithHotkey(afterItem, hotkey, true, true, recursiveLevels);
    }

    public QMenuItem GetNextAccessibleMenuItemWithHotkey(QMenuItem afterItem, Keys hotkey) => this.GetNextAccessibleMenuItemWithHotkey(afterItem, hotkey, 0);

    internal int GetAccessibleMenuItemsWithHotkeyCount(Keys hotkey, int recursiveLevels) => this.Items.GetMenuItemsWithHotkeyCount(hotkey, true, recursiveLevels);

    public int GetAccessibleMenuItemsWithHotkeyCount(Keys hotkey) => this.GetAccessibleMenuItemsWithHotkeyCount(hotkey, 0);

    public QMenuItem GetAccessibleMenuItemWithHotkey(Keys hotkey, int recursiveLevels)
    {
      QMenuItem menuItemWithHotkey = this.Items.GetMenuItemWithHotkey(hotkey, recursiveLevels);
      return menuItemWithHotkey != null && menuItemWithHotkey.IsAccessible ? menuItemWithHotkey : (QMenuItem) null;
    }

    public QMenuItem GetAccessibleMenuItemWithShortcut(Keys shortcut)
    {
      QMenuItem itemWithShortcut = this.Items.GetMenuItemWithShortcut(shortcut);
      return itemWithShortcut != null && itemWithShortcut.IsAccessible ? itemWithShortcut : (QMenuItem) null;
    }

    internal bool ShouldHandleShortcutsForControl(Control control, QShortcutScope scope) => QKeyboardFilterHelper.ShouldHandleShortcutsForControl((Control) this, control, scope);

    internal bool ShouldHandleKeyMessagesForControl(Control control) => QKeyboardFilterHelper.ShouldHandleKeyMessagesForControl((Control) this, control);

    internal bool IsLastVisibleItem(QMenuItem item) => this.IsLastVisibleItem(this.Items.IndexOf(item));

    internal bool IsLastVisibleItem(int index)
    {
      if (index < 0)
        return false;
      index = Math.Min(int.MaxValue, index + 1);
      for (int index1 = index; index1 < this.Items.Count; ++index1)
      {
        if (this.Items[index1].IsVisible)
          return false;
      }
      return true;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void MakeSureItemIsShown(QMenuItem item, bool refresh)
    {
    }

    internal int GetNextSelectableItemIndex(int index, bool loopAround)
    {
      if (index < -1)
        index = -1;
      bool flag = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.CanIterateThroughHiddenItems) == QMenuItemContainerBehaviorFlags.CanIterateThroughHiddenItems;
      int num1 = flag ? this.Items.Count : this.LastShownCommand + 1;
      int num2 = flag ? 0 : this.FirstShownCommand;
      if (index >= num1 - 1)
      {
        if (!loopAround)
          return -1;
        index = num2 - 1;
      }
      index = Math.Min(int.MaxValue, index + 1);
      for (int index1 = index; index1 < num1; ++index1)
      {
        if (this.Items[index1].IsVisible && !this.Items[index1].IsInformationOnly)
          return index1;
      }
      return loopAround ? this.GetNextSelectableItemIndex(-1, false) : -1;
    }

    protected QMenuItem GetNextSelectableItem(int index, bool loopAround)
    {
      int selectableItemIndex = this.GetNextSelectableItemIndex(index, loopAround);
      return selectableItemIndex >= 0 ? this.Items[selectableItemIndex] : (QMenuItem) null;
    }

    internal int GetPreviousSelectableItemIndex(int index, bool loopAround)
    {
      bool flag = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.CanIterateThroughHiddenItems) == QMenuItemContainerBehaviorFlags.CanIterateThroughHiddenItems;
      int index1 = flag ? this.Items.Count : this.LastShownCommand + 1;
      int num = flag ? 0 : this.FirstShownCommand;
      if (index > index1)
        index = index1;
      if (index <= num)
      {
        if (!loopAround)
          return -1;
        index = index1;
      }
      index = Math.Max(int.MinValue, index - 1);
      for (int index2 = index; index2 >= num; --index2)
      {
        if (this.Items[index2].IsVisible && !this.Items[index2].IsInformationOnly)
          return index2;
      }
      return loopAround ? this.GetPreviousSelectableItemIndex(index1, false) : -1;
    }

    protected QMenuItem GetPreviousSelectableItem(int index, bool loopAround)
    {
      int selectableItemIndex = this.GetPreviousSelectableItemIndex(index, loopAround);
      return selectableItemIndex >= 0 ? this.Items[selectableItemIndex] : (QMenuItem) null;
    }

    public void SelectNextItem() => this.SelectNextItem(true);

    public void SelectPreviousItem() => this.SelectNextItem(false);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void SelectNextItem(bool directionDown)
    {
      int index = this.HotItem == null ? (this.ExpandedItem == null ? (directionDown ? -1 : this.Items.Count) : this.Items.IndexOf(this.ExpandedItem)) : this.Items.IndexOf(this.HotItem);
      if (directionDown)
      {
        if (this.IsLastVisibleItem(index) && this.Items.HasPersonalizedItems(false) && this.Personalized)
        {
          this.DepersonalizeMenuItemContainer();
          index = -1;
        }
        QMenuItem nextSelectableItem = this.GetNextSelectableItem(index, true);
        this.MakeSureItemIsShown(nextSelectableItem, false);
        this.ResetExpandedItem();
        this.SetHotItem(nextSelectableItem, QMenuItemActivationType.Keyboard);
      }
      else
      {
        QMenuItem previousSelectableItem = this.GetPreviousSelectableItem(index, true);
        this.MakeSureItemIsShown(previousSelectableItem, false);
        this.ResetExpandedItem();
        this.SetHotItem(previousSelectableItem, QMenuItemActivationType.Keyboard);
      }
    }

    private bool ContainsOrIsContainerWithHandle(Control control, IntPtr handle)
    {
      if (control == null || !control.IsHandleCreated)
        return false;
      if (control.Handle == handle)
        return true;
      for (int index = 0; index < control.Controls.Count; ++index)
      {
        if (this.ContainsOrIsContainerWithHandle(control.Controls[index], handle))
          return true;
      }
      return false;
    }

    public override bool ContainsOrIsContainerWithHandle(IntPtr handle)
    {
      if (handle == IntPtr.Zero || !this.IsHandleCreated)
        return false;
      if (this.Handle == handle)
        return true;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        QMenuItem qmenuItem = this.Items[index];
        if (qmenuItem.Control != null && this.ContainsOrIsContainerWithHandle((Control) qmenuItem.Control, handle) || qmenuItem.ChildContainerCreated && qmenuItem.ChildContainer.ContainsOrIsContainerWithHandle(handle))
          return true;
      }
      return false;
    }

    public void EnableAllItems(bool enable)
    {
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index].Enabled != enable)
          this.Items[index].Enabled = enable;
      }
    }

    protected virtual void StartTimer()
    {
      if (this.m_bTimerRunning)
        return;
      this.m_bTimerRunning = true;
      this.StartTimer(17, this.m_iTimerInterval);
    }

    protected virtual void StopTimer()
    {
      if (!this.m_bTimerRunning)
        return;
      this.m_bTimerRunning = false;
      this.StopTimer(17);
    }

    internal QMenuItemCollection Items => (QMenuItemCollection) this.Commands;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal QMenuItem HotItem => this.m_oHotItem;

    internal virtual void SetHotItem(QMenuItem item, QMenuItemActivationType activationType)
    {
      if (this.m_oHotItem == item)
        return;
      this.m_oHotItem = item;
      if (this.m_oHotItem != null)
      {
        if (!this.m_oHotItem.VisibleWhenPersonalized && this.Personalized)
          this.DepersonalizeMenuItemContainer();
        this.MakeSureItemIsShown(this.m_oHotItem, true);
        this.ToolTipText = this.m_oHotItem.UsedToolTip;
      }
      else
        this.ToolTipText = string.Empty;
      if (this.m_oHotItem != null)
        this.StartTimer();
      this.Refresh();
      this.RaiseMenuItemSelected(new QMenuEventArgs(item, activationType, false));
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal QMenuItem MouseDownItem
    {
      get => this.m_oMouseDownItem;
      set
      {
        if (this.m_oMouseDownItem == value)
          return;
        this.m_oMouseDownItem = value;
        this.Refresh();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal QMenuItem LastMouseOverItem => this.m_oLastMouseOverItem;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal virtual QMenuItem ExpandedItem
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => this.m_oExpandedItem;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal QFloatingMenuConfiguration ChildMenuConfigurationBase
    {
      get => this.m_oChildMenuConfigurationBase;
      set => this.SetChildMenuConfigurationBase(value, true);
    }

    internal void SetChildMenuConfigurationBase(
      QFloatingMenuConfiguration configuration,
      bool raiseEvent)
    {
      if (this.m_oChildMenuConfigurationBase != null)
        this.m_oChildMenuConfigurationBase.ConfigurationChanged -= this.m_oChildMenuConfigurationChangedEventHander;
      this.m_oChildMenuConfigurationBase = configuration;
      if (this.m_oChildMenuConfigurationBase != null)
        this.m_oChildMenuConfigurationBase.ConfigurationChanged += this.m_oChildMenuConfigurationChangedEventHander;
      if (!raiseEvent)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Invalidate();
    }

    internal bool MouseOverMenuItemContainer
    {
      get
      {
        if (this.IsDisposed || !this.Visible)
          return false;
        IntPtr handle = NativeMethods.WindowFromPoint(new NativeMethods.POINT(Control.MousePosition.X, Control.MousePosition.Y));
        if (handle != IntPtr.Zero)
        {
          Control ctl = Control.FromHandle(handle);
          if (ctl == this || this.Contains(ctl))
            return true;
        }
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal QMenuItem ProposedExpandedItem
    {
      get => this.m_oProposedExpandedItem;
      set
      {
        if (value != null)
        {
          if (this.ConfigurationBase.UsedExpandingDelay > 0)
          {
            this.m_oProposedExpandedItem = value;
            this.m_lProposedExpandedItemTickStart = QMisc.TickCount;
            this.Refresh();
          }
          else
            this.SetExpandedItem(value, true, false, QMenuItemActivationType.Mouse);
          this.StartTimer();
        }
        else
          this.m_oProposedExpandedItem = (QMenuItem) null;
      }
    }

    [Browsable(false)]
    internal QMenuItem PreviousExpandedItem
    {
      get => this.m_oPreviousExpandedItem;
      set => this.m_oPreviousExpandedItem = value;
    }

    [Browsable(false)]
    public virtual bool Personalized => this.m_bPersonalized;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void PutPersonalized(bool value) => this.m_bPersonalized = value;

    public bool ShowPersonalizedItems => this.ConfigurationBase.PersonalizedItemBehavior == QPersonalizedItemBehavior.DependsOnPersonalized ? !this.Personalized : this.ConfigurationBase.PersonalizedItemBehavior == QPersonalizedItemBehavior.AlwaysVisible;

    [Browsable(false)]
    protected virtual bool AutoExpand
    {
      get => (this.m_eBehaviorFlags & QMenuItemContainerBehaviorFlags.AlwaysAutoExpand) == QMenuItemContainerBehaviorFlags.AlwaysAutoExpand || this.m_bAutoExpand;
      set => this.m_bAutoExpand = value;
    }

    internal bool ItemIsInVisibleCommands(QMenuItem menuItem)
    {
      if (menuItem == null)
        return false;
      int num = this.Items.IndexOf(menuItem);
      return num >= this.FirstShownCommand && num <= this.LastShownCommand;
    }

    internal bool BubbleEventsToCustomParent
    {
      get => this.m_bBubbleEventsToCustomParent;
      set => this.m_bBubbleEventsToCustomParent = value;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual Rectangle ContainerRectangleToScreen(Rectangle rectangle) => this.RectangleToScreen(rectangle);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual QMenuItem GetItemAtPosition(int x, int y)
    {
      QMenuItem menuItemAtPosition = this.Items.GetMenuItemAtPosition(new Point(x, y));
      return menuItemAtPosition != null && this.ItemIsInVisibleCommands(menuItemAtPosition) ? menuItemAtPosition : (QMenuItem) null;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual QMenuItem GetItemWithOuterBoundsOn(int x, int y)
    {
      QMenuItem withOuterBoundsOn = this.Items.GetMenuItemWithOuterBoundsOn(new Point(x, y));
      return withOuterBoundsOn != null && this.ItemIsInVisibleCommands(withOuterBoundsOn) ? withOuterBoundsOn : (QMenuItem) null;
    }

    internal void SetBehaviorFlags(QMenuItemContainerBehaviorFlags flags, bool value)
    {
      if (value)
        this.m_eBehaviorFlags |= flags;
      else
        this.m_eBehaviorFlags &= ~flags;
    }

    internal QMenuItem MouseOverOuterBoundsItem => this.m_oMouseOverOuterBoundsItem;

    internal void SetMouseOverOuterBoundsItem(QMenuItem menuItem, bool refresh)
    {
      this.m_oMouseOverOuterBoundsItem = menuItem;
      if (!refresh)
        return;
      this.Refresh();
    }

    internal QMenuItemContainerBehaviorFlags BehaviorFlags => this.m_eBehaviorFlags;

    [Browsable(false)]
    internal bool ItemOrDepersonalizeItemHot => this.HotItem != null || this.DepersonalizeMenuItemState == QButtonState.Hot;

    public virtual void CloseAllMenus()
    {
      if (this.ParentMenuItemContainer != null)
        this.ParentMenuItemContainer.CloseAllMenus();
      else if (this is QFloatingMenu)
        this.HideContainer();
      else
        this.ResetState();
    }

    protected virtual void HideContainer()
    {
      if (!this.Visible)
        return;
      this.ResetState();
      this.Visible = false;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void ResetState()
    {
      if (this.HasSimulatedFocus)
        this.SimulateLostFocus();
      this.m_oLastMouseOverItem = (QMenuItem) null;
      if (this == this.RootMenuItemContainer)
        this.PutPersonalized(true);
      this.PutFirstShownCommand(0);
      this.HotkeyVisible = false;
      this.SetMouseOverOuterBoundsItem((QMenuItem) null, false);
      this.SetDepersonalizeMenuItemState(QButtonState.Normal, false);
      this.ResetExpandedItem();
      this.PreviousExpandedItem = (QMenuItem) null;
      this.SetHotItem((QMenuItem) null, QMenuItemActivationType.None);
      this.ProposedExpandedItem = (QMenuItem) null;
      this.AutoExpand = false;
    }

    public virtual bool SetExpandedItem(
      QMenuItem expandedItem,
      bool animate,
      bool showHotkeyPrefix,
      QMenuItemActivationType activationType)
    {
      if (this.ExpandedItem == expandedItem)
        return true;
      this.ResetExpandedItem();
      if (expandedItem != null && expandedItem.HasChildItems && expandedItem.IsAccessible)
      {
        if (!expandedItem.VisibleWhenPersonalized && this.Personalized)
          this.DepersonalizeMenuItemContainer();
        this.MakeSureItemIsShown(this.m_oHotItem, true);
        QMenuCancelEventArgs e1 = new QMenuCancelEventArgs(expandedItem, activationType, false, true);
        QMenuEventArgs e2 = new QMenuEventArgs(expandedItem, activationType, true);
        this.RaiseMenuItemActivating(e1);
        if (e1.Cancel)
          return false;
        this.RaiseMenuItemActivated(e2);
        this.RaiseMenuShowing(e1);
        if (e1.Cancel)
          return false;
        this.SetExpandedItem(expandedItem);
        this.ShowChildMenu(expandedItem, animate, showHotkeyPrefix);
        this.RaiseMenuShowed(e2);
      }
      else
        this.Refresh();
      return true;
    }

    protected virtual void SetExpandedItem(QMenuItem expandedItem) => this.m_oExpandedItem = expandedItem;

    [Browsable(false)]
    internal Rectangle DepersonalizeMenuItemBounds
    {
      get => this.m_oDepersonalizeMenuItemBounds;
      set => this.m_oDepersonalizeMenuItemBounds = value;
    }

    public virtual void ResetExpandedItem()
    {
      if (this.m_oExpandedItem == null || !this.m_oExpandedItem.HasChildItems)
        return;
      this.PreviousExpandedItem = this.m_oExpandedItem;
      this.m_oExpandedItem.ChildMenu.HideMenu();
      this.m_oExpandedItem = (QMenuItem) null;
      this.Refresh();
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual bool ContainsMenuItem(QMenuItem menuItem) => this.Items.Contains(menuItem);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual bool ActivateItem(
      QMenuItem menuItem,
      bool expandWhenPossible,
      bool animate,
      bool showHotkeyPrefix,
      QMenuItemActivationType activationType)
    {
      if (this.Visible && this.ContainsMenuItem(menuItem))
      {
        if (menuItem != null && menuItem.HasChildItems && expandWhenPossible)
          return this.SetExpandedItem(menuItem, animate, showHotkeyPrefix, activationType);
        this.ResetExpandedItem();
        if (menuItem != null && menuItem.IsAccessible)
        {
          if (!menuItem.VisibleWhenPersonalized && this.Personalized)
            this.DepersonalizeMenuItemContainer();
          QMenuCancelEventArgs e1 = new QMenuCancelEventArgs(menuItem, activationType, false, false);
          QMenuEventArgs e2 = new QMenuEventArgs(menuItem, activationType, false);
          this.RaiseMenuItemActivating(e1);
          if (e1.Cancel)
            return false;
          if (menuItem.CloseMenuOnActivate)
            this.CloseAllMenus();
          this.RaiseMenuItemActivated(e2);
        }
      }
      else if (menuItem != null && menuItem.IsAccessible)
      {
        QMenuCancelEventArgs e3 = new QMenuCancelEventArgs(menuItem, activationType, false, false);
        QMenuEventArgs e4 = new QMenuEventArgs(menuItem, activationType, false);
        this.RaiseMenuItemActivating(e3);
        if (e3.Cancel)
          return false;
        if (this.Visible)
          this.CloseAllMenus();
        this.RaiseMenuItemActivated(e4);
      }
      return true;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal bool ExpandItem(
      QMenuItem menuItem,
      bool animate,
      bool showHotKeyPrefix,
      QMenuItemActivationType activationType)
    {
      if (menuItem == null || !menuItem.HasChildItems || !menuItem.IsEnabled)
        return false;
      this.AutoExpand = true;
      if (this.ActivateItem(menuItem, true, animate, showHotKeyPrefix, activationType))
      {
        this.SetHotItem((QMenuItem) null, activationType);
        if (this.ExpandedItem != null && this.ExpandedItem.ChildContainer is QMenu)
          ((QMenuItemContainer) this.ExpandedItem.ChildContainer).SelectNextItem(true);
      }
      return true;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual bool UnPersonalizeMenuContainsPosition(int x, int y) => this.DepersonalizeMenuItemBounds.Contains(x, y);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void DepersonalizeMenuItemContainer()
    {
    }

    [Browsable(false)]
    internal virtual QButtonState DepersonalizeMenuItemState
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => this.m_eDepersonalizeMenuItemState;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void SetDepersonalizeMenuItemState(QButtonState state, bool refresh)
    {
      if (this.m_eDepersonalizeMenuItemState == state)
        return;
      this.m_eDepersonalizeMenuItemState = state;
      if (!(this.DepersonalizeMenuItemBounds != Rectangle.Empty) || !refresh)
        return;
      this.Refresh();
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void ExcludeBoundsFromRegion(Region region, Control useCoordinateSystem)
    {
    }

    public virtual void ShowChildMenu(QMenuItem menuItem, bool animate, bool showHotkeyPrefix)
    {
      if (menuItem == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (menuItem)));
      QCommandDirections animationDirection = QCommandDirections.None;
      QRelativePositions openingItemRelativePosition = QRelativePositions.Above;
      Point location = Point.Empty;
      int num = 1;
      menuItem.ChildMenu.PutPersonalized(this.RootMenuItemContainer.Personalized);
      menuItem.ChildContainer.OwnerWindow = this.OwnerWindow;
      if (menuItem.ChildContainer is QFloatingMenu)
      {
        QFloatingMenu childContainer = (QFloatingMenu) menuItem.ChildContainer;
        childContainer.Configuration = this.ChildMenuConfigurationBase;
        childContainer.ToolTipConfiguration = (QToolTipConfiguration) QObjectCloner.CloneObject((object) this.ToolTipConfiguration);
        childContainer.ToolTipConfiguration.Enabled = this.ChildMenuConfigurationBase.ShowToolTips;
        childContainer.ColorScheme = this.ColorScheme;
        childContainer.Font = this.Font;
        childContainer.UseAnimation = animate;
        childContainer.HotkeyVisible = showHotkeyPrefix;
      }
      Rectangle screen = this.ContainerRectangleToScreen(menuItem.Bounds);
      if (this.Orientation == QCommandContainerOrientation.Horizontal)
      {
        location = new Point(screen.Left, screen.Bottom - num);
        animationDirection = QCommandDirections.Down;
        openingItemRelativePosition = QRelativePositions.Above;
      }
      if (this.Orientation == QCommandContainerOrientation.Vertical)
      {
        location = new Point(screen.Right - num, screen.Top);
        animationDirection = QCommandDirections.Right;
        openingItemRelativePosition = QRelativePositions.Left;
      }
      screen.Inflate(-num, -num);
      Size requestedSize = menuItem.ChildMenu.CalculateRequestedSize();
      QMenuCalculateBoundsResult menuBounds = QMenu.CalculateMenuBounds(new Rectangle(location, requestedSize), screen, openingItemRelativePosition, animationDirection, QMenuCalculateBoundsOptions.None);
      this.Refresh();
      menuItem.ChildMenu.ShowMenu(menuBounds.Bounds, screen, menuBounds.OpeningItemPosition, menuBounds.AnimateDirection);
    }

    internal void DefaultOnMouseMove(MouseEventArgs e) => base.OnMouseMove(e);

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.m_oLastMouseMovePoint.X == e.X && this.m_oLastMouseMovePoint.Y == e.Y)
        return;
      this.m_oLastMouseMovePoint = new Point(e.X, e.Y);
      QMenuItem qmenuItem = (QMenuItem) null;
      QMenuItem withOuterBoundsOn = this.GetItemWithOuterBoundsOn(e.X, e.Y);
      this.SetMouseOverOuterBoundsItem(withOuterBoundsOn, false);
      if (withOuterBoundsOn != null && withOuterBoundsOn.Bounds.Contains(e.X, e.Y))
        qmenuItem = withOuterBoundsOn;
      if (qmenuItem != null && qmenuItem.IsInformationOnly)
        qmenuItem = (QMenuItem) null;
      if (Control.MouseButtons == MouseButtons.Left && this.MouseDownItem != qmenuItem)
        this.MouseDownItem = qmenuItem;
      if (qmenuItem != null && qmenuItem != this.m_oLastMouseOverItem)
      {
        this.m_oLastMouseOverItem = qmenuItem;
        this.m_lMouseHoverTickStart = QMisc.TickCount;
        this.SetDepersonalizeMenuItemState(QButtonState.Normal, true);
        if (this.AutoExpand)
        {
          if (qmenuItem != this.ExpandedItem && qmenuItem != this.ProposedExpandedItem)
          {
            if (qmenuItem.HasChildItems && qmenuItem.IsEnabled)
            {
              this.SetHotItem(qmenuItem, QMenuItemActivationType.Mouse);
              this.ProposedExpandedItem = qmenuItem;
              this.m_lExpandedItemMouseOffTickStart = -1L;
            }
            else
            {
              this.SetHotItem(qmenuItem, QMenuItemActivationType.Mouse);
              this.ProposedExpandedItem = (QMenuItem) null;
              if (this.ConfigurationBase.UsedExpandingDelay == 0)
              {
                this.ResetExpandedItem();
                if ((this.m_eBehaviorFlags & QMenuItemContainerBehaviorFlags.StopAutoExpandWhenOverHotItem) == QMenuItemContainerBehaviorFlags.StopAutoExpandWhenOverHotItem)
                  this.AutoExpand = false;
              }
              if (this.m_lExpandedItemMouseOffTickStart > 0L)
                return;
              this.m_lExpandedItemMouseOffTickStart = QMisc.TickCount;
            }
          }
          else
          {
            this.m_lExpandedItemMouseOffTickStart = -1L;
            this.SetHotItem(qmenuItem, QMenuItemActivationType.Mouse);
          }
        }
        else
        {
          if (qmenuItem == this.HotItem || qmenuItem == this.ExpandedItem)
            return;
          this.ResetExpandedItem();
          this.SetHotItem(qmenuItem, QMenuItemActivationType.Mouse);
          this.m_lExpandedItemMouseOffTickStart = -1L;
        }
      }
      else
      {
        if (qmenuItem != null)
          return;
        this.m_oLastMouseOverItem = (QMenuItem) null;
        this.SetHotItem((QMenuItem) null, QMenuItemActivationType.Mouse);
        this.ProposedExpandedItem = (QMenuItem) null;
        if (this.UnPersonalizeMenuContainsPosition(e.X, e.Y))
        {
          if (this.DepersonalizeMenuItemState != QButtonState.Normal)
            return;
          this.m_lMouseHoverTickStart = QMisc.TickCount;
          if (e.Button == MouseButtons.Left)
            this.SetDepersonalizeMenuItemState(QButtonState.Pressed, true);
          else
            this.SetDepersonalizeMenuItemState(QButtonState.Hot, true);
          if (this.m_lExpandedItemMouseOffTickStart <= 0L)
            this.m_lExpandedItemMouseOffTickStart = QMisc.TickCount;
          this.StartTimer();
        }
        else
        {
          this.SetDepersonalizeMenuItemState(QButtonState.Normal, true);
          this.m_lMouseHoverTickStart = -1L;
        }
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      bool flag1 = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.SimplePersonalizing) == QMenuItemContainerBehaviorFlags.SimplePersonalizing;
      this.Capture = false;
      QMenuItem menuItem = this.GetItemAtPosition(e.X, e.Y);
      this.RaiseMenuItemMouseDown(new QMenuMouseEventArgs(e, menuItem));
      if (e.Button == MouseButtons.Left)
      {
        bool flag2 = (this.m_eBehaviorFlags & QMenuItemContainerBehaviorFlags.HideExpandedItemOnClick) == QMenuItemContainerBehaviorFlags.HideExpandedItemOnClick;
        bool flag3 = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension) == QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension;
        bool flag4 = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.ExpandItemOnMouseUp) == QMenuItemContainerBehaviorFlags.ExpandItemOnMouseUp;
        if (menuItem != null && menuItem.IsInformationOnly)
          menuItem = (QMenuItem) null;
        this.AutoExpand = false;
        if (menuItem != null)
        {
          if (menuItem == this.ExpandedItem)
          {
            if (flag2)
            {
              this.ResetExpandedItem();
              this.SetHotItem(menuItem, QMenuItemActivationType.Mouse);
            }
            else if (this.ExpandedItem.ChildMenu != null && this.ExpandedItem.ChildMenu.Personalized)
              this.ExpandedItem.ChildMenu.DepersonalizeMenu();
          }
          else if (menuItem == this.HotItem)
          {
            if (menuItem == this.PreviousExpandedItem && !flag1)
              this.RootMenuItemContainer.PutPersonalized(false);
            this.m_lMouseHoverTickStart = QMisc.TickCount;
            bool flag5 = !flag3 || flag3 && menuItem.HasChildItemsHotBounds.Contains(e.X, e.Y);
            if (!menuItem.HasChildItems && flag5 && !flag4)
              this.RaiseMenuItemsRequested(new QMenuEventArgs(menuItem, QMenuItemActivationType.Mouse, true));
            if (menuItem.HasChildItems && flag5 && !flag4)
            {
              this.AutoExpand = true;
              this.ActivateItem(menuItem, true, true, false, QMenuItemActivationType.Mouse);
            }
            else
              this.MouseDownItem = menuItem;
          }
        }
        else
        {
          this.ResetExpandedItem();
          this.SetHotItem((QMenuItem) null, QMenuItemActivationType.Mouse);
          if (this.DepersonalizeMenuItemState == QButtonState.Hot)
            this.SetDepersonalizeMenuItemState(QButtonState.Pressed, true);
        }
      }
      if (this.ExpandedItem == null && this.HasSimulatedFocus)
        this.SimulateLostFocus();
      else if (this.ExpandedItem != null || this.MouseDownItem != null)
        this.SimulateGotFocus();
      base.OnMouseDown(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.MouseOverMenuItemContainer)
        return;
      this.HandleMouseLeave();
      base.OnMouseLeave(e);
    }

    internal void HandleMouseLeave()
    {
      this.SetDepersonalizeMenuItemState(QButtonState.Normal, true);
      this.m_oMouseDownItem = (QMenuItem) null;
      this.SetHotItem((QMenuItem) null, QMenuItemActivationType.Mouse);
      this.ToolTipText = (string) null;
      this.ProposedExpandedItem = (QMenuItem) null;
      this.m_oLastMouseOverItem = (QMenuItem) null;
      this.SetMouseOverOuterBoundsItem((QMenuItem) null, true);
      this.m_lMouseHoverTickStart = -1L;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      QMenuItem menuItem = this.GetItemAtPosition(e.X, e.Y);
      this.RaiseMenuItemMouseUp(new QMenuMouseEventArgs(e, menuItem));
      base.OnMouseUp(e);
      if (menuItem != null && menuItem.IsInformationOnly)
        menuItem = (QMenuItem) null;
      bool flag1 = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.ExpandItemOnMouseUp) == QMenuItemContainerBehaviorFlags.ExpandItemOnMouseUp;
      if (menuItem != null && this.DepersonalizeMenuItemState != QButtonState.Pressed && e.Button == MouseButtons.Left)
      {
        bool flag2 = (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension) == QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension;
        bool flag3 = !flag2 || flag2 && menuItem.HasChildItemsHotBounds.Contains(e.X, e.Y);
        if (flag3 && flag1 && !menuItem.HasChildItems)
          this.RaiseMenuItemsRequested(new QMenuEventArgs(menuItem, QMenuItemActivationType.Mouse, true));
        if (flag3 && flag1 && menuItem.HasChildItems)
        {
          this.AutoExpand = true;
          this.ActivateItem(menuItem, true, true, false, QMenuItemActivationType.Mouse);
        }
        else if (!flag3 || !menuItem.HasChildItems)
          this.ActivateItem(menuItem, false, true, false, QMenuItemActivationType.Mouse);
        if (menuItem.MouseIsOverMenuItem)
          this.SetHotItem(menuItem, QMenuItemActivationType.Mouse);
        this.MouseDownItem = (QMenuItem) null;
      }
      else
      {
        if (this.DepersonalizeMenuItemState != QButtonState.Pressed)
          return;
        this.DepersonalizeMenuItemContainer();
      }
    }

    internal bool ShouldBubbleUpEvents
    {
      get
      {
        if (this.ParentMenuItemContainer == null)
          return false;
        return this.BubbleEventsToCustomParent || this.ParentMenuItemContainer != this.CustomParentCommandContainer;
      }
    }

    internal virtual void RaiseCustomizeMenuShowed(EventArgs e) => this.OnCustomizeMenuShowed(e);

    internal virtual void RaiseCustomizeMenuClosed(EventArgs e) => this.OnCustomizeMenuClosed(e);

    internal virtual void RaiseMenuItemsRequested(QMenuEventArgs e)
    {
      this.OnMenuItemsRequested(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuItemsRequested(e);
    }

    internal void RaiseMenuItemMouseDown(QMenuMouseEventArgs e)
    {
      this.OnMenuItemMouseDown(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuItemMouseDown(e);
    }

    internal void RaiseMenuItemMouseUp(QMenuMouseEventArgs e)
    {
      this.OnMenuItemMouseUp(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuItemMouseUp(e);
    }

    internal void RaiseMenuItemSelected(QMenuEventArgs e)
    {
      this.OnMenuItemSelected(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuItemSelected(e);
    }

    protected virtual void OnCustomizeMenuShowed(EventArgs e) => this.m_oCustomizeMenuShowedDelegate = QWeakDelegate.InvokeDelegate(this.m_oCustomizeMenuShowedDelegate, (object) this, (object) e);

    protected virtual void OnCustomizeMenuClosed(EventArgs e) => this.m_oCustomizeMenuClosedDelegate = QWeakDelegate.InvokeDelegate(this.m_oCustomizeMenuClosedDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemsRequested(QMenuEventArgs e)
    {
      this.m_oMenuItemsRequestedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemsRequestedDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuItemsRequested(e);
    }

    protected virtual void OnMenuItemMouseDown(QMenuMouseEventArgs e)
    {
      this.m_oMenuItemMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemMouseDownDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuItemMouseDown(e);
    }

    protected virtual void OnMenuItemMouseUp(QMenuMouseEventArgs e)
    {
      this.m_oMenuItemMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemMouseUpDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuItemMouseUp(e);
    }

    protected virtual void OnMenuItemSelected(QMenuEventArgs e)
    {
      this.m_oMenuItemSelectedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemSelectedDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuItemSelected(e);
    }

    internal void RaiseMenuItemActivating(QMenuCancelEventArgs e)
    {
      this.OnMenuItemActivating(e);
      if (e.Cancel || e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuItemActivating(e);
    }

    protected virtual void OnMenuItemActivating(QMenuCancelEventArgs e)
    {
      this.m_oMenuItemActivatingDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemActivatingDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuItemActivating(e);
    }

    internal void RaiseMenuItemActivated(QMenuEventArgs e)
    {
      this.OnMenuItemActivated(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuItemActivated(e);
    }

    protected virtual void OnMenuItemActivated(QMenuEventArgs e)
    {
      this.m_oMenuItemActivatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemActivatedDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuItemActivated(e);
    }

    internal void RaiseMenuShowing(QMenuCancelEventArgs e)
    {
      this.OnMenuShowing(e);
      if (e.Cancel || e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuShowing(e);
    }

    protected virtual void OnMenuShowing(QMenuCancelEventArgs e)
    {
      this.m_oMenuShowingDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuShowingDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuShowing(e);
    }

    internal void RaiseMenuShowed(QMenuEventArgs e)
    {
      this.OnMenuShowed(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaiseMenuShowed(e);
    }

    protected virtual void OnMenuShowed(QMenuEventArgs e)
    {
      this.m_oMenuShowedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuShowedDelegate, (object) this, (object) e);
      if (!this.ShouldBubbleUpEvents)
        return;
      this.ParentMenuItemContainer.OnMenuShowed(e);
    }

    internal void RaisePaintMenuItem(QPaintMenuItemEventArgs e)
    {
      this.OnPaintMenuItem(e);
      if (e.MenuItem == null)
        return;
      e.MenuItem.RaisePaintMenuItem(e);
    }

    protected virtual void OnPaintMenuItem(QPaintMenuItemEventArgs e) => this.m_oPaintMenuItemDelegate = QWeakDelegate.InvokeDelegate(this.m_oPaintMenuItemDelegate, (object) this, (object) e);

    internal void PaintAdornments(Graphics graphics)
    {
      if (this.m_oDesignerMenuItem == null || !this.DesignMode)
        return;
      Pen pen = new Pen(QColorScheme.Global.ExplorerBarText.GetColor(QColorScheme.Global.CurrentTheme), 1f);
      pen.DashStyle = DashStyle.Dash;
      graphics.DrawRectangle(pen, this.m_oDesignerMenuItem.ContentsBounds);
      pen.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.m_oComponents != null)
      {
        this.m_oComponents.Dispose();
        this.m_oComponents = (IContainer) null;
      }
      base.Dispose(disposing);
    }

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (e.TimerId != 17)
        return;
      if (this.ConfigurationBase.UsedExpandingDelay > 0)
      {
        if (this.ProposedExpandedItem != null)
        {
          if (this.m_lProposedExpandedItemTickStart + (long) this.ConfigurationBase.UsedExpandingDelay <= QMisc.TickCount)
          {
            this.SetExpandedItem(this.ProposedExpandedItem, true, false, QMenuItemActivationType.Mouse);
            this.ProposedExpandedItem = (QMenuItem) null;
          }
        }
        else if (this.ItemOrDepersonalizeItemHot && this.ExpandedItem != null && this.HotItem != this.ExpandedItem && this.m_lExpandedItemMouseOffTickStart > 0L && this.m_lExpandedItemMouseOffTickStart + (long) this.ConfigurationBase.UsedExpandingDelay <= QMisc.TickCount)
        {
          if ((this.m_eBehaviorFlags & QMenuItemContainerBehaviorFlags.StopAutoExpandWhenOverHotItem) == QMenuItemContainerBehaviorFlags.StopAutoExpandWhenOverHotItem)
            this.AutoExpand = false;
          this.ResetExpandedItem();
        }
      }
      if (this.m_lMouseHoverTickStart > 0L && this.m_lMouseHoverTickStart + (long) this.ConfigurationBase.DepersonalizeDelay <= QMisc.TickCount)
      {
        if (this.ExpandedItem != null)
        {
          if (this.ExpandedItem.ChildMenu != null && this.ExpandedItem.ChildMenu.Personalized)
            this.ExpandedItem.ChildMenu.DepersonalizeMenu();
        }
        else if (this.Personalized && this.DepersonalizeMenuItemState == QButtonState.Hot || this.DepersonalizeMenuItemState == QButtonState.Pressed)
          this.DepersonalizeMenuItemContainer();
        this.m_lMouseHoverTickStart = -1L;
      }
      else
      {
        if (this.ProposedExpandedItem != null || this.m_lMouseHoverTickStart > 0L || this.ExpandedItem != null)
          return;
        this.StopTimer();
      }
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Configuration_Changed(object sender, EventArgs e)
    {
      this.OnConfigurationChanged(EventArgs.Empty);
      if (!this.Visible)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
    }

    private void ChildMenuConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
    }
  }
}
