// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonInputBox
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QRibbonInputBox.bmp")]
  public class QRibbonInputBox : QInputBox
  {
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonInputBox)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    protected internal override string BackColorPropertyName => "RibbonInputBoxBackground";

    protected internal override string OuterBackColor1PropertyName => "RibbonInputBoxOuterBackground1";

    protected internal override string OuterBackColor2PropertyName => "RibbonInputBoxOuterBackground2";
  }
}
