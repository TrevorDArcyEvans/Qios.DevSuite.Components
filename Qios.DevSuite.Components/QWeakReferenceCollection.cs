// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakReferenceCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QWeakReferenceCollection : CollectionBase
  {
    public QWeakReference AddObject(object objectValue)
    {
      QWeakReference reference = new QWeakReference(objectValue);
      this.Add(reference);
      return reference;
    }

    public void RemoveObject(object objectValue, bool cleanDeadReferences)
    {
      QWeakReference weakReference = this.GetWeakReference(objectValue, cleanDeadReferences);
      if (weakReference == null)
        return;
      this.Remove(weakReference);
    }

    public void Add(QWeakReference reference)
    {
      lock (this.InnerList.SyncRoot)
        this.InnerList.Add((object) reference);
    }

    public void Remove(QWeakReference reference)
    {
      lock (this.InnerList.SyncRoot)
        this.InnerList.Remove((object) reference);
    }

    public new void Clear()
    {
      lock (this.InnerList.SyncRoot)
        base.Clear();
    }

    public new void RemoveAt(int index)
    {
      lock (this.InnerList.SyncRoot)
        base.RemoveAt(index);
    }

    public int IndexOfObject(object objectValue, bool cleanDeadReferences)
    {
      lock (this.InnerList.SyncRoot)
      {
        for (int index = this.Count - 1; index >= 0; --index)
        {
          if (this.InnerList[index] is QWeakReference inner && !inner.Finalized && inner.IsAlive)
          {
            if (inner.Target == objectValue)
              return index;
          }
          else if (cleanDeadReferences)
            this.InnerList.RemoveAt(index);
        }
        return -1;
      }
    }

    public int IndexOf(QWeakReference reference)
    {
      lock (this.InnerList.SyncRoot)
        return this.InnerList.IndexOf((object) reference);
    }

    public QWeakReference GetWeakReference(
      object objectValue,
      bool cleanDeadReferences)
    {
      int index = this.IndexOfObject(objectValue, cleanDeadReferences);
      return index >= 0 ? this[index] : (QWeakReference) null;
    }

    public bool ContainsObject(object objectValue, bool cleanDeadReferences) => this.GetWeakReference(objectValue, cleanDeadReferences) != null;

    public QWeakReference this[int index]
    {
      get
      {
        lock (this.InnerList.SyncRoot)
          return this.InnerList[index] as QWeakReference;
      }
    }

    public int CleanCollection()
    {
      int num = 0;
      lock (this.InnerList.SyncRoot)
      {
        for (int index = this.Count - 1; index >= 0; --index)
        {
          if (!(this.InnerList[index] is QWeakReference inner) || inner.Finalized || !inner.IsAlive)
          {
            this.InnerList.RemoveAt(index);
            ++num;
          }
        }
        return num;
      }
    }

    ~QWeakReferenceCollection()
    {
    }
  }
}
