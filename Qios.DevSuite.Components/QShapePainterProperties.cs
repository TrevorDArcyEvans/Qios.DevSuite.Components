// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapePainterProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing.Drawing2D;

namespace Qios.DevSuite.Components
{
  public class QShapePainterProperties
  {
    private static QShapePainterProperties m_oDefault;
    private Matrix m_oMatrix;
    private QShapePainterOptions m_eOptions;

    public QShapePainterProperties()
    {
    }

    internal QShapePainterProperties(Matrix matrix, QShapePainterOptions options)
    {
      this.m_oMatrix = matrix;
      this.m_eOptions = options;
    }

    public QShapePainterOptions Options
    {
      get => this.m_eOptions;
      set => this.m_eOptions = value;
    }

    public Matrix Matrix
    {
      get => this.m_oMatrix;
      set => this.m_oMatrix = value;
    }

    public static QShapePainterProperties Default
    {
      get
      {
        if (QShapePainterProperties.m_oDefault == null)
          QShapePainterProperties.m_oDefault = new QShapePainterProperties();
        return QShapePainterProperties.m_oDefault;
      }
    }
  }
}
