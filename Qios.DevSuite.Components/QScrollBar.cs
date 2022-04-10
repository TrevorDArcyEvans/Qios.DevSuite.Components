// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DefaultEvent("Scroll")]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QScrollBar.bmp")]
  [Designer(typeof (QScrollBarDesigner), typeof (IDesigner))]
  [ToolboxItem(true)]
  public class QScrollBar : QControl, IQScrollBarHandlerParent, IQItemColorHost, IQTimerClient
  {
    private const int m_iTimerInterval = 10;
    private const int m_iTimerIntervalPage = 300;
    private QTimerManager m_oTimerManager;
    private int m_iValue;
    private int m_iMinimum;
    private int m_iMaximum = 100;
    private int m_iLargeChange = 10;
    private int m_iSmallChange = 1;
    private Size m_oCachedRequestedSize = Size.Empty;
    private int m_iScrollStepSize;
    private bool m_bIsScrollingAnimatedHorizontal;
    private bool m_bIsScrollingAnimatedVertical;
    private Point m_oScrollUntilOffset = Point.Empty;
    private bool m_bPaintTransparentBackground;
    private QItemStates m_oItemState;
    private QScrollBarConfiguration m_oConfiguration;
    private QScrollBarHandler m_oScrollBarHandler;
    private QWeakDelegate m_oScrollDelegate;
    private QScrollBarDirection m_oCurrentDirection;

    public QScrollBar()
    {
      this.m_oConfiguration = this.CreateConfiguration();
      this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oCurrentDirection = this.m_oConfiguration.Direction;
      this.m_oScrollBarHandler = new QScrollBarHandler((IQScrollBarHandlerParent) this);
      this.m_oTimerManager = new QTimerManager(10);
      this.Height = 200;
    }

    public bool ShouldSerializeConfiguration() => this.Configuration != null && !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration()
    {
      if (this.Configuration == null)
        return;
      this.Configuration.SetToDefaultValues();
    }

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration for the QScrollBar.")]
    public QScrollBarConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= new EventHandler(this.Configuration_ConfigurationChanged);
        this.m_oConfiguration = value;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
        this.Configuration_ConfigurationChanged((object) null, EventArgs.Empty);
      }
    }

    [Description("Gets or sets a numeric value that represents the current position of the scroll box on the scroll bar control.")]
    [Category("QBehavior")]
    [DefaultValue(0)]
    public int Value
    {
      get => this.m_iValue + this.m_iMinimum;
      set
      {
        this.m_iValue = Math.Max(Math.Min(this.m_iMaximum - this.m_iLargeChange, value) - this.m_iMinimum, 0);
        this.SynchronizeScrollButtons(true);
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets a value to be added to or substracted from the Value property when the scroll box is moved a large distance.")]
    [DefaultValue(10)]
    public int LargeChange
    {
      get => this.m_iLargeChange;
      set
      {
        this.m_iLargeChange = value;
        this.SynchronizeScrollButtons(true);
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets a value to be added to or substracted from the Value property when the scroll box is moved a small distance")]
    [DefaultValue(1)]
    public int SmallChange
    {
      get => this.m_iSmallChange;
      set
      {
        this.m_iSmallChange = value;
        this.SynchronizeScrollButtons(true);
      }
    }

    [Description("Gets or sets the lower limit of values of the scrollable range")]
    [Category("QBehavior")]
    [DefaultValue(0)]
    public int Minimum
    {
      get => this.m_iMinimum;
      set
      {
        this.m_iMinimum = value;
        this.SynchronizeScrollButtons(true);
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets the upper limit of values of the scrollable range")]
    [DefaultValue(100)]
    public int Maximum
    {
      get => this.m_iMaximum;
      set
      {
        this.m_iMaximum = value;
        this.SynchronizeScrollButtons(true);
      }
    }

    [DefaultValue(false)]
    [Category("QBehavior")]
    [Description("Gets or sets whether a transparent background must be painted. When this is false the background color of the parent is painted on this Control. If this is true the parent is painted on this control. Keeping this false increases performance. Set this to false when the Control is situated on a Parent with a solid background or when the control has a rectangular filled out shape.")]
    public virtual bool PaintTransparentBackground
    {
      get => this.m_bPaintTransparentBackground;
      set
      {
        this.m_bPaintTransparentBackground = value;
        this.Invalidate();
      }
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.ScrollBar)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets the QScrollBar for the QControl.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QScrollBarAppearance Appearance => base.Appearance as QScrollBarAppearance;

    protected override string BackColorPropertyName => "ScrollBarBackground1";

    protected override string BackColor2PropertyName => "ScrollBarBackground2";

    protected override string BorderColorPropertyName => "ScrollBarBorder";

    public void StopScrolling()
    {
      this.m_bIsScrollingAnimatedHorizontal = false;
      this.m_bIsScrollingAnimatedVertical = false;
      this.UpdateScrollTimer();
    }

    public void ScrollHorizontal(
      int xValue,
      QScrollablePartMethod method,
      bool animated,
      bool page)
    {
      int x = 0;
      switch (method)
      {
        case QScrollablePartMethod.IntoView:
          x = QScrollHelper.TranslateCoordinateToOffset(xValue, this.ScrollData.ViewPort.Left, this.ScrollData.ViewPort.Right, this.ScrollData.ScrollOffset.X);
          break;
        case QScrollablePartMethod.Relative:
          x = this.ScrollData.ScrollOffset.X + xValue;
          break;
      }
      int num = this.CorrectScrollOffsetX(x);
      if (animated)
      {
        this.m_oScrollUntilOffset.X = num;
        this.m_bIsScrollingAnimatedHorizontal = true;
        this.m_iScrollStepSize = page ? this.ScrollData.ViewPort.Width : this.m_iSmallChange;
        bool flag = this.m_oTimerManager.IsRegistered((IQTimerClient) this);
        this.UpdateScrollTimer(page ? 300 : 10);
        if (!page || flag)
          return;
        this.HandleScrollTimerTick();
      }
      else
      {
        this.SetScrollOffsetX(num);
        this.SynchronizeScrollButtons(true);
      }
    }

    public void ScrollVertical(int yValue, QScrollablePartMethod method, bool animated, bool page)
    {
      int y = 0;
      switch (method)
      {
        case QScrollablePartMethod.IntoView:
          y = QScrollHelper.TranslateCoordinateToOffset(yValue, this.ScrollData.ViewPort.Top, this.ScrollData.ViewPort.Bottom, this.ScrollData.ScrollOffset.Y);
          break;
        case QScrollablePartMethod.Relative:
          y = this.ScrollData.ScrollOffset.Y + yValue;
          break;
      }
      int num = this.CorrectScrollOffsetY(y);
      if (animated)
      {
        this.m_oScrollUntilOffset.Y = num;
        this.m_bIsScrollingAnimatedVertical = true;
        this.m_iScrollStepSize = page ? this.ScrollData.ViewPort.Height : this.m_iSmallChange;
        bool flag = this.m_oTimerManager.IsRegistered((IQTimerClient) this);
        this.UpdateScrollTimer(page ? 300 : 10);
        if (!page || flag)
          return;
        this.HandleScrollTimerTick();
      }
      else
      {
        this.SetScrollOffsetY(num);
        this.SynchronizeScrollButtons(true);
      }
    }

    protected override QBalloon CreateBalloon() => (QBalloon) new QCompositeBalloon();

    protected virtual QScrollBarConfiguration CreateConfiguration() => new QScrollBarConfiguration();

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QScrollBarAppearance();

    protected virtual void OnScroll(QScrollEventArgs e) => this.m_oScrollDelegate = QWeakDelegate.InvokeDelegate(this.m_oScrollDelegate, (object) this, (object) e);

    protected override void OnDockChanged(EventArgs e)
    {
      base.OnDockChanged(e);
      if (this.Dock == DockStyle.Right || this.Dock == DockStyle.Left)
        this.Configuration.Direction = QScrollBarDirection.Vertical;
      else
        this.Configuration.Direction = QScrollBarDirection.Horizontal;
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.m_oScrollBarHandler == null)
        return;
      this.m_oScrollBarHandler.Layout();
      BoundsSpecified specified = BoundsSpecified.None;
      Size requestedSize = this.ScrollData.RequestedSize;
      if (this.Configuration.Direction == QScrollBarDirection.Vertical)
      {
        if (this.Width != requestedSize.Width)
          specified |= BoundsSpecified.Width;
      }
      else if (this.Height != requestedSize.Height)
        specified |= BoundsSpecified.Height;
      if (specified == BoundsSpecified.None)
        return;
      this.SetBounds(0, 0, requestedSize.Width, requestedSize.Height, specified);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.m_oScrollBarHandler == null)
        return;
      this.m_oScrollBarHandler.Paint(e.Graphics, this.m_oItemState);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.Parent == null)
        pevent.Graphics.Clear(SystemColors.Control);
      else if (this.m_bPaintTransparentBackground)
        QControlPaint.PaintTransparentBackground((Control) this, pevent);
      else
        pevent.Graphics.Clear(this.Parent.BackColor);
      if (this.BackgroundImage == null)
        return;
      QControlPaint.DrawImage(this.BackgroundImage, this.BackgroundImageAlign, this.BackgroundImageOffset, this.ClientRectangle, this.BackgroundImage.Size, pevent.Graphics, (ImageAttributes) null, false);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!QItemStatesHelper.IsHot(this.m_oItemState))
      {
        this.m_oItemState = QItemStatesHelper.AdjustState(this.m_oItemState, QItemStates.Hot, true);
        this.Invalidate();
      }
      if (this.m_oScrollBarHandler == null)
        return;
      this.m_oScrollBarHandler.HandleMouseMove(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.m_oScrollBarHandler == null)
        return;
      this.m_oScrollBarHandler.HandleMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.m_oScrollBarHandler == null)
        return;
      this.m_oScrollBarHandler.HandleMouseUp(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (QItemStatesHelper.IsHot(this.m_oItemState))
      {
        this.m_oItemState = QItemStatesHelper.AdjustState(this.m_oItemState, QItemStates.Hot, false);
        this.Invalidate();
      }
      base.OnMouseLeave(e);
      if (this.m_oScrollBarHandler == null)
        return;
      this.m_oScrollBarHandler.HandleMouseLeave(new MouseEventArgs(Control.MouseButtons, 0, 0, 0, 0));
    }

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      if (this.m_oCurrentDirection != this.Configuration.Direction)
      {
        this.m_oCurrentDirection = this.Configuration.Direction;
        if (this.m_oCurrentDirection == QScrollBarDirection.Horizontal)
          this.SetBounds(0, 0, this.Height, 0, BoundsSpecified.Width);
        else
          this.SetBounds(0, 0, 0, this.Width, BoundsSpecified.Height);
      }
      this.m_oCachedRequestedSize = Size.Empty;
      if (this.m_oScrollBarHandler != null)
        this.m_oScrollBarHandler.Layout();
      this.PerformLayout();
      this.Invalidate();
    }

    private Color RetrieveFirstDefinedColor(string name) => this.ColorScheme[name].Current;

    private void HandleScrollTimerTick()
    {
      if (this.m_bIsScrollingAnimatedHorizontal)
      {
        int x = this.ScrollData.ScrollOffset.X;
        this.m_bIsScrollingAnimatedHorizontal = QScrollHelper.HandleOneDirectionScrollTimerTick(ref x, this.m_oScrollUntilOffset.X, this.m_iScrollStepSize);
        this.SetScrollOffsetX(x);
      }
      if (this.m_bIsScrollingAnimatedVertical)
      {
        int y = this.ScrollData.ScrollOffset.Y;
        this.m_bIsScrollingAnimatedVertical = QScrollHelper.HandleOneDirectionScrollTimerTick(ref y, this.m_oScrollUntilOffset.Y, this.m_iScrollStepSize);
        this.SetScrollOffsetY(y);
      }
      this.SynchronizeScrollButtons(false);
      this.Invalidate();
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
      if (this.ScrollData.IsScrollingAnimated && !this.m_oTimerManager.IsRegistered((IQTimerClient) this))
      {
        this.m_oTimerManager.Register((IQTimerClient) this);
      }
      else
      {
        if (this.ScrollData.IsScrollingAnimated || !this.m_oTimerManager.IsRegistered((IQTimerClient) this))
          return;
        this.m_oTimerManager.Unregister((IQTimerClient) this);
      }
    }

    private void SynchronizeScrollButtons(bool invalidate)
    {
      if (this.m_oScrollBarHandler != null)
        this.m_oScrollBarHandler.Layout();
      this.m_oItemState = QItemStatesHelper.AdjustState(this.m_oItemState, QItemStates.Disabled, this.Configuration.Direction == QScrollBarDirection.Vertical ? this.ScrollData.IsAtVerticalStart && this.ScrollData.IsAtVerticalEnd : this.ScrollData.IsAtHorizontalStart && this.ScrollData.IsAtHorizontalEnd);
      if (!invalidate)
        return;
      this.Invalidate();
    }

    private int CorrectScrollOffsetX(int x)
    {
      if (x == 0)
        return 0;
      if (this.ScrollData.ViewPort.Left + x + this.ScrollData.ContentSize.Width < this.ScrollData.ViewPort.Right)
        x = this.ScrollData.ViewPort.Width - this.ScrollData.ContentSize.Width;
      x = Math.Min(0, x);
      return x;
    }

    private int CorrectScrollOffsetY(int y)
    {
      if (y == 0)
        return 0;
      if (this.ScrollData.ViewPort.Top + y + this.ScrollData.ContentSize.Height < this.ScrollData.ViewPort.Bottom)
        y = this.ScrollData.ViewPort.Height - this.ScrollData.ContentSize.Height;
      y = Math.Min(0, y);
      return y;
    }

    private void SetScrollOffsetX(int value)
    {
      int currentValue = this.Value;
      this.m_iValue = Math.Abs(value);
      this.OnScroll(new QScrollEventArgs(currentValue, this.Value));
    }

    private void SetScrollOffsetY(int value)
    {
      int currentValue = this.Value;
      this.m_iValue = Math.Abs(value);
      this.OnScroll(new QScrollEventArgs(currentValue, this.Value));
    }

    private Size CalculateRequestedButtonSize()
    {
      QShape shape = this.Configuration.ButtonAppearance.Shape;
      Size size = this.Configuration.UsedButtonMask.Size;
      if (shape != null)
        size = shape.InflateSize(size, true, true);
      Size requestedButtonSize = this.Configuration.ButtonMargin.InflateSizeWithMargin(this.Configuration.ButtonPadding.InflateSizeWithPadding(size, true, true), true, true);
      if (this.Configuration.Direction == QScrollBarDirection.Horizontal)
        requestedButtonSize = new Size(requestedButtonSize.Height, requestedButtonSize.Width);
      return requestedButtonSize;
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the QScrollBar value changes")]
    public event QScrollEventHandler Scroll
    {
      add => this.m_oScrollDelegate = QWeakDelegate.Combine(this.m_oScrollDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oScrollDelegate = QWeakDelegate.Remove(this.m_oScrollDelegate, (Delegate) value);
    }

    bool IQScrollBarHandlerParent.IsScrollingAnimated => this.m_bIsScrollingAnimatedHorizontal || this.m_bIsScrollingAnimatedVertical;

    private IQScrollBarHandlerParent ScrollData => (IQScrollBarHandlerParent) this;

    void IQScrollBarHandlerParent.ScrollIntoView(
      Rectangle bounds,
      bool animated)
    {
      int newValue1 = 0;
      int newValue2 = 0;
      bool scrollIntoViewAmount1 = QScrollHelper.CalculateScrollIntoViewAmount(bounds.Left, bounds.Right, this.ScrollData.ViewPort.Left, this.ScrollData.ViewPort.Right, this.ScrollData.ScrollOffset.X, out newValue1);
      bool scrollIntoViewAmount2 = QScrollHelper.CalculateScrollIntoViewAmount(bounds.Top, bounds.Bottom, this.ScrollData.ViewPort.Top, this.ScrollData.ViewPort.Bottom, this.ScrollData.ScrollOffset.Y, out newValue2);
      if (scrollIntoViewAmount1)
        this.ScrollHorizontal(newValue1, QScrollablePartMethod.IntoView, animated, false);
      if (!scrollIntoViewAmount2)
        return;
      this.ScrollVertical(newValue2, QScrollablePartMethod.IntoView, animated, false);
    }

    bool IQScrollBarHandlerParent.IsAtHorizontalStart => this.ScrollData.ScrollOffset.X == 0;

    bool IQScrollBarHandlerParent.IsAtVerticalStart => this.ScrollData.ScrollOffset.Y == 0;

    bool IQScrollBarHandlerParent.IsAtHorizontalEnd => this.ScrollData.ViewPort.Left + this.ScrollData.ScrollOffset.X + this.ScrollData.ContentSize.Width <= this.ScrollData.ViewPort.Right;

    bool IQScrollBarHandlerParent.IsAtVerticalEnd => this.ScrollData.ViewPort.Top + this.ScrollData.ScrollOffset.Y + this.ScrollData.ContentSize.Height <= this.ScrollData.ViewPort.Bottom;

    Point IQScrollBarHandlerParent.ScrollOffset => new Point(-this.m_iValue, -this.m_iValue);

    bool IQScrollBarHandlerParent.IsVisible => true;

    Size IQScrollBarHandlerParent.RequestedSize => this.ScrollData.Appearance.Shape.InflateSize(this.ScrollData.ButtonSize, true, this.Configuration.Direction == QScrollBarDirection.Vertical);

    Size IQScrollBarHandlerParent.ButtonSize
    {
      get
      {
        if (this.m_oCachedRequestedSize.IsEmpty)
          this.m_oCachedRequestedSize = this.CalculateRequestedButtonSize();
        return this.m_oCachedRequestedSize;
      }
    }

    DockStyle IQScrollBarHandlerParent.Dock => this.Configuration.Direction == QScrollBarDirection.Horizontal ? DockStyle.Bottom : DockStyle.Right;

    Rectangle IQScrollBarHandlerParent.ViewPort => new Rectangle(0, 0, this.m_iLargeChange, this.m_iLargeChange);

    Size IQScrollBarHandlerParent.ContentSize => new Size(this.m_iMaximum - this.m_iMinimum, this.m_iMaximum - this.m_iMinimum);

    Rectangle IQScrollBarHandlerParent.Bounds => new Rectangle(Point.Empty, this.Size);

    int IQScrollBarHandlerParent.ScrollStepSize => this.m_iSmallChange;

    IQItemColorHost IQScrollBarHandlerParent.ColorHost => (IQItemColorHost) this;

    QColorSet IQItemColorHost.GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      QColorSet itemColorSet = new QColorSet();
      if (additionalProperties is QCompositeScrollBarItem.ScrollBar)
      {
        if (QItemStatesHelper.IsPressed(state))
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("ScrollBarPressedBackground1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("ScrollBarPressedBackground2");
          itemColorSet.Border = this.RetrieveFirstDefinedColor("ScrollBarPressedBorder");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("CompositeText");
        }
        else
        {
          itemColorSet.Background1 = this.RetrieveFirstDefinedColor("ScrollBarBackground1");
          itemColorSet.Background2 = this.RetrieveFirstDefinedColor("ScrollBarBackground2");
          itemColorSet.Border = this.RetrieveFirstDefinedColor("ScrollBarBorder");
          itemColorSet.Foreground = this.RetrieveFirstDefinedColor("CompositeText");
        }
      }
      else if (QItemStatesHelper.IsDisabled(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("ScrollBarButtonDisabledBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("ScrollBarButtonDisabledBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("ScrollBarButtonDisabledBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("CompositeTextDisabled");
      }
      else if (QItemStatesHelper.IsNormal(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("ScrollBarButtonBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("ScrollBarButtonBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("ScrollBarButtonBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("CompositeText");
      }
      else if (QItemStatesHelper.IsPressed(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("ScrollBarButtonPressedBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("ScrollBarButtonPressedBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("ScrollBarButtonPressedBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("CompositeTextPressed");
      }
      else if (QItemStatesHelper.IsHot(state))
      {
        itemColorSet.Background1 = this.RetrieveFirstDefinedColor("ScrollBarButtonHotBackground1");
        itemColorSet.Background2 = this.RetrieveFirstDefinedColor("ScrollBarButtonHotBackground2");
        itemColorSet.Border = this.RetrieveFirstDefinedColor("ScrollBarButtonHotBorder");
        itemColorSet.Foreground = this.RetrieveFirstDefinedColor("CompositeTextHot");
      }
      return itemColorSet;
    }

    void IQTimerClient.Tick(QTimerManager manager) => this.HandleScrollTimerTick();

    void IQScrollBarHandlerParent.Invalidate() => this.Invalidate();
  }
}
