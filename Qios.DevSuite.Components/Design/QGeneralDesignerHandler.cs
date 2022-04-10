// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QGeneralDesignerHandler
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  public class QGeneralDesignerHandler : IDisposable
  {
    private IComponent m_oComponent;
    private ComponentDesigner m_oDesigner;

    public QGeneralDesignerHandler(ComponentDesigner designer, IComponent component)
    {
      this.m_oComponent = component;
      this.m_oDesigner = designer;
      if (this.DesignerHost == null)
        return;
      this.DesignerHost.LoadComplete += new EventHandler(this.DesignerHost_LoadComplete);
    }

    public Control Control => this.m_oComponent as Control;

    public IDesignerHost DesignerHost => this.m_oComponent.Site == null ? (IDesignerHost) null : this.m_oComponent.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;

    private void DesignerHost_LoadComplete(object sender, EventArgs e)
    {
      if (this.Control == null)
        return;
      this.Control.PerformLayout();
    }

    public void Dispose()
    {
      GC.SuppressFinalize((object) this);
      this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.DesignerHost == null)
        return;
      this.DesignerHost.LoadComplete -= new EventHandler(this.DesignerHost_LoadComplete);
    }

    ~QGeneralDesignerHandler() => this.Dispose(false);
  }
}
