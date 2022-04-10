// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QToolBar), "Resources.ControlImages.QToolBar.bmp")]
  [Designer(typeof (QToolBarDesigner), typeof (IDesigner))]
  [ToolboxItem(true)]
  [Designer(typeof (QToolBarDesigner), typeof (IDesigner))]
  public class QToolBar : QMenuItemContainer, IMessageFilter, IQToolBar
  {
    private QWeakMessageFilter m_oWeakMessageFilter;
    private Cursor m_oCachedCursor;
    private QToolBarPainter m_oPainter;
    private QToolBarPaintParams m_oPaintParams;
    private QToolBarBevelAppearance m_oBevelAppearance;
    private QButtonArea m_oSizingGripArea;
    private QButtonArea m_oCustomizeArea;
    private QToolBarRow m_oParentToolBarRow;
    private QToolBarRow m_oOriginalToolBarRow;
    private QToolBarForm m_oToolBarForm;
    private QToolBarAction m_oAction;
    private bool m_bIsMoving;
    private bool m_bAltKeyHandled;
    private Point m_oLastMovePointHandled = Point.Empty;
    private int m_iRowIndex = -1;
    private int m_iToolBarPositionIndex = -1;

    public QToolBar()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetQControlStyles(QControlStyles.NeverCopyBitsOnBoundsChange, true);
      this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.HideExpandedItemOnClick | QMenuItemContainerBehaviorFlags.StopAutoExpandWhenOverHotItem | QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension | QMenuItemContainerBehaviorFlags.CanSimulateFocus, true);
      this.m_oAction = new QToolBarAction((IQToolBar) this);
      this.m_oPainter = new QToolBarPainter();
      this.m_oPaintParams = new QToolBarPaintParams();
      this.m_oBevelAppearance = new QToolBarBevelAppearance();
      this.m_oBevelAppearance.AppearanceChanged += new EventHandler(this.BevelAppearance_AppearanceChanged);
      this.m_oSizingGripArea = new QButtonArea(MouseButtons.Left);
      this.m_oSizingGripArea.ButtonStateChanged += new QButtonAreaEventHandler(this.SizingGripArea_ButtonStateChanged);
      this.m_oCustomizeArea = new QButtonArea(MouseButtons.Left);
      this.m_oCustomizeArea.ButtonStateChanged += new QButtonAreaEventHandler(this.CustomizeArea_ButtonStateChanged);
      this.SetChildMenuConfigurationBase(new QFloatingMenuConfiguration(), false);
      this.SetConfigurationBase((QCommandConfiguration) new QToolBarConfiguration(), false);
      this.m_oWeakMessageFilter = new QWeakMessageFilter((object) this);
      Application.AddMessageFilter((IMessageFilter) this.m_oWeakMessageFilter);
      this.ResumeLayout(false);
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QToolBarAppearance();

    protected override QToolTipConfiguration CreateToolTipConfigurationInstance() => (QToolTipConfiguration) new QStatusBarToolTipConfiguration();

    protected override void OnConfigurationChanged(EventArgs e)
    {
      if (this.Configuration.ExpandOnItemClick)
      {
        if ((this.BehaviorFlags & QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension) == QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension)
          this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension, false);
      }
      else if ((this.BehaviorFlags & QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension) != QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension)
        this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension, true);
      base.OnConfigurationChanged(e);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the QToolBarAppearance")]
    [Category("QAppearance")]
    public virtual QToolBarAppearance Appearance => (QToolBarAppearance) base.Appearance;

    [Description("Gets or sets the QStatusBarToolTipConfiguration")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QStatusBarToolTipConfiguration ToolTipConfiguration
    {
      get => (QStatusBarToolTipConfiguration) base.ToolTipConfiguration;
      set => this.ToolTipConfiguration = value;
    }

    [Localizable(true)]
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
      set => base.Anchor = this.ToolBarHost == null || (value & AnchorStyles.Bottom) != AnchorStyles.Bottom && (value & AnchorStyles.Bottom) != AnchorStyles.Right ? value : throw new InvalidOperationException(QResources.GetException("QToolBar_Anchor_Invalid"));
    }

    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
        if (value == DockStyle.Fill)
          throw new InvalidOperationException(QResources.GetException("QToolBar_Dock_NotFill"));
        base.Dock = value == DockStyle.None || this.ToolBarHost == null ? value : throw new InvalidOperationException(QResources.GetException("QToolBar_Dock_NoDockWhenHosted"));
        if (value == DockStyle.Left || value == DockStyle.Right)
          this.SetOrientation(QCommandContainerOrientation.Vertical, true);
        else
          this.SetOrientation(QCommandContainerOrientation.Horizontal, true);
      }
    }

    [Description("Gets or sets the requested position on a ToolBarHost. This is the top position when orientation is vertical, else the left position.")]
    [Category("QBehavior")]
    [DefaultValue(0)]
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

    [Category("QBehavior")]
    [DefaultValue(-1)]
    [Description("Gets or sets the index of the row where the ToolBar must be placed on a ToolBarHost.")]
    public int RowIndex
    {
      get => this.m_oParentToolBarRow == null || this.ToolBarHost == null ? this.m_iRowIndex : this.m_oParentToolBarRow.PositionIndex;
      set
      {
        if (this.ToolBarHost != null)
          this.DockToolBar(this.ToolBarHost, value, this.ToolBarPositionIndex, this.m_oAction.UserRequestedPosition);
        else
          this.m_iRowIndex = value;
      }
    }

    [DefaultValue(-1)]
    [Category("QBehavior")]
    [Description("Gets or sets the index where the ToolBar must be placed on a ToolBarHost.")]
    public int ToolBarIndex
    {
      get => this.m_iToolBarPositionIndex;
      set
      {
        if (this.ToolBarHost != null)
          this.DockToolBar(this.ToolBarHost, this.RowIndex, value, this.m_oAction.UserRequestedPosition);
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

    public void ResetBevelAppearance() => this.m_oBevelAppearance.SetToDefaultValues();

    public bool ShouldSerializeBevelAppearance() => !this.m_oBevelAppearance.IsSetToDefaultValues();

    [Description("Gets the appearance for the bevel")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QToolBarBevelAppearance BevelAppearance => this.m_oBevelAppearance;

    public void DockToolBar(QToolBarHost host, int rowIndex, int toolBarIndex) => this.m_oAction.DockToolBar(host, rowIndex, toolBarIndex, 0);

    public void DockToolBar(
      QToolBarHost host,
      int rowIndex,
      int toolBarIndex,
      int requestedPosition)
    {
      this.m_oAction.DockToolBar(host, rowIndex, toolBarIndex, requestedPosition);
    }

    public void FloatToolBar(Point screenPoint) => this.m_oAction.FloatToolBar(screenPoint);

    protected override string BackColorPropertyName => "ToolBarBackground1";

    protected override string BackColor2PropertyName => "ToolBarBackground2";

    protected override string BorderColorPropertyName => "ToolBarBorder";

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    [Description("Gets or sets the Configuration")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QToolBarConfiguration Configuration
    {
      get => (QToolBarConfiguration) this.ConfigurationBase;
      set => this.ConfigurationBase = (QCommandConfiguration) value;
    }

    public void ResetChildMenuConfiguration() => this.ChildMenuConfiguration.SetToDefaultValues();

    public bool ShouldSerializeChildMenuConfiguration() => !this.ChildMenuConfiguration.IsSetToDefaultValues();

    [Category("QAppearance")]
    [Description("Gets or sets the configuration for menus opened from this QToolBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QFloatingMenuConfiguration ChildMenuConfiguration
    {
      get => this.ChildMenuConfigurationBase;
      set => this.SetChildMenuConfigurationBase(value, true);
    }

    [Editor(typeof (QToolItemCollectionEditor), typeof (UITypeEditor))]
    [Description("Contains the collection of ToolItems  of this QToolBar")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QMenuItemCollection ToolItems => this.Items;

    [Browsable(false)]
    public bool IsFloating => this.m_oToolBarForm != null && this.Parent == this.m_oToolBarForm;

    private QToolBarPainter Painter => this.m_oPainter;

    internal bool Horizontal => this.Orientation == QCommandContainerOrientation.Horizontal || this.Orientation == QCommandContainerOrientation.None;

    bool IQToolBar.Horizontal => this.Horizontal;

    bool IQToolBar.Stretched => this.Stretched;

    internal bool Stretched => this.Configuration.Stretched && !(this.Parent is QToolBarForm) || this.Dock != DockStyle.None;

    internal bool ShowCanMove => this.Configuration.CanMove && this.Parent is QToolBarHost;

    internal bool ShowCustomizeButton => this.Configuration.CanCustomize && !(this.Parent is QToolBarForm);

    internal bool ShowCustomizeBar => (this.Configuration.ShowCustomizeBar || this.Configuration.CanCustomize || this.Configuration.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit) && !(this.Parent is QToolBarForm);

    bool IQToolBar.ShowMoreItemsButton => this.ShowMoreItemsButton;

    internal bool ShowMoreItemsButton => this.Configuration.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit && this.LastShownCommand < this.ToolItems.Count - 1;

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

    QToolBarAction IQToolBar.Action => this.m_oAction;

    Rectangle IQToolBar.CustomizeItemBounds => this.IsFloating ? this.ToolBarForm.CustomizeItemBounds : this.RectangleToScreen(this.m_oCustomizeArea.Bounds);

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

    Rectangle IQToolBar.ProposedBounds
    {
      get => this.m_oAction.ProposedBounds;
      set => this.m_oAction.ProposedBounds = value;
    }

    Rectangle IQToolBar.Bounds => this.Bounds;

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

    bool IQToolBar.IsVisible => this.Visible;

    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      if (!this.IsFloating)
        return;
      this.m_oToolBarForm.Visible = value;
    }

    public override bool ContainsOrIsContainerWithHandle(IntPtr handle)
    {
      if (base.ContainsOrIsContainerWithHandle(handle))
        return true;
      return this.IsCustomizing && this.m_oAction.CustomizeMenu.ContainsOrIsContainerWithHandle(handle);
    }

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public bool HandleKeyDown(Keys keys) => this.HandleKeyDown(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    public bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      QMenuItem itemWithShortcut = this.GetAccessibleMenuItemWithShortcut(Control.ModifierKeys | keys);
      if (itemWithShortcut != null && this.ShouldHandleShortcutsForControl(destinationControl, this.Configuration.ShortcutScope))
      {
        this.ActivateItem(itemWithShortcut, false, true, false, QMenuItemActivationType.Shortcut);
        return itemWithShortcut.SuppressShortcutToSystem;
      }
      if (this.ShouldHandleKeyMessagesForControl(destinationControl))
      {
        if (Control.ModifierKeys == Keys.Alt)
        {
          this.HotkeyVisible = true;
          if (this.ExpandedItem == null && this.HandlePossibleHotKey(keys))
          {
            if (this.HotItem != null || this.ExpandedItem != null)
              this.SimulateGotFocus();
            this.m_bAltKeyHandled = true;
            return true;
          }
        }
        else
        {
          if (this.HasSimulatedFocus)
          {
            bool flag = false;
            if (this.ExpandedItem != null)
              flag = this.ExpandedItem.ChildMenu.HandleKeyDown(keys, destinationControl, message);
            else if (this.HandlePossibleHotKey(keys))
              return true;
            if (!flag)
              this.HandleDefaultNavigationKeys(keys, true);
            return true;
          }
          if (this.IsCustomizing)
          {
            if (!this.m_oAction.CustomizeMenu.HandleKeyDown(keys, destinationControl, message) && keys == Keys.Escape)
              this.m_oAction.CustomizeMenu.CloseAllMenus();
            return true;
          }
        }
      }
      return false;
    }

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public virtual bool HandleKeyUp(Keys keys) => this.HandleKeyUp(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    public virtual bool HandleKeyUp(Keys keys, Control destinationControl, Message message)
    {
      if (keys == Keys.Menu)
      {
        if (!this.m_bAltKeyHandled)
          this.ResetState();
        this.HotkeyVisible = false;
        this.m_bAltKeyHandled = false;
      }
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

    internal QToolBarHost ToolBarHost => this.Parent == null || !(this.Parent is QToolBarHost) ? (QToolBarHost) null : (QToolBarHost) this.Parent;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ResetState()
    {
      base.ResetState();
      if (!this.IsCustomizing)
        return;
      this.EndCustomizing();
      if (!this.IsFloating)
        return;
      this.ToolBarForm.RefreshNoClientArea();
    }

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

    internal QToolBarPaintParams PaintParams => this.m_oPaintParams;

    QToolBarPaintParams IQToolBar.PaintParams => this.m_oPaintParams;

    QToolBarConfiguration IQToolBar.ToolBarConfiguration => this.Configuration;

    QColorScheme IQToolBar.ColorScheme => this.ColorScheme;

    [Browsable(false)]
    internal bool IsMoving => this.m_bIsMoving;

    bool IQToolBar.IsCustomizing => this.IsCustomizing;

    internal bool IsCustomizing => this.m_oAction.CustomizeMenuVisible;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ExcludeBoundsFromRegion(Region region, Control useCoordinateSystem)
    {
      if (this.ExpandedItem == null)
        return;
      region.Exclude(useCoordinateSystem.RectangleToClient(this.RectangleToScreen(this.ExpandedItem.Bounds)));
    }

    internal override Color ParentMenuIntersectColor
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => (Color) this.ColorScheme.ToolBarExpandedItemBackground2;
    }

    void IQToolBar.StartCustomizing() => this.StartCustomizing();

    void IQToolBar.EndCustomizing() => this.EndCustomizing();

    private void StartCustomizing()
    {
      this.Refresh();
      this.m_oAction.ShowCustomizeMenu();
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

    private StringFormat CreateStringFormat()
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

    private void FillPaintParams()
    {
      this.CleanUpPaintParams();
      if (this.Configuration == null || this.ColorScheme == null || this.Font == null)
        return;
      this.PaintParams.ShadeImages = this.Configuration.ToolBarStyle == QToolBarStyle.Beveled;
      this.PaintParams.ShadeSeparator = this.Configuration.ToolBarStyle == QToolBarStyle.Beveled;
      this.PaintParams.ShadeSizingGrip = this.Configuration.ToolBarStyle == QToolBarStyle.Beveled;
      this.PaintParams.LayoutType = this.Configuration.LayoutType;
      this.PaintParams.ShowCustomizeBar = this.ShowCustomizeBar;
      this.PaintParams.ShowCustomizeButton = this.ShowCustomizeButton;
      this.PaintParams.ShowSizingGrip = this.ShowCanMove;
      this.PaintParams.Stretched = this.Stretched;
      this.PaintParams.ExpandOnItemClick = this.Configuration.ExpandOnItemClick;
      this.PaintParams.ToolBarStyle = this.Configuration.ToolBarStyle;
      this.PaintParams.SizingGripPadding = this.Configuration.SizingGripPadding;
      this.PaintParams.CornerSize = this.PaintParams.ToolBarStyle == QToolBarStyle.Beveled ? this.Configuration.RoundedBevelCornerSize : 0;
      this.PaintParams.RoundedBevelCornerSize = this.Configuration.RoundedBevelCornerSize;
      this.PaintParams.HasMoreItemsAreaWidth = this.Configuration.HasMoreItemsAreaWidth;
      this.PaintParams.SizingGripWidth = this.Configuration.SizingGripWidth;
      this.PaintParams.ToolBarPadding = this.Configuration.ToolBarPadding;
      this.PaintParams.StringFormat = this.CreateStringFormat();
      this.PaintParams.ItemGradientAngle = this.Configuration.ActivatedItemAppearance.GradientAngle;
      if (this.Orientation == QCommandContainerOrientation.Vertical)
        this.PaintParams.ItemGradientAngle = QMath.RotateAngle(this.PaintParams.ItemGradientAngle, 90);
      this.PaintParams.ToolBarStyle = this.Configuration.ToolBarStyle;
      this.PaintParams.CornerSize = this.PaintParams.ToolBarStyle == QToolBarStyle.Beveled ? this.Configuration.RoundedBevelCornerSize : 0;
      this.PaintParams.SizingGripStyle = this.Configuration.SizingGripStyle;
      this.PaintParams.SizingGripSize = this.Configuration.SizingGripWidth;
      this.PaintParams.ItemsSpacing = this.Configuration.ItemsSpacing;
      this.PaintParams.Font = this.Font;
      if (this.PaintParams.ShowCustomizeButton)
      {
        this.PaintParams.CustomizeImage = (Bitmap) QControlPaint.CreateColorizedImage(this.Configuration.UsedCustomizeToolBarMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.ColorScheme.ToolBarText);
        if (this.Orientation == QCommandContainerOrientation.Vertical)
          this.PaintParams.CustomizeImage = QControlPaint.RotateFlipImage((Image) this.PaintParams.CustomizeImage, RotateFlipType.Rotate270FlipNone);
        if (this.PaintParams.ShadeImages)
          this.PaintParams.CustomizeImage = QControlPaint.AddShadeToImage(this.PaintParams.CustomizeImage, (Color) this.ColorScheme.ToolBarShade, 1, 1);
        QImageCache.ExcludeFromCleanup((Image) this.PaintParams.CustomizeImage);
      }
      if (this.PaintParams.LastVisibleItem < this.ToolItems.Count - 1)
      {
        this.PaintParams.HasMoreItemsImage = (Bitmap) QControlPaint.CreateColorizedImage(this.Configuration.UsedHasMoreItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.ColorScheme.ToolBarText);
        if (this.Orientation == QCommandContainerOrientation.Vertical)
          this.PaintParams.HasMoreItemsImage = QControlPaint.RotateFlipImage((Image) this.PaintParams.HasMoreItemsImage, RotateFlipType.Rotate90FlipNone);
        if (this.PaintParams.ShadeImages)
          this.PaintParams.HasMoreItemsImage = QControlPaint.AddShadeToImage(this.PaintParams.HasMoreItemsImage, (Color) this.ColorScheme.ToolBarShade, 1, 1);
        QImageCache.ExcludeFromCleanup((Image) this.PaintParams.HasMoreItemsImage);
      }
      this.PaintParams.ShadedLineColor = (Color) this.ColorScheme.ToolBarShadeLine;
      this.PaintParams.BackgroundColor1 = (Color) this.ColorScheme.ToolBarBevel1;
      this.PaintParams.BackgroundColor2 = (Color) this.ColorScheme.ToolBarBevel2;
      this.PaintParams.MoreItemsColor1 = (Color) this.ColorScheme.ToolBarMoreItemsArea1;
      this.PaintParams.MoreItemsColor2 = (Color) this.ColorScheme.ToolBarMoreItemsArea2;
      this.PaintParams.HasMoreItemsAreaWidth = this.Configuration.HasMoreItemsAreaWidth;
      this.PaintParams.SizingGripDarkColor = (Color) this.ColorScheme.ToolBarSizingGrip;
      this.PaintParams.SizingGripLightColor = (Color) this.ColorScheme.ToolBarShade;
      this.PaintParams.TextColor = (Color) this.ColorScheme.ToolBarText;
      this.PaintParams.TextActiveColor = (Color) this.ColorScheme.ToolBarTextActive;
      this.PaintParams.TextDisabledColor = (Color) this.ColorScheme.ToolBarTextDisabled;
      this.PaintParams.SeparatorColor = (Color) this.ColorScheme.ToolBarSeparator;
      this.PaintParams.ExpandedItemBackground1Color = (Color) this.ColorScheme.ToolBarExpandedItemBackground1;
      this.PaintParams.ExpandedItemBackground2Color = (Color) this.ColorScheme.ToolBarExpandedItemBackground2;
      this.PaintParams.ExpandedItemBorderColor = (Color) this.ColorScheme.ToolBarExpandedItemBorder;
      this.PaintParams.HotItemBackground1Color = (Color) this.ColorScheme.ToolBarHotItemBackground1;
      this.PaintParams.HotItemBackground2Color = (Color) this.ColorScheme.ToolBarHotItemBackground2;
      this.PaintParams.HotItemBorderColor = (Color) this.ColorScheme.ToolBarHotItemBorder;
      this.PaintParams.PressedItemBackground1Color = (Color) this.ColorScheme.ToolBarPressedItemBackground1;
      this.PaintParams.PressedItemBackground2Color = (Color) this.ColorScheme.ToolBarPressedItemBackground2;
      this.PaintParams.PressedItemBorderColor = (Color) this.ColorScheme.ToolBarPressedItemBorder;
      this.PaintParams.CheckedItemBackground1Color = (Color) this.ColorScheme.ToolBarCheckedItemBackground1;
      this.PaintParams.CheckedItemBackground2Color = (Color) this.ColorScheme.ToolBarCheckedItemBackground2;
      this.PaintParams.CheckedItemBorderColor = (Color) this.ColorScheme.ToolBarCheckedItemBorder;
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
    }

    void IQToolBar.LayoutToolBar(
      Rectangle rectangle,
      QCommandContainerOrientation orientation,
      QToolBarLayoutFlags layoutFlags)
    {
      this.LayoutToolBar(rectangle, orientation, layoutFlags);
    }

    private void LayoutToolBar(
      Rectangle rectangle,
      QCommandContainerOrientation orientation,
      QToolBarLayoutFlags layoutFlags)
    {
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null || this.IsDisposed || this.PerformingLayout)
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
        this.Painter.LayoutHorizontal(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(new Rectangle(0, 0, rectangle.Width, rectangle.Height), false, true), (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (Control) this, (QCommandCollection) this.ToolItems);
        rectangle1 = new Rectangle(rectangle.Left, rectangle.Top, this.PaintParams.ProposedSize.Width + this.PaintParams.ToolBarPadding.Horizontal, this.PaintParams.ProposedSize.Height + this.PaintParams.ToolBarPadding.Vertical);
      }
      else
      {
        this.Painter.LayoutVertical(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(new Rectangle(0, 0, rectangle.Width, rectangle.Height), false, false), (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (Control) this, (QCommandCollection) this.ToolItems);
        rectangle1 = new Rectangle(rectangle.Left, rectangle.Top, this.PaintParams.ProposedSize.Width + this.PaintParams.ToolBarPadding.Vertical, this.PaintParams.ProposedSize.Height + this.PaintParams.ToolBarPadding.Horizontal);
      }
      this.m_oSizingGripArea.Bounds = this.PaintParams.SizingGripBounds;
      this.m_oCustomizeArea.Bounds = this.PaintParams.CustomizeBounds;
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

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      this.PerformLayout();
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
      this.LayoutToolBar(rectangle, this.Orientation, layoutFlags);
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
        base.OnMouseMove(e);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.m_oCustomizeArea.HandleMouseDown(e) && this.IsCustomizing)
        this.EndCustomizing();
      this.m_oSizingGripArea.HandleMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.m_oSizingGripArea.HandleMouseUp(e);
      this.m_oCustomizeArea.HandleMouseUp(e);
      if (!this.m_bIsMoving)
        return;
      this.EndMoving();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.m_oSizingGripArea.HandleMouseLeave((MouseEventArgs) null))
        return;
      this.m_oCustomizeArea.HandleMouseLeave((MouseEventArgs) null);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      Region clip = e.Graphics.Clip;
      e.Graphics.Clip = new Region(this.PaintParams.ItemsBounds);
      e.Graphics.Clip.Intersect(clip);
      for (int firstShownCommand = this.FirstShownCommand; firstShownCommand <= this.LastShownCommand; ++firstShownCommand)
      {
        if (this.ToolItems[firstShownCommand].IsVisible)
        {
          QCommandPaintOptions flags = QCommandPaintOptions.None;
          if (this.ToolItems[firstShownCommand] == this.ExpandedItem)
            flags |= QCommandPaintOptions.Expanded;
          if (this.ToolItems[firstShownCommand] == this.MouseDownItem)
            flags |= QCommandPaintOptions.Pressed;
          if (this.ToolItems[firstShownCommand] == this.HotItem)
            flags |= QCommandPaintOptions.Hot;
          this.Painter.DrawItem((QCommand) this.ToolItems[firstShownCommand], (QCommandConfiguration) this.Configuration, (QCommandPaintParams) this.PaintParams, (QCommandContainer) this, flags, e.Graphics);
          this.RaisePaintMenuItem(new QPaintMenuItemEventArgs(this.ToolItems[firstShownCommand], flags, this.PaintParams.StringFormat, e.Graphics));
        }
      }
      e.Graphics.Clip = clip;
      this.PaintAdornments(e.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      if (this.PaintParams == null || this.Configuration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      if (this.Orientation == QCommandContainerOrientation.Horizontal || this.Orientation == QCommandContainerOrientation.None)
      {
        this.Painter.DrawControlBackgroundHorizontal(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(this.ClientRectangle, false, true), (QAppearanceBase) this.BevelAppearance, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) this.Configuration, (Control) this, pevent.Graphics);
      }
      else
      {
        if (this.Orientation != QCommandContainerOrientation.Vertical)
          return;
        this.Painter.DrawControlBackgroundVertical(this.PaintParams.ToolBarPadding.InflateRectangleWithPadding(this.ClientRectangle, false, false), (QAppearanceBase) this.BevelAppearance, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) this.Configuration, (Control) this, pevent.Graphics);
      }
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
      base.Dispose(disposing);
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

    private void BevelAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Refresh();
    }
  }
}
