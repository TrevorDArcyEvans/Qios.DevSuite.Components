// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QNavigationHostCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  internal class QNavigationHostCollection : CollectionBase
  {
    private IComparer m_oComparer = (IComparer) new QNavigationHostCollection.QNavigationHostComparer();

    public void Add(IQNavigationHost host) => this.InnerList.Add((object) host);

    public IQNavigationHost this[int index] => this.InnerList[index] as IQNavigationHost;

    public void Remove(IQNavigationHost host) => this.InnerList.Remove((object) host);

    public bool Contains(IQNavigationHost host) => host != null && this.InnerList.Contains((object) host);

    public int IndexOf(IQNavigationHost host) => this.InnerList.IndexOf((object) host);

    public bool RequiresSort()
    {
      for (int index = 0; index < this.Count - 1; ++index)
      {
        if (this.m_oComparer.Compare((object) this[index], (object) this[index + 1]) > 0)
          return true;
      }
      return false;
    }

    public void Sort() => this.InnerList.Sort(this.m_oComparer);

    public void SortWhenRequired()
    {
      if (!this.RequiresSort())
        return;
      this.Sort();
    }

    public IQNavigationHost GetNextAccessibleNavigationHost(
      IQNavigationHost host,
      bool forward,
      bool loop)
    {
      this.SortWhenRequired();
      int num = forward ? 1 : -1;
      for (int index = (host == null || !this.Contains(host) ? (forward ? -1 : this.Count) : this.IndexOf(host)) + num; index >= 0 && index < this.Count; index += num)
      {
        if (this[index].IsAccessibleForNavigation)
          return this[index];
      }
      return loop ? this.GetNextAccessibleNavigationHost((IQNavigationHost) null, forward, false) : (IQNavigationHost) null;
    }

    private class QNavigationHostComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        IQNavigationHost qnavigationHost1 = x as IQNavigationHost;
        IQNavigationHost qnavigationHost2 = y as IQNavigationHost;
        if (qnavigationHost1 == qnavigationHost2)
          return 0;
        if (qnavigationHost1 == null)
          return 1;
        return qnavigationHost2 == null ? -1 : qnavigationHost1.LocationForOrder.Y - qnavigationHost2.LocationForOrder.Y;
      }
    }
  }
}
