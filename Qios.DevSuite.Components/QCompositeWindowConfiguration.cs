// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeWindowConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeWindowConfiguration : QFloatingWindowConfiguration
  {
    protected internal const int PropShapeWindow = 6;
    protected internal const int PropUseSizeAsRequestedSize = 7;
    protected new const int CurrentPropertyCount = 2;
    protected new const int TotalPropertyCount = 8;

    public QCompositeWindowConfiguration()
    {
      this.Properties.DefineProperty(6, (object) true);
      this.Properties.DefineProperty(7, (object) false);
    }

    [QPropertyIndex(6)]
    [Description("Determines if the window should be shaped to the shape of the QComposite")]
    [Category("QAppearance")]
    public bool ShapeWindow
    {
      get => (bool) this.Properties.GetProperty(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [Description("Indicates whether the size of the window should be used as the base for the calculation of the new window size. Setting this to true makes sure that when a the composite on a window can stretch or shrink, the composite will be stretched or shrunken to the requested size.")]
    [QPropertyIndex(7)]
    [Category("QAppearance")]
    public bool UseSizeAsRequestedSize
    {
      get => (bool) this.Properties.GetProperty(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    protected override int GetRequestedCount() => 8;
  }
}
