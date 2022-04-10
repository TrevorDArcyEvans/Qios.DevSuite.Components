// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapeItemConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QShapeItemConverter : TypeConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      System.Type destType)
    {
      if (value == null)
        return (object) null;
      QShapeItem qshapeItem = value as QShapeItem;
      if (destType == typeof (string))
        return (object) string.Format((IFormatProvider) culture, "{0}, {1}", (object) qshapeItem.Location, (object) qshapeItem.ItemType.ToString());
      if (destType == typeof (InstanceDescriptor))
      {
        if (qshapeItem.ItemType == QShapeItemType.Point)
          return (object) new InstanceDescriptor((MemberInfo) typeof (QShapeItem).GetConstructor(new System.Type[4]
          {
            typeof (float),
            typeof (float),
            typeof (AnchorStyles),
            typeof (bool)
          }), (ICollection) new object[4]
          {
            (object) qshapeItem.X,
            (object) qshapeItem.Y,
            (object) qshapeItem.Anchor,
            (object) qshapeItem.LineVisible
          }, true);
        if (qshapeItem.ItemType == QShapeItemType.Bezier)
          return (object) new InstanceDescriptor((MemberInfo) typeof (QShapeItem).GetConstructor(new System.Type[8]
          {
            typeof (float),
            typeof (float),
            typeof (float),
            typeof (float),
            typeof (float),
            typeof (float),
            typeof (AnchorStyles),
            typeof (bool)
          }), (ICollection) new object[8]
          {
            (object) qshapeItem.X,
            (object) qshapeItem.Y,
            (object) qshapeItem.BezierControl1X,
            (object) qshapeItem.BezierControl1Y,
            (object) qshapeItem.BezierControl2X,
            (object) qshapeItem.BezierControl2Y,
            (object) qshapeItem.Anchor,
            (object) qshapeItem.LineVisible
          }, true);
      }
      return base.ConvertTo(context, culture, value, destType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destType) => destType == typeof (string) || destType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destType);

    public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType) => base.CanConvertFrom(context, sourceType);

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(value, attributes);
    }
  }
}
