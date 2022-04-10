// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QKeyboardHooker
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QKeyboardHooker : IDisposable
  {
    private IntPtr m_oKeyboardHookerHandle = IntPtr.Zero;
    private NativeMethods.HookProc m_oHookProcDelegate;
    private bool m_bGlobalHook;
    private IQKeyboardHookClient m_oClient;

    public QKeyboardHooker(IQKeyboardHookClient client)
    {
      this.m_oHookProcDelegate = new NativeMethods.HookProc(this.KeyboardHookProc);
      this.m_oClient = client;
    }

    public bool KeyboardHooked
    {
      get => this.m_oKeyboardHookerHandle != IntPtr.Zero;
      set
      {
        if (value)
          this.HookKeyboard();
        else
          this.UnhookKeyboard();
      }
    }

    public bool GlobalHook
    {
      get => this.m_bGlobalHook;
      set
      {
        if (this.m_bGlobalHook == value)
          return;
        if (this.KeyboardHooked)
        {
          this.KeyboardHooked = false;
          this.m_bGlobalHook = value;
          this.KeyboardHooked = true;
        }
        else
          this.m_bGlobalHook = value;
      }
    }

    private void HookKeyboard()
    {
      lock (this)
      {
        if (this.KeyboardHooked)
          return;
        if (this.m_bGlobalHook)
          this.m_oKeyboardHookerHandle = NativeMethods.SetWindowsHookEx(13, this.m_oHookProcDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
        else
          this.m_oKeyboardHookerHandle = NativeMethods.SetWindowsHookEx(2, this.m_oHookProcDelegate, IntPtr.Zero, NativeMethods.GetCurrentThreadId());
      }
    }

    private void UnhookKeyboard()
    {
      lock (this)
      {
        if (!this.KeyboardHooked)
          return;
        NativeMethods.UnhookWindowsHookEx(this.m_oKeyboardHookerHandle);
        this.m_oKeyboardHookerHandle = IntPtr.Zero;
      }
    }

    private IntPtr KeyboardHookProc(int nCode, IntPtr wparam, IntPtr lparam)
    {
      bool cancelMessage = false;
      bool callNextHook = true;
      IntPtr num = new IntPtr(0);
      if (nCode == 0)
        this.ProcessKeyboardMessage(wparam, lparam, ref cancelMessage, ref callNextHook);
      if (callNextHook)
        num = NativeMethods.CallNextHookEx(this.m_oKeyboardHookerHandle, nCode, wparam, lparam);
      return cancelMessage ? new IntPtr(1) : num;
    }

    private void ProcessKeyboardMessage(
      IntPtr wparam,
      IntPtr lparam,
      ref bool cancelMessage,
      ref bool callNextHook)
    {
      if (this.m_oClient == null)
        return;
      if (this.m_bGlobalHook)
      {
        NativeMethods.KBDLLHOOKSTRUCT structure = (NativeMethods.KBDLLHOOKSTRUCT) Marshal.PtrToStructure(lparam, typeof (NativeMethods.KBDLLHOOKSTRUCT));
        switch (wparam.ToInt32())
        {
          case 256:
          case 260:
            this.m_oClient.HandleKeyDown((Keys) structure.vkCode, ref cancelMessage, ref callNextHook);
            break;
          case 257:
          case 261:
            this.m_oClient.HandleKeyUp((Keys) structure.vkCode, ref cancelMessage, ref callNextHook);
            break;
        }
      }
      else if ((NativeHelper.HighOrderWord((int) lparam) & 32768) == 32768)
        this.m_oClient.HandleKeyUp((Keys) wparam.ToInt32(), ref cancelMessage, ref callNextHook);
      else
        this.m_oClient.HandleKeyDown((Keys) wparam.ToInt32(), ref cancelMessage, ref callNextHook);
    }

    protected virtual void Dispose(bool disposing) => this.UnhookKeyboard();

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~QKeyboardHooker() => this.Dispose(false);
  }
}
