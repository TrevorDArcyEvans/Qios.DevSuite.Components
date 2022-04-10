// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextElementBigSmall
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextElementBigSmall : QMarkupTextElement
  {
    public const string AttributeStep = "step";

    protected QMarkupTextElementBigSmall(QMarkupTextStyle owningStyle, string tag)
      : base(owningStyle, tag)
    {
    }

    public int Step
    {
      get => QMisc.GetAsInt((object) this.GetAttributeAsString("step"), this.OwningMarkupText.Configuration.BiggerSmallerStep);
      set => this.SetAttributeValue("step", QMisc.GetAsString((object) value), true, true);
    }

    protected override void ApplyElementAttributes()
    {
      base.ApplyElementAttributes();
      int num = this.Step;
      if (string.Compare(this.Tag, "small", true, CultureInfo.InvariantCulture) == 0)
        num = -num;
      if (this.CurrentFont == null)
        return;
      QFontDefinition qfontDefinition = QFontDefinition.FromFont(this.CurrentFont);
      qfontDefinition.Size += (float) num;
      this.PutCurrentFont(qfontDefinition.GetFontFromCache((Font) null));
    }
  }
}
