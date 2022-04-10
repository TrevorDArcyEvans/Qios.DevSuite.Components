// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QThemeInfoTypeConverter
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
  public class QThemeInfoTypeConverter : ExpandableObjectConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      QThemeInfo qthemeInfo = (QThemeInfo) value;
      if (destinationType == typeof (string))
        return (object) qthemeInfo.ThemeName;
      if (destinationType != typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      return (object) new InstanceDescriptor((MemberInfo) value.GetType().GetConstructor(new Type[3]
      {
        typeof (string),
        typeof (string),
        typeof (string)
      }), (ICollection) new object[3]
      {
        (object) qthemeInfo.ThemeName,
        (object) qthemeInfo.FileName,
        (object) qthemeInfo.WindowsSchemeName
      }, true);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }
}
