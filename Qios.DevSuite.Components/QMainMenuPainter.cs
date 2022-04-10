// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMainMenuPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QMainMenuPainter : QToolBarPainter
  {
    public QMainMenuPainter() => this.CommandPainter = (QCommandPainter) new QMainMenuItemPainter();

    public QMainMenuItemPainter ItemPainter => (QMainMenuItemPainter) this.CommandPainter;

    public override void LayoutHorizontal(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      base.LayoutHorizontal(rectangle, configuration, paintParams, destinationControl, commands);
    }

    public override void LayoutVertical(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      base.LayoutVertical(rectangle, configuration, paintParams, destinationControl, commands);
    }

    public override void DrawItem(
      QCommand command,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QCommandContainer parentContainer,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
      base.DrawItem(command, configuration, paintParams, parentContainer, flags, graphics);
    }

    public override void DrawSeparator(
      QMenuItem item,
      Color separatorColor,
      Color shadeColor,
      bool addShade,
      Graphics graphics)
    {
      base.DrawSeparator(item, separatorColor, shadeColor, addShade, graphics);
    }

    public override void DrawControlBackgroundHorizontal(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
      base.DrawControlBackgroundHorizontal(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
    }

    public override void DrawControlBackgroundVertical(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
      base.DrawControlBackgroundVertical(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
    }
  }
}
