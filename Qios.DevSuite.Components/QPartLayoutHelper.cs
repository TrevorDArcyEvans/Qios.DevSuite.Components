// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartLayoutHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartLayoutHelper
  {
    private QPartLayoutHelper()
    {
    }

    public static QPartStretchStorage PrepareForStretchShrink(
      IQPart part,
      IQPartCollection collection,
      Size size,
      QPartDirection direction,
      QPartLayoutContext layoutContext)
    {
      QPartStretchStorage stretchStorage = new QPartStretchStorage();
      stretchStorage.Direction = direction;
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart part1 = collection[index];
        if (part1.IsVisible(layoutContext.VisibilitySelection))
        {
          QPartCalculatedProperties calculatedProperties = part1.CalculatedProperties;
          Size outerSize = calculatedProperties.OuterSize;
          Size unstretchedOuterSize = calculatedProperties.UnstretchedOuterSize;
          switch (direction)
          {
            case QPartDirection.Horizontal:
              if ((calculatedProperties.ShouldShrinkHorizontal || calculatedProperties.ShouldStretchHorizontal) && unstretchedOuterSize.Width != outerSize.Width)
              {
                calculatedProperties.SetSizesBasedOnOuterSize(new Size(unstretchedOuterSize.Width, outerSize.Height), false);
                unstretchedOuterSize = calculatedProperties.UnstretchedOuterSize;
                outerSize = calculatedProperties.OuterSize;
              }
              if (calculatedProperties.ShouldShrinkHorizontal)
              {
                stretchStorage.AddCanShrinkPart(part1);
                stretchStorage.UsedShrinkSize += outerSize.Width;
              }
              else
                stretchStorage.NotShrinkSize += outerSize.Width;
              if (calculatedProperties.ShouldStretchHorizontal)
              {
                stretchStorage.AddCanStretchPart(part1);
                stretchStorage.UsedStretchSize += outerSize.Width;
                continue;
              }
              stretchStorage.NotStretchSize += outerSize.Width;
              continue;
            case QPartDirection.Vertical:
              if ((calculatedProperties.ShouldShrinkVertical || calculatedProperties.ShouldStretchVertical) && unstretchedOuterSize.Height != outerSize.Height)
              {
                calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize.Width, unstretchedOuterSize.Height), false);
                unstretchedOuterSize = calculatedProperties.UnstretchedOuterSize;
                outerSize = calculatedProperties.OuterSize;
              }
              if (calculatedProperties.ShouldShrinkVertical)
              {
                calculatedProperties.GetMinimumOuterSize();
                stretchStorage.AddCanShrinkPart(part1);
                stretchStorage.UsedShrinkSize += outerSize.Height;
              }
              else
                stretchStorage.NotShrinkSize += outerSize.Height;
              if (calculatedProperties.ShouldStretchVertical)
              {
                stretchStorage.AddCanStretchPart(part1);
                stretchStorage.UsedStretchSize += outerSize.Height;
                continue;
              }
              stretchStorage.NotStretchSize += outerSize.Height;
              continue;
            default:
              continue;
          }
        }
      }
      QPartLayoutHelper.AddAdditionalRowSizesToStretchShringStorage(stretchStorage, part, collection, direction);
      switch (direction)
      {
        case QPartDirection.Horizontal:
          stretchStorage.AvailableShrinkSize = size.Width - stretchStorage.NotShrinkSize;
          stretchStorage.AvailableStretchSize = size.Width - stretchStorage.NotStretchSize;
          break;
        case QPartDirection.Vertical:
          stretchStorage.AvailableShrinkSize = size.Height - stretchStorage.NotShrinkSize;
          stretchStorage.AvailableStretchSize = size.Height - stretchStorage.NotStretchSize;
          break;
      }
      if (stretchStorage.ShouldShrink && stretchStorage.CanShrinkCount > 1)
        stretchStorage.SortCanShrinkCollection();
      return stretchStorage;
    }

    public static void SaveSizeBeforeConstraints(IQPartCollection collection)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        QPartCalculatedProperties calculatedProperties = collection[index].CalculatedProperties;
        calculatedProperties.SetEngineStateProperty(0, (object) calculatedProperties.UnstretchedOuterSize);
      }
    }

    public static bool ReapplyConstraintsRequired(
      IQPartCollection collection,
      QPartDirection direction,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      Size empty1 = Size.Empty;
      Size empty2 = Size.Empty;
      bool flag = true;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].IsVisible(layoutContext.VisibilitySelection))
        {
          QPartCalculatedProperties calculatedProperties = collection[index].CalculatedProperties;
          if (!calculatedProperties.HasLayoutFlag(QPartLayoutFlags.WidthConstraintApplied | QPartLayoutFlags.HeightConstraintApplied))
            flag = false;
          Size statePropertyAsValue = (Size) calculatedProperties.GetEngineStatePropertyAsValue(0, (ValueType) Size.Empty);
          Size unstretchedOuterSize = calculatedProperties.UnstretchedOuterSize;
          Size outerSize = calculatedProperties.OuterSize;
          if (statePropertyAsValue != unstretchedOuterSize)
            flag = false;
          switch (direction)
          {
            case QPartDirection.Horizontal:
              empty1.Width += outerSize.Width;
              empty1.Height = Math.Max(outerSize.Height, empty1.Height);
              empty2.Width += unstretchedOuterSize.Width;
              empty2.Height = Math.Max(unstretchedOuterSize.Height, empty2.Height);
              if (statePropertyAsValue.Width != unstretchedOuterSize.Width)
              {
                calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
                continue;
              }
              continue;
            case QPartDirection.Vertical:
              empty1.Height += outerSize.Height;
              empty1.Width = Math.Max(outerSize.Width, empty1.Width);
              empty2.Height += unstretchedOuterSize.Height;
              empty2.Width = Math.Max(unstretchedOuterSize.Width, empty2.Width);
              if (statePropertyAsValue.Height != unstretchedOuterSize.Height)
              {
                calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      if (properties != null)
      {
        properties.NewUnstretchedSize = empty2;
        properties.NewSize = empty1;
      }
      return flag;
    }

    public static void AssertApplyConstraintsLoopCount(ref int debugCount, IQPart part)
    {
      ++debugCount;
      if (debugCount <= 1)
        return;
      string name = part.GetType().Name;
      string partName = part.PartName;
      if (debugCount > 100)
        throw new InvalidOperationException(QResources.GetException("QPartLinearLayoutEngine_ApplyConstraintsToCollectionInfiniteLoop", (object) debugCount, (object) name, (object) partName));
    }

    public static void AddAdditionalRowSizesToStretchShringStorage(
      QPartStretchStorage stretchStorage,
      IQPart row,
      IQPartCollection collection,
      QPartDirection direction)
    {
      QPartCalculatedProperties calculatedProperties = row.CalculatedProperties;
      if (!calculatedProperties.HasLayoutFlag(QPartLayoutFlags.IsTableRow) || !(calculatedProperties.GetEngineStateProperty(1) is QPartTableLayoutEngine.QPartTableLayoutStorage engineStateProperty) || engineStateProperty.CellSizes.Length <= collection.Count)
        return;
      for (int count = collection.Count; count < engineStateProperty.CellSizes.Length; ++count)
      {
        if (direction == QPartDirection.Horizontal)
        {
          stretchStorage.NotStretchSize += engineStateProperty.UnstretchedCellSizes[count].Width;
          stretchStorage.NotShrinkSize += engineStateProperty.UnstretchedCellSizes[count].Width;
        }
        else
        {
          stretchStorage.NotStretchSize += engineStateProperty.UnstretchedCellSizes[count].Height;
          stretchStorage.NotShrinkSize += engineStateProperty.UnstretchedCellSizes[count].Height;
        }
      }
    }

    public static void CalculatePossibleMaxTableRowsAndCells(
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartLayoutStage layoutStage)
    {
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      if (calculatedProperties.HasLayoutFlag(QPartLayoutFlags.IsTableCell))
      {
        if (!(calculatedProperties.GetEngineStateProperty(1) is QPartTableLayoutEngine.QPartTableLayoutStorage engineStateProperty))
          return;
        engineStateProperty.CalculateMaxCellSize(part, layoutContext, layoutStage);
      }
      else
      {
        if (!calculatedProperties.HasLayoutFlag(QPartLayoutFlags.IsTableRow) || !(calculatedProperties.GetEngineStateProperty(1) is QPartTableLayoutEngine.QPartTableLayoutStorage engineStateProperty))
          return;
        engineStateProperty.CalculateMaxRowSize(part, layoutContext, layoutStage);
      }
    }

    public static bool ApplyShrinkInDirection(
      Size size,
      QPartDirection direction,
      QPartLayoutContext layoutContext,
      QPartStretchStorage storage)
    {
      bool flag1 = direction == QPartDirection.Horizontal;
      int canShrinkCount = storage.CanShrinkCount;
      if (canShrinkCount == 0)
        return false;
      int notShrinkSize = storage.NotShrinkSize;
      int usedShrinkSize = storage.UsedShrinkSize;
      int availableShrinkSize = storage.AvailableShrinkSize;
      if (usedShrinkSize <= availableShrinkSize)
        return false;
      if (canShrinkCount == 1)
      {
        QPartCalculatedProperties calculatedProperties = storage.LastCanShrinkPart.CalculatedProperties;
        Size outerSize = calculatedProperties.OuterSize;
        int num = availableShrinkSize - usedShrinkSize;
        if (flag1)
        {
          calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize.Width + num, outerSize.Height), false);
          if (outerSize.Width == calculatedProperties.OuterSize.Width)
            return false;
          calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
          return true;
        }
        calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize.Width, outerSize.Height + num), false);
        if (outerSize.Height == calculatedProperties.OuterSize.Height)
          return false;
        calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
        return true;
      }
      int num1 = usedShrinkSize - availableShrinkSize;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      bool flag2 = false;
      int num5 = 1;
      int val1 = (int) Math.Floor((double) availableShrinkSize / (double) canShrinkCount);
      int index = 0;
      while (index < storage.CanShrinkCount && num1 != 0)
      {
        QPartCalculatedProperties calculatedProperties = storage.GetCanShrinkPart(index).CalculatedProperties;
        Size outerSize = calculatedProperties.OuterSize;
        int num6 = flag1 ? outerSize.Width : outerSize.Height;
        int val2 = flag1 ? calculatedProperties.GetMinimumOuterSize().Width : calculatedProperties.GetMinimumOuterSize().Height;
        if (num6 > val1)
        {
          int num7 = Math.Max(Math.Max(val1, num6 - num1), val2);
          if (num7 == val2)
          {
            ++num2;
            num3 += val2;
          }
          if (num7 != num6)
          {
            num1 -= num6 - num7;
            if (flag1)
            {
              calculatedProperties.SetSizesBasedOnOuterSize(new Size(num7, outerSize.Height), false);
              if (outerSize.Width != calculatedProperties.OuterSize.Width)
              {
                calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
                flag2 = true;
              }
            }
            else
            {
              calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize.Width, num7), false);
              if (outerSize.Height != calculatedProperties.OuterSize.Height)
              {
                calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
                flag2 = true;
              }
            }
          }
          else
            ++num4;
        }
        else
          ++num4;
        if (index == storage.CanShrinkCount - 1)
        {
          if (num4 != storage.CanShrinkCount && num1 > 0)
          {
            val1 = (int) Math.Floor((double) (availableShrinkSize - num3) / (double) (canShrinkCount - num2));
            num4 = 0;
            num2 = 0;
            num3 = 0;
            index = 0;
            ++num5;
          }
          else
            ++index;
        }
        else
          ++index;
      }
      return flag2;
    }

    public static bool ApplyStretchInDirection(
      Size size,
      QPartDirection direction,
      QPartLayoutContext layoutContext,
      QPartStretchStorage storage)
    {
      bool flag1 = direction == QPartDirection.Horizontal;
      int canStretchCount = storage.CanStretchCount;
      if (canStretchCount == 0)
        return false;
      int notStretchSize = storage.NotStretchSize;
      int usedStretchSize = storage.UsedStretchSize;
      int availableStretchSize = storage.AvailableStretchSize;
      if (usedStretchSize >= availableStretchSize)
        return false;
      if (canStretchCount == 1)
      {
        QPartCalculatedProperties calculatedProperties = storage.LastCanStretchPart.CalculatedProperties;
        Size outerSize = calculatedProperties.OuterSize;
        int num = availableStretchSize - usedStretchSize;
        if (flag1)
        {
          calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize.Width + num, outerSize.Height), false);
          if (outerSize.Width == calculatedProperties.OuterSize.Width)
            return false;
          calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
          return true;
        }
        calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize.Width, outerSize.Height + num), false);
        if (outerSize.Height == calculatedProperties.OuterSize.Height)
          return false;
        calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
        return true;
      }
      int num1 = 0;
      bool flag2 = false;
      int val2 = availableStretchSize - usedStretchSize;
      int val1 = (int) Math.Round((double) val2 / (double) canStretchCount);
      int index = 0;
      while (index < storage.CanStretchCount && val2 != 0)
      {
        QPartCalculatedProperties calculatedProperties = storage.GetCanStretchPart(index).CalculatedProperties;
        Size outerSize1 = calculatedProperties.OuterSize;
        if (flag1)
        {
          int width = outerSize1.Width + Math.Min(val1, val2);
          calculatedProperties.SetSizesBasedOnOuterSize(new Size(width, outerSize1.Height), false);
          Size outerSize2 = calculatedProperties.OuterSize;
          if (outerSize2.Width != outerSize1.Width)
          {
            val2 = Math.Max(0, val2 - (outerSize2.Width - outerSize1.Width));
            calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
            flag2 = true;
          }
          else
            ++num1;
        }
        else
        {
          int height = outerSize1.Height + Math.Min(val1, val2);
          calculatedProperties.SetSizesBasedOnOuterSize(new Size(outerSize1.Width, height), false);
          Size outerSize3 = calculatedProperties.OuterSize;
          if (outerSize3.Height != outerSize1.Height)
          {
            val2 = Math.Max(0, val2 - (outerSize3.Height - outerSize1.Height));
            calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
            flag2 = true;
          }
          else
            ++num1;
        }
        if (index == storage.CanStretchCount - 1 && num1 < storage.CanStretchCount && val2 > 0)
        {
          num1 = 0;
          index = 0;
        }
        else
          ++index;
      }
      return flag2;
    }
  }
}
