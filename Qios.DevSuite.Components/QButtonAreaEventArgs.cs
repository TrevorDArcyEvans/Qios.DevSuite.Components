// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QButtonAreaEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QButtonAreaEventArgs : EventArgs
  {
    private QButtonState m_eFromState;
    private QButtonState m_eToState;
    private MouseButtons m_ePressedButtons;
    private int m_iX;
    private int m_iY;

    public QButtonAreaEventArgs(
      QButtonState fromState,
      QButtonState toState,
      MouseButtons pressedButtons,
      int x,
      int y)
    {
      this.m_eFromState = fromState;
      this.m_eToState = toState;
      this.m_ePressedButtons = pressedButtons;
      this.m_iX = x;
      this.m_iY = y;
    }

    public QButtonState FromState
    {
      get => this.m_eFromState;
      set => this.m_eFromState = value;
    }

    public QButtonState ToState
    {
      get => this.m_eToState;
      set => this.m_eToState = value;
    }

    public MouseButtons PressedButtons
    {
      get => this.m_ePressedButtons;
      set => this.m_ePressedButtons = value;
    }

    public int X
    {
      get => this.m_iX;
      set => this.m_iX = value;
    }

    public int Y
    {
      get => this.m_iY;
      set => this.m_iY = value;
    }
  }
}
