// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;

namespace Qios.DevSuite.Components.Ribbon
{
  [TypeConverter(typeof (ComponentConverter))]
  [ToolboxItem(false)]
  [DesignTimeVisible(true)]
  [Designer(typeof (QRibbonGroupDesigner), typeof (IDesigner))]
  public class QRibbonLaunchBar : QCompositeGroup
  {
    private bool m_bActive;
    private bool m_bDrawShape = true;
    private QRibbonLaunchBarItem m_oCustomizeItem;
    private QRibbonLaunchBarShowMoreItem m_oShowMoreItem;

    public QRibbonLaunchBar()
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.m_oShowMoreItem = new QRibbonLaunchBarShowMoreItem();
      this.m_oShowMoreItem.HotkeyText = "SM";
      this.m_oShowMoreItem.LayoutOrder = 2147483646;
      this.m_oShowMoreItem.PutIsSystemPart(true);
      this.m_oShowMoreItem.ItemName = "ShowMoreItem";
      this.m_oShowMoreItem.Configuration = this.Configuration.ShowMoreItemConfiguration;
      this.m_oCustomizeItem = new QRibbonLaunchBarItem();
      this.m_oCustomizeItem.HotkeyText = "CM";
      this.m_oCustomizeItem.LayoutOrder = 2147483645;
      this.m_oCustomizeItem.PutIsSystemPart(true);
      this.m_oCustomizeItem.ItemName = "CustomizeItem";
      this.m_oCustomizeItem.Configuration = this.Configuration.CustomizeItemConfiguration;
      this.Items.SuspendChangeNotification();
      this.Items.AllowAddRemoveSystemParts = true;
      this.Items.Add((IQPart) this.m_oCustomizeItem, false);
      this.Items.Add((IQPart) this.m_oShowMoreItem, false);
      this.Items.AllowAddRemoveSystemParts = false;
      this.Items.ResumeChangeNotification(false);
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonLaunchBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [Description("The ColorScheme that is used.")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QRibbonLaunchBarConfiguration();

    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QRibbonLaunchBarConfiguration Configuration
    {
      get => base.Configuration as QRibbonLaunchBarConfiguration;
      set => this.Configuration = value;
    }

    [Browsable(false)]
    public QRibbonLaunchBarItem CustomizeItem => this.m_oCustomizeItem;

    [Browsable(false)]
    public QRibbonLaunchBarShowMoreItem ShowMoreItem => this.m_oShowMoreItem;

    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QRibbonItemCollectionEditor), typeof (UITypeEditor))]
    [Editor(typeof (QRibbonPageItemCollectionEditor), typeof (UITypeEditor))]
    [Description("Contains the items of this QRibbonLaunchBar")]
    public override QPartCollection Items => base.Items;

    [Category("QBehavior")]
    [Description("Contains the items in the CustomizeMenu of this QRibbonLaunchBar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    public QPartCollection CustomizeItems => this.m_oCustomizeItem.ChildItems;

    [DefaultValue("CM")]
    [Description("Gets or sets the hotkey text for the customize item.")]
    [Category("QAppearance")]
    [Localizable(true)]
    public string HotkeyTextCustomizeItem
    {
      get => this.m_oCustomizeItem.HotkeyText;
      set => this.m_oCustomizeItem.HotkeyText = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets the hotkey text for the show more item.")]
    [DefaultValue("SM")]
    [Localizable(true)]
    public string HotkeyTextShowMoreItem
    {
      get => this.m_oShowMoreItem.HotkeyText;
      set => this.m_oShowMoreItem.HotkeyText = value;
    }

    public static QRibbonLaunchBar FindLaunchBar(IQPart fromPart)
    {
      while (true)
      {
        switch (fromPart)
        {
          case null:
          case QRibbonLaunchBar _:
            goto label_3;
          default:
            fromPart = fromPart.ParentPart;
            continue;
        }
      }
label_3:
      return fromPart as QRibbonLaunchBar;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Active
    {
      get => this.m_bActive;
      set
      {
        if (this.m_bActive == value)
          return;
        this.m_bActive = value;
        this.HandleChange(false);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool DrawShape
    {
      get => this.m_bDrawShape;
      set
      {
        bool shouldDrawShape = this.ShouldDrawShape;
        this.m_bDrawShape = value;
        if (shouldDrawShape == this.ShouldDrawShape)
          return;
        this.HandleChange(true);
      }
    }

    [Browsable(false)]
    public bool ShouldDrawShape => this.Configuration.DrawShape == QTristateBool.Undefined ? this.m_bDrawShape : this.Configuration.DrawShape == QTristateBool.True;

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      if (destinationObject == this)
      {
        QColorSet qcolorSet = new QColorSet();
        return this.m_bActive ? new QColorSet(this.RetrieveFirstDefinedColor("RibbonLaunchBarBackground1"), this.RetrieveFirstDefinedColor("RibbonLaunchBarBackground2"), this.RetrieveFirstDefinedColor("RibbonLaunchBarBorder")) : new QColorSet(this.RetrieveFirstDefinedColor("RibbonLaunchBarInactiveBackground1"), this.RetrieveFirstDefinedColor("RibbonLaunchBarInactiveBackground2"), this.RetrieveFirstDefinedColor("RibbonLaunchBarInactiveBorder"));
      }
      return destinationObject == this.m_oCustomizeItem || destinationObject == this.m_oShowMoreItem ? QRibbonItem.GetDefaultRibbonItemColorSet(destinationObject, state, this.Composite, (IQColorRetriever) this) : base.GetItemColorSet(destinationObject, state, additionalProperties);
    }

    private void PrepareForCalculatingSize()
    {
      if (!this.m_oShowMoreItem.HiddenBecauseOfConstraints)
      {
        this.m_oShowMoreItem.HiddenBecauseOfConstraints = true;
        this.m_oShowMoreItem.SetChangedFlag(QCompositeItemChangedFlags.VisibilityChanged, true);
      }
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index] is QCompositeItemBase qcompositeItemBase && qcompositeItemBase != this.m_oShowMoreItem && qcompositeItemBase.HiddenBecauseOfConstraints)
        {
          qcompositeItemBase.HiddenBecauseOfConstraints = false;
          qcompositeItemBase.SetChangedFlag(QCompositeItemChangedFlags.VisibilityChanged, true);
        }
      }
    }

    private void HideItemsWhenNeeded(
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      Size innerSize = this.CalculatedProperties.InnerSize;
      Size outerSize = this.CalculatedProperties.OuterSize;
      if (this.Configuration.ResizeBehavior != QRibbonLaunchBarResizeBehavior.ShowMoreItem || this.Configuration.Direction != QPartDirection.Horizontal)
        return;
      Size actualContentSize = additionalProperties.ActualContentSize;
      Size unstretchedInnerSize = this.CalculatedProperties.UnstretchedInnerSize;
      if (actualContentSize.Width <= innerSize.Width)
        return;
      if (!this.m_oShowMoreItem.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
      {
        this.m_oShowMoreItem.HiddenBecauseOfConstraints = false;
        this.m_oShowMoreItem.FlipChangedFlag(QCompositeItemChangedFlags.VisibilityChanged);
        if (this.m_oShowMoreItem.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        {
          this.m_oShowMoreItem.LayoutEngine.CalculatePartSize((IQPart) this.m_oShowMoreItem, layoutContext);
          actualContentSize.Width += this.m_oShowMoreItem.CalculatedProperties.OuterSize.Width;
          unstretchedInnerSize.Width += this.m_oShowMoreItem.CalculatedProperties.UnstretchedOuterSize.Width;
        }
      }
      for (int index = this.Items.Count - 1; index >= 0 && actualContentSize.Width > innerSize.Width; --index)
      {
        if (this.Items[index] is QCompositeItemBase qcompositeItemBase && qcompositeItemBase != this.m_oShowMoreItem && qcompositeItemBase.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        {
          qcompositeItemBase.HiddenBecauseOfConstraints = true;
          qcompositeItemBase.FlipChangedFlag(QCompositeItemChangedFlags.VisibilityChanged);
          actualContentSize.Width -= qcompositeItemBase.CalculatedProperties.OuterSize.Width;
          unstretchedInnerSize.Width -= qcompositeItemBase.CalculatedProperties.UnstretchedOuterSize.Width;
        }
      }
      this.CalculatedProperties.SetUnstretchedSizesBasedOnInnerSize(unstretchedInnerSize);
      this.CalculatedProperties.ActualContentSize = actualContentSize;
    }

    private void NotifyItemsOfConstraintVisibilityChanged(IList items)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        QCompositeItemBase part = items[index] as QCompositeItemBase;
        if (part.HasChangedFlag(QCompositeItemChangedFlags.VisibilityChanged))
        {
          QCompositeHelper.NotifyChildPartVisibilityChanged((IQPart) part);
          part.SetChangedFlag(QCompositeItemChangedFlags.VisibilityChanged, false);
        }
      }
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
      if (part != this)
        return;
      switch (layoutStage)
      {
        case QPartLayoutStage.CalculatingSize:
          this.PrepareForCalculatingSize();
          break;
        case QPartLayoutStage.ConstraintsApplied:
          this.HideItemsWhenNeeded(layoutContext, additionalProperties);
          break;
        case QPartLayoutStage.BoundsCalculated:
          this.NotifyItemsOfConstraintVisibilityChanged((IList) this.Parts);
          break;
      }
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      if (part == this && paintStage == QPartPaintStage.PaintingBackground)
      {
        this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)).Enabled = this.ShouldDrawShape;
        this.GetObjectPainter(QPartPaintLayer.Foreground, typeof (QPartShapePainter)).Enabled = this.ShouldDrawShape;
      }
      return base.HandlePaintStage(part, paintStage, paintContext);
    }
  }
}
