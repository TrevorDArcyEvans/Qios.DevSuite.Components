// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDiffCompareResult
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections.Specialized;

namespace Qios.DevSuite.Components
{
  public class QDiffCompareResult
  {
    private QDiffChangeCollection m_oChanges;
    private StringCollection m_oTextPartsA;
    private StringCollection m_oTextPartsB;
    private int[] m_aIndexA;
    private int[] m_aIndexB;

    internal QDiffCompareResult(
      StringCollection textPartsA,
      StringCollection textPartsB,
      int[] indexA,
      int[] indexB,
      QDiffChangeCollection changes)
    {
      this.m_oChanges = changes;
      this.m_oTextPartsA = textPartsA;
      this.m_oTextPartsB = textPartsB;
      this.m_aIndexA = indexA;
      this.m_aIndexB = indexB;
    }

    public QDiffChangeCollection Changes => this.m_oChanges;

    public StringCollection TextPartsA => this.m_oTextPartsA;

    public StringCollection TextPartsB => this.m_oTextPartsB;

    public int[] GetIndexA() => this.m_aIndexA;

    public int[] GetIndexB() => this.m_aIndexB;
  }
}
