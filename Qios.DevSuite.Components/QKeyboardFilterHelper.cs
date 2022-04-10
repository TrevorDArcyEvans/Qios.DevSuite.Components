// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QKeyboardFilterHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QKeyboardFilterHelper
  {
    private QKeyboardFilterHelper()
    {
    }

    internal static bool ShouldHandleShortcutsForControl(
      Control handlingControl,
      Control keyDestinationControl,
      QShortcutScope scope)
    {
      return scope == QShortcutScope.Application || QKeyboardFilterHelper.ShouldHandleKeyMessagesForControl(handlingControl, keyDestinationControl);
    }

    internal static bool ShouldHandleKeyMessagesForControl(
      Control handlingControl,
      Control keyDestinationControl)
    {
      return keyDestinationControl == null || QKeyboardFilterHelper.GetTopLevelWindowThroughPassThrough(handlingControl) == QKeyboardFilterHelper.GetTopLevelWindowThroughPassThrough(keyDestinationControl);
    }

    private static Control GetTopLevelWindowThroughPassThrough(Control control)
    {
      Control control1 = control.TopLevelControl ?? control;
      return control1 is IQMenuKeyPassThrough qmenuKeyPassThrough ? QKeyboardFilterHelper.GetTopLevelWindowThroughPassThrough(qmenuKeyPassThrough.PassToControl) : control1;
    }
  }
}
