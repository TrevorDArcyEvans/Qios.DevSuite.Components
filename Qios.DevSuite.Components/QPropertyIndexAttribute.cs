// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPropertyIndexAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Property)]
  internal class QPropertyIndexAttribute : Attribute
  {
    private int m_iIndex;

    public QPropertyIndexAttribute(int index) => this.m_iIndex = index;

    public int Index => this.m_iIndex;

    public static int GetPropertyIndex(PropertyDescriptor property) => property.Attributes[typeof (QPropertyIndexAttribute)] is QPropertyIndexAttribute attribute ? attribute.Index : -1;

    public static int GetPropertyIndex(PropertyInfo propertyInfo)
    {
      object[] propertyAttributes = QMisc.GetCustomPropertyAttributes(propertyInfo, typeof (QPropertyIndexAttribute));
      return propertyAttributes != null && propertyAttributes.Length > 0 && propertyAttributes[0] is QPropertyIndexAttribute qpropertyIndexAttribute ? qpropertyIndexAttribute.Index : -1;
    }
  }
}
