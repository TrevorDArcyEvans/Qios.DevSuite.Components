// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonRow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public sealed class QTabButtonRow
  {
    private QTabStrip m_oTabStrip;
    private QRange m_oRange;

    internal QTabButtonRow(QTabStrip tabStrip) => this.m_oTabStrip = tabStrip;

    public QTabStrip TabStrip => this.m_oTabStrip;

    public int RowIndex => this.m_oTabStrip == null ? 0 : this.m_oTabStrip.TabButtonRows.IndexOf(this);

    public QRange Range
    {
      get => this.m_oRange;
      set => this.m_oRange = value;
    }

    public int Start
    {
      get => this.m_oRange.Start;
      set => this.m_oRange.Start = value;
    }

    public int Size
    {
      get => this.m_oRange.Size;
      set => this.m_oRange.Size = value;
    }

    public int End
    {
      get => this.m_oRange.End;
      set => this.m_oRange.End = value;
    }

    public Point CalculatePointToControl(int x, int y, bool includeScrollPosition)
    {
      if (this.m_oTabStrip == null)
        return new Point(x, y);
      return this.m_oTabStrip.IsHorizontal ? this.m_oTabStrip.CalculatePointToControl(x, this.Start + y, includeScrollPosition) : this.m_oTabStrip.CalculatePointToControl(x + this.Start, y, includeScrollPosition);
    }

    public Rectangle CalculateBoundsToControl(Rectangle bounds, bool includeScrollPosition) => new Rectangle(this.CalculatePointToControl(bounds.Left, bounds.Top, includeScrollPosition), bounds.Size);
  }
}
