﻿// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPanelAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonPanelAppearance : QShapeAppearance
  {
    public QRibbonPanelAppearance()
    {
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RibbonPanel]);
      this.Properties.DefineProperty(1, (object) QColorStyle.Metallic);
      this.Properties.DefineProperty(6, (object) QAppearanceUnit.Pixel);
      this.Properties.DefineProperty(5, (object) 19);
    }

    [Description("Gets or sets the shape in this appearance")]
    [QShapeDesignVisible(QShapeType.Content)]
    [Category("QAppearance")]
    public override QShape Shape
    {
      get => base.Shape;
      set => base.Shape = value;
    }
  }
}
