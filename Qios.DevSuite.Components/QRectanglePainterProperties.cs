// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QRectanglePainterProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QRectanglePainterProperties
  {
    private static QRectanglePainterProperties m_oDefault;

    public static QRectanglePainterProperties Default
    {
      get
      {
        if (QRectanglePainterProperties.m_oDefault == null)
          QRectanglePainterProperties.m_oDefault = new QRectanglePainterProperties();
        return QRectanglePainterProperties.m_oDefault;
      }
    }
  }
}
