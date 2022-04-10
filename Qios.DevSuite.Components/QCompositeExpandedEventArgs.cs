// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeExpandedEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeExpandedEventArgs : QCompositeEventArgs
  {
    private QCommandDirections m_oDirection;
    private Rectangle m_oBounds;
    private bool m_bAllowAnimation;

    public QCompositeExpandedEventArgs(
      QComposite composite,
      QCompositeItemBase item,
      QCompositeActivationType activationType,
      Rectangle bounds,
      QCommandDirections animateDirection,
      bool allowAnimation)
      : base(composite, item, activationType)
    {
      this.m_oDirection = animateDirection;
      this.m_oBounds = bounds;
      this.m_bAllowAnimation = allowAnimation;
    }

    public bool AllowAnimation
    {
      get => this.m_bAllowAnimation;
      set => this.m_bAllowAnimation = value;
    }

    public Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    public QCommandDirections AnimateDirection
    {
      get => this.m_oDirection;
      set => this.m_oDirection = value;
    }
  }
}
