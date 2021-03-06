// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStatusBarPanelEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QStatusBarPanelEventArgs : EventArgs
  {
    private QStatusBarPanel m_oPanel;

    public QStatusBarPanelEventArgs(QStatusBarPanel panel) => this.Panel = panel;

    public QStatusBarPanel Panel
    {
      get => this.m_oPanel;
      set => this.m_oPanel = value;
    }
  }
}
