// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenu
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DesignerSerializer(typeof (QCompositeMenuCodeSerializer), typeof (CodeDomSerializer))]
  [ToolboxBitmap(typeof (QCompositeMenu), "Resources.ControlImages.QCompositeMenu.bmp")]
  [ToolboxItem(true)]
  [Designer(typeof (QCompositeMenuDesigner), typeof (IDesigner))]
  public class QCompositeMenu : 
    QControlComponent,
    IQCompositeItemEventRaiser,
    IQCompositeItemEventPublisher,
    IQKeyboardHookClient,
    IQKeyboardMessageFilter,
    IQFloatingWindowEventRaiser,
    IQComponentHost
  {
    private QPartCollection m_oItems;
    private QPartCollection m_oOriginalItems;
    private QCompositeWindow m_oWindow;
    private QCompositeWindow m_oCustomWindow;
    private QCompositeConfiguration m_oCompositeConfiguration;
    private QToolTipConfiguration m_oToolTipConfiguration;
    private QCompositeWindowConfiguration m_oWindowConfiguration;
    private ArrayList m_aListeners;
    private ArrayList m_aComponentListeners;
    private IWin32Window m_oOwnerWindow;
    private QKeyboardHooker m_oGlobalKeyboardHooker;
    private QKeyboardMessageFilter m_oKeyboardMessageFilter;
    private QContextMenuShortcutScope m_eShortcutScope;
    private bool m_bHandleShortcutKeys = true;
    private bool m_bSuppressDefaultContextMenu = true;
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QWeakDelegate m_oItemPaintStageDelegate;
    private QWeakDelegate m_oItemActivating;
    private QWeakDelegate m_oItemActivated;
    private QWeakDelegate m_oItemSelected;
    private QWeakDelegate m_oItemExpanded;
    private QWeakDelegate m_oItemExpanding;
    private QWeakDelegate m_oItemCollapsed;
    private QWeakDelegate m_oItemCollapsing;
    private QWeakDelegate m_oMenuShowing;
    private QWeakDelegate m_oMenuShowed;
    private QWeakDelegate m_oMenuClosing;
    private QWeakDelegate m_oMenuClosed;

    public QCompositeMenu()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oToolTipConfiguration = this.CreateToolTipConfiguration();
      if (this.m_oToolTipConfiguration != null)
        this.m_oToolTipConfiguration.ConfigurationChanged += new EventHandler(this.ToolTipConfiguration_ConfigurationChanged);
      this.m_oGlobalKeyboardHooker = new QKeyboardHooker((IQKeyboardHookClient) this);
      this.m_oGlobalKeyboardHooker.GlobalHook = true;
      this.m_oKeyboardMessageFilter = new QKeyboardMessageFilter((IQKeyboardMessageFilter) this);
      this.m_aListeners = new ArrayList();
      this.m_aComponentListeners = new ArrayList();
      this.m_oCompositeConfiguration = this.CreateCompositeConfiguration();
      this.m_oCompositeConfiguration.ConfigurationChanged += new EventHandler(this.CompositeConfiguration_ConfigurationChanged);
      this.m_oWindowConfiguration = this.CreateWindowConfiguration();
      this.m_oWindowConfiguration.ConfigurationChanged += new EventHandler(this.WindowConfiguration_ConfigurationChanged);
      this.m_oItems = new QPartCollection();
      this.ConfigureKeyboardFilters();
    }

    protected virtual QCompositeWindow CreateWindow()
    {
      QCompositeWindow window = new QCompositeWindow((IQPart) null, this.m_oItems, this.ColorScheme, (IWin32Window) null);
      window.SetCustomComponentHost((IQComponentHost) this, false);
      return window;
    }

    [Browsable(false)]
    public virtual bool CanExpand => this.Items.Count > 0 || this.m_oCustomWindow != null;

    protected virtual QToolTipConfiguration CreateToolTipConfiguration() => (QToolTipConfiguration) new QCompositeToolTipConfiguration();

    protected virtual QCompositeConfiguration CreateCompositeConfiguration() => (QCompositeConfiguration) new QCompositeMenuConfiguration();

    public bool ShouldSerializeToolTipConfiguration() => this.m_oToolTipConfiguration != null && !this.m_oToolTipConfiguration.IsSetToDefaultValues();

    public void ResetToolTipConfiguration()
    {
      if (this.m_oToolTipConfiguration == null)
        return;
      this.m_oToolTipConfiguration.SetToDefaultValues();
    }

    [Description("Gets the QToolTipConfiguration for this QCompositeMenu.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QToolTipConfiguration ToolTipConfiguration => this.m_oToolTipConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IWin32Window OwnerWindow
    {
      get => this.m_oOwnerWindow;
      set => this.m_oOwnerWindow = value;
    }

    [Category("QBehavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the OwnerForm. The Window be owned by the OwnerForm. This must be set when the owning window has TopMost set to true.")]
    public Form OwnerForm
    {
      get => this.OwnerWindow as Form;
      set => this.OwnerWindow = (IWin32Window) value;
    }

    protected virtual QCompositeWindowConfiguration CreateWindowConfiguration() => new QCompositeWindowConfiguration();

    private void SecureWindow()
    {
      if (this.m_oWindow != null)
        return;
      this.m_oWindow = this.CreateWindow();
    }

    [DefaultValue(true)]
    [Category("QBehavior")]
    [Description("Gets or sets whether the default Windows ContextMenu must be suppressed")]
    public bool SuppressDefaultContextMenu
    {
      get => this.m_bSuppressDefaultContextMenu;
      set => this.m_bSuppressDefaultContextMenu = value;
    }

    private bool ShouldSerializeItems() => this.m_oCustomWindow == null;

    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection of QCompositeItems of this menu")]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    public QPartCollection Items => this.m_oItems;

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    public bool ShouldSerializeCompositeConfiguration() => !this.m_oCompositeConfiguration.IsSetToDefaultValues();

    public void ResetCompositeConfiguration() => this.m_oCompositeConfiguration.SetToDefaultValues();

    [Description("Gets the configuration of the composite")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QCompositeConfiguration CompositeConfiguration => this.m_oCompositeConfiguration;

    public bool ShouldSerializeWindowConfiguration() => !this.m_oWindowConfiguration.IsSetToDefaultValues();

    public void ResetWindowConfiguration() => this.m_oWindowConfiguration.SetToDefaultValues();

    [Description("Gets the configuration used for the window.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeWindowConfiguration WindowConfiguration => this.m_oWindowConfiguration;

    [Description("Gets or sets whether shortcuts must be handled.")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public bool HandleShortcutKeys
    {
      get => this.m_bHandleShortcutKeys;
      set
      {
        this.m_bHandleShortcutKeys = value;
        this.ConfigureKeyboardFilters();
      }
    }

    [Description("Gets or sets the ShortcutScope. This indicates when shorcuts should react. When setting this value to Global, be carefull of the Shortcuts you chose. They get handled even when an other application has an implementation for this shortcut.")]
    [DefaultValue(QContextMenuShortcutScope.AttachedControls)]
    [Category("QBehavior")]
    public QContextMenuShortcutScope ShortcutScope
    {
      get => this.m_eShortcutScope;
      set
      {
        this.m_eShortcutScope = value;
        this.ConfigureKeyboardFilters();
      }
    }

    [Browsable(false)]
    public bool IsWindowCreated => this.Window != null;

    [Browsable(false)]
    public bool IsWindowVisible => this.Window != null && this.Window.Visible;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeWindow Window
    {
      get
      {
        if (this.m_oCustomWindow != null)
          return this.m_oCustomWindow;
        this.SecureWindow();
        return this.m_oWindow;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    [Category("QBehavior")]
    [Description("Gets or sets a custom child window. When this is set, the items, colorscheme and configuration of the custom window are used instead of items and this configuration")]
    public virtual QCompositeWindow CustomWindow
    {
      get => this.m_oCustomWindow;
      set => this.SetCustomWindow(value, true);
    }

    internal void SetCustomWindow(QCompositeWindow value, bool removeFromCurrentItem)
    {
      if (this.m_oCustomWindow != null)
      {
        this.m_oCustomWindow.SetCustomComponentHost((IQComponentHost) null, false);
        if (this.m_oCustomWindow.Items == this.m_oItems)
          this.m_oItems = (QPartCollection) null;
      }
      this.m_oCustomWindow = value;
      if (this.m_oCustomWindow != null)
      {
        this.m_oCustomWindow.SetCustomComponentHost((IQComponentHost) this, removeFromCurrentItem);
        this.m_oCustomWindow.Composite.PutParentPart((IQPart) null);
        if (this.m_oOriginalItems == null && this.m_oItems != null)
          this.m_oOriginalItems = this.m_oItems;
        this.m_oItems = this.m_oCustomWindow.Items;
      }
      else
      {
        if (this.m_oOriginalItems == null)
          return;
        this.m_oItems = this.m_oOriginalItems;
      }
    }

    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItemBase is selected")]
    [QWeakEvent]
    public event QCompositeEventHandler ItemSelected
    {
      add => this.m_oItemSelected = QWeakDelegate.Combine(this.m_oItemSelected, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemSelected = QWeakDelegate.Remove(this.m_oItemSelected, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the item is expanded")]
    public event QCompositeExpandedEventHandler ItemExpanded
    {
      add => this.m_oItemExpanded = QWeakDelegate.Combine(this.m_oItemExpanded, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanded = QWeakDelegate.Remove(this.m_oItemExpanded, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the child container is expanding")]
    public event QCompositeExpandingCancelEventHandler ItemExpanding
    {
      add => this.m_oItemExpanding = QWeakDelegate.Combine(this.m_oItemExpanding, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanding = QWeakDelegate.Remove(this.m_oItemExpanding, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the item is collapsing")]
    [QWeakEvent]
    public event QCompositeCancelEventHandler ItemCollapsing
    {
      add => this.m_oItemCollapsing = QWeakDelegate.Combine(this.m_oItemCollapsing, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemCollapsing = QWeakDelegate.Remove(this.m_oItemCollapsing, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the item is collapsed")]
    public event QCompositeEventHandler ItemCollapsed
    {
      add => this.m_oItemCollapsed = QWeakDelegate.Combine(this.m_oItemCollapsed, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemCollapsed = QWeakDelegate.Remove(this.m_oItemCollapsed, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when a layer of the QCompositeItem is painted")]
    [QWeakEvent]
    public event QCompositePaintStageEventHandler PaintItem
    {
      add => this.m_oItemPaintStageDelegate = QWeakDelegate.Combine(this.m_oItemPaintStageDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemPaintStageDelegate = QWeakDelegate.Remove(this.m_oItemPaintStageDelegate, (Delegate) value);
    }

    [Description("Gets raised when the QCompositeItemBase is activating")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QCompositeCancelEventHandler ItemActivating
    {
      add => this.m_oItemActivating = QWeakDelegate.Combine(this.m_oItemActivating, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivating = QWeakDelegate.Remove(this.m_oItemActivating, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the QCompositeItemBase is activated")]
    public event QCompositeEventHandler ItemActivated
    {
      add => this.m_oItemActivated = QWeakDelegate.Combine(this.m_oItemActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivated = QWeakDelegate.Remove(this.m_oItemActivated, (Delegate) value);
    }

    [Description("Gets raised when the menu is about to show.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeExpandingCancelEventHandler MenuShowing
    {
      add => this.m_oMenuShowing = QWeakDelegate.Combine(this.m_oMenuShowing, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuShowing = QWeakDelegate.Remove(this.m_oMenuShowing, (Delegate) value);
    }

    [Description("Gets raised when the menu is shown.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeExpandedEventHandler MenuShowed
    {
      add => this.m_oMenuShowed = QWeakDelegate.Combine(this.m_oMenuShowed, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuShowed = QWeakDelegate.Remove(this.m_oMenuShowed, (Delegate) value);
    }

    [Description("Gets raised when the menu is about to close.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QCompositeCancelEventHandler MenuClosing
    {
      add => this.m_oMenuClosing = QWeakDelegate.Combine(this.m_oMenuClosing, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuClosing = QWeakDelegate.Remove(this.m_oMenuClosing, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the menu is closed.")]
    public event QCompositeEventHandler MenuClosed
    {
      add => this.m_oMenuClosed = QWeakDelegate.Combine(this.m_oMenuClosed, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuClosed = QWeakDelegate.Remove(this.m_oMenuClosed, (Delegate) value);
    }

    internal bool ExpandBehaviorIsSet(QCompositeExpandBehavior behavior) => (this.CompositeConfiguration.ExpandBehavior & behavior) == behavior;

    private void ConfigureKeyboardFilters()
    {
      if (this.DesignMode)
        return;
      if (this.HandleShortcutKeys && this.m_eShortcutScope == QContextMenuShortcutScope.Global && !this.m_oGlobalKeyboardHooker.KeyboardHooked)
        this.m_oGlobalKeyboardHooker.KeyboardHooked = true;
      else if ((!this.HandleShortcutKeys || this.m_eShortcutScope != QContextMenuShortcutScope.Global) && this.m_oGlobalKeyboardHooker.KeyboardHooked)
        this.m_oGlobalKeyboardHooker.KeyboardHooked = false;
      if (!this.m_oKeyboardMessageFilter.Installed && (this.HandleShortcutKeys || this.IsWindowVisible))
      {
        this.m_oKeyboardMessageFilter.Install();
      }
      else
      {
        if (!this.m_oKeyboardMessageFilter.Installed || this.HandleShortcutKeys || this.IsWindowVisible)
          return;
        this.m_oKeyboardMessageFilter.Uninstall();
      }
    }

    protected virtual void ConfigureWindow()
    {
      if (this.Window == this.CustomWindow)
        return;
      this.Window.SuspendChangeNotification();
      this.Window.Configuration = this.WindowConfiguration;
      this.Window.ChildWindowConfiguration = this.WindowConfiguration;
      this.Window.CompositeConfiguration = this.CompositeConfiguration;
      this.Window.ChildCompositeConfiguration = this.CompositeConfiguration;
      this.Window.ColorScheme = this.ColorScheme;
      this.Window.ChildCompositeColorScheme = this.ColorScheme;
      this.Window.ToolTipConfiguration = this.ToolTipConfiguration;
      this.Window.ResumeChangeNotification(false);
    }

    public bool ShowMenu(Point location) => this.ShowMenu(location, (IWin32Window) null, QCompositeActivationType.None);

    public bool ShowMenu(Point location, IWin32Window owner) => this.ShowMenu(location, owner, QCompositeActivationType.None);

    public bool ShowMenu(
      Point location,
      IWin32Window owner,
      QCompositeActivationType activationType)
    {
      this.ConfigureWindow();
      this.Window.OwnerWindow = owner != null ? owner : this.OwnerWindow;
      Keys keys = Keys.None;
      bool flag = this.ExpandBehaviorIsSet(QCompositeExpandBehavior.CloseOnNavigationKeys);
      QRelativePositions openingItemRelativePosition;
      QCommandDirections animateDirection1;
      switch (this.CompositeConfiguration.ExpandDirection)
      {
        case QCompositeExpandDirection.Left:
          openingItemRelativePosition = QRelativePositions.Right;
          animateDirection1 = QCommandDirections.Left;
          if (flag)
          {
            keys = Keys.Right;
            break;
          }
          break;
        case QCompositeExpandDirection.Up:
          openingItemRelativePosition = QRelativePositions.Below;
          animateDirection1 = QCommandDirections.Up;
          if (flag)
          {
            keys = Keys.Down;
            break;
          }
          break;
        case QCompositeExpandDirection.Down:
          openingItemRelativePosition = QRelativePositions.Above;
          animateDirection1 = QCommandDirections.Down;
          if (flag)
          {
            keys = Keys.Up;
            break;
          }
          break;
        default:
          openingItemRelativePosition = QRelativePositions.Left;
          animateDirection1 = QCommandDirections.Right;
          if (flag)
          {
            keys = Keys.Left;
            break;
          }
          break;
      }
      Rectangle bounds1 = this.Window.CalculateBounds(new Rectangle(location, Size.Empty), Rectangle.Empty, ref openingItemRelativePosition, ref animateDirection1);
      QCompositeExpandingCancelEventArgs e = new QCompositeExpandingCancelEventArgs(this.Window.Composite, (QCompositeItemBase) null, activationType, bounds1, animateDirection1, true, false);
      this.OnMenuShowing(e);
      if (e.Cancel)
        return false;
      Rectangle bounds2 = e.Bounds;
      QCommandDirections animateDirection2 = e.AnimateDirection;
      this.Window.AllowAnimation = e.AllowAnimation;
      this.Window.Composite.CloseKeysNavigation = keys;
      QCompositeHelper.ResetState((IQPart) this.Window.Composite);
      if (QCompositeHelper.IsKeyboardActivationType(activationType))
        this.Window.Composite.SelectFirstItem(activationType, true);
      this.Window.Composite.OpeningItemRelativePosition = openingItemRelativePosition;
      this.Window.Show(bounds2, animateDirection2);
      this.ConfigureKeyboardFilters();
      this.OnMenuShowed(new QCompositeExpandedEventArgs(this.Window.Composite, (QCompositeItemBase) null, activationType, e.Bounds, e.AnimateDirection, e.AllowAnimation));
      return true;
    }

    public void HideMenu()
    {
      if (!this.IsWindowCreated || !this.Window.Visible)
        return;
      this.Window.Close(QCompositeActivationType.None);
    }

    public void ShowMenuOnNotifyIcon(NotifyIcon notifyIcon, Point location)
    {
      this.Window.TopMost = true;
      NativeMethods.SetForegroundWindow(QMisc.GetNotifyIconWindow(notifyIcon).Handle);
      this.ShowMenu(location, (IWin32Window) null, QCompositeActivationType.None);
    }

    internal bool IsShortcutKey(Keys key) => Enum.IsDefined(typeof (Shortcut), (object) (int) key);

    internal bool HandleShortcutKey(Keys key, out bool suppressToSystem) => QCompositeHelper.HandleShortcutKey((IQPartCollection) this.m_oItems, key, out suppressToSystem);

    private bool ShouldHandleShortcutForControl(Control destinationControl, bool isGlobalFilter)
    {
      if (this.HandleShortcutKeys)
      {
        if (this.m_eShortcutScope == QContextMenuShortcutScope.AttachedControls)
          return !isGlobalFilter && this.HasListenerForControl(destinationControl);
        if (this.m_eShortcutScope == QContextMenuShortcutScope.Application)
          return !isGlobalFilter;
        if (this.m_eShortcutScope == QContextMenuShortcutScope.Global)
          return isGlobalFilter;
      }
      return false;
    }

    protected virtual bool HandleKeyDown(
      Keys keys,
      Control destinationControl,
      Message message,
      bool isGlobalFilter)
    {
      if (!this.IsWindowVisible)
      {
        Keys key = Control.ModifierKeys | keys;
        if (this.IsShortcutKey(key) && this.ShouldHandleShortcutForControl(destinationControl, isGlobalFilter))
        {
          bool suppressToSystem = false;
          return this.HandleShortcutKey(key, out suppressToSystem) && suppressToSystem;
        }
      }
      else if (!isGlobalFilter)
      {
        this.Window.Composite.HandleKeyDown(keys, destinationControl, message, false);
        return true;
      }
      return false;
    }

    protected virtual bool HandleKeyUp(
      Keys keys,
      Control destinationControl,
      Message message,
      bool isGlobalFilter)
    {
      if (!this.IsWindowVisible || isGlobalFilter)
        return false;
      this.Window.Composite.HandleKeyUp(keys, destinationControl, message);
      return true;
    }

    private QCompositeMenuControlListener GetListenerForControl(
      Control control)
    {
      for (int index = 0; index < this.m_aListeners.Count; ++index)
      {
        QCompositeMenuControlListener aListener = (QCompositeMenuControlListener) this.m_aListeners[index];
        if (aListener.Control == control)
          return aListener;
      }
      return (QCompositeMenuControlListener) null;
    }

    public bool HasListenerForControl(Control control) => this.GetListenerForControl(control) != null;

    public void AddListener(Control control)
    {
      if (!this.HasListenerForControl(control))
        this.m_aListeners.Add((object) new QCompositeMenuControlListener(this, control, this.DesignMode));
      QCompositeMenuDesigner.GetExtenderProviderOnSite(this.Site, true)?.SetQCompositeMenu((Component) control, this);
    }

    public void RemoveListener(Control control)
    {
      QCompositeMenuControlListener listenerForControl = this.GetListenerForControl(control);
      if (listenerForControl == null)
        return;
      this.m_aListeners.Remove((object) listenerForControl);
      listenerForControl.ReleaseHandle();
    }

    public bool HasListenerForNotifyIcon(NotifyIcon notifyIcon) => this.m_aComponentListeners.Contains((object) notifyIcon);

    public void AddListener(NotifyIcon notifyIcon)
    {
      if (!this.m_aComponentListeners.Contains((object) notifyIcon))
      {
        this.m_aComponentListeners.Add((object) notifyIcon);
        this.m_oEventConsumers.Add((QWeakEventConsumer) new QWeakMouseEventConsumer((Delegate) new MouseEventHandler(this.NotifyIcon_MouseUp), (object) notifyIcon, "MouseUp"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.NotifyIcon_Disposed), (object) notifyIcon, "Disposed"));
      }
      QCompositeMenuDesigner.GetExtenderProviderOnSite(this.Site, true)?.SetQCompositeMenu((Component) notifyIcon, this);
    }

    public void RemoveListener(NotifyIcon notifyIcon)
    {
      if (!this.m_aComponentListeners.Contains((object) notifyIcon))
        return;
      this.m_aComponentListeners.Remove((object) notifyIcon);
      this.m_oEventConsumers.DetachAndRemove((Delegate) new MouseEventHandler(this.NotifyIcon_MouseUp));
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.NotifyIcon_Disposed));
    }

    private Control GetControlFromListeners(IntPtr handle)
    {
      for (int index = this.m_aListeners.Count - 1; index >= 0; --index)
      {
        Control control = ((QCompositeMenuControlListener) this.m_aListeners[index]).Control;
        if (control.Handle == handle)
          return control;
      }
      return (Control) null;
    }

    protected virtual void OnItemSelected(QCompositeEventArgs e) => this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e) => this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e) => this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);

    protected virtual void OnItemCollapsed(QCompositeEventArgs e) => this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e) => this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e) => this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e) => this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);

    protected virtual void OnItemActivated(QCompositeEventArgs e) => this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);

    protected virtual void OnMenuShowing(QCompositeExpandingCancelEventArgs e) => this.m_oMenuShowing = QWeakDelegate.InvokeDelegate(this.m_oMenuShowing, (object) this, (object) e);

    protected virtual void OnMenuShowed(QCompositeExpandedEventArgs e) => this.m_oMenuShowed = QWeakDelegate.InvokeDelegate(this.m_oMenuShowed, (object) this, (object) e);

    protected virtual void OnMenuClosing(QCompositeCancelEventArgs e) => this.m_oMenuClosing = QWeakDelegate.InvokeDelegate(this.m_oMenuClosing, (object) this, (object) e);

    protected virtual void OnMenuClosed(QCompositeEventArgs e) => this.m_oMenuClosed = QWeakDelegate.InvokeDelegate(this.m_oMenuClosed, (object) this, (object) e);

    void IQKeyboardHookClient.HandleKeyDown(
      Keys keys,
      ref bool cancelMessage,
      ref bool callNextHook)
    {
      if (!this.HandleKeyDown(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero), true))
        return;
      cancelMessage = true;
      callNextHook = false;
    }

    void IQKeyboardHookClient.HandleKeyUp(
      Keys keys,
      ref bool cancelMessage,
      ref bool callNextHook)
    {
      if (!this.HandleKeyUp(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero), true))
        return;
      cancelMessage = true;
      callNextHook = false;
    }

    bool IQKeyboardMessageFilter.HandleKeyDown(
      Keys keys,
      Control destinationControl,
      Message message)
    {
      return this.HandleKeyDown(keys, destinationControl, message, false);
    }

    bool IQKeyboardMessageFilter.HandleKeyUp(
      Keys keys,
      Control destinationControl,
      Message message)
    {
      return this.HandleKeyUp(keys, destinationControl, message, false);
    }

    void IQCompositeItemEventRaiser.RaisePaintItem(
      QCompositePaintStageEventArgs e)
    {
      this.OnPaintItem(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemActivating(
      QCompositeCancelEventArgs e)
    {
      this.OnItemActivating(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemActivated(
      QCompositeEventArgs e)
    {
      this.OnItemActivated(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemSelected(
      QCompositeEventArgs e)
    {
      this.OnItemSelected(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemExpanding(
      QCompositeExpandingCancelEventArgs e)
    {
      this.OnItemExpanding(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemExpanded(
      QCompositeExpandedEventArgs e)
    {
      this.OnItemExpanded(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemCollapsing(
      QCompositeCancelEventArgs e)
    {
      this.OnItemCollapsing(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemCollapsed(
      QCompositeEventArgs e)
    {
      this.OnItemCollapsed(e);
    }

    void IQFloatingWindowEventRaiser.RaiseClosing(CancelEventArgs e)
    {
      QCompositeCancelEventArgs e1 = new QCompositeCancelEventArgs(this.Window.Composite, (QCompositeItemBase) null, this.Window.LastCloseType, e.Cancel);
      this.OnMenuClosing(e1);
      e.Cancel = e1.Cancel;
    }

    void IQFloatingWindowEventRaiser.RaiseClosed(EventArgs e)
    {
      this.OnMenuClosed(new QCompositeEventArgs(this.Window.Composite, (QCompositeItemBase) null, this.Window.LastCloseType));
      this.ConfigureKeyboardFilters();
    }

    void IQComponentHost.SetComponent(object previousValue, object newValue)
    {
      if (previousValue == null || previousValue != this.m_oCustomWindow)
        return;
      this.SetCustomWindow(newValue as QCompositeWindow, true);
    }

    private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
    {
      if (!(sender is NotifyIcon notifyIcon) || e.Button != MouseButtons.Right)
        return;
      this.ShowMenuOnNotifyIcon(notifyIcon, Control.MousePosition);
    }

    private void NotifyIcon_Disposed(object sender, EventArgs e)
    {
      if (!(sender is NotifyIcon notifyIcon) || !this.m_aComponentListeners.Contains((object) notifyIcon))
        return;
      this.RemoveListener(notifyIcon);
    }

    private void CompositeConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
    }

    private void WindowConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
    }

    private void ToolTipConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.m_oGlobalKeyboardHooker.Dispose();
        if (this.m_oKeyboardMessageFilter.Installed)
        {
          try
          {
            this.m_oKeyboardMessageFilter.Uninstall();
          }
          catch (InvalidOperationException ex)
          {
          }
        }
      }
      base.Dispose(disposing);
    }
  }
}
