// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCollapsedItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCollapsedItemConfiguration : QCompositeItemConfiguration
  {
    protected const int PropIconConfiguration = 23;
    protected const int PropTitleConfiguration = 24;
    protected const int PropDropDownConfiguration = 25;
    protected const int PropChildWindowConfiguration = 26;
    protected new const int CurrentPropertyCount = 4;
    protected new const int TotalPropertyCount = 27;
    private string m_sDefaultContentLayoutOrder = "Icon, Title, DropDownButton";
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonCollapsedItemConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(17, (object) QTristateBool.True);
      this.Properties.DefineProperty(18, (object) QTristateBool.True);
      this.Properties.DefineProperty(1, (object) new QPadding(3, 5, 5, 3));
      this.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineResettableProperty(23, (IQResettableValue) new QRibbonCollapsedItemIconConfiguration());
      this.IconConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.IconConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.IconConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(24, (IQResettableValue) new QCompositeTextConfiguration());
      this.TitleConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.TitleConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.TitleConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(25, (IQResettableValue) new QCompositeMaskConfiguration(QRibbonItemConfiguration.DefaultDropDownMask));
      this.DropDownButtonConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.DropDownButtonConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.DropDownButtonConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(26, (IQResettableValue) new QRibbonCollapsedItemChildWindowConfiguration());
      this.ChildWindowConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected override int GetRequestedCount() => 27;

    protected override string DefaultContentPartLayoutOrder => this.m_sDefaultContentLayoutOrder;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonCollapsedItemAppearance();

    [QPropertyIndex(23)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the icon.")]
    public QRibbonCollapsedItemIconConfiguration IconConfiguration => this.Properties.GetProperty(23) as QRibbonCollapsedItemIconConfiguration;

    [QPropertyIndex(24)]
    [Description("Gets the configuration of the title.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeTextConfiguration TitleConfiguration => this.Properties.GetProperty(24) as QCompositeTextConfiguration;

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(25)]
    [Description("Gets the configuration of the drop down button.")]
    public QCompositeMaskConfiguration DropDownButtonConfiguration => this.Properties.GetProperty(25) as QCompositeMaskConfiguration;

    [QPropertyIndex(26)]
    [Description("Gets the configuration of the child floating window that shows the items when collapsed.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeWindowConfiguration ChildWindowConfiguration => this.Properties.GetProperty(26) as QCompositeWindowConfiguration;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
