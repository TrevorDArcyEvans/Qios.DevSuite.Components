// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandUIRequestEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCommandUIRequestEventArgs : EventArgs
  {
    private QCommandUIRequest m_eRequest;
    private Rectangle m_oUpdateBounds;

    public QCommandUIRequestEventArgs(QCommandUIRequest request, Rectangle updateBounds)
    {
      this.m_eRequest = request;
      this.m_oUpdateBounds = updateBounds;
    }

    public QCommandUIRequest Request => this.m_eRequest;

    public Rectangle UpdateBounds => this.m_oUpdateBounds;
  }
}
