// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QContextMenuControlListener
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QContextMenuControlListener : NativeWindow
  {
    private QContextMenu m_oContextMenu;
    private Control m_oControl;
    private bool m_bDesignMode;
    private bool m_bAssigned;
    private QWeakEventConsumerCollection m_oEventConsumers;

    public QContextMenuControlListener(QContextMenu contextMenu, Control control, bool designMode)
    {
      this.m_bDesignMode = designMode;
      this.m_oControl = control;
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_Disposed), (object) this.m_oControl, "Disposed"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_HandleCreated), (object) this.m_oControl, "HandleCreated"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_HandleDestroyed), (object) this.m_oControl, "HandleDestroyed"));
      this.m_oContextMenu = contextMenu;
      this.AssignHandle();
    }

    private void AssignHandle()
    {
      if (!this.m_oControl.IsHandleCreated || this.m_bAssigned || this.m_bDesignMode)
        return;
      this.AssignHandle(this.m_oControl.Handle);
      this.m_bAssigned = true;
    }

    public override void ReleaseHandle()
    {
      if (!this.m_bAssigned)
        return;
      base.ReleaseHandle();
      this.m_bAssigned = false;
    }

    public Control Control => this.m_oControl;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 123)
      {
        int x = (int) (short) NativeHelper.LowOrderWord(m.LParam.ToInt32());
        int y = (int) (short) NativeHelper.HighOrderWord(m.LParam.ToInt32());
        Point location = Point.Empty;
        location = x != -1 || y != -1 ? new Point(x, y) : this.m_oControl.PointToScreen(new Point(this.m_oControl.Width / 2, this.m_oControl.Height / 2));
        this.m_oContextMenu.ShowMenu(location, (IWin32Window) this.m_oControl.TopLevelControl);
        if (this.m_oContextMenu.SuppressDefaultContextMenu)
          return;
      }
      base.WndProc(ref m);
    }

    private void Control_Disposed(object sender, EventArgs e) => this.m_oContextMenu.RemoveListener(this.m_oControl);

    private void Control_HandleDestroyed(object sender, EventArgs e) => this.ReleaseHandle();

    private void Control_HandleCreated(object sender, EventArgs e)
    {
      this.ReleaseHandle();
      this.AssignHandle();
    }

    ~QContextMenuControlListener()
    {
      if (this.m_oControl == null)
        return;
      int num = this.m_oControl.IsDisposed ? 1 : 0;
    }
  }
}
