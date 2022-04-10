// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorHslTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Qios.DevSuite.Components.Design
{
  public class QColorHslTypeConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      if (destinationType == typeof (string))
        return (object) value.ToString();
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      ConstructorInfo constructor = typeof (QColorHsl).GetConstructor(new Type[3]
      {
        typeof (int),
        typeof (int),
        typeof (int)
      });
      QColorHsl qcolorHsl = (QColorHsl) value;
      return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[3]
      {
        (object) qcolorHsl.Hue,
        (object) qcolorHsl.Saturation,
        (object) qcolorHsl.Lightness
      }, true);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str))
        return base.ConvertFrom(context, culture, value);
      QColorHsl qcolorHsl = new QColorHsl();
      char[] chArray = new char[1]{ ',' };
      string[] strArray = str.Split(chArray);
      if (strArray.Length >= 1 && strArray[0] != null && strArray[0].Length > 0)
        qcolorHsl.Hue = int.Parse(strArray[0]);
      if (strArray.Length >= 2 && strArray[1] != null && strArray[1].Length > 0)
        qcolorHsl.Saturation = int.Parse(strArray[1]);
      if (strArray.Length >= 3 && strArray[2] != null && strArray[2].Length > 0)
        qcolorHsl.Lightness = int.Parse(strArray[2]);
      return (object) qcolorHsl;
    }
  }
}
