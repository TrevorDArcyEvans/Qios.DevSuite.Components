// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingMenu
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QFloatingMenu : QMenu
  {
    private int m_iShadingWidth = 5;
    private bool m_bTopMost;
    private bool m_bCreatingControl;
    private QFloatingMenuPainter m_oPainter;
    private QFloatingMenuPaintParams m_oPaintParams;
    private QControlShadeWindow m_oShadeWindow;

    public QFloatingMenu() => this.InternalConstruct();

    public QFloatingMenu(IQCommandContainer customCommandContainer)
      : base(customCommandContainer)
    {
      this.InternalConstruct();
    }

    public QFloatingMenu(QCommand parentCommand, QMenuItemCollection menuItems)
      : base(parentCommand, menuItems)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.SuspendLayout();
      this.NextMenuItemArrowKey = Keys.Down;
      this.PreviousMenuItemArrowKey = Keys.Up;
      this.OpenChildMenuArrowKey = Keys.Right;
      this.OpenChildMenuArrowKey2 = Keys.None;
      this.CloseChildMenuArrowKey = Keys.Left;
      this.SetTopLevel(true);
      this.m_oPainter = new QFloatingMenuPainter();
      this.m_oPaintParams = new QFloatingMenuPaintParams();
      this.SetConfigurationBase((QCommandConfiguration) new QFloatingMenuConfiguration(), false);
      this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.AlwaysAutoExpand | QMenuItemContainerBehaviorFlags.DoNotDrawExpandedItemWhenHotItemShown | QMenuItemContainerBehaviorFlags.CanIterateThroughHiddenItems, true);
      this.m_bCreatingControl = true;
      this.Visible = false;
      this.m_bCreatingControl = false;
      this.ResumeLayout(false);
    }

    private QFloatingMenuPainter Painter => this.m_oPainter;

    internal QFloatingMenuPaintParams PaintParams => this.m_oPaintParams;

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the Configuration")]
    [Category("QAppearance")]
    public QFloatingMenuConfiguration Configuration
    {
      get => (QFloatingMenuConfiguration) this.ConfigurationBase;
      set => this.ConfigurationBase = (QCommandConfiguration) value;
    }

    public override IWin32Window OwnerWindow
    {
      get => base.OwnerWindow;
      set
      {
        if (value == base.OwnerWindow)
          return;
        base.OwnerWindow = value;
        if (this.m_oShadeWindow == null)
          return;
        this.m_oShadeWindow.OwnerWindow = value;
      }
    }

    internal bool ShowDepersonalizeItem => this.Configuration.PersonalizedItemBehavior == QPersonalizedItemBehavior.DependsOnPersonalized && this.Personalized && this.MenuItems.HasPersonalizedItems(false);

    [Browsable(false)]
    public Size RequestedSize => this.PaintParams.RequestedSize;

    private bool ShadeVisible => this.Configuration != null && this.Configuration.UsedShowBackgroundShade;

    internal Control RealOwnerWindow => Control.FromHandle((IntPtr) NativeMethods.GetWindowLong(this.Handle, -8));

    internal bool TopMost
    {
      get => this.m_bTopMost;
      set
      {
        if (this.m_bTopMost == value)
          return;
        this.m_bTopMost = value;
        this.SetTopMostCore();
      }
    }

    private void SetTopMostCore()
    {
      if (!this.IsHandleCreated)
        return;
      if (this.m_bTopMost)
      {
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 19U);
      }
      else
      {
        if (this.OwnerWindow == null || NativeHelper.IsTopMost(this.OwnerWindow.Handle))
          return;
        NativeMethods.SetWindowPos(this.Handle, new IntPtr(-2), 0, 0, 0, 0, 19U);
      }
    }

    protected override void SetOwnerWindowCore()
    {
      base.SetOwnerWindowCore();
      if (!this.IsHandleCreated)
        return;
      NativeMethods.SetWindowLong(this.Handle, -8, this.OwnerWindow != null ? this.OwnerWindow.Handle.ToInt32() : IntPtr.Zero.ToInt32());
      if (!this.m_bTopMost && (this.OwnerWindow == null || !NativeHelper.IsTopMost(this.OwnerWindow.Handle)))
        return;
      NativeMethods.SetWindowPos(this.Handle, new IntPtr(-1), 0, 0, 0, 0, 19U);
    }

    private void SetShadeVisibleCore(bool value, bool refresh)
    {
      if (value && this.m_oShadeWindow == null)
        this.m_oShadeWindow = new QControlShadeWindow((Control) this, this.OwnerWindow);
      if (value)
      {
        this.SetShadeWindowProperties();
        this.m_oShadeWindow.Visible = true;
      }
      else
      {
        if (this.m_oShadeWindow == null)
          return;
        this.m_oShadeWindow.Visible = false;
      }
    }

    private void SetShadeWindowProperties()
    {
      if (this.m_oShadeWindow == null)
        return;
      this.m_oShadeWindow.ShadeColor = (Color) this.ColorScheme.MenuShade;
      this.m_oShadeWindow.ShadeOffsetTopLeft = new Point(this.ShadingWidth / 2, this.ShadingWidth / 2);
      this.m_oShadeWindow.ShadeOffsetBottomRight = new Point(this.ShadingWidth / 2, this.ShadingWidth / 2);
      this.m_oShadeWindow.GradientLength = this.ShadingWidth;
      this.m_oShadeWindow.CornerSize = 3;
      this.SetShadeWindowRegion();
    }

    private void SetShadeWindowRegion()
    {
      this.m_oShadeWindow.UpdateShadeBounds();
      if (this.ParentMenuItemContainer == null)
        return;
      Region region = new Region();
      region.MakeInfinite();
      this.ParentMenuItemContainer.ExcludeBoundsFromRegion(region, (Control) this.m_oShadeWindow);
      this.m_oShadeWindow.ShadeRegion = region;
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, width, height, specified);
      if (this.m_oShadeWindow == null || !this.m_oShadeWindow.Visible)
        return;
      this.SetShadeWindowRegion();
    }

    [Browsable(false)]
    private int ShadingWidth => !this.ShadeVisible ? 0 : this.m_iShadingWidth;

    public override void HandleCommandChanged(QCommandUIRequest changeRequest, QCommand sender)
    {
      if (!this.Visible)
        return;
      base.HandleCommandChanged(changeRequest, sender);
    }

    public override void HandleCommandCollectionChanged(int fromCount, int toCount)
    {
      if (!this.Visible)
        return;
      base.HandleCommandCollectionChanged(fromCount, toCount);
    }

    public override void ShowMenu(
      Rectangle bounds,
      Rectangle openingItemBounds,
      QRelativePositions openingItemRelativePosition,
      QCommandDirections animateDirection)
    {
      this.PutFirstShownCommand(0);
      this.PutAnimateDirection(animateDirection);
      this.PutOpeningItemBounds(openingItemBounds);
      this.PutOpeningItemRelativePosition(openingItemRelativePosition);
      this.PerformLayout();
      this.SetBounds(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
      this.Visible = true;
    }

    public override void ShowChildMenu(QMenuItem menuItem, bool animate, bool showHotkeyPrefix)
    {
      if (menuItem == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (menuItem)));
      menuItem.ChildMenu.PutPersonalized(this.RootMenu.Personalized);
      if (menuItem.ChildContainer is QFloatingMenu)
      {
        QFloatingMenu childContainer = (QFloatingMenu) menuItem.ChildContainer;
        childContainer.ToolTipConfiguration = (QToolTipConfiguration) QObjectCloner.CloneObject((object) this.ToolTipConfiguration);
        childContainer.ToolTipConfiguration.Enabled = this.Configuration.ShowToolTips;
        childContainer.Configuration = this.Configuration;
        childContainer.ColorScheme = this.ColorScheme;
        childContainer.Font = this.Font;
        childContainer.UseAnimation = animate;
        childContainer.HotkeyVisible = showHotkeyPrefix;
      }
      Rectangle screen = this.ContainerRectangleToScreen(new Rectangle(this.PaintParams.BorderWidth, menuItem.Top, this.Width - this.PaintParams.BorderWidth * 2, menuItem.Height));
      QMenuCalculateBoundsResult menuBounds = QMenu.CalculateMenuBounds(new Rectangle(new Point(screen.Right, screen.Top), menuItem.ChildMenu.CalculateRequestedSize()), screen, QRelativePositions.Left, QCommandDirections.Down | QCommandDirections.Right, QMenuCalculateBoundsOptions.None);
      menuItem.ChildMenu.ShowMenu(menuBounds.Bounds, screen, menuBounds.OpeningItemPosition, menuBounds.AnimateDirection);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void MakeSureItemIsShown(QMenuItem item, bool refresh)
    {
      int index = this.Items.IndexOf(item);
      if (index < 0)
        return;
      if (index < this.FirstShownCommand)
      {
        this.PutFirstShownCommand(index);
        this.PerformLayout();
        if (!refresh)
          return;
        this.Refresh();
      }
      else
      {
        if (index <= this.LastShownCommand)
          return;
        this.PutFirstShownCommand(this.FirstShownCommand + (index - this.LastShownCommand));
        this.PerformLayout();
        while (index > this.LastShownCommand)
        {
          this.PutFirstShownCommand(this.FirstShownCommand + 1);
          this.PerformLayout();
        }
        if (!refresh)
          return;
        this.Refresh();
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void DepersonalizeMenuItemContainer()
    {
      if (!this.Personalized)
        return;
      if ((this.RootMenu.BehaviorFlags & QMenuItemContainerBehaviorFlags.SimplePersonalizing) != QMenuItemContainerBehaviorFlags.SimplePersonalizing)
        this.RootMenu.PutPersonalized(false);
      this.PutPersonalized(false);
      Size requestedSize = this.CalculateRequestedSize();
      int x = this.Left;
      int y = this.Top;
      if ((this.OpeningItemRelativePosition & QRelativePositions.Below) == QRelativePositions.Below)
        y = this.OpeningItemBounds.Top - requestedSize.Height;
      if ((this.OpeningItemRelativePosition & QRelativePositions.Right) == QRelativePositions.Right)
        x = this.OpeningItemBounds.Left - requestedSize.Width;
      QMenuCalculateBoundsResult menuBounds = QMenu.CalculateMenuBounds(new Rectangle(new Point(x, y), requestedSize), this.OpeningItemBounds, this.OpeningItemRelativePosition, this.AnimateDirection, QMenuCalculateBoundsOptions.DoNotFlipToRight | QMenuCalculateBoundsOptions.DoNotFlipToBottom);
      this.PerformLayout();
      this.PutOpeningItemRelativePosition(menuBounds.OpeningItemPosition);
      this.PutAnimateDirection(menuBounds.AnimateDirection);
      this.SetBounds(menuBounds.Bounds.Left, menuBounds.Bounds.Top, menuBounds.Bounds.Width, menuBounds.Bounds.Height);
      if (this.ParentMenu != null)
        this.ParentMenu.Refresh();
      this.Refresh();
    }

    public override bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      bool flag = false;
      if (this.ExpandedMenuItem != null)
        flag = this.ExpandedMenuItem.ChildMenu.HandleKeyDown(keys, destinationControl, message);
      if (flag)
        return flag;
      if (this.HandleDefaultNavigationKeys(keys, false))
        return true;
      if (!this.HandlePossibleHotKey(keys))
        return false;
      this.MakeSureItemIsShown(this.HotItem, false);
      return true;
    }

    public override bool HandleKeyUp(Keys keys, Control destinationControl, Message message) => false;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ExcludeBoundsFromRegion(Region region, Control useCoordinateSystem)
    {
      region.Exclude(useCoordinateSystem.RectangleToClient(this.Bounds));
      if (this.ParentMenu == null)
        return;
      this.ParentMenu.ExcludeBoundsFromRegion(region, useCoordinateSystem);
    }

    private Rectangle GetRectangleToDrawWithParentIntersectionColor()
    {
      if (this.ParentMenuItem != null && this.ParentMenuItem.ParentMenuItemContainer != null)
      {
        Rectangle client = this.RectangleToClient(this.ParentMenuItem.ParentMenuItemContainer.ContainerRectangleToScreen(this.ParentMenuItem.Bounds));
        if (this.ClientRectangle.IntersectsWith(client))
        {
          Rectangle intersectionColor = Rectangle.Intersect(this.ClientRectangle, client);
          if ((this.OpeningItemRelativePosition & QRelativePositions.Vertical) > QRelativePositions.None)
          {
            intersectionColor.X += this.PaintParams.BorderWidth;
            intersectionColor.Width -= this.PaintParams.BorderWidth * 2;
          }
          else if ((this.OpeningItemRelativePosition & QRelativePositions.Horizontal) > QRelativePositions.None)
          {
            intersectionColor.Y += this.PaintParams.BorderWidth;
            intersectionColor.Height = Math.Min(intersectionColor.Height - this.PaintParams.BorderWidth * 2, this.Height - this.PaintParams.BorderWidth * 2);
          }
          return intersectionColor;
        }
      }
      return Rectangle.Empty;
    }

    private void DrawShadedWindow()
    {
    }

    private void FillPaintParams()
    {
      this.CleanUpPaintParams();
      if (this.Configuration == null || this.ColorScheme == null)
        return;
      this.PaintParams.ShowDepersonalizeItem = this.ShowDepersonalizeItem;
      this.PaintParams.StringFormat = this.CreateStringFormat();
      this.PaintParams.BorderWidth = 1;
      this.PaintParams.Font = this.Font;
      this.PaintParams.FirstVisibleItem = this.FirstShownCommand;
      this.PaintParams.LastVisibleItem = -1;
      this.PaintParams.BackgroundColor1 = (Color) this.ColorScheme.MenuBackground1;
      this.PaintParams.BackgroundColor2 = (Color) this.ColorScheme.MenuBackground2;
      this.PaintParams.TextColor = (Color) this.ColorScheme.MenuText;
      this.PaintParams.TextActiveColor = (Color) this.ColorScheme.MenuTextActive;
      this.PaintParams.TextDisabledColor = (Color) this.ColorScheme.MenuTextDisabled;
      this.PaintParams.SeparatorColor = (Color) this.ColorScheme.MenuSeparator;
      this.PaintParams.HotItemBackground1Color = (Color) this.ColorScheme.MenuHotItemBackground1;
      this.PaintParams.HotItemBackground2Color = (Color) this.ColorScheme.MenuHotItemBackground2;
      this.PaintParams.HotItemBorderColor = (Color) this.ColorScheme.MenuHotItemBorder;
      this.PaintParams.PressedItemBackground1Color = (Color) this.ColorScheme.MenuPressedItemBackground1;
      this.PaintParams.PressedItemBackground2Color = (Color) this.ColorScheme.MenuPressedItemBackground2;
      this.PaintParams.PressedItemBorderColor = (Color) this.ColorScheme.MenuPressedItemBorder;
      this.PaintParams.CheckedItemBackground1Color = (Color) this.ColorScheme.MenuCheckedItemBackground1;
      this.PaintParams.CheckedItemBackground2Color = (Color) this.ColorScheme.MenuCheckedItemBackground2;
      this.PaintParams.CheckedItemBorderColor = (Color) this.ColorScheme.MenuCheckedItemBorder;
      this.PaintParams.IconBackground1Color = (Color) this.ColorScheme.MenuIconBackground1;
      this.PaintParams.IconBackground2Color = (Color) this.ColorScheme.MenuIconBackground2;
      this.PaintParams.IconBackgroundDepersonalized1Color = (Color) this.ColorScheme.MenuIconBackgroundDepersonalized1;
      this.PaintParams.IconBackgroundDepersonalized2Color = (Color) this.ColorScheme.MenuIconBackgroundDepersonalized2;
      this.PaintParams.DepersonalizeImageBackground = (Color) this.ColorScheme.MenuDepersonalizeImageBackground;
      this.PaintParams.DepersonalizeImageForeground = (Color) this.ColorScheme.MenuDepersonalizeImageForeground;
    }

    private void CleanUpPaintParams()
    {
      if (this.PaintParams == null || this.PaintParams.StringFormat == null)
        return;
      this.PaintParams.StringFormat.Dispose();
    }

    private void FillPaintParamsForPaint()
    {
      this.PaintParams.ParentMenuIntersectColor = this.ParentMenuItemContainer != null ? this.ParentMenuItemContainer.ParentMenuIntersectColor : Color.Empty;
      this.PaintParams.ParentIntersectionBounds = this.GetRectangleToDrawWithParentIntersectionColor();
    }

    public override Size CalculateRequestedSize()
    {
      Graphics graphics = this.CreateGraphics();
      this.FillPaintParams();
      this.Painter.CalculateRequestedSize((QCommandPaintParams) this.PaintParams, (QCommandConfiguration) this.Configuration, (Control) this, (QCommandCollection) this.MenuItems, graphics);
      graphics.Dispose();
      return this.PaintParams.RequestedSize;
    }

    internal override void RaiseMenuItemsRequested(QMenuEventArgs e)
    {
      base.RaiseMenuItemsRequested(e);
      if (e.MenuItem == null || !e.MenuItem.HasChildItems)
        return;
      QMenuCalculateBoundsResult menuBounds = QMenu.CalculateMenuBounds(new Rectangle(this.Location, this.CalculateRequestedSize()), this.OpeningItemBounds, this.OpeningItemRelativePosition, this.AnimateDirection, QMenuCalculateBoundsOptions.None);
      this.PerformLayout();
      this.SetBounds(menuBounds.Bounds.Left, menuBounds.Bounds.Top, menuBounds.Bounds.Width, menuBounds.Bounds.Height);
      this.Refresh();
    }

    private void LayoutMenu()
    {
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      this.PutLastShownCommand(-1);
      Graphics graphics = this.CreateGraphics();
      this.Painter.LayoutHorizontal(this.ClientRectangle, (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (Control) this, (QCommandCollection) this.MenuItems);
      this.PutLastShownCommand(this.PaintParams.LastVisibleItem);
      this.DepersonalizeMenuItemBounds = this.PaintParams.DepersonalizeItemBounds;
      this.PutHasMoreItemsDownBounds(this.PaintParams.HasMoreItemsDownBounds);
      this.PutHasMoreItemsUpBounds(this.PaintParams.HasMoreItemsUpBounds);
      graphics.Dispose();
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style = int.MinValue;
        if (this.Configuration != null && this.Configuration.TopMost)
          createParams.ExStyle |= 8;
        createParams.ExStyle = 0;
        createParams.ExStyle |= 134217728;
        createParams.ExStyle |= 128;
        if (this.OwnerWindow != null)
          createParams.Parent = QControlHelper.GetUndisposedHandle(this.OwnerWindow);
        return createParams;
      }
    }

    protected override void SetVisibleCore(bool value)
    {
      if (!this.m_bCreatingControl)
      {
        if (value)
        {
          QMenuAnimationType qmenuAnimationType = this.UseAnimation ? this.Configuration.UsedAnimationType : QMenuAnimationType.None;
          int flagsFromDirection = NativeHelper.GetAnimateWindowFlagsFromDirection(this.AnimateDirection);
          if (qmenuAnimationType != QMenuAnimationType.None && flagsFromDirection > 0)
          {
            QControlHelper.SecureAllControlHandles((Control) this, true);
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 19U);
            switch (qmenuAnimationType)
            {
              case QMenuAnimationType.Fade:
                NativeMethods.AnimateWindow(this.Handle, this.Configuration.AnimateTime, 524288 | flagsFromDirection);
                break;
              case QMenuAnimationType.Slide:
                NativeMethods.AnimateWindow(this.Handle, this.Configuration.AnimateTime, 262144 | flagsFromDirection);
                break;
            }
          }
          else
            NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 83U);
          for (int index = 0; index < this.MenuItems.Count; ++index)
          {
            if (this.MenuItems[index].IsVisible && this.MenuItems[index].Control != null)
            {
              this.Refresh();
              break;
            }
          }
          if (this.ShadeVisible)
            this.SetShadeVisibleCore(true, true);
        }
        else
        {
          this.SetShadeVisibleCore(false, false);
          NativeMethods.ShowWindow(this.Handle, 0);
          Control control = this.RetrieveTopmostControl();
          if (control != null)
          {
            Rectangle client1 = control.RectangleToClient(this.Bounds);
            control.Invalidate(client1, true);
            if (this.m_oShadeWindow != null)
            {
              Rectangle client2 = control.RectangleToClient(this.m_oShadeWindow.Bounds);
              control.Invalidate(client2, true);
            }
            control.Update();
          }
        }
      }
      base.SetVisibleCore(value);
      QControlHelper.UpdateControlRoot((Control) this);
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 33)
        m.Result = (IntPtr) 3;
      else if (m.Msg == 791)
      {
        bool visibilityProperty = QControlHelper.GetControlInternalVisibilityProperty((Control) this);
        QControlHelper.ForceControlInternalVisibilityProperty((Control) this, true);
        base.WndProc(ref m);
        QControlHelper.ForceControlInternalVisibilityProperty((Control) this, visibilityProperty);
      }
      else if (m.Msg == 792)
        base.WndProc(ref m);
      else if (m.Msg == 15)
        base.WndProc(ref m);
      else
        base.WndProc(ref m);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.PerformingLayout)
        return;
      this.PutPerformingLayout(true);
      this.FillPaintParams();
      this.LayoutMenu();
      this.PutPerformingLayout(false);
    }

    protected override void DrawMenuItem(
      QMenuItem menuItem,
      StringFormat format,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
      if (!menuItem.IsVisible)
        return;
      this.PaintParams.StringFormat = format;
      this.Painter.DrawItem((QCommand) menuItem, (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (QCommandContainer) this, flags, graphics);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      if (this.FirstShownCommand > 0)
        this.Painter.DrawHasMoreItemsUpImage((QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, e.Graphics);
      if (this.DepersonalizeMenuItemBounds.Height > 0)
        this.Painter.DrawDepersonalizeMenuImage((QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, this.DepersonalizeMenuItemState, e.Graphics);
      if (this.LastShownCommand == this.MenuItems.Count - 1)
        return;
      this.Painter.DrawHasMoreItemsDownImage((QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, e.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      this.Painter.DrawControlBackgroundHorizontal(this.Bounds, (QAppearanceBase) this.Appearance, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) this.Configuration, (Control) this, pevent.Graphics);
    }

    protected override void OnConfigurationChanged(EventArgs e)
    {
      if (this.Visible)
        this.SetShadeVisibleCore(this.ShadeVisible, false);
      this.ToolTipConfiguration.Enabled = this.Configuration.ShowToolTips;
      this.TopMost = this.Configuration.TopMost;
      base.OnConfigurationChanged(e);
    }
  }
}
