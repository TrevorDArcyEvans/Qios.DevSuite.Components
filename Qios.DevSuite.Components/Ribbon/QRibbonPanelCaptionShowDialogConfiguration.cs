// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonPanelCaptionShowDialogConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonPanelCaptionShowDialogConfiguration : QCompositeItemConfiguration
  {
    protected const int PropShowDialogMask = 23;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 24;
    private static Image m_oDefaultMask;
    private EventHandler m_oChildObjectChangedHandler;

    public QRibbonPanelCaptionShowDialogConfiguration()
    {
      if (QRibbonPanelCaptionShowDialogConfiguration.m_oDefaultMask == null)
        QRibbonPanelCaptionShowDialogConfiguration.m_oDefaultMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonPanelShowDialogMask.png"));
      this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
      this.Properties.DefineProperty(23, (object) null);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Far);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.Properties.DefineProperty(17, (object) QTristateBool.True);
      this.Properties.DefineProperty(18, (object) QTristateBool.True);
      this.Properties.DefineProperty(1, (object) new QPadding(0, 1, 0, 0));
      this.Properties.DefineProperty(0, (object) new QMargin(1, 0, 1, 1));
    }

    protected override int GetRequestedCount() => 24;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonShowDialogButtonAppearance();

    [Browsable(false)]
    public virtual Image UsedShowDialogMask => this.ShowDialogMask != null ? this.ShowDialogMask : QRibbonPanelCaptionShowDialogConfiguration.m_oDefaultMask;

    [Category("QAppearance")]
    [QPropertyIndex(23)]
    [Description("Contains the base image that is used to for the ShowDialog button. In the Mask the Color Red is replaced by the TextColor.")]
    public Image ShowDialogMask
    {
      get => this.Properties.GetProperty(23) as Image;
      set => this.Properties.SetProperty(23, (object) value);
    }

    private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
