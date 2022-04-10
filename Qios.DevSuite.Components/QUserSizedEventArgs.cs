// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QUserSizedEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QUserSizedEventArgs : EventArgs
  {
    private Size m_oNewSize;

    public QUserSizedEventArgs(Size newSize) => this.m_oNewSize = newSize;

    public Size NewSize => this.m_oNewSize;
  }
}
