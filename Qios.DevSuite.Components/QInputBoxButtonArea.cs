// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxButtonArea
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QInputBoxButtonArea : QButtonArea
  {
    internal QInputBoxButtonArea(MouseButtons listensToButtons)
      : base(listensToButtons)
    {
    }

    public override bool HandleMouseDown(MouseEventArgs e)
    {
      if (!this.Visible || !this.Enabled || this.State != QButtonState.Hot || !this.Bounds.Contains(e.X, e.Y) || !this.ListensToButton(e.Button))
        return false;
      this.SetState(QButtonState.Pressed, e);
      return true;
    }

    public override bool HandleMouseUp(MouseEventArgs e)
    {
      if (!this.Visible || !this.Enabled)
        return false;
      if (this.Bounds.Contains(e.X, e.Y))
        this.SetState(QButtonState.Hot, e);
      else
        this.SetState(QButtonState.Normal, e);
      return false;
    }
  }
}
