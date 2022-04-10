// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMdiClientSub
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QMdiClientSub : NativeWindow
  {
    private Control m_oMdiClientForm;
    private QWeakEventConsumerCollection m_oEventConsumers;

    public QMdiClientSub(Control mdiClientForm)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oMdiClientForm = mdiClientForm;
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.MdiClientForm_HandleCreated), (object) this.m_oMdiClientForm, "HandleCreated"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.MdiClientForm_HandleDestroyed), (object) this.m_oMdiClientForm, "HandleDestroyed"));
      if (!this.m_oMdiClientForm.Created)
        return;
      this.AssignHandle(this.m_oMdiClientForm.Handle);
    }

    public Control MdiClientForm => this.m_oMdiClientForm;

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 560:
          break;
        case 564:
          break;
        default:
          base.WndProc(ref m);
          break;
      }
    }

    private void MdiClientForm_HandleCreated(object sender, EventArgs e) => this.AssignHandle(this.m_oMdiClientForm.Handle);

    private void MdiClientForm_HandleDestroyed(object sender, EventArgs e) => this.ReleaseHandle();
  }
}
