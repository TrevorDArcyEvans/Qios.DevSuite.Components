// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMdiChildSub
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class QMdiChildSub : NativeWindow
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private QMainFormOverride m_oMainFormOverride;
    private IQMainMenu m_oMainMenu;
    private Form m_oMdiChild;
    private int m_iLastHandledParameter;

    public QMdiChildSub(QMainFormOverride mainFormOverride, IQMainMenu mainMenu, Form mdiChild)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oMainFormOverride = mainFormOverride;
      this.m_oMainMenu = mainMenu;
      this.m_oMdiChild = mdiChild;
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.MdiChild_HandleCreated), (object) this.m_oMdiChild, "HandleCreated"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.MdiChild_HandleDestroyed), (object) this.m_oMdiChild, "HandleDestroyed"));
      if (!mdiChild.Created)
        return;
      this.AssignHandle(mdiChild.Handle);
    }

    public Form MdiChild => this.m_oMdiChild;

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 5)
        return;
      int int32 = m.WParam.ToInt32();
      if (int32 == this.m_iLastHandledParameter)
        return;
      this.m_iLastHandledParameter = int32;
      switch (int32)
      {
        case 0:
        case 1:
        case 2:
          this.m_oMainMenu.HandleMdiChildWindowStateChanged(int32);
          break;
      }
    }

    private void MdiChild_HandleCreated(object sender, EventArgs e)
    {
      this.m_oMainFormOverride.AddMdiChildSub(this.m_oMdiChild);
      this.AssignHandle(this.m_oMdiChild.Handle);
    }

    private void MdiChild_HandleDestroyed(object sender, EventArgs e)
    {
      this.m_oMainFormOverride.RemoveMdiChildSub(this);
      this.ReleaseHandle();
    }
  }
}
