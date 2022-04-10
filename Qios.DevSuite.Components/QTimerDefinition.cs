// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTimerDefinition
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Runtime.InteropServices;

namespace Qios.DevSuite.Components
{
  internal class QTimerDefinition
  {
    private int m_iTimerID;
    private int m_iTimerInterval;
    private GCHandle m_oGCHandle;

    public QTimerDefinition(int timerID, int timerInterval)
    {
      this.m_iTimerID = timerID;
      this.m_iTimerInterval = timerInterval;
    }

    public QTimerDefinition(int timerID, int timerInterval, GCHandle handle)
    {
      this.m_iTimerID = timerID;
      this.m_iTimerInterval = timerInterval;
      this.m_oGCHandle = handle;
    }

    public int TimerID => this.m_iTimerID;

    public int TimerInteval
    {
      get => this.m_iTimerInterval;
      set => this.m_iTimerInterval = value;
    }

    public GCHandle GCHandle
    {
      get => this.m_oGCHandle;
      set => this.m_oGCHandle = value;
    }
  }
}
