// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartLayoutStageProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartLayoutStageProperties
  {
    private Size m_oAvailableSize;
    private Size m_oActualContentSize;
    private Rectangle m_oAvailableBounds;
    private QPartApplyConstraintProperties m_oApplyConstraintProperties;

    public QPartLayoutStageProperties(
      Size availableSize,
      Rectangle availableBounds,
      Size actualContentSize)
    {
      this.m_oAvailableSize = availableSize;
      this.m_oAvailableBounds = availableBounds;
      this.m_oActualContentSize = actualContentSize;
    }

    public QPartLayoutStageProperties(Size availableSize, Rectangle availableBounds)
    {
      this.m_oAvailableSize = availableSize;
      this.m_oAvailableBounds = availableBounds;
    }

    public QPartLayoutStageProperties(Size availableSize, Size actualContentSize)
    {
      this.m_oAvailableSize = availableSize;
      this.m_oActualContentSize = actualContentSize;
    }

    public QPartLayoutStageProperties(
      Size availableSize,
      Size actualContentSize,
      QPartApplyConstraintProperties applyConstraintProperties)
    {
      this.m_oAvailableSize = availableSize;
      this.m_oActualContentSize = actualContentSize;
      this.m_oApplyConstraintProperties = applyConstraintProperties;
    }

    public Rectangle AvailableBounds => this.m_oAvailableBounds;

    public Size AvailableSize => this.m_oAvailableSize;

    public Size ActualContentSize => this.m_oActualContentSize;

    public QPartApplyConstraintProperties ApplyConstraintProperties => this.m_oApplyConstraintProperties;
  }
}
