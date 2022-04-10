// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStripButtonChangeEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QTabStripButtonChangeEventArgs : EventArgs
  {
    private QTabButton m_oFromButton;
    private QTabButton m_oToButton;
    private bool m_bCanSetToButton;
    private bool m_bCanCancel;
    private bool m_bCancel;

    public QTabStripButtonChangeEventArgs(
      QTabButton fromButton,
      QTabButton toButton,
      bool isChangingEvent)
    {
      this.m_oFromButton = fromButton;
      this.m_oToButton = toButton;
      this.m_bCanSetToButton = isChangingEvent;
      this.m_bCanCancel = isChangingEvent;
    }

    public QTabButton FromButton => this.m_oFromButton;

    public QTabButton ToButton
    {
      get => this.m_oToButton;
      set
      {
        if (!this.m_bCanSetToButton)
          throw new InvalidOperationException(QResources.GetException("QTabStripButtonChangeEventArgs_CannotSetToButton"));
        this.m_oToButton = value;
      }
    }

    internal bool Cancel
    {
      get => this.m_bCancel;
      set => this.m_bCancel = this.m_bCanCancel || !value ? value : throw new InvalidOperationException(QResources.GetException("QTabStripButtonChangeEventArgs_CannotCancel"));
    }
  }
}
