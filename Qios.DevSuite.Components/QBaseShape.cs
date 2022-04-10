// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QBaseShape
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QShapeDesigner), typeof (IDesigner))]
  internal class QBaseShape : QShape
  {
    private QBaseShapeType m_eBaseShapeType;

    internal QBaseShape(
      string shapeName,
      QBaseShapeType baseShapeType,
      Size size,
      Rectangle contentBounds)
      : base(true)
    {
      this.ShapeName = shapeName;
      this.Size = size;
      this.ContentBounds = contentBounds;
      this.BaseShapeType = baseShapeType;
    }

    public override void CopyTo(QShape shape, bool includeItems)
    {
      base.CopyTo(shape, includeItems);
      shape.SetBaseShape((QShape) this, false, false);
    }

    public override QBaseShapeType BaseShapeType
    {
      get => this.m_eBaseShapeType;
      set => this.m_eBaseShapeType = value;
    }

    internal void SetShapeType(QShapeType type) => this.ShapeType = type;
  }
}
