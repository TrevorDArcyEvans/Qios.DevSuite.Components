// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakMouseEventConsumer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QWeakMouseEventConsumer : QWeakEventConsumer
  {
    public QWeakMouseEventConsumer(
      Delegate targetDelegate,
      object sourceObject,
      string sourceEvent)
      : base(targetDelegate, sourceObject, sourceEvent)
    {
    }

    protected override Delegate CreateAttachingDelegate() => (Delegate) new MouseEventHandler(this.MouseMethodInvoked);

    private void MouseMethodInvoked(object sender, MouseEventArgs e)
    {
      if (!this.TargetDelegate.TargetIsFinalized)
        this.InvokeTarget(sender, (object) e);
      else
        this.DetachEvent();
    }
  }
}
