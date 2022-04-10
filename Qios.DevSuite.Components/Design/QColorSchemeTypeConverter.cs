// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorSchemeTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  internal class QColorSchemeTypeConverter : ExpandableObjectConverter
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

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      if (context == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (context)));
      QColorScheme qcolorScheme = (QColorScheme) value;
      Type type1 = qcolorScheme.GetType();
      Type controlType = context.Instance != null ? context.Instance.GetType() : (Type) null;
      PropertyDescriptorCollection properties = base.GetProperties(context, value, attributes);
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      Type type2 = context.Instance.GetType();
      if (context.PropertyDescriptor != null)
      {
        while (type2 != null)
        {
          PropertyInfo property = type2.GetProperty(context.PropertyDescriptor.Name);
          if (property != null)
          {
            object[] customAttributes = property.GetCustomAttributes(typeof (QColorSchemeShowColorsAttribute), true);
            Array.Sort((Array) customAttributes, (IComparer) new QColorSchemeTypeConverter.QColorSchemeShowColorsComparer());
            arrayList2.AddRange((ICollection) customAttributes);
            if (type2 != null)
              type2 = type2.BaseType;
          }
          else
            type2 = (Type) null;
        }
      }
      for (int index1 = 0; index1 < properties.Count; ++index1)
      {
        PropertyDescriptor propertyDescriptor = properties[index1];
        if (propertyDescriptor.PropertyType == typeof (QColor))
        {
          QColorSchemeScope scope = qcolorScheme.Scope;
          bool visible = false;
          switch (scope)
          {
            case QColorSchemeScope.Control:
              visible = QColorDesignVisibleAttribute.ShouldBeVisibleForType(type1.GetProperty(propertyDescriptor.Name).GetCustomAttributes(typeof (QColorDesignVisibleAttribute), true), controlType);
              break;
            case QColorSchemeScope.ControlAndRelated:
              visible = QColorDesignVisibleAttribute.ShouldBeVisibleForType(type1.GetProperty(propertyDescriptor.Name).GetCustomAttributes(typeof (QColorDesignVisibleAttribute), true), controlType, scope);
              break;
            case QColorSchemeScope.All:
              visible = true;
              break;
          }
          if (context.Instance != null && arrayList2.Count > 0)
          {
            object[] customAttributes = type1.GetProperty(propertyDescriptor.Name).GetCustomAttributes(typeof (QColorCategoryVisibleAttribute), true);
            for (int index2 = arrayList2.Count - 1; index2 >= 0; --index2)
              ((QColorSchemeShowColorsAttribute) arrayList2[index2]).ShowColor(customAttributes, scope, ref visible);
          }
          if (visible)
            arrayList1.Add((object) new QColorSchemeTypeConverter.QColorSchemePropertyDescriptor(propertyDescriptor.Name, propertyDescriptor.Attributes, context));
        }
        else
          arrayList1.Add((object) propertyDescriptor);
      }
      return (PropertyDescriptorCollection) new QColorSchemeTypeConverter.QColorSchemePropertyDescriptorCollection((PropertyDescriptor[]) arrayList1.ToArray(typeof (PropertyDescriptor)));
    }

    protected sealed class QColorSchemePropertyDescriptor : TypeConverter.SimplePropertyDescriptor
    {
      private ITypeDescriptorContext m_oContext;

      public QColorSchemePropertyDescriptor(
        string colorName,
        AttributeCollection attributes,
        ITypeDescriptorContext context)
        : base(typeof (QColorScheme), colorName, typeof (QColor))
      {
        this.m_oContext = context;
        int length = 1;
        Attribute[] attributeArray;
        if (attributes != null)
        {
          attributeArray = new Attribute[attributes.Count + length];
          for (int index = 0; index < attributes.Count; ++index)
            attributeArray[index] = attributes[index];
        }
        else
          attributeArray = new Attribute[length];
        attributeArray[attributeArray.Length - 1] = (Attribute) new RefreshPropertiesAttribute(RefreshProperties.Repaint);
        this.AttributeArray = attributeArray;
      }

      public override object GetValue(object component) => (object) ((QColorSchemeBase) component)[this.Name];

      public override bool IsReadOnly => false;

      public override void SetValue(object component, object value)
      {
      }

      public override bool CanResetValue(object component) => this.ShouldSerializeValue(component);

      public override void ResetValue(object component)
      {
        if (component == null)
          throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (component)));
        this.m_oContext.OnComponentChanging();
        ((QColorSchemeBase) component)[this.Name].Reset();
        this.m_oContext.OnComponentChanged();
      }

      public override bool ShouldSerializeValue(object component) => component != null ? ((QColorSchemeBase) component)[this.Name].ShouldSerialize() : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (component)));
    }

    internal class QColorSchemePropertyDescriptorCollection : PropertyDescriptorCollection
    {
      public QColorSchemePropertyDescriptorCollection(PropertyDescriptor[] properties)
        : base(properties)
      {
      }

      public override PropertyDescriptorCollection Sort(
        IComparer comparer)
      {
        return base.Sort((IComparer) new QColorSchemeTypeConverter.QColorSchemePropertyComparer());
      }
    }

    internal class QColorSchemePropertyComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        if (x == null && y == null)
          return 0;
        if (x == null)
          return -1;
        if (y == null)
          return 1;
        PropertyDescriptor propertyDescriptor1 = (PropertyDescriptor) x;
        PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor) y;
        QColorSchemeTypeConverter.QColorSchemePropertyDescriptor propertyDescriptor3 = x as QColorSchemeTypeConverter.QColorSchemePropertyDescriptor;
        QColorSchemeTypeConverter.QColorSchemePropertyDescriptor propertyDescriptor4 = y as QColorSchemeTypeConverter.QColorSchemePropertyDescriptor;
        if (propertyDescriptor3 != null && propertyDescriptor4 != null || propertyDescriptor3 == null && propertyDescriptor4 == null)
          return string.Compare(propertyDescriptor1.Name, propertyDescriptor2.Name, false, CultureInfo.InvariantCulture);
        return propertyDescriptor3 != null ? 1 : -1;
      }
    }

    private class QColorSchemeShowColorsComparer : IComparer
    {
      public int Compare(object x, object y) => ((QColorSchemeShowColorsAttribute) x).Method.CompareTo((object) ((QColorSchemeShowColorsAttribute) y).Method);
    }
  }
}
