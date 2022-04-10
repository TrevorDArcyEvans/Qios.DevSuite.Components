// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlSelector
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QControlSelector : Control, IQWeakEventPublisher
  {
    private static Cursor m_oCursor;
    private bool m_bWeakEventHandlers = true;
    private Rectangle m_oLastDrawnRectangle = Rectangle.Empty;
    private Color m_oLastDrawnColor = Color.Empty;
    private Control m_oSelectedControl;
    private Control m_oHoveringControl;
    private QWeakDelegate m_oHoveringControlChangedDelegate;
    private QWeakDelegate m_oSelectedControlChangedDelegate;

    public QControlSelector()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      if (!(QControlSelector.m_oCursor == (Cursor) null))
        return;
      QControlSelector.m_oCursor = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Cursors.Crosshair.cur"));
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the user hovers over an other control while the QControlSelector is selecting.")]
    public event EventHandler HoveringControlChanged
    {
      add => this.m_oHoveringControlChangedDelegate = QWeakDelegate.Combine(this.m_oHoveringControlChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oHoveringControlChangedDelegate = QWeakDelegate.Remove(this.m_oHoveringControlChangedDelegate, (Delegate) value);
    }

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when the user selects an other control.")]
    public event EventHandler SelectedControlChanged
    {
      add => this.m_oSelectedControlChangedDelegate = QWeakDelegate.Combine(this.m_oHoveringControlChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oSelectedControlChangedDelegate = QWeakDelegate.Remove(this.m_oHoveringControlChangedDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public bool IsSelectingControl => Cursor.Current == QControlSelector.m_oCursor;

    public Control SelectedControl => this.m_oSelectedControl;

    private void PutSelectedControl(Control control, bool raiseEvent)
    {
      this.m_oSelectedControl = control;
      if (!raiseEvent)
        return;
      this.OnSelectedControlChanged(EventArgs.Empty);
    }

    public Control HoveringControl => this.m_oHoveringControl;

    private void PutHoveringControl(Control control, bool raiseEvent)
    {
      if (this.m_oHoveringControl == control)
        return;
      this.m_oHoveringControl = control;
      if (!raiseEvent)
        return;
      this.OnHoveringControlChanged(EventArgs.Empty);
    }

    private void SetCrossHair(bool value)
    {
      if (value)
        Cursor.Current = QControlSelector.m_oCursor;
      else
        Cursor.Current = Cursors.Default;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.SetCrossHair(true);
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.SetCrossHair(false);
      Control oHoveringControl = this.m_oHoveringControl;
      this.PutHoveringControl((Control) null, false);
      this.PutSelectedControl(oHoveringControl, true);
      this.Invalidate();
      this.DrawReversibleRectangle(Rectangle.Empty);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.IsSelectingControl)
        return;
      Control controlFromHandle = QControlHelper.GetFirstControlFromHandle(NativeMethods.WindowFromPoint(new NativeMethods.POINT(Control.MousePosition.X, Control.MousePosition.Y)));
      this.PutHoveringControl(controlFromHandle, true);
      Rectangle rectangle = Rectangle.Empty;
      if (controlFromHandle != null)
        rectangle = controlFromHandle.Parent == null ? controlFromHandle.RectangleToScreen(new Rectangle(Point.Empty, controlFromHandle.Size)) : controlFromHandle.Parent.RectangleToScreen(controlFromHandle.Bounds);
      this.DrawReversibleRectangle(rectangle);
    }

    private void DrawReversibleRectangle(Rectangle rectangle)
    {
      if (rectangle == this.m_oLastDrawnRectangle)
        return;
      if (this.m_oLastDrawnRectangle != Rectangle.Empty)
        ControlPaint.DrawReversibleFrame(this.m_oLastDrawnRectangle, this.m_oLastDrawnColor, FrameStyle.Thick);
      this.m_oLastDrawnRectangle = rectangle;
      this.m_oLastDrawnColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue);
      if (!(this.m_oLastDrawnRectangle != Rectangle.Empty))
        return;
      ControlPaint.DrawReversibleFrame(this.m_oLastDrawnRectangle, this.m_oLastDrawnColor, FrameStyle.Thick);
    }

    protected virtual void OnSelectedControlChanged(EventArgs e) => this.m_oSelectedControlChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oSelectedControlChangedDelegate, (object) this, (object) e);

    protected virtual void OnHoveringControlChanged(EventArgs e) => this.m_oHoveringControlChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oHoveringControlChangedDelegate, (object) this, (object) e);

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.IsSelectingControl)
        return;
      QControlSelector.m_oCursor.Draw(e.Graphics, new Rectangle((int) Math.Round((double) this.ClientRectangle.Width / 2.0 - (double) QControlSelector.m_oCursor.Size.Width / 2.0), (int) Math.Round((double) this.ClientRectangle.Height / 2.0 - (double) QControlSelector.m_oCursor.Size.Height / 2.0), QControlSelector.m_oCursor.Size.Width, QControlSelector.m_oCursor.Size.Height));
    }
  }
}
