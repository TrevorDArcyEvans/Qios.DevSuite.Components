// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeToolTipConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QCompositeToolTipConfiguration : QEnabledToolTipConfiguration
  {
    public QCompositeToolTipConfiguration() => this.Properties.DefineProperty(2, (object) -1);

    protected override QBalloonWindowAppearance CreateBalloonWindowAppearanceInstance() => (QBalloonWindowAppearance) new QCompositeBalloonWindowAppearance();

    protected override QBalloonWindowConfiguration CreateBalloonWindowConfigurationInstance() => (QBalloonWindowConfiguration) new QCompositeBalloonWindowConfiguration();
  }
}
