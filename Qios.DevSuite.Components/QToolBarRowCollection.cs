// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarRowCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  internal class QToolBarRowCollection : CollectionBase
  {
    private QToolBarHost m_oParentToolBarHost;

    internal QToolBarRowCollection(QToolBarHost parentToolBarHost) => this.m_oParentToolBarHost = parentToolBarHost;

    internal QToolBarHost ParentToolBarHost => this.m_oParentToolBarHost;

    internal void Add(QToolBarRow toolBarRow)
    {
      toolBarRow.ParentCollection = this;
      toolBarRow.SetPositionIndex(this.PositionIndexForAdd, false);
      this.InnerList.Add((object) toolBarRow);
    }

    internal void Remove(QToolBarRow toolBarRow)
    {
      toolBarRow.ParentCollection = (QToolBarRowCollection) null;
      if (this.ParentToolBarHost != null && !this.ParentToolBarHost.Initializing)
        this.DecreasePositionIndices(toolBarRow);
      this.InnerList.Remove((object) toolBarRow);
    }

    internal void Insert(int index, QToolBarRow toolBarRow)
    {
      if (index >= 0)
      {
        toolBarRow.ParentCollection = this;
        toolBarRow.SetPositionIndex(this.PositionIndexForAdd, false);
        this.InnerList.Add((object) toolBarRow);
        this.SetPositionIndex(toolBarRow, index);
        this.SortByPositionIndex();
      }
      else
        this.Add(toolBarRow);
    }

    internal int IndexOf(QToolBarRow toolBarRow) => this.InnerList.Contains((object) toolBarRow) ? this.InnerList.IndexOf((object) toolBarRow) : -1;

    internal QToolBarRow this[int index] => (QToolBarRow) this.InnerList[index];

    internal bool Contains(QToolBarRow row) => this.InnerList.Contains((object) row);

    internal int VisibleToolBarCount
    {
      get
      {
        int visibleToolBarCount = 0;
        for (int index = 0; index < this.Count; ++index)
          visibleToolBarCount += this[index].VisibleToolBarCount;
        return visibleToolBarCount;
      }
    }

    internal QToolBarRow GetByPositionIndex(int index) => this.GetByPositionIndex(index, true);

    internal QToolBarRow GetByPositionIndex(int index, bool firstHit)
    {
      int index1 = 0;
      QToolBarRow byPositionIndex = (QToolBarRow) null;
      for (; index1 < this.Count; ++index1)
      {
        if (this[index1].PositionIndex == index)
        {
          if (firstHit)
            return this[index1];
          byPositionIndex = this[index1];
        }
      }
      return byPositionIndex;
    }

    internal int FirstPositionIndex => this.Count != 0 ? this[0].PositionIndex : 0;

    internal int LastPositionIndex => this.Count != 0 ? this[this.Count - 1].PositionIndex : 0;

    internal int PositionIndexForAdd => this.Count != 0 ? this[this.Count - 1].PositionIndex + 1 : 0;

    internal void SortByPositionIndex() => this.InnerList.Sort(0, this.Count, (IComparer) new QToolBarRowPositionComparer());

    private void DecreasePositionIndices(QToolBarRow removingRow)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].PositionIndex > removingRow.PositionIndex)
          this[index].SetPositionIndex(this[index].PositionIndex - 1, false);
      }
    }

    internal void SetPositionIndex(QToolBarRow row, int index)
    {
      if (!this.Contains(row))
        throw new InvalidOperationException(QResources.GetException("QToolBarRowCollection_RowNotInCollection"));
      if (this.ParentToolBarHost == null || !this.ParentToolBarHost.Initializing)
      {
        this.DecreasePositionIndices(row);
        row.SetPositionIndex(index, false);
        for (int index1 = 0; index1 < this.Count; ++index1)
        {
          if (this[index1].PositionIndex == index && this[index1] != row)
          {
            row = this[index1];
            ++index;
            row.SetPositionIndex(index, false);
          }
        }
      }
      else
        row.SetPositionIndex(index, false);
      this.SortByPositionIndex();
    }

    internal int LayoutToolBarRows(IQToolBar movingToolBar)
    {
      int num = this.ParentToolBarHost.ToolBarHostPadding.Top;
      for (int index = 0; index < this.Count; ++index)
      {
        QToolBarRow qtoolBarRow = this[index];
        if (qtoolBarRow.VisibleToolBarCount > 0)
          num += this.ParentToolBarHost.ToolBarMargin.Top;
        qtoolBarRow.Start = num;
        qtoolBarRow.LayoutToolBars(movingToolBar);
        if (qtoolBarRow.VisibleToolBarCount > 0)
          num = qtoolBarRow.End + this.ParentToolBarHost.ToolBarMargin.Bottom;
      }
      return this.VisibleToolBarCount <= 0 ? 0 : num + this.ParentToolBarHost.ToolBarHostPadding.Bottom;
    }

    internal QToolBarRow InsertToolBarIntoNextRow(
      QToolBarRow sourceToolBarRow,
      IQToolBar toolBar)
    {
      int num = this.IndexOf(sourceToolBarRow);
      QToolBarRow toolBarRow;
      if (num < 0 || num >= this.Count - 1)
      {
        toolBarRow = new QToolBarRow();
        this.Add(toolBarRow);
      }
      else
        toolBarRow = this[num + 1];
      toolBarRow.Insert(0, toolBar);
      return toolBarRow;
    }

    internal IQToolBar[] GetToolBarsForThisRow(QToolBarRow toolBarRow)
    {
      int num1 = this.IndexOf(toolBarRow);
      if (num1 < 0)
        return (IQToolBar[]) null;
      ArrayList arrayList = (ArrayList) null;
      for (int index1 = num1 + 1; index1 < this.Count; ++index1)
      {
        QToolBarRow qtoolBarRow = this[index1];
        for (int index2 = 0; index2 < qtoolBarRow.Count; ++index2)
        {
          IQToolBar qtoolBar = qtoolBarRow[index2];
          int num2 = qtoolBar.OriginalToolBarRow != null ? this.IndexOf(qtoolBar.OriginalToolBarRow) : -1;
          if (num2 >= 0 && num2 <= num1)
          {
            if (arrayList == null)
              arrayList = new ArrayList();
            arrayList.Add((object) qtoolBar);
          }
        }
      }
      return arrayList != null ? (IQToolBar[]) arrayList.ToArray(typeof (IQToolBar)) : (IQToolBar[]) null;
    }

    internal void NotifyRowChanged(QToolBarRow changedRow)
    {
      if (changedRow.Count != 0)
        return;
      this.Remove(changedRow);
    }
  }
}
