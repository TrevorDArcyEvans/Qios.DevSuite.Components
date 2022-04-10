// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCommandControlContainerDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QCommandControlContainerDesigner : ParentControlDesigner
  {
    private QCommandControlContainer m_oContainer;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.m_oContainer = (QCommandControlContainer) component;
      this.GridSize = new Size(8, 8);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
    }

    protected override bool DrawGrid
    {
      get => true;
      set
      {
      }
    }

    public override SelectionRules SelectionRules => SelectionRules.Visible | SelectionRules.BottomSizeable | SelectionRules.RightSizeable;

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oGeneralHandler == null)
        return;
      this.m_oGeneralHandler.Dispose();
    }
  }
}
