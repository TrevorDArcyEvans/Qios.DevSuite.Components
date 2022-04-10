// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxAccelerationCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QInputBoxAccelerationCollection : CollectionBase
  {
    private IComparer m_oComparer;
    private bool m_bUnsorted;

    internal void Sort()
    {
      if (!this.m_bUnsorted)
        return;
      this.InnerList.Sort(this.Comparer);
      this.m_bUnsorted = false;
    }

    private IComparer Comparer
    {
      get
      {
        if (this.m_oComparer == null)
          this.m_oComparer = (IComparer) new QInputBoxAccelerationCollection.QInputBoxAccelerationComparer();
        return this.m_oComparer;
      }
    }

    public void Add(QInputBoxAcceleration acceleration)
    {
      this.InnerList.Add((object) acceleration);
      this.m_bUnsorted = true;
    }

    public void Remove(QInputBoxAcceleration acceleration)
    {
      this.InnerList.Remove((object) acceleration);
      this.m_bUnsorted = true;
    }

    public QInputBoxAcceleration this[int index] => this.InnerList[index] as QInputBoxAcceleration;

    private class QInputBoxAccelerationComparer : IComparer
    {
      public int Compare(object item1, object item2)
      {
        QInputBoxAcceleration qinputBoxAcceleration1 = item1 as QInputBoxAcceleration;
        QInputBoxAcceleration qinputBoxAcceleration2 = item2 as QInputBoxAcceleration;
        if (qinputBoxAcceleration1 == null && qinputBoxAcceleration2 == null)
          return 0;
        if (qinputBoxAcceleration1 == null)
          return -1;
        return qinputBoxAcceleration2 == null ? 1 : qinputBoxAcceleration1.Seconds.CompareTo(qinputBoxAcceleration2.Seconds);
      }
    }
  }
}
