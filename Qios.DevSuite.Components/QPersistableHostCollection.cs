// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPersistableHostCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QPersistableHostCollection : CollectionBase
  {
    public void Add(IQPersistableHost persistableHost) => this.InnerList.Add((object) persistableHost);

    public void Insert(int index, IQPersistableHost host) => this.InnerList.Insert(index, (object) host);

    public void Remove(IQPersistableHost persistableHost) => this.InnerList.Remove((object) persistableHost);

    public IQPersistableHost this[int index] => (IQPersistableHost) this.InnerList[index];

    public IQPersistableHost this[string persistGuid]
    {
      get
      {
        Guid guid = new Guid(persistGuid);
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].PersistGuid == guid)
            return this[index];
        }
        return (IQPersistableHost) null;
      }
    }

    public bool Contains(IQPersistableHost persistableHost) => this.InnerList.Contains((object) persistableHost);

    public bool Contains(Guid persistGuid) => this[persistGuid.ToString()] != null;

    public int IndexOf(IQPersistableHost host) => this.InnerList.IndexOf((object) host);

    public void CopyTo(IQPersistableHost[] hosts, int index) => ((ICollection) this).CopyTo((Array) hosts, index);
  }
}
