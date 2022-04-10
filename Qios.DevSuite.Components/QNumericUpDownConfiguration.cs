// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QNumericUpDownConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QNumericUpDownConfiguration : QInputBoxConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 18;

    public QNumericUpDownConfiguration() => this.Properties.DefineProperty(6, (object) QInputBoxStyle.UpDown);

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QAutoCompleteMode AutoCompleteMode
    {
      get => base.AutoCompleteMode;
      set => base.AutoCompleteMode = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QAutoCompleteSource AutoCompleteSource
    {
      get => base.AutoCompleteSource;
      set => base.AutoCompleteSource = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QInputBoxStyle InputStyle
    {
      get => base.InputStyle;
      set => base.InputStyle = value;
    }

    protected override int GetRequestedCount() => 18;
  }
}
