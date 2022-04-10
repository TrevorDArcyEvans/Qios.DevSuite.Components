// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QSubArray
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  internal class QSubArray
  {
    private int[] m_aArray;
    private int m_iOffset;
    private int m_iLength;

    internal QSubArray(int[] array)
    {
      this.m_aArray = array;
      this.m_iLength = this.m_aArray.Length;
    }

    internal QSubArray(QSubArray subArray, int offset, int length)
    {
      this.m_aArray = subArray.Array;
      this.m_iOffset = subArray.Offset + offset;
      this.m_iLength = length;
    }

    internal int this[int index] => this.m_aArray[this.m_iOffset + index];

    internal int[] Array => this.m_aArray;

    internal int Length => this.m_iLength;

    internal int Offset => this.m_iOffset;
  }
}
