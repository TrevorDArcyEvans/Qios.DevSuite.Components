// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QProductInfo
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QProductInfo
  {
    private string m_sProductNamePublic;
    private string m_sProductNameInternal;
    private Guid m_oProductGuid;
    private Version m_oProductVersion;
    private string m_sProductWebsiteUrl;
    private string m_sProductWebsiteTitle;
    private string m_sProductVersionInfoWebsiteUrl;
    private string m_sProductVersionInfoWebsiteTitle;
    private string m_sProductPublicBuyNowUrl;
    private string m_sProductPrivateBuyNowUrl;
    private Assembly m_oProductAssembly;

    public string ProductNamePublic
    {
      get => this.m_sProductNamePublic;
      set => this.m_sProductNamePublic = value;
    }

    public string ProductNameInternal
    {
      get => this.m_sProductNameInternal;
      set => this.m_sProductNameInternal = value;
    }

    public Guid ProductGuid
    {
      get => this.m_oProductGuid;
      set => this.m_oProductGuid = value;
    }

    public Version ProductVersion
    {
      get => this.m_oProductVersion;
      set => this.m_oProductVersion = value;
    }

    public string ProductWebsiteUrl
    {
      get => this.m_sProductWebsiteUrl;
      set => this.m_sProductWebsiteUrl = value;
    }

    public string ProductWebsiteTitle
    {
      get => this.m_sProductWebsiteTitle;
      set => this.m_sProductWebsiteTitle = value;
    }

    public string ProductVersionInfoWebsiteUrl
    {
      get => this.m_sProductVersionInfoWebsiteUrl;
      set => this.m_sProductVersionInfoWebsiteUrl = value;
    }

    public string ProductVersionInfoWebsiteTitle
    {
      get => this.m_sProductVersionInfoWebsiteTitle;
      set => this.m_sProductVersionInfoWebsiteTitle = value;
    }

    public string ProductPublicBuyNowUrl
    {
      get => this.m_sProductPublicBuyNowUrl;
      set => this.m_sProductPublicBuyNowUrl = value;
    }

    public string ProductPrivateBuyNowUrl
    {
      get => this.m_sProductPrivateBuyNowUrl;
      set => this.m_sProductPrivateBuyNowUrl = value;
    }

    public Assembly ProductAssembly
    {
      get => this.m_oProductAssembly;
      set => this.m_oProductAssembly = value;
    }
  }
}
