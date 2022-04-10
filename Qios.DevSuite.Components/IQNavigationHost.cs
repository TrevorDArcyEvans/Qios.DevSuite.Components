// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQNavigationHost
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQNavigationHost
  {
    void SelectFirstOrCurrentItem(bool forward);

    void SelectNextItem(bool forward, bool loop);

    bool IsAccessibleForNavigation { get; }

    Point LocationForOrder { get; }

    bool HandleShortcutKey(Keys key, out bool suppressToSystem);
  }
}
