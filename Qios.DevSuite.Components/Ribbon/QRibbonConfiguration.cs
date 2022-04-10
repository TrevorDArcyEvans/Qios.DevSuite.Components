// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonConfiguration : QTabControlConfiguration
  {
    protected const int PropHotkeyWindowConfiguration = 3;
    protected const int PropHotkeyWindowShowBehavior = 4;
    protected new const int CurrentPropertyCount = 2;
    protected new const int TotalPropertyCount = 5;

    public QRibbonConfiguration()
    {
      this.Properties.DefineProperty(0, (object) new QMargin(0, -1, 0, 0));
      this.Properties.DefineResettableProperty(3, (IQResettableValue) new QHotkeyWindowConfiguration());
      this.Properties.DefineProperty(4, (object) QHotkeyWindowShowBehavior.Automatic);
    }

    protected override int GetRequestedCount() => 5;

    protected override QTabControlContentAppearance CreateContentApperearance() => (QTabControlContentAppearance) new QRibbonContentAppearance();

    [QPropertyIndex(4)]
    [Description("Gets or sets the show behavior of the QHotkeyWindow.")]
    [Category("QAppearance")]
    public QHotkeyWindowShowBehavior HotkeyWindowShowBehavior
    {
      get => (QHotkeyWindowShowBehavior) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [QPropertyIndex(3)]
    [Description("Gets or sets the configuration for the HotkeyWindow. This window is displayed when ALT is pressed")]
    public QHotkeyWindowConfiguration HotkeyWindowConfiguration => this.Properties.GetProperty(3) as QHotkeyWindowConfiguration;
  }
}
