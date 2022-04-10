// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenuItemForegroundPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QCompositeMenuItemForegroundPainter : QPartShapePainter
  {
    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      QCompositeMenuItem qcompositeMenuItem = part as QCompositeMenuItem;
      object contentObject = part.ContentObject;
      if (qcompositeMenuItem != null && qcompositeMenuItem.Checked && qcompositeMenuItem.Configuration.CheckBehaviour == QCompositeMenuItemCheckBehaviour.CheckIcon)
        QShapePainter.Default.Paint(qcompositeMenuItem.IconPart.CalculatedProperties.Bounds, this.Appearance.Shape, (IQAppearance) this.Appearance, qcompositeMenuItem.GetCheckedColorSet(), this.PropertiesToUse, this.FillerPropertiesToUse, this.Options, paintContext.Graphics);
      base.PaintObject(part, paintContext);
    }
  }
}
