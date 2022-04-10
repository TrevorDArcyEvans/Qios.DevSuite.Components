// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeChangeEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeChangeEventArgs : EventArgs
  {
    private bool m_bPerformLayout;
    private Rectangle m_oInvalidateRectangle;

    public QCompositeChangeEventArgs(bool performLayout, Rectangle invalidateRectangle)
    {
      this.m_bPerformLayout = performLayout;
      this.m_oInvalidateRectangle = invalidateRectangle;
    }

    public bool PerformLayout => this.m_bPerformLayout;

    public Rectangle InvalidateRectangle => this.m_oInvalidateRectangle;
  }
}
