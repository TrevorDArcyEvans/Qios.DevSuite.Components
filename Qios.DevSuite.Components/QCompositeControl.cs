// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeControlDesigner), typeof (IDesigner))]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QComposite.bmp")]
  [ToolboxItem(true)]
  [DefaultEvent("ItemActivated")]
  [Designer(typeof (QCompositeControlRootDesigner), typeof (IRootDesigner))]
  public class QCompositeControl : 
    QControl,
    IQSupportInitialize,
    ISupportInitialize,
    IQCompositeContainer,
    IQDesignableItemContainer,
    IQKeyboardMessageFilter,
    IQMouseHookClient,
    IQCompositeEventRaiser,
    IQCompositeItemEventRaiser,
    IQManagedLayoutParent,
    IQCompositeItemEventPublisher,
    IQNavigationHost,
    IQHotkeyHandlerHost
  {
    private bool m_bIsInitializing;
    private bool m_bFocusable;
    private QShortcutScope m_eShortcutScope;
    private bool m_bHandleAltKey;
    private bool m_bHandleShortcuts;
    private bool m_bPaintTransparentBackground;
    private bool m_bCompositeManualResize;
    private bool m_bFocusedKeyHandling;
    private bool m_bHasSimulatedFocus;
    private QMouseHooker m_oMouseHooker;
    private QKeyboardMessageFilter m_oKeyboardMessageFilter;
    private int m_iSuspendChangeNotification;
    private bool m_bPerformingLayout;
    private EventHandler m_oCurrentMouseOverControlMouseLeaveHandler;
    private QComposite m_oComposite;
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

    public QCompositeControl()
    {
      this.m_oKeyboardMessageFilter = new QKeyboardMessageFilter((IQKeyboardMessageFilter) this);
      this.m_oCurrentMouseOverControlMouseLeaveHandler = new EventHandler(this.CurrentMouseOverControl_MouseLeave);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.m_oComposite = this.CreateComposite();
    }

    protected virtual QComposite CreateComposite() => new QComposite((IQPart) null, (QPartCollection) null, (IQCompositeContainer) this, this.CreateCompositeConfiguration(), this.ColorScheme);

    protected virtual QCompositeConfiguration CreateCompositeConfiguration() => (QCompositeConfiguration) null;

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QCompositeToolTipConfiguration();

    protected override QBalloon CreateBalloon() => (QBalloon) new QCompositeBalloon();

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QComposite is about to handle navigation keys that are pressed")]
    public event QCompositeKeyboardCancelEventHandler CompositeKeyPress
    {
      add => this.m_oCompositeKeyPress = QWeakDelegate.Combine(this.m_oCompositeKeyPress, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCompositeKeyPress = QWeakDelegate.Remove(this.m_oCompositeKeyPress, (Delegate) value);
    }

    [Description("Gets raised when the selected item is changed")]
    [QWeakEvent]
    [Category("QEvents")]
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

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the item is expanded")]
    public event QCompositeExpandedEventHandler ItemExpanded
    {
      add => this.m_oItemExpanded = QWeakDelegate.Combine(this.m_oItemExpanded, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanded = QWeakDelegate.Remove(this.m_oItemExpanded, (Delegate) value);
    }

    [Description("Gets raised when the child container is expanding")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeExpandingCancelEventHandler ItemExpanding
    {
      add => this.m_oItemExpanding = QWeakDelegate.Combine(this.m_oItemExpanding, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemExpanding = QWeakDelegate.Remove(this.m_oItemExpanding, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the item is collapsing")]
    [Category("QEvents")]
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

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a layer of the QCompositeItem is painted")]
    public event QCompositePaintStageEventHandler PaintItem
    {
      add => this.m_oItemPaintStageDelegate = QWeakDelegate.Combine(this.m_oItemPaintStageDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemPaintStageDelegate = QWeakDelegate.Remove(this.m_oItemPaintStageDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Control.ControlCollection Controls => base.Controls;

    [DefaultValue(false)]
    [Description("Gets or sets whether this QCompositeControl is focusable.")]
    [Category("QBehavior")]
    public bool Focusable
    {
      get => this.m_bFocusable;
      set
      {
        if (value == this.m_bFocusable)
          return;
        this.m_bFocusable = value;
        this.SetStyle(ControlStyles.Selectable, this.m_bFocusable);
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets the scope of the shortcuts when HandleShortcutKeys is true")]
    [DefaultValue(QShortcutScope.ParentForm)]
    public QShortcutScope ShortcutScope
    {
      get => this.m_eShortcutScope;
      set => this.m_eShortcutScope = value;
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether the Alt key must be handled when the control doesn't have the focus. When you are adding this control the a QRibbon as an AdditionalNavigationControl, don't set this property")]
    [Category("QBehavior")]
    public bool HandleAltKey
    {
      get => this.m_bHandleAltKey;
      set
      {
        this.m_bHandleAltKey = value;
        this.ConfigureKeyboardFilter();
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether the shortcut keys must be handled. When you are adding this control the a QRibbon as an AdditionalNavigationControl, don't set this property")]
    [Category("QBehavior")]
    public bool HandleShortcutKeys
    {
      get => this.m_bHandleShortcuts;
      set
      {
        this.m_bHandleShortcuts = value;
        this.ConfigureKeyboardFilter();
      }
    }

    [Description("Gets or sets whether a transparent background must be painted. When this is false the background color of the parent is painted on this Control. If this is true the parent is painted on this control. Keeping this false increases performance. Set this to false when the Control is situated on a Parent with a solid background or when the control has a rectangular filled out shape.")]
    [DefaultValue(false)]
    [Category("QBehavior")]
    public virtual bool PaintTransparentBackground
    {
      get => this.m_bPaintTransparentBackground;
      set
      {
        this.m_bPaintTransparentBackground = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    public bool HasSimulatedFocus => this.m_bHasSimulatedFocus;

    private void ConfigureKeyboardFilter()
    {
      if (this.DesignMode)
        return;
      if (!this.m_oKeyboardMessageFilter.Installed && (this.HandleShortcutKeys || this.HandleAltKey || this.Composite.ExpandedItem != null))
      {
        this.m_oKeyboardMessageFilter.Install();
      }
      else
      {
        if (!this.m_oKeyboardMessageFilter.Installed || this.HandleShortcutKeys || this.HandleAltKey || this.Composite.ExpandedItem != null)
          return;
        this.m_oKeyboardMessageFilter.Uninstall();
      }
    }

    protected void HandleChildObjectChanged(bool performLayout, Rectangle invalidateRectangle)
    {
      if (this.m_iSuspendChangeNotification > 0)
        return;
      if (performLayout)
      {
        this.PerformLayout();
        this.Invalidate();
      }
      else if (invalidateRectangle.IsEmpty)
        this.Invalidate();
      else
        this.Invalidate(invalidateRectangle);
    }

    Control IQManagedLayoutParent.Control => (Control) this;

    void IQManagedLayoutParent.HandleChildObjectChanged(
      bool performLayout,
      Rectangle invalidateRectangle)
    {
      this.HandleChildObjectChanged(performLayout, invalidateRectangle);
    }

    bool IQCompositeContainer.ContainsControl(Control control)
    {
      Control parent = control.Parent;
      while (parent != null && parent != this)
        parent = parent.Parent;
      return parent == this;
    }

    IQCompositeContainer IQCompositeContainer.ParentContainer => (IQCompositeContainer) null;

    bool IQCompositeContainer.Close(QCompositeActivationType closeType)
    {
      if (!this.HasSimulatedFocus)
        return false;
      if (this.Focused)
      {
        this.Composite.ShowHotkeyWindows = false;
        this.Composite.StopProcessingHotkeys(false);
      }
      else
        this.LoseSimulatedFocus(false);
      return true;
    }

    bool IQCompositeContainer.CanClose => this.HasSimulatedFocus;

    bool IQCompositeContainer.IsFocused => this.Focused || this.HasSimulatedFocus;

    Control IQCompositeContainer.Control => (Control) this;

    IList IQDesignableItemContainer.AssociatedComponents => this.AssociatedComponents;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected virtual IList AssociatedComponents => (IList) new ArrayList((ICollection) this.Items);

    void IQCompositeEventRaiser.RaiseUsedToolTipTextChanged(
      QCompositeEventArgs e)
    {
      if (e.Composite != this.m_oComposite)
        return;
      base.ToolTipText = this.m_oComposite.UsedToolTipText;
    }

    void IQCompositeEventRaiser.RaiseCompositeKeyPress(
      QCompositeKeyboardCancelEventArgs e)
    {
      this.OnCompositeKeyPress(e);
    }

    void IQCompositeEventRaiser.RaiseSelectedItemChanged(QCompositeEventArgs e) => this.OnSelectedItemChanged(e);

    void IQCompositeItemEventRaiser.RaisePaintItem(
      QCompositePaintStageEventArgs e)
    {
      if (e.Composite == this.Composite && e.Item == null && e.Stage == QPartPaintStage.BackgroundPainted && this.BackgroundImage != null)
        QControlPaint.DrawImage(this.BackgroundImage, this.BackgroundImageAlign, this.BackgroundImageOffset, this.ClientRectangle, this.BackgroundImage.Size, e.Context.Graphics, (ImageAttributes) null, false);
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

    [Browsable(false)]
    public QComposite Composite => this.m_oComposite;

    [Browsable(false)]
    public bool IsPerformingLayout => this.m_bPerformingLayout;

    [Description("Overridden. Gets the QAppearance for the QControl. Overridden to return null. The QCompositeControl uses the appearance of its Configuration object to match the structure of the QCompositeItems.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QAppearanceBase Appearance => base.Appearance;

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) null;

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set
      {
        base.ColorScheme = value;
        this.m_oComposite.ColorScheme = value;
      }
    }

    public bool ShouldSerializeChildCompositeColorScheme() => this.ChildCompositeColorScheme.ShouldSerialize();

    public void ResetChildCompositeColorScheme() => this.ChildCompositeColorScheme.Reset();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used for child composites")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    public virtual QColorScheme ChildCompositeColorScheme
    {
      get => this.m_oComposite.ChildCompositeColorScheme;
      set => this.m_oComposite.ChildCompositeColorScheme = value;
    }

    public override string ToolTipText
    {
      get => this.m_oComposite.UsedToolTipText;
      set
      {
        this.m_oComposite.ToolTipText = value;
        base.ToolTipText = value;
      }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (this.m_oComposite == null)
        return;
      this.m_oComposite.Enabled = this.Enabled;
    }

    protected virtual void OnCompositeKeyPress(QCompositeKeyboardCancelEventArgs e) => this.m_oCompositeKeyPress = QWeakDelegate.InvokeDelegate(this.m_oCompositeKeyPress, (object) this, (object) e);

    protected virtual void OnSelectedItemChanged(QCompositeEventArgs e) => this.m_oSelectedItemChanged = QWeakDelegate.InvokeDelegate(this.m_oSelectedItemChanged, (object) this, (object) e);

    protected virtual void OnItemSelected(QCompositeEventArgs e) => this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (e.Composite == this.Composite)
        this.ConfigureKeyboardFilter();
      if (!QCompositeHelper.IsKeyboardActivationType(e.ActivationType) && this.HasSimulatedFocus)
        this.LoseSimulatedFocus(false);
      this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);
    }

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e)
    {
      if (e.Composite == this.m_oComposite)
        this.Refresh();
      this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsed(QCompositeEventArgs e)
    {
      if (e.Composite == this.Composite)
        this.ConfigureKeyboardFilter();
      this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e) => this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e) => this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e) => this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);

    protected virtual void OnItemActivated(QCompositeEventArgs e)
    {
      if (this.HasSimulatedFocus)
        this.LoseSimulatedFocus(false);
      this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);
    }

    protected override string BackColorPropertyName => "CompositeBackground1";

    protected override string BackColor2PropertyName => "CompositeBackground2";

    protected override string BorderColorPropertyName => "CompositeBorder";

    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    [Description("Gets the collection of QCompositeItems of this QCompositeControl. This are the items that can be designed.")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QPartCollection Items => this.m_oComposite.Items;

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QComposite.")]
    [Category("QAppearance")]
    public virtual QCompositeConfiguration Configuration
    {
      get => this.m_oComposite.Configuration;
      set => this.m_oComposite.Configuration = value;
    }

    public bool ShouldSerializeChildCompositeConfiguration() => !this.ChildCompositeConfiguration.IsSetToDefaultValues();

    public void ResetChildCompositeConfiguration() => this.ChildCompositeConfiguration.SetToDefaultValues();

    [Description("Contains the ChildCompositeConfiguration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QCompositeConfiguration ChildCompositeConfiguration
    {
      get => this.m_oComposite.ChildCompositeConfiguration;
      set => this.m_oComposite.ChildCompositeConfiguration = value;
    }

    public bool ShouldSerializeChildWindowConfiguration() => !this.ChildWindowConfiguration.IsSetToDefaultValues();

    public void ResetChildWindowConfiguration() => this.ChildWindowConfiguration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the ChildWindowConfiguration for the QComposite.")]
    [Category("QAppearance")]
    public virtual QCompositeWindowConfiguration ChildWindowConfiguration
    {
      get => this.m_oComposite.ChildWindowConfiguration;
      set => this.m_oComposite.ChildWindowConfiguration = value;
    }

    public int SuspendChangeNotification()
    {
      ++this.m_iSuspendChangeNotification;
      return this.m_iSuspendChangeNotification;
    }

    public int ResumeChangeNotification(bool notifyChange)
    {
      this.m_iSuspendChangeNotification = Math.Max(0, --this.m_iSuspendChangeNotification);
      if (notifyChange)
        this.HandleChildObjectChanged(true, Rectangle.Empty);
      return this.m_iSuspendChangeNotification;
    }

    [Browsable(false)]
    public int ChangeNotificationSuspended => this.m_iSuspendChangeNotification;

    public new void ResumeLayout(bool performLayout) => base.ResumeLayout(true);

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

    public void SimulateFocus() => this.SimulateFocus(this.Composite.ShouldShowHotkeyWindows());

    internal void SimulateFocus(bool shouldShowHotkeyWindows)
    {
      if (this.m_bHasSimulatedFocus)
        return;
      this.Composite.ShowHotkeyWindows = false;
      this.Composite.StartProcessingHotkeys(false);
      NativeMethods.HideCaret(IntPtr.Zero);
      this.m_bHasSimulatedFocus = true;
      this.MouseHooker.MouseHooked = true;
    }

    public void LoseSimulatedFocus() => this.LoseSimulatedFocus(false);

    internal void LoseSimulatedFocus(bool forceEvenWhenFocused)
    {
      if (!forceEvenWhenFocused && this.Focused || !this.m_bHasSimulatedFocus)
        return;
      this.m_bHasSimulatedFocus = false;
      this.Composite.ClearCurrentFocusedChildControl();
      this.Composite.StopProcessingHotkeys(true);
      this.Composite.ShowHotkeyWindows = false;
      NativeMethods.ShowCaret(IntPtr.Zero);
      this.MouseHooker.MouseHooked = false;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.SimulateFocus();
      if (Control.MouseButtons != MouseButtons.None && this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)) || this.Composite.SelectedItem != null || this.Composite.ExpandedItem != null)
        return;
      this.Composite.SelectFirstItem(QCompositeActivationType.Keyboard, true);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.Composite.CurrentFocusedChildControl != null)
        return;
      this.LoseSimulatedFocus(true);
      this.Composite.SelectItem((QCompositeItemBase) null, QCompositeActivationType.Keyboard, true);
    }

    protected override bool ProcessKeyMessage(ref Message m) => this.HandleFocusedKeyMessage(ref m) || base.ProcessKeyMessage(ref m);

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) => this.HandleFocusedKeyMessage(ref msg) || base.ProcessCmdKey(ref msg, keyData);

    private bool HandleFocusedKeyMessage(ref Message m)
    {
      this.m_bFocusedKeyHandling = true;
      bool flag = false;
      switch (m.Msg)
      {
        case 256:
        case 260:
          flag = this.HandleKeyDown((Keys) (int) m.WParam, (Control) this, m);
          break;
        case 257:
        case 261:
          flag = this.HandleKeyUp((Keys) (int) m.WParam, (Control) this, m);
          break;
      }
      this.m_bFocusedKeyHandling = false;
      return flag;
    }

    protected virtual bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      if (this.Focused && !this.m_bFocusedKeyHandling)
        return false;
      QCompositeKeyboardCancelEventArgs e = new QCompositeKeyboardCancelEventArgs(this.Composite, keys, false);
      ((IQCompositeEventRaiser) this.Composite).RaiseCompositeKeyPress(e);
      if (e.Cancel)
        return false;
      keys = e.Keys;
      if (this.Composite.ExpandedItem != null || this.Composite.CurrentFocusedChildControl != null)
        return this.Composite.HandleKeyDown(keys, destinationControl, message, false);
      if (this.Composite.IsMenuKey(keys))
      {
        this.Composite.HandleKeyDown(keys, destinationControl, message, false);
        return false;
      }
      if (!this.HasSimulatedFocus)
      {
        bool suppressToSystem = false;
        if (this.HandleShortcutKeys && this.Composite.IsShortcutKey(keys | Control.ModifierKeys) && this.Composite.HandleShortcutKey(keys | Control.ModifierKeys, out suppressToSystem))
          return suppressToSystem;
        if (this.HandleAltKey && this.Composite.IsAltWithHotkey(keys | Control.ModifierKeys))
        {
          this.SimulateFocus(false);
          if (this.Composite.HandleHotkey(keys))
            return true;
          this.LoseSimulatedFocus(false);
          return false;
        }
      }
      else if ((this.HasSimulatedFocus || this.Focused) && (!this.Focused || keys != Keys.Tab))
        return this.Composite.HandleKeyDown(keys, destinationControl, message, false);
      return false;
    }

    protected virtual bool HandleKeyUp(Keys keys, Control destinationControl, Message message)
    {
      this.Composite.HandleKeyUp(keys, destinationControl, message);
      return false;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 562 && this.m_bCompositeManualResize)
      {
        this.m_bCompositeManualResize = false;
        Point client = this.PointToClient(Control.MousePosition);
        this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 0, client.X, client.Y, 0));
      }
      base.WndProc(ref m);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      this.m_oComposite.HandleMouseWheel(e);
      base.OnMouseWheel(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      this.m_oComposite.HandleMouseMove(e);
      base.OnMouseMove(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      this.m_oComposite.HandleMouseEnter(e);
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (!QCompositeHelper.AssignMouseLeaveToPossibleCurrentControl((Control) this, this.m_oCurrentMouseOverControlMouseLeaveHandler))
        this.m_oComposite.HandleMouseLeave(e);
      base.OnMouseLeave(e);
    }

    private void CurrentMouseOverControl_MouseLeave(object sender, EventArgs e)
    {
      ((Control) sender).MouseLeave -= this.m_oCurrentMouseOverControlMouseLeaveHandler;
      if (QCompositeHelper.AssignMouseLeaveToPossibleCurrentControl((Control) this, this.m_oCurrentMouseOverControlMouseLeaveHandler))
        return;
      this.m_oComposite.HandleMouseLeave(EventArgs.Empty);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.m_oComposite.HandleMouseDown(e);
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      QCompositeResizeBorder resizeBorder = this.Composite.GetResizeBorder(new Point(e.X, e.Y));
      if (resizeBorder == QCompositeResizeBorder.None)
        return;
      this.m_bCompositeManualResize = true;
      NativeMethods.SendMessage(this.Handle, 161, new IntPtr((int) resizeBorder), IntPtr.Zero);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.m_oComposite.HandleMouseUp(e);
      base.OnMouseUp(e);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.PaintTransparentBackground)
        QControlPaint.PaintTransparentBackground((Control) this, pevent);
      else if (this.Parent != null)
        pevent.Graphics.Clear(this.Parent.BackColor);
      else
        pevent.Graphics.Clear(Control.DefaultBackColor);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      QPartPaintContext fromControl = QPartPaintContext.CreateFromControl((Control) this, e.Graphics);
      this.m_oComposite.PaintComposite(fromControl);
      fromControl.Dispose();
      this.RaisePaintEvent((object) null, e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      this.m_bPerformingLayout = true;
      base.OnLayout(levent);
      Rectangle clientRectangle = this.ClientRectangle;
      QPartLayoutContext fromControl = QPartLayoutContext.CreateFromControl((Control) this);
      this.m_oComposite.LayoutEngine.PerformLayout(clientRectangle, (IQPart) this.m_oComposite, fromControl);
      fromControl.Dispose();
      Size size = this.Composite.CalculatedProperties.OuterBounds.Size;
      BoundsSpecified specified = BoundsSpecified.None;
      if (size.Height != this.ClientSize.Height)
        specified |= BoundsSpecified.Height;
      if (size.Width != this.ClientSize.Width)
        specified |= BoundsSpecified.Width;
      if (this.DesignMode)
        this.SetBounds(0, 0, Math.Max(16, size.Width), Math.Max(16, size.Height), specified);
      else
        this.SetBounds(0, 0, size.Width, size.Height, specified);
      this.m_bPerformingLayout = false;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_oMouseHooker != null)
          this.m_oMouseHooker.Dispose();
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

    [Browsable(false)]
    public bool IsInitializing => this.m_bIsInitializing;

    public void BeginInit()
    {
      this.m_bIsInitializing = true;
      this.SuspendLayout();
      this.SuspendChangeNotification();
    }

    public void EndInit()
    {
      this.m_bIsInitializing = false;
      this.ResumeLayout(false);
      this.ResumeChangeNotification(true);
    }

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

    bool IQMouseHookClient.SuppressMessageToDestination(
      int code,
      ref NativeMethods.MOUSEHOOKSTRUCT mouseHookStruct)
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
      this.LoseSimulatedFocus(false);
      cancelMessage = false;
    }

    IQHotkeyItem IQHotkeyHandlerHost.SelectedItem => ((IQHotkeyHandlerHost) this.m_oComposite).SelectedItem;

    IQHotkeyItem IQHotkeyHandlerHost.ActivatedItem => ((IQHotkeyHandlerHost) this.m_oComposite).SelectedItem;

    void IQHotkeyHandlerHost.AddHotkeyItems(IList list) => ((IQHotkeyHandlerHost) this.m_oComposite).AddHotkeyItems(list);

    void IQHotkeyHandlerHost.ConfigureHotkeyWindow(IQHotkeyItem item) => ((IQHotkeyHandlerHost) this.m_oComposite).ConfigureHotkeyWindow(item);

    bool IQNavigationHost.HandleShortcutKey(Keys key, out bool suppressToSystem) => ((IQNavigationHost) this.m_oComposite).HandleShortcutKey(key, out suppressToSystem);

    void IQNavigationHost.SelectNextItem(bool forward, bool loop) => ((IQNavigationHost) this.m_oComposite).SelectNextItem(forward, loop);

    void IQNavigationHost.SelectFirstOrCurrentItem(bool forward) => ((IQNavigationHost) this.m_oComposite).SelectFirstOrCurrentItem(forward);

    bool IQNavigationHost.IsAccessibleForNavigation => this.Visible && this.Enabled;

    Point IQNavigationHost.LocationForOrder => this.PointToScreen(Point.Empty);

    Rectangle IQCompositeContainer.RectangleToScreen([In] Rectangle obj0) => this.RectangleToScreen(obj0);

    [SpecialName]
    Size IQCompositeContainer.get_Size() => this.Size;

    Point IQMouseHookClient.PointToClient([In] Point obj0) => this.PointToClient(obj0);
  }
}
