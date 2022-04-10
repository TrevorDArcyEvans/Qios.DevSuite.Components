// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarAction
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QToolBarAction
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private IQToolBar m_oToolBar;
    private Rectangle m_oProposedBounds;
    private int m_iUserRequestedPosition;
    private int m_iUserPriority;
    private Point m_oDragStartOffset;
    private QFloatingMenu m_oCustomizeMenu;

    public QToolBarAction(IQToolBar toolBar)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oToolBar = toolBar;
    }

    public IQToolBar ToolBar => this.m_oToolBar;

    public QMenuItemContainer MenuItemContainer => !(this.m_oToolBar is QMenuItemContainer) ? (QMenuItemContainer) null : (QMenuItemContainer) this.m_oToolBar;

    public Rectangle ProposedBounds
    {
      get => this.m_oProposedBounds;
      set => this.m_oProposedBounds = value;
    }

    public Point DragStartOffSet
    {
      get => this.m_oDragStartOffset;
      set => this.m_oDragStartOffset = value;
    }

    public int UserRequestedPosition
    {
      get => this.m_iUserRequestedPosition;
      set => this.m_iUserRequestedPosition = value;
    }

    public int UserPriority
    {
      get => this.m_iUserPriority;
      set => this.m_iUserPriority = value;
    }

    public void ShowCustomizeMenu()
    {
      if (this.MenuItemContainer != null)
      {
        QMenuItemContainer menuItemContainer = this.MenuItemContainer;
        this.CustomizeMenu.Configuration = menuItemContainer.ChildMenuConfigurationBase;
        this.CustomizeMenu.ColorScheme = menuItemContainer.ColorScheme;
        this.CustomizeMenu.Font = menuItemContainer.Font;
        this.CustomizeMenu.Items.Clear();
        for (int index = menuItemContainer.LastShownCommand + 1; index < menuItemContainer.Items.Count; ++index)
        {
          QMenuItem qmenuItem = menuItemContainer.Items[index];
          if (qmenuItem.IsVisible && (this.CustomizeMenu.MenuItems.Count > 0 || !qmenuItem.IsSeparator))
          {
            QMenuItem menuItem = (QMenuItem) qmenuItem.Clone(true);
            menuItem.SetAdditionalProperty(3, (object) QCommandCopyType.ExecuteOriginal);
            this.CustomizeMenu.Items.Add(menuItem);
          }
        }
        if (this.ToolBar.ToolBarConfiguration.CanCustomize && this.ToolBar.ToolBarConfiguration.ShowDefaultCustomizeItems)
        {
          if (this.CustomizeMenu.Items.Count > 0 && !this.CustomizeMenu.Items[0].IsSeparator)
            this.CustomizeMenu.Items.Add(new QMenuItem(true));
          QMenuItem menuItem = new QMenuItem(this.ToolBar.ToolBarConfiguration.CustomizeItemTitle);
          if (this.ToolBar.ToolBarHost != null)
            menuItem.MenuItems.AddRange(this.ToolBar.ToolBarHost.RetrieveCustomizeMenu());
          else if (this.MenuItemContainer != null)
            menuItem.MenuItems.AddRange(this.MenuItemContainer.RetrieveCustomizeMenu());
          this.CustomizeMenu.Items.Add(menuItem);
        }
      }
      if (this.CustomizeMenu.Items.Count <= 0)
        return;
      Rectangle customizeItemBounds = this.ToolBar.CustomizeItemBounds;
      Size requestedSize = this.CustomizeMenu.CalculateRequestedSize();
      QMenuCalculateBoundsResult menuBounds = QMenu.CalculateMenuBounds(new Rectangle(customizeItemBounds.Left, customizeItemBounds.Bottom, requestedSize.Width, requestedSize.Height), customizeItemBounds, QRelativePositions.Above | QRelativePositions.Left, QCommandDirections.Down | QCommandDirections.Right, QMenuCalculateBoundsOptions.None);
      this.CustomizeMenu.ShowMenu(menuBounds.Bounds, customizeItemBounds, menuBounds.OpeningItemPosition, menuBounds.AnimateDirection);
    }

    private void SecureCustomizeMenu()
    {
      if (this.m_oCustomizeMenu != null)
        return;
      this.m_oCustomizeMenu = this.MenuItemContainer == null ? new QFloatingMenu() : new QFloatingMenu((IQCommandContainer) this.MenuItemContainer);
      this.m_oCustomizeMenu.BubbleEventsToCustomParent = false;
      this.m_oCustomizeMenu.MenuItemMouseDown += new QMenuMouseEventHandler(this.m_oCustomizeMenu_MenuItemMouseDown);
      this.m_oCustomizeMenu.MenuItemMouseUp += new QMenuMouseEventHandler(this.m_oCustomizeMenu_MenuItemMouseUp);
      this.m_oCustomizeMenu.MenuItemSelected += new QMenuEventHandler(this.CustomizeMenu_MenuItemSelected);
      this.m_oCustomizeMenu.MenuItemActivating += new QMenuCancelEventHandler(this.CustomizeMenu_MenuItemActivating);
      this.m_oCustomizeMenu.MenuItemActivated += new QMenuEventHandler(this.CustomizeMenu_MenuItemActivated);
      this.m_oCustomizeMenu.MenuShowing += new QMenuCancelEventHandler(this.CustomizeMenu_MenuShowing);
      this.m_oCustomizeMenu.MenuShowed += new QMenuEventHandler(this.CustomizeMenu_MenuShowed);
      this.m_oCustomizeMenu.PaintMenuItem += new QPaintMenuItemEventHandler(this.CustomizeMenu_PaintMenuItem);
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.CustomizeMenu_VisibleChanged), (object) this.m_oCustomizeMenu, "VisibleChanged"));
    }

    internal QFloatingMenu CustomizeMenu
    {
      get
      {
        this.SecureCustomizeMenu();
        return this.m_oCustomizeMenu;
      }
    }

    internal bool CustomizeMenuVisible => this.m_oCustomizeMenu != null && this.m_oCustomizeMenu.Visible;

    public virtual QToolBarHost GetAvailableHost(
      Form parent,
      QToolBarHost currentHost,
      Point screenPoint)
    {
      if (parent == null)
        return (QToolBarHost) null;
      if (currentHost != null && currentHost.IsAvailableForPoint(screenPoint))
        return currentHost;
      for (int index = 0; index < parent.Controls.Count; ++index)
      {
        if (parent.Controls[index] is QToolBarHost && parent.Controls[index].Visible && parent.Controls[index].Enabled)
        {
          QToolBarHost control = (QToolBarHost) parent.Controls[index];
          if (control.IsAvailableForPoint(screenPoint))
            return control;
        }
      }
      return (QToolBarHost) null;
    }

    public void DockToolBar(
      QToolBarHost host,
      int rowIndex,
      int toolBarIndex,
      int requestedPosition)
    {
      if (host == null)
        return;
      QToolBarHost toolBarHost = this.ToolBar.ToolBarHost;
      bool flag1 = rowIndex < 0;
      bool flag2 = toolBarIndex < 0;
      QToolBarRow toolBarRow = host.Rows.GetByPositionIndex(rowIndex);
      if (this.ToolBar.ToolBarHost == host && this.ToolBar.ParentToolBarRow != null)
      {
        int positionIndex = this.ToolBar.ParentToolBarRow.PositionIndex;
        if (toolBarRow == null && positionIndex != rowIndex || flag1)
        {
          if (!flag1 && this.ToolBar.ParentToolBarRow.VisibleToolBarCount == 1)
          {
            toolBarRow = this.ToolBar.ParentToolBarRow;
            this.ToolBar.ParentToolBarRow.SetPositionIndex(rowIndex, true);
          }
          else
          {
            toolBarRow = new QToolBarRow();
            host.Rows.Insert(rowIndex, toolBarRow);
          }
        }
        if (toolBarRow == this.ToolBar.ParentToolBarRow)
        {
          if (!flag2)
            this.ToolBar.SetToolBarPositionIndex(toolBarIndex, true);
        }
        else
          toolBarRow.Insert(toolBarIndex, this.ToolBar);
      }
      else
      {
        if (flag1 && flag2 && host.Rows.Count > 0)
          toolBarRow = host.Rows[host.Rows.Count - 1];
        else if (toolBarRow == null)
        {
          toolBarRow = new QToolBarRow();
          host.Rows.Insert(rowIndex, toolBarRow);
        }
        toolBarRow.Insert(toolBarIndex, this.ToolBar);
      }
      if (toolBarHost != host)
      {
        if (toolBarHost != null)
        {
          toolBarHost.SuspendLayout();
          toolBarHost.Controls.Remove((Control) this.ToolBar);
          toolBarHost.ResumeLayout(false);
        }
        host.SuspendLayout();
        host.Controls.Add((Control) this.ToolBar);
        host.ResumeLayout(false);
      }
      this.ToolBar.OriginalToolBarRow = (QToolBarRow) null;
      this.ToolBar.UserRequestedPosition = requestedPosition;
      this.ToolBar.ParentToolBarRow.SetAsFirstPriority(this.ToolBar);
      this.ToolBar.ToolBarHost.LayoutToolBarHost((IQToolBar) null);
      if (toolBarHost == null || toolBarHost == host)
        return;
      toolBarHost.LayoutToolBarHost((IQToolBar) null);
    }

    public bool FloatToolBar(Point point)
    {
      if (!this.ToolBar.ToolBarConfiguration.CanFloat)
        return false;
      if (!this.ToolBar.IsFloating)
      {
        if (this.ToolBar.ToolBarForm == null)
          this.ToolBar.ToolBarForm = new QToolBarForm(this.ToolBar, this.ToolBar.OwnerWindow);
        if (this.ToolBar.ParentToolBarRow != null)
          this.ToolBar.ParentToolBarRow.Remove(this.ToolBar);
        this.ToolBar.OriginalToolBarRow = (QToolBarRow) null;
        this.ToolBar.ToolBarForm.Owner = this.ToolBar is QControl ? ((QControl) this.ToolBar).ParentForm : (Form) null;
        this.ToolBar.ToolBarForm.ColorScheme = this.ToolBar.ColorScheme;
        this.ToolBar.ToolBarForm.Controls.Add((Control) this.ToolBar);
        this.ToolBar.ToolBarForm.Location = point;
        this.ToolBar.ToolBarForm.Show();
      }
      else
        this.ToolBar.ToolBarForm.Location = point;
      return true;
    }

    public void MoveToolBar(Point proposedScreenPoint)
    {
      Point point = new Point(proposedScreenPoint.X - this.DragStartOffSet.X, proposedScreenPoint.Y - this.DragStartOffSet.Y);
      Point location = this.ToolBar.Bounds.Location;
      QToolBarHost toolBarHost = this.ToolBar.ToolBarHost;
      QToolBarHost availableHost = this.GetAvailableHost(this.ToolBar.IsFloating ? this.ToolBar.ToolBarForm.Owner : ((QControl) this.ToolBar).ParentForm, toolBarHost, proposedScreenPoint);
      if (availableHost == null)
      {
        if (this.ToolBar.IsFloating)
          this.ToolBar.ToolBarForm.SetBounds(point.X, point.Y, 0, 0, BoundsSpecified.Location);
        else
          this.FloatToolBar(point);
      }
      else
      {
        if (this.ToolBar.IsFloating)
        {
          this.ToolBar.ToolBarForm.Visible = false;
          this.ToolBar.ToolBarForm.Controls.Remove((Control) this.ToolBar);
        }
        QToolBarRowPosition toolBarRowPosition = availableHost.GetToolBarRowPosition(proposedScreenPoint);
        Point client = availableHost.PointToClient(point);
        if (toolBarRowPosition.ToolBarRow == null)
        {
          int index = toolBarRowPosition.PositionType == QToolBarRowPositionType.AfterToolBarRow ? availableHost.Rows.PositionIndexForAdd : 0;
          int num = this.ToolBar.ParentToolBarRow == null || !availableHost.Rows.Contains(this.ToolBar.ParentToolBarRow) ? -1 : this.ToolBar.ParentToolBarRow.PositionIndex;
          if (this.ToolBar.ParentToolBarRow == null || this.ToolBar.ParentToolBarRow.VisibleToolBarCount > 1 || index == availableHost.Rows.FirstPositionIndex && num != availableHost.Rows.FirstPositionIndex || index == availableHost.Rows.PositionIndexForAdd && num != availableHost.Rows.LastPositionIndex)
          {
            QToolBarRow toolBarRow = new QToolBarRow();
            availableHost.Rows.Insert(index, toolBarRow);
            toolBarRow.Add(this.ToolBar);
          }
        }
        else
        {
          if (toolBarRowPosition.PositionType == QToolBarRowPositionType.OnToolBarRow && (toolBarRowPosition.ToolBarRow.IsStretched || this.ToolBar.Stretched))
            toolBarRowPosition.PositionType = QToolBarRowPositionType.BeforeToolBarRow;
          if (toolBarRowPosition.PositionType == QToolBarRowPositionType.BeforeToolBarRow)
          {
            if (toolBarRowPosition.ToolBarRow != this.ToolBar.ParentToolBarRow || this.ToolBar.ParentToolBarRow != null && this.ToolBar.ParentToolBarRow.VisibleToolBarCount > 1)
            {
              QToolBarRow toolBarRow = new QToolBarRow();
              availableHost.Rows.Insert(toolBarRowPosition.ToolBarRowIndex, toolBarRow);
              toolBarRow.Add(this.ToolBar);
            }
          }
          else if (toolBarRowPosition.PositionType == QToolBarRowPositionType.AfterToolBarRow)
          {
            if (toolBarRowPosition.ToolBarRow != this.ToolBar.ParentToolBarRow || this.ToolBar.ParentToolBarRow != null && this.ToolBar.ParentToolBarRow.VisibleToolBarCount > 1)
            {
              QToolBarRow toolBarRow = new QToolBarRow();
              availableHost.Rows.Insert(toolBarRowPosition.ToolBarRowIndex + 1, toolBarRow);
              toolBarRow.Add(this.ToolBar);
            }
          }
          else if (toolBarRowPosition.PositionType == QToolBarRowPositionType.OnToolBarRow)
          {
            int toolBarInsertIndex = toolBarRowPosition.ToolBarRow.GetToolBarInsertIndex(client);
            int barPositionIndex = this.ToolBar.ToolBarPositionIndex;
            if (toolBarRowPosition.ToolBarRow == this.ToolBar.ParentToolBarRow)
            {
              if (toolBarInsertIndex > barPositionIndex && !this.ToolBar.ToolBarHost.Initializing)
                --toolBarInsertIndex;
              if (toolBarInsertIndex != barPositionIndex)
                this.ToolBar.SetToolBarPositionIndex(toolBarInsertIndex, true);
            }
            else
              toolBarRowPosition.ToolBarRow.Insert(toolBarInsertIndex, this.ToolBar);
          }
        }
        this.ToolBar.ParentToolBarRow.SetAsFirstPriority(this.ToolBar);
        if (toolBarHost != availableHost)
        {
          this.ToolBar.OriginalToolBarRow = (QToolBarRow) null;
          if (toolBarHost != null)
          {
            toolBarHost.SuspendLayout();
            toolBarHost.Controls.Remove((Control) this.ToolBar);
            toolBarHost.ResumeLayout(false);
          }
          this.ToolBar.LayoutToolBar(new Rectangle(0, 0, 0, 0), availableHost.UsedOrientation, QToolBarLayoutFlags.None);
          availableHost.SuspendLayout();
          availableHost.Controls.Add((Control) this.ToolBar);
          availableHost.ResumeLayout(false);
        }
        this.ProposedBounds = new Rectangle(client.X, client.Y, this.ToolBar.Bounds.Width, this.ToolBar.Bounds.Height);
        this.UserRequestedPosition = this.ToolBar.Horizontal ? this.ProposedBounds.Left : this.ProposedBounds.Top;
        this.ToolBar.ToolBarHost.LayoutToolBarHost(this.ToolBar);
        if (toolBarHost != null && toolBarHost != availableHost)
        {
          toolBarHost.LayoutToolBarHost((IQToolBar) null);
          if (toolBarHost.Dock == DockStyle.Right && toolBarHost.Width == 0 && toolBarHost.Parent != null)
            toolBarHost.Parent.PerformLayout((Control) toolBarHost, "Bounds");
        }
        if (toolBarHost == availableHost)
          this.ToolBar.ToolBarHost.Refresh();
        this.UserRequestedPosition = this.ToolBar.Horizontal ? this.ToolBar.Bounds.Left : this.ToolBar.Bounds.Top;
        if (!(this.ToolBar.Bounds.Location != location))
          return;
        this.ToolBar.OriginalToolBarRow = (QToolBarRow) null;
      }
    }

    private static void GetOriginalItem(
      QMenuItem menuItem,
      out QMenuItem originalItem,
      out QCommandCopyType copyType)
    {
      originalItem = (QMenuItem) null;
      copyType = QCommandCopyType.ExecuteOriginal;
      if (menuItem == null)
        return;
      object additionalProperty1 = menuItem.GetAdditionalProperty(2);
      object additionalProperty2 = menuItem.GetAdditionalProperty(3);
      originalItem = additionalProperty1 as QMenuItem;
      if (!(additionalProperty2 is QCommandCopyType qcommandCopyType))
        return;
      copyType = qcommandCopyType;
    }

    private void CustomizeMenu_MenuItemSelected(object sender, QMenuEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
        this.MenuItemContainer.RaiseMenuItemSelected(new QMenuEventArgs(originalItem, e.ActivationType, e.Expanded));
      else
        this.MenuItemContainer.RaiseMenuItemSelected(e);
    }

    private void CustomizeMenu_MenuItemActivating(object sender, QMenuCancelEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
      {
        QMenuCancelEventArgs e1 = new QMenuCancelEventArgs(originalItem, e.ActivationType, e.Cancel, e.Expanded);
        this.MenuItemContainer.RaiseMenuItemActivating(e1);
        e.Cancel = e1.Cancel;
      }
      else
        this.MenuItemContainer.RaiseMenuItemActivating(e);
    }

    private void m_oCustomizeMenu_MenuItemMouseUp(object sender, QMenuMouseEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.CustomizeOriginal)
      {
        e.MenuItem.Checked = !e.MenuItem.Checked;
        originalItem.VisibleWhenPersonalized = e.MenuItem.Checked;
        this.MenuItemContainer.RaiseMenuItemMouseUp(e);
      }
      else if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
        this.MenuItemContainer.RaiseMenuItemMouseUp(new QMenuMouseEventArgs(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta), originalItem));
      else
        this.MenuItemContainer.RaiseMenuItemMouseUp(e);
    }

    private void m_oCustomizeMenu_MenuItemMouseDown(object sender, QMenuMouseEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.CustomizeOriginal)
        this.MenuItemContainer.RaiseMenuItemMouseDown(e);
      else if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
        this.MenuItemContainer.RaiseMenuItemMouseDown(new QMenuMouseEventArgs(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta), originalItem));
      else
        this.MenuItemContainer.RaiseMenuItemMouseDown(e);
    }

    private void CustomizeMenu_MenuItemActivated(object sender, QMenuEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.CustomizeOriginal)
        this.MenuItemContainer.RaiseMenuItemActivated(e);
      else if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
        this.MenuItemContainer.RaiseMenuItemActivated(new QMenuEventArgs(originalItem, e.ActivationType, e.Expanded));
      else
        this.MenuItemContainer.RaiseMenuItemActivated(e);
    }

    private void CustomizeMenu_MenuShowing(object sender, QMenuCancelEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
      {
        QMenuCancelEventArgs e1 = new QMenuCancelEventArgs(originalItem, e.ActivationType, e.Cancel, e.Expanded);
        this.MenuItemContainer.RaiseMenuShowing(e1);
        e.Cancel = e1.Cancel;
      }
      else
        this.MenuItemContainer.RaiseMenuShowing(e);
    }

    private void CustomizeMenu_MenuShowed(object sender, QMenuEventArgs e)
    {
      QMenuItem originalItem = (QMenuItem) null;
      QCommandCopyType copyType = QCommandCopyType.ExecuteOriginal;
      QToolBarAction.GetOriginalItem(e.MenuItem, out originalItem, out copyType);
      if (this.MenuItemContainer == null)
        return;
      if (originalItem != null && copyType == QCommandCopyType.ExecuteOriginal)
        this.MenuItemContainer.RaiseMenuShowed(new QMenuEventArgs(originalItem, e.ActivationType, e.Expanded));
      else
        this.MenuItemContainer.RaiseMenuShowed(e);
    }

    private void CustomizeMenu_PaintMenuItem(object sender, QPaintMenuItemEventArgs e)
    {
      if (this.MenuItemContainer == null)
        return;
      this.MenuItemContainer.RaisePaintMenuItem(e);
    }

    private void CustomizeMenu_VisibleChanged(object sender, EventArgs e)
    {
      if (this.MenuItemContainer == null)
        return;
      if ((sender as Control).Visible)
        this.MenuItemContainer.RaiseCustomizeMenuShowed(EventArgs.Empty);
      else
        this.MenuItemContainer.RaiseCustomizeMenuClosed(EventArgs.Empty);
    }
  }
}
