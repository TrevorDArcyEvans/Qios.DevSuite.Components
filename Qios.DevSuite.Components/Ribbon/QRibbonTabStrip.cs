// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonTabStrip
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(false)]
  public class QRibbonTabStrip : QTabStrip
  {
    internal QRibbonTabStrip(IQTabStripHost tabStripHost, Font font, DockStyle dockStyle)
      : base(tabStripHost, font, dockStyle)
    {
    }

    protected override QTabStripConfiguration CreateConfiguration(Font font) => (QTabStripConfiguration) new QRibbonTabStripConfiguration(font);

    protected override QTabStripPainter CreatePainter() => (QTabStripPainter) new QRibbonTabStripPainter();

    protected override QTabStripNavigationArea CreateNavigationArea() => (QTabStripNavigationArea) new QRibbonTabStripNavigationArea(this);

    public QRibbonTabStripConfiguration Configuration => base.Configuration as QRibbonTabStripConfiguration;

    public QRibbonTabStripNavigationArea NavigationArea => base.NavigationArea as QRibbonTabStripNavigationArea;

    public QRibbon Ribbon => this.Parent as QRibbon;

    public void UpdateMdiVisbilityState()
    {
      QRibbonTabStripNavigationArea navigationArea = this.NavigationArea;
      QRibbonTabStripConfiguration configuration = this.Configuration;
      bool flag = this.Ribbon != null && this.Ribbon.MdiButtonsShouldBeVisible;
      if (navigationArea == null || configuration == null)
        return;
      navigationArea.MdiClose.Visible = configuration.MdiCloseButtonVisible && flag;
      navigationArea.MdiRestore.Visible = configuration.MdiRestoreButtonVisible && flag;
      navigationArea.MdiMinimize.Visible = configuration.MdiMinimizeButtonVisible && flag;
    }

    protected internal override void UpdatePropertiesFromConfiguration()
    {
      QRibbonTabStripNavigationArea navigationArea = this.NavigationArea;
      QRibbonTabStripConfiguration configuration = this.Configuration;
      if (navigationArea != null && configuration != null)
      {
        this.UpdateMdiVisbilityState();
        navigationArea.Help.Visible = configuration.HelpButtonVisible;
        navigationArea.MdiClose.AdditionalData = (object) configuration.UsedMdiCloseMask;
        navigationArea.MdiRestore.AdditionalData = (object) configuration.UsedMdiRestoreMask;
        navigationArea.MdiMinimize.AdditionalData = (object) configuration.UsedMdiMinimizeMask;
        navigationArea.Help.AdditionalData = (object) configuration.UsedHelpMask;
      }
      base.UpdatePropertiesFromConfiguration();
    }
  }
}
