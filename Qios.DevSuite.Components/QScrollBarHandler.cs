// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollBarHandler
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QScrollBarHandler
  {
    private IQScrollBarHandlerParent m_oParent;
    private QButtonArea m_oUpButton;
    private QButtonArea m_oDownButton;
    private QButtonArea m_oTrackButton;
    private QScrollBarHandler.QButtonAreaCollection m_oButtons;
    private bool m_bTracking;
    private Point m_oTrackingOffset = Point.Empty;
    private Rectangle m_oTrackBar = Rectangle.Empty;
    private DockStyle m_oTrackBarDirection;

    public QScrollBarHandler(IQScrollBarHandlerParent parent)
    {
      this.m_oParent = parent;
      this.m_oUpButton = new QButtonArea(MouseButtons.Left);
      this.m_oDownButton = new QButtonArea(MouseButtons.Left);
      this.m_oTrackButton = new QButtonArea(MouseButtons.Left);
      this.m_oButtons = new QScrollBarHandler.QButtonAreaCollection();
      this.m_oButtons.AddButtonArea(this.m_oUpButton);
      this.m_oButtons.AddButtonArea(this.m_oTrackButton);
      this.m_oButtons.AddButtonArea(this.m_oDownButton);
      this.m_oButtons.ButtonStateChanged += new QButtonAreaEventHandler(this.m_oButtons_ButtonStateChanged);
    }

    private bool Horizontal => this.m_oParent.Dock == DockStyle.Top || this.m_oParent.Dock == DockStyle.Bottom;

    public void Synchronize() => this.Layout();

    public bool HandleMouseLeave(MouseEventArgs e)
    {
      bool flag = false;
      if (this.m_bTracking)
        return true;
      for (int index = 0; index < this.m_oButtons.Count; ++index)
      {
        if (this.m_oButtons[index].HandleMouseLeave(e))
          flag = true;
      }
      return flag;
    }

    public bool HandleMouseMove(MouseEventArgs e)
    {
      bool flag = false;
      if (this.m_bTracking)
      {
        this.HandleTracking(new Point(e.X, e.Y));
        return true;
      }
      if (this.m_oTrackBarDirection != DockStyle.None)
        this.ScrollToTrackPoint(new Point(e.X, e.Y));
      for (int index = 0; index < this.m_oButtons.Count; ++index)
      {
        if (this.m_oButtons[index].HandleMouseMove(e))
          flag = true;
      }
      return flag;
    }

    public bool HandleMouseUp(MouseEventArgs e)
    {
      bool flag = false;
      if (this.m_oTrackBarDirection != DockStyle.None)
      {
        if (this.m_oParent.IsScrollingAnimated)
          this.m_oParent.StopScrolling();
        this.m_oTrackBarDirection = DockStyle.None;
        this.m_oParent.Invalidate();
      }
      for (int index = 0; index < this.m_oButtons.Count; ++index)
      {
        if (this.m_oButtons[index].HandleMouseUp(e))
          flag = true;
      }
      return flag;
    }

    private bool ScrollToTrackPoint(Point point)
    {
      if (!this.m_oTrackBar.Contains(point.X, point.Y) || this.m_oTrackButton.Bounds.Contains(point.X, point.Y))
        return false;
      if (this.Horizontal)
      {
        bool flag = this.m_oTrackButton.Bounds.X > point.X;
        int x = point.X;
        this.m_oParent.ScrollHorizontal(this.ConvertPointOnTrackBarToScrollOffset(!flag ? x + this.m_oTrackButton.Bounds.Width / 2 : x - this.m_oTrackButton.Bounds.Width / 2), QScrollablePartMethod.IntoView, this.m_oParent.Configuration.ScrollAnimated, true);
        this.m_oTrackBarDirection = flag ? DockStyle.Left : DockStyle.Right;
      }
      else
      {
        bool flag = this.m_oTrackButton.Bounds.Y > point.Y;
        int y = point.Y;
        this.m_oParent.ScrollVertical(this.ConvertPointOnTrackBarToScrollOffset(!flag ? y + this.m_oTrackButton.Bounds.Height / 2 : y - this.m_oTrackButton.Bounds.Height / 2), QScrollablePartMethod.IntoView, this.m_oParent.Configuration.ScrollAnimated, true);
        this.m_oTrackBarDirection = flag ? DockStyle.Top : DockStyle.Bottom;
      }
      return true;
    }

    public bool HandleMouseDown(MouseEventArgs e)
    {
      bool flag = false;
      for (int index = 0; index < this.m_oButtons.Count; ++index)
      {
        if (this.m_oButtons[index].HandleMouseDown(e))
          flag = true;
      }
      if (!flag)
        flag = this.ScrollToTrackPoint(new Point(e.X, e.Y));
      return flag;
    }

    public void Paint(Graphics graphics, QItemStates states)
    {
      if (!this.m_oParent.IsVisible)
        return;
      bool flag1 = QItemStatesHelper.IsDisabled(states);
      bool flag2 = QItemStatesHelper.IsHot(states);
      Rectangle bounds1 = Rectangle.Empty;
      if (this.m_oTrackBarDirection != DockStyle.None)
      {
        switch (this.m_oTrackBarDirection)
        {
          case DockStyle.Top:
            bounds1 = new Rectangle(this.m_oTrackBar.X, this.m_oTrackBar.Y, this.m_oTrackBar.Width, this.m_oTrackButton.Location.Y - this.m_oTrackBar.Y);
            break;
          case DockStyle.Bottom:
            bounds1 = new Rectangle(this.m_oTrackBar.X, this.m_oTrackButton.Bounds.Bottom, this.m_oTrackBar.Width, this.m_oTrackBar.Bottom - this.m_oTrackButton.Bounds.Bottom);
            break;
          case DockStyle.Left:
            bounds1 = new Rectangle(this.m_oTrackBar.X, this.m_oTrackBar.Y, this.m_oTrackButton.Location.X - this.m_oTrackBar.X, this.m_oTrackBar.Height);
            break;
          case DockStyle.Right:
            bounds1 = new Rectangle(this.m_oTrackButton.Bounds.Right, this.m_oTrackBar.Y, this.m_oTrackBar.Right - this.m_oTrackButton.Bounds.Right, this.m_oTrackBar.Height);
            break;
        }
      }
      QAppearanceWrapper appearance1 = new QAppearanceWrapper((IQAppearance) this.m_oParent.Appearance);
      if (this.Horizontal)
        appearance1.AdjustBackgroundOrientationsToVertical();
      QColorSet itemColorSet1 = this.m_oParent.ColorHost.GetItemColorSet((object) this, QItemStates.Normal, (object) QCompositeScrollBarItem.ScrollBar);
      QShapePainterProperties properties1 = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
      QAppearanceFillerProperties fillerProperties = new QAppearanceFillerProperties();
      fillerProperties.DockStyle = this.Horizontal ? DockStyle.Right : DockStyle.Top;
      QShapePainter.Default.Paint(this.m_oParent.Bounds, appearance1.Shape, (IQAppearance) appearance1, itemColorSet1, properties1, fillerProperties, QPainterOptions.Default, graphics);
      if (!bounds1.IsEmpty && !flag1)
      {
        QColorSet itemColorSet2 = this.m_oParent.ColorHost.GetItemColorSet((object) this, QItemStates.Pressed, (object) QCompositeScrollBarItem.ScrollBar);
        QShapePainter.Default.Paint(bounds1, appearance1.Shape, (IQAppearance) appearance1, itemColorSet2, properties1, fillerProperties, QPainterOptions.Default, graphics);
      }
      for (int index = 0; index < this.m_oButtons.Count; ++index)
      {
        if (!this.m_oButtons[index].Size.IsEmpty && (!flag1 || this.m_oButtons[index] != this.m_oTrackButton))
        {
          QItemStates itemState = this.m_oButtons[index].ItemState;
          if (flag1)
            itemState |= QItemStates.Disabled;
          QColorSet itemColorSet3 = this.m_oParent.ColorHost.GetItemColorSet((object) this, itemState, (object) QCompositeScrollBarItem.ScrollButton);
          if (!flag2)
          {
            if (this.m_oButtons[index] == this.m_oUpButton || this.m_oButtons[index] == this.m_oDownButton)
            {
              if ((this.m_oParent.Configuration.AlwaysDrawBackground & QCompositeScrollBarItem.ScrollButton) != QCompositeScrollBarItem.ScrollButton)
              {
                itemColorSet3.Background1 = Color.Empty;
                itemColorSet3.Background2 = Color.Empty;
                itemColorSet3.Border = Color.Empty;
              }
            }
            else if (this.m_oButtons[index] == this.m_oTrackButton)
            {
              if ((this.m_oParent.Configuration.AlwaysDrawBackground & QCompositeScrollBarItem.ScrollBar) != QCompositeScrollBarItem.ScrollBar)
              {
                itemColorSet3.Background1 = Color.Empty;
                itemColorSet3.Background2 = Color.Empty;
                itemColorSet3.Border = Color.Empty;
              }
            }
            else if ((this.m_oParent.Configuration.AlwaysDrawBackground & QCompositeScrollBarItem.CustomButton) != QCompositeScrollBarItem.CustomButton)
            {
              itemColorSet3.Background1 = Color.Empty;
              itemColorSet3.Background2 = Color.Empty;
              itemColorSet3.Border = Color.Empty;
            }
          }
          DockStyle dockStyle = DockStyle.None;
          Rectangle bounds2 = this.m_oButtons[index].Bounds;
          Image image = (Image) null;
          switch (this.m_oParent.Dock)
          {
            case DockStyle.Top:
            case DockStyle.Bottom:
              if (this.m_oButtons[index] == this.m_oUpButton)
                image = (Image) QControlPaint.RotateFlipImage(this.m_oParent.Configuration.UsedButtonMask, RotateFlipType.Rotate270FlipNone);
              else if (this.m_oButtons[index] == this.m_oDownButton)
                image = (Image) QControlPaint.RotateFlipImage(this.m_oParent.Configuration.UsedButtonMask, RotateFlipType.Rotate90FlipNone);
              else if (this.m_oButtons[index] == this.m_oTrackButton)
                image = (Image) QControlPaint.RotateFlipImage(this.m_oParent.Configuration.UsedTrackButtonMask, RotateFlipType.Rotate90FlipNone);
              else if (this.m_oButtons[index] is QImageButtonArea)
                image = ((QImageButtonArea) this.m_oButtons[index]).Image;
              dockStyle = this.m_oButtons.IndexOf(this.m_oButtons[index]) <= this.m_oButtons.IndexOf(this.m_oTrackButton) ? DockStyle.Left : DockStyle.Right;
              break;
            case DockStyle.Left:
            case DockStyle.Right:
              if (this.m_oButtons[index] == this.m_oUpButton)
                image = this.m_oParent.Configuration.UsedButtonMask;
              else if (this.m_oButtons[index] == this.m_oDownButton)
                image = (Image) QControlPaint.RotateFlipImage(this.m_oParent.Configuration.UsedButtonMask, RotateFlipType.Rotate180FlipX);
              else if (this.m_oButtons[index] == this.m_oTrackButton)
                image = this.m_oParent.Configuration.UsedTrackButtonMask;
              else if (this.m_oButtons[index] is QImageButtonArea)
                image = ((QImageButtonArea) this.m_oButtons[index]).Image;
              dockStyle = this.m_oButtons.IndexOf(this.m_oButtons[index]) <= this.m_oButtons.IndexOf(this.m_oTrackButton) ? DockStyle.Top : DockStyle.Bottom;
              break;
          }
          Rectangle rectangle1 = this.m_oParent.Configuration.ButtonMargin.InflateRectangleWithMargin(bounds2, false, dockStyle);
          QShape shape;
          QAppearanceWrapper appearance2;
          if (this.m_oButtons[index] == this.m_oTrackButton)
          {
            shape = this.m_oParent.Configuration.TrackButtonAppearance.Shape;
            appearance2 = new QAppearanceWrapper((IQAppearance) this.m_oParent.Configuration.TrackButtonAppearance);
          }
          else
          {
            shape = this.m_oParent.Configuration.ButtonAppearance.Shape;
            appearance2 = new QAppearanceWrapper((IQAppearance) this.m_oParent.Configuration.ButtonAppearance);
          }
          if (this.Horizontal)
            appearance2.AdjustBackgroundOrientationsToVertical();
          if (shape != null)
          {
            QShapePainterProperties properties2 = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
            QShapePainter.Default.Paint(rectangle1, shape, (IQAppearance) appearance2, itemColorSet3, properties2, new QAppearanceFillerProperties()
            {
              DockStyle = dockStyle
            }, QPainterOptions.Default, graphics);
          }
          if (image != null)
          {
            Rectangle rectangle2 = this.m_oParent.Configuration.ButtonPadding.InflateRectangleWithPadding(rectangle1, false, dockStyle);
            Rectangle rectangle3 = shape.InflateRectangle(rectangle2, false, dockStyle);
            Rectangle rectangle4 = QMath.AlignElement(image.Size, ContentAlignment.MiddleCenter, rectangle3, true);
            QControlPaint.DrawImage(graphics, image, Color.Red, itemColorSet3.Foreground, rectangle4);
          }
        }
      }
    }

    public void Layout()
    {
      if (!this.m_oParent.IsVisible || this.m_oParent.Bounds.IsEmpty)
        return;
      Rectangle rectangle = this.m_oParent.Appearance.Shape.InflateRectangle(this.m_oParent.Bounds, false, this.Horizontal ? DockStyle.Right : DockStyle.Top);
      for (int index = 0; index < this.m_oButtons.Count; ++index)
      {
        if (this.m_oButtons[index] == this.m_oUpButton || this.m_oButtons[index] == this.m_oDownButton)
        {
          if (this.m_oButtons[index].Size != this.m_oParent.ButtonSize)
            this.m_oButtons[index].Size = this.m_oParent.ButtonSize;
        }
        else if (this.m_oButtons[index] != this.m_oTrackButton)
        {
          if (this.m_oButtons[index] is QImageButtonArea && ((QImageButtonArea) this.m_oButtons[index]).Image != null)
          {
            Size size = this.m_oParent.Configuration.ButtonPadding.InflateSizeWithPadding(((QImageButtonArea) this.m_oButtons[index]).Image.Size, true, !this.Horizontal);
            this.m_oButtons[index].Size = !this.Horizontal ? new Size(Math.Min(this.m_oParent.ButtonSize.Width, size.Width), size.Height) : new Size(size.Width, Math.Min(this.m_oParent.ButtonSize.Height, size.Height));
          }
          else if (this.m_oButtons[index].Size != this.m_oParent.ButtonSize)
            this.m_oButtons[index].Size = this.m_oParent.ButtonSize;
        }
      }
      int num1 = -1;
      int num2 = -1;
      for (int index = 0; index < this.m_oButtons.Count && num2 == -1 || index > num2 && num2 > -1; index += num2 > -1 ? -1 : 1)
      {
        switch (this.m_oParent.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            if (num2 > -1)
            {
              if (num1 < 0)
                num1 = rectangle.Right;
              num1 -= this.m_oButtons[index].Size.Width;
              this.m_oButtons[index].Location = new Point(num1, rectangle.Y);
              break;
            }
            if (num1 < 0)
              num1 = rectangle.X;
            this.m_oButtons[index].Location = new Point(num1, rectangle.Y);
            if (this.m_oButtons[index] == this.m_oTrackButton)
            {
              num2 = index;
              index = this.m_oButtons.Count;
              this.m_oTrackBar.X = num1;
              this.m_oTrackBar.Y = rectangle.Y;
              num1 = -1;
              break;
            }
            num1 += this.m_oButtons[index].Size.Width;
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            if (num2 > -1)
            {
              if (num1 < 0)
                num1 = rectangle.Bottom;
              num1 -= this.m_oButtons[index].Size.Height;
              this.m_oButtons[index].Location = new Point(rectangle.X, num1);
              break;
            }
            if (num1 < 0)
              num1 = rectangle.Y;
            this.m_oButtons[index].Location = new Point(rectangle.X, num1);
            if (this.m_oButtons[index] == this.m_oTrackButton)
            {
              num2 = index;
              index = this.m_oButtons.Count;
              this.m_oTrackBar.Y = num1;
              this.m_oTrackBar.X = rectangle.X;
              num1 = -1;
              break;
            }
            num1 += this.m_oButtons[index].Size.Height;
            break;
        }
      }
      Size size1 = this.m_oParent.Configuration.UsedTrackButtonMask.Size;
      Size size2 = QPadding.InflateSize(this.m_oParent.Configuration.UsedTrackButtonMask.Size, new IQPadding[1]
      {
        (IQPadding) this.m_oParent.Configuration.ButtonPadding
      }, true, true);
      switch (this.m_oParent.Dock)
      {
        case DockStyle.Top:
        case DockStyle.Bottom:
          this.m_oTrackBar.Size = new Size(num1 - this.m_oTrackBar.Location.X, this.m_oParent.ButtonSize.Height);
          int num3 = this.m_oParent.ContentSize.Width - this.m_oParent.ViewPort.Width;
          if (num3 <= 0)
            break;
          int width = this.m_oTrackBar.Width;
          this.m_oTrackButton.Size = this.m_oParent.ViewPort.Width < this.m_oParent.ContentSize.Width ? new Size(Math.Max(size2.Width, this.m_oTrackBar.Width * this.m_oParent.ViewPort.Width / this.m_oParent.ContentSize.Width), this.m_oTrackBar.Height) : new Size(this.m_oTrackBar.Width, this.m_oTrackBar.Height);
          this.m_oTrackButton.Location = new Point(this.m_oTrackBar.Location.X + (width - this.m_oTrackButton.Size.Width) * Math.Abs(this.m_oParent.ScrollOffset.X) / num3, this.m_oTrackBar.Location.Y);
          break;
        case DockStyle.Left:
        case DockStyle.Right:
          this.m_oTrackBar.Size = new Size(this.m_oParent.ButtonSize.Width, num1 - this.m_oTrackBar.Location.Y);
          int num4 = this.m_oParent.ContentSize.Height - this.m_oParent.ViewPort.Height;
          if (num4 <= 0)
            break;
          int height = this.m_oTrackBar.Height;
          this.m_oTrackButton.Size = this.m_oParent.ViewPort.Height < this.m_oParent.ContentSize.Height ? new Size(this.m_oTrackBar.Width, Math.Max(size2.Height, this.m_oTrackBar.Height * this.m_oParent.ViewPort.Height / this.m_oParent.ContentSize.Height)) : new Size(this.m_oTrackBar.Width, this.m_oTrackBar.Height);
          this.m_oTrackButton.Location = new Point(this.m_oTrackBar.Location.X, this.m_oTrackBar.Location.Y + (height - this.m_oTrackButton.Size.Height) * Math.Abs(this.m_oParent.ScrollOffset.Y) / num4);
          break;
      }
    }

    private void HandleTracking(Point point)
    {
      point.Offset(-this.m_oTrackingOffset.X, -this.m_oTrackingOffset.Y);
      int scrollOffset = this.ConvertPointOnTrackBarToScrollOffset(this.Horizontal ? point.X : point.Y);
      if (scrollOffset == (this.Horizontal ? this.m_oParent.ScrollOffset.X : this.m_oParent.ScrollOffset.Y))
        return;
      this.m_oParent.ScrollIntoView(new Rectangle(this.m_oParent.ViewPort.X + (this.Horizontal ? scrollOffset : Math.Abs(this.m_oParent.ScrollOffset.X)), this.m_oParent.ViewPort.Y + (this.Horizontal ? Math.Abs(this.m_oParent.ScrollOffset.Y) : scrollOffset), this.m_oParent.ViewPort.Width, this.m_oParent.ViewPort.Height), false);
    }

    private int ConvertPointOnTrackBarToScrollOffset(int coordinate)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      switch (this.m_oParent.Dock)
      {
        case DockStyle.Top:
        case DockStyle.Bottom:
          num1 = this.m_oParent.ContentSize.Width - this.m_oParent.ViewPort.Width;
          num2 = this.m_oTrackBar.Width - this.m_oTrackButton.Bounds.Width;
          if (this.m_oTrackButton.Bounds.X == coordinate)
            return this.m_oParent.ScrollOffset.X;
          num3 = coordinate - this.m_oTrackBar.Left;
          break;
        case DockStyle.Left:
        case DockStyle.Right:
          num1 = this.m_oParent.ContentSize.Height - this.m_oParent.ViewPort.Height;
          num2 = this.m_oTrackBar.Height - this.m_oTrackButton.Bounds.Height;
          if (this.m_oTrackButton.Bounds.Y == coordinate)
            return this.m_oParent.ScrollOffset.Y;
          num3 = coordinate - this.m_oTrackBar.Top;
          break;
      }
      return num3 * num1 / num2;
    }

    private void StartTracking(Point point)
    {
      if (this.Horizontal)
      {
        if (this.m_oParent.IsAtHorizontalEnd && this.m_oParent.IsAtHorizontalStart)
          return;
      }
      else if (this.m_oParent.IsAtVerticalEnd && this.m_oParent.IsAtVerticalStart)
        return;
      this.m_bTracking = true;
      this.m_oTrackingOffset = new Point(point.X - this.m_oTrackButton.Location.X, point.Y - this.m_oTrackButton.Location.Y);
    }

    private void StopTracking() => this.m_bTracking = false;

    private void m_oButtons_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      QItemStates state1 = QItemStatesHelper.FromButtonState(e.FromState);
      QItemStates state2 = QItemStatesHelper.FromButtonState(e.ToState);
      bool flag1 = !QItemStatesHelper.IsPressed(state1) && QItemStatesHelper.IsPressed(state2);
      bool flag2 = QItemStatesHelper.IsPressed(state1) && !QItemStatesHelper.IsPressed(state2);
      bool flag3 = !QItemStatesHelper.IsHot(state1) && QItemStatesHelper.IsHot(state2);
      bool flag4 = QItemStatesHelper.IsHot(state1) && !QItemStatesHelper.IsHot(state2);
      if (flag1 && !this.m_oParent.Configuration.ScrollOnMouseOver || flag3 && this.m_oParent.Configuration.ScrollOnMouseOver)
      {
        if (sender == this.m_oUpButton || sender == this.m_oDownButton)
        {
          switch (this.m_oParent.Dock)
          {
            case DockStyle.Top:
            case DockStyle.Bottom:
              this.m_oParent.ScrollHorizontal(!this.m_oParent.Configuration.ScrollAnimated ? (sender == this.m_oUpButton ? this.m_oParent.ScrollStepSize : -this.m_oParent.ScrollStepSize) : (sender == this.m_oUpButton ? this.m_oParent.ViewPort.Left : this.m_oParent.ViewPort.Left + this.m_oParent.ContentSize.Width), this.m_oParent.Configuration.ScrollAnimated ? QScrollablePartMethod.IntoView : QScrollablePartMethod.Relative, this.m_oParent.Configuration.ScrollAnimated, false);
              break;
            case DockStyle.Left:
            case DockStyle.Right:
              this.m_oParent.ScrollVertical(!this.m_oParent.Configuration.ScrollAnimated ? (sender == this.m_oUpButton ? this.m_oParent.ScrollStepSize : -this.m_oParent.ScrollStepSize) : (sender == this.m_oUpButton ? this.m_oParent.ViewPort.Top : this.m_oParent.ViewPort.Top + this.m_oParent.ContentSize.Height), this.m_oParent.Configuration.ScrollAnimated ? QScrollablePartMethod.IntoView : QScrollablePartMethod.Relative, this.m_oParent.Configuration.ScrollAnimated, false);
              break;
          }
        }
        else if (flag1 && sender == this.m_oTrackButton)
          this.StartTracking(new Point(e.X, e.Y));
      }
      else if (flag2 && !this.m_oParent.Configuration.ScrollOnMouseOver || flag4 && this.m_oParent.Configuration.ScrollOnMouseOver)
      {
        if (sender == this.m_oUpButton || sender == this.m_oDownButton)
        {
          if (this.m_oParent.IsScrollingAnimated)
            this.m_oParent.StopScrolling();
        }
        else if (flag2 && sender == this.m_oTrackButton)
          this.StopTracking();
      }
      if (this.m_oParent == null)
        return;
      this.m_oParent.Invalidate();
    }

    private class QButtonAreaCollection : CollectionBase, IQWeakEventPublisher
    {
      private bool m_bWeakEventHandlers = true;
      private QWeakDelegate m_oButtonStateChangedDelegate;

      [Browsable(false)]
      [DefaultValue(true)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public bool WeakEventHandlers
      {
        get => this.m_bWeakEventHandlers;
        set => this.m_bWeakEventHandlers = value;
      }

      [QWeakEvent]
      public event QButtonAreaEventHandler ButtonStateChanged
      {
        add => this.m_oButtonStateChangedDelegate = QWeakDelegate.Combine(this.m_oButtonStateChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
        remove => this.m_oButtonStateChangedDelegate = QWeakDelegate.Remove(this.m_oButtonStateChangedDelegate, (Delegate) value);
      }

      internal virtual void OnButtonStateChanged(object sender, QButtonAreaEventArgs e) => this.m_oButtonStateChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oButtonStateChangedDelegate, sender, (object) e);

      public void AddButtonArea(QButtonArea button)
      {
        if (button == null || this.InnerList.Contains((object) button))
          return;
        button.ButtonStateChanged += new QButtonAreaEventHandler(this.button_ButtonStateChanged);
        this.InnerList.Add((object) button);
      }

      public void RemoveButtonArea(QButtonArea button)
      {
        if (button == null || !this.InnerList.Contains((object) button))
          return;
        button.ButtonStateChanged += new QButtonAreaEventHandler(this.button_ButtonStateChanged);
        this.InnerList.Remove((object) button);
      }

      public void InsertButtonArea(int index, QButtonArea button)
      {
        if (button == null || this.InnerList.Contains((object) button))
          return;
        this.InnerList.Insert(index, (object) button);
      }

      public int IndexOf(QButtonArea buttonArea) => this.InnerList.IndexOf((object) buttonArea);

      public QButtonArea this[int index] => this.InnerList[index] as QButtonArea;

      private void button_ButtonStateChanged(object sender, QButtonAreaEventArgs e) => this.OnButtonStateChanged(sender, e);
    }
  }
}
