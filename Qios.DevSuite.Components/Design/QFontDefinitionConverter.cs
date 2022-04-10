// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QFontDefinitionConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QFontDefinitionConverter : ExpandableObjectConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      if (destinationType == typeof (string))
      {
        QFontDefinition qfontDefinition = (QFontDefinition) value;
        if (qfontDefinition == QFontDefinition.Empty)
          return (object) "Empty";
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(qfontDefinition.FontFamily + ";");
        stringBuilder.Append(qfontDefinition.Size.ToString() + ";");
        stringBuilder.Append("Style=" + QFontStyleConverter.ConvertToString(qfontDefinition.Style, ","));
        return (object) stringBuilder.ToString();
      }
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      ConstructorInfo constructor = typeof (QFontDefinition).GetConstructor(new Type[6]
      {
        typeof (string),
        typeof (bool),
        typeof (bool),
        typeof (bool),
        typeof (bool),
        typeof (float)
      });
      QFontDefinition qfontDefinition1 = (QFontDefinition) value;
      return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[6]
      {
        (object) qfontDefinition1.FontFamily,
        (object) qfontDefinition1.Bold,
        (object) qfontDefinition1.Italic,
        (object) qfontDefinition1.Strikeout,
        (object) qfontDefinition1.Underline,
        (object) qfontDefinition1.Size
      }, true);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str))
        return base.ConvertFrom(context, culture, value);
      QFontDefinition qfontDefinition = new QFontDefinition((string) null, QFontStyle.Regular, -1f);
      try
      {
        char[] chArray = new char[1]{ ';' };
        string[] strArray1 = str.Split(chArray);
        if (strArray1.Length < 3 && string.Compare(strArray1[0], "empty", true) == 0)
          return (object) QFontDefinition.Empty;
        if (!QMisc.IsEmpty((object) strArray1[0]))
          qfontDefinition.FontFamily = strArray1[0];
        if (!QMisc.IsEmpty((object) strArray1[1]))
          qfontDefinition.Size = (float) Convert.ToDouble(strArray1[1], (IFormatProvider) culture);
        if (!QMisc.IsEmpty((object) strArray1[2]))
        {
          string[] strArray2 = strArray1[2].Split('=');
          if (strArray2.Length < 2)
            throw new Exception();
          qfontDefinition.Style = string.Compare(strArray2[0].Trim(), "style", true, culture) == 0 ? QFontStyleConverter.ConvertFromString(strArray2[1], ",") : throw new Exception();
        }
      }
      catch
      {
        throw new NotSupportedException(QResources.GetException("QFontDefinition_InvalidFormat"));
      }
      return (object) qfontDefinition;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      return propertyValues != null ? (object) new QFontDefinition(propertyValues[(object) "FontFamily"] as string, (bool) QMisc.AsValueType(propertyValues[(object) "Bold"]), (bool) QMisc.AsValueType(propertyValues[(object) "Italic"]), (bool) QMisc.AsValueType(propertyValues[(object) "Strikeout"]), (bool) QMisc.AsValueType(propertyValues[(object) "Underline"]), (float) propertyValues[(object) "Size"]) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (propertyValues)));
    }
  }
}
