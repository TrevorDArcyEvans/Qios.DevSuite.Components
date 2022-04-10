// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QHotkeyHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QHotkeyHelper
  {
    private const string m_sFindHotKeyPattern = "(&)(\\w)";
    private const string m_sRemoveHotKeyPattern = "(&)(.?)";
    private static Regex m_oFindHotKeyRegex;
    private static Regex m_oRemoveHotKeyRegex;

    private QHotkeyHelper()
    {
    }

    internal static void ConfigureHotkeyWindow(
      Control control,
      IQHotkeyHandlerHost navigationHost,
      IQHotkeyItem item,
      QHotkeyWindowConfiguration configuration,
      QColorScheme colorScheme)
    {
      QColorSet colorSet = new QColorSet((Color) colorScheme["HotkeyWindowBackground1"], (Color) colorScheme["HotkeyWindowBackground2"], (Color) colorScheme["HotkeyWindowBorder"], (Color) colorScheme["HotkeyWindowText"]);
      QHotkeyHelper.ConfigureHotkeyWindow(control, navigationHost, item, configuration, colorSet);
    }

    internal static void ConfigureHotkeyWindow(
      Control control,
      IQHotkeyHandlerHost navigationHost,
      IQHotkeyItem item,
      QHotkeyWindowConfiguration configuration,
      QColorSet colorSet)
    {
      if (item.HotKeyWindow == null)
        item.HotKeyWindow = new QHotkeyWindow();
      item.HotKeyWindow.NavigationHost = navigationHost;
      item.HotKeyWindow.Configuration = configuration;
      item.HotKeyWindow.Font = control.Font;
      item.HotKeyWindow.ColorSet = colorSet;
      item.HotKeyWindow.Hotkeys = item.HotkeyText;
      item.HotKeyWindow.Opacity = item.Enabled ? 1.0 : 0.5;
      item.HotKeyWindow.OwnerWindow = (IWin32Window) control;
      item.HotKeyWindow.PerformLayout();
      item.HotKeyWindow.Location = item.GetHotkeyWindowPosition(item.HotKeyWindow.Size);
    }

    internal static Keys[] AddPressedKey(Keys[] keySequence, Keys key)
    {
      Keys[] destinationArray;
      if (keySequence == null)
      {
        destinationArray = new Keys[1];
      }
      else
      {
        destinationArray = new Keys[keySequence.Length + 1];
        Array.Copy((Array) keySequence, (Array) destinationArray, keySequence.Length);
      }
      destinationArray[destinationArray.Length - 1] = key;
      return destinationArray;
    }

    internal static int GetHotkeyItemCount(Keys[] keySequence, bool exact, IList list)
    {
      int hotkeyItemCount = 0;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index] is IQHotkeyItem qhotkeyItem && qhotkeyItem.HasHotkey && qhotkeyItem.MatchesHotkeySequence(keySequence, exact))
          ++hotkeyItemCount;
      }
      return hotkeyItemCount;
    }

    internal static void FilterHotkeyItems(Keys[] keySequence, bool exact, IList list)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index] is IQHotkeyItem qhotkeyItem && qhotkeyItem.HasHotkey && !qhotkeyItem.MatchesHotkeySequence(keySequence, exact))
        {
          if (qhotkeyItem.HotKeyWindow != null)
            qhotkeyItem.ShowHotkeyWindow(false);
          list[index] = (object) null;
        }
      }
      QMisc.CleanupList(list);
    }

    internal static void HideHotkeyWindows(IEnumerable list)
    {
      if (list == null)
        return;
      foreach (object obj in list)
      {
        if (obj is IQHotkeyItem qhotkeyItem)
          qhotkeyItem.ShowHotkeyWindow(false);
      }
    }

    internal static void SetParentHotkeyItem(IQHotkeyItem parentItem, IEnumerable enumerable)
    {
      foreach (object obj in enumerable)
      {
        if (obj is IQHotkeyItem qhotkeyItem)
          qhotkeyItem.ParentHotkeyItem = parentItem;
      }
    }

    internal static bool AllItemsMatchesHotkeySequence(
      Keys[] sequence,
      IEnumerable enumerable,
      bool exact)
    {
      foreach (object obj in enumerable)
      {
        if (obj is IQHotkeyItem qhotkeyItem && !qhotkeyItem.MatchesHotkeySequence(sequence, exact))
          return false;
      }
      return true;
    }

    internal static IQHotkeyItem GetNextHotkeyItem(
      IList items,
      IQHotkeyItem currentItem,
      bool loopAround)
    {
      if (items == null || items.Count == 0)
        return (IQHotkeyItem) null;
      if (currentItem == null)
        return items[0] as IQHotkeyItem;
      int index1 = -1;
      for (int index2 = 0; index2 < items.Count; ++index2)
      {
        if (items[index2] == currentItem)
        {
          index1 = index2 + 1;
          break;
        }
      }
      for (; index1 >= 0 && index1 < items.Count; ++index1)
      {
        IQHotkeyItem qhotkeyItem = items[index1] as IQHotkeyItem;
        if (qhotkeyItem.Visible && qhotkeyItem.HasHotkey)
          break;
      }
      if (index1 >= 0 && index1 < items.Count)
        return items[index1] as IQHotkeyItem;
      return loopAround ? QHotkeyHelper.GetNextHotkeyItem(items, (IQHotkeyItem) null, false) : (IQHotkeyItem) null;
    }

    public static Keys FindHotKey(string value)
    {
      char hotkeyChar = QHotkeyHelper.FindHotkeyChar(value);
      return hotkeyChar > char.MinValue ? QHotkeyHelper.ConvertToKeys(hotkeyChar) : Keys.None;
    }

    public static char FindHotkeyChar(string value)
    {
      switch (value)
      {
        case "":
        case null:
          return char.MinValue;
        default:
          if (QHotkeyHelper.m_oFindHotKeyRegex == null)
            QHotkeyHelper.m_oFindHotKeyRegex = new Regex("(&)(\\w)");
          Match match = QHotkeyHelper.m_oFindHotKeyRegex.Match(value);
          return match != null && match.Groups.Count >= 2 ? match.Groups[2].Value[0] : char.MinValue;
      }
    }

    public static Keys[] GetHotkeySequence(string hotkeyText)
    {
      switch (hotkeyText)
      {
        case "":
        case null:
          return (Keys[]) null;
        default:
          char[] charArray = hotkeyText.ToCharArray();
          ArrayList arrayList = new ArrayList();
          for (int index = 0; index < charArray.Length; ++index)
          {
            Keys keys = QHotkeyHelper.ConvertToKeys(charArray[index]);
            if (keys != Keys.None)
              arrayList.Add((object) keys);
          }
          return (Keys[]) arrayList.ToArray(typeof (Keys));
      }
    }

    public static bool MatchesHotkeySequence(Keys[] itemHotkeys, Keys[] sequence, bool exact)
    {
      if (itemHotkeys == null || itemHotkeys.Length == 0 || exact && sequence.Length != itemHotkeys.Length || sequence.Length > itemHotkeys.Length)
        return false;
      for (int index = 0; index < sequence.Length; ++index)
      {
        if (sequence[index] != itemHotkeys[index])
          return false;
      }
      return true;
    }

    public static string RemoveHotkeyPrefix(string value)
    {
      if (QMisc.IsEmpty((object) value))
        return value;
      if (QHotkeyHelper.m_oRemoveHotKeyRegex == null)
        QHotkeyHelper.m_oRemoveHotKeyRegex = new Regex("(&)(.?)");
      return QHotkeyHelper.m_oRemoveHotKeyRegex.Replace(value, "$2");
    }

    public static Keys ConvertToKeys(char charValue)
    {
      short num = NativeMethods.VkKeyScan(charValue);
      return num < (short) 0 ? Keys.None : (Keys) NativeHelper.LowOrderByte(num);
    }

    public static char ConvertToChar(Keys keys) => (char) NativeHelper.LowOrderWord(NativeMethods.MapVirtualKey((int) keys, 2));
  }
}
