// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartMarkupTextLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartMarkupTextLayoutEngine : QPartLinearLayoutEngine
  {
    private static QPartMarkupTextLayoutEngine m_oDefault;

    public static QPartMarkupTextLayoutEngine Default
    {
      get
      {
        if (QPartMarkupTextLayoutEngine.m_oDefault == null)
          QPartMarkupTextLayoutEngine.m_oDefault = new QPartMarkupTextLayoutEngine();
        return QPartMarkupTextLayoutEngine.m_oDefault;
      }
    }

    protected override void ApplyConstraintsToCustomContent(
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      if (!(part.ContentObject is QMarkupText contentObject))
        return;
      Size innerSize = calculatedProperties.InnerSize;
      contentObject.PutBounds(new Rectangle(Point.Empty, innerSize));
      QMarkupTextParams markupParams = new QMarkupTextParams(layoutContext.Graphics);
      contentObject.RenderMarkupText(markupParams);
      Size size = contentObject.Size;
      calculatedProperties.SetUnstretchedSizesBasedOnInnerSize(size);
      calculatedProperties.SetSizesBasedOnInnerSize(innerSize, false);
    }
  }
}
