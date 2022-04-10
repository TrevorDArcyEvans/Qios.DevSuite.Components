// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindowEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QShapedWindowEventArgs : EventArgs
  {
    private QShapedWindow m_oShapedWindow;

    public QShapedWindowEventArgs(QShapedWindow shapedWindow) => this.m_oShapedWindow = shapedWindow;

    public QShapedWindow ShapedWindow
    {
      get => this.m_oShapedWindow;
      set => this.m_oShapedWindow = value;
    }
  }
}
