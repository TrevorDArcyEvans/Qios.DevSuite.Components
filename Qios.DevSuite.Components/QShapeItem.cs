// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QShapeItemConverter))]
  public class QShapeItem : ICloneable
  {
    private QShape m_oParentShape;
    private QShapeItemType m_eItemType;
    private PointF m_oLocation = PointF.Empty;
    private PointF m_oBezierControl1 = PointF.Empty;
    private PointF m_oBezierControl2 = PointF.Empty;
    private AnchorStyles m_eAnchor;
    private QShapeItemParts m_eSelectionParts;
    private PointF m_oLastCalculatedLineIntersection;
    private bool m_bLineVisible = true;
    private QShapeItem m_oCachedItem;

    public QShapeItem()
    {
    }

    public QShapeItem(PointF location, AnchorStyles anchor, bool lineVisible)
      : this(QShapeItemType.Point, location, (PointF) Point.Empty, (PointF) Point.Empty, anchor, lineVisible)
    {
    }

    public QShapeItem(float locationX, float locationY, AnchorStyles anchor, bool lineVisible)
      : this(QShapeItemType.Point, new PointF(locationX, locationY), PointF.Empty, PointF.Empty, anchor, lineVisible)
    {
    }

    public QShapeItem(
      PointF location,
      PointF bezierControl1,
      PointF bezierControl2,
      AnchorStyles anchor,
      bool lineVisible)
      : this(QShapeItemType.Bezier, location, bezierControl1, bezierControl2, anchor, lineVisible)
    {
    }

    public QShapeItem(
      float locationX,
      float locationY,
      float bezierControl1X,
      float bezierControl1Y,
      float bezierControl2X,
      float bezierControl2Y,
      AnchorStyles anchor,
      bool lineVisible)
      : this(QShapeItemType.Bezier, new PointF(locationX, locationY), new PointF(bezierControl1X, bezierControl1Y), new PointF(bezierControl2X, bezierControl2Y), anchor, lineVisible)
    {
    }

    public QShapeItem(
      QShapeItemType itemType,
      float locationX,
      float locationY,
      float bezierControl1X,
      float bezierControl1Y,
      float bezierControl2X,
      float bezierControl2Y,
      AnchorStyles anchor,
      bool lineVisible)
      : this(itemType, new PointF(locationX, locationY), new PointF(bezierControl1X, bezierControl1Y), new PointF(bezierControl2X, bezierControl2Y), anchor, lineVisible)
    {
    }

    public QShapeItem(
      QShapeItemType itemType,
      PointF location,
      PointF bezierControl1,
      PointF bezierControl2,
      AnchorStyles anchor,
      bool lineVisible)
    {
      this.m_eItemType = itemType;
      this.m_oLocation = location;
      this.m_oBezierControl1 = bezierControl1;
      this.m_oBezierControl2 = bezierControl2;
      this.m_eAnchor = anchor;
      this.m_bLineVisible = lineVisible;
    }

    [Browsable(false)]
    public QShape ParentShape => this.m_oParentShape;

    internal void PutParentShape(QShape shape) => this.m_oParentShape = shape;

    [Description("Identifies what kind of type this item must be")]
    [DefaultValue(QShapeItemType.Point)]
    [Category("QBehavior")]
    public QShapeItemType ItemType
    {
      get => this.m_eItemType;
      set
      {
        if (this.m_eItemType == value)
          return;
        this.m_eItemType = value;
        if (this.m_eItemType == QShapeItemType.Bezier && this.m_oParentShape != null)
        {
          PointF normalizedVector = this.m_oParentShape.CalculateNormalizedVector(this);
          float lineLength = this.m_oParentShape.CalculateLineLength(this);
          this.m_oBezierControl1 = this.m_oParentShape.RoundPoint(this.m_oLocation.X + (float) ((double) normalizedVector.X * (double) lineLength * 0.333333343267441), this.m_oLocation.Y + (float) ((double) normalizedVector.Y * (double) lineLength * 0.333333343267441));
          this.m_oBezierControl2 = this.m_oParentShape.RoundPoint(this.m_oLocation.X + (float) ((double) normalizedVector.X * (double) lineLength * 0.666666686534882), this.m_oLocation.Y + (float) ((double) normalizedVector.Y * (double) lineLength * 0.666666686534882));
        }
        this.NotifyParentShapeItemChanged();
      }
    }

    [Category("QBehavior")]
    [DefaultValue(AnchorStyles.None)]
    [Description("Anchors the item to a specified edge. When the shape is resized this item will be on the same location relative to the Anchor.")]
    public AnchorStyles Anchor
    {
      get => this.m_eAnchor;
      set
      {
        if (this.m_eAnchor == value)
          return;
        this.m_eAnchor = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    public bool ShouldSerializeLocation() => this.m_oLocation != PointF.Empty;

    public void ResetLocation() => this.Location = PointF.Empty;

    [TypeConverter(typeof (QPointFConverter))]
    [Category("QBehavior")]
    [Description("Specifies the location of the item")]
    public PointF Location
    {
      get => this.m_oLocation;
      set
      {
        if (this.m_oLocation == value)
          return;
        this.m_oLocation = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float X
    {
      get => this.m_oLocation.X;
      set
      {
        if (object.Equals((object) this.m_oLocation.X, (object) value))
          return;
        this.m_oLocation.X = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public float Y
    {
      get => this.m_oLocation.Y;
      set
      {
        if (object.Equals((object) this.m_oLocation.Y, (object) value))
          return;
        this.m_oLocation.Y = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    public bool ShouldSerializeBezierControl1() => this.m_oBezierControl1 != PointF.Empty;

    public void ResetBezierControl1() => this.BezierControl1 = PointF.Empty;

    [TypeConverter(typeof (QPointFConverter))]
    [Category("QBehavior")]
    [Description("When the ItmType is Bezier, this point specifies where the first control point of the Bezier is located")]
    public PointF BezierControl1
    {
      get => this.m_oBezierControl1;
      set
      {
        if (this.m_oBezierControl1 == value)
          return;
        this.m_oBezierControl1 = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float BezierControl1X
    {
      get => this.m_oBezierControl1.X;
      set
      {
        if (object.Equals((object) this.m_oBezierControl1.X, (object) value))
          return;
        this.m_oBezierControl1.X = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public float BezierControl1Y
    {
      get => this.m_oBezierControl1.Y;
      set
      {
        if (object.Equals((object) this.m_oBezierControl1.Y, (object) value))
          return;
        this.m_oBezierControl1.Y = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    public bool ShouldSerializeBezierControl2() => this.m_oBezierControl2 != PointF.Empty;

    public void ResetBezierControl2() => this.BezierControl2 = PointF.Empty;

    [TypeConverter(typeof (QPointFConverter))]
    [Description("When the ItmType is Bezier, this point specifies where the second control point of the Bezier is located")]
    [Category("QBehavior")]
    public PointF BezierControl2
    {
      get => this.m_oBezierControl2;
      set
      {
        if (this.m_oBezierControl2 == value)
          return;
        this.m_oBezierControl2 = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float BezierControl2X
    {
      get => this.m_oBezierControl2.X;
      set
      {
        if (object.Equals((object) this.m_oBezierControl2.X, (object) value))
          return;
        this.m_oBezierControl2.X = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float BezierControl2Y
    {
      get => this.m_oBezierControl2.Y;
      set
      {
        if (object.Equals((object) this.m_oBezierControl2.Y, (object) value))
          return;
        this.m_oBezierControl2.Y = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PointF LastCalculatedLineIntersection
    {
      get => this.m_oLastCalculatedLineIntersection;
      set => this.m_oLastCalculatedLineIntersection = value;
    }

    [Description("Gets or sets whether the Line is visible")]
    [DefaultValue(true)]
    [Category("QBehavior")]
    public bool LineVisible
    {
      get => this.m_bLineVisible;
      set
      {
        if (this.m_bLineVisible == value)
          return;
        this.m_bLineVisible = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QShapeItemParts SelectionParts
    {
      get => this.m_eSelectionParts;
      set
      {
        if (this.m_eSelectionParts == value)
          return;
        this.m_eSelectionParts = value;
        this.NotifyParentShapeItemChanged();
      }
    }

    public QLinePointResult LineCrossesRectangle(RectangleF rectangle) => this.m_oParentShape != null ? this.m_oParentShape.LineCrossesRectangle(this, rectangle) : new QLinePointResult(false, (PointF) Point.Empty);

    public QLinePointResult LineCrossesPoint(PointF point, float margin)
    {
      PointF empty = PointF.Empty;
      return this.m_oParentShape != null ? this.m_oParentShape.LineCrossesPoint(this, point, Rectangle.Empty, margin) : new QLinePointResult(false, PointF.Empty);
    }

    public QLinePointResult LineCrossesPoint(
      PointF point,
      Rectangle destination,
      float margin)
    {
      PointF empty = PointF.Empty;
      return this.m_oParentShape != null ? this.m_oParentShape.LineCrossesPoint(this, point, destination, margin) : new QLinePointResult(false, PointF.Empty);
    }

    public QShapeItemParts GetItemPartsInRectangle(
      RectangleF rectangle,
      QShapeItemParts partsToCheck,
      bool returnOnHit)
    {
      QShapeItemParts partsInRectangle = QShapeItemParts.None;
      PointF empty = PointF.Empty;
      if ((partsToCheck & QShapeItemParts.Location) == QShapeItemParts.Location && rectangle.Contains(this.Location))
      {
        partsInRectangle |= QShapeItemParts.Location;
        if (returnOnHit)
          return partsInRectangle;
      }
      if (this.ItemType == QShapeItemType.Bezier)
      {
        if ((partsToCheck & QShapeItemParts.ControlPoint1) == QShapeItemParts.ControlPoint1 && rectangle.Contains(this.BezierControl1))
        {
          partsInRectangle |= QShapeItemParts.ControlPoint1;
          if (returnOnHit)
            return partsInRectangle;
        }
        if ((partsToCheck & QShapeItemParts.ControlPoint2) == QShapeItemParts.ControlPoint2 && rectangle.Contains(this.BezierControl2))
        {
          partsInRectangle |= QShapeItemParts.ControlPoint2;
          if (returnOnHit)
            return partsInRectangle;
        }
      }
      if ((partsToCheck & QShapeItemParts.Line) == QShapeItemParts.Line && this.ItemType == QShapeItemType.Point)
      {
        QLinePointResult qlinePointResult = this.LineCrossesRectangle(rectangle);
        if (qlinePointResult.Result)
        {
          this.m_oLastCalculatedLineIntersection = qlinePointResult.Location;
          partsInRectangle |= QShapeItemParts.Line;
          if (returnOnHit)
            return partsInRectangle;
        }
      }
      return partsInRectangle;
    }

    public QShapeItemParts GetItemPartsOnPoint(
      PointF point,
      Rectangle destination,
      float margin,
      QShapeItemParts partsToCheck,
      bool returnOnHit)
    {
      QShapeItemParts partsInRectangle = this.GetItemPartsInRectangle(new RectangleF(point.X - margin / 2f, point.Y - margin / 2f, margin, margin), partsToCheck & QShapeItemParts.AllPoints, returnOnHit);
      if (partsInRectangle != QShapeItemParts.None && returnOnHit || (partsToCheck & QShapeItemParts.Line) != QShapeItemParts.Line || this.ItemType != QShapeItemType.Point && this.ItemType != QShapeItemType.Bezier)
        return partsInRectangle;
      QLinePointResult qlinePointResult = this.LineCrossesPoint(point, destination, margin);
      if (qlinePointResult.Result)
      {
        this.m_oLastCalculatedLineIntersection = qlinePointResult.Location;
        partsInRectangle |= QShapeItemParts.Line;
        if (returnOnHit)
          return partsInRectangle;
      }
      return partsInRectangle;
    }

    public void CacheCurrentLocationProperties()
    {
      if (this.m_oCachedItem == null)
        this.m_oCachedItem = new QShapeItem();
      this.CopyTo(this.m_oCachedItem);
    }

    public void RestorePropertiesFromCache()
    {
      if (this.m_oCachedItem != null)
        this.m_oCachedItem.CopyTo(this);
      this.NotifyParentShapeItemChanged();
    }

    public void MoveSelectedPartsRelativeToCache(PointF relativePoint)
    {
      if (this.SelectionParts == QShapeItemParts.None)
        return;
      this.MovePartsRelativeToCache(relativePoint, this.SelectionParts);
    }

    public PointF RoundPoint(float x, float y) => this.m_oParentShape != null ? this.m_oParentShape.RoundPoint(x, y) : new PointF(x, y);

    public void MovePartsRelativeToCache(PointF relativePoint, QShapeItemParts parts)
    {
      if (this.m_oCachedItem == null)
        return;
      if ((parts & QShapeItemParts.Location) == QShapeItemParts.Location)
        this.m_oLocation = this.RoundPoint(this.m_oCachedItem.Location.X + relativePoint.X, this.m_oCachedItem.Location.Y + relativePoint.Y);
      if ((parts & QShapeItemParts.ControlPoint1) == QShapeItemParts.ControlPoint1)
        this.m_oBezierControl1 = this.RoundPoint(this.m_oCachedItem.BezierControl1.X + relativePoint.X, this.m_oCachedItem.BezierControl1.Y + relativePoint.Y);
      if ((parts & QShapeItemParts.ControlPoint2) == QShapeItemParts.ControlPoint2)
        this.m_oBezierControl2 = this.RoundPoint(this.m_oCachedItem.BezierControl2.X + relativePoint.X, this.m_oCachedItem.BezierControl2.Y + relativePoint.Y);
      this.NotifyParentShapeItemChanged();
    }

    public void CopyTo(QShapeItem item)
    {
      item.m_eItemType = this.m_eItemType;
      item.m_oLocation = this.m_oLocation;
      item.m_oBezierControl1 = this.m_oBezierControl1;
      item.m_oBezierControl2 = this.m_oBezierControl2;
      item.m_eAnchor = this.m_eAnchor;
      item.m_eSelectionParts = this.m_eSelectionParts;
      item.m_bLineVisible = this.m_bLineVisible;
      item.m_oLastCalculatedLineIntersection = this.m_oLastCalculatedLineIntersection;
    }

    public object Clone()
    {
      QShapeItem qshapeItem = new QShapeItem();
      this.CopyTo(qshapeItem);
      return (object) qshapeItem;
    }

    internal bool IsEqualTo(object obj) => obj is QShapeItem qshapeItem && qshapeItem.ItemType == this.ItemType && !(qshapeItem.Location != this.Location) && !(qshapeItem.BezierControl1 != this.BezierControl1) && !(qshapeItem.BezierControl2 != this.BezierControl2) && qshapeItem.Anchor == this.Anchor && qshapeItem.LineVisible == this.LineVisible;

    private void NotifyParentShapeItemChanged()
    {
      if (this.m_oParentShape == null)
        return;
      this.m_oParentShape.HandleItemChanged(this);
    }
  }
}
