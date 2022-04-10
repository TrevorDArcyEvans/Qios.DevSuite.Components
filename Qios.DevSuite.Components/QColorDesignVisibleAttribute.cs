// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorDesignVisibleAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public sealed class QColorDesignVisibleAttribute : Attribute
  {
    private bool m_bRelated;
    private QColorDesignVisibilityType m_eType;
    private Type m_oControlType;
    private QColorDesignVisibilityInheritanceType m_eInheritanceType;

    public QColorDesignVisibleAttribute(QColorDesignVisibilityType visibilityType) => this.m_eType = visibilityType;

    public QColorDesignVisibleAttribute(QColorDesignVisibilityType visibilityType, Type controlType)
    {
      this.m_eType = visibilityType;
      this.m_oControlType = controlType;
    }

    public QColorDesignVisibleAttribute(
      QColorDesignVisibilityType visibilityType,
      Type controlType,
      QColorDesignVisibilityInheritanceType inheritanceType)
    {
      this.m_eType = visibilityType;
      this.m_oControlType = controlType;
      this.m_eInheritanceType = inheritanceType;
    }

    public QColorDesignVisibleAttribute(
      QColorDesignVisibilityType visibilityType,
      Type controlType,
      bool related)
    {
      this.m_eType = visibilityType;
      this.m_oControlType = controlType;
      this.m_bRelated = related;
    }

    public QColorDesignVisibleAttribute(
      QColorDesignVisibilityType visibilityType,
      Type controlType,
      QColorDesignVisibilityInheritanceType inheritanceType,
      bool related)
    {
      this.m_eType = visibilityType;
      this.m_oControlType = controlType;
      this.m_eInheritanceType = inheritanceType;
      this.m_bRelated = related;
    }

    public QColorDesignVisibilityType VisibilityType => this.m_eType;

    public Type ControlType => this.m_oControlType;

    public QColorDesignVisibilityInheritanceType InheritanceType => this.m_eInheritanceType;

    public bool Related => this.m_bRelated;

    public bool ShouldBeVisibleForType(Type type) => this.ShouldBeVisibleForType(type, QColorSchemeScope.Control);

    public bool ShouldBeHiddenForType(Type type)
    {
      if (type == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (type)));
      if (this.m_eType != QColorDesignVisibilityType.Hide)
        return false;
      if (this.m_oControlType == type)
        return true;
      if (this.m_eInheritanceType == QColorDesignVisibilityInheritanceType.All)
        return type.IsSubclassOf(this.m_oControlType);
      return this.m_eInheritanceType == QColorDesignVisibilityInheritanceType.ExternalOnly && type.IsSubclassOf(this.m_oControlType) && type.Assembly != this.m_oControlType.Assembly;
    }

    public bool ShouldBeVisibleForType(Type type, QColorSchemeScope scope)
    {
      if (type == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (type)));
      if (this.m_eType == QColorDesignVisibilityType.None)
        return false;
      if (this.m_eType == QColorDesignVisibilityType.All)
        return scope != QColorSchemeScope.Control || !this.m_bRelated;
      bool flag = false;
      if (this.m_oControlType == type)
        flag = true;
      else if (this.m_eInheritanceType == QColorDesignVisibilityInheritanceType.All)
        flag = type.IsSubclassOf(this.m_oControlType);
      else if (this.m_eInheritanceType == QColorDesignVisibilityInheritanceType.ExternalOnly)
        flag = type.IsSubclassOf(this.m_oControlType) && type.Assembly != this.m_oControlType.Assembly;
      if (this.m_eType == QColorDesignVisibilityType.AllExcept)
        return !flag;
      return this.m_eType == QColorDesignVisibilityType.NoneExcept && (scope != QColorSchemeScope.Control || !this.m_bRelated) && flag;
    }

    internal static bool ShouldBeVisibleForType(object[] attributes, Type controlType) => QColorDesignVisibleAttribute.ShouldBeVisibleForType(attributes, controlType, QColorSchemeScope.Control);

    internal static bool ShouldBeVisibleForType(
      object[] attributes,
      Type controlType,
      QColorSchemeScope scope)
    {
      if (attributes != null)
      {
        for (int index = 0; index < attributes.Length; ++index)
        {
          if (attributes[index] is QColorDesignVisibleAttribute && ((QColorDesignVisibleAttribute) attributes[index]).ShouldBeHiddenForType(controlType))
            return false;
        }
        for (int index = 0; index < attributes.Length; ++index)
        {
          if (attributes[index] is QColorDesignVisibleAttribute && ((QColorDesignVisibleAttribute) attributes[index]).ShouldBeVisibleForType(controlType, scope))
            return true;
        }
      }
      return false;
    }
  }
}
