// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenuItemBackgroundPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QCompositeMenuItemBackgroundPainter : QPartShapePainter
  {
    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      QCompositeMenuItem qcompositeMenuItem = part as QCompositeMenuItem;
      object contentObject = part.ContentObject;
      if (qcompositeMenuItem == null)
      {
        base.PaintObject(part, paintContext);
      }
      else
      {
        if (!qcompositeMenuItem.DropDownSplitPart.IsVisible(QPartVisibilitySelectionTypes.IncludeAll) || QItemStatesHelper.IsDisabled(qcompositeMenuItem.ItemState) || !QItemStatesHelper.IsHot(qcompositeMenuItem.ItemState))
        {
          base.PaintObject(part, paintContext);
        }
        else
        {
          Rectangle bounds = part.CalculatedProperties.GetBounds(this.DrawOnBounds);
          float multiplyBrightness = 1.4f;
          QColorSet colorSet = this.ColorSet;
          QColorSet qcolorSet = new QColorSet(QMath.ModifyColor(this.ColorSet.Background1, 1f, multiplyBrightness, 1f), QMath.ModifyColor(this.ColorSet.Background2, 1f, multiplyBrightness, 1f), this.ColorSet.Border, this.ColorSet.Foreground);
          QColorSet colors1 = qcompositeMenuItem.IsDropDownAreaHot ? colorSet : qcolorSet;
          QColorSet colors2 = qcompositeMenuItem.IsDropDownAreaHot ? qcolorSet : colorSet;
          GraphicsPath path = QShapePainter.Default.Paint(bounds, this.Appearance.Shape, (IQAppearance) this.Appearance, colors2, this.PropertiesToUse, this.FillerPropertiesToUse, this.Options, paintContext.Graphics);
          if (path != null)
            this.PutLastDrawnGraphicsPath(path);
          Rectangle dropDownArea = qcompositeMenuItem.DropDownArea;
          Region savedRegion = QControlPaint.AdjustClip(paintContext.Graphics, new Region(dropDownArea), CombineMode.Intersect);
          QShapePainter.Default.Paint(bounds, this.Appearance.Shape, (IQAppearance) this.Appearance, colors1, this.PropertiesToUse, this.FillerPropertiesToUse, this.Options, paintContext.Graphics);
          QControlPaint.RestoreClip(paintContext.Graphics, savedRegion);
        }
        if (qcompositeMenuItem == null || !qcompositeMenuItem.Checked || qcompositeMenuItem.Configuration.CheckBehaviour != QCompositeMenuItemCheckBehaviour.CheckIcon)
          return;
        QShapePainter.Default.Paint(qcompositeMenuItem.IconPart.CalculatedProperties.Bounds, this.Appearance.Shape, (IQAppearance) this.Appearance, qcompositeMenuItem.GetCheckedColorSet(), this.PropertiesToUse, this.FillerPropertiesToUse, this.Options, paintContext.Graphics);
      }
    }
  }
}
