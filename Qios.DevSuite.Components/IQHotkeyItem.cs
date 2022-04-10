// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQHotkeyItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQHotkeyItem : IQNavigationItem
  {
    Point GetHotkeyWindowPosition(Size hotkeyWindowSize);

    bool HasHotkey { get; }

    string HotkeyText { get; }

    IQHotkeyItem ParentHotkeyItem { get; set; }

    bool HasChildHotkeyItems { get; }

    void AddChildHotkeyItems(IList list);

    bool MatchesHotkeySequence(Keys[] sequence, bool exact);

    QHotkeyWindow HotKeyWindow { get; set; }

    void ShowHotkeyWindow(bool show);
  }
}
