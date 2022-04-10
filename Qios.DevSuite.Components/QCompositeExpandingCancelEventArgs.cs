// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeExpandingCancelEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeExpandingCancelEventArgs : QCompositeExpandedEventArgs
  {
    private bool m_bCancel;

    public QCompositeExpandingCancelEventArgs(
      QComposite composite,
      QCompositeItemBase item,
      QCompositeActivationType activationType,
      Rectangle bounds,
      QCommandDirections animateDirection,
      bool allowAnimation,
      bool cancel)
      : base(composite, item, activationType, bounds, animateDirection, allowAnimation)
    {
      this.m_bCancel = cancel;
    }

    public bool Cancel
    {
      get => this.m_bCancel;
      set => this.m_bCancel = value;
    }
  }
}
