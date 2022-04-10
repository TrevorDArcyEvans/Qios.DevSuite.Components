// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxItemComparer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.Globalization;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QInputBoxItemComparer : IComparer
  {
    private QInputBox m_oInputBox;

    public QInputBoxItemComparer(QInputBox inputBox) => this.m_oInputBox = inputBox;

    public int Compare(object item1, object item2) => item1 == null ? (item2 == null ? 0 : -1) : (item2 == null ? 1 : Application.CurrentCulture.CompareInfo.Compare(this.m_oInputBox.GetItemText(item1), this.m_oInputBox.GetItemText(item2), CompareOptions.StringSort));
  }
}
