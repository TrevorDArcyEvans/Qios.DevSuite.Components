// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QSlideEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QSlideEventArgs : EventArgs
  {
    private bool m_bSlideIn;
    private bool m_bUseMotion;

    public QSlideEventArgs(bool slideIn, bool useMotion)
    {
      this.m_bSlideIn = slideIn;
      this.m_bUseMotion = useMotion;
    }

    public bool SlideIn => this.m_bSlideIn;

    public bool UseMotion => this.m_bUseMotion;
  }
}
