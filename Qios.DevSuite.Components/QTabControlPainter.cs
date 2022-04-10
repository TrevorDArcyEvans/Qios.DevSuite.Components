// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabControlPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QTabControlPainter
  {
    public virtual void DrawContentShape(
      QTabControl tabControl,
      Rectangle contentShapeBounds,
      QTabControlConfiguration configuration,
      QTabControlPaintParams paintParams,
      Graphics graphics)
    {
      this.DrawContentShade(tabControl, contentShapeBounds, configuration, paintParams, graphics);
      QShape shape = configuration.ContentAppearance.Shape;
      if (tabControl.ActiveTabPageRuntime != null && configuration.ContentAppearance.UseControlBackgroundForShape)
      {
        QTabPage activeTabPageRuntime = tabControl.ActiveTabPageRuntime;
        QPaintBackgroundObjects currentObjects = new QPaintBackgroundObjects();
        currentObjects.BackgroundBounds = ((IQTabButtonSource) activeTabPageRuntime).GetBoundsForBackgroundFill();
        ((IQPaintBackgroundObjectsProvider) activeTabPageRuntime.TabButton).GetBackgroundObjects(currentObjects, (Control) tabControl);
        QShapePainter.Default.FillBackground(contentShapeBounds, shape, (IQAppearance) activeTabPageRuntime.Appearance, new QColorSet(activeTabPageRuntime.BackColor, activeTabPageRuntime.BackColor2), QShapePainterProperties.Default, new QAppearanceFillerProperties()
        {
          AlternativeBoundsForBrushCreation = currentObjects.BackgroundBounds
        }, QPainterOptions.Default, graphics);
        QShapePainter.Default.FillForeground(contentShapeBounds, shape, (IQAppearance) configuration.ContentAppearance, new QColorSet(paintParams.ContentBackground1, paintParams.ContentBackground1, paintParams.ContentBorder), QShapePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      }
      else
        QShapePainter.Default.Paint(contentShapeBounds, shape, (IQAppearance) configuration.ContentAppearance, new QColorSet(paintParams.ContentBackground1, paintParams.ContentBackground2, paintParams.ContentBorder, Color.Empty), QShapePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
    }

    protected virtual void DrawContentShade(
      QTabControl tabControl,
      Rectangle contentShapeBounds,
      QTabControlConfiguration configuration,
      QTabControlPaintParams paintParams,
      Graphics graphics)
    {
      QControlPaint.DrawShapeShade((IQShadedShapeAppearance) configuration.ContentAppearance, contentShapeBounds, DockStyle.None, paintParams.ContentShade, graphics);
    }
  }
}
