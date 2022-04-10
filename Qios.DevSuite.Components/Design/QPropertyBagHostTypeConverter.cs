// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QPropertyBagHostTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  internal class QPropertyBagHostTypeConverter : ExpandableObjectConverter
  {
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

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || base.CanConvertTo(context, destinationType);

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      IQPropertyBagHost qpropertyBagHost = (IQPropertyBagHost) value;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(qpropertyBagHost.GetType());
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < properties.Count; ++index)
      {
        if (properties[index].IsBrowsable)
        {
          if (qpropertyBagHost.Properties.IsDefined(properties[index].Name))
          {
            Attribute[] attributes1 = new Attribute[properties[index].Attributes.Count];
            properties[index].Attributes.CopyTo((Array) attributes1, 0);
            arrayList.Add((object) new QPropertyBagHostTypeConverter.QPropertyBagPropertyDescriptor(qpropertyBagHost.GetType(), properties[index].Name, properties[index].PropertyType, attributes1, context));
          }
          else
            arrayList.Add((object) properties[index]);
        }
      }
      return new PropertyDescriptorCollection((PropertyDescriptor[]) arrayList.ToArray(typeof (PropertyDescriptor)));
    }

    protected class QPropertyBagPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
    {
      private ITypeDescriptorContext m_oContext;

      public QPropertyBagPropertyDescriptor(
        Type hostType,
        string propertyName,
        Type propertyType,
        Attribute[] attributes,
        ITypeDescriptorContext context)
        : base(hostType, propertyName, propertyType, attributes)
      {
        this.m_oContext = context;
      }

      public override object GetValue(object component) => component.GetType().GetProperty(this.Name, this.PropertyType, Type.EmptyTypes).GetValue(component, (object[]) null);

      public override void SetValue(object component, object value)
      {
        if (this.m_oContext != null)
          this.m_oContext.OnComponentChanging();
        component.GetType().GetProperty(this.Name).SetValue(component, value, (object[]) null);
        if (this.m_oContext == null)
          return;
        this.m_oContext.OnComponentChanged();
      }

      public override bool CanResetValue(object component) => this.ShouldSerializeValue(component);

      public override void ResetValue(object component)
      {
        if (this.m_oContext != null)
          this.m_oContext.OnComponentChanging();
        ((IQPropertyBagHost) component).Properties.ResetProperty(this.Name);
        if (this.m_oContext == null)
          return;
        this.m_oContext.OnComponentChanged();
      }

      public override bool ShouldSerializeValue(object component) => !((IQPropertyBagHost) component).Properties.IsSetToDefaultValue(this.Name);
    }
  }
}
