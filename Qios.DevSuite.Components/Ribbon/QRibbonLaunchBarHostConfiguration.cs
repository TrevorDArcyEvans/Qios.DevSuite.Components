// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarHostConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarHostConfiguration : QCompositeConfiguration
  {
    private const string m_sDefaultContentPartLayoutOrder = "LaunchBarArea, ItemArea";
    protected const int PropLaunchBarAreaConfiguration = 29;
    protected const int PropItemAreaConfiguration = 30;
    protected new const int CurrentPropertyCount = 2;
    protected new const int TotalPropertyCount = 31;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonLaunchBarHostConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Down);
      this.Properties.DefineProperty(1, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(3, (object) false);
      this.Properties.DefineResettableProperty(29, (IQResettableValue) new QRibbonLaunchBarHostGroupConfiguration());
      this.LaunchBarAreaConfiguration.Properties.DefineProperty(2, (object) true);
      this.LaunchBarAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(30, (IQResettableValue) new QRibbonLaunchBarHostGroupConfiguration());
      this.ItemAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected override string DefaultContentPartLayoutOrder => "LaunchBarArea, ItemArea";

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [QPropertyIndex(12)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration of the LaunchBar area of this QRibbonLaunchBar.")]
    [QPropertyIndex(29)]
    public QRibbonLaunchBarHostGroupConfiguration LaunchBarAreaConfiguration => this.Properties.GetProperty(29) as QRibbonLaunchBarHostGroupConfiguration;

    [QPropertyIndex(30)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration of the custom iteam area of this QRibbonLaunchBarHost.")]
    public QRibbonLaunchBarHostGroupConfiguration ItemAreaConfiguration => this.Properties.GetProperty(30) as QRibbonLaunchBarHostGroupConfiguration;

    protected override int GetRequestedCount() => 31;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonLaunchBarHostAppearance();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override QMargin IconBackgroundMargin
    {
      get => base.IconBackgroundMargin;
      set => base.IconBackgroundMargin = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override int IconBackgroundSize
    {
      get => base.IconBackgroundSize;
      set => base.IconBackgroundSize = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool IconBackgroundVisible
    {
      get => base.IconBackgroundVisible;
      set => base.IconBackgroundVisible = value;
    }

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
