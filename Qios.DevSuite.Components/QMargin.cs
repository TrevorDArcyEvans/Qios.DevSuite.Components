// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMargin
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QMarginConverter))]
  [Serializable]
  public struct QMargin : IQMargin
  {
    private int m_iLeft;
    private int m_iTop;
    private int m_iRight;
    private int m_iBottom;
    private static QMargin m_oEmpty = new QMargin(0, 0, 0, 0);

    public QMargin(int left, int top, int bottom, int right)
    {
      this.m_iLeft = left;
      this.m_iTop = top;
      this.m_iBottom = bottom;
      this.m_iRight = right;
    }

    public static QMargin Empty => QMargin.m_oEmpty;

    [Description("Gets or sets the Left padding")]
    public int Left
    {
      get => this.m_iLeft;
      set => this.m_iLeft = value;
    }

    [Description("Gets or sets the Top margin")]
    public int Top
    {
      get => this.m_iTop;
      set => this.m_iTop = value;
    }

    [Description("Gets or sets the Right margin")]
    public int Right
    {
      get => this.m_iRight;
      set => this.m_iRight = value;
    }

    [Description("Gets or sets the Bottom margin")]
    public int Bottom
    {
      get => this.m_iBottom;
      set => this.m_iBottom = value;
    }

    [Browsable(false)]
    public int Vertical => this.m_iBottom + this.m_iTop;

    [Browsable(false)]
    public int Horizontal => this.m_iLeft + this.m_iRight;

    public QMargin ToMargin() => this;

    public Rectangle InflateRectangleWithMargin(
      Rectangle rectangle,
      bool inflate,
      QContentOrientation orientation)
    {
      int num = inflate ? -1 : 1;
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          return new Rectangle(rectangle.Left + this.Left * num, rectangle.Top + this.Top * num, rectangle.Width - this.Horizontal * num, rectangle.Height - this.Vertical * num);
        case QContentOrientation.VerticalDown:
          return new Rectangle(rectangle.Left + this.Bottom * num, rectangle.Top + this.Left * num, rectangle.Width - this.Vertical * num, rectangle.Height - this.Horizontal * num);
        case QContentOrientation.VerticalUp:
          return new Rectangle(rectangle.Left + this.Top * num, rectangle.Top + this.Right * num, rectangle.Width - this.Vertical * num, rectangle.Height - this.Horizontal * num);
        default:
          return Rectangle.Empty;
      }
    }

    public Rectangle InflateNegativeMargins(Rectangle rectangle)
    {
      if (this.Top < 0)
        rectangle.Height -= this.Top;
      if (this.Bottom < 0)
        rectangle.Height -= this.Bottom;
      if (this.Left < 0)
        rectangle.Width -= this.Left;
      if (this.Right < 0)
        rectangle.Width -= this.Right;
      if (this.Top < 0)
        rectangle.Y += this.Top;
      if (this.Left < 0)
        rectangle.X += this.Left;
      return rectangle;
    }

    public Rectangle InflateRectangleWithMargin(
      Rectangle rectangle,
      bool inflate,
      bool horizontal)
    {
      return this.InflateRectangleWithMargin(rectangle, inflate, horizontal ? QContentOrientation.Horizontal : QContentOrientation.VerticalDown);
    }

    public Size InflateSizeWithMargin(Size size, bool inflate, QContentOrientation orientation)
    {
      int num = inflate ? 1 : -1;
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          return new Size(size.Width + this.Horizontal * num, size.Height + this.Vertical * num);
        case QContentOrientation.VerticalDown:
        case QContentOrientation.VerticalUp:
          return new Size(size.Width + this.Vertical * num, size.Height + this.Horizontal * num);
        default:
          return Size.Empty;
      }
    }

    public Size InflateSizeWithMargin(Size size, bool inflate, bool horizontal) => this.InflateSizeWithMargin(size, inflate, horizontal ? QContentOrientation.Horizontal : QContentOrientation.VerticalDown);

    public Rectangle InflateRectangleWithMargin(
      Rectangle rectangle,
      bool inflate,
      DockStyle dockStyle)
    {
      int num = inflate ? -1 : 1;
      switch (dockStyle)
      {
        case DockStyle.None:
        case DockStyle.Top:
        case DockStyle.Fill:
          return new Rectangle(rectangle.Left + this.Left * num, rectangle.Top + this.Top * num, rectangle.Width - this.Horizontal * num, rectangle.Height - this.Vertical * num);
        case DockStyle.Bottom:
          return new Rectangle(rectangle.Left + this.Left * num, rectangle.Top + this.Bottom * num, rectangle.Width - this.Horizontal * num, rectangle.Height - this.Vertical * num);
        case DockStyle.Left:
          return new Rectangle(rectangle.Left + this.Top * num, rectangle.Top + this.Right * num, rectangle.Width - this.Vertical * num, rectangle.Height - this.Horizontal * num);
        case DockStyle.Right:
          return new Rectangle(rectangle.Left + this.Bottom * num, rectangle.Top + this.Left * num, rectangle.Width - this.Vertical * num, rectangle.Height - this.Horizontal * num);
        default:
          return Rectangle.Empty;
      }
    }

    Size IQMargin.InflateSize(Size size, bool inflate, bool horizontal) => this.InflateSizeWithMargin(size, inflate, horizontal);

    Rectangle IQMargin.InflateRectangle(
      Rectangle rectangle,
      bool inflate,
      DockStyle dockStyle)
    {
      return this.InflateRectangleWithMargin(rectangle, inflate, dockStyle);
    }

    public static Size InflateSize(
      Size size,
      IQMargin[] margins,
      bool inflate,
      bool horizontal)
    {
      for (int index = 0; index < margins.Length; ++index)
        size = margins[index].InflateSize(size, inflate, horizontal);
      return size;
    }

    public static Rectangle InflateRectangle(
      Rectangle rectangle,
      IQMargin[] margins,
      bool inflate,
      DockStyle dockStyle)
    {
      for (int index = 0; index < margins.Length; ++index)
        rectangle = margins[index].InflateRectangle(rectangle, inflate, dockStyle);
      return rectangle;
    }

    public static QMargin SumMarings(IQMargin[] margins)
    {
      int left = 0;
      int top = 0;
      int right = 0;
      int bottom = 0;
      for (int index = 0; index < margins.Length; ++index)
      {
        QMargin margin = margins[index].ToMargin();
        left += margin.Left;
        top += margin.Top;
        right += margin.Right;
        bottom += margin.Bottom;
      }
      return new QMargin(left, top, bottom, right);
    }

    public static QMargin MaxMargins(params QMargin[] margins)
    {
      QMargin empty = QMargin.Empty;
      for (int index = 0; index < margins.Length; ++index)
      {
        empty.Left = Math.Max(empty.Left, margins[index].Left);
        empty.Right = Math.Max(empty.Right, margins[index].Right);
        empty.Bottom = Math.Max(empty.Bottom, margins[index].Bottom);
        empty.Top = Math.Max(empty.Top, margins[index].Top);
      }
      return empty;
    }

    public override bool Equals(object obj) => obj is QMargin qmargin && this.m_iLeft == qmargin.m_iLeft && this.m_iRight == qmargin.m_iRight && this.m_iTop == qmargin.m_iTop && this.m_iBottom == qmargin.m_iBottom;

    public override int GetHashCode() => this.m_iLeft.GetHashCode() ^ this.m_iRight.GetHashCode() ^ this.m_iTop.GetHashCode() ^ this.m_iBottom.GetHashCode();

    public static bool operator ==(QMargin operand1, QMargin operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QMargin operand1, QMargin operand2) => !operand1.Equals((object) operand2);
  }
}
