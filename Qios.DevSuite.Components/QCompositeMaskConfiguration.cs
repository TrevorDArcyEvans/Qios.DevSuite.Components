// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMaskConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeMaskConfiguration : QCompositeImageConfiguration
  {
    protected const int PropMask = 15;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 16;
    [QCloneBehavior(QCloneBehaviorType.ByReference)]
    private Image m_oDefaultMask;

    private QCompositeMaskConfiguration()
    {
    }

    public QCompositeMaskConfiguration(Image defaultMask)
    {
      this.m_oDefaultMask = defaultMask;
      this.Properties.DefineProperty(15, (object) null);
    }

    protected override int GetRequestedCount() => 16;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Image DefaultMask
    {
      get => this.m_oDefaultMask;
      set => this.m_oDefaultMask = value;
    }

    [Browsable(false)]
    public virtual Image UsedMask => this.Mask != null ? this.Mask : this.DefaultMask;

    [Description("Contains the base image that is used. In the Mask the Color Red is replaced by the TextColor.")]
    [QPropertyIndex(15)]
    [Category("QAppearance")]
    public Image Mask
    {
      get => this.Properties.GetProperty(15) as Image;
      set => this.Properties.SetProperty(15, (object) value);
    }
  }
}
