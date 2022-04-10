// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMisc
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Qios.DevSuite.Components
{
  internal class QMisc
  {
    private QMisc()
    {
    }

    public static long TickCount => DateTime.Now.Ticks / 10000L;

    public static ValueType GetDefaultValueAsValueType(
      object instance,
      string propertyName)
    {
      return (ValueType) QMisc.GetDefaultValue(instance, propertyName);
    }

    public static object[] GetCustomPropertyAttributes(
      PropertyInfo propertyInfo,
      System.Type attributeType)
    {
      System.Type baseType;
      for (object[] propertyAttributes = (object[]) null; propertyInfo != null && (propertyAttributes == null || propertyAttributes.Length == 0); propertyInfo = baseType == null ? (PropertyInfo) null : baseType.GetProperty(propertyInfo.Name))
      {
        propertyAttributes = propertyInfo.GetCustomAttributes(attributeType, true);
        if (propertyAttributes != null && propertyAttributes.Length != 0)
          return propertyAttributes;
        baseType = propertyInfo.DeclaringType.BaseType;
      }
      return (object[]) null;
    }

    public static object GetDefaultValue(object instance, string propertyName)
    {
      if (instance == null)
        return (object) null;
      switch (propertyName)
      {
        case "":
        case null:
          return (object) null;
        default:
          object[] propertyAttributes = QMisc.GetCustomPropertyAttributes(instance.GetType().GetProperty(propertyName), typeof (DefaultValueAttribute));
          return propertyAttributes != null && propertyAttributes.Length > 0 ? ((DefaultValueAttribute) propertyAttributes[0]).Value : (object) null;
      }
    }

    public static void SetToDefaultValues(object instance)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
      for (int index = 0; index < properties.Count; ++index)
      {
        try
        {
          if (properties[index].CanResetValue(instance))
            properties[index].ResetValue(instance);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(QResources.GetException("QMisc_CannotSetDefaultValues", (object) properties[index].ComponentType.ToString(), (object) properties[index].Name, (object) ex.Message));
        }
      }
    }

    public static bool IsSetToDefaultValues(object instance)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
      for (int index = 0; index < properties.Count; ++index)
      {
        if (properties[index].ShouldSerializeValue(instance))
          return false;
      }
      return true;
    }

    public static object AssertObjectOfType(object value, string objectName, System.Type type) => value != null && (value.GetType() == type || value.GetType().IsSubclassOf(type)) ? value : throw new InvalidOperationException(objectName + " does not inherit from " + type.Name + " or is null.");

    internal static PropertyGrid FindPropertyGrid(Control control)
    {
      if (control is PropertyGrid propertyGrid1)
        return propertyGrid1;
      foreach (Control control1 in (ArrangedElementCollection) control.Controls)
      {
        PropertyGrid propertyGrid2 = QMisc.FindPropertyGrid(control1);
        if (propertyGrid2 != null)
          return propertyGrid2;
      }
      return (PropertyGrid) null;
    }

    public static ValueType PtrToValueType(IntPtr pointer, System.Type valueType) => (ValueType) Marshal.PtrToStructure(pointer, valueType);

    public static Enum ParseEnumeration(System.Type enumType, string stringValue, bool ignoreCase) => (Enum) Enum.Parse(enumType, stringValue, ignoreCase);

    public static bool IsEmpty(object value) => value == null || value.ToString().Length == 0;

    public static void CleanupList(IList list)
    {
      int num = 0;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index] == null)
          ++num;
        else if (num > 0)
          list[index - num] = list[index];
      }
      if (num < list.Count)
      {
        for (; num > 0; --num)
          list.RemoveAt(list.Count - 1);
      }
      else
        list.Clear();
    }

    public static NativeWindow GetNotifyIconWindow(NotifyIcon notifyIcon) => typeof (NotifyIcon).GetField("window", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) notifyIcon) as NativeWindow;

    public static StringFormat ConfigureStringFormat(
      ContentAlignment alignment,
      StringFormat format)
    {
      switch (alignment)
      {
        case ContentAlignment.TopLeft:
          format.LineAlignment = StringAlignment.Near;
          format.Alignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopCenter:
          format.LineAlignment = StringAlignment.Near;
          format.Alignment = StringAlignment.Center;
          break;
        case ContentAlignment.TopRight:
          format.LineAlignment = StringAlignment.Near;
          format.Alignment = StringAlignment.Far;
          break;
        case ContentAlignment.MiddleLeft:
          format.LineAlignment = StringAlignment.Center;
          format.Alignment = StringAlignment.Near;
          break;
        case ContentAlignment.MiddleCenter:
          format.LineAlignment = StringAlignment.Center;
          format.Alignment = StringAlignment.Center;
          break;
        case ContentAlignment.MiddleRight:
          format.LineAlignment = StringAlignment.Center;
          format.Alignment = StringAlignment.Far;
          break;
        case ContentAlignment.BottomLeft:
          format.LineAlignment = StringAlignment.Far;
          format.Alignment = StringAlignment.Near;
          break;
        case ContentAlignment.BottomCenter:
          format.LineAlignment = StringAlignment.Far;
          format.Alignment = StringAlignment.Center;
          break;
        case ContentAlignment.BottomRight:
          format.LineAlignment = StringAlignment.Far;
          format.Alignment = StringAlignment.Far;
          break;
      }
      return format;
    }

    public static SmoothingMode GetSmoothingMode(QSmoothingMode smoothingMode)
    {
      switch (smoothingMode)
      {
        case QSmoothingMode.None:
          return SmoothingMode.Default;
        case QSmoothingMode.AntiAlias:
          return SmoothingMode.AntiAlias;
        default:
          return SmoothingMode.Default;
      }
    }

    public static ValueType AsValueType(object value) => (ValueType) value;

    public static object GetViaTypeConverter(object value, System.Type destinationType)
    {
      if (value == null)
        return (object) null;
      System.Type type = value.GetType();
      if (type == destinationType)
        return value;
      TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
      return converter.CanConvertFrom(type) ? converter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, value) : (object) null;
    }

    public static System.Type[] CombineTypeLists(params System.Type[][] typeLists)
    {
      int length = 0;
      for (int index = 0; index < typeLists.Length; ++index)
        length += typeLists[index].Length;
      System.Type[] destinationArray = new System.Type[length];
      int destinationIndex = 0;
      for (int index = 0; index < typeLists.Length; ++index)
      {
        Array.Copy((Array) typeLists[index], 0, (Array) destinationArray, destinationIndex, typeLists[index].Length);
        destinationIndex += typeLists[index].Length;
      }
      return destinationArray;
    }

    public static string GetAsString(object value, string defaultValue)
    {
      if (QMisc.IsEmpty(value))
        return defaultValue;
      TypeConverter converter = TypeDescriptor.GetConverter(value);
      if (converter != null)
      {
        if (converter.CanConvertTo(typeof (string)))
          return converter.ConvertToInvariantString(value);
      }
      try
      {
        return Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (InvalidCastException ex)
      {
        return defaultValue;
      }
    }

    public static string GetAsString(object value) => QMisc.GetAsString(value, string.Empty);

    public static DateTime GetAsDateTime(object value, DateTime defaultValue)
    {
      if (QMisc.IsEmpty(value))
        return defaultValue;
      try
      {
        return Convert.ToDateTime(value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (InvalidCastException ex)
      {
        return defaultValue;
      }
    }

    public static DateTime GetAsDateTime(object value) => QMisc.GetAsDateTime(value, DateTime.MinValue);

    public static Size GetAsSize(object value, Size defaultValue)
    {
      object viaTypeConverter;
      return (viaTypeConverter = QMisc.GetViaTypeConverter(value, typeof (Size))) == null ? defaultValue : (Size) viaTypeConverter;
    }

    public static Size GetAsSize(object value) => QMisc.GetAsSize(value, Size.Empty);

    public static Rectangle GetAsRectangle(object value, Rectangle defaultValue)
    {
      object viaTypeConverter;
      return (viaTypeConverter = QMisc.GetViaTypeConverter(value, typeof (Rectangle))) == null ? defaultValue : (Rectangle) viaTypeConverter;
    }

    public static Rectangle GetAsRectangle(object value) => QMisc.GetAsRectangle(value, Rectangle.Empty);

    public static Color GetAsColor(string color, QColorScheme colorScheme) => QMisc.GetAsColor(color, colorScheme, Color.Empty);

    public static Color GetAsColor(string color, QColorScheme colorScheme, Color defaultValue)
    {
      if (QMisc.IsEmpty((object) color))
        return defaultValue;
      QColor qcolor = (QColor) null;
      if (colorScheme.IsValidColor(color))
        qcolor = colorScheme[color];
      if (qcolor != null)
        return qcolor.Current;
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (Color));
      try
      {
        return (Color) converter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, color);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static int GetAsInt(object value, int defaultValue)
    {
      if (QMisc.IsEmpty(value))
        return defaultValue;
      try
      {
        return Convert.ToInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (InvalidCastException ex)
      {
        return defaultValue;
      }
    }

    public static int GetAsInt(object value) => QMisc.GetAsInt(value, 0);

    public static double GetAsDouble(object value, double defaultValue)
    {
      if (QMisc.IsEmpty(value))
        return 0.0;
      try
      {
        return Convert.ToDouble(value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (InvalidCastException ex)
      {
        return defaultValue;
      }
    }

    public static double GetAsDouble(object value) => QMisc.GetAsDouble(value, 0.0);

    public static bool GetTristateAsBool(QTristateBool value, bool undefinedValue)
    {
      if (value == QTristateBool.True)
        return true;
      return value != QTristateBool.False && undefinedValue;
    }

    public static bool GetAsBool(object value, bool defaultValue)
    {
      if (QMisc.IsEmpty(value))
        return defaultValue;
      try
      {
        return Convert.ToBoolean(value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch (InvalidCastException ex)
      {
        return defaultValue;
      }
    }

    public static bool GetAsBool(object value) => QMisc.GetAsBool(value, false);

    public static Guid GetAsGuid(object value, Guid defaultValue)
    {
      if (QMisc.IsEmpty(value))
        return defaultValue;
      if (value is Guid asGuid)
        return asGuid;
      try
      {
        return new Guid(value.ToString());
      }
      catch (FormatException ex)
      {
        return defaultValue;
      }
    }

    public static Guid GetAsGuid(object value) => QMisc.GetAsGuid(value, Guid.Empty);
  }
}
