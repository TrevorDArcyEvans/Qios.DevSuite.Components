// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QMenuItemConverter))]
  [ToolboxItem(false)]
  [Designer(typeof (QMenuItemDesigner), typeof (IDesigner))]
  public class QMenuItem : QCommand
  {
    private QCommandPaintOptions m_oFlags;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakEventConsumerCollection m_oEventConsumers;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bForceInvisibility;
    private QCommandControlContainer m_oControl;
    private Rectangle m_oControlBounds;
    private bool m_bOrientationForFlow;
    private QMargin m_oMargin = QMargin.Empty;
    private QPadding m_oPadding = QPadding.Empty;
    private QCommandOrientation m_eOrientation;
    private Rectangle m_oOuterBounds = Rectangle.Empty;
    private Rectangle m_oContentsBounds;
    private Color m_oIconColorToReplace = Color.Empty;
    private Color m_oCheckedIconColorToReplace = Color.Red;
    private bool m_bIsSeparator;
    private bool m_bCloseMenuOnActivate = true;
    private string m_sTitle;
    private string m_sToolTip;
    private string m_sDisplayedTitle;
    private QIconContainer m_oIconContainer;
    private QIconContainer m_oDisabledIconContainer;
    private QIconContainer m_oCheckedIconContainer;
    private bool m_bDisabledIconGrayScaled = true;
    private static Icon m_oStaticCheckedIcon;
    private bool m_bChecked;
    private Shortcut m_eShortcut;
    private bool m_bSuppressShortcutToSystem = true;
    private Keys m_eHotkey;
    private bool m_bVisible = true;
    private bool m_bVisibleWhenPersonalized = true;
    private bool m_bUserHasRightToExecute = true;
    private bool m_bEnabled = true;
    private bool m_bInformationOnly;
    private QCommandUserRightBehavior m_eUserRightBehavior = QCommandUserRightBehavior.DisableWhenNoRight;
    private Rectangle m_oIconBounds;
    private Rectangle m_oTextBounds;
    private Rectangle m_oShortcutBounds;
    private Rectangle m_oHasChildItemsBounds;
    private Rectangle m_oHasChildItemsHotBounds;
    private Rectangle m_oSeparatorBounds;
    private object m_oCommandObject;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oParentMenuItemCollectionChangedDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuItemMouseDownDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuItemMouseUpDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuItemSelectedDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuItemActivatingDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuItemActivatedDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuShowingDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuShowedDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oPaintMenuItemDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oMenuItemsRequestedDelegate;

    public QMenuItem(IContainer container)
      : base(container)
    {
      this.InternalConstruct();
    }

    public QMenuItem()
      : this((string) null, (string) null, (Icon) null, Shortcut.None)
    {
    }

    public QMenuItem(bool separator)
      : this((string) null, (string) null, (Icon) null, Shortcut.None)
    {
      this.IsSeparator = separator;
    }

    public QMenuItem(string title)
      : this(title, (string) null, (Icon) null, Shortcut.None)
    {
    }

    public QMenuItem(string title, string name)
      : this(title, name, (Icon) null, Shortcut.None)
    {
    }

    public QMenuItem(string title, string itemName, Icon icon, Shortcut shortcut)
    {
      this.InternalConstruct();
      this.Title = title;
      this.ItemName = itemName;
      this.Icon = icon;
      this.Shortcut = shortcut;
    }

    private void InternalConstruct()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      if (QMenuItem.m_oStaticCheckedIcon == null)
        QMenuItem.m_oStaticCheckedIcon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Icons.Checked.ico"), 16, 16);
      this.m_oIconContainer = new QIconContainer();
      this.m_oDisabledIconContainer = new QIconContainer();
      this.m_oCheckedIconContainer = new QIconContainer(QMenuItem.m_oStaticCheckedIcon);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler ParentMenuItemCollectionChanged
    {
      add => this.m_oParentMenuItemCollectionChangedDelegate = QWeakDelegate.Combine(this.m_oParentMenuItemCollectionChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oParentMenuItemCollectionChangedDelegate = QWeakDelegate.Remove(this.m_oParentMenuItemCollectionChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a user tries to expand a menuItem while the menuItem doens't have any child menuitems.")]
    public event QMenuEventHandler MenuItemsRequested
    {
      add => this.m_oMenuItemsRequestedDelegate = QWeakDelegate.Combine(this.m_oMenuItemsRequestedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemsRequestedDelegate = QWeakDelegate.Remove(this.m_oMenuItemsRequestedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when a user selects a menuItem via the mouse or the Keyboard")]
    [QWeakEvent]
    public event QMenuEventHandler MenuItemSelected
    {
      add => this.m_oMenuItemSelectedDelegate = QWeakDelegate.Combine(this.m_oMenuItemSelectedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemSelectedDelegate = QWeakDelegate.Remove(this.m_oMenuItemSelectedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a user activates a MenuItem by clicking it, using a ShortCut, a HotKey or navigating to it with the keyboard. This event can be canceled.")]
    public event QMenuCancelEventHandler MenuItemActivating
    {
      add => this.m_oMenuItemActivatingDelegate = QWeakDelegate.Combine(this.m_oMenuItemActivatingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemActivatingDelegate = QWeakDelegate.Remove(this.m_oMenuItemActivatingDelegate, (Delegate) value);
    }

    [Description("Gets raised when a user activates a menuItem by clicking it, using a ShortCut, a HotKey or navigating to it with the keyboard.")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QMenuEventHandler MenuItemActivated
    {
      add => this.m_oMenuItemActivatedDelegate = QWeakDelegate.Combine(this.m_oMenuItemActivatedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuItemActivatedDelegate = QWeakDelegate.Remove(this.m_oMenuItemActivatedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when a menuItem is activated and it has ChildMenuItems before the ChildMenu pops up. This event can be canceled.")]
    public event QMenuCancelEventHandler MenuShowing
    {
      add => this.m_oMenuShowingDelegate = QWeakDelegate.Combine(this.m_oMenuShowingDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuShowingDelegate = QWeakDelegate.Remove(this.m_oMenuShowingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a menuItem is activated and it has ChildMenuItems when the ChildMenu is popped up.")]
    public event QMenuEventHandler MenuShowed
    {
      add => this.m_oMenuShowedDelegate = QWeakDelegate.Combine(this.m_oMenuShowedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oMenuShowedDelegate = QWeakDelegate.Remove(this.m_oMenuShowedDelegate, (Delegate) value);
    }

    [Description("Gets raised when a MenuItem must be painted")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QPaintMenuItemEventHandler PaintMenuItem
    {
      add => this.m_oPaintMenuItemDelegate = QWeakDelegate.Combine(this.m_oPaintMenuItemDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oPaintMenuItemDelegate = QWeakDelegate.Remove(this.m_oPaintMenuItemDelegate, (Delegate) value);
    }

    [Editor(typeof (QMenuItemCollectionEditor), typeof (UITypeEditor))]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the collection of MenuItems of this MenuItem")]
    public virtual QMenuItemCollection MenuItems => (QMenuItemCollection) this.Commands;

    [Browsable(false)]
    public QMenuItemCollection ParentMenuItemCollection
    {
      get
      {
        if (this.ParentMenuItem != null)
          return this.ParentMenuItem.MenuItems;
        return this.ParentMenuItemContainer != null ? this.ParentMenuItemContainer.Items : (QMenuItemCollection) null;
      }
    }

    [Browsable(false)]
    public QMenu ParentMenu => this.ParentContainer is QMenu ? (QMenu) this.ParentContainer : (QMenu) null;

    [Browsable(false)]
    public QMenuItemContainer ParentMenuItemContainer => this.ParentContainer is QMenuItemContainer ? (QMenuItemContainer) this.ParentContainer : (QMenuItemContainer) null;

    [Browsable(false)]
    public QMenuItem ParentMenuItem => this.ParentCommand is QMenuItem ? (QMenuItem) this.ParentCommand : (QMenuItem) null;

    [Browsable(false)]
    public QMenu ChildMenu => this.ChildContainer is QMenu ? (QMenu) this.ChildContainer : (QMenu) null;

    [Description("Gets or sets whether this QMenuItem is a separator")]
    [Category("QAppearance")]
    [DefaultValue(false)]
    public bool IsSeparator
    {
      get => this.m_bIsSeparator;
      set
      {
        this.m_bIsSeparator = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the Menu must be closed when the Item is activated")]
    [Category("QAppearance")]
    public bool CloseMenuOnActivate
    {
      get => this.m_bCloseMenuOnActivate;
      set
      {
        this.m_bCloseMenuOnActivate = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.None);
      }
    }

    [Description("Gets or sets the text on the ToolTip. This must contain valid Xml as used with QMarkupText")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    [Localizable(true)]
    public string ToolTip
    {
      get => this.m_sToolTip;
      set
      {
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sToolTip = value;
      }
    }

    [Browsable(false)]
    public string UsedToolTip => !QMisc.IsEmpty((object) this.m_sToolTip) ? this.m_sToolTip : HttpUtility.HtmlEncode(this.DisplayedTitle);

    [Localizable(true)]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the title of the QMenuItem")]
    [Category("QAppearance")]
    public string Title
    {
      get => this.m_sTitle;
      set
      {
        this.m_sTitle = value;
        this.m_eHotkey = QHotkeyHelper.FindHotKey(value);
        this.m_sDisplayedTitle = QHotkeyHelper.RemoveHotkeyPrefix(value);
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    public string DisplayedTitle => this.m_sDisplayedTitle;

    public bool ShouldSerializeIcon() => this.m_oIconContainer.ShouldSerializeIcon();

    public void ResetIcon() => this.m_oIconContainer.ResetIcon();

    [Category("QAppearance")]
    [Description("Gets or sets the Icon of the QMenuItem")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public Icon Icon
    {
      get => this.m_oIconContainer.Icon;
      set
      {
        this.m_oIconContainer.Icon = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public string IconResourceName
    {
      get => this.m_oIconContainer.ResourceName;
      set
      {
        this.m_oIconContainer.ResourceName = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets whether the Icon is loaded from a resource")]
    [Browsable(false)]
    public bool IconLoadedFromResource => this.m_oIconContainer.LoadedFromResource;

    public bool ShouldSerializeCheckedIcon() => this.m_oCheckedIconContainer.ShouldSerializeIcon();

    public void ResetCheckedIcon() => this.m_oCheckedIconContainer.ResetIcon();

    [QXmlSave(QXmlSaveType.NeverSave)]
    [Description("Gets or sets the Icon of the QMenuItem when it is checked. When this is not set the default CheckedIcon is used")]
    [Category("QAppearance")]
    public Icon CheckedIcon
    {
      get => this.m_oCheckedIconContainer.Icon;
      set
      {
        this.m_oCheckedIconContainer.Icon = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets a possible resource name to load the CheckedIcon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string CheckedIconResourceName
    {
      get => this.m_oCheckedIconContainer.ResourceName;
      set
      {
        this.m_oCheckedIconContainer.ResourceName = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    public bool CheckedIconLoadedFromResource => this.m_oCheckedIconContainer.LoadedFromResource;

    public bool ShouldSerializeDisabledIcon() => this.m_oDisabledIconContainer.ShouldSerializeIcon();

    public void ResetDisabledIcon() => this.m_oDisabledIconContainer.ResetIcon();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the DisabledIcon of the QMenuItem. When this is not set the default Icon is used for painting.")]
    [Category("QAppearance")]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public Icon DisabledIcon
    {
      get => this.m_oDisabledIconContainer.Icon;
      set
      {
        this.m_oDisabledIconContainer.Icon = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets a possible resource name to load the DisabledIcon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string DisabledIconResourceName
    {
      get => this.m_oDisabledIconContainer.ResourceName;
      set
      {
        this.m_oDisabledIconContainer.ResourceName = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    [Description("Gets whether the DisabledIcon is loaded from a resource")]
    public bool DisabledIconLoadedFromResource => this.m_oDisabledIconContainer.LoadedFromResource;

    [Description(" Gets or sets whether the Icon or DisabledIcon must be drawn gray-scaled when disabled.")]
    [DefaultValue(true)]
    [Category("QAppearance")]
    public bool DisabledIconGrayScaled
    {
      get => this.m_bDisabledIconGrayScaled;
      set
      {
        this.m_bDisabledIconGrayScaled = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.Redraw);
      }
    }

    [Browsable(false)]
    public Icon UsedIcon
    {
      get
      {
        if (!this.Enabled && this.DisabledIcon != null)
          return this.DisabledIcon;
        if (!this.Checked)
          return this.Icon;
        if (this.CheckedIcon != null)
          return this.CheckedIcon;
        return this.Icon != null ? this.Icon : this.m_oCheckedIconContainer.DefaultIcon;
      }
    }

    [Browsable(false)]
    public Color UsedIconReplaceColor => !this.Checked ? this.IconColorToReplace : this.CheckedIconColorToReplace;

    private bool ShouldSerializeIconColorToReplace() => this.m_oIconColorToReplace != Color.Empty;

    private void ResetIconColorToReplace() => this.m_oIconColorToReplace = Color.Empty;

    [Category("QAppearance")]
    [Description("Gets or sets the Color of the Icon or DisabledIcon to replace with the TextColor")]
    public Color IconColorToReplace
    {
      get => this.m_oIconColorToReplace;
      set => this.m_oIconColorToReplace = value;
    }

    [Description("Contains the Color of the CheckedIcon to replace with the TextColor")]
    [DefaultValue(typeof (Color), "Red")]
    [Category("QAppearance")]
    public Color CheckedIconColorToReplace
    {
      get => this.m_oCheckedIconColorToReplace;
      set => this.m_oCheckedIconColorToReplace = value;
    }

    [Category("QAppearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the command is checked")]
    public bool Checked
    {
      get => this.m_bChecked;
      set
      {
        this.m_bChecked = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [DefaultValue(Shortcut.None)]
    [Localizable(true)]
    [Description("Contains the ShortCut of the Command")]
    [Category("QBehavior")]
    public Shortcut Shortcut
    {
      get => this.m_eShortcut;
      set
      {
        this.m_eShortcut = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    public Keys ShortcutKeys => (Keys) this.m_eShortcut;

    [Browsable(false)]
    public string ShortcutString => TypeDescriptor.GetConverter(typeof (Keys)).ConvertToString((object) this.ShortcutKeys);

    [Description("Gets or sets whether the pressed shortcut must be suppressed and not be bubbeled up to the system. Turn this off to let the system handle the shortcut.")]
    [DefaultValue(true)]
    [Category("QBehavior")]
    public virtual bool SuppressShortcutToSystem
    {
      get => this.m_bSuppressShortcutToSystem;
      set => this.m_bSuppressShortcutToSystem = value;
    }

    [Browsable(false)]
    public Keys Hotkey => this.m_eHotkey;

    [Category("QBehavior")]
    [DefaultValue(true)]
    [Description("Gets or sets whether this item is Visible")]
    public bool Visible
    {
      get => this.m_bVisible;
      set
      {
        bool isVisible = this.IsVisible;
        this.m_bVisible = value;
        if (isVisible == this.IsVisible)
          return;
        this.CalculateControlBoundsProperties(true);
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether this item is Enabled")]
    [Category("QBehavior")]
    public bool Enabled
    {
      get => this.m_bEnabled;
      set
      {
        bool isEnabled = this.IsEnabled;
        this.m_bEnabled = value;
        if (this.Control != null)
          this.Control.Enabled = value;
        if (isEnabled == this.IsEnabled)
          return;
        this.CalculateControlBoundsProperties(true);
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets or sets whether this item has only information purposes. When it is true it cannot be clicked.")]
    [DefaultValue(false)]
    [Category("QBehavior")]
    public bool InformationOnly
    {
      get => this.m_bInformationOnly;
      set
      {
        if (this.m_bInformationOnly == value)
          return;
        this.m_bInformationOnly = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.Redraw);
      }
    }

    [Browsable(false)]
    public bool IsInformationOnly => this.m_bInformationOnly || this.m_bIsSeparator || this.Control != null;

    [DefaultValue(true)]
    [Category("QBehavior")]
    [Description("Gets or sets whether the use has right to execute the QMenuItem")]
    public bool UserHasRightToExecute
    {
      get => this.m_bUserHasRightToExecute;
      set
      {
        if (this.UserRightBehavior == QCommandUserRightBehavior.DisableWhenNoRight)
        {
          bool isEnabled = this.IsEnabled;
          this.m_bUserHasRightToExecute = value;
          if (isEnabled != this.IsEnabled)
            this.NotifyParentContainerOfChange(QCommandUIRequest.Redraw);
        }
        else if (this.UserRightBehavior == QCommandUserRightBehavior.HideWhenNoRight)
        {
          bool isVisible = this.IsVisible;
          this.m_bUserHasRightToExecute = value;
          if (isVisible != this.IsVisible)
            this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
        }
        else
          this.m_bUserHasRightToExecute = value;
        this.CalculateControlBoundsProperties(true);
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets whether the QMenuItem is shown when the QMenu is personalized.")]
    [DefaultValue(true)]
    public bool VisibleWhenPersonalized
    {
      get => this.m_bVisibleWhenPersonalized;
      set
      {
        bool isVisible = this.IsVisible;
        this.m_bVisibleWhenPersonalized = value;
        if (isVisible == this.IsVisible)
          return;
        this.CalculateControlBoundsProperties(false);
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets or sets what should happen when the user has no right to execute the QMenuItem")]
    [Category("QBehavior")]
    [DefaultValue(QCommandUserRightBehavior.DisableWhenNoRight)]
    public QCommandUserRightBehavior UserRightBehavior
    {
      get => this.m_eUserRightBehavior;
      set
      {
        if (this.m_eUserRightBehavior == value)
          return;
        this.m_eUserRightBehavior = value;
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    public virtual bool IsVisible
    {
      get
      {
        if (!this.Visible || !this.UserHasRightToExecute && this.UserRightBehavior == QCommandUserRightBehavior.HideWhenNoRight || !this.VisibleWhenPersonalized && this.ParentMenuItemContainer != null && !this.ParentMenuItemContainer.ShowPersonalizedItems)
          return false;
        if (this.IsSeparator && this.ParentMenuItemCollection != null)
        {
          QMenuItem previousVisibleItem = this.PreviousVisibleItem;
          if (previousVisibleItem == null || previousVisibleItem.IsSeparator)
            return false;
        }
        return true;
      }
    }

    internal bool OrientationForFlow
    {
      get => this.m_bOrientationForFlow;
      set
      {
        if (this.m_bOrientationForFlow == value)
          return;
        this.m_bOrientationForFlow = value;
      }
    }

    internal bool ForceInvisibility
    {
      get => this.m_bForceInvisibility;
      set
      {
        if (this.m_bForceInvisibility == value)
          return;
        this.m_bForceInvisibility = value;
        if (!value || this.Control == null || this.ParentControl != this.Control.Parent)
          return;
        this.Control.Visible = false;
      }
    }

    internal QCommandOrientation Orientation
    {
      get => this.m_eOrientation;
      set
      {
        if (this.m_eOrientation == value)
          return;
        this.m_eOrientation = value;
        this.CalculateBoundsProperties();
      }
    }

    internal QPadding Padding
    {
      get => this.m_oPadding;
      set
      {
        if (this.m_oPadding == value)
          return;
        this.m_oPadding = value;
        this.CalculateBoundsProperties();
      }
    }

    internal QMargin Margin
    {
      get => this.m_oMargin;
      set
      {
        if (this.m_oMargin == value)
          return;
        this.m_oMargin = value;
        this.CalculateBoundsProperties();
      }
    }

    public Rectangle OuterBounds => this.m_oOuterBounds;

    internal void PutOuterBounds(Rectangle value) => this.PutContentsBounds(this.Padding.InflateRectangleWithPadding(this.Margin.InflateRectangleWithMargin(value, false, this.m_eOrientation == QCommandOrientation.Horizontal || this.m_bOrientationForFlow), false, this.m_eOrientation == QCommandOrientation.Horizontal || this.m_bOrientationForFlow));

    public Rectangle ContentsBounds => this.m_oContentsBounds;

    internal void PutContentsBounds(Rectangle value)
    {
      this.m_oContentsBounds = value;
      this.CalculateBoundsProperties();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    [Browsable(false)]
    public override Rectangle Bounds
    {
      get => base.Bounds;
      set => this.PutContentsBounds(this.Padding.InflateRectangleWithPadding(value, false, this.m_eOrientation == QCommandOrientation.Horizontal || this.m_bOrientationForFlow));
    }

    [QXmlSave(QXmlSaveType.NeverSave)]
    [Browsable(false)]
    [DefaultValue(null)]
    public virtual QCommandControlContainer Control
    {
      get => this.m_oControl;
      set
      {
        if (this.m_oControl == value)
          return;
        if (this.m_oControl != null)
          this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.m_oControl_Disposed));
        this.m_oControl = value;
        if (this.m_oControl != null)
        {
          if (this.m_oControl.PreferredSize.IsEmpty)
            this.m_oControl.PreferredSize = this.m_oControl.Size;
          this.m_oControl.Enabled = this.Enabled;
          this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.m_oControl_Disposed), (object) this.m_oControl, "Disposed"));
        }
        this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    public Rectangle ControlBounds => this.m_oControlBounds;

    internal void PutControlBounds(Rectangle value)
    {
      this.m_oControlBounds = value;
      if (this.Control == null || this.Control.Size.Equals((object) this.m_oControlBounds.Size))
        return;
      this.Control.Size = this.m_oControlBounds.Size;
    }

    public Rectangle ContentsRectangleToParent(Rectangle rectangle) => new Rectangle(rectangle.Left + this.m_oContentsBounds.Left, rectangle.Top + this.m_oContentsBounds.Top, rectangle.Width, rectangle.Height);

    public Rectangle ParentRectangleToContents(Rectangle rectangle) => new Rectangle(rectangle.Left - this.m_oContentsBounds.Left, rectangle.Top - this.m_oContentsBounds.Top, rectangle.Width, rectangle.Height);

    public Point ContentsPointToParent(Point point) => new Point(point.X + this.m_oContentsBounds.X, point.Y + this.m_oContentsBounds.Top);

    internal void SetControlParent()
    {
      if (this.Control == null || this.ForceInvisibility)
        return;
      System.Windows.Forms.Control parentControl = this.ParentControl;
      for (QCommand parentCommand = this.ParentCommand; parentControl == null && parentCommand != null; parentCommand = parentCommand.ParentCommand)
        parentControl = parentCommand.ParentControl;
      if (parentControl != null)
      {
        if (this.Control.Parent == parentControl)
          return;
        this.Control.DisconnectFromParent();
        if (this.Control.Parent != null && this.Control.Parent.Controls.Contains((System.Windows.Forms.Control) this.Control))
          this.Control.Parent.Controls.Remove((System.Windows.Forms.Control) this.Control);
        parentControl.SuspendLayout();
        parentControl.Controls.Add((System.Windows.Forms.Control) this.Control);
        parentControl.ResumeLayout(false);
      }
      this.CalculateControlBoundsProperties(false);
      this.Control.ResetBackground();
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void CalculateBoundsProperties()
    {
      base.Bounds = this.m_oPadding.InflateRectangleWithPadding(this.m_oContentsBounds, true, this.m_eOrientation == QCommandOrientation.Horizontal || this.m_bOrientationForFlow);
      this.m_oOuterBounds = this.m_oMargin.InflateRectangleWithMargin(base.Bounds, true, this.m_eOrientation == QCommandOrientation.Horizontal || this.m_bOrientationForFlow);
    }

    internal void CalculateControlBoundsProperties(bool childItemsOnly)
    {
      QExplorerItem qexplorerItem = this as QExplorerItem;
      QMenuItem qmenuItem = this;
      if (qexplorerItem != null && qexplorerItem.ItemState != QExplorerItemState.Expanded)
      {
        for (int index = 0; index < this.MenuItems.Count; ++index)
          this.MenuItems[index].CalculateControlBoundsProperties(false);
      }
      if (childItemsOnly || this.Control == null)
        return;
      bool flag1 = false;
      bool flag2 = false;
      if (qexplorerItem != null)
      {
        flag1 = qexplorerItem.IsVisible;
        flag2 = qexplorerItem.IsEnabled;
        if (qexplorerItem.InMovement)
          flag1 = false;
        if (qexplorerItem.ParentCommand is QExplorerItem && ((QExplorerItem) this.ParentCommand).ItemState != QExplorerItemState.Expanded)
          flag1 = false;
      }
      else if (qmenuItem != null)
      {
        flag1 = !this.ForceInvisibility && qmenuItem.IsVisible;
        flag2 = qmenuItem.IsEnabled;
      }
      if (this.ForceInvisibility)
        return;
      if (this.Control.Parent != null)
        this.Control.Parent.SuspendLayout();
      if (flag1 != this.Control.Visible)
        this.Control.Visible = flag1;
      if (flag2 != this.Control.Enabled)
        this.Control.Enabled = flag2;
      if (!this.Control.GetLocation().Equals((object) this.ContentsRectangleToParent(this.ControlBounds).Location))
        this.Control.SetLocation(this.ContentsRectangleToParent(this.ControlBounds).Location);
      if (this.Control.Parent == null)
        return;
      this.Control.Parent.ResumeLayout(false);
    }

    internal virtual bool IsExpanded => this.ParentMenuItemContainer != null && this.ParentMenuItemContainer.ExpandedItem == this;

    [Browsable(false)]
    public virtual bool IsAccessible => this.Visible && this.Enabled && this.UserHasRightToExecute;

    [Browsable(false)]
    public virtual bool IsEnabled => this.Enabled && (this.ParentMenuItemContainer == null || this.ParentMenuItemContainer.Enabled) && (this.UserHasRightToExecute || this.UserRightBehavior != QCommandUserRightBehavior.DisableWhenNoRight);

    [Browsable(false)]
    public QMenuItem PreviousVisibleItem => this.GetNextVisibleItem(false);

    [Browsable(false)]
    public QMenuItem NextVisibleItem => this.GetNextVisibleItem(true);

    public override void HandleChildCommandCollectionChanged(int fromCount, int toCount)
    {
      base.HandleChildCommandCollectionChanged(fromCount, toCount);
      if (fromCount != 0 && toCount != 0)
        return;
      this.NotifyParentContainerOfChange(QCommandUIRequest.PerformLayout);
    }

    public virtual void LoadFromXml(IXPathNavigable itemElement, QMenuItemLoadType loadType)
    {
      QXmlHelper.LoadObjectFromXmlElement(itemElement, (object) this, (PropertyDescriptorCollection) null);
      if (QXmlHelper.ContainsChildElement(itemElement, "menuItemCollection"))
        this.MenuItems.LoadFromXml(QXmlHelper.SelectChildNavigable(itemElement, "menuItemCollection"), loadType);
      if (!QXmlHelper.ContainsChildElement(itemElement, "designName"))
        return;
      this.DesignName = QXmlHelper.GetChildElementString(itemElement, "designName");
    }

    public virtual void SaveToXml(IXPathNavigable collectionElement, QMenuItemSaveType saveType)
    {
      bool flag = saveType == QMenuItemSaveType.PersonalizedStateOnly;
      if (flag && this.VisibleWhenPersonalized && (!this.HasChildItems || !this.MenuItems.HasPersonalizedItems(true)) && !(this is QExplorerItem))
        return;
      IXPathNavigable xpathNavigable = QXmlHelper.AddElement(collectionElement, "item");
      System.Type type = this.GetType();
      string str = type.FullName + ", " + type.Assembly.GetName().Name;
      QXmlHelper.AddAttribute(xpathNavigable, "type", (object) str);
      QXmlHelper.AddAttribute(xpathNavigable, "name", (object) this.ItemName);
      if (!flag)
      {
        QXmlHelper.SaveObjectToXml(xpathNavigable, (object) this, (PropertyDescriptorCollection) null);
        if (this.Site != null)
          QXmlHelper.AddElement(xpathNavigable, "designName", (object) this.Site.Name);
      }
      else if (!this.VisibleWhenPersonalized)
        QXmlHelper.AddElement(xpathNavigable, "visibleWhenPersonalized", (object) this.VisibleWhenPersonalized);
      this.SavePropertiesToXml(xpathNavigable, saveType);
      if (!this.HasChildItems)
        return;
      this.MenuItems.SaveToXml(xpathNavigable, saveType);
    }

    internal virtual void SavePropertiesToXml(IXPathNavigable element, QMenuItemSaveType saveType)
    {
    }

    internal QMenuItem GetNextVisibleItem(bool directionDown) => this.ParentMenuItemCollection != null ? this.ParentMenuItemCollection.GetNextVisibleMenuItem(this, directionDown) : (QMenuItem) null;

    [Browsable(false)]
    internal QCommandPaintOptions Flags
    {
      get => this.m_oFlags;
      set
      {
        if (this.m_oFlags == value)
          return;
        this.m_oFlags = value;
        if (this.Control == null)
          return;
        this.Control.ResetBackground();
      }
    }

    [Browsable(false)]
    public Rectangle IconBounds => this.m_oIconBounds;

    internal void PutIconBounds(Rectangle value) => this.m_oIconBounds = value;

    [Browsable(false)]
    public Rectangle TextBounds => this.m_oTextBounds;

    internal void PutTextBounds(Rectangle value) => this.m_oTextBounds = value;

    [Browsable(false)]
    public Rectangle ShortcutBounds => this.m_oShortcutBounds;

    internal void PutShortcutBounds(Rectangle value) => this.m_oShortcutBounds = value;

    [Browsable(false)]
    public Rectangle HasChildItemsBounds => this.m_oHasChildItemsBounds;

    internal void PutHasChildItemsBounds(Rectangle value) => this.m_oHasChildItemsBounds = value;

    [Browsable(false)]
    public Rectangle HasChildItemsHotBounds => this.m_oHasChildItemsHotBounds;

    internal void PutHasChildItemsHotBounds(Rectangle value) => this.m_oHasChildItemsHotBounds = value;

    [Browsable(false)]
    public Rectangle SeparatorBounds => this.m_oSeparatorBounds;

    internal void PutSeparatorBounds(Rectangle value) => this.m_oSeparatorBounds = value;

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object CommandObject
    {
      get => this.m_oCommandObject;
      set => this.m_oCommandObject = value;
    }

    protected internal override void SetParent(IQCommandContainer container, QCommand command)
    {
      base.SetParent(container, command);
      this.OnParentMenuItemCollectionChanged(EventArgs.Empty);
    }

    protected override QCommandCollection CreateCommandCollection() => (QCommandCollection) new QMenuItemCollection((IQCommandContainer) null, (QCommand) this);

    protected override IQCommandContainer CreateChildCommandContainer() => (IQCommandContainer) new QFloatingMenu((QCommand) this, this.MenuItems);

    [Browsable(false)]
    public virtual bool MouseIsOverMenuItem
    {
      get
      {
        if (this.ParentContainer == null || !(this.ParentContainer is System.Windows.Forms.Control))
          return false;
        System.Windows.Forms.Control parentContainer = (System.Windows.Forms.Control) this.ParentContainer;
        return parentContainer.Visible && this.Bounds.Contains(parentContainer.PointToClient(System.Windows.Forms.Control.MousePosition));
      }
    }

    internal void EmptyCachedObjects() => this.m_bChecked = this.m_bChecked;

    public QMenuItem GetMenuItemWithShortcut(Keys shortcut)
    {
      if (this.MenuItems != null && this.MenuItems.Count > 0)
        return this.MenuItems.GetMenuItemWithShortcut(shortcut);
      return this.ShortcutKeys == shortcut ? this : (QMenuItem) null;
    }

    internal virtual Size CalculateIconSizes(Size preferredSize)
    {
      this.m_oIconContainer.CalculateIconSize(preferredSize);
      this.m_oDisabledIconContainer.CalculateIconSize(preferredSize);
      this.m_oCheckedIconContainer.CalculateIconSize(preferredSize);
      return this.UsedIcon == null ? Size.Empty : preferredSize;
    }

    internal void RaiseMenuItemMouseDown(QMenuMouseEventArgs e) => this.OnMenuItemMouseDown(e);

    internal void RaiseMenuItemMouseUp(QMenuMouseEventArgs e) => this.OnMenuItemMouseUp(e);

    internal void RaiseParentMenuItemCollectionChanged(EventArgs e) => this.OnParentMenuItemCollectionChanged(e);

    protected virtual void OnParentMenuItemCollectionChanged(EventArgs e) => this.m_oParentMenuItemCollectionChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oParentMenuItemCollectionChangedDelegate, (object) this, (object) e);

    internal void RaiseMenuItemsRequested(QMenuEventArgs e) => this.OnMenuItemsRequested(e);

    protected virtual void OnMenuItemMouseDown(QMenuMouseEventArgs e) => this.m_oMenuItemMouseDownDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemMouseDownDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemMouseUp(QMenuMouseEventArgs e) => this.m_oMenuItemMouseUpDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemMouseUpDelegate, (object) this, (object) e);

    protected virtual void OnMenuItemsRequested(QMenuEventArgs e) => this.m_oMenuItemsRequestedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemsRequestedDelegate, (object) this, (object) e);

    internal void RaiseMenuItemSelected(QMenuEventArgs e) => this.OnMenuItemSelected(e);

    protected virtual void OnMenuItemSelected(QMenuEventArgs e) => this.m_oMenuItemSelectedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemSelectedDelegate, (object) this, (object) e);

    internal void RaiseMenuItemActivating(QMenuCancelEventArgs e) => this.OnMenuItemActivating(e);

    protected virtual void OnMenuItemActivating(QMenuCancelEventArgs e) => this.m_oMenuItemActivatingDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemActivatingDelegate, (object) this, (object) e);

    internal void RaiseMenuItemActivated(QMenuEventArgs e) => this.OnMenuItemActivated(e);

    protected virtual void OnMenuItemActivated(QMenuEventArgs e) => this.m_oMenuItemActivatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuItemActivatedDelegate, (object) this, (object) e);

    internal void RaiseMenuShowing(QMenuCancelEventArgs e) => this.OnMenuShowing(e);

    protected virtual void OnMenuShowing(QMenuCancelEventArgs e) => this.m_oMenuShowingDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuShowingDelegate, (object) this, (object) e);

    internal void RaisePaintMenuItem(QPaintMenuItemEventArgs e) => this.OnPaintMenuItem(e);

    protected virtual void OnPaintMenuItem(QPaintMenuItemEventArgs e) => this.m_oPaintMenuItemDelegate = QWeakDelegate.InvokeDelegate(this.m_oPaintMenuItemDelegate, (object) this, (object) e);

    internal void RaiseMenuShowed(QMenuEventArgs e) => this.OnMenuShowed(e);

    protected virtual void OnMenuShowed(QMenuEventArgs e) => this.m_oMenuShowedDelegate = QWeakDelegate.InvokeDelegate(this.m_oMenuShowedDelegate, (object) this, (object) e);

    private void m_oControl_Disposed(object sender, EventArgs e)
    {
      if (this.m_oControl == null)
        return;
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.m_oControl_Disposed));
      this.m_oControl = (QCommandControlContainer) null;
    }
  }
}
