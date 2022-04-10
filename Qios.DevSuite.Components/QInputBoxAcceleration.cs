// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxAcceleration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QInputBoxAcceleration
  {
    private long m_iTicks;
    private Decimal m_iIncrement;
    private int m_iSeconds;

    public QInputBoxAcceleration(int seconds, Decimal increment)
    {
      this.m_iIncrement = increment;
      this.m_iSeconds = Math.Max(0, seconds);
      this.m_iTicks = (long) (10000000 * this.m_iSeconds);
    }

    internal long Ticks => this.m_iTicks;

    public int Seconds => this.m_iSeconds;

    public Decimal Increment => this.m_iIncrement;
  }
}
