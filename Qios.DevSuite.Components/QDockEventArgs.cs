// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QDockEventArgs : EventArgs
  {
    private QDockBar m_oDockBar;
    private QDockControl m_oDockControl;

    public QDockEventArgs(QDockBar dockBar, QDockControl dockControl)
    {
      this.m_oDockBar = dockBar;
      this.m_oDockControl = dockControl;
    }

    public QDockBar DockBar => this.m_oDockBar;

    public QDockControl DockControl => this.m_oDockControl;
  }
}
