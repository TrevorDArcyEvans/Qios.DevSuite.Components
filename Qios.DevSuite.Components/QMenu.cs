// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenu
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public abstract class QMenu : QMenuItemContainer
  {
    private bool m_bUseAnimation = true;
    private Rectangle m_oHasMoreItemsUpBounds = Rectangle.Empty;
    private Rectangle m_oHasMoreItemsDownBounds = Rectangle.Empty;
    private QCommandDirections m_eAnimateDirection;
    private Rectangle m_oOpeningItemBounds = Rectangle.Empty;
    private QRelativePositions m_eOpeningItemRelativePosition;

    protected QMenu()
    {
    }

    protected QMenu(IQCommandContainer customCommandContainer)
      : base(customCommandContainer)
    {
    }

    protected QMenu(QCommand parentCommand, QMenuItemCollection menuItems)
      : base(parentCommand, menuItems)
    {
    }

    [Browsable(false)]
    public string MenuName => this.ParentMenuItem != null ? this.ParentMenuItem.FullName : (string) null;

    public override Color ForeColor
    {
      get => this.ColorScheme.MenuText.Current;
      set => this.ColorScheme.MenuText.Current = value;
    }

    [Browsable(false)]
    internal QCommandDirections AnimateDirection => this.m_eAnimateDirection;

    [Browsable(false)]
    internal QRelativePositions OpeningItemRelativePosition => this.m_eOpeningItemRelativePosition;

    [Browsable(false)]
    internal Rectangle OpeningItemBounds => this.m_oOpeningItemBounds;

    [Browsable(false)]
    internal Rectangle HasMoreItemsUpBounds => this.m_oHasMoreItemsUpBounds;

    [Browsable(false)]
    internal Rectangle HasMoreItemsDownBounds => this.m_oHasMoreItemsDownBounds;

    public QMenuItem FindMenuItem(string relativeName) => this.MenuItems.FindMenuItemWithRelativeName(relativeName);

    [Browsable(false)]
    internal bool DrawExpandedItem => (this.BehaviorFlags & QMenuItemContainerBehaviorFlags.DoNotDrawExpandedItemWhenHotItemShown) != QMenuItemContainerBehaviorFlags.DoNotDrawExpandedItemWhenHotItemShown || !this.ItemOrDepersonalizeItemHot && (this.MouseOverOuterBoundsItem == null || this.MouseOverOuterBoundsItem == this.ExpandedMenuItem);

    [Description("Gets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QAppearance Appearance => (QAppearance) base.Appearance;

    [Description("Contains the collection of MenuItems of this QMenu")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QMenuItemCollectionEditor), typeof (UITypeEditor))]
    public QMenuItemCollection MenuItems => this.Items;

    [Browsable(false)]
    public QMenu ParentMenu => this.ParentCommandContainer != null && this.ParentCommandContainer is QMenu ? (QMenu) this.ParentCommandContainer : (QMenu) null;

    [Browsable(false)]
    public QMenu RootMenu => this.ParentMenu != null ? this.ParentMenu.RootMenu : this;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QMenuItem HotMenuItem
    {
      get => this.HotItem;
      set => this.SetHotItem(value, QMenuItemActivationType.None);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool UseAnimation
    {
      get => this.m_bUseAnimation;
      set => this.m_bUseAnimation = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal QMenuItem ProposedExpandedMenuItem
    {
      get => this.ProposedExpandedItem;
      set => this.ProposedExpandedItem = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QMenuItem ExpandedMenuItem => this.ExpandedItem;

    public bool ActivateMenuItem(
      QMenuItem menuItem,
      bool animate,
      bool showHotkeyPrefix,
      QMenuItemActivationType activationType)
    {
      return this.ActivateItem(menuItem, true, animate, showHotkeyPrefix, activationType);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal bool ExpandMenuItem(
      QMenuItem menuItem,
      bool animate,
      bool showHotKeyPrefix,
      QMenuItemActivationType activationType)
    {
      return this.ExpandItem(menuItem, animate, showHotKeyPrefix, activationType);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual StringFormat CreateStringFormat()
    {
      StringFormat stringFormat = new StringFormat(StringFormat.GenericDefault);
      stringFormat.LineAlignment = StringAlignment.Near;
      stringFormat.Alignment = StringAlignment.Near;
      stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      stringFormat.FormatFlags = StringFormatFlags.NoWrap;
      if (this.Orientation == QCommandContainerOrientation.Vertical)
        stringFormat.FormatFlags |= StringFormatFlags.DirectionVertical;
      stringFormat.HotkeyPrefix = this.HotkeyVisible ? HotkeyPrefix.Show : HotkeyPrefix.Hide;
      return stringFormat;
    }

    public void DepersonalizeMenu() => this.DepersonalizeMenuItemContainer();

    public abstract void ShowMenu(
      Rectangle bounds,
      Rectangle openingItemBounds,
      QRelativePositions openingItemRelativePosition,
      QCommandDirections animateDirection);

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public virtual bool HandleKeyDown(Keys keys) => this.HandleKeyDown(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    public virtual bool HandleKeyDown(Keys keys, Control destinationControl, Message message) => false;

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public virtual bool HandleKeyUp(Keys keys) => this.HandleKeyUp(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    public virtual bool HandleKeyUp(Keys keys, Control destinationControl, Message message) => false;

    public virtual Size CalculateRequestedSize() => Size.Empty;

    protected abstract void DrawMenuItem(
      QMenuItem menuItem,
      StringFormat format,
      QCommandPaintOptions flags,
      Graphics graphics);

    public virtual void HideMenu() => this.HideContainer();

    public QMenuItem GetMenuItemAtPosition(int left, int top) => this.GetItemAtPosition(left, top);

    internal static QMenuCalculateBoundsResult CalculateMenuBounds(
      Rectangle originalBounds,
      Rectangle openingItemBounds,
      QRelativePositions openingItemRelativePosition,
      QCommandDirections animationDirection,
      QMenuCalculateBoundsOptions options)
    {
      bool flag1 = (options & QMenuCalculateBoundsOptions.DoNotFlipToRight) == QMenuCalculateBoundsOptions.DoNotFlipToRight;
      bool flag2 = (options & QMenuCalculateBoundsOptions.DoNotFlipToBottom) == QMenuCalculateBoundsOptions.DoNotFlipToBottom;
      int x = originalBounds.Left;
      int y = originalBounds.Top;
      int width = originalBounds.Width;
      int num1 = originalBounds.Height;
      Screen screen = Screen.FromRectangle(openingItemBounds);
      if ((animationDirection & QCommandDirections.Left) == QCommandDirections.Left)
      {
        animationDirection &= ~QCommandDirections.Left;
        animationDirection |= QCommandDirections.Right;
      }
      if ((animationDirection & QCommandDirections.Up) == QCommandDirections.Up)
      {
        animationDirection &= ~QCommandDirections.Up;
        animationDirection |= QCommandDirections.Down;
      }
      if (screen.WorkingArea.Left > originalBounds.Left)
      {
        if ((openingItemRelativePosition & QRelativePositions.Horizontal) != QRelativePositions.None)
        {
          if (!flag1)
          {
            x = openingItemBounds.Right;
            openingItemRelativePosition = openingItemRelativePosition & QRelativePositions.Vertical | ~openingItemRelativePosition & QRelativePositions.Horizontal;
            if ((animationDirection & QCommandDirections.Left) == QCommandDirections.Left)
            {
              animationDirection &= ~QCommandDirections.Left;
              animationDirection |= QCommandDirections.Right;
            }
          }
        }
        else
          x = screen.WorkingArea.Left;
      }
      else if (screen.WorkingArea.Right < originalBounds.Right)
      {
        if ((openingItemRelativePosition & QRelativePositions.Horizontal) != QRelativePositions.None)
        {
          x = openingItemBounds.Left - originalBounds.Width;
          openingItemRelativePosition = openingItemRelativePosition & QRelativePositions.Vertical | ~openingItemRelativePosition & QRelativePositions.Horizontal;
          if ((animationDirection & QCommandDirections.Right) == QCommandDirections.Right)
          {
            animationDirection &= ~QCommandDirections.Right;
            animationDirection |= QCommandDirections.Left;
          }
        }
        else
          x = screen.WorkingArea.Right - originalBounds.Width;
      }
      if (screen.WorkingArea.Top > originalBounds.Top)
      {
        if ((openingItemRelativePosition & QRelativePositions.Vertical) != QRelativePositions.None)
        {
          int num2 = openingItemBounds.Top - screen.WorkingArea.Top;
          int val1 = screen.WorkingArea.Bottom - openingItemBounds.Bottom;
          if (val1 > num1 || val1 > num2)
          {
            if (!flag2)
            {
              y = openingItemBounds.Bottom;
              num1 = Math.Min(val1, originalBounds.Height);
              openingItemRelativePosition = openingItemRelativePosition & QRelativePositions.Horizontal | ~openingItemRelativePosition & QRelativePositions.Vertical;
              if ((animationDirection & QCommandDirections.Up) == QCommandDirections.Up)
              {
                animationDirection &= ~QCommandDirections.Up;
                animationDirection |= QCommandDirections.Down;
              }
            }
          }
          else
          {
            y = screen.WorkingArea.Top;
            num1 = num2;
          }
        }
        else
        {
          y = screen.WorkingArea.Top;
          num1 = Math.Min(num1, screen.WorkingArea.Height);
        }
      }
      else if (screen.WorkingArea.Bottom < originalBounds.Bottom)
      {
        if ((openingItemRelativePosition & QRelativePositions.Vertical) != QRelativePositions.None)
        {
          int val1 = openingItemBounds.Top - screen.WorkingArea.Top;
          int num3 = screen.WorkingArea.Bottom - openingItemBounds.Bottom;
          if (val1 > num1 || val1 > num3)
          {
            num1 = Math.Min(val1, originalBounds.Height);
            y = openingItemBounds.Top - num1;
            openingItemRelativePosition = openingItemRelativePosition & QRelativePositions.Horizontal | ~openingItemRelativePosition & QRelativePositions.Vertical;
            if ((animationDirection & QCommandDirections.Down) == QCommandDirections.Down)
            {
              animationDirection &= ~QCommandDirections.Down;
              animationDirection |= QCommandDirections.Up;
            }
          }
          else
            num1 = num3;
        }
        else
        {
          num1 = Math.Min(num1, screen.WorkingArea.Height);
          y = screen.WorkingArea.Bottom - num1;
        }
      }
      return new QMenuCalculateBoundsResult(new Rectangle(x, y, width, num1), openingItemRelativePosition, animationDirection);
    }

    private bool HasMoreItemsUpContainsPosition(int x, int y) => this.m_oHasMoreItemsUpBounds != Rectangle.Empty && this.m_oHasMoreItemsUpBounds.Contains(x, y);

    internal void PutHasMoreItemsUpBounds(Rectangle bounds) => this.m_oHasMoreItemsUpBounds = bounds;

    private bool HasMoreItemsDownContainsPosition(int x, int y) => this.m_oHasMoreItemsDownBounds != Rectangle.Empty && this.m_oHasMoreItemsDownBounds.Contains(x, y);

    internal void PutHasMoreItemsDownBounds(Rectangle bounds) => this.m_oHasMoreItemsDownBounds = bounds;

    internal void PutAnimateDirection(QCommandDirections direction) => this.m_eAnimateDirection = direction;

    internal void PutOpeningItemBounds(Rectangle openingItemBounds) => this.m_oOpeningItemBounds = openingItemBounds;

    internal void PutOpeningItemRelativePosition(QRelativePositions openingItemRelativePosition) => this.m_eOpeningItemRelativePosition = openingItemRelativePosition;

    protected override string BackColorPropertyName => "MenuBackground1";

    protected override string BackColor2PropertyName => "MenuBackground2";

    protected override string BorderColorPropertyName => "MenuBorder";

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QAppearance();

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.HasMoreItemsDownContainsPosition(e.X, e.Y) && !this.HasMoreItemsUpContainsPosition(e.X, e.Y))
        return;
      this.StartTimer();
    }

    protected override void OnMouseDown(MouseEventArgs e) => base.OnMouseDown(e);

    protected override void OnMouseLeave(EventArgs e) => base.OnMouseLeave(e);

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.ColorScheme == null)
        return;
      StringFormat stringFormat = this.CreateStringFormat();
      bool drawExpandedItem = this.DrawExpandedItem;
      for (int firstShownCommand = this.FirstShownCommand; firstShownCommand <= this.LastShownCommand; ++firstShownCommand)
      {
        if (this.MenuItems[firstShownCommand].IsVisible)
        {
          QCommandPaintOptions flags = QCommandPaintOptions.None;
          if (drawExpandedItem && this.MenuItems[firstShownCommand] == this.ExpandedMenuItem)
            flags |= QCommandPaintOptions.Expanded;
          if (this.MenuItems[firstShownCommand] == this.MouseDownItem)
            flags |= QCommandPaintOptions.Pressed;
          if (this.MenuItems[firstShownCommand] == this.HotMenuItem)
            flags |= QCommandPaintOptions.Hot;
          if (this.Orientation == QCommandContainerOrientation.Vertical)
            flags |= QCommandPaintOptions.Vertical;
          this.DrawMenuItem(this.MenuItems[firstShownCommand], stringFormat, flags, e.Graphics);
          this.RaisePaintMenuItem(new QPaintMenuItemEventArgs(this.MenuItems[firstShownCommand], flags, stringFormat, e.Graphics));
        }
      }
      stringFormat.Dispose();
      this.PaintAdornments(e.Graphics);
    }

    protected virtual void ResetMenuState() => this.ResetState();

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ResetState()
    {
      base.ResetState();
      this.PutOpeningItemRelativePosition(QRelativePositions.None);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 28 && (int) m.WParam == 0 && this.Visible)
      {
        if (this.RootMenuItemContainer != null && this.RootMenuItemContainer != this)
          this.RootMenuItemContainer.ResetState();
        else
          this.HideMenu();
      }
      base.WndProc(ref m);
    }

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (e.TimerId != 17)
        return;
      Point client = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
      if (this.HasMoreItemsDownContainsPosition(client.X, client.Y))
      {
        this.ResetExpandedItem();
        this.ProposedExpandedMenuItem = (QMenuItem) null;
        this.SetHotItem((QMenuItem) null, QMenuItemActivationType.Mouse);
        this.PutFirstShownCommand(this.FirstShownCommand + 1);
        this.PerformLayout();
        this.Refresh();
      }
      else
      {
        if (!this.HasMoreItemsUpContainsPosition(client.X, client.Y))
          return;
        this.ResetExpandedItem();
        this.ProposedExpandedMenuItem = (QMenuItem) null;
        this.SetHotItem((QMenuItem) null, QMenuItemActivationType.Mouse);
        this.PutFirstShownCommand(this.FirstShownCommand - 1);
        this.PerformLayout();
        this.Refresh();
      }
    }

    protected override void StopTimer()
    {
      Point client = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
      if (this.HasMoreItemsDownContainsPosition(client.X, client.Y) || this.HasMoreItemsUpContainsPosition(client.X, client.Y))
        return;
      base.StopTimer();
    }
  }
}
