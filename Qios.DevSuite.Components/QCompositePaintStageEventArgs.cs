// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositePaintStageEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QCompositePaintStageEventArgs : QCompositeEventArgs
  {
    private QPartPaintContext m_oPaintContext;
    private QPartPaintStage m_ePaintStage;

    public QCompositePaintStageEventArgs(
      QPartPaintContext paintContext,
      QPartPaintStage paintStage,
      QComposite composite,
      QCompositeItemBase item)
      : base(composite, item, QCompositeActivationType.None)
    {
      this.m_oPaintContext = paintContext;
      this.m_ePaintStage = paintStage;
    }

    public QPartPaintContext Context => this.m_oPaintContext;

    public QPartPaintStage Stage => this.m_ePaintStage;
  }
}
