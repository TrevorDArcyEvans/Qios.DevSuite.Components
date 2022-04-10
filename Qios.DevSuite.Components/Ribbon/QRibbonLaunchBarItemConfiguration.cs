// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarItemConfiguration : QCompositeItemConfiguration
  {
    protected const int PropMask = 23;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 24;
    private Image m_oDefaultMask;

    protected QRibbonLaunchBarItemConfiguration()
    {
    }

    public QRibbonLaunchBarItemConfiguration(Image defaultMask)
    {
      this.m_oDefaultMask = defaultMask;
      this.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.Properties.DefineProperty(1, (object) new QPadding(2, 6, 6, 3));
      this.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(23, (object) null);
    }

    protected override int GetRequestedCount() => 24;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonItemAppearance(QRibbonItemConfigurationType.RibbonLaunchBar);

    [Description("Contains the base image that is used to for the button. In the Mask the Color Red is replaced by the MaskColor.")]
    [Category("QAppearance")]
    [QPropertyIndex(23)]
    public Image ButtonMask
    {
      get => this.Properties.GetProperty(23) as Image;
      set => this.Properties.SetProperty(23, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedButtonMask => this.ButtonMask != null ? this.ButtonMask : this.m_oDefaultMask;
  }
}
