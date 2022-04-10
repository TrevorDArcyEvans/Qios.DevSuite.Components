// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQPartLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public interface IQPartLayoutEngine
  {
    void PerformLayout(Rectangle rectangle, IQPart part, QPartLayoutContext layoutContext);

    void PrepareForLayout(IQPart part, QPartLayoutContext layoutContext);

    Size CalculatePartSize(IQPart part, QPartLayoutContext layoutContext);

    void ApplyConstraints(
      Size size,
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties);

    void LayoutPart(Rectangle rectangle, IQPart part, QPartLayoutContext layoutContext);

    void FinishLayout(IQPart part, QPartLayoutContext layoutContext);
  }
}
