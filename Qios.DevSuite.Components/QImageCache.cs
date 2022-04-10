// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QImageCache
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QImageCache : QObjectCache
  {
    private const int DefaultUnloadSizeFactor = 10;
    private const int DefaultMinimumMaxSize = 10;
    private const int DefaultMaximumSize = 500;
    private static QImageCache m_oCache;

    internal QImageCache()
      : base(500, 10, 10)
    {
    }

    public static bool Enabled
    {
      get
      {
        QImageCache.SecureImageCache();
        return QImageCache.m_oCache.CacheEnabled;
      }
      set
      {
        QImageCache.SecureImageCache();
        QImageCache.m_oCache.CacheEnabled = value;
      }
    }

    [Obsolete("Not used anymore, ignore this property")]
    public static bool DisposeImagesWhenClearingCache
    {
      get => false;
      set
      {
      }
    }

    public static int MaximumSize
    {
      get
      {
        QImageCache.SecureImageCache();
        return QImageCache.m_oCache.MaxSize;
      }
      set
      {
        QImageCache.SecureImageCache();
        lock (QImageCache.m_oCache)
          QImageCache.m_oCache.MaxSize = value;
      }
    }

    public static int CurrentSize
    {
      get
      {
        if (QImageCache.m_oCache == null)
          return 0;
        lock (QImageCache.m_oCache)
          return QImageCache.m_oCache.CurrentCacheSize;
      }
    }

    public static void ExcludeFromCleanup(Image image)
    {
      QImageCache.SecureImageCache();
      lock (QImageCache.m_oCache)
        QImageCache.m_oCache.ExcludeObjectFromCleanup((object) image);
    }

    public static void IncludeInCleanup(Image image)
    {
      QImageCache.SecureImageCache();
      lock (QImageCache.m_oCache)
        QImageCache.m_oCache.IncludeObjectInCleanup((object) image);
    }

    private static void SecureImageCache()
    {
      if (QImageCache.m_oCache != null)
        return;
      QImageCache.m_oCache = new QImageCache();
    }

    public static void StoreImage(QImageCacheKey key, Image cachedImage)
    {
      QImageCache.SecureImageCache();
      lock (QImageCache.m_oCache)
        QImageCache.m_oCache.StoreObject((object) key, (object) cachedImage);
    }

    public static Image FindImage(QImageCacheKey key)
    {
      if (QImageCache.m_oCache == null)
        return (Image) null;
      lock (QImageCache.m_oCache)
        return QImageCache.m_oCache.FindObject((object) key) as Image;
    }

    public static void ClearCache(bool ignoreExclusions)
    {
      if (QImageCache.m_oCache == null)
        return;
      lock (QImageCache.m_oCache)
        QImageCache.m_oCache.ClearObjectCache(ignoreExclusions);
    }
  }
}
