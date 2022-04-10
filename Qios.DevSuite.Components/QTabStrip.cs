// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStrip
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  public class QTabStrip : IDisposable, IQWeakEventPublisher
  {
    private const double DropScrollRange = 0.15;
    private bool m_bWeakEventHandlers = true;
    private bool m_bSuspendLayout;
    private bool m_bIsScrolling;
    private bool m_bRequestingLayout;
    private int m_iScrollingTill;
    private QTabButton m_oButtonToActiveAfterRemoval;
    private bool m_bHandlingRemoval;
    private GraphicsPath m_oLastDrawnGraphicsPath;
    private bool m_bIsInitializing;
    private QTabStripPainter m_oPainter;
    private QTabStripNavigationArea m_oNavigationArea;
    private QTabStripConfiguration m_oConfiguration;
    private QTabButtonDropArea m_oDropArea;
    private QTabButtonDropAreaCollection m_aDropAreaCollection;
    private QTabButtonCollection m_aTabButtons;
    private QTabButtonRowCollection m_aTabButtonRows;
    private Rectangle m_oBounds;
    private Rectangle m_oOuterBounds;
    private Size m_oCalculatedSize;
    private Rectangle m_oCalculatedContentBounds;
    private Rectangle m_oCalculatedButtonAreaBounds;
    private Rectangle m_oCalculatedScrollArea;
    private Size m_oRequiredButtonsSize;
    private Size m_oLastCalculatedAvailableSize;
    private DockStyle m_eDock = DockStyle.Bottom;
    private Control m_oParent;
    private IQTabStripHost m_oTabStripHost;
    private QTabButton m_oActiveButton;
    private QTabButton m_oHotButton;
    private Point m_oScrollPosition = new Point(0, 0);
    private bool m_bIsDisposed;
    private object m_oAdditionalData;
    private QWeakDelegate m_oActiveButtonChangingDelegate;
    private QWeakDelegate m_oActiveButtonChangedDelegate;
    private QWeakDelegate m_oHotButtonChangedDelegate;
    private QWeakDelegate m_oCloseButtonClickDelegate;

    internal QTabStrip(IQTabStripHost tabStripHost, Font font, DockStyle dockStyle)
    {
      this.SuspendLayout();
      this.m_oNavigationArea = this.CreateNavigationArea();
      this.m_oNavigationArea.ButtonStateChanged += new QButtonAreaEventHandler(this.NavigationButtons_ButtonStateChanged);
      this.m_oParent = tabStripHost as Control;
      this.m_oTabStripHost = tabStripHost;
      this.m_eDock = dockStyle;
      this.m_oPainter = this.CreatePainter();
      this.m_oConfiguration = this.CreateConfiguration(font);
      this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_aTabButtons = new QTabButtonCollection(this);
      this.m_aTabButtonRows = new QTabButtonRowCollection(this);
      this.m_aDropAreaCollection = new QTabButtonDropAreaCollection(this);
      this.UpdatePropertiesFromConfiguration();
      this.ResumeLayout();
    }

    protected virtual QTabStripPainter CreatePainter() => new QTabStripPainter();

    protected virtual QTabStripConfiguration CreateConfiguration(Font font) => new QTabStripConfiguration(font);

    protected virtual QTabStripNavigationArea CreateNavigationArea() => new QTabStripNavigationArea(this);

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the active button is about to change.")]
    public event QTabStripButtonChangeEventHandler ActiveButtonChanging
    {
      add => this.m_oActiveButtonChangingDelegate = QWeakDelegate.Combine(this.m_oActiveButtonChangingDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oActiveButtonChangingDelegate = QWeakDelegate.Remove(this.m_oActiveButtonChangingDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the active button is changed.")]
    [Category("QEvents")]
    public event QTabStripButtonChangeEventHandler ActiveButtonChanged
    {
      add => this.m_oActiveButtonChangedDelegate = QWeakDelegate.Combine(this.m_oActiveButtonChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oActiveButtonChangedDelegate = QWeakDelegate.Remove(this.m_oActiveButtonChangedDelegate, (Delegate) value);
    }

    [Description("Gets raised when the hot button is changed")]
    [QWeakEvent]
    [Category("QEvents")]
    public event QTabStripButtonChangeEventHandler HotButtonChanged
    {
      add => this.m_oHotButtonChangedDelegate = QWeakDelegate.Combine(this.m_oHotButtonChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oHotButtonChangedDelegate = QWeakDelegate.Remove(this.m_oHotButtonChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the close button is clicked")]
    public event EventHandler CloseButtonClick
    {
      add => this.m_oCloseButtonClickDelegate = QWeakDelegate.Combine(this.m_oCloseButtonClickDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oCloseButtonClickDelegate = QWeakDelegate.Remove(this.m_oCloseButtonClickDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public QTabStripConfiguration Configuration => this.m_oConfiguration;

    [Localizable(true)]
    public Font Font
    {
      get => this.m_oConfiguration.Font;
      set => this.m_oConfiguration.PutFont(value);
    }

    public object AdditionalData
    {
      get => this.m_oAdditionalData;
      set => this.m_oAdditionalData = value;
    }

    public GraphicsPath LastDrawnGraphicsPath
    {
      get => this.m_oLastDrawnGraphicsPath;
      set
      {
        if (this.m_oLastDrawnGraphicsPath == value)
          return;
        if (this.m_oLastDrawnGraphicsPath != null)
          this.m_oLastDrawnGraphicsPath.Dispose();
        this.m_oLastDrawnGraphicsPath = value;
      }
    }

    public bool CloseButtonEnabled
    {
      get => this.m_oNavigationArea.Close.Enabled;
      set
      {
        if (this.m_oNavigationArea.Close.Enabled == value)
          return;
        this.m_oNavigationArea.Close.Enabled = value;
        this.HandleTabStripChanged(QCommandUIRequest.Redraw, this.NavigationArea.CalculateBoundsToControl(this.NavigationArea.Close.Bounds));
      }
    }

    public QTabStripPainter Painter
    {
      get => this.m_oPainter;
      set
      {
        this.m_oPainter = value;
        if (this.m_oPainter != null && this.m_oParent != null)
          this.m_oPainter.Win32Window = (IWin32Window) this.m_oParent;
        this.HandleTabStripChanged(QCommandUIRequest.PerformLayout, Rectangle.Empty);
      }
    }

    public bool IsHorizontal => this.m_eDock == DockStyle.Top || this.m_eDock == DockStyle.Bottom;

    public bool IsInitializing => this.m_bIsInitializing;

    public Rectangle Bounds => this.m_oBounds;

    public Rectangle OuterBounds => this.m_oOuterBounds;

    internal void PutBounds(Rectangle bounds)
    {
      this.m_oBounds = bounds;
      this.m_oOuterBounds = this.Configuration.StripMargin.InflateRectangleWithMargin(this.m_oBounds, true, this.Dock);
    }

    internal void PutOuterBounds(Rectangle outerBounds)
    {
      this.m_oOuterBounds = outerBounds;
      this.m_oBounds = this.Configuration.StripMargin.InflateRectangleWithMargin(this.m_oOuterBounds, false, this.Dock);
    }

    public Size CalculatedSize => this.m_oCalculatedSize;

    public Rectangle CalculatedContentBounds => this.m_oCalculatedContentBounds;

    public Rectangle CalculatedButtonAreaBounds => this.m_oCalculatedButtonAreaBounds;

    public Rectangle CalculatedScrollArea => this.m_oCalculatedScrollArea;

    public Size RequiredButtonsSize => this.m_oRequiredButtonsSize;

    public void BeginInit() => this.m_bIsInitializing = true;

    public void EndInit() => this.m_bIsInitializing = false;

    public QTabStripPaintParams RetrievePaintParams() => this.m_oTabStripHost != null ? this.m_oTabStripHost.RetrieveTabStripPaintParams() : (QTabStripPaintParams) null;

    public Rectangle CalculateBoundsToControl(Rectangle bounds, bool includeScrollPosition) => new Rectangle(this.CalculatePointToControl(bounds.Left, bounds.Top, includeScrollPosition), bounds.Size);

    public Rectangle CalculateControlBoundsToThis(
      Rectangle bounds,
      bool includeScrollPosition)
    {
      return new Rectangle(this.CalculateControlPointToThis(bounds.Left, bounds.Top, includeScrollPosition), bounds.Size);
    }

    public Point CalculatePointToControl(int x, int y, bool includeScrollPosition) => includeScrollPosition ? new Point(this.m_oBounds.Left + this.m_oScrollPosition.X + x, this.m_oBounds.Top + this.m_oScrollPosition.Y + y) : new Point(this.m_oBounds.Left + x, this.m_oBounds.Top + y);

    public Point CalculateControlPointToThis(int x, int y, bool includeScrollPosition) => includeScrollPosition ? new Point(x - (this.m_oBounds.Left + this.m_oScrollPosition.X), y - (this.m_oBounds.Top + this.m_oScrollPosition.Y)) : new Point(x - this.m_oBounds.Left, y - this.m_oBounds.Top);

    public bool IsOnStripPart(Point controlPoint) => this.Bounds.Contains(controlPoint) && (this.GetButtonAtPoint(controlPoint, QTabButtonSelectionTypes.MustBeVisible) != null || this.NavigationArea.VisibleButtonCount > 0 && this.NavigationArea.ContainsControlPoint(controlPoint));

    public QTabButtonCollection TabButtons => this.m_aTabButtons;

    public QTabButtonRowCollection TabButtonRows => this.m_aTabButtonRows;

    public DockStyle Dock => this.m_eDock;

    public Control Parent
    {
      get
      {
        if (this.m_oParent != null && this.m_oParent.IsDisposed)
          this.m_oParent = (Control) null;
        return this.m_oParent;
      }
    }

    internal QTabButtonDropArea DropArea
    {
      get => this.m_oDropArea;
      set
      {
        if (value == this.m_oDropArea)
          return;
        this.m_oDropArea = value;
        this.m_oTabStripHost.HandleTabStripUiRequest(this, QCommandUIRequest.Redraw, Rectangle.Empty);
      }
    }

    internal IQTabStripHost TabStripHost => this.m_oTabStripHost;

    public QTabStripNavigationArea NavigationArea => this.m_oNavigationArea;

    public QTabButton ActiveButton
    {
      get => this.m_oActiveButton;
      set => this.SetActiveButton(value, true, true, true);
    }

    internal void SavePersistableObject(QPersistenceManager manager, IXPathNavigable parentElement)
    {
      for (int index = 0; index < this.TabButtons.Count; ++index)
      {
        if (this.TabButtons[index].Control is IQPersistableObject control)
          control.SavePersistableObject(manager, parentElement);
      }
    }

    internal void SetActiveButton(
      QTabButton button,
      bool deactiveCurrentTabButtonSource,
      bool activateNewTabButtonSource,
      bool redraw)
    {
      if (this.m_oActiveButton == button)
        return;
      if (button != null && !this.TabButtons.Contains(button))
        throw new InvalidOperationException(QResources.GetException("QTabStrip_SetActiveButtonInvalid", (object) button.Text));
      QTabButton oActiveButton = this.m_oActiveButton;
      QTabStripButtonChangeEventArgs e = new QTabStripButtonChangeEventArgs(oActiveButton, button, true);
      this.OnActiveButtonChanging(e);
      button = e.ToButton;
      if (e.Cancel)
        return;
      if (deactiveCurrentTabButtonSource && this.m_oActiveButton != null && this.m_oActiveButton.TabButtonSource != null)
        this.m_oActiveButton.TabButtonSource.DeactivateSource();
      this.m_oActiveButton = button;
      if (activateNewTabButtonSource && this.m_oActiveButton != null && this.m_oActiveButton.TabButtonSource != null)
        this.m_oActiveButton.TabButtonSource.ActivateSource();
      if (!this.MakeButtonRowFirstRow(this.m_oActiveButton, redraw) && !this.ScrollButtonIntoView(this.m_oActiveButton, this.Configuration.UseScrollAnimation, redraw) && redraw)
        this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
      this.OnActiveButtonChanged(new QTabStripButtonChangeEventArgs(oActiveButton, button, false));
    }

    public QTabButton HotButton
    {
      get => this.m_oHotButton;
      set => this.PutHotButton(value);
    }

    internal void PutHotButton(QTabButton button)
    {
      if (this.m_oHotButton == button)
        return;
      QTabButton oHotButton = this.m_oHotButton;
      this.m_oHotButton = button;
      this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
      this.OnHotButtonChanged(new QTabStripButtonChangeEventArgs(oHotButton, button, false));
    }

    public bool IsDisposed => this.m_bIsDisposed;

    public void SuspendLayout() => this.m_bSuspendLayout = true;

    public void ResumeLayout() => this.m_bSuspendLayout = false;

    public bool HasVisibleButtons => this.TabButtons.HasVisibleButtons;

    public int VisibleButtonCount => this.TabButtons.VisibleButtonCount;

    public bool IsVisible => this.TabStripHost.UserIsDragging && this.UsedAllowDrop || this.Configuration.StripVisibleWithoutButtons || this.VisibleButtonCount > 0;

    public int AccessibleButtonCount => this.TabButtons.AccessibleButtonCount;

    public QTabButton GetNextButton(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row,
      bool loopAround)
    {
      return this.TabButtons.GetNextButton(fromButton, selectionType, row, loopAround);
    }

    public QTabButton GetPreviousButton(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row,
      bool loopAround)
    {
      return this.TabButtons.GetPreviousButton(fromButton, selectionType, row, loopAround);
    }

    public QTabButton GetNextVisibleButton(QTabButton fromButton, bool loopAround) => this.GetNextButton(fromButton, QTabButtonSelectionTypes.MustBeVisible, (QTabButtonRow) null, loopAround);

    public QTabButton GetPreviousVisibleButton(QTabButton fromButton, bool loopAround) => this.GetPreviousButton(fromButton, QTabButtonSelectionTypes.MustBeVisible, (QTabButtonRow) null, loopAround);

    public QTabButton GetNextButtonForNavigation(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      bool loopAround)
    {
      return this.TabButtons.GetNextButtonForNavigation(fromButton, selectionType, loopAround);
    }

    public QTabButton GetPreviousButtonForNavigation(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      bool loopAround)
    {
      return this.TabButtons.GetPreviousButtonForNavigation(fromButton, selectionType, loopAround);
    }

    public QTabButton GetNextAccessibleButtonForNavigation(
      QTabButton fromButton,
      bool loopAround)
    {
      return this.GetNextButtonForNavigation(fromButton, QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeEnabled, loopAround);
    }

    public QTabButton GetPreviousAccessibleButtonForNavigation(
      QTabButton fromButton,
      bool loopAround)
    {
      return this.GetPreviousButtonForNavigation(fromButton, QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeEnabled, loopAround);
    }

    [Browsable(false)]
    public QTabButton FirstDrawnButton
    {
      get
      {
        Rectangle boundsToControl1 = this.CalculateBoundsToControl(this.m_oCalculatedScrollArea, false);
        for (QTabButton nextVisibleButton = this.GetNextVisibleButton((QTabButton) null, false); nextVisibleButton != null; nextVisibleButton = this.GetNextVisibleButton(nextVisibleButton, false))
        {
          Rectangle boundsToControl2 = nextVisibleButton.BoundsToControl;
          if (this.IsHorizontal && boundsToControl2.Left >= boundsToControl1.Left || !this.IsHorizontal && boundsToControl2.Top >= boundsToControl1.Top)
            return nextVisibleButton;
        }
        return (QTabButton) null;
      }
    }

    [Browsable(false)]
    public QTabButton LastDrawnButton
    {
      get
      {
        Rectangle boundsToControl1 = this.CalculateBoundsToControl(this.m_oCalculatedScrollArea, false);
        for (QTabButton previousVisibleButton = this.GetPreviousVisibleButton((QTabButton) null, false); previousVisibleButton != null; previousVisibleButton = this.GetPreviousVisibleButton(previousVisibleButton, false))
        {
          Rectangle boundsToControl2 = previousVisibleButton.BoundsToControl;
          if (this.IsHorizontal && boundsToControl2.Right <= boundsToControl1.Right || !this.IsHorizontal && boundsToControl2.Bottom <= boundsToControl1.Bottom)
            return previousVisibleButton;
        }
        return (QTabButton) null;
      }
    }

    [Browsable(false)]
    public bool CanScrollLeft => this.ScrollLeftAvailable > 0;

    [Browsable(false)]
    public bool CanScrollRight => this.ScrollRightAvailable > 0;

    [Browsable(false)]
    public int ScrollLeftAvailable => this.IsHorizontal ? -this.m_oScrollPosition.X : -this.m_oScrollPosition.Y;

    [Browsable(false)]
    public int ScrollRightAvailable => this.IsHorizontal ? this.m_oScrollPosition.X + this.m_oRequiredButtonsSize.Width - this.m_oCalculatedScrollArea.Width : this.m_oScrollPosition.Y + this.m_oRequiredButtonsSize.Height - this.m_oCalculatedScrollArea.Height;

    public bool ScrollLeft() => this.Configuration.IsScroll ? this.ScrollLeft(this.Configuration.ScrollStep, true) : throw new InvalidOperationException(QResources.GetException("QTabStrip_CannotScroll"));

    public bool ScrollLeft(int amount, bool redraw)
    {
      if (!this.Configuration.IsScroll)
        throw new InvalidOperationException(QResources.GetException("QTabStrip_CannotScroll"));
      if (this.IsHorizontal)
      {
        int num = Math.Min(amount, this.ScrollLeftAvailable);
        if (num > 0)
        {
          this.m_oScrollPosition.X += num;
          this.UpdateScrollButtons();
          if (redraw)
            this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
          this.NotifyScrollStep();
          return true;
        }
      }
      else
      {
        int num = Math.Min(amount, -this.m_oScrollPosition.Y);
        if (num > 0)
        {
          this.m_oScrollPosition.Y += num;
          this.UpdateScrollButtons();
          if (redraw)
            this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
          this.NotifyScrollStep();
          return true;
        }
      }
      return false;
    }

    public bool ScrollOneButtonLeft(bool animate, bool redraw)
    {
      QTabButton qtabButton = this.FirstDrawnButton;
      if (qtabButton != null)
        qtabButton = this.GetPreviousVisibleButton(qtabButton, false);
      if (qtabButton == null)
        return false;
      this.ScrollButtonIntoView(qtabButton, animate, redraw);
      return true;
    }

    public bool ScrollRight() => this.Configuration.IsScroll ? this.ScrollRight(this.Configuration.ScrollStep, true) : throw new InvalidOperationException(QResources.GetException("QTabStrip_CannotScroll"));

    public bool ScrollRight(int amount, bool redraw)
    {
      if (!this.Configuration.IsScroll)
        throw new InvalidOperationException(QResources.GetException("QTabStrip_CannotScroll"));
      if (this.IsHorizontal)
      {
        int num = Math.Min(amount, this.ScrollRightAvailable);
        if (num > 0)
        {
          this.m_oScrollPosition.X -= num;
          this.UpdateScrollButtons();
          if (redraw)
            this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
          this.NotifyScrollStep();
          return true;
        }
      }
      else
      {
        int num = Math.Min(amount, this.ScrollRightAvailable);
        if (num > 0)
        {
          this.m_oScrollPosition.Y -= num;
          this.UpdateScrollButtons();
          if (redraw)
            this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
          this.NotifyScrollStep();
          return true;
        }
      }
      return false;
    }

    public bool ScrollOneButtonRight(bool animate, bool redraw)
    {
      QTabButton qtabButton = this.LastDrawnButton;
      if (qtabButton != null)
        qtabButton = this.GetNextVisibleButton(qtabButton, false);
      if (qtabButton == null)
        return false;
      this.ScrollButtonIntoView(qtabButton, animate, redraw);
      return true;
    }

    public bool MakeButtonRowFirstRow(QTabButton button, bool redraw)
    {
      if (button == null || button.TabButtonRow == null || !this.Configuration.UseStackMoveToFront || !this.TabButtonRows.MakeFirstVisibleRow(button.TabButtonRow))
        return false;
      this.Painter.CalculateRowRanges(this, this.CalculatedButtonAreaBounds);
      this.TabButtons.Sort();
      if (redraw)
        this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
      return true;
    }

    public bool ScrollButtonIntoView(QTabButton button, bool animate, bool redraw)
    {
      if (button == null || !this.Configuration.IsScroll)
        return false;
      if (this.IsHorizontal)
      {
        if (button.Bounds.Left - this.m_oCalculatedScrollArea.Left + this.m_oScrollPosition.X < 0)
        {
          this.StartScrolling(button.Bounds.Left - this.m_oCalculatedScrollArea.Left - this.Configuration.ScrollMargin, animate, redraw);
          return true;
        }
        if (button.Bounds.Right - this.m_oCalculatedScrollArea.Left + this.m_oScrollPosition.X > this.m_oCalculatedScrollArea.Width)
        {
          this.StartScrolling(button.Bounds.Right - this.m_oCalculatedScrollArea.Left + this.Configuration.ScrollMargin, animate, redraw);
          return true;
        }
      }
      else
      {
        if (button.Bounds.Top - this.m_oCalculatedScrollArea.Top + this.m_oScrollPosition.Y < 0)
        {
          this.StartScrolling(button.Bounds.Top - this.m_oCalculatedScrollArea.Top - this.Configuration.ScrollMargin, animate, redraw);
          return true;
        }
        if (button.Bounds.Bottom - this.m_oCalculatedScrollArea.Top + this.m_oScrollPosition.Y > this.m_oCalculatedScrollArea.Height)
        {
          this.StartScrolling(button.Bounds.Bottom - this.m_oCalculatedScrollArea.Top + this.Configuration.ScrollMargin, animate, redraw);
          return true;
        }
      }
      return false;
    }

    public void StartScrolling(int tillPointVisible, bool animate, bool redraw)
    {
      if (this.IsHorizontal)
      {
        int num = tillPointVisible + this.m_oScrollPosition.X;
        if (num < 0)
        {
          if (animate)
          {
            this.m_iScrollingTill = tillPointVisible;
            this.m_bIsScrolling = true;
            this.m_oTabStripHost.StartAnimateTimer(this);
          }
          else
            this.ScrollLeft(-num, redraw);
        }
        else
        {
          if (num <= this.m_oCalculatedScrollArea.Width)
            return;
          if (animate)
          {
            this.m_iScrollingTill = tillPointVisible;
            this.m_bIsScrolling = true;
            this.m_oTabStripHost.StartAnimateTimer(this);
          }
          else
            this.ScrollRight(num - this.m_oCalculatedScrollArea.Width, redraw);
        }
      }
      else
      {
        int num = tillPointVisible + this.m_oScrollPosition.Y;
        if (num < 0)
        {
          if (animate)
          {
            this.m_iScrollingTill = tillPointVisible;
            this.m_bIsScrolling = true;
            this.m_oTabStripHost.StartAnimateTimer(this);
          }
          else
            this.ScrollLeft(-num, redraw);
        }
        else
        {
          if (num <= this.m_oCalculatedScrollArea.Height)
            return;
          if (animate)
          {
            this.m_iScrollingTill = tillPointVisible;
            this.m_bIsScrolling = true;
            this.m_oTabStripHost.StartAnimateTimer(this);
          }
          else
            this.ScrollRight(num - this.m_oCalculatedScrollArea.Height, redraw);
        }
      }
    }

    public void StopScrolling()
    {
      if (this.TabStripHost.UserIsDragging)
        this.CalculateDropAreas();
      this.m_bIsScrolling = false;
      this.m_oTabStripHost.StopAnimateTimer(this);
    }

    internal void HandleAnimateTimerTick()
    {
      if (!this.m_bIsScrolling)
        return;
      if (this.IsHorizontal)
      {
        int num = this.m_iScrollingTill + this.m_oScrollPosition.X;
        if (num < 0)
        {
          if (this.ScrollLeft(Math.Min(this.Configuration.ScrollStep, -num), true))
            return;
          this.StopScrolling();
        }
        else if (num > this.m_oCalculatedScrollArea.Width)
        {
          if (this.ScrollRight(Math.Min(this.Configuration.ScrollStep, num - this.m_oCalculatedScrollArea.Width), true))
            return;
          this.StopScrolling();
        }
        else
        {
          if (!this.m_bIsScrolling)
            return;
          this.StopScrolling();
        }
      }
      else
      {
        int num = this.m_iScrollingTill + this.m_oScrollPosition.Y;
        if (num < 0)
        {
          if (this.ScrollLeft(Math.Min(this.Configuration.ScrollStep, -num), true))
            return;
          this.StopScrolling();
        }
        else if (num > this.m_oCalculatedScrollArea.Height)
        {
          if (this.ScrollRight(Math.Min(this.Configuration.ScrollStep, num - this.m_oCalculatedScrollArea.Height), true))
            return;
          this.StopScrolling();
        }
        else
        {
          if (!this.m_bIsScrolling)
            return;
          this.StopScrolling();
        }
      }
    }

    internal bool UsedAllowDrag
    {
      get
      {
        QTabControl tabStripHost = this.TabStripHost as QTabControl;
        if (this.Configuration.AllowDrag && this.TabStripHost.AllowDrag)
          return true;
        return tabStripHost != null && tabStripHost.IsDesignMode;
      }
    }

    internal bool UsedAllowDrop
    {
      get
      {
        QTabControl tabStripHost = this.TabStripHost as QTabControl;
        if (this.Configuration.AllowDrop && this.TabStripHost.AllowDrop)
          return true;
        return tabStripHost != null && tabStripHost.IsDesignMode;
      }
    }

    private bool UpdateScrollButtons()
    {
      bool flag = false;
      if (this.NavigationArea.ScrollLeft.Enabled != this.CanScrollLeft)
      {
        this.NavigationArea.ScrollLeft.Enabled = this.CanScrollLeft;
        flag = true;
      }
      if (this.NavigationArea.ScrollRight.Enabled != this.CanScrollRight)
      {
        this.NavigationArea.ScrollRight.Enabled = this.CanScrollRight;
        flag = true;
      }
      return flag;
    }

    internal void CorrectScrollAfterPossibleResize(bool redraw)
    {
      bool flag = false;
      if (this.Configuration.IsScroll && this.ScrollRightAvailable < 0)
      {
        this.ScrollLeft(-this.ScrollRightAvailable, false);
        flag = true;
      }
      if (this.UpdateScrollButtons())
        flag = true;
      if (!redraw || !flag)
        return;
      this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
    }

    public QTabButton GetButtonAtPoint(
      Point controlPoint,
      QTabButtonSelectionTypes selection)
    {
      Rectangle empty = Rectangle.Empty;
      if (!(!this.Configuration.ButtonAreaClip ? this.CalculateBoundsToControl(this.CalculatedContentBounds, false) : this.CalculateBoundsToControl(this.CalculatedButtonAreaBounds, false)).Contains(controlPoint))
        return (QTabButton) null;
      if (this.ActiveButton != null && this.ActiveButton.ContainsControlPoint(controlPoint) && this.ActiveButton.MatchesSelection(selection, (QTabButtonRow) null))
        return this.ActiveButton;
      for (QTabButton nextButton = this.GetNextButton((QTabButton) null, selection, (QTabButtonRow) null, false); nextButton != null; nextButton = this.GetNextButton(nextButton, selection, (QTabButtonRow) null, false))
      {
        if (nextButton.ContainsControlPoint(controlPoint))
          return nextButton;
      }
      return (QTabButton) null;
    }

    public QTabButton GetAccessibleButtonAtPoint(Point controlPoint) => this.GetButtonAtPoint(controlPoint, QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeEnabled);

    internal void ClearDropAreas() => this.m_aDropAreaCollection.Clear();

    internal void CalculateDropAreas()
    {
      this.m_aDropAreaCollection.Clear();
      if (!this.UsedAllowDrop)
        return;
      if (this.m_aTabButtonRows.Count > 0)
      {
        for (int index = 0; index < this.m_aTabButtonRows.Count; ++index)
          this.CalculateDropAreas(this.m_aTabButtonRows[index]);
      }
      else
        this.CalculateDropAreas((QTabButtonRow) null);
    }

    private void CalculateDropAreas(QTabButtonRow row)
    {
      QTabButtonDropArea area = (QTabButtonDropArea) null;
      if (this.TabButtons.Count == 0)
      {
        this.m_aDropAreaCollection.Add(new QTabButtonDropArea(this.Bounds, (QTabButton) null, this));
      }
      else
      {
        QTabButton nextButton = this.GetNextButton((QTabButton) null, QTabButtonSelectionTypes.MustBeInRow, row, false);
        while (nextButton != null)
        {
          Rectangle boundsToControl = nextButton.BoundsToControl;
          if (area != null)
          {
            if (this.IsHorizontal)
            {
              if (area.Bounds.Right > boundsToControl.Left)
              {
                int num1 = (area.Bounds.Right - boundsToControl.Left) / 2;
                area.Bounds = QMath.SetWidth(area.Bounds, area.Bounds.Width - num1);
                int num2 = area.Bounds.Right - boundsToControl.Left;
                boundsToControl.X += num2;
                boundsToControl.Width -= num2;
              }
              else if (area.Bounds.Right < boundsToControl.Left)
              {
                int num3 = (boundsToControl.Left - area.Bounds.Right) / 2;
                area.Bounds = QMath.SetWidth(area.Bounds, area.Bounds.Width + num3);
                int num4 = boundsToControl.Left - area.Bounds.Right;
                boundsToControl.X -= num4;
                boundsToControl.Width += num4;
              }
            }
            else if (area.Bounds.Bottom > boundsToControl.Top)
            {
              int num5 = (area.Bounds.Bottom - boundsToControl.Top) / 2;
              area.Bounds = QMath.SetHeight(area.Bounds, area.Bounds.Height - num5);
              int num6 = area.Bounds.Bottom - boundsToControl.Top;
              boundsToControl.Y += num6;
              boundsToControl.Height -= num6;
            }
            else if (area.Bounds.Bottom < boundsToControl.Top)
            {
              int num7 = (boundsToControl.Top - area.Bounds.Bottom) / 2;
              area.Bounds = QMath.SetHeight(area.Bounds, area.Bounds.Height + num7);
              int num8 = boundsToControl.Top - area.Bounds.Bottom;
              boundsToControl.Y -= num8;
              boundsToControl.Height += num8;
            }
          }
          bool flag = area == null;
          area = new QTabButtonDropArea(boundsToControl, nextButton, this);
          area.FirstInRow = flag;
          if (flag)
          {
            if (this.IsHorizontal)
            {
              if (area.Bounds.Left > this.Bounds.Left)
                area.Bounds = new Rectangle(area.Bounds.Left - (area.Bounds.Left - this.Bounds.Left), area.Bounds.Top, area.Bounds.Width + (area.Bounds.Left - this.Bounds.Left), area.Bounds.Height);
            }
            else if (area.Bounds.Top > this.Bounds.Top)
              area.Bounds = new Rectangle(area.Bounds.Left, area.Bounds.Top - (area.Bounds.Top - this.Bounds.Top), area.Bounds.Width, area.Bounds.Height + (area.Bounds.Top - this.Bounds.Top));
          }
          this.m_aDropAreaCollection.Add(area);
          nextButton = this.GetNextButton(nextButton, QTabButtonSelectionTypes.MustBeInRow, row, false);
          if (nextButton == null)
            area.LastInRow = true;
        }
        if (area == null)
          return;
        if (this.IsHorizontal)
        {
          if (area.Bounds.Right >= this.Bounds.Right)
            return;
          area.Bounds = QMath.SetWidth(area.Bounds, area.Bounds.Width + (this.Bounds.Right - area.Bounds.Right));
        }
        else
        {
          if (area.Bounds.Bottom >= this.Bounds.Bottom)
            return;
          area.Bounds = QMath.SetHeight(area.Bounds, area.Bounds.Height + (this.Bounds.Bottom - area.Bounds.Bottom));
        }
      }
    }

    internal bool CalculateLayout(Size availableSize, bool forceLayout)
    {
      if (this.m_bSuspendLayout || this.Parent == null || !forceLayout && !this.m_bRequestingLayout && this.m_oLastCalculatedAvailableSize == availableSize)
        return false;
      this.m_oLastCalculatedAvailableSize = availableSize;
      if (availableSize.Width < 0)
        availableSize.Width = 0;
      if (availableSize.Height < 0)
        availableSize.Height = 0;
      this.m_bRequestingLayout = false;
      Graphics graphics = this.Parent.CreateGraphics();
      this.m_oCalculatedSize = this.m_oPainter.CalculateStripLayout(this, availableSize, graphics);
      this.m_oCalculatedContentBounds = this.m_oPainter.LastCalculatedStripContentBounds;
      this.m_oCalculatedButtonAreaBounds = this.m_oPainter.LastCalculatedButtonAreaBounds;
      if (this.IsHorizontal)
      {
        this.m_oCalculatedScrollArea = new Rectangle(this.m_oCalculatedButtonAreaBounds.Left, this.m_oCalculatedButtonAreaBounds.Top, this.m_oPainter.LastCalculatedAvailableButtonsSize.Width, this.m_oPainter.LastCalculatedAvailableButtonsSize.Height);
        this.m_oRequiredButtonsSize = new Size(this.m_oPainter.LastCalculatedButtonsSize.Width, this.m_oPainter.LastCalculatedButtonsSize.Height);
      }
      else
      {
        this.m_oCalculatedScrollArea = new Rectangle(this.m_oCalculatedButtonAreaBounds.Left, this.m_oCalculatedButtonAreaBounds.Top, this.m_oPainter.LastCalculatedAvailableButtonsSize.Width, this.m_oPainter.LastCalculatedAvailableButtonsSize.Height);
        this.m_oRequiredButtonsSize = new Size(this.m_oPainter.LastCalculatedButtonsSize.Width, this.m_oPainter.LastCalculatedButtonsSize.Height);
      }
      graphics.Dispose();
      if (this.m_oActiveButton != null)
        this.MakeButtonRowFirstRow(this.m_oActiveButton, false);
      return true;
    }

    internal void HandleMouseDown(MouseEventArgs e)
    {
      if (!this.IsVisible)
        return;
      QTabButton button = (QTabButton) null;
      this.NavigationArea.HandleMouseDown(e);
      Point controlPoint = new Point(e.X, e.Y);
      if (!this.NavigationArea.ContainsControlPoint(controlPoint))
        button = this.GetAccessibleButtonAtPoint(controlPoint);
      if (button == null)
        return;
      if (button != this.ActiveButton)
        this.SetActiveButton(button, true, true, true);
      else
        this.ScrollButtonIntoView(button, this.Configuration.UseScrollAnimation, true);
    }

    internal void HandleMouseMove(MouseEventArgs e)
    {
      if (!this.IsVisible)
        return;
      QTabButton button = (QTabButton) null;
      this.NavigationArea.HandleMouseMove(e);
      Point controlPoint = new Point(e.X, e.Y);
      if (!this.NavigationArea.ContainsControlPoint(controlPoint))
        button = this.GetAccessibleButtonAtPoint(controlPoint);
      if (button != null && !this.UserIsDraggingTabButton)
      {
        if (button == this.HotButton)
          return;
        this.PutHotButton(button);
      }
      else
      {
        if (this.HotButton == null)
          return;
        this.PutHotButton((QTabButton) null);
      }
    }

    private bool UserIsDraggingTabButton => this.m_oTabStripHost != null && this.m_oTabStripHost.UserIsDraggingTabButton;

    internal void HandleMouseUp(MouseEventArgs e)
    {
      if (!this.IsVisible)
        return;
      this.NavigationArea.HandleMouseUp(e);
    }

    internal void HandleMouseLeave()
    {
      if (!this.IsVisible)
        return;
      this.NavigationArea.HandleMouseLeave();
      if (this.HotButton == null)
        return;
      this.PutHotButton((QTabButton) null);
    }

    private void HandleDragScrolling(Point mouseCoordinate)
    {
      if (!this.Bounds.Contains(mouseCoordinate))
      {
        if (!this.m_bIsScrolling)
          return;
        this.StopScrolling();
      }
      else
      {
        Rectangle boundsToControl = this.CalculateBoundsToControl(this.CalculatedButtonAreaBounds, false);
        if (this.IsHorizontal)
        {
          if ((double) boundsToControl.X + (double) boundsToControl.Width * 0.15 > (double) mouseCoordinate.X)
          {
            if (this.m_bIsScrolling)
              return;
            this.StartScrolling(0, true, false);
            return;
          }
          if ((double) boundsToControl.Right - (double) boundsToControl.Width * 0.15 < (double) mouseCoordinate.X)
          {
            if (this.m_bIsScrolling)
              return;
            this.StartScrolling(this.RequiredButtonsSize.Width, true, false);
            return;
          }
        }
        else
        {
          if ((double) boundsToControl.Y + (double) boundsToControl.Height * 0.15 > (double) mouseCoordinate.Y)
          {
            if (this.m_bIsScrolling)
              return;
            this.StartScrolling(0, true, false);
            return;
          }
          if ((double) boundsToControl.Bottom - (double) boundsToControl.Height * 0.15 < (double) mouseCoordinate.Y)
          {
            if (this.m_bIsScrolling)
              return;
            this.StartScrolling(this.RequiredButtonsSize.Height, true, false);
            return;
          }
        }
        if (!this.m_bIsScrolling)
          return;
        this.StopScrolling();
      }
    }

    internal bool HandleDragging(Point mouseCoordinate, QTabButton button)
    {
      if (button == null)
        return false;
      this.HandleDragScrolling(mouseCoordinate);
      if (this.Bounds.Contains(mouseCoordinate))
      {
        for (int index = 0; index < this.m_aDropAreaCollection.Count; ++index)
        {
          if (this.m_aDropAreaCollection[index].Bounds.Contains(mouseCoordinate))
          {
            this.DropArea = this.m_aDropAreaCollection[index];
            if (this.DropArea.CalculateDock(mouseCoordinate, this.IsHorizontal, button))
              this.m_oTabStripHost.HandleTabStripUiRequest(this, QCommandUIRequest.Redraw, Rectangle.Empty);
            return this.DropArea.AllowDrop;
          }
        }
      }
      this.DropArea = (QTabButtonDropArea) null;
      return false;
    }

    protected internal virtual void UpdatePropertiesFromConfiguration()
    {
      if (!this.Configuration.IsScroll && this.m_oScrollPosition != Point.Empty)
        this.m_oScrollPosition = Point.Empty;
      this.m_oNavigationArea.Close.Visible = this.Configuration.CloseButtonVisible;
      this.m_oNavigationArea.ScrollLeft.AdditionalData = (object) this.Configuration.UsedScrollLeftMask;
      this.m_oNavigationArea.ScrollRight.AdditionalData = (object) this.Configuration.UsedScrollRightMask;
      this.m_oNavigationArea.Close.AdditionalData = (object) this.Configuration.UsedCloseMask;
      for (int index = 0; index < this.NavigationArea.ButtonAreas.Length; ++index)
      {
        QButtonArea buttonArea = this.NavigationArea.ButtonAreas[index];
        Image additionalData = this.NavigationArea.ButtonAreas[index].AdditionalData as Image;
        if (buttonArea != null && additionalData != null)
        {
          buttonArea.RequestedSize = this.Configuration.NavigationButtonPadding.InflateSizeWithPadding(additionalData.Size, true, true);
          if (this.Configuration.UsedNavigationButtonHotAppearance is QShapeAppearance buttonHotAppearance)
            buttonArea.RequestedSize = buttonHotAppearance.Shape.InflateSize(buttonArea.RequestedSize, true, true);
        }
      }
    }

    internal void HandleTabStripChanged(QCommandUIRequest changeRequest, Rectangle invalidateBounds)
    {
      if (this.m_oTabStripHost == null)
        return;
      if (changeRequest == QCommandUIRequest.PerformLayout)
        this.m_bRequestingLayout = true;
      this.m_oTabStripHost.HandleTabStripUiRequest(this, changeRequest, invalidateBounds);
    }

    internal void HandleTabButtonRemoving(QTabButton button, bool activateNewTabWhenActive)
    {
      if (button == this.m_oHotButton)
        this.m_oHotButton = (QTabButton) null;
      if (button != this.m_oActiveButton)
        return;
      if (activateNewTabWhenActive)
      {
        this.m_bHandlingRemoval = true;
        this.m_oButtonToActiveAfterRemoval = this.GetNextAccessibleButtonForNavigation(button, false);
        if (this.m_oButtonToActiveAfterRemoval != null)
          return;
        this.m_oButtonToActiveAfterRemoval = this.GetPreviousAccessibleButtonForNavigation(button, false);
      }
      else
        this.m_oActiveButton = (QTabButton) null;
    }

    internal void HandleTabButtonCollectionChanged()
    {
      if (this.m_bHandlingRemoval)
      {
        this.m_bHandlingRemoval = false;
        this.SetActiveButton(this.m_oButtonToActiveAfterRemoval, true, true, false);
        this.m_oButtonToActiveAfterRemoval = (QTabButton) null;
      }
      this.HandleTabStripChanged(QCommandUIRequest.PerformLayout, Rectangle.Empty);
    }

    internal void NotifyScrollStep()
    {
      for (int index = 0; index < this.m_aTabButtons.Count; ++index)
        this.m_aTabButtons[index].TabButtonSource.HandleScrollStep();
    }

    internal void Draw(Graphics graphics) => this.m_oPainter.DrawTabStrip(this, graphics);

    internal void DrawDropArea(Graphics graphics)
    {
      if (this.DropArea == null)
        return;
      this.m_oPainter.DrawDropArea(graphics, this, this.DropArea);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (this.m_bIsDisposed)
        return;
      if (disposing)
      {
        if (this.m_oLastDrawnGraphicsPath != null)
        {
          this.m_oLastDrawnGraphicsPath.Dispose();
          this.m_oLastDrawnGraphicsPath = (GraphicsPath) null;
        }
        for (int index = 0; index < this.m_aTabButtons.Count; ++index)
          this.m_aTabButtons[index].Dispose();
        this.m_aTabButtons.Clear();
        if (this.m_oButtonToActiveAfterRemoval != null && this.m_oButtonToActiveAfterRemoval.IsDisposed)
        {
          this.m_oButtonToActiveAfterRemoval.Dispose();
          this.m_oButtonToActiveAfterRemoval = (QTabButton) null;
        }
        if (this.m_oConfiguration != null)
        {
          this.m_oConfiguration.Dispose();
          this.m_oConfiguration = (QTabStripConfiguration) null;
        }
      }
      this.m_bIsDisposed = true;
    }

    ~QTabStrip() => this.Dispose(false);

    private void OnActiveButtonChanging(QTabStripButtonChangeEventArgs e) => this.m_oActiveButtonChangingDelegate = QWeakDelegate.InvokeDelegate(this.m_oActiveButtonChangingDelegate, (object) this, (object) e);

    private void OnActiveButtonChanged(QTabStripButtonChangeEventArgs e) => this.m_oActiveButtonChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oActiveButtonChangedDelegate, (object) this, (object) e);

    private void OnHotButtonChanged(QTabStripButtonChangeEventArgs e) => this.m_oHotButtonChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oHotButtonChangedDelegate, (object) this, (object) e);

    private void OnCloseButtonClick(EventArgs e) => this.m_oCloseButtonClickDelegate = QWeakDelegate.InvokeDelegate(this.m_oCloseButtonClickDelegate, (object) this, (object) e);

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.UpdatePropertiesFromConfiguration();
      this.HandleTabStripChanged(QCommandUIRequest.PerformLayout, Rectangle.Empty);
    }

    private void NavigationButtons_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      QButtonArea qbuttonArea = sender as QButtonArea;
      if (e.ToState == QButtonState.Pressed)
      {
        if (qbuttonArea == this.m_oNavigationArea.ScrollLeft)
        {
          if (this.Configuration.UseScrollOneButton)
            this.ScrollOneButtonLeft(this.Configuration.UseScrollAnimation, false);
          else
            this.StartScrolling(0, this.Configuration.UseScrollAnimation, false);
        }
        else if (qbuttonArea == this.m_oNavigationArea.ScrollRight)
        {
          if (this.Configuration.UseScrollOneButton)
            this.ScrollOneButtonRight(this.Configuration.UseScrollAnimation, false);
          else if (this.IsHorizontal)
            this.StartScrolling(this.m_oRequiredButtonsSize.Width, this.Configuration.UseScrollAnimation, false);
          else
            this.StartScrolling(this.m_oRequiredButtonsSize.Height, this.Configuration.UseScrollAnimation, false);
        }
        else
        {
          QButtonArea close = this.m_oNavigationArea.Close;
        }
        this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
      }
      else if (e.FromState == QButtonState.Pressed)
      {
        if (qbuttonArea == this.m_oNavigationArea.ScrollLeft)
        {
          if (this.Configuration.UseScrollAnimation && !this.Configuration.UseScrollOneButton)
            this.StopScrolling();
          this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
        }
        else if (qbuttonArea == this.m_oNavigationArea.ScrollRight)
        {
          if (this.Configuration.UseScrollAnimation && !this.Configuration.UseScrollOneButton)
            this.StopScrolling();
          this.HandleTabStripChanged(QCommandUIRequest.Redraw, Rectangle.Empty);
        }
        else if (qbuttonArea == this.m_oNavigationArea.Close && e.PressedButtons != Control.MouseButtons)
        {
          this.HandleTabStripChanged(QCommandUIRequest.Redraw, this.NavigationArea.CalculateBoundsToControl(qbuttonArea.Bounds));
          this.OnCloseButtonClick(EventArgs.Empty);
        }
        else
          this.HandleTabStripChanged(QCommandUIRequest.Redraw, this.NavigationArea.CalculateBoundsToControl(qbuttonArea.Bounds));
      }
      else
        this.HandleTabStripChanged(QCommandUIRequest.Redraw, this.NavigationArea.CalculateBoundsToControl(qbuttonArea.Bounds));
    }
  }
}
