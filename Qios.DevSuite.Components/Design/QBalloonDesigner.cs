// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QBalloonDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QBalloonDesigner : ComponentDesigner
  {
    private QBalloon m_oBalloon;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      if (component == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (component)));
      base.Initialize(component);
      this.m_oBalloon = (QBalloon) component;
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      QBalloonDesigner.GetExtenderProviderOnSite(component.Site, true);
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oBalloon.ColorScheme);
    }

    internal static QBalloonExtender GetExtenderProviderOnSite(
      ISite site,
      bool createWhenNotAvailable)
    {
      if (site == null)
        return (QBalloonExtender) null;
      IExtenderListService service1 = (IExtenderListService) site.GetService(typeof (IExtenderListService));
      if (service1 != null)
      {
        IExtenderProvider[] extenderProviders = service1.GetExtenderProviders();
        for (int index = 0; index < extenderProviders.Length; ++index)
        {
          if (extenderProviders[index] is QBalloonExtender)
            return (QBalloonExtender) extenderProviders[index];
        }
      }
      if (createWhenNotAvailable)
      {
        IExtenderProviderService service2 = (IExtenderProviderService) site.GetService(typeof (IExtenderProviderService));
        if (service2 != null)
        {
          QBalloonExtender provider = new QBalloonExtender();
          service2.AddExtenderProvider((IExtenderProvider) provider);
          return provider;
        }
      }
      return (QBalloonExtender) null;
    }

    public override DesignerVerbCollection Verbs => this.m_oColorSchemeXmlHandler.Verbs;

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oGeneralHandler == null)
        return;
      this.m_oGeneralHandler.Dispose();
    }
  }
}
