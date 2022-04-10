// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QRibbonDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Ribbon;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QRibbonDesigner : QTabControlDesigner
  {
    public override void Initialize(IComponent component) => base.Initialize(component);

    public QRibbon Ribbon => (QRibbon) this.Control;

    protected override DesignerVerb AddTabPageVerb => new DesignerVerb(QResources.GetGeneral("QRibbonDesigner_AddRibbonPage"), new EventHandler(this.AddRibbonPageVerbClick));

    protected override Type TabPageType => typeof (QRibbonPage);

    private void AddRibbonPageVerbClick(object sender, EventArgs e) => this.AddTabPage();
  }
}
