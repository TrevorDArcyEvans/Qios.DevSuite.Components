// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDiffChange
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QDiffChange
  {
    private QDiffChangeType m_eType;
    private int m_iStartA;
    private int m_iStartB;
    private int m_iLengthA;
    private int m_iLengthB;

    public QDiffChange(QDiffChangeType type, int startA, int startB, int lengthA, int lengthB)
    {
      this.m_eType = type;
      this.m_iStartA = startA;
      this.m_iStartB = startB;
      this.m_iLengthA = lengthA;
      this.m_iLengthB = lengthB;
    }

    public QDiffChangeType Type => this.m_eType;

    public int StartA => this.m_iStartA;

    public int StartB => this.m_iStartB;

    public int LengthA => this.m_iLengthA;

    public int LengthB => this.m_iLengthB;
  }
}
