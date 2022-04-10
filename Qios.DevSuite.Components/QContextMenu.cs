// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QContextMenu
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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QContextMenuDesigner), typeof (IDesigner))]
  [DesignerSerializer(typeof (QContextMenuCodeSerializer), typeof (CodeDomSerializer))]
  [ToolboxBitmap(typeof (QContextMenu), "Resources.ControlImages.QContextMenu.bmp")]
  [ToolboxItem(true)]
  public class QContextMenu : QControlComponent, IMessageFilter
  {
    private QWeakMessageFilter m_oWeakMessageFilter;
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QCommandCollection m_oMenuItems;
    private ArrayList m_aListeners;
    private ArrayList m_aComponentListeners;
    private QToolTipConfiguration m_oToolTipConfiguration;
    private QFloatingMenuConfiguration m_oConfiguration;
    private Container m_oComponents;
    private QFloatingMenu m_oFloatingMenu;
    private IWin32Window m_oOwnerWindow;
    private IntPtr m_hKeyboardMessageHookHandle = IntPtr.Zero;
    private NativeMethods.HookProc m_oKeyboardMessageHookProcHandler;
    private QContextMenuShortcutScope m_eShortcutScope;
    private bool m_bSuppressDefaultContextMenu = true;
    private QMenuCancelEventHandler m_oFloatingMenuMenuItemActivatingHandler;
    private QMenuEventHandler m_oFloatingMenuMenuItemSelectedHandler;
    private QMenuEventHandler m_oFloatingMenuMenuItemActivatedHandler;
    private QMenuCancelEventHandler m_oFloatingMenuMenuShowingHandler;
    private QMenuEventHandler m_oFloatingMenuMenuShowedHandler;
    private QMenuMouseEventHandler m_oFloatingMenuMenuItemMouseDownHandler;
    private QMenuMouseEventHandler m_oFloatingMenuMenuItemMouseUpHandler;
    private QPaintMenuItemEventHandler m_oFloatingMenuPaintMenuItemHandler;
    private QWeakDelegate m_oMenuItemMouseDownDelegate;
    private QWeakDelegate m_oMenuItemMouseUpDelegate;
    private QWeakDelegate m_oMenuItemSelectedDelegate;
    private QWeakDelegate m_oMenuItemActivatingDelegate;
    private QWeakDelegate m_oMenuItemActivatedDelegate;
    private QWeakDelegate m_oMenuShowingDelegate;
    private QWeakDelegate m_oMenuShowedDelegate;
    private QWeakDelegate m_oPaintMenuItemDelegate;

    public QContextMenu(IContainer container)
    {
      if (container == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (container)));
      container.Add((IComponent) this);
      this.InternalConstruct();
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event QMenuMouseEventHandler MenuItemMouseDown
    {
      add => this.m_oMenuItemMouseDownDelegate = QWeakDelegate.Combine(this.m_oMenuItemMouseDownDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemMouseDownDelegate = QWeakDelegate.Remove(this.m_oMenuItemMouseDownDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event QMenuMouseEventHandler MenuItemMouseUp
    {
      add => this.m_oMenuItemMouseUpDelegate = QWeakDelegate.Combine(this.m_oMenuItemMouseUpDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemMouseUpDelegate = QWeakDelegate.Remove(this.m_oMenuItemMouseUpDelegate, (Delegate) value);
    }

    [Description("Gets raised when a user selects a menuItem via the mouse or the Keyboard")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QMenuEventHandler MenuItemSelected
    {
      add => this.m_oMenuItemSelectedDelegate = QWeakDelegate.Combine(this.m_oMenuItemSelectedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemSelectedDelegate = QWeakDelegate.Remove(this.m_oMenuItemSelectedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when a user activates a MenuItem by clicking it, using a ShortCut, a HotKey or navigating to it with the keyboard. This event can be canceled.")]
    [QWeakEvent]
    public event QMenuCancelEventHandler MenuItemActivating
    {
      add => this.m_oMenuItemActivatingDelegate = QWeakDelegate.Combine(this.m_oMenuItemActivatingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemActivatingDelegate = QWeakDelegate.Remove(this.m_oMenuItemActivatingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a user activates a menuItem by clicking it, using a ShortCut, a HotKey or navigating to it with the keyboard.")]
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

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a MenuItem must be painted")]
    public event QPaintMenuItemEventHandler PaintMenuItem
    {
      add => this.m_oPaintMenuItemDelegate = QWeakDelegate.Combine(this.m_oPaintMenuItemDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oPaintMenuItemDelegate = QWeakDelegate.Remove(this.m_oPaintMenuItemDelegate, (Delegate) value);
    }

    public QContextMenu() => this.InternalConstruct();

    private void InternalConstruct()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_aListeners = new ArrayList();
      this.m_aComponentListeners = new ArrayList();
      this.m_oComponents = new Container();
      this.Configuration = new QFloatingMenuConfiguration();
      this.ToolTipConfiguration = new QToolTipConfiguration();
      this.m_oFloatingMenuMenuItemActivatingHandler = new QMenuCancelEventHandler(this.FloatingMenu_MenuItemActivating);
      this.m_oFloatingMenuMenuItemSelectedHandler = new QMenuEventHandler(this.FloatingMenu_MenuItemSelected);
      this.m_oFloatingMenuMenuItemActivatedHandler = new QMenuEventHandler(this.FloatingMenu_MenuItemActivated);
      this.m_oFloatingMenuMenuShowingHandler = new QMenuCancelEventHandler(this.FloatingMenu_MenuShowing);
      this.m_oFloatingMenuMenuShowedHandler = new QMenuEventHandler(this.FloatingMenu_MenuShowed);
      this.m_oFloatingMenuPaintMenuItemHandler = new QPaintMenuItemEventHandler(this.FloatingMenu_PaintMenuItem);
      this.m_oFloatingMenuMenuItemMouseUpHandler = new QMenuMouseEventHandler(this.FloatingMenu_MenuItemMouseUp);
      this.m_oFloatingMenuMenuItemMouseDownHandler = new QMenuMouseEventHandler(this.FloatingMenu_MenuItemMouseDown);
      this.m_oWeakMessageFilter = new QWeakMessageFilter((object) this);
      Application.AddMessageFilter((IMessageFilter) this.m_oWeakMessageFilter);
    }

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    [Category("QAppearance")]
    [Description("Gets or sets the configuration for menu.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QFloatingMenuConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= new EventHandler(this.Configuration_ConfigurationChanged);
        this.m_oConfiguration = value;
        if (this.m_oConfiguration == null)
          return;
        this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      }
    }

    public void ResetToolTipConfiguration() => this.ToolTipConfiguration.SetToDefaultValues();

    public bool ShouldSerializeToolTipConfiguration() => !this.ToolTipConfiguration.IsSetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the QToolTipConfiguration for the QControl.")]
    [Category("QAppearance")]
    public QToolTipConfiguration ToolTipConfiguration
    {
      get => this.m_oToolTipConfiguration;
      set
      {
        if (this.m_oToolTipConfiguration != null)
          this.m_oToolTipConfiguration.ConfigurationChanged -= new EventHandler(this.ToolTipConfiguration_ConfigurationChanged);
        this.m_oToolTipConfiguration = value;
        if (this.m_oToolTipConfiguration == null)
          return;
        this.m_oToolTipConfiguration.ConfigurationChanged += new EventHandler(this.ToolTipConfiguration_ConfigurationChanged);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QMenuItemCollectionEditor), typeof (UITypeEditor))]
    [Description("Contains the collection of MenuItems of this QContextMenu")]
    [Category("QBehavior")]
    public QMenuItemCollection MenuItems
    {
      get
      {
        if (this.m_oMenuItems == null)
          this.m_oMenuItems = (QCommandCollection) new QMenuItemCollection();
        return (QMenuItemCollection) this.m_oMenuItems;
      }
    }

    [Obsolete("Obsolete since 1.2.0.20. Use the ShortcutScope property")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("Obsolete since 1.2.0.20. Use the ShortcutScope property")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool GlobalShortcutKeys
    {
      get => this.m_eShortcutScope == QContextMenuShortcutScope.Global;
      set
      {
        if (value)
          this.ShortcutScope = QContextMenuShortcutScope.Global;
        else
          this.ShortcutScope = QContextMenuShortcutScope.AttachedControls;
      }
    }

    [Category("QBehavior")]
    [DefaultValue(QContextMenuShortcutScope.AttachedControls)]
    [Description("Gets or sets the ShortcutScope. This indicates when shorcuts should react. When setting this value to Global, be carefull of the Shortcuts you chose. They get handled even when an other application has an implementation for this shortcut.")]
    public QContextMenuShortcutScope ShortcutScope
    {
      get => this.m_eShortcutScope;
      set
      {
        if (!this.DesignMode && this.m_eShortcutScope == QContextMenuShortcutScope.Global)
          this.UninstallKeyboardMessageHook();
        this.m_eShortcutScope = value;
        if (this.DesignMode || this.m_eShortcutScope != QContextMenuShortcutScope.Global)
          return;
        this.InstallKeyboardMessageHook();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWin32Window OwnerWindow
    {
      get => this.m_oOwnerWindow;
      set
      {
        this.m_oOwnerWindow = value;
        if (this.m_oFloatingMenu == null)
          return;
        this.m_oFloatingMenu.OwnerWindow = value;
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the OwnerForm. All the childMenus will be owned by the OwnerForm.")]
    [Category("QBehavior")]
    public Form OwnerForm
    {
      get => this.OwnerWindow as Form;
      set => this.OwnerWindow = (IWin32Window) value;
    }

    [Description("Gets or sets whether the default Windows ContextMenu must be suppressed")]
    [Category("QBehavior")]
    [DefaultValue(true)]
    public bool SuppressDefaultContextMenu
    {
      get => this.m_bSuppressDefaultContextMenu;
      set => this.m_bSuppressDefaultContextMenu = value;
    }

    private QContextMenuControlListener GetListenerForControl(
      Control control)
    {
      for (int index = 0; index < this.m_aListeners.Count; ++index)
      {
        QContextMenuControlListener aListener = (QContextMenuControlListener) this.m_aListeners[index];
        if (aListener.Control == control)
          return aListener;
      }
      return (QContextMenuControlListener) null;
    }

    public bool HasListenerForControl(Control control) => this.GetListenerForControl(control) != null;

    public void AddListener(Control control)
    {
      if (!this.HasListenerForControl(control))
        this.m_aListeners.Add((object) new QContextMenuControlListener(this, control, this.DesignMode));
      QContextMenuDesigner.GetExtenderProviderOnSite(this.Site, true)?.SetQContextMenu((Component) control, this);
    }

    public void RemoveListener(Control control)
    {
      QContextMenuControlListener listenerForControl = this.GetListenerForControl(control);
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
      QContextMenuDesigner.GetExtenderProviderOnSite(this.Site, true)?.SetQContextMenu((Component) notifyIcon, this);
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
        Control control = ((QContextMenuControlListener) this.m_aListeners[index]).Control;
        if (control.IsDisposed)
          this.RemoveListener(control);
        else if (control.Handle == handle)
          return control;
      }
      return (Control) null;
    }

    private void SecureFloatingMenu()
    {
      if (this.m_oFloatingMenu != null)
        return;
      this.m_oFloatingMenu = new QFloatingMenu((QCommand) null, this.MenuItems);
      this.m_oFloatingMenu.OwnerWindow = this.m_oOwnerWindow;
      this.m_oFloatingMenu.MenuItemSelected += this.m_oFloatingMenuMenuItemSelectedHandler;
      this.m_oFloatingMenu.MenuItemActivating += this.m_oFloatingMenuMenuItemActivatingHandler;
      this.m_oFloatingMenu.MenuItemActivated += this.m_oFloatingMenuMenuItemActivatedHandler;
      this.m_oFloatingMenu.MenuShowing += this.m_oFloatingMenuMenuShowingHandler;
      this.m_oFloatingMenu.MenuShowed += this.m_oFloatingMenuMenuShowedHandler;
      this.m_oFloatingMenu.PaintMenuItem += this.m_oFloatingMenuPaintMenuItemHandler;
      this.m_oFloatingMenu.MenuItemMouseDown += this.m_oFloatingMenuMenuItemMouseDownHandler;
      this.m_oFloatingMenu.MenuItemMouseUp += this.m_oFloatingMenuMenuItemMouseUpHandler;
    }

    [Browsable(false)]
    internal QFloatingMenu FloatingMenu
    {
      get
      {
        this.SecureFloatingMenu();
        return this.m_oFloatingMenu;
      }
    }

    public void CreateFloatingMenu() => this.SecureFloatingMenu();

    [Browsable(false)]
    public bool FloatingMenuCreated => this.m_oFloatingMenu != null && !this.m_oFloatingMenu.IsDisposed;

    public bool FloatingMenuVisible => this.m_oFloatingMenu != null && this.m_oFloatingMenu.Visible;

    private QMenuItem GetAccessibleMenuItemWithShortcut(Keys shortcut)
    {
      QMenuItem itemWithShortcut = this.MenuItems.GetMenuItemWithShortcut(shortcut);
      return itemWithShortcut != null && itemWithShortcut.IsAccessible ? itemWithShortcut : (QMenuItem) null;
    }

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public bool HandleKeyDown(Keys keys) => this.HandleKeyDown(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    public bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      QMenuItem itemWithShortcut = this.GetAccessibleMenuItemWithShortcut(Control.ModifierKeys | keys);
      if (itemWithShortcut != null)
      {
        if (this.m_eShortcutScope == QContextMenuShortcutScope.Global || this.m_eShortcutScope != QContextMenuShortcutScope.Application && !this.HasListenerForControl(destinationControl))
          return false;
        this.FloatingMenu.ActivateMenuItem(itemWithShortcut, false, true, QMenuItemActivationType.Shortcut);
        return itemWithShortcut.SuppressShortcutToSystem;
      }
      if (!this.FloatingMenuVisible)
        return false;
      if (this.FloatingMenu.HandleKeyDown(keys, destinationControl, message))
        return true;
      if (keys == Keys.Escape)
      {
        this.FloatingMenu.HideMenu();
        return true;
      }
      if (keys != Keys.Menu)
        return true;
      this.FloatingMenu.HideMenu();
      return false;
    }

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public bool HandleKeyUp(Keys keys) => this.HandleKeyUp(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    public bool HandleKeyUp(Keys keys, Control destinationControl, Message message) => this.FloatingMenu.HandleKeyUp(keys, destinationControl, message);

    public void ShowMenu(Point location) => this.ShowMenu(location, (IWin32Window) null);

    public void ShowMenuOnNotifyIcon(NotifyIcon notifyIcon, Point location)
    {
      this.Configuration.TopMost = true;
      NativeMethods.SetForegroundWindow(QMisc.GetNotifyIconWindow(notifyIcon).Handle);
      this.ShowMenu(location, (IWin32Window) null);
    }

    public void ShowMenu(Point location, IWin32Window owner) => this.ShowMenu(new Rectangle(location, Size.Empty), location, QRelativePositions.Above | QRelativePositions.Left, QCommandDirections.Down | QCommandDirections.Right, owner);

    public void ShowMenu(
      Rectangle openingItemBounds,
      Point menuLocation,
      QRelativePositions openingItemRelativePosition,
      QCommandDirections animationDirection,
      IWin32Window owner)
    {
      QMenuCancelEventArgs e = new QMenuCancelEventArgs((QMenuItem) null, QMenuItemActivationType.None, false, true);
      this.OnMenuShowing(e);
      if (e.Cancel || this.MenuItems.Count == 0)
        return;
      this.FloatingMenu.OwnerWindow = owner;
      this.FloatingMenu.Configuration = this.Configuration;
      this.FloatingMenu.ColorScheme = this.ColorScheme;
      this.FloatingMenu.Font = this.Font;
      this.FloatingMenu.ToolTipConfiguration = this.ToolTipConfiguration;
      Rectangle openingItemBounds1 = openingItemBounds;
      Size requestedSize = this.FloatingMenu.CalculateRequestedSize();
      QMenuCalculateBoundsResult menuBounds = QMenu.CalculateMenuBounds(new Rectangle(menuLocation, requestedSize), openingItemBounds1, openingItemRelativePosition, animationDirection, QMenuCalculateBoundsOptions.None);
      this.FloatingMenu.ShowMenu(menuBounds.Bounds, openingItemBounds1, menuBounds.OpeningItemPosition, menuBounds.AnimateDirection);
      this.OnMenuShowed(new QMenuEventArgs((QMenuItem) null, QMenuItemActivationType.None, true));
    }

    public void HideMenu()
    {
      if (!this.FloatingMenuVisible)
        return;
      this.m_oFloatingMenu.HideMenu();
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public bool PreFilterMessage(ref Message m)
    {
      switch (m.Msg)
      {
        case 161:
        case 164:
        case 167:
        case 171:
        case 513:
        case 516:
        case 519:
        case 523:
          if (this.FloatingMenuVisible && !this.FloatingMenu.ContainsOrIsContainerWithHandle(m.HWnd))
          {
            this.FloatingMenu.HideMenu();
            break;
          }
          break;
        case 256:
        case 260:
          if (this.FloatingMenuCreated && this.FloatingMenu.ContainsOrIsContainerWithHandle(m.HWnd))
            return false;
          Control controlFromHandle1 = QControlHelper.GetFirstControlFromHandle(m.HWnd);
          return this.HandleKeyDown((Keys) (int) m.WParam, controlFromHandle1, m);
        case 257:
        case 261:
          if (this.FloatingMenuCreated && this.FloatingMenu.ContainsOrIsContainerWithHandle(m.HWnd))
            return false;
          Control controlFromHandle2 = QControlHelper.GetFirstControlFromHandle(m.HWnd);
          return this.HandleKeyUp((Keys) (int) m.WParam, controlFromHandle2, m);
      }
      return false;
    }

    private void InstallKeyboardMessageHook()
    {
      if (!(this.m_hKeyboardMessageHookHandle == IntPtr.Zero))
        return;
      if (this.m_oKeyboardMessageHookProcHandler == null)
        this.m_oKeyboardMessageHookProcHandler = new NativeMethods.HookProc(this.KeyboardMessageHookProc);
      this.m_hKeyboardMessageHookHandle = NativeMethods.SetWindowsHookEx(13, this.m_oKeyboardMessageHookProcHandler, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
    }

    private void UninstallKeyboardMessageHook()
    {
      if (!(this.m_hKeyboardMessageHookHandle != IntPtr.Zero))
        return;
      NativeMethods.UnhookWindowsHookEx(this.m_hKeyboardMessageHookHandle);
      this.m_hKeyboardMessageHookHandle = IntPtr.Zero;
    }

    private IntPtr KeyboardMessageHookProc(int nCode, IntPtr wParam, IntPtr lParam)
    {
      if (nCode == 0)
      {
        NativeMethods.KBDLLHOOKSTRUCT structure = (NativeMethods.KBDLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof (NativeMethods.KBDLLHOOKSTRUCT));
        switch ((int) wParam)
        {
          case 256:
          case 260:
            QMenuItem itemWithShortcut = this.GetAccessibleMenuItemWithShortcut(Control.ModifierKeys | (Keys) structure.vkCode);
            if (itemWithShortcut != null)
            {
              this.FloatingMenu.ActivateMenuItem(itemWithShortcut, false, true, QMenuItemActivationType.Shortcut);
              break;
            }
            break;
        }
      }
      return NativeMethods.CallNextHookEx(this.m_hKeyboardMessageHookHandle, nCode, wParam, lParam);
    }

    protected virtual void OnMenuItemActivating(QMenuCancelEventArgs e) => this.m_oMenuItemActivatingDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemActivatingDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemSelected(QMenuEventArgs e) => this.m_oMenuItemSelectedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemSelectedDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemActivated(QMenuEventArgs e) => this.m_oMenuItemActivatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemActivatedDelegate, (object) this, (object) e);

    protected virtual void OnMenuShowing(QMenuCancelEventArgs e) => this.m_oMenuShowingDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuShowingDelegate, (object) this, (object) e);

    protected virtual void OnMenuShowed(QMenuEventArgs e) => this.m_oMenuShowedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuShowedDelegate, (object) this, (object) e);

    protected virtual void OnPaintMenuItem(QPaintMenuItemEventArgs e) => this.m_oPaintMenuItemDelegate = QWeakDelegate.InvokeDelegate(this.m_oPaintMenuItemDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemMouseDown(QMenuMouseEventArgs e) => this.m_oMenuItemMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemMouseUp(QMenuMouseEventArgs e) => this.m_oMenuItemMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemMouseUpDelegate, (object) this, (object) e);

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing)
          Application.RemoveMessageFilter((IMessageFilter) this.m_oWeakMessageFilter);
      }
      catch (InvalidOperationException ex)
      {
      }
      this.UninstallKeyboardMessageHook();
      if (!disposing)
        return;
      if (this.m_oComponents != null)
      {
        this.m_oComponents.Dispose();
        this.m_oComponents = (Container) null;
      }
      if (this.m_oFloatingMenu == null)
        return;
      this.m_oFloatingMenu.Dispose();
      this.m_oFloatingMenu = (QFloatingMenu) null;
    }

    private void FloatingMenu_MenuItemMouseUp(object sender, QMenuMouseEventArgs e) => this.OnMenuItemMouseUp(e);

    private void FloatingMenu_MenuItemMouseDown(object sender, QMenuMouseEventArgs e) => this.OnMenuItemMouseDown(e);

    private void FloatingMenu_MenuItemActivating(object sender, QMenuCancelEventArgs e) => this.OnMenuItemActivating(e);

    private void FloatingMenu_MenuItemSelected(object sender, QMenuEventArgs e) => this.OnMenuItemSelected(e);

    private void FloatingMenu_MenuItemActivated(object sender, QMenuEventArgs e) => this.OnMenuItemActivated(e);

    private void FloatingMenu_MenuShowing(object sender, QMenuCancelEventArgs e) => this.OnMenuShowing(e);

    private void FloatingMenu_MenuShowed(object sender, QMenuEventArgs e) => this.OnMenuShowed(e);

    private void FloatingMenu_PaintMenuItem(object sender, QPaintMenuItemEventArgs e) => this.OnPaintMenuItem(e);

    private void Control_Disposed(object sender, EventArgs e) => this.RemoveListener((Control) sender);

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      if (this.m_oConfiguration == null || this.m_oToolTipConfiguration == null || this.m_oConfiguration.ShowToolTips == this.m_oToolTipConfiguration.Enabled)
        return;
      this.m_oToolTipConfiguration.Enabled = this.m_oConfiguration.ShowToolTips;
    }

    private void ToolTipConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
      if (this.m_oConfiguration == null || this.m_oToolTipConfiguration == null || this.m_oConfiguration.ShowToolTips == this.m_oToolTipConfiguration.Enabled)
        return;
      this.m_oConfiguration.ShowToolTips = this.m_oToolTipConfiguration.Enabled;
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
  }
}
