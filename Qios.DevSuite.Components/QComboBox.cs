// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QComboBox
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QComboBox : QInputBox
  {
    protected override QInputBoxConfiguration CreateConfigurationInstance() => (QInputBoxConfiguration) new QComboBoxConfiguration();

    [Description("Gets or sets the QInputBoxConfiguration for this QInputBox.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QComboBoxConfiguration Configuration
    {
      get => base.Configuration as QComboBoxConfiguration;
      set => this.Configuration = (QInputBoxConfiguration) value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxAccelerationCollection Accelerations => base.Accelerations;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Decimal NumericValue
    {
      get => base.NumericValue;
      set => base.NumericValue = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Decimal Increment
    {
      get => base.Increment;
      set => base.Increment = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Decimal MaximumValue
    {
      get => base.MaximumValue;
      set => base.MaximumValue = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Decimal MinimumValue
    {
      get => base.MinimumValue;
      set => base.MinimumValue = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string FormatString
    {
      get => base.FormatString;
      set => base.FormatString = value;
    }
  }
}
