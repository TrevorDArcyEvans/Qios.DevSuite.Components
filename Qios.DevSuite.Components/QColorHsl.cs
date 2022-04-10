// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorHsl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QColorHslTypeConverter))]
  public struct QColorHsl
  {
    public int Hue;
    public int Saturation;
    public int Lightness;
    public static readonly QColorHsl Empty = new QColorHsl(0, 0, 0);

    public QColorHsl(int hue, int saturation, int lightness)
    {
      this.Hue = hue;
      this.Saturation = saturation;
      this.Lightness = lightness;
    }

    public static QColorHsl FromColor(Color color) => QColorHsl.FromRGB((int) color.R, (int) color.G, (int) color.B);

    public Color ToColor() => Color.FromArgb(QColorHsl.HslToRgb(this.Hue, this.Saturation, this.Lightness));

    public int ToRGB() => QColorHsl.HslToRgb(this.Hue, this.Saturation, this.Lightness);

    public void SetElement(QColorHslElement element, int value)
    {
      switch (element)
      {
        case QColorHslElement.Hue:
          this.Hue = value;
          break;
        case QColorHslElement.Saturation:
          this.Saturation = value;
          break;
        case QColorHslElement.Lightness:
          this.Lightness = value;
          break;
      }
    }

    public int GetElement(QColorHslElement element)
    {
      switch (element)
      {
        case QColorHslElement.Hue:
          return this.Hue;
        case QColorHslElement.Saturation:
          return this.Saturation;
        case QColorHslElement.Lightness:
          return this.Lightness;
        default:
          return 0;
      }
    }

    public static int HslToRgb(int hue, int saturation, int lightness)
    {
      double num1 = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      double num4 = (double) hue / (double) byte.MaxValue * 360.0 % 360.0;
      double num5 = (double) saturation / (double) byte.MaxValue;
      double num6 = (double) lightness / (double) byte.MaxValue;
      if (num5 == 0.0)
      {
        num1 = num6;
        num2 = num6;
        num3 = num6;
      }
      else
      {
        double d = num4 / 60.0;
        int num7 = (int) Math.Floor(d);
        double num8 = d - (double) num7;
        double num9 = num6 * (1.0 - num5);
        double num10 = num6 * (1.0 - num5 * num8);
        double num11 = num6 * (1.0 - num5 * (1.0 - num8));
        switch (num7)
        {
          case 0:
            num1 = num6;
            num2 = num11;
            num3 = num9;
            break;
          case 1:
            num1 = num10;
            num2 = num6;
            num3 = num9;
            break;
          case 2:
            num1 = num9;
            num2 = num6;
            num3 = num11;
            break;
          case 3:
            num1 = num9;
            num2 = num10;
            num3 = num6;
            break;
          case 4:
            num1 = num11;
            num2 = num9;
            num3 = num6;
            break;
          case 5:
            num1 = num6;
            num2 = num9;
            num3 = num10;
            break;
        }
      }
      byte num12 = (byte) (num1 * (double) byte.MaxValue);
      byte num13 = (byte) (num2 * (double) byte.MaxValue);
      return (int) (byte) (num3 * (double) byte.MaxValue) | (int) num13 << 8 | (int) num12 << 16 | -16777216;
    }

    public static QColorHsl FromRGB(int red, int green, int blue)
    {
      double val1 = (double) red / (double) byte.MaxValue;
      double val2_1 = (double) green / (double) byte.MaxValue;
      double val2_2 = (double) blue / (double) byte.MaxValue;
      double num1 = Math.Min(Math.Min(val1, val2_1), val2_2);
      double num2 = Math.Max(Math.Max(val1, val2_1), val2_2);
      double num3 = num2;
      double num4 = num2 - num1;
      double num5;
      double num6;
      if (num2 == 0.0 || num4 == 0.0)
      {
        num5 = 0.0;
        num6 = 0.0;
      }
      else
      {
        num5 = num4 / num2;
        num6 = val1 != num2 ? (val2_1 != num2 ? 4.0 + (val1 - val2_1) / num4 : 2.0 + (val2_2 - val1) / num4) : (val2_1 - val2_2) / num4;
      }
      double num7 = num6 * 60.0;
      if (num7 < 0.0)
        num7 += 360.0;
      return new QColorHsl((int) (num7 / 360.0 * (double) byte.MaxValue), (int) (num5 * (double) byte.MaxValue), (int) (num3 * (double) byte.MaxValue));
    }

    public static QColorHslElement GetUnusedElement(
      QColorHslElement a,
      QColorHslElement b)
    {
      if (a != QColorHslElement.Hue && b != QColorHslElement.Hue)
        return QColorHslElement.Hue;
      if (a != QColorHslElement.Saturation && b != QColorHslElement.Saturation)
        return QColorHslElement.Saturation;
      return a != QColorHslElement.Lightness && b != QColorHslElement.Lightness ? QColorHslElement.Lightness : QColorHslElement.None;
    }

    public override bool Equals(object obj) => obj is QColorHsl qcolorHsl && this.Hue == qcolorHsl.Hue && this.Saturation == qcolorHsl.Saturation && this.Lightness == qcolorHsl.Lightness;

    public override int GetHashCode() => this.Hue ^ this.Saturation ^ this.Lightness;

    public static bool operator ==(QColorHsl a, QColorHsl b) => a.Equals((object) b);

    public static bool operator !=(QColorHsl a, QColorHsl b) => !a.Equals((object) b);

    public override string ToString() => string.Format("{0}, {1}, {2}", (object) this.Hue, (object) this.Saturation, (object) this.Lightness);
  }
}
