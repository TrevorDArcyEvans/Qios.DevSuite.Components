// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QSpacing
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  [TypeConverter(typeof (QSpacingConverter))]
  [Serializable]
  public struct QSpacing
  {
    private int m_iBefore;
    private int m_iAfter;
    private static QSpacing m_oEmpty = new QSpacing(0, 0);

    public QSpacing(int before, int after)
    {
      this.m_iBefore = before;
      this.m_iAfter = after;
    }

    public static QSpacing Empty => QSpacing.m_oEmpty;

    [Description("Gets or sets the spacing before an element")]
    public int Before
    {
      get => this.m_iBefore;
      set => this.m_iBefore = value;
    }

    [Description("Gets or sets the spacing after an element")]
    public int After
    {
      get => this.m_iAfter;
      set => this.m_iAfter = value;
    }

    [Browsable(false)]
    public int All => this.Before + this.After;

    public Rectangle InflateRectangleWithSpacing(
      Rectangle rectangle,
      bool inflate,
      QContentOrientation orientation)
    {
      int num = inflate ? -1 : 1;
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          return new Rectangle(rectangle.Left + this.Before * num, rectangle.Top, rectangle.Width - this.All * num, rectangle.Height);
        case QContentOrientation.VerticalDown:
          return new Rectangle(rectangle.Left, rectangle.Top + this.Before * num, rectangle.Width, rectangle.Height - this.All * num);
        case QContentOrientation.VerticalUp:
          return new Rectangle(rectangle.Left, rectangle.Top + this.After * num, rectangle.Width, rectangle.Height - this.All * num);
        default:
          return Rectangle.Empty;
      }
    }

    public Size InflateSizeWithSpacing(
      Size size,
      bool inflate,
      QContentOrientation orientation)
    {
      int num = inflate ? 1 : -1;
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          return new Size(size.Width + this.All * num, size.Height);
        case QContentOrientation.VerticalDown:
        case QContentOrientation.VerticalUp:
          return new Size(size.Width, size.Height + this.All * num);
        default:
          return Size.Empty;
      }
    }

    public override bool Equals(object obj) => obj is QSpacing qspacing && this.m_iBefore == qspacing.m_iBefore && this.m_iAfter == qspacing.m_iAfter;

    public override int GetHashCode() => this.m_iBefore.GetHashCode() ^ this.m_iAfter.GetHashCode();

    public static bool operator ==(QSpacing operand1, QSpacing operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QSpacing operand1, QSpacing operand2) => !operand1.Equals((object) operand2);
  }
}
