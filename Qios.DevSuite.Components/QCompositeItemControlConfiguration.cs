// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemControlConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeItemControlConfiguration : QContentPartConfiguration
  {
    protected const int PropScrollWithImage = 15;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 16;

    public QCompositeItemControlConfiguration()
    {
      this.Properties.DefineProperty(15, (object) true);
      this.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
    }

    protected override int GetRequestedCount() => 16;

    [Category("QAppearance")]
    [Description("Gets or sets whether an image of the control must be used when scrolling animated.")]
    [QPropertyIndex(15)]
    public bool ScrollWithImage
    {
      get => (bool) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }
  }
}
