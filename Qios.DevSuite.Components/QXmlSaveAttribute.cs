// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QXmlSaveAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class QXmlSaveAttribute : Attribute
  {
    private QXmlSaveType m_eSaveType;

    public QXmlSaveAttribute(QXmlSaveType saveType) => this.m_eSaveType = saveType;

    public QXmlSaveType SaveType => this.m_eSaveType;

    public static bool ShouldSaveProperty(PropertyDescriptor property, object component)
    {
      if (property == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (property)));
      return !(property.Attributes[typeof (QXmlSaveAttribute)] is QXmlSaveAttribute attribute) || attribute.SaveType == QXmlSaveType.DependsOnDefaultValue ? property.ShouldSerializeValue(component) : attribute.SaveType == QXmlSaveType.AlwaysSave;
    }
  }
}
