// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QCompositeEventArgs : EventArgs
  {
    private QComposite m_oComposite;
    private QCompositeItemBase m_oOriginalItem;
    private QCompositeItemBase m_oItem;
    private QCompositeActivationType m_eActivationType;

    public QCompositeEventArgs(
      QComposite composite,
      QCompositeItemBase item,
      QCompositeActivationType activationType)
    {
      this.m_oComposite = composite;
      this.m_oItem = item;
      this.m_eActivationType = activationType;
      QCompositeItemBase qcompositeItemBase = item;
      if (qcompositeItemBase == null)
        return;
      this.m_oOriginalItem = qcompositeItemBase.OriginalItem;
    }

    public QCompositeItemBase Item => this.m_oItem;

    public QCompositeItemBase OriginalItem => this.m_oOriginalItem;

    public QCompositeActivationType ActivationType => this.m_eActivationType;

    public QComposite Composite => this.m_oComposite;
  }
}
