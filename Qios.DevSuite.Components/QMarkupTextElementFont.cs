// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextElementFont
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Xml;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextElementFont : QMarkupTextElement
  {
    public const string AttributeFace = "face";
    public const string AttributeSize = "size";
    public const string AttributeColor = "color";

    protected QMarkupTextElementFont(QMarkupTextStyle owningStyle, string tag)
      : base(owningStyle, tag)
    {
    }

    public string Face
    {
      get => QMarkupTextElementFont.ReadFaceAttribute(this.MarkupNode);
      set => this.SetAttributeValue("face", value, true, true);
    }

    public int Size
    {
      get => QMisc.GetAsInt((object) this.GetAttributeAsString("size"), -1);
      set => this.SetAttributeValue("size", QMisc.GetAsString((object) value), true, true);
    }

    public Color Color
    {
      get => QMisc.GetAsColor(this.GetAttributeAsString("color"), this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColor(this.OwningMarkupText.ColorScheme, this.CurrentColor));
      set => this.SetAttributeValue("color", QMisc.GetAsString((object) value), true, true);
    }

    public static string ReadFaceAttribute(XmlNode node)
    {
      string attributeAsString = QMarkupTextElement.GetAttributeAsString(node, "face", string.Empty);
      if (QMisc.IsEmpty((object) attributeAsString))
        return (string) null;
      FontFamily fontFamily;
      try
      {
        fontFamily = new FontFamily(attributeAsString);
      }
      catch (ArgumentException ex)
      {
        return (string) null;
      }
      return fontFamily?.Name;
    }

    protected override void ApplyElementAttributes()
    {
      base.ApplyElementAttributes();
      QFontDefinition qfontDefinition = new QFontDefinition(this.Face, (QFontStyle) FontStyle.Regular, (float) this.Size, false);
      if (this.CurrentFont != null)
        this.PutCurrentFont(qfontDefinition.GetFontFromCache(this.ParentFont));
      this.PutCurrentColor(this.Color);
    }
  }
}
