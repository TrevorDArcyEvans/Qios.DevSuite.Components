// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QPointFConverter
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

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QPointFConverter : TypeConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      if (culture == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (culture)));
      if (destinationType == typeof (string))
      {
        PointF pointF = (PointF) value;
        return (object) string.Format((IFormatProvider) culture, "{0}{2}{1}", (object) pointF.X, (object) pointF.Y, (object) culture.TextInfo.ListSeparator);
      }
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      ConstructorInfo constructor = typeof (PointF).GetConstructor(new Type[2]
      {
        typeof (float),
        typeof (float)
      });
      PointF pointF1 = (PointF) value;
      return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[2]
      {
        (object) pointF1.X,
        (object) pointF1.Y
      }, true);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (culture == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (culture)));
      if (!(value is string str))
        return base.ConvertFrom(context, culture, value);
      PointF empty = PointF.Empty;
      try
      {
        string[] strArray = str.Split(culture.TextInfo.ListSeparator.ToCharArray());
        empty.X = (float) Convert.ToDouble(strArray[0], (IFormatProvider) culture);
        empty.Y = (float) Convert.ToInt32(strArray[1], (IFormatProvider) culture);
      }
      catch
      {
        throw new NotSupportedException(QResources.GetException("QPointFConverter_InvalidFormat"));
      }
      return (object) empty;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(value, attributes);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      return propertyValues != null ? (object) new PointF((float) QMisc.AsValueType(propertyValues[(object) "X"]), (float) QMisc.AsValueType(propertyValues[(object) "Y"])) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (propertyValues)));
    }
  }
}
