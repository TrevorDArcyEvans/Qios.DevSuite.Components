// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMenuItemDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QMenuItemDesigner : ComponentDesigner
  {
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
    }

    public override ICollection AssociatedComponents => this.Component is QMenuItem ? (ICollection) ((QMenuItem) this.Component).MenuItems : base.AssociatedComponents;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.GetService(typeof (IComponentChangeService)) is IComponentChangeService service)
          service.ComponentRemoving -= new ComponentEventHandler(this.ChangeService_ComponentRemoving);
        if (this.m_oGeneralHandler != null)
          this.m_oGeneralHandler.Dispose();
      }
      base.Dispose(disposing);
    }

    private void ChangeService_ComponentRemoving(object sender, ComponentEventArgs e)
    {
      if (e.Component != this.Component)
        return;
      IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      foreach (IComponent associatedComponent in (IEnumerable) this.AssociatedComponents)
        service.DestroyComponent(associatedComponent);
      if (!(e.Component is QMenuItem component) || component.Control == null)
        return;
      service.DestroyComponent((IComponent) component.Control);
      component.Control = (QCommandControlContainer) null;
    }
  }
}
