// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QOffscreenBitmapsManager
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  internal class QOffscreenBitmapsManager
  {
    private static ArrayList m_aBitmapSets;
    private static int m_iMaximumSize = 100;

    public static QOffscreenBitmapSet GetFreeBitmapSet()
    {
      lock (typeof (QOffscreenBitmapsManager))
      {
        if (QOffscreenBitmapsManager.m_aBitmapSets == null)
          QOffscreenBitmapsManager.m_aBitmapSets = new ArrayList();
        if (QOffscreenBitmapsManager.m_aBitmapSets.Count == QOffscreenBitmapsManager.m_iMaximumSize)
          throw new InvalidOperationException(QResources.GetException("QOffscreenBitmapManager_MaxUsedSets", (object) QOffscreenBitmapsManager.m_iMaximumSize));
        for (int index = 0; index < QOffscreenBitmapsManager.m_aBitmapSets.Count; ++index)
        {
          QOffscreenBitmapSet aBitmapSet = (QOffscreenBitmapSet) QOffscreenBitmapsManager.m_aBitmapSets[index];
          if (!aBitmapSet.Locked)
          {
            aBitmapSet.Locked = true;
            return aBitmapSet;
          }
        }
        QOffscreenBitmapSet freeBitmapSet = new QOffscreenBitmapSet();
        QOffscreenBitmapsManager.m_aBitmapSets.Add((object) freeBitmapSet);
        freeBitmapSet.Locked = true;
        return freeBitmapSet;
      }
    }

    public static void FreeBitmapSet(QOffscreenBitmapSet bitmapSet) => bitmapSet.Locked = false;
  }
}
