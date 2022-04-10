// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockForm
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  internal class QDockForm : Form, IQMenuKeyPassThrough
  {
    private QDockContainer m_oControl;
    private EventHandler m_oActiveControlChangedEventHandler;
    private EventHandler m_oChildControlsChangedEventHandler;
    private QWeakEventConsumerCollection m_oEventConsumers;

    public QDockForm(Form owner)
    {
      this.SuspendLayout();
      this.Owner = owner;
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oActiveControlChangedEventHandler = new EventHandler(this.Control_ActiveControlChanged);
      this.m_oChildControlsChangedEventHandler = new EventHandler(this.Control_ChildControlsChanged);
      this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        if (this.Owner != null)
          createParams.Parent = QControlHelper.GetUndisposedHandle((IWin32Window) this.Owner);
        return createParams;
      }
    }

    public void InitDockForm(QDockContainer control, int x, int y)
    {
      this.m_oControl = control;
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oActiveControlChangedEventHandler, (object) control, "ActiveControlChanged"));
      this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) this.m_oChildControlsChangedEventHandler, (object) control, "ChildControlsChanged"));
      this.ClientSize = control.DockedSize;
      control.SetParent((Control) this, 0, DockStyle.Fill, control.DockedSize, false);
      control.CurrentTopLevelControl = (Control) this;
      this.SetMinimumSize();
      this.SetCloseButton();
      if (this.Owner != null)
        NativeMethods.SetWindowPos(this.Handle, this.Owner.Handle, x, y, this.Width, this.Height, 0U);
      else
        NativeMethods.SetWindowPos(this.Handle, IntPtr.Zero, x, y, this.Width, this.Height, 4U);
      this.UpdateBounds();
    }

    private void SetMinimumSize()
    {
      if (this.m_oControl == null)
        return;
      Size minimumSize = this.m_oControl.MinimumSize;
      minimumSize.Width += SystemInformation.FrameBorderSize.Width * 2;
      minimumSize.Height += SystemInformation.ToolWindowCaptionHeight + SystemInformation.FrameBorderSize.Height * 2;
      this.MinimumSize = minimumSize;
    }

    protected override void SetVisibleCore(bool value)
    {
      base.SetVisibleCore(value);
      this.SetTextToActiveControl();
    }

    internal void SetTextToActiveControl()
    {
      if (this.m_oControl != null && this.m_oControl.CurrentWindow != null)
      {
        this.Text = this.m_oControl.CurrentWindow.Text;
        this.FormBorderStyle = this.m_oControl.CurrentWindow.FormBorderStyleUndocked;
        this.ShowInTaskbar = this.m_oControl.CurrentWindow.ShowInTaskbarUndocked;
        this.Icon = this.m_oControl.CurrentWindow.Icon;
      }
      else
      {
        this.Text = ' '.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        this.Icon = (Icon) null;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (this.m_oControl != null)
      {
        if (disposing)
        {
          this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oActiveControlChangedEventHandler);
          this.m_oEventConsumers.DetachAndRemove((Delegate) this.m_oChildControlsChangedEventHandler);
        }
        this.m_oControl = (QDockContainer) null;
      }
      base.Dispose(disposing);
    }

    Control IQMenuKeyPassThrough.PassToControl => (Control) this.Owner;

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (QMainMenu.PassSCKeyMenu(m, (Control) this.Owner))
        return;
      if (m.Msg == 161)
      {
        this.SetMinimumSize();
        if (NativeMethods.SendMessage(this.Handle, 132, IntPtr.Zero, m.LParam).ToInt32() == 2)
        {
          this.Select();
          this.m_oControl.StartDragging(new Point(Control.MousePosition.X - this.Left, Control.MousePosition.Y - this.Top));
          return;
        }
      }
      base.WndProc(ref m);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      this.SetMinimumSize();
      if (levent.AffectedControl == this.m_oControl && levent.AffectedProperty == "Bounds")
        this.ClientSize = this.m_oControl.Size;
      base.OnLayout(levent);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      if (e.Cancel)
        return;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl && !((QDockControl) this.Controls[index]).RaiseOnClosing(true))
        {
          e.Cancel = true;
          return;
        }
      }
      bool flag = true;
      for (int index = this.Controls.Count - 1; index >= 0; --index)
      {
        if (this.Controls[index] is QDockControl && !((QDockControl) this.Controls[index]).CloseControl(false))
          flag = false;
      }
      if (flag)
      {
        if (this.Owner == null)
          return;
        this.Owner.Activate();
      }
      else
        e.Cancel = true;
    }

    private void SetCloseButton()
    {
      bool flag = true;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl && !((QDockControl) this.Controls[index]).NoControlCanClose())
        {
          flag = false;
          break;
        }
      }
      this.ControlBox = !flag;
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is QDockControl)
          ((QDockControl) this.Controls[index]).RaiseOnClosed(true);
      }
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      if (this.Controls.Count == 0)
      {
        if (this.Owner != null)
          this.Owner.Activate();
        this.Hide();
        this.Dispose();
      }
      base.OnControlRemoved(e);
    }

    private void Control_ChildControlsChanged(object sender, EventArgs e) => this.SetCloseButton();

    private void Control_ActiveControlChanged(object sender, EventArgs e) => this.SetTextToActiveControl();
  }
}
