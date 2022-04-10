// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonTabButtonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(false)]
  public class QRibbonTabButtonConfiguration : QTabButtonConfiguration
  {
    protected const int PropHotkeyWindowRelativeOffset = 18;
    protected const int PropHotkeyWindowAlignment = 19;
    protected new const int CurrentPropertyCount = 2;
    protected new const int TotalPropertyCount = 20;

    public QRibbonTabButtonConfiguration()
    {
      this.Properties.DefineProperty(18, (object) new Point(0, 10));
      this.Properties.DefineProperty(19, (object) ContentAlignment.BottomCenter);
    }

    protected override int GetRequestedCount() => 20;

    protected override QTabButtonAppearance CreateAppearance() => (QTabButtonAppearance) new QRibbonTabButtonAppearance();

    protected override QTabButtonAppearance CreateAppearanceHot() => (QTabButtonAppearance) new QRibbonTabButtonAppearance();

    protected override QTabButtonAppearance CreateAppearanceActive() => (QTabButtonAppearance) new QRibbonTabButtonAppearanceActive();

    [QPropertyIndex(18)]
    [Category("QAppearance")]
    [Description("Gets or set the relative offset of the HotkeyWindow. This offset is added after the alignment.")]
    public Point HotkeyWindowRelativeOffset
    {
      get => (Point) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(19)]
    [Description("Gets or set the alignment of the hotkeyWindow.")]
    public ContentAlignment HotkeyWindowAlignment
    {
      get => (ContentAlignment) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Description("Contains the appearance of a regular button.")]
    [Category("QAppearance")]
    [QPropertyIndex(15)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonTabButtonAppearance Appearance => base.Appearance as QRibbonTabButtonAppearance;

    [Description("Gets the appearance of a hot button. For the configuration on the TabStrip, this AppearanceHot inherits its default values from the Appearance property. For the configuration on a QTabButton or QTabPage, this property inherits its default values from the AppeareanceHot property from a parenting QTabStripConfiguration.")]
    [QPropertyIndex(16)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonTabButtonAppearance AppearanceHot => base.AppearanceHot as QRibbonTabButtonAppearance;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    [Description("Gets the appearance of an active button. For the configuration on the TabStrip, this AppearanceActive inherits its default values from the Appearance property. For the configuration on a QTabButton or QTabPage, this property inherits its default values from the AppearanceActive property from a parenting QTabStrpConfiguration.")]
    public QRibbonTabButtonAppearance AppearanceActive => base.AppearanceActive as QRibbonTabButtonAppearance;
  }
}
