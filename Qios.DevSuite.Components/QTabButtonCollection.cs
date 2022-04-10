// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public sealed class QTabButtonCollection : CollectionBase
  {
    private QTabStrip m_oTabStrip;
    private QTabButtonComparer m_oComparer;

    internal QTabButtonCollection(QTabStrip tabStrip)
    {
      this.m_oComparer = new QTabButtonComparer();
      this.m_oTabStrip = tabStrip != null ? tabStrip : throw new InvalidOperationException("The TabStrip cannot be null");
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new void RemoveAt(int index) => base.RemoveAt(index);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new void Clear() => base.Clear();

    public QTabStrip TabStrip => this.m_oTabStrip;

    public bool HasVisibleButtons => this.HasButtonsThatMatchSelection(QTabButtonSelectionTypes.MustBeVisible, (QTabButtonRow) null);

    public bool HasAccessisbleButtons => this.HasButtonsThatMatchSelection(QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeEnabled, (QTabButtonRow) null);

    public int VisibleButtonCount => this.GetButtonCountThatMatchSelection(QTabButtonSelectionTypes.MustBeVisible, (QTabButtonRow) null);

    public int AccessibleButtonCount => this.GetButtonCountThatMatchSelection(QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeEnabled, (QTabButtonRow) null);

    public QTabButton this[int index] => (QTabButton) this.InnerList[index];

    public int IndexOf(QTabButton button) => this.InnerList.Contains((object) button) ? this.InnerList.IndexOf((object) button) : -1;

    public bool Contains(QTabButton button) => this.InnerList.Contains((object) button);

    public void CopyTo(QTabButton[] buttons, int index) => ((ICollection) this).CopyTo((Array) buttons, index);

    public int GetButtonIndexWithControl(Control control)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Control == control)
          return index;
      }
      return -1;
    }

    public QTabButton GetButtonWithControl(Control control)
    {
      int indexWithControl = this.GetButtonIndexWithControl(control);
      return indexWithControl >= 0 ? this[indexWithControl] : (QTabButton) null;
    }

    public int GetButtonCountThatMatchSelection(
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row)
    {
      int thatMatchSelection = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].MatchesSelection(selectionType, row))
          ++thatMatchSelection;
      }
      return thatMatchSelection;
    }

    public bool HasButtonsThatMatchSelection(
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].MatchesSelection(selectionType, row))
          return true;
      }
      return false;
    }

    public QTabButton GetPreviousButton(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row,
      bool loopAround)
    {
      int index = fromButton != null ? this.IndexOf(fromButton) - 1 : this.Count - 1;
      QTabButton previousButton = (QTabButton) null;
      while (previousButton == null && index >= 0)
      {
        QTabButton qtabButton = this[index];
        if (qtabButton.MatchesSelection(selectionType, row))
          previousButton = qtabButton;
        else
          --index;
      }
      if (previousButton != null)
        return previousButton;
      return loopAround && this.Count > 0 ? this.GetPreviousButton((QTabButton) null, selectionType, (QTabButtonRow) null, false) : (QTabButton) null;
    }

    public QTabButton GetNextButton(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row,
      bool loopAround)
    {
      int index = fromButton != null ? this.IndexOf(fromButton) + 1 : 0;
      QTabButton nextButton = (QTabButton) null;
      while (nextButton == null && index < this.Count)
      {
        QTabButton qtabButton = this[index];
        if (qtabButton.MatchesSelection(selectionType, row))
          nextButton = qtabButton;
        else
          ++index;
      }
      if (nextButton != null)
        return nextButton;
      return loopAround && this.Count > 0 ? this.GetNextButton((QTabButton) null, selectionType, row, false) : (QTabButton) null;
    }

    public QTabButton GetNextButtonForNavigation(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      bool loopAround)
    {
      if (this.Count == 0)
        return (QTabButton) null;
      if (fromButton == null)
        return this.GetLowestTabButtonForNavigation(selectionType);
      QTabButton y = (QTabButton) null;
      for (int index = 0; index < this.Count; ++index)
      {
        QTabButton x = this[index];
        if (x.MatchesSelection(selectionType, (QTabButtonRow) null) && this.m_oComparer.Compare(x, fromButton, true) > 0 && (y == null || this.m_oComparer.Compare(x, y, true) < 0))
          y = x;
      }
      return y == null && loopAround && fromButton != null ? this.GetNextButtonForNavigation((QTabButton) null, selectionType, false) : y;
    }

    public QTabButton GetPreviousButtonForNavigation(
      QTabButton fromButton,
      QTabButtonSelectionTypes selectionType,
      bool loopAround)
    {
      if (this.Count == 0)
        return (QTabButton) null;
      if (fromButton == null)
        return this.GetHighestButtonForNavigation(selectionType);
      QTabButton y = (QTabButton) null;
      for (int index = this.Count - 1; index >= 0; --index)
      {
        QTabButton x = this[index];
        if (x.MatchesSelection(selectionType, (QTabButtonRow) null) && this.m_oComparer.Compare(x, fromButton, true) < 0 && (y == null || this.m_oComparer.Compare(x, y, true) > 0))
          y = x;
      }
      return y == null && loopAround && fromButton != null ? this.GetPreviousButtonForNavigation((QTabButton) null, selectionType, false) : y;
    }

    internal void Add(QTabButton button) => this.Add(button, button.ButtonOrder, false);

    internal void Add(QTabButton button, int buttonOrder, bool activateButton)
    {
      if (!this.Contains(button))
      {
        if (button.TabButtonCollection != null && button.TabButtonCollection.Contains(button))
          throw new InvalidOperationException(QResources.GetException("QTabButtonCollection_ButtonAlreadyInOtherCollection", (object) button.Text));
        this.InnerList.Add((object) button);
      }
      if (buttonOrder < 0)
        buttonOrder = this.GetMaxButtonOrderForButton(button) + 1;
      button.SetButtonOrder(buttonOrder, false);
      if (!this.m_oTabStrip.IsInitializing)
      {
        this.SortWhenRequired();
        this.IncreaseButtonOrders(buttonOrder, button);
      }
      button.PutTabButtonCollection(this);
      if (buttonOrder < 0)
        buttonOrder = this.GetMaxButtonOrderForButton(button) + 1;
      button.SetButtonOrder(buttonOrder, false);
      this.SortWhenRequired();
      if (activateButton)
        this.m_oTabStrip.SetActiveButton(button, true, true, false);
      this.NotifyTabStripOfChange();
    }

    internal void Insert(int index, QTabButton button) => throw new NotImplementedException();

    internal void Remove(QTabButton button, bool activateNewTabWhenActive)
    {
      if (button != null)
      {
        if (this.m_oTabStrip != null)
          this.m_oTabStrip.HandleTabButtonRemoving(button, activateNewTabWhenActive);
        this.InnerList.Remove((object) button);
        if (!this.m_oTabStrip.IsInitializing)
        {
          int buttonOrder1 = button.ButtonOrder;
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index] != button && this[index].ButtonOrder > button.ButtonOrder)
            {
              int buttonOrder2 = buttonOrder1;
              buttonOrder1 = this[index].ButtonOrder;
              this[index].SetButtonOrder(buttonOrder2, false);
            }
          }
        }
      }
      this.NotifyTabStripOfChange();
      button?.PutTabButtonCollection((QTabButtonCollection) null);
    }

    internal void Remove(QTabButton button) => this.Remove(button, true);

    internal bool SortRequired
    {
      get
      {
        for (int index = 0; index < this.Count - 1; ++index)
        {
          if (this.m_oComparer.Compare((object) this[index], (object) this[index + 1]) > 0)
            return true;
        }
        return false;
      }
    }

    internal void Sort() => this.InnerList.Sort((IComparer) this.m_oComparer);

    internal void SortWhenRequired()
    {
      if (!this.SortRequired)
        return;
      this.Sort();
    }

    internal int GetMaxButtonOrderForButton(QTabButton button)
    {
      if (button.Configuration.Alignment == QTabButtonAlignment.Far)
        return this.GetMaxButtonOrder(QTabButtonSelectionTypes.MustBeNearAligned, button, (QTabButtonRow) null);
      return button.Configuration.Alignment == QTabButtonAlignment.Near ? this.GetMaxButtonOrder(QTabButtonSelectionTypes.MustBeNearAligned, button, (QTabButtonRow) null) : 0;
    }

    internal int GetMaxButtonOrder(
      QTabButtonSelectionTypes selectionType,
      QTabButton ignoreButton,
      QTabButtonRow row)
    {
      int val2 = -1;
      for (int index = 0; index < this.Count; ++index)
      {
        QTabButton qtabButton = this[index];
        if (qtabButton != ignoreButton && qtabButton.MatchesSelection(selectionType, row))
          val2 = Math.Max(qtabButton.ButtonOrder, val2);
      }
      return val2;
    }

    internal QTabButton GetLowestTabButtonForNavigation(
      QTabButtonSelectionTypes selectionType)
    {
      if (this.Count == 0)
        return (QTabButton) null;
      QTabButton y = (QTabButton) null;
      for (int index = 0; index < this.Count; ++index)
      {
        QTabButton x = this[index];
        if (x.MatchesSelection(selectionType, (QTabButtonRow) null) && (y == null || this.m_oComparer.Compare(x, y, true) < 0))
          y = x;
      }
      return y;
    }

    internal QTabButton GetHighestButtonForNavigation(
      QTabButtonSelectionTypes selectionType)
    {
      if (this.Count == 0)
        return (QTabButton) null;
      QTabButton y = (QTabButton) null;
      for (int index = 0; index < this.Count; ++index)
      {
        QTabButton x = this[index];
        if (x.MatchesSelection(selectionType, (QTabButtonRow) null) && (y == null || this.m_oComparer.Compare(x, y, true) > 0))
          y = x;
      }
      return y;
    }

    internal void DecreaseButtonOrders(int afterOrder, QTabButton ignoreButton)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != ignoreButton && this[index].ButtonOrder > afterOrder)
          this[index].SetButtonOrder(this[index].ButtonOrder - 1, false);
      }
    }

    internal void IncreaseButtonOrders(int fromOrder, QTabButton ignoreButton)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ButtonOrder == fromOrder && this[index] != ignoreButton)
        {
          ++fromOrder;
          this[index].SetButtonOrder(fromOrder, false);
        }
      }
    }

    internal void SetButtonOrder(QTabButton button, int order) => this.SetButtonOrder(button, order, true);

    internal void SetButtonOrder(QTabButton button, int order, bool notifyTabStripOfChange)
    {
      this.SortWhenRequired();
      if (order < 0)
        order = this.GetMaxButtonOrderForButton(button) + 1;
      if (this.m_oTabStrip.IsInitializing)
        button.SetButtonOrder(order, false);
      else if (order > button.ButtonOrder)
      {
        int index1 = this.IndexOf(button);
        for (int index = index1 + 1; index < this.Count; ++index)
        {
          QTabButton qtabButton = this[index];
          if (qtabButton.ButtonOrder >= order)
          {
            if (qtabButton.ButtonOrder > order)
            {
              button.SetButtonOrder(order, false);
              break;
            }
            break;
          }
          this.SwapTabButtonAndOrder(index1, index);
          ++index1;
        }
      }
      else if (order < button.ButtonOrder)
      {
        int index1 = this.IndexOf(button);
        for (int index = index1 - 1; index >= 0; --index)
        {
          if (this[index].ButtonOrder < order)
          {
            if (button.ButtonOrder != order)
            {
              button.SetButtonOrder(order, false);
              break;
            }
            break;
          }
          this.SwapTabButtonAndOrder(index1, index);
          --index1;
        }
      }
      else if (order != button.ButtonOrder)
        this.SetButtonOrder(button, order);
      if (!notifyTabStripOfChange)
        return;
      this.NotifyTabStripOfChange();
    }

    private void SwapTabButtonAndOrder(int index1, int index2)
    {
      QTabButton qtabButton1 = this[index1];
      QTabButton qtabButton2 = this[index2];
      this.InnerList[index1] = (object) qtabButton2;
      this.InnerList[index2] = (object) qtabButton1;
      int buttonOrder = qtabButton1.ButtonOrder;
      qtabButton1.SetButtonOrder(qtabButton2.ButtonOrder, false);
      qtabButton2.SetButtonOrder(buttonOrder, false);
    }

    private void SwapTabButtonOrder(QTabButton button1, QTabButton button2)
    {
      int buttonOrder = button1.ButtonOrder;
      button1.SetButtonOrder(button2.ButtonOrder, false);
      button2.SetButtonOrder(buttonOrder, false);
    }

    internal void NotifyTabStripOfChange()
    {
      if (this.m_oTabStrip == null || this.m_oTabStrip.IsDisposed)
        return;
      this.m_oTabStrip.HandleTabButtonCollectionChanged();
    }

    protected override void OnClear()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].PutTabButtonCollection((QTabButtonCollection) null);
      base.OnClear();
    }
  }
}
