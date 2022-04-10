// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  internal class QPartHelper
  {
    internal static Rectangle GetPartPaintBounds(IQPart part, bool addScrollOffset)
    {
      Rectangle rectangle = part.CalculatedProperties.OuterBounds;
      foreach (IQMargin margin in part.Properties.GetMargins(part))
        rectangle = margin.InflateNegativeMargins(rectangle);
      if (addScrollOffset)
        rectangle.Offset(part.CalculatedProperties.CachedScrollOffset);
      return rectangle;
    }

    internal static void ScrollIntoView(IQPart part, QTristateBool useAnimation)
    {
      IQManagedLayoutParent displayParent = part.DisplayParent;
      for (IQPart qpart = part.ParentPart; qpart != null; qpart = qpart != displayParent ? qpart.ParentPart : (IQPart) null)
      {
        part.CalculatedProperties.ClearCachedPaintProperties();
        qpart.CalculatedProperties.ClearCachedPaintProperties();
        if (qpart is IQScrollablePart qscrollablePart && qscrollablePart.ScrollData != null)
        {
          if (useAnimation == QTristateBool.Undefined)
            qscrollablePart.ScrollData.ScrollIntoView(part);
          else
            qscrollablePart.ScrollData.ScrollIntoView(part, useAnimation == QTristateBool.True);
        }
      }
    }

    internal static bool IsSystemPart(IQPart part) => part != null && part.IsSystemPart;

    internal static Region CreatePossibleContentClipRegion(
      IQPart part,
      GraphicsPath lastDrawnShape)
    {
      Region region = (Region) null;
      if (lastDrawnShape != null)
        region = new Region(lastDrawnShape);
      if (part is IQScrollablePart qscrollablePart && qscrollablePart.ScrollData != null && qscrollablePart.ScrollData.HasVisibleScrollableAreas)
      {
        if (region != null)
          region.Intersect(part.CalculatedProperties.InnerBounds);
        else
          region = new Region(part.CalculatedProperties.InnerBounds);
      }
      if (region != null)
      {
        QPartHelper.AdjustRegionToScrollOffset(part, region);
        Region parentClipRegion = (Region) part.CalculatedProperties.CachedParentClipRegion;
        if (parentClipRegion != null)
          region.Intersect(parentClipRegion);
      }
      return region;
    }

    internal static void AdjustRegionToScrollOffset(IQPart part, Region region)
    {
      Point cachedScrollOffset = part.CalculatedProperties.CachedScrollOffset;
      if (!(cachedScrollOffset != Point.Empty))
        return;
      region.Translate(cachedScrollOffset.X, cachedScrollOffset.Y);
    }

    internal static Region AdjustPaintRegion(
      IQPart part,
      GraphicsPath lastDrawnShape,
      QPartPaintContext paintContext)
    {
      Region region = (Region) null;
      if (lastDrawnShape != null)
      {
        if (region == null)
          region = paintContext.Graphics.Clip.Clone();
        paintContext.Graphics.SetClip(lastDrawnShape, CombineMode.Intersect);
      }
      if (part is IQScrollablePart qscrollablePart && qscrollablePart.ScrollData != null && qscrollablePart.ScrollData.HasVisibleScrollableAreas)
      {
        if (region == null)
          region = paintContext.Graphics.Clip.Clone();
        paintContext.Graphics.SetClip(part.CalculatedProperties.InnerBounds, CombineMode.Intersect);
      }
      return region;
    }

    internal static void RestorePaintRegion(Region previousRegion, QPartPaintContext paintContext)
    {
      if (previousRegion == null)
        return;
      paintContext.Graphics.SetClip(previousRegion, CombineMode.Replace);
    }

    internal static IQPart GetItemAtPointRecursive(IQPart rootPart, Point point)
    {
      if (rootPart.HitTest(point.X, point.Y) == QPartHitTestResult.Nowhere)
        return (IQPart) null;
      if (rootPart.Parts != null)
      {
        for (int index = 0; index < rootPart.Parts.Count; ++index)
        {
          IQPart atPointRecursive = QPartHelper.GetItemAtPointRecursive(rootPart.Parts[index], point);
          if (atPointRecursive != null)
            return atPointRecursive;
        }
      }
      return rootPart;
    }

    internal static QPartHitTestResult DefaultHitTest(IQPart part, int x, int y)
    {
      if (!part.CalculatedProperties.CachedScrollCorrectedBounds.Contains(x, y))
        return QPartHitTestResult.Nowhere;
      Region parentClipRegion = (Region) part.CalculatedProperties.CachedParentClipRegion;
      if (parentClipRegion == null)
        return QPartHitTestResult.Bounds;
      if (!parentClipRegion.IsVisible((float) x, (float) y))
        return QPartHitTestResult.Nowhere;
      return part is IQScrollablePart qscrollablePart && qscrollablePart.ScrollData != null && qscrollablePart.ScrollData.HitTest(x, y) ? QPartHitTestResult.ScrollArea : QPartHitTestResult.Bounds;
    }

    internal static IQPadding[] GetDefaultPaddings(
      IQPart part,
      QPadding padding,
      QShapeAppearance appearance)
    {
      int length = 1;
      bool flag1 = false;
      bool flag2 = false;
      var qscrollablePart = part as IQScrollablePart;
      if (qscrollablePart != null && qscrollablePart.ScrollData != null && qscrollablePart.ScrollData.HasVisibleScrollableAreas)
      {
        flag1 = true;
        ++length;
      }
      switch (appearance)
      {
        case null:
          IQPadding[] defaultPaddings = new IQPadding[length];
          int num1 = 0;
          IQPadding[] qpaddingArray1 = defaultPaddings;
          int index1 = num1;
          int num2 = index1 + 1;
          // ISSUE: variable of a boxed type
          __Boxed<QPadding> local = (ValueType) padding;
          qpaddingArray1[index1] = (IQPadding) local;
          if (flag2)
            defaultPaddings[num2++] = (IQPadding) appearance.Shape;
          if (flag1)
          {
            IQPadding[] qpaddingArray2 = defaultPaddings;
            int index2 = num2;
            int num3 = index2 + 1;
            QScrollablePartData scrollData = qscrollablePart.ScrollData;
            qpaddingArray2[index2] = (IQPadding) scrollData;
          }
          return defaultPaddings;
        default:
          flag2 = true;
          ++length;
          goto case null;
      }
    }

    internal static void ClearLayoutFlagsRecursive(IQPart rootPart)
    {
      if (rootPart == null)
        return;
      rootPart.CalculatedProperties.LayoutFlags = QPartLayoutFlags.None;
      if (rootPart.Parts == null)
        return;
      for (int index = 0; index < rootPart.Parts.Count; ++index)
        QPartHelper.ClearLayoutFlagsRecursive(rootPart.Parts[index]);
    }

    internal static Size GetCustomContentSize(
      IQPart part,
      object contentObject,
      QPartLayoutContext layoutContext)
    {
      switch (contentObject)
      {
        case null:
          return Size.Empty;
        case Size customContentSize:
          return customContentSize;
        case Rectangle rectangle:
          return rectangle.Size;
        case Image _:
          return (contentObject as Image).Size;
        case Icon _:
          return (contentObject as Icon).Size;
        case string _:
          string text = contentObject as string;
          Size size = Size.Ceiling(layoutContext.Graphics.MeasureString(text, layoutContext.Font, PointF.Empty, layoutContext.StringFormat));
          return part.Properties is QCompositeTextConfiguration properties && properties.Orientation != QContentOrientation.Horizontal ? new Size(size.Height, size.Width) : size;
        case IQPartSizedContent _:
          IQPartSizedContent qpartSizedContent = contentObject as IQPartSizedContent;
          qpartSizedContent.CalculateSize(layoutContext);
          return qpartSizedContent.Size;
        default:
          throw new InvalidOperationException(QResources.GetException("QPartHelper_UnknownContent", (object) contentObject.ToString()));
      }
    }

    internal static Rectangle GetPartsBounds(IQPartCollection collection)
    {
      Rectangle partsBounds = Rectangle.Empty;
      if (collection != null && collection.Count > 0)
      {
        partsBounds = collection[0].CalculatedProperties.OuterBounds;
        for (int index = 1; index < collection.Count; ++index)
        {
          partsBounds.X = Math.Min(partsBounds.Left, collection[index].CalculatedProperties.OuterBounds.Left);
          partsBounds.Y = Math.Min(partsBounds.Top, collection[index].CalculatedProperties.OuterBounds.Top);
          if (partsBounds.Right < collection[index].CalculatedProperties.OuterBounds.Right)
            partsBounds.Width = collection[index].CalculatedProperties.OuterBounds.Right - partsBounds.X;
          if (partsBounds.Bottom < collection[index].CalculatedProperties.OuterBounds.Bottom)
            partsBounds.Height = collection[index].CalculatedProperties.OuterBounds.Bottom - partsBounds.Y;
        }
      }
      return partsBounds;
    }

    internal static int GetCountInSelection(
      IQPartCollection collection,
      QPartVisibilitySelectionTypes visibilitySelection,
      params QPartSelectionTypes[] selection)
    {
      int countInSelection = 0;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].IsVisible(visibilitySelection) && collection[index].FitsInSelection(selection))
          ++countInSelection;
      }
      return countInSelection;
    }

    internal static QPartArray GetPartsInSelection(
      IQPartCollection collection,
      QPartVisibilitySelectionTypes visibilitySelection,
      object additionalProperties,
      params QPartSelectionTypes[] selection)
    {
      int countInSelection = QPartHelper.GetCountInSelection(collection, visibilitySelection, selection);
      QPartArray partsInSelection = new QPartArray(countInSelection);
      if (countInSelection > 0)
      {
        int index1 = 0;
        for (int index2 = 0; index2 < collection.Count; ++index2)
        {
          if (collection[index2].IsVisible(visibilitySelection) && collection[index2].FitsInSelection(selection))
            partsInSelection[index1] = collection[index2];
        }
      }
      return partsInSelection;
    }

    internal static Size GetCalculatedSize(
      IQPartCollection collection,
      QPartVisibilitySelectionTypes visibilitySelection,
      QPartCalculateSizedReturnOptions returnOptions,
      params QPartSelectionTypes[] selection)
    {
      bool flag1 = (returnOptions & QPartCalculateSizedReturnOptions.SumWidth) == QPartCalculateSizedReturnOptions.SumWidth;
      bool flag2 = (returnOptions & QPartCalculateSizedReturnOptions.SumHeight) == QPartCalculateSizedReturnOptions.SumHeight;
      bool flag3 = (returnOptions & QPartCalculateSizedReturnOptions.MaximumWidth) == QPartCalculateSizedReturnOptions.MaximumWidth;
      bool flag4 = (returnOptions & QPartCalculateSizedReturnOptions.MaximumHeight) == QPartCalculateSizedReturnOptions.MaximumHeight;
      Size empty = Size.Empty;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].IsVisible(visibilitySelection) && collection[index].FitsInSelection(selection))
        {
          Size requestedSize = QPartHelper.GetRequestedSize(collection[index], returnOptions);
          if (flag1)
            empty.Width += requestedSize.Width;
          else if (flag3)
            empty.Width = Math.Max(empty.Width, requestedSize.Width);
          if (flag2)
            empty.Height += requestedSize.Height;
          else if (flag4)
            empty.Height = Math.Max(empty.Height, requestedSize.Height);
        }
      }
      return empty;
    }

    private static Size GetRequestedSize(
      IQPart part,
      QPartCalculateSizedReturnOptions returnOptions)
    {
      bool flag1 = (returnOptions & QPartCalculateSizedReturnOptions.InnerSize) == QPartCalculateSizedReturnOptions.InnerSize;
      bool flag2 = (returnOptions & QPartCalculateSizedReturnOptions.OuterSize) == QPartCalculateSizedReturnOptions.OuterSize;
      bool flag3 = (returnOptions & QPartCalculateSizedReturnOptions.MinimumSize) == QPartCalculateSizedReturnOptions.MinimumSize;
      bool flag4 = (returnOptions & QPartCalculateSizedReturnOptions.MaximumSize) == QPartCalculateSizedReturnOptions.MaximumSize;
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      if (flag3)
        return flag1 || flag2 ? calculatedProperties.GetMinimumOuterSize() : calculatedProperties.AppliedMinimumSize;
      if (flag4)
      {
        if (flag1)
          return calculatedProperties.GetMaximumInnerSize();
        return flag2 ? calculatedProperties.GetMaximumOuterSize() : calculatedProperties.AppliedMaximumSize;
      }
      if (flag1)
        return calculatedProperties.InnerSize;
      return flag2 ? calculatedProperties.OuterSize : calculatedProperties.Size;
    }

    internal static Rectangle AlignPartSize(
      Size size,
      QPartAlignment alignment,
      Rectangle inBounds,
      QPartDirection direction,
      bool reverse,
      bool allowOverflow)
    {
      if (direction == QPartDirection.Horizontal)
        return QPartHelper.AlignPartSize(size.Width, alignment, inBounds.Left, inBounds.Right, reverse, allowOverflow).AdjustRectangle(inBounds, true);
      return direction == QPartDirection.Vertical ? QPartHelper.AlignPartSize(size.Height, alignment, inBounds.Top, inBounds.Bottom, reverse, allowOverflow).AdjustRectangle(inBounds, false) : Rectangle.Empty;
    }

    internal static QRange AlignPartSize(
      int size,
      QPartAlignment alignment,
      int start,
      int end,
      bool reverse,
      bool allowOverflow)
    {
      QRange qrange = QRange.Empty;
      if (alignment == QPartAlignment.Near || alignment == QPartAlignment.Far && reverse)
        qrange = new QRange(start, size);
      else if (alignment == QPartAlignment.Far || alignment == QPartAlignment.Near && reverse)
        qrange = new QRange(end - size, size);
      else if (alignment == QPartAlignment.Centered)
        qrange = new QRange(QMath.GetStartForCenter(start, end, size), size);
      if (!allowOverflow)
      {
        qrange.Start = Math.Max(start, qrange.Start);
        qrange.End = Math.Min(end, qrange.End);
      }
      return qrange;
    }

    internal static bool IsFirstVisiblePart(
      IQPartCollection collection,
      QPartVisibilitySelectionTypes visibilitySelection,
      IQPart part)
    {
      int partIndex = QPartHelper.GetPartIndex(collection, part);
      for (int index = 0; index < partIndex; ++index)
      {
        if (collection[index].IsVisible(visibilitySelection))
          return false;
      }
      return true;
    }

    internal static bool IsLastVisiblePart(
      IQPartCollection collection,
      QPartVisibilitySelectionTypes visibilitySelection,
      IQPart part)
    {
      int partIndex = QPartHelper.GetPartIndex(collection, part);
      for (int index = collection.Count - 1; index > partIndex; --index)
      {
        if (collection[index].IsVisible(visibilitySelection))
          return false;
      }
      return true;
    }

    internal static int GetContentPartIndex(IQPart part, string name) => QPartHelper.GetPartIndex(part.ContentObject as IQPartCollection, name);

    internal static int GetPartIndex(IQPartCollection collection, string name)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        if (string.Compare(collection[index].PartName, name, true) == 0)
          return index;
      }
      return -1;
    }

    internal static int GetPartIndex(IQPartCollection collection, IQPart part)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index] == part)
          return index;
      }
      return -1;
    }

    internal static IQPart GetContentPart(IQPart part, string name) => QPartHelper.GetPart(part.ContentObject as IQPartCollection, name);

    internal static IQPart GetPart(IQPartCollection collection, string name)
    {
      int partIndex = QPartHelper.GetPartIndex(collection, name);
      return partIndex >= 0 ? collection[partIndex] : (IQPart) null;
    }

    internal static void SortContentPartsWhenRequired(IQPart part)
    {
      if (!(part.ContentObject is QPartCollection contentObject))
        return;
      contentObject.SortPartsWhenRequired();
    }

    internal static void SetContentPartsLayoutOrder(
      IQPartCollection collection,
      string contentPartsString)
    {
      if (collection == null)
        return;
      switch (contentPartsString)
      {
        case "":
          break;
        case null:
          break;
        default:
          string[] strArray = contentPartsString.Split(',');
          if (strArray.Length != collection.Count)
            throw new InvalidOperationException(QResources.GetException("QPartHelper_ContentPartOrderInvalidCount"));
          for (int layoutOrder = 0; layoutOrder < strArray.Length; ++layoutOrder)
          {
            string name = strArray[layoutOrder].Trim();
            (QPartHelper.GetPart(collection, name) ?? throw new InvalidOperationException(QResources.GetException("QPartHelper_ContentPartOrderPartNotFound", (object) name))).SetLayoutOrder(layoutOrder);
          }
          break;
      }
    }

    internal static void RaiseHandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part.LayoutListener == null)
        return;
      part.LayoutListener.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
    }

    internal static void RaiseHandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      if (part.PaintListener == null)
        return;
      part.PaintListener.HandlePaintStage(part, paintStage, paintContext);
    }

    internal static bool IsVisible(
      IQPart part,
      bool visibleProperty,
      bool hiddenBecauseOfConstraintsProperty,
      QPartVisibilitySelectionTypes visibilityType)
    {
      return part.CalculatedProperties.HasLayoutFlag(QPartLayoutFlags.OverriddenVisible) || visibilityType == QPartVisibilitySelectionTypes.IncludeNone || ((visibilityType & QPartVisibilitySelectionTypes.IncludeConfiguration) != QPartVisibilitySelectionTypes.IncludeConfiguration || part.Properties.GetVisible(part) != QTristateBool.False) && ((visibilityType & QPartVisibilitySelectionTypes.IncludeVisible) != QPartVisibilitySelectionTypes.IncludeVisible || visibleProperty) && ((visibilityType & QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints) != QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints || !hiddenBecauseOfConstraintsProperty);
    }

    internal static bool IsPartHierarchyVisible(
      IQPart part,
      QPartVisibilitySelectionTypes visibilitySelection)
    {
      for (; part != null; part = part.ParentPart)
      {
        if (!part.IsVisible(visibilitySelection))
          return false;
      }
      return true;
    }

    internal static bool FitsInSelection(IQPart part, params QPartSelectionTypes[] selectionTypes)
    {
      bool flag = true;
      for (int index = 0; index < selectionTypes.Length; ++index)
      {
        if (!QPartHelper.FitsInSelectionInternal(part, selectionTypes[index]))
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    private static bool FitsInSelectionInternal(IQPart part, QPartSelectionTypes selectionTypes)
    {
      if (selectionTypes == QPartSelectionTypes.All)
        return true;
      IQPartSharedProperties properties = part.Properties;
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      return (selectionTypes & QPartSelectionTypes.HorizontalNearAligned) == QPartSelectionTypes.HorizontalNearAligned && properties.GetAlignmentHorizontal(part) == QPartAlignment.Near || (selectionTypes & QPartSelectionTypes.HorizontalCenterAligned) == QPartSelectionTypes.HorizontalCenterAligned && properties.GetAlignmentHorizontal(part) == QPartAlignment.Centered || (selectionTypes & QPartSelectionTypes.HorizontalFarAligned) == QPartSelectionTypes.HorizontalFarAligned && properties.GetAlignmentHorizontal(part) == QPartAlignment.Far || (selectionTypes & QPartSelectionTypes.VerticalNearAligned) == QPartSelectionTypes.VerticalNearAligned && properties.GetAlignmentVertical(part) == QPartAlignment.Near || (selectionTypes & QPartSelectionTypes.VerticalCenterAligned) == QPartSelectionTypes.VerticalCenterAligned && properties.GetAlignmentVertical(part) == QPartAlignment.Centered || (selectionTypes & QPartSelectionTypes.VerticalFarAligned) == QPartSelectionTypes.VerticalFarAligned && properties.GetAlignmentVertical(part) == QPartAlignment.Far || (selectionTypes & QPartSelectionTypes.StretchHorizontal) == QPartSelectionTypes.StretchHorizontal && calculatedProperties.ShouldStretchHorizontal || (selectionTypes & QPartSelectionTypes.StretchVertical) == QPartSelectionTypes.StretchVertical && calculatedProperties.ShouldStretchVertical || (selectionTypes & QPartSelectionTypes.NotStretchHorizontal) == QPartSelectionTypes.NotStretchHorizontal && !calculatedProperties.ShouldStretchHorizontal || (selectionTypes & QPartSelectionTypes.NotStretchVertical) == QPartSelectionTypes.NotStretchVertical && !calculatedProperties.ShouldStretchVertical || (selectionTypes & QPartSelectionTypes.ShrinkHorizontal) == QPartSelectionTypes.ShrinkHorizontal && calculatedProperties.ShouldShrinkHorizontal || (selectionTypes & QPartSelectionTypes.ShrinkVertical) == QPartSelectionTypes.ShrinkVertical && calculatedProperties.ShouldShrinkVertical || (selectionTypes & QPartSelectionTypes.NotShrinkHorizontal) == QPartSelectionTypes.NotShrinkHorizontal && !calculatedProperties.ShouldShrinkHorizontal || (selectionTypes & QPartSelectionTypes.NotShrinkVertical) == QPartSelectionTypes.NotShrinkVertical && !calculatedProperties.ShouldShrinkVertical || (selectionTypes & QPartSelectionTypes.SetToMinimumWidth) == QPartSelectionTypes.SetToMinimumWidth && calculatedProperties.IsSetToMinimumWidth || (selectionTypes & QPartSelectionTypes.SetToMaximumWidth) == QPartSelectionTypes.SetToMaximumWidth && calculatedProperties.IsSetToMaximumWidth || (selectionTypes & QPartSelectionTypes.SetToMinimumHeight) == QPartSelectionTypes.SetToMinimumHeight && calculatedProperties.IsSetToMinimumHeight || (selectionTypes & QPartSelectionTypes.SetToMaximumHeight) == QPartSelectionTypes.SetToMaximumHeight && calculatedProperties.IsSetToMaximumHeight || (selectionTypes & QPartSelectionTypes.NotSetToMinimumWidth) == QPartSelectionTypes.NotSetToMinimumWidth && !calculatedProperties.IsSetToMinimumWidth || (selectionTypes & QPartSelectionTypes.NotSetToMaximumWidth) == QPartSelectionTypes.NotSetToMaximumWidth && !calculatedProperties.IsSetToMaximumWidth || (selectionTypes & QPartSelectionTypes.NotSetToMinimumHeight) == QPartSelectionTypes.NotSetToMinimumHeight && !calculatedProperties.IsSetToMinimumHeight || (selectionTypes & QPartSelectionTypes.NotSetToMaximumHeight) == QPartSelectionTypes.NotSetToMaximumHeight && !calculatedProperties.IsSetToMaximumHeight;
    }

    internal static void AddAllHotkeyItems(IQPartCollection collection, IList list)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
        QPartHelper.AddAllHotkeyItems(collection[index], list);
    }

    internal static void AddAllHotkeyItems(IQPart part, IList list)
    {
      if (part is IQHotkeyItem qhotkeyItem && qhotkeyItem.HasHotkey && qhotkeyItem.Visible)
        list.Add((object) part);
      QPartHelper.AddAllHotkeyItems(part.ContentObject as IQPartCollection, list);
    }
  }
}
