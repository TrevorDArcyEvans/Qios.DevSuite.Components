// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingInputBoxWindowCompositeScrollConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QFloatingInputBoxWindowCompositeScrollConfiguration : QCompositeScrollConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 15;

    public QFloatingInputBoxWindowCompositeScrollConfiguration()
    {
      this.Properties.DefineProperty(13, (object) QCompositeScrollVisibility.Automatic);
      this.Properties.DefineProperty(11, (object) QCompositeScrollType.ScrollBar);
      this.Properties.DefineProperty(2, (object) new QPadding(4, 4, 4, 4));
    }

    protected override int GetRequestedCount() => 15;
  }
}
