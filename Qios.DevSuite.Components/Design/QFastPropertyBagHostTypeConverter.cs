// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QFastPropertyBagHostTypeConverter
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
  internal class QFastPropertyBagHostTypeConverter : ExpandableObjectConverter
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
      IQFastPropertyBagHost qfastPropertyBagHost = (IQFastPropertyBagHost) value;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(qfastPropertyBagHost.GetType());
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < properties.Count; ++index)
      {
        if (properties[index].IsBrowsable)
        {
          int propertyIndex = QPropertyIndexAttribute.GetPropertyIndex(properties[index]);
          if (propertyIndex >= 0)
          {
            if (!qfastPropertyBagHost.Properties.IsHidden(propertyIndex))
            {
              Attribute[] attributes1 = new Attribute[properties[index].Attributes.Count];
              properties[index].Attributes.CopyTo((Array) attributes1, 0);
              arrayList.Add((object) new QFastPropertyBagHostTypeConverter.QFastPropertyBagPropertyDescriptor(qfastPropertyBagHost.GetType(), properties[index].Name, properties[index].PropertyType, attributes1, context));
            }
          }
          else
            arrayList.Add((object) properties[index]);
        }
      }
      return (PropertyDescriptorCollection) new QFastPropertyBagHostTypeConverter.QFastPropertyDescriptiorCollection((PropertyDescriptor[]) arrayList.ToArray(typeof (PropertyDescriptor)));
    }

    internal class QFastPropertyDescriptiorCollection : PropertyDescriptorCollection
    {
      public QFastPropertyDescriptiorCollection(PropertyDescriptor[] properties)
        : base(properties)
      {
      }

      public override PropertyDescriptorCollection Sort(
        IComparer comparer)
      {
        return base.Sort((IComparer) new QFastPropertyBagHostTypeConverter.QFastPropertyComparer());
      }
    }

    protected class QFastPropertyBagPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
    {
      private ITypeDescriptorContext m_oContext;

      public QFastPropertyBagPropertyDescriptor(
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
        component.GetType().GetProperty(this.Name, this.PropertyType, Type.EmptyTypes).SetValue(component, value, (object[]) null);
        if (this.m_oContext == null)
          return;
        this.m_oContext.OnComponentChanged();
      }

      public override bool CanResetValue(object component) => this.ShouldSerializeValue(component);

      public override void ResetValue(object component)
      {
        if (this.m_oContext != null)
          this.m_oContext.OnComponentChanging();
        ((IQFastPropertyBagHost) component).Properties.ResetProperty(QPropertyIndexAttribute.GetPropertyIndex(component.GetType().GetProperty(this.Name, this.PropertyType, Type.EmptyTypes)));
        if (this.m_oContext == null)
          return;
        this.m_oContext.OnComponentChanged();
      }

      public override bool ShouldSerializeValue(object component) => !((IQFastPropertyBagHost) component).Properties.IsSetToDefaultValue(QPropertyIndexAttribute.GetPropertyIndex(component.GetType().GetProperty(this.Name, this.PropertyType, Type.EmptyTypes)));
    }

    internal class QFastPropertyComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        PropertyDescriptor propertyDescriptor1 = x as PropertyDescriptor;
        PropertyDescriptor propertyDescriptor2 = y as PropertyDescriptor;
        if (propertyDescriptor1 == null && propertyDescriptor2 == null)
          return 0;
        if (propertyDescriptor1 == null)
          return -1;
        if (propertyDescriptor2 == null)
          return 1;
        bool flag1 = propertyDescriptor1.PropertyType.IsSubclassOf(typeof (QFastPropertyBagHost));
        bool flag2 = propertyDescriptor2.PropertyType.IsSubclassOf(typeof (QFastPropertyBagHost));
        if (flag1 && flag2 || !flag1 && !flag2)
          return string.Compare(propertyDescriptor1.Name, propertyDescriptor2.Name);
        if (flag1)
          return -1;
        return flag2 ? 1 : 0;
      }
    }
  }
}
