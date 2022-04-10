// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonItemBar : QRibbonItemGroupBase
  {
    public QRibbonItemBar()
      : base(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QRibbonItemBarConfiguration();

    [Category("QAppearance")]
    [Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonItemBarConfiguration Configuration
    {
      get => base.Configuration as QRibbonItemBarConfiguration;
      set => this.Configuration = value;
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonSeparator)]
    [Category("QAppearance")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    private void InternalConstruct()
    {
    }

    protected override void SetConfigurationBaseProperties(bool raiseEvent)
    {
      if (this.RibbonPageComposite != null)
        this.Configuration.Properties.SetBaseProperties(this.RibbonPageComposite.Configuration.DefaultBarConfiguration.Properties, true, raiseEvent);
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

    public static QRibbonItemBar FindItemBar(IQPart fromPart)
    {
      while (true)
      {
        switch (fromPart)
        {
          case null:
          case QRibbonItemBar _:
            goto label_3;
          default:
            fromPart = fromPart.ParentPart;
            continue;
        }
      }
label_3:
      return fromPart as QRibbonItemBar;
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return new QColorSet()
      {
        Background1 = this.RetrieveFirstDefinedColor("RibbonItemBarBackground1"),
        Background2 = this.RetrieveFirstDefinedColor("RibbonItemBarBackground2"),
        Border = this.RetrieveFirstDefinedColor("RibbonItemBarBorder"),
        Foreground = Color.Empty
      };
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      if (part == this)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingContent:
            paintContext.Push();
            paintContext.Reverse = true;
            break;
          case QPartPaintStage.ContentPainted:
            paintContext.Pull();
            break;
        }
      }
      return base.HandlePaintStage(part, paintStage, paintContext);
    }
  }
}
