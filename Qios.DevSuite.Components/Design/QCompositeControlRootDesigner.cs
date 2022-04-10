// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeControlRootDesigner
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  public class QCompositeControlRootDesigner : QContainerControlDocumentDesigner, IQDesigner
  {
    private QCompositeControlDesignerHelper m_oDesignerHelper;

    public override void Initialize(IComponent component)
    {
      this.m_oDesignerHelper = new QCompositeControlDesignerHelper((ComponentDesigner) this, component as IQCompositeContainer);
      this.m_oDesignerHelper.CreationTypes = this.GetCreationTypes();
      base.Initialize(component);
      base.DrawGrid = false;
    }

    public virtual System.Type[] GetCreationTypes() => QCompositeDesignerHelper.DefaultCompositeCreationTypes;

    [Browsable(false)]
    protected override bool DrawGrid
    {
      get => false;
      set
      {
      }
    }

    public override DesignerVerbCollection Verbs => this.m_oDesignerHelper.Verbs;

    protected override void PreFilterProperties(IDictionary properties)
    {
      base.PreFilterProperties(properties);
      properties.Add((object) "DrawOutlines", (object) TypeDescriptor.CreateProperty(typeof (QCompositeControlRootDesigner), "DrawOutlines", typeof (bool), (Attribute) new DefaultValueAttribute(true), (Attribute) new DescriptionAttribute("Indicates whether the outline of items must be drawn"), (Attribute) CategoryAttribute.Design, (Attribute) DesignOnlyAttribute.Yes));
      properties.Remove((object) "DrawGrid");
      properties.Remove((object) "GridSize");
    }

    public bool DrawOutlines
    {
      get => this.m_oDesignerHelper.DrawOutlines;
      set => this.m_oDesignerHelper.DrawOutlines = value;
    }

    protected override void OnSetCursor()
    {
      if (!this.m_oDesignerHelper.ShouldSetCursor())
        base.OnSetCursor();
      else
        this.m_oDesignerHelper.SetCursor();
    }

    protected override void OnMouseDragBegin(int x, int y)
    {
      if (!this.m_oDesignerHelper.ShouldHandleMouseDragBegin(x, y))
        base.OnMouseDragBegin(x, y);
      else
        this.m_oDesignerHelper.HandleMouseDragBegin(x, y);
    }

    protected override void OnMouseDragMove(int x, int y)
    {
      if (!this.m_oDesignerHelper.ShouldHandleMouseDragMove(x, y))
        base.OnMouseDragMove(x, y);
      else
        this.m_oDesignerHelper.HandleMouseDragMove(x, y);
    }

    protected override void OnMouseDragEnd(bool cancel)
    {
      if (!this.m_oDesignerHelper.ShouldHandleMouseDragEnd(cancel))
        base.OnMouseDragEnd(cancel);
      else
        this.m_oDesignerHelper.HandleMouseDragEnd(cancel);
    }

    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
      base.OnPaintAdornments(pe);
      this.m_oDesignerHelper.HandlePaintAdornments(pe);
    }

    protected override void WndProc(ref Message m)
    {
      if (this.m_oDesignerHelper != null && this.m_oDesignerHelper.ShouldCallDefaultWndProc(ref m))
        this.DefWndProc(ref m);
      else
        base.WndProc(ref m);
    }

    public override ICollection AssociatedComponents => this.m_oDesignerHelper.AssociatedComponents;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.m_oDesignerHelper.Dispose();
      base.Dispose(disposing);
    }

    public void NotifyComponentCreation(object creatingSource, bool isCreatingComponent) => this.m_oDesignerHelper.IgnoreCreation = isCreatingComponent;
  }
}
