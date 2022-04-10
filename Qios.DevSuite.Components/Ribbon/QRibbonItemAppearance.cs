// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonItemAppearance : QCompositeItemAppearance
  {
    private QRibbonItemAppearance()
    {
    }

    public QRibbonItemAppearance(QRibbonItemConfigurationType configurationType)
    {
      this.Properties.DefineProperty(1, (object) QColorStyle.Metallic);
      this.Properties.DefineProperty(9, (object) 1);
      this.Properties.DefineProperty(11, (object) 100);
      if (configurationType == QRibbonItemConfigurationType.RibbonItemBar)
        this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareButton]);
      else
        this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RoundedButton2]);
    }

    [QShapeDesignVisible(QShapeType.Button)]
    [Description("Gets or sets the shape in this appearance")]
    [Category("QAppearance")]
    public override QShape Shape
    {
      get => base.Shape;
      set => base.Shape = value;
    }
  }
}
