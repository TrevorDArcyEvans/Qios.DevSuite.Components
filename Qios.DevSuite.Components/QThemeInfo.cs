// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QThemeInfo
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QThemeInfoTypeConverter))]
  public class QThemeInfo
  {
    private string m_sThemeName;
    private string m_sFileName;
    private string m_sWindowsSchemeName;
    private bool m_bIsSystemTheme;

    public QThemeInfo()
    {
    }

    public QThemeInfo(string themeName)
      : this(themeName, (string) null, (string) null, false)
    {
    }

    public QThemeInfo(string themeName, string fileName, string windowsSchemeName)
      : this(themeName, fileName, windowsSchemeName, false)
    {
    }

    internal QThemeInfo(
      string themeName,
      string fileName,
      string windowsSchemeName,
      bool isSystemTheme)
    {
      this.m_sThemeName = themeName;
      this.m_sFileName = fileName;
      this.m_sWindowsSchemeName = windowsSchemeName;
      this.m_bIsSystemTheme = isSystemTheme;
    }

    public bool ShouldSerialize() => this.ShouldSerializeFileName() || this.ShouldSerializeThemeName() || this.ShouldSerializeWindowsSchemeName();

    public bool ShouldSerializeThemeName() => !this.m_bIsSystemTheme && this.m_sThemeName != null;

    public void ResetThemeName()
    {
      if (this.m_bIsSystemTheme)
        return;
      this.m_sThemeName = (string) null;
    }

    [Category("QBehavior")]
    [Description("Gets or sets the ThemeName. This name is used to match it with a colorscheme")]
    public string ThemeName
    {
      get => this.m_sThemeName;
      set
      {
        this.AssertSystemTheme(nameof (ThemeName));
        this.m_sThemeName = value;
      }
    }

    public bool ShouldSerializeFileName() => !this.m_bIsSystemTheme && this.m_sFileName != null;

    public void ResetFileName()
    {
      if (this.m_bIsSystemTheme)
        return;
      this.m_sFileName = (string) null;
    }

    [Description("Gets or sets the FileName")]
    [Category("QBehavior")]
    public string FileName
    {
      get => this.m_sFileName;
      set
      {
        this.AssertSystemTheme(nameof (FileName));
        this.m_sFileName = value;
      }
    }

    public bool ShouldSerializeWindowsSchemeName() => !this.m_bIsSystemTheme && this.m_sWindowsSchemeName != null;

    public void ResetWindowsSchemeName()
    {
      if (this.m_bIsSystemTheme)
        return;
      this.m_sWindowsSchemeName = (string) null;
    }

    [Description("Gets or sets the WindowsSchemeName")]
    [Category("QBehavior")]
    public string WindowsSchemeName
    {
      get => this.m_sWindowsSchemeName;
      set
      {
        this.AssertSystemTheme(nameof (WindowsSchemeName));
        this.m_sWindowsSchemeName = value;
      }
    }

    [Description("Indicates whether this theme is a system theme.")]
    [Category("QBehavior")]
    public bool IsSystemTheme => this.m_bIsSystemTheme;

    public virtual void SaveToXml(IXPathNavigable collectionElement)
    {
      IXPathNavigable xpathNavigable = QXmlHelper.AddElement(collectionElement, "theme");
      Type type = this.GetType();
      string str = type.FullName + ", " + type.Assembly.GetName().Name;
      QXmlHelper.AddAttribute(xpathNavigable, "type", (object) str);
      QXmlHelper.SaveObjectToXml(xpathNavigable, (object) this, (PropertyDescriptorCollection) null);
      this.SavePropertiesToXml(xpathNavigable);
    }

    public virtual void LoadFromXml(IXPathNavigable itemElement) => QXmlHelper.LoadObjectFromXmlElement(itemElement, (object) this, (PropertyDescriptorCollection) null);

    internal virtual void SavePropertiesToXml(IXPathNavigable element)
    {
    }

    internal void AssertSystemTheme(string property)
    {
      if (this.m_bIsSystemTheme)
        throw new InvalidOperationException(QResources.GetException("QThemeInfo_CannotChangeSystemTheme", (object) property));
    }

    internal QThemeInfo CloneTheme() => new QThemeInfo()
    {
      WindowsSchemeName = this.m_sWindowsSchemeName,
      ThemeName = this.m_sThemeName,
      FileName = this.m_sFileName,
      m_bIsSystemTheme = this.m_bIsSystemTheme
    };
  }
}
