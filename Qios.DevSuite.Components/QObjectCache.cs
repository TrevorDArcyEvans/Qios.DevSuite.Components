// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QObjectCache
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  public abstract class QObjectCache : IDisposable
  {
    private bool m_bDisposed;
    private Hashtable m_oObjects;
    private ArrayList m_oCleanupExcludedObject;
    private ArrayList m_oSortableEntries;
    private int m_iUnloadSizeFactor = 10;
    private int m_iMaxSize = 50;
    private int m_iMinimumMaxSize = 10;
    private int m_iUnloadSize;
    private bool m_bCacheEnabled = true;

    protected QObjectCache(int maxSize, int unloadSizeFactor, int minimumMaxSize)
    {
      this.m_iMinimumMaxSize = minimumMaxSize;
      this.m_iMaxSize = Math.Max(maxSize, this.m_iMinimumMaxSize);
      this.m_iUnloadSizeFactor = unloadSizeFactor;
      this.m_iUnloadSize = this.m_iMaxSize / this.m_iUnloadSizeFactor;
      this.m_oCleanupExcludedObject = new ArrayList();
      this.m_oObjects = new Hashtable(this.m_iMaxSize);
      this.m_oSortableEntries = new ArrayList();
    }

    protected int MaxSize
    {
      get => this.m_iMaxSize;
      set
      {
        this.m_iMaxSize = Math.Max(value, this.m_iMinimumMaxSize);
        this.m_iUnloadSize = this.m_iMaxSize / 10;
        this.CleanupObjectCache();
      }
    }

    protected bool CacheEnabled
    {
      get => this.m_bCacheEnabled;
      set => this.m_bCacheEnabled = value;
    }

    protected int CurrentCacheSize => this.m_oObjects.Count;

    protected void ExcludeObjectFromCleanup(object value)
    {
      if (this.m_oCleanupExcludedObject.Contains(value))
        return;
      this.m_oCleanupExcludedObject.Add(value);
    }

    protected void IncludeObjectInCleanup(object value)
    {
      if (!this.m_oCleanupExcludedObject.Contains(value))
        return;
      this.m_oCleanupExcludedObject.Remove(value);
    }

    protected void StoreObject(object key, object value)
    {
      if (!this.m_bCacheEnabled)
        return;
      this.m_oObjects.Add(key, (object) new QObjectCache.QObjectCacheValue(value, QMisc.TickCount));
      this.CleanupObjectCache();
    }

    protected object FindObject(object key)
    {
      if (!this.m_bCacheEnabled)
        return (object) null;
      if (!(this.m_oObjects[key] is QObjectCache.QObjectCacheValue oObject))
        return (object) null;
      oObject.LastRetrievedTick = QMisc.TickCount;
      return oObject.Value;
    }

    protected void ClearObjectCache(bool ignoreExclusions) => this.ClearObjectCacheTill(0, ignoreExclusions);

    protected void ClearObjectCacheTill(int newSize, bool ignoreExclusions)
    {
      this.m_oSortableEntries.Clear();
      this.m_oSortableEntries.AddRange((ICollection) this.m_oObjects);
      this.m_oSortableEntries.Sort((IComparer) new QObjectCache.QObjectCacheLastRetrievedComparer());
      int index = 0;
      while (this.m_oObjects.Count > newSize)
      {
        object key = ((DictionaryEntry) this.m_oSortableEntries[index]).Key;
        QObjectCache.QObjectCacheValue qobjectCacheValue = (QObjectCache.QObjectCacheValue) ((DictionaryEntry) this.m_oSortableEntries[index]).Value;
        if (ignoreExclusions || !this.m_oCleanupExcludedObject.Contains(qobjectCacheValue.Value))
        {
          this.m_oObjects.Remove(key);
          if (ignoreExclusions)
            this.m_oCleanupExcludedObject.Remove(qobjectCacheValue.Value);
        }
        ++index;
      }
      this.m_oSortableEntries.Clear();
    }

    protected void CleanupObjectCache()
    {
      if (this.m_oObjects.Count <= this.m_iMaxSize)
        return;
      this.ClearObjectCacheTill(Math.Max(this.m_iMaxSize - this.m_iUnloadSize, 0), false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bDisposed)
        return;
      int num = disposing ? 1 : 0;
      this.m_bDisposed = true;
    }

    ~QObjectCache() => this.Dispose(false);

    private class QObjectCacheValue
    {
      private object m_oValue;
      private long m_lLastRetrievedTick;

      public QObjectCacheValue(object value, long lastRetrievedTick)
      {
        this.m_oValue = value;
        this.m_lLastRetrievedTick = lastRetrievedTick;
      }

      public long LastRetrievedTick
      {
        get => this.m_lLastRetrievedTick;
        set => this.m_lLastRetrievedTick = value;
      }

      public object Value => this.m_oValue;
    }

    internal class QObjectCacheLastRetrievedComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        QObjectCache.QObjectCacheValue qobjectCacheValue1 = (QObjectCache.QObjectCacheValue) ((DictionaryEntry) x).Value;
        QObjectCache.QObjectCacheValue qobjectCacheValue2 = (QObjectCache.QObjectCacheValue) ((DictionaryEntry) y).Value;
        if (qobjectCacheValue1.LastRetrievedTick > qobjectCacheValue2.LastRetrievedTick)
          return 1;
        return qobjectCacheValue1.LastRetrievedTick < qobjectCacheValue2.LastRetrievedTick ? -1 : 0;
      }
    }
  }
}
