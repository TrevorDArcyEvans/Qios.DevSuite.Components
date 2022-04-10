// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabPageChangeEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QTabPageChangeEventArgs : EventArgs
  {
    private QTabPage m_oFromPage;
    private QTabPage m_oToPage;
    private bool m_bCanSetToPage;
    private bool m_bCancel;

    public QTabPageChangeEventArgs(QTabPage fromPage, QTabPage toPage, bool canSetToPage)
    {
      this.m_oFromPage = fromPage;
      this.m_oToPage = toPage;
      this.m_bCanSetToPage = canSetToPage;
      this.m_bCancel = false;
    }

    public QTabPage FromPage => this.m_oFromPage;

    public QTabPage ToPage
    {
      get => this.m_oToPage;
      set
      {
        if (!this.m_bCanSetToPage)
          throw new InvalidOperationException(QResources.GetException("QTabPageChangeEventArgs_CannotSetToPage"));
        this.m_oToPage = value;
      }
    }

    public bool Cancel
    {
      get => this.m_bCancel;
      set
      {
        if (!this.m_bCanSetToPage)
          throw new InvalidOperationException(QResources.GetException("QTabPageChangeEventArgs_CannotSetCancel"));
        this.m_bCancel = value;
      }
    }
  }
}
