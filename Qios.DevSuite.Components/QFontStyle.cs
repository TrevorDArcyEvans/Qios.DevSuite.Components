// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFontStyle
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QFontStyleConverter))]
  [Serializable]
  public struct QFontStyle
  {
    private FontStyle m_eFontStyle;
    private static QFontStyle m_eRegular = new QFontStyle(FontStyle.Regular);

    public QFontStyle(FontStyle style) => this.m_eFontStyle = style;

    public QFontStyle(bool bold, bool italic, bool strikeout, bool underline)
    {
      this.m_eFontStyle = FontStyle.Regular;
      this.Bold = bold;
      this.Italic = italic;
      this.Strikeout = strikeout;
      this.Underline = underline;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public FontStyle Style => this.m_eFontStyle;

    public static QFontStyle Regular => QFontStyle.m_eRegular;

    [Description("Gets or sets whether the Font must be bold.")]
    [DefaultValue(false)]
    public bool Bold
    {
      get => (this.m_eFontStyle & FontStyle.Bold) == FontStyle.Bold;
      set
      {
        if (value)
          this.m_eFontStyle |= FontStyle.Bold;
        else
          this.m_eFontStyle &= ~FontStyle.Bold;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether the Font must be Italic")]
    public bool Italic
    {
      get => (this.m_eFontStyle & FontStyle.Italic) == FontStyle.Italic;
      set
      {
        if (value)
          this.m_eFontStyle |= FontStyle.Italic;
        else
          this.m_eFontStyle &= ~FontStyle.Italic;
      }
    }

    [Description("Gets or sets whether the Font must be Strikeout")]
    [DefaultValue(false)]
    public bool Strikeout
    {
      get => (this.m_eFontStyle & FontStyle.Strikeout) == FontStyle.Strikeout;
      set
      {
        if (value)
          this.m_eFontStyle |= FontStyle.Strikeout;
        else
          this.m_eFontStyle &= ~FontStyle.Strikeout;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether the font must be underlined")]
    public bool Underline
    {
      get => (this.m_eFontStyle & FontStyle.Underline) == FontStyle.Underline;
      set
      {
        if (value)
          this.m_eFontStyle |= FontStyle.Underline;
        else
          this.m_eFontStyle &= ~FontStyle.Underline;
      }
    }

    public static implicit operator FontStyle(QFontStyle fontStyle) => fontStyle.m_eFontStyle;

    public FontStyle ToFontStyle() => this.m_eFontStyle;

    public static implicit operator QFontStyle(FontStyle fontStyle) => new QFontStyle(fontStyle);

    public static QFontStyle FromFontStyle(FontStyle fontStyle) => new QFontStyle(fontStyle);

    public override bool Equals(object obj) => obj is QFontStyle qfontStyle && qfontStyle.Style == this.Style;

    public static int Compare(object obj1, object obj2)
    {
      if (obj1 == null)
        return -1;
      return obj2 == null || !obj1.Equals(obj2) ? 1 : 0;
    }

    public override int GetHashCode() => this.m_eFontStyle.GetHashCode();

    public static bool operator ==(QFontStyle operand1, QFontStyle operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QFontStyle operand1, QFontStyle operand2) => !operand1.Equals((object) operand2);
  }
}
