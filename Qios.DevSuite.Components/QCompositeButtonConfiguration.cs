// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeButtonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QCompositeButtonConfiguration : QCompositeMenuItemConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 31;
    private static Image m_oDefaultDropDownMask;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QCompositeButtonConfiguration()
    {
      this.SecureMask();
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(0, (object) new QMargin(2, 2, 2, 2));
      this.Properties.DefineProperty(29, (object) true);
      this.Properties.DefineProperty(21, (object) (QCompositeExpandBehavior.NotAutoExpand | QCompositeExpandBehavior.NotAutoChangeExpand | QCompositeExpandBehavior.PaintExpandedChildWhenHot | QCompositeExpandBehavior.CloseExpandedItemOnClick));
      this.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Down);
      this.SuspendChangeNotification();
      this.TitleConfiguration.Properties.DefineProperty(0, (object) new QMargin(1, 2, 2, 1));
      this.DropDownButtonConfiguration.Properties.DefineProperty(0, (object) new QMargin(1, 0, 0, 1));
      this.ResumeChangeNotification(false);
    }

    private void SecureMask()
    {
      if (QCompositeButtonConfiguration.m_oDefaultDropDownMask != null)
        return;
      QCompositeButtonConfiguration.m_oDefaultDropDownMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.VerySmallArrowDownMask.png"));
    }

    protected override int GetRequestedCount() => 31;

    protected override QCompositeMaskConfiguration CreateDropDownConfiguration()
    {
      this.SecureMask();
      return new QCompositeMaskConfiguration(QCompositeButtonConfiguration.m_oDefaultDropDownMask);
    }

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
