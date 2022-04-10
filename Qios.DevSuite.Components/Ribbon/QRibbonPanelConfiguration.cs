// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPanelConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonPanelConfiguration : QCompositeGroupOrderedConfiguration
  {
    protected const int PropCaptionConfiguration = 18;
    protected const int PropItemAreaConfiguration = 19;
    protected const int PropResizeBehavior = 20;
    protected const int PropCollapsedConfiguration = 21;
    protected new const int CurrentPropertyCount = 4;
    protected new const int TotalPropertyCount = 22;
    private const string m_sDefaultContentLayoutOrder = "ItemArea, Caption";
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonPanelConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(3, (object) true);
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(1, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
      this.Properties.DefineResettableProperty(18, (IQResettableValue) new QRibbonPanelCaptionConfiguration());
      this.CaptionConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(19, (IQResettableValue) new QRibbonPanelItemAreaConfiguration());
      this.ItemAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(21, (IQResettableValue) new QRibbonCollapsedItemConfiguration());
      this.CollapsedConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineProperty(20, (object) (QRibbonPanelResizeBehavior.HideHorizontalText | QRibbonPanelResizeBehavior.Collapse));
    }

    protected override int GetRequestedCount() => 22;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonPanelAppearance();

    protected override string DefaultContentPartLayoutOrder => "ItemArea, Caption";

    [QPropertyIndex(18)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the caption area.")]
    [Category("QAppearance")]
    public QRibbonPanelCaptionConfiguration CaptionConfiguration => this.Properties.GetProperty(18) as QRibbonPanelCaptionConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration of the item area.")]
    [QPropertyIndex(19)]
    public QRibbonPanelItemAreaConfiguration ItemAreaConfiguration => this.Properties.GetProperty(19) as QRibbonPanelItemAreaConfiguration;

    [QPropertyIndex(21)]
    [Description("Gets the configuration for the item that is displayed when the QRibbonPanel is collapsed.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonCollapsedItemConfiguration CollapsedConfiguration => this.Properties.GetProperty(21) as QRibbonCollapsedItemConfiguration;

    [QPropertyIndex(20)]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [Description("Gets or sets the behavior that must be applied when the panel doesn't fit. If this is turned off default resize behaviors like scrolling can be applied")]
    [Category("QAppearance")]
    public QRibbonPanelResizeBehavior ResizeBehavior
    {
      get => (QRibbonPanelResizeBehavior) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    public override string GetContentLayoutOrder(IQPart part) => !(part is QRibbonPanel qribbonPanel) || !qribbonPanel.IsCollapsed ? base.GetContentLayoutOrder(part) : (string) null;

    public override IQPadding[] GetPaddings(IQPart part)
    {
      if (!(part is QRibbonPanel qribbonPanel) || !qribbonPanel.IsCollapsed)
        return base.GetPaddings(part);
      return new IQPadding[1]{ (IQPadding) QPadding.Empty };
    }

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
