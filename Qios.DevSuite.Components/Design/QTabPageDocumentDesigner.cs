// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QTabPageDocumentDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QTabPageDocumentDesigner : QContainerControlDocumentDesigner
  {
    private QTabPage m_oTabPage;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;

    public override void Initialize(IComponent component)
    {
      this.m_oTabPage = component as QTabPage;
      base.Initialize(component);
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oTabPage.ColorScheme);
    }

    public override DesignerVerbCollection Verbs => this.m_oColorSchemeXmlHandler.Verbs;
  }
}
