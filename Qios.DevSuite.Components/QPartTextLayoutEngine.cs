// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartTextLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartTextLayoutEngine : QPartLinearLayoutEngine
  {
    private static QPartTextLayoutEngine m_oDefault;

    public static QPartTextLayoutEngine Default
    {
      get
      {
        if (QPartTextLayoutEngine.m_oDefault == null)
          QPartTextLayoutEngine.m_oDefault = new QPartTextLayoutEngine();
        return QPartTextLayoutEngine.m_oDefault;
      }
    }

    protected override void ApplyConstraintsToCustomContent(
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      QPartNativeSizedString contentObject1 = part.ContentObject as QPartNativeSizedString;
      bool flag1 = false;
      string contentObject2;
      if (contentObject1 != null)
      {
        contentObject2 = contentObject1.Value;
        flag1 = true;
      }
      else
        contentObject2 = part.ContentObject as string;
      if (contentObject2 == null || (layoutContext.StringFormat.FormatFlags & StringFormatFlags.NoWrap) == StringFormatFlags.NoWrap)
        return;
      Size innerSize1 = calculatedProperties.InnerSize;
      Size empty = Size.Empty;
      Size innerSize2;
      if (flag1)
      {
        innerSize2 = NativeHelper.CalculateTextSize(contentObject2, layoutContext.Font, innerSize1.Width, (QDrawTextOptions) 16, layoutContext.Graphics);
      }
      else
      {
        bool flag2 = part.Properties is QCompositeTextConfiguration properties1 && properties1.Orientation != QContentOrientation.Horizontal;
        innerSize2 = Size.Ceiling(layoutContext.Graphics.MeasureString(contentObject2, layoutContext.Font, flag2 ? innerSize1.Height : innerSize1.Width, layoutContext.StringFormat));
        if (flag2)
          innerSize2 = new Size(innerSize2.Height, innerSize2.Width);
      }
      calculatedProperties.SetUnstretchedSizesBasedOnInnerSize(innerSize2);
      calculatedProperties.SetSizesBasedOnInnerSize(innerSize1, false);
    }
  }
}
