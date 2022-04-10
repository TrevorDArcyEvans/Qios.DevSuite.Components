// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QScrollEventArgs : EventArgs
  {
    private int m_iCurrentValue;
    private int m_iPreviousValue;

    public QScrollEventArgs(int currentValue, int previousValue)
    {
      this.CurrentValue = currentValue;
      this.PreviousValue = previousValue;
    }

    public int CurrentValue
    {
      get => this.m_iCurrentValue;
      set => this.m_iCurrentValue = value;
    }

    public int PreviousValue
    {
      get => this.m_iPreviousValue;
      set => this.m_iPreviousValue = value;
    }
  }
}
