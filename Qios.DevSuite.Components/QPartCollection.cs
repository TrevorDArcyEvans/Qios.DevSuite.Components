// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  public class QPartCollection : 
    CollectionBase,
    IQPartCollection,
    ICloneable,
    IList,
    ICollection,
    IEnumerable
  {
    private bool m_bAllowAddRemoveSystemParts;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private int m_iSuspendChangeNotification;
    private string m_sLastContentLayoutOrder;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQManagedLayoutParent m_oDisplayParent;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private IQPart m_oParentPart;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private ArrayList m_aCachedSystemParts;

    internal QPartCollection()
    {
    }

    internal QPartCollection(IQPart parentPart, IQManagedLayoutParent displayParent)
    {
      this.m_oParentPart = parentPart;
      this.m_oDisplayParent = displayParent;
    }

    public int Add(IQPart part) => this.Add(part, true);

    public int Add(IQPart part, bool removeFromCurrentParent)
    {
      int startIndex = -1;
      if (this.CanAddOrRemove(part))
      {
        this.InnerList.Add((object) part);
        startIndex = this.InnerList.Count - 1;
        this.SetPartIndices(startIndex);
        this.ResetSinglePartParents(part, removeFromCurrentParent);
        this.HandleChange(true);
      }
      return startIndex;
    }

    public void Insert(int index, IQPart part) => this.Insert(index, part, true);

    public void Insert(int index, IQPart part, bool removeFromCurrentParent)
    {
      if (!this.CanAddOrRemove(part))
        return;
      this.InnerList.Insert(index, (object) part);
      this.SetPartIndices(index);
      this.ResetSinglePartParents(part, removeFromCurrentParent);
      this.HandleChange(true);
    }

    public void AddRange(IQPartCollection collection) => this.AddRange(collection, true);

    public void AddRange(IQPartCollection collection, bool removePartsFromCurrentParent)
    {
      for (int index = 0; index < collection.Count; ++index)
      {
        this.SuspendChangeNotification();
        this.Add(collection[index], removePartsFromCurrentParent);
        this.ResumeChangeNotification(true);
      }
    }

    public void Remove(IQPart part)
    {
      if (!this.CanAddOrRemove(part))
        return;
      int startIndex = this.InnerList.IndexOf((object) part);
      if (startIndex < 0)
        return;
      this.InnerList.Remove((object) part);
      this.ClearSinglePartParents(part);
      this.SetPartIndices(startIndex);
      this.HandleChange(true);
    }

    internal bool AllowAddRemoveSystemParts
    {
      get => this.m_bAllowAddRemoveSystemParts;
      set => this.m_bAllowAddRemoveSystemParts = value;
    }

    public IQPart ParentPart => this.m_oParentPart;

    public IQManagedLayoutParent DisplayParent => this.m_oDisplayParent;

    [Browsable(false)]
    public int ChangeNotificationSuspended => this.m_iSuspendChangeNotification;

    public IQPart this[int index]
    {
      get => this.InnerList[index] as IQPart;
      set => this.SetPart(index, value, true);
    }

    public void SetPart(int index, IQPart value, bool removeValueFromCurrentParentPart)
    {
      IQPart part = this[index];
      if (part != null)
        this.ClearSinglePartParents(part);
      if (value != null)
      {
        this.ResetSinglePartParents(value, removeValueFromCurrentParentPart);
        value.CalculatedProperties.PutPartIndex(index);
      }
      this.HandleChange(true);
    }

    public IQPart this[string partName]
    {
      get
      {
        int index = this.IndexOf(partName);
        return index >= 0 ? this[index] : (IQPart) null;
      }
    }

    public int IndexOf(IQPart part) => this.InnerList.IndexOf((object) part);

    public bool Contains(IQPart part) => this.InnerList.Contains((object) part);

    public int IndexOf(string name)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.Compare(this[index].PartName, name, true, CultureInfo.InvariantCulture) == 0)
          return index;
      }
      return -1;
    }

    public IQPart FindPart(string name)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        IQPart part1 = this[index];
        if (string.Compare(part1.PartName, name, true) == 0)
          return part1;
        if (part1.Parts != null)
        {
          IQPart part2 = part1.Parts.FindPart(name);
          if (part2 != null)
            return part2;
        }
      }
      return (IQPart) null;
    }

    protected override void OnClear()
    {
      if (this.m_aCachedSystemParts != null)
        this.m_aCachedSystemParts.Clear();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this.CanAddOrRemove(this[index]))
        {
          this.ClearSinglePartParents(this[index]);
        }
        else
        {
          if (this.m_aCachedSystemParts == null)
            this.m_aCachedSystemParts = new ArrayList();
          this.m_aCachedSystemParts.Add((object) this[index]);
        }
      }
      base.OnClear();
    }

    protected override void OnClearComplete()
    {
      if (this.m_aCachedSystemParts != null)
      {
        for (int index = 0; index < this.m_aCachedSystemParts.Count; ++index)
          this.InnerList.Add(this.m_aCachedSystemParts[index]);
        this.m_aCachedSystemParts.Clear();
      }
      base.OnClearComplete();
      this.HandleChange(true);
    }

    public void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      this.m_oDisplayParent = displayParent;
      for (int index = 0; index < this.Count; ++index)
        this[index].SetDisplayParent(this.m_oDisplayParent);
    }

    public void SetParent(IQPart parentPart, bool removePartFromCurrentParent)
    {
      this.m_oParentPart = parentPart;
      for (int index = 0; index < this.Count; ++index)
        this[index].SetParent(this.m_oParentPart, this, removePartFromCurrentParent, false);
    }

    internal void ResetPartParents(bool removePartFromCurrentParent)
    {
      for (int index = 0; index < this.Count; ++index)
        this.ResetSinglePartParents(this[index], removePartFromCurrentParent);
    }

    internal void ResetSinglePartParents(IQPart part, bool removePartFromCurrentParent)
    {
      if (part == null)
        return;
      if (this.m_oParentPart != null)
        part.SetParent(this.m_oParentPart, this, removePartFromCurrentParent, false);
      if (this.m_oDisplayParent == null)
        return;
      part.SetDisplayParent(this.m_oDisplayParent);
    }

    internal void ClearSinglePartParents(IQPart part)
    {
      if (part == null)
        return;
      if (this.m_oParentPart == part.ParentPart)
        part.SetParent((IQPart) null, (QPartCollection) null, false, false);
      if (this.m_oDisplayParent != part.DisplayParent)
        return;
      part.SetDisplayParent((IQManagedLayoutParent) null);
    }

    public int SuspendChangeNotification()
    {
      ++this.m_iSuspendChangeNotification;
      return this.m_iSuspendChangeNotification;
    }

    public int ResumeChangeNotification(bool notifyChange)
    {
      this.m_iSuspendChangeNotification = Math.Max(0, --this.m_iSuspendChangeNotification);
      if (notifyChange)
        this.HandleChange(true);
      return this.m_iSuspendChangeNotification;
    }

    private bool CanAddOrRemove(IQPart part) => !QPartHelper.IsSystemPart(part) || this.m_bAllowAddRemoveSystemParts;

    public bool SortRequired()
    {
      if (this.m_oParentPart != null)
      {
        string contentLayoutOrder = this.m_oParentPart.Properties.GetContentLayoutOrder(this.m_oParentPart);
        if (contentLayoutOrder != null && contentLayoutOrder.Length > 0)
          return string.Compare(contentLayoutOrder, this.m_sLastContentLayoutOrder, true) != 0;
      }
      QPartComparer qpartComparer = new QPartComparer(this, this.m_oParentPart);
      for (int index = 0; index < this.Count - 1; ++index)
      {
        IQPart x = this[index];
        IQPart y = this[index + 1];
        if (qpartComparer.Compare((object) x, (object) y) > 0)
          return true;
      }
      return false;
    }

    public void SetPartIndices(int startIndex)
    {
      for (int index = startIndex; index < this.Count; ++index)
        this[index].CalculatedProperties.PutPartIndex(index);
    }

    public bool SortPartsWhenRequired()
    {
      if (!this.SortRequired())
        return false;
      this.SortParts();
      return true;
    }

    public void CopyTo(Array array, int index) => this.InnerList.CopyTo(array, index);

    public void CopyTo(IList list)
    {
      for (int index = 0; index < this.Count; ++index)
        list.Add((object) this[index]);
    }

    public void SortParts() => this.SortParts(false);

    public void SortParts(bool force)
    {
      if (this.m_oParentPart != null)
      {
        string contentLayoutOrder = this.m_oParentPart.Properties.GetContentLayoutOrder(this.m_oParentPart);
        if (contentLayoutOrder != null && contentLayoutOrder.Length > 0 && (force || string.Compare(contentLayoutOrder, this.m_sLastContentLayoutOrder, true) != 0))
        {
          QPartHelper.SetContentPartsLayoutOrder((IQPartCollection) this, contentLayoutOrder);
          this.m_sLastContentLayoutOrder = contentLayoutOrder;
        }
      }
      this.InnerList.Sort((IComparer) new QPartComparer(this, this.m_oParentPart));
      this.SetPartIndices(0);
    }

    private void HandleChange(bool performLayout)
    {
      if (this.m_iSuspendChangeNotification > 0 || this.m_oDisplayParent == null)
        return;
      this.m_oDisplayParent.HandleChildObjectChanged(performLayout, Rectangle.Empty);
    }

    public object Clone()
    {
      QPartCollection qpartCollection = (QPartCollection) QObjectCloner.CloneObject((object) this, false, 1);
      qpartCollection.SuspendChangeNotification();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] is ICloneable cloneable)
          qpartCollection.InnerList.Add(cloneable.Clone());
        else
          qpartCollection.InnerList.Add((object) this[index]);
      }
      qpartCollection.SetPartIndices(0);
      qpartCollection.ResumeChangeNotification(false);
      return (object) qpartCollection;
    }

    bool IList.IsReadOnly => this.InnerList.IsReadOnly;

    object IList.this[int index]
    {
      get => (object) this[index];
      set => this[index] = value as IQPart;
    }

    void IList.RemoveAt(int index) => this.RemoveAt(index);

    void IList.Insert(int index, object value) => this.Insert(index, value as IQPart, true);

    void IList.Remove(object value) => this.Remove(value as IQPart);

    bool IList.Contains(object value) => this.Contains(value as IQPart);

    void IList.Clear() => this.Clear();

    int IList.IndexOf(object value) => this.IndexOf(value as IQPart);

    int IList.Add(object value) => this.Add(value as IQPart, true);

    bool IList.IsFixedSize => this.InnerList.IsFixedSize;

    bool ICollection.IsSynchronized => this.InnerList.IsSynchronized;

    int ICollection.Count => this.Count;

    object ICollection.SyncRoot => this.InnerList.SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => this.InnerList.GetEnumerator();
  }
}
