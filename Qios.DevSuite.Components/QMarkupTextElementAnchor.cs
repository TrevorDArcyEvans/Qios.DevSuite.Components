// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextElementAnchor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextElementAnchor : QMarkupTextElement
  {
    public const string AttributeColor = "color";
    public const string AttributeColorHot = "colorHot";
    public const string AttributeColorActive = "colorActive";
    public const string AttributeColorDisabled = "colorDisabled";
    public const string AttributeHRef = "href";

    protected QMarkupTextElementAnchor(QMarkupTextStyle owningStyle, string tag)
      : base(owningStyle, tag)
    {
    }

    public Color Color
    {
      get => QMisc.GetAsColor(this.GetAttributeAsString("color"), this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColor(this.OwningMarkupText.ColorScheme, this.CurrentColor));
      set => this.SetAttributeValue("color", QMisc.GetAsString((object) value), true, true);
    }

    public Color ColorHot
    {
      get => QMisc.GetAsColor(this.GetAttributeAsString("colorHot"), this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColorHot(this.OwningMarkupText.ColorScheme, this.CurrentColor));
      set => this.SetAttributeValue("colorHot", QMisc.GetAsString((object) value), true, true);
    }

    public Color ColorActive
    {
      get => QMisc.GetAsColor(this.GetAttributeAsString("colorActive"), this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColorActive(this.OwningMarkupText.ColorScheme, this.CurrentColor));
      set => this.SetAttributeValue("colorActive", QMisc.GetAsString((object) value), true, true);
    }

    public Color ColorDisabled
    {
      get => QMisc.GetAsColor(this.GetAttributeAsString("colorDisabled"), this.OwningMarkupText.ColorScheme, this.OwningStyle.GetUsedTextColorDisabled(this.OwningMarkupText.ColorScheme, this.CurrentColor));
      set => this.SetAttributeValue("colorDisabled", QMisc.GetAsString((object) value), true, true);
    }

    public string HRef
    {
      get => this.GetAttributeAsString("href", string.Empty);
      set => this.SetAttributeValue("href", value, true, true);
    }

    protected override void ApplyElementAttributes()
    {
      base.ApplyElementAttributes();
      if (!this.Enabled)
        this.PutCurrentColor(this.ColorDisabled);
      else if (this.Active)
        this.PutCurrentColor(this.ColorActive);
      else if (this.Hot)
        this.PutCurrentColor(this.ColorHot);
      else
        this.PutCurrentColor(this.Color);
    }
  }
}
