// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShape
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DesignerSerializer(typeof (QShapeCodeSerializer), typeof (CodeDomSerializer))]
  [ToolboxBitmap(typeof (QShape), "Resources.ControlImages.QShape.bmp")]
  [Designer(typeof (QShapeDesigner), typeof (IDesigner))]
  [TypeConverter(typeof (QShapeConverter))]
  public class QShape : Component, ICloneable, IQWeakEventPublisher, IQPadding, IQMargin
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bConvertedFromString;
    private QBaseShapeType m_eBaseShapeType;
    private QShape m_oBaseShape;
    private QShapeType m_eShapeType;
    private Point m_oFocusPoint = Point.Empty;
    private AnchorStyles m_eFocusPointAnchor = AnchorStyles.Top | AnchorStyles.Left;
    private QShapeItemCollection m_oItems;
    private Size m_oSize = new Size(100, 20);
    private string m_sShapeName;
    private bool m_bIsChanged;
    private Rectangle m_oContentBounds = new Rectangle(2, 2, 96, 16);
    private Size m_oMinimumSize = Size.Empty;
    private int m_iPrecision;
    private bool m_bSuspendChange;
    private static QBaseShapeCollection m_oBaseShapes = new QBaseShapeCollection();
    private QWeakDelegate m_oShapeChangedDelegate;

    public QShape()
      : this(false)
    {
    }

    public QShape(QBaseShapeType shapeType)
      : this(false)
    {
      this.ClonedBaseShapeType = shapeType;
      this.ConvertedFromString = true;
    }

    internal QShape(bool isBaseShape)
    {
      this.InternalConstruct();
      if (isBaseShape)
        return;
      this.SetBaseShape(QShape.BaseShapes[QBaseShapeType.RectangleShape], true, false);
    }

    [QWeakEvent]
    public event EventHandler ShapeChanged
    {
      add => this.m_oShapeChangedDelegate = QWeakDelegate.Combine(this.m_oShapeChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oShapeChangedDelegate = QWeakDelegate.Remove(this.m_oShapeChangedDelegate, (Delegate) value);
    }

    [DefaultValue(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool ConvertedFromString
    {
      get => this.m_bConvertedFromString;
      set => this.m_bConvertedFromString = value;
    }

    internal QShapeDesigner RetrieveActiveDesigner() => this.Site != null && this.Site.GetService(typeof (IDesignerHost)) is IDesignerHost service ? service.GetDesigner((IComponent) this) as QShapeDesigner : (QShapeDesigner) null;

    private void CloneBaseShape() => this.BaseShape.CopyTo(this);

    private void InternalConstruct() => this.m_oItems = new QShapeItemCollection(this, 2);

    public static QBaseShapeCollection BaseShapes => QShape.m_oBaseShapes;

    internal bool DiffersFromBaseShape => !this.IsEqualTo((object) this.BaseShape);

    public bool ShouldSerializeBaseShapeType() => !this.ShouldSerializeClonedBaseShapeType();

    [Browsable(false)]
    [Description("Gets or sets the type of the shape")]
    public virtual QBaseShapeType BaseShapeType
    {
      get => this.m_eBaseShapeType;
      set => this.SetBaseShape(QShape.BaseShapes[value], true, false);
    }

    public bool ShouldSerializeClonedBaseShapeType() => !this.ShouldSerializeItems();

    [Browsable(false)]
    [Description("Gets or sets the type of the shape that this shape should be cloned of")]
    public QBaseShapeType ClonedBaseShapeType
    {
      get => this.m_eBaseShapeType;
      set
      {
        this.SetBaseShape(QShape.BaseShapes[value], false, false);
        this.CloneBaseShape();
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Obsolete since Version 1.1.0.0, Use the SmoothingMode from the QShapeDesigner")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QSmoothingMode DesignSmoothingMode
    {
      get
      {
        QShapeDesigner qshapeDesigner = this.RetrieveActiveDesigner();
        return qshapeDesigner == null ? QSmoothingMode.None : qshapeDesigner.SmoothingMode;
      }
      set
      {
        QShapeDesigner qshapeDesigner = this.RetrieveActiveDesigner();
        if (qshapeDesigner == null)
          return;
        qshapeDesigner.SmoothingMode = value;
      }
    }

    [Browsable(false)]
    public bool IsChanged => this.m_bIsChanged;

    public PointF RoundPoint(float x, float y) => new PointF((float) Math.Round((double) x, this.m_iPrecision), (float) Math.Round((double) y, this.m_iPrecision));

    public PointF RoundPoint(PointF point) => this.RoundPoint(point.X, point.Y);

    public void ResetChanged() => this.m_bIsChanged = false;

    public void SuspendChange() => this.m_bSuspendChange = true;

    public void ResumeChange(bool raiseEvent)
    {
      this.m_bSuspendChange = false;
      this.HandleShapeChanged(raiseEvent);
    }

    public bool ShouldSerializeShapeType() => this.ShapeType != this.BaseShape.ShapeType;

    public void ResetShapeType() => this.ShapeType = this.BaseShape.ShapeType;

    [Description("Gets or sets the type of the shape")]
    [Category("QBehavior")]
    public virtual QShapeType ShapeType
    {
      get => this.m_eShapeType;
      set => this.m_eShapeType = value;
    }

    public bool ShouldSerializeShapeName() => string.Compare(this.ShapeName, this.BaseShape.ShapeName, false, CultureInfo.InvariantCulture) != 0;

    public void ResetShapeName() => this.ShapeName = this.BaseShape.ShapeName;

    [Category("QBehavior")]
    [Description("Gets or sets the name of the shape")]
    public string ShapeName
    {
      get => this.m_sShapeName;
      set
      {
        this.m_sShapeName = value;
        this.HandleShapeChanged();
      }
    }

    public bool ShouldSerializeItems()
    {
      if (this.Items.Count != this.BaseShape.Items.Count)
        return true;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (!this.Items[index].IsEqualTo((object) this.BaseShape.Items[index]))
          return true;
      }
      return false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public QShapeItemCollection Items => this.m_oItems;

    public bool ShouldSerializePrecision() => this.Precision != this.BaseShape.Precision;

    public void ResetPrecision() => this.Precision = this.BaseShape.Precision;

    [Description("Contains the precision that is used (in decimals) to calculate values in.")]
    [Category("QBehavior")]
    public int Precision
    {
      get => this.m_iPrecision;
      set => this.m_iPrecision = value >= 0 && value <= 15 ? value : throw new InvalidOperationException(QResources.GetException("QShape_PrecisionInvalid"));
    }

    public bool ShouldSerializeSize() => !this.Size.Equals((object) this.BaseShape.Size);

    public void ResetSize() => this.Size = this.BaseShape.Size;

    [Category("QBehavior")]
    [Description("Gets or sets the size of the shape")]
    public Size Size
    {
      get => this.m_oSize;
      set
      {
        this.m_oSize = value;
        this.HandleShapeChanged();
      }
    }

    public bool ShouldSerializeFocusPoint() => !this.FocusPoint.Equals((object) this.BaseShape.FocusPoint);

    public void ResetFocusPoint() => this.FocusPoint = this.BaseShape.FocusPoint;

    [Category("QBehavior")]
    [Description("Gets or sets the focus point of the shape")]
    public Point FocusPoint
    {
      get => this.m_oFocusPoint;
      set
      {
        this.m_oFocusPoint = value;
        this.HandleShapeChanged();
      }
    }

    public bool ShouldSerializeFocusPointAnchor() => !this.FocusPointAnchor.Equals((object) this.BaseShape.FocusPointAnchor);

    public void ResetFocusPointAnchor() => this.FocusPointAnchor = this.BaseShape.FocusPointAnchor;

    [Description("Gets or sets the focus point anchors of the shape")]
    [Category("QBehavior")]
    public AnchorStyles FocusPointAnchor
    {
      get => this.m_eFocusPointAnchor;
      set
      {
        this.m_eFocusPointAnchor = value;
        this.HandleShapeChanged();
      }
    }

    public bool ShouldSerializeContentBounds() => !this.ContentBounds.Equals((object) this.BaseShape.ContentBounds);

    public void ResetContentBounds() => this.ContentBounds = this.BaseShape.ContentBounds;

    [Description("Gets or sets the bounds of the contents. This is used to determine where the contents must be located on the shape.")]
    [Category("QBehavior")]
    public Rectangle ContentBounds
    {
      get => this.m_oContentBounds;
      set
      {
        this.m_oContentBounds = value;
        this.HandleShapeChanged();
      }
    }

    internal void SetBaseShape(QShape baseShape, bool copyValues, bool copyItems)
    {
      if (baseShape == null)
        return;
      this.m_oBaseShape = baseShape;
      if (copyValues)
        this.m_oBaseShape.CopyTo(this, copyItems);
      this.m_eBaseShapeType = this.m_oBaseShape.BaseShapeType;
    }

    internal QShape BaseShape => this.m_oBaseShape;

    public Size CalculateShapeSize(Size contentSize, bool horizontal) => this.InflateSize(contentSize, true, horizontal);

    public Size InflateSize(Size size, bool inflate, bool horizontal) => new QPadding(this.m_oContentBounds.Left, this.m_oContentBounds.Top, this.m_oSize.Height - this.m_oContentBounds.Bottom, this.m_oSize.Width - this.m_oContentBounds.Right).InflateSizeWithPadding(size, inflate, horizontal);

    public Rectangle CalculateContentBounds(Rectangle shapeBounds, DockStyle dockStyle) => this.InflateRectangle(shapeBounds, false, dockStyle);

    public Rectangle InflateNegativeMargins(Rectangle rectangle)
    {
      int top = this.m_oContentBounds.Top;
      int left = this.m_oContentBounds.Left;
      int num1 = this.m_oSize.Height - this.m_oContentBounds.Bottom;
      int num2 = this.m_oSize.Width - this.m_oContentBounds.Right;
      if (top < 0)
        rectangle.Height -= top;
      if (num1 < 0)
        rectangle.Height -= num1;
      if (left < 0)
        rectangle.Width -= left;
      if (num2 < 0)
        rectangle.Width -= num2;
      if (top < 0)
        rectangle.Y += top;
      if (left < 0)
        rectangle.X += left;
      return rectangle;
    }

    public Rectangle InflateRectangle(
      Rectangle rectangle,
      bool inflate,
      DockStyle dockStyle)
    {
      int left = this.m_oContentBounds.Left;
      int num = this.m_oSize.Width - this.m_oContentBounds.Right;
      return new QPadding(dockStyle != DockStyle.Left ? left : num, this.m_oContentBounds.Top, this.m_oSize.Height - this.m_oContentBounds.Bottom, dockStyle != DockStyle.Left ? num : left).InflateRectangleWithPadding(rectangle, inflate, dockStyle);
    }

    public QMargin ToMargin() => new QMargin(this.m_oContentBounds.Left, this.m_oContentBounds.Top, this.m_oSize.Height - this.m_oContentBounds.Bottom, this.m_oSize.Width - this.m_oContentBounds.Right);

    public QPadding ToPadding() => new QPadding(this.m_oContentBounds.Left, this.m_oContentBounds.Top, this.m_oSize.Height - this.m_oContentBounds.Bottom, this.m_oSize.Width - this.m_oContentBounds.Right);

    [Browsable(false)]
    public Size MinimumSize => this.m_oMinimumSize;

    public QShapeItem GetNextItem(QShapeItem item) => this.m_oItems.GetNextItem(item);

    public QShapeItem GetPreviousItem(QShapeItem item) => this.m_oItems.GetPreviousItem(item);

    public PointF CalculateNormalizedVector(QShapeItem item)
    {
      QShapeItem nextItem = this.GetNextItem(item);
      return nextItem != null ? QMath.CalculateNormalizedVector(item.Location, nextItem.Location) : PointF.Empty;
    }

    public float CalculateLineLength(QShapeItem item)
    {
      QShapeItem nextItem = this.GetNextItem(item);
      return nextItem != null ? QMath.CalculateLineLength(item.Location, nextItem.Location) : 0.0f;
    }

    public QLinePointResult LineCrossesRectangle(
      QShapeItem item,
      RectangleF rectangle)
    {
      QShapeItem nextItem = this.GetNextItem(item);
      return nextItem != null ? QMath.LineCrossesRectangle(item.Location, nextItem.Location, rectangle) : new QLinePointResult(false, PointF.Empty);
    }

    public QLinePointResult LineCrossesPoint(
      QShapeItem item,
      PointF point,
      Rectangle destination,
      float margin)
    {
      PointF empty = (PointF) Point.Empty;
      QShapeItem nextItem = this.GetNextItem(item);
      if (nextItem == null)
        return new QLinePointResult(false, PointF.Empty);
      GraphicsPath path = new GraphicsPath();
      if (item.ItemType == QShapeItemType.Point)
        path.AddLine(destination.IsEmpty ? item.Location : this.TranslatePoint(item.Location, destination, AnchorStyles.None, true), destination.IsEmpty ? nextItem.Location : this.TranslatePoint(nextItem.Location, destination, AnchorStyles.None, true));
      else if (item.ItemType == QShapeItemType.Bezier)
        path.AddBezier(destination.IsEmpty ? item.Location : this.TranslatePoint(item.Location, destination, AnchorStyles.None, true), destination.IsEmpty ? item.BezierControl1 : this.TranslatePoint(item.BezierControl1, destination, AnchorStyles.None, true), destination.IsEmpty ? item.BezierControl2 : this.TranslatePoint(item.BezierControl2, destination, AnchorStyles.None, true), destination.IsEmpty ? nextItem.Location : this.TranslatePoint(nextItem.Location, destination, AnchorStyles.None, true));
      float num = margin;
      if (this.Size.Width > 0 && !destination.IsEmpty)
        num = margin * (float) (destination.Width / this.Size.Width);
      Pen pen1 = new Pen(Color.Black, num);
      bool result = path.IsOutlineVisible(destination.IsEmpty ? point : this.TranslatePoint(point, destination, AnchorStyles.None, true), pen1);
      PointF pointF = point;
      if (result && !destination.IsEmpty)
      {
        Pen pen2 = new Pen(Color.Black, 1f);
        pointF = this.LocateCrossPoint(pointF, destination, path, num, pen2);
        if (pointF.IsEmpty)
          result = false;
        else
          pointF = this.TranslatePoint(pointF, destination, new Rectangle(Point.Empty, this.Size), AnchorStyles.None, true);
        pen2.Dispose();
      }
      QLinePointResult qlinePointResult = new QLinePointResult(result, pointF);
      pen1.Dispose();
      path.Dispose();
      return qlinePointResult;
    }

    private PointF LocateCrossPoint(
      PointF point,
      Rectangle destination,
      GraphicsPath path,
      float margin,
      Pen pen)
    {
      PointF pointF = this.TranslatePoint(point, destination, AnchorStyles.None, true);
      float num = (float) ((double) margin / 2.0 + 1.0);
      PointF point1;
      for (int index = 0; (double) index < (double) num; ++index)
      {
        point1 = new PointF(pointF.X - (float) index, pointF.Y - (float) index);
        if (path.IsOutlineVisible(point1, pen))
          return point1;
      }
      for (int index = 0; (double) index < (double) num; ++index)
      {
        point1 = new PointF(pointF.X + (float) index, pointF.Y - (float) index);
        if (path.IsOutlineVisible(point1, pen))
          return point1;
      }
      for (int index = 0; (double) index < (double) num; ++index)
      {
        point1 = new PointF(pointF.X + (float) index, pointF.Y + (float) index);
        if (path.IsOutlineVisible(point1, pen))
          return point1;
      }
      for (int index = 0; (double) index < (double) num; ++index)
      {
        point1 = new PointF(pointF.X - (float) index, pointF.Y + (float) index);
        if (path.IsOutlineVisible(point1, pen))
          return point1;
      }
      return PointF.Empty;
    }

    private bool ItemIsAnchored(QShapeItem item, AnchorStyles anchorStyle) => item != null && (anchorStyle & item.Anchor) == anchorStyle;

    public void MirrorShape(QShapeMirrorOption options)
    {
      if (this.Items.Count == 0)
        return;
      float num = 0.0f;
      bool flag = options == QShapeMirrorOption.LeftToRight || options == QShapeMirrorOption.RightToLeft;
      AnchorStyles anchorStyle1 = AnchorStyles.None;
      AnchorStyles anchorStyle2 = AnchorStyles.None;
      switch (options)
      {
        case QShapeMirrorOption.LeftToRight:
          anchorStyle1 = AnchorStyles.Left;
          anchorStyle2 = AnchorStyles.Right;
          num = (float) this.m_oSize.Width / 2f;
          break;
        case QShapeMirrorOption.RightToLeft:
          anchorStyle1 = AnchorStyles.Right;
          anchorStyle2 = AnchorStyles.Left;
          num = (float) this.m_oSize.Width / 2f;
          break;
        case QShapeMirrorOption.TopToBottom:
          anchorStyle1 = AnchorStyles.Top;
          anchorStyle2 = AnchorStyles.Bottom;
          num = (float) this.m_oSize.Height / 2f;
          break;
        case QShapeMirrorOption.BottomToTop:
          anchorStyle1 = AnchorStyles.Bottom;
          anchorStyle2 = AnchorStyles.Top;
          num = (float) this.m_oSize.Height / 2f;
          break;
      }
      QShapeItem qshapeItem1 = (QShapeItem) null;
      QShapeItem qshapeItem2 = (QShapeItem) null;
      QShapeItemCollection qshapeItemCollection1 = new QShapeItemCollection(2);
      QShapeItemCollection qshapeItemCollection2 = new QShapeItemCollection(2);
      this.SuspendChange();
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.ItemIsAnchored(this.Items[index], anchorStyle1) || !this.ItemIsAnchored(this.Items[index], anchorStyle2) && (options == QShapeMirrorOption.LeftToRight && (double) this.Items[index].X < (double) num || options == QShapeMirrorOption.RightToLeft && (double) this.Items[index].X > (double) num || options == QShapeMirrorOption.TopToBottom && (double) this.Items[index].Y < (double) num || options == QShapeMirrorOption.BottomToTop && (double) this.Items[index].Y > (double) num))
        {
          if (index == 0)
          {
            QShapeItem qshapeItem3 = this.Items[0];
            if (qshapeItem2 == qshapeItem3)
              return;
            this.Items.RemoveAt(index);
            this.Items.Add(qshapeItem3);
            --index;
            if (qshapeItem2 == null)
              qshapeItem2 = qshapeItem3;
          }
          else
          {
            if (qshapeItemCollection2.Count > 0)
              qshapeItem1 = qshapeItemCollection2[qshapeItemCollection2.Count - 1];
            qshapeItemCollection2.Add(this.Items[index]);
            QShapeItem qshapeItem4 = (QShapeItem) this.Items[index].Clone();
            if (this.ItemIsAnchored(this.Items[index], anchorStyle1))
            {
              qshapeItem4.Anchor &= ~anchorStyle1;
              qshapeItem4.Anchor |= anchorStyle2;
            }
            if (flag)
              qshapeItem4.X = (float) this.Size.Width - qshapeItem4.X;
            else
              qshapeItem4.Y = (float) this.Size.Height - qshapeItem4.Y;
            if (qshapeItem1 != null && qshapeItem1.ItemType == QShapeItemType.Bezier)
            {
              qshapeItem4.ItemType = QShapeItemType.Bezier;
              if (flag)
              {
                qshapeItem4.BezierControl1 = new PointF((float) this.Size.Width - qshapeItem1.BezierControl2X, qshapeItem1.BezierControl2Y);
                qshapeItem4.BezierControl2 = new PointF((float) this.Size.Width - qshapeItem1.BezierControl1X, qshapeItem1.BezierControl1Y);
              }
              else
              {
                qshapeItem4.BezierControl1 = new PointF(qshapeItem1.BezierControl2X, (float) this.Size.Height - qshapeItem1.BezierControl2Y);
                qshapeItem4.BezierControl2 = new PointF(qshapeItem1.BezierControl1X, (float) this.Size.Height - qshapeItem1.BezierControl1Y);
              }
            }
            else
              qshapeItem4.ItemType = QShapeItemType.Point;
            qshapeItemCollection1.Insert(0, qshapeItem4);
          }
        }
      }
      if (qshapeItemCollection2.Count > 0 && qshapeItemCollection1.Count > 0)
      {
        QShapeItem qshapeItem5 = qshapeItemCollection2[qshapeItemCollection2.Count - 1];
        if (qshapeItem5.ItemType == QShapeItemType.Bezier)
          qshapeItem5.BezierControl2 = !flag ? new PointF(qshapeItem5.BezierControl1X, (float) this.Size.Height - qshapeItem5.BezierControl1Y) : new PointF((float) this.Size.Width - qshapeItem5.BezierControl1X, qshapeItem5.BezierControl1Y);
        QShapeItem previousItem = this.Items.GetPreviousItem(qshapeItemCollection2[0]);
        if (previousItem != null && previousItem.ItemType == QShapeItemType.Bezier)
        {
          QShapeItem qshapeItem6 = qshapeItemCollection1[qshapeItemCollection1.Count - 1];
          qshapeItem6.ItemType = QShapeItemType.Bezier;
          if (flag)
          {
            qshapeItem6.BezierControl1 = new PointF((float) this.Size.Width - previousItem.BezierControl2X, previousItem.BezierControl2Y);
            qshapeItem6.BezierControl2 = previousItem.BezierControl2;
          }
          else
          {
            qshapeItem6.BezierControl1 = new PointF(previousItem.BezierControl2X, (float) this.Size.Height - previousItem.BezierControl2Y);
            qshapeItem6.BezierControl2 = previousItem.BezierControl2;
          }
        }
      }
      this.Items.Clear();
      for (int index1 = 0; index1 < qshapeItemCollection2.Count; ++index1)
      {
        this.Items.Add(qshapeItemCollection2[index1]);
        int index2 = qshapeItemCollection2.Count - 2 - index1;
        if (index2 < qshapeItemCollection1.Count && index2 >= 0)
          qshapeItemCollection1[index2].LineVisible = qshapeItemCollection2[index1].LineVisible;
        else
          qshapeItemCollection1[index1].LineVisible = qshapeItemCollection2[index1].LineVisible;
      }
      for (int index = 0; index < qshapeItemCollection1.Count; ++index)
        this.Items.Add(qshapeItemCollection1[index]);
      this.ResumeChange(true);
    }

    public void FlipShape(Orientation orientation)
    {
      this.SuspendChange();
      switch (orientation)
      {
        case Orientation.Horizontal:
          for (int index = 0; index < this.Items.Count; ++index)
          {
            QShapeItem qshapeItem = this.Items[index];
            qshapeItem.X = (float) this.Size.Width - qshapeItem.X;
            if (this.ItemIsAnchored(qshapeItem, AnchorStyles.Left) && !this.ItemIsAnchored(qshapeItem, AnchorStyles.Right))
            {
              qshapeItem.Anchor &= ~AnchorStyles.Left;
              qshapeItem.Anchor |= AnchorStyles.Right;
            }
            else if (this.ItemIsAnchored(qshapeItem, AnchorStyles.Right) && !this.ItemIsAnchored(qshapeItem, AnchorStyles.Left))
            {
              qshapeItem.Anchor &= ~AnchorStyles.Right;
              qshapeItem.Anchor |= AnchorStyles.Left;
            }
            if (qshapeItem.ItemType == QShapeItemType.Bezier)
            {
              qshapeItem.BezierControl1X = (float) this.Size.Width - qshapeItem.BezierControl1X;
              qshapeItem.BezierControl2X = (float) this.Size.Width - qshapeItem.BezierControl2X;
            }
          }
          this.m_oContentBounds.X = this.Size.Width - this.ContentBounds.Right;
          this.m_oFocusPoint.X = this.Size.Width - this.FocusPoint.X;
          break;
        case Orientation.Vertical:
          for (int index = 0; index < this.Items.Count; ++index)
          {
            QShapeItem qshapeItem = this.Items[index];
            qshapeItem.Y = (float) this.Size.Height - qshapeItem.Y;
            if (this.ItemIsAnchored(qshapeItem, AnchorStyles.Top) && !this.ItemIsAnchored(qshapeItem, AnchorStyles.Bottom))
            {
              qshapeItem.Anchor &= ~AnchorStyles.Top;
              qshapeItem.Anchor |= AnchorStyles.Bottom;
            }
            else if (this.ItemIsAnchored(qshapeItem, AnchorStyles.Bottom) && !this.ItemIsAnchored(qshapeItem, AnchorStyles.Top))
            {
              qshapeItem.Anchor &= ~AnchorStyles.Bottom;
              qshapeItem.Anchor |= AnchorStyles.Top;
            }
            if (qshapeItem.ItemType == QShapeItemType.Bezier)
            {
              qshapeItem.BezierControl1Y = (float) this.Size.Height - qshapeItem.BezierControl1Y;
              qshapeItem.BezierControl2Y = (float) this.Size.Height - qshapeItem.BezierControl2Y;
            }
          }
          this.m_oContentBounds.Y = this.Size.Height - this.ContentBounds.Bottom;
          this.m_oFocusPoint.Y = this.Size.Height - this.FocusPoint.Y;
          break;
      }
      this.ResumeChange(true);
    }

    public void AppendToPath(
      Rectangle destinationBounds,
      QShapePathOptions options,
      GraphicsPath path)
    {
      this.AppendToPath(new Rectangle(Point.Empty, this.m_oSize), destinationBounds, options, path);
    }

    public void AppendToPath(Size destinationSize, QShapePathOptions options, GraphicsPath path) => this.AppendToPath(new Rectangle(Point.Empty, this.m_oSize), new Rectangle(Point.Empty, destinationSize), options, path);

    public void AppendToPath(
      Rectangle sourceBounds,
      Rectangle destinationBounds,
      QShapePathOptions options,
      GraphicsPath path)
    {
      bool flag1 = (options & QShapePathOptions.VisibleLines) == QShapePathOptions.VisibleLines;
      bool flag2 = (options & QShapePathOptions.InvisibleLines) == QShapePathOptions.InvisibleLines;
      bool ignoreAnchor = (options & QShapePathOptions.IgnoreAnchors) == QShapePathOptions.IgnoreAnchors;
      if (this.Items.Count <= 1)
        return;
      bool flag3 = false;
      int index = 0;
      while (index < this.Items.Count)
      {
        QShapeItem qshapeItem = this.Items[index];
        if (qshapeItem.LineVisible && flag1 || !qshapeItem.LineVisible && flag2)
        {
          if (!flag3)
          {
            path.StartFigure();
            flag3 = true;
          }
          QShapeItem nextItem = this.GetNextItem(qshapeItem);
          if (qshapeItem.ItemType == QShapeItemType.Point)
          {
            path.AddLine(this.TranslatePoint(qshapeItem.Location, sourceBounds, destinationBounds, qshapeItem.Anchor, ignoreAnchor), this.TranslatePoint(nextItem.Location, sourceBounds, destinationBounds, nextItem.Anchor, ignoreAnchor));
            ++index;
          }
          else if (qshapeItem.ItemType == QShapeItemType.Bezier)
          {
            path.AddBezier(this.TranslatePoint(qshapeItem.Location, sourceBounds, destinationBounds, qshapeItem.Anchor, ignoreAnchor), this.TranslatePoint(qshapeItem.BezierControl1, sourceBounds, destinationBounds, qshapeItem.Anchor, ignoreAnchor), this.TranslatePoint(qshapeItem.BezierControl2, sourceBounds, destinationBounds, nextItem.Anchor, ignoreAnchor), this.TranslatePoint(nextItem.Location, sourceBounds, destinationBounds, nextItem.Anchor, ignoreAnchor));
            ++index;
          }
        }
        else
        {
          flag3 = false;
          ++index;
        }
      }
    }

    private Matrix CreateTransformationMatrix(
      RectangleF destinationBounds,
      SizeF shapeSize,
      DockStyle dockStyle)
    {
      return QMath.CreateTransformationMatrix(destinationBounds, shapeSize, dockStyle);
    }

    public GraphicsPath CreateGraphicsPath(
      Rectangle destinationBounds,
      DockStyle dockStyle,
      QShapePathOptions options,
      Matrix matrix)
    {
      bool flag = (options & QShapePathOptions.AllLines) == QShapePathOptions.AllLines;
      if (destinationBounds.Width <= 0 || destinationBounds.Height <= 0)
        return (GraphicsPath) null;
      Size size = dockStyle == DockStyle.Left || dockStyle == DockStyle.Right ? new Size(destinationBounds.Height, destinationBounds.Width) : destinationBounds.Size;
      Matrix transformationMatrix = this.CreateTransformationMatrix((RectangleF) destinationBounds, (SizeF) size, dockStyle);
      if (matrix != null)
        transformationMatrix.Multiply(matrix);
      GraphicsPath path = new GraphicsPath();
      this.AppendToPath(size, options, path);
      if (flag)
        path.CloseFigure();
      path.Transform(transformationMatrix);
      return path;
    }

    public GraphicsPath DrawShape(
      Rectangle destinationBounds,
      Pen borderPen,
      Brush backgroundBrush,
      DockStyle dockStyle,
      QSmoothingMode smoothingMode,
      QShapeDrawOptions options,
      Graphics graphics)
    {
      return this.DrawShape(destinationBounds, borderPen, backgroundBrush, dockStyle, smoothingMode, options, graphics, (Matrix) null);
    }

    public GraphicsPath DrawShape(
      Rectangle destinationBounds,
      Pen borderPen,
      Brush backgroundBrush,
      DockStyle dockStyle,
      QSmoothingMode smoothingMode,
      QShapeDrawOptions options,
      Graphics graphics,
      Matrix matrix)
    {
      if (borderPen == null && backgroundBrush == null)
        return (GraphicsPath) null;
      if (destinationBounds.Width <= 0 || destinationBounds.Height <= 0)
        return (GraphicsPath) null;
      bool flag1 = (options & QShapeDrawOptions.DrawBorder) == QShapeDrawOptions.DrawBorder;
      bool flag2 = (options & QShapeDrawOptions.FillBackground) == QShapeDrawOptions.FillBackground;
      bool flag3 = (options & QShapeDrawOptions.ExceedBackgroundWithBorder) == QShapeDrawOptions.ExceedBackgroundWithBorder;
      bool flag4 = (options & QShapeDrawOptions.ReturnDrawnShape) == QShapeDrawOptions.ReturnDrawnShape;
      SmoothingMode smoothingMode1 = graphics.SmoothingMode;
      graphics.SmoothingMode = QMisc.GetSmoothingMode(smoothingMode);
      GraphicsPath path = (GraphicsPath) null;
      if (flag2 || flag4)
        path = this.CreateGraphicsPath(destinationBounds, dockStyle, QShapePathOptions.AllLines, matrix);
      if (backgroundBrush != null && flag2)
      {
        graphics.FillPath(backgroundBrush, path);
        if (borderPen != null && flag3)
        {
          Pen pen = (Pen) borderPen.Clone();
          pen.Brush = backgroundBrush;
          graphics.DrawPath(pen, path);
          pen.Dispose();
        }
      }
      if (borderPen != null && flag1)
      {
        GraphicsPath graphicsPath = this.CreateGraphicsPath(destinationBounds, dockStyle, QShapePathOptions.VisibleLines, matrix);
        graphics.DrawPath(borderPen, graphicsPath);
        graphicsPath.Dispose();
      }
      graphics.SmoothingMode = smoothingMode1;
      if (flag4)
        return path;
      path?.Dispose();
      return (GraphicsPath) null;
    }

    public void DrawShapeDesign(
      Rectangle sourceBounds,
      Rectangle destinationBounds,
      float lineSize,
      float controlLineSize,
      float pointSize,
      float controlPointSize,
      float selectionRectangleBorderSize,
      float selectionDirectionLineSize,
      Color lineColor,
      Color controlLineColor,
      Color pointColor,
      Color controlPointColor,
      Color selectionRectangleColor,
      Color selectionDirectionColor,
      Color invisibleLineColor,
      bool showShapeItemNumbers,
      Graphics graphics)
    {
      if ((double) controlPointSize < 5.0)
        controlPointSize = 5f;
      if ((double) pointSize < 5.0)
        pointSize = 5f;
      GraphicsPath path1 = new GraphicsPath();
      this.AppendToPath(sourceBounds, destinationBounds, QShapePathOptions.InvisibleLines | QShapePathOptions.IgnoreAnchors, path1);
      Pen pen1 = new Pen(invisibleLineColor, lineSize);
      pen1.LineJoin = LineJoin.Round;
      graphics.DrawPath(pen1, path1);
      pen1.Dispose();
      path1.Dispose();
      GraphicsPath path2 = new GraphicsPath();
      this.AppendToPath(sourceBounds, destinationBounds, QShapePathOptions.VisibleLines | QShapePathOptions.IgnoreAnchors, path2);
      graphics.DrawPath(new Pen(lineColor, lineSize)
      {
        LineJoin = LineJoin.Round
      }, path2);
      path2.Dispose();
      Brush circleBrush1 = (Brush) new SolidBrush(pointColor);
      Pen rectanglePen = new Pen(selectionRectangleColor, selectionRectangleBorderSize);
      Pen pen2 = new Pen(selectionDirectionColor, selectionDirectionLineSize);
      pen2.DashStyle = DashStyle.Dash;
      Pen pen3 = new Pen(controlLineColor, controlLineSize);
      Brush circleBrush2 = (Brush) new SolidBrush(controlPointColor);
      Brush circleBrush3 = (Brush) new SolidBrush(selectionRectangleColor);
      Pen pen4 = new Pen(controlLineColor, controlLineSize);
      pen4.DashStyle = DashStyle.Dash;
      RectangleF rectangleF = this.TranslateRectangle((RectangleF) this.m_oContentBounds, destinationBounds, AnchorStyles.None, true);
      graphics.DrawRectangle(pen4, rectangleF.Left, rectangleF.Top, rectangleF.Width, rectangleF.Height);
      pen4.Dispose();
      if (this.Items.Count > 0)
      {
        for (int index = 0; index < this.Items.Count; ++index)
        {
          QShapeItem qshapeItem = this.Items[index];
          QShapeItem nextItem = this.GetNextItem(qshapeItem);
          PointF pt1 = this.TranslatePoint(qshapeItem.Location, sourceBounds, destinationBounds, AnchorStyles.None, true);
          PointF pointF = this.TranslatePoint(nextItem.Location, sourceBounds, destinationBounds, AnchorStyles.None, true);
          if ((qshapeItem.SelectionParts & QShapeItemParts.Location) == QShapeItemParts.Location)
          {
            if (qshapeItem.ItemType == QShapeItemType.Point)
              graphics.DrawLine(pen2, pt1, pointF);
            else if (qshapeItem.ItemType == QShapeItemType.Bezier)
              graphics.DrawBezier(pen2, pt1, this.TranslatePoint(qshapeItem.BezierControl1, sourceBounds, destinationBounds, AnchorStyles.None, true), this.TranslatePoint(qshapeItem.BezierControl2, sourceBounds, destinationBounds, AnchorStyles.None, true), pointF);
          }
        }
        for (int index = 0; index < this.Items.Count; ++index)
        {
          QShapeItem qshapeItem = this.Items[index];
          QShapeItem nextItem = this.GetNextItem(qshapeItem);
          PointF pointF1 = this.TranslatePoint(qshapeItem.Location, sourceBounds, destinationBounds, AnchorStyles.None, true);
          PointF pt1 = this.TranslatePoint(nextItem.Location, sourceBounds, destinationBounds, AnchorStyles.None, true);
          bool flag1 = (qshapeItem.SelectionParts & QShapeItemParts.Location) == QShapeItemParts.Location;
          bool flag2 = (qshapeItem.SelectionParts & QShapeItemParts.Line) == QShapeItemParts.Line;
          if (qshapeItem.ItemType == QShapeItemType.Bezier)
          {
            PointF pointF2 = this.TranslatePoint(qshapeItem.BezierControl1, sourceBounds, destinationBounds, AnchorStyles.None, true);
            PointF pointF3 = this.TranslatePoint(qshapeItem.BezierControl2, sourceBounds, destinationBounds, AnchorStyles.None, true);
            bool flag3 = (qshapeItem.SelectionParts & QShapeItemParts.ControlPoint1) == QShapeItemParts.ControlPoint1;
            bool flag4 = (qshapeItem.SelectionParts & QShapeItemParts.ControlPoint2) == QShapeItemParts.ControlPoint2;
            graphics.DrawLine(pen3, pointF1, pointF2);
            graphics.DrawLine(pen3, pt1, pointF3);
            this.IllustratePoint(pointF2, controlPointSize, circleBrush2, flag3 ? rectanglePen : (Pen) null, graphics);
            this.IllustratePoint(pointF3, controlPointSize, circleBrush2, flag4 ? rectanglePen : (Pen) null, graphics);
          }
          if (showShapeItemNumbers)
          {
            Font fontFromCache = new QFontDefinition("Verdana", (QFontStyle) FontStyle.Bold, Math.Min(pointSize * 2f, 10f)).GetFontFromCache();
            this.IllustratePoint(pointF1, pointSize, circleBrush1, flag1 ? rectanglePen : (Pen) null, graphics, this.Items.IndexOf(qshapeItem).ToString((IFormatProvider) CultureInfo.InvariantCulture), fontFromCache);
          }
          else
            this.IllustratePoint(pointF1, pointSize, circleBrush1, flag1 ? rectanglePen : (Pen) null, graphics);
          if (flag2)
            this.IllustratePoint(this.TranslatePoint(qshapeItem.LastCalculatedLineIntersection, sourceBounds, destinationBounds, AnchorStyles.None, true), pointSize, (Brush) null, rectanglePen, graphics);
        }
      }
      this.IllustratePoint(this.TranslatePoint((PointF) this.FocusPoint, sourceBounds, destinationBounds, this.FocusPointAnchor, true), controlPointSize, circleBrush3, (Pen) null, graphics);
      pen3.Dispose();
      circleBrush1.Dispose();
      rectanglePen.Dispose();
    }

    public void IllustratePoint(
      PointF point,
      float pointSize,
      Brush circleBrush,
      Pen rectanglePen,
      Graphics graphics,
      string text,
      Font font)
    {
      float num = pointSize / 2f;
      RectangleF rect = new RectangleF(point.X - num, point.Y - num, pointSize, pointSize);
      if (circleBrush != null)
        graphics.FillEllipse(circleBrush, rect);
      if (rectanglePen != null)
        graphics.DrawRectangle(rectanglePen, rect.Left, rect.Top, rect.Width, rect.Height);
      if (text == null)
        return;
      graphics.DrawString(text, font, circleBrush, point.X + 5f, point.Y + 5f);
    }

    public void IllustratePoint(
      PointF point,
      float pointSize,
      Brush circleBrush,
      Pen rectanglePen,
      Graphics graphics)
    {
      this.IllustratePoint(point, pointSize, circleBrush, rectanglePen, graphics, (string) null, (Font) null);
    }

    public PointF TranslatePoint(
      PointF point,
      Rectangle destinationBounds,
      AnchorStyles anchor,
      bool ignoreAnchor)
    {
      return this.TranslatePoint(point, new Rectangle(Point.Empty, this.m_oSize), destinationBounds, anchor, ignoreAnchor);
    }

    public PointF TranslatePoint(
      PointF point,
      Rectangle sourceBounds,
      Rectangle destinationBounds,
      AnchorStyles anchor,
      bool ignoreAnchor)
    {
      float x = 0.0f;
      float y = 0.0f;
      if (ignoreAnchor || (anchor & AnchorStyles.Left) == AnchorStyles.None && (anchor & AnchorStyles.Right) == AnchorStyles.None)
        x = (float) destinationBounds.Left + (point.X - (float) sourceBounds.Left) / (float) sourceBounds.Width * (float) destinationBounds.Width;
      else if ((anchor & AnchorStyles.Left) == AnchorStyles.Left)
        x = (float) destinationBounds.Left + (point.X - (float) sourceBounds.Left);
      else if ((anchor & AnchorStyles.Right) == AnchorStyles.Right)
        x = (float) destinationBounds.Right - ((float) sourceBounds.Right - point.X);
      if (ignoreAnchor || (anchor & AnchorStyles.Top) == AnchorStyles.None && (anchor & AnchorStyles.Bottom) == AnchorStyles.None)
        y = (float) destinationBounds.Top + (point.Y - (float) sourceBounds.Top) / (float) sourceBounds.Height * (float) destinationBounds.Height;
      else if ((anchor & AnchorStyles.Top) == AnchorStyles.Top)
        y = (float) destinationBounds.Top + (point.Y - (float) sourceBounds.Top);
      else if ((anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
        y = (float) destinationBounds.Bottom - ((float) sourceBounds.Bottom - point.Y);
      return this.RoundPoint(x, y);
    }

    public RectangleF TranslateRectangle(
      RectangleF rectangle,
      Rectangle destinationBounds,
      AnchorStyles anchor,
      bool ignoreAnchor)
    {
      PointF location = rectangle.Location;
      PointF point = new PointF(rectangle.Right, rectangle.Bottom);
      PointF pointF1 = this.TranslatePoint(location, destinationBounds, anchor, ignoreAnchor);
      PointF pointF2 = this.TranslatePoint(point, destinationBounds, anchor, ignoreAnchor);
      return RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y);
    }

    public override string ToString() => !QMisc.IsEmpty((object) this.m_sShapeName) ? this.m_sShapeName : base.ToString();

    private void AdjustMinimumSizeMargin(
      PointF point,
      AnchorStyles anchor,
      ref float left,
      ref float top,
      ref float bottom,
      ref float right)
    {
      if ((anchor & AnchorStyles.Left) == AnchorStyles.Left)
        left = Math.Max(left, point.X);
      if ((anchor & AnchorStyles.Top) == AnchorStyles.Top)
        top = Math.Max(top, point.Y);
      if ((anchor & AnchorStyles.Right) == AnchorStyles.Right)
        right = Math.Max(right, (float) this.m_oSize.Width - point.X);
      if ((anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom)
        return;
      bottom = Math.Max(bottom, (float) this.m_oSize.Height - point.Y);
    }

    private void CalculateMinimumSize()
    {
      float left = 0.0f;
      float top = 0.0f;
      float bottom = 0.0f;
      float right = 0.0f;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        QShapeItem qshapeItem = this.Items[index];
        QShapeItem nextItem = this.GetNextItem(qshapeItem);
        this.AdjustMinimumSizeMargin(qshapeItem.Location, qshapeItem.Anchor, ref left, ref top, ref bottom, ref right);
        if (qshapeItem.ItemType == QShapeItemType.Bezier)
        {
          this.AdjustMinimumSizeMargin(qshapeItem.BezierControl1, qshapeItem.Anchor, ref left, ref top, ref bottom, ref right);
          if (nextItem != null)
            this.AdjustMinimumSizeMargin(qshapeItem.BezierControl2, nextItem.Anchor, ref left, ref top, ref bottom, ref right);
        }
      }
      this.m_oMinimumSize = new Size((int) Math.Ceiling((double) left + (double) right), (int) Math.Ceiling((double) top + (double) bottom));
    }

    internal void HandleCollectionChanged() => this.HandleShapeChanged();

    internal void HandleItemChanged(QShapeItem item) => this.HandleShapeChanged();

    internal void HandleShapeChanged() => this.HandleShapeChanged(true);

    internal void HandleShapeChanged(bool raiseEvent)
    {
      this.CalculateMinimumSize();
      this.m_bIsChanged = true;
      if (!raiseEvent)
        return;
      this.RaiseShapeChanged();
    }

    internal void RaiseShapeChanged()
    {
      if (this.m_bSuspendChange)
        return;
      this.OnShapeChanged(EventArgs.Empty);
    }

    protected virtual void OnShapeChanged(EventArgs e) => this.m_oShapeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oShapeChangedDelegate, (object) this, (object) e);

    public virtual void CopyTo(QShape shape, bool includeItems)
    {
      shape.SuspendChange();
      shape.m_sShapeName = this.m_sShapeName;
      shape.m_oSize = this.m_oSize;
      shape.m_oFocusPoint = this.m_oFocusPoint;
      shape.m_eFocusPointAnchor = this.m_eFocusPointAnchor;
      shape.m_iPrecision = this.m_iPrecision;
      shape.m_oContentBounds = this.m_oContentBounds;
      shape.m_oBaseShape = this.m_oBaseShape;
      shape.m_eShapeType = this.m_eShapeType;
      shape.m_eBaseShapeType = this.m_eBaseShapeType;
      if (includeItems)
      {
        shape.Items.Clear();
        for (int index = 0; index < this.Items.Count; ++index)
          shape.Items.Add((QShapeItem) this.Items[index].Clone());
      }
      shape.ResumeChange(true);
      shape.m_bIsChanged = this.m_bIsChanged;
    }

    public virtual void CopyTo(QShape shape) => this.CopyTo(shape, true);

    internal bool IsEqualTo(object obj)
    {
      if (!(obj is QShape qshape) || string.Compare(this.ShapeName, qshape.ShapeName, false, CultureInfo.InvariantCulture) != 0 || !this.Size.Equals((object) qshape.Size) || !this.FocusPoint.Equals((object) qshape.FocusPoint) || !this.FocusPointAnchor.Equals((object) qshape.FocusPointAnchor) || this.Precision != qshape.Precision || !this.ContentBounds.Equals((object) qshape.ContentBounds) || !this.ShapeType.Equals((object) qshape.ShapeType) || this.Items.Count != qshape.Items.Count)
        return false;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (!this.Items[index].IsEqualTo((object) qshape.Items[index]))
          return false;
      }
      return true;
    }

    public object Clone()
    {
      QShape shape = new QShape();
      this.CopyTo(shape);
      shape.m_bConvertedFromString = this.m_bConvertedFromString;
      return (object) shape;
    }
  }
}
