// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFontDefinition
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QFontDefinitionConverter))]
  public struct QFontDefinition
  {
    private bool m_bThrowException;
    private string m_sFontFamily;
    private QFontStyle m_eStyle;
    private float m_fSize;
    private static QFontDefinition m_oEmpty = new QFontDefinition((string) null, QFontStyle.Regular, -1f);

    public QFontDefinition(
      string fontFamily,
      bool bold,
      bool italic,
      bool strikeout,
      bool underline,
      float size)
    {
      this.m_bThrowException = true;
      this.m_sFontFamily = QFontDefinition.GetValidFontFamily(fontFamily, this.m_bThrowException);
      this.m_eStyle = new QFontStyle(bold, italic, strikeout, underline);
      this.m_fSize = size;
    }

    public QFontDefinition(string fontFamily, QFontStyle style, float size)
    {
      this.m_bThrowException = true;
      this.m_sFontFamily = QFontDefinition.GetValidFontFamily(fontFamily, this.m_bThrowException);
      this.m_eStyle = style;
      this.m_fSize = size;
    }

    public QFontDefinition(string fontFamily, QFontStyle style, float size, bool throwException)
    {
      this.m_bThrowException = throwException;
      this.m_sFontFamily = QFontDefinition.GetValidFontFamily(fontFamily, throwException);
      this.m_eStyle = style;
      this.m_fSize = size;
    }

    public QFontDefinition(QFontStyle style)
    {
      this.m_bThrowException = true;
      this.m_sFontFamily = (string) null;
      this.m_fSize = -1f;
      this.m_eStyle = style;
    }

    public QFontDefinition(float size, QFontStyle style)
    {
      this.m_bThrowException = true;
      this.m_sFontFamily = (string) null;
      this.m_fSize = size;
      this.m_eStyle = style;
    }

    public QFontDefinition(float size)
    {
      this.m_bThrowException = true;
      this.m_sFontFamily = (string) null;
      this.m_eStyle = QFontStyle.Regular;
      this.m_fSize = size;
    }

    public static QFontDefinition FromFont(Font font) => new QFontDefinition(font.Name, (QFontStyle) font.Style, font.SizeInPoints, false);

    public static QFontDefinition Empty => QFontDefinition.m_oEmpty;

    [Description("Gets or sets the FamilyName of the font. If this is set to null it is undefined.")]
    [Category("QBehavior")]
    [TypeConverter(typeof (FontConverter.FontNameConverter))]
    [DefaultValue(null)]
    public string FontFamily
    {
      get => this.m_sFontFamily;
      set => this.m_sFontFamily = QFontDefinition.GetValidFontFamily(value, this.m_bThrowException);
    }

    public static string GetValidFontFamily(string value, bool throwException)
    {
      if (QMisc.IsEmpty((object) value))
        return (string) null;
      if (!throwException)
        return value;
      System.Drawing.FontFamily fontFamily;
      try
      {
        fontFamily = new System.Drawing.FontFamily(value);
      }
      catch (ArgumentException ex)
      {
        throw new InvalidOperationException(QResources.GetException("QFontDefinition_InvalidFontFamily", (object) value));
      }
      return fontFamily?.Name;
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether this FontDefinition is Bold.")]
    public bool Bold
    {
      get => this.m_eStyle.Bold;
      set => this.m_eStyle.Bold = value;
    }

    [Description("Gets or sets whether this FontDefinition is Italic.")]
    [DefaultValue(false)]
    public bool Italic
    {
      get => this.m_eStyle.Italic;
      set => this.m_eStyle.Italic = value;
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether this FontDefinition is Underlined.")]
    public bool Underline
    {
      get => this.m_eStyle.Underline;
      set => this.m_eStyle.Underline = value;
    }

    [Description("Gets or sets whether this FontDefinition is Strikeout.")]
    [DefaultValue(false)]
    public bool Strikeout
    {
      get => this.m_eStyle.Strikeout;
      set => this.m_eStyle.Strikeout = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QFontStyle Style
    {
      get => this.m_eStyle;
      set => this.m_eStyle = value;
    }

    [DefaultValue(-1f)]
    [Category("QBehavior")]
    [Description("Gets the Size. If this is set to -1 it is undefined.")]
    public float Size
    {
      get => this.m_fSize;
      set => this.m_fSize = value;
    }

    public bool RequiresNewCreation(Font baseFont)
    {
      if (baseFont == null)
        return true;
      FontStyle style = baseFont.Style;
      float sizeInPoints = baseFont.SizeInPoints;
      string name = baseFont.Name;
      return this.m_sFontFamily != null && string.Compare(this.m_sFontFamily, name, true, CultureInfo.InvariantCulture) != 0 || (double) this.m_fSize >= 0.0 && (double) sizeInPoints != (double) this.m_fSize || this.m_eStyle != (QFontStyle) FontStyle.Regular && (QFontStyle) (style & (FontStyle) this.m_eStyle) != this.m_eStyle;
    }

    public Font GetFontFromCache() => this.GetFontFromCache((Font) null);

    public Font GetFontFromCache(Font baseFont)
    {
      QFontDefinition definition = QFontDefinition.Empty;
      if (baseFont != null)
        definition = QFontDefinition.FromFont(baseFont);
      definition.ApplyDefinition(this);
      Font font = QFontCache.FindFont(definition);
      if (font == null)
      {
        font = definition.CreateNewFont(baseFont);
        QFontCache.StoreFont(definition, font);
      }
      return font;
    }

    public Font CreateNewFont(Font baseFont)
    {
      FontStyle style = baseFont != null ? baseFont.Style : FontStyle.Regular;
      float emSize = baseFont != null ? baseFont.SizeInPoints : -1f;
      string str = baseFont?.Name;
      if (this.m_sFontFamily != null && string.Compare(this.m_sFontFamily, str, true, CultureInfo.InvariantCulture) != 0)
        str = this.m_sFontFamily;
      if ((double) this.m_fSize >= 0.0 && (double) emSize != (double) this.m_fSize)
        emSize = this.m_fSize;
      if (this.m_eStyle != (QFontStyle) FontStyle.Regular && (QFontStyle) (style & (FontStyle) this.m_eStyle) != this.m_eStyle)
        style |= (FontStyle) this.m_eStyle;
      return new Font(str, emSize, style);
    }

    public Font CreateNewFontIfNeeded(Font baseFont) => this.RequiresNewCreation(baseFont) ? this.CreateNewFont(baseFont) : baseFont;

    public void ApplyDefinition(QFontDefinition definition)
    {
      if (!QMisc.IsEmpty((object) definition.m_sFontFamily))
        this.m_sFontFamily = definition.m_sFontFamily;
      if ((double) definition.m_fSize >= 0.0)
        this.m_fSize = definition.m_fSize;
      ref QFontDefinition local = ref this;
      local.m_eStyle = (QFontStyle) ((FontStyle) local.m_eStyle | definition.Style.Style);
    }

    public override bool Equals(object obj) => obj is QFontDefinition qfontDefinition && string.Compare(this.m_sFontFamily != null ? this.m_sFontFamily : string.Empty, qfontDefinition.m_sFontFamily != null ? qfontDefinition.m_sFontFamily : string.Empty, true, CultureInfo.InvariantCulture) == 0 && (double) this.m_fSize == (double) qfontDefinition.m_fSize && !(this.m_eStyle != qfontDefinition.m_eStyle);

    public override int GetHashCode() => (this.m_sFontFamily != null ? this.m_sFontFamily.GetHashCode() : 0) ^ this.m_fSize.GetHashCode() ^ this.m_eStyle.GetHashCode();

    public static bool operator ==(QFontDefinition operand1, QFontDefinition operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QFontDefinition operand1, QFontDefinition operand2) => !operand1.Equals((object) operand2);
  }
}
