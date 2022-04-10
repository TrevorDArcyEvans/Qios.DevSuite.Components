// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeIconConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeIconConfiguration : QContentPartConfiguration
  {
    protected internal const int PropIconSize = 15;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 16;

    public QCompositeIconConfiguration() => this.Properties.DefineProperty(15, (object) new Size(16, 16));

    protected override int GetRequestedCount() => 16;

    [Category("QAppearance")]
    [QPropertyIndex(15)]
    [Description("Gets or sets the size of the Icon")]
    public Size IconSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }
  }
}
