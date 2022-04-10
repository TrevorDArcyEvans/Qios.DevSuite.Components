// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTimerManager
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Runtime.InteropServices;

namespace Qios.DevSuite.Components
{
  internal class QTimerManager : IDisposable
  {
    private IntPtr m_oTimerID = IntPtr.Zero;
    private QWeakReferenceCollection m_oCollection;
    private int m_iInterval;

    public QTimerManager(int interval)
    {
      this.m_iInterval = interval;
      this.m_oCollection = new QWeakReferenceCollection();
    }

    public int Interval
    {
      get => this.m_iInterval;
      set => this.m_iInterval = value;
    }

    public bool IsRegistered(IQTimerClient client) => this.m_oCollection.ContainsObject((object) client, true);

    public void Register(IQTimerClient client)
    {
      if (this.m_oCollection.ContainsObject((object) client, true))
        return;
      this.m_oCollection.Add(new QWeakReference((object) client));
      this.StartStopTimer();
    }

    public void Unregister(IQTimerClient client)
    {
      if (!this.m_oCollection.ContainsObject((object) client, true))
        return;
      this.m_oCollection.RemoveObject((object) client, true);
      this.StartStopTimer();
    }

    private void QTimerCallback(IntPtr handle, uint msg, uint idEvent, int dwTime)
    {
      if ((long) idEvent != (long) this.m_oTimerID.ToInt32())
        return;
      for (int index = 0; index < this.m_oCollection.Count; ++index)
      {
        if (this.m_oCollection[index].Target is IQTimerClient target)
          target.Tick(this);
      }
    }

    private void StartStopTimer()
    {
      this.m_oCollection.CleanCollection();
      if (this.m_oCollection.Count > 0 && this.m_oTimerID == IntPtr.Zero)
      {
        this.StartTimer();
      }
      else
      {
        if (this.m_oCollection.Count != 0 || !(this.m_oTimerID != IntPtr.Zero))
          return;
        this.StopTimer();
      }
    }

    private void StartTimer()
    {
      if (this.m_oTimerID != IntPtr.Zero)
        this.StopTimer();
      QTimerCallbackDelegate lpTimerFunc = new QTimerCallbackDelegate(this.QTimerCallback);
      GCHandle.Alloc((object) lpTimerFunc);
      this.m_oTimerID = NativeMethods.SetTimer(IntPtr.Zero, IntPtr.Zero, (uint) this.m_iInterval, lpTimerFunc);
    }

    private void StopTimer()
    {
      if (!(this.m_oTimerID != IntPtr.Zero))
        return;
      NativeMethods.KillTimer(IntPtr.Zero, this.m_oTimerID);
      this.m_oTimerID = IntPtr.Zero;
    }

    protected virtual void Dispose(bool disposing) => this.StopTimer();

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~QTimerManager() => this.Dispose(false);
  }
}
