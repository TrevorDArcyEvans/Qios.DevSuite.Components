// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QComposite
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QComposite : 
    IQScrollablePart,
    IQPart,
    IQPartPaintListener,
    IQPartLayoutListener,
    IQWeakEventPublisher,
    IQManagedLayoutParent,
    IDisposable,
    IQItemColorHost,
    IQColorRetriever,
    IQHotkeyHandlerHost,
    IQNavigationHost,
    IQCompositeItemEventRaiser,
    IQCompositeEventRaiser,
    IQCompositeItemEventPublisher,
    IQItemStatesImplementation
  {
    private Control m_oCurrentFocusedChildControl;
    private IntPtr m_oPreviousFocusedChildHandle;
    private QScrollablePartData m_oScrollData;
    private Region m_oSavedPaintRegion;
    private QRegion m_oContentClipRegion;
    private IQCompositeItemEventRaiser m_oCachedParentItemEventRaiser;
    private object m_oCachedParentContainerItemEventRaiserInHierachy;
    private IQPart m_oParentPart;
    private QPartCollection m_oParentCollection;
    private bool m_bIsSystemPart;
    private QHotkeyHandler m_oHotkeyHandler;
    private QRelativePositions m_oOpeningItemRelativePosition;
    private Cursor m_oCursor;
    private bool m_bMouseOverComposite;
    private QTristateBool m_ePaintExpandedItemOverride;
    private string m_sPartName = nameof (QComposite);
    private IQItemColorHost m_oColorHost;
    private bool m_bShowHotkeyWindows;
    private IQPartPaintListener m_oPaintListener;
    private IQPartLayoutListener m_oLayoutListener;
    private QCompositeItemBase m_oCancelActivationForItem;
    private QTimerManager m_oTimerManager;
    private QCompositeItemBase m_oSelectedItem;
    private QCompositeItem m_oExpandedItem;
    private bool m_bHotkeyPrefixVisible;
    private IQCompositeContainer m_oParentContainer;
    private Keys m_oActivationKeys = Keys.Return;
    private Keys m_oActivationKeys2 = Keys.Space;
    private Keys m_oCloseKeys = Keys.Escape;
    private Keys m_oCloseKeysNavigation;
    private MouseButtons m_ePressedButtons = MouseButtons.Left;
    private Point m_oMouseDownPoint = new Point(-1, -1);
    private Point m_oMouseMovePoint = new Point(-1, -1);
    private string m_sUsedToolTipText;
    private string m_sToolTipText;
    private QItemStates m_eItemStates;
    private GraphicsPath m_oLastDrawnGraphicsPath;
    private bool m_bIsDisposed;
    private QColorScheme m_oColorScheme;
    private QColorScheme m_oChildCompositeColorScheme;
    private bool m_bPerformingLayout;
    private bool m_bWeakEventHandlers = true;
    private QCompositeConfiguration m_oConfiguration;
    private QCompositeConfiguration m_oChildCompositeConfiguration;
    private QCompositeWindowConfiguration m_oChildWindowConfiguration;
    private IQPartObjectPainter[] m_aObjectPainters;
    private QPartCollection m_oPartCollection;
    private QPartCalculatedProperties m_oCalculatedProperties;
    private ArrayList m_aCapturedMouseParts;
    private int m_iLayoutOrder;
    private QWeakDelegate m_oCompositeKeyPress;
    private QWeakDelegate m_oUsedToolTipTextChanged;
    private QWeakDelegate m_oSelectedItemChanged;
    private QWeakDelegate m_oItemPaintStageDelegate;
    private QWeakDelegate m_oChangeDelegate;
    private QWeakDelegate m_oColorsChangedDelegate;
    private QWeakDelegate m_oDisposedDelegate;
    private QWeakDelegate m_oItemActivating;
    private QWeakDelegate m_oItemActivated;
    private QWeakDelegate m_oItemSelected;
    private QWeakDelegate m_oItemExpanded;
    private QWeakDelegate m_oItemExpanding;
    private QWeakDelegate m_oItemCollapsed;
    private QWeakDelegate m_oItemCollapsing;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;

    internal QComposite(
      IQPart parentPart,
      QPartCollection useCollection,
      IQCompositeContainer parentContainer,
      QCompositeConfiguration configuration,
      QColorScheme colorScheme)
    {
      this.InternalConstruct(parentPart, useCollection, parentContainer, configuration, colorScheme);
    }

    private void InternalConstruct(
      IQPart parentPart,
      QPartCollection useCollection,
      IQCompositeContainer parentContainer,
      QCompositeConfiguration configuration,
      QColorScheme colorScheme)
    {
      this.m_oHotkeyHandler = new QHotkeyHandler((IQHotkeyHandlerHost) this);
      this.m_oParentContainer = parentContainer;
      this.m_oParentPart = parentPart;
      this.m_oLayoutListener = (IQPartLayoutListener) this;
      this.m_oPaintListener = (IQPartPaintListener) this;
      this.m_oTimerManager = new QTimerManager(50);
      this.m_oCalculatedProperties = new QPartCalculatedProperties((IQPart) this);
      this.m_aObjectPainters = this.CreatePainters((IQPartObjectPainter[]) null);
      this.m_oConfiguration = configuration != null ? configuration : this.CreateConfiguration();
      this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oChildCompositeConfiguration = this.CreateChildCompositeConfiguration();
      this.m_oChildWindowConfiguration = this.CreateChildWindowConfiguration();
      this.m_oChildCompositeColorScheme = this.CreateChildCompositeColorScheme();
      this.m_oColorSchemeColorsChangedEventHandler = new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oColorScheme = colorScheme != null ? colorScheme : this.CreateColorScheme();
      this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
      if (useCollection != null)
      {
        this.m_oPartCollection = useCollection;
        this.m_oPartCollection.SetParent((IQPart) this, false);
        this.m_oPartCollection.SetDisplayParent((IQManagedLayoutParent) this);
      }
      else
        this.m_oPartCollection = new QPartCollection((IQPart) this, (IQManagedLayoutParent) this);
    }

    ~QComposite() => this.Dispose(false);

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QComposite is about to handle navigation keys that are pressed")]
    public event QCompositeKeyboardCancelEventHandler CompositeKeyPress
    {
      add => this.m_oCompositeKeyPress = QWeakDelegate.Combine(this.m_oCompositeKeyPress, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCompositeKeyPress = QWeakDelegate.Remove(this.m_oCompositeKeyPress, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the UsedToolTip has changed.")]
    [QWeakEvent]
    public event QCompositeEventHandler UsedToolTipTextChanged
    {
      add => this.m_oUsedToolTipTextChanged = QWeakDelegate.Combine(this.m_oUsedToolTipTextChanged, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oUsedToolTipTextChanged = QWeakDelegate.Remove(this.m_oUsedToolTipTextChanged, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the selected item is changed")]
    [Category("QEvents")]
    public event QCompositeEventHandler SelectedItemChanged
    {
      add => this.m_oSelectedItemChanged = QWeakDelegate.Combine(this.m_oSelectedItemChanged, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oSelectedItemChanged = QWeakDelegate.Remove(this.m_oSelectedItemChanged, (Delegate) value);
    }

    [Description("Gets raised when an QCompositeItemBase is activating. This event can be cancelled.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeCancelEventHandler ItemActivating
    {
      add => this.m_oItemActivating = QWeakDelegate.Combine(this.m_oItemActivating, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivating = QWeakDelegate.Remove(this.m_oItemActivating, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when an QCompositeItemBase is activated.")]
    [QWeakEvent]
    public event QCompositeEventHandler ItemActivated
    {
      add => this.m_oItemActivated = QWeakDelegate.Combine(this.m_oItemActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivated = QWeakDelegate.Remove(this.m_oItemActivated, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when an QCompositeItemBase is selected.")]
    public event QCompositeEventHandler ItemSelected
    {
      add => this.m_oItemSelected = QWeakDelegate.Combine(this.m_oItemSelected, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemSelected = QWeakDelegate.Remove(this.m_oItemSelected, (Delegate) value);
    }

    [Description("Gets raised when the item is expanded")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QCompositeExpandedEventHandler ItemExpanded
    {
      add => this.m_oItemExpanded = QWeakDelegate.Combine(this.m_oItemExpanded, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanded = QWeakDelegate.Remove(this.m_oItemExpanded, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the child container is expanding")]
    [QWeakEvent]
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

    [Description("Gets raised when the item is collapsed")]
    [Category("QEvents")]
    [QWeakEvent]
    public event QCompositeEventHandler ItemCollapsed
    {
      add => this.m_oItemCollapsed = QWeakDelegate.Combine(this.m_oItemCollapsed, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemCollapsed = QWeakDelegate.Remove(this.m_oItemCollapsed, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the QComposite gets disposed.")]
    public event EventHandler Disposed
    {
      add => this.m_oDisposedDelegate = QWeakDelegate.Combine(this.m_oDisposedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oDisposedDelegate = QWeakDelegate.Remove(this.m_oDisposedDelegate, (Delegate) value);
    }

    [Description("Gets raised when the QComposite has visually changed")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeChangeEventHandler Change
    {
      add => this.m_oChangeDelegate = QWeakDelegate.Combine(this.m_oChangeDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oChangeDelegate = QWeakDelegate.Remove(this.m_oChangeDelegate, (Delegate) value);
    }

    [Description("Gets raised when the colors or the QColorScheme changes")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler ColorsChanged
    {
      add => this.m_oColorsChangedDelegate = QWeakDelegate.Combine(this.m_oColorsChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oColorsChangedDelegate = QWeakDelegate.Remove(this.m_oColorsChangedDelegate, (Delegate) value);
    }

    [Description("Gets raised when a painting stage of an QCompositeItemBase is complete")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositePaintStageEventHandler PaintItem
    {
      add => this.m_oItemPaintStageDelegate = QWeakDelegate.Combine(this.m_oItemPaintStageDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemPaintStageDelegate = QWeakDelegate.Remove(this.m_oItemPaintStageDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IQItemColorHost ColorHost
    {
      get => this.m_oColorHost == null ? (IQItemColorHost) this : this.m_oColorHost;
      set => this.m_oColorHost = value;
    }

    [DefaultValue(null)]
    [Description("Gets or sets the mouse cursor for this QComposite")]
    [Category("QBehavior")]
    public Cursor Cursor
    {
      get => this.m_oCursor;
      set => this.m_oCursor = value;
    }

    [Browsable(false)]
    [Description("Gets the QItemStates for the QComposite")]
    public QItemStates ItemState => this.m_eItemStates;

    protected virtual QItemStates GetState(
      QItemStates checkForStates,
      bool includeParentStates)
    {
      return this.m_eItemStates & checkForStates;
    }

    protected virtual QItemStates HasStatesDefined(
      QItemStates checkForStates,
      QTristateBool stateValue)
    {
      QItemStates qitemStates = checkForStates;
      if (this.HasHotState != stateValue)
        qitemStates &= ~QItemStates.Hot;
      if (this.HasPressedState != stateValue)
        qitemStates &= ~QItemStates.Pressed;
      if (stateValue != QTristateBool.False)
        qitemStates &= ~QItemStates.Checked;
      if (stateValue == QTristateBool.False)
        qitemStates &= ~QItemStates.Disabled;
      return qitemStates;
    }

    public virtual bool HasStatesDefined(QItemStates states) => this.HasStatesDefined(states, QTristateBool.True) == states;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual QTristateBool HasHotState => QTristateBool.Undefined;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual QTristateBool HasPressedState => QTristateBool.Undefined;

    public virtual void AdjustState(QItemStates adjustment, bool setValue)
    {
      if (QItemStatesHelper.IsState(this.m_eItemStates, adjustment) == setValue)
        return;
      this.m_eItemStates = QItemStatesHelper.AdjustState(this.m_eItemStates, adjustment, setValue);
      this.HandleChildObjectChanged(false);
    }

    [Browsable(false)]
    public bool IsFloating => this.ParentControl is QCompositeWindow;

    [Browsable(false)]
    public bool ParentControlIsVisible => this.ParentControl != null && this.ParentControl.Visible;

    [Category("QBehavior")]
    [Description("Gets or sets whether this item is Enabled")]
    [DefaultValue(true)]
    public bool Enabled
    {
      get => !QItemStatesHelper.IsDisabled(this.m_eItemStates);
      set
      {
        bool enabled = this.Enabled;
        this.AdjustState(QItemStates.Disabled, !value);
        if (enabled == this.Enabled)
          return;
        QCompositeHelper.NotifyChildPartEnabledChanged((IQPart) this);
      }
    }

    [Category("QAppearance")]
    [Localizable(true)]
    [DefaultValue(null)]
    [Description("Gets or sets the ToolTipText. This must contain Xml as used with QMarkupText. The ToolTip, see ToolTipConfiguration, must be enabled for this to show.")]
    public virtual string ToolTipText
    {
      get => this.m_sToolTipText;
      set
      {
        if (this.m_sToolTipText == value)
          return;
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sToolTipText = value;
      }
    }

    [Browsable(false)]
    public string UsedToolTipText => this.m_sUsedToolTipText != null ? this.m_sUsedToolTipText : this.m_sToolTipText;

    [Browsable(false)]
    public bool IsDisposed => this.m_bIsDisposed;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    [Description("Gets the collection of QCompositeItems of this QComposite. This are the items that can be desigend.")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QPartCollection Items => this.m_oPartCollection;

    public bool ShouldSerializeConfiguration() => this.Configuration != null && !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration()
    {
      if (this.Configuration == null)
        return;
      this.Configuration.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QComposite.")]
    [Category("QAppearance")]
    public QCompositeConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= new EventHandler(this.Configuration_ConfigurationChanged);
        this.m_oConfiguration = value;
        if (this.m_oConfiguration != null)
        {
          this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
          if (this.m_oScrollData != null)
            this.m_oScrollData.Configuration = this.m_oConfiguration.GetScrollConfiguration((IQPart) this);
        }
        this.Configuration_ConfigurationChanged((object) null, EventArgs.Empty);
      }
    }

    public bool ShouldSerializeChildWindowConfiguration() => !this.m_oChildWindowConfiguration.IsSetToDefaultValues();

    public void ResetChildWindowConfiguration() => this.m_oChildWindowConfiguration.SetToDefaultValues();

    [Category("QAppearance")]
    [Description("Contains the ChildWindowConfiguration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeWindowConfiguration ChildWindowConfiguration
    {
      get => this.m_oChildWindowConfiguration;
      set => this.m_oChildWindowConfiguration = value;
    }

    public bool ShouldSerializeChildCompositeConfiguration() => this.ChildCompositeConfiguration != null && !this.ChildCompositeConfiguration.IsSetToDefaultValues();

    public void ResetChildCompositeConfiguration()
    {
      if (this.ChildCompositeConfiguration == null)
        return;
      this.ChildCompositeConfiguration.SetToDefaultValues();
    }

    [Category("QAppearance")]
    [Description("Contains the ChildCompositeConfiguration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeConfiguration ChildCompositeConfiguration
    {
      get => this.m_oChildCompositeConfiguration;
      set => this.m_oChildCompositeConfiguration = value;
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [Description("Gets or sets the QColorScheme that is used")]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
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
        this.m_oPartCollection.ResetPartParents(false);
        if (this.m_oColorScheme == null || this.IsDisposed)
          return;
        this.HandleChildObjectChanged(true);
      }
    }

    public bool ShouldSerializeChildCompositeColorScheme() => this.ChildCompositeColorScheme.ShouldSerialize();

    public void ResetChildCompositeColorScheme() => this.ChildCompositeColorScheme.Reset();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used for child composites")]
    public virtual QColorScheme ChildCompositeColorScheme
    {
      get => this.m_oChildCompositeColorScheme;
      set => this.m_oChildCompositeColorScheme = value;
    }

    protected virtual QScrollablePartData CreateScrollData() => new QScrollablePartData((IQScrollablePart) this, this.Configuration != null ? this.Configuration.GetScrollConfiguration((IQPart) this) : (QCompositeScrollConfiguration) null);

    protected virtual void SynchronizeScrollData()
    {
      QCompositeScrollConfiguration scrollConfiguration = this.Configuration != null ? this.Configuration.GetScrollConfiguration((IQPart) this) : (QCompositeScrollConfiguration) null;
      if (scrollConfiguration != null && (scrollConfiguration.ScrollHorizontal != QCompositeScrollVisibility.None || scrollConfiguration.ScrollVertical != QCompositeScrollVisibility.None))
      {
        if (this.m_oScrollData != null)
          return;
        this.m_oScrollData = this.CreateScrollData();
      }
      else
        this.m_oScrollData = (QScrollablePartData) null;
    }

    protected virtual void ClearCachedParents()
    {
      this.m_oCachedParentItemEventRaiser = (IQCompositeItemEventRaiser) null;
      this.m_oCachedParentContainerItemEventRaiserInHierachy = (object) null;
    }

    protected virtual IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) new QPartShapePainter());
      currentPainters = QPartObjectPainter.AddObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) new QCompositeIconBackgroundPainter());
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Foreground, (IQPartObjectPainter) new QPartShapePainter());
      return currentPainters;
    }

    protected virtual QColorScheme CreateColorScheme() => new QColorScheme();

    protected virtual QColorScheme CreateChildCompositeColorScheme() => new QColorScheme();

    protected virtual QCompositeConfiguration CreateConfiguration() => new QCompositeConfiguration();

    protected virtual QCompositeWindowConfiguration CreateChildWindowConfiguration() => new QCompositeWindowConfiguration();

    protected virtual QCompositeConfiguration CreateChildCompositeConfiguration() => new QCompositeConfiguration();

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bIsDisposed)
        return;
      if (disposing && this.m_oColorScheme != null && !this.m_oColorScheme.IsDisposed)
      {
        this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
        this.m_oColorScheme.Dispose();
      }
      this.m_bIsDisposed = true;
      this.OnDisposed(EventArgs.Empty);
    }

    internal Control CurrentFocusedChildControl => this.m_oCurrentFocusedChildControl;

    internal QRelativePositions OpeningItemRelativePosition
    {
      get => this.m_oOpeningItemRelativePosition;
      set => this.m_oOpeningItemRelativePosition = value;
    }

    internal QCompositeItemBase CancelActivationForItem
    {
      get => this.m_oCancelActivationForItem;
      set => this.m_oCancelActivationForItem = value;
    }

    internal bool AllowMultipleHotSiblings => false;

    internal bool AllowMultiplePressedSiblings => false;

    internal bool UsedAutoExpand(QCompositeItem item) => this.m_oExpandedItem == null ? this.AutoExpand((object) item) : this.AutoChangeExpand((object) item);

    internal QCompositeExpandDirection GetExpandDirection(
      QCompositeItem item)
    {
      QCompositeExpandDirection expandDirection = this.Configuration.GetExpandDirection((IQPart) this);
      if (item != null && item.Configuration.ExpandDirection != QCompositeExpandDirection.Inherited)
        return item.Configuration.ExpandDirection;
      return expandDirection != QCompositeExpandDirection.Inherited ? expandDirection : QCompositeExpandDirection.Right;
    }

    internal QCompositeResizeBorder GetResizeBorder(Point location)
    {
      QCompositeResizeItem qcompositeResizeItem = (QCompositeResizeItem) null;
      for (IQPart qpart = QPartHelper.GetItemAtPointRecursive((IQPart) this, location); qpart != null && qcompositeResizeItem == null; qpart = qpart.ParentPart)
        qcompositeResizeItem = qpart as QCompositeResizeItem;
      return qcompositeResizeItem != null ? qcompositeResizeItem.Configuration.ResizeBorder : QCompositeResizeBorder.None;
    }

    internal bool PaintExpandedItem(object item)
    {
      if (this.m_ePaintExpandedItemOverride != QTristateBool.Undefined)
        return this.m_ePaintExpandedItemOverride == QTristateBool.True;
      return this.m_oSelectedItem == null || this.PaintExpandedChildWhenHot(item);
    }

    internal QTristateBool PaintExpandedItemOverride
    {
      get => this.m_ePaintExpandedItemOverride;
      set => this.m_ePaintExpandedItemOverride = value;
    }

    internal bool ExpandBehaviorIsSet(QCompositeExpandBehavior behavior, object item)
    {
      QCompositeItem ancestorAsCompositeItem = QCompositeHelper.GetItemOrAncestorAsCompositeItem(item, (IQPart) this);
      return QCompositeExpandBehaviorHelper.IsSet(behavior, this.Configuration.GetExpandBehavior((IQPart) this), ancestorAsCompositeItem != null ? ancestorAsCompositeItem.Configuration.ExpandBehavior : QCompositeExpandBehavior.None);
    }

    internal bool AutoExpand(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.AutoExpand, item);

    internal bool AutoChangeExpand(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.AutoChangeExpand, item);

    internal bool PaintExpandedChildWhenHot(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.PaintExpandedChildWhenHot, item);

    internal bool ExpandOnNavigationKeys(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.ExpandOnNavigationKeys, item);

    internal bool PositionOutsideComposite(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.PositionOutsideComposite, item);

    internal bool CloseOnNavigationKeys(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.CloseOnNavigationKeys, item);

    internal bool CloseExpandedItemOnClick(object item) => this.ExpandBehaviorIsSet(QCompositeExpandBehavior.CloseExpandedItemOnClick, item);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QToolTipConfiguration ToolTipConfiguration
    {
      get
      {
        if (this.m_oParentContainer.Control is QControl)
          return ((QControl) this.m_oParentContainer.Control).ToolTipConfiguration;
        return this.m_oParentContainer.Control is QContainerControl ? ((QContainerControl) this.m_oParentContainer.Control).ToolTipConfiguration : (QToolTipConfiguration) null;
      }
    }

    internal QTimerManager TimerManager => this.m_oTimerManager;

    internal bool MouseOverComposite => this.m_bMouseOverComposite;

    [Browsable(false)]
    public Control ParentControl => this.m_oParentContainer == null ? (Control) null : this.m_oParentContainer.Control;

    [Browsable(false)]
    public QCompositeItemBase ParentItem => this.m_oParentPart as QCompositeItemBase;

    [Browsable(false)]
    public QComposite ParentComposite => this.ParentItem == null ? (QComposite) null : this.ParentItem.Composite;

    internal IQCompositeContainer ParentContainer => this.m_oParentContainer;

    internal IQManagedLayoutParent ManagedLayoutParent => this.m_oParentContainer as IQManagedLayoutParent;

    private IQCompositeItemEventRaiser ParentContainerItemEventRaiser => this.m_oParentContainer as IQCompositeItemEventRaiser;

    private IQCompositeItemEventRaiser ParentItemEventRaiser
    {
      get
      {
        if (this.m_oCachedParentItemEventRaiser == null)
          this.m_oCachedParentItemEventRaiser = QCompositeHelper.FindParentItemEventRaiser((IQPart) this);
        return this.m_oCachedParentItemEventRaiser;
      }
    }

    private bool ParentContainerItemEventRaiserInHierachy
    {
      get
      {
        if (this.m_oCachedParentContainerItemEventRaiserInHierachy == null)
        {
          if (this.ParentContainerItemEventRaiser != null && this.ParentItemEventRaiser != null)
          {
            IQPart qpart = this.ParentItemEventRaiser as IQPart;
            while (qpart != null && qpart != this.ParentContainerItemEventRaiser)
              qpart = qpart.ParentPart;
            this.m_oCachedParentContainerItemEventRaiserInHierachy = (object) (qpart == this.ParentContainerItemEventRaiser);
          }
          else
            this.m_oCachedParentContainerItemEventRaiserInHierachy = (object) false;
        }
        return (bool) this.m_oCachedParentContainerItemEventRaiserInHierachy;
      }
    }

    internal IQCompositeEventRaiser CompositeEventRaiser => this.m_oParentContainer as IQCompositeEventRaiser;

    internal IQCompositeEventRaiser ParentCompositeEventRaiser => (IQCompositeEventRaiser) this.ParentComposite;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeItemBase SelectedItem
    {
      get => this.m_oSelectedItem;
      set => this.SelectItem(value, QCompositeActivationType.None, true);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeItem ExpandedItem => this.m_oExpandedItem;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal bool HotkeyPrefixVisible
    {
      get
      {
        switch (this.Configuration.GetHotkeyPrefixVisibilityType((IQPart) this))
        {
          case QHotkeyVisibilityType.Always:
            return true;
          case QHotkeyVisibilityType.Never:
            return false;
          case QHotkeyVisibilityType.WhenAltIsPressed:
            return this.m_bHotkeyPrefixVisible;
          default:
            return false;
        }
      }
      set
      {
        if (this.m_bHotkeyPrefixVisible == value)
          return;
        bool hotkeyPrefixVisible = this.HotkeyPrefixVisible;
        this.m_bHotkeyPrefixVisible = value;
        if (this.HotkeyPrefixVisible == hotkeyPrefixVisible)
          return;
        this.HandleChildObjectChanged(false);
      }
    }

    internal Keys CloseKeysNavigation
    {
      get => this.m_oCloseKeysNavigation;
      set => this.m_oCloseKeysNavigation = value;
    }

    private QCompositeItemBase GetParentWithPressedState(QCompositeItemBase item)
    {
      if (item == null)
        return (QCompositeItemBase) null;
      if (item.HasPressedState == QTristateBool.True)
        return item;
      for (IQPart parentPart = item.ParentPart; parentPart != null; parentPart = parentPart.ParentPart)
      {
        item = parentPart as QCompositeItemBase;
        if (item != null && item.HasPressedState == QTristateBool.True)
          return item;
      }
      return (QCompositeItemBase) null;
    }

    private QCompositeItem GetParentWithHotState(QCompositeItem item)
    {
      if (item == null)
        return (QCompositeItem) null;
      if (item.HasHotState == QTristateBool.True)
        return item;
      for (IQPart parentPart = item.ParentPart; parentPart != null; parentPart = parentPart.ParentPart)
      {
        item = parentPart as QCompositeItem;
        if (item != null && item.HasHotState == QTristateBool.True)
          return item;
      }
      return (QCompositeItem) null;
    }

    internal bool ShouldShowHotkeyWindows() => this.Configuration.GetHotkeyWindowShowBehavior((IQPart) this) == QHotkeyWindowShowBehavior.Automatic;

    protected internal virtual bool ShowHotkeyWindows
    {
      get => this.m_bShowHotkeyWindows;
      set
      {
        this.m_bShowHotkeyWindows = value;
        this.m_oHotkeyHandler.ShouldShowHotkeyWindows = value && this.ShouldShowHotkeyWindows();
      }
    }

    internal bool IsProcessingHotkeys => this.m_oHotkeyHandler.IsProcessing;

    internal void StartProcessingHotkeys(bool selectFirstItem) => this.m_oHotkeyHandler.StartProcessing(selectFirstItem);

    internal void StopProcessingHotkeys(bool resetCurrentSelectedItem) => this.m_oHotkeyHandler.StopProcessing(resetCurrentSelectedItem);

    protected internal virtual void FocusChildControl(Control control)
    {
      this.m_oPreviousFocusedChildHandle = NativeMethods.GetFocus();
      QControlHelper.GetFirstControlFromHandle(this.m_oPreviousFocusedChildHandle);
      this.m_oCurrentFocusedChildControl = control;
      this.m_oCurrentFocusedChildControl.Focus();
      this.m_oCurrentFocusedChildControl.LostFocus += new EventHandler(this.CurrentFocusedChildControl_LostFocus);
    }

    protected internal virtual void UnfocusCurrentChildControl()
    {
      if (this.m_oCurrentFocusedChildControl != null && this.m_oPreviousFocusedChildHandle != IntPtr.Zero)
      {
        QControlHelper.GetFirstControlFromHandle(this.m_oPreviousFocusedChildHandle);
        NativeMethods.SetFocus(this.m_oPreviousFocusedChildHandle);
      }
      this.m_oPreviousFocusedChildHandle = IntPtr.Zero;
      this.m_oCurrentFocusedChildControl = (Control) null;
    }

    protected internal virtual void ClearCurrentFocusedChildControl()
    {
      if (this.m_oCurrentFocusedChildControl != null)
      {
        this.m_oCurrentFocusedChildControl.LostFocus -= new EventHandler(this.CurrentFocusedChildControl_LostFocus);
        this.m_oCurrentFocusedChildControl = (Control) null;
      }
      this.m_oPreviousFocusedChildHandle = IntPtr.Zero;
    }

    private void CurrentFocusedChildControl_LostFocus(object sender, EventArgs e)
    {
      Control control = sender as Control;
      if (control == this.m_oCurrentFocusedChildControl)
        this.ClearCurrentFocusedChildControl();
      else
        control.LostFocus -= new EventHandler(this.CurrentFocusedChildControl_LostFocus);
    }

    internal void CaptureMouse(IQMouseHandler mouseHandler)
    {
      if (this.m_aCapturedMouseParts == null)
        this.m_aCapturedMouseParts = new ArrayList();
      this.ParentContainer.Control.Capture = true;
      if (this.m_aCapturedMouseParts.Contains((object) mouseHandler))
        return;
      this.m_aCapturedMouseParts.Add((object) mouseHandler);
    }

    internal void HandleMouseWheel(MouseEventArgs e)
    {
      IQPart qpart = QPartHelper.GetItemAtPointRecursive((IQPart) this, new Point(e.X, e.Y));
      IQScrollablePart qscrollablePart;
      for (qscrollablePart = (IQScrollablePart) null; qpart != null && (qscrollablePart == null || qscrollablePart.ScrollData == null); qpart = qpart.ParentPart)
        qscrollablePart = qpart as IQScrollablePart;
      if (qscrollablePart == null || qscrollablePart.ScrollData == null || qscrollablePart.ScrollData.IsAtVerticalStart && qscrollablePart.ScrollData.IsAtVerticalEnd)
        return;
      int yValue = (int) ((double) (e.Delta * SystemInformation.MouseWheelScrollLines / 120) * (double) Control.DefaultFont.Size);
      qscrollablePart.ScrollData.ScrollVertical(yValue, QScrollablePartMethod.Relative, false);
    }

    internal void HandleMouseMove(MouseEventArgs e)
    {
      if (this.m_aCapturedMouseParts != null && this.m_aCapturedMouseParts.Count > 0)
      {
        for (int index = 0; index < this.m_aCapturedMouseParts.Count; ++index)
          (this.m_aCapturedMouseParts[index] as IQMouseHandler).HandleMouseMove(e);
      }
      else
      {
        Point point = new Point(e.X, e.Y);
        if (this.m_oMouseMovePoint == point)
          return;
        this.m_oMouseMovePoint = point;
        if (this.m_oScrollData != null)
          this.m_oScrollData.HandleMouseMove(e);
        if (this.Configuration.GetPressedBehavior((IQPart) this) == QCompositePressedBehaviour.MovePressedItem && Control.MouseButtons == this.m_ePressedButtons)
          this.m_oMouseDownPoint = new Point(0, 0);
        bool siblingHot = false;
        bool siblingPressed = false;
        QCompositeHelper.QCompositeHelperStorage storage = new QCompositeHelper.QCompositeHelperStorage();
        storage.MouseDownPoint = this.m_oMouseDownPoint;
        storage.PressedBehaviour = this.Configuration.GetPressedBehavior((IQPart) this);
        QCompositeHelper.HandleMouseMove(this, (IQPart) this, e, storage, ref siblingHot, ref siblingPressed);
        if (storage.ToolTipItem != null && !QItemStatesHelper.IsExpanded(storage.ToolTipItem.ItemState))
          this.PutUsedToolTipText(storage.ToolTipItem.ToolTipText);
        else
          this.PutUsedToolTipText((string) null);
        if (storage.CursorItem != null)
        {
          if (!(this.m_oParentContainer.Cursor != storage.CursorItem.Cursor))
            return;
          this.m_oParentContainer.Cursor = storage.CursorItem.Cursor;
        }
        else
        {
          if (!(this.m_oParentContainer.Cursor != Cursors.Default))
            return;
          this.m_oParentContainer.Cursor = Cursors.Default;
        }
      }
    }

    internal void HandleMouseEnter(EventArgs e)
    {
      this.m_bMouseOverComposite = true;
      if (this.m_oMouseDownPoint.X >= 0 && this.m_oMouseDownPoint.Y >= 0 && Control.MouseButtons != this.m_ePressedButtons)
        this.m_oMouseDownPoint = new Point(int.MinValue, int.MinValue);
      if (this.m_oExpandedItem == null)
        return;
      this.m_oExpandedItem.HandleChange(false);
    }

    internal void HandleMouseLeave(EventArgs e)
    {
      this.m_bMouseOverComposite = false;
      bool siblingHot = false;
      bool siblingPressed = false;
      MouseEventArgs e1 = new MouseEventArgs(Control.MouseButtons, 0, int.MinValue, int.MinValue, 0);
      if (this.m_oScrollData != null)
        this.m_oScrollData.HandleMouseMove(e1);
      QCompositeHelper.HandleMouseMove(this, (IQPart) this, e1, new QCompositeHelper.QCompositeHelperStorage()
      {
        MouseDownPoint = this.m_oMouseDownPoint,
        PressedBehaviour = this.Configuration.GetPressedBehavior((IQPart) this)
      }, ref siblingHot, ref siblingPressed);
      this.PutUsedToolTipText((string) null);
      if (this.m_oParentContainer.Cursor != Cursors.Default)
        this.m_oParentContainer.Cursor = Cursors.Default;
      this.HandleChildObjectChanged(false);
    }

    internal void HandleMouseDown(MouseEventArgs e)
    {
      NativeMethods.ReleaseCapture();
      if (this.ParentControl != null && this.ParentControl.CanFocus && this.ParentControl.CanSelect && !this.ParentControl.Focused)
        this.ParentControl.Focus();
      if (this.m_oScrollData != null)
        this.m_oScrollData.HandleMouseDown(e);
      if (this.m_ePressedButtons != e.Button)
        return;
      this.m_oMouseDownPoint = new Point(e.X, e.Y);
      bool siblingPressed = false;
      QCompositeHelper.HandleMouseDown(this, (IQPart) this, e, ref siblingPressed);
    }

    internal void HandleMouseUp(MouseEventArgs e)
    {
      this.m_oMouseDownPoint = new Point(-1, -1);
      if (this.m_aCapturedMouseParts != null && this.m_aCapturedMouseParts.Count > 0)
      {
        for (int index = 0; index < this.m_aCapturedMouseParts.Count; ++index)
          (this.m_aCapturedMouseParts[index] as IQMouseHandler).HandleMouseUp(e);
        this.m_aCapturedMouseParts.Clear();
        NativeMethods.ReleaseCapture();
      }
      else
      {
        if (this.m_oScrollData != null)
          this.m_oScrollData.HandleMouseUp(e);
        if (this.m_ePressedButtons != e.Button)
          return;
        if (!QCompositeHelper.HandleMouseUp(this, (IQPart) this, e))
          this.CollapseExpandedItem(QCompositeActivationType.Mouse);
        this.CancelActivationForItem = (QCompositeItemBase) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GraphicsPath LastDrawnGraphicsPath => this.m_oLastDrawnGraphicsPath;

    protected void PutLastDrawnGraphicsPath(GraphicsPath path)
    {
      if (this.m_oLastDrawnGraphicsPath == path)
        return;
      if (this.m_oLastDrawnGraphicsPath != null)
        this.m_oLastDrawnGraphicsPath.Dispose();
      this.m_oLastDrawnGraphicsPath = path;
    }

    internal void PutUsedToolTipText(string text)
    {
      if (this.UsedToolTipText == text)
        return;
      this.m_sUsedToolTipText = !(this.m_sToolTipText == text) ? text : (string) null;
      this.OnUsedToolTipTextChanged(new QCompositeEventArgs(this, (QCompositeItemBase) null, QCompositeActivationType.None));
    }

    private void Configuration_ConfigurationChanged(object sender, EventArgs e) => this.HandleChildObjectChanged(true);

    private void ColorScheme_ColorsChanged(object sender, EventArgs e)
    {
      this.OnColorsChanged(EventArgs.Empty);
      this.HandleChildObjectChanged(true);
    }

    public void SetObjectPainter(QPartPaintLayer paintLayer, IQPartObjectPainter painter) => this.m_aObjectPainters = QPartObjectPainter.SetObjectPainter(this.m_aObjectPainters, paintLayer, painter);

    public Rectangle RectangleToClient(Rectangle rectangle) => this.ParentControl == null ? rectangle : this.ParentControl.RectangleToClient(rectangle);

    public Rectangle RectangleToScreen(Rectangle rectangle) => this.ParentControl == null ? rectangle : this.ParentControl.RectangleToScreen(rectangle);

    public Point PointToClient(Point point) => this.ParentControl == null ? point : this.ParentControl.PointToClient(point);

    public Point PointToScreen(Point point) => this.ParentControl == null ? point : this.ParentControl.PointToScreen(point);

    protected internal virtual bool HandleKeyDown(
      Keys keys,
      Control destinationControl,
      Message message,
      bool force)
    {
      if (this.ExpandedItem != null)
        return this.ExpandedItem.ChildComposite.HandleKeyDown(keys, destinationControl, message, force);
      if (this.m_oCurrentFocusedChildControl != null)
      {
        if (keys != Keys.Escape)
          return false;
        this.UnfocusCurrentChildControl();
        return true;
      }
      if (this.ParentContainer.ContainsControl(destinationControl))
        return false;
      if (this.IsMenuKey(keys))
        this.HotkeyPrefixVisible = true;
      else if (this.ParentControlIsVisible)
      {
        if (this.IsExpansionKey(keys))
        {
          this.HandleExpansionKey(keys);
          return true;
        }
        if (this.IsActivationKey(keys))
        {
          this.HandleActivationKey(keys);
          return true;
        }
        if (this.IsDeactivationKey(keys))
        {
          this.HandleDeactivationKey(keys);
          return true;
        }
        if (this.IsNavigationKey(keys))
        {
          this.HandleNavigationKey(keys);
          return true;
        }
        if (!this.IsHotkey(keys))
          return false;
        this.HandleHotkey(keys);
        return true;
      }
      return false;
    }

    protected internal virtual bool HandleKeyUp(
      Keys keys,
      Control destinationControl,
      Message message)
    {
      if (this.m_oExpandedItem != null)
        this.m_oExpandedItem.ChildComposite.HandleKeyUp(keys, destinationControl, message);
      if (!this.IsFloating && this.IsMenuKey(keys))
        this.HotkeyPrefixVisible = false;
      return false;
    }

    internal bool IsActivationKey(Keys keys) => keys == this.m_oActivationKeys || keys == this.m_oActivationKeys2;

    internal bool IsExpansionKey(Keys keys)
    {
      if (!(this.SelectedItem is QCompositeItem selectedItem) || !selectedItem.CanExpand || !this.ExpandOnNavigationKeys((object) selectedItem))
        return false;
      QCompositeExpandDirection expandDirection = this.GetExpandDirection(selectedItem);
      if (expandDirection == QCompositeExpandDirection.Right && keys == Keys.Right || expandDirection == QCompositeExpandDirection.Left && keys == Keys.Left || expandDirection == QCompositeExpandDirection.Up && keys == Keys.Up)
        return true;
      return expandDirection == QCompositeExpandDirection.Down && keys == Keys.Down;
    }

    internal bool IsDeactivationKey(Keys keys) => keys == this.m_oCloseKeys || keys == this.m_oCloseKeysNavigation && this.ParentContainer.CanClose;

    internal bool IsNavigationKey(Keys keys) => keys == Keys.Left || keys == Keys.Right || keys == Keys.Up || keys == Keys.Down;

    internal bool IsMenuKey(Keys key) => key == Keys.Menu;

    internal bool IsAltWithHotkey(Keys key) => (key & Keys.Alt) == Keys.Alt && this.IsHotkey(key & ~Keys.Alt);

    internal bool IsHotkey(Keys keys) => QHotkeyHelper.ConvertToChar(keys) != char.MinValue;

    internal bool IsShortcutKey(Keys key) => Enum.IsDefined(typeof (Shortcut), (object) (int) key);

    internal bool HandleActivationKey(Keys keys) => this.SelectedItem != null && this.SelectedItem.Activate(QCompositeItemActivationOptions.Automatic, QCompositeActivationType.Keyboard);

    internal bool HandleDeactivationKey(Keys keys)
    {
      if (this.ParentContainer == null || !this.ParentContainer.CanClose)
        return false;
      this.ParentContainer.Close(QCompositeActivationType.Keyboard);
      return true;
    }

    internal bool HandleExpansionKey(Keys keys) => this.m_oSelectedItem is QCompositeItem oSelectedItem && this.ExpandItem(oSelectedItem, QCompositeActivationType.Keyboard);

    internal bool HandleNavigationKey(Keys keys)
    {
      switch (keys)
      {
        case Keys.End:
          return this.SelectFirstItem(QCompositeActivationType.Keyboard, false);
        case Keys.Home:
          return this.SelectFirstItem(QCompositeActivationType.Keyboard, true);
        case Keys.Left:
        case Keys.Up:
          return this.SelectNextItem(QCompositeActivationType.Keyboard, false, true);
        case Keys.Right:
        case Keys.Down:
          return this.SelectNextItem(QCompositeActivationType.Keyboard, true, true);
        default:
          return false;
      }
    }

    internal bool HandleHotkey(Keys key)
    {
      if (!this.m_oHotkeyHandler.HandleHotkey(key))
        return false;
      if (this.m_oHotkeyHandler.IsProcessing || this.ExpandedItem != null)
      {
        this.ShowHotkeyWindows = true;
        if (this.ExpandedItem != null)
        {
          this.ExpandedItem.ChildComposite.ShowHotkeyWindows = true;
          this.ExpandedItem.ChildComposite.StartProcessingHotkeys(false);
        }
      }
      return true;
    }

    internal bool HandleShortcutKey(Keys key, out bool suppressToSystem) => QCompositeHelper.HandleShortcutKey((IQPartCollection) new QPartArray(new IQPart[1]
    {
      (IQPart) this
    }), key, out suppressToSystem);

    public bool SelectNextItem(
      QCompositeActivationType activationType,
      bool forward,
      bool loopAround)
    {
      QCompositeItemBase orderedRecursive = QCompositeHelper.GetNextItemOrderedRecursive((IQPart) this, (IQPart) this.SelectedItem, new QCompositeNavigationFilter(QCompositeNavigationFilterOptions.Visible | QCompositeNavigationFilterOptions.HasPressedState), forward, loopAround);
      if (orderedRecursive == null)
        return false;
      this.SelectItem(orderedRecursive, activationType, true);
      return true;
    }

    public bool SelectFirstItem(QCompositeActivationType activationType, bool first)
    {
      QCompositeItemBase orderedRecursive = QCompositeHelper.GetNextItemOrderedRecursive((IQPart) this, (IQPart) null, new QCompositeNavigationFilter(QCompositeNavigationFilterOptions.Visible | QCompositeNavigationFilterOptions.HasPressedState), first, false);
      if (orderedRecursive == null)
        return false;
      this.SelectItem(orderedRecursive, activationType, true);
      return true;
    }

    internal void HandleWindowClosing(CancelEventArgs e)
    {
      if (this.ParentComposite == null)
        return;
      this.ParentComposite.HandleChildWindowClosing(e);
    }

    internal void HandleWindowClosed()
    {
      this.HotkeyPrefixVisible = false;
      this.ClearCurrentFocusedChildControl();
      if (!this.m_oHotkeyHandler.IsIdle)
        this.m_oHotkeyHandler.StopProcessing();
      if (this.ParentComposite == null)
        return;
      this.ParentComposite.HandleChildWindowClosed();
    }

    internal void HandleChildWindowClosing(CancelEventArgs e)
    {
      QCompositeCancelEventArgs e1 = new QCompositeCancelEventArgs(this, (QCompositeItemBase) this.m_oExpandedItem, QCompositeActivationType.None, false);
      ((IQCompositeItemEventRaiser) this.m_oExpandedItem).RaiseItemCollapsing(e1);
      if (!e1.Cancel)
        return;
      e.Cancel = true;
    }

    internal void HandleChildWindowClosed()
    {
      QCompositeItem oExpandedItem = this.m_oExpandedItem;
      QCompositeActivationType lastCloseType = this.m_oExpandedItem.ChildWindow.LastCloseType;
      oExpandedItem.AdjustState(QItemStates.Expanded, false, lastCloseType);
      ((IQCompositeItemEventRaiser) this.m_oExpandedItem).RaiseItemCollapsed(new QCompositeEventArgs(this, (QCompositeItemBase) this.m_oExpandedItem, lastCloseType));
      this.m_oExpandedItem = (QCompositeItem) null;
      if (QCompositeHelper.IsKeyboardActivationType(lastCloseType) && this.ParentContainer.IsFocused && this.SelectedItem == null)
        this.SelectItem((QCompositeItemBase) oExpandedItem, lastCloseType, true);
      if (!QCompositeHelper.IsKeyboardActivationType(lastCloseType) || !this.ShowHotkeyWindows)
        return;
      this.m_oHotkeyHandler.ResumeProcessing(false);
    }

    public void SelectItem(
      QCompositeItemBase item,
      QCompositeActivationType activationType,
      bool resetStateOfCurrentItem)
    {
      if (item == this.m_oSelectedItem)
        return;
      if (resetStateOfCurrentItem && this.m_oSelectedItem != null && QItemStatesHelper.IsHot(this.m_oSelectedItem.ItemState))
        this.m_oSelectedItem.AdjustState(QItemStates.Hot, false, activationType);
      QCompositeItemBase oSelectedItem = this.m_oSelectedItem;
      this.m_oSelectedItem = (QCompositeItemBase) null;
      if (item != null && item.IsVisible(QPartVisibilitySelectionTypes.IncludeAll) && item.HasHotState == QTristateBool.True && item.HasPressedState == QTristateBool.True)
      {
        this.m_oSelectedItem = item;
        if (activationType == QCompositeActivationType.Hotkey || activationType == QCompositeActivationType.Keyboard)
          this.m_oSelectedItem.ScrollIntoView(QTristateBool.False);
        if (!QItemStatesHelper.IsHot(item.ItemState))
          item.AdjustState(QItemStates.Hot, true, activationType);
        ((IQCompositeItemEventRaiser) this.m_oSelectedItem)?.RaiseItemSelected(new QCompositeEventArgs(this, item, activationType));
      }
      if (oSelectedItem != this.m_oSelectedItem)
        this.OnSelectedItemChanged(new QCompositeEventArgs(this, this.m_oSelectedItem, activationType));
      if (this.m_oExpandedItem == null)
        return;
      if (this.Configuration.GetExpandingDelay((IQPart) this) > 0)
      {
        if (this.m_oExpandedItem == this.m_oSelectedItem || this.m_oSelectedItem == null)
          this.m_oExpandedItem.AdjustTimerState(QCompositeTimerAction.Collapsing, false, activationType);
        else if (this.AutoChangeExpand((object) this.m_oExpandedItem))
          this.m_oExpandedItem.AdjustTimerState(QCompositeTimerAction.Collapsing, true, activationType);
      }
      this.m_oExpandedItem.HandleChange(false);
    }

    public void ActivateItem(
      QCompositeItemBase item,
      QCompositeItemActivationOptions options,
      QCompositeActivationType activationType)
    {
      if (item == null)
        return;
      if (item.HasPressedState != QTristateBool.True)
      {
        item = this.GetParentWithPressedState(item);
        if (item == null)
          return;
      }
      item.Activate(options, activationType);
    }

    public bool ExpandItem(QCompositeItem item, QCompositeActivationType activationType) => this.ExpandItem(item, activationType, false);

    internal bool ExpandItem(
      QCompositeItem item,
      QCompositeActivationType activationType,
      bool disableAnimation)
    {
      if (item == null || !QCompositeHelper.IsAccessibleRecursive((IQPart) item, QPartVisibilitySelectionTypes.IncludeAll) || this.m_oParentContainer == null || QItemStatesHelper.IsExpanded(item.ItemState) || !item.CanExpand)
        return false;
      bool flag = this.CollapseExpandedItem(activationType);
      if (this.m_oExpandedItem != null)
        return false;
      if (flag)
        disableAnimation = true;
      this.m_oExpandedItem = item;
      QCompositeWindow childWindow = this.m_oExpandedItem.ChildWindow;
      this.m_oExpandedItem.ChildWindow.OwnerWindow = (IWin32Window) this.ParentControl;
      this.ConfigureChildWindow(this.m_oExpandedItem, this.m_oExpandedItem.ChildWindow);
      this.m_oExpandedItem.ConfigureChildWindow();
      Keys keys = Keys.None;
      QRelativePositions openingItemRelativePosition;
      QCommandDirections animateDirection1;
      switch (this.GetExpandDirection(this.m_oExpandedItem))
      {
        case QCompositeExpandDirection.Left:
          openingItemRelativePosition = QRelativePositions.Right;
          animateDirection1 = QCommandDirections.Left;
          if (this.CloseOnNavigationKeys((object) this.m_oExpandedItem))
          {
            keys = Keys.Right;
            break;
          }
          break;
        case QCompositeExpandDirection.Up:
          openingItemRelativePosition = QRelativePositions.Below;
          animateDirection1 = QCommandDirections.Up;
          if (this.CloseOnNavigationKeys((object) this.m_oExpandedItem))
          {
            keys = Keys.Down;
            break;
          }
          break;
        case QCompositeExpandDirection.Down:
          openingItemRelativePosition = QRelativePositions.Above;
          animateDirection1 = QCommandDirections.Down;
          if (this.CloseOnNavigationKeys((object) this.m_oExpandedItem))
          {
            keys = Keys.Up;
            break;
          }
          break;
        default:
          openingItemRelativePosition = QRelativePositions.Left;
          animateDirection1 = QCommandDirections.Right;
          if (this.CloseOnNavigationKeys((object) this.m_oExpandedItem))
          {
            keys = Keys.Left;
            break;
          }
          break;
      }
      this.m_oExpandedItem.ScrollIntoView(QTristateBool.False);
      Rectangle screen;
      Rectangle openingItemBounds;
      if (this.PositionOutsideComposite((object) this.m_oExpandedItem))
      {
        screen = this.m_oParentContainer.RectangleToScreen(new Rectangle(Point.Empty, this.m_oParentContainer.Size));
        openingItemBounds = item.CalculatedProperties.CachedScrollCorrectedBounds;
      }
      else
      {
        screen = this.m_oParentContainer.RectangleToScreen(item.CalculatedProperties.CachedScrollCorrectedBounds);
        openingItemBounds = new Rectangle(Point.Empty, item.CalculatedProperties.CachedScrollCorrectedBounds.Size);
      }
      this.m_oExpandedItem.ChildComposite.CloseKeysNavigation = keys;
      Rectangle bounds1 = this.m_oExpandedItem.ChildWindow.CalculateBounds(screen, openingItemBounds, ref openingItemRelativePosition, ref animateDirection1);
      QCompositeExpandingCancelEventArgs e1 = new QCompositeExpandingCancelEventArgs(this, (QCompositeItemBase) item, activationType, bounds1, animateDirection1, !disableAnimation, false);
      ((IQCompositeItemEventRaiser) item).RaiseItemExpanding(e1);
      if (e1.Cancel)
      {
        this.m_oExpandedItem = (QCompositeItem) null;
        return false;
      }
      Rectangle bounds2 = e1.Bounds;
      QCommandDirections animateDirection2 = e1.AnimateDirection;
      this.m_oExpandedItem.ChildWindow.AllowAnimation = e1.AllowAnimation;
      QCompositeHelper.ResetState((IQPart) this.m_oExpandedItem.ChildComposite);
      this.PutUsedToolTipText((string) null);
      this.m_oExpandedItem.ChildComposite.OpeningItemRelativePosition = openingItemRelativePosition;
      this.m_oExpandedItem.ChildWindow.Show(bounds2, animateDirection2);
      item.AdjustState(QItemStates.Expanded, true, activationType);
      QCompositeExpandedEventArgs e2 = new QCompositeExpandedEventArgs(this, (QCompositeItemBase) item, activationType, e1.Bounds, e1.AnimateDirection, e1.AllowAnimation);
      ((IQCompositeItemEventRaiser) item).RaiseItemExpanded(e2);
      this.m_oHotkeyHandler.SuspendProcessing();
      if (QCompositeHelper.IsKeyboardActivationType(activationType))
      {
        this.m_oExpandedItem.ChildComposite.SelectFirstItem(activationType, true);
        this.m_oExpandedItem.ChildComposite.ShowHotkeyWindows = this.ShowHotkeyWindows;
        if (this.ShowHotkeyWindows)
          this.m_oExpandedItem.ChildComposite.StartProcessingHotkeys(false);
      }
      else
        this.m_oExpandedItem.ChildComposite.ShowHotkeyWindows = false;
      this.m_oExpandedItem.ChildComposite.HotkeyPrefixVisible = this.HotkeyPrefixVisible;
      return true;
    }

    public bool CollapseExpandedItem(QCompositeActivationType activationType) => this.CollapseItem(this.m_oExpandedItem, activationType);

    public bool CollapseItem(QCompositeItem item, QCompositeActivationType activationType)
    {
      if (item == null || !QItemStatesHelper.IsExpanded(item.ItemState) || item.ChildWindow == null)
        return false;
      item.ChildWindow.Close(activationType);
      return true;
    }

    protected virtual void ConfigureChildWindow(QCompositeItem item, QCompositeWindow window)
    {
      if (item.CustomChildWindow == window)
        return;
      window.SuspendChangeNotification();
      window.Configuration = this.ChildWindowConfiguration;
      window.ChildWindowConfiguration = this.ChildWindowConfiguration;
      window.CompositeConfiguration = this.ChildCompositeConfiguration;
      window.ChildCompositeConfiguration = this.ChildCompositeConfiguration;
      window.ColorScheme = this.ChildCompositeColorScheme;
      window.ChildCompositeColorScheme = this.ChildCompositeColorScheme;
      window.ToolTipConfiguration = this.ToolTipConfiguration;
      window.ResumeChangeNotification(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public virtual Color RetrieveFirstDefinedColor(string colorName) => this.ColorScheme[colorName].Current;

    public virtual QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      QColorSet itemColorSet = new QColorSet();
      if (destinationObject == this)
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("CompositeBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("CompositeBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("CompositeBorder");
        itemColorSet.Foreground = QItemStatesHelper.IsDisabled(state) ? this.RetrieveFirstDefinedColor("CompositeTextDisabled") : this.RetrieveFirstDefinedColor("CompositeText");
      }
      else if (destinationObject is QCompositeIconBackgroundPainter)
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("CompositeIconBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("CompositeIconBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("CompositeIconBackgroundBorder");
      }
      return itemColorSet;
    }

    public void PaintComposite(QPartPaintContext paintContext) => this.PaintEngine.PerformPaint((IQPart) this, paintContext);

    protected virtual void OnCompositeKeyPress(QCompositeKeyboardCancelEventArgs e)
    {
      if (this.ParentCompositeEventRaiser != null)
        this.ParentCompositeEventRaiser.RaiseCompositeKeyPress(e);
      if (this.CompositeEventRaiser != null)
        this.CompositeEventRaiser.RaiseCompositeKeyPress(e);
      this.m_oCompositeKeyPress = QWeakDelegate.InvokeDelegate(this.m_oCompositeKeyPress, (object) this, (object) e);
    }

    protected virtual void OnUsedToolTipTextChanged(QCompositeEventArgs e)
    {
      if (this.ParentCompositeEventRaiser != null)
        this.ParentCompositeEventRaiser.RaiseUsedToolTipTextChanged(e);
      if (this.CompositeEventRaiser != null)
        this.CompositeEventRaiser.RaiseUsedToolTipTextChanged(e);
      this.m_oUsedToolTipTextChanged = QWeakDelegate.InvokeDelegate(this.m_oUsedToolTipTextChanged, (object) this, (object) e);
    }

    protected virtual void OnSelectedItemChanged(QCompositeEventArgs e)
    {
      if (this.ParentCompositeEventRaiser != null)
        this.ParentCompositeEventRaiser.RaiseSelectedItemChanged(e);
      if (this.CompositeEventRaiser != null)
        this.CompositeEventRaiser.RaiseSelectedItemChanged(e);
      this.m_oSelectedItemChanged = QWeakDelegate.InvokeDelegate(this.m_oSelectedItemChanged, (object) this, (object) e);
    }

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaisePaintItem(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaisePaintItem(e);
      this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);
    }

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (!QCompositeHelper.IsKeyboardActivationType(e.ActivationType))
        this.ShowHotkeyWindows = false;
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemExpanded(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemExpanded(e);
      this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);
    }

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemExpanding(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemExpanding(e);
      this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemCollapsing(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemCollapsing(e);
      this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsed(QCompositeEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemCollapsed(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemCollapsed(e);
      this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);
    }

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemActivating(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemActivating(e);
      this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);
    }

    protected virtual void OnItemSelected(QCompositeEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemSelected(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemSelected(e);
      this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);
    }

    protected virtual void OnItemActivated(QCompositeEventArgs e)
    {
      if (this.ParentContainerItemEventRaiser != null && !this.ParentContainerItemEventRaiserInHierachy)
        this.ParentContainerItemEventRaiser.RaiseItemActivated(e);
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemActivated(e);
      this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);
    }

    protected virtual void OnColorsChanged(EventArgs e) => this.m_oColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oColorsChangedDelegate, (object) this, (object) e);

    protected virtual void OnChange(QCompositeChangeEventArgs e) => this.m_oChangeDelegate = QWeakDelegate.InvokeDelegate(this.m_oChangeDelegate, (object) this, (object) e);

    private void OnDisposed(EventArgs e) => this.m_oDisposedDelegate = QWeakDelegate.InvokeDelegate(this.m_oDisposedDelegate, (object) this, (object) e);

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
      this.OnUsedToolTipTextChanged(e);
    }

    void IQCompositeEventRaiser.RaiseCompositeKeyPress(
      QCompositeKeyboardCancelEventArgs e)
    {
      this.OnCompositeKeyPress(e);
    }

    void IQCompositeEventRaiser.RaiseSelectedItemChanged(QCompositeEventArgs e) => this.OnSelectedItemChanged(e);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPartLayoutEngine LayoutEngine
    {
      get
      {
        switch (this.Configuration.GetLayout((IQPart) this))
        {
          case QCompositeItemLayout.Linear:
            return (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
          case QCompositeItemLayout.Flow:
            return (IQPartLayoutEngine) QPartFlowLayoutEngine.Default;
          case QCompositeItemLayout.Table:
            return (IQPartLayoutEngine) QPartTableLayoutEngine.Default;
          default:
            return (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
        }
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPartPaintEngine PaintEngine => (IQPartPaintEngine) QPartDefaultPaintEngine.Default;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QRegion ContentClipRegion => this.m_oContentClipRegion;

    protected void PutContentClipRegion(Region region)
    {
      if (this.m_oContentClipRegion != null)
        this.m_oContentClipRegion.Dispose();
      this.m_oContentClipRegion = region != null ? new QRegion(region) : (QRegion) null;
    }

    internal void PutParentPart(IQPart part) => this.m_oParentPart = part;

    internal void PutPartName(string name) => this.m_sPartName = name;

    [Browsable(false)]
    public virtual string PartName => this.m_sPartName;

    [Browsable(false)]
    public virtual object ContentObject => (object) this.m_oPartCollection;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual QPartCollection Parts => this.m_oPartCollection;

    [Browsable(false)]
    public virtual QPartCalculatedProperties CalculatedProperties => this.m_oCalculatedProperties;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int LayoutOrder => this.m_iLayoutOrder;

    [Browsable(false)]
    public virtual IQPartSharedProperties Properties => (IQPartSharedProperties) this.Configuration;

    [Browsable(false)]
    public virtual bool IsVisible(QPartVisibilitySelectionTypes visibilityTypes) => QPartHelper.IsVisible((IQPart) this, true, false, visibilityTypes);

    public virtual QPartHitTestResult HitTest(int x, int y) => QPartHelper.DefaultHitTest((IQPart) this, x, y);

    [Browsable(false)]
    public virtual IQPart ParentPart => this.m_oParentPart;

    [Browsable(false)]
    public virtual QPartCollection ParentCollection => this.m_oParentCollection;

    [Browsable(false)]
    public virtual IQManagedLayoutParent DisplayParent => this.m_oParentContainer as IQManagedLayoutParent;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IQPartLayoutListener LayoutListener
    {
      get => this.m_oLayoutListener;
      set => this.m_oLayoutListener = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IQPartPaintListener PaintListener
    {
      get => this.m_oPaintListener;
      set => this.m_oPaintListener = value;
    }

    public virtual IQPartObjectPainter GetObjectPainter(
      QPartPaintLayer paintLayer,
      System.Type painterType)
    {
      return QPartObjectPainter.GetObjectPainter(this.m_aObjectPainters, paintLayer, painterType);
    }

    public virtual bool FitsInSelection(params QPartSelectionTypes[] selectionType) => QPartHelper.FitsInSelection((IQPart) this, selectionType);

    void IQPart.PushCalculatedProperties() => this.m_oCalculatedProperties = QPartCalculatedProperties.PushCalculatedProperties(this.m_oCalculatedProperties);

    void IQPart.PullCalculatedProperties() => this.m_oCalculatedProperties = QPartCalculatedProperties.PullCalculatedProperties(this.m_oCalculatedProperties);

    bool IQPart.IsSystemPart => this.m_bIsSystemPart;

    internal void PutIsSystemPart(bool value) => this.m_bIsSystemPart = value;

    void IQPart.SetParent(
      IQPart parentPart,
      QPartCollection ParentCollection,
      bool removeFromCurrentParent,
      bool addToNewParent)
    {
      if (removeFromCurrentParent && this.m_oParentCollection != null)
        this.m_oParentCollection.Remove((IQPart) this);
      this.m_oParentPart = parentPart;
      this.m_oParentCollection = ParentCollection;
      this.ClearCachedParents();
      if (!addToNewParent || this.m_oParentCollection == null)
        return;
      this.m_oParentCollection.Add((IQPart) this, false);
    }

    void IQPart.SetDisplayParent(IQManagedLayoutParent displayParent) => this.ClearCachedParents();

    void IQPart.SetLayoutOrder(int layoutOrder) => this.m_iLayoutOrder = layoutOrder;

    void IQPartLayoutListener.HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      this.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
    }

    protected virtual void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this)
        return;
      if (layoutStage == QPartLayoutStage.PreparingForLayout)
        this.SynchronizeScrollData();
      if (this.m_oScrollData == null)
        return;
      this.m_oScrollData.HandleLayoutStage(layoutStage, layoutContext, additionalProperties);
    }

    void IQPartPaintListener.HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      this.HandlePaintStage(part, paintStage, paintContext);
    }

    protected virtual QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet = (QColorSet) null;
      QShapeAppearance appearance = this.Configuration.GetAppearance((IQPart) this);
      if (part == this)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingBackground:
            if (appearance != null)
            {
              qcolorSet = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
              if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter1)
              {
                objectPainter1.Appearance = appearance;
                objectPainter1.DrawOnBounds = QPartBoundsType.Bounds;
                objectPainter1.Options = QPainterOptions.FillBackground;
                objectPainter1.Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape | QShapePainterOptions.StayWithinBounds);
                objectPainter1.ColorSet = qcolorSet;
              }
              if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QCompositeIconBackgroundPainter)) is QCompositeIconBackgroundPainter objectPainter2)
              {
                objectPainter2.Enabled = this.Configuration.GetIconBackgroundVisible((IQPart) this);
                if (objectPainter2.Enabled)
                {
                  objectPainter2.ClipOnPainter = objectPainter1;
                  objectPainter2.ColorSet = this.ColorHost.GetItemColorSet((object) objectPainter2, QItemStates.Normal, (object) null);
                  objectPainter2.Size = this.Configuration.GetIconBackgroundSize((IQPart) this);
                  objectPainter2.Margin = this.Configuration.GetIconBackgroundMargin((IQPart) this);
                }
              }
              if (this.GetObjectPainter(QPartPaintLayer.Foreground, typeof (QPartShapePainter)) is QPartShapePainter objectPainter3)
              {
                objectPainter3.Appearance = appearance;
                objectPainter3.DrawOnBounds = QPartBoundsType.Bounds;
                objectPainter3.Options = QPainterOptions.FillForeground;
                objectPainter3.Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
                objectPainter3.ColorSet = qcolorSet;
                break;
              }
              break;
            }
            break;
          case QPartPaintStage.BackgroundPainted:
            if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter)
              this.PutLastDrawnGraphicsPath(objectPainter.LastDrawnGraphicsPath);
            else
              this.PutLastDrawnGraphicsPath((GraphicsPath) null);
            this.m_oSavedPaintRegion = QPartHelper.AdjustPaintRegion((IQPart) this, this.LastDrawnGraphicsPath, paintContext);
            break;
          case QPartPaintStage.PaintingContent:
            if (this.m_oScrollData != null && this.m_oScrollData.ScrollOffset != Point.Empty)
            {
              paintContext.Graphics.TranslateTransform((float) this.m_oScrollData.ScrollOffset.X, (float) this.m_oScrollData.ScrollOffset.Y);
              break;
            }
            break;
          case QPartPaintStage.ContentPainted:
            if (this.m_oScrollData != null && this.m_oScrollData.ScrollOffset != Point.Empty)
            {
              paintContext.Graphics.TranslateTransform((float) -this.m_oScrollData.ScrollOffset.X, (float) -this.m_oScrollData.ScrollOffset.Y);
              break;
            }
            break;
          case QPartPaintStage.PaintingForeground:
            QPartHelper.RestorePaintRegion(this.m_oSavedPaintRegion, paintContext);
            this.m_oSavedPaintRegion = (Region) null;
            break;
          case QPartPaintStage.ForegroundPainted:
            if (this.m_oScrollData != null)
            {
              this.m_oScrollData.PaintScrollAreas(paintContext);
              break;
            }
            break;
          case QPartPaintStage.PaintFinished:
            this.PutContentClipRegion(QPartHelper.CreatePossibleContentClipRegion((IQPart) this, this.LastDrawnGraphicsPath));
            break;
        }
      }
      this.OnPaintItem(new QCompositePaintStageEventArgs(paintContext, paintStage, this, (QCompositeItemBase) null));
      return qcolorSet;
    }

    public bool IsPerformingLayout => this.m_bPerformingLayout;

    Control IQManagedLayoutParent.Control => this.ParentControl;

    public void HandleChildObjectChanged(bool performLayout) => this.HandleChildObjectChanged(performLayout, Rectangle.Empty);

    public void HandleChildObjectChanged(bool performLayout, Rectangle invalidateRectangle)
    {
      if (performLayout)
        this.m_bPerformingLayout = true;
      this.OnChange(new QCompositeChangeEventArgs(performLayout, invalidateRectangle));
      if (this.ManagedLayoutParent != null)
        this.ManagedLayoutParent.HandleChildObjectChanged(performLayout, invalidateRectangle);
      if (!performLayout)
        return;
      this.m_bPerformingLayout = false;
    }

    IQHotkeyItem IQHotkeyHandlerHost.SelectedItem => this.SelectedItem as IQHotkeyItem;

    IQHotkeyItem IQHotkeyHandlerHost.ActivatedItem => (IQHotkeyItem) this.ExpandedItem;

    void IQHotkeyHandlerHost.AddHotkeyItems(IList list) => QPartHelper.AddAllHotkeyItems((IQPartCollection) this.Parts, list);

    void IQHotkeyHandlerHost.ConfigureHotkeyWindow(IQHotkeyItem item) => QHotkeyHelper.ConfigureHotkeyWindow(this.ParentControl, (IQHotkeyHandlerHost) this, item, this.Configuration.GetHotkeyWindowConfiguration((IQPart) this), this.ColorScheme);

    bool IQNavigationHost.HandleShortcutKey(Keys key, out bool suppressToSystem) => this.HandleShortcutKey(key, out suppressToSystem);

    void IQNavigationHost.SelectNextItem(bool forward, bool loop) => this.SelectNextItem(QCompositeActivationType.Keyboard, forward, loop);

    void IQNavigationHost.SelectFirstOrCurrentItem(bool forward) => this.SelectFirstItem(QCompositeActivationType.Keyboard, forward);

    bool IQNavigationHost.IsAccessibleForNavigation => this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll) && this.Enabled;

    Point IQNavigationHost.LocationForOrder => this.PointToScreen(Point.Empty);

    QScrollablePartData IQScrollablePart.ScrollData => this.m_oScrollData;

    void IQScrollablePart.CaptureMouse(QScrollablePartData scrollablePartData) => this.CaptureMouse((IQMouseHandler) scrollablePartData);

    QItemStates IQItemStatesImplementation.GetState(
      QItemStates checkForStates,
      bool includeParentStates)
    {
      return this.GetState(checkForStates, includeParentStates);
    }

    QItemStates IQItemStatesImplementation.HasStatesDefined(
      QItemStates checkForStates,
      QTristateBool stateValue)
    {
      return this.HasStatesDefined(checkForStates, stateValue);
    }

    bool IQItemStatesImplementation.HasStatesDefined(QItemStates states) => this.HasStatesDefined(states);

    void IQItemStatesImplementation.AdjustState(
      QItemStates state,
      bool setValue,
      object additionalInfo)
    {
      this.AdjustState(state, setValue);
    }
  }
}
