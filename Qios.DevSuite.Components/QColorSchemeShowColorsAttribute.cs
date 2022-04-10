// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorSchemeShowColorsAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
  public sealed class QColorSchemeShowColorsAttribute : Attribute
  {
    private QColorCategory m_eCategory;
    private bool m_bRelated;
    private QColorSchemeShowColorsMethod m_eMethod;

    public QColorSchemeShowColorsAttribute(QColorSchemeShowColorsMethod method) => this.m_eMethod = method;

    public QColorSchemeShowColorsAttribute(
      QColorSchemeShowColorsMethod method,
      QColorCategory category)
    {
      this.m_eMethod = method;
      this.m_eCategory = category;
    }

    public QColorSchemeShowColorsAttribute(
      QColorSchemeShowColorsMethod method,
      QColorCategory category,
      bool related)
    {
      this.m_bRelated = related;
      this.m_eMethod = method;
      this.m_eCategory = category;
    }

    public void ShowColor(object[] categories, QColorSchemeScope scope, ref bool visible)
    {
      if (categories == null)
        return;
      if (scope == QColorSchemeScope.All)
        visible = true;
      else if (this.m_eMethod == QColorSchemeShowColorsMethod.Clear)
      {
        visible = false;
      }
      else
      {
        for (int index = 0; index < categories.Length; ++index)
        {
          QColorCategoryVisibleAttribute category = categories[index] as QColorCategoryVisibleAttribute;
          switch (this.m_eMethod)
          {
            case QColorSchemeShowColorsMethod.Add:
              if (category.Category == this.m_eCategory && (scope == QColorSchemeScope.ControlAndRelated || !this.m_bRelated))
              {
                visible = true;
                break;
              }
              break;
            case QColorSchemeShowColorsMethod.Remove:
              if (category.Category == this.m_eCategory)
              {
                visible = false;
                break;
              }
              break;
          }
        }
      }
    }

    public QColorSchemeShowColorsMethod Method => this.m_eMethod;

    public bool Related => this.m_bRelated;

    public QColorCategory Category => this.m_eCategory;
  }
}
