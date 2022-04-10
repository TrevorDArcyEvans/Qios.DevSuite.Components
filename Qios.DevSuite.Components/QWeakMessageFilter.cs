// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakMessageFilter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QWeakMessageFilter : QWeakReference, IMessageFilter
  {
    public QWeakMessageFilter(object actualMessageFilter)
      : base(actualMessageFilter)
    {
      if (!(actualMessageFilter is Component component))
        return;
      component.Disposed += new EventHandler(this.Component_Disposed);
    }

    private void Component_Disposed(object sender, EventArgs e) => Application.RemoveMessageFilter((IMessageFilter) this);

    public virtual bool PreFilterMessage(ref Message m) => !this.Finalized && this.IsAlive && this.Target != null && (this.Target as IMessageFilter).PreFilterMessage(ref m);
  }
}
