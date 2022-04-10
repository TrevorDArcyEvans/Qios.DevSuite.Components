// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  internal class QColorTypeConverter : TypeConverter
  {
    internal const string AllThemesName = "AllThemes";
    private TypeConverter m_oColorConverter;

    public QColorTypeConverter() => this.m_oColorConverter = TypeDescriptor.GetConverter(typeof (Color));

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (context == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (context)));
      QColor color1 = ((QColorSchemeBase) context.Instance).GetColor(context.PropertyDescriptor.Name);
      if (value is string str && str.StartsWith("@"))
      {
        color1.ColorReference = str;
      }
      else
      {
        Color color2 = (Color) QMisc.AsValueType(this.m_oColorConverter.ConvertFrom(value));
        color1.Current = color2;
      }
      return (object) null;
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (value == null)
        return (object) null;
      QColor qcolor = (QColor) value;
      if (destinationType != typeof (string))
        return this.m_oColorConverter.ConvertTo((object) qcolor.Current, destinationType);
      string usedColorReference = qcolor.UsedColorReference;
      return usedColorReference != null && usedColorReference.Length > 0 ? (object) usedColorReference : this.m_oColorConverter.ConvertTo((object) qcolor.Current, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => this.m_oColorConverter.CanConvertFrom(sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => this.m_oColorConverter.CanConvertTo(destinationType);

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      if (context == null || context.Instance == null)
        return (PropertyDescriptorCollection) null;
      QColorScheme instance = (QColorScheme) context.Instance;
      PropertyDescriptor[] properties = new PropertyDescriptor[instance.Themes.Count + 1];
      for (int index = 0; index < instance.Themes.Count; ++index)
        properties[index] = (PropertyDescriptor) new QColorTypeConverter.QColorThemePropertyDescriptor(instance.Themes[index].ThemeName, context);
      properties[properties.Length - 1] = (PropertyDescriptor) new QColorTypeConverter.QColorThemePropertyDescriptor("AllThemes", context, true);
      return new PropertyDescriptorCollection(properties);
    }

    protected class QColorThemePropertyDescriptor : TypeConverter.SimplePropertyDescriptor
    {
      private bool m_bSetAllThemes;
      private ITypeDescriptorContext m_oContext;

      public QColorThemePropertyDescriptor(string themeName, ITypeDescriptorContext context)
        : base(typeof (QColor), themeName, typeof (Color))
      {
        this.m_oContext = context;
        this.AttributeArray = new Attribute[1]
        {
          (Attribute) new RefreshPropertiesAttribute(RefreshProperties.Repaint)
        };
      }

      public QColorThemePropertyDescriptor(
        string themeName,
        ITypeDescriptorContext context,
        bool setAllThemes)
        : base(typeof (QColor), themeName, typeof (Color))
      {
        this.m_oContext = context;
        this.AttributeArray = new Attribute[1]
        {
          (Attribute) new RefreshPropertiesAttribute(RefreshProperties.Repaint)
        };
        this.m_bSetAllThemes = setAllThemes;
      }

      public override object GetValue(object component)
      {
        QColor qcolor = (QColor) component;
        if (!this.m_bSetAllThemes)
          return (object) qcolor[this.Name];
        IEnumerator enumerator = ((QColorSchemeBase) this.m_oContext.Instance).Themes.GetEnumerator();
        Color empty = Color.Empty;
        while (enumerator.MoveNext() && enumerator.Current != null)
        {
          string themeName = ((QThemeInfo) enumerator.Current).ThemeName;
          if (themeName != "HighContrast")
          {
            if (empty.IsEmpty)
              empty = qcolor[themeName];
            else if (empty != qcolor[themeName])
              return (object) Color.Empty;
          }
        }
        return (object) empty;
      }

      public override void SetValue(object component, object value)
      {
        this.m_oContext.OnComponentChanging();
        QColor qcolor = (QColor) component;
        if (this.m_bSetAllThemes)
        {
          IEnumerator enumerator = ((QColorSchemeBase) this.m_oContext.Instance).Themes.GetEnumerator();
          while (enumerator.MoveNext() && enumerator.Current != null)
          {
            string themeName = ((QThemeInfo) enumerator.Current).ThemeName;
            if (themeName != "HighContrast")
              qcolor[themeName] = (Color) value;
          }
        }
        else
          qcolor[this.Name] = (Color) value;
        this.m_oContext.OnComponentChanged();
      }

      public override bool CanResetValue(object component) => this.ShouldSerializeValue(component);

      public override void ResetValue(object component)
      {
        this.m_oContext.OnComponentChanging();
        ((QColor) component).ResetColor(this.Name);
        this.m_oContext.OnComponentChanged();
      }

      public override bool ShouldSerializeValue(object component) => ((QColor) component).ShouldSerializeColor(this.Name);
    }
  }
}
