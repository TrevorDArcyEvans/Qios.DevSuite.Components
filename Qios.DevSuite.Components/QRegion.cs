// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRegion
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QRegion : IDisposable
  {
    private bool m_bIsDisposed;
    private Region m_oRegion;

    public QRegion(Region region) => this.m_oRegion = region;

    public bool IsDisposed => this.m_bIsDisposed;

    public Region Region => this.m_bIsDisposed ? (Region) null : this.m_oRegion;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.m_bIsDisposed = true;
      this.m_oRegion.Dispose();
    }

    ~QRegion() => this.Dispose(false);

    public static implicit operator Region(QRegion region) => region?.Region;
  }
}
