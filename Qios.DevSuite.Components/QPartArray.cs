// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartArray
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  internal class QPartArray : IQPartCollection
  {
    private IQPart[] m_aParts;

    public QPartArray(int size) => this.m_aParts = new IQPart[size];

    public QPartArray(IQPartCollection collection, int startIndex, int count)
    {
      this.m_aParts = new IQPart[count];
      int index1 = 0;
      for (int index2 = startIndex; index2 < startIndex + count; ++index2)
      {
        this.m_aParts[index1] = collection[index2];
        ++index1;
      }
    }

    public QPartArray(params IQPart[] parts) => this.m_aParts = parts;

    public IQPart this[int index]
    {
      get => this.m_aParts[index];
      set => this.m_aParts[index] = value;
    }

    public int Count => this.m_aParts.Length;

    public bool Contains(IQPart part) => this.IndexOf(part) >= 0;

    public int IndexOf(IQPart part)
    {
      for (int index = 0; index < this.m_aParts.Length; ++index)
      {
        if (this.m_aParts[index] == part)
          return index;
      }
      return -1;
    }
  }
}
