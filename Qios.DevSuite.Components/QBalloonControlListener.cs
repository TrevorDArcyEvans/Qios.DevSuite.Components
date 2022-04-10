// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QBalloonControlListener
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QBalloonControlListener
  {
    private QWeakEventConsumerCollection m_oEventConsumers;
    private Point m_oSpecifiedLocation = Point.Empty;
    private bool m_bConnected;
    private bool m_bEnabled = true;
    private bool m_bRemoveAfterHide;
    private QBalloon m_oBalloon;
    private Control m_oControl;
    private string m_sText;
    private int m_iInitialDelayTickCount;
    private int m_iAutoPopupDelayTickCount;
    private bool m_bIgnoreWorkingArea;
    private QBalloonWindowPositioning m_ePositioning;

    internal QBalloonControlListener(QBalloon balloon, Control control, string text)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oControl = control;
      this.m_oBalloon = balloon;
      this.m_sText = text;
    }

    internal QBalloonControlListener(
      QBalloon balloon,
      Control control,
      string text,
      bool removeAfterHide)
    {
      this.m_oEventConsumers = new QWeakEventConsumerCollection();
      this.m_oControl = control;
      this.m_oBalloon = balloon;
      this.m_sText = text;
      this.m_bRemoveAfterHide = removeAfterHide;
    }

    internal Point SpecifiedLocation
    {
      get => this.m_oSpecifiedLocation;
      set => this.m_oSpecifiedLocation = value;
    }

    internal bool IgnoreWorkingArea
    {
      get => this.m_bIgnoreWorkingArea;
      set => this.m_bIgnoreWorkingArea = value;
    }

    internal QBalloonWindowPositioning Positioning
    {
      get => this.m_ePositioning;
      set => this.m_ePositioning = value;
    }

    internal bool RemoveAfterHide
    {
      get => this.m_bRemoveAfterHide;
      set => this.m_bRemoveAfterHide = value;
    }

    internal bool Enabled
    {
      get => this.m_bEnabled;
      set => this.m_bEnabled = value;
    }

    internal string Text
    {
      get => this.m_sText;
      set => this.m_sText = value;
    }

    internal Control Control => this.m_oControl;

    internal bool Connected => this.m_bConnected;

    internal QBalloon Balloon => this.m_oBalloon;

    internal void Connect()
    {
      if (this.m_oControl == null)
        return;
      if (!this.Connected)
      {
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_GotFocus), (object) this.m_oControl, "GotFocus"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_LostFocus), (object) this.m_oControl, "LostFocus"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_MouseEnter), (object) this.m_oControl, "MouseEnter"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_MouseLeave), (object) this.m_oControl, "MouseLeave"));
        this.m_oEventConsumers.Add(new QWeakEventConsumer((Delegate) new EventHandler(this.Control_Disposed), (object) this.m_oControl, "Disposed"));
        this.m_bConnected = true;
      }
      IntPtr handle = NativeMethods.WindowFromPoint(new NativeMethods.POINT(Control.MousePosition.X, Control.MousePosition.Y));
      if (!(handle != IntPtr.Zero) || Control.FromHandle(handle) != this.m_oControl)
        return;
      this.Control_MouseEnter((object) this, EventArgs.Empty);
    }

    internal void Disconnect()
    {
      if (this.m_oControl == null || !this.Connected)
        return;
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_GotFocus));
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_LostFocus));
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_MouseEnter));
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_MouseLeave));
      this.m_oEventConsumers.DetachAndRemove((Delegate) new EventHandler(this.Control_Disposed));
      this.m_bConnected = false;
      if (this.m_oBalloon.BalloonWindow != null && this.m_oBalloon.BalloonWindow.Visible && this.m_oBalloon.AnimationStatus != QBalloon.QBalloonAnimationStatus.FadingOut)
        this.Hide(false);
      if (this.m_oBalloon == null || this.m_oBalloon.LastListener != this)
        return;
      this.m_oBalloon.LastListener = (QBalloonControlListener) null;
    }

    internal void TimerTick()
    {
      if (this.m_oBalloon == null)
        return;
      if (this.m_iInitialDelayTickCount != 0 && this.m_iInitialDelayTickCount + this.m_oBalloon.Configuration.InitialDelay < Environment.TickCount)
      {
        if (this.m_oBalloon.ReShowDelayTickCount > 0 && this.m_oBalloon.ReShowDelayTickCount + this.m_oBalloon.Configuration.ReshowDelay > Environment.TickCount)
          return;
        this.Show(false);
      }
      if (this.m_iAutoPopupDelayTickCount == 0 || this.m_oBalloon.Configuration.AutoPopDelay < 0 || this.m_iAutoPopupDelayTickCount + this.m_oBalloon.Configuration.AutoPopDelay >= Environment.TickCount)
        return;
      this.Hide(false);
    }

    internal void Show(bool delayed)
    {
      if (!this.m_bEnabled)
        return;
      if (delayed)
      {
        if (this.m_oControl != null && !this.m_oBalloon.Configuration.ShowAlways)
        {
          Form form = this.m_oControl.FindForm();
          if (form != null && form != Form.ActiveForm && (!form.IsMdiChild || form.MdiParent != null && form.MdiParent != Form.ActiveForm))
            return;
        }
        this.m_iInitialDelayTickCount = Environment.TickCount;
      }
      else
      {
        this.m_oBalloon.ShowBalloonWindow(this.m_oSpecifiedLocation, this, this.m_oControl, this.m_sText, this.m_ePositioning, this.m_bIgnoreWorkingArea);
        this.m_iInitialDelayTickCount = 0;
        this.m_oBalloon.ReShowDelayTickCount = Environment.TickCount;
      }
    }

    internal void Hide(bool delayed)
    {
      this.m_iInitialDelayTickCount = 0;
      if (delayed)
      {
        this.m_iAutoPopupDelayTickCount = Environment.TickCount;
      }
      else
      {
        if (this.m_oBalloon.BalloonWindow != null && this.m_oBalloon.BalloonWindow.TargetControl == this.m_oControl && this.m_oBalloon.LastListener == this)
        {
          if (this.m_oBalloon.BalloonWindow.IsHandleCreated)
          {
            NativeMethods.POINT Point = new NativeMethods.POINT(Control.MousePosition.X, Control.MousePosition.Y);
            IntPtr num = NativeMethods.WindowFromPoint(Point);
            bool flag = num == this.m_oBalloon.BalloonWindow.Handle || this.m_oBalloon.BalloonWindow.ControlContainer != null && this.m_oBalloon.BalloonWindow.ControlContainer.Handle == num;
            Control targetControl = this.m_oBalloon.BalloonWindow.TargetControl;
            this.m_oBalloon.HideBalloonWindow(this);
            if (flag && targetControl != null && !targetControl.IsDisposed && !targetControl.Disposing)
            {
              Point = new NativeMethods.POINT(Control.MousePosition.X, Control.MousePosition.Y);
              if (NativeMethods.WindowFromPoint(Point) == targetControl.Handle)
                this.m_oBalloon.IgnoreNextMouseEnter = true;
            }
          }
          if (this.m_bRemoveAfterHide)
            this.m_oBalloon.RemoveListener(this, true);
        }
        this.m_iAutoPopupDelayTickCount = 0;
      }
    }

    private void Control_GotFocus(object sender, EventArgs e)
    {
      if (!this.m_oBalloon.Configuration.ShowOnFocus)
        return;
      this.m_ePositioning = QBalloonWindowPositioning.ControlBounds;
      if (this.m_oBalloon.BalloonWindow != null && this.m_oBalloon.BalloonWindow.Visible)
        this.Show(false);
      else
        this.Show(true);
    }

    private void Control_LostFocus(object sender, EventArgs e)
    {
      if (!this.m_oBalloon.Configuration.ShowOnFocus || this.m_oBalloon.BalloonWindow != null && this.m_oBalloon.BalloonWindow.Focused || !this.m_oBalloon.Configuration.AutoHide)
        return;
      this.Hide(false);
    }

    private void Control_MouseEnter(object sender, EventArgs e)
    {
      if (this.m_oBalloon.IgnoreNextMouseEnter)
      {
        this.m_oBalloon.IgnoreNextMouseEnter = false;
      }
      else
      {
        if (!this.m_oBalloon.Configuration.ShowOnHover)
          return;
        this.m_ePositioning = QBalloonWindowPositioning.CursorLocation;
        if (this.m_oBalloon.BalloonWindow != null && this.m_oBalloon.BalloonWindow.Visible)
          this.Show(false);
        else
          this.Show(true);
      }
    }

    private void Control_MouseLeave(object sender, EventArgs e)
    {
      if (!this.m_oBalloon.Configuration.ShowOnHover)
        return;
      if (this.m_iInitialDelayTickCount != 0)
        this.m_iInitialDelayTickCount = 0;
      if (!this.m_oBalloon.Configuration.AutoHide)
        return;
      this.Hide(false);
    }

    private void Control_Disposed(object sender, EventArgs e)
    {
      if (this.m_oBalloon == null)
        return;
      this.m_oBalloon.RemoveListener(this, true);
    }
  }
}
