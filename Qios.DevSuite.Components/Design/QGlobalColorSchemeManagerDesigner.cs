// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QGlobalColorSchemeManagerDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QGlobalColorSchemeManagerDesigner : ComponentDesigner
  {
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      QGlobalColorSchemeManager colorSchemeManager = component as QGlobalColorSchemeManager;
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      if (colorSchemeManager == null)
        return;
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), (QColorScheme) colorSchemeManager.Global);
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
