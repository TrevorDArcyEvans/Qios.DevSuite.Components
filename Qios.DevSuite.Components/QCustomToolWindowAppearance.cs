// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCustomToolWindowAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCustomToolWindowAppearance : QAppearance
  {
    protected const int PropCaptionHeight = 20;
    protected const int PropCaptionMargin = 21;
    protected new const int CurrentPropertyCount = 2;
    protected new const int TotalPropertyCount = 22;

    public QCustomToolWindowAppearance()
    {
      this.Properties.DefineProperty(20, (object) 18);
      this.Properties.DefineProperty(21, (object) new QMargin(1, 1, 1, 1));
      this.Properties.DefineProperty(19, (object) 2);
    }

    protected override int GetRequestedCount() => 22;

    [Description("Gets or sets the height of the caption.")]
    [Category("QAppearance")]
    [QPropertyIndex(20)]
    public int CaptionHeight
    {
      get => (int) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the Margin between the Border and the Caption")]
    [QPropertyIndex(21)]
    public QMargin CaptionMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }
  }
}
