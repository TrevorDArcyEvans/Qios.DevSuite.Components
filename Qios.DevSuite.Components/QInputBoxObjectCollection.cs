// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxObjectCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [ListBindable(false)]
  public class QInputBoxObjectCollection : IList, ICollection, IEnumerable
  {
    private ArrayList m_oInnerList;
    private IComparer m_oComparer;
    private QInputBox m_oOwner;

    public QInputBoxObjectCollection(QInputBox owner) => this.m_oOwner = owner;

    internal void Sort() => this.InnerList.Sort(this.Comparer);

    public int Add(object item)
    {
      this.m_oOwner.CheckNoDataSource();
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      this.InnerList.Add(item);
      int num = -1;
      bool flag = false;
      try
      {
        if (this.m_oOwner.Sorted)
        {
          this.InnerList.Sort(this.Comparer);
          num = this.InnerList.IndexOf(item);
        }
        else
          num = this.InnerList.Count - 1;
        flag = true;
      }
      finally
      {
        if (!flag)
          this.InnerList.Remove(item);
      }
      this.m_oOwner.UpdateAutoComplete();
      return num;
    }

    public void AddRange(object[] items)
    {
      this.m_oOwner.CheckNoDataSource();
      this.AddRangeInternal((IList) items);
      this.m_oOwner.UpdateAutoComplete();
    }

    internal void AddRangeInternal(IList items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      foreach (object obj in (IEnumerable) items)
      {
        if (obj == null)
          throw new ArgumentNullException("item");
      }
      this.InnerList.AddRange((ICollection) items);
      if (!this.m_oOwner.Sorted)
        return;
      this.InnerList.Sort(this.Comparer);
    }

    public void Clear()
    {
      this.m_oOwner.CheckNoDataSource();
      this.ClearInternal(false);
      this.m_oOwner.UpdateAutoComplete();
    }

    internal void ClearInternal(bool silent)
    {
      this.InnerList.Clear();
      if (silent)
        return;
      this.m_oOwner.SelectedIndex = -1;
    }

    public bool Contains(object value) => this.IndexOf(value) != -1;

    public void CopyTo(object[] dest, int arrayIndex) => this.InnerList.CopyTo((Array) dest, arrayIndex);

    public IEnumerator GetEnumerator() => this.InnerList.GetEnumerator();

    public int IndexOf(object value) => value != null ? this.InnerList.IndexOf(value) : throw new ArgumentNullException(nameof (value));

    public void Insert(int index, object item)
    {
      this.m_oOwner.CheckNoDataSource();
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (index < 0 || index > this.InnerList.Count)
        throw new ArgumentOutOfRangeException();
      if (this.m_oOwner.Sorted)
        this.Add(item);
      else
        this.InnerList.Insert(index, item);
      this.m_oOwner.UpdateAutoComplete();
    }

    public void Remove(object value)
    {
      int index = this.InnerList.IndexOf(value);
      if (index == -1)
        return;
      this.RemoveAt(index);
    }

    public void RemoveAt(int index)
    {
      this.m_oOwner.CheckNoDataSource();
      if (index < 0 || index >= this.InnerList.Count)
        throw new ArgumentOutOfRangeException();
      int selectedIndex = this.m_oOwner.SelectedIndex;
      this.InnerList.RemoveAt(index);
      if (index < selectedIndex)
        --this.m_oOwner.SelectedIndex;
      else if (index == selectedIndex)
        this.m_oOwner.SelectedIndex = index >= this.InnerList.Count ? -1 : selectedIndex;
      this.m_oOwner.UpdateAutoComplete();
    }

    internal void SetItemInternal(int index, object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (index < 0 || index >= this.InnerList.Count)
        throw new ArgumentOutOfRangeException();
      this.InnerList[index] = value;
      if (this.m_oOwner.SelectedIndex != index)
        return;
      this.m_oOwner.UpdateText();
    }

    void ICollection.CopyTo(Array dest, int index) => this.InnerList.CopyTo(dest, index);

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => (object) this;

    int IList.Add(object item) => this.Add(item);

    bool IList.IsFixedSize => false;

    private IComparer Comparer
    {
      get
      {
        if (this.m_oComparer == null)
          this.m_oComparer = (IComparer) new QInputBoxItemComparer(this.m_oOwner);
        return this.m_oComparer;
      }
    }

    public int Count => this.InnerList.Count;

    private ArrayList InnerList
    {
      get
      {
        if (this.m_oInnerList == null)
          this.m_oInnerList = new ArrayList();
        return this.m_oInnerList;
      }
    }

    public bool IsReadOnly => false;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual object this[int index]
    {
      get
      {
        if (index < 0 || index >= this.InnerList.Count)
          throw new ArgumentOutOfRangeException();
        return this.InnerList[index];
      }
      set
      {
        this.m_oOwner.CheckNoDataSource();
        this.SetItemInternal(index, value);
        this.m_oOwner.UpdateAutoComplete();
      }
    }
  }
}
