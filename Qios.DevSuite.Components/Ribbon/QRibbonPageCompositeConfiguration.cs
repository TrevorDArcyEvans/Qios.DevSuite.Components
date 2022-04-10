// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPageCompositeConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonPageCompositeConfiguration : QCompositeConfiguration
  {
    protected const int PropDefaultPanelConfiguration = 29;
    protected const int PropDefaultGroupConfiguration = 30;
    protected const int PropDefaultBarConfiguration = 31;
    protected const int PropDefaultItemConfiguration = 32;
    protected new const int CurrentPropertyCount = 4;
    protected new const int TotalPropertyCount = 33;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonPageCompositeConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(3, (object) true);
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Down);
      this.Properties.DefineProperty(17, (object) (QCompositeExpandBehavior.PaintExpandedChildWhenHot | QCompositeExpandBehavior.CloseExpandedItemOnClick));
      this.Properties.DefineProperty(19, (object) QCompositePressedBehaviour.RememberPressedItem);
      this.Properties.DefineResettableProperty(29, (IQResettableValue) new QRibbonPanelConfiguration());
      this.DefaultPanelConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(30, (IQResettableValue) new QRibbonItemGroupConfiguration());
      this.DefaultGroupConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(31, (IQResettableValue) new QRibbonItemBarConfiguration());
      this.DefaultBarConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(32, (IQResettableValue) new QRibbonItemConfiguration(QRibbonItemConfigurationType.Default));
      this.DefaultItemConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected override int GetRequestedCount() => 33;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) null;

    [Description("Gets the configuration for QRibbonItems.")]
    [QPropertyIndex(29)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonPanelConfiguration DefaultPanelConfiguration => this.Properties.GetProperty(29) as QRibbonPanelConfiguration;

    [Category("QAppearance")]
    [QPropertyIndex(30)]
    [Description("Gets the configuration of visible QRibbonItemGroups.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonItemGroupConfiguration DefaultGroupConfiguration => this.Properties.GetProperty(30) as QRibbonItemGroupConfiguration;

    [Description("Gets the configuration of visible QRibbonItemBars.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(31)]
    [Category("QAppearance")]
    public QRibbonItemBarConfiguration DefaultBarConfiguration => this.Properties.GetProperty(31) as QRibbonItemBarConfiguration;

    [QPropertyIndex(32)]
    [Description("Gets the configuration for QRibbonItems.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonItemConfiguration DefaultItemConfiguration => this.Properties.GetProperty(32) as QRibbonItemConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QTristateBool Visible
    {
      get => QTristateBool.Undefined;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QHotkeyWindowShowBehavior HotkeyWindowShowBehavior
    {
      get => QHotkeyWindowShowBehavior.Automatic;
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QHotkeyWindowConfiguration HotkeyWindowConfiguration => base.HotkeyWindowConfiguration;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private QRibbonPanel GetRibbonPaneForCollapsedItem(IQPart part) => !(part is QRibbonCollapsedItem.QRibbonCollapsedItemComposite collapsedItemComposite) ? (QRibbonPanel) null : collapsedItemComposite.CollapsedItem.RibbonPanel;

    private IQPartSharedProperties GetSharedPropertiesForCollapsedItem(
      IQPart part)
    {
      return !(part is QRibbonCollapsedItem.QRibbonCollapsedItemComposite collapsedItemComposite) ? (IQPartSharedProperties) null : collapsedItemComposite.CollapsedItem.RibbonPanel.Properties;
    }

    public override QTristateBool GetVisible(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetVisible(part) : forCollapsedItem.GetVisible(part);
    }

    public override IQPadding[] GetPaddings(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetPaddings(part) : forCollapsedItem.GetPaddings(part);
    }

    public override IQMargin[] GetMargins(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetMargins(part) : forCollapsedItem.GetMargins(part);
    }

    public override Size GetMinimumSize(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetMinimumSize(part) : forCollapsedItem.GetMinimumSize(part);
    }

    public override Size GetMaximumSize(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetMaximumSize(part) : forCollapsedItem.GetMaximumSize(part);
    }

    public override QPartDirection GetDirection(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetDirection(part) : forCollapsedItem.GetDirection(part);
    }

    public override QPartAlignment GetAlignmentHorizontal(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetAlignmentHorizontal(part) : forCollapsedItem.GetAlignmentHorizontal(part);
    }

    public override QPartAlignment GetAlignmentVertical(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetAlignmentVertical(part) : forCollapsedItem.GetAlignmentVertical(part);
    }

    public override QPartAlignment GetContentAlignmentHorizontal(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetContentAlignmentHorizontal(part) : forCollapsedItem.GetContentAlignmentHorizontal(part);
    }

    public override QPartAlignment GetContentAlignmentVertical(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetContentAlignmentVertical(part) : forCollapsedItem.GetContentAlignmentVertical(part);
    }

    public override string GetContentLayoutOrder(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetContentLayoutOrder(part) : forCollapsedItem.GetContentLayoutOrder(part);
    }

    public override QPartOptions GetOptions(IQPart part)
    {
      IQPartSharedProperties forCollapsedItem = this.GetSharedPropertiesForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetOptions(part) : forCollapsedItem.GetOptions(part);
    }

    public override bool GetIconBackgroundVisible(IQPart part) => this.GetRibbonPaneForCollapsedItem(part) == null && base.GetIconBackgroundVisible(part);

    public override QMargin GetIconBackgroundMargin(IQPart part) => this.GetRibbonPaneForCollapsedItem(part) == null ? base.GetIconBackgroundMargin(part) : QMargin.Empty;

    public override int GetIconBackgroundSize(IQPart part) => this.GetRibbonPaneForCollapsedItem(part) == null ? base.GetIconBackgroundSize(part) : 0;

    public override QShapeAppearance GetAppearance(IQPart part)
    {
      QRibbonPanel forCollapsedItem = this.GetRibbonPaneForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetAppearance(part) : forCollapsedItem.Configuration.Appearance;
    }

    public override QCompositeItemLayout GetLayout(IQPart part)
    {
      QRibbonPanel forCollapsedItem = this.GetRibbonPaneForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetLayout(part) : forCollapsedItem.Configuration.Layout;
    }

    public override QCompositeScrollConfiguration GetScrollConfiguration(
      IQPart part)
    {
      QRibbonPanel forCollapsedItem = this.GetRibbonPaneForCollapsedItem(part);
      return forCollapsedItem == null ? base.GetScrollConfiguration(part) : forCollapsedItem.Configuration.ScrollConfiguration;
    }
  }
}
