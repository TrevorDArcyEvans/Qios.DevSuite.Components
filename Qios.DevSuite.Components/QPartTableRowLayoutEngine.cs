// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartTableRowLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartTableRowLayoutEngine : QPartLinearLayoutEngine
  {
    private static IQPartLayoutEngine m_oDefault;

    public static IQPartLayoutEngine Default
    {
      get
      {
        if (QPartTableRowLayoutEngine.m_oDefault == null)
          QPartTableRowLayoutEngine.m_oDefault = (IQPartLayoutEngine) new QPartTableRowLayoutEngine();
        return QPartTableRowLayoutEngine.m_oDefault;
      }
    }

    protected override QPartDirection GetPartDirection(IQPart part)
    {
      if (part.ParentPart == null || !(part.ParentPart.LayoutEngine is QPartTableLayoutEngine))
        return base.GetPartDirection(part);
      return part.ParentPart.Properties.GetDirection(part.ParentPart) != QPartDirection.Horizontal ? QPartDirection.Horizontal : QPartDirection.Vertical;
    }

    private void SetPartVisibilityOverride(IQPartCollection collection, bool value)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
        collection[index].CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.OverriddenVisible, value);
    }

    public override void ApplyConstraints(
      Size size,
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      this.SetPartVisibilityOverride((IQPartCollection) part.Parts, true);
      base.ApplyConstraints(size, part, layoutContext, properties);
      this.SetPartVisibilityOverride((IQPartCollection) part.Parts, false);
    }

    public override void LayoutPart(
      Rectangle rectangle,
      IQPart part,
      QPartLayoutContext layoutContext)
    {
      this.SetPartVisibilityOverride((IQPartCollection) part.Parts, true);
      base.LayoutPart(rectangle, part, layoutContext);
      this.SetPartVisibilityOverride((IQPartCollection) part.Parts, false);
    }
  }
}
