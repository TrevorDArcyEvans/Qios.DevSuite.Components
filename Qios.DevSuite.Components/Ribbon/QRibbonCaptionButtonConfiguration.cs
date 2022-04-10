// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionButtonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCaptionButtonConfiguration : QCompositeItemConfiguration
  {
    protected const int PropMask = 23;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 24;
    private Image m_oDefaultMask;

    public QRibbonCaptionButtonConfiguration(Image defaultMask)
    {
      this.m_oDefaultMask = defaultMask;
      this.Properties.DefineProperty(17, (object) QTristateBool.True);
      this.Properties.DefineProperty(18, (object) QTristateBool.True);
      this.Properties.DefineProperty(1, (object) new QPadding(4, 4, 4, 4));
      this.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
      this.Properties.DefineProperty(23, (object) null);
    }

    protected override int GetRequestedCount() => 24;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonCaptionButtonAppearance();

    [Category("QAppearance")]
    [QPropertyIndex(23)]
    [Description("Contains the base image that is used to for the button. In the Mask the Color Red is replaced by the MaskColor.")]
    public Image ButtonMask
    {
      get => this.Properties.GetProperty(23) as Image;
      set => this.Properties.SetProperty(23, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedButtonMask => this.ButtonMask != null ? this.ButtonMask : this.m_oDefaultMask;
  }
}
