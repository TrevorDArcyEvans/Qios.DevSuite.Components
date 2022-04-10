// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QShapeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QShapeConverter : ComponentConverter
  {
    public QShapeConverter(Type type)
      : base(type)
    {
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => false;

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      ICollection standardValues = (ICollection) base.GetStandardValues(context);
      AttributeCollection attributes = context.PropertyDescriptor.Attributes;
      if (attributes == null || attributes.Count == 0)
        return standardValues as TypeConverter.StandardValuesCollection;
      ArrayList values1 = new ArrayList();
      foreach (object obj in (IEnumerable) standardValues)
      {
        QShape shape = obj as QShape;
        if (this.AllowShapeAsStandardValue(shape, attributes))
          values1.Add((object) shape);
      }
      Array values2 = Enum.GetValues(typeof (QBaseShapeType));
      for (int index = 0; index < values2.Length; ++index)
      {
        QBaseShapeType type = (QBaseShapeType) values2.GetValue(index);
        if (QShape.BaseShapes[type] != null && this.AllowShapeTypeAsStandardValue(QShape.BaseShapes[type].ShapeType, attributes))
          values1.Add((object) type);
      }
      return new TypeConverter.StandardValuesCollection((ICollection) values1);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (string) || destinationType == typeof (QBaseShapeType) || destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || sourceType == typeof (QBaseShapeType) || base.CanConvertFrom(context, sourceType);

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destType)
    {
      QShape qshape = value as QShape;
      if (destType == typeof (string))
      {
        if (context != null)
        {
          IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
          if (service != null)
          {
            string name = service.GetName(value);
            if (name != null)
              return (object) name;
            return value != null ? (object) ("(" + value.ToString() + ")") : (object) null;
          }
        }
      }
      else
      {
        if (destType == typeof (QBaseShapeType))
          return (object) qshape.BaseShapeType;
        if (destType == typeof (InstanceDescriptor))
        {
          if (!qshape.ConvertedFromString)
            return (object) new InstanceDescriptor((MemberInfo) typeof (QShape).GetConstructor(new Type[0]), (ICollection) new object[0], true);
          return (object) new InstanceDescriptor((MemberInfo) typeof (QShape).GetConstructor(new Type[1]
          {
            typeof (QBaseShapeType)
          }), (ICollection) new object[1]
          {
            (object) qshape.BaseShapeType
          }, true);
        }
      }
      return base.ConvertTo(context, culture, value, destType);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (value.GetType() == typeof (string))
      {
        try
        {
          string str = Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
          if (str != null && str.StartsWith("(") && str.EndsWith(")"))
            str = str.Substring(1, str.Length - 2);
          QBaseShapeType qbaseShapeType = (QBaseShapeType) Enum.Parse(typeof (QBaseShapeType), str, false);
          return (object) new QShape()
          {
            ConvertedFromString = true,
            BaseShapeType = qbaseShapeType,
            ClonedBaseShapeType = qbaseShapeType
          };
        }
        catch (ArgumentNullException ex)
        {
          return base.ConvertFrom(context, culture, value);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          return base.ConvertFrom(context, culture, value);
        }
        catch (ArgumentException ex)
        {
          return base.ConvertFrom(context, culture, value);
        }
      }
      else
      {
        if (value.GetType() != typeof (QBaseShapeType))
          return base.ConvertFrom(context, culture, value);
        return (object) new QShape()
        {
          BaseShapeType = QBaseShapeType.RectangleShape,
          ClonedBaseShapeType = QBaseShapeType.RectangleShape
        };
      }
    }

    private bool AllowShapeTypeAsStandardValue(QShapeType shapeType, AttributeCollection attributes)
    {
      bool flag = false;
      for (int index = 0; index < attributes.Count; ++index)
      {
        if (attributes[index] is QShapeDesignVisibleAttribute attribute)
        {
          flag = true;
          if (attribute.ShouldBeVisibleForShapeType(shapeType))
            return true;
        }
      }
      return !flag;
    }

    private bool AllowShapeAsStandardValue(QShape shape, AttributeCollection attributes)
    {
      if (shape == null)
        return false;
      bool flag = false;
      for (int index = 0; index < attributes.Count; ++index)
      {
        if (attributes[index] is QShapeDesignVisibleAttribute attribute)
        {
          flag = true;
          if (attribute.ShouldBeVisibleForShape(shape))
            return true;
        }
      }
      return !flag;
    }
  }
}
