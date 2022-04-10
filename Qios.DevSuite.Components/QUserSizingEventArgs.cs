// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QUserSizingEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QUserSizingEventArgs : EventArgs
  {
    private Size m_oNewSize;
    private bool m_bCancel;

    public QUserSizingEventArgs(Size newSize) => this.NewSize = newSize;

    public Size NewSize
    {
      get => this.m_oNewSize;
      set => this.m_oNewSize = value;
    }

    public bool Cancel
    {
      get => this.m_bCancel;
      set => this.m_bCancel = value;
    }
  }
}
