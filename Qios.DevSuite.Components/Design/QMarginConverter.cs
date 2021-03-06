// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMarginConverter
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
  public class QMarginConverter : TypeConverter
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
        QMargin qmargin = (QMargin) value;
        return (object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0},{1},{2},{3}", (object) qmargin.Bottom, (object) qmargin.Left, (object) qmargin.Right, (object) qmargin.Top);
      }
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      ConstructorInfo constructor = typeof (QMargin).GetConstructor(new Type[4]
      {
        typeof (int),
        typeof (int),
        typeof (int),
        typeof (int)
      });
      QMargin qmargin1 = (QMargin) value;
      return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[4]
      {
        (object) qmargin1.Left,
        (object) qmargin1.Top,
        (object) qmargin1.Bottom,
        (object) qmargin1.Right
      }, true);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string str))
        return base.ConvertFrom(context, culture, value);
      QMargin empty = QMargin.Empty;
      QMargin qmargin;
      try
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = str.Split(chArray);
        qmargin = new QMargin();
        qmargin.Bottom = Convert.ToInt32(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture);
        qmargin.Left = Convert.ToInt32(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture);
        qmargin.Right = Convert.ToInt32(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture);
        qmargin.Top = Convert.ToInt32(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch
      {
        throw new NotSupportedException(QResources.GetException("QMarginConverter_InvalidFormat"));
      }
      return (object) qmargin;
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
      return propertyValues != null ? (object) new QMargin((int) QMisc.AsValueType(propertyValues[(object) "Left"]), (int) QMisc.AsValueType(propertyValues[(object) "Top"]), (int) QMisc.AsValueType(propertyValues[(object) "Bottom"]), (int) QMisc.AsValueType(propertyValues[(object) "Right"])) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (propertyValues)));
    }
  }
}
