// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPersistableObjectCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QPersistableObjectCollection : CollectionBase
  {
    public void Add(IQPersistableObject persistableObject) => this.InnerList.Add((object) persistableObject);

    public void Insert(int index, IQPersistableObject persistableObject) => this.InnerList.Insert(index, (object) persistableObject);

    public void Remove(IQPersistableObject persistableObject) => this.InnerList.Remove((object) persistableObject);

    public IQPersistableObject this[int index] => (IQPersistableObject) this.InnerList[index];

    private void AddDependencies(ArrayList list, IQPersistableObject persistableObject)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != persistableObject && !list.Contains((object) this[index]) && persistableObject.MustBePersistedAfter(this[index]))
        {
          this.AddDependencies(list, this[index]);
          if (!list.Contains((object) this[index]))
            list.Add((object) this[index]);
        }
      }
    }

    public void SortPersistableObjects()
    {
      ArrayList arrayList = new ArrayList(this.Count);
      for (int index = 0; index < this.Count; ++index)
      {
        if (!arrayList.Contains((object) this[index]))
        {
          this.AddDependencies(arrayList, this[index]);
          if (!arrayList.Contains((object) this[index]))
            arrayList.Add((object) this[index]);
        }
      }
      this.InnerList.Clear();
      this.InnerList.AddRange((ICollection) arrayList);
    }

    public IQPersistableObject this[string persistGuid]
    {
      get
      {
        Guid guid = new Guid(persistGuid);
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].PersistGuid == guid)
            return this[index];
        }
        return (IQPersistableObject) null;
      }
    }

    public bool Contains(IQPersistableObject persistableObject) => this.InnerList.Contains((object) persistableObject);

    public bool Contains(Guid persistGuid) => this[persistGuid.ToString()] != null;

    public int IndexOf(IQPersistableObject persistableObject) => this.InnerList.IndexOf((object) persistableObject);

    public void CopyTo(IQPersistableObject[] objects, int index) => ((ICollection) this).CopyTo((Array) objects, index);
  }
}
