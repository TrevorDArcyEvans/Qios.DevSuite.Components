// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QLinePointResult
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QLinePointResult
  {
    private bool m_bResult;
    private PointF m_oLocation;

    public QLinePointResult(bool result, PointF location)
    {
      this.m_oLocation = location;
      this.m_bResult = result;
    }

    public bool Result => this.m_bResult;

    public PointF Location => this.m_oLocation;
  }
}
