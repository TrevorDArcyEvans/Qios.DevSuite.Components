// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxButtonEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QInputBoxButtonEventArgs : MouseEventArgs
  {
    private QInputBoxButtonType m_eButtonType = QInputBoxButtonType.DropDownButton;

    public QInputBoxButtonEventArgs(
      QInputBoxButtonType buttonType,
      MouseButtons buttons,
      int clicks,
      int left,
      int top)
      : base(buttons, clicks, left, top, 0)
    {
      this.m_eButtonType = buttonType;
    }

    public QInputBoxButtonType ButtonType => this.m_eButtonType;
  }
}
