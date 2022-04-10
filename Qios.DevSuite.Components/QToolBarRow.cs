// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarRow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QToolBarRow : CollectionBase
  {
    private QToolBarRowCollection m_oParentCollection;
    private QRange m_oRange = QRange.Empty;
    private int m_iPositionIndex = -1;

    internal QToolBarRow()
    {
    }

    internal int PositionIndex
    {
      get => this.m_iPositionIndex;
      set => this.SetPositionIndex(value, true);
    }

    internal int FirstToolBarPositionIndex => this.Count != 0 ? this[0].ToolBarPositionIndex : 0;

    internal int LastToolBarPositionIndex => this.Count != 0 ? this[this.Count - 1].ToolBarPositionIndex : 0;

    internal int ToolBarPositionIndexForAdd => this.Count != 0 ? this[this.Count - 1].ToolBarPositionIndex + 1 : 0;

    internal void SetPositionIndex(int positionIndex, bool updateParentCollection)
    {
      if (updateParentCollection && this.ParentCollection != null)
        this.ParentCollection.SetPositionIndex(this, positionIndex);
      else
        this.m_iPositionIndex = positionIndex;
    }

    internal void SortByToolBarPositionIndex() => this.InnerList.Sort(0, this.Count, (IComparer) new QToolBarPositionComparer());

    private void DecreaseToolBarPositionIndices(IQToolBar removingToolBar)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ToolBarPositionIndex > removingToolBar.ToolBarPositionIndex)
          this[index].SetToolBarPositionIndex(this[index].ToolBarPositionIndex - 1, false);
      }
    }

    internal void SetToolBarPositionIndex(IQToolBar toolBar, int index)
    {
      if (!this.Contains(toolBar))
        throw new InvalidOperationException(QResources.GetException("QToolBarRow_ToolBarNotOnRow"));
      if (this.ParentToolBarHost == null || !this.ParentToolBarHost.Initializing)
      {
        this.DecreaseToolBarPositionIndices(toolBar);
        toolBar.SetToolBarPositionIndex(index, false);
        for (int index1 = 0; index1 < this.Count; ++index1)
        {
          if (this[index1].ToolBarPositionIndex == index && this[index1] != toolBar)
          {
            toolBar = this[index1];
            ++index;
            toolBar.SetToolBarPositionIndex(index, false);
          }
        }
      }
      else
        toolBar.SetToolBarPositionIndex(index, false);
      this.SortByToolBarPositionIndex();
    }

    internal QToolBarRowCollection ParentCollection
    {
      get => this.m_oParentCollection;
      set => this.m_oParentCollection = value;
    }

    internal QToolBarHost ParentToolBarHost => this.m_oParentCollection != null ? this.m_oParentCollection.ParentToolBarHost : (QToolBarHost) null;

    internal QRange Range => this.m_oRange;

    internal int Start
    {
      get => this.m_oRange.Start;
      set => this.m_oRange.Start = value;
    }

    internal int End
    {
      get => this.m_oRange.End;
      set => this.m_oRange.End = value;
    }

    internal int Size
    {
      get => this.m_oRange.Size;
      set => this.m_oRange.Size = value;
    }

    internal int GetVisibleCountLayoutType(QToolBarLayoutType layoutType)
    {
      int visibleCountLayoutType = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsVisible && this[index].ToolBarConfiguration.LayoutType == layoutType)
          ++visibleCountLayoutType;
      }
      return visibleCountLayoutType;
    }

    internal int VisibleToolBarCount
    {
      get
      {
        int visibleToolBarCount = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].IsVisible)
            ++visibleToolBarCount;
        }
        return visibleToolBarCount;
      }
    }

    internal IQToolBar GetPreviousVisibleToolBar(IQToolBar toolBar)
    {
      if (toolBar == null || !this.Contains(toolBar))
        return (IQToolBar) null;
      int index = this.IndexOf(toolBar) - 1;
      while (index >= 0 && !this[index].IsVisible)
        --index;
      return index >= 0 ? this[index] : (IQToolBar) null;
    }

    internal IQToolBar GetNextVisibleToolBar(IQToolBar toolBar)
    {
      if (toolBar == null || !this.Contains(toolBar))
        return (IQToolBar) null;
      int index = this.IndexOf(toolBar) + 1;
      while (index < this.Count && !this[index].IsVisible)
        ++index;
      return index < this.Count ? this[index] : (IQToolBar) null;
    }

    internal IQToolBar GetToolBarWithPriority(
      int comparePriority,
      int fromIndex,
      int tillIndex,
      bool higher)
    {
      int num = higher ? int.MaxValue : int.MinValue;
      IQToolBar toolBarWithPriority = (IQToolBar) null;
      for (int index = fromIndex; index < tillIndex; ++index)
      {
        if (this[index].IsVisible)
        {
          if (higher)
          {
            if (this[index].UserPriority >= comparePriority && this[index].UserPriority < num)
            {
              toolBarWithPriority = this[index];
              num = toolBarWithPriority.UserPriority;
            }
          }
          else if (this[index].UserPriority <= comparePriority && this[index].UserPriority > num)
          {
            toolBarWithPriority = this[index];
            num = toolBarWithPriority.UserPriority;
          }
        }
      }
      return toolBarWithPriority;
    }

    internal int GetMinimumStart(IQToolBar toolBar, int fromIndex, int startPosition)
    {
      int num1 = startPosition;
      if (fromIndex < 0)
        fromIndex = 0;
      int num2 = this.IndexOf(toolBar);
      for (int index = fromIndex; index < num2; ++index)
      {
        if (this[index].IsVisible)
        {
          if (this[index].UserPriority < toolBar.UserPriority)
            num1 = !this.ParentToolBarHost.Horizontal ? this[index].ProposedBounds.Bottom + this.ParentToolBarHost.ToolBarMargin.Right : this[index].ProposedBounds.Right + this.ParentToolBarHost.ToolBarMargin.Right;
          else if (this.ParentToolBarHost.Horizontal)
            num1 += this[index].ProposedBounds.Width + this.ParentToolBarHost.ToolBarMargin.Horizontal;
          else
            num1 += this[index].ProposedBounds.Height + this.ParentToolBarHost.ToolBarMargin.Horizontal;
        }
      }
      return num1 + this.ParentToolBarHost.ToolBarMargin.Left;
    }

    internal int GetMaximumEndPosition(IQToolBar toolBar, int tillIndex, int endPosition)
    {
      int num1 = endPosition;
      if (tillIndex > this.Count)
        tillIndex = this.Count;
      int num2 = this.IndexOf(toolBar);
      for (int index = tillIndex - 1; index > num2; --index)
      {
        if (this[index].IsVisible)
        {
          if (this[index].UserPriority < toolBar.UserPriority)
            num1 = !this.ParentToolBarHost.Horizontal ? this[index].ProposedBounds.Top - this.ParentToolBarHost.ToolBarMargin.Left : this[index].ProposedBounds.Left - this.ParentToolBarHost.ToolBarMargin.Left;
          else if (this.ParentToolBarHost.Horizontal)
            num1 -= this[index].ProposedBounds.Width + this.ParentToolBarHost.ToolBarMargin.Horizontal;
          else
            num1 -= this[index].ProposedBounds.Height + this.ParentToolBarHost.ToolBarMargin.Horizontal;
        }
      }
      return num1 - this.ParentToolBarHost.ToolBarMargin.Right;
    }

    internal int GetMinimumSize(int fromIndex, int tillIndex) => this.GetMinimumSize(fromIndex, tillIndex, (IQToolBar) null);

    internal int GetMinimumSize(int fromIndex, int tillIndex, IQToolBar addingToolBar)
    {
      if (this.Count <= 0)
        return 0;
      if (fromIndex < 0)
        fromIndex = 0;
      if (tillIndex > this.Count)
        tillIndex = this.Count;
      int minimumSize = 0;
      for (int index = fromIndex; index < tillIndex; ++index)
      {
        if (this[index].IsVisible)
        {
          System.Drawing.Size minimumSizeForRow = this[index].GetMinimumSizeForRow(this, addingToolBar);
          minimumSize = minimumSize + this.ParentToolBarHost.ToolBarMargin.Horizontal + (this.ParentToolBarHost.Horizontal ? minimumSizeForRow.Width : minimumSizeForRow.Height);
        }
      }
      return minimumSize;
    }

    internal int GetRequestedSize(int fromIndex, int tillIndex)
    {
      if (this.Count <= 0)
        return 0;
      if (fromIndex < 0)
        fromIndex = 0;
      if (tillIndex > this.Count)
        tillIndex = this.Count;
      int requestedSize = 0;
      for (int index = fromIndex; index < tillIndex; ++index)
      {
        if (this[index].IsVisible)
          requestedSize = requestedSize + this.ParentToolBarHost.ToolBarMargin.Horizontal + (this.ParentToolBarHost.Horizontal ? this[index].RequestedSize.Width : this[index].RequestedSize.Height);
      }
      return requestedSize;
    }

    internal int GetUsedSize(int fromIndex, int tillIndex)
    {
      if (this.Count <= 0)
        return 0;
      if (fromIndex < 0)
        fromIndex = 0;
      if (tillIndex > this.Count)
        tillIndex = this.Count;
      int usedSize = 0;
      for (int index = fromIndex; index < tillIndex; ++index)
      {
        if (this[index].IsVisible)
          usedSize = usedSize + this.ParentToolBarHost.ToolBarMargin.Horizontal + (this.ParentToolBarHost.Horizontal ? this[index].ProposedBounds.Width : this[index].ProposedBounds.Height);
      }
      return usedSize;
    }

    internal int LayoutProposedBounds(
      int fromIndex,
      int tillIndex,
      int startPosition,
      int availableSpace)
    {
      if (fromIndex < 0)
        fromIndex = 0;
      if (tillIndex > this.Count)
        tillIndex = this.Count;
      if (fromIndex == tillIndex)
        return 0;
      int num1 = this.GetUsedSize(fromIndex, tillIndex) - availableSpace;
      bool higher = num1 < 0;
      int comparePriority1 = higher ? 0 : int.MaxValue;
      if (this.ParentToolBarHost.Horizontal)
      {
        IQToolBar toolBarWithPriority1;
        while ((toolBarWithPriority1 = this.GetToolBarWithPriority(comparePriority1, fromIndex, tillIndex, higher)) != null)
        {
          comparePriority1 = higher ? toolBarWithPriority1.UserPriority + 1 : toolBarWithPriority1.UserPriority - 1;
          if (toolBarWithPriority1.IsVisible)
          {
            if (num1 > 0)
            {
              int width = Math.Max(toolBarWithPriority1.MinimumSize.Width, toolBarWithPriority1.ProposedBounds.Width - num1);
              num1 -= toolBarWithPriority1.ProposedBounds.Width - width;
              toolBarWithPriority1.ProposedBounds = QMath.SetWidth(toolBarWithPriority1.ProposedBounds, width);
            }
            else if (num1 < 0)
            {
              int width = Math.Min(toolBarWithPriority1.RequestedSize.Width, toolBarWithPriority1.ProposedBounds.Width - num1);
              num1 += width - toolBarWithPriority1.ProposedBounds.Width;
              toolBarWithPriority1.ProposedBounds = QMath.SetWidth(toolBarWithPriority1.ProposedBounds, width);
            }
          }
        }
        int comparePriority2 = 0;
        IQToolBar toolBarWithPriority2;
        while ((toolBarWithPriority2 = this.GetToolBarWithPriority(comparePriority2, fromIndex, tillIndex, true)) != null)
        {
          comparePriority2 = toolBarWithPriority2.UserPriority + 1;
          if (toolBarWithPriority2.IsVisible)
          {
            int minimumStart = this.GetMinimumStart(toolBarWithPriority2, fromIndex, startPosition);
            int maximumEndPosition = this.GetMaximumEndPosition(toolBarWithPriority2, tillIndex, startPosition + availableSpace);
            int num2 = toolBarWithPriority2.UserRequestedPosition >= 0 ? toolBarWithPriority2.UserRequestedPosition : toolBarWithPriority2.ProposedBounds.Left;
            if (minimumStart > num2)
              num2 = minimumStart;
            if (num2 + toolBarWithPriority2.ProposedBounds.Width > maximumEndPosition)
              num2 = maximumEndPosition - toolBarWithPriority2.ProposedBounds.Width;
            toolBarWithPriority2.ProposedBounds = QMath.SetX(toolBarWithPriority2.ProposedBounds, num2);
          }
        }
      }
      else
      {
        IQToolBar toolBarWithPriority3;
        while ((toolBarWithPriority3 = this.GetToolBarWithPriority(comparePriority1, fromIndex, tillIndex, higher)) != null)
        {
          comparePriority1 = higher ? toolBarWithPriority3.UserPriority + 1 : toolBarWithPriority3.UserPriority - 1;
          if (toolBarWithPriority3.IsVisible)
          {
            if (num1 > 0)
            {
              int height = Math.Max(toolBarWithPriority3.MinimumSize.Height, toolBarWithPriority3.ProposedBounds.Height - num1);
              num1 -= toolBarWithPriority3.ProposedBounds.Height - height;
              toolBarWithPriority3.ProposedBounds = QMath.SetHeight(toolBarWithPriority3.ProposedBounds, height);
            }
            else if (num1 < 0)
            {
              int height = Math.Min(toolBarWithPriority3.RequestedSize.Height, toolBarWithPriority3.ProposedBounds.Height - num1);
              num1 += height - toolBarWithPriority3.ProposedBounds.Height;
              toolBarWithPriority3.ProposedBounds = QMath.SetHeight(toolBarWithPriority3.ProposedBounds, height);
            }
          }
        }
        int comparePriority3 = 0;
        IQToolBar toolBarWithPriority4;
        while ((toolBarWithPriority4 = this.GetToolBarWithPriority(comparePriority3, fromIndex, tillIndex, true)) != null)
        {
          comparePriority3 = toolBarWithPriority4.UserPriority + 1;
          if (toolBarWithPriority4.IsVisible)
          {
            int minimumStart = this.GetMinimumStart(toolBarWithPriority4, fromIndex, startPosition);
            int maximumEndPosition = this.GetMaximumEndPosition(toolBarWithPriority4, tillIndex, startPosition + availableSpace);
            int num3 = toolBarWithPriority4.UserRequestedPosition >= 0 ? toolBarWithPriority4.UserRequestedPosition : toolBarWithPriority4.ProposedBounds.Top;
            if (num3 + toolBarWithPriority4.ProposedBounds.Height > maximumEndPosition)
              num3 = maximumEndPosition - toolBarWithPriority4.ProposedBounds.Height;
            if (minimumStart > num3)
              num3 = minimumStart;
            toolBarWithPriority4.ProposedBounds = QMath.SetY(toolBarWithPriority4.ProposedBounds, num3);
          }
        }
      }
      return num1;
    }

    internal void LayoutToolBars(IQToolBar movingToolBar)
    {
      int startPosition = this.ParentToolBarHost.ToolBarsStartPosition;
      int availableSpace1 = this.ParentToolBarHost.ToolBarsAvailableSpace;
      if (movingToolBar != null && !this.Contains(movingToolBar))
        movingToolBar = (IQToolBar) null;
      for (int index = 0; index < this.Count; ++index)
      {
        IQToolBar qtoolBar = this[index];
        if (qtoolBar.Stretched)
          qtoolBar.ProposedBounds = !this.ParentToolBarHost.Horizontal ? new Rectangle(qtoolBar.ProposedBounds.Left, startPosition + this.ParentToolBarHost.ToolBarMargin.Left, qtoolBar.ProposedBounds.Height, availableSpace1 - this.ParentToolBarHost.ToolBarMargin.Horizontal) : new Rectangle(startPosition + this.ParentToolBarHost.ToolBarMargin.Left, qtoolBar.ProposedBounds.Top, availableSpace1 - this.ParentToolBarHost.ToolBarMargin.Horizontal, qtoolBar.ProposedBounds.Height);
      }
      this.Size = 0;
      int num1 = this.GetUsedSize(0, this.Count) - availableSpace1;
      if (num1 > 0)
      {
        int minimumSize = this.GetMinimumSize(0, this.Count);
        if (minimumSize > availableSpace1)
        {
          for (int index = this.Count - 1; minimumSize > availableSpace1 && index > 0; --index)
          {
            IQToolBar toolBar = this[index];
            if (toolBar.OriginalToolBarRow == null)
              toolBar.OriginalToolBarRow = this;
            toolBar.UserRequestedPosition = 0;
            this.ParentCollection.InsertToolBarIntoNextRow(this, toolBar);
            minimumSize -= (this.ParentToolBarHost.Horizontal ? toolBar.MinimumSize.Width : toolBar.MinimumSize.Height) - this.ParentToolBarHost.ToolBarMargin.Horizontal;
          }
        }
      }
      else if (num1 < 0)
      {
        IQToolBar[] toolBarsForThisRow = this.ParentCollection.GetToolBarsForThisRow(this);
        if (toolBarsForThisRow != null)
        {
          int index = 0;
          for (bool flag = false; index < toolBarsForThisRow.Length && !flag; ++index)
          {
            int minimumSize = this.GetMinimumSize(0, this.Count, toolBarsForThisRow[index]);
            int num2 = availableSpace1 - minimumSize;
            System.Drawing.Size minimumSizeForRow = toolBarsForThisRow[index].GetMinimumSizeForRow(this, (IQToolBar) null);
            if (this.ParentToolBarHost.Horizontal ? num2 > minimumSizeForRow.Width : num2 > minimumSizeForRow.Height)
            {
              if (toolBarsForThisRow[index].OriginalToolBarRow == this)
                toolBarsForThisRow[index].OriginalToolBarRow = (QToolBarRow) null;
              this.Add(toolBarsForThisRow[index]);
            }
            else
              flag = true;
          }
        }
      }
      int num3 = 0;
      IQToolBar previousVisibleToolBar = this.GetPreviousVisibleToolBar(movingToolBar);
      if (movingToolBar != null && previousVisibleToolBar != null)
      {
        num3 = this.IndexOf(movingToolBar);
        int val1 = !this.ParentToolBarHost.Horizontal ? movingToolBar.ProposedBounds.Top - this.ParentToolBarHost.ToolBarMargin.Left : movingToolBar.ProposedBounds.Left - this.ParentToolBarHost.ToolBarMargin.Left;
        int minimumSize1 = this.GetMinimumSize(num3, this.Count);
        int requestedSize1 = this.GetRequestedSize(0, num3);
        int requestedSize2 = this.GetRequestedSize(num3, this.Count);
        int minimumSize2 = this.GetMinimumSize(0, num3);
        int num4 = Math.Min(Math.Max(val1, startPosition + minimumSize2), startPosition + availableSpace1 - minimumSize1);
        if (num1 == 0)
          num4 = Math.Min(Math.Max(num4, startPosition + availableSpace1 - requestedSize2), startPosition + requestedSize1);
        else if (num1 < 0)
          num4 = Math.Min(Math.Max(num4, startPosition + requestedSize1), startPosition + availableSpace1 - requestedSize2);
        int num5 = num4 + this.ParentToolBarHost.ToolBarMargin.Left;
        if (this.ParentToolBarHost.Horizontal)
        {
          movingToolBar.ProposedBounds = QMath.SetX(movingToolBar.ProposedBounds, num5);
          int availableSpace2 = movingToolBar.ProposedBounds.Left - this.ParentToolBarHost.ToolBarMargin.Left - startPosition;
          this.LayoutProposedBounds(0, num3, startPosition, availableSpace2);
          startPosition = Math.Max(previousVisibleToolBar.ProposedBounds.Right + this.ParentToolBarHost.ToolBarMargin.Right, num4);
        }
        else
        {
          movingToolBar.ProposedBounds = QMath.SetY(movingToolBar.ProposedBounds, num5);
          int availableSpace3 = movingToolBar.ProposedBounds.Top - this.ParentToolBarHost.ToolBarMargin.Left - startPosition;
          this.LayoutProposedBounds(0, num3, startPosition, availableSpace3);
          startPosition = Math.Max(previousVisibleToolBar.ProposedBounds.Bottom + this.ParentToolBarHost.ToolBarMargin.Right, num4);
        }
        availableSpace1 = this.ParentToolBarHost.ToolBarsEndPosition - startPosition;
      }
      this.LayoutProposedBounds(num3, this.Count, startPosition, availableSpace1);
      for (int index = 0; index < this.Count; ++index)
      {
        IQToolBar qtoolBar = this[index];
        if (qtoolBar.IsVisible)
        {
          if (this.ParentToolBarHost.Horizontal)
          {
            qtoolBar.ProposedBounds = new Rectangle(qtoolBar.ProposedBounds.Left, this.Start, qtoolBar.ProposedBounds.Width, 0);
            qtoolBar.LayoutToolBar(qtoolBar.ProposedBounds, this.ParentToolBarHost.UsedOrientation, QToolBarLayoutFlags.FixedWidth | QToolBarLayoutFlags.DoNotSetBounds);
            this.Size = Math.Max(qtoolBar.ProposedBounds.Height, this.Size);
          }
          else
          {
            qtoolBar.ProposedBounds = new Rectangle(this.Start, qtoolBar.ProposedBounds.Top, 0, qtoolBar.ProposedBounds.Height);
            qtoolBar.LayoutToolBar(qtoolBar.ProposedBounds, this.ParentToolBarHost.UsedOrientation, QToolBarLayoutFlags.FixedWidth | QToolBarLayoutFlags.DoNotSetBounds);
            this.Size = Math.Max(qtoolBar.ProposedBounds.Width, this.Size);
          }
        }
      }
      for (int index = 0; index < this.Count; ++index)
      {
        IQToolBar qtoolBar = this[index];
        if (qtoolBar.IsVisible)
        {
          qtoolBar.ProposedBounds = !this.ParentToolBarHost.Horizontal ? new Rectangle(this.Start, qtoolBar.ProposedBounds.Top, this.Size, qtoolBar.ProposedBounds.Height) : new Rectangle(qtoolBar.ProposedBounds.Left, this.Start, qtoolBar.ProposedBounds.Width, this.Size);
          QToolBarLayoutFlags layoutFlags = (QToolBarLayoutFlags) (3 | (qtoolBar.ProposedBounds == qtoolBar.Bounds ? 16 : 0));
          qtoolBar.LayoutToolBar(qtoolBar.ProposedBounds, this.ParentToolBarHost.UsedOrientation, layoutFlags);
        }
      }
    }

    internal void DecreasePriorityFromToolBar(IQToolBar toolBar)
    {
      if (toolBar.UserPriority == this.Count - 1)
        return;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].UserPriority > toolBar.UserPriority)
          --this[index].UserPriority;
      }
    }

    internal void SetAsFirstPriority(IQToolBar toolBar)
    {
      if (toolBar.UserPriority == 0)
        return;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].UserPriority < toolBar.UserPriority)
          ++this[index].UserPriority;
      }
      toolBar.UserPriority = 0;
    }

    internal void SetPrioritiesForInsert(IQToolBar toolBarToInsert, int priority)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].UserPriority >= priority)
          ++this[index].UserPriority;
      }
      toolBarToInsert.UserPriority = priority;
    }

    internal QToolBarRowPositionType GetPositionType(Point point)
    {
      int num = this.ParentToolBarHost.Horizontal ? point.Y : point.X;
      if (num < this.Start)
        return QToolBarRowPositionType.BeforeToolBarRow;
      return num < this.End ? QToolBarRowPositionType.OnToolBarRow : QToolBarRowPositionType.AfterToolBarRow;
    }

    internal int GetToolBarInsertIndex(Point point)
    {
      int num = this.ParentToolBarHost.Horizontal ? point.X : point.Y;
      for (int index = 0; index < this.Count; ++index)
      {
        if ((this.ParentToolBarHost.Horizontal ? this[index].Bounds.Left + this[index].Bounds.Width / 2 : this[index].Bounds.Top + this[index].Bounds.Height / 2) > num)
          return this[index].ToolBarPositionIndex;
      }
      return this.ToolBarPositionIndexForAdd;
    }

    internal void Add(IQToolBar toolBar)
    {
      QToolBarRow.RemoveFromOldToolBarRow(toolBar);
      this.SetPrioritiesForInsert(toolBar, this.Count);
      toolBar.SetToolBarPositionIndex(this.ToolBarPositionIndexForAdd, false);
      this.InnerList.Add((object) toolBar);
      toolBar.ParentToolBarRow = this;
      this.m_oParentCollection.NotifyRowChanged(this);
    }

    internal void Remove(IQToolBar toolBar)
    {
      this.DecreasePriorityFromToolBar(toolBar);
      if (this.ParentToolBarHost == null || !this.ParentToolBarHost.Initializing)
        this.DecreaseToolBarPositionIndices(toolBar);
      this.InnerList.Remove((object) toolBar);
      toolBar.ParentToolBarRow = (QToolBarRow) null;
      this.m_oParentCollection.NotifyRowChanged(this);
    }

    internal void Insert(int index, IQToolBar toolBar)
    {
      if (index >= 0)
      {
        QToolBarRow.RemoveFromOldToolBarRow(toolBar);
        this.SetPrioritiesForInsert(toolBar, this.Count);
        toolBar.SetToolBarPositionIndex(this.ToolBarPositionIndexForAdd, false);
        this.InnerList.Add((object) toolBar);
        toolBar.ParentToolBarRow = this;
        this.SetToolBarPositionIndex(toolBar, index);
        this.m_oParentCollection.NotifyRowChanged(this);
      }
      else
        this.Add(toolBar);
    }

    private static void RemoveFromOldToolBarRow(IQToolBar toolBar)
    {
      if (toolBar.ParentToolBarRow == null)
        return;
      toolBar.ParentToolBarRow.Remove(toolBar);
      toolBar.ParentToolBarRow = (QToolBarRow) null;
    }

    internal bool IsStretched
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Stretched)
            return true;
        }
        return false;
      }
    }

    internal int IndexOf(IQToolBar toolBar) => this.InnerList.IndexOf((object) toolBar);

    internal IQToolBar this[int index] => (IQToolBar) this.InnerList[index];

    internal bool Contains(IQToolBar toolBar) => this.InnerList.Contains((object) toolBar);
  }
}
