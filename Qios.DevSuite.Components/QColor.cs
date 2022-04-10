// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QColorTypeConverter))]
  [Editor(typeof (QColorEditor), typeof (UITypeEditor))]
  [DesignerSerializer(typeof (QColorCodeSerializer), typeof (CodeDomSerializer))]
  public class QColor
  {
    public const string ColorReferenceSymbol = "@";
    public static char[] ColorReferenceSymbolCharArray = "@".ToCharArray();
    private string m_sColorName;
    private bool m_bIsEmpty;
    private Hashtable m_aThemeColors;
    private Hashtable m_aDefaultThemeColors;
    private string m_sColorReference;
    private string m_sDefaultColorReference;
    private QColorSchemeBase m_oColorScheme;
    private static QColor m_oEmpty = new QColor(true);

    internal QColor(bool empty) => this.m_bIsEmpty = empty;

    internal QColor(QColorSchemeBase colorScheme, string colorName)
    {
      this.m_oColorScheme = colorScheme;
      this.m_sColorName = colorName;
    }

    internal QColor(QColorSchemeBase colorScheme, string colorName, string defaultColorReference)
    {
      this.m_oColorScheme = colorScheme;
      this.m_sColorName = colorName;
      this.m_sDefaultColorReference = defaultColorReference.TrimStart(QColor.ColorReferenceSymbolCharArray);
    }

    internal QColor(
      QColorSchemeBase colorScheme,
      string colorName,
      params Color[] defaultThemeColors)
    {
      this.m_oColorScheme = colorScheme;
      this.m_sColorName = colorName;
      if (defaultThemeColors == null)
        return;
      this.SecureDefaultThemeColors();
      for (int index = 0; index < defaultThemeColors.Length; ++index)
        this.m_aDefaultThemeColors.Add((object) this.m_oColorScheme.Themes[index].ThemeName, (object) defaultThemeColors[index]);
    }

    public static QColor Empty => QColor.m_oEmpty;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QXmlSave(QXmlSaveType.NeverSave)]
    public Color Current
    {
      get => !this.m_bIsEmpty ? this.GetColor(this.m_oColorScheme.CurrentTheme) : Color.Empty;
      set
      {
        if (this.m_bIsEmpty)
          throw new InvalidOperationException(QResources.GetException("QColor_Current_NotEmpty"));
        this.SetColor(this.m_oColorScheme.CurrentTheme, value);
      }
    }

    public Color this[string theme]
    {
      get => this.GetColor(theme);
      set => this.SetColor(theme, value);
    }

    public bool IsEmpty => this.m_bIsEmpty;

    public static implicit operator Color(QColor colorValue) => QColor.ToColor(colorValue);

    public static Color ToColor(QColor colorValue) => colorValue != null ? colorValue.Current : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (colorValue)));

    public QColorSchemeBase ColorScheme => this.m_oColorScheme;

    public string ColorName => this.m_sColorName;

    public bool ShouldSerializeColorReference() => this.m_sColorReference != null && this.m_sColorReference.Length > 0 && string.Compare(this.ColorReference, this.DefaultColorReference, true, CultureInfo.InvariantCulture) != 0;

    public void ResetColorReference() => this.m_sColorReference = (string) null;

    [QXmlSave(QXmlSaveType.NeverSave)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a reference to an other color in the QColorScheme, for example: @PanelBackground1")]
    public string ColorReference
    {
      get => this.m_sColorReference != null && this.m_sColorReference.Length > 0 ? "@" + this.m_sColorReference : this.m_sColorReference;
      set => this.SetColor(value);
    }

    [Browsable(false)]
    public string DefaultColorReference
    {
      get
      {
        if (this.ColorScheme.BaseColorScheme != null)
          return this.ColorScheme.BaseColorScheme.GetColor(this.ColorName).UsedColorReference;
        return this.m_sDefaultColorReference != null && this.m_sDefaultColorReference.Length > 0 ? "@" + this.m_sDefaultColorReference : this.m_sDefaultColorReference;
      }
    }

    [Browsable(false)]
    public string UsedColorReference
    {
      get
      {
        string referenceInternal = this.UsedColorReferenceInternal;
        return referenceInternal != null && referenceInternal.Length > 0 ? "@" + referenceInternal : (string) null;
      }
    }

    private string UsedColorReferenceInternal
    {
      get
      {
        if (this.m_aThemeColors != null && this.m_aThemeColors.Count > 0)
          return (string) null;
        if (this.m_sColorReference != null && this.m_sColorReference.Length > 0)
          return this.m_sColorReference;
        if (this.ColorScheme.BaseColorScheme != null)
          return this.ColorScheme.BaseColorScheme[this.ColorName].UsedColorReferenceInternal;
        return this.m_sDefaultColorReference != null && this.m_sDefaultColorReference.Length > 0 ? this.m_sDefaultColorReference : (string) null;
      }
    }

    [Browsable(false)]
    public int ThemeColorCount => this.m_aThemeColors == null ? 0 : this.m_aThemeColors.Count;

    public bool ContainsColorForTheme(string theme) => !this.m_bIsEmpty && this.m_aThemeColors != null && this.m_aThemeColors.Contains((object) theme);

    public bool ContainsColorForCurrentTheme() => this.ContainsColorForTheme(this.m_oColorScheme.CurrentTheme);

    public bool ContainsDefaultColorForTheme(string theme) => !this.m_bIsEmpty && this.m_aDefaultThemeColors != null && this.m_aDefaultThemeColors.Contains((object) theme);

    public virtual void SaveToXml(IXPathNavigable collectionElement, QColorSaveType saveOptions)
    {
      IXPathNavigable xpathNavigable = QXmlHelper.AddElement(collectionElement, "color");
      Type type = this.GetType();
      string str = type.FullName + ", " + type.Assembly.GetName().Name;
      QXmlHelper.AddAttribute(xpathNavigable, "type", (object) str);
      QXmlHelper.AddAttribute(xpathNavigable, "name", (object) this.ColorName);
      QXmlHelper.SaveObjectToXml(xpathNavigable, (object) this, (PropertyDescriptorCollection) null);
      this.SavePropertiesToXml(xpathNavigable, saveOptions);
    }

    public virtual void LoadFromXml(IXPathNavigable itemElement)
    {
      QXmlHelper.LoadObjectFromXmlElement(itemElement, (object) this, (PropertyDescriptorCollection) null);
      this.LoadPropertiesFromXml(itemElement);
    }

    internal virtual void LoadPropertiesFromXml(IXPathNavigable element)
    {
      string attributeString = QXmlHelper.GetAttributeString(element, "colorReference", (string) null);
      if (attributeString != null && attributeString.Length > 0)
      {
        this.SetColor(attributeString);
      }
      else
      {
        for (int index = 0; index < this.m_oColorScheme.Themes.Count; ++index)
        {
          string themeName = this.m_oColorScheme.Themes[index].ThemeName;
          if (themeName != null)
          {
            string str = XmlConvert.EncodeName(themeName);
            if (QXmlHelper.ContainsChildElement(element, str))
            {
              object obj = QMisc.GetViaTypeConverter((object) QXmlHelper.GetChildElementString(element, str), typeof (Color)) ?? Convert.ChangeType(obj, typeof (Color), (IFormatProvider) CultureInfo.InvariantCulture);
              try
              {
                if (obj is Color color)
                  this.SetColor(themeName, color);
              }
              catch (Exception ex)
              {
                throw new InvalidOperationException(QResources.GetException("QXmlHelper_InitializeObjectFromXmlSetThrewException", (object) typeof (QColor).Name, (object) themeName, obj), ex);
              }
            }
          }
        }
      }
    }

    internal virtual void SavePropertiesToXml(IXPathNavigable element, QColorSaveType saveOptions)
    {
      if (this.m_sColorReference != null && this.m_sColorReference.Length > 0)
      {
        QXmlHelper.AddAttribute(element, "colorReference", (object) this.ColorReference);
      }
      else
      {
        for (int index = 0; index < this.m_oColorScheme.Themes.Count; ++index)
        {
          if (this.m_oColorScheme.Themes[index].ThemeName != null && (saveOptions != QColorSaveType.ChangedThemeColors || this.ShouldSerializeColor(this.m_oColorScheme.Themes[index].ThemeName)))
            QXmlHelper.AddElement(element, XmlConvert.EncodeName(this.m_oColorScheme.Themes[index].ThemeName), (object) this.GetColor(this.m_oColorScheme.Themes[index].ThemeName));
        }
      }
    }

    internal Color GetDefaultColor(string theme)
    {
      if (this.m_bIsEmpty)
        return Color.Empty;
      string referenceInternal = this.UsedColorReferenceInternal;
      if (referenceInternal != null && referenceInternal.Length > 0)
        return this.m_oColorScheme.GetColor(referenceInternal).GetColor(theme);
      if (this.m_oColorScheme.BaseColorScheme != null)
        return this.m_oColorScheme.BaseColorScheme.GetColor(this.m_sColorName).GetColor(theme);
      if (this.m_sDefaultColorReference != null && this.m_sDefaultColorReference.Length > 0)
        return this.ColorScheme[this.m_sDefaultColorReference].GetColor(theme);
      return this.ContainsDefaultColorForTheme(theme) ? (Color) QMisc.AsValueType(this.m_aDefaultThemeColors[(object) theme]) : this.GetDefaultColor("Default");
    }

    internal bool ShouldSerialize()
    {
      if (this.m_bIsEmpty)
        return false;
      if (this.ShouldSerializeColorReference())
        return true;
      if (this.m_aThemeColors != null)
      {
        foreach (string key in (IEnumerable) this.m_aThemeColors.Keys)
        {
          if (this.ShouldSerializeColor(key))
            return true;
        }
      }
      return false;
    }

    internal void Reset()
    {
      if (this.m_bIsEmpty)
        return;
      bool flag = false;
      if (this.m_sColorReference != null)
      {
        this.ResetColorReference();
        flag = true;
      }
      if (this.m_aThemeColors != null)
      {
        this.m_aThemeColors.Clear();
        flag = true;
      }
      if (!flag)
        return;
      this.m_oColorScheme.RaiseColorsChanged();
    }

    internal bool ShouldSerializeColor(string theme) => !this.m_bIsEmpty && (this.m_sColorReference == null || this.m_sColorReference.Length <= 0) && this.GetColor(theme) != this.GetDefaultColor(theme);

    internal void ResetColor(string theme)
    {
      if (this.m_bIsEmpty || this.m_aThemeColors == null || !this.m_aThemeColors.Contains((object) theme))
        return;
      this.m_aThemeColors.Remove((object) theme);
      this.m_oColorScheme.RaiseColorsChanged();
    }

    private void SecureThemeColors()
    {
      if (this.m_aThemeColors != null)
        return;
      this.m_aThemeColors = new Hashtable((IEqualityComparer) StringComparer.InvariantCultureIgnoreCase);
    }

    private void SecureDefaultThemeColors()
    {
      if (this.m_aDefaultThemeColors != null)
        return;
      this.m_aDefaultThemeColors = new Hashtable();
    }

    public void SetColor(string theme, Color color) => this.SetColor(theme, color, true);

    public void SetColor(string theme, Color color, bool checkIfThemeExists)
    {
      if (this.m_bIsEmpty)
        throw new InvalidOperationException(QResources.GetException("QColor_SetColor_NotEmpty"));
      if (checkIfThemeExists && !this.m_oColorScheme.Themes.Contains(theme))
        throw new InvalidOperationException(QResources.GetException("QColor_ThemeNotDefined", (object) this.m_sColorName, (object) theme));
      this.m_sColorReference = (string) null;
      Color defaultColor = this.GetDefaultColor(theme);
      if (defaultColor == color)
      {
        if (!this.ContainsColorForTheme(theme))
          return;
        this.m_aThemeColors.Remove((object) theme);
        if (!(theme == this.ColorScheme.CurrentTheme))
          return;
        this.m_oColorScheme.RaiseColorsChanged();
      }
      else
      {
        if (!(defaultColor != color))
          return;
        if (this.ContainsColorForTheme(theme))
        {
          this.m_aThemeColors[(object) theme] = (object) color;
        }
        else
        {
          this.SecureThemeColors();
          this.m_aThemeColors.Add((object) theme, (object) color);
        }
        if (!(theme == this.ColorScheme.CurrentTheme))
          return;
        this.m_oColorScheme.RaiseColorsChanged();
      }
    }

    private bool CheckCircularColorReference(string colorReference, bool throwException)
    {
      bool flag = false;
      QColor qcolor;
      string referenceInternal;
      if (string.Compare(this.ColorName, colorReference, false, CultureInfo.InvariantCulture) == 0)
      {
        qcolor = this;
        flag = true;
      }
      else
      {
        for (qcolor = this.ColorScheme[colorReference]; qcolor != null; qcolor = referenceInternal == null || referenceInternal.Length <= 0 ? (QColor) null : this.ColorScheme[referenceInternal])
        {
          if (string.Compare(qcolor.ColorName, this.ColorName, false, CultureInfo.InvariantCulture) != 0)
          {
            referenceInternal = qcolor.UsedColorReferenceInternal;
          }
          else
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag || qcolor == null)
        return true;
      if (throwException)
        throw new InvalidOperationException(QResources.GetException("QColor_CircularColorReference", (object) this.ColorName, (object) ("@" + colorReference)));
      return false;
    }

    public void SetColor(string colorReference)
    {
      if (string.Compare(colorReference, this.DefaultColorReference, true, CultureInfo.InvariantCulture) == 0)
      {
        this.m_sColorReference = (string) null;
        this.m_aThemeColors = (Hashtable) null;
      }
      else if (colorReference != null && colorReference.Length > 0)
      {
        string str = colorReference.TrimStart(QColor.ColorReferenceSymbolCharArray);
        if (this.ColorScheme.IsValidColor(str))
        {
          this.CheckCircularColorReference(str, true);
          this.m_sColorReference = str;
          this.m_aThemeColors = (Hashtable) null;
        }
        else
          throw new InvalidOperationException(QResources.GetException("QColor_ReferenceNotFound", (object) colorReference));
      }
      else
        this.m_sColorReference = colorReference;
      this.m_oColorScheme.RaiseColorsChanged();
    }

    public Color GetColor(string theme)
    {
      if (this.m_bIsEmpty)
        return Color.Empty;
      string referenceInternal = this.UsedColorReferenceInternal;
      if (referenceInternal != null && referenceInternal.Length > 0)
        return this.ColorScheme[referenceInternal].GetColor(theme);
      return this.ContainsColorForTheme(theme) ? (Color) QMisc.AsValueType(this.m_aThemeColors[(object) theme]) : this.GetDefaultColor(theme);
    }

    internal QColor CloneColor(QColorSchemeBase colorScheme)
    {
      QColor qcolor = new QColor(colorScheme, this.ColorName);
      if (this.m_aDefaultThemeColors != null)
      {
        qcolor.SecureDefaultThemeColors();
        foreach (string key in (IEnumerable) this.m_aDefaultThemeColors.Keys)
        {
          object defaultThemeColor = this.m_aDefaultThemeColors[(object) key];
          qcolor.m_aDefaultThemeColors.Add((object) key, (object) QMisc.AsValueType(defaultThemeColor));
        }
      }
      if (this.m_aThemeColors != null)
      {
        qcolor.SecureThemeColors();
        foreach (string key in (IEnumerable) this.m_aThemeColors.Keys)
        {
          object aThemeColor = this.m_aThemeColors[(object) key];
          qcolor.m_aThemeColors.Add((object) key, (object) (Color) QMisc.AsValueType(aThemeColor));
        }
      }
      if (this.m_sColorReference != null && this.m_sColorReference.Length > 0)
        qcolor.m_sColorReference = this.m_sColorReference;
      return qcolor;
    }
  }
}
