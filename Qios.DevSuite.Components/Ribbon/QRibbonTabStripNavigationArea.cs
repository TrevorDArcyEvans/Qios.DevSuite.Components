// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonTabStripNavigationArea
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(false)]
  public class QRibbonTabStripNavigationArea : QTabStripNavigationArea
  {
    private const int HelpIndex = 3;
    private const int MdiMinimizeIndex = 4;
    private const int MdiRestoreIndex = 5;
    private const int MdiCloseIndex = 6;
    protected new const int DefaultTotalButtonCount = 7;

    public QRibbonTabStripNavigationArea(QRibbonTabStrip ribbonTabStrip)
      : base((QTabStrip) ribbonTabStrip)
    {
    }

    public QButtonArea Help => this.ButtonAreas[3];

    public QButtonArea MdiClose => this.ButtonAreas[6];

    public QButtonArea MdiRestore => this.ButtonAreas[5];

    public QButtonArea MdiMinimize => this.ButtonAreas[4];

    protected override int RequestedButtonCount => 7;
  }
}
