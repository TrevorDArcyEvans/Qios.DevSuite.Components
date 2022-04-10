// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapePainterControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QShapePainterControl : Control, IQWeakEventPublisher
  {
    private const int m_iContentsSizeDiagonalMargin = 10;
    private QShape m_oShape;
    private QShapeDesigner m_oShapeDesigner;
    private QScrollBarExtension m_oScrollBarExtension;
    private QShapeItem m_oActiveItem;
    private QShapeItemParts m_eActiveItemPart;
    private bool m_bShowItemNumbers;
    private bool m_bUpdatingScrollValues;
    private int m_iSelectMargin = 2;
    private float m_fZoom = 1f;
    private float m_fZoomStep = 2f;
    private QMargin m_oShapeMargin = new QMargin(10, 10, 10, 10);
    private QMargin m_oZoomedShapeMargin = new QMargin(10, 10, 10, 10);
    private Size m_oZoomedShapeSize = Size.Empty;
    private Size m_oScrollSize = Size.Empty;
    private Point m_oMouseDownAtPixel;
    private Point m_oMouseMoveHandledAtPixel;
    private QShapePainterControl.QShapePainterControlAction m_eCurrentAction;
    private QRectangleSide m_eCurrentSizingSide;
    private Rectangle m_oSelectionBounds = Rectangle.Empty;
    private Point m_oOrigin = Point.Empty;
    private ContextMenuStrip cmShapeMenu;
    private ToolStripMenuItem miZoomIn;
    private ToolStripMenuItem miZoomOut;
    private ToolStripMenuItem miFitShape;
    private ToolStripMenuItem miShowShape;
    private ToolStripMenuItem miShowShapeItemNumber;
    private ToolStripMenuItem miConvertToLine;
    private ToolStripMenuItem miConvertToBezier;
    private ToolStripMenuItem miAddPoint;
    private ToolStripMenuItem miRemovePoint;
    private ToolStripMenuItem miAnchor;
    private ToolStripMenuItem miAnchorTopLeft;
    private ToolStripMenuItem miAnchorTopRight;
    private ToolStripMenuItem miAnchorBottomLeft;
    private ToolStripMenuItem miAnchorBottomRight;
    private ToolStripMenuItem miMirror;
    private ToolStripMenuItem miMirrorLeftToRight;
    private ToolStripMenuItem miMirrorRightToLeft;
    private ToolStripMenuItem miMirrorTopToBottom;
    private ToolStripMenuItem miMirrorBottomToTop;
    private ToolStripMenuItem miFlipHorizontal;
    private ToolStripMenuItem miFlipVertical;
    private ToolStripMenuItem miLineVisible;
    private bool m_bWeakEventHandlers = true;
    private QWeakDelegate m_oActiveItemChangedDelegate;
    private QWeakDelegate m_oSelectedItemsChangedDelegate;

    public QShapePainterControl()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      this.m_oScrollBarExtension = new QScrollBarExtension((Control)this, QScrollBarVisibility.Both);
      this.m_oScrollBarExtension.Scroll += new QScrollEventHandler(this.ScrollBarExtension_Scroll);
      this.miZoomIn = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_ZoomIn"), null, new EventHandler(this.ShapeMenu_Click));
      this.miZoomOut = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_ZoomOut"), null, new EventHandler(this.ShapeMenu_Click));
      this.miFitShape = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_FitShape"), null, new EventHandler(this.ShapeMenu_Click));
      this.miShowShape = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_ShowShape"), null, new EventHandler(this.ShapeMenu_Click));
      this.miShowShapeItemNumber = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_ShowShapeItemNumber"), null, new EventHandler(this.ShapeMenu_Click));
      this.miConvertToLine = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_ConvertToLine"), null, new EventHandler(this.ShapeMenu_Click));
      this.miConvertToBezier = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_ConvertToBezier"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAddPoint = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_AddPoint"), null, new EventHandler(this.ShapeMenu_Click));
      this.miRemovePoint = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_RemovePoint"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAnchorTopLeft = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_AnchorTopLeft"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAnchorTopRight = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_AnchorTopRight"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAnchorBottomLeft = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_AnchorBottomLeft"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAnchorBottomRight = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_AnchorBottomRight"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAnchor = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Anchor"), null, new EventHandler(this.ShapeMenu_Click));
      this.miAnchor.DropDownItems.AddRange(new ToolStripMenuItem[4]
      {
        this.miAnchorTopLeft,
        this.miAnchorTopRight,
        this.miAnchorBottomLeft,
        this.miAnchorBottomRight
      });
      this.miMirrorLeftToRight = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_MirrorLeftToRight"), null, new EventHandler(this.ShapeMenu_Click));
      this.miMirrorRightToLeft = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_MirrorRightToLeft"), null, new EventHandler(this.ShapeMenu_Click));
      this.miMirrorTopToBottom = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_MirrorTopToBottom"), null, new EventHandler(this.ShapeMenu_Click));
      this.miMirrorBottomToTop = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_MirrorBottomToTop"), null, new EventHandler(this.ShapeMenu_Click));
      this.miFlipHorizontal = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_FlipHorizontal"), null, new EventHandler(this.ShapeMenu_Click));
      this.miFlipVertical = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_FlipVertical"), null, new EventHandler(this.ShapeMenu_Click));
      this.miMirror = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Mirror"), null, new EventHandler(this.ShapeMenu_Click));
      this.miMirror.DropDownItems.AddRange(new ToolStripMenuItem[4]
      {
        this.miMirrorLeftToRight,
        this.miMirrorRightToLeft,
        this.miMirrorTopToBottom,
        this.miMirrorBottomToTop
      });
      this.miLineVisible = new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_LineVisible"), null, new EventHandler(this.ShapeMenu_Click));
      this.cmShapeMenu = new ContextMenuStrip();
      this.cmShapeMenu.Items.AddRange(new ToolStripMenuItem[]
      {
        this.miZoomIn,
        this.miZoomOut,
        this.miFitShape,
        this.miShowShape,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miShowShapeItemNumber,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miConvertToLine,
        this.miConvertToBezier,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miAddPoint,
        this.miRemovePoint,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miAnchor,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miMirror,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miFlipHorizontal,
        this.miFlipVertical,
        new ToolStripMenuItem(QResources.GetGeneral("QShapePainterControl_ShapeMenu_Separator")),
        this.miLineVisible
      });
    }

    [QWeakEvent]
    public event EventHandler ActiveItemChanged
    {
      add => this.m_oActiveItemChangedDelegate = QWeakDelegate.Combine(this.m_oActiveItemChangedDelegate, (Delegate)value, this.m_bWeakEventHandlers);
      remove => this.m_oActiveItemChangedDelegate = QWeakDelegate.Remove(this.m_oActiveItemChangedDelegate, (Delegate)value);
    }

    [QWeakEvent]
    public event EventHandler SelectedItemsChanged
    {
      add => this.m_oSelectedItemsChangedDelegate = QWeakDelegate.Combine(this.m_oSelectedItemsChangedDelegate, (Delegate)value, this.m_bWeakEventHandlers);
      remove => this.m_oSelectedItemsChangedDelegate = QWeakDelegate.Remove(this.m_oSelectedItemsChangedDelegate, (Delegate)value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public QShapeItem ActiveItem => this.m_oActiveItem;

    public QShapeItemParts ActiveItemPart => this.m_eActiveItemPart;

    public QShapeItem[] SelectedItems => this.m_oShape.Items.GetSelectedItems(QShapeItemParts.All);

    public float Zoom
    {
      get => this.m_fZoom;
      set
      {
        if (object.Equals((object)this.m_fZoom, (object)value))
          return;
        this.m_fZoom = value;
        this.HandleLayoutPropertyChanged();
        this.UpdateLayout();
        this.Refresh();
      }
    }

    public bool ShowItemNumbers
    {
      get => this.m_bShowItemNumbers;
      set
      {
        this.m_bShowItemNumbers = value;
        this.Refresh();
      }
    }

    internal Rectangle DestinationBounds => new Rectangle(this.m_oOrigin.X + this.m_oZoomedShapeMargin.Left, this.m_oOrigin.Y + this.m_oZoomedShapeMargin.Top, this.m_oZoomedShapeSize.Width, this.m_oZoomedShapeSize.Height);

    public void ZoomToPoint(float zoom, Point point)
    {
      if ((double)zoom < 0.0 || object.Equals((object)this.m_fZoom, (object)zoom))
        return;
      this.m_fZoom = zoom;
      this.HandleLayoutPropertyChanged();
      this.CenterPoint(point);
    }

    public void CenterPoint(Point point)
    {
      Point point1 = point;
      if (this.m_oScrollSize.Width > this.ClientSize.Width)
      {
        this.m_oOrigin.X = Math.Min(0, this.m_oOrigin.X + (this.ClientSize.Width / 2 - point1.X));
        this.m_oOrigin.X = Math.Max(this.m_oOrigin.X, this.ClientSize.Width - this.m_oScrollSize.Width);
      }
      if (this.m_oScrollSize.Height > this.ClientSize.Height)
      {
        this.m_oOrigin.Y = Math.Min(0, this.m_oOrigin.Y + (this.ClientSize.Height / 2 - point1.Y));
        this.m_oOrigin.Y = Math.Max(this.m_oOrigin.Y, this.ClientSize.Height - this.m_oScrollSize.Height);
      }
      this.UpdateLayout();
      this.Refresh();
    }

    public float ZoomStep
    {
      get => this.m_fZoomStep;
      set => this.m_fZoomStep = value;
    }

    public QShape Shape
    {
      get => this.m_oShape;
      set
      {
        if (this.m_oShape != null)
          this.m_oShape.ShapeChanged -= new EventHandler(this.Shape_ShapeChanged);
        this.m_oShape = value;
        this.m_oShapeDesigner = (QShapeDesigner)null;
        if (this.m_oShape != null)
        {
          this.m_oShapeDesigner = this.m_oShape.RetrieveActiveDesigner();
          this.m_oShape.ShapeChanged += new EventHandler(this.Shape_ShapeChanged);
        }
        this.m_fZoom = this.CalculateZoomForFit();
        this.HandleLayoutPropertyChanged();
        this.UpdateLayout();
        this.Refresh();
      }
    }

    public float CalculateZoomForFit() => this.m_oShape != null ? Math.Min((float)this.ClientSize.Width / (float)(this.m_oShape.Size.Width + this.m_oShapeMargin.Horizontal), (float)this.ClientSize.Height / (float)(this.m_oShape.Size.Height + this.m_oShapeMargin.Vertical)) : 1f;

    internal void HandleShapePropertyChanged() => this.Invalidate();

    public PointF TranslatePixel(Point pixel) => this.m_oShape.TranslatePoint((PointF)pixel, new Rectangle(this.m_oOrigin.X + this.m_oZoomedShapeMargin.Left, this.m_oOrigin.Y + this.m_oZoomedShapeMargin.Top, this.m_oZoomedShapeSize.Width, this.m_oZoomedShapeSize.Height), new Rectangle(Point.Empty, this.m_oShape.Size), AnchorStyles.None, true);

    public RectangleF TranslatePixelRectangle(Rectangle rectangle)
    {
      PointF pointF1 = this.TranslatePixel(rectangle.Location);
      PointF pointF2 = this.TranslatePixel(new Point(rectangle.Right, rectangle.Bottom));
      return RectangleF.FromLTRB(pointF1.X, pointF1.Y, pointF2.X, pointF2.Y);
    }

    public QShapeItem GetFirstShapeItemOnPixel(
      Point pixel,
      float margin,
      QShapeItemParts parts)
    {
      if (this.m_oShape == null)
        return (QShapeItem)null;
      QShapeItem[] itemsOnPoint = this.m_oShape.Items.GetItemsOnPoint(this.TranslatePixel(pixel), margin, parts);
      return itemsOnPoint != null && itemsOnPoint.Length > 0 ? itemsOnPoint[0] : (QShapeItem)null;
    }

    public QShapeItemParts GetShapeItemParts(
      QShapeItem item,
      Point pixel,
      float margin,
      QShapeItemParts partsToCheck,
      bool returnOnFirstHit)
    {
      if (item == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object)nameof(item)));
      PointF point = this.TranslatePixel(pixel);
      return item.GetItemPartsOnPoint(point, this.DestinationBounds, margin, partsToCheck, returnOnFirstHit);
    }

    public void SelectItem(
      QShapeItem item,
      QShapeItemParts parts,
      bool append,
      bool resetAllOnNoItem,
      bool refresh)
    {
      if (item != null && parts != QShapeItemParts.None)
      {
        if (append)
        {
          if ((item.SelectionParts | parts) == item.SelectionParts)
            item.SelectionParts &= ~parts;
          else
            item.SelectionParts |= parts;
        }
        else if ((item.SelectionParts | parts) != item.SelectionParts)
        {
          this.m_oShape.Items.SelectShapeItemParts(QShapeItemParts.None);
          item.SelectionParts = parts;
        }
        else if (refresh)
          this.Refresh();
      }
      else if (!append && resetAllOnNoItem)
      {
        this.m_oShape.SuspendChange();
        this.m_oShape.Items.SelectShapeItemParts(QShapeItemParts.None);
        this.m_oShape.ResumeChange(true);
      }
      this.OnSelectedItemsChanged(EventArgs.Empty);
    }

    public void SelectFirstItemAtPixel(Point pixel, bool append, bool resetAllOnNoItem)
    {
      QShapeItem shapeItemOnPixel = this.GetFirstShapeItemOnPixel(pixel, (float)this.m_iSelectMargin, QShapeItemParts.AllPoints);
      QShapeItemParts parts = QShapeItemParts.None;
      if (shapeItemOnPixel != null)
      {
        parts = this.GetShapeItemParts(shapeItemOnPixel, pixel, (float)this.m_iSelectMargin, QShapeItemParts.AllPoints, true);
      }
      else
      {
        shapeItemOnPixel = this.GetFirstShapeItemOnPixel(pixel, (float)this.m_iSelectMargin, QShapeItemParts.Line);
        if (shapeItemOnPixel != null)
          parts = this.GetShapeItemParts(shapeItemOnPixel, pixel, (float)this.m_iSelectMargin, QShapeItemParts.Line, true);
      }
      this.m_oActiveItem = shapeItemOnPixel;
      this.m_eActiveItemPart = parts;
      this.OnActiveItemChanged(EventArgs.Empty);
      this.SelectItem(shapeItemOnPixel, parts, append, resetAllOnNoItem, true);
    }

    public void SelectAllItemsPointPartsAtPixelRectangle(Rectangle pixelRectangle, bool append)
    {
      RectangleF rectangle = this.TranslatePixelRectangle(pixelRectangle);
      QShapeItem[] itemsInRectangle = this.m_oShape.Items.GetItemsInRectangle(rectangle, QShapeItemParts.AllPoints);
      if (!append)
        this.m_oShape.Items.SelectShapeItemParts(QShapeItemParts.None);
      this.m_oShape.SuspendChange();
      if (itemsInRectangle != null && itemsInRectangle.Length > 0)
      {
        for (int index = 0; index < itemsInRectangle.Length; ++index)
        {
          QShapeItem qshapeItem = itemsInRectangle[index];
          QShapeItemParts partsInRectangle = qshapeItem.GetItemPartsInRectangle(rectangle, QShapeItemParts.AllPoints, false);
          if (index == 0)
          {
            this.m_oActiveItem = qshapeItem;
            this.m_eActiveItemPart = partsInRectangle;
          }
          this.SelectItem(qshapeItem, partsInRectangle, true, true, false);
        }
      }
      this.m_oShape.ResumeChange(true);
      this.OnSelectedItemsChanged(EventArgs.Empty);
    }

    private QRectangleSide GetContentsSizingSide(Point coordinate) => this.m_oShape == null ? QRectangleSide.None : this.GetRectangleSizingSide(this.m_oShape.TranslateRectangle((RectangleF)this.m_oShape.ContentBounds, this.DestinationBounds, AnchorStyles.None, true), coordinate);

    private void SetContentsSizingCursor(QRectangleSide side)
    {
      switch (side)
      {
        case QRectangleSide.None:
          this.Cursor = Cursors.Default;
          break;
        case QRectangleSide.North:
        case QRectangleSide.South:
          this.Cursor = Cursors.SizeNS;
          break;
        case QRectangleSide.West:
        case QRectangleSide.East:
          this.Cursor = Cursors.SizeWE;
          break;
        case QRectangleSide.NorthWest:
        case QRectangleSide.SouthEast:
          this.Cursor = Cursors.SizeNWSE;
          break;
        case QRectangleSide.SouthWest:
        case QRectangleSide.NorthEast:
          this.Cursor = Cursors.SizeNESW;
          break;
      }
    }

    private QRectangleSide GetRectangleSizingSide(
      RectangleF rectangle,
      Point coordinate)
    {
      Rectangle rect = new Rectangle(coordinate.X - 1, coordinate.Y - 1, 3, 3);
      QRectangleSide rectangleSizingSide = QRectangleSide.None;
      RectangleF rectangleF1 = new RectangleF(rectangle.Location.X, rectangle.Location.Y, rectangle.Width, 1f);
      RectangleF rectangleF2 = new RectangleF(rectangle.Location.X, rectangle.Location.Y + rectangle.Height, rectangle.Width, 1f);
      RectangleF rectangleF3 = new RectangleF(rectangle.Location.X, rectangle.Location.Y, 1f, rectangle.Height);
      RectangleF rectangleF4 = new RectangleF(rectangle.Location.X + rectangle.Width, rectangle.Location.Y, 1f, rectangle.Height);
      RectangleF rectangleF5 = new RectangleF(rectangle.X - 5f, rectangle.Y - 5f, 10f, 10f);
      RectangleF rectangleF6 = new RectangleF(rectangle.Right - 5f, rectangle.Y - 5f, 10f, 10f);
      RectangleF rectangleF7 = new RectangleF(rectangle.X - 5f, rectangle.Bottom - 5f, 10f, 10f);
      RectangleF rectangleF8 = new RectangleF(rectangle.Right - 5f, rectangle.Bottom - 5f, 10f, 10f);
      if (rectangleF1.IntersectsWith((RectangleF)rect))
        rectangleSizingSide = !rectangleF5.IntersectsWith((RectangleF)rect) ? (!rectangleF6.IntersectsWith((RectangleF)rect) ? QRectangleSide.North : QRectangleSide.NorthEast) : QRectangleSide.NorthWest;
      else if (rectangleF2.IntersectsWith((RectangleF)rect))
        rectangleSizingSide = !rectangleF7.IntersectsWith((RectangleF)rect) ? (!rectangleF8.IntersectsWith((RectangleF)rect) ? QRectangleSide.South : QRectangleSide.SouthEast) : QRectangleSide.SouthWest;
      else if (rectangleF3.IntersectsWith((RectangleF)rect))
        rectangleSizingSide = !rectangleF7.IntersectsWith((RectangleF)rect) ? (!rectangleF5.IntersectsWith((RectangleF)rect) ? QRectangleSide.West : QRectangleSide.NorthWest) : QRectangleSide.SouthWest;
      else if (rectangleF4.IntersectsWith((RectangleF)rect))
        rectangleSizingSide = !rectangleF8.IntersectsWith((RectangleF)rect) ? (!rectangleF6.IntersectsWith((RectangleF)rect) ? QRectangleSide.East : QRectangleSide.NorthEast) : QRectangleSide.SouthEast;
      return rectangleSizingSide;
    }

    private void ShowContextMenu(Point point)
    {
      bool flag1 = this.m_oActiveItem != null && this.m_eActiveItemPart == QShapeItemParts.Location;
      bool flag2 = this.m_oActiveItem != null && this.m_eActiveItemPart == QShapeItemParts.Line;
      this.miConvertToBezier.Enabled = (flag1 || flag2) && this.m_oActiveItem.ItemType == QShapeItemType.Point;
      this.miConvertToLine.Enabled = flag1 && this.m_oActiveItem.ItemType == QShapeItemType.Bezier;
      this.miAddPoint.Enabled = flag2;
      this.miRemovePoint.Enabled = flag1;
      this.miAnchor.Enabled = flag1;
      if (this.miAnchor.Enabled)
      {
        this.miAnchorTopLeft.Checked = this.m_oActiveItem.Anchor == (AnchorStyles.Top | AnchorStyles.Left);
        this.miAnchorTopRight.Checked = this.m_oActiveItem.Anchor == (AnchorStyles.Top | AnchorStyles.Right);
        this.miAnchorBottomLeft.Checked = this.m_oActiveItem.Anchor == (AnchorStyles.Bottom | AnchorStyles.Left);
        this.miAnchorBottomRight.Checked = this.m_oActiveItem.Anchor == (AnchorStyles.Bottom | AnchorStyles.Right);
      }
      this.miLineVisible.Enabled = flag1 || flag2;
      this.miLineVisible.Checked = this.miLineVisible.Enabled && this.m_oActiveItem.LineVisible;
      this.miZoomOut.Enabled = (double)this.Zoom - (double)this.ZoomStep > 0.0;
      this.cmShapeMenu.Show((Control)this, point);
    }

    private void ConvertActiveItemToLine()
    {
      if (this.m_oActiveItem == null || this.m_oActiveItem.ItemType != QShapeItemType.Bezier)
        return;
      this.m_oActiveItem.ItemType = QShapeItemType.Point;
    }

    private void ConvertActiveItemToBezier()
    {
      if (this.m_oActiveItem == null || this.m_oActiveItem.ItemType != QShapeItemType.Point)
        return;
      this.m_oActiveItem.ItemType = QShapeItemType.Bezier;
    }

    private void InsertPointAfterActiveItem()
    {
      if (this.m_oActiveItem == null || (this.m_oActiveItem.SelectionParts & QShapeItemParts.Line) != QShapeItemParts.Line)
        return;
      AnchorStyles anchor = (AnchorStyles)(0 | ((double)this.m_oActiveItem.LastCalculatedLineIntersection.X < (double)(this.m_oShape.Size.Width / 2) ? 4 : 8) | ((double)this.m_oActiveItem.LastCalculatedLineIntersection.Y < (double)(this.m_oShape.Size.Height / 2) ? 1 : 2));
      this.m_oShape.SuspendChange();
      if (this.m_oActiveItem.ItemType == QShapeItemType.Bezier)
      {
        QShapeItem nextItem = this.m_oShape.Items.GetNextItem(this.m_oActiveItem);
        if (nextItem != null)
        {
          this.m_oActiveItem.BezierControl2X += this.m_oActiveItem.LastCalculatedLineIntersection.X - nextItem.Location.X;
          this.m_oActiveItem.BezierControl2Y += this.m_oActiveItem.LastCalculatedLineIntersection.Y - nextItem.Location.Y;
        }
      }
      int num = this.m_oShape.Items.IndexOf(this.m_oActiveItem);
      if (num < this.m_oShape.Items.Count - 1)
        this.m_oShape.Items.Insert(num + 1, new QShapeItem(this.m_oActiveItem.LastCalculatedLineIntersection, anchor, this.m_oActiveItem.LineVisible));
      else
        this.m_oShape.Items.Add(new QShapeItem(this.m_oActiveItem.LastCalculatedLineIntersection, anchor, this.m_oActiveItem.LineVisible));
      this.m_oShape.ResumeChange(true);
    }

    private void RemoveActiveItem()
    {
      if (this.m_oActiveItem == null)
        return;
      if (this.m_oShape.Items.Count > this.m_oShape.Items.MinimumItemCount)
      {
        this.m_oShape.SuspendChange();
        QShapeItem previousItem = this.m_oShape.Items.GetPreviousItem(this.m_oActiveItem);
        QShapeItem nextItem = this.m_oShape.Items.GetNextItem(this.m_oActiveItem);
        if (previousItem != null && nextItem != null && previousItem.ItemType == QShapeItemType.Bezier)
        {
          previousItem.BezierControl2X += nextItem.Location.X - this.m_oActiveItem.Location.X;
          previousItem.BezierControl2Y += nextItem.Location.Y - this.m_oActiveItem.Location.Y;
        }
        this.m_oShape.Items.Remove(this.m_oActiveItem);
        this.m_oShape.ResumeChange(true);
      }
      else
      {
        int num = (int)MessageBox.Show((IWin32Window)this.TopLevelControl, QResources.GetException("QShapeItemCollection_CountBelowMinimumItemCount", (object)this.m_oShape.Items.MinimumItemCount), this.TopLevelControl.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void AnchorActiveItem(AnchorStyles anchor, bool setAnchor)
    {
      if (this.m_oActiveItem == null)
        return;
      if (setAnchor)
        this.m_oActiveItem.Anchor = anchor;
      else
        this.m_oActiveItem.Anchor &= ~anchor;
    }

    private void MirrorShape(QShapeMirrorOption options)
    {
      if (this.m_oShape == null)
        return;
      this.m_oShape.MirrorShape(options);
    }

    private void FlipShape(Orientation orientation)
    {
      if (this.m_oShape == null)
        return;
      this.m_oShape.FlipShape(orientation);
    }

    private void SwapActiveItemLineVisible()
    {
      if (this.m_oActiveItem == null)
        return;
      this.m_oActiveItem.LineVisible = !this.m_oActiveItem.LineVisible;
    }

    internal void HandleLayoutPropertyChanged()
    {
      this.m_oZoomedShapeMargin = new QMargin((int)((double)this.m_oShapeMargin.Left * (double)this.m_fZoom), (int)((double)this.m_oShapeMargin.Top * (double)this.m_fZoom), (int)((double)this.m_oShapeMargin.Bottom * (double)this.m_fZoom), (int)((double)this.m_oShapeMargin.Right * (double)this.m_fZoom));
      this.m_oZoomedShapeSize = this.m_oShape == null ? Size.Empty : new Size((int)((double)this.m_oShape.Size.Width * (double)this.m_fZoom), (int)((double)this.m_oShape.Size.Height * (double)this.m_fZoom));
      this.m_oScrollSize = this.m_oZoomedShapeMargin.InflateSizeWithMargin(this.m_oZoomedShapeSize, true, true);
    }

    internal void UpdateLayout()
    {
      if (this.m_oShape == null)
        return;
      this.m_oScrollBarExtension.SetScrollSize(this.m_oScrollSize);
      this.m_bUpdatingScrollValues = true;
      if (this.ClientSize.Width > this.m_oScrollSize.Width)
      {
        this.m_oOrigin.X = QMath.GetStartForCenter(0, this.ClientSize.Width, this.m_oScrollSize.Width);
      }
      else
      {
        this.m_oScrollBarExtension.ScrollHorizontalValue = -this.m_oOrigin.X;
        this.m_oOrigin.X = -this.m_oScrollBarExtension.ScrollHorizontalValue;
      }
      if (this.ClientSize.Height > this.m_oScrollSize.Height)
      {
        this.m_oOrigin.Y = QMath.GetStartForCenter(0, this.ClientSize.Height, this.m_oScrollSize.Height);
      }
      else
      {
        this.m_oScrollBarExtension.ScrollVerticalValue = -this.m_oOrigin.Y;
        this.m_oOrigin.Y = -this.m_oScrollBarExtension.ScrollVerticalValue;
      }
      this.m_bUpdatingScrollValues = false;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.m_oShape == null)
        return;
      this.m_oMouseDownAtPixel = new Point(e.X, e.Y);
      if (e.Button == MouseButtons.Left)
      {
        QRectangleSide contentsSizingSide = this.GetContentsSizingSide(this.m_oMouseDownAtPixel);
        if (contentsSizingSide != QRectangleSide.None)
        {
          this.m_eCurrentAction = QShapePainterControl.QShapePainterControlAction.SizingContent;
          this.m_eCurrentSizingSide = contentsSizingSide;
        }
        else
        {
          this.SelectFirstItemAtPixel(this.m_oMouseDownAtPixel, Control.ModifierKeys == Keys.Shift, true);
          if (this.m_oActiveItem != null)
          {
            this.m_eCurrentAction = QShapePainterControl.QShapePainterControlAction.Moving;
            this.m_oShape.Items.CacheSelectedParts();
          }
          else
          {
            this.m_oSelectionBounds = Rectangle.Empty;
            this.m_eCurrentAction = QShapePainterControl.QShapePainterControlAction.Selecting;
          }
        }
      }
      else
      {
        if (e.Button != MouseButtons.Right)
          return;
        this.SelectFirstItemAtPixel(this.m_oMouseDownAtPixel, Control.ModifierKeys == Keys.Shift, false);
        this.ShowContextMenu(this.m_oMouseDownAtPixel);
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.m_oShape == null)
        return;
      Point point = new Point(e.X, e.Y);
      if (point == this.m_oMouseDownAtPixel || point == this.m_oMouseMoveHandledAtPixel)
        return;
      this.m_oMouseMoveHandledAtPixel = point;
      if (this.m_eCurrentAction == QShapePainterControl.QShapePainterControlAction.Moving)
      {
        PointF pointF1 = this.TranslatePixel(this.m_oMouseDownAtPixel);
        PointF pointF2 = this.TranslatePixel(new Point(e.X, e.Y));
        if (this.m_oShape == null)
          return;
        this.m_oShape.SuspendChange();
        this.m_oShape.Items.MoveSelectedItemPartsRelativeToCache(new PointF(pointF2.X - pointF1.X, pointF2.Y - pointF1.Y));
        this.m_oShape.ResumeChange(true);
      }
      else if (this.m_eCurrentAction == QShapePainterControl.QShapePainterControlAction.Selecting)
      {
        this.m_oSelectionBounds = Rectangle.FromLTRB(Math.Min(this.m_oMouseDownAtPixel.X, point.X), Math.Min(this.m_oMouseDownAtPixel.Y, point.Y), Math.Max(this.m_oMouseDownAtPixel.X, point.X), Math.Max(this.m_oMouseDownAtPixel.Y, point.Y));
        this.Refresh();
      }
      else if (this.m_eCurrentAction == QShapePainterControl.QShapePainterControlAction.SizingContent)
      {
        Rectangle rectangle = this.m_oShape.ContentBounds;
        PointF pointF = this.TranslatePixel(point);
        if ((this.m_eCurrentSizingSide & QRectangleSide.North) == QRectangleSide.North)
          rectangle = new Rectangle(rectangle.X, (int)pointF.Y, rectangle.Width, rectangle.Height - ((int)pointF.Y - rectangle.Y));
        if ((this.m_eCurrentSizingSide & QRectangleSide.South) == QRectangleSide.South)
          rectangle.Height = (int)pointF.Y - rectangle.Y;
        if ((this.m_eCurrentSizingSide & QRectangleSide.West) == QRectangleSide.West)
          rectangle = new Rectangle((int)pointF.X, rectangle.Y, rectangle.Width - ((int)pointF.X - rectangle.X), rectangle.Height);
        if ((this.m_eCurrentSizingSide & QRectangleSide.East) == QRectangleSide.East)
          rectangle.Width = (int)pointF.X - rectangle.X;
        bool flag1 = false;
        bool flag2 = false;
        if (rectangle.Width < 0)
        {
          rectangle.X -= Math.Abs(rectangle.Width);
          rectangle.Width = Math.Abs(rectangle.Width);
          flag1 = true;
        }
        if (rectangle.Height < 0)
        {
          rectangle.Y -= Math.Abs(rectangle.Height);
          rectangle.Height = Math.Abs(rectangle.Height);
          flag2 = true;
        }
        if (flag1)
        {
          if ((this.m_eCurrentSizingSide & QRectangleSide.East) == QRectangleSide.East)
          {
            this.m_eCurrentSizingSide &= ~QRectangleSide.East;
            this.m_eCurrentSizingSide |= QRectangleSide.West;
          }
          else if ((this.m_eCurrentSizingSide & QRectangleSide.West) == QRectangleSide.West)
          {
            this.m_eCurrentSizingSide &= ~QRectangleSide.West;
            this.m_eCurrentSizingSide |= QRectangleSide.East;
          }
        }
        if (flag2)
        {
          if ((this.m_eCurrentSizingSide & QRectangleSide.North) == QRectangleSide.North)
          {
            this.m_eCurrentSizingSide &= ~QRectangleSide.North;
            this.m_eCurrentSizingSide |= QRectangleSide.South;
          }
          else if ((this.m_eCurrentSizingSide & QRectangleSide.South) == QRectangleSide.South)
          {
            this.m_eCurrentSizingSide &= ~QRectangleSide.South;
            this.m_eCurrentSizingSide |= QRectangleSide.North;
          }
        }
        this.SetContentsSizingCursor(this.m_eCurrentSizingSide);
        this.m_oShape.ContentBounds = rectangle;
      }
      else
        this.SetContentsSizingCursor(this.GetContentsSizingSide(point));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.m_eCurrentAction == QShapePainterControl.QShapePainterControlAction.Selecting)
      {
        this.m_eCurrentAction = QShapePainterControl.QShapePainterControlAction.None;
        this.SelectAllItemsPointPartsAtPixelRectangle(this.m_oSelectionBounds, Control.ModifierKeys == Keys.Shift);
        this.m_oSelectionBounds = Rectangle.Empty;
        this.Refresh();
      }
      else
        this.m_eCurrentAction = QShapePainterControl.QShapePainterControlAction.None;
      base.OnMouseUp(e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.UpdateLayout();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.Clear(SystemColors.Window);
      Graphics graphics = e.Graphics;
      if (this.m_oShape != null)
      {
        SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
        e.Graphics.SmoothingMode = QMisc.GetSmoothingMode(this.m_oShapeDesigner != null ? this.m_oShapeDesigner.SmoothingMode : QSmoothingMode.None);
        Rectangle rectangle = new Rectangle(this.m_oOrigin.X + this.m_oZoomedShapeMargin.Left, this.m_oOrigin.Y + this.m_oZoomedShapeMargin.Top, this.m_oZoomedShapeSize.Width, this.m_oZoomedShapeSize.Height);
        Brush brush = (Brush)new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White);
        Pen pen1 = new Pen(Color.LightGray, 1f);
        Pen pen2 = new Pen(Color.Orange, 1f);
        pen2.DashStyle = DashStyle.Dash;
        graphics.FillRectangle(brush, rectangle);
        graphics.DrawRectangle(pen1, rectangle);
        if (this.m_oShapeDesigner != null && this.m_oShapeDesigner.BackgroundImage != null)
        {
          Rectangle destRect = new Rectangle(rectangle.X + (int)((double)this.m_oShapeDesigner.BackgroundImagePosition.X * (double)this.m_fZoom), rectangle.Y + (int)((double)this.m_oShapeDesigner.BackgroundImagePosition.Y * (double)this.m_fZoom), (int)((double)this.m_oShapeDesigner.BackgroundImageScale / 100.0 * (double)this.m_oShapeDesigner.BackgroundImage.Width * (double)this.m_fZoom), (int)((double)this.m_oShapeDesigner.BackgroundImageScale / 100.0 * (double)this.m_oShapeDesigner.BackgroundImage.Height * (double)this.m_fZoom));
          ImageAttributes imageAttr = new ImageAttributes();
          if (this.m_oShapeDesigner.BackgroundImageOpacity < 100)
          {
            float[][] newColorMatrix = new float[5][]
            {
              new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
              new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
              new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
              new float[5]
              {
                0.0f,
                0.0f,
                0.0f,
                (float) this.m_oShapeDesigner.BackgroundImageOpacity / 100f,
                0.0f
              },
              new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
            };
            imageAttr.SetColorMatrix(new ColorMatrix(newColorMatrix));
          }
          e.Graphics.DrawImage(this.m_oShapeDesigner.BackgroundImage, destRect, 0, 0, this.m_oShapeDesigner.BackgroundImage.Width, this.m_oShapeDesigner.BackgroundImage.Height, GraphicsUnit.Pixel, imageAttr);
          imageAttr.ClearColorMatrix();
        }
        graphics.DrawLine(pen2, rectangle.X + rectangle.Width / 2, rectangle.Y, rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height);
        graphics.DrawLine(pen2, rectangle.X, rectangle.Y + rectangle.Height / 2, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height / 2);
        pen1.Dispose();
        brush.Dispose();
        pen2.Dispose();
        this.m_oShape.DrawShapeDesign(new Rectangle(Point.Empty, this.m_oShape.Size), rectangle, this.m_fZoom * 1.2f, 1f, (float)this.m_iSelectMargin * this.m_fZoom, (float)this.m_iSelectMargin * this.m_fZoom, this.m_fZoom * 0.5f, this.m_fZoom * 0.25f, Color.Black, Color.Green, Color.FromArgb((int)byte.MaxValue, 0, 0), Color.FromArgb(0, 128, 0), Color.FromArgb(0, 0, (int)byte.MaxValue), Color.White, Color.FromArgb(128, 128, 128, 128), this.ShowItemNumbers, graphics);
        e.Graphics.SmoothingMode = smoothingMode;
      }
      if (this.m_eCurrentAction == QShapePainterControl.QShapePainterControlAction.Selecting)
      {
        Pen pen = new Pen(Color.FromArgb(192, 0, 0, (int)byte.MaxValue), 1f);
        graphics.DrawRectangle(pen, this.m_oSelectionBounds);
        pen.Dispose();
      }
      base.OnPaint(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyData != Keys.Escape && e.KeyData != (Keys.Z | Keys.Control))
        return;
      this.m_oShape.Items.RestoreSelectedPartsFromCache();
    }

    protected virtual void OnActiveItemChanged(EventArgs e) => this.m_oActiveItemChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oActiveItemChangedDelegate, (object)this, (object)e);

    protected virtual void OnSelectedItemsChanged(EventArgs e) => this.m_oSelectedItemsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSelectedItemsChangedDelegate, (object)this, (object)e);

    private void ShapeMenu_Click(object sender, EventArgs e)
    {
      if (sender == this.miZoomIn)
        this.ZoomToPoint(this.Zoom + this.ZoomStep, this.m_oMouseDownAtPixel);
      else if (sender == this.miZoomOut)
        this.ZoomToPoint(this.Zoom - this.ZoomStep, this.m_oMouseDownAtPixel);
      else if (sender == this.miFitShape)
        this.Zoom = this.CalculateZoomForFit();
      else if (sender == this.miShowShapeItemNumber)
      {
        this.ShowItemNumbers = !this.ShowItemNumbers;
        this.miShowShapeItemNumber.Checked = this.ShowItemNumbers;
      }
      else if (sender == this.miShowShape)
        this.Zoom = 1f;
      else if (sender == this.miConvertToLine)
        this.ConvertActiveItemToLine();
      else if (sender == this.miConvertToBezier)
        this.ConvertActiveItemToBezier();
      else if (sender == this.miAddPoint)
        this.InsertPointAfterActiveItem();
      else if (sender == this.miRemovePoint)
        this.RemoveActiveItem();
      else if (sender == this.miAnchorTopLeft)
        this.AnchorActiveItem(AnchorStyles.Top | AnchorStyles.Left, !this.miAnchorTopLeft.Checked);
      else if (sender == this.miAnchorTopRight)
        this.AnchorActiveItem(AnchorStyles.Top | AnchorStyles.Right, !this.miAnchorTopRight.Checked);
      else if (sender == this.miAnchorBottomLeft)
        this.AnchorActiveItem(AnchorStyles.Bottom | AnchorStyles.Left, !this.miAnchorBottomLeft.Checked);
      else if (sender == this.miAnchorBottomRight)
        this.AnchorActiveItem(AnchorStyles.Bottom | AnchorStyles.Right, !this.miAnchorBottomRight.Checked);
      else if (sender == this.miMirrorLeftToRight)
        this.MirrorShape(QShapeMirrorOption.LeftToRight);
      else if (sender == this.miMirrorRightToLeft)
        this.MirrorShape(QShapeMirrorOption.RightToLeft);
      else if (sender == this.miMirrorTopToBottom)
        this.MirrorShape(QShapeMirrorOption.TopToBottom);
      else if (sender == this.miMirrorBottomToTop)
        this.MirrorShape(QShapeMirrorOption.BottomToTop);
      else if (sender == this.miFlipHorizontal)
        this.FlipShape(Orientation.Horizontal);
      else if (sender == this.miFlipVertical)
      {
        this.FlipShape(Orientation.Vertical);
      }
      else
      {
        if (sender != this.miLineVisible)
          return;
        this.SwapActiveItemLineVisible();
      }
    }

    private void ScrollBarExtension_Scroll(object sender, QScrollEventArgs e)
    {
      if (this.m_bUpdatingScrollValues)
        return;
      bool flag = false;
      if (this.m_oScrollBarExtension.ScrollHorizontalVisible && this.m_oOrigin.X != -this.m_oScrollBarExtension.ScrollHorizontalValue)
      {
        this.m_oOrigin.X = -this.m_oScrollBarExtension.ScrollHorizontalValue;
        flag = true;
      }
      if (this.m_oScrollBarExtension.ScrollVerticalVisible && this.m_oOrigin.Y != -this.m_oScrollBarExtension.ScrollVerticalValue)
      {
        this.m_oOrigin.Y = -this.m_oScrollBarExtension.ScrollVerticalValue;
        flag = true;
      }
      if (!flag)
        return;
      this.Invalidate();
      this.Update();
    }

    private void Shape_ShapeChanged(object sender, EventArgs e) => this.Refresh();

    internal enum QShapePainterControlAction
    {
      None,
      Moving,
      Selecting,
      SizingContent,
    }
  }
}
