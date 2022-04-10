// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QScrollBarDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  internal class QScrollBarDesigner : ControlDesigner
  {
    private QScrollBar m_oScrollBar;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      this.m_oScrollBar = component as QScrollBar;
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
    }

    public override SelectionRules SelectionRules
    {
      get
      {
        SelectionRules selectionRules = base.SelectionRules;
        if (this.m_oScrollBar != null)
          selectionRules = this.m_oScrollBar.Configuration.Direction != QScrollBarDirection.Horizontal ? selectionRules & ~SelectionRules.LeftSizeable & ~SelectionRules.RightSizeable : selectionRules & ~SelectionRules.TopSizeable & ~SelectionRules.BottomSizeable;
        return selectionRules;
      }
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this.m_oGeneralHandler == null)
        return;
      this.m_oGeneralHandler.Dispose();
    }
  }
}
