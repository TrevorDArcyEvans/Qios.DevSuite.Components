// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeBalloonWindowConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeBalloonWindowConfiguration : QBalloonWindowConfiguration
  {
    public QCompositeBalloonWindowConfiguration()
    {
      this.Properties.DefineProperty(21, (object) new QPadding(3, 3, 3, 3));
      this.Properties.DefineProperty(10, (object) new Size(200, 0));
    }
  }
}
