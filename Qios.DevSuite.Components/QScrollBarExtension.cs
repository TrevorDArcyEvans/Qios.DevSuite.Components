// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollBarExtension
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QScrollBarExtension : IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private QScrollBarVisibility m_eVisibility;
    private Control m_oSourceControl;
    private QScrollBarExtension.ControlHook m_oSourceControlHook;
    private Size m_oScrollSize = Size.Empty;
    private QWeakDelegate m_oScrollDelegate;

    internal QScrollBarExtension(Control sourceControl, QScrollBarVisibility visibility)
    {
      this.m_oSourceControl = sourceControl;
      this.m_oSourceControlHook = new QScrollBarExtension.ControlHook(this.m_oSourceControl.Handle, this);
      this.m_oSourceControlHook.InitiateHook();
      this.m_eVisibility = visibility;
    }

    [QWeakEvent]
    [Category("QEvents")]
    internal event QScrollEventHandler Scroll
    {
      add => this.m_oScrollDelegate = QWeakDelegate.Combine(this.m_oScrollDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oScrollDelegate = QWeakDelegate.Remove(this.m_oScrollDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    internal virtual void OnScroll(QScrollEventArgs e) => this.m_oScrollDelegate = QWeakDelegate.InvokeDelegate(this.m_oScrollDelegate, (object) this, (object) e);

    internal Size ScrollSize => this.m_oScrollSize;

    internal bool SetScrollSize(Size scrollSize)
    {
      this.m_oScrollSize = scrollSize;
      return this.LayoutScrollBar();
    }

    internal bool ScrollHorizontalVisible => this.HasScrollBar(QScrollBarDirection.Horizontal);

    internal bool ScrollVerticalVisible => this.HasScrollBar(QScrollBarDirection.Vertical);

    internal int ScrollHorizontalValue
    {
      get => this.GetScrollValue(QScrollBarDirection.Horizontal);
      set => this.SetScrollValue(value, QScrollBarDirection.Horizontal);
    }

    internal int ScrollVerticalValue
    {
      get => this.GetScrollValue(QScrollBarDirection.Vertical);
      set => this.SetScrollValue(value, QScrollBarDirection.Vertical);
    }

    internal int ScrollHorizontalMax => this.GetScrollMax(QScrollBarDirection.Horizontal);

    internal int ScrollVerticalMax => this.GetScrollMax(QScrollBarDirection.Vertical);

    private int LineHeight => Math.Max(1, this.m_oScrollSize.Height / 100);

    private bool LayoutScrollBarHorizontal(int style) => (this.m_eVisibility == QScrollBarVisibility.Horizontal || this.m_eVisibility == QScrollBarVisibility.Both) && this.SetScrollBar(QScrollBarDirection.Horizontal, this.m_oScrollSize.Width > this.m_oSourceControl.ClientRectangle.Width, style);

    private bool LayoutScrollBarVertical(int style) => (this.m_eVisibility == QScrollBarVisibility.Vertical || this.m_eVisibility == QScrollBarVisibility.Both) && this.SetScrollBar(QScrollBarDirection.Vertical, this.m_oScrollSize.Height > this.m_oSourceControl.ClientRectangle.Height, style);

    private void SecureControlHook()
    {
      if (!(this.m_oSourceControl.Handle != this.m_oSourceControlHook.Handle))
        return;
      this.m_oSourceControlHook.InitiateHook(this.m_oSourceControl.Handle);
    }

    private bool LayoutScrollBar()
    {
      if (this.m_oSourceControl == null || this.m_oSourceControl.IsDisposed)
        return false;
      this.SecureControlHook();
      int windowLong = NativeMethods.GetWindowLong(this.m_oSourceControl.Handle, -16);
      bool flag1 = this.LayoutScrollBarVertical(windowLong);
      bool flag2 = this.LayoutScrollBarHorizontal(windowLong);
      if (flag2 && this.LayoutScrollBarVertical(NativeMethods.GetWindowLong(this.m_oSourceControl.Handle, -16)))
      {
        flag1 = true;
        this.LayoutScrollBarHorizontal(NativeMethods.GetWindowLong(this.m_oSourceControl.Handle, -16));
      }
      if (this.HasScrollBar(QScrollBarDirection.Horizontal))
      {
        this.SetScrollMax(this.m_oScrollSize.Width, QScrollBarDirection.Horizontal);
        this.SetScrollPageSize(this.m_oSourceControl.ClientRectangle.Width, QScrollBarDirection.Horizontal);
      }
      if (this.HasScrollBar(QScrollBarDirection.Vertical))
      {
        this.SetScrollMax(this.m_oScrollSize.Height, QScrollBarDirection.Vertical);
        this.SetScrollPageSize(this.m_oSourceControl.ClientRectangle.Height, QScrollBarDirection.Vertical);
      }
      return flag2 || flag1;
    }

    private void SetScrollPageSize(int size, QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 2,
        nPage = size
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      NativeMethods.SetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi, true);
    }

    private int GetScrollPageSize(QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 2
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      NativeMethods.GetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi);
      return lpsi.nPage;
    }

    private void SetScrollMax(int maximum, QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 1
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      lpsi.nMin = this.GetScrollMin(direction);
      lpsi.nMax = maximum;
      NativeMethods.SetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi, true);
    }

    private int GetScrollMax(QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 1
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      NativeMethods.GetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi);
      return lpsi.nMax;
    }

    private int GetScrollMin(QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 1
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      NativeMethods.GetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi);
      return lpsi.nMin;
    }

    private void SetScrollMin(int minimum, QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 1
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      lpsi.nMin = minimum;
      lpsi.nMax = this.GetScrollMax(direction);
      NativeMethods.SetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi, true);
    }

    private int GetScrollValue(QScrollBarDirection direction)
    {
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 4
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      NativeMethods.GetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi);
      return lpsi.nPos;
    }

    private void SetScrollValue(int position, QScrollBarDirection direction)
    {
      int scrollValue = this.GetScrollValue(direction);
      NativeMethods.SCROLLINFO lpsi = new NativeMethods.SCROLLINFO()
      {
        fMask = 4,
        nPos = position
      };
      lpsi.cbSize = Marshal.SizeOf((object) lpsi);
      NativeMethods.SetScrollInfo(this.m_oSourceControl.Handle, direction == QScrollBarDirection.Horizontal ? 0 : 1, ref lpsi, true);
      this.OnScroll(new QScrollEventArgs(this.GetScrollValue(direction), scrollValue));
    }

    private bool HasScrollBar(QScrollBarDirection direction)
    {
      int windowLong = NativeMethods.GetWindowLong(this.m_oSourceControl.Handle, -16);
      int num = direction == QScrollBarDirection.Vertical ? 2097152 : 1048576;
      return (windowLong & num) == num;
    }

    private bool SetScrollBar(QScrollBarDirection direction, bool visible, int style)
    {
      bool flag1 = false;
      if (this.m_oSourceControl == null || this.m_oSourceControl.IsDisposed)
        return flag1;
      int num = direction == QScrollBarDirection.Vertical ? 2097152 : 1048576;
      bool flag2;
      if ((style & num) == num)
      {
        if (!visible)
        {
          style &= ~num;
          NativeMethods.SetWindowLong(this.m_oSourceControl.Handle, -16, style);
          this.SetScrollValue(0, direction);
          this.ForceWindowUpdate();
          flag2 = true;
        }
        else
          flag2 = false;
      }
      else if (visible)
      {
        style |= num;
        NativeMethods.SetWindowLong(this.m_oSourceControl.Handle, -16, style);
        this.ForceWindowUpdate();
        flag2 = true;
      }
      else
        flag2 = false;
      return flag2;
    }

    private void ForceWindowUpdate() => NativeMethods.SetWindowPos(this.m_oSourceControl.Handle, IntPtr.Zero, 0, 0, 0, 0, 55U);

    private class ControlHook : NativeWindow
    {
      private QScrollBarExtension m_oParentScrollBarExtension;
      private IntPtr m_oHandle;

      protected override void OnHandleChange() => base.OnHandleChange();

      internal ControlHook(IntPtr hWnd, QScrollBarExtension scrollBarExtension)
      {
        this.m_oParentScrollBarExtension = scrollBarExtension;
        this.m_oHandle = hWnd;
      }

      internal void InitiateHook() => this.AssignHandle(this.m_oHandle);

      internal void InitiateHook(IntPtr handle)
      {
        if (this.Handle != IntPtr.Zero)
          this.ReleaseHandle();
        this.m_oHandle = handle;
        this.AssignHandle(this.m_oHandle);
      }

      protected override void WndProc(ref Message m)
      {
        if (m.Msg == 276 || m.Msg == 277)
        {
          int num = (int) m.WParam & (int) ushort.MaxValue;
          QScrollBarDirection direction = m.Msg == 277 ? QScrollBarDirection.Vertical : QScrollBarDirection.Horizontal;
          int scrollValue = this.m_oParentScrollBarExtension.GetScrollValue(direction);
          int position = scrollValue;
          switch (num)
          {
            case 0:
              position -= this.m_oParentScrollBarExtension.LineHeight;
              break;
            case 1:
              position += this.m_oParentScrollBarExtension.LineHeight;
              break;
            case 2:
              position -= this.m_oParentScrollBarExtension.GetScrollPageSize(direction);
              break;
            case 3:
              position += this.m_oParentScrollBarExtension.GetScrollPageSize(direction);
              break;
            case 4:
            case 5:
              position = (int) m.WParam >> 16;
              break;
          }
          if (scrollValue == position)
            return;
          this.m_oParentScrollBarExtension.SetScrollValue(position, direction);
        }
        else
          base.WndProc(ref m);
      }
    }
  }
}
