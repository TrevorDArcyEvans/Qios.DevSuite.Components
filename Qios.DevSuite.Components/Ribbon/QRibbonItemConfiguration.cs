// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonItemConfiguration : QCompositeMenuItemConfiguration
  {
    private static Image m_oDefaultDropDownMask;
    private QRibbonItemConfigurationType m_eConfigurationType;

    private QRibbonItemConfiguration()
    {
    }

    public QRibbonItemConfiguration(QRibbonItemConfigurationType configurationType)
    {
      QRibbonItemConfiguration.SecureMask();
      this.m_eConfigurationType = configurationType;
      this.Properties.DefineProperty(30, (object) QCompositeMenuItemCheckBehaviour.CheckItem);
      if (this.m_eConfigurationType == QRibbonItemConfigurationType.Default)
      {
        this.Properties.DefineProperty(1, (object) new QPadding(4, 3, 3, 4));
        this.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      }
      else if (this.m_eConfigurationType == QRibbonItemConfigurationType.RibbonItemBar)
      {
        this.Properties.DefineProperty(1, (object) new QPadding(2, 3, 3, 2));
        this.Properties.DefineProperty(0, (object) new QMargin(-2, 0, 0, 0));
        this.Properties.DefineProperty(8, (object) new Size(18, 18));
      }
      else if (this.m_eConfigurationType == QRibbonItemConfigurationType.RibbonLaunchBar)
      {
        this.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.Properties.DefineProperty(1, (object) new QPadding(1, 2, 2, 1));
        this.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
        this.Properties.DefineProperty(8, (object) new Size(18, 18));
        this.TitleConfiguration.Properties.DefineProperty(10, (object) QTristateBool.False);
      }
      this.SuspendChangeNotification();
      this.IconConfiguration.Properties.DefineProperty(15, (object) new Size(16, 16));
      this.IconConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.IconConfiguration.Properties.DefineProperty(1, (object) new QPadding(2, 0, 0, 2));
      this.TitleConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.ShortcutConfiguration.Properties.DefineProperty(10, (object) QTristateBool.False);
      this.DropDownButtonConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.DropDownButtonConfiguration.Properties.DefineProperty(1, (object) new QPadding(0, 2, 2, 0));
      this.DropDownSplitConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.DropDownSplitConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.DropDownSplitConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.ResumeChangeNotification(false);
    }

    [Browsable(false)]
    public static Image DefaultDropDownMask
    {
      get
      {
        QRibbonItemConfiguration.SecureMask();
        return QRibbonItemConfiguration.m_oDefaultDropDownMask;
      }
    }

    private static void SecureMask()
    {
      if (QRibbonItemConfiguration.m_oDefaultDropDownMask != null)
        return;
      QRibbonItemConfiguration.m_oDefaultDropDownMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.VerySmallArrowDownMask.png"));
    }

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonItemAppearance(this.m_eConfigurationType);

    protected override QCompositeMaskConfiguration CreateDropDownConfiguration()
    {
      QRibbonItemConfiguration.SecureMask();
      return new QCompositeMaskConfiguration(QRibbonItemConfiguration.m_oDefaultDropDownMask);
    }
  }
}
