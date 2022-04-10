// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarItemPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QToolBarItemPainter : QCommandPainter
  {
    public override bool ShowHasChildItems(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      if (command is QToolItem qtoolItem)
      {
        if (qtoolItem.HasChildItemsImageVisibility == QHasChildItemsImageVisibility.Never)
          return false;
        if (qtoolItem.HasChildItemsImageVisibility == QHasChildItemsImageVisibility.Always)
          return true;
      }
      return base.ShowHasChildItems(command, configuration, destinationControl);
    }
  }
}
