// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QDesignerObjectCreationVerb
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QDesignerObjectCreationVerb : DesignerVerb
  {
    private IDesigner m_oDesigner;
    private Type m_oCreationType;

    public QDesignerObjectCreationVerb(IDesigner designer, Type creationType)
      : base("Add " + creationType.Name, (EventHandler) null)
    {
      this.m_oDesigner = designer;
      this.m_oCreationType = creationType;
    }

    public override void Invoke()
    {
      IDesignerHost service = this.m_oDesigner.Component.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
      IComponent component = service.CreateComponent(this.m_oCreationType);
      QDesignerMainTextAttribute.SetPossibleText((object) component, component.Site.Name);
      service.Container.Add(component);
    }
  }
}
