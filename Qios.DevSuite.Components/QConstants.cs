// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QConstants
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public sealed class QConstants
  {
    internal const int InternalStartTimerID = 16;
    internal const string PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8";
    private static QProductInfo m_oDevSuiteProductInfo;
    internal static readonly Guid DevSuiteProductGuid = new Guid("{064C4C20-C849-4ABF-85EF-B69092E75F3C}");

    private QConstants()
    {
    }

    public static QProductInfo DevSuiteProductInfo
    {
      get
      {
        if (QConstants.m_oDevSuiteProductInfo == null)
          QConstants.InitializeDevSuiteProductInfo();
        return QConstants.m_oDevSuiteProductInfo;
      }
    }

    private static void InitializeDevSuiteProductInfo()
    {
      QConstants.m_oDevSuiteProductInfo = new QProductInfo();
      QConstants.m_oDevSuiteProductInfo.ProductGuid = QConstants.DevSuiteProductGuid;
      QConstants.m_oDevSuiteProductInfo.ProductNamePublic = "Qios.DevSuite";
      QConstants.m_oDevSuiteProductInfo.ProductNameInternal = "Qios.DevSuite.Components";
      QConstants.m_oDevSuiteProductInfo.ProductAssembly = Assembly.GetAssembly(typeof (QConstants));
      QConstants.m_oDevSuiteProductInfo.ProductVersion = QConstants.m_oDevSuiteProductInfo.ProductAssembly.GetName().Version;
      QConstants.m_oDevSuiteProductInfo.ProductWebsiteTitle = "www.qiosdevsuite.com";
      QConstants.m_oDevSuiteProductInfo.ProductWebsiteUrl = "http://www.qiosdevsuite.com";
      QConstants.m_oDevSuiteProductInfo.ProductVersionInfoWebsiteTitle = "www.qiosdevsuite.com/versioninformation";
      QConstants.m_oDevSuiteProductInfo.ProductVersionInfoWebsiteUrl = "http://www.qiosdevsuite.com/versioninformation";
      QConstants.m_oDevSuiteProductInfo.ProductPrivateBuyNowUrl = "http://www.qiosdevsuite.com/?buynow=1&LicenseNumber={0}&AuthenticationCode={1}";
      QConstants.m_oDevSuiteProductInfo.ProductPublicBuyNowUrl = "http://www.qiosdevsuite.com/?buynow=1";
    }
  }
}
