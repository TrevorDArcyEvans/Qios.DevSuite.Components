// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QMenuEventArgs : EventArgs
  {
    private QMenuItem m_oMenuItem;
    private QMenuItemActivationType m_eActivationType;
    private bool m_bExpanded;

    public QMenuEventArgs(
      QMenuItem menuItem,
      QMenuItemActivationType activationType,
      bool expanded)
    {
      this.m_oMenuItem = menuItem;
      this.m_eActivationType = activationType;
      this.m_bExpanded = expanded;
    }

    public QMenuItem MenuItem => this.m_oMenuItem;

    public QMenuItemActivationType ActivationType => this.m_eActivationType;

    public bool Expanded => this.m_bExpanded;
  }
}
