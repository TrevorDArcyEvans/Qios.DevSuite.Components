// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQPart
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public interface IQPart
  {
    string PartName { get; }

    object ContentObject { get; }

    QPartCollection Parts { get; }

    int LayoutOrder { get; }

    void SetLayoutOrder(int layoutOrder);

    IQPart ParentPart { get; }

    QPartCollection ParentCollection { get; }

    void SetParent(
      IQPart parentPart,
      QPartCollection parentCollection,
      bool removeFromCurrentParent,
      bool addToNewParent);

    IQManagedLayoutParent DisplayParent { get; }

    void SetDisplayParent(IQManagedLayoutParent displayParent);

    bool IsSystemPart { get; }

    IQPartLayoutEngine LayoutEngine { get; }

    IQPartPaintEngine PaintEngine { get; }

    IQPartLayoutListener LayoutListener { get; }

    IQPartPaintListener PaintListener { get; }

    IQPartSharedProperties Properties { get; }

    QPartCalculatedProperties CalculatedProperties { get; }

    void PushCalculatedProperties();

    void PullCalculatedProperties();

    bool IsVisible(QPartVisibilitySelectionTypes visibilityType);

    QPartHitTestResult HitTest(int x, int y);

    QRegion ContentClipRegion { get; }

    IQPartObjectPainter GetObjectPainter(
      QPartPaintLayer paintLayer,
      Type painterType);

    bool FitsInSelection(params QPartSelectionTypes[] selectionType);
  }
}
