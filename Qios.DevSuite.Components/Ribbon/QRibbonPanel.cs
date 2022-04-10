// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPanel
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

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonGroupDesigner), typeof (IDesigner))]
  [DefaultEvent("ItemActivated")]
  [ToolboxItem(false)]
  public class QRibbonPanel : QCompositeGroup
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPart m_oCaptionPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeText m_oCaptionTitlePart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeItem m_oCaptionShowDialogPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPart m_oItemAreaPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private bool m_bCollapsed;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonCollapsedItem m_oCollapsedItem;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QPartCollection m_oCollapsedItems;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonPanelResizeBehavior m_eAppliedResizeBehavior;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oCaptionShowDialogItemActivated;

    protected QRibbonPanel(object sourceObject, QObjectClonerConstructOptions options)
      : base(QCompositeItemCreationOptions.None)
    {
    }

    public QRibbonPanel()
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.m_oCaptionTitlePart = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oCaptionTitlePart.ItemName = "Title";
      this.m_oCaptionTitlePart.ColorHost = (IQItemColorHost) this;
      this.m_oCaptionTitlePart.Configuration = (QCompositeTextConfiguration) this.Configuration.CaptionConfiguration.TitleConfiguration;
      this.m_oCaptionShowDialogPart = new QCompositeItem(QCompositeItemCreationOptions.None);
      this.m_oCaptionShowDialogPart.ItemName = "ShowDialog";
      this.m_oCaptionShowDialogPart.ColorHost = (IQItemColorHost) this;
      this.m_oCaptionShowDialogPart.Configuration = (QCompositeItemConfiguration) this.Configuration.CaptionConfiguration.ShowDialogConfiguration;
      this.m_oCaptionShowDialogPart.SetObjectPainter(QPartPaintLayer.Content, (IQPartObjectPainter) new QPartImagePainter());
      this.m_oCaptionPart = new QPart("Caption", false, new IQPart[2]
      {
        (IQPart) this.m_oCaptionTitlePart,
        (IQPart) this.m_oCaptionShowDialogPart
      });
      this.m_oCaptionPart.Properties = (IQPartSharedProperties) this.Configuration.CaptionConfiguration;
      this.m_oCaptionPart.SetObjectPainter(QPartPaintLayer.Background, (IQPartObjectPainter) new QPartRectanglePainter());
      this.m_oItemAreaPart = new QPart("ItemArea", false, new IQPart[0]);
      this.m_oItemAreaPart.Properties = (IQPartSharedProperties) this.Configuration.ItemAreaConfiguration;
      this.Parts.SuspendChangeNotification();
      this.Parts.Add((IQPart) this.m_oItemAreaPart, false);
      this.Parts.Add((IQPart) this.m_oCaptionPart, false);
      this.Parts.ResumeChangeNotification(false);
      this.m_oCollapsedItem = new QRibbonCollapsedItem(this.Configuration.CollapsedConfiguration, (IQItemColorHost) this);
      this.m_oCollapsedItems = new QPartCollection((IQPart) this, (IQManagedLayoutParent) null);
      this.m_oCollapsedItems.SuspendChangeNotification();
      this.m_oCollapsedItems.Add((IQPart) this.m_oCollapsedItem, false);
      this.m_oCollapsedItems.ResumeChangeNotification(false);
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QRibbonPanelConfiguration();

    public override object Clone()
    {
      QRibbonPanel qribbonPanel = base.Clone() as QRibbonPanel;
      qribbonPanel.m_oItemAreaPart = qribbonPanel.Parts["ItemArea"] as QPart;
      if (qribbonPanel.m_oItemAreaPart != null)
        qribbonPanel.m_oItemAreaPart.Properties = (IQPartSharedProperties) qribbonPanel.Configuration.ItemAreaConfiguration;
      qribbonPanel.m_oCaptionPart = qribbonPanel.Parts["Caption"] as QPart;
      if (qribbonPanel.m_oCaptionPart != null)
      {
        qribbonPanel.m_oCaptionPart.Properties = (IQPartSharedProperties) qribbonPanel.Configuration.CaptionConfiguration;
        qribbonPanel.m_oCaptionPart.SetObjectPainter(QPartPaintLayer.Background, (IQPartObjectPainter) new QPartRectanglePainter());
      }
      qribbonPanel.m_oCaptionTitlePart = qribbonPanel.m_oCaptionPart.Parts["Title"] as QCompositeText;
      if (qribbonPanel.m_oCaptionTitlePart != null)
      {
        qribbonPanel.m_oCaptionTitlePart.Configuration = (QCompositeTextConfiguration) qribbonPanel.Configuration.CaptionConfiguration.TitleConfiguration;
        qribbonPanel.m_oCaptionTitlePart.ColorHost = (IQItemColorHost) qribbonPanel;
      }
      qribbonPanel.m_oCaptionShowDialogPart = qribbonPanel.m_oCaptionPart.Parts["ShowDialog"] as QCompositeItem;
      if (qribbonPanel.m_oCaptionShowDialogPart != null)
      {
        qribbonPanel.m_oCaptionShowDialogPart.Configuration = (QCompositeItemConfiguration) qribbonPanel.Configuration.CaptionConfiguration.ShowDialogConfiguration;
        qribbonPanel.m_oCaptionShowDialogPart.ColorHost = (IQItemColorHost) qribbonPanel;
        qribbonPanel.m_oCaptionShowDialogPart.SetObjectPainter(QPartPaintLayer.Content, (IQPartObjectPainter) new QPartImagePainter());
      }
      return (object) qribbonPanel;
    }

    protected override void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      base.SetDisplayParent(displayParent);
      this.SetConfigurationBaseProperties(false);
    }

    protected virtual void SetConfigurationBaseProperties(bool raiseEvent)
    {
      if (this.RibbonPageComposite != null)
      {
        this.Configuration.Properties.SetBaseProperties(this.RibbonPageComposite.Configuration.DefaultPanelConfiguration.Properties, true, raiseEvent);
        this.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.RibbonPage.ColorScheme, false);
      }
      else
        this.Configuration.Properties.SetBaseProperties((QFastPropertyBag) null, true, raiseEvent);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QRibbonPanel. DefaultValues are inherited from QRibbonPage.PanelConfiguration")]
    [Category("QAppearance")]
    public QRibbonPanelConfiguration Configuration => base.Configuration as QRibbonPanelConfiguration;

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the ShowDialogItem is activated")]
    public event QCompositeEventHandler CaptionShowDialogItemActivated
    {
      add => this.m_oCaptionShowDialogItemActivated = QWeakDelegate.Combine(this.m_oCaptionShowDialogItemActivated, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCaptionShowDialogItemActivated = QWeakDelegate.Remove(this.m_oCaptionShowDialogItemActivated, (Delegate) value);
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonSeparator)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonPanel)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the text")]
    [Localizable(true)]
    [QDesignerMainText(true)]
    public string Title
    {
      get => this.m_oCaptionTitlePart.Title;
      set
      {
        this.m_oCaptionTitlePart.SetTitle(value, false);
        this.m_oCollapsedItem.TitlePart.SetTitle(value, false);
        this.HandleChange(true);
      }
    }

    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the HotkeyText for the item when this panel is collapsed.")]
    [Localizable(true)]
    public string HotkeyTextCollapsed
    {
      get => this.m_oCollapsedItem.HotkeyText;
      set => this.m_oCollapsedItem.HotkeyText = value;
    }

    [Category("Appearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the HotkeyText for the show dialog item.")]
    [Localizable(true)]
    public string HotkeyTextShowDialog
    {
      get => this.m_oCaptionShowDialogPart.HotkeyText;
      set => this.m_oCaptionShowDialogPart.HotkeyText = value;
    }

    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the Icon. This Icon is displayed when the panel is collapsed.")]
    public Icon Icon
    {
      get => this.m_oCollapsedItem.IconPart.Icon;
      set
      {
        this.m_oCollapsedItem.IconPart.SetIcon(value, false);
        if (!this.m_bCollapsed)
          return;
        this.HandleChange(true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection of QRibbonItems of this QRibbon")]
    [Category("QBehavior")]
    [Editor(typeof (QRibbonItemCollectionEditor), typeof (UITypeEditor))]
    public override QPartCollection Items => this.m_oItemAreaPart.Parts;

    [Browsable(false)]
    public override QTristateBool HasHotState => QTristateBool.True;

    [Browsable(false)]
    public bool IsCollapsed => this.m_bCollapsed;

    [Browsable(false)]
    public QRibbonPage RibbonPage => this.Composite == null ? (QRibbonPage) null : this.Composite.ParentControl as QRibbonPage;

    [Browsable(false)]
    public QRibbonPageComposite RibbonPageComposite => this.Composite as QRibbonPageComposite;

    [Browsable(false)]
    public QRibbon Ribbon => this.RibbonPage?.Ribbon;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QPart CaptionPart => this.m_oCaptionPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeText CaptionTitlePart => this.m_oCaptionTitlePart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QCompositeItem CaptionShowDialogPart => this.m_oCaptionShowDialogPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QPart ItemAreaPart => this.m_oItemAreaPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected override IList AssociatedComponents => (IList) new ArrayList((ICollection) this.Items);

    public static QRibbonPanel FindRibbonPanel(IQPart fromPart)
    {
      while (true)
      {
        switch (fromPart)
        {
          case null:
          case QRibbonPanel _:
            goto label_3;
          default:
            fromPart = fromPart.ParentPart;
            continue;
        }
      }
label_3:
      return fromPart as QRibbonPanel;
    }

    internal bool SupportsResizeBehavior(QRibbonPanelResizeBehavior behavior) => (this.Configuration.ResizeBehavior & behavior) == behavior;

    internal bool HasResizeBehaviorApplied(QRibbonPanelResizeBehavior behavior) => (this.m_eAppliedResizeBehavior & behavior) == behavior;

    internal QRibbonPanelResizeBehavior AppliedResizeBehavior => this.m_eAppliedResizeBehavior;

    private bool HideHorizonalTexts(IQPart part, bool hide)
    {
      QPartVisibilitySelectionTypes visibilityTypes = ~QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints;
      if (part is QRibbonItem qribbonItem)
      {
        if (!hide || qribbonItem.Configuration.Direction == QPartDirection.Horizontal && qribbonItem.TitlePart != null && qribbonItem.TitlePart.IsVisible(visibilityTypes) && qribbonItem != null)
        {
          QCompositeItemBase titlePart = (QCompositeItemBase) qribbonItem.TitlePart;
          if (titlePart != null && titlePart.HiddenBecauseOfConstraints != hide)
          {
            titlePart.HiddenBecauseOfConstraints = hide;
            return true;
          }
        }
        return false;
      }
      if (!(part.ContentObject is IQPartCollection contentObject))
        return true;
      bool flag = false;
      for (int index = 0; index < contentObject.Count; ++index)
      {
        if (this.HideHorizonalTexts(contentObject[index], hide))
          flag = true;
      }
      return flag;
    }

    private bool CheckWhetherContentFits(
      Size availableSize,
      QPartVisibilitySelectionTypes excludeVisibility,
      bool horizontal,
      QPartLayoutContext layoutContext)
    {
      layoutContext.CalculatePartSizeOptions |= QPartCalculatePartSizeOptions.PeekOnly;
      layoutContext.VisibilitySelection &= ~excludeVisibility;
      Size partSize = this.LayoutEngine.CalculatePartSize((IQPart) this, layoutContext);
      layoutContext.CalculatePartSizeOptions &= ~QPartCalculatePartSizeOptions.PeekOnly;
      layoutContext.VisibilitySelection |= excludeVisibility;
      return horizontal ? availableSize.Width + this.CalculatedProperties.UnstretchedOuterSize.Width >= partSize.Width : availableSize.Height + this.CalculatedProperties.UnstretchedOuterSize.Height >= partSize.Height;
    }

    private bool ApplyResizeBehaviorHideHorizonalTexts()
    {
      if (this.HideHorizonalTexts((IQPart) this, true))
      {
        this.m_eAppliedResizeBehavior |= QRibbonPanelResizeBehavior.HideHorizontalText;
        return true;
      }
      this.m_eAppliedResizeBehavior &= ~QRibbonPanelResizeBehavior.HideHorizontalText;
      return false;
    }

    private bool ApplyResizeBehaviorCollapse()
    {
      this.m_eAppliedResizeBehavior |= QRibbonPanelResizeBehavior.Collapse;
      this.Collapse(false);
      return true;
    }

    internal bool ApplyResizeBehavior(QRibbonPanelResizeBehavior behavior)
    {
      bool flag = false;
      if (this.SupportsResizeBehavior(behavior) && !this.HasResizeBehaviorApplied(behavior))
      {
        if ((behavior & QRibbonPanelResizeBehavior.Collapse) == QRibbonPanelResizeBehavior.Collapse)
          flag = this.ApplyResizeBehaviorCollapse();
        else if ((behavior & QRibbonPanelResizeBehavior.HideHorizontalText) == QRibbonPanelResizeBehavior.HideHorizontalText)
          flag = this.ApplyResizeBehaviorHideHorizonalTexts();
      }
      return flag;
    }

    private bool UnapplyResizeBehaviorHideHorizonalTexts(
      Size availableSize,
      QPartLayoutContext layoutContext)
    {
      if (!this.HasResizeBehaviorApplied(QRibbonPanelResizeBehavior.HideHorizontalText) || this.SupportsResizeBehavior(QRibbonPanelResizeBehavior.HideHorizontalText) && !this.CheckWhetherContentFits(availableSize, QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints, true, layoutContext))
        return false;
      this.m_eAppliedResizeBehavior &= ~QRibbonPanelResizeBehavior.HideHorizontalText;
      return this.HideHorizonalTexts((IQPart) this, false);
    }

    private bool UnapplyResizeBehaviorCollapse(Size availableSize, QPartLayoutContext layoutContext)
    {
      if (!this.HasResizeBehaviorApplied(QRibbonPanelResizeBehavior.Collapse))
        return false;
      this.Uncollapse(true);
      if (this.SupportsResizeBehavior(QRibbonPanelResizeBehavior.Collapse) && !this.CheckWhetherContentFits(availableSize, QPartVisibilitySelectionTypes.IncludeNone, true, layoutContext))
      {
        this.Collapse(true);
        return false;
      }
      this.m_eAppliedResizeBehavior &= ~QRibbonPanelResizeBehavior.Collapse;
      this.Uncollapse(false);
      return true;
    }

    internal bool UnapplyResizeBehavior(
      QRibbonPanelResizeBehavior behavior,
      Size availableSize,
      QPartLayoutContext layoutContext)
    {
      bool flag = false;
      if (this.HasResizeBehaviorApplied(behavior))
      {
        if ((behavior & QRibbonPanelResizeBehavior.Collapse) == QRibbonPanelResizeBehavior.Collapse)
          flag = this.UnapplyResizeBehaviorCollapse(availableSize, layoutContext);
        else if ((behavior & QRibbonPanelResizeBehavior.HideHorizontalText) == QRibbonPanelResizeBehavior.HideHorizontalText)
          flag = this.UnapplyResizeBehaviorHideHorizonalTexts(availableSize, layoutContext);
      }
      return flag;
    }

    public void ReapplyCurrentResizeBehavior()
    {
      if ((this.m_eAppliedResizeBehavior & QRibbonPanelResizeBehavior.HideHorizontalText) != QRibbonPanelResizeBehavior.HideHorizontalText)
        return;
      this.HideHorizonalTexts((IQPart) this, true);
    }

    public void Collapse(bool measurementOnly)
    {
      if (!measurementOnly)
      {
        base.Parts.SuspendChangeNotification();
        this.m_oCollapsedItem.ChildItems.SuspendChangeNotification();
        this.m_oCollapsedItem.ChildItems.Clear();
        for (int index = 0; index < base.Parts.Count; ++index)
          this.m_oCollapsedItem.ChildItems.Add(base.Parts[index], false);
        ((IQPart) this.m_oCollapsedItem).SetDisplayParent((IQManagedLayoutParent) this.Composite);
        this.m_oCollapsedItem.ChildItems.SetDisplayParent((IQManagedLayoutParent) this.m_oCollapsedItem.ChildComposite);
        base.Parts.ResumeChangeNotification(false);
        this.m_oCollapsedItem.ChildItems.ResumeChangeNotification(false);
      }
      this.m_bCollapsed = true;
    }

    public void Uncollapse(bool measurementOnly)
    {
      this.m_bCollapsed = false;
      if (measurementOnly)
        return;
      base.Parts.SuspendChangeNotification();
      this.m_oCollapsedItem.ChildItems.SuspendChangeNotification();
      this.m_oCollapsedItem.ChildItems.Clear();
      this.m_oCollapsedItem.ChildItems.ResumeChangeNotification(false);
      base.Parts.ResetPartParents(false);
      base.Parts.ResumeChangeNotification(false);
    }

    public override object ContentObject => this.m_bCollapsed ? (object) this.m_oCollapsedItems : base.ContentObject;

    public override QPartCollection Parts => this.m_bCollapsed ? this.m_oCollapsedItems : base.Parts;

    public override IQPartObjectPainter GetObjectPainter(
      QPartPaintLayer paintLayer,
      Type painterType)
    {
      return this.m_bCollapsed ? (IQPartObjectPainter) null : base.GetObjectPainter(paintLayer, painterType);
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      QColorSet itemColorSet = new QColorSet();
      if (destinationObject == this.m_oCaptionPart)
      {
        if (QItemStatesHelper.IsDisabled(state))
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelCaptionArea1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelCaptionArea2");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelCaptionShowDialogDisabled");
        }
        else if (QItemStatesHelper.IsPressed(state) || QItemStatesHelper.IsHot(state))
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelHotCaptionArea1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelHotCaptionArea2");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelCaptionShowDialogHot");
        }
        else
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelCaptionArea1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelCaptionArea2");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelCaptionShowDialog");
        }
      }
      else if (destinationObject == this.m_oCaptionShowDialogPart)
      {
        if (QItemStatesHelper.IsDisabled(state))
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonItemBackground1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonItemBackground2");
          itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonItemBorder");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelTextDisabled");
        }
        if (QItemStatesHelper.IsPressed(state))
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonItemActiveBackground1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonItemActiveBackground2");
          itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonItemActiveBorder");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelTextActive");
        }
        else if (QItemStatesHelper.IsHot(state))
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonItemHotBackground1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonItemHotBackground2");
          itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonItemHotBorder");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelTextHot");
        }
        else
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonItemBackground1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonItemBackground2");
          itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonItemBorder");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelText");
        }
      }
      else if (QItemStatesHelper.IsDisabled(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonPanelBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelTextDisabled");
      }
      else if (QItemStatesHelper.IsPressed(state) || QItemStatesHelper.IsExpanded(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelActiveBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelActiveBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonPanelActiveBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelTextActive");
      }
      else if (QItemStatesHelper.IsHot(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelHotBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelHotBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonPanelHotBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelTextHot");
      }
      else
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("RibbonPanelBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("RibbonPanelBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("RibbonPanelBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("RibbonPanelText");
      }
      return itemColorSet;
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
      if (part != this || layoutStage != QPartLayoutStage.CalculatingSize)
        return;
      this.m_oCaptionShowDialogPart.PutContentObject((object) this.Configuration.CaptionConfiguration.ShowDialogConfiguration.UsedShowDialogMask);
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet = base.HandlePaintStage(part, paintStage, paintContext);
      if (part == this && paintStage == QPartPaintStage.PaintingBackground && !this.m_bCollapsed)
      {
        QColorSet itemColorSet = this.ColorHost.GetItemColorSet((object) this.m_oCaptionPart, this.ItemState, (object) null);
        QPartRectanglePainter objectPainter1 = this.m_oCaptionPart.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartRectanglePainter)) as QPartRectanglePainter;
        objectPainter1.Appearance = (IQAppearance) this.Configuration.CaptionConfiguration.Appearance;
        objectPainter1.ColorSet = itemColorSet;
        QPartImagePainter objectPainter2 = this.m_oCaptionShowDialogPart.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartImagePainter)) as QPartImagePainter;
        objectPainter2.ColorToReplace = Color.FromArgb((int) byte.MaxValue, 0, 0);
        objectPainter2.ColorToReplaceWith = itemColorSet.Foreground;
      }
      return qcolorSet;
    }

    protected override void OnItemActivated(QCompositeEventArgs e)
    {
      base.OnItemActivated(e);
      if (e.Item != this.CaptionShowDialogPart && e.OriginalItem != this.CaptionShowDialogPart)
        return;
      this.OnCaptionShowDialogItemActivated(e);
    }

    protected virtual void OnCaptionShowDialogItemActivated(QCompositeEventArgs e) => this.m_oCaptionShowDialogItemActivated = QWeakDelegate.InvokeDelegate(this.m_oCaptionShowDialogItemActivated, (object) this, (object) e);
  }
}
