// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonComparer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  internal class QTabButtonComparer : IComparer
  {
    public int Compare(object x, object y) => this.Compare(x as QTabButton, y as QTabButton, false);

    public int Compare(QTabButton x, QTabButton y, bool ignoreRows)
    {
      QTabButton qtabButton1 = x;
      QTabButton qtabButton2 = y;
      if (qtabButton1 == null && qtabButton2 == null)
        return 0;
      if (qtabButton1 == null && qtabButton2 != null)
        return -1;
      if (qtabButton1 != null && qtabButton2 == null)
        return 1;
      if (!ignoreRows && qtabButton1.TabButtonRow == null && qtabButton2.TabButtonRow != null)
        return -1;
      if (!ignoreRows && qtabButton1.TabButtonRow != null && qtabButton2.TabButtonRow == null)
        return 1;
      if (!ignoreRows && qtabButton1.TabButtonRow != qtabButton2.TabButtonRow)
        return qtabButton1.TabButtonRow.RowIndex - qtabButton2.TabButtonRow.RowIndex;
      if (qtabButton1.Configuration.Alignment == qtabButton2.Configuration.Alignment)
        return qtabButton1.ButtonOrder - qtabButton2.ButtonOrder;
      return qtabButton1.Configuration.Alignment == QTabButtonAlignment.Near ? -1 : 1;
    }
  }
}
