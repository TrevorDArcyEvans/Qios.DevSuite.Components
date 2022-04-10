// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQPartSharedProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public interface IQPartSharedProperties
  {
    QPartAlignment GetAlignmentHorizontal(IQPart part);

    QPartAlignment GetAlignmentVertical(IQPart part);

    QPartAlignment GetContentAlignmentHorizontal(IQPart part);

    QPartAlignment GetContentAlignmentVertical(IQPart part);

    QPartDirection GetDirection(IQPart part);

    Size GetMinimumSize(IQPart part);

    Size GetMaximumSize(IQPart part);

    QTristateBool GetVisible(IQPart part);

    string GetContentLayoutOrder(IQPart part);

    QPartOptions GetOptions(IQPart part);

    IQPadding[] GetPaddings(IQPart part);

    IQMargin[] GetMargins(IQPart part);
  }
}
