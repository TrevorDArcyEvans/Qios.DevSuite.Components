// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QRibbonCaptionDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Ribbon;
using System;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QRibbonCaptionDesigner : QCompositeControlDesigner
  {
    private DesignerVerbCollection m_oLocalVerbs;
    private DesignerVerb m_oCreateLaunchBarVerb;
    private DesignerVerb m_oCreateApplicationButtonVerb;

    public override Type[] GetCreationTypes() => QMisc.CombineTypeLists(QCompositeDesignerHelper.DefaultRibbonGroupCreationTypes, base.GetCreationTypes());

    public QRibbonCaption RibbonCaption => this.Component as QRibbonCaption;

    public IDesignerHost DesignerHost => (IDesignerHost) this.GetService(typeof (IDesignerHost));

    public override DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oLocalVerbs == null)
        {
          this.m_oCreateApplicationButtonVerb = new DesignerVerb("Create ApplicationButton", new EventHandler(this.Verb_CreateApplicationButton));
          this.m_oCreateLaunchBarVerb = new DesignerVerb("Create LaunchBar", new EventHandler(this.Verb_CreateLaunchBar));
          this.m_oLocalVerbs = new DesignerVerbCollection(new DesignerVerb[2]
          {
            this.m_oCreateApplicationButtonVerb,
            this.m_oCreateLaunchBarVerb
          });
          for (int index = 0; index < base.Verbs.Count; ++index)
            this.m_oLocalVerbs.Add(base.Verbs[index]);
        }
        return this.m_oLocalVerbs;
      }
    }

    protected override void OnContextMenu(int x, int y)
    {
      this.m_oCreateLaunchBarVerb.Enabled = this.RibbonCaption.LaunchBar == null;
      this.m_oCreateApplicationButtonVerb.Enabled = this.RibbonCaption.ApplicationButton == null;
      base.OnContextMenu(x, y);
    }

    private void Verb_CreateLaunchBar(object sender, EventArgs e)
    {
      this.NotifyComponentCreation((object) this, true);
      this.RibbonCaption.LaunchBar = this.DesignerHost.CreateComponent(typeof (QRibbonLaunchBar)) as QRibbonLaunchBar;
      ((MenuCommand) sender).Enabled = false;
      this.NotifyComponentCreation((object) this, false);
    }

    private void Verb_CreateApplicationButton(object sender, EventArgs e)
    {
      this.NotifyComponentCreation((object) this, true);
      this.RibbonCaption.ApplicationButton = this.DesignerHost.CreateComponent(typeof (QRibbonApplicationButton)) as QRibbonApplicationButton;
      ((MenuCommand) sender).Enabled = false;
      this.NotifyComponentCreation((object) this, false);
    }
  }
}
