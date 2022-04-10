// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItem
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
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeItemDesigner), typeof (IDesigner))]
  [DefaultEvent("ItemActivated")]
  public class QCompositeItem : QCompositeItemBase, IQHotkeyItem, IQNavigationItem, IQComponentHost
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Region m_oSavedPaintRegion;
    private bool m_bCloseMenuOnActivate = true;
    private bool m_bUserHasRightToExecute = true;
    private Cursor m_oCursor;
    private string m_sHotkeyText;
    private Keys[] m_aHotkeySequence;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QHotkeyWindow m_oHotkeyWindow;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQHotkeyItem m_oParentHotkeyItem;
    private string m_sToolTipText;
    private QCommandUserRightBehavior m_eUserRightBehavior = QCommandUserRightBehavior.DisableWhenNoRight;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeWindow m_oChildWindow;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeWindow m_oCustomChildWindow;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCollection m_oChildParts;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCollection m_oOriginalChildParts;
    private Shortcut m_eShortcut;
    private bool m_bSuppressShortcutToSystem = true;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeItemTemplate m_oTemplate;

    protected QCompositeItem(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeItem(QCompositeItemCreationOptions options)
      : base(options)
    {
      if ((options & QCompositeItemCreationOptions.CreateChildItemsCollection) != QCompositeItemCreationOptions.CreateChildItemsCollection)
        return;
      this.m_oChildParts = new QPartCollection((IQPart) this, (IQManagedLayoutParent) null);
    }

    public QCompositeItem()
      : this(QCompositeItemCreationOptions.Default)
    {
    }

    internal QCompositeItem(object contentObject)
      : this(QCompositeItemCreationOptions.None)
    {
      this.PutContentObject(contentObject);
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) new QPartShapePainter()
      {
        DrawOnBounds = QPartBoundsType.Bounds,
        Options = QPainterOptions.FillBackground,
        Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape | QShapePainterOptions.StayWithinBounds)
      });
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Foreground, (IQPartObjectPainter) new QPartShapePainter()
      {
        DrawOnBounds = QPartBoundsType.Bounds,
        Options = QPainterOptions.FillForeground,
        Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds)
      });
      return currentPainters;
    }

    public override IQPartLayoutEngine LayoutEngine
    {
      get
      {
        if (this.IsLayoutEngineSet)
          return base.LayoutEngine;
        if (this.Configuration.Layout == QCompositeItemLayout.Auto)
          return this.ParentPart != null && this.ParentPart.LayoutEngine is QPartTableLayoutEngine ? QPartTableRowLayoutEngine.Default : (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
        if (this.Configuration.Layout == QCompositeItemLayout.Linear)
          return (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
        if (this.Configuration.Layout == QCompositeItemLayout.Flow)
          return (IQPartLayoutEngine) QPartFlowLayoutEngine.Default;
        return this.Configuration.Layout == QCompositeItemLayout.Table ? (IQPartLayoutEngine) QPartTableLayoutEngine.Default : base.LayoutEngine;
      }
      set => base.LayoutEngine = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeItemConfiguration();

    [Category("QBehavior")]
    [DefaultValue(null)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the mouse cursor for this QCompositeItem")]
    [Browsable(true)]
    public override Cursor Cursor
    {
      get => this.m_oCursor;
      set => this.m_oCursor = value;
    }

    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    [Browsable(true)]
    [Category("QBehavior")]
    [Description("Gets the collection of CompositeItems of this QCompositeItem. These are the items that are displayed in this QCompositeItem.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QPartCollection Items => this.Parts;

    protected override IList AssociatedComponents
    {
      get
      {
        IList associatedComponents = base.AssociatedComponents;
        if (this.CustomChildWindow == null)
        {
          if (this.ChildItems != null)
            this.ChildItems.CopyTo(associatedComponents);
        }
        else
          associatedComponents.Add((object) this.CustomChildWindow);
        return associatedComponents;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected internal override QPartCollection DesignablePartsCollection => this.Items;

    [DefaultValue(null)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Localizable(true)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the ToolTipText. This must contain Xml as used with QMarkupText. The ToolTip, see ToolTipConfiguration, must be enabled for this to show.")]
    [Category("QAppearance")]
    public override string ToolTipText
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

    [Category("QAppearance")]
    [Description("Gets or sets the HotkeyText.")]
    [DefaultValue(null)]
    [Localizable(true)]
    public virtual string HotkeyText
    {
      get => this.m_sHotkeyText;
      set
      {
        this.m_sHotkeyText = value;
        this.m_aHotkeySequence = QHotkeyHelper.GetHotkeySequence(this.m_sHotkeyText);
      }
    }

    [Browsable(false)]
    public Keys[] HotkeySequence => this.m_aHotkeySequence;

    [Browsable(false)]
    public bool HasChildItems => this.m_oChildParts != null && this.m_oChildParts.Count > 0;

    [Browsable(false)]
    public virtual bool CanExpand => this.HasChildItems || this.m_oCustomChildWindow != null;

    [Browsable(false)]
    public QCompositeWindow ChildWindow
    {
      get
      {
        if (this.m_oCustomChildWindow != null)
          return this.m_oCustomChildWindow;
        if (this.m_oChildWindow == null)
          this.m_oChildWindow = this.CreateChildWindow();
        return this.m_oChildWindow;
      }
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeItem)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [DefaultValue(null)]
    [Category("QBehavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a custom child window. When this is set, the child items, configuration and colorscheme of the custom child window are used instead of the current settings")]
    public virtual QCompositeWindow CustomChildWindow
    {
      get => this.m_oCustomChildWindow;
      set => this.SetCustomChildWindow(value, true, true);
    }

    internal void SetCustomChildWindow(
      QCompositeWindow value,
      bool removeFromCurrentItem,
      bool notifyChange)
    {
      if (value != null)
      {
        for (IQPart parentPart = this.ParentPart; parentPart != null; parentPart = parentPart.ParentPart)
        {
          if (parentPart == value.Composite)
            throw new InvalidOperationException(QResources.GetException("QCompositeItem_CustomChildWindowCircularReference"));
        }
      }
      if (this.m_oCustomChildWindow != null)
      {
        this.m_oCustomChildWindow.SetCustomComponentHost((IQComponentHost) null, false);
        if (this.m_oCustomChildWindow.Items == this.m_oChildParts)
          this.m_oChildParts = (QPartCollection) null;
      }
      this.m_oCustomChildWindow = value;
      if (this.m_oCustomChildWindow != null)
      {
        this.m_oCustomChildWindow.SetCustomComponentHost((IQComponentHost) this, removeFromCurrentItem);
        this.m_oCustomChildWindow.Composite.PutParentPart((IQPart) this);
        if (this.m_oOriginalChildParts == null && this.m_oChildParts != null)
          this.m_oOriginalChildParts = this.m_oChildParts;
        this.m_oChildParts = this.m_oCustomChildWindow.Items;
      }
      else if (this.m_oOriginalChildParts != null)
        this.m_oChildParts = this.m_oOriginalChildParts;
      if (!notifyChange)
        return;
      this.HandleChange(true);
    }

    private bool ShouldSerializeChildItems() => this.m_oCustomChildWindow == null;

    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    [Description("Gets the child collection of CompositeItems of this QCompositeItem. The child collection will be shown in a QCompositeWindow")]
    public virtual QPartCollection ChildItems => this.m_oChildParts;

    protected internal virtual void ConfigureChildWindow()
    {
    }

    [Browsable(false)]
    public QComposite ChildComposite => this.ChildWindow.Composite;

    protected virtual QCompositeWindow CreateChildWindow() => new QCompositeWindow((IQPart) this, this.ChildItems, this.Composite.ColorScheme, (IWin32Window) this.Composite?.ParentContainer.Control);

    [DefaultValue(true)]
    [Category("QAppearance")]
    [Description("Gets or sets whether the Menu must be closed when the Item is activated")]
    public override bool CloseMenuOnActivate
    {
      get => this.m_bCloseMenuOnActivate;
      set => this.m_bCloseMenuOnActivate = value;
    }

    [DefaultValue(true)]
    [Category("QBehavior")]
    [Description("Gets or sets whether the use has right to execute the QCompositeItem")]
    public bool UserHasRightToExecute
    {
      get => this.m_bUserHasRightToExecute;
      set
      {
        if (this.UserRightBehavior == QCommandUserRightBehavior.DisableWhenNoRight)
        {
          bool isEnabled = this.IsEnabled;
          this.m_bUserHasRightToExecute = value;
          if (isEnabled == this.IsEnabled)
            return;
          this.UpdateEnabledState();
          QCompositeHelper.NotifyChildPartEnabledChanged((IQPart) this);
          this.HandleChange(false);
        }
        else if (this.UserRightBehavior == QCommandUserRightBehavior.HideWhenNoRight)
        {
          bool flag = this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll);
          this.m_bUserHasRightToExecute = value;
          if (flag == this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
            return;
          this.UpdateEnabledState();
          QCompositeHelper.NotifyChildPartVisibilityChanged((IQPart) this);
          this.HandleChange(true);
        }
        else
        {
          this.UpdateEnabledState();
          this.m_bUserHasRightToExecute = value;
        }
      }
    }

    [Description("Gets or sets what should happen when the user has no right to execute the QCompositeItem")]
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
        this.UpdateEnabledState();
        if (this.m_bUserHasRightToExecute)
          return;
        QCompositeHelper.NotifyChildPartEnabledChanged((IQPart) this);
        QCompositeHelper.NotifyChildPartVisibilityChanged((IQPart) this);
        this.HandleChange(true);
      }
    }

    [Description("Contains the ShortCut of the QCompositeItem")]
    [DefaultValue(Shortcut.None)]
    [Category("QBehavior")]
    [Localizable(true)]
    public virtual Shortcut Shortcut
    {
      get => this.m_eShortcut;
      set => this.m_eShortcut = value;
    }

    public override bool IsVisible(QPartVisibilitySelectionTypes visibilityTypes)
    {
      bool flag = !this.UserHasRightToExecute && this.UserRightBehavior == QCommandUserRightBehavior.HideWhenNoRight;
      return QPartHelper.IsVisible((IQPart) this, this.Visible && !flag, this.HiddenBecauseOfConstraints, visibilityTypes);
    }

    [Category("QAppearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the QCompositeIconBase is checked")]
    public virtual bool Checked
    {
      get => QItemStatesHelper.IsChecked(this.BaseItemState);
      set
      {
        if (this.Checked == value)
          return;
        this.BaseItemState = QItemStatesHelper.AdjustState(this.BaseItemState, QItemStates.Checked, value);
        this.HandleChange(true);
      }
    }

    [Browsable(false)]
    public override bool IsEnabled => base.IsEnabled && (this.UserHasRightToExecute || this.UserRightBehavior != QCommandUserRightBehavior.DisableWhenNoRight);

    [Browsable(false)]
    public bool IsExpanded => QItemStatesHelper.IsExpanded(this.ItemState);

    [Browsable(false)]
    public Keys ShortcutKeys => (Keys) this.m_eShortcut;

    [Browsable(false)]
    public string ShortcutString => TypeDescriptor.GetConverter(typeof (Keys)).ConvertToString((object) this.ShortcutKeys);

    [Category("QBehavior")]
    [Description("Gets or sets whether the pressed shortcut must be suppressed and not be bubbeled up to the system. Turn this off to let the system handle the shortcut.")]
    [DefaultValue(true)]
    public bool SuppressShortcutToSystem
    {
      get => this.m_bSuppressShortcutToSystem;
      set => this.m_bSuppressShortcutToSystem = value;
    }

    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Contains the template to use for creating this item. Setting this value will remove all child parts and rebuild the item from scratch.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual QCompositeItemTemplate Template
    {
      get => this.m_oTemplate;
      set
      {
        this.m_oTemplate = value;
        if (this.m_oTemplate == null)
          return;
        this.m_oTemplate.Apply(this);
      }
    }

    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeItemConfiguration Configuration
    {
      get => base.Configuration as QCompositeItemConfiguration;
      set => this.Configuration = (QContentPartConfiguration) value;
    }

    [Browsable(false)]
    public override QTristateBool HasHotState => this.Configuration.HasHotState;

    [Browsable(false)]
    public override QTristateBool HasPressedState => this.Configuration.HasPressedState;

    [Browsable(false)]
    public override QTristateBool HasCheckedState => QTristateBool.True;

    protected internal override bool MatchesHotkeySequence(Keys[] sequence, bool exact) => QHotkeyHelper.MatchesHotkeySequence(this.m_aHotkeySequence, sequence, exact);

    protected internal override bool MatchesShortcut(Keys shortcut) => shortcut == this.ShortcutKeys;

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet1 = (QColorSet) null;
      if (part == this && this.Configuration.Appearance != null)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingBackground:
            qcolorSet1 = (QColorSet) null;
            if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter1)
            {
              if (qcolorSet1 == null)
                qcolorSet1 = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
              objectPainter1.Appearance = this.Configuration.Appearance;
              objectPainter1.ColorSet = qcolorSet1;
            }
            if (this.GetObjectPainter(QPartPaintLayer.Foreground, typeof (QPartShapePainter)) is QPartShapePainter objectPainter2)
            {
              if (qcolorSet1 == null)
                qcolorSet1 = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
              objectPainter2.Appearance = this.Configuration.Appearance;
              objectPainter2.ColorSet = qcolorSet1;
              break;
            }
            break;
          case QPartPaintStage.BackgroundPainted:
            if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter3)
              this.PutLastDrawnGraphicsPath(objectPainter3.LastDrawnGraphicsPath);
            else
              this.PutLastDrawnGraphicsPath((GraphicsPath) null);
            this.m_oSavedPaintRegion = QPartHelper.AdjustPaintRegion((IQPart) this, this.LastDrawnGraphicsPath, paintContext);
            break;
          case QPartPaintStage.PaintingForeground:
            QPartHelper.RestorePaintRegion(this.m_oSavedPaintRegion, paintContext);
            this.m_oSavedPaintRegion = (Region) null;
            break;
          case QPartPaintStage.PaintFinished:
            this.PutContentClipRegion(QPartHelper.CreatePossibleContentClipRegion((IQPart) this, this.LastDrawnGraphicsPath));
            break;
        }
      }
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }

    public override object Clone()
    {
      QCompositeItem parentPart = (QCompositeItem) base.Clone();
      if (this.m_oCustomChildWindow == null && this.m_oChildParts != null)
      {
        parentPart.m_oChildParts = this.m_oChildParts.Clone() as QPartCollection;
        parentPart.m_oChildParts.SetParent((IQPart) parentPart, false);
      }
      return (object) parentPart;
    }

    public override void RemoveCloneLink()
    {
      base.RemoveCloneLink();
      if (this.m_oCustomChildWindow != null || this.m_oChildParts == null)
        return;
      QCompositeHelper.RemoveCloneLinks((IQPartCollection) this.m_oChildParts);
    }

    public override void MoveUnclonablesToClone()
    {
      base.MoveUnclonablesToClone();
      if (this.m_oCustomChildWindow != null)
      {
        (this.LastClonedItem as QCompositeItem).SetCustomChildWindow(this.m_oCustomChildWindow, false, false);
      }
      else
      {
        if (this.m_oChildParts == null)
          return;
        QCompositeHelper.MoveUnclonablesToClones((IQPartCollection) this.m_oChildParts);
      }
    }

    public override void RestoreUnclonablesFromClone()
    {
      base.RestoreUnclonablesFromClone();
      if (!(this.LastClonedItem is QCompositeItem))
        return;
      if (this.m_oCustomChildWindow != null)
      {
        this.SetCustomChildWindow(this.m_oCustomChildWindow, true, false);
      }
      else
      {
        if (this.m_oChildParts == null)
          return;
        QCompositeHelper.RestoreUnclonablesFromClones((IQPartCollection) this.m_oChildParts);
      }
    }

    protected internal override void HandleScrollingStage(
      IQScrollablePart scrollingPart,
      QScrollablePartScrollStage stage)
    {
      base.HandleScrollingStage(scrollingPart, stage);
      switch (stage)
      {
        case QScrollablePartScrollStage.Scrolling:
          if (this.m_oHotkeyWindow == null || !this.m_oHotkeyWindow.ShouldBeVisible)
            break;
          this.m_oHotkeyWindow.Location = this.GetHotkeyWindowPosition(this.m_oHotkeyWindow.Size);
          bool flag = this.HotkeyWindowInScrollBounds();
          if (flag && !this.m_oHotkeyWindow.Visible)
          {
            this.m_oHotkeyWindow.Show();
            break;
          }
          if (flag || !this.m_oHotkeyWindow.Visible)
            break;
          this.m_oHotkeyWindow.Hide();
          break;
      }
    }

    string IQHotkeyItem.HotkeyText => this.HotkeyText;

    bool IQHotkeyItem.HasHotkey => this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll) && this.m_aHotkeySequence != null && this.m_aHotkeySequence.Length > 0;

    bool IQHotkeyItem.MatchesHotkeySequence(Keys[] sequence, bool exact) => this.MatchesHotkeySequence(sequence, exact);

    QHotkeyWindow IQHotkeyItem.HotKeyWindow
    {
      get => this.m_oHotkeyWindow;
      set => this.m_oHotkeyWindow = value;
    }

    private bool HotkeyWindowInScrollBounds()
    {
      IQPart parentPart = this.ParentPart;
      if (this.m_oHotkeyWindow == null || parentPart == null || this.ParentControl == null)
        return true;
      Rectangle screen = this.ParentControl.RectangleToScreen(parentPart.CalculatedProperties.CachedScrollCorrectedBounds);
      return parentPart.CalculatedProperties.AppliedPadding.InflateRectangleWithPadding(screen, false, true).IntersectsWith(this.m_oHotkeyWindow.Bounds);
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

    bool IQHotkeyItem.HasChildHotkeyItems => false;

    void IQHotkeyItem.AddChildHotkeyItems(IList list)
    {
    }

    private Point GetHotkeyWindowPosition(Size hotkeyWindowSize)
    {
      Point hotkeyWindowPosition = Point.Empty;
      if (this.ParentControl != null)
      {
        Rectangle screen = this.ParentControl.RectangleToScreen(this.CalculatedProperties.CachedScrollCorrectedBounds);
        Rectangle rectangle = QMath.AlignElement(hotkeyWindowSize, this.Configuration.HotkeyWindowAlignment, screen, true);
        hotkeyWindowPosition = new Point(rectangle.Location.X + this.Configuration.HotkeyWindowRelativeOffset.X, rectangle.Location.Y + this.Configuration.HotkeyWindowRelativeOffset.Y);
      }
      return hotkeyWindowPosition;
    }

    Point IQHotkeyItem.GetHotkeyWindowPosition(Size hotkeyWindowSize) => this.GetHotkeyWindowPosition(hotkeyWindowSize);

    void IQNavigationItem.Activate(
      bool activate,
      QNavigationActivationReason reason,
      QNavigationActivationType activationType)
    {
      QCompositeActivationType compositeActivationType = QCompositeHelper.GetAsCompositeActivationType(activationType);
      if (activate)
      {
        if (activationType != QNavigationActivationType.Shortcut)
          this.Activate(QCompositeItemActivationOptions.Automatic, compositeActivationType);
        else
          this.Activate(QCompositeItemActivationOptions.Activate, compositeActivationType);
      }
      else if (QItemStatesHelper.IsExpanded(this.ItemState) && this.Composite != null)
        this.Composite.CollapseItem(this, compositeActivationType);
      else
        this.AdjustState(QItemStates.Hot, false, compositeActivationType);
    }

    void IQNavigationItem.Select(
      bool select,
      QNavigationSelectionReason reason,
      QNavigationActivationType activationType)
    {
      if (select)
      {
        if (this.Composite == null)
          return;
        this.Composite.SelectItem((QCompositeItemBase) this, QCompositeHelper.GetAsCompositeActivationType(activationType), true);
      }
      else
        this.AdjustState(QItemStates.Hot, false, QCompositeHelper.GetAsCompositeActivationType(activationType));
    }

    bool IQNavigationItem.Enabled => this.IsAccessible(QPartVisibilitySelectionTypes.IncludeAll);

    bool IQNavigationItem.Visible => this.IsVisible(QPartVisibilitySelectionTypes.IncludeAll);

    void IQComponentHost.SetComponent(object previousValue, object newValue)
    {
      if (previousValue == null || previousValue != this.m_oCustomChildWindow)
        return;
      this.SetCustomChildWindow(newValue as QCompositeWindow, true, true);
    }
  }
}
