﻿// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMarkupTextStyleConverter
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
  public class QMarkupTextStyleConverter : TypeConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      QMarkupTextStyle qmarkupTextStyle = value as QMarkupTextStyle;
      return destinationType == typeof (string) ? (!QMisc.IsEmpty((object) qmarkupTextStyle.DefaultTag) ? (object) (qmarkupTextStyle.DefaultTag + ": " + typeof (QMarkupTextStyle).Name) : (object) typeof (QMarkupTextStyle).Name) : (destinationType == typeof (InstanceDescriptor) ? (object) new InstanceDescriptor((MemberInfo) typeof (QMarkupTextStyle).GetConstructor(new Type[0]), (ICollection) new object[0], false) : base.ConvertTo(context, culture, value, destinationType));
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }
}
