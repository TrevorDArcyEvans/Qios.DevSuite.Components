// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QContextMenuDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QContextMenuDesigner : ComponentDesigner
  {
    private QContextMenu m_oMenu;
    private QMenuItemXmlHandler m_oMenuItemXmlHandler;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      if (component == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (component)));
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      this.m_oMenu = (QContextMenu) component;
      this.m_oMenuItemXmlHandler = new QMenuItemXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oMenu.MenuItems);
      ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
      QContextMenuDesigner.GetExtenderProviderOnSite(component.Site, true);
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oMenu.ColorScheme);
    }

    internal static QContextMenuExtender GetExtenderProviderOnSite(
      ISite site,
      bool createWhenNotAvailable)
    {
      if (site == null)
        return (QContextMenuExtender) null;
      IExtenderListService service1 = (IExtenderListService) site.GetService(typeof (IExtenderListService));
      if (service1 != null)
      {
        IExtenderProvider[] extenderProviders = service1.GetExtenderProviders();
        for (int index = 0; index < extenderProviders.Length; ++index)
        {
          if (extenderProviders[index] is QContextMenuExtender)
            return (QContextMenuExtender) extenderProviders[index];
        }
      }
      if (createWhenNotAvailable)
      {
        IExtenderProviderService service2 = (IExtenderProviderService) site.GetService(typeof (IExtenderProviderService));
        if (service2 != null)
        {
          QContextMenuExtender provider = new QContextMenuExtender();
          service2.AddExtenderProvider((IExtenderProvider) provider);
          return provider;
        }
      }
      return (QContextMenuExtender) null;
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        DesignerVerbCollection verbs = new DesignerVerbCollection();
        verbs.AddRange(this.m_oMenuItemXmlHandler.Verbs);
        verbs.AddRange(this.m_oColorSchemeXmlHandler.Verbs);
        return verbs;
      }
    }

    public override ICollection AssociatedComponents => this.Component is QContextMenu ? (ICollection) ((QContextMenu) this.Component).MenuItems : base.AssociatedComponents;

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
    }
  }
}
