// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFontCache
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QFontCache : QObjectCache
  {
    private const int DefaultUnloadSizeFactor = 10;
    private const int DefaultMinimumMaxSize = 10;
    private const int DefaultMaximumSize = 50;
    private static QFontCache m_oCache;

    internal QFontCache()
      : base(50, 10, 10)
    {
    }

    public static bool Enabled
    {
      get
      {
        QFontCache.SecureFontCache();
        return QFontCache.m_oCache.CacheEnabled;
      }
      set
      {
        QFontCache.SecureFontCache();
        QFontCache.m_oCache.CacheEnabled = value;
      }
    }

    public static int MaximumSize
    {
      get
      {
        QFontCache.SecureFontCache();
        return QFontCache.m_oCache.MaxSize;
      }
      set
      {
        QFontCache.SecureFontCache();
        lock (QFontCache.m_oCache)
          QFontCache.m_oCache.MaxSize = value;
      }
    }

    public static int CurrentSize
    {
      get
      {
        if (QFontCache.m_oCache == null)
          return 0;
        lock (QFontCache.m_oCache)
          return QFontCache.m_oCache.CurrentCacheSize;
      }
    }

    public static void ExcludeFromCleanup(Font font)
    {
      QFontCache.SecureFontCache();
      lock (QFontCache.m_oCache)
        QFontCache.m_oCache.ExcludeObjectFromCleanup((object) font);
    }

    public static void IncludeInCleanup(Font font)
    {
      QFontCache.SecureFontCache();
      lock (QFontCache.m_oCache)
        QFontCache.m_oCache.IncludeObjectInCleanup((object) font);
    }

    private static void SecureFontCache()
    {
      if (QFontCache.m_oCache != null)
        return;
      QFontCache.m_oCache = new QFontCache();
    }

    public static void StoreFont(Font font) => QFontCache.StoreFont(QFontDefinition.FromFont(font), font);

    public static void StoreFont(QFontDefinition definition, Font font)
    {
      QFontCache.SecureFontCache();
      lock (QFontCache.m_oCache)
        QFontCache.m_oCache.StoreObject((object) definition, (object) font);
    }

    public static Font FindFont(QFontDefinition definition)
    {
      if (QFontCache.m_oCache == null)
        return (Font) null;
      lock (QFontCache.m_oCache)
        return QFontCache.m_oCache.FindObject((object) definition) as Font;
    }

    public static void ClearCache(bool ignoreExclusions)
    {
      if (QFontCache.m_oCache == null)
        return;
      lock (QFontCache.m_oCache)
        QFontCache.m_oCache.ClearObjectCache(ignoreExclusions);
    }
  }
}
