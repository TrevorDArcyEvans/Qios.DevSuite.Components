// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemGroup
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonItemGroup : QRibbonItemGroupBase
  {
    public QRibbonItemGroup()
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [Category("QAppearance")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QRibbonItemGroupConfiguration();

    protected override void SetConfigurationBaseProperties(bool raiseEvent)
    {
      if (this.RibbonPageComposite != null)
        this.Configuration.Properties.SetBaseProperties(this.RibbonPageComposite.Configuration.DefaultGroupConfiguration.Properties, true, raiseEvent);
      else
        this.Configuration.Properties.SetBaseProperties((QFastPropertyBag) null, true, raiseEvent);
      if (this.RibbonPanel != null)
      {
        if (this.ColorScheme == null)
          return;
        this.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.RibbonPanel.ColorScheme, false);
      }
      else
      {
        if (this.LaunchBar == null || this.ColorScheme == null)
          return;
        this.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.LaunchBar.ColorScheme, false);
      }
    }
  }
}
