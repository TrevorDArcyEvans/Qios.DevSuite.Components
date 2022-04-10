// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QSpacingConverter
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
  [ToolboxItem(false)]
  public class QSpacingConverter : TypeConverter
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
        QSpacing qspacing = (QSpacing) value;
        return (object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0},{1}", (object) qspacing.Before, (object) qspacing.After);
      }
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      ConstructorInfo constructor = typeof (QSpacing).GetConstructor(new Type[2]
      {
        typeof (int),
        typeof (int)
      });
      QSpacing qspacing1 = (QSpacing) value;
      return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[2]
      {
        (object) qspacing1.Before,
        (object) qspacing1.After
      }, true);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str))
        return base.ConvertFrom(context, culture, value);
      QSpacing empty = QSpacing.Empty;
      QSpacing qspacing;
      try
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = str.Split(chArray);
        qspacing = new QSpacing();
        qspacing.Before = Convert.ToInt32(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture);
        qspacing.After = Convert.ToInt32(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch
      {
        throw new NotSupportedException(QResources.GetException("QSpacingConverter_InvalidFormat"));
      }
      return (object) qspacing;
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
      return propertyValues != null ? (object) new QSpacing((int) QMisc.AsValueType(propertyValues[(object) "Before"]), (int) QMisc.AsValueType(propertyValues[(object) "After"])) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (propertyValues)));
    }
  }
}
