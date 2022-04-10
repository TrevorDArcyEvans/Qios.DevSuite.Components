// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFastPropertyChangedEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QFastPropertyChangedEventArgs : EventArgs
  {
    private int m_iIndex;

    public QFastPropertyChangedEventArgs(int index) => this.m_iIndex = index;

    public int PropertyIndex => this.m_iIndex;
  }
}
