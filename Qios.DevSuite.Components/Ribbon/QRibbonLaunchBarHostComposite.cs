// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarHostComposite
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarHostComposite : QComposite
  {
    private QPart m_oItemAreaPart;
    private QPart m_oLaunchBarAreaPart;

    protected internal QRibbonLaunchBarHostComposite(QRibbonLaunchBarHost lauchBarHost)
      : base((IQPart) null, (QPartCollection) null, (IQCompositeContainer) lauchBarHost, (QCompositeConfiguration) null, lauchBarHost.ColorScheme)
    {
      this.m_oLaunchBarAreaPart = new QPart("LaunchBarArea", false, new IQPart[0]);
      this.m_oLaunchBarAreaPart.Properties = (IQPartSharedProperties) this.Configuration.LaunchBarAreaConfiguration;
      this.m_oItemAreaPart = new QPart("ItemArea", false, new IQPart[0]);
      this.m_oItemAreaPart.Properties = (IQPartSharedProperties) this.Configuration.ItemAreaConfiguration;
      this.Parts.SuspendChangeNotification();
      this.Parts.Add((IQPart) this.m_oLaunchBarAreaPart, false);
      this.Parts.Add((IQPart) this.m_oItemAreaPart, false);
      this.Parts.ResumeChangeNotification(false);
    }

    protected override QCompositeConfiguration CreateConfiguration() => (QCompositeConfiguration) new QRibbonLaunchBarHostConfiguration();

    protected override QCompositeConfiguration CreateChildCompositeConfiguration() => (QCompositeConfiguration) new QCompositeMenuConfiguration();

    public QRibbonLaunchBarHostConfiguration Configuration => base.Configuration as QRibbonLaunchBarHostConfiguration;

    public QRibbonLaunchBarHost LaunchBarHost => this.ParentContainer as QRibbonLaunchBarHost;

    public QPart LaunchBarAreaPart => this.m_oLaunchBarAreaPart;

    public QPart ItemAreaPart => this.m_oItemAreaPart;

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      if (destinationObject != this)
        return base.GetItemColorSet(destinationObject, state, additionalProperties);
      return new QColorSet()
      {
        Background1 = this.RetrieveFirstDefinedColor("RibbonLaunchBarHostBackground1"),
        Background2 = this.RetrieveFirstDefinedColor("RibbonLaunchBarHostBackground2"),
        Border = this.RetrieveFirstDefinedColor("RibbonLaunchBarHostBorder")
      };
    }
  }
}
