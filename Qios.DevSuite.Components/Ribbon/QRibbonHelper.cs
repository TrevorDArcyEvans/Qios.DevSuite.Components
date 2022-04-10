// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  internal class QRibbonHelper
  {
    private QRibbonHelper()
    {
    }

    public static Font GetRibbonCaptionFont()
    {
      try
      {
        return QRibbonHelper.GetCalibriCaptionFont();
      }
      catch
      {
        return QRibbonHelper.GetVerdanaCaptionFont();
      }
    }

    private static Font GetCalibriCaptionFont()
    {
      Qios.DevSuite.Components.NativeMethods.NONCLIENTMETRICS nonClientMetrics = NativeHelper.GetNonClientMetrics();
      nonClientMetrics.lfCaptionFont.lfFaceName = "Calibri";
      nonClientMetrics.lfCaptionFont.lfWeight = 0;
      if (nonClientMetrics.lfCaptionFont.lfHeight > 0)
        ++nonClientMetrics.lfCaptionFont.lfHeight;
      else if (nonClientMetrics.lfCaptionFont.lfHeight < 0)
        --nonClientMetrics.lfCaptionFont.lfHeight;
      return Font.FromLogFont((object) nonClientMetrics.lfCaptionFont);
    }

    private static Font GetVerdanaCaptionFont()
    {
      Qios.DevSuite.Components.NativeMethods.NONCLIENTMETRICS nonClientMetrics = NativeHelper.GetNonClientMetrics();
      nonClientMetrics.lfCaptionFont.lfFaceName = "Verdana";
      nonClientMetrics.lfCaptionFont.lfWeight = 0;
      if (nonClientMetrics.lfCaptionFont.lfHeight > 0)
        --nonClientMetrics.lfCaptionFont.lfHeight;
      else if (nonClientMetrics.lfCaptionFont.lfHeight < 0)
        ++nonClientMetrics.lfCaptionFont.lfHeight;
      return Font.FromLogFont((object) nonClientMetrics.lfCaptionFont);
    }
  }
}
