// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeDesignVisibleAttribute
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public sealed class QShapeDesignVisibleAttribute : Attribute
  {
    private QShapeType m_eShapeType;

    public QShapeDesignVisibleAttribute(QShapeType shapeType) => this.m_eShapeType = shapeType;

    public QShapeType ShapeType => this.m_eShapeType;

    public bool ShouldBeVisibleForShape(QShape shape) => shape != null ? this.ShouldBeVisibleForShapeType(shape.ShapeType) : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (shape)));

    public bool ShouldBeVisibleForShapeType(QShapeType shapeType) => this.m_eShapeType == shapeType;
  }
}
