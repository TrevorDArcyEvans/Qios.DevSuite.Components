// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQMainMenu
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQMainMenu
  {
    void HandleActiveMdiChildChanged();

    void HandleMdiChildWindowStateChanged(int sizeParameter);

    void HandleMenukeyDown(ref Message m);

    void HandleDeactivate(IntPtr activatingWindow);
  }
}
