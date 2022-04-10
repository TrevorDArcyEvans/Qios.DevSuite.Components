// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QComboBoxConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QComboBoxConfiguration : QInputBoxConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 18;

    public QComboBoxConfiguration() => this.Properties.DefineProperty(6, (object) QInputBoxStyle.DropDown);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QInputBoxStyle InputStyle
    {
      get => base.InputStyle;
      set => base.InputStyle = value;
    }

    [DefaultValue(QComboBoxStyle.DropDown)]
    [Category("QAppearance")]
    [Description("Gets or sets the drop down style of the QComboBox")]
    public virtual QComboBoxStyle DropDownStyle
    {
      get
      {
        try
        {
          return (QComboBoxStyle) base.InputStyle;
        }
        catch
        {
          return QComboBoxStyle.DropDown;
        }
      }
      set => base.InputStyle = (QInputBoxStyle) value;
    }

    protected override int GetRequestedCount() => 18;
  }
}
