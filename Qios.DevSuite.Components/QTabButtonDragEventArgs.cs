// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonDragEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QTabButtonDragEventArgs : EventArgs
  {
    private QTabButton m_oSourceTabButton;
    private QTabButton m_oTargetTabButton;
    private QTabStrip m_oTargetTabStrip;
    private QTabButtonDockStyle m_oTargetDock = QTabButtonDockStyle.Left;
    private int m_iTargetButtonOrder = -1;
    private bool m_bCancel;
    private bool m_bAllowDrop;
    private bool m_bCanSetAllowDrop;
    private bool m_bCanSetCancel;

    public QTabButtonDragEventArgs(
      QTabButton sourceTabButton,
      QTabButton targetTabButton,
      QTabStrip targetTabStrip,
      QTabButtonDockStyle targetDock,
      int targetButtonOrder,
      bool allowDrop,
      bool canSetCancel,
      bool canSetAllowDrop)
    {
      this.m_oSourceTabButton = sourceTabButton;
      this.m_oTargetTabButton = targetTabButton;
      this.m_oTargetTabStrip = targetTabStrip;
      this.m_oTargetDock = targetDock;
      this.m_bAllowDrop = allowDrop;
      this.m_iTargetButtonOrder = targetButtonOrder;
      this.m_bCanSetAllowDrop = canSetAllowDrop;
      this.m_bCanSetCancel = canSetCancel;
    }

    public QTabButtonDockStyle TargetDock => this.m_oTargetDock;

    public int TargetButtonOrder => this.m_iTargetButtonOrder;

    public QTabButton SourceTabButton => this.m_oSourceTabButton;

    public QTabButton TargetTabButton => this.m_oTargetTabButton;

    public QTabStrip TargetTabStrip => this.m_oTargetTabStrip;

    public bool Cancel
    {
      get => this.m_bCancel;
      set
      {
        if (!this.m_bCanSetCancel)
          throw new InvalidOperationException(QResources.GetException("QTabButtonDragEventArgs_CannotSetCancel"));
        this.m_bCancel = value;
      }
    }

    public bool AllowDrop
    {
      get => this.m_bAllowDrop;
      set
      {
        if (!this.m_bCanSetAllowDrop)
          throw new InvalidOperationException(QResources.GetException("QTabButtonDragEventArgs_CannotSetAllowDrop"));
        this.m_bAllowDrop = value;
      }
    }
  }
}
