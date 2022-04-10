// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollBarAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QScrollBarAppearance : QShapeAppearance
  {
    public QScrollBarAppearance() => this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareButton]);

    [Description("Gets or sets the shape in this appearance")]
    [Category("QAppearance")]
    [QShapeDesignVisible(QShapeType.Button)]
    public override QShape Shape
    {
      get => base.Shape;
      set => base.Shape = value;
    }
  }
}
