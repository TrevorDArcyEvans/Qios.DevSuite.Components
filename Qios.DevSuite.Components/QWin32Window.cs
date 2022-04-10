// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWin32Window
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QWin32Window : IWin32Window
  {
    private NativeWindow m_oNativeWindow;

    public QWin32Window(NativeWindow nativeWindow) => this.m_oNativeWindow = nativeWindow;

    public IntPtr Handle => this.m_oNativeWindow.Handle;
  }
}
