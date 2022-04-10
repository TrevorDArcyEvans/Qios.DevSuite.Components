// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPanelCaptionConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonPanelCaptionConfiguration : QGroupPartConfiguration
  {
    private const string m_sDefaultContentLayoutOrder = "Title, ShowDialog";
    protected const int PropAppearance = 15;
    protected const int PropTitleConfiguration = 16;
    protected const int PropShowDialogConfiguration = 17;
    protected new const int CurrentPropertyCount = 3;
    protected new const int TotalPropertyCount = 18;
    private EventHandler m_oChildObjectChangedHandler;

    public QRibbonPanelCaptionConfiguration()
    {
      this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
      this.Properties.DefineProperty(1, (object) new QPadding(1, 0, 1, 1));
      this.Properties.DefineProperty(8, (object) new Size(0, 15));
      this.Properties.DefineProperty(4, (object) true);
      this.Properties.DefineProperty(2, (object) true);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) new QRibbonPanelCaptionAppearance());
      this.Appearance.AppearanceChanged += this.m_oChildObjectChangedHandler;
      this.Properties.DefineResettableProperty(16, (IQResettableValue) new QRibbonPanelCaptionTitleConfiguration());
      this.TitleConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
      this.Properties.DefineResettableProperty(17, (IQResettableValue) new QRibbonPanelCaptionShowDialogConfiguration());
      this.ShowDialogConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
    }

    protected override int GetRequestedCount() => 18;

    protected override string DefaultContentPartLayoutOrder => "Title, ShowDialog";

    [QPropertyIndex(15)]
    [Description("Gets or sets the appearance of the captionArea of a QRibbonPanel.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonPanelCaptionAppearance Appearance => this.Properties.GetProperty(15) as QRibbonPanelCaptionAppearance;

    [Category("QAppearance")]
    [Description("Gets the configuration of the text of the captionArea of a QRibbonPanel.")]
    [QPropertyIndex(16)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonPanelCaptionTitleConfiguration TitleConfiguration => this.Properties.GetProperty(16) as QRibbonPanelCaptionTitleConfiguration;

    [Description("Gets the configuration of the ShowDialog button of the captionArea of a QRibbonPanel.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(17)]
    public QRibbonPanelCaptionShowDialogConfiguration ShowDialogConfiguration => this.Properties.GetProperty(17) as QRibbonPanelCaptionShowDialogConfiguration;

    private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
