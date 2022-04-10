// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMenuItemContainerDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QMenuItemContainerDesigner : ParentControlDesigner
  {
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private bool m_bLastHitTestValid;
    private bool m_bDragDrop;
    private QMenuItemContainerDesigner.QCommandControlInfo[] m_oSelectionContainers;
    private QMenuItemContainer m_oMenuItemContainer;
    private QMenuItemXmlHandler m_oMenuItemXmlHandler;
    private IComponentChangeService m_oChangeService;
    private IDesignerHost m_oDesignerHost;
    private ISelectionService m_oSelectionService;
    private IToolboxService m_oToolBoxService;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      if (component == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (component)));
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      this.DrawGrid = false;
      this.m_oMenuItemContainer = (QMenuItemContainer) component;
      this.m_oMenuItemXmlHandler = new QMenuItemXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oMenuItemContainer.Items);
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oMenuItemContainer.ColorScheme);
      this.m_oChangeService = (IComponentChangeService) this.GetService(typeof (IComponentChangeService));
      this.m_oChangeService.ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
      this.m_oChangeService.ComponentAdding += new ComponentEventHandler(this.ChangeService_ComponentAdding);
      this.m_oChangeService.ComponentAdded += new ComponentEventHandler(this.ChangeService_ComponentAdded);
      this.m_oChangeService.ComponentRemoved += new ComponentEventHandler(this.ChangeService_ComponentRemoved);
      this.m_oChangeService.ComponentChanging += new ComponentChangingEventHandler(this.ChangeService_ComponentChanging);
      this.m_oChangeService.ComponentChanged += new ComponentChangedEventHandler(this.ChangeService_ComponentChanged);
      this.m_oDesignerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
      this.m_oSelectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      this.m_oToolBoxService = this.GetService(typeof (IToolboxService)) as IToolboxService;
    }

    private QMenuItem DraggingOverMenuItem
    {
      get => this.m_oMenuItemContainer.DesignerMenuItem;
      set
      {
        if (this.m_oMenuItemContainer.DesignerMenuItem == value)
          return;
        this.m_oMenuItemContainer.DesignerMenuItem = value;
      }
    }

    protected override bool GetHitTest(Point point)
    {
      if (this.m_oSelectionService.PrimarySelection != this.m_oMenuItemContainer)
        return false;
      if (this.m_oMenuItemContainer is QExplorerBar)
      {
        Point client = (this.m_oMenuItemContainer as QExplorerBar).ContainerPointToClient(point);
        QMenuItem itemAtPosition = this.m_oMenuItemContainer.GetItemAtPosition(client.X, client.Y);
        if (itemAtPosition != null && (itemAtPosition as QExplorerItem).ItemType == QExplorerItemType.GroupItem)
        {
          this.m_bLastHitTestValid = true;
          return true;
        }
        if (!this.m_oMenuItemContainer.ClientRectangle.Contains(client))
        {
          this.m_bLastHitTestValid = true;
          return true;
        }
      }
      if (this.m_bLastHitTestValid)
        this.m_oMenuItemContainer.HandleMouseLeave();
      this.m_bLastHitTestValid = false;
      return false;
    }

    private void QMenuItemContainer_MouseLeave()
    {
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        DesignerVerbCollection verbs = new DesignerVerbCollection();
        verbs.AddRange(this.m_oMenuItemXmlHandler.Verbs);
        verbs.AddRange(this.m_oColorSchemeXmlHandler.Verbs);
        return verbs;
      }
    }

    public override ICollection AssociatedComponents
    {
      get
      {
        if (!(this.Component is QMenuItemContainer))
          return base.AssociatedComponents;
        QMenuItemCollection items = ((QMenuItemContainer) this.Component).Items;
        ICollection associatedComponents1 = base.AssociatedComponents;
        int index1 = 0;
        int length = 0;
        if (items != null)
          length += items.Count;
        if (associatedComponents1 != null)
          length += associatedComponents1.Count;
        object[] associatedComponents2 = new object[length];
        if (items != null)
        {
          for (int index2 = 0; index2 < items.Count; ++index2)
          {
            associatedComponents2[index2] = (object) items[index2];
            index1 = index2;
          }
        }
        if (associatedComponents1 != null)
        {
          foreach (object obj in (IEnumerable) associatedComponents1)
          {
            ++index1;
            associatedComponents2[index1] = obj;
          }
        }
        return (ICollection) associatedComponents2;
      }
    }

    protected override IComponent[] CreateToolCore(
      ToolboxItem tool,
      int x,
      int y,
      int width,
      int height,
      bool hasLocation,
      bool hasSize)
    {
      return this.m_bDragDrop ? base.CreateToolCore(tool, x, y, width, height, hasLocation, hasSize) : (IComponent[]) null;
    }

    protected override void OnSetCursor() => Cursor.Current = Cursors.Default;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.m_oChangeService != null)
      {
        if (this.m_oGeneralHandler != null)
          this.m_oGeneralHandler.Dispose();
        this.m_oChangeService.ComponentRemoved -= new ComponentEventHandler(this.ChangeService_ComponentRemoved);
        this.m_oChangeService.ComponentRemoving -= new ComponentEventHandler(this.ChangeService_ComponentRemoving);
        this.m_oChangeService.ComponentAdding -= new ComponentEventHandler(this.ChangeService_ComponentAdding);
        this.m_oChangeService.ComponentAdded -= new ComponentEventHandler(this.ChangeService_ComponentAdded);
        this.m_oChangeService.ComponentChanging -= new ComponentChangingEventHandler(this.ChangeService_ComponentChanging);
        this.m_oChangeService.ComponentChanged -= new ComponentChangedEventHandler(this.ChangeService_ComponentChanged);
      }
      base.Dispose(disposing);
    }

    private void ChangeService_ComponentRemoving(object sender, ComponentEventArgs e)
    {
      if (e.Component != this.Component)
        return;
      IDesignerHost service = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      foreach (IComponent associatedComponent in (IEnumerable) this.AssociatedComponents)
      {
        if (!(associatedComponent is Control control) || !control.IsDisposed)
          service.DestroyComponent(associatedComponent);
      }
    }

    private void ChangeService_ComponentAdding(object sender, ComponentEventArgs e)
    {
    }

    protected override void OnDragEnter(DragEventArgs de)
    {
      base.OnDragEnter(de);
      ICollection selectedComponents = this.m_oSelectionService.GetSelectedComponents();
      this.m_oSelectionContainers = new QMenuItemContainerDesigner.QCommandControlInfo[selectedComponents.Count];
      IEnumerator enumerator = selectedComponents.GetEnumerator();
      int index = 0;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is Control current && current.Parent is QCommandControlContainer parent)
          this.m_oSelectionContainers[index] = new QMenuItemContainerDesigner.QCommandControlInfo(parent, current);
        ++index;
      }
    }

    protected override void OnDragDrop(DragEventArgs de)
    {
      try
      {
        this.m_bDragDrop = true;
        base.OnDragDrop(de);
        if (this.m_oDesignerHost.InTransaction)
        {
          this.m_oDesignerHost.TransactionClosed += new DesignerTransactionCloseEventHandler(this.m_oDesignerHost_TransactionClosed);
        }
        else
        {
          this.SecureControls(this.m_oSelectionService.GetSelectedComponents(), this.DraggingOverMenuItem);
          this.DraggingOverMenuItem = (QMenuItem) null;
          this.m_oMenuItemContainer.Invalidate();
        }
      }
      finally
      {
        if (!this.m_oDesignerHost.InTransaction)
        {
          this.m_bDragDrop = false;
          this.m_oSelectionContainers = (QMenuItemContainerDesigner.QCommandControlInfo[]) null;
        }
      }
    }

    private void m_oDesignerHost_TransactionClosed(
      object sender,
      DesignerTransactionCloseEventArgs e)
    {
      this.m_oDesignerHost.TransactionClosed -= new DesignerTransactionCloseEventHandler(this.m_oDesignerHost_TransactionClosed);
      try
      {
        this.SecureControls(this.m_oSelectionService.GetSelectedComponents(), this.DraggingOverMenuItem);
      }
      finally
      {
        this.DraggingOverMenuItem = (QMenuItem) null;
        this.m_bDragDrop = false;
        this.m_oSelectionContainers = (QMenuItemContainerDesigner.QCommandControlInfo[]) null;
      }
    }

    protected override void OnDragOver(DragEventArgs de)
    {
      Point point = !(this.m_oMenuItemContainer is QExplorerBar menuItemContainer) ? this.m_oMenuItemContainer.PointToClient(new Point(de.X, de.Y)) : menuItemContainer.ContainerPointToClient(new Point(de.X, de.Y));
      this.DraggingOverMenuItem = this.m_oMenuItemContainer.GetItemAtPosition(point.X, point.Y);
      base.OnDragOver(de);
    }

    protected override void OnDragLeave(EventArgs e)
    {
      this.DraggingOverMenuItem = (QMenuItem) null;
      base.OnDragLeave(e);
      this.m_oMenuItemContainer.Invalidate();
    }

    private QMenuItemContainerDesigner.QCommandControlInfo FindControlInfo(
      Control control)
    {
      if (this.m_oSelectionContainers == null || this.m_oSelectionContainers.Length == 0)
        return (QMenuItemContainerDesigner.QCommandControlInfo) null;
      for (int index = 0; index < this.m_oSelectionContainers.Length; ++index)
      {
        if (this.m_oSelectionContainers[index] != null && this.m_oSelectionContainers[index].Control == control)
          return this.m_oSelectionContainers[index];
      }
      return (QMenuItemContainerDesigner.QCommandControlInfo) null;
    }

    private void SecureControls(ICollection collection, QMenuItem menuItem)
    {
      if (collection == null || collection.Count == 0 || menuItem == null)
        return;
      IEnumerator enumerator = collection.GetEnumerator();
      int index = 0;
      while (enumerator.MoveNext())
      {
        if (enumerator.Current is Control current)
        {
          this.SecureControl(current, menuItem);
          if (this.m_bDragDrop && this.m_oSelectionContainers != null && this.m_oSelectionContainers.Length == collection.Count && this.m_oSelectionContainers[index] != null)
            this.m_oSelectionContainers[index] = (QMenuItemContainerDesigner.QCommandControlInfo) null;
          ++index;
        }
      }
    }

    private void SecureControl(Control control, QMenuItem menuItem)
    {
      if (control == null || menuItem == null)
        return;
      DesignerTransaction transaction = this.m_oDesignerHost.CreateTransaction();
      this.m_oChangeService.OnComponentChanging((object) this.m_oMenuItemContainer, (MemberDescriptor) null);
      this.m_oChangeService.OnComponentChanging((object) menuItem, (MemberDescriptor) null);
      this.m_oChangeService.OnComponentChanging((object) control, (MemberDescriptor) null);
      try
      {
        QMenuItemContainerDesigner.QCommandControlInfo controlInfo = this.FindControlInfo(control);
        if ((control.Parent == null || control.Parent is QMenuItemContainer) && menuItem.Control == null)
        {
          QCommandControlContainer component = this.m_oDesignerHost.CreateComponent(typeof (QCommandControlContainer)) as QCommandControlContainer;
          control.Parent = (Control) null;
          component.Controls.Add(control);
          if (controlInfo == null)
          {
            control.Location = new Point(5, 5);
            component.Size = new Size(control.Size.Width + 10, control.Size.Height + 10);
          }
          else
          {
            control.Location = controlInfo.Location;
            Size size = new Size(control.Location.X + control.Width + 5, control.Location.Y + control.Height + 5);
            component.Size = size;
          }
          this.m_oDesignerHost.Container.Add((IComponent) component);
        }
        else if (menuItem.Control != null)
        {
          control.Parent = (Control) menuItem.Control;
          control.Location = controlInfo != null ? controlInfo.Location : new Point(5, 5);
          Size size = new Size(Math.Max(control.Location.X + control.Width + 5, menuItem.Control.Width), Math.Max(control.Location.Y + control.Height + 5, menuItem.Control.Height));
          if (!menuItem.Control.Size.Equals((object) size))
          {
            menuItem.Control.PreferredSize = size;
            menuItem.Control.Size = size;
          }
        }
        if (control.Parent is QCommandControlContainer)
        {
          menuItem.Control = control.Parent as QCommandControlContainer;
          control.BringToFront();
        }
        menuItem.SetControlParent();
        this.m_oMenuItemContainer.PerformLayout();
        this.m_oChangeService.OnComponentChanged((object) control, (MemberDescriptor) null, (object) null, (object) null);
        this.m_oChangeService.OnComponentChanged((object) menuItem, (MemberDescriptor) null, (object) null, (object) null);
        this.m_oChangeService.OnComponentChanged((object) this.m_oMenuItemContainer, (MemberDescriptor) null, (object) null, (object) null);
        transaction.Commit();
      }
      catch
      {
        transaction.Cancel();
        throw;
      }
    }

    private void RemoveUnusedContainer(QCommandControlContainer container, Control excludeControl)
    {
      if (container == null || container.Controls.Count != 0 && (excludeControl == null || container.Controls.Count != 1 || container.Controls[0] != excludeControl))
        return;
      QMenuItem controlContainer = this.GetItemWithControlContainer(container, this.m_oMenuItemContainer.Items);
      if (controlContainer == null)
        return;
      DesignerTransaction transaction = this.m_oDesignerHost.CreateTransaction();
      try
      {
        container.Parent = (Control) null;
        controlContainer.Control = (QCommandControlContainer) null;
        this.m_oDesignerHost.Container.Remove((IComponent) container);
        transaction.Commit();
      }
      catch
      {
        transaction.Cancel();
        throw;
      }
    }

    private QMenuItem GetItemWithControlContainer(
      QCommandControlContainer container,
      QMenuItemCollection collection)
    {
      if (collection == null)
        return (QMenuItem) null;
      for (int index = 0; index < collection.Count; ++index)
      {
        if (collection[index].Control == container)
          return collection[index];
        QMenuItem controlContainer = this.GetItemWithControlContainer(container, collection[index].MenuItems);
        if (controlContainer != null)
          return controlContainer;
      }
      return (QMenuItem) null;
    }

    private void ChangeService_ComponentAdded(object sender, ComponentEventArgs e)
    {
    }

    private void ChangeService_ComponentRemoved(object sender, ComponentEventArgs e)
    {
      Control component1 = e.Component as Control;
      if (component1 != null)
      {
        QCommandControlContainer parent = component1.Parent as QCommandControlContainer;
      }
      if (e.Component is QCommandControlContainer component2)
      {
        QMenuItem controlContainer = this.GetItemWithControlContainer(component2, this.m_oMenuItemContainer.Items);
        if (controlContainer != null)
          controlContainer.Control = (QCommandControlContainer) null;
      }
      this.m_oMenuItemContainer.PerformLayout();
      this.m_oMenuItemContainer.Invalidate();
    }

    private void ChangeService_ComponentChanging(object sender, ComponentChangingEventArgs e)
    {
      if (!(e.Component is QCommandControlContainer component) || component.Disposing || component.IsDisposed)
        return;
      component.SizeChanged += new EventHandler(this.tmp_oContainer_SizeChanged);
    }

    private void ChangeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
    {
      if (!(e.Component is QCommandControlContainer component) || component.Disposing || component.IsDisposed)
        return;
      component.SizeChanged -= new EventHandler(this.tmp_oContainer_SizeChanged);
      if (e.Member != null && (e.Member.Name == "Size" || e.Member.Name == "Width" || e.Member.Name == "Height" || e.Member.Name == "Bounds"))
        component.PreferredSize = component.Size;
      this.m_oMenuItemContainer.PerformLayout();
      this.m_oMenuItemContainer.Invalidate();
    }

    private void tmp_oContainer_SizeChanged(object sender, EventArgs e) => this.m_oMenuItemContainer.Invalidate();

    private class QCommandControlInfo
    {
      private Point m_oLocation;
      private Control m_oControl;
      private QCommandControlContainer m_oContainer;

      internal QCommandControlInfo(QCommandControlContainer container, Control control)
      {
        this.m_oContainer = container;
        this.m_oControl = control;
        if (this.m_oControl != null)
        {
          if (this.m_oControl.Parent is QCommandControlContainer)
            this.m_oLocation = this.m_oControl.Location;
          else
            this.m_oLocation = new Point(5, 5);
        }
        else
          this.m_oLocation = Point.Empty;
      }

      internal QCommandControlContainer Container => this.m_oContainer;

      internal Control Control => this.m_oControl;

      internal Point Location => this.m_oLocation;
    }
  }
}
