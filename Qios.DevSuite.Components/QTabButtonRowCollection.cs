// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonRowCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public sealed class QTabButtonRowCollection : CollectionBase
  {
    private QTabStrip m_oTabStrip;

    internal QTabButtonRowCollection(QTabStrip tabStrip) => this.m_oTabStrip = tabStrip;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new void RemoveAt(int index) => base.RemoveAt(index);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new void Clear() => base.Clear();

    public QTabButtonRow this[int index] => (QTabButtonRow) this.InnerList[index];

    public bool Contains(QTabButtonRow tabButtonRow) => this.InnerList.Contains((object) tabButtonRow);

    public void CopyTo(QTabButtonRow[] tabButtonRows, int index) => ((ICollection) this).CopyTo((Array) tabButtonRows, index);

    public int IndexOf(QTabButtonRow tabButtonRow) => this.InnerList.Contains((object) tabButtonRow) ? this.InnerList.IndexOf((object) tabButtonRow) : -1;

    internal bool IsFirstVisibleRow(QTabButtonRow row) => this.Contains(row) && this[0] == row;

    internal bool MakeFirstVisibleRow(QTabButtonRow row)
    {
      if (row == null || this.Count <= 1 || !this.Contains(row) || this.IsFirstVisibleRow(row))
        return false;
      for (int index = this.IndexOf(row); index > 0; --index)
        this.InnerList[index] = this.InnerList[index - 1];
      this.InnerList[0] = (object) row;
      return true;
    }

    internal void Add(QTabButtonRow tabButtonRow) => this.InnerList.Add((object) tabButtonRow);

    internal void Insert(int index, QTabButtonRow tabButtonRow) => this.InnerList.Insert(index, (object) tabButtonRow);

    internal void Remove(QTabButtonRow tabButtonRow) => this.InnerList.Remove((object) tabButtonRow);
  }
}
