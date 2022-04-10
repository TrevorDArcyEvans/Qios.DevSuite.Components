// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuMouseEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QMenuMouseEventArgs : EventArgs
  {
    private QMenuItem m_oMenuItem;
    private MouseButtons m_oButton;
    private int m_iClicks;
    private int m_iDelta;
    private int m_iX;
    private int m_iY;

    public QMenuMouseEventArgs(QMenuItem menuItem) => this.m_oMenuItem = menuItem;

    internal QMenuMouseEventArgs(MouseEventArgs e, QMenuItem menuItem)
    {
      this.m_oMenuItem = menuItem;
      this.m_oButton = e.Button;
      this.m_iClicks = e.Clicks;
      this.m_iDelta = e.Delta;
      this.m_iX = e.X;
      this.m_iY = e.Y;
    }

    public QMenuItem MenuItem => this.m_oMenuItem;

    public MouseButtons Button => this.m_oButton;

    public int Clicks => this.m_iClicks;

    public int Delta => this.m_iDelta;

    public int X => this.m_iX;

    public int Y => this.m_iY;
  }
}
