// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMouseHooker
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QMouseHooker
  {
    private bool m_bExitOnMouseDown = true;
    private bool m_bExitOnMouseUp = true;
    private bool m_bIgnoreNextMouseUpMessage;
    private IntPtr m_oMouseHookHandle = IntPtr.Zero;
    private NativeMethods.HookProc m_oHookProcDelegate;
    private IQMouseHookClient m_oClient;

    public QMouseHooker(IQMouseHookClient client)
    {
      this.m_oHookProcDelegate = new NativeMethods.HookProc(this.MouseHookProc);
      this.m_oClient = client;
    }

    public bool ExitOnMouseDown
    {
      get => this.m_bExitOnMouseDown;
      set => this.m_bExitOnMouseDown = value;
    }

    public bool ExitOnMouseUp
    {
      get => this.m_bExitOnMouseUp;
      set => this.m_bExitOnMouseUp = value;
    }

    public bool IgnoreNextMouseUpMessage
    {
      get => this.m_bIgnoreNextMouseUpMessage;
      set => this.m_bIgnoreNextMouseUpMessage = value;
    }

    public bool MouseHooked
    {
      get => this.m_oMouseHookHandle != IntPtr.Zero;
      set
      {
        if (value)
          this.HookMouse();
        else
          this.UnhookMouse();
      }
    }

    private void HookMouse()
    {
      lock (this)
      {
        if (this.MouseHooked)
          return;
        this.m_oMouseHookHandle = NativeMethods.SetWindowsHookEx(7, this.m_oHookProcDelegate, IntPtr.Zero, NativeMethods.GetCurrentThreadId());
      }
    }

    private void UnhookMouse()
    {
      lock (this)
      {
        if (this.MouseHooked)
        {
          NativeMethods.UnhookWindowsHookEx(this.m_oMouseHookHandle);
          this.m_oMouseHookHandle = IntPtr.Zero;
        }
        this.m_bIgnoreNextMouseUpMessage = false;
      }
    }

    private IntPtr MouseHookProc(int nCode, IntPtr wparam, IntPtr lparam)
    {
      bool cancelMessage = false;
      bool callNextHook = true;
      IntPtr num = new IntPtr(0);
      if (nCode == 0)
      {
        NativeMethods.MOUSEHOOKSTRUCTEX structure = (NativeMethods.MOUSEHOOKSTRUCTEX) Marshal.PtrToStructure(lparam, typeof (NativeMethods.MOUSEHOOKSTRUCTEX));
        this.ProcessMouseMessage(wparam, lparam, ref structure, out cancelMessage, out callNextHook);
      }
      if (callNextHook)
        num = NativeMethods.CallNextHookEx(this.m_oMouseHookHandle, nCode, wparam, lparam);
      return cancelMessage ? new IntPtr(1) : num;
    }

    private void ProcessMouseMessage(
      IntPtr wparam,
      IntPtr lparam,
      ref NativeMethods.MOUSEHOOKSTRUCTEX mouseHookStructEx,
      out bool cancelMessage,
      out bool callNextHook)
    {
      cancelMessage = false;
      callNextHook = true;
      int lpdwProcessId = 0;
      NativeMethods.GetWindowThreadProcessId(mouseHookStructEx.MOUSEHOOKSTRUCT.hWnd, ref lpdwProcessId);
      if (this.m_oClient == null)
        return;
      if (this.m_bIgnoreNextMouseUpMessage && this.IsMouseUpMessage((int) wparam))
      {
        this.m_bIgnoreNextMouseUpMessage = false;
      }
      else
      {
        if (this.m_oClient.SuppressMessageToDestination((int) wparam, ref mouseHookStructEx.MOUSEHOOKSTRUCT))
          cancelMessage = true;
        if ((int) wparam == 522)
        {
          Point mousePosition = Control.MousePosition;
          this.m_oClient.HandleMouseWheelMessage(ref cancelMessage, new MouseEventArgs(MouseButtons.None, 0, mousePosition.X, mousePosition.Y, (int) mouseHookStructEx.mouseData >> 16));
        }
        if (!this.IsExitMessage((int) wparam))
          return;
        this.m_oClient.HandleExitMessage(ref cancelMessage);
      }
    }

    private bool IsMouseActivateMessage(int message) => message == 33;

    private bool IsMouseDownMessage(int message)
    {
      switch (message)
      {
        case 161:
        case 163:
        case 164:
        case 166:
        case 167:
        case 169:
        case 171:
        case 173:
        case 513:
        case 515:
        case 516:
        case 518:
        case 519:
        case 521:
        case 523:
        case 525:
          return true;
        default:
          return false;
      }
    }

    private bool IsMouseUpMessage(int message)
    {
      switch (message)
      {
        case 162:
        case 165:
        case 168:
        case 172:
        case 514:
        case 517:
        case 520:
        case 524:
          return true;
        default:
          return false;
      }
    }

    private bool IsExitMessage(int message)
    {
      if (this.IsMouseActivateMessage(message))
        return true;
      if (this.IsMouseUpMessage(message))
        return this.m_bExitOnMouseUp;
      return this.IsMouseDownMessage(message) && this.m_bExitOnMouseDown;
    }

    protected virtual void Dispose(bool disposing) => this.UnhookMouse();

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~QMouseHooker() => this.Dispose(false);
  }
}
