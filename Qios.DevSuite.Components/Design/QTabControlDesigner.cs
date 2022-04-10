// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QTabControlDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QTabControlDesigner : ParentControlDesigner
  {
    private QTabControl m_oTabControl;
    private bool m_bActivateAfterAdd;
    private DesignerTransaction m_oDragDropTransaction;
    private QTabButton m_oDragDropButton;
    private ISelectionService m_oSelectionService;
    private IDesignerHost m_oDesignerHost;
    private IComponentChangeService m_oChangeService;
    private QGeneralDesignerHandler m_oGeneralHandler;
    private DesignerVerbCollection m_oVerbs;
    private QColorSchemeXmlHandler m_oColorSchemeXmlHandler;
    private Control m_oDragSource;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      this.m_oSelectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      if (this.m_oSelectionService != null)
        this.m_oSelectionService.SelectionChanged += new EventHandler(this.SelectionService_SelectionChanged);
      this.m_oDesignerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
      this.m_oChangeService = this.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
      if (this.m_oChangeService != null)
        this.m_oChangeService.ComponentAdded += new ComponentEventHandler(this.ChangeService_ComponentAdded);
      this.m_oTabControl = component as QTabControl;
      if (this.m_oTabControl == null)
        return;
      this.m_oColorSchemeXmlHandler = new QColorSchemeXmlHandler((ComponentDesigner) this, (IDesignerHost) this.GetService(typeof (IDesignerHost)), this.m_oTabControl.ColorScheme);
      this.m_oTabControl.TabButtonDragging += new QTabButtonDragEventHandler(this.TabControl_TabButtonDragging);
      this.m_oTabControl.ControlAdded += new ControlEventHandler(this.TabControl_ControlAdded);
    }

    public QTabControl TabControl => (QTabControl) this.Control;

    internal Control DragSource
    {
      get
      {
        if (this.m_oDragSource == null)
        {
          this.m_oDragSource = new Control();
          this.m_oDragSource.QueryContinueDrag += new QueryContinueDragEventHandler(this.DragSource_QueryContinueDrag);
        }
        return this.m_oDragSource;
      }
    }

    protected override bool GetHitTest(Point point) => false;

    public override DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oVerbs == null)
        {
          this.m_oVerbs = new DesignerVerbCollection();
          this.m_oVerbs.Add(this.AddTabPageVerb);
          this.m_oVerbs.AddRange(this.m_oColorSchemeXmlHandler.Verbs);
        }
        return this.m_oVerbs;
      }
    }

    protected virtual DesignerVerb AddTabPageVerb => new DesignerVerb(QResources.GetGeneral("QTabControlDesigner_AddTabPage"), new EventHandler(this.AddTabPageVerbClick));

    protected virtual System.Type TabPageType => typeof (QTabPage);

    protected void AddTabPage()
    {
      DesignerTransaction transaction = this.m_oDesignerHost.CreateTransaction();
      try
      {
        QTabPage component = this.m_oDesignerHost.CreateComponent(this.TabPageType) as QTabPage;
        component.Text = component.Site.Name;
        this.TabControl.Controls.Add((Control) component);
      }
      catch
      {
        transaction.Cancel();
        throw;
      }
      transaction.Commit();
    }

    private void AddTabPageVerbClick(object sender, EventArgs e) => this.AddTabPage();

    protected override void OnMouseDragBegin(int x, int y)
    {
      Point client = this.m_oTabControl.PointToClient(new Point(x, y));
      this.TabControl.HandleMouseDown(new MouseEventArgs(MouseButtons.Left, 0, client.X, client.Y, 0));
      base.OnMouseDragBegin(x, y);
    }

    protected override void OnMouseDragEnd(bool cancel)
    {
      Point client = this.m_oTabControl.PointToClient(Control.MousePosition);
      this.TabControl.HandleMouseUp(new MouseEventArgs(MouseButtons.Left, 0, client.X, client.Y, 0));
      base.OnMouseDragEnd(cancel);
    }

    protected override void OnMouseDragMove(int x, int y)
    {
      Point client = this.m_oTabControl.PointToClient(new Point(x, y));
      this.m_oTabControl.HandleMouseMoveBeginDrag(new MouseEventArgs(MouseButtons.Left, 0, client.X, client.Y, 0), this.DragSource);
      base.OnMouseDragMove(x, y);
    }

    protected override void OnDragEnter(DragEventArgs de)
    {
      if (this.m_oTabControl.CanHandleDragData(de.Data))
        this.m_oTabControl.HandleDragEnter(de);
      else
        base.OnDragEnter(de);
    }

    protected override void OnDragOver(DragEventArgs de)
    {
      if (this.m_oTabControl.CanHandleDragData(de.Data))
        this.m_oTabControl.HandleDragOver(de);
      else
        base.OnDragOver(de);
    }

    protected override void OnDragDrop(DragEventArgs de)
    {
      if (this.m_oTabControl.CanHandleDragData(de.Data))
        this.m_oTabControl.HandleDragDrop(de);
      else
        base.OnDragDrop(de);
    }

    protected override void OnDragLeave(EventArgs e)
    {
      this.m_oTabControl.HandleDragLeave();
      base.OnDragLeave(e);
    }

    private void DragSource_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
    {
      this.m_oTabControl.HandleQueryContinueDrag(e);
      if (this.m_oDragDropTransaction == null || e.Action != DragAction.Cancel && e.Action != DragAction.Drop)
        return;
      this.m_oChangeService.OnComponentChanged((object) (this.m_oDragDropButton.Control as QTabPage), (MemberDescriptor) null, (object) null, (object) null);
      this.m_oDragDropTransaction.Commit();
      this.m_oDragDropTransaction = (DesignerTransaction) null;
      this.m_oDragDropButton = (QTabButton) null;
    }

    private void TabControl_TabButtonDragging(object sender, QTabButtonDragEventArgs e)
    {
      if (this.m_oDragDropTransaction != null)
      {
        e.Cancel = true;
      }
      else
      {
        this.m_oDragDropTransaction = this.m_oDesignerHost.CreateTransaction();
        this.m_oDragDropButton = e.SourceTabButton;
        this.m_oChangeService.OnComponentChanging((object) (this.m_oDragDropButton.Control as QTabPage), (MemberDescriptor) null);
      }
    }

    private void TabControl_ControlAdded(object sender, ControlEventArgs e)
    {
      if (e.Control is QTabPage control && this.m_oTabControl != null && control.TabControl == this.m_oTabControl && this.m_oTabControl.ActiveTabPage != control)
      {
        if (this.m_bActivateAfterAdd)
        {
          control.ButtonOrder = -1;
          this.m_oTabControl.ActivateTabPage(control);
        }
        else
          control.Visible = false;
      }
      this.m_bActivateAfterAdd = false;
    }

    private void ChangeService_ComponentAdded(object sender, ComponentEventArgs e)
    {
      QTabPage component = e.Component as QTabPage;
      if (this.m_oSelectionService == null || this.m_oSelectionService.PrimarySelection != this.Component || component == null)
        return;
      this.m_bActivateAfterAdd = true;
    }

    private void SelectionService_SelectionChanged(object sender, EventArgs e)
    {
      QTabPage page = this.m_oSelectionService != null ? this.m_oSelectionService.PrimarySelection as QTabPage : (QTabPage) null;
      if (this.m_oTabControl == null || page == null || !this.m_oTabControl.Controls.Contains((Control) page) || this.m_oTabControl.ActiveTabPageRuntime == page)
        return;
      this.m_oTabControl.ActivateTabPage(page);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      if (this.m_oGeneralHandler != null)
        this.m_oGeneralHandler.Dispose();
      if (this.m_oDragSource == null)
        return;
      this.m_oDragSource.QueryContinueDrag -= new QueryContinueDragEventHandler(this.DragSource_QueryContinueDrag);
    }
  }
}
