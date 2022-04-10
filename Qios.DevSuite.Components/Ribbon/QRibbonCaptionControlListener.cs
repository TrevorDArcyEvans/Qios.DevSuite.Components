// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionControlListener
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  internal sealed class QRibbonCaptionControlListener : NativeWindow
  {
    private bool m_bAssigned;
    private QRibbonCaption m_oRibbonCaption;
    private Control m_oControl;
    private QWeakEventConsumerCollection m_oEventConsumers;
    private bool m_bDesignMode;

    public QRibbonCaptionControlListener(
      QRibbonCaption ribbonCaption,
      Control control,
      bool designMode)
    {
      this.m_bDesignMode = designMode;
      this.m_oControl = control;
      this.m_oRibbonCaption = ribbonCaption;
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_HandleCreated), (object) this.m_oControl, "HandleCreated"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_HandleDestroyed), (object) this.m_oControl, "HandleDestroyed"));
      this.AssignHandle();
    }

    private void AssignHandle()
    {
      if (!this.m_oControl.IsHandleCreated || this.m_bAssigned)
        return;
      this.AssignHandle(this.m_oControl.Handle);
      this.AdjustControlStyle();
      this.m_bAssigned = true;
    }

    internal void AdjustControlStyle()
    {
      if (!this.m_oControl.IsHandleCreated)
        return;
      int windowLong = Qios.DevSuite.Components.NativeMethods.GetWindowLong(this.m_oControl.Handle, -16);
      int num1 = !this.m_oRibbonCaption.ControlBox ? windowLong & -524289 : windowLong | 524288;
      int num2 = !this.m_oRibbonCaption.MinimizeBox ? num1 & -131073 : num1 | 131072;
      Qios.DevSuite.Components.NativeMethods.SetWindowLong(this.m_oControl.Handle, -16, !this.m_oRibbonCaption.MaximizeBox ? num2 & -65537 : num2 | 65536);
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
      if (this.m_oRibbonCaption != null && !this.m_oRibbonCaption.IsDisposed)
      {
        if (!this.m_bDesignMode && m.Msg == 132)
        {
          if (this.m_oControl is Form oControl && NativeHelper.GetCurrentFormState(oControl) == FormWindowState.Minimized)
          {
            base.WndProc(ref m);
            return;
          }
          Point client = this.m_oRibbonCaption.PointToClient(new Point(m.LParam.ToInt32()));
          if (this.m_oRibbonCaption.ClientRectangle.Contains(client))
          {
            if (this.m_oRibbonCaption.PointIsOnIconArea(client))
            {
              m.Result = new IntPtr(3);
              return;
            }
            if (!this.m_oRibbonCaption.PointIsOnItemArea(client))
            {
              m.Result = new IntPtr(2);
              return;
            }
          }
        }
        else if (!this.m_bDesignMode && m.Msg == 6)
        {
          int wparam = (int) m.WParam;
          this.m_oRibbonCaption.Active = wparam == 1 || wparam == 2;
        }
        else if (m.Msg == 5)
        {
          this.m_oRibbonCaption.PutMaximized(m.WParam.ToInt32() == 2);
        }
        else
        {
          if (m.Msg == 128)
          {
            base.WndProc(ref m);
            this.m_oRibbonCaption.AdjustToControlIcon();
            return;
          }
          if (m.Msg == 71)
          {
            if (((int) ((Qios.DevSuite.Components.NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (Qios.DevSuite.Components.NativeMethods.WINDOWPOS))).flags & 32) == 32)
              this.m_oRibbonCaption.UpdateTextFromAttachedControl();
          }
          else if (!this.m_bDesignMode && m.Msg == 134)
          {
            if (this.m_oControl is Form oControl && oControl.MdiParent != null)
              this.m_oRibbonCaption.Active = m.WParam.ToInt32() != 0;
          }
          else if (m.Msg == 36)
          {
            base.WndProc(ref m);
            return;
          }
        }
      }
      base.WndProc(ref m);
    }

    private void Control_HandleDestroyed(object sender, EventArgs e) => this.ReleaseHandle();

    private void Control_HandleCreated(object sender, EventArgs e)
    {
      this.ReleaseHandle();
      this.AssignHandle();
    }
  }
}
