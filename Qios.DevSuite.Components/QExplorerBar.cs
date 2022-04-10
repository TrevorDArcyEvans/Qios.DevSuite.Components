// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QExplorerBar), "Resources.ControlImages.QExplorerBar.bmp")]
  [Designer(typeof (QExplorerBarDesigner), typeof (IDesigner))]
  [ToolboxItem(true)]
  public class QExplorerBar : QMenuItemContainer, IMessageFilter
  {
    private const int AnimationTimerId = 18;
    private QWeakMessageFilter m_oWeakMessageFilter;
    private bool m_bInitialized;
    private bool m_bSuspendPaint;
    private QExplorerItem m_oAutoScrollItem;
    private QExplorerItem m_oCurrentDepersonalizeItem;
    private QMenuItem m_oFocusedMenuItem;
    private bool m_bAltKeyHandled;
    private Size m_oLastScrollSize;
    private Size m_oLastCalculatedSize;
    private QScrollBarExtension m_oScrollBarExtension;
    private int m_iAnimationTimerInterval = 15;
    private bool m_bTimerRunning;
    private QExplorerBarPainter m_oPainter;
    private QExplorerBarPaintParams m_oPaintParams;
    private QExplorerBarConfiguration m_oConfigurationBase;
    private EventHandler m_oConfigurationChangedEventHander;
    private QExplorerBarItemConfiguration m_oItemConfigurationBase;
    private EventHandler m_oItemConfigurationChangedEventHander;
    private QExplorerBarGroupItemConfiguration m_oGroupItemConfigurationBase;
    private EventHandler m_oGroupItemConfigurationChangedEventHander;
    private QWeakDelegate m_oCalculatedSizeChangedDelegate;

    public QExplorerBar()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Selectable, true);
      this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.StopAutoExpandWhenOverHotItem | QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension | QMenuItemContainerBehaviorFlags.ExpandItemOnMouseUp, true);
      this.m_oPainter = new QExplorerBarPainter();
      this.m_oPaintParams = new QExplorerBarPaintParams();
      this.m_oItemConfigurationChangedEventHander = new EventHandler(this.ItemConfiguration_ConfigurationChanged);
      this.m_oGroupItemConfigurationChangedEventHander = new EventHandler(this.GroupItemConfiguration_ConfigurationChanged);
      this.m_oConfigurationChangedEventHander = new EventHandler(this.Configuration_ConfigurationChanged);
      this.SetOrientation(QCommandContainerOrientation.Vertical, false);
      this.SetExplorerBarConfigurationBase(new QExplorerBarConfiguration(), false);
      this.SetItemConfigurationBase(new QExplorerBarItemConfiguration(), false);
      this.SetGroupItemConfigurationBase(new QExplorerBarGroupItemConfiguration(), false);
      this.SetConfigurationBase((QCommandConfiguration) this.m_oGroupItemConfigurationBase, false);
      this.SetChildMenuConfigurationBase(new QFloatingMenuConfiguration(), false);
      this.GroupItemConfiguration.PutFont(this.Font);
      this.ItemConfiguration.PutFont(this.Font);
      this.PutPersonalized(false);
      this.m_oScrollBarExtension = new QScrollBarExtension((Control) this, QScrollBarVisibility.Vertical);
      this.m_oScrollBarExtension.Scroll += new QScrollEventHandler(this.m_oScrollBarExtension_Scroll);
      this.m_oWeakMessageFilter = new QWeakMessageFilter((object) this);
      Application.AddMessageFilter((IMessageFilter) this.m_oWeakMessageFilter);
      this.ResumeLayout(false);
    }

    [Description("Gets raised when the CalculatedSize property changes.")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler CalculatedSizeChanged
    {
      add => this.m_oCalculatedSizeChangedDelegate = QWeakDelegate.Combine(this.m_oCalculatedSizeChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oCalculatedSizeChangedDelegate = QWeakDelegate.Remove(this.m_oCalculatedSizeChangedDelegate, (Delegate) value);
    }

    [Description("Gets the calculated size of the QExplorerBar")]
    [Browsable(false)]
    public Size CalculatedSize => new Size(this.Width, this.ScrollSize.Height + this.ClientAreaMarginBottom + this.ClientAreaMarginTop);

    [Browsable(false)]
    public int ScrollHorizontalValue => this.m_oScrollBarExtension.ScrollHorizontalValue;

    [Browsable(false)]
    public int ScrollVerticalValue => this.m_oScrollBarExtension.ScrollVerticalValue;

    [Localizable(true)]
    public override Font Font
    {
      get => base.Font;
      set
      {
        base.Font = value;
        if (this.GroupItemConfiguration != null)
          this.GroupItemConfiguration.PutFont(value);
        if (this.ItemConfiguration == null)
          return;
        this.ItemConfiguration.PutFont(value);
      }
    }

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the QAppearance.")]
    public virtual QExplorerBarAppearance Appearance => (QExplorerBarAppearance) base.Appearance;

    [Category("QAppearance")]
    [Description("Gets or sets the Configuration of this QExplorerBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QExplorerBarConfiguration Configuration
    {
      get => this.m_oConfigurationBase;
      set => this.m_oConfigurationBase = value;
    }

    [Description("Gets or sets the GroupConfiguration of this QExplorerBar. The GroupConfiguration applies to the first level of QExplorerItems.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QExplorerBarGroupItemConfiguration GroupItemConfiguration
    {
      get => this.m_oGroupItemConfigurationBase;
      set => this.m_oGroupItemConfigurationBase = value;
    }

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the ItemConfiguration of this QExplorerBar. The ItemConfiguration applies to the second level of QExplorerItems.")]
    public QExplorerBarItemConfiguration ItemConfiguration
    {
      get => this.m_oItemConfigurationBase;
      set => this.m_oItemConfigurationBase = value;
    }

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the ChildMenuConfiguration of this QExplorerBar. The ChildMenuConfiguration applies to the third level of QExplorerItems.")]
    public QFloatingMenuConfiguration ChildMenuConfiguration
    {
      get => this.ChildMenuConfigurationBase;
      set => this.ChildMenuConfigurationBase = value;
    }

    [Editor(typeof (QExplorerGroupItemCollectionEditor), typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the collection of QExplorerItems of this QExplorerBar")]
    [Category("QBehavior")]
    public QMenuItemCollection ExplorerItems => this.Items;

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public bool PreFilterMessage(ref Message m)
    {
      if (this.IsDisposed)
        return false;
      Control destinationControl = (Control) null;
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
          Point point = new Point(m.LParam.ToInt32());
          if (!this.ContainsOrIsContainerWithHandle(m.HWnd))
            this.ResetState();
          return false;
        case 256:
        case 260:
          QControlHelper.GetFirstControlFromHandle(m.HWnd);
          return this.HandleKeyDown((Keys) (int) m.WParam, destinationControl, m);
        case 257:
        case 261:
          QControlHelper.GetFirstControlFromHandle(m.HWnd);
          return this.HandleKeyUp((Keys) (int) m.WParam, destinationControl, m);
        default:
          return false;
      }
    }

    [Obsolete("obsolete since version 1.2.0.20, Use the overload containing the destinationControl and the message")]
    public virtual bool HandleKeyDown(Keys keys) => this.HandleKeyDown(keys, (Control) null, Message.Create(IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero));

    internal override void SetHotItem(QMenuItem item, QMenuItemActivationType activationType)
    {
      base.SetHotItem(item, activationType);
      if (item == null)
        return;
      QExplorerItem parentCommand = item.ParentCommand as QExplorerItem;
      if (!item.IsAccessible || parentCommand == null || item.VisibleWhenPersonalized || !parentCommand.Personalized || !parentCommand.Expanded)
        return;
      parentCommand.PutPersonalized(false);
    }

    internal override bool HandlePossibleHotKey(Keys keys)
    {
      int itemsWithHotkeyCount = this.GetAccessibleMenuItemsWithHotkeyCount(keys, 1);
      if (itemsWithHotkeyCount <= 0)
        return false;
      QMenuItem menuItemWithHotkey = this.GetNextAccessibleMenuItemWithHotkey(this.HotItem, keys, 1);
      if (itemsWithHotkeyCount > 1)
      {
        this.SetHotItem(menuItemWithHotkey, QMenuItemActivationType.Keyboard);
        this.FocusedMenuItem = menuItemWithHotkey;
      }
      else
      {
        if (menuItemWithHotkey.HasChildItems)
        {
          this.SetExpandedItem(menuItemWithHotkey, true, true, QMenuItemActivationType.Hotkey);
        }
        else
        {
          this.SetHotItem(menuItemWithHotkey, QMenuItemActivationType.Keyboard);
          this.ActivateItem(this.HotItem, false, true, this.HotkeyVisible, QMenuItemActivationType.Hotkey);
        }
        this.m_bAltKeyHandled = true;
      }
      return true;
    }

    public virtual bool HandleKeyDown(Keys keys, Control destinationControl, Message message)
    {
      QMenuItem itemWithShortcut = this.GetAccessibleMenuItemWithShortcut(Control.ModifierKeys | keys);
      if (itemWithShortcut != null && this.ShouldHandleShortcutsForControl(destinationControl, this.Configuration.ShortcutScope))
      {
        this.ActivateMenuItem(itemWithShortcut, false, true, QMenuItemActivationType.Shortcut);
        return itemWithShortcut.SuppressShortcutToSystem;
      }
      if (this.ShouldHandleKeyMessagesForControl(destinationControl))
      {
        if (this.ExpandedItem != null)
        {
          if (!this.ExpandedItem.ChildMenu.HandleKeyDown(keys, destinationControl, message) && keys == Keys.Escape)
          {
            this.SetHotItem(this.ExpandedItem, QMenuItemActivationType.Keyboard);
            this.ResetExpandedItem();
            this.ProposedExpandedItem = (QMenuItem) null;
            this.AutoExpand = false;
          }
          return true;
        }
        if (Control.ModifierKeys == Keys.Alt)
        {
          this.HotkeyVisible = true;
          if (this.HandlePossibleHotKey(keys))
          {
            this.Refresh();
            return true;
          }
        }
        else if (this.Focused && this.HandlePossibleHotKey(keys))
        {
          this.Refresh();
          return true;
        }
        if (this.Focused && this.HandleFocus(keys))
          return true;
        if ((keys == Keys.Return || keys == Keys.Space) && (this.Focused || Control.ModifierKeys == Keys.Alt) && this.FocusedMenuItem != null)
        {
          if (this.FocusedMenuItem.HasChildItems)
          {
            this.SetExpandedItem(this.FocusedMenuItem, true, true, QMenuItemActivationType.Keyboard);
          }
          else
          {
            this.SetHotItem(this.FocusedMenuItem, QMenuItemActivationType.Keyboard);
            this.ActivateItem(this.HotItem, false, true, this.HotkeyVisible, QMenuItemActivationType.Hotkey);
          }
          this.m_bAltKeyHandled = true;
          this.Refresh();
          return true;
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

    internal bool ActivateMenuItem(
      QMenuItem menuItem,
      bool animate,
      bool showHotkeyPrefix,
      QMenuItemActivationType activationType)
    {
      return this.ActivateItem(menuItem, true, animate, showHotkeyPrefix, activationType);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override bool ActivateItem(
      QMenuItem menuItem,
      bool expandWhenPossible,
      bool animate,
      bool showHotkeyPrefix,
      QMenuItemActivationType activationType)
    {
      bool flag = base.ActivateItem(menuItem, expandWhenPossible, animate, showHotkeyPrefix, activationType);
      QExplorerItem qexplorerItem = menuItem as QExplorerItem;
      if (flag && qexplorerItem != null)
      {
        QExplorerItem parentCommand = menuItem.ParentCommand as QExplorerItem;
        if (qexplorerItem.IsAccessible && parentCommand != null && !qexplorerItem.VisibleWhenPersonalized && parentCommand.Personalized && parentCommand.Expanded)
          parentCommand.PutPersonalized(false);
        if (qexplorerItem.IsAccessible && !qexplorerItem.IsInformationOnly && activationType == QMenuItemActivationType.Mouse)
          this.FocusedMenuItem = menuItem;
      }
      return flag;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void ResetState() => base.ResetState();

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void PutPersonalized(bool value) => base.PutPersonalized(false);

    internal void SuspendPaint() => this.m_bSuspendPaint = true;

    internal void ResumePaint()
    {
      this.m_bSuspendPaint = false;
      this.Refresh();
    }

    [Browsable(false)]
    public override bool Personalized => this.m_oCurrentDepersonalizeItem != null && this.m_oCurrentDepersonalizeItem.Personalized;

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override bool UnPersonalizeMenuContainsPosition(int x, int y)
    {
      for (int index = 0; index < this.ExplorerItems.Count; ++index)
      {
        if (this.ExplorerItems[index] is QExplorerItem && (this.ExplorerItems[index] as QExplorerItem).ItemType == QExplorerItemType.GroupItem)
        {
          QExplorerItem explorerItem = (QExplorerItem) this.ExplorerItems[index];
          if (explorerItem.DepersonalizeItemBounds.Contains(x, y))
          {
            this.m_oCurrentDepersonalizeItem = explorerItem;
            this.DepersonalizeMenuItemBounds = explorerItem.DepersonalizeItemBounds;
            return true;
          }
        }
      }
      this.m_oCurrentDepersonalizeItem = (QExplorerItem) null;
      this.DepersonalizeMenuItemBounds = Rectangle.Empty;
      return false;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void SetDepersonalizeMenuItemState(QButtonState state, bool refresh) => base.SetDepersonalizeMenuItemState(state, refresh);

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override void DepersonalizeMenuItemContainer()
    {
      if (this.m_oCurrentDepersonalizeItem == null || !this.m_oCurrentDepersonalizeItem.Personalized)
        return;
      this.m_oCurrentDepersonalizeItem.PutPersonalized(false);
    }

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetItemConfiguration() => this.ItemConfiguration.SetToDefaultValues();

    public bool ShouldSerializeItemConfiguration() => !this.ItemConfiguration.IsSetToDefaultValues();

    public void ResetGroupItemConfiguration() => this.GroupItemConfiguration.SetToDefaultValues();

    public bool ShouldSerializeGroupItemConfiguration() => !this.GroupItemConfiguration.IsSetToDefaultValues();

    public void ResetChildMenuConfiguration() => this.ChildMenuConfiguration.SetToDefaultValues();

    public bool ShouldSerializeChildMenuConfiguration() => !this.ChildMenuConfiguration.IsSetToDefaultValues();

    protected override void SetExpandedItem(QMenuItem expandedItem)
    {
      if (expandedItem is QExplorerItem qexplorerItem && qexplorerItem.ItemType == QExplorerItemType.GroupItem)
        return;
      base.SetExpandedItem(expandedItem);
    }

    public override bool ContainsOrIsContainerWithHandle(IntPtr handle)
    {
      if (handle == IntPtr.Zero)
        return false;
      return this.Handle == handle || this.ContainsOrIsContainerWithHandleRecursive(handle, this.Commands);
    }

    private bool ContainsOrIsContainerWithHandleRecursive(
      IntPtr handle,
      QCommandCollection commands)
    {
      for (int index = 0; index < commands.Count; ++index)
      {
        QCommand command = commands.GetCommand(index);
        QExplorerItem qexplorerItem = command as QExplorerItem;
        if (command.ChildContainerCreated && command.ChildContainer.ContainsOrIsContainerWithHandle(handle) || qexplorerItem != null && qexplorerItem.ItemType == QExplorerItemType.GroupItem && qexplorerItem.Expanded && this.ContainsOrIsContainerWithHandleRecursive(handle, command.Commands))
          return true;
      }
      return false;
    }

    public override bool LoadPersistableObject(
      QPersistenceManager manager,
      IXPathNavigable persistableObjectElement,
      object parentState)
    {
      this.SuspendPaint();
      this.SuspendLayout();
      try
      {
        return base.LoadPersistableObject(manager, persistableObjectElement, parentState);
      }
      finally
      {
        this.ResumeLayout();
        this.ResumePaint();
      }
    }

    public override void ShowChildMenu(QMenuItem menuItem, bool animate, bool showHotkeyPrefix)
    {
      if (menuItem is QExplorerItem qexplorerItem && qexplorerItem.ItemType == QExplorerItemType.GroupItem)
      {
        this.ToggleItemState(qexplorerItem, animate, true, true);
        this.PutAutoScrollItem(qexplorerItem);
      }
      else
        base.ShowChildMenu(menuItem, animate, showHotkeyPrefix);
    }

    private QExplorerBarPainter Painter => this.m_oPainter;

    private Size ScrollSize
    {
      get
      {
        if (this.m_oPainter == null)
          return Size.Empty;
        for (int index = 0; index < this.ExplorerItems.Count; ++index)
        {
          if (this.ExplorerItems[index] is QExplorerItem explorerItem && explorerItem.InMotion)
            return this.m_oLastScrollSize;
        }
        this.m_oLastScrollSize = this.m_oPainter.ScrollSize;
        return this.m_oLastScrollSize;
      }
    }

    internal QExplorerBarPaintParams PaintParams => this.m_oPaintParams;

    internal QMenuItem FocusedMenuItem
    {
      get => this.m_oFocusedMenuItem;
      set
      {
        if (this.m_oFocusedMenuItem is QExplorerItem && this.m_oFocusedMenuItem.Control != null)
          this.m_oFocusedMenuItem.Control.ResetBackground();
        this.m_oFocusedMenuItem = value;
        this.SetHotItem(this.m_oFocusedMenuItem, QMenuItemActivationType.Keyboard);
      }
    }

    protected override void Select(bool directed, bool forward)
    {
      this.FocusedMenuItem = (QMenuItem) null;
      if (!forward)
      {
        for (int index = this.Items.Count - 1; index >= 0 && this.FocusedMenuItem == null; --index)
        {
          if (this.Items[index].IsAccessible && this.Items[index].IsVisible && this.Items[index] is QExplorerItem)
            this.FocusedMenuItem = this.Items[index];
        }
      }
      else
      {
        for (int index = 0; index < this.Items.Count && this.FocusedMenuItem == null; ++index)
        {
          if (this.Items[index].IsAccessible && this.Items[index].IsVisible && this.Items[index] is QExplorerItem)
            this.FocusedMenuItem = this.Items[index];
        }
      }
      this.Refresh();
      base.Select(directed, forward);
    }

    private int ClientAreaMarginTop => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth;

    private int ClientAreaMarginLeft => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth;

    private int ClientAreaMarginRight => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth;

    private int ClientAreaMarginBottom => !this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth;

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 791)
      {
        if (((int) m.LParam & 2) == 2 && (((int) m.LParam & 1) != 1 || this.Visible))
        {
          Rectangle windowBounds = NativeHelper.GetWindowBounds((Control) this);
          Rectangle rectangle1 = new Rectangle(this.ClientAreaMarginLeft, this.ClientAreaMarginTop, windowBounds.Width - this.ClientAreaMarginLeft - this.ClientAreaMarginRight, windowBounds.Height - this.ClientAreaMarginTop - this.ClientAreaMarginBottom);
          Rectangle rectangle2 = new Rectangle(0, 0, windowBounds.Width, windowBounds.Height);
          Graphics graphics = Graphics.FromHdc(m.WParam);
          Region savedRegion = QControlPaint.AdjustClip(graphics, new Region(rectangle2), CombineMode.Replace);
          this.OnPaintNonClientArea(new PaintEventArgs(graphics, rectangle2));
          QControlPaint.RestoreClip(graphics, savedRegion);
          graphics.Dispose();
        }
        base.WndProc(ref m);
      }
      else if (m.Msg == 792)
      {
        Graphics graphics = Graphics.FromHdc(m.WParam);
        PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, this.ClientRectangle);
        this.OnPaintBackground(paintEventArgs);
        this.OnPaint(paintEventArgs);
        graphics.Dispose();
      }
      else if (m.Msg == 131)
      {
        if (m.WParam != IntPtr.Zero)
        {
          NativeMethods.NCCALCSIZE_PARAMS valueType = (NativeMethods.NCCALCSIZE_PARAMS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.NCCALCSIZE_PARAMS));
          valueType.rgrc0.left += this.ClientAreaMarginLeft;
          valueType.rgrc0.top += this.ClientAreaMarginTop;
          valueType.rgrc0.bottom -= this.ClientAreaMarginBottom;
          valueType.rgrc0.right -= this.ClientAreaMarginRight;
          valueType.rgrc1 = valueType.rgrc0;
          Marshal.StructureToPtr((object) valueType, m.LParam, false);
          base.WndProc(ref m);
          m.Result = IntPtr.Zero;
        }
        else
        {
          NativeMethods.RECT valueType = (NativeMethods.RECT) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.RECT));
          valueType.left += this.ClientAreaMarginLeft;
          valueType.top += this.ClientAreaMarginTop;
          valueType.bottom -= this.ClientAreaMarginBottom;
          valueType.right -= this.ClientAreaMarginRight;
          Marshal.StructureToPtr((object) valueType, m.LParam, false);
          base.WndProc(ref m);
          m.Result = IntPtr.Zero;
        }
      }
      else if (m.Msg == 133)
      {
        base.WndProc(ref m);
        if (this.ClientAreaMarginLeft == 0 && this.ClientAreaMarginTop == 0 && this.ClientAreaMarginRight == 0 && this.ClientAreaMarginBottom == 0)
          return;
        Rectangle windowBounds = NativeHelper.GetWindowBounds((Control) this);
        Rectangle rect = new Rectangle(this.ClientAreaMarginLeft, this.ClientAreaMarginTop, windowBounds.Width - this.ClientAreaMarginLeft - this.ClientAreaMarginRight, windowBounds.Height - this.ClientAreaMarginTop - this.ClientAreaMarginBottom);
        Rectangle rectangle = new Rectangle(0, 0, windowBounds.Width, windowBounds.Height);
        Region region = new Region(rectangle);
        region.Exclude(rect);
        IntPtr num1 = IntPtr.Zero;
        IntPtr num2 = IntPtr.Zero;
        QOffscreenBitmapSet bitmapSet = (QOffscreenBitmapSet) null;
        try
        {
          num1 = NativeMethods.GetWindowDC(m.HWnd);
          num2 = NativeMethods.CreateCompatibleDC(num1);
          bitmapSet = QOffscreenBitmapsManager.GetFreeBitmapSet();
          IntPtr hObject = bitmapSet.SecureOffscreenDesktopBitmap(rectangle.Size);
          NativeMethods.SelectObject(num2, hObject);
          using (Graphics graphics = Graphics.FromHdc(num2))
          {
            graphics.Clip = region;
            this.OnPaintNonClientArea(new PaintEventArgs(graphics, rectangle));
          }
          int num3 = 0;
          int num4 = 0;
          NativeMethods.BitBlt(num1, num3, num4, rectangle.Width, this.ClientAreaMarginTop, num2, num3, num4, 13369376);
          int num5 = 0;
          int num6 = 0;
          NativeMethods.BitBlt(num1, num5, num6, this.ClientAreaMarginLeft, rectangle.Height, num2, num5, num6, 13369376);
          int num7 = 0;
          int num8 = rectangle.Height - this.ClientAreaMarginBottom;
          NativeMethods.BitBlt(num1, num7, num8, rectangle.Width, this.ClientAreaMarginBottom, num2, num7, num8, 13369376);
          int num9 = rectangle.Width - this.ClientAreaMarginRight;
          int num10 = 0;
          NativeMethods.BitBlt(num1, num9, num10, this.ClientAreaMarginRight, rectangle.Height, num2, num9, num10, 13369376);
        }
        finally
        {
          if (num2 != IntPtr.Zero)
            NativeMethods.DeleteDC(num2);
          if (num1 != IntPtr.Zero)
            NativeMethods.ReleaseDC(m.HWnd, num1);
          if (bitmapSet != null)
            QOffscreenBitmapsManager.FreeBitmapSet(bitmapSet);
        }
      }
      else
        base.WndProc(ref m);
    }

    protected override string BackColorPropertyName => "ExplorerBarBackground1";

    protected override string BackColor2PropertyName => "ExplorerBarBackground2";

    protected override string BorderColorPropertyName => "ExplorerBarBorder";

    protected override QAppearanceBase CreateAppearanceInstance()
    {
      QExplorerBarAppearance appearanceInstance = new QExplorerBarAppearance();
      appearanceInstance.AppearanceChanged += new EventHandler(this.tmp_oAppearance_AppearanceChanged);
      return (QAppearanceBase) appearanceInstance;
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (levent.AffectedControl != null && levent.AffectedControl != this)
        return;
      Size clientSize = this.ClientSize;
      this.LayoutExplorerBar(this.ClientRectangle);
      if (!(clientSize != this.ClientSize))
        return;
      this.LayoutExplorerBar(this.ClientRectangle);
    }

    private static QMenuItemCollection GetMenuItemParentCollection(
      QMenuItem menuItem)
    {
      if (menuItem == null)
        return (QMenuItemCollection) null;
      if (menuItem.ParentCommand != null && menuItem.ParentCommand is QMenuItem)
        return (menuItem.ParentCommand as QMenuItem).MenuItems;
      return menuItem.ParentContainer != null && menuItem.ParentContainer is QExplorerBar ? (menuItem.ParentContainer as QExplorerBar).Items : (QMenuItemCollection) null;
    }

    private static QMenuItem GetNextMenuItemForFocus(QMenuItem menuItem, bool forward) => QExplorerBar.GetNextMenuItemForFocus(menuItem, QExplorerBar.GetMenuItemParentCollection(menuItem), forward, true);

    private static QMenuItem GetNextMenuItemForFocus(
      QMenuItem menuItem,
      QMenuItemCollection collection,
      bool forward,
      bool recursive)
    {
      QExplorerItem qexplorerItem = menuItem as QExplorerItem;
      if (forward && recursive && qexplorerItem != null && qexplorerItem.ItemState == QExplorerItemState.Expanded && QExplorerBar.GetNextMenuItemForFocus((QMenuItem) null, menuItem.MenuItems, forward, recursive) is QExplorerItem menuItemForFocus1)
        return (QMenuItem) menuItemForFocus1;
      if (collection == null)
        return (QMenuItem) null;
      bool flag = menuItem == null;
      if (forward)
      {
        for (int index = 0; index < collection.Count; ++index)
        {
          if (!flag)
            flag = collection[index] == menuItem;
          else if (collection[index] is QExplorerItem && flag && !collection[index].IsSeparator && collection[index].IsAccessible && ((QExplorerItem) collection[index]).IsVisibleForFocus && !collection[index].InformationOnly)
            return (QMenuItem) (collection[index] as QExplorerItem);
        }
      }
      else
      {
        for (int index = collection.Count - 1; index >= 0; --index)
        {
          if (!flag)
            flag = collection[index] == menuItem;
          else if (collection[index] is QExplorerItem && flag && !collection[index].IsSeparator && collection[index].IsAccessible && ((QExplorerItem) collection[index]).IsVisibleForFocus && !collection[index].InformationOnly)
            return (collection[index] as QExplorerItem).ItemState == QExplorerItemState.Expanded && QExplorerBar.GetNextMenuItemForFocus((QMenuItem) null, collection[index].MenuItems, forward, recursive) is QExplorerItem menuItemForFocus2 ? (QMenuItem) menuItemForFocus2 : (QMenuItem) (collection[index] as QExplorerItem);
        }
      }
      if (forward && menuItem != null)
      {
        if (menuItem.ParentCommand != null)
          return QExplorerBar.GetNextMenuItemForFocus(menuItem.ParentCommand as QMenuItem, QExplorerBar.GetMenuItemParentCollection(menuItem.ParentCommand as QMenuItem), forward, false);
      }
      else if (menuItem != null && menuItem.ParentCommand is QMenuItem)
        return !((QMenuItem) menuItem.ParentCommand).IsInformationOnly ? menuItem.ParentCommand as QMenuItem : QExplorerBar.GetNextMenuItemForFocus(menuItem.ParentCommand as QMenuItem, QExplorerBar.GetMenuItemParentCollection(menuItem.ParentCommand as QMenuItem), forward, false);
      return (QMenuItem) null;
    }

    private bool HandleFocus(Keys keys)
    {
      if (this.DesignMode || this.FocusedMenuItem == null)
        return false;
      bool forward;
      switch (keys)
      {
        case Keys.Tab:
          forward = Control.ModifierKeys != Keys.Shift;
          break;
        case Keys.Left:
        case Keys.Up:
          forward = false;
          break;
        case Keys.Right:
        case Keys.Down:
          forward = true;
          break;
        default:
          return false;
      }
      this.FocusedMenuItem = QExplorerBar.GetNextMenuItemForFocus(this.FocusedMenuItem, forward);
      this.Refresh();
      return this.FocusedMenuItem != null;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (!this.m_oScrollBarExtension.ScrollVerticalVisible)
        return;
      this.m_oScrollBarExtension.ScrollVerticalValue -= (int) ((double) (e.Delta * SystemInformation.MouseWheelScrollLines / 120) * (double) this.Font.Size);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Refresh();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (this.DesignMode || this.ContainsFocus)
        return;
      this.FocusedMenuItem = (QMenuItem) null;
      this.Refresh();
      base.OnLostFocus(e);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.PerformLayout((Control) null, (string) null);
    }

    public override DockStyle Dock
    {
      get => base.Dock;
      set
      {
        bool flag = !this.PerformingLayout;
        if (flag)
          this.PutPerformingLayout(true);
        base.Dock = value;
        if (!flag)
          return;
        this.PutPerformingLayout(false);
        this.LayoutExplorerBar(this.ClientRectangle);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.CanFocus)
        this.Focus();
      base.OnMouseDown(this.ApplyScrollPositionToMouseEventArgs(e));
    }

    protected override void OnMouseMove(MouseEventArgs e) => base.OnMouseMove(this.ApplyScrollPositionToMouseEventArgs(e));

    protected override void OnMouseUp(MouseEventArgs e) => base.OnMouseUp(this.ApplyScrollPositionToMouseEventArgs(e));

    protected virtual void OnCalculatedSizeChanged(EventArgs e) => this.m_oCalculatedSizeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oCalculatedSizeChangedDelegate, (object) this, (object) e);

    public new void ResumeLayout(bool performLayout) => base.ResumeLayout(true);

    protected override bool AutoExpand
    {
      get => base.AutoExpand;
      set
      {
      }
    }

    private static double GetStepPercentage(int stepNumber, int stepCount) => (1.0 - Math.Cos(Math.PI * (double) stepNumber / (double) stepCount)) / 2.0;

    private MouseEventArgs ApplyScrollPositionToMouseEventArgs(MouseEventArgs e) => new MouseEventArgs(e.Button, e.Clicks, e.X + this.ScrollHorizontalValue, e.Y + this.ScrollVerticalValue, e.Delta);

    private void FillPaintParams()
    {
      this.CleanUpPaintParams();
      this.PaintParams.Font = this.Font;
      this.PaintParams.ItemStringFormat = this.CreateStringFormat((QCommandConfiguration) this.ItemConfiguration);
      this.PaintParams.GroupItemStringFormat = this.CreateStringFormat((QCommandConfiguration) this.GroupItemConfiguration);
      this.PaintParams.TextColor = (Color) this.ColorScheme.ExplorerBarText;
      this.PaintParams.TextHotColor = (Color) this.ColorScheme.ExplorerBarTextHot;
      this.PaintParams.TextPressedColor = (Color) this.ColorScheme.ExplorerBarTextPressed;
      this.PaintParams.TextExpandedColor = (Color) this.ColorScheme.ExplorerBarTextExpanded;
      this.PaintParams.TextDisabledColor = (Color) this.ColorScheme.ExplorerBarTextDisabled;
      this.PaintParams.SeparatorColor = (Color) this.ColorScheme.ExplorerBarSeparator;
      this.PaintParams.DepersonalizeImageForeground = (Color) this.ColorScheme.ExplorerBarDepersonalizeImageForeground;
      this.PaintParams.DepersonalizeImageBackground = (Color) this.ColorScheme.ExplorerBarDepersonalizeImageBackground;
      this.PaintParams.Configuration = this.Configuration;
      this.PaintParams.ItemConfiguration = this.ItemConfiguration;
      this.PaintParams.GroupItemConfiguration = this.GroupItemConfiguration;
      this.PaintParams.ColorScheme = this.ColorScheme;
      this.PaintParams.ExpandedItemBackground1Color = (Color) this.ColorScheme.ExplorerBarExpandedItemBackground1;
      this.PaintParams.ExpandedItemBackground2Color = (Color) this.ColorScheme.ExplorerBarExpandedItemBackground2;
      this.PaintParams.ExpandedItemBorderColor = (Color) this.ColorScheme.ExplorerBarExpandedItemBorder;
      this.PaintParams.HotItemBackground1Color = (Color) this.ColorScheme.ExplorerBarHotItemBackground1;
      this.PaintParams.HotItemBackground2Color = (Color) this.ColorScheme.ExplorerBarHotItemBackground2;
      this.PaintParams.HotItemBorderColor = (Color) this.ColorScheme.ExplorerBarHotItemBorder;
      this.PaintParams.PressedItemBackground1Color = (Color) this.ColorScheme.ExplorerBarPressedItemBackground1;
      this.PaintParams.PressedItemBackground2Color = (Color) this.ColorScheme.ExplorerBarPressedItemBackground2;
      this.PaintParams.PressedItemBorderColor = (Color) this.ColorScheme.ExplorerBarPressedItemBorder;
      this.PaintParams.CheckedItemBackground1Color = (Color) this.ColorScheme.ExplorerBarCheckedItemBackground1;
      this.PaintParams.CheckedItemBackground2Color = (Color) this.ColorScheme.ExplorerBarCheckedItemBackground2;
      this.PaintParams.CheckedItemBorderColor = (Color) this.ColorScheme.ExplorerBarCheckedItemBorder;
      this.PaintParams.GroupPanelBackground1Color = (Color) this.ColorScheme.ExplorerBarGroupPanelBackground1;
      this.PaintParams.GroupPanelBackground2Color = (Color) this.ColorScheme.ExplorerBarGroupPanelBackground2;
      this.PaintParams.GroupPanelBorderColor = (Color) this.ColorScheme.ExplorerBarGroupPanelBorder;
      this.PaintParams.GroupItemBackground1Color = (Color) this.ColorScheme.ExplorerBarGroupItemBackground1;
      this.PaintParams.GroupItemBackground2Color = (Color) this.ColorScheme.ExplorerBarGroupItemBackground2;
      this.PaintParams.GroupItemBorderColor = (Color) this.ColorScheme.ExplorerBarGroupItemBorder;
      this.PaintParams.ExpandedGroupItemBackground1Color = (Color) this.ColorScheme.ExplorerBarExpandedGroupItemBackground1;
      this.PaintParams.ExpandedGroupItemBackground2Color = (Color) this.ColorScheme.ExplorerBarExpandedGroupItemBackground2;
      this.PaintParams.ExpandedGroupItemBorderColor = (Color) this.ColorScheme.ExplorerBarExpandedGroupItemBorder;
      this.PaintParams.HotGroupItemBackground1Color = (Color) this.ColorScheme.ExplorerBarHotGroupItemBackground1;
      this.PaintParams.HotGroupItemBackground2Color = (Color) this.ColorScheme.ExplorerBarHotGroupItemBackground2;
      this.PaintParams.HotGroupItemBorderColor = (Color) this.ColorScheme.ExplorerBarHotGroupItemBorder;
      this.PaintParams.PressedGroupItemBackground1Color = (Color) this.ColorScheme.ExplorerBarPressedGroupItemBackground1;
      this.PaintParams.PressedGroupItemBackground2Color = (Color) this.ColorScheme.ExplorerBarPressedGroupItemBackground2;
      this.PaintParams.PressedGroupItemBorderColor = (Color) this.ColorScheme.ExplorerBarPressedGroupItemBorder;
      this.PaintParams.CheckedGroupItemBackground1Color = (Color) this.ColorScheme.ExplorerBarCheckedGroupItemBackground1;
      this.PaintParams.CheckedGroupItemBackground2Color = (Color) this.ColorScheme.ExplorerBarCheckedGroupItemBackground2;
      this.PaintParams.CheckedGroupItemBorderColor = (Color) this.ColorScheme.ExplorerBarCheckedGroupItemBorder;
      this.PaintParams.HasMoreChildItemsColor = (Color) this.ColorScheme.ExplorerBarHasMoreChildItemsColor;
    }

    private void FillPaintParamsForPaint() => this.CleanUpPaintParamsForPaint();

    private void CleanUpPaintParamsForPaint() => this.PaintParams.UpdateColors((QExplorerItem) null);

    private void CleanUpPaintParams() => this.PaintParams.UpdateColors((QExplorerItem) null);

    private void UpdateControlBitmaps(QMenuItemCollection collection, bool onlyCreateEmptyBitmaps)
    {
      if (collection == null)
        return;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index] is QExplorerItem qexplorerItem && !qexplorerItem.InMovement && !qexplorerItem.InMotion)
        {
          if (qexplorerItem.Control != null && (onlyCreateEmptyBitmaps && qexplorerItem.Control.Bitmap == null || !onlyCreateEmptyBitmaps))
            qexplorerItem.Control.CreateBitmap();
          this.UpdateControlBitmaps(qexplorerItem.MenuItems, onlyCreateEmptyBitmaps);
        }
      }
    }

    private void UpdateCalculatedSize()
    {
      Size calculatedSize = this.CalculatedSize;
      if (!(calculatedSize != this.m_oLastCalculatedSize))
        return;
      this.m_oLastCalculatedSize = calculatedSize;
      this.OnCalculatedSizeChanged(EventArgs.Empty);
    }

    private void UpdateScrollBars()
    {
      if (this.m_oScrollBarExtension == null)
        return;
      if (this.m_oScrollBarExtension.SetScrollSize(this.ScrollSize))
      {
        this.LayoutExplorerBar(this.ClientRectangle);
        this.UpdateControlBitmaps(this.ExplorerItems, true);
      }
      this.UpdateControlsScrollPosition();
    }

    private void UpdateScrollPosition()
    {
      if (this.m_oAutoScrollItem == null)
        return;
      int y = this.m_oAutoScrollItem.GroupBounds.Y;
      int num1 = this.m_oAutoScrollItem.GroupBounds.Y + this.m_oAutoScrollItem.GroupBounds.Height;
      int num2 = this.m_oScrollBarExtension.ScrollVerticalValue + this.ClientRectangle.Height;
      int scrollVerticalValue = this.m_oScrollBarExtension.ScrollVerticalValue;
      if (num1 > num2)
      {
        this.m_oScrollBarExtension.ScrollVerticalValue = Math.Min(this.ScrollSize.Height - this.ClientRectangle.Height, num1 - this.ClientRectangle.Height + 5);
      }
      else
      {
        if (y >= scrollVerticalValue)
          return;
        this.m_oScrollBarExtension.ScrollVerticalValue = Math.Max(0, y - 5);
      }
    }

    private void UpdateControlsScrollPosition()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QCommandControlContainer)
          (this.Controls[index] as QCommandControlContainer).PositionOffset = new Size(this.ScrollHorizontalValue, this.ScrollVerticalValue);
      }
    }

    private StringFormat CreateStringFormat(QCommandConfiguration configuration)
    {
      QExplorerBarGroupItemConfiguration itemConfiguration1 = configuration as QExplorerBarGroupItemConfiguration;
      QExplorerBarItemConfiguration itemConfiguration2 = configuration as QExplorerBarItemConfiguration;
      bool flag = false;
      if (itemConfiguration1 != null && itemConfiguration1.ItemWrapText)
        flag = true;
      else if (itemConfiguration2 != null && itemConfiguration2.ItemWrapText)
        flag = true;
      StringFormat stringFormat = new StringFormat(StringFormat.GenericDefault);
      stringFormat.LineAlignment = StringAlignment.Near;
      stringFormat.Alignment = itemConfiguration2 == null || itemConfiguration2.ItemOrientation != QCommandOrientation.Vertical ? (itemConfiguration1 == null || itemConfiguration1.ItemOrientation != QCommandOrientation.Vertical ? StringAlignment.Near : StringAlignment.Center) : StringAlignment.Center;
      if (flag)
      {
        stringFormat.Trimming = StringTrimming.None;
      }
      else
      {
        stringFormat.Trimming = StringTrimming.EllipsisCharacter;
        stringFormat.FormatFlags = StringFormatFlags.NoWrap;
      }
      stringFormat.HotkeyPrefix = this.HotkeyVisible ? HotkeyPrefix.Show : HotkeyPrefix.Hide;
      return stringFormat;
    }

    private void LayoutExplorerBar(Rectangle rectangle)
    {
      if (this.PaintParams == null || this.Configuration == null || this.GroupItemConfiguration == null || this.ItemConfiguration == null || this.ColorScheme == null || this.PerformingLayout)
        return;
      this.PutPerformingLayout(true);
      this.FillPaintParams();
      if (this.Orientation == QCommandContainerOrientation.Vertical || this.Orientation == QCommandContainerOrientation.None)
        this.Painter.LayoutVertical(this.PaintParams.Configuration.ExplorerBarPadding.InflateRectangleWithPadding(new Rectangle(0, 0, rectangle.Width, rectangle.Height), false, true), (QCommandConfiguration) null, (QCommandPaintParams) this.PaintParams, (Control) this, (QCommandCollection) this.ExplorerItems);
      this.PutPerformingLayout(false);
      this.UpdateCalculatedSize();
      this.UpdateScrollBars();
    }

    internal void SetItemConfigurationBase(
      QExplorerBarItemConfiguration configuration,
      bool raiseEvent)
    {
      if (this.m_oItemConfigurationBase != null)
        this.m_oItemConfigurationBase.ConfigurationChanged -= this.m_oItemConfigurationChangedEventHander;
      this.m_oItemConfigurationBase = configuration;
      if (this.m_oItemConfigurationBase != null)
        this.m_oItemConfigurationBase.ConfigurationChanged += this.m_oItemConfigurationChangedEventHander;
      this.ItemConfiguration_ConfigurationChanged((object) null, EventArgs.Empty);
      if (!raiseEvent)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Invalidate();
    }

    internal void SetGroupItemConfigurationBase(
      QExplorerBarGroupItemConfiguration configuration,
      bool raiseEvent)
    {
      if (this.m_oGroupItemConfigurationBase != null)
        this.m_oGroupItemConfigurationBase.ConfigurationChanged -= this.m_oGroupItemConfigurationChangedEventHander;
      this.m_oGroupItemConfigurationBase = configuration;
      if (this.m_oGroupItemConfigurationBase != null)
        this.m_oGroupItemConfigurationBase.ConfigurationChanged += this.m_oGroupItemConfigurationChangedEventHander;
      if (!raiseEvent)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Invalidate();
    }

    internal void SetExplorerBarConfigurationBase(
      QExplorerBarConfiguration configuration,
      bool raiseEvent)
    {
      if (this.m_oConfigurationBase != null)
        this.m_oConfigurationBase.ConfigurationChanged -= this.m_oConfigurationChangedEventHander;
      this.m_oConfigurationBase = configuration;
      if (this.m_oConfigurationBase != null)
        this.m_oConfigurationBase.ConfigurationChanged += this.m_oConfigurationChangedEventHander;
      if (!raiseEvent)
        return;
      this.PerformLayout((Control) null, (string) null);
      this.Invalidate();
    }

    internal void PutAutoScrollItem(QExplorerItem item)
    {
      this.m_oAutoScrollItem = item;
      if (this.m_oAutoScrollItem.InMotion)
        return;
      this.UpdateScrollPosition();
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override Rectangle ContainerRectangleToScreen(Rectangle rectangle) => base.ContainerRectangleToScreen(new Rectangle(rectangle.X - this.m_oScrollBarExtension.ScrollHorizontalValue, rectangle.Y - this.m_oScrollBarExtension.ScrollVerticalValue, rectangle.Width, rectangle.Height));

    internal Point ContainerPointToClient(Point point) => this.PointToClient(new Point(point.X + this.m_oScrollBarExtension.ScrollHorizontalValue, point.Y + this.m_oScrollBarExtension.ScrollVerticalValue));

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override bool ContainsMenuItem(QMenuItem menuItem) => this.ContainsMenuItemRecursive(menuItem, this.Items);

    private bool ContainsMenuItemRecursive(QMenuItem menuItem, QMenuItemCollection collection)
    {
      if (collection.Contains(menuItem))
        return true;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index] is QExplorerItem && ((QExplorerItem) collection[index]).Expanded && this.ContainsMenuItemRecursive(menuItem, collection[index].MenuItems))
          return true;
      }
      return false;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override QMenuItem GetItemAtPosition(int x, int y)
    {
      QMenuItem menuItemAtPosition = this.GetRecursiveMenuItemAtPosition(new Point(x, y), this.Items);
      return menuItemAtPosition != null && menuItemAtPosition.IsVisible ? menuItemAtPosition : (QMenuItem) null;
    }

    private QMenuItem GetRecursiveMenuItemAtPosition(
      Point point,
      QMenuItemCollection items)
    {
      QMenuItem menuItemAtPosition = items.GetMenuItemAtPosition(point);
      if (menuItemAtPosition != null)
        return menuItemAtPosition;
      for (int index = 0; index < items.Count && menuItemAtPosition == null; ++index)
      {
        if (items[index] is QExplorerItem && (items[index] as QExplorerItem).Expanded)
          menuItemAtPosition = this.GetRecursiveMenuItemAtPosition(point, items[index].MenuItems);
      }
      return menuItemAtPosition;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal override QMenuItem GetItemWithOuterBoundsOn(int x, int y)
    {
      QMenuItem withOuterBoundsOn = this.GetRecursiveMenuItemWithOuterBoundsOn(new Point(x, y), this.Items);
      return withOuterBoundsOn != null && withOuterBoundsOn.IsVisible ? withOuterBoundsOn : (QMenuItem) null;
    }

    private QMenuItem GetRecursiveMenuItemWithOuterBoundsOn(
      Point point,
      QMenuItemCollection items)
    {
      QMenuItem withOuterBoundsOn = items.GetMenuItemWithOuterBoundsOn(point);
      if (withOuterBoundsOn != null)
        return withOuterBoundsOn;
      for (int index = 0; index < items.Count && withOuterBoundsOn == null; ++index)
      {
        if (items[index] is QExplorerItem && (items[index] as QExplorerItem).Expanded)
          withOuterBoundsOn = this.GetRecursiveMenuItemWithOuterBoundsOn(point, items[index].MenuItems);
      }
      return withOuterBoundsOn;
    }

    internal override Color ParentMenuIntersectColor
    {
      [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")] get => (Color) this.ColorScheme.ExplorerBarExpandedItemBackground2;
    }

    private void ToggleItemState(
      QExplorerItem item,
      bool animate,
      bool enforceExpandMultiple,
      bool startAnimation)
    {
      if (this.GroupItemConfiguration.UsedAnimationType == QExplorerItemAnimationType.None || !animate)
      {
        item.ItemState = !item.Expanded ? QExplorerItemState.Expanded : QExplorerItemState.Collapsed;
        if (enforceExpandMultiple)
          this.EnforceExpandMultiple(item, animate);
        this.ValidateFocusedMenuItem();
        this.PerformLayout((Control) null, (string) null);
      }
      else
      {
        this.UpdateControlBitmaps(this.ExplorerItems, true);
        this.Painter.SuppressControlMovement = true;
        if (item.InMotion)
        {
          int motionStep = item.MotionStep;
          int motionHeight = item.MotionHeight;
          int motionOffset = item.MotionOffset;
          int num = this.GroupItemConfiguration.AnimateTime / this.m_iAnimationTimerInterval;
          item.ItemState = item.ItemState != QExplorerItemState.Expanding ? QExplorerItemState.Expanding : QExplorerItemState.Collapsing;
          this.PerformLayout((Control) null, (string) null);
          item.MotionStep = num - motionStep;
          item.MotionHeight = motionHeight;
          item.MotionOffset = motionOffset;
          this.ValidateFocusedMenuItem();
          this.ProcessMotion();
        }
        else
        {
          bool expanded = item.Expanded;
          int num = 0;
          if (expanded)
            num = item.GroupBounds.Height - item.Bounds.Height;
          if (enforceExpandMultiple)
            this.EnforceExpandMultiple(item, animate);
          if (expanded)
          {
            this.CreateExpandedBitmap(item);
            item.ItemState = QExplorerItemState.Collapsing;
            this.PerformLayout((Control) null, (string) null);
          }
          else
          {
            item.ItemState = QExplorerItemState.Expanding;
            this.PerformLayout((Control) null, (string) null);
            this.CreateExpandedBitmap(item);
          }
          item.MotionHeight = item.ItemState != QExplorerItemState.Expanding ? num : item.GroupBounds.Height - item.Bounds.Height;
          this.ValidateFocusedMenuItem();
          if (!startAnimation)
            return;
          this.ProcessMotion();
          if (this.m_bTimerRunning)
            return;
          this.m_bTimerRunning = true;
          this.StartTimer(18, this.m_iAnimationTimerInterval);
        }
      }
    }

    private bool EnforceExpandMultiple(QExplorerItem item, bool animate)
    {
      bool flag = false;
      if (!this.GroupItemConfiguration.CanExpandMultiple)
      {
        QCommandCollection qcommandCollection = (QCommandCollection) null;
        if (item.ParentCommand != null)
          qcommandCollection = item.ParentCommand.Commands;
        else if (item.ParentContainer != null)
          qcommandCollection = item.ParentContainer.Commands;
        if (qcommandCollection != null)
        {
          for (int index = 0; index < qcommandCollection.Count; ++index)
          {
            QCommand command = qcommandCollection.GetCommand(index);
            QExplorerItem qexplorerItem = command as QExplorerItem;
            if (command != item && qexplorerItem != null && qexplorerItem.ItemType == QExplorerItemType.GroupItem && qexplorerItem.Expanded)
            {
              flag = true;
              this.ToggleItemState(qexplorerItem, animate, false, false);
            }
          }
        }
      }
      return flag;
    }

    private void ValidateFocusedMenuItem()
    {
      if (this.m_oFocusedMenuItem == null)
        return;
      if (!this.m_oFocusedMenuItem.IsAccessible || !this.m_oFocusedMenuItem.IsVisible || this.m_oFocusedMenuItem.InformationOnly)
      {
        this.m_oFocusedMenuItem = this.m_oFocusedMenuItem.ParentCommand as QMenuItem;
      }
      else
      {
        if (!(this.m_oFocusedMenuItem.ParentCommand is QExplorerItem) || (this.m_oFocusedMenuItem.ParentCommand as QExplorerItem).ItemState == QExplorerItemState.Expanded)
          return;
        this.m_oFocusedMenuItem = this.m_oFocusedMenuItem.ParentCommand as QMenuItem;
      }
      this.ValidateFocusedMenuItem();
    }

    private void CreateExpandedBitmap(QExplorerItem item)
    {
      this.FillPaintParamsForPaint();
      Bitmap paintingBitmap = new Bitmap(this.ClientSize.Width, item.GroupBounds.Height + Math.Max(this.GroupItemConfiguration.ItemAppearance.BorderWidth, this.GroupItemConfiguration.ActivatedItemAppearance.BorderWidth) + 2 + (this.GroupItemConfiguration.ShadeVisible ? Math.Abs(this.GroupItemConfiguration.ShadePosition.Y) * 2 : 0));
      if (item.ExpandedBitmap != null)
        item.ExpandedBitmap.Dispose();
      item.ExpandedBitmap = paintingBitmap;
      Graphics graphics = Graphics.FromImage((Image) paintingBitmap);
      graphics.TranslateTransform(0.0f, (float) -(item.GroupBounds.Top - Math.Abs(this.GroupItemConfiguration.ShadePosition.Y) - 1));
      this.Painter.DrawItem((QCommand) item, (QCommandConfiguration) null, (QCommandPaintParams) this.PaintParams, (QCommandContainer) this, QCommandPaintOptions.Expanded, graphics, true, true, paintingBitmap);
      graphics.ResetTransform();
      graphics.Dispose();
    }

    private void ProcessMotion()
    {
      bool flag = false;
      this.Painter.SuppressControlMovement = false;
      int num = 0;
      int stepCount = this.GroupItemConfiguration.AnimateTime / this.m_iAnimationTimerInterval;
      for (int index = 0; index < this.ExplorerItems.Count; ++index)
      {
        if (this.ExplorerItems[index] is QExplorerItem)
        {
          QExplorerItem explorerItem = this.ExplorerItems[index] as QExplorerItem;
          explorerItem.PositionOffset = num;
          if (explorerItem.InMotion)
          {
            flag = true;
            ++explorerItem.MotionStep;
            int motionHeight = explorerItem.MotionHeight;
            int motionOffset = explorerItem.MotionOffset;
            int val1 = (int) ((double) motionHeight * QExplorerBar.GetStepPercentage(explorerItem.MotionStep, stepCount));
            if (val1 >= motionHeight && explorerItem.ItemState == QExplorerItemState.Expanding)
              explorerItem.ItemState = QExplorerItemState.Expanded;
            else if (val1 >= motionHeight && explorerItem.ItemState == QExplorerItemState.Collapsing)
            {
              explorerItem.ItemState = QExplorerItemState.Collapsed;
            }
            else
            {
              explorerItem.MotionOffset = Math.Min(val1, motionHeight);
              if (explorerItem.ItemState == QExplorerItemState.Expanding)
                num -= explorerItem.MotionHeight - explorerItem.MotionOffset;
              else
                num += explorerItem.MotionHeight - explorerItem.MotionOffset;
            }
          }
        }
      }
      if (flag)
      {
        this.Refresh();
      }
      else
      {
        this.m_bTimerRunning = false;
        this.StopTimer(18);
        this.UpdateCalculatedSize();
        this.UpdateScrollBars();
        this.UpdateControlsScrollPosition();
        this.UpdateScrollPosition();
        if (!(this.Parent is QCommandControlContainer))
          return;
        (this.Parent as QCommandControlContainer).CreateBitmap();
      }
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.Visible || this.m_bInitialized)
        return;
      this.UpdateControlBitmaps(this.ExplorerItems, false);
      this.m_bInitialized = true;
    }

    protected override void OnTimerElapsed(QControlTimerEventArgs e)
    {
      base.OnTimerElapsed(e);
      if (e.TimerId != 18)
        return;
      this.ProcessMotion();
    }

    protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

    private Rectangle PaintRectangle
    {
      get
      {
        int num1 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth;
        int num2 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth;
        int num3 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth;
        int num4 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth;
        return new Rectangle(this.ClientRectangle.X + num3, this.ClientRectangle.Y + num1, this.ClientRectangle.Width - (num3 + num4), this.ClientRectangle.Height - (num1 + num2));
      }
    }

    private void OnPaintNonClientArea(PaintEventArgs e) => e.Graphics.Clear(this.BorderColor);

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.m_bSuspendPaint || this.PaintParams == null || this.Configuration == null || this.GroupItemConfiguration == null || this.ItemConfiguration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      e.Graphics.TranslateTransform((float) -this.m_oScrollBarExtension.ScrollHorizontalValue, (float) -this.m_oScrollBarExtension.ScrollVerticalValue);
      for (int firstShownCommand = this.FirstShownCommand; firstShownCommand <= this.LastShownCommand; ++firstShownCommand)
      {
        if (this.ExplorerItems[firstShownCommand].IsVisible && this.ExplorerItems[firstShownCommand] is QExplorerItem)
        {
          QCommandPaintOptions flags = QCommandPaintOptions.None;
          if ((this.ExplorerItems[firstShownCommand] as QExplorerItem).Expanded)
            flags |= QCommandPaintOptions.Expanded;
          if (this.ExplorerItems[firstShownCommand] == this.MouseDownItem && !this.ExplorerItems[firstShownCommand].IsInformationOnly)
            flags |= QCommandPaintOptions.Pressed;
          if (this.ExplorerItems[firstShownCommand] == this.HotItem && !this.ExplorerItems[firstShownCommand].IsInformationOnly)
            flags |= QCommandPaintOptions.Hot;
          this.Painter.DrawItem((QCommand) this.ExplorerItems[firstShownCommand], (QCommandConfiguration) null, (QCommandPaintParams) this.PaintParams, (QCommandContainer) this, flags, e.Graphics);
          this.RaisePaintMenuItem(new QPaintMenuItemEventArgs(this.ExplorerItems[firstShownCommand], flags, this.PaintParams.StringFormat, e.Graphics));
        }
      }
      this.PaintAdornments(e.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);
      if (this.PaintParams == null || this.Configuration == null || this.GroupItemConfiguration == null || this.ItemConfiguration == null || this.ColorScheme == null)
        return;
      this.FillPaintParamsForPaint();
      if (this.Orientation == QCommandContainerOrientation.Horizontal || this.Orientation == QCommandContainerOrientation.None)
      {
        this.Painter.DrawControlBackgroundHorizontal(this.ClientRectangle, (QAppearanceBase) null, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) null, (Control) this, pevent.Graphics);
      }
      else
      {
        if (this.Orientation != QCommandContainerOrientation.Vertical)
          return;
        this.Painter.DrawControlBackgroundVertical(this.ClientRectangle, (QAppearanceBase) null, (QColorSchemeBase) this.ColorScheme, (QCommandPaintParams) this.PaintParams, (QCommandConfiguration) null, (Control) this, pevent.Graphics);
      }
    }

    protected override bool DrawBorders => false;

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

    private void PerformNonClientAreaLayout() => NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 39U);

    private void ItemConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
      if (this.ItemConfiguration.ExpandOnItemClick)
      {
        if ((this.BehaviorFlags & QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension) == QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension)
          this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension, false);
      }
      else if ((this.BehaviorFlags & QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension) != QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension)
        this.SetBehaviorFlags(QMenuItemContainerBehaviorFlags.UseHasChildItemHotBoundsForExpension, true);
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
    }

    private void GroupItemConfiguration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
    }

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.PerformLayout((Control) null, (string) null);
      this.Refresh();
    }

    private void m_oScrollBarExtension_Scroll(object sender, QScrollEventArgs e)
    {
      this.ResetExpandedItem();
      this.UpdateControlsScrollPosition();
      this.Refresh();
    }

    private void tmp_oAppearance_AppearanceChanged(object sender, EventArgs e) => this.PerformNonClientAreaLayout();
  }
}
