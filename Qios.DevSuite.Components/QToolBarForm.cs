// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarForm
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QToolBarForm : QCustomToolWindow
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private IQToolBar m_oToolBar;
    private QButtonArea m_oCloseButtonArea;
    private QButtonArea m_oCustomizeButtonArea;
    private bool m_bPerformingLayout;
    private int m_iCaptionTitleRight;
    private int m_iUserRequestedToolBarWidth;
    private bool m_bIsChangingToolBars;

    public QToolBarForm()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.SuspendLayout();
      this.InternalConstruct();
    }

    internal QToolBarForm(IQToolBar toolBar, IWin32Window owner)
      : base(owner)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.SuspendLayout();
      this.m_oToolBar = toolBar;
      this.m_oToolBar.ToolBarConfiguration.ConfigurationChanged += new EventHandler(this.ToolBarConfiguration_ConfigurationChanged);
      if (this.ToolBarControl != null)
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.ToolBar_Disposed), (object) this.ToolBarControl, "Disposed"));
      this.InternalConstruct();
    }

    internal Rectangle CustomizeItemBounds => new Rectangle(this.Left + this.m_oCustomizeButtonArea.Bounds.Left, this.Top + this.m_oCustomizeButtonArea.Bounds.Top, this.m_oCustomizeButtonArea.Bounds.Width, this.m_oCustomizeButtonArea.Bounds.Height);

    public override Form Owner
    {
      get => base.Owner;
      set
      {
        if (base.Owner != null)
        {
          this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Owner_VisibleChanged));
          this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Owner_Closed));
          this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Owner_Activated));
          this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Owner_Deactivate));
        }
        base.Owner = value;
        if (base.Owner == null)
          return;
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Owner_VisibleChanged), (object) base.Owner, "VisibleChanged"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Owner_Closed), (object) base.Owner, "Closed"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Owner_Activated), (object) base.Owner, "Activated"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Owner_Deactivate), (object) base.Owner, "Deactivate"));
      }
    }

    private void InternalConstruct()
    {
      this.CanSizeBottom = false;
      this.CanSizeTop = false;
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetQControlStyles(QControlStyles.DoNotCopyBitsOnUserSize, true);
      this.m_oCloseButtonArea = new QButtonArea(MouseButtons.Left);
      this.m_oCloseButtonArea.ButtonStateChanged += new QButtonAreaEventHandler(this.CloseButtonArea_ButtonStateChanged);
      this.m_oCustomizeButtonArea = new QButtonArea(MouseButtons.Left);
      this.m_oCustomizeButtonArea.ButtonStateChanged += new QButtonAreaEventHandler(this.CustomizeButtonArea_ButtonStateChanged);
      this.Visible = false;
      this.ResumeLayout(false);
    }

    protected override void SetVisibleCore(bool value) => base.SetVisibleCore(value);

    internal bool ShowCustomize
    {
      get
      {
        if (this.m_oToolBar == null)
          return false;
        return this.m_oToolBar.ToolBarConfiguration.CanCustomize || this.m_oToolBar.ShowMoreItemsButton;
      }
    }

    internal bool CanClose => this.m_oToolBar != null && this.m_oToolBar.ToolBarConfiguration.CanClose;

    internal QPadding ButtonsPadding => this.m_oToolBar == null ? QPadding.Empty : this.m_oToolBar.ToolBarConfiguration.FormButtonsPadding;

    protected override string BackColor2PropertyName => "ToolBarFormBackground1";

    protected override string BackColorPropertyName => "ToolBarFormBackground2";

    protected override string BorderColorPropertyName => "ToolBarFormBorder";

    protected override string CaptionColor1PropertyName => "ToolBarFormCaption1";

    protected override string CaptionColor2PropertyName => "ToolBarFormCaption2";

    public override Rectangle CaptionTitleBounds
    {
      get
      {
        Rectangle captionBounds = this.CaptionBounds;
        return Rectangle.FromLTRB(captionBounds.Left, captionBounds.Top, this.m_iCaptionTitleRight, captionBounds.Bottom);
      }
    }

    public int UserRequestedToolBarWidth
    {
      get => this.m_iUserRequestedToolBarWidth;
      set
      {
        if (this.m_iUserRequestedToolBarWidth == value)
          return;
        this.m_iUserRequestedToolBarWidth = value;
        this.PerformLayout();
        this.Refresh();
      }
    }

    public override void Refresh()
    {
      base.Refresh();
      this.RefreshNoClientArea();
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, width, height, specified);
    }

    internal void NotifyToolBarSizeChanged(IQToolBar toolBar)
    {
      if (!this.Controls.Contains((Control) toolBar))
        return;
      this.PerformLayout();
      this.Refresh();
    }

    internal Control ToolBarControl => !(this.m_oToolBar is Control) ? (Control) null : (Control) this.m_oToolBar;

    internal bool IsChangingToolBars => this.m_bIsChangingToolBars;

    private void LayoutCaptionButtons()
    {
      if (this.m_oCloseButtonArea == null || this.m_oCustomizeButtonArea == null)
        return;
      Rectangle captionBounds = this.CaptionBounds;
      int x = captionBounds.Right - this.ButtonsPadding.Right;
      int num1 = captionBounds.Top + this.ButtonsPadding.Top;
      int num2 = this.CaptionBounds.Bottom - this.ButtonsPadding.Bottom - num1;
      Size size;
      if (this.CanClose)
      {
        size = this.m_oToolBar != null ? this.m_oToolBar.ToolBarConfiguration.UsedFormCloseToolBarMask.Size : Size.Empty;
        x -= size.Width;
        this.m_oCloseButtonArea.Bounds = new Rectangle(x, num1 + (num2 / 2 - size.Height / 2), size.Width, size.Height);
      }
      else
        this.m_oCloseButtonArea.Bounds = Rectangle.Empty;
      if (this.ShowCustomize)
      {
        size = this.m_oToolBar != null ? this.m_oToolBar.ToolBarConfiguration.UsedFormCustomizeToolBarMask.Size : Size.Empty;
        x -= size.Width;
        this.m_oCustomizeButtonArea.Bounds = new Rectangle(x, num1 + (num2 / 2 - size.Height / 2), size.Width, size.Height);
      }
      else
        this.m_oCustomizeButtonArea.Bounds = Rectangle.Empty;
      this.m_iCaptionTitleRight = x - this.ButtonsPadding.Left;
    }

    protected override void OnUserSizing(QUserSizingEventArgs e)
    {
      this.m_bIsChangingToolBars = true;
      if (this.m_oToolBar != null && this.Controls.Contains((Control) this.m_oToolBar))
      {
        this.m_oToolBar.LayoutToolBar(new Rectangle(0, 0, Math.Max(this.MinimumClientSize.Width, e.NewSize.Width - (this.ClientAreaMarginLeft + this.ClientAreaMarginRight)), 0), QCommandContainerOrientation.Horizontal, QToolBarLayoutFlags.MaximumWidth);
        int width = this.m_oToolBar.Bounds.Width + this.ClientAreaMarginLeft + this.ClientAreaMarginRight;
        int height = this.m_oToolBar.Bounds.Height + this.ClientAreaMarginTop + this.ClientAreaMarginBottom;
        e.NewSize = new Size(width, height);
        this.m_iUserRequestedToolBarWidth = this.m_oToolBar.Bounds.Width;
      }
      this.m_bIsChangingToolBars = false;
      base.OnUserSizing(e);
    }

    protected override void OnUserSized(QUserSizedEventArgs e)
    {
      base.OnUserSized(e);
      this.RefreshNoClientArea();
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      if (e.Control != this.m_oToolBar)
        return;
      this.m_bIsChangingToolBars = true;
      this.m_oToolBar.OwnerWindow = (IWin32Window) this;
      QToolBarLayoutFlags layoutFlags = QToolBarLayoutFlags.None;
      if (this.m_iUserRequestedToolBarWidth > 0)
        layoutFlags = QToolBarLayoutFlags.MaximumWidth;
      this.m_oToolBar.LayoutToolBar(this.ClientRectangle, QCommandContainerOrientation.Horizontal, layoutFlags);
      this.m_bIsChangingToolBars = false;
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      if (e.Control != this.m_oToolBar || !this.Visible)
        return;
      this.Hide();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.m_bPerformingLayout)
        return;
      this.m_bPerformingLayout = true;
      if (!this.IsUserSizing && this.m_oToolBar != null && this.Controls.Contains((Control) this.m_oToolBar))
      {
        this.m_bIsChangingToolBars = true;
        if (this.m_iUserRequestedToolBarWidth > 0)
          this.m_oToolBar.LayoutToolBar(new Rectangle(0, 0, this.m_iUserRequestedToolBarWidth, 0), QCommandContainerOrientation.Horizontal, QToolBarLayoutFlags.MaximumWidth);
        this.MinimumClientSize = this.m_oToolBar.MinimumSize;
        this.ClientSize = this.m_oToolBar.ProposedBounds.Size;
        this.m_bIsChangingToolBars = false;
        this.Text = this.m_oToolBar.Text;
      }
      this.LayoutCaptionButtons();
      this.m_bPerformingLayout = false;
    }

    private void DrawButton(
      QButtonArea buttonArea,
      bool alwaysDrawHot,
      Image image,
      Graphics graphics)
    {
      if (!(buttonArea.Bounds != Rectangle.Empty))
        return;
      bool flag = alwaysDrawHot || buttonArea.State == QButtonState.Hot || buttonArea.State == QButtonState.Pressed;
      Color replaceColorWith = (Color) (flag ? this.ColorScheme.ToolBarFormButtonActive : this.ColorScheme.ToolBarFormButton);
      if (flag)
        QRectanglePainter.Default.Paint(buttonArea.Bounds, (IQAppearance) new QAppearanceWrapper((IQAppearance) null)
        {
          GradientAngle = 90
        }, new QColorSet((Color) this.ColorScheme.ToolBarHotItemBackground1, (Color) this.ColorScheme.ToolBarHotItemBackground2, (Color) this.ColorScheme.ToolBarHotItemBorder), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      QControlPaint.DrawImage(graphics, image, Color.FromArgb((int) byte.MaxValue, 0, 0), replaceColorWith, buttonArea.Bounds);
    }

    protected override void OnPaintNonClientArea(PaintEventArgs e)
    {
      base.OnPaintNonClientArea(e);
      if (this.m_oToolBar == null)
        return;
      this.DrawButton(this.m_oCloseButtonArea, false, this.m_oToolBar.ToolBarConfiguration.UsedFormCloseToolBarMask, e.Graphics);
      this.DrawButton(this.m_oCustomizeButtonArea, this.m_oToolBar.IsCustomizing, this.m_oToolBar.ToolBarConfiguration.UsedFormCustomizeToolBarMask, e.Graphics);
    }

    protected override void OnNonClientAreaMouseMove(QNonClientAreaMouseEventArgs e)
    {
      base.OnNonClientAreaMouseMove(e);
      this.m_oCloseButtonArea.HandleMouseMove((MouseEventArgs) e);
      this.m_oCustomizeButtonArea.HandleMouseMove((MouseEventArgs) e);
    }

    protected override void OnNonClientAreaMouseLeave(EventArgs e)
    {
      base.OnNonClientAreaMouseLeave(e);
      this.m_oCloseButtonArea.HandleMouseLeave((MouseEventArgs) null);
      this.m_oCustomizeButtonArea.HandleMouseLeave((MouseEventArgs) null);
    }

    protected override void OnNonClientAreaMouseDown(QNonClientAreaMouseEventArgs e)
    {
      base.OnNonClientAreaMouseDown(e);
      if (e.NonClientAreaLocation == QNonClientAreaLocation.Caption)
      {
        this.Select();
        this.BringToFront();
      }
      e.CancelDefaultAction = this.m_oCloseButtonArea.HandleMouseDown((MouseEventArgs) e) || e.CancelDefaultAction;
      e.CancelDefaultAction = this.m_oCustomizeButtonArea.HandleMouseDown((MouseEventArgs) e) || e.CancelDefaultAction;
      if (e.CancelDefaultAction || e.NonClientAreaLocation != QNonClientAreaLocation.Caption || this.m_oToolBar == null || e.Button != MouseButtons.Left)
        return;
      Point point = new Point(this.Left + e.X, this.Top + e.Y);
      e.CancelDefaultAction = true;
      this.m_oToolBar.StartMoving(new Point(point.X - this.Left, point.Y - this.Top));
    }

    protected override void OnNonClientAreaDoubleClick(QNonClientAreaMouseEventArgs e)
    {
      base.OnNonClientAreaDoubleClick(e);
      e.CancelDefaultAction = true;
    }

    protected override void OnNonClientAreaMouseUp(QNonClientAreaMouseEventArgs e)
    {
      base.OnNonClientAreaMouseUp(e);
      e.CancelDefaultAction = this.m_oCloseButtonArea.HandleMouseUp((MouseEventArgs) e) || e.CancelDefaultAction;
      e.CancelDefaultAction = this.m_oCustomizeButtonArea.HandleMouseUp((MouseEventArgs) e) || e.CancelDefaultAction;
    }

    private void CloseButtonArea_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      this.RefreshNoClientArea();
      if (e.ToState != QButtonState.Pressed)
        return;
      if (this.m_oToolBar.ToolBarConfiguration.HideOnClose)
        this.Visible = false;
      else
        this.Dispose();
    }

    private void CustomizeButtonArea_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      switch (e.ToState)
      {
        case QButtonState.Normal:
          this.Refresh();
          break;
        case QButtonState.Hot:
          this.Refresh();
          break;
        case QButtonState.Pressed:
          if (!this.m_oToolBar.IsCustomizing)
          {
            this.m_oToolBar.StartCustomizing();
            break;
          }
          this.m_oToolBar.EndCustomizing();
          break;
      }
    }

    private void ToolBarConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Refresh();
    }

    private void ToolBar_Disposed(object sender, EventArgs e) => this.Dispose();

    private void Owner_VisibleChanged(object sender, EventArgs e)
    {
      if (this.ToolBarControl == null || !this.Controls.Contains(this.ToolBarControl))
        return;
      this.Visible = this.Owner.Visible;
    }

    private void Owner_Closed(object sender, EventArgs e) => this.Dispose();

    private void Owner_Activated(object sender, EventArgs e)
    {
    }

    private void Owner_Deactivate(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.Owner = (Form) null;
    }
  }
}
