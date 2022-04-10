// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QItemStatesHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QItemStatesHelper
  {
    private QItemStatesHelper()
    {
    }

    public static bool IsHot(QItemStates state) => (state & QItemStates.Hot) == QItemStates.Hot;

    public static bool IsPressed(QItemStates state) => (state & QItemStates.Pressed) == QItemStates.Pressed;

    public static bool IsExpanded(QItemStates state) => (state & QItemStates.Expanded) == QItemStates.Expanded;

    public static bool IsChecked(QItemStates state) => (state & QItemStates.Checked) == QItemStates.Checked;

    public static bool IsDisabled(QItemStates state) => (state & QItemStates.Disabled) == QItemStates.Disabled;

    public static bool IsNormal(QItemStates state) => state == QItemStates.Normal;

    public static bool IsState(QItemStates state, QItemStates mustBeState) => (state & mustBeState) == mustBeState;

    public static QItemStates AdjustState(
      QItemStates state,
      QItemStates adjustment,
      bool setValue)
    {
      if (setValue)
        state |= adjustment;
      else
        state &= ~adjustment;
      return state;
    }

    public static QItemStates FromButtonState(QButtonState state)
    {
      switch (state)
      {
        case QButtonState.Inactive:
          return QItemStates.Disabled;
        case QButtonState.Hot:
          return QItemStates.Hot;
        case QButtonState.Pressed:
          return QItemStates.Pressed;
        default:
          return QItemStates.Normal;
      }
    }
  }
}
