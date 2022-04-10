// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorCategoryVisibleAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public sealed class QColorCategoryVisibleAttribute : Attribute
  {
    private QColorCategory m_eCategory;

    public QColorCategoryVisibleAttribute(QColorCategory category) => this.m_eCategory = category;

    public QColorCategory Category => this.m_eCategory;
  }
}
