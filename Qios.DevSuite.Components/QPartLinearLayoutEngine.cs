// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartLinearLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartLinearLayoutEngine : IQPartLayoutEngine
  {
    private static QPartLinearLayoutEngine m_oDefault;

    public static QPartLinearLayoutEngine Default
    {
      get
      {
        if (QPartLinearLayoutEngine.m_oDefault == null)
          QPartLinearLayoutEngine.m_oDefault = new QPartLinearLayoutEngine();
        return QPartLinearLayoutEngine.m_oDefault;
      }
    }

    protected virtual QPartDirection GetPartDirection(IQPart part) => part.Properties.GetDirection(part);

    protected virtual IQPartLayoutEngine GetPartLayoutEngine(IQPart part) => part.LayoutEngine;

    internal bool IsPartVisible(
      IQPart part,
      QPartVisibilitySelectionTypes visibilitySelection,
      QPartLayoutStage layoutStage)
    {
      return part.IsVisible(visibilitySelection);
    }

    public virtual void PerformLayout(
      Rectangle rectangle,
      IQPart part,
      QPartLayoutContext layoutContext)
    {
      this.PrepareForLayout(part, layoutContext);
      Size partSize = this.CalculatePartSize(part, layoutContext);
      QPartApplyConstraintProperties properties = new QPartApplyConstraintProperties(true);
      if (rectangle.Width <= 0)
        rectangle.Width = partSize.Width;
      if (rectangle.Height <= 0)
        rectangle.Height = partSize.Height;
      this.ApplyConstraints(rectangle.Size, part, layoutContext, properties);
      rectangle.Width = properties.NewSize.Width;
      rectangle.Height = properties.NewSize.Height;
      this.LayoutPart(rectangle, part, layoutContext);
      this.FinishLayout(part, layoutContext);
    }

    public virtual void PrepareForLayout(IQPart part, QPartLayoutContext layoutContext)
    {
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.PreparingForLayout, layoutContext, (QPartLayoutStageProperties) null);
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      calculatedProperties.ClearCalculatedSizeProperties();
      calculatedProperties.ClearAppliedLayoutProperties();
      calculatedProperties.ClearEngineState();
      calculatedProperties.ClearCachedPaintProperties();
      calculatedProperties.ClearBoundsProperties();
      if (!(part.ContentObject is IQPartCollection contentObject))
        return;
      for (int index = 0; index < contentObject.Count; ++index)
        contentObject[index].LayoutEngine.PrepareForLayout(contentObject[index], layoutContext);
    }

    public virtual void FinishLayout(IQPart part, QPartLayoutContext layoutContext)
    {
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.LayoutFinished, layoutContext, (QPartLayoutStageProperties) null);
      if (!(part.ContentObject is IQPartCollection contentObject))
        return;
      for (int index = 0; index < contentObject.Count; ++index)
        contentObject[index].LayoutEngine.FinishLayout(contentObject[index], layoutContext);
    }

    public virtual Size CalculatePartSize(IQPart part, QPartLayoutContext layoutContext)
    {
      bool flag = (layoutContext.CalculatePartSizeOptions & QPartCalculatePartSizeOptions.PeekOnly) == QPartCalculatePartSizeOptions.PeekOnly;
      if (flag)
        part.PushCalculatedProperties();
      QPartHelper.SortContentPartsWhenRequired(part);
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.CalculatingSize, layoutContext, (QPartLayoutStageProperties) null);
      object contentObject = part.ContentObject;
      IQPartSharedProperties properties = part.Properties;
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      Size empty = Size.Empty;
      Size partSize = Size.Empty;
      if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingSize))
      {
        Size innerSize;
        if (contentObject is IQPartCollection)
        {
          IQPartCollection collection = contentObject as IQPartCollection;
          innerSize = this.CalculatePartCollectionSize(part, this.GetPartDirection(part), collection, layoutContext);
        }
        else
          innerSize = QPartHelper.GetCustomContentSize(part, contentObject, layoutContext);
        calculatedProperties.ActualContentSize = innerSize;
        calculatedProperties.UnstretchedActualContentSize = innerSize;
        calculatedProperties.SetSizesBasedOnInnerSize(innerSize, true);
        partSize = calculatedProperties.OuterSize;
        QPartLayoutHelper.CalculatePossibleMaxTableRowsAndCells(part, layoutContext, QPartLayoutStage.ApplyingConstraints);
      }
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.SizeCalculated, layoutContext, (QPartLayoutStageProperties) null);
      if (flag)
        part.PullCalculatedProperties();
      return partSize;
    }

    public virtual Size CalculatePartCollectionSize(
      IQPart part,
      QPartDirection direction,
      IQPartCollection collection,
      QPartLayoutContext layoutContext)
    {
      Size contentSize = Size.Empty;
      for (int index = 0; index < collection.Count; ++index)
      {
        Size partSize = this.GetPartLayoutEngine(collection[index]).CalculatePartSize(collection[index], layoutContext);
        contentSize = this.AdjustContentSizeForPartCalculation(direction, contentSize, partSize);
      }
      return contentSize;
    }

    public virtual Size AdjustContentSizeForPartCalculation(
      QPartDirection direction,
      Size contentSize,
      Size contentPartSize)
    {
      switch (direction)
      {
        case QPartDirection.Horizontal:
          contentSize.Width += contentPartSize.Width;
          contentSize.Height = Math.Max(contentSize.Height, contentPartSize.Height);
          break;
        case QPartDirection.Vertical:
          contentSize.Width = Math.Max(contentSize.Width, contentPartSize.Width);
          contentSize.Height += contentPartSize.Height;
          break;
      }
      return contentSize;
    }

    public virtual void ApplyConstraints(
      Size size,
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.ApplyingConstraints, layoutContext, new QPartLayoutStageProperties(size, Size.Empty, properties));
      Size empty = Size.Empty;
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      bool flag1 = this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.ApplyingConstraints);
      string partName = part.PartName;
      if (flag1)
      {
        object contentObject = part.ContentObject;
        Size outerSize = calculatedProperties.OuterSize;
        Size unstretchedOuterSize = calculatedProperties.UnstretchedOuterSize;
        Size requestedSize = size;
        bool flag2 = !calculatedProperties.HasLayoutFlag(QPartLayoutFlags.WidthConstraintApplied) || !calculatedProperties.HasLayoutFlag(QPartLayoutFlags.HeightConstraintApplied) || calculatedProperties.HasLayoutFlag(QPartLayoutFlags.IsTableRow);
        Size size1 = this.ApplyConstraintsToSize(requestedSize, unstretchedOuterSize, layoutContext, calculatedProperties.AppliedOptions);
        if (size1 != outerSize)
        {
          calculatedProperties.SetSizesBasedOnOuterSize(size1, false);
          if (outerSize != calculatedProperties.OuterSize)
            flag2 = true;
        }
        calculatedProperties.LayoutFlags |= QPartLayoutFlags.WidthConstraintApplied | QPartLayoutFlags.HeightConstraintApplied;
        if (flag2)
        {
          Size basedOnOuterSize = calculatedProperties.GetInnerSizeBasedOnOuterSize(size);
          Size innerSize = calculatedProperties.InnerSize;
          Size unstretchedInnerSize = calculatedProperties.UnstretchedInnerSize;
          QPartDirection partDirection = this.GetPartDirection(part);
          QPartApplyConstraintProperties properties1 = new QPartApplyConstraintProperties(properties.CalledFromRoot, partDirection, part.Properties.GetOptions(part));
          if (contentObject is IQPartCollection)
          {
            IQPartCollection collection1 = contentObject as IQPartCollection;
            Size collection2 = this.ApplyConstraintsToCollection(part, basedOnOuterSize, unstretchedInnerSize, this.GetPartDirection(part), collection1, layoutContext, properties1);
            Size newUnstretchedSize = properties1.NewUnstretchedSize;
            calculatedProperties.ActualContentSize = collection2;
            calculatedProperties.UnstretchedActualContentSize = newUnstretchedSize;
            calculatedProperties.SetUnstretchedSizesBasedOnInnerSize(newUnstretchedSize);
            Size size2 = this.ApplyConstraintsToSize(basedOnOuterSize, collection2, layoutContext, calculatedProperties.AppliedOptions);
            calculatedProperties.SetSizesBasedOnInnerSize(size2, false);
          }
          else
            this.ApplyConstraintsToCustomContent(part, layoutContext, properties1);
          properties.NewSize = calculatedProperties.OuterSize;
          properties.NewUnstretchedSize = calculatedProperties.UnstretchedOuterSize;
        }
      }
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.ConstraintsApplied, layoutContext, new QPartLayoutStageProperties(size, part.CalculatedProperties.ActualContentSize, properties));
      if (!flag1)
        return;
      QPartLayoutHelper.CalculatePossibleMaxTableRowsAndCells(part, layoutContext, QPartLayoutStage.ApplyingConstraints);
      properties.NewSize = calculatedProperties.OuterSize;
      properties.NewUnstretchedSize = calculatedProperties.UnstretchedOuterSize;
    }

    protected virtual Size ApplyConstraintsToSize(
      Size requestedSize,
      Size actualSize,
      QPartLayoutContext layoutContext,
      QPartOptions options)
    {
      Size size = actualSize;
      if (requestedSize.Width > actualSize.Width && (options & QPartOptions.StretchHorizontal) != QPartOptions.None || requestedSize.Width < actualSize.Width && (options & QPartOptions.ShrinkHorizontal) != QPartOptions.None)
        size.Width = requestedSize.Width;
      if (requestedSize.Height > actualSize.Height && (options & QPartOptions.StretchVertical) != QPartOptions.None || requestedSize.Height < actualSize.Height && (options & QPartOptions.ShrinkVertical) != QPartOptions.None)
        size.Height = requestedSize.Height;
      return size;
    }

    protected virtual void ApplyConstraintsToCustomContent(
      IQPart part,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
    }

    public virtual Size ApplyConstraintsToCollection(
      IQPart part,
      Size requestedSize,
      Size unstretchedCollectionSize,
      QPartDirection direction,
      IQPartCollection collection,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      Size empty1 = Size.Empty;
      Size empty2 = Size.Empty;
      bool flag = false;
      int debugCount = 0;
      Size size1 = requestedSize;
      if (!properties.DoOneLoopSimple)
        size1 = this.ApplyConstraintsToSize(requestedSize, unstretchedCollectionSize, layoutContext, properties.Options);
      while (!flag)
      {
        QPartLayoutHelper.AssertApplyConstraintsLoopCount(ref debugCount, part);
        QPartStretchStorage storage = QPartLayoutHelper.PrepareForStretchShrink(part, collection, size1, direction, layoutContext);
        if (!QPartLayoutHelper.ApplyStretchInDirection(size1, direction, layoutContext, storage))
          QPartLayoutHelper.ApplyShrinkInDirection(size1, direction, layoutContext, storage);
        if (!properties.DoOneLoopSimple)
          QPartLayoutHelper.SaveSizeBeforeConstraints(collection);
        for (int index = 0; index < collection.Count; ++index)
        {
          IQPart part1 = collection[index];
          if (this.IsPartVisible(part1, layoutContext.VisibilitySelection, QPartLayoutStage.ApplyingConstraints))
          {
            Size outerSize = part1.CalculatedProperties.OuterSize;
            IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part1);
            Size size2 = direction == QPartDirection.Horizontal ? new Size(outerSize.Width, size1.Height) : new Size(size1.Width, outerSize.Height);
            QPartApplyConstraintProperties properties1 = new QPartApplyConstraintProperties(false, direction, properties.Options);
            partLayoutEngine.ApplyConstraints(size2, part1, layoutContext, properties1);
          }
        }
        flag = properties.DoOneLoopSimple || QPartLayoutHelper.ReapplyConstraintsRequired(collection, direction, layoutContext, properties);
        if (!flag && properties.CalledFromRoot)
          size1 = this.ApplyConstraintsToSize(requestedSize, properties.NewUnstretchedSize, layoutContext, properties.Options);
        else
          flag = true;
      }
      return properties.NewSize;
    }

    public virtual void LayoutPart(
      Rectangle rectangle,
      IQPart part,
      QPartLayoutContext layoutContext)
    {
      QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
      IQPartSharedProperties properties = part.Properties;
      calculatedProperties.ClearBoundsProperties();
      QPartHelper.SortContentPartsWhenRequired(part);
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.CalculatingBounds, layoutContext, new QPartLayoutStageProperties(rectangle.Size, rectangle, calculatedProperties.ActualContentSize));
      if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds))
      {
        QRange horizontalRange = QPartHelper.AlignPartSize(calculatedProperties.OuterSize.Width, properties.GetAlignmentHorizontal(part), rectangle.Left, rectangle.Right, false, true);
        QRange verticalRange = QPartHelper.AlignPartSize(calculatedProperties.OuterSize.Height, properties.GetAlignmentVertical(part), rectangle.Top, rectangle.Bottom, false, true);
        calculatedProperties.SetBoundsBasedOnOuterBounds(QRange.CreateRectangle(horizontalRange, verticalRange));
        object contentObject = part.ContentObject;
        if (contentObject is IQPartCollection)
        {
          IQPartCollection collection = contentObject as IQPartCollection;
          switch (this.GetPartDirection(part))
          {
            case QPartDirection.Horizontal:
              this.LayoutPartCollectionHorizontal(part.CalculatedProperties.InnerBounds, collection, layoutContext);
              break;
            case QPartDirection.Vertical:
              this.LayoutPartCollectionVertical(part.CalculatedProperties.InnerBounds, collection, layoutContext);
              break;
          }
        }
      }
      QPartHelper.RaiseHandleLayoutStage(part, QPartLayoutStage.BoundsCalculated, layoutContext, new QPartLayoutStageProperties(rectangle.Size, rectangle, calculatedProperties.ActualContentSize));
    }

    protected virtual void LayoutPartCollectionHorizontal(
      Rectangle bounds,
      IQPartCollection collection,
      QPartLayoutContext layoutContext)
    {
      Rectangle inBounds = bounds;
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart part = collection[index];
        IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part);
        QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
        IQPartSharedProperties properties = part.Properties;
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && properties.GetAlignmentHorizontal(part) == QPartAlignment.Near)
        {
          Rectangle rectangle = new Rectangle(inBounds.Left, inBounds.Top, calculatedProperties.OuterSize.Width, inBounds.Height);
          partLayoutEngine.LayoutPart(rectangle, part, layoutContext);
          inBounds = Rectangle.FromLTRB(calculatedProperties.OuterBounds.Right, inBounds.Top, inBounds.Right, inBounds.Bottom);
        }
      }
      for (int index = collection.Count - 1; index >= 0; --index)
      {
        IQPart part = collection[index];
        IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part);
        QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
        IQPartSharedProperties properties = part.Properties;
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && properties.GetAlignmentHorizontal(part) == QPartAlignment.Far)
        {
          Rectangle rectangle = new Rectangle(inBounds.Right - calculatedProperties.OuterSize.Width, inBounds.Top, calculatedProperties.OuterSize.Width, inBounds.Height);
          partLayoutEngine.LayoutPart(rectangle, part, layoutContext);
          inBounds = Rectangle.FromLTRB(inBounds.Left, inBounds.Top, calculatedProperties.OuterBounds.Left, inBounds.Bottom);
        }
      }
      Rectangle rectangle1 = QPartHelper.AlignPartSize(QPartHelper.GetCalculatedSize(collection, layoutContext.VisibilitySelection, QPartCalculateSizedReturnOptions.DefaultForHorizontalDirection | QPartCalculateSizedReturnOptions.OuterSize, QPartSelectionTypes.HorizontalCenterAligned), QPartAlignment.Centered, inBounds, QPartDirection.Horizontal, false, true);
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart part = collection[index];
        IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part);
        QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
        IQPartSharedProperties properties = part.Properties;
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && properties.GetAlignmentHorizontal(part) == QPartAlignment.Centered)
        {
          Rectangle rectangle2 = new Rectangle(rectangle1.Left, rectangle1.Top, calculatedProperties.OuterSize.Width, rectangle1.Height);
          partLayoutEngine.LayoutPart(rectangle2, part, layoutContext);
          rectangle1 = Rectangle.FromLTRB(calculatedProperties.OuterBounds.Right, rectangle1.Top, rectangle1.Right, rectangle1.Bottom);
        }
      }
    }

    protected virtual void LayoutPartCollectionVertical(
      Rectangle bounds,
      IQPartCollection collection,
      QPartLayoutContext layoutContext)
    {
      Rectangle inBounds = bounds;
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart part = collection[index];
        IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part);
        QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
        IQPartSharedProperties properties = part.Properties;
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && properties.GetAlignmentVertical(part) == QPartAlignment.Near)
        {
          Rectangle rectangle = new Rectangle(inBounds.Left, inBounds.Top, inBounds.Width, calculatedProperties.OuterSize.Height);
          partLayoutEngine.LayoutPart(rectangle, part, layoutContext);
          inBounds = Rectangle.FromLTRB(inBounds.Left, calculatedProperties.OuterBounds.Bottom, inBounds.Right, inBounds.Bottom);
        }
      }
      for (int index = collection.Count - 1; index >= 0; --index)
      {
        IQPart part = collection[index];
        IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part);
        QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
        IQPartSharedProperties properties = part.Properties;
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && properties.GetAlignmentVertical(part) == QPartAlignment.Far)
        {
          Rectangle rectangle = new Rectangle(inBounds.Left, inBounds.Bottom - calculatedProperties.OuterSize.Height, inBounds.Width, calculatedProperties.OuterSize.Height);
          partLayoutEngine.LayoutPart(rectangle, part, layoutContext);
          inBounds = Rectangle.FromLTRB(inBounds.Left, inBounds.Top, inBounds.Right, calculatedProperties.OuterBounds.Top);
        }
      }
      Rectangle rectangle1 = QPartHelper.AlignPartSize(QPartHelper.GetCalculatedSize(collection, layoutContext.VisibilitySelection, QPartCalculateSizedReturnOptions.DefaultForVerticalDirection | QPartCalculateSizedReturnOptions.OuterSize, QPartSelectionTypes.VerticalCenterAligned), QPartAlignment.Centered, inBounds, QPartDirection.Vertical, false, true);
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart part = collection[index];
        IQPartLayoutEngine partLayoutEngine = this.GetPartLayoutEngine(part);
        QPartCalculatedProperties calculatedProperties = part.CalculatedProperties;
        IQPartSharedProperties properties = part.Properties;
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && properties.GetAlignmentVertical(part) == QPartAlignment.Centered)
        {
          Rectangle rectangle2 = new Rectangle(rectangle1.Left, rectangle1.Top, rectangle1.Width, calculatedProperties.OuterSize.Height);
          partLayoutEngine.LayoutPart(rectangle2, part, layoutContext);
          rectangle1 = Rectangle.FromLTRB(rectangle1.Left, calculatedProperties.OuterBounds.Bottom, rectangle1.Right, rectangle1.Bottom);
        }
      }
    }
  }
}
