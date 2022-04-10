// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMath
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public sealed class QMath
  {
    private QMath()
    {
    }

    internal static Color ModifyColor(
      Color color,
      float multiplyHue,
      float multiplyBrightness,
      float multiplySaturation)
    {
      if (color == Color.Empty)
        return color;
      float saturation = color.GetSaturation();
      float brightness = color.GetBrightness();
      float num1 = color.GetHue() / 360f * multiplyHue;
      float num2 = brightness * multiplyBrightness;
      float num3 = saturation * multiplySaturation;
      double num4;
      double num5;
      double num6;
      if ((double) num2 == 0.0)
      {
        double num7;
        num4 = num7 = 0.0;
        num5 = num7;
        num6 = num7;
      }
      else if ((double) num3 == 0.0)
      {
        double num8;
        num4 = num8 = (double) num2;
        num5 = num8;
        num6 = num8;
      }
      else
      {
        double num9 = (double) num2 <= 0.5 ? (double) num2 * (1.0 + (double) num3) : (double) num2 + (double) num3 - (double) num2 * (double) num3;
        double num10 = 2.0 * (double) num2 - num9;
        double[] numArray1 = new double[3]
        {
          (double) num1 + 1.0 / 3.0,
          (double) num1,
          (double) num1 - 1.0 / 3.0
        };
        double[] numArray2 = new double[3];
        for (int index = 0; index < 3; ++index)
        {
          if (numArray1[index] < 0.0)
            ++numArray1[index];
          if (numArray1[index] > 1.0)
            --numArray1[index];
          numArray2[index] = 6.0 * numArray1[index] >= 1.0 ? (2.0 * numArray1[index] >= 1.0 ? (3.0 * numArray1[index] >= 2.0 ? num10 : num10 + (num9 - num10) * (2.0 / 3.0 - numArray1[index]) * 6.0) : num9) : num10 + (num9 - num10) * numArray1[index] * 6.0;
        }
        num6 = numArray2[0];
        num5 = numArray2[1];
        num4 = numArray2[2];
      }
      return Color.FromArgb((int) color.A, Math.Max(0, Math.Min((int) byte.MaxValue, (int) ((double) byte.MaxValue * num6))), Math.Max(0, Math.Min((int) byte.MaxValue, (int) ((double) byte.MaxValue * num5))), Math.Max(0, Math.Min((int) byte.MaxValue, (int) ((double) byte.MaxValue * num4))));
    }

    public static int GetStartForCenter(int position1, int position2, int size) => position1 + (int) Math.Round((double) (position2 - position1) / 2.0 - (double) size / 2.0);

    public static bool ValueInMargin(int value, int measureValue, int margin) => measureValue - margin <= value && value < measureValue + margin;

    public static Rectangle SetX(Rectangle rectangle, int value)
    {
      rectangle.X = value;
      return rectangle;
    }

    public static Rectangle SetY(Rectangle rectangle, int value)
    {
      rectangle.Y = value;
      return rectangle;
    }

    public static Rectangle SetHeight(Rectangle rectangle, int height)
    {
      rectangle.Height = height;
      return rectangle;
    }

    public static Rectangle SetWidth(Rectangle rectangle, int width)
    {
      rectangle.Width = width;
      return rectangle;
    }

    public static Rectangle SetRight(Rectangle rectangle, int right) => Rectangle.FromLTRB(rectangle.Left, rectangle.Top, right, rectangle.Bottom);

    public static Rectangle SetBottom(Rectangle rectangle, int bottom) => Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, bottom);

    public static Rectangle MoveRectangleIntoBounds(Rectangle rectangle, Rectangle bounds)
    {
      if (rectangle.Left < bounds.Left)
        rectangle.Offset(bounds.Left - rectangle.Left, 0);
      if (rectangle.Top < bounds.Top)
        rectangle.Offset(bounds.Top - rectangle.Top, 0);
      if (rectangle.Right > bounds.Right)
        rectangle.Offset(bounds.Right - rectangle.Right, 0);
      if (rectangle.Bottom > bounds.Bottom)
        rectangle.Offset(0, bounds.Bottom - rectangle.Bottom);
      return rectangle;
    }

    public static ContentAlignment RotateAlignment(
      ContentAlignment alignment,
      QContentOrientation orientation)
    {
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          return alignment;
        case QContentOrientation.VerticalDown:
          switch (alignment)
          {
            case ContentAlignment.TopLeft:
              return ContentAlignment.TopRight;
            case ContentAlignment.TopCenter:
              return ContentAlignment.MiddleRight;
            case ContentAlignment.TopRight:
              return ContentAlignment.BottomRight;
            case ContentAlignment.MiddleLeft:
              return ContentAlignment.TopCenter;
            case ContentAlignment.MiddleCenter:
              return ContentAlignment.MiddleCenter;
            case ContentAlignment.MiddleRight:
              return ContentAlignment.BottomCenter;
            case ContentAlignment.BottomLeft:
              return ContentAlignment.TopLeft;
            case ContentAlignment.BottomCenter:
              return ContentAlignment.MiddleLeft;
            case ContentAlignment.BottomRight:
              return ContentAlignment.BottomLeft;
          }
          break;
        case QContentOrientation.VerticalUp:
          switch (alignment)
          {
            case ContentAlignment.TopLeft:
              return ContentAlignment.BottomLeft;
            case ContentAlignment.TopCenter:
              return ContentAlignment.MiddleLeft;
            case ContentAlignment.TopRight:
              return ContentAlignment.TopLeft;
            case ContentAlignment.MiddleLeft:
              return ContentAlignment.BottomCenter;
            case ContentAlignment.MiddleCenter:
              return ContentAlignment.MiddleCenter;
            case ContentAlignment.MiddleRight:
              return ContentAlignment.TopCenter;
            case ContentAlignment.BottomLeft:
              return ContentAlignment.BottomRight;
            case ContentAlignment.BottomCenter:
              return ContentAlignment.MiddleRight;
            case ContentAlignment.BottomRight:
              return ContentAlignment.TopRight;
          }
          break;
      }
      return ContentAlignment.MiddleCenter;
    }

    public static QImageAlign RotateImageAlign(
      QImageAlign align,
      QContentOrientation orientation)
    {
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          return align;
        case QContentOrientation.VerticalDown:
          switch (align)
          {
            case QImageAlign.RepeatedVertical:
              return QImageAlign.RepeatedHorizontal;
            case QImageAlign.RepeatedHorizontal:
              return QImageAlign.RepeatedVertical;
            case QImageAlign.RepeatedBoth:
              return QImageAlign.RepeatedBoth;
            case QImageAlign.Stretched:
              return QImageAlign.Stretched;
            case QImageAlign.Centered:
              return QImageAlign.Centered;
            case QImageAlign.TopLeft:
              return QImageAlign.TopRight;
            case QImageAlign.CenterLeft:
              return QImageAlign.TopMiddle;
            case QImageAlign.BottomLeft:
              return QImageAlign.TopLeft;
            case QImageAlign.TopRight:
              return QImageAlign.BottomRight;
            case QImageAlign.CenterRight:
              return QImageAlign.BottomMiddle;
            case QImageAlign.BottomRight:
              return QImageAlign.BottomLeft;
            case QImageAlign.TopMiddle:
              return QImageAlign.CenterRight;
            case QImageAlign.BottomMiddle:
              return QImageAlign.CenterLeft;
          }
          break;
        case QContentOrientation.VerticalUp:
          switch (align)
          {
            case QImageAlign.RepeatedVertical:
              return QImageAlign.RepeatedHorizontal;
            case QImageAlign.RepeatedHorizontal:
              return QImageAlign.RepeatedVertical;
            case QImageAlign.RepeatedBoth:
              return QImageAlign.RepeatedBoth;
            case QImageAlign.Stretched:
              return QImageAlign.Stretched;
            case QImageAlign.Centered:
              return QImageAlign.Centered;
            case QImageAlign.TopLeft:
              return QImageAlign.BottomLeft;
            case QImageAlign.CenterLeft:
              return QImageAlign.BottomMiddle;
            case QImageAlign.BottomLeft:
              return QImageAlign.BottomRight;
            case QImageAlign.TopRight:
              return QImageAlign.TopLeft;
            case QImageAlign.CenterRight:
              return QImageAlign.TopMiddle;
            case QImageAlign.BottomRight:
              return QImageAlign.TopRight;
            case QImageAlign.TopMiddle:
              return QImageAlign.CenterLeft;
            case QImageAlign.BottomMiddle:
              return QImageAlign.CenterRight;
          }
          break;
      }
      return QImageAlign.Centered;
    }

    public static QRange AlignElement(
      int size,
      QContentAlignment alignment,
      bool nearIsStart,
      QRange range)
    {
      switch (alignment)
      {
        case QContentAlignment.Near:
          return !nearIsStart ? new QRange(range.End - size, size) : new QRange(range.Start, size);
        case QContentAlignment.Far:
          return !nearIsStart ? new QRange(range.Start, size) : new QRange(range.End - size, size);
        case QContentAlignment.Center:
          return new QRange(QMath.GetStartForCenter(range.Start, range.End, size), size);
        case QContentAlignment.Stretched:
          return range;
        default:
          return QRange.Empty;
      }
    }

    public static Rectangle AlignElement(
      Size size,
      ContentAlignment alignment,
      Rectangle rectangle,
      bool allowOverflow)
    {
      Rectangle b = Rectangle.Empty;
      switch (alignment)
      {
        case ContentAlignment.TopLeft:
          b = new Rectangle(rectangle.Left, rectangle.Top, size.Width, size.Height);
          break;
        case ContentAlignment.TopCenter:
          b = new Rectangle(QMath.GetStartForCenter(rectangle.Left, rectangle.Right, size.Width), rectangle.Top, size.Width, size.Height);
          break;
        case ContentAlignment.TopRight:
          b = new Rectangle(rectangle.Right - size.Width, rectangle.Top, size.Width, size.Height);
          break;
        case ContentAlignment.MiddleLeft:
          b = new Rectangle(rectangle.Left, QMath.GetStartForCenter(rectangle.Top, rectangle.Bottom, size.Height), size.Width, size.Height);
          break;
        case ContentAlignment.MiddleCenter:
          b = new Rectangle(QMath.GetStartForCenter(rectangle.Left, rectangle.Right, size.Width), QMath.GetStartForCenter(rectangle.Top, rectangle.Bottom, size.Height), size.Width, size.Height);
          break;
        case ContentAlignment.MiddleRight:
          b = new Rectangle(rectangle.Right - size.Width, QMath.GetStartForCenter(rectangle.Top, rectangle.Bottom, size.Height), size.Width, size.Height);
          break;
        case ContentAlignment.BottomLeft:
          b = new Rectangle(rectangle.Left, rectangle.Bottom - size.Height, size.Width, size.Height);
          break;
        case ContentAlignment.BottomCenter:
          b = new Rectangle(QMath.GetStartForCenter(rectangle.Left, rectangle.Right, size.Width), rectangle.Bottom - size.Height, size.Width, size.Height);
          break;
        case ContentAlignment.BottomRight:
          b = new Rectangle(rectangle.Right - size.Width, rectangle.Bottom - size.Height, size.Width, size.Height);
          break;
      }
      return !allowOverflow ? Rectangle.Intersect(rectangle, b) : b;
    }

    public static void AlignTextIconRangesNextToEachOther(
      QContentOrder contentOrder,
      ref QRange imageRange,
      ref QRange textRange,
      QRange fullRange,
      bool resizeTextRangeOnOverflow)
    {
      if (contentOrder == QContentOrder.ImageText)
      {
        QMath.AlignRangesNextToEachOther(ref imageRange, ref textRange, fullRange, resizeTextRangeOnOverflow);
      }
      else
      {
        if (contentOrder != QContentOrder.TextImage)
          return;
        QMath.AlignRangesNextToEachOther(ref textRange, ref imageRange, fullRange, !resizeTextRangeOnOverflow);
      }
    }

    public static void AlignRangesNextToEachOther(
      ref QRange range1,
      ref QRange range2,
      QRange fullRange,
      bool resizeRange2OnOverflow)
    {
      int val1_1 = range1.End - range2.Start;
      if (val1_1 <= 0)
        return;
      int val2_1 = range1.Start - fullRange.Start;
      int val2_2 = fullRange.End - range2.End;
      int num = Math.Max(Math.Min(Math.Max(Math.Min(val1_1, range2.Size / 2), val1_1 - val2_2), val2_1), 0);
      int val1_2 = Math.Max(val1_1 - num, 0);
      if (!resizeRange2OnOverflow)
        val1_2 = Math.Min(val1_2, val2_2);
      range1.Start -= num;
      range2.Start += val1_2;
      if (!resizeRange2OnOverflow && range1.End > range2.Start)
        range1.Size -= range1.End - range2.Start;
      else
        range2.End = Math.Min(range2.End, fullRange.End);
    }

    public static int RotateAngle(int angle, int degrees) => (angle + degrees) % 360;

    public static PointF CalculateNormalizedVector(PointF point1, PointF point2)
    {
      float num1 = point2.X - point1.X;
      float num2 = point2.Y - point1.Y;
      if ((double) num1 == 0.0)
        return new PointF(0.0f, (float) Math.Sign(num2));
      if ((double) num2 == 0.0)
        return new PointF((float) Math.Sign(num1), 0.0f);
      float num3 = (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
      return new PointF(num1 / num3, num2 / num3);
    }

    public static float CalculateLineLength(PointF point1, PointF point2)
    {
      float num1 = point2.X - point1.X;
      float num2 = point2.Y - point1.Y;
      if ((double) num1 == 0.0)
        return Math.Abs(num2);
      return (double) num2 == 0.0 ? Math.Abs(num1) : (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
    }

    public static bool PointInBetween(PointF point0, PointF point1, PointF pointToCheck) => ((double) point0.X <= (double) pointToCheck.X && (double) pointToCheck.X <= (double) point1.X || (double) point0.X >= (double) pointToCheck.X && (double) pointToCheck.X >= (double) point1.X) && ((double) point0.Y <= (double) pointToCheck.Y && (double) pointToCheck.Y <= (double) point1.Y || (double) point0.Y >= (double) pointToCheck.Y && (double) pointToCheck.Y >= (double) point1.Y);

    public static QLinePointResult LinesIntersect(
      PointF point0,
      PointF point1,
      PointF point2,
      PointF point3)
    {
      PointF pointF = PointF.Empty;
      float num1 = (float) (((double) point3.X - (double) point2.X) * ((double) point0.Y - (double) point2.Y) - ((double) point3.Y - (double) point2.Y) * ((double) point0.X - (double) point2.X));
      float num2 = (float) (((double) point1.X - (double) point0.X) * ((double) point0.Y - (double) point2.Y) - ((double) point1.Y - (double) point0.Y) * ((double) point0.X - (double) point2.X));
      float num3 = (float) (((double) point3.Y - (double) point2.Y) * ((double) point1.X - (double) point0.X) - ((double) point3.X - (double) point2.X) * ((double) point1.Y - (double) point0.Y));
      if ((double) num3 != 0.0)
      {
        pointF = new PointF(point0.X + (float) ((double) num1 / (double) num3 * ((double) point1.X - (double) point0.X)), point0.Y + (float) ((double) num1 / (double) num3 * ((double) point1.Y - (double) point0.Y)));
        if (QMath.PointInBetween(point0, point1, pointF) && QMath.PointInBetween(point2, point3, pointF))
          return new QLinePointResult(true, pointF);
      }
      else if ((double) num1 == 0.0 || (double) num2 == 0.0)
        return new QLinePointResult(true, point0);
      return new QLinePointResult(false, PointF.Empty);
    }

    public static QLinePointResult LineCrossesPoint(
      PointF linePoint1,
      PointF linePoint2,
      PointF pointToCompare,
      float margin)
    {
      PointF empty1 = PointF.Empty;
      PointF empty2 = PointF.Empty;
      PointF empty3 = PointF.Empty;
      PointF point2 = new PointF(pointToCompare.X - margin / 2f, pointToCompare.Y);
      PointF point3 = new PointF(pointToCompare.X + margin / 2f, pointToCompare.Y);
      QLinePointResult qlinePointResult1 = QMath.LinesIntersect(linePoint1, linePoint2, point2, point3);
      bool result1 = qlinePointResult1.Result;
      PointF location1 = qlinePointResult1.Location;
      point2 = new PointF(pointToCompare.X, pointToCompare.Y - margin / 2f);
      point3 = new PointF(pointToCompare.X, pointToCompare.Y + margin / 2f);
      QLinePointResult qlinePointResult2 = QMath.LinesIntersect(linePoint1, linePoint2, point2, point3);
      bool result2 = qlinePointResult2.Result;
      PointF location2 = qlinePointResult2.Location;
      PointF location3;
      bool result3;
      if (result1 && result2)
      {
        location3 = pointToCompare;
        result3 = true;
      }
      else if (result1)
      {
        location3 = location1;
        result3 = true;
      }
      else if (result2)
      {
        location3 = location2;
        result3 = true;
      }
      else
      {
        location3 = (PointF) Point.Empty;
        result3 = false;
      }
      return new QLinePointResult(result3, location3);
    }

    public static QLinePointResult LineCrossesRectangle(
      PointF linePoint1,
      PointF linePoint2,
      RectangleF rectangle)
    {
      PointF location = rectangle.Location;
      PointF point3 = new PointF(rectangle.Right, rectangle.Top);
      QLinePointResult qlinePointResult1 = QMath.LinesIntersect(linePoint1, linePoint2, location, point3);
      if (qlinePointResult1.Result)
        return qlinePointResult1;
      PointF point2 = rectangle.Location;
      point3 = new PointF(rectangle.Left, rectangle.Bottom);
      QLinePointResult qlinePointResult2 = QMath.LinesIntersect(linePoint1, linePoint2, point2, point3);
      if (qlinePointResult2.Result)
        return qlinePointResult2;
      point2 = new PointF(rectangle.Right, rectangle.Top);
      point3 = new PointF(rectangle.Right, rectangle.Bottom);
      QLinePointResult qlinePointResult3 = QMath.LinesIntersect(linePoint1, linePoint2, point2, point3);
      if (qlinePointResult3.Result)
        return qlinePointResult3;
      point2 = new PointF(rectangle.Left, rectangle.Bottom);
      point3 = new PointF(rectangle.Right, rectangle.Bottom);
      QLinePointResult qlinePointResult4 = QMath.LinesIntersect(linePoint1, linePoint2, point2, point3);
      return qlinePointResult4.Result ? qlinePointResult4 : new QLinePointResult(false, PointF.Empty);
    }

    public static Matrix CreateTransformationMatrix(
      RectangleF destinationBounds,
      SizeF size,
      DockStyle dockStyle)
    {
      Matrix transformationMatrix;
      switch (dockStyle)
      {
        case DockStyle.Bottom:
          transformationMatrix = new Matrix(new RectangleF(PointF.Empty, size), new PointF[3]
          {
            new PointF(destinationBounds.Left, destinationBounds.Bottom),
            new PointF(destinationBounds.Right, destinationBounds.Bottom),
            new PointF(destinationBounds.Left, destinationBounds.Top)
          });
          break;
        case DockStyle.Left:
          transformationMatrix = new Matrix(new RectangleF(PointF.Empty, size), new PointF[3]
          {
            new PointF(destinationBounds.Left, destinationBounds.Top),
            new PointF(destinationBounds.Left, destinationBounds.Bottom),
            new PointF(destinationBounds.Right, destinationBounds.Top)
          });
          break;
        case DockStyle.Right:
          transformationMatrix = new Matrix(new RectangleF(PointF.Empty, size), new PointF[3]
          {
            new PointF(destinationBounds.Right, destinationBounds.Top),
            new PointF(destinationBounds.Right, destinationBounds.Bottom),
            new PointF(destinationBounds.Left, destinationBounds.Top)
          });
          break;
        default:
          transformationMatrix = new Matrix(new RectangleF(PointF.Empty, size), new PointF[3]
          {
            new PointF(destinationBounds.Left, destinationBounds.Top),
            new PointF(destinationBounds.Right, destinationBounds.Top),
            new PointF(destinationBounds.Left, destinationBounds.Bottom)
          });
          break;
      }
      return transformationMatrix;
    }

    public static float CalculateFontAscent(Graphics graphics, Font font) => font.GetHeight(graphics) / (float) font.FontFamily.GetLineSpacing(font.Style) * (float) font.FontFamily.GetCellAscent(font.Style);

    public static float CalculateFontDescent(Graphics graphics, Font font) => font.GetHeight(graphics) / (float) font.FontFamily.GetLineSpacing(font.Style) * (float) font.FontFamily.GetCellDescent(font.Style);

    public static Size CalculateOuterSize(
      QShape shape,
      QPadding padding,
      QMargin margin,
      Size contentSize,
      bool horizontal)
    {
      Size size = padding.InflateSizeWithPadding(contentSize, true, horizontal);
      if (shape != null)
        size = shape.CalculateShapeSize(size, horizontal);
      return margin.InflateSizeWithMargin(size, true, horizontal);
    }
  }
}
