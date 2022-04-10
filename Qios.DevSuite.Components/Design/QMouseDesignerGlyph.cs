// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMouseDesignerGlyph
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace Qios.DevSuite.Components.Design
{
  internal class QMouseDesignerGlyph : Glyph
  {
    private ComponentDesigner m_oDesigner;
    private Control m_oControl;
    private BehaviorService m_oBehaviorService;
    private IQMouseDesignerHandler m_oHandler;
    private ISelectionService m_oSelectionService;
    private Adorner m_oAdorner;

    public QMouseDesignerGlyph(
      ComponentDesigner designer,
      IQMouseDesignerHandler handler,
      BehaviorService behaviorService,
      Control control)
      : base((System.Windows.Forms.Design.Behavior.Behavior) new QMouseDesignerGlyph.QMouseBehavior(behaviorService))
    {
      ((QMouseDesignerGlyph.QMouseBehavior) this.Behavior).Glyph = this;
      this.m_oDesigner = designer;
      this.m_oControl = control;
      this.m_oBehaviorService = behaviorService;
      this.m_oHandler = handler;
      this.m_oSelectionService = this.m_oControl.Site.GetService(typeof (ISelectionService)) as ISelectionService;
    }

    public void AddToService()
    {
      this.m_oAdorner = new Adorner();
      this.m_oAdorner.Glyphs.Add((Glyph) this);
      this.m_oBehaviorService.Adorners.Add(this.m_oAdorner);
    }

    public void RemoveFromService() => this.m_oBehaviorService.Adorners.Remove(this.m_oAdorner);

    public IQMouseDesignerHandler Handler
    {
      get => this.m_oHandler;
      set => this.m_oHandler = value;
    }

    public override Rectangle Bounds => this.m_oBehaviorService.ControlRectInAdornerWindow(this.m_oControl);

    public override Cursor GetHitTest(Point p) => (Cursor) null;

    public override void Paint(PaintEventArgs pe)
    {
    }

    public class QMouseBehavior : System.Windows.Forms.Design.Behavior.Behavior
    {
      private int DEBUGCOUNTER;

      public QMouseBehavior()
      {
      }

      public QMouseBehavior(BehaviorService behaviorService)
        : base(true, behaviorService)
      {
      }

      public QMouseDesignerGlyph Glyph { get; set; }

      public override void OnDragDrop(Glyph g, DragEventArgs e) => base.OnDragDrop(g, e);

      public override void OnDragEnter(Glyph g, DragEventArgs e) => base.OnDragEnter(g, e);

      public override void OnDragLeave(Glyph g, EventArgs e) => base.OnDragLeave(g, e);

      public override void OnDragOver(Glyph g, DragEventArgs e) => base.OnDragOver(g, e);

      public override void OnGiveFeedback(Glyph g, GiveFeedbackEventArgs e) => base.OnGiveFeedback(g, e);
    }
  }
}
