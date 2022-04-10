// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionTextConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCaptionTextConfiguration : QGroupPartConfiguration
  {
    private const string m_sDefaultContentPartLayoutOrder = "DocumentText, SeparatorText, ApplicationText";
    protected const int PropDocumentTextConfiguration = 15;
    protected const int PropSeparatorTextConfiguration = 16;
    protected const int PropApplicationTextConfiguration = 17;
    protected const int PropSeparatorText = 18;
    protected new const int CurrentPropertyCount = 4;
    protected new const int TotalPropertyCount = 19;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonCaptionTextConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(2, (object) true);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) new QCompositeTextConfiguration());
      this.DocumentTextConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.DocumentTextConfiguration.Properties.DefineProperty(15, (object) QDrawTextOptions.EndEllipsis);
      this.DocumentTextConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.DocumentTextConfiguration.Properties.DefineProperty(2, (object) true);
      this.DocumentTextConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(16, (IQResettableValue) new QCompositeTextConfiguration());
      this.SeparatorTextConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.SeparatorTextConfiguration.Properties.DefineProperty(15, (object) QDrawTextOptions.EndEllipsis);
      this.SeparatorTextConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.SeparatorTextConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(17, (IQResettableValue) new QCompositeTextConfiguration());
      this.ApplicationTextConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.ApplicationTextConfiguration.Properties.DefineProperty(15, (object) QDrawTextOptions.EndEllipsis);
      this.ApplicationTextConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.ApplicationTextConfiguration.Properties.DefineProperty(2, (object) true);
      this.ApplicationTextConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineProperty(18, (object) "-");
    }

    protected override string DefaultContentPartLayoutOrder => "DocumentText, SeparatorText, ApplicationText";

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(15)]
    [Category("QAppearance")]
    [Description("Gets the configuration of the document text")]
    public QCompositeTextConfiguration DocumentTextConfiguration => this.Properties.GetProperty(15) as QCompositeTextConfiguration;

    [QPropertyIndex(16)]
    [Description("Gets the configuration of the separator between the application and document text")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeTextConfiguration SeparatorTextConfiguration => this.Properties.GetProperty(16) as QCompositeTextConfiguration;

    [QPropertyIndex(17)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the application text")]
    [Category("QAppearance")]
    public QCompositeTextConfiguration ApplicationTextConfiguration => this.Properties.GetProperty(17) as QCompositeTextConfiguration;

    [Localizable(true)]
    [Description("Gets or sets a possible separator text.")]
    [Category("QAppearance")]
    [QPropertyIndex(18)]
    public string SeparatorText
    {
      get => this.Properties.GetProperty(18) as string;
      set => this.Properties.SetProperty(18, (object) value);
    }

    protected override int GetRequestedCount() => 19;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
