// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeItemBaseDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QCompositeItemBaseDesigner : ComponentDesigner, IQDesigner
  {
    private bool m_bIgnoreCreation;
    private DesignerVerbCollection m_oVerbs;
    private QGeneralDesignerHandler m_oGeneralHandler;

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.m_oGeneralHandler = new QGeneralDesignerHandler((ComponentDesigner) this, component);
      if (this.ChangeService != null)
      {
        this.ChangeService.ComponentAdding += new ComponentEventHandler(this.ChangeService_ComponentAdding);
        this.ChangeService.ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
      }
      if (this.SelectionService != null)
        this.SelectionService.SelectionChanged += new EventHandler(this.SelectionService_SelectionChanged);
      ((IComponentChangeService) this.GetService(typeof (IComponentChangeService))).ComponentRemoving += new ComponentEventHandler(this.ChangeService_ComponentRemoving);
    }

    public virtual Type[] GetCreationTypes() => (Type[]) null;

    public QCompositeItemBase Item => this.Component as QCompositeItemBase;

    public IComponentChangeService ChangeService => this.GetService(typeof (IComponentChangeService)) as IComponentChangeService;

    public ISelectionService SelectionService => this.GetService(typeof (ISelectionService)) as ISelectionService;

    public IQPart GetDesignableParentPart()
    {
      IQPart parentPart = this.Item.ParentPart;
      while (parentPart != null && (!(parentPart is IComponent) || ((IComponent) parentPart).Site == null))
        parentPart = parentPart.ParentPart;
      return parentPart;
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        if (this.m_oVerbs == null)
        {
          this.m_oVerbs = new DesignerVerbCollection();
          this.m_oVerbs.Add(new DesignerVerb("Select Parent", new EventHandler(this.VerbSelectParent)));
          Type[] creationTypes = this.GetCreationTypes();
          if (creationTypes != null)
          {
            for (int index = 0; index < creationTypes.Length; ++index)
              this.m_oVerbs.Add((DesignerVerb) new QDesignerObjectCreationVerb((IDesigner) this, creationTypes[index]));
          }
        }
        return this.m_oVerbs;
      }
    }

    private void VerbSelectParent(object sender, EventArgs e)
    {
      IQPart designableParentPart = this.GetDesignableParentPart();
      if (this.SelectionService == null)
        return;
      if (designableParentPart != null)
      {
        this.SelectionService.SetSelectedComponents((ICollection) new object[1]
        {
          (object) designableParentPart
        }, SelectionTypes.Replace);
      }
      else
      {
        if (this.Item.Composite.ParentControl == null)
          return;
        this.SelectionService.SetSelectedComponents((ICollection) new object[1]
        {
          (object) this.Item.Composite.ParentControl
        }, SelectionTypes.Replace);
      }
    }

    public override ICollection AssociatedComponents => QDesignableItemContainerHelper.AddAssociatedComponents(this.Component, (ICollection) new ArrayList());

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_oGeneralHandler != null)
          this.m_oGeneralHandler.Dispose();
        if (this.ChangeService != null)
        {
          this.ChangeService.ComponentAdding -= new ComponentEventHandler(this.ChangeService_ComponentAdding);
          this.ChangeService.ComponentRemoving -= new ComponentEventHandler(this.ChangeService_ComponentRemoving);
        }
        if (this.SelectionService != null)
          this.SelectionService.SelectionChanged -= new EventHandler(this.SelectionService_SelectionChanged);
      }
      base.Dispose(disposing);
    }

    protected void RemoveFromParent()
    {
      if (this.Item == null || this.Item.ParentCollection == null)
        return;
      this.Item.ParentCollection.Remove((IQPart) this.Item);
    }

    protected virtual void HandleSelectionChanged()
    {
    }

    private void SelectionService_SelectionChanged(object sender, EventArgs e) => this.HandleSelectionChanged();

    private void ChangeService_ComponentAdding(object sender, ComponentEventArgs e)
    {
      if (this.m_bIgnoreCreation || QCompositeDesignerHelper.IgnoreAddingComponent(e.Component) || this.Item == null || this.SelectionService == null || this.SelectionService.PrimarySelection != this.Component || !(e.Component is QCompositeItemBase))
        return;
      QCompositeItemBase component = e.Component as QCompositeItemBase;
      if (this.Item.DesignablePartsCollection != null)
      {
        if (component == null || component.ParentCollection != null)
          return;
        this.Item.DesignablePartsCollection.Add((IQPart) component, true);
      }
      else
        throw new InvalidOperationException(QResources.GetException("QCompositeItemBaseDesigner_CannotAddItem", (object) this.Item.GetType().Name));
    }

    private void ChangeService_ComponentRemoving(object sender, ComponentEventArgs e)
    {
      if (e.Component != this.Component)
        return;
      this.RemoveFromParent();
      QDesignableItemContainerHelper.RemoveFromPossibleHost(this.Component);
      QDesignableItemContainerHelper.DestroyAssociatedComponents(this.Component, this.AssociatedComponents, false);
    }

    public void NotifyComponentCreation(object creatingSource, bool isCreatingComponent) => this.m_bIgnoreCreation = isCreatingComponent;
  }
}
