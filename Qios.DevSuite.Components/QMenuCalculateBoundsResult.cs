// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuCalculateBoundsResult
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QMenuCalculateBoundsResult
  {
    private Rectangle m_oBounds;
    private QRelativePositions m_eOpeningItemPosition;
    private QCommandDirections m_eAnimateDirection;

    public QMenuCalculateBoundsResult(
      Rectangle bounds,
      QRelativePositions openingItemPosition,
      QCommandDirections animateDirection)
    {
      this.m_oBounds = bounds;
      this.m_eOpeningItemPosition = openingItemPosition;
      this.m_eAnimateDirection = animateDirection;
    }

    public Rectangle Bounds => this.m_oBounds;

    public QRelativePositions OpeningItemPosition => this.m_eOpeningItemPosition;

    public QCommandDirections AnimateDirection => this.m_eAnimateDirection;
  }
}
