// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRoundedRectanglePainterProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QRoundedRectanglePainterProperties
  {
    private static QRoundedRectanglePainterProperties m_oDefault;
    private int m_iCornerSize = 6;
    private QDrawRoundedRectangleOptions m_eOptions = QDrawRoundedRectangleOptions.All;

    public QRoundedRectanglePainterProperties()
    {
    }

    internal QRoundedRectanglePainterProperties(
      QDrawRoundedRectangleOptions options,
      int cornerSize)
    {
      this.m_eOptions = options;
      this.m_iCornerSize = cornerSize;
    }

    public QDrawRoundedRectangleOptions Options
    {
      get => this.m_eOptions;
      set => this.m_eOptions = value;
    }

    public int CornerSize
    {
      get => this.m_iCornerSize;
      set => this.m_iCornerSize = value;
    }

    public static QRoundedRectanglePainterProperties Default
    {
      get
      {
        if (QRoundedRectanglePainterProperties.m_oDefault == null)
          QRoundedRectanglePainterProperties.m_oDefault = new QRoundedRectanglePainterProperties();
        return QRoundedRectanglePainterProperties.m_oDefault;
      }
    }
  }
}
