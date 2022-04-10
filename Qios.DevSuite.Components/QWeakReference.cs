// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakReference
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QWeakReference : WeakReference
  {
    private bool m_bFinalized;

    public QWeakReference(object target)
      : base(target)
    {
    }

    public bool Finalized => this.m_bFinalized;

    ~QWeakReference() => this.m_bFinalized = true;
  }
}
