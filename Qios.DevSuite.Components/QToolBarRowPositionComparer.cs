// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarRowPositionComparer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  internal class QToolBarRowPositionComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      if (x == null && y == null)
        return 0;
      if (x == null)
        return -1;
      return y == null ? 1 : ((QToolBarRow) x).PositionIndex - ((QToolBarRow) y).PositionIndex;
    }
  }
}
