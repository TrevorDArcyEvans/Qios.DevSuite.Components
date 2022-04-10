// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCommandConfigurationTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QCommandConfigurationTypeConverter : ExpandableObjectConverter
  {
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || base.CanConvertTo(context, destinationType);

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      return destinationType == typeof (string) ? (object) ("(" + value.GetType().Name + ")") : base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
