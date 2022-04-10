// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextRenderedText
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextRenderedText : QMarkupTextRenderedPart
  {
    private string m_sText;
    private string m_sVisibleText;
    private int m_iFirstVisibleCharacter;
    private int m_iLastVisibleCharacter;
    private int[] m_aStringWidths;

    public QMarkupTextRenderedText(float baseLine, SizeF size, string text, int[] stringWidths)
      : base(baseLine, size)
    {
      this.m_sText = text;
      this.m_iFirstVisibleCharacter = 0;
      this.m_iLastVisibleCharacter = this.m_sText.Length - 1;
      this.m_aStringWidths = stringWidths;
    }

    public string Text => this.m_sText;

    public string VisibleText => this.m_sVisibleText != null && this.m_sVisibleText.Length > 0 ? this.m_sVisibleText : this.m_sText;

    public int FirstVisibleCharacter
    {
      get => this.m_iFirstVisibleCharacter;
      set => this.RecalculateVisibleTextPart(value, this.m_iLastVisibleCharacter);
    }

    public int LastVisibleCharacter
    {
      get => this.m_iFirstVisibleCharacter;
      set => this.RecalculateVisibleTextPart(this.m_iFirstVisibleCharacter, value);
    }

    private void RecalculateVisibleTextPart(int firstIndex, int lastIndex)
    {
      if (firstIndex < 0)
        firstIndex = 0;
      if (lastIndex >= this.m_sText.Length)
        lastIndex = this.m_sText.Length - 1;
      this.m_iFirstVisibleCharacter = firstIndex;
      this.m_iLastVisibleCharacter = lastIndex;
      this.m_sVisibleText = this.m_iFirstVisibleCharacter == 0 && this.m_iLastVisibleCharacter == this.m_sText.Length - 1 || this.m_iFirstVisibleCharacter > this.m_iLastVisibleCharacter ? (string) null : this.m_sText.Substring(this.m_iFirstVisibleCharacter, this.m_iLastVisibleCharacter - this.m_iFirstVisibleCharacter + 1);
      if (this.m_iLastVisibleCharacter >= 0 && this.m_iLastVisibleCharacter < this.m_aStringWidths.Length && this.m_iFirstVisibleCharacter >= 0 && this.m_iFirstVisibleCharacter <= this.m_iLastVisibleCharacter)
        this.PutWidth((float) this.m_aStringWidths[this.m_iLastVisibleCharacter] - (this.m_iFirstVisibleCharacter > 0 ? (float) this.m_aStringWidths[this.m_iFirstVisibleCharacter - 1] : 0.0f));
      else
        this.PutWidth(0.0f);
    }

    public override bool TrimLeft()
    {
      if (this.m_sText == null)
        return false;
      int index = 0;
      while (index < this.m_sText.Length && char.IsWhiteSpace(this.m_sText, index))
        ++index;
      this.FirstVisibleCharacter = index;
      return this.VisibleText != null && this.VisibleText.Length > 0;
    }

    public override bool TrimRight()
    {
      if (this.m_sText == null)
        return false;
      int index = this.m_sText.Length - 1;
      while (index >= 0 && char.IsWhiteSpace(this.m_sText, index))
        --index;
      this.LastVisibleCharacter = index;
      return this.VisibleText != null && this.VisibleText.Length > 0;
    }

    public int[] StringWidths => this.m_aStringWidths;

    public override void Draw(Graphics graphics)
    {
      PointF absoluteLocation = this.AbsoluteLocation;
      Rectangle bounds = new Rectangle((int) Math.Round((double) absoluteLocation.X), (int) Math.Round((double) absoluteLocation.Y), (int) short.MaxValue, (int) short.MaxValue);
      QDrawTextOptions options = QDrawTextOptions.None;
      if (this.MarkupText != null)
      {
        Rectangle clippingBounds = this.MarkupText.ClippingBounds;
        bounds.Intersect(clippingBounds);
        options = this.MarkupText.Configuration.DrawTextOptions;
      }
      NativeHelper.DrawText(this.VisibleText, this.Element.CurrentFont, bounds, this.Element.CurrentColor, options, graphics);
      if (!this.Element.Focused)
        return;
      Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, Math.Min((int) Math.Round((double) this.Width), bounds.Width), Math.Min((int) Math.Round((double) this.Height), bounds.Height));
      if (rectangle.Width <= 0 || rectangle.Height <= 0 || this.MarkupText == null)
        return;
      this.MarkupText.FocusRectangles.Add((object) rectangle);
    }

    protected override void Dispose(bool disposing) => base.Dispose(disposing);
  }
}
