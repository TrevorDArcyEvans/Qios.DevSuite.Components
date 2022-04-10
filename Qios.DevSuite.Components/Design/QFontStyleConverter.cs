// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QFontStyleConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QFontStyleConverter : ExpandableObjectConverter
  {
    public static string ConvertToString(QFontStyle style, string separator)
    {
      if (style == QFontStyle.Regular)
        return "Regular";
      StringBuilder stringBuilder = new StringBuilder();
      if (style.Bold)
        stringBuilder.Append("Bold" + separator + " ");
      if (style.Italic)
        stringBuilder.Append("Italic" + separator + " ");
      if (style.Strikeout)
        stringBuilder.Append("Strikeout" + separator + " ");
      if (style.Underline)
        stringBuilder.Append("Underline" + separator + " ");
      return stringBuilder.ToString();
    }

    public static QFontStyle ConvertFromString(string value, string separator)
    {
      QFontStyle qfontStyle = new QFontStyle(FontStyle.Regular);
      try
      {
        string[] strArray = value.Split(separator.ToCharArray());
        for (int index = 0; index < strArray.Length; ++index)
        {
          switch (strArray[index] != null ? strArray[index].Trim().ToLower(CultureInfo.CurrentCulture) : string.Empty)
          {
            case "bold":
              qfontStyle.Bold = true;
              break;
            case "italic":
              qfontStyle.Italic = true;
              break;
            case "underline":
              qfontStyle.Underline = true;
              break;
            case "strikeout":
              qfontStyle.Strikeout = true;
              break;
          }
        }
      }
      catch
      {
        throw new NotSupportedException(QResources.GetException("QFontStyle_InvalidFormat"));
      }
      return qfontStyle;
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      if (destinationType == typeof (string))
        return (object) QFontStyleConverter.ConvertToString((QFontStyle) value, ";");
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      ConstructorInfo constructor = typeof (QFontStyle).GetConstructor(new Type[4]
      {
        typeof (bool),
        typeof (bool),
        typeof (bool),
        typeof (bool)
      });
      QFontStyle qfontStyle = (QFontStyle) value;
      return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[4]
      {
        (object) qfontStyle.Bold,
        (object) qfontStyle.Italic,
        (object) qfontStyle.Strikeout,
        (object) qfontStyle.Underline
      }, true);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return value is string str ? (object) QFontStyleConverter.ConvertFromString(str, ";") : base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      return propertyValues != null ? (object) new QFontStyle((bool) QMisc.AsValueType(propertyValues[(object) "Bold"]), (bool) QMisc.AsValueType(propertyValues[(object) "Italic"]), (bool) QMisc.AsValueType(propertyValues[(object) "Strikeout"]), (bool) QMisc.AsValueType(propertyValues[(object) "Underline"])) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (propertyValues)));
    }
  }
}
