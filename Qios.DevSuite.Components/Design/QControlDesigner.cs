// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QControlDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QControlDesigner : ControlDesigner
  {
    private QControl m_oControl;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      if (component == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (component)));
      base.Initialize(component);
      this.m_oControl = (QControl) component;
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oControl.ColorScheme);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
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
