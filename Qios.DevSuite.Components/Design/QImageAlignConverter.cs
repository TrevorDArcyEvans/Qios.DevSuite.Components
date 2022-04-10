// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QImageAlignConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QImageAlignConverter : EnumConverter
  {
    public QImageAlignConverter()
      : base(typeof (QImageAlign))
    {
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      return destinationType == typeof (ContentAlignment) ? (object) QImageAlignConverter.ConvertToContentAlignment((QImageAlign) value) : base.ConvertTo(context, culture, value, destinationType);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return value is ContentAlignment contentAlignment ? (object) QImageAlignConverter.ConvertFromContentAlignment(contentAlignment) : base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (ContentAlignment) || base.CanConvertTo(context, destinationType);

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (ContentAlignment) || base.CanConvertFrom(context, sourceType);

    public static QImageAlign ConvertFromContentAlignment(
      ContentAlignment contentAlignment)
    {
      switch (contentAlignment)
      {
        case ContentAlignment.TopLeft:
          return QImageAlign.TopLeft;
        case ContentAlignment.TopCenter:
          return QImageAlign.TopMiddle;
        case ContentAlignment.TopRight:
          return QImageAlign.TopRight;
        case ContentAlignment.MiddleLeft:
          return QImageAlign.CenterLeft;
        case ContentAlignment.MiddleCenter:
          return QImageAlign.Centered;
        case ContentAlignment.MiddleRight:
          return QImageAlign.CenterRight;
        case ContentAlignment.BottomLeft:
          return QImageAlign.BottomLeft;
        case ContentAlignment.BottomCenter:
          return QImageAlign.BottomMiddle;
        case ContentAlignment.BottomRight:
          return QImageAlign.BottomRight;
        default:
          return QImageAlign.Centered;
      }
    }

    public static ContentAlignment ConvertToContentAlignment(QImageAlign imageAlign)
    {
      switch (imageAlign)
      {
        case QImageAlign.Centered:
          return ContentAlignment.MiddleCenter;
        case QImageAlign.TopLeft:
          return ContentAlignment.TopLeft;
        case QImageAlign.CenterLeft:
          return ContentAlignment.MiddleLeft;
        case QImageAlign.BottomLeft:
          return ContentAlignment.BottomLeft;
        case QImageAlign.TopRight:
          return ContentAlignment.TopRight;
        case QImageAlign.CenterRight:
          return ContentAlignment.MiddleRight;
        case QImageAlign.BottomRight:
          return ContentAlignment.BottomRight;
        case QImageAlign.TopMiddle:
          return ContentAlignment.TopCenter;
        case QImageAlign.BottomMiddle:
          return ContentAlignment.BottomCenter;
        default:
          return ContentAlignment.MiddleCenter;
      }
    }
  }
}
