// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeResizeItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeResizeItemConfiguration : QCompositeItemConfiguration
  {
    protected internal const int PropResizeBorder = 23;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 24;

    public QCompositeResizeItemConfiguration() => this.Properties.DefineProperty(23, (object) QCompositeResizeBorder.BottomRight);

    [QPropertyIndex(23)]
    [Category("QAppearance")]
    [Description("Gets or sets the resize border")]
    public QCompositeResizeBorder ResizeBorder
    {
      get => (QCompositeResizeBorder) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    protected override int GetRequestedCount() => 24;
  }
}
