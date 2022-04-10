// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QKeyboardMessageFilter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QKeyboardMessageFilter : QWeakMessageFilter
  {
    private bool m_bInstalled;

    public QKeyboardMessageFilter(IQKeyboardMessageFilter keyboardMessageFilter)
      : base((object) keyboardMessageFilter)
    {
    }

    public bool Installed
    {
      get => this.m_bInstalled;
      set
      {
        if (value)
          this.Install();
        else
          this.Uninstall();
      }
    }

    public void Install()
    {
      if (this.m_bInstalled)
        return;
      Application.AddMessageFilter((IMessageFilter) this);
      this.m_bInstalled = true;
    }

    public void Uninstall()
    {
      if (!this.m_bInstalled)
        return;
      Application.RemoveMessageFilter((IMessageFilter) this);
      this.m_bInstalled = false;
    }

    public override bool PreFilterMessage(ref Message m)
    {
      if ((m.Msg == 256 || m.Msg == 257 || m.Msg == 260 || m.Msg == 261) && !this.Finalized && this.IsAlive && this.Target != null)
      {
        if (this.Target is IQKeyboardMessageFilter target)
        {
          switch (m.Msg)
          {
            case 256:
            case 260:
              Control controlFromHandle1 = QControlHelper.GetFirstControlFromHandle(m.HWnd);
              return target.HandleKeyDown((Keys) (int) m.WParam, controlFromHandle1, m);
            case 257:
            case 261:
              Control controlFromHandle2 = QControlHelper.GetFirstControlFromHandle(m.HWnd);
              return target.HandleKeyUp((Keys) (int) m.WParam, controlFromHandle2, m);
          }
        }
      }
      return false;
    }
  }
}
