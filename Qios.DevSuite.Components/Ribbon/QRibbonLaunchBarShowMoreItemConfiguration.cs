// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarShowMoreItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarShowMoreItemConfiguration : QRibbonLaunchBarItemConfiguration
  {
    protected const int PropChildCompositeConfiguration = 24;
    protected const int PropChildWindowConfiguration = 25;
    protected new const int CurrentPropertyCount = 2;
    protected new const int TotalPropertyCount = 26;
    private EventHandler m_oChildObjectsChangedEventHandler;

    private QRibbonLaunchBarShowMoreItemConfiguration()
    {
    }

    public QRibbonLaunchBarShowMoreItemConfiguration(Image defaultMask)
      : base(defaultMask)
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Far);
      this.Properties.DefineResettableProperty(25, (IQResettableValue) new QCompositeWindowConfiguration());
      this.Properties.DefineResettableProperty(24, (IQResettableValue) new QCompositeConfiguration());
      this.ChildCompositeConfiguration.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Down);
      this.ChildCompositeConfiguration.Properties.DefineProperty(17, (object) QCompositeExpandBehavior.CloseExpandedItemOnClick);
    }

    protected override int GetRequestedCount() => 26;

    [Description("Gets the configuration of the child window.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(25)]
    public QCompositeWindowConfiguration ChildWindowConfiguration => this.Properties.GetProperty(25) as QCompositeWindowConfiguration;

    [Description("Gets the configuration of the child composite.")]
    [QPropertyIndex(24)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeConfiguration ChildCompositeConfiguration => this.Properties.GetProperty(24) as QCompositeConfiguration;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
