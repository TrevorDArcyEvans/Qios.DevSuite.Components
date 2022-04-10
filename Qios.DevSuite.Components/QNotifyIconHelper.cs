// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QNotifyIconHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QNotifyIconHelper
  {
    private QNotifyIconHelper()
    {
    }

    public static NativeWindow GetNotifyIconWindow(NotifyIcon notifyIcon) => typeof (NotifyIcon).GetField("window", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) notifyIcon) as NativeWindow;

    private static IntPtr RetrieveNotificationAreaHandle()
    {
      IntPtr zero = IntPtr.Zero;
      IntPtr window = NativeMethods.FindWindow(new StringBuilder("Shell_TrayWnd"), (StringBuilder) null);
      IntPtr hwndParent = !(window == IntPtr.Zero) ? NativeMethods.FindWindowEx(window, IntPtr.Zero, new StringBuilder("TrayNotifyWnd"), (StringBuilder) null) : throw new InvalidOperationException(QResources.GetException("QNotificationIconHelper_CannotFindWindow", (object) "Shell_TrayWnd", (object) ""));
      if (hwndParent == IntPtr.Zero)
        throw new InvalidOperationException(QResources.GetException("QNotificationIconHelper_CannotFindWindow", (object) "TrayNotifyWnd", (object) ""));
      IntPtr num;
      if (NativeHelper.WindowsXP)
      {
        IntPtr windowEx = NativeMethods.FindWindowEx(hwndParent, IntPtr.Zero, new StringBuilder("SysPager"), (StringBuilder) null);
        num = !(windowEx == IntPtr.Zero) ? NativeMethods.FindWindowEx(windowEx, IntPtr.Zero, new StringBuilder("ToolbarWindow32"), (StringBuilder) null) : throw new InvalidOperationException(QResources.GetException("QNotificationIconHelper_CannotFindWindow", (object) "SysPager", (object) ""));
        if (num == IntPtr.Zero)
          throw new InvalidOperationException(QResources.GetException("QNotificationIconHelper_CannotFindWindow", (object) "ToolbarWindow32", (object) ""));
      }
      else
      {
        num = NativeMethods.FindWindowEx(hwndParent, IntPtr.Zero, new StringBuilder("ToolbarWindow32"), (StringBuilder) null);
        if (num == IntPtr.Zero)
          throw new InvalidOperationException(QResources.GetException("QNotificationIconHelper_CannotFindWindow", (object) "ToolbarWindow32", (object) ""));
      }
      return num;
    }

    public static Rectangle RetrieveNotificationAreaBounds()
    {
      IntPtr hWnd = QNotifyIconHelper.RetrieveNotificationAreaHandle();
      NativeMethods.RECT lpRect = new NativeMethods.RECT();
      NativeMethods.GetWindowRect(hWnd, out lpRect);
      return NativeHelper.CreateRectangle(lpRect);
    }

    public static Rectangle RetrieveNotifyIconBounds(NotifyIcon icon)
    {
      IntPtr num1 = QNotifyIconHelper.RetrieveNotificationAreaHandle();
      NativeWindow notifyIconWindow = QNotifyIconHelper.GetNotifyIconWindow(icon);
      IntPtr num2 = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      IntPtr num3 = IntPtr.Zero;
      Rectangle rectangle = Rectangle.Empty;
      int num4 = -1;
      try
      {
        num2 = NativeHelper.AllocateMemoryInOtherProcess(num1, Marshal.SizeOf(QX64Helper.Is64BitOperatingSystem ? typeof (NativeMethods.TBBUTTONx64) : typeof (NativeMethods.TBBUTTON)), ref zero1);
        int int32 = NativeMethods.SendMessage(num1, 1048, IntPtr.Zero, IntPtr.Zero).ToInt32();
        if (int32 < 0)
          throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "SendMessage TB_BUTTONCOUNT"));
        for (int index = 0; index < int32; ++index)
        {
          if (NativeMethods.SendMessage(num1, 1047, new IntPtr(index), num2) == IntPtr.Zero)
            throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "SendMessage TB_GETBUTTON"));
          IntPtr zero2 = IntPtr.Zero;
          IntPtr num5;
          if (QX64Helper.Is64BitOperatingSystem)
          {
            NativeMethods.TBBUTTONx64 tbbuttoNx64 = (NativeMethods.TBBUTTONx64) NativeHelper.ReadMemoryFromOtherProcess(zero1, num2, typeof (NativeMethods.TBBUTTONx64));
            num5 = (IntPtr) NativeHelper.ReadMemoryFromOtherProcess(zero1, tbbuttoNx64.dwData, typeof (IntPtr));
          }
          else
          {
            NativeMethods.TBBUTTON tbbutton = (NativeMethods.TBBUTTON) NativeHelper.ReadMemoryFromOtherProcess(zero1, num2, typeof (NativeMethods.TBBUTTON));
            num5 = (IntPtr) NativeHelper.ReadMemoryFromOtherProcess(zero1, tbbutton.dwData, typeof (IntPtr));
          }
          if (num5 == notifyIconWindow.Handle)
          {
            num4 = index;
            break;
          }
        }
        if (num4 >= 0)
        {
          num3 = NativeHelper.AllocateMemoryInOtherProcess(num1, Marshal.SizeOf(typeof (NativeMethods.RECT)), ref zero1);
          if (NativeMethods.SendMessage(num1, 1053, new IntPtr(num4), num3) == IntPtr.Zero)
            throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "SendMessage TB_GETITEMRECT"));
          NativeMethods.RECT rect = (NativeMethods.RECT) NativeHelper.ReadMemoryFromOtherProcess(zero1, num3, typeof (NativeMethods.RECT));
          rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        }
      }
      finally
      {
        try
        {
          if (num2 != IntPtr.Zero)
            NativeHelper.FreeMemory(zero1, num2);
          if (num3 != IntPtr.Zero)
            NativeHelper.FreeMemory(zero1, num3);
        }
        finally
        {
          NativeHelper.CloseProcessHandle(zero1);
        }
      }
      if (!(rectangle != Rectangle.Empty))
        return Rectangle.Empty;
      NativeMethods.POINT lpPoint = new NativeMethods.POINT(rectangle.X, rectangle.Y);
      NativeMethods.ClientToScreen(num1, ref lpPoint);
      return new Rectangle(lpPoint.x, lpPoint.y, rectangle.Width, rectangle.Height);
    }
  }
}
