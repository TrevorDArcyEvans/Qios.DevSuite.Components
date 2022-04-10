// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenuConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QCompositeMenuConfiguration : QCompositeConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 29;

    public QCompositeMenuConfiguration()
    {
      this.Properties.DefineProperty(4, (object) false);
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
      this.Properties.DefineProperty(16, (object) QCompositeItemLayout.Table);
      this.Properties.DefineProperty(17, (object) (QCompositeExpandBehavior.AutoExpand | QCompositeExpandBehavior.AutoChangeExpand | QCompositeExpandBehavior.ExpandOnNavigationKeys | QCompositeExpandBehavior.CloseOnNavigationKeys));
      this.Properties.DefineProperty(23, (object) true);
      this.ScrollConfiguration.Properties.DefineProperty(13, (object) QCompositeScrollVisibility.Automatic);
      this.ScrollConfiguration.Properties.DefineProperty(5, (object) true);
    }

    protected override int GetRequestedCount() => 29;
  }
}
