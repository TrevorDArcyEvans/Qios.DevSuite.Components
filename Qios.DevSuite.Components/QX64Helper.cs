// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QX64Helper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Runtime.InteropServices;

namespace Qios.DevSuite.Components
{
  public static class QX64Helper
  {
    public static bool Is64BitProcess => IntPtr.Size == 8;

    public static bool Is64BitOperatingSystem
    {
      get
      {
        if (QX64Helper.Is64BitProcess)
          return true;
        bool isWow64;
        return QX64Helper.ModuleContainsFunction("kernel32.dll", "IsWow64Process") && QX64Helper.IsWow64Process(QX64Helper.GetCurrentProcess(), out isWow64) && isWow64;
      }
    }

    private static bool ModuleContainsFunction(string moduleName, string methodName)
    {
      IntPtr moduleHandle = QX64Helper.GetModuleHandle(moduleName);
      return moduleHandle != IntPtr.Zero && QX64Helper.GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetModuleHandle(string moduleName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);
  }
}
