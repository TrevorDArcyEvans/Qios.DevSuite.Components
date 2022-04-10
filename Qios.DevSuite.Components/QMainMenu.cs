// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMainMenu
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QMainMenuDesigner), typeof (IDesigner))]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QMainMenu), "Resources.ControlImages.QMainMenu.bmp")]
  public class QMainMenu : QMenu, ISupportInitialize, IMessageFilter, IQToolBar, IQMainMenu
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QWeakMessageFilter m_oWeakMessageFilter;
    private QToolBarAction m_oAction;
    private Cursor m_oCachedCursor;
    private QToolBarForm m_oToolBarForm;
    private QMainMenuPainter m_oPainter;
    private int m_iRowIndex = -1;
    private int m_iToolBarPositionIndex = -1;
    private QMainMenuPaintParams m_oPaintParams;
    private QToolBarBevelAppearance m_oBevelAppearance;
    private QButtonArea m_oSizingGripArea;
    private QButtonArea m_oCustomizeArea;
    private bool m_bIsMoving;
    private Point m_oLastMovePointHandled = Point.Empty;
    private QMenuMdiButtons m_oMdiButtons;
    private Form m_oForm;
    private QMainFormOverride m_oMainFormOverride;
    private IContainer m_oComponents;
    private Control m_oParentControl;
    private QToolBarRow m_oParentToolBarRow;
    private QToolBarRow m_oOriginalToolBarRow;
    private EventHandler m_oParentControlSizeChanged;

    public QMainMenu()
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oMainFormOverride = new QMainFormOverride((IQMainMenu) this);
      this.SuspendLayout();
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetQControlStyles(QControlStyles.NeverCopyBitsOnBoundsChange, true);
      this.m_oAction = new QToolBarAction((IQToolBar) this);
      this.m_oSizingGripArea = new QButtonArea(MouseButtons.Left);
      this.m_oSizingGripArea.ButtonStateChanged += new QButtonAreaEventHandler(this.SizingGripArea_ButtonStateChanged);
      this.m_oCustomizeArea = new QButtonArea(MouseButtons.Left);
      this.m_oCustomizeArea.ButtonStateChanged += new QButtonAreaEventHandler(this.CustomizeArea_ButtonStateChanged);
      this.m_oPaintParams = new QMainMenuPaintParams();
      this.m_oPainter = new QMainMenuPainter();
      this.m_oBevelAppearance = new QToolBarBevelAppearance();
      this.m_oBevelAppearance.AppearanceChanged += new EventHandler(this.BevelAppearance_AppearanceChanged);
      this.m_oParentControlSizeChanged = new EventHandler(this.ParentControl_SizeChanged);
      this.m_oComponents = (IContainer) new Container();
      this.m_oMdiButtons = new QMenuMdiButtons(this, this.m_oComponents);
      this.SetConfigurationBase((QCommandConfiguration) new QMainMenuConfiguration(), false);
      this.SetChildMenuConfigurationBase(new QFloatingMenuConfiguration(), false);
      this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.HideExpandedItemOnClick | QMenuItemContainerBehaviorFlags.CanSimulateFocus, true);
      this.Dock = DockStyle.Top;
      this.m_oWeakMessageFilter = new QWeakMessageFilter((object) this);
      Application.AddMessageFilter((IMessageFilter) this.m_oWeakMessageFilter);
      this.ResumeLayout(false);
    }

    [Description("Gets or sets the form this QMainMenu belongs to. When this form is a MDI Form the MDI buttons and the Active Mdi Child Icon are shown")]
    [DefaultValue(null)]
    [Category("QBehavior")]
    public Form Form
    {
      get => this.m_oForm;
      set
      {
        if (this.m_oForm == value)
          return;
        this.m_oForm = value;
        this.m_oMainFormOverride.Form = value;
        this.PerformLayout();
        this.Invalidate();
      }
    }

    [Browsable(false)]
    public Form MdiForm => this.m_oMainFormOverride.MdiForm;

    [Browsable(false)]
    public bool MdiButtonsVisible => this.MdiForm != null && this.MdiForm.ActiveMdiChild != null && NativeHelper.GetCurrentFormState(this.MdiForm.ActiveMdiChild) == FormWindowState.Maximized && this.m_oMdiButtons.IsVisible;

    public bool ActiveMdiIconVisible => this.m_oMainFormOverride.ActiveMdiChild != null && NativeHelper.GetCurrentFormState(this.MdiForm.ActiveMdiChild) == FormWindowState.Maximized && this.Configuration.ActiveMdiChildIconVisible && this.m_oMainFormOverride.ActiveMdiChildIcon != null;

    public void ResetBevelAppearance() => this.m_oBevelAppearance.SetToDefaultValues();

    public bool ShouldSerializeBevelAppearance() => !this.m_oBevelAppearance.IsSetToDefaultValues();

    [Description("Gets the appearance for the bevel")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QToolBarBevelAppearance BevelAppearance => this.m_oBevelAppearance;

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    [Description("Gets or sets the Configuration")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QMainMenuConfiguration Configuration
    {
      get => (QMainMenuConfiguration) this.ConfigurationBase;
      set => this.ConfigurationBase = (QCommandConfiguration) value;
    }

    [DefaultValue(-1)]
    [Category("QBehavior")]
    [Description("Gets or sets the index of the row where the ToolBar must be placed on a ToolBarHost.")]
    public int RowIndex
    {
      get => this.m_oParentToolBarRow == null || this.ToolBarHost == null ? this.m_iRowIndex : this.m_oParentToolBarRow.PositionIndex;
      set
      {
        if (this.ToolBarHost != null)
          this.DockMainMenu(this.ToolBarHost, value, this.ToolBarPositionIndex, this.m_oAction.UserRequestedPosition);
        else
          this.m_iRowIndex = value;
      }
    }

    [Category("QBehavior")]
    [DefaultValue(-1)]
    [Description("Gets or sets the index where the ToolBar must be placed on a ToolBarHost.")]
    public int ToolBarIndex
    {
      get => this.m_iToolBarPositionIndex;
      set
      {
        if (this.ToolBarHost != null)
          this.DockMainMenu(this.ToolBarHost, this.RowIndex, value, this.m_oAction.UserRequestedPosition);
        else
          this.m_iToolBarPositionIndex = value;
      }
    }

    int IQToolBar.ToolBarPositionIndex => this.ToolBarPositionIndex;

    internal int ToolBarPositionIndex => this.m_iToolBarPositionIndex;

    void IQToolBar.SetToolBarPositionIndex(int positionIndex, bool updateParentRow) => this.SetToolBarPositionIndex(positionIndex, updateParentRow);

    internal void SetToolBarPositionIndex(int positionIndex, bool updateParentRow)
    {
      if (updateParentRow && this.m_oParentToolBarRow != null)
        this.m_oParentToolBarRow.SetToolBarPositionIndex((IQToolBar) this, positionIndex);
      else
        this.m_iToolBarPositionIndex = positionIndex;
    }

    [Description("Gets or sets the requested position on a ToolBarHost. This is the top position when orientation is vertical, else the left position.")]
    [DefaultValue(0)]
    [Category("QBehavior")]
    public int RequestedPosition
    {
      get => this.m_oAction.UserRequestedPosition;
      set
      {
        if (this.m_oAction.UserRequestedPosition == value)
          return;
        this.m_oAction.UserRequestedPosition = value;
        if (value > 0 && this.m_oParentToolBarRow != null)
          this.m_oParentToolBarRow.SetAsFirstPriority((IQToolBar) this);
        this.PerformLayout();
      }
    }

    [Browsable(false)]
    public bool IsFloating => this.m_oToolBarForm != null && this.Parent == this.m_oToolBarForm;

    public void DockMainMenu(QToolBarHost host, int rowIndex, int toolBarIndex) => this.m_oAction.DockToolBar(host, rowIndex, toolBarIndex, 0);

    public void DockMainMenu(
      QToolBarHost host,
      int rowIndex,
      int toolBarIndex,
      int requestedPosition)
    {
      this.m_oAction.DockToolBar(host, rowIndex, toolBarIndex, requestedPosition);
    }

    void IQToolBar.DockToolBar(
      QToolBarHost host,
      int rowIndex,
      int toolBarIndex,
      int requestedPosition)
    {
      this.m_oAction.DockToolBar(host, rowIndex, toolBarIndex, requestedPosition);
    }

    void IQToolBar.FloatToolBar(Point screenPoint) => this.m_oAction.FloatToolBar(screenPoint);

    public void FloatMainMenu(Point screenPoint) => this.m_oAction.FloatToolBar(screenPoint);

    internal int MenuItemGradientAngle => this.PaintParams.ItemGradientAngle;

    internal bool Horizontal => this.Orientation == QCommandContainerOrientation.Horizontal || this.Orientation == QCommandContainerOrientation.None;

    bool IQToolBar.Horizontal => this.Horizontal;

    Rectangle IQToolBar.CustomizeItemBounds => this.CustomizeItemBounds;

    internal Rectangle CustomizeItemBounds => this.IsFloating ? this.ToolBarForm.CustomizeItemBounds : this.RectangleToScreen(this.m_oCustomizeArea.Bounds);

    [Browsable(false)]
    internal bool IsMoving => this.m_bIsMoving;

    bool IQToolBar.IsCustomizing => this.IsCustomizing;

    internal bool IsCustomizing => this.m_oAction.CustomizeMenuVisible;

    bool IQToolBar.Stretched => this.Stretched;

    internal bool Stretched => this.Configuration.Stretched && !(this.Parent is QToolBarForm) || this.Dock != DockStyle.None;

    internal QToolBarHost ToolBarHost => this.Parent == null || !(this.Parent is QToolBarHost) ? (QToolBarHost) null : (QToolBarHost) this.Parent;

    QToolBarHost IQToolBar.ToolBarHost => this.ToolBarHost;

    QToolBarForm IQToolBar.ToolBarForm
    {
      get => this.ToolBarForm;
      set => this.ToolBarForm = value;
    }

    internal QToolBarForm ToolBarForm
    {
      get => this.m_oToolBarForm;
      set => this.m_oToolBarForm = value;
    }

    internal bool ShowCanMove => this.Configuration.CanMove && this.Parent is QToolBarHost;

    internal bool ShowCustomizeButton => this.Configuration.CanCustomize && !(this.Parent is QToolBarForm);

    internal bool ShowCustomizeBar => (this.Configuration.ShowCustomizeBar || this.Configuration.CanCustomize || this.Configuration.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit) && !(this.Parent is QToolBarForm);

    bool IQToolBar.ShowMoreItemsButton => this.ShowMoreItemsButton;

    internal bool ShowMoreItemsButton => this.Configuration.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit && this.LastShownCommand < this.MenuItems.Count - 1;

    public void BeginInit()
    {
    }

    public void EndInit()
    {
      if (this.Form == null)
        this.Form = this.ParentForm;
      else
        this.m_oMainFormOverride.TryToAssignMdiClient();
    }

    private QMainMenuPaintParams PaintParams => this.m_oPaintParams;

    QToolBarPaintParams IQToolBar.PaintParams => (QToolBarPaintParams) this.m_oPaintParams;

    QToolBarConfiguration IQToolBar.ToolBarConfiguration => (QToolBarConfiguration) this.Configuration;

    QColorScheme IQToolBar.ColorScheme => this.ColorScheme;

    void IQToolBar.StartCustomizing() => this.StartCustomizing();

    void IQToolBar.EndCustomizing() => this.EndCustomizing();

    private void StartCustomizing()
    {
      this.m_oAction.ShowCustomizeMenu();
      this.Refresh();
    }

    private void EndCustomizing()
    {
      if (!this.IsCustomizing)
        return;
      this.m_oAction.CustomizeMenu.HideMenu();
      this.Refresh();
    }

    void IQToolBar.StartMoving(Point startOffset) => this.StartMoving(startOffset);

    internal void StartMoving(Point startOffset)
    {
      if (this.m_bIsMoving)
        return;
      if (!this.Capture)
        this.Capture = true;
      if (this.Cursor != Cursors.SizeAll)
        this.m_oCachedCursor = this.Cursor;
      this.Cursor = Cursors.SizeAll;
      this.m_bIsMoving = true;
      this.m_oAction.DragStartOffSet = startOffset;
    }

    internal void EndMoving()
    {
      if (!this.m_bIsMoving)
        return;
      this.Cursor = this.m_oCachedCursor;
      this.m_bIsMoving = false;
      this.Capture = false;
    }

    internal void MoveToolBar(Point proposedScreenPoint) => this.m_oAction.MoveToolBar(proposedScreenPoint);

    private QMainMenuPainter Painter => this.m_oPainter;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ResetState()
    {
      if (this.HasSimulatedFocus)
      {
        this.SimulateLostFocus();
        base.ResetState();
      }
      if (!this.IsCustomizing)
        return;
      this.EndCustomizing();
      if (!this.IsFloating)
        return;
      this.ToolBarForm.RefreshNoClientArea();
    }

    public override sealed string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        if (!this.Visible)
          return;
        this.PerformLayout();
        this.Refresh();
      }
    }

    public override AnchorStyles Anchor
    {
      get => base.Anchor;
      set => base.Anchor = this.ToolBarHost == null || (value & AnchorStyles.Bottom) != AnchorStyles.Bottom && (value & AnchorStyles.Bottom) != AnchorStyles.Right ? value : throw new InvalidOperationException(QResources.GetException("QMainMenu_Anchor_Invalid"));
    }

    [DefaultValue(DockStyle.Top)]
    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
        if (value == DockStyle.Fill)
          throw new InvalidOperationException(QResources.GetException("QMainMenu_Dock_NotFill"));
        if (value == DockStyle.Left || value == DockStyle.Right)
        {
          if (this.ToolBarHost != null)
            throw new InvalidOperationException(QResources.GetException("QMainMen_Dock_DontDockWhenHosted"));
          this.SetOrientation(QCommandContainerOrientation.Vertical, true);
        }
        else
        {
          if (this.ToolBarHost != null && value != DockStyle.None)
            throw new InvalidOperationException(QResources.GetException("QMainMen_Dock_DontDockWhenHosted"));
          this.SetOrientation(QCommandContainerOrientation.Horizontal, true);
        }
        base.Dock = value;
      }
    }

    public override void ShowMenu(
      Rectangle bounds,
      Rectangle openingItemBounds,
      QRelativePositions openingItemRelativePosition,
      QCommandDirections animateDirection)
    {
      this.Visible = true;
    }

    public override bool ContainsOrIsContainerWithHandle(IntPtr handle)
    {
      if (base.ContainsOrIsContainerWithHandle(handle))
        return true;
      return this.IsCustomizing && this.m_oAction.CustomizeMenu.ContainsOrIsContainerWithHandle(handle);
    }

    QToolBarRow IQToolBar.ParentToolBarRow
    {
      get => this.m_oParentToolBarRow;
      set => this.m_oParentToolBarRow = value;
    }

    QToolBarRow IQToolBar.OriginalToolBarRow
    {
      get => this.m_oOriginalToolBarRow;
      set => this.m_oOriginalToolBarRow = value;
    }

    int IQToolBar.UserRequestedPosition
    {
      get => this.m_oAction.UserRequestedPosition;
      set => this.m_oAction.UserRequestedPosition = value;
    }

    int IQToolBar.UserPriority
    {
      get => this.m_oAction.UserPriority;
      set => this.m_oAction.UserPriority = value;
    }

    QToolBarAction IQToolBar.Action => this.m_oAction;

    Rectangle IQToolBar.ProposedBounds
    {
      get => this.m_oAction.ProposedBounds;
      set => this.m_oAction.ProposedBounds = value;
    }

    Rectangle IQToolBar.Bounds => this.Bounds;

    bool IQToolBar.IsVisible => this.Visible;

    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      if (!this.IsFloating)
        return;
      this.m_oToolBarForm.Visible = value;
    }

    internal Size GetMinimumSizeForRow(QToolBarRow row, IQToolBar addingToolBar)
    {
      bool flag = false;
      if (this.Stretched && this.ToolBarHost != null)
        flag = true;
      else if (row != null)
      {
        int visibleCountLayoutType = row.GetVisibleCountLayoutType(QToolBarLayoutType.SetMoreItemsOnNoFit);
        if (addingToolBar != null && addingToolBar.ToolBarConfiguration.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit)
          ++visibleCountLayoutType;
        flag = visibleCountLayoutType > 0 && this.Configuration.LayoutType == QToolBarLayoutType.ExpandOnNoFit;
      }
      return flag ? ((IQToolBar) this).RequestedSize : this.Configuration.ToolBarPadding.InflateSizeWithPadding(this.PaintParams.MinimumSize, true, this.Orientation != QCommandContainerOrientation.Vertical);
    }

    Size IQToolBar.GetMinimumSizeForRow(QToolBarRow row, IQToolBar addingToolBar) => this.GetMinimumSizeForRow(row, addingToolBar);

    Size IQToolBar.MinimumSize => this.GetMinimumSizeForRow(this.m_oParentToolBarRow, (IQToolBar) null);

    Size IQToolBar.RequestedSize
    {
      get
      {
        if (!this.Stretched || this.ToolBarHost == null)
          return this.Configuration.ToolBarPadding.InflateSizeWithPadding(this.PaintParams.RequestedSize, true, this.Orientation != QCommandContainerOrientation.Vertical);
        return this.Horizontal ? new Size(this.ToolBarHost.ToolBarsAvailableSpace - this.ToolBarHost.ToolBarMargin.Horizontal, this.PaintParams.MinimumSize.Height) : new Size(this.PaintParams.MinimumSize.Width, this.ToolBarHost.ToolBarsAvailableSpace - this.ToolBarHost.ToolBarMargin.Horizontal);
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ExcludeBoundsFromRegion(Region region, Control useCoordinateSystem)
    {
      if (this.ExpandedMenuItem == null)
        return;
      region.Exclude(useCoordinateSystem.RectangleToClient(this.RectangleToScreen(this.ExpandedMenuItem.Bounds)));
    }

    public void ResetChildMenuConfiguration() => this.ChildMenuConfiguration.SetToDefaultValues();

    public bool ShouldSerializeChildMenuConfiguration() => !this.ChildMenuConfiguration.IsSetToDefaultValues();

    [Category("QAppearance")]
    [Description("Gets or sets the configuration for menus opened from this mainmenu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QFloatingMenuConfiguration ChildMenuConfiguration
    {
      get => this.ChildMenuConfigurationBase;
      set => this.ChildMenuConfigurationBase = value;
    }

    public override bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      QMenuItem itemWithShortcut = this.GetAccessibleMenuItemWithShortcut(Control.ModifierKeys | keys);
      if (itemWithShortcut != null && this.ShouldHandleShortcutsForControl(destinationControl, this.Configuration.ShortcutScope))
      {
        this.ActivateMenuItem(itemWithShortcut, false, true, QMenuItemActivationType.Shortcut);
        return itemWithShortcut.SuppressShortcutToSystem;
      }
      if (this.ShouldHandleKeyMessagesForControl(destinationControl))
      {
        if (this.HasSimulatedFocus)
        {
          bool flag = false;
          if (this.ExpandedItem != null)
            flag = this.ExpandedItem.ChildMenu.HandleKeyDown(keys, destinationControl, message);
          else if (this.HandlePossibleHotKey(keys))
            return true;
          if (!flag && !this.HandleDefaultNavigationKeys(keys, true) && Control.ModifierKeys == Keys.Alt && keys == Keys.Menu)
            this.ResetState();
          return true;
        }
        if (Control.ModifierKeys == Keys.Alt)
        {
          this.HotkeyVisible = true;
          if (this.HandlePossibleHotKey(keys))
          {
            this.SimulateGotFocus();
            return true;
          }
        }
      }
      return false;
    }

    public override bool HandleKeyUp(Keys keys, Control destinationControl, Message message)
    {
      if (keys == Keys.Menu)
        this.HotkeyVisible = false;
      return false;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public bool PreFilterMessage(ref Message m)
    {
      if (this.IsDisposed)
        return false;
      switch (m.Msg)
      {
        case 161:
        case 164:
        case 167:
        case 171:
        case 513:
        case 516:
        case 519:
        case 523:
          Point pt = new Point(m.LParam.ToInt32());
          if ((!this.IsFloating || !this.IsCustomizing || !this.ToolBarForm.CustomizeItemBounds.Contains(pt)) && !this.ContainsOrIsContainerWithHandle(m.HWnd))
            this.ResetState();
          return false;
        case 256:
        case 260:
          if (this.ContainsOrIsContainerWithHandle(m.HWnd))
            return false;
          Control controlFromHandle1 = QControlHelper.GetFirstControlFromHandle(m.HWnd);
          return this.HandleKeyDown((Keys) (int) m.WParam, controlFromHandle1, m);
        case 257:
        case 261:
          if (this.ContainsOrIsContainerWithHandle(m.HWnd))
            return false;
          Control controlFromHandle2 = QControlHelper.GetFirstControlFromHandle(m.HWnd);
          return this.HandleKeyUp((Keys) (int) m.WParam, controlFromHandle2, m);
        default:
          return false;
      }
    }

    protected override string BackColorPropertyName => "MainMenuBackground1";

    protected override string BackColor2PropertyName => "MainMenuBackground2";

    protected override string BorderColorPropertyName => "MainMenuBorder";

    internal override Color ParentMenuIntersectColor
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => (Color) this.ColorScheme.MainMenuExpandedItemBackground2;
    }

    private void FillPaintParams()
    {
      this.CleanUpPaintParams();
      if (this.Configuration == null || this.ColorScheme == null)
        return;
      this.PaintParams.ShadeImages = this.Configuration.ToolBarStyle == QToolBarStyle.Beveled;
      this.PaintParams.ShadeSeparator = this.Configuration.ToolBarStyle == QToolBarStyle.Beveled;
      this.PaintParams.ShadeSizingGrip = this.Configuration.ToolBarStyle == QToolBarStyle.Beveled;
      this.PaintParams.LayoutType = this.Configuration.LayoutType;
      this.PaintParams.ShowCustomizeBar = this.ShowCustomizeBar;
      this.PaintParams.Stretched = this.Stretched;
      this.PaintParams.ExpandOnItemClick = this.Configuration.ExpandOnItemClick;
      this.PaintParams.ShowCustomizeButton = this.ShowCustomizeButton;
      this.PaintParams.ShowSizingGrip = this.ShowCanMove;
      this.PaintParams.ToolBarStyle = this.Configuration.ToolBarStyle;
      this.PaintParams.CornerSize = this.PaintParams.ToolBarStyle == QToolBarStyle.Beveled ? this.Configuration.RoundedBevelCornerSize : 0;
      this.PaintParams.SizingGripWidth = this.Configuration.SizingGripWidth;
      this.PaintParams.SizingGripPadding = this.Configuration.SizingGripPadding;
      this.PaintParams.HasMoreItemsAreaWidth = this.Configuration.HasMoreItemsAreaWidth;
      this.PaintParams.ToolBarPadding = this.Configuration.ToolBarPadding;
      this.PaintParams.ItemsSpacing = this.Configuration.ItemsSpacing;
      this.PaintParams.StringFormat = this.CreateStringFormat();
      this.PaintParams.Font = this.Font;
      if (this.ActiveMdiIconVisible)
      {
        this.PaintParams.ActiveMdiIcon = this.m_oMainFormOverride.ActiveMdiChildIcon;
        this.PaintParams.ActiveMdiIconPadding = this.Configuration.ActiveMdiChildIconPadding;
      }
      if (this.MdiButtonsVisible)
      {
        this.PaintParams.MdiButtons = this.m_oMdiButtons;
        this.PaintParams.MdiButtonsSize = this.Configuration.MdiButtonsSize;
        this.PaintParams.MdiButtonsPadding = this.Configuration.MdiButtonsPadding;
      }
      if (this.PaintParams.ShowCustomizeButton)
      {
        this.PaintParams.CustomizeImage = (Bitmap) QControlPaint.CreateColorizedImage(this.Configuration.UsedCustomizeToolBarMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.ColorScheme.MenuText);
        if (this.Orientation == QCommandContainerOrientation.Vertical)
          this.PaintParams.CustomizeImage = QControlPaint.RotateFlipImage((Image) this.PaintParams.CustomizeImage, RotateFlipType.Rotate270FlipNone);
        if (this.PaintParams.ShadeImages)
          this.PaintParams.CustomizeImage = QControlPaint.AddShadeToImage(this.PaintParams.CustomizeImage, (Color) this.ColorScheme.ToolBarShade, 1, 1);
        QImageCache.ExcludeFromCleanup((Image) this.PaintParams.CustomizeImage);
      }
      if (this.PaintParams.LastVisibleItem < this.MenuItems.Count - 1)
      {
        this.PaintParams.HasMoreItemsImage = (Bitmap) QControlPaint.CreateColorizedImage(this.Configuration.UsedHasMoreItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.ColorScheme.MenuText);
        if (this.Orientation == QCommandContainerOrientation.Vertical)
          this.PaintParams.HasMoreItemsImage = QControlPaint.RotateFlipImage((Image) this.PaintParams.HasMoreItemsImage, RotateFlipType.Rotate90FlipNone);
        if (this.PaintParams.ShadeImages)
          this.PaintParams.HasMoreItemsImage = QControlPaint.AddShadeToImage(this.PaintParams.HasMoreItemsImage, (Color) this.ColorScheme.ToolBarShade, 1, 1);
        QImageCache.ExcludeFromCleanup((Image) this.PaintParams.HasMoreItemsImage);
      }
      this.PaintParams.ItemGradientAngle = this.Configuration.ActivatedItemAppearance.GradientAngle;
      if (this.Orientation == QCommandContainerOrientation.Vertical)
        this.PaintParams.ItemGradientAngle = QMath.RotateAngle(this.PaintParams.ItemGradientAngle, 90);
      this.PaintParams.ShadedLineColor = (Color) this.ColorScheme.MainMenuShadeLine;
      this.PaintParams.BackgroundColor1 = (Color) this.ColorScheme.MainMenuBevel1;
      this.PaintParams.BackgroundColor2 = (Color) this.ColorScheme.MainMenuBevel2;
      this.PaintParams.MoreItemsColor1 = (Color) this.ColorScheme.MainMenuMoreItemsArea1;
      this.PaintParams.MoreItemsColor2 = (Color) this.ColorScheme.MainMenuMoreItemsArea2;
      this.PaintParams.SizingGripDarkColor = (Color) this.ColorScheme.MainMenuSizingGrip;
      this.PaintParams.SizingGripLightColor = (Color) this.ColorScheme.MainMenuShade;
      this.PaintParams.TextColor = (Color) this.ColorScheme.MainMenuText;
      this.PaintParams.TextActiveColor = (Color) this.ColorScheme.MainMenuTextActive;
      this.PaintParams.TextDisabledColor = (Color) this.ColorScheme.MainMenuTextDisabled;
      this.PaintParams.SeparatorColor = (Color) this.ColorScheme.MainMenuSeparator;
      this.PaintParams.ExpandedItemBackground1Color = (Color) this.ColorScheme.MainMenuExpandedItemBackground1;
      this.PaintParams.ExpandedItemBackground2Color = (Color) this.ColorScheme.MainMenuExpandedItemBackground2;
      this.PaintParams.ExpandedItemBorderColor = (Color) this.ColorScheme.MainMenuExpandedItemBorder;
      this.PaintParams.HotItemBackground1Color = (Color) this.ColorScheme.MainMenuHotItemBackground1;
      this.PaintParams.HotItemBackground2Color = (Color) this.ColorScheme.MainMenuHotItemBackground2;
      this.PaintParams.HotItemBorderColor = (Color) this.ColorScheme.MainMenuHotItemBorder;
      this.PaintParams.PressedItemBackground1Color = (Color) this.ColorScheme.MainMenuPressedItemBackground1;
      this.PaintParams.PressedItemBackground2Color = (Color) this.ColorScheme.MainMenuPressedItemBackground2;
      this.PaintParams.PressedItemBorderColor = (Color) this.ColorScheme.MainMenuPressedItemBorder;
      this.PaintParams.CheckedItemBackground1Color = (Color) this.ColorScheme.MenuCheckedItemBackground1;
      this.PaintParams.CheckedItemBackground2Color = (Color) this.ColorScheme.MenuCheckedItemBackground2;
      this.PaintParams.CheckedItemBorderColor = (Color) this.ColorScheme.MenuCheckedItemBorder;
    }

    private void FillPaintParamsForPaint()
    {
      this.PaintParams.CustomizeAreaState = this.IsCustomizing ? QButtonState.Pressed : this.m_oCustomizeArea.State;
      this.PaintParams.SizingGripState = this.m_oSizingGripArea.State;
      this.PaintParams.Flags = QDrawToolBarFlags.None;
      this.PaintParams.Flags |= this.PaintParams.ShowSizingGrip ? QDrawToolBarFlags.DrawSizingGrip : QDrawToolBarFlags.None;
      this.PaintParams.Flags |= this.PaintParams.ShowCustomizeBar ? QDrawToolBarFlags.DrawCustomizeBar : QDrawToolBarFlags.None;
      this.PaintParams.Flags |= this.PaintParams.ShowCustomizeButton ? QDrawToolBarFlags.DrawCustomizeButton : QDrawToolBarFlags.None;
      this.PaintParams.Flags |= this.ShowMoreItemsButton ? QDrawToolBarFlags.DrawMoreItems : QDrawToolBarFlags.None;
    }

    private void CleanUpPaintParams()
    {
      if (this.PaintParams == null)
        return;
      QImageCache.IncludeInCleanup((Image) this.PaintParams.CustomizeImage);
      this.PaintParams.CustomizeImage = (Bitmap) null;
      QImageCache.IncludeInCleanup((Image) this.PaintParams.HasMoreItemsImage);
      this.PaintParams.HasMoreItemsImage = (Bitmap) null;
      if (this.PaintParams.StringFormat != null)
        this.PaintParams.StringFormat.Dispose();
      this.PaintParams.StringFormat = (StringFormat) null;
      this.PaintParams.MdiButtons = (QMenuMdiButtons) null;
      this.PaintParams.ActiveMdiIcon = (Icon) null;
    }

    void IQToolBar.LayoutToolBar(
      Rectangle rectangle,
      QCommandContainerOrientation orientation,
      QToolBarLayoutFlags layoutFlags)
    {
      this.LayoutMenu(rectangle, orientation, layoutFlags);
    }

    internal static bool PassSCKeyMenu(Message m, Control sendToControl)
    {
      if (m.Msg != 274 || (m.WParam.ToInt32() & 65520) != 61696)
        return false;
      if (sendToControl != null && sendToControl.IsHandleCreated && !sendToControl.IsDisposed)
        NativeMethods.PostMessage(sendToControl.Handle, m.Msg, (uint) (int) m.WParam, (uint) (int) m.LParam);
      return true;
    }

    private void LayoutMenu(
      Rectangle rectangle,
      QCommandContainerOrientation orientation,
      QToolBarLayoutFlags layoutFlags)
    {
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null || this.PerformingLayout)
        return;
      this.PutPerformingLayout(true);
      this.SetOrientation(orientation, false);
      this.FillPaintParams();
      bool flag1 = false;
      if (this.ToolBarHost != null)
        flag1 = !this.ToolBarHost.IsChangingToolBars;
      else if (this.IsFloating)
        flag1 = !this.ToolBarForm.IsChangingToolBars;
      bool flag2 = (layoutFlags & QToolBarLayoutFlags.DoNotSetBounds) != QToolBarLayoutFlags.DoNotSetBounds;
      this.PaintParams.LayoutFlags = layoutFlags;
      Rectangle rectangle1;
      if (this.Orientation == QCommandContainerOrientation.Horizontal || this.Orientation == QCommandContainerOrientation.None)
      {
        this.Painter.LayoutHorizontal(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(new Rectangle(0, 0, rectangle.Width, rectangle.Height), false, true), (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (Control) this, (QCommandCollection) this.MenuItems);
        rectangle1 = new Rectangle(rectangle.Left, rectangle.Top, this.PaintParams.ProposedSize.Width + this.PaintParams.ToolBarPadding.Horizontal, this.PaintParams.ProposedSize.Height + this.PaintParams.ToolBarPadding.Vertical);
      }
      else
      {
        this.Painter.LayoutVertical(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(new Rectangle(0, 0, rectangle.Width, rectangle.Height), false, false), (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (Control) this, (QCommandCollection) this.MenuItems);
        rectangle1 = new Rectangle(rectangle.Left, rectangle.Top, this.PaintParams.ProposedSize.Width + this.PaintParams.ToolBarPadding.Vertical, this.PaintParams.ProposedSize.Height + this.PaintParams.ToolBarPadding.Horizontal);
      }
      this.m_oSizingGripArea.Bounds = this.PaintParams.SizingGripBounds;
      this.m_oCustomizeArea.Bounds = this.PaintParams.CustomizeBounds;
      if (this.MdiButtonsVisible)
      {
        this.m_oMdiButtons.SetBounds(this.PaintParams.MdiButtonsBounds);
      }
      else
      {
        this.m_oMdiButtons.EmptyCachedObjects();
        this.m_oMdiButtons.SetBounds(Rectangle.Empty);
      }
      this.PutLastShownCommand(this.PaintParams.LastVisibleItem);
      if (flag2 && !flag1)
      {
        BoundsSpecified specified = BoundsSpecified.All;
        if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
          specified &= ~BoundsSpecified.Height;
        if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
          specified &= ~BoundsSpecified.Width;
        this.SetBounds(rectangle1.Left, rectangle1.Top, rectangle1.Width, rectangle1.Height, specified);
      }
      this.m_oAction.ProposedBounds = rectangle1;
      this.PutPerformingLayout(false);
      if (this.ToolBarHost != null && flag1 && flag2)
      {
        this.ToolBarHost.NotifyToolBarSizeChanged((IQToolBar) this);
      }
      else
      {
        if (!this.IsFloating || !flag1)
          return;
        this.ToolBarForm.NotifyToolBarSizeChanged((IQToolBar) this);
      }
    }

    protected override void DrawMenuItem(
      QMenuItem menuItem,
      StringFormat format,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
      this.Painter.DrawItem((QCommand) menuItem, (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (QCommandContainer) this, flags, graphics);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.IsMoving)
      {
        Point screen = this.PointToScreen(new Point(e.X, e.Y));
        if (this.m_oLastMovePointHandled == screen)
        {
          this.DefaultOnMouseMove(e);
        }
        else
        {
          this.m_oLastMovePointHandled = screen;
          this.MoveToolBar(screen);
          this.DefaultOnMouseMove(e);
        }
      }
      else
      {
        this.m_oSizingGripArea.HandleMouseMove(e);
        this.m_oCustomizeArea.HandleMouseMove(e);
        QMenuMdiButton buttonAtLocation = this.m_oMdiButtons.GetButtonAtLocation(e.X, e.Y);
        if (buttonAtLocation != null)
        {
          if (buttonAtLocation.ButtonState == QButtonState.Normal)
            this.m_oMdiButtons.SetButtonToState(buttonAtLocation, QButtonState.Hot);
        }
        else
          this.m_oMdiButtons.SetAllButtonStatesTo(QButtonState.Normal, true);
        base.OnMouseMove(e);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.m_oCustomizeArea.HandleMouseDown(e) && this.IsCustomizing)
        this.EndCustomizing();
      if (this.m_oSizingGripArea.HandleMouseDown(e))
        return;
      QMenuMdiButton buttonAtLocation = this.m_oMdiButtons.GetButtonAtLocation(e.X, e.Y);
      if (buttonAtLocation == null)
        return;
      this.m_oMdiButtons.SetButtonToState(buttonAtLocation, QButtonState.Pressed);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      QMenuMdiButton buttonAtLocation = this.m_oMdiButtons.GetButtonAtLocation(e.X, e.Y);
      if (buttonAtLocation != null)
      {
        if (buttonAtLocation.IsCloseButton)
        {
          if (this.m_oMainFormOverride.ActiveMdiChild != null)
            this.m_oMainFormOverride.ActiveMdiChild.Close();
        }
        else if (buttonAtLocation.IsRestoreButton)
        {
          if (this.m_oMainFormOverride.ActiveMdiChild != null)
            this.m_oMainFormOverride.ActiveMdiChild.WindowState = FormWindowState.Normal;
        }
        else if (buttonAtLocation.IsMimimizeButton && this.m_oMainFormOverride.ActiveMdiChild != null)
          this.m_oMainFormOverride.ActiveMdiChild.WindowState = FormWindowState.Minimized;
        this.m_oMdiButtons.SetButtonToState(buttonAtLocation, QButtonState.Hot);
      }
      this.m_oSizingGripArea.HandleMouseUp(e);
      this.m_oCustomizeArea.HandleMouseUp(e);
      if (!this.m_bIsMoving)
        return;
      this.EndMoving();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.m_oCustomizeArea.HandleMouseLeave((MouseEventArgs) null);
      this.m_oSizingGripArea.HandleMouseLeave((MouseEventArgs) null);
      this.m_oMdiButtons.SetAllButtonStatesTo(QButtonState.Normal, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      this.FillPaintParamsForPaint();
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      base.OnPaint(e);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      if (this.Orientation == QCommandContainerOrientation.Horizontal || this.Orientation == QCommandContainerOrientation.None)
        this.Painter.DrawControlBackgroundHorizontal(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(this.ClientRectangle, false, true), (QAppearanceBase) this.BevelAppearance, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) this.Configuration, (Control) this, pevent.Graphics);
      else if (this.Orientation == QCommandContainerOrientation.Vertical)
        this.Painter.DrawControlBackgroundVertical(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(this.ClientRectangle, false, false), (QAppearanceBase) this.BevelAppearance, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) this.Configuration, (Control) this, pevent.Graphics);
      if (this.ActiveMdiIconVisible)
        QControlPaint.DrawIcon(this.m_oMainFormOverride.ActiveMdiChildIcon, this.PaintParams.ActiveMdiIconBounds, pevent.Graphics);
      if (!this.MdiButtonsVisible)
        return;
      this.m_oMdiButtons.Draw(pevent.Graphics);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.PerformingLayout || this.ToolBarHost == null)
        base.SetBoundsCore(x, y, width, height, specified);
      else
        this.RequestedPosition = this.Horizontal ? Math.Max(x, 0) : Math.Max(y, 0);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      Rectangle rectangle = this.Bounds;
      QToolBarLayoutFlags layoutFlags = QToolBarLayoutFlags.None;
      if (this.ToolBarHost != null && this.Stretched)
        rectangle = this.Horizontal ? new Rectangle(this.ToolBarHost.ToolBarsStartPosition, this.Top, this.ToolBarHost.ToolBarsAvailableSpace, this.Height) : new Rectangle(this.Left, this.ToolBarHost.ToolBarsStartPosition, this.Width, this.ToolBarHost.ToolBarsAvailableSpace);
      else if (this.ToolBarHost == null)
        layoutFlags |= QToolBarLayoutFlags.FixedWidth;
      this.LayoutMenu(rectangle, this.Orientation, layoutFlags);
    }

    protected override void OnParentChanged(EventArgs e)
    {
      if (this.m_oParentControl != null)
        this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oParentControlSizeChanged);
      if (this.Parent != null)
      {
        this.m_oParentControl = this.Parent;
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oParentControlSizeChanged, (object) this.m_oParentControl, "SizeChanged"));
      }
      base.OnParentChanged(e);
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing)
          Application.RemoveMessageFilter((IMessageFilter) this.m_oWeakMessageFilter);
      }
      catch (InvalidOperationException ex)
      {
      }
      if (disposing)
      {
        if (this.m_oSizingGripArea != null)
          this.m_oSizingGripArea.ButtonStateChanged -= new QButtonAreaEventHandler(this.SizingGripArea_ButtonStateChanged);
        this.m_oComponents.Dispose();
      }
      base.Dispose(disposing);
    }

    private void ParentControl_SizeChanged(object sender, EventArgs e)
    {
      if (this.m_oParentControl == null || this.Dock != DockStyle.Right)
        return;
      this.m_oParentControl.PerformLayout();
    }

    private void BevelAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Refresh();
    }

    private void SizingGripArea_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      if (e.FromState == QButtonState.Pressed || this.IsMoving)
        this.EndMoving();
      switch (e.ToState)
      {
        case QButtonState.Normal:
          this.Cursor = this.m_oCachedCursor;
          break;
        case QButtonState.Hot:
          if (this.Cursor != Cursors.SizeAll)
            this.m_oCachedCursor = this.Cursor;
          this.Cursor = Cursors.SizeAll;
          break;
        case QButtonState.Pressed:
          this.StartMoving(new Point(e.X, e.Y));
          break;
      }
    }

    private void CustomizeArea_ButtonStateChanged(object sender, QButtonAreaEventArgs e)
    {
      switch (e.ToState)
      {
        case QButtonState.Normal:
        case QButtonState.Hot:
          this.ToolTipText = this.Configuration.CustomizeItemTooltip;
          this.Refresh();
          break;
        case QButtonState.Pressed:
          if (!this.IsCustomizing)
          {
            this.StartCustomizing();
            break;
          }
          this.EndCustomizing();
          break;
      }
    }

    protected override void OnConfigurationChanged(EventArgs e)
    {
      this.m_oMainFormOverride.MdiChildIconSize = this.Configuration.IconSize;
      if ((this.BehaviorFlags & QMenuItemContainerBehaviorFlags.SimplePersonalizing) == QMenuItemContainerBehaviorFlags.SimplePersonalizing != this.Configuration.SimplePersonalizing)
        this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.SimplePersonalizing, this.Configuration.SimplePersonalizing);
      base.OnConfigurationChanged(e);
    }

    void IQMainMenu.HandleActiveMdiChildChanged()
    {
      this.PerformLayout();
      this.Invalidate();
    }

    void IQMainMenu.HandleMdiChildWindowStateChanged(int sizeParameter)
    {
      this.PerformLayout();
      this.Invalidate();
    }

    void IQMainMenu.HandleMenukeyDown(ref Message m)
    {
      if (!(m.LParam == IntPtr.Zero) || this.HasSimulatedFocus)
        return;
      this.SimulateGotFocus();
      this.SelectNextItem();
    }

    void IQMainMenu.HandleDeactivate(IntPtr activatingWindow)
    {
      if (this.ContainsOrIsContainerWithHandle(activatingWindow) || !this.HasSimulatedFocus)
        return;
      this.ResetState();
    }
  }
}
