// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeWindow
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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeControlRootDesigner), typeof (IRootDesigner))]
  [DesignTimeVisible(true)]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QCompositeWindow), "Resources.ControlImages.QCompositeWindow.bmp")]
  [Designer(typeof (QCompositeWindowDesigner), typeof (IDesigner))]
  [DefaultEvent("ItemActivated")]
  public class QCompositeWindow : 
    QFloatingWindow,
    IQCompositeContainer,
    IQDesignableItemContainer,
    IQCompositeEventRaiser,
    IQCompositeItemEventRaiser,
    IQManagedLayoutParent,
    IQCompositeItemEventPublisher
  {
    private bool m_bCompositeManualResize;
    private QCompositeActivationType m_eLastCloseType;
    private int m_iSuspendChangeNotification;
    private int m_iLevelsToPaintOnShadeWindow = 1;
    private Size m_oRequestedSize = Size.Empty;
    private bool m_bPerformingLayout;
    private QComposite m_oComposite;
    private EventHandler m_oCurrentMouseOverControlMouseLeaveHandler;
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

    public QCompositeWindow() => this.InternalConstruct((IQPart) null, (QPartCollection) null, (QColorScheme) null);

    public QCompositeWindow(IQPart parentPart, QPartCollection items) => this.InternalConstruct(parentPart, items, (QColorScheme) null);

    public QCompositeWindow(
      IQPart parentPart,
      QPartCollection items,
      QColorScheme colorScheme,
      IWin32Window ownerWindow)
      : base(ownerWindow)
    {
      this.InternalConstruct(parentPart, items, colorScheme);
    }

    public QCompositeWindow(IWin32Window ownerWindow)
      : base(ownerWindow)
    {
      this.InternalConstruct((IQPart) null, (QPartCollection) null, (QColorScheme) null);
    }

    private void InternalConstruct(
      IQPart parentPart,
      QPartCollection items,
      QColorScheme colorScheme)
    {
      this.SuspendLayout();
      this.m_oCurrentMouseOverControlMouseLeaveHandler = new EventHandler(this.CurrentMouseOverControl_MouseLeave);
      this.MinimumClientSize = new Size(0, 0);
      this.SetStyle(ControlStyles.Selectable, false);
      if (colorScheme != null)
        this.ColorScheme = colorScheme;
      this.m_oComposite = this.CreateComposite(parentPart, items);
      this.ResumeLayout(false);
    }

    protected virtual QComposite CreateComposite(IQPart parentPart, QPartCollection items) => new QComposite(parentPart, items, (IQCompositeContainer) this, this.CreateCompositeConfiguration(), this.ColorScheme);

    protected virtual QCompositeConfiguration CreateCompositeConfiguration() => (QCompositeConfiguration) null;

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QCompositeToolTipConfiguration();

    protected override QBalloon CreateBalloon() => (QBalloon) new QCompositeBalloon();

    internal IQCompositeItemEventRaiser CustomItemEventRaiser => this.CustomComponentHost as IQCompositeItemEventRaiser;

    internal int LevelsToPaintOnShadeWindow
    {
      get => this.m_iLevelsToPaintOnShadeWindow;
      set => this.m_iLevelsToPaintOnShadeWindow = value;
    }

    [Description("Gets raised when the QComposite is about to handle navigation keys that are pressed")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositeKeyboardCancelEventHandler CompositeKeyPress
    {
      add => this.m_oCompositeKeyPress = QWeakDelegate.Combine(this.m_oCompositeKeyPress, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCompositeKeyPress = QWeakDelegate.Remove(this.m_oCompositeKeyPress, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the selected item is changed")]
    public event QCompositeEventHandler SelectedItemChanged
    {
      add => this.m_oSelectedItemChanged = QWeakDelegate.Combine(this.m_oSelectedItemChanged, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oSelectedItemChanged = QWeakDelegate.Remove(this.m_oSelectedItemChanged, (Delegate) value);
    }

    [Description("Gets raised when the QCompositeItemBase is selected")]
    [QWeakEvent]
    [Category("QEvents")]
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

    [Category("QEvents")]
    [Description("Gets raised when the item is collapsing")]
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

    [Description("Gets raised when a layer of the QCompositeItem is painted")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QCompositePaintStageEventHandler PaintItem
    {
      add => this.m_oItemPaintStageDelegate = QWeakDelegate.Combine(this.m_oItemPaintStageDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oItemPaintStageDelegate = QWeakDelegate.Remove(this.m_oItemPaintStageDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [Description("Gets raised when the QCompositeItemBase is activating")]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new Control.ControlCollection Controls => base.Controls;

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

    protected override void SetShadeWindowProperties()
    {
      base.SetShadeWindowProperties();
      if (!(this.ShadeWindow is QCompositeShadeWindow shadeWindow))
        return;
      if (this.Configuration.ShapeWindow)
      {
        shadeWindow.Shape = this.Composite.Configuration.GetAppearance((IQPart) this.Composite).Shape;
        shadeWindow.ShapeBounds = this.Composite.CalculatedProperties.Bounds;
      }
      else
      {
        shadeWindow.Shape = (QShape) null;
        shadeWindow.ShapeBounds = Rectangle.Empty;
      }
    }

    protected override void SetShadeWindowRegion() => base.SetShadeWindowRegion();

    private void UpdateShapeBounds()
    {
      if (!(this.ShadeWindow is QCompositeShadeWindow shadeWindow))
        return;
      if (this.Configuration.ShapeWindow)
      {
        if (!(shadeWindow.ShapeBounds != this.Composite.CalculatedProperties.Bounds))
          return;
        shadeWindow.ShapeBounds = this.Composite.CalculatedProperties.Bounds;
        shadeWindow.Refresh();
      }
      else
      {
        if (shadeWindow.ShapeBounds.IsEmpty)
          return;
        shadeWindow.ShapeBounds = Rectangle.Empty;
      }
    }

    protected override void SetVisibleCore(bool value)
    {
      if (value)
      {
        if (this.Configuration.ShapeWindow)
          this.AdjustRegionToShape(false);
        base.SetVisibleCore(value);
        if (!this.Configuration.ShapeWindow || !this.Configuration.ShowBackgroundShade)
          return;
        this.AdjustRegionToShape(true);
      }
      else
        base.SetVisibleCore(value);
    }

    private void AdjustRegionToShape(bool excludeBorderFromRegion)
    {
      if (this.DesignMode)
        return;
      Rectangle bounds = this.m_oComposite.CalculatedProperties.Bounds;
      QShapeAppearance appearance = this.Composite.Configuration.GetAppearance((IQPart) this.Composite);
      GraphicsPath graphicsPath = appearance.Shape.CreateGraphicsPath(bounds, DockStyle.None, QShapePathOptions.AllLines, (Matrix) null);
      if (graphicsPath != null)
      {
        Region region = new Region(graphicsPath);
        if (excludeBorderFromRegion)
        {
          Pen pen = new Pen(SystemBrushes.Control, (float) appearance.BorderWidth);
          pen.Alignment = PenAlignment.Inset;
          graphicsPath.Widen(pen);
          region.Exclude(graphicsPath);
          pen.Dispose();
        }
        this.Region = region;
      }
      else
      {
        if (this.Region == null)
          return;
        this.Region = (Region) null;
      }
    }

    protected override QFloatingWindowConfiguration CreateConfigurationInstance() => (QFloatingWindowConfiguration) new QCompositeWindowConfiguration();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the QFloatingWindowConfiguration for this QCompositeWindow.")]
    [Category("QAppearance")]
    public QCompositeWindowConfiguration Configuration
    {
      get => base.Configuration as QCompositeWindowConfiguration;
      set => this.Configuration = value;
    }

    [DefaultValue(typeof (Size), "0, 0")]
    public override Size MinimumClientSize
    {
      get => base.MinimumClientSize;
      set => base.MinimumClientSize = value;
    }

    protected override void OnUserSizing(QUserSizingEventArgs e)
    {
      if (this.Composite != null && this.Composite.Configuration != null)
      {
        Size newSize = e.NewSize;
        if (this.Composite.Configuration.MaximumSize.Width > 0 && this.Composite.Configuration.MaximumSize.Width < e.NewSize.Width)
          newSize.Width = this.Composite.Configuration.MaximumSize.Width;
        if (this.Composite.Configuration.MaximumSize.Height > 0 && this.Composite.Configuration.MaximumSize.Height < e.NewSize.Height)
          newSize.Height = this.Composite.Configuration.MaximumSize.Height;
        newSize.Width = Math.Max(newSize.Width, this.Composite.Configuration.MinimumSize.Width);
        newSize.Height = Math.Max(newSize.Height, this.Composite.Configuration.MinimumSize.Height);
        e.NewSize = newSize;
      }
      base.OnUserSizing(e);
    }

    internal QCompositeActivationType LastCloseType => this.m_eLastCloseType;

    protected override void CreateShadeWindow() => this.ShadeWindow = (QControlShadeWindow) new QCompositeShadeWindow((Control) this, this.OwnerWindow);

    public override bool Close(QFloatingWindowCloseType closeType)
    {
      this.m_eLastCloseType = QCompositeActivationTypeHelper.FromFloatingWindowCloseType(closeType);
      if (this.Visible)
        this.m_oComposite.CollapseExpandedItem(this.m_eLastCloseType);
      return base.Close(closeType);
    }

    public bool Close(QCompositeActivationType closeType)
    {
      this.m_eLastCloseType = closeType;
      if (this.Visible)
        this.m_oComposite.CollapseExpandedItem(closeType);
      return base.Close(QCompositeActivationTypeHelper.ToFloatingWindowCloseType(closeType));
    }

    Control IQManagedLayoutParent.Control => (Control) this;

    bool IQManagedLayoutParent.IsPerformingLayout => this.m_bPerformingLayout;

    protected void HandleChildObjectChanged(bool performLayout, Rectangle invalidateRectangle)
    {
      if (this.m_iSuspendChangeNotification > 0)
        return;
      if (performLayout)
      {
        this.PerformLayout();
        this.Invalidate(true);
      }
      else if (invalidateRectangle.IsEmpty)
        this.Invalidate(true);
      else
        this.Invalidate(invalidateRectangle, true);
    }

    void IQManagedLayoutParent.HandleChildObjectChanged(
      bool performLayout,
      Rectangle invalidateRectangle)
    {
      this.HandleChildObjectChanged(performLayout, invalidateRectangle);
    }

    bool IQCompositeContainer.ContainsControl(Control control) => this.ContainsControl(control);

    Cursor IQCompositeContainer.Cursor
    {
      get => this.Cursor;
      set
      {
        this.Cursor = value;
        Cursor.Current = value;
      }
    }

    IQCompositeContainer IQCompositeContainer.ParentContainer
    {
      get
      {
        QCompositeItemBase parentItem = this.m_oComposite.ParentItem;
        return parentItem == null || parentItem.Composite == null ? (IQCompositeContainer) null : parentItem.Composite.ParentContainer;
      }
    }

    bool IQCompositeContainer.CanClose => true;

    bool IQCompositeContainer.IsFocused => this.Visible;

    Control IQCompositeContainer.Control => (Control) this;

    IList IQDesignableItemContainer.AssociatedComponents => this.AssociatedComponents;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected virtual IList AssociatedComponents => (IList) new ArrayList((ICollection) this.Items);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QComposite Composite => this.m_oComposite;

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.Composite)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set
      {
        base.ColorScheme = value;
        if (this.m_oComposite == null)
          return;
        this.m_oComposite.ColorScheme = value;
      }
    }

    public bool ShouldSerializeChildCompositeColorScheme() => this.ChildCompositeColorScheme.ShouldSerialize();

    public void ResetChildCompositeColorScheme() => this.ChildCompositeColorScheme.Reset();

    [Description("Gets or sets the QColorScheme that is used for child composites")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QColorScheme ChildCompositeColorScheme
    {
      get => this.m_oComposite.ChildCompositeColorScheme;
      set => this.m_oComposite.ChildCompositeColorScheme = value;
    }

    public bool ShouldSerializeChildCompositeConfiguration() => !this.ChildCompositeConfiguration.IsSetToDefaultValues();

    public void ResetChildCompositeConfiguration() => this.ChildCompositeConfiguration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the ChildCompositeConfiguration for the QComposite.")]
    [Category("QAppearance")]
    public QCompositeConfiguration ChildCompositeConfiguration
    {
      get => this.m_oComposite.ChildCompositeConfiguration;
      set => this.m_oComposite.ChildCompositeConfiguration = value;
    }

    [Browsable(false)]
    [Description("Overridden. Gets the QAppearance for the QControl. Overridden to return null. The QCompositeControl uses the appearance of its Configuration object to match the structure of the QCompositeItems.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QAppearanceBase Appearance => base.Appearance;

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) null;

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (!this.Configuration.ShapeWindow || !this.IsHandleCreated || !NativeHelper.IsWindowVisible((Control) this))
        return;
      this.AdjustRegionToShape(this.Configuration.ShowBackgroundShade);
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

    protected virtual void OnItemSelected(QCompositeEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemSelected(e);
      this.m_oItemSelected = QWeakDelegate.InvokeDelegate(this.m_oItemSelected, (object) this, (object) e);
    }

    protected virtual void OnItemExpanded(QCompositeExpandedEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemExpanded(e);
      this.m_oItemExpanded = QWeakDelegate.InvokeDelegate(this.m_oItemExpanded, (object) this, (object) e);
    }

    protected virtual void OnItemExpanding(QCompositeExpandingCancelEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemExpanding(e);
      this.m_oItemExpanding = QWeakDelegate.InvokeDelegate(this.m_oItemExpanding, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsed(QCompositeEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemCollapsed(e);
      this.m_oItemCollapsed = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsed, (object) this, (object) e);
    }

    protected virtual void OnItemCollapsing(QCompositeCancelEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemCollapsing(e);
      this.m_oItemCollapsing = QWeakDelegate.InvokeDelegate(this.m_oItemCollapsing, (object) this, (object) e);
    }

    protected virtual void OnPaintItem(QCompositePaintStageEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaisePaintItem(e);
      this.m_oItemPaintStageDelegate = QWeakDelegate.InvokeDelegate(this.m_oItemPaintStageDelegate, (object) this, (object) e);
    }

    protected virtual void OnItemActivating(QCompositeCancelEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemActivating(e);
      this.m_oItemActivating = QWeakDelegate.InvokeDelegate(this.m_oItemActivating, (object) this, (object) e);
    }

    protected virtual void OnItemActivated(QCompositeEventArgs e)
    {
      if (this.CustomItemEventRaiser != null)
        this.CustomItemEventRaiser.RaiseItemActivated(e);
      this.m_oItemActivated = QWeakDelegate.InvokeDelegate(this.m_oItemActivated, (object) this, (object) e);
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

    protected override string BackColorPropertyName => "CompositeBackground1";

    protected override string BackColor2PropertyName => "CompositeBackground2";

    protected override string BorderColorPropertyName => "CompositeBorder";

    [Description("Gets the collection of QCompositeItems of this QCompositeControl")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QBehavior")]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    public virtual QPartCollection Items => this.m_oComposite.Items;

    public bool ShouldSerializeChildWindowConfiguration() => !this.ChildWindowConfiguration.IsSetToDefaultValues();

    public void ResetChildWindowConfiguration() => this.ChildWindowConfiguration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the ChildWindowConfiguration for the QComposite.")]
    [Category("QAppearance")]
    public QCompositeWindowConfiguration ChildWindowConfiguration
    {
      get => this.m_oComposite.ChildWindowConfiguration;
      set => this.m_oComposite.ChildWindowConfiguration = value;
    }

    public bool ShouldSerializeCompositeConfiguration() => !this.CompositeConfiguration.IsSetToDefaultValues();

    public void ResetCompositeConfiguration() => this.CompositeConfiguration.SetToDefaultValues();

    [Description("Contains the Configuration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QCompositeConfiguration CompositeConfiguration
    {
      get => this.m_oComposite.Configuration;
      set => this.m_oComposite.Configuration = value;
    }

    [Browsable(false)]
    public QCompositeItemBase ParentItem => this.m_oComposite.ParentItem;

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

    protected override void OnMouseEnter(EventArgs e)
    {
      this.m_oComposite.HandleMouseEnter(e);
      base.OnMouseEnter(e);
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
      if (!this.DesignMode)
        return;
      Brush brush = (Brush) new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White);
      pevent.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
      brush.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      QPartPaintContext fromControl = QPartPaintContext.CreateFromControl((Control) this, e.Graphics);
      this.m_oComposite.PaintComposite(fromControl);
      fromControl.Dispose();
      this.RaisePaintEvent((object) null, e);
    }

    public void PerformLayout(Size requestedSize)
    {
      this.m_oRequestedSize = requestedSize;
      this.PerformLayout();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      this.m_bPerformingLayout = true;
      base.OnLayout(levent);
      Rectangle rectangle = Rectangle.Empty;
      rectangle = !this.Configuration.UseSizeAsRequestedSize ? new Rectangle(Point.Empty, this.m_oRequestedSize) : new Rectangle(Point.Empty, this.ClientSize);
      QPartLayoutContext fromControl = QPartLayoutContext.CreateFromControl((Control) this);
      this.m_oComposite.LayoutEngine.PerformLayout(rectangle, (IQPart) this.m_oComposite, fromControl);
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
      this.UpdateShapeBounds();
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
        ((IQManagedLayoutParent) this).HandleChildObjectChanged(true, Rectangle.Empty);
      return this.m_iSuspendChangeNotification;
    }

    [Browsable(false)]
    public int ChangeNotificationSuspended => this.m_iSuspendChangeNotification;

    public override void Show(Rectangle bounds, QCommandDirections animateDirection)
    {
      this.m_oRequestedSize = bounds.Size;
      base.Show(bounds, animateDirection);
    }

    public override Rectangle CalculateBounds(
      Rectangle openingItemZone,
      Rectangle openingItemBounds,
      ref QRelativePositions openingItemRelativePosition,
      ref QCommandDirections animateDirection)
    {
      this.m_oRequestedSize = Size.Empty;
      return base.CalculateBounds(openingItemZone, openingItemBounds, ref openingItemRelativePosition, ref animateDirection);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      this.Composite.HandleWindowClosing(e);
      base.OnClosing(e);
    }

    protected override void OnClosed(EventArgs e)
    {
      this.Composite.HandleWindowClosed();
      base.OnClosed(e);
    }

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
      if (!QItemStatesHelper.IsExpanded(e.Item.ItemState) && e.Item.CloseMenuOnActivate)
        this.CloseAll(QFloatingWindowCloseType.ClickedOutsideWindow);
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
      if (e.Composite == this.m_oComposite)
        this.Refresh();
      this.OnItemExpanding(e);
    }

    void IQCompositeItemEventRaiser.RaiseItemExpanded(
      QCompositeExpandedEventArgs e)
    {
      if (e.Composite == this.Composite)
        this.MouseHooker.MouseHooked = false;
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
      if (e.Composite == this.Composite)
        this.MouseHooker.MouseHooked = true;
      this.OnItemCollapsed(e);
    }

    Rectangle IQCompositeContainer.RectangleToScreen([In] Rectangle obj0) => this.RectangleToScreen(obj0);
  }
}
