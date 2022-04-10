// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QLine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public struct QLine
  {
    private Point m_oP1;
    private Point m_oP2;
    private static QLine m_oEmpty = new QLine(0, 0, 0, 0);

    public QLine(int left1, int top1, int left2, int top2)
    {
      this.m_oP1 = new Point(left1, top1);
      this.m_oP2 = new Point(left2, top2);
    }

    public QLine(Point p1, Point p2)
    {
      this.m_oP1 = p1;
      this.m_oP2 = p2;
    }

    public static QLine Empty => QLine.m_oEmpty;

    [Description("Gets or sets the first point ")]
    public Point P1
    {
      get => this.m_oP1;
      set => this.m_oP1 = value;
    }

    [Description("Gets or sets the second point ")]
    public Point P2
    {
      get => this.m_oP2;
      set => this.m_oP2 = value;
    }

    public override bool Equals(object obj) => obj is QLine qline && !(this.m_oP1 != qline.m_oP1) && !(this.m_oP2 != qline.m_oP2);

    public override int GetHashCode() => this.m_oP1.GetHashCode() ^ this.m_oP2.GetHashCode();

    public static bool operator ==(QLine operand1, QLine operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QLine operand1, QLine operand2) => !operand1.Equals((object) operand2);
  }
}
