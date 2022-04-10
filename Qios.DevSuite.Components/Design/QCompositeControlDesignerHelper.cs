// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeControlDesignerHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  public class QCompositeControlDesignerHelper : IDisposable
  {
    private DesignerVerbCollection m_oVerbs;
    private bool m_bDrawOutlines = true;
    private System.Type[] m_aCreationTypes;
    private bool m_bIgnoreCreation;
    private bool m_bHadSelection;
    private ISite m_oSite;
    private ComponentDesigner m_oDesigner;
    private Control m_oControl;
    private IQCompositeContainer m_oCompositeContainer;
    private QGeneralDesignerHandler m_oGeneralHandler;

    internal QCompositeControlDesignerHelper(
      ComponentDesigner designer,
      IQCompositeContainer container)
    {
      this.m_oCompositeContainer = container;
      this.m_oControl = this.m_oCompositeContainer.Control;
      this.m_oSite = this.m_oControl.Site;
      this.m_oDesigner = designer;
      this.m_oGeneralHandler = new QGeneralDesignerHandler(designer, (IComponent) this.m_oControl);
      if (this.SelectionService != null)
        this.SelectionService.SelectionChanged += new EventHandler(this.SelectionService_SelectionChanged);
      if (this.ChangeService != null)
      {
        this.ChangeService.ComponentAdding += new ComponentEventHandler(this.ChangeService_ComponentAdding);
        this.ChangeService.ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
      }
      if (this.DesignerHost == null)
        return;
      this.DesignerHost.TransactionClosed += new DesignerTransactionCloseEventHandler(this.Host_TransactionClosed);
    }

    public bool DrawOutlines
    {
      get => this.m_bDrawOutlines;
      set => this.m_bDrawOutlines = value;
    }

    public bool IgnoreCreation
    {
      get => this.m_bIgnoreCreation;
      set => this.m_bIgnoreCreation = value;
    }

    public virtual System.Type[] CreationTypes
    {
      get => this.m_aCreationTypes;
      set => this.m_aCreationTypes = value;
    }

    public DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oVerbs == null)
        {
          this.m_oVerbs = new DesignerVerbCollection();
          if (this.m_aCreationTypes != null && this.m_oCompositeContainer.AssociatedComponents != null)
          {
            for (int index = 0; index < this.m_aCreationTypes.Length; ++index)
              this.m_oVerbs.Add((DesignerVerb) new QDesignerObjectCreationVerb((IDesigner) this.m_oDesigner, this.m_aCreationTypes[index]));
          }
        }
        return this.m_oVerbs;
      }
    }

    public IComponentChangeService ChangeService => this.m_oSite.GetService(typeof (IComponentChangeService)) as IComponentChangeService;

    public IDesignerHost DesignerHost => this.m_oSite.GetService(typeof (IDesignerHost)) as IDesignerHost;

    public ISelectionService SelectionService => this.m_oSite.GetService(typeof (ISelectionService)) as ISelectionService;

    public ICollection AssociatedComponents => QDesignableItemContainerHelper.AddAssociatedComponents((IComponent) this.m_oControl, (ICollection) new ArrayList());

    public bool ShouldSetCursor()
    {
      Point mousePosition = Control.MousePosition;
      return this.ShouldHandleMouseDragBegin(mousePosition.X, mousePosition.Y);
    }

    public void SetCursor() => Cursor.Current = Cursors.Hand;

    public bool ShouldHandleMouseDragBegin(int x, int y)
    {
      Point client = this.m_oControl.PointToClient(new Point(x, y));
      return (this.ContainsSelectedControl() || this.ContainsAllSelectedParts()) && this.GetDesignablePartAtPoint(client.X, client.Y) != null;
    }

    public void HandleMouseDragBegin(int x, int y)
    {
      Point client = this.m_oControl.PointToClient(new Point(x, y));
      this.SetSelectedDesignablePart(client.X, client.Y, SelectionTypes.Click);
    }

    public bool ShouldHandleMouseDragMove(int x, int y) => this.ContainsAllSelectedParts();

    public void HandleMouseDragMove(int x, int y)
    {
    }

    public bool ShouldHandleMouseDragEnd(bool cancel) => this.ContainsAllSelectedParts();

    public void HandleMouseDragEnd(bool cancel)
    {
    }

    private IQPart GetDesignablePartAtPoint(int x, int y)
    {
      IQPart designablePartAtPoint = QPartHelper.GetItemAtPointRecursive((IQPart) this.m_oCompositeContainer.Composite, new Point(x, y));
      while (designablePartAtPoint != null && (!(designablePartAtPoint is IComponent) || ((IComponent) designablePartAtPoint).Site == null))
        designablePartAtPoint = designablePartAtPoint.ParentPart;
      return designablePartAtPoint;
    }

    private bool ContainsAllSelectedParts()
    {
      if (this.SelectionService == null || this.SelectionService.PrimarySelection == null)
        return false;
      foreach (object selectedComponent in (IEnumerable) this.SelectionService.GetSelectedComponents())
      {
        if (!(selectedComponent is IQPart qpart) || qpart.DisplayParent != this.m_oCompositeContainer.Composite)
          return false;
      }
      return true;
    }

    private bool ContainsSelectedControl() => this.SelectionService != null && this.SelectionService.GetComponentSelected((object) this.m_oControl);

    private bool SetSelectedDesignablePart(int x, int y, SelectionTypes selectionType)
    {
      IQPart designablePartAtPoint = this.GetDesignablePartAtPoint(x, y);
      bool flag = false;
      if (designablePartAtPoint != null)
      {
        this.SelectionService.SetSelectedComponents((ICollection) new object[1]
        {
          (object) designablePartAtPoint
        }, selectionType);
        flag = true;
      }
      return flag;
    }

    private void PaintDesignerBounds(ICollection components, Pen pen, PaintEventArgs pe)
    {
      if (components == null)
        return;
      foreach (object component in (IEnumerable) components)
      {
        if (component is QCompositeItemBase qcompositeItemBase && !((IQPart) qcompositeItemBase).IsSystemPart)
        {
          ICollection designablePartsCollection = (ICollection) qcompositeItemBase.DesignablePartsCollection;
          Rectangle scrollCorrectedBounds = qcompositeItemBase.CalculatedProperties.CachedScrollCorrectedBounds;
          --scrollCorrectedBounds.Width;
          --scrollCorrectedBounds.Height;
          pe.Graphics.DrawRectangle(pen, scrollCorrectedBounds);
          this.PaintDesignerBounds(designablePartsCollection, pen, pe);
        }
      }
    }

    public void HandlePaintAdornments(PaintEventArgs pe)
    {
      if (this.m_bDrawOutlines)
      {
        HatchBrush hatchBrush = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(128, 0, 0, (int) byte.MaxValue), Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
        Pen pen = new Pen((Brush) hatchBrush, 1f);
        this.PaintDesignerBounds(this.AssociatedComponents, pen, pe);
        pen.Dispose();
        hatchBrush.Dispose();
      }
      if (this.SelectionService == null)
        return;
      foreach (object selectedComponent in (IEnumerable) this.SelectionService.GetSelectedComponents())
      {
        if (selectedComponent is IQPart qpart && qpart.DisplayParent == this.m_oCompositeContainer.Composite)
        {
          Rectangle scrollCorrectedBounds = qpart.CalculatedProperties.CachedScrollCorrectedBounds;
          HatchBrush hatchBrush = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(192, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(192, 0, 0, 0));
          Pen pen = new Pen((Brush) hatchBrush, 2f);
          pen.Alignment = PenAlignment.Outset;
          pe.Graphics.DrawRectangle(pen, scrollCorrectedBounds);
          pen.Dispose();
          hatchBrush.Dispose();
        }
      }
    }

    public virtual bool ShouldCallDefaultWndProc(ref Message m) => m.Msg == 516 || m.Msg == 164;

    private void ChangeService_ComponentAdding(object sender, ComponentEventArgs e)
    {
      if (this.m_bIgnoreCreation || QCompositeDesignerHelper.IgnoreAddingComponent(e.Component) || this.SelectionService == null || this.SelectionService.PrimarySelection != this.m_oControl || !(e.Component is QCompositeItemBase) || !(e.Component is QCompositeItemBase component) || component.ParentCollection != null)
        return;
      this.m_oCompositeContainer.Items.Add((IQPart) component, true);
    }

    private void ChangeService_ComponentRemoving(object sender, ComponentEventArgs e)
    {
      if (e.Component != this.m_oControl)
        return;
      QDesignableItemContainerHelper.RemoveFromPossibleHost((IComponent) this.m_oControl);
      QDesignableItemContainerHelper.DestroyAssociatedComponents((IComponent) this.m_oControl, this.AssociatedComponents, false);
    }

    private void Host_TransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
    {
      if (!(this.m_oControl is IQManagedLayoutParent oControl))
        return;
      oControl.HandleChildObjectChanged(true, Rectangle.Empty);
    }

    private void SelectionService_SelectionChanged(object sender, EventArgs e)
    {
      if (this.m_bHadSelection)
      {
        this.m_oControl.Invalidate();
        this.m_bHadSelection = this.ContainsAllSelectedParts();
      }
      else
      {
        this.m_bHadSelection = this.ContainsAllSelectedParts();
        if (!this.m_bHadSelection)
          return;
        this.m_oControl.Invalidate();
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.m_oGeneralHandler != null)
        this.m_oGeneralHandler.Dispose();
      if (this.ChangeService != null)
      {
        this.ChangeService.ComponentAdding -= new ComponentEventHandler(this.ChangeService_ComponentAdding);
        this.ChangeService.ComponentRemoving -= new ComponentEventHandler(this.ChangeService_ComponentRemoving);
      }
      if (this.DesignerHost != null)
        this.DesignerHost.TransactionClosed -= new DesignerTransactionCloseEventHandler(this.Host_TransactionClosed);
      if (this.SelectionService == null)
        return;
      this.SelectionService.SelectionChanged -= new EventHandler(this.SelectionService_SelectionChanged);
    }

    ~QCompositeControlDesignerHelper() => this.Dispose(false);
  }
}
