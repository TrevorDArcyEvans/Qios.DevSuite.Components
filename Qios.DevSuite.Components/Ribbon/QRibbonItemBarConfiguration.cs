// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemBarConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonItemBarConfiguration : QCompositeGroupConfiguration
  {
    protected const int PropDefaultItemConfiguration = 18;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 19;
    private EventHandler m_oChildObjectsChangedHandler;

    public QRibbonItemBarConfiguration()
    {
      this.m_oChildObjectsChangedHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(1, (object) new QPadding(1, 0, 0, 0));
      this.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
      this.Properties.DefineResettableProperty(18, (IQResettableValue) this.CreateItemConfiguration());
      this.DefaultItemConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedHandler;
    }

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonItemBarAppearance();

    protected override int GetRequestedCount() => 19;

    protected virtual QRibbonItemConfiguration CreateItemConfiguration() => new QRibbonItemConfiguration(QRibbonItemConfigurationType.RibbonItemBar);

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string ContentLayoutOrder
    {
      get => (string) null;
      set
      {
      }
    }

    [Category("QAppearance")]
    [Description("Gets the configuration for QRibbonItems used on this bar.")]
    [QPropertyIndex(18)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonItemConfiguration DefaultItemConfiguration => this.Properties.GetProperty(18) as QRibbonItemConfiguration;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
