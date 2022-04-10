// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemBase
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QCompositeItemConverter))]
  [Designer(typeof (QCompositeItemBaseDesigner), typeof (IDesigner))]
  [DesignerSerializer(typeof (QCompositeItemBaseCodeSerializer), typeof (CodeDomSerializer))]
  [DesignTimeVisible(false)]
  [ToolboxItem(false)]
  public abstract class QCompositeItemBase : 
    IComponent,
    IDisposable,
    IQDesignableItemContainer,
    IQWeakEventPublisher,
    IQPart,
    IQMouseHandler,
    IQPartLayoutListener,
    IQPartPaintListener,
    ICloneable,
    IQTimerClient,
    IQItemColorHost,
    IQColorRetriever,
    IQCompositeItemEventRaiser,
    IQCompositeItemEventPublisher,
    IQHostedComponent,
    IQItemStatesImplementation
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQComponentHost m_oCustomComponentHost;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private int m_iSuspendChangeNotification;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bIsDeserializingFromCode;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQCompositeItemEventRaiser m_oCachedParentItemEventRaiser;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeItemBase m_oOriginalItem;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeItemBase m_oLastClonedItem;
    [QCloneBehavior(QCloneBehaviorType.ByReference)]
    private object m_oUserReference;
    [QCloneBehavior(QCloneBehaviorType.ByReference)]
    private object m_oSystemReference;
    private QCompositeItemChangedFlags m_eChangedFlags;
    private bool m_bIsSystemPart;
    private bool m_bVisible = true;
    private bool m_bHiddenBecauseOfConstraints;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeItemBase.QCompositeTimerState m_oTimerState = new QCompositeItemBase.QCompositeTimerState();
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQManagedLayoutParent m_oDisplayParent;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQItemColorHost m_oColorHost;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartPaintListener m_oPaintListener;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartLayoutListener m_oLayoutListener;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QItemStates m_eItemStates;
    private bool m_bEnabled = true;
    private int m_iLayoutOrder;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private GraphicsPath m_oLastDrawnGraphicsPath;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRegion m_oContentClipRegion;
    private QColorScheme m_oColorScheme;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPart m_oParentPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCollection m_oParentCollection;
    private object m_oContentObject;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPartObjectPainter[] m_aObjectPainters;
    private IQPartConfigurationBase m_oConfiguration;
    [QCloneBehavior(QCloneBehaviorType.ByReference)]
    private IQPartLayoutEngine m_oLayoutEngine;
    [QCloneBehavior(QCloneBehaviorType.ByReference)]
    private IQPartPaintEngine m_oPaintEngine;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCalculatedProperties m_oCalculatedProperties;
    private string m_sItemName;
    private bool m_bWeakEventHandlers = true;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bIsDisposed;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private ISite m_oSite;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oDisposedDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemPaintStageDelegate;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemActivating;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemActivated;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemSelected;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemExpanded;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemExpanding;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemCollapsed;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oItemCollapsing;

    protected QCompositeItemBase(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    protected QCompositeItemBase()
      : this(QCompositeItemCreationOptions.Default)
    {
    }

    protected QCompositeItemBase(QCompositeItemCreationOptions options)
    {
      this.m_aObjectPainters = this.CreatePainters((IQPartObjectPainter[]) null);
      this.m_oCalculatedProperties = new QPartCalculatedProperties((IQPart) this);
      this.m_oLayoutListener = (IQPartLayoutListener) this;
      this.m_oPaintListener = (IQPartPaintListener) this;
      if ((options & QCompositeItemCreationOptions.CreateItemsCollection) == QCompositeItemCreationOptions.CreateItemsCollection)
        this.m_oContentObject = (object) new QPartCollection((IQPart) this, (IQManagedLayoutParent) null);
      if ((options & QCompositeItemCreationOptions.CreateConfiguration) == QCompositeItemCreationOptions.CreateConfiguration)
      {
        this.m_oConfiguration = this.CreateConfiguration();
        this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      }
      if ((options & QCompositeItemCreationOptions.CreateColorScheme) != QCompositeItemCreationOptions.CreateColorScheme)
        return;
      this.m_oColorScheme = new QColorScheme(false);
      this.m_oColorScheme.ColorsChanged += new EventHandler(this.ColorScheme_ColorsChanged);
    }

    public virtual void BeginCodeDeserialization() => this.m_bIsDeserializingFromCode = true;

    public void EndCodeDeserialization() => this.m_bIsDeserializingFromCode = false;

    [Browsable(false)]
    public bool IsDeserializingFromCode
    {
      get
      {
        if (this.m_bIsDeserializingFromCode)
          return true;
        for (IQPart parentPart = this.ParentPart; parentPart != null; parentPart = parentPart.ParentPart)
        {
          if (parentPart is QCompositeItemBase qcompositeItemBase)
            return qcompositeItemBase.IsDeserializingFromCode;
        }
        return false;
      }
    }

    ~QCompositeItemBase() => this.Dispose(false);

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItem gets disposed")]
    public event EventHandler Disposed
    {
      add => this.m_oDisposedDelegate = QWeakDelegate.Combine(this.m_oDisposedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oDisposedDelegate = QWeakDelegate.Remove(this.m_oDisposedDelegate, (Delegate) value);
    }

    [Description("Gets raised when a layer of the QCompositeItem is painted")]
    [QWeakEvent]
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

    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItemBase is activated")]
    [QWeakEvent]
    public event QCompositeEventHandler ItemActivated
    {
      add => this.m_oItemActivated = QWeakDelegate.Combine(this.m_oItemActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivated = QWeakDelegate.Remove(this.m_oItemActivated, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItemBase is selected")]
    [QWeakEvent]
    public event QCompositeEventHandler ItemSelected
    {
      add => this.m_oItemSelected = QWeakDelegate.Combine(this.m_oItemSelected, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemSelected = QWeakDelegate.Remove(this.m_oItemSelected, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the item is expanded")]
    public event QCompositeExpandedEventHandler ItemExpanded
    {
      add => this.m_oItemExpanded = QWeakDelegate.Combine(this.m_oItemExpanded, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanded = QWeakDelegate.Remove(this.m_oItemExpanded, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the child container is expanding")]
    [Category("QEvents")]
    public event QCompositeExpandingCancelEventHandler ItemExpanding
    {
      add => this.m_oItemExpanding = QWeakDelegate.Combine(this.m_oItemExpanding, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanding = QWeakDelegate.Remove(this.m_oItemExpanding, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the item is collapsing")]
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

    [Browsable(false)]
    public QComposite Composite => this.m_oDisplayParent as QComposite;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object UserReference
    {
      get => this.m_oUserReference;
      set => this.m_oUserReference = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object SystemReference
    {
      get => this.m_oSystemReference;
      set => this.m_oSystemReference = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual Cursor Cursor
    {
      get => (Cursor) null;
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Localizable(true)]
    public virtual string ToolTipText
    {
      get => (string) null;
      set
      {
      }
    }

    [Browsable(false)]
    public virtual QTristateBool HasHotState => QTristateBool.Undefined;

    [Browsable(false)]
    public virtual QTristateBool HasPressedState => QTristateBool.Undefined;

    [Browsable(false)]
    public virtual QTristateBool HasCheckedState => QTristateBool.False;

    [Browsable(false)]
    public QItemStates ItemState => this.GetState(QItemStates.All, true);

    protected virtual QItemStates GetState(
      QItemStates checkForStates,
      bool includeParentStates)
    {
      checkForStates &= ~this.HasStatesDefined(checkForStates, QTristateBool.False);
      QItemStates state1 = this.m_eItemStates & checkForStates;
      if (includeParentStates)
      {
        IQPart qpart = this.ParentPart;
        checkForStates &= ~QItemStates.Checked;
        for (checkForStates &= ~this.HasStatesDefined(checkForStates, QTristateBool.True); qpart != null && checkForStates != QItemStates.Normal; qpart = qpart is QComposite ? (IQPart) null : qpart.ParentPart)
        {
          if (qpart is IQItemStatesImplementation statesImplementation)
          {
            checkForStates &= ~statesImplementation.HasStatesDefined(checkForStates, QTristateBool.False);
            QItemStates state2 = statesImplementation.GetState(checkForStates, false);
            state1 |= state2;
            checkForStates &= ~state2;
            checkForStates &= ~statesImplementation.HasStatesDefined(checkForStates, QTristateBool.True);
          }
        }
      }
      return state1;
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
      if (this.HasCheckedState != stateValue)
        qitemStates &= ~QItemStates.Checked;
      if (stateValue != QTristateBool.Undefined)
        qitemStates &= ~(QItemStates.Disabled | QItemStates.Expanded);
      return qitemStates;
    }

    public virtual bool HasStatesDefined(QItemStates states) => this.HasStatesDefined(states, QTristateBool.True) == states;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQItemColorHost ColorHost
    {
      get => this.m_oColorHost == null ? (IQItemColorHost) this : this.m_oColorHost;
      set => this.m_oColorHost = value;
    }

    [DefaultValue(false)]
    [Browsable(false)]
    public virtual bool CloseMenuOnActivate
    {
      get => false;
      set
      {
      }
    }

    [DefaultValue(true)]
    [Category("QBehavior")]
    [Description("Gets or sets whether this item is Visible")]
    public bool Visible
    {
      get => this.m_bVisible;
      set => this.SetVisible(value, true, true);
    }

    public virtual void SetVisible(bool value, bool notiyChilds, bool notiyChange)
    {
      bool flag = this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll);
      this.m_bVisible = value;
      if (flag == this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        return;
      if (notiyChilds)
        QCompositeHelper.NotifyChildPartVisibilityChanged((IQPart) this);
      if (!notiyChange)
        return;
      this.HandleChange(true);
    }

    [Browsable(false)]
    public virtual bool IsEnabled => this.Enabled;

    [Browsable(false)]
    public bool IsAccessible(QPartVisibilitySelectionTypes visibilitySelection) => this.IsVisible(visibilitySelection) && this.IsEnabled;

    [Category("QBehavior")]
    [Description("Gets or sets whether this item is Enabled")]
    [DefaultValue(true)]
    public bool Enabled
    {
      get => this.m_bEnabled;
      set
      {
        bool isEnabled = this.IsEnabled;
        this.m_bEnabled = value;
        this.UpdateEnabledState();
        if (isEnabled == this.IsEnabled)
          return;
        QCompositeHelper.NotifyChildPartEnabledChanged((IQPart) this);
      }
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme != null && this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme()
    {
      if (this.ColorScheme == null)
        return;
      this.ColorScheme.Reset();
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set
      {
        if (this.m_oColorScheme == value)
          return;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged -= new EventHandler(this.ColorScheme_ColorsChanged);
        this.m_oColorScheme = value;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged += new EventHandler(this.ColorScheme_ColorsChanged);
        if (this.m_oColorScheme == null || this.IsDisposed)
          return;
        this.HandleChange(false);
      }
    }

    [DefaultValue(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [QXmlSave(QXmlSaveType.NeverSave)]
    [Description("Contains the Name of the QCompositeItem")]
    [DefaultValue(null)]
    [Category("QBehavior")]
    public string ItemName
    {
      get => this.m_sItemName;
      set => this.m_sItemName = value;
    }

    [Browsable(false)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ISite Site
    {
      get => this.m_oSite;
      set => this.m_oSite = value;
    }

    [Browsable(false)]
    public bool IsDisposed => this.m_bIsDisposed;

    public bool ShouldSerializeConfiguration() => this.Configuration != null && !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration()
    {
      if (this.Configuration == null)
        return;
      this.Configuration.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    public virtual QContentPartConfiguration Configuration
    {
      get => this.m_oConfiguration as QContentPartConfiguration;
      set
      {
        if (this.m_oConfiguration == value)
          return;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= new EventHandler(this.Configuration_ConfigurationChanged);
        this.m_oConfiguration = (IQPartConfigurationBase) value;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
        this.Configuration_ConfigurationChanged((object) this.m_oConfiguration, EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual IQPartSharedProperties Properties => (IQPartSharedProperties) this.m_oConfiguration;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeItemBase LastClonedItem => this.m_oLastClonedItem;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeItemBase OriginalItem => this.m_oOriginalItem;

    [Browsable(false)]
    protected virtual bool ShouldRaiseEventOnOriginal => this.m_oOriginalItem != null;

    internal IQComponentHost CustomComponentHost => this.m_oCustomComponentHost;

    internal void SetCustomComponentHost(IQComponentHost value, bool removeFromCurrentHost)
    {
      if (removeFromCurrentHost && this.m_oCustomComponentHost != null)
        this.m_oCustomComponentHost.SetComponent((object) this, (object) null);
      this.m_oCustomComponentHost = value;
    }

    internal QCompositeItemChangedFlags ChangedFlags
    {
      get => this.m_eChangedFlags;
      set => this.m_eChangedFlags = value;
    }

    internal void FlipChangedFlag(QCompositeItemChangedFlags flag)
    {
      if ((this.m_eChangedFlags & flag) == flag)
        this.m_eChangedFlags &= ~flag;
      else
        this.m_eChangedFlags |= flag;
    }

    internal void SetChangedFlag(QCompositeItemChangedFlags flag, bool value)
    {
      if (value)
        this.m_eChangedFlags |= flag;
      else
        this.m_eChangedFlags &= ~flag;
    }

    internal bool HasChangedFlag(QCompositeItemChangedFlags flag) => (this.m_eChangedFlags & flag) == flag;

    internal bool HiddenBecauseOfConstraints
    {
      get => this.m_bHiddenBecauseOfConstraints;
      set => this.m_bHiddenBecauseOfConstraints = value;
    }

    internal QCompositeItemBase.QCompositeTimerState TimerState => this.m_oTimerState;

    [Browsable(false)]
    public bool ParentControlIsInitializing => this.ParentContainer is IQSupportInitialize parentContainer && parentContainer.IsInitializing;

    [Browsable(false)]
    public Control ParentControl => this.Composite?.ParentControl;

    internal IQCompositeContainer ParentContainer => this.Composite?.ParentContainer;

    internal bool IsLayoutEngineSet => this.m_oLayoutEngine != null;

    internal IQCompositeItemEventRaiser ParentItemEventRaiser
    {
      get
      {
        if (this.m_oCachedParentItemEventRaiser == null)
          this.m_oCachedParentItemEventRaiser = QCompositeHelper.FindParentItemEventRaiser((IQPart) this);
        return this.m_oCachedParentItemEventRaiser;
      }
    }

    public bool Activate(QCompositeActivationType activationType) => this.Activate(QCompositeItemActivationOptions.Automatic, activationType);

    public virtual bool Activate(
      QCompositeItemActivationOptions options,
      QCompositeActivationType activationType)
    {
      bool flag1 = false;
      bool flag2 = (options & QCompositeItemActivationOptions.Automatic) == QCompositeItemActivationOptions.Automatic;
      bool flag3 = (options & QCompositeItemActivationOptions.Expand) == QCompositeItemActivationOptions.FocusControl;
      bool flag4 = (options & QCompositeItemActivationOptions.Expand) == QCompositeItemActivationOptions.Expand;
      bool flag5 = (options & QCompositeItemActivationOptions.Activate) == QCompositeItemActivationOptions.Activate;
      if (this.HasPressedState != QTristateBool.True)
        return false;
      IQCompositeItemControl qcompositeItemControl = this as IQCompositeItemControl;
      if ((flag2 || flag3) && qcompositeItemControl != null && qcompositeItemControl.Control != null && qcompositeItemControl.Control.Visible && this.Composite != null && QCompositeHelper.IsAccessibleRecursive((IQPart) this, QPartVisibilitySelectionTypes.IncludeAll))
      {
        this.Composite.FocusChildControl(qcompositeItemControl.Control);
        flag1 = true;
        if (flag2)
          return true;
      }
      if ((flag2 || flag4) && QCompositeHelper.IsAccessibleRecursive((IQPart) this, QPartVisibilitySelectionTypes.IncludeAll) && this is QCompositeItem && this.Composite != null && this.Composite.ExpandItem(this as QCompositeItem, activationType))
      {
        flag1 = true;
        if (flag2)
          return true;
      }
      if ((flag2 || flag5 || flag3) && QCompositeHelper.IsAccessibleRecursive((IQPart) this, ~QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints))
      {
        QCompositeCancelEventArgs e = new QCompositeCancelEventArgs(this.Composite, this, activationType, false);
        ((IQCompositeItemEventRaiser) this).RaiseItemActivating(e);
        if (e.Cancel)
          return false;
        if (this.Composite != null)
          this.Composite.CollapseExpandedItem(activationType);
        ((IQCompositeItemEventRaiser) this).RaiseItemActivated(new QCompositeEventArgs(this.Composite, this, activationType));
        flag1 = true;
      }
      return flag1;
    }

    public void ScrollIntoView() => QPartHelper.ScrollIntoView((IQPart) this, QTristateBool.Undefined);

    public void ScrollIntoView(QTristateBool animated) => QPartHelper.ScrollIntoView((IQPart) this, animated);

    public void SetObjectPainter(QPartPaintLayer paintLayer, IQPartObjectPainter painter) => this.m_aObjectPainters = QPartObjectPainter.SetObjectPainter(this.m_aObjectPainters, paintLayer, painter);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual QRegion ContentClipRegion => this.m_oContentClipRegion;

    protected void PutContentClipRegion(Region region)
    {
      if (this.m_oContentClipRegion != null)
        this.m_oContentClipRegion.Dispose();
      this.m_oContentClipRegion = region != null ? new QRegion(region) : (QRegion) null;
    }

    public virtual bool IsVisible(QPartVisibilitySelectionTypes visibilityTypes) => QPartHelper.IsVisible((IQPart) this, this.Visible, this.m_bHiddenBecauseOfConstraints, visibilityTypes);

    public Point PointToClient(Point point)
    {
      QComposite composite = this.Composite;
      return composite == null ? point : composite.PointToClient(point);
    }

    public Point PointToScreen(Point point)
    {
      QComposite composite = this.Composite;
      return composite == null ? point : composite.PointToScreen(point);
    }

    public virtual void RemoveCloneLink()
    {
      if (this.m_oLastClonedItem != null)
      {
        this.m_oLastClonedItem.m_oOriginalItem = (QCompositeItemBase) null;
        this.m_oLastClonedItem = (QCompositeItemBase) null;
      }
      QCompositeHelper.RemoveCloneLinks((IQPartCollection) this.Parts);
    }

    public virtual void MoveUnclonablesToClone() => QCompositeHelper.MoveUnclonablesToClones((IQPartCollection) this.Parts);

    public virtual void RestoreUnclonablesFromClone() => QCompositeHelper.RestoreUnclonablesFromClones((IQPartCollection) this.Parts);

    public Rectangle RectangleToClient(Rectangle rectangle)
    {
      QComposite composite = this.Composite;
      return composite == null ? rectangle : composite.RectangleToClient(rectangle);
    }

    public Rectangle RectangleToScreen(Rectangle rectangle)
    {
      QComposite composite = this.Composite;
      return composite == null ? rectangle : composite.RectangleToScreen(rectangle);
    }

    public virtual object Clone()
    {
      QCompositeItemBase qcompositeItemBase = this.Site == null ? (QCompositeItemBase) QObjectCloner.CreateNewObjectInstance((object) this) : (QCompositeItemBase) ((IDesignerHost) this.Site.GetService(typeof (IDesignerHost))).CreateComponent(this.GetType());
      QObjectCloner.CopyToObject((object) this, (object) qcompositeItemBase, false);
      if (qcompositeItemBase.Parts != null)
        qcompositeItemBase.Parts.SetParent((IQPart) qcompositeItemBase, false);
      if (qcompositeItemBase.m_oColorScheme != null)
        qcompositeItemBase.m_oColorScheme.ColorsChanged += new EventHandler(qcompositeItemBase.ColorScheme_ColorsChanged);
      if (qcompositeItemBase.m_oConfiguration != null)
        qcompositeItemBase.m_oConfiguration.ConfigurationChanged += new EventHandler(qcompositeItemBase.Configuration_ConfigurationChanged);
      qcompositeItemBase.m_oOriginalItem = this;
      this.m_oLastClonedItem = qcompositeItemBase;
      return (object) qcompositeItemBase;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected internal virtual void HandleAncestorVisibilityChanged()
    {
    }

    protected internal virtual void HandleAncestorEnabledChanged()
    {
    }

    protected internal virtual void HandleScrollingStage(
      IQScrollablePart scrollingPart,
      QScrollablePartScrollStage stage)
    {
      this.CalculatedProperties.ClearCachedPaintProperties();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected QItemStates BaseItemState
    {
      get => this.m_eItemStates;
      set => this.m_eItemStates = value;
    }

    protected internal virtual bool MatchesHotkeySequence(Keys[] sequence, bool exact) => false;

    protected internal virtual bool MatchesShortcut(Keys shortcut) => false;

    protected internal virtual bool MatchesNavigationFilter(QCompositeNavigationFilter filter) => this.IsVisible(filter.PartVisibilitySelection) && (!filter.MustBeEnabled || this.Enabled) && (!filter.MustHavePressedState || this.HasPressedState == QTristateBool.True) && (!filter.MustMatchShortcut || this.MatchesShortcut(filter.Shortcut));

    protected internal virtual void AdjustState(
      QItemStates adjustment,
      bool setValue,
      QCompositeActivationType activationType)
    {
      if (QItemStatesHelper.IsState(this.m_eItemStates, adjustment) == setValue)
        return;
      this.m_eItemStates = QItemStatesHelper.AdjustState(this.m_eItemStates, adjustment, setValue);
      if (this is QCompositeItem qcompositeItem && QItemStatesHelper.IsHot(adjustment))
      {
        QComposite composite = this.Composite;
        if (composite != null)
        {
          if (this.HasPressedState == QTristateBool.True && this.HasHotState == QTristateBool.True)
          {
            if (setValue)
              composite.SelectItem(this, activationType, false);
            else if (composite.SelectedItem == this)
              composite.SelectItem((QCompositeItemBase) null, activationType, false);
          }
          if (qcompositeItem.CanExpand && qcompositeItem.HasPressedState == QTristateBool.True && composite.UsedAutoExpand(qcompositeItem) && !QCompositeHelper.IsKeyboardActivationType(activationType))
          {
            if (setValue)
            {
              if (!QItemStatesHelper.IsExpanded(this.m_eItemStates))
              {
                if (composite.Configuration.GetExpandingDelay((IQPart) composite) == 0 || activationType == QCompositeActivationType.Hotkey)
                  composite.ExpandItem(this as QCompositeItem, activationType);
                else
                  this.AdjustTimerState(QCompositeTimerAction.Expanding, true, activationType);
              }
              else
                this.AdjustTimerState(QCompositeTimerAction.Collapsing, false, activationType);
            }
            else
              this.AdjustTimerState(QCompositeTimerAction.Expanding, false, activationType);
          }
        }
      }
      this.HandleChange(false);
    }

    protected internal virtual bool HandleMouseMove(MouseEventArgs e) => true;

    protected internal virtual bool HandleMouseDown(MouseEventArgs e) => true;

    protected internal virtual bool HandleMouseUp(MouseEventArgs e) => !QItemStatesHelper.IsExpanded(this.ItemState);

    protected virtual void SetParent(
      IQPart parentPart,
      QPartCollection parentCollection,
      bool removeFromCurrentParent,
      bool addToNewParent)
    {
      if (removeFromCurrentParent && this.m_oParentCollection != null)
        this.m_oParentCollection.Remove((IQPart) this);
      this.m_oParentPart = parentPart;
      this.m_oParentCollection = parentCollection;
      this.ClearCachedParents();
      if (!addToNewParent || this.m_oParentCollection == null)
        return;
      this.m_oParentCollection.Add((IQPart) this, false);
    }

    protected virtual void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      this.ClearCachedParents();
      this.m_oDisplayParent = displayParent;
      if (this.ColorScheme != null)
        this.ColorScheme.SetBaseColorScheme(this.Composite != null ? (QColorSchemeBase) this.Composite.ColorScheme : (QColorSchemeBase) null, false);
      if (this.Parts == null)
        return;
      this.Parts.SetDisplayParent(this.m_oDisplayParent);
    }

    protected virtual void ClearCachedParents() => this.m_oCachedParentItemEventRaiser = (IQCompositeItemEventRaiser) null;

    protected virtual IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      return currentPainters;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bIsDisposed)
        return;
      if (disposing && this.m_oColorScheme != null && !this.m_oColorScheme.IsDisposed)
        this.m_oColorScheme.Dispose();
      this.m_bIsDisposed = true;
      this.OnDisposed(EventArgs.Empty);
    }

    protected virtual IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QContentPartConfiguration();

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaisePaintItem(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemPaintStageDelegate, (object) this, (object) e);
      this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);
    }

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemActivating(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemActivating = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemActivating, (object) this, (object) e);
      this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);
    }

    protected virtual void OnItemActivated(QCompositeEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemActivated(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemActivated = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemActivated, (object) this, (object) e);
      this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);
    }

    protected virtual void OnItemSelected(QCompositeEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemSelected(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemSelected = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemSelected, (object) this, (object) e);
      this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);
    }

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemExpanded(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemExpanded = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemExpanded, (object) this, (object) e);
      this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);
    }

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemExpanding(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemExpanding = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemExpanding, (object) this, (object) e);
      this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsed(QCompositeEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemCollapsed(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemCollapsed, (object) this, (object) e);
      this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e)
    {
      QCompositeItemBase qcompositeItemBase = this.ShouldRaiseEventOnOriginal ? this.OriginalItem : (QCompositeItemBase) null;
      if (this.ParentItemEventRaiser != null)
        this.ParentItemEventRaiser.RaiseItemCollapsing(e);
      if (qcompositeItemBase != null)
        qcompositeItemBase.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(qcompositeItemBase.m_oItemCollapsing, (object) this, (object) e);
      this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);
    }

    internal void AdjustTimerState(
      QCompositeTimerAction action,
      bool value,
      QCompositeActivationType expandingType)
    {
      switch (expandingType)
      {
        case QCompositeActivationType.None:
          if (value)
            this.m_oTimerState.CurrentAction |= action;
          else
            this.m_oTimerState.CurrentAction &= ~action;
          this.UpdateTimerRegistration();
          break;
        case QCompositeActivationType.Mouse:
          expandingType = QCompositeActivationType.MouseTimed;
          goto default;
        default:
          this.m_oTimerState.ExpandingType = expandingType;
          goto case QCompositeActivationType.None;
      }
    }

    internal void UpdateTimerRegistration()
    {
      QComposite composite = this.Composite;
      if (composite == null)
        return;
      if (this.m_oTimerState.NeedTimer && !composite.TimerManager.IsRegistered((IQTimerClient) this))
      {
        composite.TimerManager.Register((IQTimerClient) this);
      }
      else
      {
        if (this.m_oTimerState.NeedTimer || !composite.TimerManager.IsRegistered((IQTimerClient) this))
          return;
        composite.TimerManager.Unregister((IQTimerClient) this);
      }
    }

    internal void PutContentObject(object value) => this.m_oContentObject = value;

    internal void UpdateEnabledState() => this.AdjustState(QItemStates.Disabled, !this.IsEnabled, QCompositeActivationType.None);

    private void OnDisposed(EventArgs e) => this.m_oDisposedDelegate = QWeakDelegate.InvokeDelegate(this.m_oDisposedDelegate, (object) this, (object) e);

    private void ColorScheme_ColorsChanged(object sender, EventArgs e) => this.HandleChange(false);

    protected void PutLastDrawnGraphicsPath(GraphicsPath path)
    {
      if (this.m_oLastDrawnGraphicsPath == path)
        return;
      if (this.m_oLastDrawnGraphicsPath != null)
        this.m_oLastDrawnGraphicsPath.Dispose();
      this.m_oLastDrawnGraphicsPath = path;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GraphicsPath LastDrawnGraphicsPath => this.m_oLastDrawnGraphicsPath;

    public virtual int SuspendChangeNotification()
    {
      ++this.m_iSuspendChangeNotification;
      return this.m_iSuspendChangeNotification;
    }

    public virtual int ResumeChangeNotification(bool notifyChange)
    {
      this.m_iSuspendChangeNotification = Math.Max(0, --this.m_iSuspendChangeNotification);
      if (notifyChange)
        this.HandleChange(true);
      return this.m_iSuspendChangeNotification;
    }

    protected internal virtual void HandleChange(bool performLayout)
    {
      if (this.m_iSuspendChangeNotification > 0 || this.m_oDisplayParent == null)
        return;
      this.m_oDisplayParent.HandleChildObjectChanged(performLayout, QPartHelper.GetPartPaintBounds((IQPart) this, true));
    }

    private void Configuration_ConfigurationChanged(object sender, EventArgs e) => this.HandleChange(true);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQPart ParentPart => this.m_oParentPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual QPartCollection ParentCollection => this.m_oParentCollection;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQManagedLayoutParent DisplayParent => this.m_oDisplayParent;

    [Browsable(false)]
    public virtual string PartName => this.ItemName;

    [Browsable(false)]
    public virtual object ContentObject => this.m_oContentObject;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual QPartCollection Parts => this.m_oContentObject as QPartCollection;

    [Browsable(false)]
    public bool HasItems => this.Parts != null && this.Parts.Count > 0;

    [Browsable(false)]
    public virtual QPartCollection Items => this.Parts;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual int LayoutOrder
    {
      get => this.m_iLayoutOrder;
      set => this.SetLayoutOrder(value, true);
    }

    public void SetLayoutOrder(int value, bool notifyChange)
    {
      this.m_iLayoutOrder = value;
      if (!notifyChange)
        return;
      this.HandleChange(true);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQPartLayoutEngine LayoutEngine
    {
      get => this.m_oLayoutEngine == null ? (IQPartLayoutEngine) QPartLinearLayoutEngine.Default : this.m_oLayoutEngine;
      set => this.m_oLayoutEngine = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual IQPartPaintEngine PaintEngine
    {
      get => this.m_oPaintEngine == null ? (IQPartPaintEngine) QPartDefaultPaintEngine.Default : this.m_oPaintEngine;
      set => this.m_oPaintEngine = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQPartLayoutListener LayoutListener
    {
      get => this.m_oLayoutListener;
      set => this.m_oLayoutListener = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual IQPartPaintListener PaintListener
    {
      get => this.m_oPaintListener;
      set => this.m_oPaintListener = value;
    }

    [Browsable(false)]
    public virtual QPartCalculatedProperties CalculatedProperties => this.m_oCalculatedProperties;

    public virtual IQPartObjectPainter GetObjectPainter(
      QPartPaintLayer paintLayer,
      System.Type painterType)
    {
      return QPartObjectPainter.GetObjectPainter(this.m_aObjectPainters, paintLayer, painterType);
    }

    public virtual bool FitsInSelection(params QPartSelectionTypes[] selectionType) => QPartHelper.FitsInSelection((IQPart) this, selectionType);

    public virtual QPartHitTestResult HitTest(int x, int y) => QPartHelper.DefaultHitTest((IQPart) this, x, y);

    void IQPart.PushCalculatedProperties() => this.m_oCalculatedProperties = QPartCalculatedProperties.PushCalculatedProperties(this.m_oCalculatedProperties);

    void IQPart.PullCalculatedProperties() => this.m_oCalculatedProperties = QPartCalculatedProperties.PullCalculatedProperties(this.m_oCalculatedProperties);

    bool IQPart.IsSystemPart => this.m_bIsSystemPart;

    internal void PutIsSystemPart(bool value) => this.m_bIsSystemPart = value;

    void IQPart.SetParent(
      IQPart parentPart,
      QPartCollection parentCollection,
      bool removeFromCurrentParent,
      bool addToNewParent)
    {
      this.SetParent(parentPart, parentCollection, removeFromCurrentParent, addToNewParent);
    }

    void IQPart.SetDisplayParent(IQManagedLayoutParent displayParent) => this.SetDisplayParent(displayParent);

    void IQPart.SetLayoutOrder(int layoutOrder) => this.SetLayoutOrder(layoutOrder, false);

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
      if (part == this)
        ((IQCompositeItemEventRaiser) this).RaisePaintItem(new QCompositePaintStageEventArgs(paintContext, paintStage, this.Composite, this));
      return (QColorSet) null;
    }

    public static QColorSet GetDefaultCompositeItemColorSet(
      object item,
      QItemStates state,
      QComposite parentComposite,
      IQColorRetriever retriever)
    {
      QColorSet compositeItemColorSet = new QColorSet();
      if (QItemStatesHelper.IsDisabled(state))
      {
        compositeItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeItemDisabledBackground1");
        compositeItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeItemDisabledBackground2");
        compositeItemColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeItemDisabledBorder");
        compositeItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextDisabled");
      }
      else if (QItemStatesHelper.IsChecked(state) && QItemStatesHelper.IsHot(state))
      {
        float multiplyBrightness = 1.1f;
        compositeItemColorSet.Background1 = QMath.ModifyColor(retriever.RetrieveFirstDefinedColor("CompositeItemPressedBackground1"), 1f, multiplyBrightness, 1f);
        compositeItemColorSet.Background2 = QMath.ModifyColor(retriever.RetrieveFirstDefinedColor("CompositeItemPressedBackground2"), 1f, multiplyBrightness, 1f);
        compositeItemColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeItemPressedBorder");
        compositeItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextPressed");
      }
      else if (QItemStatesHelper.IsPressed(state) || QItemStatesHelper.IsChecked(state))
      {
        compositeItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeItemPressedBackground1");
        compositeItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeItemPressedBackground2");
        compositeItemColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeItemPressedBorder");
        compositeItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextPressed");
      }
      else if (QItemStatesHelper.IsExpanded(state) && parentComposite.PaintExpandedItem(item))
      {
        compositeItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeItemExpandedBackground1");
        compositeItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeItemExpandedBackground2");
        compositeItemColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeItemExpandedBorder");
        compositeItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextExpanded");
      }
      else if (QItemStatesHelper.IsHot(state))
      {
        compositeItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeItemHotBackground1");
        compositeItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeItemHotBackground2");
        compositeItemColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeItemHotBorder");
        compositeItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextHot");
      }
      else
      {
        compositeItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeItemBackground1");
        compositeItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeItemBackground2");
        compositeItemColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeItemBorder");
        compositeItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeText");
      }
      return compositeItemColorSet;
    }

    public virtual QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return QCompositeItemBase.GetDefaultCompositeItemColorSet(destinationObject, state, this.Composite, (IQColorRetriever) this);
    }

    public virtual Color RetrieveFirstDefinedColor(string colorName)
    {
      if (this.ColorScheme != null)
        return this.ColorScheme[colorName].Current;
      return this.Composite != null ? (Color) this.Composite.ColorScheme[colorName] : Color.Empty;
    }

    bool IQMouseHandler.HandleMouseMove(MouseEventArgs e) => this.HandleMouseMove(e);

    bool IQMouseHandler.HandleMouseDown(MouseEventArgs e) => this.HandleMouseDown(e);

    bool IQMouseHandler.HandleMouseUp(MouseEventArgs e) => this.HandleMouseUp(e);

    IList IQDesignableItemContainer.AssociatedComponents => this.AssociatedComponents;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    protected virtual IList AssociatedComponents => this.Parts != null ? (IList) new ArrayList((ICollection) this.Parts) : (IList) new ArrayList();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    protected internal virtual QPartCollection DesignablePartsCollection => (QPartCollection) null;

    void IQTimerClient.Tick(QTimerManager manager)
    {
      QComposite composite = this.Composite;
      if (composite != null)
      {
        if (this.m_oTimerState.Expanding)
        {
          this.m_oTimerState.ExpandingElapsed += manager.Interval;
          if (this.m_oTimerState.ExpandingElapsed > composite.Configuration.GetExpandingDelay((IQPart) composite))
          {
            this.m_oTimerState.Expanding = false;
            composite.ExpandItem(this as QCompositeItem, this.m_oTimerState.ExpandingType);
          }
        }
        else if (this.m_oTimerState.Collapsing)
        {
          this.m_oTimerState.CollapsingElapsed += manager.Interval;
          if (this.m_oTimerState.CollapsingElapsed > composite.Configuration.GetExpandingDelay((IQPart) composite))
          {
            this.m_oTimerState.Collapsing = false;
            composite.CollapseItem(this as QCompositeItem, this.m_oTimerState.ExpandingType);
          }
        }
      }
      this.UpdateTimerRegistration();
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

    IQComponentHost IQHostedComponent.ComponentHost => this.CustomComponentHost;

    void IQHostedComponent.SetComponentHost(
      IQComponentHost value,
      bool removeFromCurrentHost)
    {
      this.SetCustomComponentHost(value, removeFromCurrentHost);
    }

    void IQItemStatesImplementation.AdjustState(
      QItemStates state,
      bool setValue,
      object additionalInfo)
    {
      if (additionalInfo is QCompositeActivationType activationType)
        this.AdjustState(state, setValue, activationType);
      else
        this.AdjustState(state, setValue, QCompositeActivationType.None);
    }

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

    internal struct QCompositeTimerState
    {
      private QCompositeTimerAction m_eAction;
      private int m_iFadingHotElapsed;
      private int m_iExpandingElapsed;
      private int m_iCollapsingElapsed;
      private QCompositeActivationType m_eExpandingType;

      public int FadingHotElapsed
      {
        get => this.m_iFadingHotElapsed;
        set => this.m_iFadingHotElapsed = value;
      }

      public bool Expanding
      {
        get => (this.m_eAction & QCompositeTimerAction.Expanding) == QCompositeTimerAction.Expanding;
        set
        {
          if (this.Expanding == value)
            return;
          if (value)
            this.m_eAction |= QCompositeTimerAction.Expanding;
          else
            this.m_eAction &= ~QCompositeTimerAction.Expanding;
          this.ExpandingElapsed = 0;
        }
      }

      public int ExpandingElapsed
      {
        get => this.m_iExpandingElapsed;
        set => this.m_iExpandingElapsed = value;
      }

      public QCompositeActivationType ExpandingType
      {
        get => this.m_eExpandingType;
        set => this.m_eExpandingType = value;
      }

      public bool Collapsing
      {
        get => (this.m_eAction & QCompositeTimerAction.Collapsing) == QCompositeTimerAction.Collapsing;
        set
        {
          if (this.Collapsing == value)
            return;
          if (value)
            this.m_eAction |= QCompositeTimerAction.Collapsing;
          else
            this.m_eAction &= ~QCompositeTimerAction.Collapsing;
          this.CollapsingElapsed = 0;
        }
      }

      public QCompositeTimerAction CurrentAction
      {
        get => this.m_eAction;
        set
        {
          this.Expanding = (value & QCompositeTimerAction.Expanding) == QCompositeTimerAction.Expanding;
          this.Collapsing = (value & QCompositeTimerAction.Collapsing) == QCompositeTimerAction.Collapsing;
        }
      }

      public int CollapsingElapsed
      {
        get => this.m_iCollapsingElapsed;
        set => this.m_iCollapsingElapsed = value;
      }

      public bool NeedTimer => this.m_eAction != QCompositeTimerAction.None;
    }
  }
}
