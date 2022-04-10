// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartFlowLayoutEngine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartFlowLayoutEngine : QPartLinearLayoutEngine
  {
    private static QPartFlowLayoutEngine m_oDefault;

    public static QPartFlowLayoutEngine Default
    {
      get
      {
        if (QPartFlowLayoutEngine.m_oDefault == null)
          QPartFlowLayoutEngine.m_oDefault = new QPartFlowLayoutEngine();
        return QPartFlowLayoutEngine.m_oDefault;
      }
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
      Size empty1 = Size.Empty;
      Size empty2 = Size.Empty;
      Size unstretchedCollectionSize1 = Size.Empty;
      Size size = Size.Empty;
      int startIndex = -1;
      int count = 0;
      QPartApplyConstraintProperties properties1 = new QPartApplyConstraintProperties(false, direction, properties.Options);
      switch (direction)
      {
        case QPartDirection.Horizontal:
          for (int index = 0; index < collection.Count; ++index)
          {
            IQPart part1 = collection[index];
            Size outerSize = part1.CalculatedProperties.OuterSize;
            Size unstretchedOuterSize = part1.CalculatedProperties.UnstretchedOuterSize;
            if (this.IsPartVisible(part1, layoutContext.VisibilitySelection, QPartLayoutStage.ApplyingConstraints))
            {
              if (size.Width + outerSize.Width <= requestedSize.Width)
              {
                if (startIndex < 0)
                  startIndex = index;
                ++count;
                size.Width += outerSize.Width;
                size.Height = Math.Max(size.Height, unstretchedOuterSize.Height);
                unstretchedCollectionSize1.Width += unstretchedOuterSize.Width;
                unstretchedCollectionSize1.Height = Math.Max(unstretchedCollectionSize1.Height, unstretchedOuterSize.Height);
              }
              else
              {
                if (startIndex >= 0)
                {
                  QPartArray collection1 = new QPartArray(collection, startIndex, count);
                  size = this.ApplyConstraintsToOneRow(part, new Size(requestedSize.Width, size.Height), unstretchedCollectionSize1, direction, (IQPartCollection) collection1, layoutContext, properties1);
                  empty2.Height += properties1.NewUnstretchedSize.Height;
                  empty2.Width = Math.Max(properties1.NewUnstretchedSize.Width, empty2.Width);
                }
                empty1.Height += size.Height;
                empty1.Width = Math.Max(size.Width, empty1.Width);
                size = new Size(outerSize.Width, unstretchedOuterSize.Height);
                unstretchedCollectionSize1 = unstretchedOuterSize;
                startIndex = index;
                count = 1;
              }
            }
            else if (startIndex >= 0)
              ++count;
          }
          if (startIndex >= 0)
          {
            QPartArray collection2 = new QPartArray(collection, startIndex, count);
            size = this.ApplyConstraintsToOneRow(part, new Size(requestedSize.Width, size.Height), unstretchedCollectionSize1, direction, (IQPartCollection) collection2, layoutContext, properties1);
            empty2.Height += properties1.NewUnstretchedSize.Height;
            empty2.Width = Math.Max(properties1.NewUnstretchedSize.Width, empty2.Width);
          }
          empty1.Height += size.Height;
          empty1.Width = Math.Max(size.Width, empty1.Width);
          break;
        case QPartDirection.Vertical:
          for (int index = 0; index < collection.Count; ++index)
          {
            IQPart part2 = collection[index];
            Size outerSize = part2.CalculatedProperties.OuterSize;
            Size unstretchedOuterSize = part2.CalculatedProperties.UnstretchedOuterSize;
            if (this.IsPartVisible(part2, layoutContext.VisibilitySelection, QPartLayoutStage.ApplyingConstraints))
            {
              if (size.Height + outerSize.Height <= requestedSize.Height)
              {
                if (startIndex < 0)
                  startIndex = index;
                ++count;
                size.Height += outerSize.Height;
                size.Width = Math.Max(size.Width, unstretchedOuterSize.Width);
                unstretchedCollectionSize1.Height += unstretchedOuterSize.Height;
                unstretchedCollectionSize1.Width = Math.Max(unstretchedCollectionSize1.Width, unstretchedOuterSize.Width);
              }
              else
              {
                if (startIndex >= 0)
                {
                  QPartArray collection3 = new QPartArray(collection, startIndex, count);
                  size = this.ApplyConstraintsToOneRow(part, new Size(size.Width, requestedSize.Height), unstretchedCollectionSize1, direction, (IQPartCollection) collection3, layoutContext, properties1);
                  empty2.Width += properties1.NewUnstretchedSize.Width;
                  empty2.Height = Math.Max(properties1.NewUnstretchedSize.Height, empty2.Height);
                }
                empty1.Width += size.Width;
                empty1.Height = Math.Max(size.Height, empty1.Height);
                size = new Size(unstretchedOuterSize.Width, outerSize.Height);
                unstretchedCollectionSize1 = unstretchedOuterSize;
                startIndex = index;
                count = 1;
              }
            }
            else if (startIndex >= 0)
              ++count;
          }
          if (startIndex >= 0)
          {
            QPartArray collection4 = new QPartArray(collection, startIndex, count);
            size = this.ApplyConstraintsToOneRow(part, new Size(size.Width, requestedSize.Height), unstretchedCollectionSize1, direction, (IQPartCollection) collection4, layoutContext, properties1);
            empty2.Width += properties1.NewUnstretchedSize.Width;
            empty2.Height = Math.Max(properties1.NewUnstretchedSize.Height, empty2.Height);
          }
          empty1.Width += size.Width;
          empty1.Height = Math.Max(size.Height, empty1.Height);
          break;
      }
      properties.NewSize = empty1;
      properties.NewUnstretchedSize = empty2;
      return empty1;
    }

    private Size ApplyConstraintsToOneRow(
      IQPart part,
      Size requestedSize,
      Size unstretchedCollectionSize,
      QPartDirection direction,
      IQPartCollection collection,
      QPartLayoutContext layoutContext,
      QPartApplyConstraintProperties properties)
    {
      return QPartLinearLayoutEngine.Default.ApplyConstraintsToCollection(part, requestedSize, unstretchedCollectionSize, direction, collection, layoutContext, properties);
    }

    private QPartFlowLayoutEngine.QPartFlowLayoutStorage LayoutPartCollection(
      Rectangle bounds,
      IQPartCollection collection,
      QPartLayoutContext layoutContext,
      QPartDirection direction)
    {
      Size empty1 = Size.Empty;
      Size empty2 = Size.Empty;
      QPartFlowLayoutEngine.QPartFlowLayoutStorage flowLayoutStorage = new QPartFlowLayoutEngine.QPartFlowLayoutStorage(bounds, collection, layoutContext, direction, this);
      for (int index = 0; index < collection.Count; ++index)
      {
        IQPart part = collection[index];
        if (this.IsPartVisible(part, layoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds))
        {
          if (!flowLayoutStorage.PartFitsInRow(part))
            flowLayoutStorage.CloseRow();
          flowLayoutStorage.PartAddToRow(part, index);
        }
      }
      flowLayoutStorage.CloseRow();
      flowLayoutStorage.CleanUp();
      return flowLayoutStorage;
    }

    protected override void LayoutPartCollectionHorizontal(
      Rectangle bounds,
      IQPartCollection collection,
      QPartLayoutContext layoutContext)
    {
      this.LayoutPartCollection(bounds, collection, layoutContext, QPartDirection.Horizontal);
    }

    protected override void LayoutPartCollectionVertical(
      Rectangle bounds,
      IQPartCollection collection,
      QPartLayoutContext layoutContext)
    {
      this.LayoutPartCollection(bounds, collection, layoutContext, QPartDirection.Vertical);
    }

    public class QPartFlowLayoutStorage
    {
      private Rectangle OriginalBounds = Rectangle.Empty;
      private Rectangle ContentBounds = Rectangle.Empty;
      private IQPartCollection Collection;
      private QPartLayoutContext LayoutContext;
      private QPartFlowLayoutEngine Engine;
      private QPartDirection Direction;
      private int FirstRowPartIndex = -1;
      private int LastRowPartIndex = -1;
      private int ReservedForNear;
      private int ReservedForCenter;
      private int ReservedForFar;
      private int ReservedForDelta;

      public QPartFlowLayoutStorage(
        Rectangle bounds,
        IQPartCollection collection,
        QPartLayoutContext context,
        QPartDirection direction,
        QPartFlowLayoutEngine engine)
      {
        this.OriginalBounds = bounds;
        this.ContentBounds = bounds;
        this.Collection = collection;
        this.LayoutContext = context;
        this.Engine = engine;
        this.Direction = direction;
      }

      public void CleanUp()
      {
        this.Engine = (QPartFlowLayoutEngine) null;
        this.LayoutContext = (QPartLayoutContext) null;
        this.Collection = (IQPartCollection) null;
      }

      private bool Horizontal => this.Direction == QPartDirection.Horizontal;

      private Size Reserved => this.Horizontal ? new Size(this.ReservedForCenter + this.ReservedForFar + this.ReservedForNear, this.ReservedForDelta) : new Size(this.ReservedForDelta, this.ReservedForNear + this.ReservedForCenter + this.ReservedForFar);

      public bool PartFitsInRow(IQPart part) => this.Horizontal ? this.ContentBounds.Width - this.Reserved.Width >= part.CalculatedProperties.OuterSize.Width : this.ContentBounds.Height - this.Reserved.Height >= part.CalculatedProperties.OuterSize.Height;

      public void PartAddToRow(IQPart part, int index)
      {
        if (this.FirstRowPartIndex < 0)
          this.FirstRowPartIndex = index;
        this.LastRowPartIndex = index;
        this.ReserveForPart(part, true);
      }

      private void ReserveForPart(IQPart part, bool delta)
      {
        if (!this.Engine.IsPartVisible(part, this.LayoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds))
          return;
        switch (this.Horizontal ? part.Properties.GetAlignmentHorizontal(part) : part.Properties.GetAlignmentVertical(part))
        {
          case QPartAlignment.Near:
            this.ReservedForNear += this.Horizontal ? part.CalculatedProperties.OuterSize.Width : part.CalculatedProperties.OuterSize.Height;
            break;
          case QPartAlignment.Centered:
            this.ReservedForCenter += this.Horizontal ? part.CalculatedProperties.OuterSize.Width : part.CalculatedProperties.OuterSize.Height;
            break;
          case QPartAlignment.Far:
            this.ReservedForFar += this.Horizontal ? part.CalculatedProperties.OuterSize.Width : part.CalculatedProperties.OuterSize.Height;
            break;
        }
        if (!delta)
          return;
        this.ReservedForDelta = Math.Max(this.ReservedForDelta, this.Horizontal ? part.CalculatedProperties.OuterSize.Height : part.CalculatedProperties.OuterSize.Width);
      }

      public void CloseRow()
      {
        if (this.FirstRowPartIndex < 0)
          return;
        this.ReservedForNear = 0;
        this.ReservedForFar = 0;
        this.ReservedForCenter = 0;
        for (int firstRowPartIndex = this.FirstRowPartIndex; firstRowPartIndex <= this.LastRowPartIndex; ++firstRowPartIndex)
          this.ReserveForPart(this.Collection[firstRowPartIndex], false);
        Rectangle rectangle1 = QPartHelper.AlignPartSize(this.Horizontal ? new Size(this.ReservedForCenter, this.ReservedForDelta) : new Size(this.ReservedForDelta, this.ReservedForCenter), QPartAlignment.Centered, this.Horizontal ? new Rectangle(this.ContentBounds.X + this.ReservedForNear, this.ContentBounds.Top, this.ContentBounds.Width - (this.ReservedForNear + this.ReservedForFar), this.ReservedForDelta) : new Rectangle(this.ContentBounds.X, this.ContentBounds.Top + this.ReservedForNear, this.ReservedForDelta, this.ContentBounds.Height - (this.ReservedForNear + this.ReservedForFar)), this.Direction, false, true);
        Rectangle rectangle2;
        for (int firstRowPartIndex = this.FirstRowPartIndex; firstRowPartIndex <= this.LastRowPartIndex; ++firstRowPartIndex)
        {
          IQPart part = this.Collection[firstRowPartIndex];
          if (this.Engine.IsPartVisible(part, this.LayoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds))
          {
            switch (this.Horizontal ? part.Properties.GetAlignmentHorizontal(part) : part.Properties.GetAlignmentVertical(part))
            {
              case QPartAlignment.Near:
                rectangle2 = !this.Horizontal ? new Rectangle(this.ContentBounds.Left, this.ContentBounds.Top, this.ReservedForDelta, part.CalculatedProperties.OuterSize.Height) : new Rectangle(this.ContentBounds.Left, this.ContentBounds.Top, part.CalculatedProperties.OuterSize.Width, this.ReservedForDelta);
                this.Engine.GetPartLayoutEngine(part).LayoutPart(rectangle2, part, this.LayoutContext);
                this.ContentBounds = !this.Horizontal ? Rectangle.FromLTRB(this.ContentBounds.Left, part.CalculatedProperties.OuterBounds.Bottom, this.ContentBounds.Right, this.ContentBounds.Bottom) : Rectangle.FromLTRB(part.CalculatedProperties.OuterBounds.Right, this.ContentBounds.Top, this.ContentBounds.Right, this.ContentBounds.Bottom);
                continue;
              case QPartAlignment.Centered:
                rectangle2 = !this.Horizontal ? new Rectangle(rectangle1.Left, rectangle1.Top, this.ReservedForDelta, part.CalculatedProperties.OuterSize.Height) : new Rectangle(rectangle1.Left, rectangle1.Top, part.CalculatedProperties.OuterSize.Width, this.ReservedForDelta);
                this.Engine.GetPartLayoutEngine(part).LayoutPart(rectangle2, part, this.LayoutContext);
                rectangle1 = !this.Horizontal ? Rectangle.FromLTRB(rectangle1.Left, part.CalculatedProperties.OuterBounds.Bottom, rectangle1.Right, rectangle1.Bottom) : Rectangle.FromLTRB(part.CalculatedProperties.OuterBounds.Right, rectangle1.Top, rectangle1.Right, rectangle1.Bottom);
                continue;
              default:
                continue;
            }
          }
        }
        for (int lastRowPartIndex = this.LastRowPartIndex; lastRowPartIndex >= this.FirstRowPartIndex; --lastRowPartIndex)
        {
          IQPart part = this.Collection[lastRowPartIndex];
          if (this.Engine.IsPartVisible(part, this.LayoutContext.VisibilitySelection, QPartLayoutStage.CalculatingBounds) && (this.Horizontal ? (int) part.Properties.GetAlignmentHorizontal(part) : (int) part.Properties.GetAlignmentVertical(part)) == 2)
          {
            rectangle2 = !this.Horizontal ? new Rectangle(this.ContentBounds.Left, this.ContentBounds.Bottom - part.CalculatedProperties.OuterSize.Height, this.ReservedForDelta, part.CalculatedProperties.OuterSize.Height) : new Rectangle(this.ContentBounds.Right - part.CalculatedProperties.OuterSize.Width, this.ContentBounds.Top, part.CalculatedProperties.OuterSize.Width, this.ReservedForDelta);
            this.Engine.GetPartLayoutEngine(part).LayoutPart(rectangle2, part, this.LayoutContext);
            this.ContentBounds = !this.Horizontal ? Rectangle.FromLTRB(this.ContentBounds.Left, this.ContentBounds.Top, this.ContentBounds.Right, part.CalculatedProperties.OuterBounds.Top) : Rectangle.FromLTRB(this.ContentBounds.Left, this.ContentBounds.Top, part.CalculatedProperties.OuterBounds.Left, this.ContentBounds.Bottom);
          }
        }
        this.ContentBounds = !this.Horizontal ? Rectangle.FromLTRB(Math.Max(this.ContentBounds.Left + this.ReservedForDelta, this.OriginalBounds.Left), this.OriginalBounds.Top, this.OriginalBounds.Right, this.OriginalBounds.Bottom) : Rectangle.FromLTRB(this.OriginalBounds.Left, Math.Max(this.ContentBounds.Top + this.ReservedForDelta, this.OriginalBounds.Top), this.OriginalBounds.Right, this.OriginalBounds.Bottom);
        this.FirstRowPartIndex = -1;
        this.LastRowPartIndex = -1;
        this.ReservedForNear = 0;
        this.ReservedForFar = 0;
        this.ReservedForCenter = 0;
        this.ReservedForDelta = 0;
      }
    }
  }
}
