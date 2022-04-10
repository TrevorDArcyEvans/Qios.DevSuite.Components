// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonContentAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonContentAppearance : QTabControlContentAppearance
  {
    public QRibbonContentAppearance()
    {
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RibbonContent]);
      this.Properties.DefineProperty(23, (object) true);
      this.Properties.DefineProperty(19, (object) true);
      this.Properties.DefineProperty(17, (object) new Point(2, 3));
    }
  }
}
