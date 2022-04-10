// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppearanceFillers
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QAppearanceFillers
  {
    private QAppearanceFiller[] m_aFillers;

    internal QAppearanceFillers()
    {
      this.m_aFillers = new QAppearanceFiller[Enum.GetValues(typeof (QColorStyle)).Length];
      this.m_aFillers[0] = (QAppearanceFiller) new QSolidAppearanceFiller();
      this.m_aFillers[2] = (QAppearanceFiller) new QMetallicAppearanceFiller();
      this.m_aFillers[1] = (QAppearanceFiller) new QGradientAppearanceFiller();
    }

    public QAppearanceFiller this[QColorStyle style] => this.m_aFillers[(int) style];
  }
}
