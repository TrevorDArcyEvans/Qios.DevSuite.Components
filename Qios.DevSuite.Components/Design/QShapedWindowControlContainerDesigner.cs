// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapedWindowControlContainerDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QShapedWindowControlContainerDesigner : ParentControlDesigner
  {
    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.GridSize = new Size(8, 8);
    }

    protected override bool DrawGrid
    {
      get => true;
      set => base.DrawGrid = value;
    }

    public override SelectionRules SelectionRules => SelectionRules.Locked;
  }
}
