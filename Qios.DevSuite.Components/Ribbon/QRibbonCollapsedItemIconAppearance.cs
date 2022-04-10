// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCollapsedItemIconAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCollapsedItemIconAppearance : QCompositeItemAppearance
  {
    public QRibbonCollapsedItemIconAppearance()
    {
      this.Properties.DefineProperty(1, (object) QColorStyle.Solid);
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RoundedButton2]);
    }
  }
}
