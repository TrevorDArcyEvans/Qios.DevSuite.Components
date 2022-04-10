// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollablePartData
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QScrollablePartData : IQPadding, IQItemColorHost, IQTimerClient, IQMouseHandler
  {
    private const int m_iTimerInterval = 10;
    private const int m_iTimerIntervalPage = 300;
    public const int ScrollAreaTopIndex = 0;
    public const int ScrollAreaLeftIndex = 1;
    public const int ScrollAreaRightIndex = 2;
    public const int ScrollAreaBottomIndex = 3;
    private int m_iScrollStepSize;
    private Point m_oScrollOffset = Point.Empty;
    private Rectangle m_oViewPort = Rectangle.Empty;
    private Rectangle m_oOuterViewPort = Rectangle.Empty;
    private Size m_oContentSize = Size.Empty;
    private QScrollablePartData.QScrollArea m_oHotArea;
    private QScrollablePartData.QScrollArea m_oPressedArea;
    private IQItemColorHost m_oColorHost;
    private IQScrollablePart m_oScrollablePart;
    private QScrollablePartFlags m_eFlags;
    private QScrollablePartData.QScrollArea[] m_aScrollAreas;
    private QCompositeScrollConfiguration m_oConfiguration;
    private QTimerManager m_oTimerManager;
    private bool m_bIsScrollingAnimatedHorizontal;
    private bool m_bIsScrollingAnimatedVertical;
    private Point m_oScrollUntilOffset = Point.Empty;
    private object m_oCachedPadding;

    public QScrollablePartData(IQScrollablePart part, QCompositeScrollConfiguration configuration)
    {
      this.m_aScrollAreas = new QScrollablePartData.QScrollArea[4];
      this.m_aScrollAreas[0] = new QScrollablePartData.QScrollArea(this, DockStyle.Top);
      this.m_aScrollAreas[1] = new QScrollablePartData.QScrollArea(this, DockStyle.Left);
      this.m_aScrollAreas[2] = new QScrollablePartData.QScrollArea(this, DockStyle.Right);
      this.m_aScrollAreas[3] = new QScrollablePartData.QScrollArea(this, DockStyle.Bottom);
      this.m_oScrollablePart = part;
      this.m_oConfiguration = configuration;
      this.m_oTimerManager = new QTimerManager(10);
    }

    public Point ScrollOffset => this.m_oScrollOffset;

    public IQScrollablePart ScrollablePart => this.m_oScrollablePart;

    public IQManagedLayoutParent DisplayParent
    {
      get
      {
        if (!(this.m_oScrollablePart is IQManagedLayoutParent displayParent))
          displayParent = this.m_oScrollablePart.DisplayParent;
        return displayParent;
      }
    }

    public QComposite Composite
    {
      get
      {
        if (!(this.m_oScrollablePart is QComposite composite))
          composite = this.m_oScrollablePart.DisplayParent as QComposite;
        return composite;
      }
    }

    public QCompositeScrollConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set => this.m_oConfiguration = value;
    }

    private IQItemColorHost ColorHost
    {
      set => this.m_oColorHost = value;
      get => this.m_oColorHost == null ? (IQItemColorHost) this : this.m_oColorHost;
    }

    private IQColorRetriever ColorRetriever => this.m_oScrollablePart as IQColorRetriever;

    public bool ScrollHorizontalVisible
    {
      get => QScrollablePartData.IsFlagSet(this.m_eFlags, QScrollablePartFlags.ScrollHorizontal);
      set => this.m_eFlags = QScrollablePartData.SetFlag(this.m_eFlags, QScrollablePartFlags.ScrollHorizontal, value);
    }

    public bool ScrollVerticalVisible
    {
      get => QScrollablePartData.IsFlagSet(this.m_eFlags, QScrollablePartFlags.ScrollVertical);
      set => this.m_eFlags = QScrollablePartData.SetFlag(this.m_eFlags, QScrollablePartFlags.ScrollVertical, value);
    }

    public bool ScrollWithBar
    {
      get => QScrollablePartData.IsFlagSet(this.m_eFlags, QScrollablePartFlags.ScrollWithBar);
      set => this.m_eFlags = QScrollablePartData.SetFlag(this.m_eFlags, QScrollablePartFlags.ScrollWithBar, value);
    }

    public bool IsAtHorizontalStart => this.m_oScrollOffset.X == 0;

    public bool IsAtVerticalStart => this.m_oScrollOffset.Y == 0;

    public bool IsAtHorizontalEnd => this.m_oViewPort.Left + this.m_oScrollOffset.X + this.m_oContentSize.Width <= this.m_oViewPort.Right;

    public bool IsAtVerticalEnd => this.m_oViewPort.Top + this.m_oScrollOffset.Y + this.m_oContentSize.Height <= this.m_oViewPort.Bottom;

    public bool IsScrollingAnimated => this.m_bIsScrollingAnimatedHorizontal || this.m_bIsScrollingAnimatedVertical;

    public bool HasVisibleScrollableAreas => this.ScrollVerticalVisible || this.ScrollHorizontalVisible;

    public QScrollablePartData.QScrollArea ScrollAreaTop => this.m_aScrollAreas[0];

    public QScrollablePartData.QScrollArea ScrollAreaLeft => this.m_aScrollAreas[1];

    public QScrollablePartData.QScrollArea ScrollAreaRight => this.m_aScrollAreas[2];

    public QScrollablePartData.QScrollArea ScrollAreaBottom => this.m_aScrollAreas[3];

    public QScrollablePartData.QScrollArea HotArea
    {
      get => this.m_oHotArea;
      set
      {
        bool flag = false;
        if (this.m_oHotArea == value)
          return;
        if (this.m_oHotArea != null)
        {
          this.m_oHotArea.Hot = false;
          flag = true;
          if (this.ScrollWithBar && this.m_oHotArea.ScrollBar != null)
            this.m_oHotArea.ScrollBar.HandleMouseLeave(new MouseEventArgs(Control.MouseButtons, 0, 0, 0, 0));
        }
        this.m_oHotArea = value;
        if (this.m_oHotArea != null)
        {
          this.m_oHotArea.Hot = true;
          flag = true;
        }
        if (!flag)
          return;
        QComposite composite = this.Composite;
        if (composite != null)
          composite.PaintExpandedItemOverride = this.m_oHotArea != null ? QTristateBool.False : QTristateBool.Undefined;
        this.Invalidate();
      }
    }

    public QScrollablePartData.QScrollArea PressedArea
    {
      get => this.m_oPressedArea;
      set
      {
        bool flag = false;
        if (this.m_oPressedArea == value)
          return;
        if (this.m_oPressedArea != null)
        {
          this.m_oPressedArea.Pressed = false;
          flag = true;
        }
        this.m_oPressedArea = value;
        if (this.m_oPressedArea != null)
        {
          this.m_oPressedArea.Pressed = true;
          flag = true;
        }
        if (!flag)
          return;
        this.Invalidate();
      }
    }

    public Rectangle ContentBounds
    {
      get
      {
        Rectangle contentBounds = new Rectangle(this.m_oViewPort.Location, this.m_oContentSize);
        contentBounds.Offset(this.m_oScrollOffset);
        return contentBounds;
      }
    }

    public void ScrollHorizontal(int xValue, QScrollablePartMethod method, bool animated) => this.ScrollHorizontal(xValue, method, animated, false);

    internal void ScrollHorizontal(
      int xValue,
      QScrollablePartMethod method,
      bool animated,
      bool page)
    {
      int x = 0;
      switch (method)
      {
        case QScrollablePartMethod.IntoView:
          x = QScrollHelper.TranslateCoordinateToOffset(xValue, this.m_oViewPort.Left, this.m_oViewPort.Right, this.m_oScrollOffset.X);
          break;
        case QScrollablePartMethod.Relative:
          x = this.m_oScrollOffset.X + xValue;
          break;
      }
      int num = this.CorrectScrollOffsetX(x);
      if (animated)
      {
        this.m_oScrollUntilOffset.X = num;
        this.m_bIsScrollingAnimatedHorizontal = true;
        this.m_iScrollStepSize = page ? this.m_oViewPort.Width : this.Configuration.ScrollStepSize;
        bool flag = this.m_oTimerManager.IsRegistered((IQTimerClient) this);
        this.UpdateScrollTimer(page ? 300 : 10);
        if (!page || flag)
          return;
        this.HandleScrollTimerTick();
      }
      else
      {
        this.m_oScrollOffset.X = num;
        this.SynchronizeScrollButtons(true);
        this.NotifyScrollingStage(QScrollablePartScrollStage.Scrolling);
      }
    }

    public void ScrollVertical(int yValue, QScrollablePartMethod method, bool animated) => this.ScrollVertical(yValue, method, animated, false);

    internal void ScrollVertical(
      int yValue,
      QScrollablePartMethod method,
      bool animated,
      bool page)
    {
      int y = 0;
      switch (method)
      {
        case QScrollablePartMethod.IntoView:
          y = QScrollHelper.TranslateCoordinateToOffset(yValue, this.m_oViewPort.Top, this.m_oViewPort.Bottom, this.m_oScrollOffset.Y);
          break;
        case QScrollablePartMethod.Relative:
          y = this.m_oScrollOffset.Y + yValue;
          break;
      }
      int num = this.CorrectScrollOffsetY(y);
      if (animated)
      {
        this.m_oScrollUntilOffset.Y = num;
        this.m_bIsScrollingAnimatedVertical = true;
        this.m_iScrollStepSize = page ? this.m_oViewPort.Height : this.Configuration.ScrollStepSize;
        bool flag = this.m_oTimerManager.IsRegistered((IQTimerClient) this);
        this.UpdateScrollTimer(page ? 300 : 10);
        if (!page || flag)
          return;
        this.HandleScrollTimerTick();
      }
      else
      {
        this.m_oScrollOffset.Y = num;
        this.SynchronizeScrollButtons(true);
        this.NotifyScrollingStage(QScrollablePartScrollStage.Scrolling);
      }
    }

    public void ScrollIntoView(IQPart part) => this.ScrollIntoView(part.CalculatedProperties.Bounds, this.Configuration.ScrollAnimated);

    public void ScrollIntoView(IQPart part, bool animated) => this.ScrollIntoView(part.CalculatedProperties.Bounds, animated);

    public void ScrollIntoView(Rectangle bounds) => this.ScrollIntoView(bounds, this.Configuration.ScrollAnimated);

    public void ScrollIntoView(Rectangle bounds, bool animated)
    {
      int newValue1 = 0;
      int newValue2 = 0;
      bool scrollIntoViewAmount1 = QScrollHelper.CalculateScrollIntoViewAmount(bounds.Left, bounds.Right, this.m_oViewPort.Left, this.m_oViewPort.Right, this.m_oScrollOffset.X, out newValue1);
      bool scrollIntoViewAmount2 = QScrollHelper.CalculateScrollIntoViewAmount(bounds.Top, bounds.Bottom, this.m_oViewPort.Top, this.m_oViewPort.Bottom, this.m_oScrollOffset.Y, out newValue2);
      if (scrollIntoViewAmount1)
        this.ScrollHorizontal(newValue1, QScrollablePartMethod.IntoView, animated);
      if (!scrollIntoViewAmount2)
        return;
      this.ScrollVertical(newValue2, QScrollablePartMethod.IntoView, animated);
    }

    public void StopScrolling()
    {
      this.m_bIsScrollingAnimatedHorizontal = false;
      this.m_bIsScrollingAnimatedVertical = false;
      this.UpdateScrollTimer();
    }

    public void StopScrollingHorizontal()
    {
      this.m_bIsScrollingAnimatedHorizontal = false;
      this.UpdateScrollTimer();
    }

    public void StopScrollingVertical()
    {
      this.m_bIsScrollingAnimatedVertical = false;
      this.UpdateScrollTimer();
    }

    private void UpdateScrollTimer(int interval)
    {
      if (this.m_oTimerManager.Interval != interval)
        this.m_oTimerManager.Interval = interval;
      this.UpdateScrollTimer();
    }

    private void UpdateScrollTimer()
    {
      if (this.IsScrollingAnimated && !this.m_oTimerManager.IsRegistered((IQTimerClient) this))
      {
        this.m_oTimerManager.Register((IQTimerClient) this);
        this.NotifyScrollingStage(new QScrollablePartScrollStage[1]);
      }
      else
      {
        if (this.IsScrollingAnimated || !this.m_oTimerManager.IsRegistered((IQTimerClient) this))
          return;
        this.m_oTimerManager.Unregister((IQTimerClient) this);
        this.NotifyScrollingStage(QScrollablePartScrollStage.ScrollingEnded);
      }
    }

    private void HandleScrollTimerTick()
    {
      if (this.m_bIsScrollingAnimatedHorizontal)
      {
        int x = this.m_oScrollOffset.X;
        this.m_bIsScrollingAnimatedHorizontal = QScrollHelper.HandleOneDirectionScrollTimerTick(ref x, this.m_oScrollUntilOffset.X, this.m_iScrollStepSize);
        this.m_oScrollOffset.X = x;
      }
      if (this.m_bIsScrollingAnimatedVertical)
      {
        int y = this.m_oScrollOffset.Y;
        this.m_bIsScrollingAnimatedVertical = QScrollHelper.HandleOneDirectionScrollTimerTick(ref y, this.m_oScrollUntilOffset.Y, this.m_iScrollStepSize);
        this.m_oScrollOffset.Y = y;
      }
      if (this.IsScrollingAnimated)
        this.NotifyScrollingStage(QScrollablePartScrollStage.Scrolling);
      this.SynchronizeScrollButtons(false);
      this.Invalidate();
      this.UpdateScrollTimer();
    }

    private int CorrectScrollOffsetX(int x)
    {
      if (x == 0)
        return 0;
      if (this.m_oViewPort.Left + x + this.m_oContentSize.Width < this.m_oViewPort.Right)
        x = this.m_oViewPort.Width - this.m_oContentSize.Width;
      x = Math.Min(0, x);
      return x;
    }

    private int CorrectScrollOffsetY(int y)
    {
      if (y == 0)
        return 0;
      if (this.m_oViewPort.Top + y + this.m_oContentSize.Height < this.m_oViewPort.Bottom)
        y = this.m_oViewPort.Height - this.m_oContentSize.Height;
      y = Math.Min(0, y);
      return y;
    }

    private bool SynchronizeScrollOffset(bool invalidate)
    {
      if (this.m_oScrollOffset == Point.Empty)
        return false;
      Point oScrollOffset = this.m_oScrollOffset;
      this.m_oScrollOffset = new Point(this.CorrectScrollOffsetX(this.m_oScrollOffset.X), this.CorrectScrollOffsetY(this.m_oScrollOffset.Y));
      if (!(this.m_oScrollOffset != oScrollOffset))
        return false;
      if (invalidate)
        this.Invalidate();
      return true;
    }

    private void SynchronizeScrollButtons(bool invalidate)
    {
      if (this.ScrollWithBar)
      {
        this.ScrollAreaBottom.Synchronize();
        this.ScrollAreaTop.Synchronize();
        this.ScrollAreaLeft.Synchronize();
        this.ScrollAreaRight.Synchronize();
        this.ScrollAreaTop.Disabled = this.IsAtHorizontalStart && this.IsAtHorizontalEnd;
        this.ScrollAreaBottom.Disabled = this.IsAtHorizontalStart && this.IsAtHorizontalEnd;
        this.ScrollAreaLeft.Disabled = this.IsAtVerticalStart && this.IsAtVerticalEnd;
        this.ScrollAreaRight.Disabled = this.IsAtVerticalStart && this.IsAtVerticalEnd;
      }
      else
      {
        this.ScrollAreaTop.Disabled = this.IsAtVerticalStart;
        this.ScrollAreaBottom.Disabled = this.IsAtVerticalEnd;
        this.ScrollAreaLeft.Disabled = this.IsAtHorizontalStart;
        this.ScrollAreaRight.Disabled = this.IsAtHorizontalEnd;
      }
      if (!invalidate)
        return;
      this.Invalidate();
    }

    private void SynchronizeAll(bool invalidate, bool notifyScrollingStage)
    {
      bool flag = this.SynchronizeScrollOffset(false);
      this.SynchronizeScrollButtons(false);
      if (flag && invalidate)
        this.Invalidate();
      if (!flag || !notifyScrollingStage)
        return;
      this.NotifyScrollingStage(QScrollablePartScrollStage.Scrolling);
    }

    public void ClearCachedObjects()
    {
      this.m_oCachedPadding = (object) null;
      for (int index = 0; index < this.m_aScrollAreas.Length; ++index)
        this.m_aScrollAreas[index].ClearCachedObjects();
    }

    private void NotifyScrollingStage(params QScrollablePartScrollStage[] stages)
    {
      foreach (QScrollablePartScrollStage stage in stages)
        QCompositeHelper.NotifyChildPartScrollingStage(this.m_oScrollablePart, (IQPart) this.m_oScrollablePart, stage);
    }

    public bool ScrollAreaVisible(DockStyle dockStyle)
    {
      switch (dockStyle)
      {
        case DockStyle.Top:
          return this.ScrollVerticalVisible && !this.ScrollWithBar;
        case DockStyle.Bottom:
          if (this.ScrollVerticalVisible && !this.ScrollWithBar)
            return true;
          return this.ScrollHorizontalVisible && this.ScrollWithBar;
        case DockStyle.Left:
          return this.ScrollHorizontalVisible && !this.ScrollWithBar;
        case DockStyle.Right:
          if (this.ScrollHorizontalVisible && !this.ScrollWithBar)
            return true;
          return this.ScrollVerticalVisible && this.ScrollWithBar;
        default:
          return false;
      }
    }

    public QScrollablePartData.QScrollArea GetArea(Point location)
    {
      for (int index = 0; index < this.m_aScrollAreas.Length; ++index)
      {
        if (this.m_aScrollAreas[index].IsVisible && this.m_aScrollAreas[index].Bounds.Contains(location))
          return this.m_aScrollAreas[index];
      }
      return (QScrollablePartData.QScrollArea) null;
    }

    public void HandleLayoutStage(
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (this.m_oConfiguration == null)
        return;
      switch (layoutStage)
      {
        case QPartLayoutStage.CalculatingSize:
          if (this.m_oConfiguration.ScrollHorizontal != QCompositeScrollVisibility.Automatic)
            this.ScrollHorizontalVisible = this.m_oConfiguration.ScrollHorizontal == QCompositeScrollVisibility.Always;
          if (this.m_oConfiguration.ScrollVertical != QCompositeScrollVisibility.Automatic)
            this.ScrollVerticalVisible = this.m_oConfiguration.ScrollVertical == QCompositeScrollVisibility.Always;
          this.ClearCachedObjects();
          break;
        case QPartLayoutStage.ConstraintsApplied:
          if (this.m_oConfiguration.ScrollHorizontal == QCompositeScrollVisibility.None && this.m_oConfiguration.ScrollVertical == QCompositeScrollVisibility.None && !this.HasVisibleScrollableAreas)
            break;
          QPartCalculatedProperties calculatedProperties = this.m_oScrollablePart.CalculatedProperties;
          Size actualContentSize = additionalProperties.ActualContentSize;
          Size size = this.InflateSize(calculatedProperties.InnerSize, true, true);
          bool flag = false;
          if (actualContentSize.Width > size.Width && this.m_oConfiguration.ScrollHorizontal != QCompositeScrollVisibility.None || this.m_oConfiguration.ScrollHorizontal == QCompositeScrollVisibility.Always)
          {
            if (!this.ScrollHorizontalVisible)
            {
              this.ScrollHorizontalVisible = true;
              calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
              flag = true;
            }
          }
          else if (this.ScrollHorizontalVisible)
          {
            this.ScrollHorizontalVisible = false;
            calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.WidthConstraintApplied;
            flag = true;
          }
          if (actualContentSize.Height > size.Height && this.m_oConfiguration.ScrollVertical != QCompositeScrollVisibility.None || this.m_oConfiguration.ScrollVertical == QCompositeScrollVisibility.Always)
          {
            if (!this.ScrollVerticalVisible)
            {
              this.ScrollVerticalVisible = true;
              calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
              flag = true;
            }
          }
          else if (this.ScrollVerticalVisible)
          {
            this.ScrollVerticalVisible = false;
            calculatedProperties.LayoutFlags &= ~QPartLayoutFlags.HeightConstraintApplied;
            flag = true;
          }
          if (this.m_oConfiguration.ScrollType == QCompositeScrollType.Button && this.ScrollWithBar)
          {
            this.ScrollWithBar = false;
            flag |= this.HasVisibleScrollableAreas;
          }
          else if (this.m_oConfiguration.ScrollType == QCompositeScrollType.ScrollBar && !this.ScrollWithBar)
          {
            this.ScrollWithBar = true;
            flag |= this.HasVisibleScrollableAreas;
          }
          if (!flag)
            break;
          this.ClearCachedObjects();
          calculatedProperties.ClearAppliedLayoutProperties();
          calculatedProperties.SetUnstretchedSizesBasedOnInnerSize(calculatedProperties.UnstretchedActualContentSize);
          calculatedProperties.SetSizesBasedOnOuterSize(calculatedProperties.OuterSize, false);
          break;
        case QPartLayoutStage.BoundsCalculated:
          QPadding padding = this.ToPadding();
          this.m_oViewPort = this.m_oScrollablePart.CalculatedProperties.InnerBounds;
          this.m_oOuterViewPort = padding.InflateRectangleWithPadding(this.m_oViewPort, true, true);
          this.m_oContentSize = this.m_oScrollablePart.CalculatedProperties.ActualContentSize;
          if (this.HasVisibleScrollableAreas)
            this.LayoutScrollAreas(this.m_oOuterViewPort);
          this.SynchronizeAll(false, true);
          break;
      }
    }

    public bool HitTest(int x, int y) => this.HasVisibleScrollableAreas && this.GetScrollCorrectedArea(new Point(x, y)) != null;

    public QScrollablePartData.QScrollArea GetScrollCorrectedArea(Point point) => this.GetArea(this.GetScrollCorrectedPoint(point));

    private Point GetScrollCorrectedPoint(Point point)
    {
      Point cachedScrollOffset = this.m_oScrollablePart.CalculatedProperties.CachedScrollOffset;
      cachedScrollOffset.X = point.X - cachedScrollOffset.X;
      cachedScrollOffset.Y = point.Y - cachedScrollOffset.Y;
      return cachedScrollOffset;
    }

    private void HandleScrollingViaScrollArea(QScrollablePartData.QScrollArea area)
    {
      if (area == null || this.ScrollWithBar)
        return;
      if (this.Configuration.ScrollAnimated)
      {
        if (area.Dock == DockStyle.Top)
          this.ScrollVertical(this.m_oViewPort.Top, QScrollablePartMethod.IntoView, true);
        else if (area.Dock == DockStyle.Left)
          this.ScrollHorizontal(this.m_oViewPort.Left, QScrollablePartMethod.IntoView, true);
        else if (area.Dock == DockStyle.Right)
        {
          this.ScrollHorizontal(this.m_oViewPort.Left + this.m_oContentSize.Width, QScrollablePartMethod.IntoView, true);
        }
        else
        {
          if (area.Dock != DockStyle.Bottom)
            return;
          this.ScrollVertical(this.m_oViewPort.Top + this.m_oContentSize.Height, QScrollablePartMethod.IntoView, true);
        }
      }
      else if (area.Dock == DockStyle.Top)
        this.ScrollVertical(this.Configuration.ScrollStepSize, QScrollablePartMethod.Relative, false);
      else if (area.Dock == DockStyle.Left)
        this.ScrollHorizontal(this.Configuration.ScrollStepSize, QScrollablePartMethod.Relative, false);
      else if (area.Dock == DockStyle.Right)
      {
        this.ScrollHorizontal(-this.Configuration.ScrollStepSize, QScrollablePartMethod.Relative, false);
      }
      else
      {
        if (area.Dock != DockStyle.Bottom)
          return;
        this.ScrollVertical(-this.Configuration.ScrollStepSize, QScrollablePartMethod.Relative, false);
      }
    }

    public bool HandleMouseMove(MouseEventArgs e)
    {
      QScrollablePartData.QScrollArea scrollCorrectedArea = this.GetScrollCorrectedArea(new Point(e.X, e.Y));
      bool flag = this.HotArea != scrollCorrectedArea;
      this.HotArea = scrollCorrectedArea;
      if (scrollCorrectedArea != null && this.ScrollWithBar && scrollCorrectedArea.ScrollBar != null)
      {
        scrollCorrectedArea.ScrollBar.HandleMouseMove(e);
        return flag;
      }
      if (this.PressedArea != null && this.ScrollWithBar && this.PressedArea.ScrollBar != null)
      {
        this.PressedArea.ScrollBar.HandleMouseMove(e);
        return flag;
      }
      if (this.Configuration.ScrollOnMouseOver)
      {
        if (this.HotArea != null && !this.IsScrollingAnimated)
          this.HandleScrollingViaScrollArea(this.HotArea);
        else if (this.HotArea == null && this.IsScrollingAnimated && this.PressedArea == null)
          this.StopScrolling();
      }
      return flag;
    }

    public bool HandleMouseDown(MouseEventArgs e)
    {
      QScrollablePartData.QScrollArea scrollCorrectedArea = this.GetScrollCorrectedArea(new Point(e.X, e.Y));
      bool flag = this.PressedArea != scrollCorrectedArea;
      this.PressedArea = scrollCorrectedArea;
      if (scrollCorrectedArea != null)
        this.m_oScrollablePart.CaptureMouse(this);
      if (scrollCorrectedArea != null && this.ScrollWithBar && scrollCorrectedArea.ScrollBar != null)
      {
        scrollCorrectedArea.ScrollBar.HandleMouseDown(e);
        return flag;
      }
      if (scrollCorrectedArea != null && !this.ScrollWithBar)
        this.HandleScrollingViaScrollArea(scrollCorrectedArea);
      return flag;
    }

    public bool HandleMouseUp(MouseEventArgs e)
    {
      if (this.PressedArea != null && this.ScrollWithBar && this.PressedArea.ScrollBar != null)
      {
        this.PressedArea.ScrollBar.HandleMouseUp(e);
        return true;
      }
      if (this.PressedArea == null || this.ScrollWithBar)
        return false;
      if (this.IsScrollingAnimated)
        this.StopScrolling();
      this.PressedArea = (QScrollablePartData.QScrollArea) null;
      return true;
    }

    public void LayoutScrollAreas(Rectangle rectangle)
    {
      for (int index = 0; index < this.m_aScrollAreas.Length; ++index)
        this.m_aScrollAreas[index].LayoutScrollArea(rectangle);
    }

    public void PaintScrollAreas(QPartPaintContext paintContext)
    {
      for (int index = 0; index < this.m_aScrollAreas.Length; ++index)
      {
        if (this.m_aScrollAreas[index].IsVisible)
          this.m_aScrollAreas[index].PaintScrollArea(paintContext);
      }
    }

    public Size InflateSize(Size size, bool inflate, bool horizontal)
    {
      QPadding padding = this.ToPadding();
      return padding != QPadding.Empty ? padding.InflateSizeWithPadding(size, inflate, horizontal) : size;
    }

    public Rectangle InflateRectangle(
      Rectangle rectangle,
      bool inflate,
      DockStyle dockStyle)
    {
      QPadding padding = this.ToPadding();
      return padding != QPadding.Empty ? padding.InflateRectangleWithPadding(rectangle, inflate, dockStyle) : rectangle;
    }

    public QPadding ToPadding()
    {
      if (this.m_oCachedPadding == null)
      {
        if (this.HasVisibleScrollableAreas)
        {
          QPadding qpadding = new QPadding(0, 0, 0, 0);
          if (this.ScrollWithBar)
          {
            qpadding.Left = this.ScrollAreaLeft.IsVisible ? ((IQScrollBarHandlerParent) this.ScrollAreaLeft).RequestedSize.Width : 0;
            qpadding.Top = this.ScrollAreaTop.IsVisible ? ((IQScrollBarHandlerParent) this.ScrollAreaTop).RequestedSize.Height : 0;
            qpadding.Bottom = this.ScrollAreaBottom.IsVisible ? ((IQScrollBarHandlerParent) this.ScrollAreaBottom).RequestedSize.Height : 0;
            qpadding.Right = this.ScrollAreaRight.IsVisible ? ((IQScrollBarHandlerParent) this.ScrollAreaRight).RequestedSize.Width : 0;
          }
          else
          {
            qpadding.Left = this.ScrollAreaLeft.IsVisible ? this.ScrollAreaLeft.RequestedSize.Width : 0;
            qpadding.Top = this.ScrollAreaTop.IsVisible ? this.ScrollAreaTop.RequestedSize.Height : 0;
            qpadding.Bottom = this.ScrollAreaBottom.IsVisible ? this.ScrollAreaBottom.RequestedSize.Height : 0;
            qpadding.Right = this.ScrollAreaRight.IsVisible ? this.ScrollAreaRight.RequestedSize.Width : 0;
          }
          this.m_oCachedPadding = (object) qpadding;
        }
        else
          this.m_oCachedPadding = (object) QPadding.Empty;
      }
      return (QPadding) this.m_oCachedPadding;
    }

    public static QColorSet GetDefaultCompositeScrollButtonColorSet(
      object item,
      QItemStates state,
      object additionalData,
      IQColorRetriever retriever)
    {
      QColorSet scrollButtonColorSet = new QColorSet();
      if (additionalData is QCompositeScrollBarItem.ScrollBar)
      {
        if (QItemStatesHelper.IsPressed(state))
        {
          scrollButtonColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeScrollBarPressedBackground1");
          scrollButtonColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeScrollBarPressedBackground2");
          scrollButtonColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeScrollBarPressedBorder");
          scrollButtonColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeText");
        }
        else
        {
          scrollButtonColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeScrollBarBackground1");
          scrollButtonColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeScrollBarBackground2");
          scrollButtonColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeScrollBarBorder");
          scrollButtonColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeText");
        }
      }
      else if (QItemStatesHelper.IsDisabled(state))
      {
        scrollButtonColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonDisabledBackground1");
        scrollButtonColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonDisabledBackground2");
        scrollButtonColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonDisabledBorder");
        scrollButtonColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextDisabled");
      }
      else if (QItemStatesHelper.IsNormal(state))
      {
        scrollButtonColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonBackground1");
        scrollButtonColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonBackground2");
        scrollButtonColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonBorder");
        scrollButtonColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeText");
      }
      else if (QItemStatesHelper.IsPressed(state))
      {
        scrollButtonColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonPressedBackground1");
        scrollButtonColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonPressedBackground2");
        scrollButtonColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonPressedBorder");
        scrollButtonColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextPressed");
      }
      else if (QItemStatesHelper.IsHot(state))
      {
        scrollButtonColorSet.Background1 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonHotBackground1");
        scrollButtonColorSet.Background2 = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonHotBackground2");
        scrollButtonColorSet.Border = retriever.RetrieveFirstDefinedColor("CompositeScrollButtonHotBorder");
        scrollButtonColorSet.Foreground = retriever.RetrieveFirstDefinedColor("CompositeTextHot");
      }
      return scrollButtonColorSet;
    }

    public QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return QScrollablePartData.GetDefaultCompositeScrollButtonColorSet(destinationObject, state, additionalProperties, this.ColorRetriever);
    }

    public static bool IsFlagSet(QScrollablePartFlags flagsToTest, QScrollablePartFlags forFlag) => (flagsToTest & forFlag) == forFlag;

    public static QScrollablePartFlags SetFlag(
      QScrollablePartFlags flagsToAdjust,
      QScrollablePartFlags flagToSet,
      bool value)
    {
      if (value)
        flagsToAdjust |= flagToSet;
      else
        flagsToAdjust &= ~flagToSet;
      return flagsToAdjust;
    }

    public void Invalidate() => this.DisplayParent?.HandleChildObjectChanged(false, QPartHelper.GetPartPaintBounds((IQPart) this.m_oScrollablePart, true));

    void IQTimerClient.Tick(QTimerManager manager) => this.HandleScrollTimerTick();

    public class QScrollArea : IQScrollBarHandlerParent
    {
      private Rectangle m_oBounds;
      private DockStyle m_eDockStyle;
      private QScrollablePartData m_oScrollData;
      private object m_oCachedRequestedSize;
      private QItemStates m_eItemState;
      private QScrollBarHandler m_oScrollBarHandler;

      public QScrollArea(QScrollablePartData scrollData, DockStyle dockStyle)
      {
        this.m_eDockStyle = dockStyle;
        this.m_oScrollData = scrollData;
      }

      public void Synchronize()
      {
        if (this.m_oScrollBarHandler == null || !this.m_oScrollData.ScrollWithBar)
          return;
        this.m_oScrollBarHandler.Synchronize();
      }

      bool IQScrollBarHandlerParent.IsAtHorizontalEnd => this.m_oScrollData.IsAtHorizontalEnd;

      bool IQScrollBarHandlerParent.IsAtHorizontalStart => this.m_oScrollData.IsAtHorizontalStart;

      bool IQScrollBarHandlerParent.IsAtVerticalEnd => this.m_oScrollData.IsAtVerticalEnd;

      bool IQScrollBarHandlerParent.IsAtVerticalStart => this.m_oScrollData.IsAtVerticalStart;

      void IQScrollBarHandlerParent.ScrollHorizontal(
        int xValue,
        QScrollablePartMethod method,
        bool animated,
        bool page)
      {
        this.m_oScrollData.ScrollHorizontal(xValue, method, animated, page);
      }

      void IQScrollBarHandlerParent.ScrollVertical(
        int yValue,
        QScrollablePartMethod method,
        bool animated,
        bool page)
      {
        this.m_oScrollData.ScrollVertical(yValue, method, animated, page);
      }

      void IQScrollBarHandlerParent.ScrollIntoView(
        Rectangle bounds,
        bool animated)
      {
        this.m_oScrollData.ScrollIntoView(bounds, animated);
      }

      void IQScrollBarHandlerParent.StopScrolling() => this.m_oScrollData.StopScrolling();

      void IQScrollBarHandlerParent.Invalidate() => this.m_oScrollData.Invalidate();

      bool IQScrollBarHandlerParent.IsScrollingAnimated => this.m_oScrollData.IsScrollingAnimated;

      int IQScrollBarHandlerParent.ScrollStepSize => this.Configuration.ScrollStepSize;

      QScrollBarConfiguration IQScrollBarHandlerParent.Configuration
      {
        get => (QScrollBarConfiguration) this.Configuration;
        set => this.Configuration = value as QCompositeScrollConfiguration;
      }

      QScrollBarAppearance IQScrollBarHandlerParent.Appearance => (QScrollBarAppearance) this.Configuration.BarAppearance;

      internal QScrollBarHandler ScrollBar => this.m_oScrollBarHandler;

      public DockStyle Dock => this.m_eDockStyle;

      public void ClearCachedObjects() => this.m_oCachedRequestedSize = (object) null;

      public QCompositeScrollConfiguration Configuration
      {
        get => this.m_oScrollData == null ? (QCompositeScrollConfiguration) null : this.m_oScrollData.Configuration;
        set
        {
          if (this.m_oScrollData == null)
            return;
          this.m_oScrollData.Configuration = value;
        }
      }

      public QShape ButtonShape => this.Configuration == null || this.Configuration.ButtonAppearance == null ? (QShape) null : this.Configuration.ButtonAppearance.Shape;

      public int RequestedWidth => this.RequestedSize.Width;

      public int RequestedHeight => this.RequestedSize.Height;

      public Rectangle Bounds => this.m_oBounds;

      public QItemStates ItemState
      {
        get => this.m_eItemState;
        set => this.m_eItemState = value;
      }

      public bool Hot
      {
        get => QItemStatesHelper.IsHot(this.m_eItemState);
        set => this.m_eItemState = QItemStatesHelper.AdjustState(this.m_eItemState, QItemStates.Hot, value);
      }

      public bool Pressed
      {
        get => QItemStatesHelper.IsPressed(this.m_eItemState);
        set => this.m_eItemState = QItemStatesHelper.AdjustState(this.m_eItemState, QItemStates.Pressed, value);
      }

      public bool Disabled
      {
        get => QItemStatesHelper.IsDisabled(this.m_eItemState);
        set => this.m_eItemState = QItemStatesHelper.AdjustState(this.m_eItemState, QItemStates.Disabled, value);
      }

      private Size CalculateRequestedButtonSize()
      {
        QCompositeScrollConfiguration configuration = this.Configuration;
        QShape buttonShape = this.ButtonShape;
        Size size1 = configuration.UsedButtonMask.Size;
        if (buttonShape != null)
          size1 = buttonShape.InflateSize(size1, true, true);
        Size size2 = configuration.ButtonPadding.InflateSizeWithPadding(size1, true, true);
        Size requestedButtonSize = configuration.ButtonMargin.InflateSizeWithMargin(size2, true, true);
        if (this.m_oScrollData.ScrollWithBar)
        {
          if (this.m_eDockStyle == DockStyle.Top || this.m_eDockStyle == DockStyle.Bottom)
            requestedButtonSize = new Size(requestedButtonSize.Height, requestedButtonSize.Width);
        }
        else if (this.m_eDockStyle == DockStyle.Left || this.m_eDockStyle == DockStyle.Right)
          requestedButtonSize = new Size(requestedButtonSize.Height, requestedButtonSize.Width);
        return requestedButtonSize;
      }

      private void LayoutScrollBarHandler()
      {
        if (!this.m_oScrollData.ScrollWithBar)
          return;
        if (this.m_oScrollBarHandler == null)
          this.m_oScrollBarHandler = new QScrollBarHandler((IQScrollBarHandlerParent) this);
        this.m_oScrollBarHandler.Layout();
      }

      public bool IsVisible => this.m_oScrollData.ScrollAreaVisible(this.m_eDockStyle);

      public Size RequestedSize
      {
        get
        {
          if (this.m_oCachedRequestedSize == null)
            this.m_oCachedRequestedSize = (object) this.CalculateRequestedButtonSize();
          return (Size) this.m_oCachedRequestedSize;
        }
      }

      IQItemColorHost IQScrollBarHandlerParent.ColorHost => this.m_oScrollData.ColorHost;

      Point IQScrollBarHandlerParent.ScrollOffset => this.m_oScrollData.m_oScrollOffset;

      Size IQScrollBarHandlerParent.RequestedSize => ((IQScrollBarHandlerParent) this).Appearance.Shape.InflateSize(((IQScrollBarHandlerParent) this).ButtonSize, true, this.Dock == DockStyle.Bottom || this.Dock == DockStyle.Top);

      Size IQScrollBarHandlerParent.ButtonSize => this.RequestedSize;

      Size IQScrollBarHandlerParent.ContentSize => this.m_oScrollData.m_oContentSize;

      Rectangle IQScrollBarHandlerParent.ViewPort => this.m_oScrollData.m_oViewPort;

      public void LayoutScrollArea(Rectangle rectangle)
      {
        if (!this.IsVisible)
        {
          this.m_oBounds = Rectangle.Empty;
        }
        else
        {
          Size size = this.m_oScrollData.ScrollWithBar ? ((IQScrollBarHandlerParent) this).RequestedSize : this.RequestedSize;
          QPadding padding = this.m_oScrollData.ToPadding();
          if (this.m_eDockStyle == DockStyle.Top)
            this.m_oBounds = new Rectangle(padding.Left + rectangle.Left, rectangle.Top, rectangle.Width - padding.Horizontal, size.Height);
          else if (this.m_eDockStyle == DockStyle.Left)
            this.m_oBounds = new Rectangle(rectangle.Left, rectangle.Top + padding.Top, size.Width, rectangle.Height - padding.Vertical);
          else if (this.m_eDockStyle == DockStyle.Right)
            this.m_oBounds = new Rectangle(rectangle.Right - size.Width, rectangle.Top + padding.Top, size.Width, rectangle.Height - padding.Vertical);
          else if (this.m_eDockStyle == DockStyle.Bottom)
            this.m_oBounds = new Rectangle(rectangle.Left + padding.Left, rectangle.Bottom - size.Height, rectangle.Width - padding.Horizontal, size.Height);
        }
        this.LayoutScrollBarHandler();
      }

      public void PaintScrollArea(QPartPaintContext paintContext)
      {
        QCompositeScrollConfiguration configuration = this.Configuration;
        if (configuration == null)
          return;
        QItemStates eItemState = this.m_eItemState;
        if ((IQPart) this.m_oScrollData.ScrollablePart is IQItemStatesImplementation scrollablePart && QItemStatesHelper.IsDisabled(scrollablePart.ItemState))
          eItemState |= QItemStates.Disabled;
        if (!this.m_oScrollData.ScrollWithBar)
        {
          QColorSet itemColorSet = this.m_oScrollData.ColorHost.GetItemColorSet((object) this, eItemState, (object) null);
          Rectangle oBounds = this.m_oBounds;
          Rectangle rectangle1 = configuration.ButtonMargin.InflateRectangleWithMargin(oBounds, false, this.m_eDockStyle);
          QShape buttonShape = this.ButtonShape;
          if (buttonShape != null)
          {
            QShapePainterProperties properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
            QShapePainter.Default.Paint(rectangle1, buttonShape, (IQAppearance) configuration.ButtonAppearance, itemColorSet, properties, new QAppearanceFillerProperties()
            {
              DockStyle = this.m_eDockStyle
            }, QPainterOptions.Default, paintContext.Graphics);
          }
          Rectangle rectangle2 = configuration.ButtonPadding.InflateRectangleWithPadding(rectangle1, false, this.m_eDockStyle);
          Image image = QControlPaint.RotateFlipImage(configuration.UsedButtonMask, this.m_eDockStyle);
          Rectangle rectangle3 = QMath.AlignElement(image.Size, ContentAlignment.MiddleCenter, rectangle2, true);
          QControlPaint.DrawImage(paintContext.Graphics, image, Color.Red, itemColorSet.Foreground, rectangle3);
        }
        else
        {
          if (this.m_oScrollBarHandler == null)
            return;
          this.m_oScrollBarHandler.Paint(paintContext.Graphics, eItemState);
        }
      }
    }
  }
}
