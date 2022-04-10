// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartCalculatedProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QPartCalculatedProperties
  {
    public const int EngineStateUnstretchedBeforeConstraints = 0;
    public const int EngineStateTableStorage = 1;
    public const int EngineStatePropertyCount = 2;
    private const int AppliedLayoutPropPadding = 0;
    private const int AppliedLayoutPropMargin = 1;
    private const int AppliedLayoutPropMinimumSize = 2;
    private const int AppliedLayoutPropMaximumSize = 3;
    private const int AppliedLayoutPropOptions = 4;
    private const int AppliedLayoutPropertyCount = 5;
    private const int CachedPaintPropScrollOffset = 0;
    private const int CachedPaintPropScrollCorrectedBounds = 1;
    private const int CachedPaintPropScrollCorrectedInnerBounds = 2;
    private const int CachedPaintPropScrollCorrectedOuterBounds = 3;
    private const int CachedPaintPropParentClipArea = 4;
    private const int CachedPaintPropertyCount = 5;
    private IQPart m_oPart;
    private QPartCalculatedProperties m_oPreviousProperties;
    private int m_iPartIndex;
    private Size m_oUnstretchedOuterSize;
    private Size m_oUnstretchedSize;
    private Size m_oUnstretchedInnerSize;
    private Size m_oActualContentSize;
    private Size m_oUnstretchedActualContentSize;
    private Size m_oInnerSize;
    private Size m_oSize;
    private Size m_oOuterSize;
    private Rectangle m_oInnerBounds;
    private Rectangle m_oBounds;
    private Rectangle m_oOuterBounds;
    private object[] m_aEngineState;
    private object[] m_aAppliedLayoutProperties;
    private object[] m_aCachedPaintProperties;
    private QPartLayoutFlags m_eLayoutFlags;

    internal QPartCalculatedProperties(IQPart part)
    {
      this.m_oPart = part;
      this.m_aAppliedLayoutProperties = new object[5];
      this.m_aCachedPaintProperties = new object[5];
    }

    public IQPart Part => this.m_oPart;

    public int PartIndex => this.m_iPartIndex;

    internal void PutPartIndex(int index) => this.m_iPartIndex = index;

    public Size UnstretchedOuterSize
    {
      get => this.m_oUnstretchedOuterSize;
      set => this.m_oUnstretchedOuterSize = value;
    }

    public Size UnstretchedSize
    {
      get => this.m_oUnstretchedSize;
      set => this.m_oUnstretchedSize = value;
    }

    public Size UnstretchedInnerSize
    {
      get => this.m_oUnstretchedInnerSize;
      set => this.m_oUnstretchedInnerSize = value;
    }

    public Size ActualContentSize
    {
      get => this.m_oActualContentSize;
      set => this.m_oActualContentSize = value;
    }

    public Size UnstretchedActualContentSize
    {
      get => this.m_oUnstretchedActualContentSize;
      set => this.m_oUnstretchedActualContentSize = value;
    }

    public Size Size
    {
      get => this.m_oSize;
      set => this.m_oSize = value;
    }

    public Size OuterSize
    {
      get => this.m_oOuterSize;
      set => this.m_oOuterSize = value;
    }

    public Size InnerSize
    {
      get => this.m_oInnerSize;
      set => this.m_oInnerSize = value;
    }

    public Rectangle InnerBounds
    {
      get => this.m_oInnerBounds;
      set => this.m_oInnerBounds = value;
    }

    public Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    public Rectangle OuterBounds
    {
      get => this.m_oOuterBounds;
      set => this.m_oOuterBounds = value;
    }

    public QPartLayoutFlags LayoutFlags
    {
      get => this.m_eLayoutFlags;
      set => this.m_eLayoutFlags = value;
    }

    public QPadding AppliedPadding
    {
      get
      {
        object appliedLayoutProperty;
        if ((appliedLayoutProperty = this.m_aAppliedLayoutProperties[0]) != null)
          return (QPadding) appliedLayoutProperty;
        QPadding appliedPadding = QPadding.SumPaddings(this.m_oPart.Properties.GetPaddings(this.m_oPart));
        this.m_aAppliedLayoutProperties[0] = (object) appliedPadding;
        return appliedPadding;
      }
      set => this.m_aAppliedLayoutProperties[0] = (object) value;
    }

    public QMargin AppliedMargin
    {
      get
      {
        object appliedLayoutProperty;
        if ((appliedLayoutProperty = this.m_aAppliedLayoutProperties[1]) != null)
          return (QMargin) appliedLayoutProperty;
        QMargin appliedMargin = QMargin.SumMarings(this.m_oPart.Properties.GetMargins(this.m_oPart));
        this.m_aAppliedLayoutProperties[1] = (object) appliedMargin;
        return appliedMargin;
      }
      set => this.m_aAppliedLayoutProperties[1] = (object) value;
    }

    public Size AppliedMinimumSize
    {
      get
      {
        object appliedLayoutProperty;
        if ((appliedLayoutProperty = this.m_aAppliedLayoutProperties[2]) != null)
          return (Size) appliedLayoutProperty;
        Size minimumSize = this.m_oPart.Properties.GetMinimumSize(this.m_oPart);
        this.m_aAppliedLayoutProperties[2] = (object) minimumSize;
        return minimumSize;
      }
      set => this.m_aAppliedLayoutProperties[2] = (object) value;
    }

    public Size AppliedMaximumSize
    {
      get
      {
        object appliedLayoutProperty;
        if ((appliedLayoutProperty = this.m_aAppliedLayoutProperties[3]) != null)
          return (Size) appliedLayoutProperty;
        Size maximumSize = this.m_oPart.Properties.GetMaximumSize(this.m_oPart);
        this.m_aAppliedLayoutProperties[3] = (object) maximumSize;
        return maximumSize;
      }
    }

    public QPartOptions AppliedOptions
    {
      get
      {
        object appliedLayoutProperty;
        if ((appliedLayoutProperty = this.m_aAppliedLayoutProperties[4]) != null)
          return (QPartOptions) appliedLayoutProperty;
        QPartOptions options = this.m_oPart.Properties.GetOptions(this.m_oPart);
        this.m_aAppliedLayoutProperties[4] = (object) options;
        return options;
      }
      set => this.m_aAppliedLayoutProperties[4] = (object) value;
    }

    private Point CalculateCachedScrollOffset()
    {
      Point cachedScrollOffset = Point.Empty;
      if (!(this.m_oPart is IQManagedLayoutParent))
      {
        IQPart parentPart1 = this.m_oPart.ParentPart;
        IQScrollablePart parentPart2 = this.m_oPart.ParentPart as IQScrollablePart;
        if (parentPart1 != null)
        {
          cachedScrollOffset = parentPart1.CalculatedProperties.CachedScrollOffset;
          if (parentPart2 != null && parentPart2.ScrollData != null)
            cachedScrollOffset.Offset(parentPart2.ScrollData.ScrollOffset.X, parentPart2.ScrollData.ScrollOffset.Y);
        }
      }
      return cachedScrollOffset;
    }

    public Point CachedScrollOffset
    {
      get
      {
        object cachedPaintProperty;
        if ((cachedPaintProperty = this.m_aCachedPaintProperties[0]) != null)
          return (Point) cachedPaintProperty;
        Point cachedScrollOffset = this.CalculateCachedScrollOffset();
        this.m_aCachedPaintProperties[0] = (object) cachedScrollOffset;
        return cachedScrollOffset;
      }
    }

    public Rectangle CachedScrollCorrectedBounds
    {
      get
      {
        object cachedPaintProperty;
        if ((cachedPaintProperty = this.m_aCachedPaintProperties[1]) != null)
          return (Rectangle) cachedPaintProperty;
        Rectangle bounds = this.Bounds;
        bounds.Offset(this.CachedScrollOffset);
        this.m_aCachedPaintProperties[1] = (object) bounds;
        return bounds;
      }
    }

    public Rectangle CachedScrollCorrectedInnerBounds
    {
      get
      {
        object cachedPaintProperty;
        if ((cachedPaintProperty = this.m_aCachedPaintProperties[2]) != null)
          return (Rectangle) cachedPaintProperty;
        Rectangle innerBounds = this.InnerBounds;
        innerBounds.Offset(this.CachedScrollOffset);
        this.m_aCachedPaintProperties[2] = (object) innerBounds;
        return innerBounds;
      }
    }

    public Rectangle CachedScrollCorrectedOuterBounds
    {
      get
      {
        object cachedPaintProperty;
        if ((cachedPaintProperty = this.m_aCachedPaintProperties[3]) != null)
          return (Rectangle) cachedPaintProperty;
        Rectangle outerBounds = this.OuterBounds;
        outerBounds.Offset(this.CachedScrollOffset);
        this.m_aCachedPaintProperties[3] = (object) outerBounds;
        return outerBounds;
      }
    }

    private QRegion CalculateCachedParentClipRegion()
    {
      QRegion parentClipRegion = (QRegion) null;
      if (!(this.m_oPart is IQManagedLayoutParent))
      {
        IQPart parentPart = this.m_oPart.ParentPart;
        if (parentPart != null)
          return parentPart.ContentClipRegion != null ? parentPart.ContentClipRegion : parentPart.CalculatedProperties.CachedParentClipRegion;
      }
      return parentClipRegion;
    }

    public QRegion CachedParentClipRegion
    {
      get
      {
        object cachedPaintProperty;
        if ((cachedPaintProperty = this.m_aCachedPaintProperties[4]) != null)
        {
          QRegion parentClipRegion = cachedPaintProperty as QRegion;
          if (!parentClipRegion.IsDisposed)
            return parentClipRegion;
        }
        QRegion parentClipRegion1 = this.CalculateCachedParentClipRegion();
        this.m_aCachedPaintProperties[4] = (object) parentClipRegion1;
        return parentClipRegion1;
      }
    }

    public bool ShouldStretchHorizontal => (this.AppliedOptions & QPartOptions.StretchHorizontal) != QPartOptions.None;

    public bool ShouldStretchVertical => (this.AppliedOptions & QPartOptions.StretchVertical) != QPartOptions.None;

    public bool ShouldStretch(bool horizontal) => !horizontal ? (this.AppliedOptions & QPartOptions.StretchVertical) != QPartOptions.None : (this.AppliedOptions & QPartOptions.StretchHorizontal) != QPartOptions.None;

    public bool ShouldStretch(QPartDirection direction) => direction != QPartDirection.Horizontal ? (this.AppliedOptions & QPartOptions.StretchVertical) != QPartOptions.None : (this.AppliedOptions & QPartOptions.StretchHorizontal) != QPartOptions.None;

    public bool ShouldShrinkHorizontal => (this.AppliedOptions & QPartOptions.ShrinkHorizontal) != QPartOptions.None;

    public bool ShouldShrinkVertical => (this.AppliedOptions & QPartOptions.ShrinkVertical) != QPartOptions.None;

    public bool ShouldShrink(bool horizontal) => !horizontal ? (this.AppliedOptions & QPartOptions.ShrinkVertical) != QPartOptions.None : (this.AppliedOptions & QPartOptions.ShrinkHorizontal) != QPartOptions.None;

    public bool ShouldShrink(QPartDirection direction) => direction != QPartDirection.Horizontal ? (this.AppliedOptions & QPartOptions.ShrinkVertical) != QPartOptions.None : (this.AppliedOptions & QPartOptions.ShrinkHorizontal) != QPartOptions.None;

    public bool IsSetToMinimumWidth => this.m_oSize.Width <= this.AppliedMinimumSize.Width;

    public bool IsSetToMinimumHeight => this.m_oSize.Height <= this.AppliedMinimumSize.Height;

    public bool IsSetToMaximumWidth
    {
      get
      {
        Size appliedMaximumSize = this.AppliedMaximumSize;
        return appliedMaximumSize.Width > 0 && this.m_oSize.Width >= appliedMaximumSize.Width;
      }
    }

    public bool IsSetToMaximumHeight
    {
      get
      {
        Size appliedMaximumSize = this.AppliedMaximumSize;
        return appliedMaximumSize.Height > 0 && this.m_oSize.Height >= appliedMaximumSize.Height;
      }
    }

    public Size GetMinimumOuterSize()
    {
      Size size1 = this.ApplyUnstretchedMinimumSize(this.AppliedMinimumSize);
      if (size1.Width > 0 || size1.Height > 0)
      {
        Size size2 = this.AppliedMargin.InflateSizeWithMargin(size1, true, true);
        if (size1.Width > 0)
          size1.Width = size2.Width;
        if (size1.Height > 0)
          size1.Height = size2.Height;
      }
      return size1;
    }

    public Size GetMinimumInnerSize()
    {
      Size minimumInnerSize = this.AppliedMinimumSize;
      if (minimumInnerSize.Width > 0 || minimumInnerSize.Height > 0)
      {
        minimumInnerSize = this.AppliedPadding.InflateSizeWithPadding(this.ApplyUnstretchedMinimumSize(this.m_oSize), false, true);
        minimumInnerSize.Width = Math.Max(minimumInnerSize.Width, 0);
        minimumInnerSize.Height = Math.Max(minimumInnerSize.Height, 0);
      }
      return minimumInnerSize;
    }

    public Size GetMaximumOuterSize()
    {
      Size appliedMaximumSize = this.AppliedMaximumSize;
      if (appliedMaximumSize.Width > 0 || appliedMaximumSize.Height > 0)
      {
        Size size = this.AppliedMargin.InflateSizeWithMargin(appliedMaximumSize, true, true);
        if (appliedMaximumSize.Width > 0)
          appliedMaximumSize.Width = size.Width;
        if (appliedMaximumSize.Height > 0)
          appliedMaximumSize.Height = size.Height;
      }
      return appliedMaximumSize;
    }

    public Size GetMaximumInnerSize()
    {
      Size appliedMaximumSize = this.AppliedMaximumSize;
      if (appliedMaximumSize.Width > 0 || appliedMaximumSize.Height > 0)
      {
        Size size = this.AppliedPadding.InflateSizeWithPadding(appliedMaximumSize, false, true);
        if (appliedMaximumSize.Width > 0)
          appliedMaximumSize.Width = size.Width;
        if (appliedMaximumSize.Height > 0)
          appliedMaximumSize.Height = size.Height;
      }
      return appliedMaximumSize;
    }

    public Rectangle GetBounds(QPartBoundsType boundsType)
    {
      switch (boundsType)
      {
        case QPartBoundsType.InnerBounds:
          return this.m_oInnerBounds;
        case QPartBoundsType.Bounds:
          return this.m_oBounds;
        case QPartBoundsType.OuterBounds:
          return this.m_oOuterBounds;
        default:
          return Rectangle.Empty;
      }
    }

    public bool HasLayoutFlag(QPartLayoutFlags flag) => (this.m_eLayoutFlags & flag) == flag;

    public void SetLayoutFlag(QPartLayoutFlags flag, bool value)
    {
      if (value)
        this.m_eLayoutFlags |= flag;
      else
        this.m_eLayoutFlags &= ~flag;
    }

    public ValueType GetEngineStatePropertyAsValue(int index, ValueType defaultValue) => this.m_aEngineState != null && this.m_aEngineState[index] != null ? (ValueType) this.m_aEngineState[index] : defaultValue;

    public object GetEngineStateProperty(int index) => this.m_aEngineState != null ? this.m_aEngineState[index] : (object) null;

    public void SetEngineStateProperty(int index, object value)
    {
      if (this.m_aEngineState == null)
        this.m_aEngineState = new object[2];
      this.m_aEngineState[index] = value;
    }

    public void ClearCalculatedSizeProperties()
    {
      this.m_oSize = Size.Empty;
      this.m_oActualContentSize = Size.Empty;
      this.m_oUnstretchedInnerSize = Size.Empty;
      this.m_oUnstretchedOuterSize = Size.Empty;
      this.m_oUnstretchedSize = Size.Empty;
      this.m_oOuterSize = Size.Empty;
      this.m_oInnerSize = Size.Empty;
      this.m_eLayoutFlags = QPartLayoutFlags.None;
    }

    public void ClearBoundsProperties()
    {
      this.m_oBounds = Rectangle.Empty;
      this.m_oInnerBounds = Rectangle.Empty;
      this.m_oOuterBounds = Rectangle.Empty;
    }

    public void ClearEngineState()
    {
      if (this.m_aEngineState == null)
        return;
      Array.Clear((Array) this.m_aEngineState, 0, this.m_aEngineState.Length);
    }

    public void ClearAppliedLayoutProperties() => Array.Clear((Array) this.m_aAppliedLayoutProperties, 0, this.m_aAppliedLayoutProperties.Length);

    public void ClearCachedPaintProperties() => Array.Clear((Array) this.m_aCachedPaintProperties, 0, this.m_aCachedPaintProperties.Length);

    public Size ApplyMinMaxRules(Size partSize) => this.ApplyMinMaxRules(partSize, true);

    public Size ApplyMinMaxRules(Size partSize, bool applyUnstretchedMinimumSize)
    {
      Size partSize1 = this.AppliedMinimumSize;
      Size appliedMaximumSize = this.AppliedMaximumSize;
      if (applyUnstretchedMinimumSize)
        partSize1 = this.ApplyUnstretchedMinimumSize(partSize1);
      partSize.Width = Math.Max(partSize.Width, partSize1.Width);
      partSize.Height = Math.Max(partSize.Height, partSize1.Height);
      if (appliedMaximumSize.Width > 0)
        partSize.Width = Math.Min(partSize.Width, appliedMaximumSize.Width);
      if (appliedMaximumSize.Height > 0)
        partSize.Height = Math.Min(partSize.Height, appliedMaximumSize.Height);
      return partSize;
    }

    public Size ApplyUnstretchedMinimumSize(Size partSize)
    {
      if (!this.ShouldShrinkHorizontal)
        partSize.Width = Math.Max(this.m_oUnstretchedSize.Width, partSize.Width);
      if (!this.ShouldShrinkVertical)
        partSize.Height = Math.Max(this.m_oUnstretchedSize.Height, partSize.Height);
      return partSize;
    }

    public Size GetInnerSizeBasedOnSize(Size size) => this.AppliedPadding.InflateSizeWithPadding(this.ApplyMinMaxRules(size), false, true);

    public Size GetInnerSizeBasedOnOuterSize(Size outerSize) => this.AppliedPadding.InflateSizeWithPadding(this.ApplyMinMaxRules(this.AppliedMargin.InflateSizeWithMargin(outerSize, false, true)), false, true);

    public Size GetOuterSizeBasedOnInnerSize(Size innerSize) => this.AppliedMargin.InflateSizeWithMargin(this.ApplyMinMaxRules(this.AppliedPadding.InflateSizeWithPadding(innerSize, true, true)), true, true);

    public void SetSizesBasedOnOuterSize(Size outerSize, bool adjustUnstretchedSize)
    {
      QPadding appliedPadding = this.AppliedPadding;
      QMargin appliedMargin = this.AppliedMargin;
      Size size = this.ApplyMinMaxRules(appliedMargin.InflateSizeWithMargin(outerSize, false, true), !adjustUnstretchedSize);
      this.m_oInnerSize = appliedPadding.InflateSizeWithPadding(size, false, true);
      this.m_oOuterSize = appliedMargin.InflateSizeWithMargin(size, true, true);
      this.m_oSize = size;
      if (!adjustUnstretchedSize)
        return;
      this.m_oUnstretchedInnerSize = this.m_oInnerSize;
      this.m_oUnstretchedOuterSize = this.m_oOuterSize;
      this.m_oUnstretchedSize = this.m_oSize;
    }

    public void SetSizesBasedOnSize(Size size, bool adjustUnstretchedSize)
    {
      QPadding appliedPadding = this.AppliedPadding;
      QMargin appliedMargin = this.AppliedMargin;
      Size size1 = this.ApplyMinMaxRules(size, !adjustUnstretchedSize);
      this.m_oInnerSize = appliedPadding.InflateSizeWithPadding(size1, false, true);
      this.m_oOuterSize = appliedMargin.InflateSizeWithMargin(size1, true, true);
      this.m_oSize = size1;
      if (!adjustUnstretchedSize)
        return;
      this.m_oUnstretchedInnerSize = this.m_oInnerSize;
      this.m_oUnstretchedOuterSize = this.m_oOuterSize;
      this.m_oUnstretchedSize = this.m_oSize;
    }

    public void SetSizesBasedOnInnerSize(Size innerSize, bool adjustUnstretchedSize)
    {
      QPadding appliedPadding = this.AppliedPadding;
      QMargin appliedMargin = this.AppliedMargin;
      Size size = this.ApplyMinMaxRules(appliedPadding.InflateSizeWithPadding(innerSize, true, true), !adjustUnstretchedSize);
      this.m_oInnerSize = appliedPadding.InflateSizeWithPadding(size, false, true);
      this.m_oOuterSize = appliedMargin.InflateSizeWithMargin(size, true, true);
      this.m_oSize = size;
      if (!adjustUnstretchedSize)
        return;
      this.m_oUnstretchedInnerSize = this.m_oInnerSize;
      this.m_oUnstretchedOuterSize = this.m_oOuterSize;
      this.m_oUnstretchedSize = this.m_oSize;
    }

    public void SetUnstretchedSizesBasedOnInnerSize(Size innerSize)
    {
      QPadding appliedPadding = this.AppliedPadding;
      QMargin appliedMargin = this.AppliedMargin;
      Size size = this.ApplyMinMaxRules(appliedPadding.InflateSizeWithPadding(innerSize, true, true), false);
      this.m_oUnstretchedInnerSize = appliedPadding.InflateSizeWithPadding(size, false, true);
      this.m_oUnstretchedOuterSize = appliedMargin.InflateSizeWithMargin(size, true, true);
      this.m_oUnstretchedSize = size;
    }

    public void SetUnstretchedSizesBasedOnSize(Size size)
    {
      QPadding appliedPadding = this.AppliedPadding;
      QMargin appliedMargin = this.AppliedMargin;
      Size size1 = this.ApplyMinMaxRules(size, false);
      this.m_oUnstretchedInnerSize = appliedPadding.InflateSizeWithPadding(size1, false, true);
      this.m_oUnstretchedOuterSize = appliedMargin.InflateSizeWithMargin(size1, true, true);
      this.m_oUnstretchedSize = size1;
    }

    public void SetUnstretchedSizesBasedOnOuterSize(Size outerSize)
    {
      QPadding appliedPadding = this.AppliedPadding;
      QMargin appliedMargin = this.AppliedMargin;
      Size size = this.ApplyMinMaxRules(appliedMargin.InflateSizeWithMargin(outerSize, false, true), false);
      this.m_oUnstretchedInnerSize = appliedPadding.InflateSizeWithPadding(size, false, true);
      this.m_oUnstretchedOuterSize = appliedMargin.InflateSizeWithMargin(size, true, true);
      this.m_oUnstretchedSize = size;
    }

    public void SetBoundsBasedOnOuterBounds(Rectangle outerBounds)
    {
      QPadding appliedPadding = this.AppliedPadding;
      Rectangle rectangle = this.AppliedMargin.InflateRectangleWithMargin(outerBounds, false, DockStyle.None);
      this.m_oOuterBounds = outerBounds;
      this.m_oBounds = rectangle;
      this.m_oInnerBounds = appliedPadding.InflateRectangleWithPadding(rectangle, false, DockStyle.None);
    }

    public static QPartCalculatedProperties PushCalculatedProperties(
      QPartCalculatedProperties currentProperties)
    {
      return new QPartCalculatedProperties(currentProperties.m_oPart)
      {
        m_oPreviousProperties = currentProperties
      };
    }

    public static QPartCalculatedProperties PullCalculatedProperties(
      QPartCalculatedProperties currentProperties)
    {
      return currentProperties != null && currentProperties.m_oPreviousProperties != null ? currentProperties.m_oPreviousProperties : throw new InvalidOperationException(QResources.GetException("QPartCalculatedProperties_PullCalculatedProperties_NoProperties"));
    }
  }
}
