// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbon
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonDesigner), typeof (IDesigner))]
  [ToolboxItem(true)]
  [DefaultEvent("ItemActivated")]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QRibbon.bmp")]
  public class QRibbon : 
    QTabControl,
    IQMouseHookClient,
    IQMainMenu,
    IQHotkeyHandlerHost,
    IQNavigationHost,
    IQCompositeItemEventRaiser,
    IQCompositeEventRaiser,
    IQKeyboardMessageFilter,
    IQCompositeItemEventPublisher
  {
    private QKeyboardMessageFilter m_oKeyboardMessageFilter;
    private QNavigationHostCollection m_oNavigationHosts;
    private QRibbon.QRibbonPageContentNavigationHost m_oPageContentNavigationHost;
    private IQNavigationHost m_oSelectedNavigationHost;
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QShortcutScope m_eShortcutScope;
    private QRibbonPageCompositeConfiguration m_oDefaultPageConfiguration;
    private QColorScheme m_oChildCompositeColorScheme;
    private QCompositeConfiguration m_oChildCompositeConfiguration;
    private QCompositeWindowConfiguration m_oChildWindowConfiguration;
    private QMainFormOverride m_oMainFormOverride;
    private QHotkeyHandler m_oHotkeyHandler;
    private QMouseHooker m_oMouseHooker;
    private bool m_bHasSimulatedFocus;
    private QRibbonCaption m_oRibbonCaption;
    private QRibbonLaunchBarHost m_oLaunchBarHost;
    private QCompositeItem m_oExpandedItem;
    private IQNavigationItem m_oSelectedNavigationItem;
    private EventHandler m_oChildObjectConfigurationChangedEventHandler;
    private QWeakDelegate m_oCompositeKeyPress;
    private QWeakDelegate m_oSelectedItemChanged;
    private QWeakDelegate m_oItemPaintStageDelegate;
    private QWeakDelegate m_oItemActivating;
    private QWeakDelegate m_oItemActivated;
    private QWeakDelegate m_oItemSelected;
    private QWeakDelegate m_oItemExpanded;
    private QWeakDelegate m_oItemExpanding;
    private QWeakDelegate m_oItemCollapsed;
    private QWeakDelegate m_oItemCollapsing;
    private QWeakDelegate m_oHelpButtonActivated;

    public QRibbon()
      : base(true, false, false, false)
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.Selectable, false);
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oKeyboardMessageFilter = new QKeyboardMessageFilter((IQKeyboardMessageFilter) this);
      Application.AddMessageFilter((IMessageFilter) this.m_oKeyboardMessageFilter);
      this.m_oMainFormOverride = new QMainFormOverride((IQMainMenu) this);
      this.m_oHotkeyHandler = new QHotkeyHandler((IQHotkeyHandlerHost) this);
      this.TabStrip.NavigationArea.ButtonStateChanged += new QButtonAreaEventHandler(this.NavigationArea_ButtonStateChanged);
      this.m_oPageContentNavigationHost = new QRibbon.QRibbonPageContentNavigationHost(this);
      this.m_oNavigationHosts = new QNavigationHostCollection();
      this.m_oNavigationHosts.Add((IQNavigationHost) this.m_oPageContentNavigationHost);
      this.m_oNavigationHosts.Add((IQNavigationHost) this);
      this.m_oChildObjectConfigurationChangedEventHandler = new EventHandler(this.ChildObject_ConfigurationChanged);
      this.m_oDefaultPageConfiguration = this.CreatePageConfiguration();
      this.m_oDefaultPageConfiguration.ConfigurationChanged += this.m_oChildObjectConfigurationChangedEventHandler;
      this.m_oChildWindowConfiguration = this.CreateChildWindowConfiguration();
      this.m_oChildWindowConfiguration.ConfigurationChanged += this.m_oChildObjectConfigurationChangedEventHandler;
      this.m_oChildCompositeConfiguration = this.CreateChildCompositeConfiguration();
      this.m_oChildCompositeConfiguration.ConfigurationChanged += this.m_oChildObjectConfigurationChangedEventHandler;
      this.m_oChildCompositeColorScheme = new QColorScheme(false);
      this.ResumeLayout(false);
    }

    protected virtual QRibbonPageCompositeConfiguration CreatePageConfiguration() => new QRibbonPageCompositeConfiguration();

    protected virtual QCompositeWindowConfiguration CreateChildWindowConfiguration() => new QCompositeWindowConfiguration();

    protected virtual QCompositeConfiguration CreateChildCompositeConfiguration() => (QCompositeConfiguration) new QCompositeMenuConfiguration();

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QCompositeToolTipConfiguration();

    protected override QBalloon CreateBalloon() => (QBalloon) new QCompositeBalloon();

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonPage)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Ribbon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonPanel)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Description("Contains the Configuration for the QRibbon")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonConfiguration Configuration => base.Configuration as QRibbonConfiguration;

    public bool ShouldSerializeChildCompositeColorScheme() => this.ChildCompositeColorScheme.ShouldSerialize();

    public void ResetChildCompositeColorScheme() => this.ChildCompositeColorScheme.Reset();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used for child composites")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    public virtual QColorScheme ChildCompositeColorScheme => this.m_oChildCompositeColorScheme;

    public bool ShouldSerializeChildCompositeConfiguration() => !this.m_oChildCompositeConfiguration.IsSetToDefaultValues();

    public void ResetChildCompositeConfiguration() => this.m_oChildCompositeConfiguration.SetToDefaultValues();

    [Category("QAppearance")]
    [Description("Contains the ChildCompositeConfiguration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeConfiguration ChildCompositeConfiguration => this.m_oChildCompositeConfiguration;

    public bool ShouldSerializeChildWindowConfiguration() => !this.ChildWindowConfiguration.IsSetToDefaultValues();

    public void ResetChildWindowConfiguration() => this.ChildWindowConfiguration.SetToDefaultValues();

    [Description("Contains the ChildWindowConfiguration for the QComposite.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeWindowConfiguration ChildWindowConfiguration => this.m_oChildWindowConfiguration;

    public bool ShouldSerializeDefaultPageConfiguration() => !this.m_oDefaultPageConfiguration.IsSetToDefaultValues();

    public void ResetDefaultPageConfiguration() => this.m_oDefaultPageConfiguration.SetToDefaultValues();

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QRibbonPage.")]
    public QRibbonPageCompositeConfiguration DefaultPageConfiguration => this.m_oDefaultPageConfiguration;

    [Description("Gets or sets the scope of the shortcuts when HandleShortcutKeys is true")]
    [Category("QBehavior")]
    [DefaultValue(QShortcutScope.ParentForm)]
    public QShortcutScope ShortcutScope
    {
      get => this.m_eShortcutScope;
      set => this.m_eShortcutScope = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool FocusTabButtons
    {
      get => false;
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool WrapTabAround
    {
      get => false;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool WrapTabButtonNavigationAround
    {
      get => false;
      set
      {
      }
    }

    [Category("QBehavior")]
    [DefaultValue(null)]
    [Description("Gets or sets a possible RibbonCation. When this is set the RibbonCation is included in the hotkey handling. If you want to add additional Controls that should be included in the hotkey and navigation key handling use AddAdditionalNavigationControl.")]
    public QRibbonCaption Caption
    {
      get => this.m_oRibbonCaption;
      set
      {
        if (this.m_oRibbonCaption == value)
          return;
        if (this.m_oRibbonCaption != null)
        {
          this.AttachNavigationHostEventHandlers((QCompositeControl) this.m_oRibbonCaption);
          this.m_oNavigationHosts.Remove((IQNavigationHost) this.m_oRibbonCaption);
        }
        this.m_oRibbonCaption = value;
        if (this.m_oRibbonCaption == null)
          return;
        this.AttachNavigationHostEventHandlers((QCompositeControl) this.m_oRibbonCaption);
        this.m_oNavigationHosts.Add((IQNavigationHost) this.m_oRibbonCaption);
      }
    }

    [DefaultValue(null)]
    [Category("QBehavior")]
    [Description("Gets or sets a possible LaunchBarHost. When this is set the LaunchBarHost is included in the hotkey handling. If you want to add additional Controls that should be included in the hotkey and navigation key handling use AddAdditionalNavigationControl.")]
    public QRibbonLaunchBarHost LaunchBarHost
    {
      get => this.m_oLaunchBarHost;
      set
      {
        if (this.m_oLaunchBarHost == value)
          return;
        if (this.m_oLaunchBarHost != null)
        {
          this.AttachNavigationHostEventHandlers((QCompositeControl) this.m_oLaunchBarHost);
          this.m_oNavigationHosts.Remove((IQNavigationHost) this.m_oLaunchBarHost);
        }
        this.m_oLaunchBarHost = value;
        if (this.m_oLaunchBarHost == null)
          return;
        this.AttachNavigationHostEventHandlers((QCompositeControl) this.m_oLaunchBarHost);
        this.m_oNavigationHosts.Add((IQNavigationHost) this.m_oLaunchBarHost);
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the Form that this QRibbon controls. It will override default MDI handling and the systemMenu of that form.")]
    [Category("QBehavior")]
    public Form Form
    {
      get => this.m_oMainFormOverride.Form;
      set => this.m_oMainFormOverride.Form = value;
    }

    [Browsable(false)]
    public bool MdiButtonsShouldBeVisible => this.m_oMainFormOverride != null && this.Form != null && this.Form.IsMdiContainer && this.Form.ActiveMdiChild != null && NativeHelper.GetCurrentFormState(this.Form.ActiveMdiChild) == FormWindowState.Maximized;

    [Browsable(false)]
    public QRibbonPage ActiveRibbonPage => this.ActiveTabPageRuntime as QRibbonPage;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QRibbonPageComposite ActiveRibbonPageComposite => this.ActiveRibbonPage == null ? (QRibbonPageComposite) null : this.ActiveRibbonPage.RibbonPageComposite;

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QComposite is about to handle navigation keys that are pressed")]
    public event QCompositeKeyboardCancelEventHandler CompositeKeyPress
    {
      add => this.m_oCompositeKeyPress = QWeakDelegate.Combine(this.m_oCompositeKeyPress, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCompositeKeyPress = QWeakDelegate.Remove(this.m_oCompositeKeyPress, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the selected item is changed")]
    [QWeakEvent]
    public event QCompositeEventHandler SelectedItemChanged
    {
      add => this.m_oSelectedItemChanged = QWeakDelegate.Combine(this.m_oSelectedItemChanged, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oSelectedItemChanged = QWeakDelegate.Remove(this.m_oSelectedItemChanged, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItemBase is selected")]
    public event QCompositeEventHandler ItemSelected
    {
      add => this.m_oItemSelected = QWeakDelegate.Combine(this.m_oItemSelected, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemSelected = QWeakDelegate.Remove(this.m_oItemSelected, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the item is expanded")]
    [QWeakEvent]
    public event QCompositeExpandedEventHandler ItemExpanded
    {
      add => this.m_oItemExpanded = QWeakDelegate.Combine(this.m_oItemExpanded, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanded = QWeakDelegate.Remove(this.m_oItemExpanded, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the child container is expanding")]
    public event QCompositeExpandingCancelEventHandler ItemExpanding
    {
      add => this.m_oItemExpanding = QWeakDelegate.Combine(this.m_oItemExpanding, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanding = QWeakDelegate.Remove(this.m_oItemExpanding, (Delegate) value);
    }

    [Description("Gets raised when the item is collapsing")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QCompositeCancelEventHandler ItemCollapsing
    {
      add => this.m_oItemCollapsing = QWeakDelegate.Combine(this.m_oItemCollapsing, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemCollapsing = QWeakDelegate.Remove(this.m_oItemCollapsing, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the item is collapsed")]
    [Category("QEvents")]
    public event QCompositeEventHandler ItemCollapsed
    {
      add => this.m_oItemCollapsed = QWeakDelegate.Combine(this.m_oItemCollapsed, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemCollapsed = QWeakDelegate.Remove(this.m_oItemCollapsed, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when a layer of the QCompositeItem is painted")]
    [Category("QEvents")]
    public event QCompositePaintStageEventHandler PaintItem
    {
      add => this.m_oItemPaintStageDelegate = QWeakDelegate.Combine(this.m_oItemPaintStageDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemPaintStageDelegate = QWeakDelegate.Remove(this.m_oItemPaintStageDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the QCompositeItemBase is activating")]
    public event QCompositeCancelEventHandler ItemActivating
    {
      add => this.m_oItemActivating = QWeakDelegate.Combine(this.m_oItemActivating, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivating = QWeakDelegate.Remove(this.m_oItemActivating, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItemBase is activated")]
    public event QCompositeEventHandler ItemActivated
    {
      add => this.m_oItemActivated = QWeakDelegate.Combine(this.m_oItemActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivated = QWeakDelegate.Remove(this.m_oItemActivated, (Delegate) value);
    }

    [Description("Gets raised when the help button is activated")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler HelpButtonActivated
    {
      add => this.m_oHelpButtonActivated = QWeakDelegate.Combine(this.m_oHelpButtonActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oHelpButtonActivated = QWeakDelegate.Remove(this.m_oHelpButtonActivated, (Delegate) value);
    }

    [Browsable(false)]
    public QCompositeItem ExpandedItem => this.m_oExpandedItem;

    [Browsable(false)]
    public QCompositeItem SelectedItem => this.m_oSelectedNavigationItem as QCompositeItem;

    [Browsable(false)]
    public QComposite ExpandedComposite => this.m_oExpandedItem == null ? (QComposite) null : this.m_oExpandedItem.ChildComposite;

    internal IQNavigationItem SelectedNavigationItem => this.m_oSelectedNavigationItem;

    public void SelectItem(QCompositeItemBase item, QCompositeActivationType activationType) => this.SelectNavigationItem(item as IQNavigationItem, QCompositeHelper.GetAsNavigationActivationType(activationType));

    public void ActivateItem(
      QCompositeItemBase item,
      QCompositeItemActivationOptions options,
      QCompositeActivationType activationType)
    {
      item.Activate(options, activationType);
    }

    public void AddAdditionalNavigationControl(QCompositeControl control)
    {
      if (this.m_oNavigationHosts.Contains((IQNavigationHost) control))
        return;
      this.m_oNavigationHosts.Add((IQNavigationHost) control);
      this.AttachNavigationHostEventHandlers(control);
    }

    public void RemoveAdditionalNavigationControl(QCompositeControl control)
    {
      if (!this.m_oNavigationHosts.Contains((IQNavigationHost) control))
        return;
      this.m_oNavigationHosts.Remove((IQNavigationHost) control);
      this.RemoveNavigationHostEventHandlers(control);
      if (control == this.m_oRibbonCaption)
        this.m_oRibbonCaption = (QRibbonCaption) null;
      if (control != this.m_oLaunchBarHost)
        return;
      this.m_oLaunchBarHost = (QRibbonLaunchBarHost) null;
    }

    private void AttachNavigationHostEventHandlers(QCompositeControl control)
    {
      control.ItemExpanded += new QCompositeExpandedEventHandler(this.NavigationHost_ItemExpanded);
      control.ItemCollapsed += new QCompositeEventHandler(this.NavigationHost_ItemCollapsed);
      control.SelectedItemChanged += new QCompositeEventHandler(this.NavigationHost_SelectedItemChanged);
      control.ItemActivated += new QCompositeEventHandler(this.NavigationHost_ItemActivated);
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.NavigationHost_Disposed), (object) control, "Disposed"));
    }

    private void RemoveNavigationHostEventHandlers(QCompositeControl control)
    {
      control.ItemExpanded -= new QCompositeExpandedEventHandler(this.NavigationHost_ItemExpanded);
      control.ItemCollapsed -= new QCompositeEventHandler(this.NavigationHost_ItemCollapsed);
      control.SelectedItemChanged -= new QCompositeEventHandler(this.NavigationHost_SelectedItemChanged);
      control.ItemActivated -= new QCompositeEventHandler(this.NavigationHost_ItemActivated);
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.NavigationHost_Disposed));
    }

    internal void SelectNavigationItem(
      IQNavigationItem item,
      QNavigationActivationType activationType)
    {
      item?.Select(true, QNavigationSelectionReason.OneItemOnly, activationType);
    }

    private void SetShowHotkeyWindowsOfChilds(bool value)
    {
      if (this.ActiveRibbonPage != null)
        this.ActiveRibbonPage.Composite.ShowHotkeyWindows = value;
      for (int index = 0; index < this.m_oNavigationHosts.Count; ++index)
      {
        if (this.m_oNavigationHosts[index] is IQCompositeContainer oNavigationHost)
          oNavigationHost.Composite.ShowHotkeyWindows = value;
      }
    }

    private bool ShouldShowHotkeyWindows() => this.Configuration.HotkeyWindowShowBehavior == QHotkeyWindowShowBehavior.Automatic;

    public void SimulateFocus() => this.SimulateFocus(this.ShouldShowHotkeyWindows());

    internal void SimulateFocus(bool shouldShowHotkeyWindows)
    {
      if (this.m_bHasSimulatedFocus)
        return;
      this.SetShowHotkeyWindowsOfChilds(shouldShowHotkeyWindows);
      this.m_oHotkeyHandler.ShouldShowHotkeyWindows = shouldShowHotkeyWindows;
      this.m_oHotkeyHandler.StartProcessing();
      Qios.DevSuite.Components.NativeMethods.HideCaret(IntPtr.Zero);
      this.m_bHasSimulatedFocus = true;
      this.MouseHooker.MouseHooked = true;
    }

    public void LoseSimulatedFocus()
    {
      if (!this.m_bHasSimulatedFocus)
        return;
      this.m_bHasSimulatedFocus = false;
      this.SetShowHotkeyWindowsOfChilds(false);
      if (this.ActiveRibbonPageComposite != null)
        this.ActiveRibbonPageComposite.ClearCurrentFocusedChildControl();
      if (!this.m_oHotkeyHandler.IsIdle)
        this.m_oHotkeyHandler.StopProcessing();
      if (this.SelectedNavigationItem != null)
        this.SelectedNavigationItem.Select(false, QNavigationSelectionReason.None, QNavigationActivationType.None);
      Qios.DevSuite.Components.NativeMethods.ShowCaret(IntPtr.Zero);
      this.MouseHooker.MouseHooked = false;
    }

    public bool ContainsControl(Control control)
    {
      if (control == null)
        return false;
      if (QCompositeHelper.IsOrContainsControl((IQCompositeContainer) this.ActiveRibbonPage, control))
        return true;
      for (int index = 0; index < this.m_oNavigationHosts.Count; ++index)
      {
        if (QCompositeHelper.IsOrContainsControl(this.m_oNavigationHosts[index] as IQCompositeContainer, control))
          return true;
      }
      return false;
    }

    private void SelectNextItem(bool forward, bool loop)
    {
      if (this.m_oSelectedNavigationHost != null)
        this.m_oSelectedNavigationHost.SelectNextItem(forward, loop);
      else
        ((IQNavigationHost) this.m_oPageContentNavigationHost).SelectFirstOrCurrentItem(true);
    }

    private void SelectNextNavigationHost(bool forward, bool loop)
    {
      IQNavigationHost accessibleNavigationHost = this.m_oNavigationHosts.GetNextAccessibleNavigationHost(this.m_oSelectedNavigationHost, forward, loop);
      if (accessibleNavigationHost == null)
        return;
      this.m_oSelectedNavigationHost = accessibleNavigationHost;
      accessibleNavigationHost.SelectFirstOrCurrentItem(true);
    }

    private bool IsMenuKey(Keys key) => key == Keys.Menu;

    private bool IsAltWithHotkey(Keys key) => (key & Keys.Alt) == Keys.Alt && this.IsHotkey(key & ~Keys.Alt);

    private bool IsShortcutKey(Keys key) => Enum.IsDefined(typeof (Shortcut), (object) (int) key);

    private bool IsNavigationKey(Keys key) => key == Keys.Left || key == Keys.Right || key == Keys.Up || key == Keys.Down;

    private bool IsDeactivationKey(Keys key) => key == Keys.Escape;

    private bool IsActivationKey(Keys key) => key == Keys.Return;

    private bool IsHotkey(Keys keys) => QHotkeyHelper.ConvertToChar(keys) != char.MinValue;

    private bool HandleShortcutKey(Keys key, out bool suppressToSystem)
    {
      suppressToSystem = false;
      for (int index = 0; index < this.m_oNavigationHosts.Count; ++index)
      {
        if (this.m_oNavigationHosts[index].HandleShortcutKey(key, out suppressToSystem))
          return true;
      }
      return false;
    }

    private void HandleNavigationKey(Keys key)
    {
      if (this.m_oHotkeyHandler.IsProcessing)
      {
        this.m_oHotkeyHandler.StopProcessing(false);
        this.SetShowHotkeyWindowsOfChilds(false);
      }
      switch (key)
      {
        case Keys.Left:
          this.SelectNextItem(false, true);
          break;
        case Keys.Up:
          this.SelectNextNavigationHost(false, false);
          break;
        case Keys.Right:
          this.SelectNextItem(true, true);
          break;
        case Keys.Down:
          this.SelectNextNavigationHost(true, false);
          break;
      }
    }

    private void HandleDeactivationKey(Keys key)
    {
      if (this.m_oHotkeyHandler.IsProcessing)
      {
        this.m_oHotkeyHandler.HandleDeactivationKey();
        if (!this.m_oHotkeyHandler.IsIdle)
          return;
        this.LoseSimulatedFocus();
      }
      else
        this.LoseSimulatedFocus();
    }

    private void HandleActivationKey(Keys key)
    {
      if (this.m_oHotkeyHandler.IsProcessing)
      {
        this.m_oHotkeyHandler.HandleActivationKey();
      }
      else
      {
        if (this.SelectedNavigationItem == null)
          return;
        this.SelectedNavigationItem.Activate(true, QNavigationActivationReason.None, QNavigationActivationType.Keyboard);
      }
    }

    private bool HandleHotkey(Keys key) => this.m_oHotkeyHandler.HandleHotkey(key);

    protected virtual bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      QCompositeKeyboardCancelEventArgs e = new QCompositeKeyboardCancelEventArgs((QComposite) null, keys, false);
      ((IQCompositeEventRaiser) this).RaiseCompositeKeyPress(e);
      if (e.Cancel)
        return false;
      keys = e.Keys;
      if (this.ExpandedComposite != null)
        return this.ExpandedComposite.HandleKeyDown(keys, destinationControl, message, false);
      if (this.ActiveRibbonPageComposite != null && this.ActiveRibbonPageComposite.CurrentFocusedChildControl != null)
        return this.ActiveRibbonPageComposite.HandleKeyDown(keys, destinationControl, message, false);
      if (this.IsMenuKey(keys))
        return false;
      if (!this.HasSimulatedFocus)
      {
        bool suppressToSystem = false;
        if (this.IsShortcutKey(Control.ModifierKeys | keys) && QKeyboardFilterHelper.ShouldHandleShortcutsForControl((Control) this, destinationControl, this.ShortcutScope) && this.HandleShortcutKey(Control.ModifierKeys | keys, out suppressToSystem))
          return suppressToSystem;
        if (QKeyboardFilterHelper.ShouldHandleKeyMessagesForControl((Control) this, destinationControl) && this.IsAltWithHotkey(Control.ModifierKeys | keys))
        {
          this.SimulateFocus(false);
          if (this.HandleHotkey(keys))
          {
            if (this.m_oHotkeyHandler.IsProcessing)
            {
              this.SetShowHotkeyWindowsOfChilds(this.ShouldShowHotkeyWindows());
              this.m_oHotkeyHandler.ShouldShowHotkeyWindows = this.ShouldShowHotkeyWindows();
            }
            else if (this.ExpandedItem != null)
            {
              this.ExpandedItem.ChildComposite.ShowHotkeyWindows = true;
              this.ExpandedItem.ChildComposite.StartProcessingHotkeys(false);
              this.m_oHotkeyHandler.ShouldShowHotkeyWindows = this.ShouldShowHotkeyWindows();
            }
            return true;
          }
          this.LoseSimulatedFocus();
        }
      }
      else if (this.HasSimulatedFocus)
      {
        if (this.IsNavigationKey(keys))
          this.HandleNavigationKey(keys);
        else if (this.IsDeactivationKey(keys))
          this.HandleDeactivationKey(keys);
        else if (this.IsActivationKey(keys))
          this.HandleActivationKey(keys);
        else if (this.IsHotkey(keys))
          this.HandleHotkey(keys);
        return true;
      }
      return false;
    }

    protected virtual bool HandleKeyUp(Keys keys, Control destinationControl, Message message)
    {
      if (this.ExpandedComposite == null)
        return false;
      this.ExpandedComposite.HandleKeyUp(keys, destinationControl, message);
      return true;
    }

    [Browsable(false)]
    public bool HasSimulatedFocus => this.m_bHasSimulatedFocus;

    public override void EndInit()
    {
      base.EndInit();
      if (this.Form == null)
        this.Form = this.ParentForm;
      else
        this.m_oMainFormOverride.TryToAssignMdiClient();
    }

    internal QMouseHooker MouseHooker
    {
      get
      {
        if (this.m_oMouseHooker == null)
          this.m_oMouseHooker = new QMouseHooker((IQMouseHookClient) this);
        this.m_oMouseHooker.ExitOnMouseDown = true;
        return this.m_oMouseHooker;
      }
    }

    protected override System.Type AllowedDragDropTabButtonType => typeof (QRibbonTabButton);

    protected override QTabStrip CreateTabStrip(DockStyle dock)
    {
      QRibbonTabStrip tabStrip = new QRibbonTabStrip((IQTabStripHost) this, this.Font, dock);
      tabStrip.Painter = (QTabStripPainter) new QRibbonTabStripPainter();
      return (QTabStrip) tabStrip;
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QRibbonAppearance();

    protected override QTabControlConfiguration CreateTabControlConfiguration() => (QTabControlConfiguration) new QRibbonConfiguration();

    public bool ShouldSerializeTabStripConfiguration() => this.ShouldSerializeTabStripTopConfiguration();

    public void ResetTabStripConfiguration() => this.ResetTabStripTopConfiguration();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QRibbonTabStripConfiguration TabStripConfiguration => base.TabStripTopConfiguration as QRibbonTabStripConfiguration;

    [Browsable(false)]
    public QRibbonTabStrip TabStrip => base.TabStripTop as QRibbonTabStrip;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new QTabStripConfiguration TabStripTopConfiguration => base.TabStripTopConfiguration;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new QTabStrip TabStripTop => base.TabStripTop;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new QTabStripConfiguration TabStripLeftConfiguration => base.TabStripLeftConfiguration;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new QTabStrip TabStripLeft => base.TabStripLeft;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new QTabStripConfiguration TabStripBottomConfiguration => base.TabStripBottomConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new QTabStrip TabStripBottom => base.TabStripBottom;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new QTabStripConfiguration TabStripRightConfiguration => base.TabStripRightConfiguration;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new QTabStrip TabStripRight => base.TabStripRight;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the QAppearance.")]
    [Category("QAppearance")]
    public virtual QRibbonAppearance Appearance => base.Appearance as QRibbonAppearance;

    protected override string BackColorPropertyName => "RibbonBackground1";

    protected override string BackColor2PropertyName => "RibbonBackground2";

    protected override string BorderColorPropertyName => "RibbonBorder";

    protected override void UpdateTabStripPaintParams(QTabStripPaintParams paintParams)
    {
      paintParams.Border = (Color) this.ColorScheme.RibbonTabStripBorder;
      paintParams.Background1 = (Color) this.ColorScheme.RibbonTabStripBackground1;
      paintParams.Background2 = (Color) this.ColorScheme.RibbonTabStripBackground2;
      paintParams.DropIndicatorBackground = (Color) this.ColorScheme.RibbonDropIndicatorBackground;
      paintParams.DropIndicatorBorder = (Color) this.ColorScheme.RibbonDropIndicatorBorder;
      paintParams.NavigationButtonReplace = Color.FromArgb((int) byte.MaxValue, 0, 0);
      paintParams.NavigationButtonBackground1 = (Color) this.ColorScheme.RibbonTabButtonBackground1;
      paintParams.NavigationButtonBackground2 = (Color) this.ColorScheme.RibbonTabButtonBackground2;
      paintParams.NavigationButtonBorder = (Color) this.ColorScheme.RibbonTabButtonBorder;
      paintParams.NavigationButtonBackground1Hot = (Color) this.ColorScheme.RibbonTabStripNavigationButtonHot1;
      paintParams.NavigationButtonBackground2Hot = (Color) this.ColorScheme.RibbonTabStripNavigationButtonHot2;
      paintParams.NavigationButtonBorderHot = (Color) this.ColorScheme.RibbonTabStripNavigationButtonHotBorder;
      paintParams.NavigationButtonBackground1Active = (Color) this.ColorScheme.RibbonTabStripNavigationButtonActive1;
      paintParams.NavigationButtonBackground2Active = (Color) this.ColorScheme.RibbonTabStripNavigationButtonActive2;
      paintParams.NavigationButtonBorderActive = (Color) this.ColorScheme.RibbonTabStripNavigationButtonActiveBorder;
      paintParams.NavigationButtonReplaceWith = (Color) this.ColorScheme.RibbonTabButtonMask;
      paintParams.NavigationButtonReplaceWithHot = (Color) this.ColorScheme.RibbonTabButtonMask;
      paintParams.NavigationButtonReplaceWithActive = (Color) this.ColorScheme.RibbonTabButtonMask;
      paintParams.NavigationButtonReplaceWithDisabled = (Color) this.ColorScheme.RibbonTabButtonMaskDisabled;
      paintParams.NavigationAreaBackground1 = (Color) this.ColorScheme.RibbonTabStripNavigationAreaBackground1;
      paintParams.NavigationAreaBackground2 = (Color) this.ColorScheme.RibbonTabStripNavigationAreaBackground2;
      paintParams.NavigationAreaBorder = (Color) this.ColorScheme.RibbonTabStripNavigationAreaBorder;
    }

    protected override void UpdateTabControlPaintParams(QTabControlPaintParams paintParams)
    {
      paintParams.ContentBackground1 = (Color) this.ColorScheme.RibbonContentBackground1;
      paintParams.ContentBackground2 = (Color) this.ColorScheme.RibbonContentBackground2;
      paintParams.ContentBorder = (Color) this.ColorScheme.RibbonContentBorder;
      paintParams.ContentShade = (Color) this.ColorScheme.RibbonContentShade;
    }

    protected override void HandleConfigurationChanged() => base.HandleConfigurationChanged();

    private void SetBaseProperties(QRibbonPage page)
    {
      if (page == null)
        return;
      page.Configuration.Properties.SetBaseProperties(this.DefaultPageConfiguration.Properties, true, false);
      page.ChildCompositeConfiguration.Properties.SetBaseProperties(this.ChildCompositeConfiguration.Properties, true, false);
      page.ChildWindowConfiguration.Properties.SetBaseProperties(this.ChildWindowConfiguration.Properties, true, false);
      page.ChildCompositeColorScheme.SetBaseColorScheme((QColorSchemeBase) this.ChildCompositeColorScheme, false);
      page.ToolTipConfiguration.Properties.SetBaseProperties(this.ToolTipConfiguration.Properties, true, false);
    }

    private void ResetBaseProperties(QRibbonPage page)
    {
      if (page == null)
        return;
      if (page.Configuration.Properties.BaseProperties == this.DefaultPageConfiguration.Properties)
        page.Configuration.Properties.SetBaseProperties((QFastPropertyBag) null, true, false);
      if (page.ChildCompositeConfiguration.Properties.BaseProperties == this.ChildCompositeConfiguration.Properties.BaseProperties)
        page.ChildCompositeConfiguration.Properties.SetBaseProperties((QFastPropertyBag) null, true, false);
      if (page.ChildWindowConfiguration.Properties.BaseProperties == this.ChildWindowConfiguration.Properties)
        page.ChildWindowConfiguration.Properties.SetBaseProperties((QFastPropertyBag) null, true, false);
      if (page.ChildCompositeColorScheme.BaseColorScheme == this.ChildCompositeColorScheme)
        page.ChildCompositeColorScheme.SetBaseColorScheme((QColorSchemeBase) null, false);
      if (page.ToolTipConfiguration.Properties.BaseProperties != this.ToolTipConfiguration.Properties)
        return;
      page.ToolTipConfiguration.Properties.SetBaseProperties((QFastPropertyBag) null, true, false);
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      this.SetBaseProperties(e.Control as QRibbonPage);
      base.OnControlAdded(e);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      this.ResetBaseProperties(e.Control as QRibbonPage);
      base.OnControlRemoved(e);
    }

    protected override void OnHotPageChanged(QTabPageChangeEventArgs e)
    {
      this.HandleSelectedNavigationItemChanged((IQNavigationItem) (e.ToPage as QRibbonPage), QNavigationActivationType.None);
      base.OnHotPageChanged(e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.TabStrip != null)
        this.TabStrip.UpdateMdiVisbilityState();
      base.OnLayout(levent);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_oMouseHooker != null)
          this.m_oMouseHooker.Dispose();
        try
        {
          Application.RemoveMessageFilter((IMessageFilter) this.m_oKeyboardMessageFilter);
        }
        catch (InvalidOperationException ex)
        {
        }
      }
      base.Dispose(disposing);
    }

    private bool CompositeIsLocatedOnRibbonPage(QComposite composite)
    {
      Control parentControl = composite.ParentControl;
      return parentControl != null && this.Controls.Contains(parentControl);
    }

    private bool CompositeIsLocatedOnRibbonOrNavigationHost(QComposite composite)
    {
      if (this.CompositeIsLocatedOnRibbonPage(composite))
        return true;
      for (int index = 0; index < this.m_oNavigationHosts.Count; ++index)
      {
        if (this.m_oNavigationHosts[index] is IQCompositeContainer oNavigationHost && oNavigationHost.Composite == composite)
          return true;
      }
      return false;
    }

    protected virtual void OnCompositeKeyPress(QCompositeKeyboardCancelEventArgs e) => this.m_oCompositeKeyPress = QWeakDelegate.InvokeDelegate(this.m_oCompositeKeyPress, (object) this, (object) e);

    protected virtual void OnSelectedItemChanged(QCompositeEventArgs e)
    {
      if (this.CompositeIsLocatedOnRibbonOrNavigationHost(e.Composite))
        this.HandleSelectedNavigationItemChanged(e.Item as IQNavigationItem, QCompositeHelper.GetAsNavigationActivationType(e.ActivationType));
      this.m_oSelectedItemChanged = QWeakDelegate.InvokeDelegate(this.m_oSelectedItemChanged, (object) this, (object) e);
    }

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e) => this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e) => this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);

    protected virtual void OnItemActivated(QCompositeEventArgs e)
    {
      this.HandleItemActivated(e);
      this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);
    }

    protected virtual void OnItemSelected(QCompositeEventArgs e) => this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e) => this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (this.CompositeIsLocatedOnRibbonOrNavigationHost(e.Composite))
        this.m_oExpandedItem = e.Item as QCompositeItem;
      if (!QCompositeHelper.IsKeyboardActivationType(e.ActivationType) && this.HasSimulatedFocus)
        this.LoseSimulatedFocus();
      this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsed(QCompositeEventArgs e)
    {
      this.HandleItemCollapsed(e);
      this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e) => this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);

    protected virtual void OnHelpButtonActivated(EventArgs e) => this.m_oHelpButtonActivated = QWeakDelegate.InvokeDelegate(this.m_oHelpButtonActivated, (object) this, (object) e);

    private IQNavigationHost FindNavigationHost(IQNavigationItem item)
    {
      switch (item)
      {
        case QRibbonPage _:
          return (IQNavigationHost) this;
        case QCompositeItemBase qcompositeItemBase:
          if (qcompositeItemBase.Composite != null)
          {
            if (this.CompositeIsLocatedOnRibbonPage(qcompositeItemBase.Composite))
              return (IQNavigationHost) this.m_oPageContentNavigationHost;
            if (qcompositeItemBase.Composite.ParentContainer != null && this.m_oNavigationHosts.Contains(qcompositeItemBase.Composite.ParentContainer as IQNavigationHost))
              return qcompositeItemBase.Composite.ParentContainer as IQNavigationHost;
            break;
          }
          break;
      }
      return (IQNavigationHost) null;
    }

    private void HandleSelectedNavigationItemChanged(
      IQNavigationItem item,
      QNavigationActivationType activationType)
    {
      if (this.SelectedNavigationItem != null)
        this.SelectedNavigationItem.Select(false, QNavigationSelectionReason.None, activationType);
      this.m_oSelectedNavigationItem = item;
      this.m_oSelectedNavigationHost = this.FindNavigationHost(item);
    }

    private void HandleItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (this.CompositeIsLocatedOnRibbonOrNavigationHost(e.Composite))
        this.m_oExpandedItem = e.Item as QCompositeItem;
      if (QCompositeHelper.IsKeyboardActivationType(e.ActivationType) || !this.HasSimulatedFocus)
        return;
      this.LoseSimulatedFocus();
    }

    private void HandleItemCollapsed(QCompositeEventArgs e)
    {
      if (this.m_oExpandedItem != e.Item)
        return;
      this.m_oExpandedItem = (QCompositeItem) null;
      if (!this.HasSimulatedFocus || !this.m_oHotkeyHandler.IsSuspended)
        return;
      if (e.ActivationType == QCompositeActivationType.Mouse)
        this.m_oHotkeyHandler.StopProcessing();
      else
        this.m_oHotkeyHandler.ResumeProcessing();
    }

    private void HandleItemActivated(QCompositeEventArgs e)
    {
      if (!this.HasSimulatedFocus)
        return;
      this.LoseSimulatedFocus();
    }

    private void PerformChildControlsLayout()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
        this.Controls[index].PerformLayout();
    }

    private void ChildObject_ConfigurationChanged(object sender, EventArgs e)
    {
      this.PerformChildControlsLayout();
      this.Invalidate(this.ClientRectangle, true);
    }

    bool IQMouseHookClient.SuppressMessageToDestination(
      int code,
      ref Qios.DevSuite.Components.NativeMethods.MOUSEHOOKSTRUCT mouseHookStruct)
    {
      return false;
    }

    void IQMouseHookClient.HandleMouseWheelMessage(
      ref bool cancelMessage,
      MouseEventArgs e)
    {
    }

    void IQMouseHookClient.HandleExitMessage(ref bool cancelMessage)
    {
      this.LoseSimulatedFocus();
      cancelMessage = false;
    }

    void IQMainMenu.HandleActiveMdiChildChanged() => this.TabStrip.HandleTabStripChanged(QCommandUIRequest.PerformLayout, Rectangle.Empty);

    void IQMainMenu.HandleMdiChildWindowStateChanged(int sizeParameter) => this.TabStrip.HandleTabStripChanged(QCommandUIRequest.PerformLayout, Rectangle.Empty);

    void IQMainMenu.HandleMenukeyDown(ref Message m)
    {
      if (!this.HasSimulatedFocus)
        this.SimulateFocus();
      else
        this.LoseSimulatedFocus();
    }

    void IQMainMenu.HandleDeactivate(IntPtr activatingWindow)
    {
      if (this.ContainsControl(QControlHelper.GetFirstControlFromHandle(activatingWindow)) || !this.HasSimulatedFocus)
        return;
      this.LoseSimulatedFocus();
    }

    void IQHotkeyHandlerHost.AddHotkeyItems(IList list)
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is IQHotkeyItem control && control.Visible && control.HasHotkey)
          list.Add((object) control);
      }
      for (int index = 0; index < this.m_oNavigationHosts.Count; ++index)
      {
        if (this.m_oNavigationHosts[index] is IQHotkeyHandlerHost oNavigationHost && oNavigationHost != this && oNavigationHost != this.m_oPageContentNavigationHost)
          oNavigationHost.AddHotkeyItems(list);
      }
    }

    IQHotkeyItem IQHotkeyHandlerHost.SelectedItem => this.m_oSelectedNavigationItem as IQHotkeyItem;

    IQHotkeyItem IQHotkeyHandlerHost.ActivatedItem => (IQHotkeyItem) this.m_oExpandedItem;

    void IQHotkeyHandlerHost.ConfigureHotkeyWindow(IQHotkeyItem item)
    {
      if (!item.HasHotkey)
        return;
      QHotkeyHelper.ConfigureHotkeyWindow((Control) this, (IQHotkeyHandlerHost) this, item, this.Configuration.HotkeyWindowConfiguration, this.ColorScheme);
    }

    bool IQNavigationHost.HandleShortcutKey(Keys key, out bool suppressToSystem)
    {
      suppressToSystem = false;
      return false;
    }

    void IQNavigationHost.SelectNextItem(bool forward, bool loop) => this.SelectNavigationItem(forward ? (IQNavigationItem) (this.GetNextAccessibleTabPage(this.ActiveTabPage, true) as QRibbonPage) : (IQNavigationItem) (this.GetPreviousAccessibleTabPage(this.ActiveTabPage, true) as QRibbonPage), QNavigationActivationType.Keyboard);

    void IQNavigationHost.SelectFirstOrCurrentItem(bool forward) => this.SelectNavigationItem((IQNavigationItem) this.ActiveRibbonPage, QNavigationActivationType.Keyboard);

    bool IQNavigationHost.IsAccessibleForNavigation => this.Visible && this.Enabled;

    Point IQNavigationHost.LocationForOrder => this.PointToScreen(Point.Empty);

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

    void IQCompositeEventRaiser.RaiseUsedToolTipTextChanged(
      QCompositeEventArgs e)
    {
    }

    void IQCompositeEventRaiser.RaiseCompositeKeyPress(
      QCompositeKeyboardCancelEventArgs e)
    {
      this.OnCompositeKeyPress(e);
    }

    void IQCompositeEventRaiser.RaiseSelectedItemChanged(QCompositeEventArgs e) => this.OnSelectedItemChanged(e);

    bool IQKeyboardMessageFilter.HandleKeyDown(
      Keys keys,
      Control destinationControl,
      Message message)
    {
      return this.HandleKeyDown(keys, destinationControl, message);
    }

    bool IQKeyboardMessageFilter.HandleKeyUp(
      Keys keys,
      Control destinationControl,
      Message message)
    {
      return this.HandleKeyUp(keys, destinationControl, message);
    }

    private void NavigationHost_ItemExpanded(object sender, QCompositeExpandedEventArgs e) => this.HandleItemExpanded(e);

    private void NavigationHost_ItemCollapsed(object sender, QCompositeEventArgs e) => this.HandleItemCollapsed(e);

    private void NavigationHost_SelectedItemChanged(object sender, QCompositeEventArgs e)
    {
      if (!this.CompositeIsLocatedOnRibbonOrNavigationHost(e.Composite))
        return;
      this.HandleSelectedNavigationItemChanged(e.Item as IQNavigationItem, QCompositeHelper.GetAsNavigationActivationType(e.ActivationType));
    }

    private void NavigationHost_Disposed(object sender, EventArgs e) => this.RemoveAdditionalNavigationControl(sender as QCompositeControl);

    private void NavigationHost_ItemActivated(object sender, QCompositeEventArgs e) => this.HandleItemActivated(e);

    private void NavigationArea_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      QButtonArea qbuttonArea = sender as QButtonArea;
      bool flag = this.Form != null && this.Form.ActiveMdiChild != null;
      if (e.FromState != QButtonState.Pressed || e.PressedButtons == Control.MouseButtons)
        return;
      if (qbuttonArea == this.TabStrip.NavigationArea.MdiClose)
      {
        if (!flag)
          return;
        this.Form.ActiveMdiChild.Close();
      }
      else if (qbuttonArea == this.TabStrip.NavigationArea.MdiRestore)
      {
        if (!flag)
          return;
        this.Form.ActiveMdiChild.WindowState = FormWindowState.Normal;
      }
      else if (qbuttonArea == this.TabStrip.NavigationArea.MdiMinimize)
      {
        if (!flag)
          return;
        this.Form.ActiveMdiChild.WindowState = FormWindowState.Minimized;
      }
      else
      {
        if (qbuttonArea != this.TabStrip.NavigationArea.Help)
          return;
        this.OnHelpButtonActivated(EventArgs.Empty);
      }
    }

    Point IQMouseHookClient.PointToClient([In] Point obj0) => this.PointToClient(obj0);

    private class QRibbonPageContentNavigationHost : IQNavigationHost
    {
      private QRibbon m_oRibbon;

      public QRibbonPageContentNavigationHost(QRibbon ribbon) => this.m_oRibbon = ribbon;

      bool IQNavigationHost.HandleShortcutKey(Keys key, out bool suppressToSystem)
      {
        suppressToSystem = false;
        QCompositeNavigationFilter filter = new QCompositeNavigationFilter(QCompositeNavigationFilterOptions.VisibleForShortcut | QCompositeNavigationFilterOptions.Enabled | QCompositeNavigationFilterOptions.MatchShortcut, key);
        for (int index = 0; index < this.m_oRibbon.Controls.Count; ++index)
        {
          if (this.m_oRibbon.Controls[index] is QRibbonPage control && control.ButtonVisible && control.Enabled)
          {
            QCompositeItemBase firstItemRecursive = QCompositeHelper.GetFirstItemRecursive((IQPart) control.Composite, true, filter);
            if (firstItemRecursive != null)
            {
              ((IQNavigationItem) firstItemRecursive).Activate(true, QNavigationActivationReason.None, QNavigationActivationType.Shortcut);
              QCompositeItem qcompositeItem = firstItemRecursive as QCompositeItem;
              suppressToSystem = qcompositeItem == null || qcompositeItem.SuppressShortcutToSystem;
              return true;
            }
          }
        }
        return false;
      }

      void IQNavigationHost.SelectFirstOrCurrentItem(bool forward) => this.m_oRibbon.SelectNavigationItem(QCompositeHelper.GetNextItemOrderedRecursive((IQPart) this.m_oRibbon.ActiveRibbonPage.Composite, (IQPart) null, new QCompositeNavigationFilter(QCompositeNavigationFilterOptions.Visible | QCompositeNavigationFilterOptions.HasPressedState), forward, false) as IQNavigationItem, QNavigationActivationType.Keyboard);

      void IQNavigationHost.SelectNextItem(bool forward, bool loop)
      {
        if (this.m_oRibbon.ActiveRibbonPage == null)
          return;
        this.m_oRibbon.SelectNavigationItem(QCompositeHelper.GetNextItemOrderedRecursive((IQPart) this.m_oRibbon.ActiveRibbonPage.Composite, (IQPart) (this.m_oRibbon.SelectedNavigationItem as QCompositeItemBase), new QCompositeNavigationFilter(QCompositeNavigationFilterOptions.Visible | QCompositeNavigationFilterOptions.HasPressedState), forward, loop) as IQNavigationItem, QNavigationActivationType.Keyboard);
      }

      bool IQNavigationHost.IsAccessibleForNavigation => this.m_oRibbon.ActiveRibbonPage != null && this.m_oRibbon.ActiveRibbonPage.Visible && this.m_oRibbon.ActiveRibbonPage.Enabled;

      Point IQNavigationHost.LocationForOrder => this.m_oRibbon.ActiveRibbonPage != null ? this.m_oRibbon.ActiveRibbonPage.PointToScreen(Point.Empty) : Point.Empty;
    }
  }
}
