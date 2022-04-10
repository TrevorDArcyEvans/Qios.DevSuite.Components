// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapedWindowDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QShapedWindowDesigner : QTranslucentWindowDesigner
  {
    private QShapedWindow m_oWindow;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.m_oWindow = component as QShapedWindow;
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oWindow.ColorScheme);
    }

    public override DesignerVerbCollection Verbs => this.m_oColorSchemeXmlHandler.Verbs;
  }
}
