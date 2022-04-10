// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRange
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [Serializable]
  public struct QRange
  {
    private int m_iStart;
    private int m_iSize;
    private static QRange m_oEmpty = new QRange(0, 0);

    public QRange(int start, int size)
    {
      this.m_iStart = start;
      this.m_iSize = size;
    }

    public static QRange Empty => QRange.m_oEmpty;

    public static QRange FromStartEnd(int startValue, int endValue) => new QRange(startValue, endValue - startValue);

    public static QRange FromRectangle(Rectangle rectangle, bool leftRight) => leftRight ? new QRange(rectangle.Left, rectangle.Width) : new QRange(rectangle.Top, rectangle.Height);

    public Rectangle AdjustRectangle(Rectangle rectangle, bool leftRight) => leftRight ? new Rectangle(this.Start, rectangle.Top, this.Size, rectangle.Height) : new Rectangle(rectangle.Left, this.Start, rectangle.Width, this.Size);

    public static Rectangle CreateRectangle(QRange horizontalRange, QRange verticalRange) => new Rectangle(horizontalRange.Start, verticalRange.Start, horizontalRange.Size, verticalRange.Size);

    public int Size
    {
      get => this.m_iSize;
      set => this.m_iSize = value;
    }

    public int Start
    {
      get => this.m_iStart;
      set => this.m_iStart = value;
    }

    public int End
    {
      get => this.m_iStart + this.m_iSize;
      set => this.m_iSize = value - this.m_iStart;
    }

    public bool Contains(int position) => position >= this.Start && position < this.End;

    public override bool Equals(object obj) => obj is QRange qrange && this.m_iSize == qrange.m_iSize && this.m_iStart == qrange.m_iStart;

    public override int GetHashCode() => this.m_iStart.GetHashCode() ^ this.m_iSize.GetHashCode();

    public static bool operator ==(QRange operand1, QRange operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QRange operand1, QRange operand2) => !operand1.Equals((object) operand2);
  }
}
