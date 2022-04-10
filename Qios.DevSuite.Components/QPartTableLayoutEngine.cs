// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartTableLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartTableLayoutEngine : QPartLinearLayoutEngine
  {
    private static QPartTableLayoutEngine m_oDefault;

    public static QPartTableLayoutEngine Default
    {
      get
      {
        if (QPartTableLayoutEngine.m_oDefault == null)
          QPartTableLayoutEngine.m_oDefault = new QPartTableLayoutEngine();
        return QPartTableLayoutEngine.m_oDefault;
      }
    }

    protected override QPartDirection GetPartDirection(IQPart part)
    {
      if (part.ParentPart == null || !(part.ParentPart.LayoutEngine is QPartTableLayoutEngine))
        return base.GetPartDirection(part);
      return part.ParentPart.Properties.GetDirection(part.ParentPart) != QPartDirection.Horizontal ? QPartDirection.Horizontal : QPartDirection.Vertical;
    }

    public override Size CalculatePartCollectionSize(
      IQPart part,
      QPartDirection direction,
      IQPartCollection collection,
      QPartLayoutContext layoutContext)
    {
      QPartTableLayoutEngine.QPartTableLayoutStorage tableLayoutStorage = new QPartTableLayoutEngine.QPartTableLayoutStorage(this);
      part.CalculatedProperties.SetEngineStateProperty(1, (object) tableLayoutStorage);
      tableLayoutStorage.CreateCellArray(collection);
      tableLayoutStorage.MarkRowsAndCells(collection);
      base.CalculatePartCollectionSize(part, direction, collection, layoutContext);
      return tableLayoutStorage.ApplyRowCellSizes(collection, Size.Empty, direction, layoutContext, (QPartApplyConstraintProperties) null);
    }

    public override Size ApplyConstraintsToCollection(
      IQPart part,
      Size requestedSize,
      Size unstretchedCollectionSize,
      QPartDirection direction,
      IQPartCollection collection,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      QPartTableLayoutEngine.QPartTableLayoutStorage engineStateProperty = part.CalculatedProperties.GetEngineStateProperty(1) as QPartTableLayoutEngine.QPartTableLayoutStorage;
      bool flag = false;
      Size empty = Size.Empty;
      int debugCount = 0;
      properties.DoOneLoopSimple = true;
      Size size = this.ApplyConstraintsToSize(requestedSize, unstretchedCollectionSize, layoutContext, properties.Options);
      while (!flag)
      {
        QPartLayoutHelper.AssertApplyConstraintsLoopCount(ref debugCount, part);
        engineStateProperty.ClearSizes();
        QPartLayoutHelper.SaveSizeBeforeConstraints(collection);
        base.ApplyConstraintsToCollection(part, size, unstretchedCollectionSize, direction, collection, layoutContext, properties);
        engineStateProperty.ApplyRowCellSizes(collection, size, direction, layoutContext, properties);
        flag = QPartLayoutHelper.ReapplyConstraintsRequired(collection, direction, layoutContext, properties);
        if (!flag && properties.CalledFromRoot)
          size = this.ApplyConstraintsToSize(requestedSize, properties.NewUnstretchedSize, layoutContext, properties.Options);
        else
          flag = true;
      }
      return properties.NewSize;
    }

    public class QPartTableLayoutStorage
    {
      private Size[] m_aCellSizes;
      private Size[] m_aUnstretchedCellSizes;
      private QPadding[] m_aPaddings;
      private QMargin[] m_aMargins;
      private QMargin m_oRowMargin = QMargin.Empty;
      private QPadding m_oRowPadding = QPadding.Empty;
      private QPartTableLayoutEngine m_oEngine;

      public QPartTableLayoutStorage(QPartTableLayoutEngine engine) => this.m_oEngine = engine;

      public Size[] CellSizes => this.m_aCellSizes;

      public Size[] UnstretchedCellSizes => this.m_aUnstretchedCellSizes;

      protected QPartTableLayoutEngine Engine => this.m_oEngine;

      public void ClearCellArrays()
      {
        if (this.m_aCellSizes != null)
          Array.Clear((Array) this.m_aCellSizes, 0, this.m_aCellSizes.Length);
        if (this.m_aUnstretchedCellSizes != null)
          Array.Clear((Array) this.m_aUnstretchedCellSizes, 0, this.m_aUnstretchedCellSizes.Length);
        if (this.m_aPaddings != null)
          Array.Clear((Array) this.m_aPaddings, 0, this.m_aPaddings.Length);
        if (this.m_aMargins != null)
          Array.Clear((Array) this.m_aMargins, 0, this.m_aPaddings.Length);
        this.m_oRowMargin = QMargin.Empty;
        this.m_oRowPadding = QPadding.Empty;
      }

      public void ClearSizes()
      {
        if (this.m_aCellSizes == null)
          return;
        Array.Clear((Array) this.m_aCellSizes, 0, this.m_aCellSizes.Length);
      }

      public void CreateCellArray(IQPartCollection rowCollection)
      {
        int val1 = 0;
        for (int index = 0; index < rowCollection.Count; ++index)
        {
          if (rowCollection[index].LayoutEngine is QPartTableRowLayoutEngine)
            val1 = !(rowCollection[index].ContentObject is IQPartCollection contentObject) ? Math.Max(val1, 1) : Math.Max(val1, contentObject.Count);
        }
        this.m_aCellSizes = new Size[val1];
        this.m_aUnstretchedCellSizes = new Size[val1];
        this.m_aPaddings = new QPadding[val1];
        this.m_aMargins = new QMargin[val1];
      }

      public void CalculateMaxCellSize(
        IQPart cell,
        QPartLayoutContext layoutContext,
        QPartLayoutStage layoutStage)
      {
        if (!cell.IsVisible(layoutContext.VisibilitySelection))
          return;
        QPartCalculatedProperties calculatedProperties = cell.CalculatedProperties;
        Size outerSize = calculatedProperties.OuterSize;
        Size unstretchedOuterSize = calculatedProperties.UnstretchedOuterSize;
        int partIndex = calculatedProperties.PartIndex;
        Size aCellSiz = this.m_aCellSizes[partIndex];
        Size unstretchedCellSiz = this.m_aUnstretchedCellSizes[partIndex];
        this.m_aCellSizes[partIndex] = new Size(Math.Max(aCellSiz.Width, outerSize.Width), Math.Max(aCellSiz.Height, outerSize.Height));
        this.m_aUnstretchedCellSizes[partIndex] = new Size(Math.Max(unstretchedCellSiz.Width, unstretchedOuterSize.Width), Math.Max(unstretchedCellSiz.Height, unstretchedOuterSize.Height));
        this.m_aPaddings[partIndex] = QPadding.MaxPaddings(this.m_aPaddings[partIndex], calculatedProperties.AppliedPadding);
        this.m_aMargins[partIndex] = QMargin.MaxMargins(this.m_aMargins[partIndex], calculatedProperties.AppliedMargin);
      }

      public void CalculateMaxRowSize(
        IQPart row,
        QPartLayoutContext layoutContext,
        QPartLayoutStage layoutStage)
      {
        if (!row.IsVisible(layoutContext.VisibilitySelection))
          return;
        this.m_oRowPadding = QPadding.MaxPaddings(this.m_oRowPadding, row.CalculatedProperties.AppliedPadding);
        this.m_oRowMargin = QMargin.MaxMargins(this.m_oRowMargin, row.CalculatedProperties.AppliedMargin);
      }

      public void MarkRowsAndCells(IQPartCollection rowCollection)
      {
        for (int index1 = 0; index1 < rowCollection.Count; ++index1)
        {
          IQPart row = rowCollection[index1];
          if (row.LayoutEngine is QPartTableRowLayoutEngine)
          {
            row.CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.IsTableRow, true);
            row.CalculatedProperties.SetEngineStateProperty(1, (object) this);
            if (rowCollection[index1].ContentObject is IQPartCollection contentObject)
            {
              for (int index2 = 0; index2 < contentObject.Count; ++index2)
              {
                IQPart qpart = contentObject[index2];
                qpart.CalculatedProperties.SetLayoutFlag(QPartLayoutFlags.IsTableCell, true);
                qpart.CalculatedProperties.SetEngineStateProperty(1, (object) this);
              }
            }
          }
        }
      }

      public Size ApplyRowCellSizes(
        IQPartCollection rowCollection,
        Size requestedSize,
        QPartDirection direction,
        QPartLayoutContext layoutContext,
        QPartApplyConstraintProperties properties)
      {
        if (this.m_aCellSizes == null)
          return Size.Empty;
        Size contentSize1 = Size.Empty;
        Size contentSize2 = Size.Empty;
        for (int index1 = 0; index1 < rowCollection.Count; ++index1)
        {
          IQPart row = rowCollection[index1];
          QPartCalculatedProperties calculatedProperties1 = row.CalculatedProperties;
          QPartDirection partDirection = this.Engine.GetPartDirection(row);
          if (calculatedProperties1.HasLayoutFlag(QPartLayoutFlags.IsTableRow))
          {
            Size size1 = Size.Empty;
            Size size2 = Size.Empty;
            if (rowCollection[index1].ContentObject is IQPartCollection contentObject)
            {
              for (int index2 = 0; index2 < contentObject.Count; ++index2)
              {
                IQPart qpart = contentObject[index2];
                QPartCalculatedProperties calculatedProperties2 = qpart.CalculatedProperties;
                if (calculatedProperties2.HasLayoutFlag(QPartLayoutFlags.IsTableCell))
                {
                  switch (direction)
                  {
                    case QPartDirection.Horizontal:
                      calculatedProperties2.AppliedPadding = new QPadding(calculatedProperties2.AppliedPadding.Left, this.m_aPaddings[index2].Top, this.m_aPaddings[index2].Bottom, calculatedProperties2.AppliedPadding.Right);
                      calculatedProperties2.AppliedMargin = new QMargin(calculatedProperties2.AppliedMargin.Left, this.m_aMargins[index2].Top, this.m_aMargins[index2].Bottom, calculatedProperties2.AppliedMargin.Right);
                      calculatedProperties2.SetUnstretchedSizesBasedOnOuterSize(new Size(calculatedProperties2.UnstretchedOuterSize.Width, this.m_aUnstretchedCellSizes[index2].Height));
                      calculatedProperties2.SetSizesBasedOnOuterSize(new Size(calculatedProperties2.OuterSize.Width, this.m_aCellSizes[index2].Height), false);
                      break;
                    case QPartDirection.Vertical:
                      calculatedProperties2.AppliedPadding = new QPadding(this.m_aPaddings[index2].Left, calculatedProperties2.AppliedPadding.Top, calculatedProperties2.AppliedPadding.Bottom, this.m_aPaddings[index2].Right);
                      calculatedProperties2.AppliedMargin = new QMargin(this.m_aMargins[index2].Left, calculatedProperties2.AppliedMargin.Top, calculatedProperties2.AppliedMargin.Bottom, this.m_aMargins[index2].Right);
                      calculatedProperties2.SetUnstretchedSizesBasedOnOuterSize(new Size(this.m_aUnstretchedCellSizes[index2].Width, calculatedProperties2.UnstretchedOuterSize.Height));
                      calculatedProperties2.SetSizesBasedOnOuterSize(new Size(this.m_aCellSizes[index2].Width, calculatedProperties2.OuterSize.Height), false);
                      break;
                  }
                  size1 = this.Engine.AdjustContentSizeForPartCalculation(partDirection, size1, qpart.CalculatedProperties.OuterSize);
                  size2 = this.Engine.AdjustContentSizeForPartCalculation(partDirection, size2, qpart.CalculatedProperties.UnstretchedOuterSize);
                }
              }
              if (partDirection == QPartDirection.Horizontal)
              {
                calculatedProperties1.AppliedPadding = new QPadding(this.m_oRowPadding.Left, calculatedProperties1.AppliedPadding.Top, calculatedProperties1.AppliedPadding.Bottom, this.m_oRowPadding.Right);
                calculatedProperties1.AppliedMargin = new QMargin(this.m_oRowMargin.Left, calculatedProperties1.AppliedMargin.Top, calculatedProperties1.AppliedMargin.Bottom, this.m_oRowMargin.Right);
              }
              else
              {
                calculatedProperties1.AppliedPadding = new QPadding(calculatedProperties1.AppliedPadding.Left, this.m_oRowPadding.Top, this.m_oRowPadding.Bottom, calculatedProperties1.AppliedPadding.Right);
                calculatedProperties1.AppliedMargin = new QMargin(calculatedProperties1.AppliedMargin.Left, this.m_oRowMargin.Top, this.m_oRowMargin.Bottom, calculatedProperties1.AppliedMargin.Right);
              }
              if (size2 != Size.Empty)
                rowCollection[index1].CalculatedProperties.SetUnstretchedSizesBasedOnInnerSize(size2);
              if (size1 != Size.Empty)
              {
                if (properties != null)
                  size1 = this.m_oEngine.ApplyConstraintsToSize(calculatedProperties1.GetInnerSizeBasedOnOuterSize(requestedSize), size1, layoutContext, calculatedProperties1.AppliedOptions);
                rowCollection[index1].CalculatedProperties.SetSizesBasedOnInnerSize(size1, false);
              }
            }
          }
          contentSize1 = this.Engine.AdjustContentSizeForPartCalculation(direction, contentSize1, row.CalculatedProperties.OuterSize);
          contentSize2 = this.Engine.AdjustContentSizeForPartCalculation(direction, contentSize2, row.CalculatedProperties.UnstretchedOuterSize);
        }
        if (properties != null)
        {
          properties.NewSize = contentSize1;
          properties.NewUnstretchedSize = contentSize2;
        }
        return contentSize1;
      }
    }
  }
}
