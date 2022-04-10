// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartDefaultPaintEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartDefaultPaintEngine : IQPartPaintEngine
  {
    private static QPartDefaultPaintEngine m_oDefault;

    public static QPartDefaultPaintEngine Default
    {
      get
      {
        if (QPartDefaultPaintEngine.m_oDefault == null)
          QPartDefaultPaintEngine.m_oDefault = new QPartDefaultPaintEngine();
        return QPartDefaultPaintEngine.m_oDefault;
      }
    }

    public void PerformPaint(IQPart part, QPartPaintContext paintContext)
    {
      part.PaintEngine.PaintPart(part, paintContext);
      part.PaintEngine.FinishPaint(part, paintContext);
    }

    public void FinishPaint(IQPart part, QPartPaintContext paintContext)
    {
      part.CalculatedProperties.ClearCachedPaintProperties();
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.PaintFinished, paintContext);
      if (!(part.ContentObject is IQPartCollection contentObject))
        return;
      for (int index = 0; index < contentObject.Count; ++index)
      {
        IQPart part1 = contentObject[index];
        part1.PaintEngine.FinishPaint(part1, paintContext);
      }
    }

    public void PaintPart(IQPart part, QPartPaintContext paintContext)
    {
      part.CalculatedProperties.ClearCachedPaintProperties();
      if (paintContext.CurrentLevel >= paintContext.RecursiveLevels && paintContext.RecursiveLevels >= 0 || !paintContext.Graphics.ClipBounds.IntersectsWith((RectangleF) QPartHelper.GetPartPaintBounds(part, false)) || !part.IsVisible(QPartVisibilitySelectionTypes.IncludeAll))
        return;
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.PaintingBackground, paintContext);
      this.PaintBackground(part, paintContext);
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.BackgroundPainted, paintContext);
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.PaintingContent, paintContext);
      this.PaintContent(part, paintContext);
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.ContentPainted, paintContext);
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.PaintingForeground, paintContext);
      this.PaintForeground(part, paintContext);
      QPartHelper.RaiseHandlePaintStage(part, QPartPaintStage.ForegroundPainted, paintContext);
    }

    protected void PaintBackground(IQPart part, QPartPaintContext paintContext) => this.PaintLayer(part, QPartPaintLayer.Background, paintContext);

    protected void PaintContent(IQPart part, QPartPaintContext paintContext)
    {
      this.PaintLayer(part, QPartPaintLayer.Content, paintContext);
      if (!(part.ContentObject is IQPartCollection contentObject))
        return;
      int num1 = paintContext.Reverse ? contentObject.Count - 1 : 0;
      int num2 = paintContext.Reverse ? -1 : contentObject.Count;
      int num3 = paintContext.Reverse ? -1 : 1;
      int num4 = paintContext.CurrentLevel + 1;
      for (int index = num1; index != num2; index += num3)
      {
        paintContext.CurrentLevel = num4;
        IQPart part1 = contentObject[index];
        part1.PaintEngine.PaintPart(part1, paintContext);
      }
    }

    protected void PaintForeground(IQPart part, QPartPaintContext paintContext) => this.PaintLayer(part, QPartPaintLayer.Foreground, paintContext);

    protected void PaintLayer(IQPart part, QPartPaintLayer layer, QPartPaintContext paintContext)
    {
      if (part == null)
        return;
      for (IQPartObjectPainter qpartObjectPainter = part.GetObjectPainter(layer, (Type) null); qpartObjectPainter != null; qpartObjectPainter = qpartObjectPainter.NextPainter)
        qpartObjectPainter.PaintObject(part, paintContext);
    }
  }
}
