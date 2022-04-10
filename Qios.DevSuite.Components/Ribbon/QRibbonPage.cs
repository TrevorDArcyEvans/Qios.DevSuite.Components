// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPage
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
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonPageDesigner), typeof (IDesigner))]
  [ToolboxItem(false)]
  public class QRibbonPage : 
    QTabPage,
    IQManagedLayoutParent,
    IQHotkeyItem,
    IQNavigationItem,
    IQSupportInitialize,
    ISupportInitialize,
    IQCompositeContainer,
    IQDesignableItemContainer,
    IQCompositeEventRaiser,
    IQCompositeItemEventRaiser,
    IQCompositeItemEventPublisher
  {
    private QComposite m_oComposite;
    private bool m_bIsInitializing;
    private int m_iSuspendChangeNotification;
    private bool m_bPerformingLayout;
    private string m_sHotkeyText;
    private Keys[] m_aHotkeySequence;
    private QHotkeyWindow m_oHotkeyWindow;
    private EventHandler m_oCurrentMouseOverControlMouseLeaveHandler;
    private IQHotkeyItem m_oParentHotkeyItem;
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

    public QRibbonPage()
    {
      this.SuspendLayout();
      this.m_oCurrentMouseOverControlMouseLeaveHandler = new EventHandler(this.CurrentMouseOverControl_MouseLeave);
      this.SetStyle(ControlStyles.Selectable, false);
      this.m_oComposite = this.CreateComposite();
      this.ResumeLayout(false);
    }

    protected virtual QComposite CreateComposite() => (QComposite) new QRibbonPageComposite((IQCompositeContainer) this, this.CreateCompositeConfiguration(), this.ColorScheme);

    protected virtual QCompositeConfiguration CreateCompositeConfiguration() => (QCompositeConfiguration) new QRibbonPageCompositeConfiguration();

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QCompositeToolTipConfiguration();

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

    [Description("Gets raised when the item is expanded")]
    [QWeakEvent]
    [Category("QEvents")]
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

    [QWeakEvent]
    [Category("QEvents")]
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

    [Description("Gets raised when a layer of the QCompositeItem is painted")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositePaintStageEventHandler PaintItem
    {
      add => this.m_oItemPaintStageDelegate = QWeakDelegate.Combine(this.m_oItemPaintStageDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemPaintStageDelegate = QWeakDelegate.Remove(this.m_oItemPaintStageDelegate, (Delegate) value);
    }

    [Description("Gets raised when the QCompositeItemBase is activating")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeCancelEventHandler ItemActivating
    {
      add => this.m_oItemActivating = QWeakDelegate.Combine(this.m_oItemActivating, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivating = QWeakDelegate.Remove(this.m_oItemActivating, (Delegate) value);
    }

    [Description("Gets raised when the QCompositeItemBase is activated")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeEventHandler ItemActivated
    {
      add => this.m_oItemActivated = QWeakDelegate.Combine(this.m_oItemActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemActivated = QWeakDelegate.Remove(this.m_oItemActivated, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QRibbonPageComposite RibbonPageComposite => this.m_oComposite as QRibbonPageComposite;

    [Description("Gets or sets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QRibbonPageAppearance Appearance => (QRibbonPageAppearance) base.Appearance;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new Control.ControlCollection Controls => base.Controls;

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QRibbonPageAppearance();

    protected override QTabButton CreateTabButton() => (QTabButton) new QRibbonTabButton((IQTabButtonSource) this);

    protected override string BackColorPropertyName => "RibbonPageBackground1";

    protected override string BackColor2PropertyName => "RibbonPageBackground2";

    protected override string BorderColorPropertyName => "RibbonPageBorder";

    [Category("QBehavior")]
    [Description("Gets the collection of QRibbonItems of this QRibbon")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QRibbonPageItemCollectionEditor), typeof (UITypeEditor))]
    public QPartCollection Items => this.m_oComposite.Items;

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonSeparator)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonPanel)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonPage)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
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

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used for child composites")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    public virtual QColorScheme ChildCompositeColorScheme
    {
      get => this.m_oComposite.ChildCompositeColorScheme;
      set => this.m_oComposite.ChildCompositeColorScheme = value;
    }

    public bool ShouldSerializeChildCompositeConfiguration() => !this.m_oComposite.ChildCompositeConfiguration.IsSetToDefaultValues();

    public void ResetChildCompositeConfiguration() => this.m_oComposite.ChildCompositeConfiguration.SetToDefaultValues();

    [Description("Contains the ChildCompositeConfiguration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QCompositeConfiguration ChildCompositeConfiguration
    {
      get => this.m_oComposite.ChildCompositeConfiguration;
      set => this.m_oComposite.ChildCompositeConfiguration = value;
    }

    public bool ShouldSerializeChildWindowConfiguration() => !this.ChildWindowConfiguration.IsSetToDefaultValues();

    public void ResetChildWindowConfiguration() => this.ChildWindowConfiguration.SetToDefaultValues();

    [Description("Contains the ChildWindowConfiguration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QCompositeWindowConfiguration ChildWindowConfiguration
    {
      get => this.m_oComposite.ChildWindowConfiguration;
      set => this.m_oComposite.ChildWindowConfiguration = value;
    }

    public bool ShouldSerializeConfiguration() => !this.m_oComposite.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.m_oComposite.Configuration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QRibbonPage. DefaultValues are inherited from QRibbon.PageConfiguration")]
    [Category("QAppearance")]
    public QRibbonPageCompositeConfiguration Configuration => this.m_oComposite.Configuration as QRibbonPageCompositeConfiguration;

    [Description("Gets or sets the configuration of the QRibbonTabButton for this QRibbonPage. DefaultValues are inherited from from ButtonConfiguration in QRibbon.TabStripConfiguration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QButtonAppearance")]
    public QRibbonTabButtonConfiguration ButtonConfiguration
    {
      get => base.ButtonConfiguration as QRibbonTabButtonConfiguration;
      set => this.ButtonConfiguration = value;
    }

    [Category("Appearance")]
    [Description("Gets or sets a possible hotkey text. This text is displayed on the Hotkey window")]
    [Localizable(true)]
    [DefaultValue(null)]
    public string HotkeyText
    {
      get => this.m_sHotkeyText;
      set
      {
        this.m_sHotkeyText = value;
        this.m_aHotkeySequence = QHotkeyHelper.GetHotkeySequence(this.m_sHotkeyText);
      }
    }

    internal IQCompositeItemEventRaiser ParentCompositeItemEventRaiser => (IQCompositeItemEventRaiser) this.Ribbon;

    internal IQCompositeEventRaiser ParentCompositeEventRaiser => (IQCompositeEventRaiser) this.Ribbon;

    Control IQManagedLayoutParent.Control => (Control) this;

    void IQManagedLayoutParent.HandleChildObjectChanged(
      bool performLayout,
      Rectangle invalidateRectangle)
    {
      this.HandleChildObjectChanged(performLayout, invalidateRectangle);
    }

    protected virtual void HandleChildObjectChanged(
      bool performLayout,
      Rectangle invalidateRectangle)
    {
      if (this.m_iSuspendChangeNotification > 0)
        return;
      if (performLayout)
      {
        if (!this.m_bPerformingLayout)
          this.PerformLayout();
        this.Invalidate(this.ClientRectangle, true);
      }
      else
        this.Invalidate(invalidateRectangle, true);
    }

    [Browsable(false)]
    public QRibbon Ribbon => this.TabControl as QRibbon;

    [Browsable(false)]
    public bool IsPerformingLayout => this.m_bPerformingLayout;

    public int SuspendChangeNotification()
    {
      ++this.m_iSuspendChangeNotification;
      return this.m_iSuspendChangeNotification;
    }

    public int ResumeChangeNotification(bool notifyChange)
    {
      this.m_iSuspendChangeNotification = Math.Max(0, --this.m_iSuspendChangeNotification);
      if (notifyChange)
        ((IQManagedLayoutParent) this).HandleChildObjectChanged(true, Rectangle.Empty);
      return this.m_iSuspendChangeNotification;
    }

    [Browsable(false)]
    public int ChangeNotificationSuspended => this.m_iSuspendChangeNotification;

    protected override void UpdateTabButtonPaintParams(QTabButtonPaintParams paintParams)
    {
      paintParams.ButtonBackground1 = this.RetrieveFirstDefinedColor("RibbonTabButtonBackground1");
      paintParams.ButtonBackground2 = this.RetrieveFirstDefinedColor("RibbonTabButtonBackground2");
      paintParams.ButtonBorder = this.RetrieveFirstDefinedColor("RibbonTabButtonBorder");
      paintParams.ButtonText = this.RetrieveFirstDefinedColor("RibbonTabButtonText");
      paintParams.ButtonTextDisabled = this.RetrieveFirstDefinedColor("RibbonTabButtonTextDisabled");
      paintParams.ButtonShade = this.RetrieveFirstDefinedColor("RibbonTabButtonShade");
      paintParams.ButtonActiveBackground1 = this.RetrieveFirstDefinedColor("RibbonTabButtonActiveBackground1");
      paintParams.ButtonActiveBackground2 = this.RetrieveFirstDefinedColor("RibbonTabButtonActiveBackground2");
      paintParams.ButtonActiveBorder = this.RetrieveFirstDefinedColor("RibbonTabButtonActiveBorder");
      paintParams.ButtonActiveText = this.RetrieveFirstDefinedColor("RibbonTabButtonActiveText");
      paintParams.ButtonHotBackground1 = this.RetrieveFirstDefinedColor("RibbonTabButtonHotBackground1");
      paintParams.ButtonHotBackground2 = this.RetrieveFirstDefinedColor("RibbonTabButtonHotBackground2");
      paintParams.ButtonHotBorder = this.RetrieveFirstDefinedColor("RibbonTabButtonHotBorder");
      paintParams.ButtonHotText = this.RetrieveFirstDefinedColor("RibbonTabButtonHotText");
      paintParams.IconReplace = Color.FromArgb((int) byte.MaxValue, 0, 0);
      paintParams.IconReplaceColorWith = this.RetrieveFirstDefinedColor("RibbonTabButtonActiveText");
    }

    protected override QBalloon CreateBalloon() => (QBalloon) new QCompositeBalloon();

    protected override void OnVisibleChanged(EventArgs e)
    {
      int num = this.Visible ? 1 : 0;
      base.OnVisibleChanged(e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.m_bPerformingLayout = true;
      Rectangle clientRectangle = this.ClientRectangle;
      QPartLayoutContext fromControl = QPartLayoutContext.CreateFromControl((Control) this);
      this.m_oComposite.LayoutEngine.PerformLayout(clientRectangle, (IQPart) this.m_oComposite, fromControl);
      fromControl.Dispose();
      Size size = this.Composite.CalculatedProperties.OuterBounds.Size;
      if (size.Height == this.ClientSize.Height)
      {
        int width1 = size.Width;
        int width2 = this.ClientSize.Width;
      }
      this.m_bPerformingLayout = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      QPartPaintContext fromControl = QPartPaintContext.CreateFromControl((Control) this, e.Graphics);
      this.m_oComposite.PaintComposite(fromControl);
      fromControl.Dispose();
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
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.m_oComposite.HandleMouseUp(e);
      base.OnMouseUp(e);
    }

    protected override void HandleScrollStep()
    {
      base.HandleScrollStep();
      if (this.m_oHotkeyWindow == null || !this.m_oHotkeyWindow.ShouldBeVisible)
        return;
      this.m_oHotkeyWindow.Location = this.GetHotkeyWindowPosition(this.m_oHotkeyWindow.Size);
      bool flag = this.HotkeyWindowInScrollBounds();
      if (flag && !this.m_oHotkeyWindow.Visible)
      {
        this.m_oHotkeyWindow.Show();
      }
      else
      {
        if (flag || !this.m_oHotkeyWindow.Visible)
          return;
        this.m_oHotkeyWindow.Hide();
      }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      if (this.m_oComposite == null)
        return;
      this.m_oComposite.Enabled = this.Enabled;
    }

    protected virtual void OnCompositeKeyPress(QCompositeKeyboardCancelEventArgs e)
    {
      if (this.ParentCompositeEventRaiser != null)
        this.ParentCompositeEventRaiser.RaiseCompositeKeyPress(e);
      this.m_oCompositeKeyPress = QWeakDelegate.InvokeDelegate(this.m_oCompositeKeyPress, (object) this, (object) e);
    }

    protected virtual void OnSelectedItemChanged(QCompositeEventArgs e)
    {
      if (this.ParentCompositeEventRaiser != null)
        this.ParentCompositeEventRaiser.RaiseSelectedItemChanged(e);
      this.m_oSelectedItemChanged = QWeakDelegate.InvokeDelegate(this.m_oSelectedItemChanged, (object) this, (object) e);
    }

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaisePaintItem(e);
      this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);
    }

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemActivating(e);
      this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);
    }

    protected virtual void OnItemActivated(QCompositeEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemActivated(e);
      this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);
    }

    protected virtual void OnItemSelected(QCompositeEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemSelected(e);
      this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);
    }

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemExpanded(e);
      this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);
    }

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e)
    {
      if (e.Composite == this.m_oComposite)
        this.Refresh();
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemExpanding(e);
      this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsed(QCompositeEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemCollapsed(e);
      this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e)
    {
      if (this.ParentCompositeItemEventRaiser != null)
        this.ParentCompositeItemEventRaiser.RaiseItemCollapsing(e);
      this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);
    }

    private void PanelConfiguration_ConfigurationChanged(object sender, EventArgs e) => this.HandleChildObjectChanged(true, this.ClientRectangle);

    private void ContentConfiguration_ConfigurationChanged(object sender, EventArgs e) => this.HandleChildObjectChanged(true, this.ClientRectangle);

    void IQCompositeEventRaiser.RaiseUsedToolTipTextChanged(
      QCompositeEventArgs e)
    {
      if (e.Composite != this.m_oComposite)
        return;
      this.ToolTipText = this.m_oComposite.UsedToolTipText;
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

    string IQHotkeyItem.HotkeyText => this.HotkeyText;

    bool IQHotkeyItem.HasHotkey => this.TabButton.Visible && this.m_aHotkeySequence != null && this.m_aHotkeySequence.Length > 0;

    bool IQHotkeyItem.MatchesHotkeySequence(Keys[] sequence, bool exact) => QHotkeyHelper.MatchesHotkeySequence(this.m_aHotkeySequence, sequence, exact);

    QHotkeyWindow IQHotkeyItem.HotKeyWindow
    {
      get => this.m_oHotkeyWindow;
      set => this.m_oHotkeyWindow = value;
    }

    void IQHotkeyItem.ShowHotkeyWindow(bool show)
    {
      if (this.m_oHotkeyWindow == null)
        return;
      if (show)
      {
        this.m_oHotkeyWindow.ShouldBeVisible = true;
        if (!this.HotkeyWindowInScrollBounds())
          return;
        this.m_oHotkeyWindow.Show();
      }
      else
      {
        this.m_oHotkeyWindow.ShouldBeVisible = false;
        this.m_oHotkeyWindow.Hide();
      }
    }

    IQHotkeyItem IQHotkeyItem.ParentHotkeyItem
    {
      get => this.m_oParentHotkeyItem;
      set => this.m_oParentHotkeyItem = value;
    }

    bool IQHotkeyItem.HasChildHotkeyItems => true;

    void IQHotkeyItem.AddChildHotkeyItems(IList list) => QPartHelper.AddAllHotkeyItems((IQPart) this.m_oComposite, list);

    private bool HotkeyWindowInScrollBounds() => this.TabButton.TabStrip.CalculatedButtonAreaBounds.IntersectsWith(this.TabButton.BoundsToControl);

    private Point GetHotkeyWindowPosition(Size hotkeyWindowSize)
    {
      if (this.TabControl == null || this.TabButton == null)
        return Point.Empty;
      Rectangle screen = this.TabControl.RectangleToScreen(this.TabButton.BoundsToControl);
      Rectangle rectangle = QMath.AlignElement(hotkeyWindowSize, this.ButtonConfiguration.HotkeyWindowAlignment, screen, true);
      return new Point(rectangle.Location.X + this.ButtonConfiguration.HotkeyWindowRelativeOffset.X, rectangle.Location.Y + this.ButtonConfiguration.HotkeyWindowRelativeOffset.Y);
    }

    Point IQHotkeyItem.GetHotkeyWindowPosition(Size hotkeyWindowSize) => this.GetHotkeyWindowPosition(hotkeyWindowSize);

    void IQNavigationItem.Select(
      bool select,
      QNavigationSelectionReason reason,
      QNavigationActivationType activationType)
    {
      if (this.TabButton == null || this.TabButton.TabStrip == null)
        return;
      if (select)
      {
        this.TabButton.TabStrip.HotButton = this.TabButton;
        this.TabButton.TabStrip.ScrollButtonIntoView(this.TabButton, this.TabButton.TabStrip.Configuration.UseScrollAnimation, true);
        if (reason != QNavigationSelectionReason.OneItemOnly)
          return;
        this.TabButton.TabStrip.ActiveButton = this.TabButton;
      }
      else
      {
        if (this.TabButton.TabStrip.HotButton != this.TabButton)
          return;
        this.TabButton.TabStrip.HotButton = (QTabButton) null;
      }
    }

    void IQNavigationItem.Activate(
      bool activate,
      QNavigationActivationReason reason,
      QNavigationActivationType activationType)
    {
      if (!activate)
        return;
      ((IQTabButtonSource) this).ActivateSource();
    }

    bool IQNavigationItem.Enabled => this.TabButton.IsAccessible;

    bool IQNavigationItem.Visible => this.TabButton.Visible;

    [Browsable(false)]
    public bool IsInitializing => this.m_bIsInitializing;

    public void BeginInit()
    {
      this.m_bIsInitializing = true;
      this.SuspendChangeNotification();
    }

    public void EndInit()
    {
      this.m_bIsInitializing = false;
      this.ResumeChangeNotification(true);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QComposite Composite => this.m_oComposite;

    bool IQCompositeContainer.CanClose => false;

    IQCompositeContainer IQCompositeContainer.ParentContainer => (IQCompositeContainer) null;

    bool IQCompositeContainer.Close(QCompositeActivationType closeType) => false;

    Cursor IQCompositeContainer.Cursor
    {
      get => this.Cursor;
      set => this.Cursor = value;
    }

    bool IQCompositeContainer.IsFocused => this.Ribbon != null && this.Ribbon.HasSimulatedFocus;

    Rectangle IQCompositeContainer.RectangleToScreen(
      Rectangle rectangle)
    {
      return this.RectangleToScreen(rectangle);
    }

    Size IQCompositeContainer.Size => this.Size;

    Control IQCompositeContainer.Control => (Control) this;

    bool IQCompositeContainer.ContainsControl(Control control)
    {
      Control parent = control.Parent;
      while (parent != null && parent != this)
        parent = parent.Parent;
      return parent == this;
    }

    IList IQDesignableItemContainer.AssociatedComponents => this.AssociatedComponents;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected virtual IList AssociatedComponents => (IList) new ArrayList((ICollection) this.Items);
  }
}
