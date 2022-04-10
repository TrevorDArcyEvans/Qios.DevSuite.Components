// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMainFormSub
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QMainFormSub : NativeWindow
  {
    private IQMainMenu m_oMainMenu;
    private Form m_oMainForm;
    private QWeakEventConsumerCollection m_oEventConsumers;

    public QMainFormSub(IQMainMenu mainMenu, Form mainForm)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oMainMenu = mainMenu;
      this.m_oMainForm = mainForm;
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.MainForm_HandleCreated), (object) this.m_oMainForm, "HandleCreated"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.MainForm_HandleDestroyed), (object) this.m_oMainForm, "HandleDestroyed"));
      if (!this.m_oMainForm.IsHandleCreated)
        return;
      this.AssignHandle(this.m_oMainForm.Handle);
    }

    public Form MainForm => this.m_oMainForm;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 274)
      {
        if ((m.WParam.ToInt32() & 65520) == 61696)
        {
          this.m_oMainMenu.HandleMenukeyDown(ref m);
          return;
        }
      }
      else if (m.Msg == 6 && (int) m.WParam == 0)
        this.m_oMainMenu.HandleDeactivate(m.LParam);
      base.WndProc(ref m);
    }

    private void MainForm_HandleCreated(object sender, EventArgs e) => this.AssignHandle(this.m_oMainForm.Handle);

    private void MainForm_HandleDestroyed(object sender, EventArgs e) => this.ReleaseHandle();
  }
}
