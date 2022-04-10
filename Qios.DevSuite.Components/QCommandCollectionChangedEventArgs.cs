// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandCollectionChangedEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QCommandCollectionChangedEventArgs : EventArgs
  {
    private int m_iFromCount;
    private int m_iToCount;

    public QCommandCollectionChangedEventArgs(int fromCount, int toCount)
    {
      this.m_iFromCount = fromCount;
      this.m_iToCount = toCount;
    }

    public int FromCount => this.m_iFromCount;

    public int ToCount => this.m_iToCount;
  }
}
