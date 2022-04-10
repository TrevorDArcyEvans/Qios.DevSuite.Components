// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartTextPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Drawing.Text;

namespace Qios.DevSuite.Components
{
  public class QPartTextPainter : QPartObjectPainter
  {
    private QPartBoundsType m_eDrawOnBounds;
    private Color m_oTextColor;
    private Font m_oFont;
    private StringFormat m_oStringFormat;
    private QContentOrientation m_oOrientation;

    public QPartBoundsType DrawOnBounds
    {
      get => this.m_eDrawOnBounds;
      set => this.m_eDrawOnBounds = value;
    }

    public Color TextColor
    {
      get => this.m_oTextColor;
      set => this.m_oTextColor = value;
    }

    public Font Font
    {
      get => this.m_oFont;
      set => this.m_oFont = value;
    }

    public QContentOrientation Orientation
    {
      get => this.m_oOrientation;
      set => this.m_oOrientation = value;
    }

    public StringFormat StringFormat
    {
      get => this.m_oStringFormat;
      set => this.m_oStringFormat = value;
    }

    public static StringFormat CreateStringFormat(
      StringFormat baseFormat,
      QDrawTextOptions options)
    {
      return QPartTextPainter.CreateStringFormat(baseFormat, options, true, true);
    }

    public static StringFormat CreateStringFormat(
      StringFormat baseFormat,
      QDrawTextOptions options,
      bool globalHotkeyPrefixVisible,
      bool wrapText)
    {
      StringFormat stringFormat1;
      StringFormat stringFormat2 = stringFormat1 = new StringFormat(baseFormat);
      stringFormat2.Trimming = (options & QDrawTextOptions.WordEllipsis) != QDrawTextOptions.WordEllipsis ? ((options & QDrawTextOptions.PathEllipsis) != QDrawTextOptions.PathEllipsis ? ((options & QDrawTextOptions.EndEllipsis) != QDrawTextOptions.EndEllipsis ? StringTrimming.None : StringTrimming.EllipsisCharacter) : StringTrimming.EllipsisPath) : StringTrimming.Word;
      stringFormat2.HotkeyPrefix = (options & QDrawTextOptions.HidePrefix) != QDrawTextOptions.HidePrefix ? ((options & QDrawTextOptions.IgnorePrefix) != QDrawTextOptions.IgnorePrefix ? HotkeyPrefix.Show : HotkeyPrefix.None) : HotkeyPrefix.Hide;
      if (!globalHotkeyPrefixVisible && stringFormat2.HotkeyPrefix == HotkeyPrefix.Show)
        stringFormat2.HotkeyPrefix = HotkeyPrefix.Hide;
      if (!wrapText)
        stringFormat2.FormatFlags |= StringFormatFlags.NoWrap;
      return stringFormat2;
    }

    public static int CreateNativeDrawTextOptions(StringFormat stringFormat)
    {
      int nativeDrawTextOptions = 0;
      if ((stringFormat.FormatFlags & StringFormatFlags.NoWrap) != StringFormatFlags.NoWrap)
        nativeDrawTextOptions |= 16;
      if ((stringFormat.FormatFlags & StringFormatFlags.NoClip) == StringFormatFlags.NoClip)
        nativeDrawTextOptions |= 256;
      if (stringFormat.Trimming == StringTrimming.EllipsisWord)
        nativeDrawTextOptions |= 262144;
      else if (stringFormat.Trimming == StringTrimming.EllipsisPath)
        nativeDrawTextOptions |= 16384;
      else if (stringFormat.Trimming == StringTrimming.EllipsisCharacter)
        nativeDrawTextOptions |= 32768;
      if (stringFormat.HotkeyPrefix == HotkeyPrefix.Hide)
        nativeDrawTextOptions |= 1048576;
      return nativeDrawTextOptions;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled || part == null || this.m_oTextColor == Color.Empty)
        return;
      string text = (string) null;
      Size empty = Size.Empty;
      bool flag = false;
      if (part.ContentObject is string)
        text = part.ContentObject as string;
      else if (part.ContentObject is IQPartSizedContent)
      {
        IQPartSizedContent contentObject = part.ContentObject as IQPartSizedContent;
        text = contentObject.ContentObject as string;
        Size size = contentObject.Size;
        flag = true;
      }
      if (text == null || text.Length == 0)
        return;
      Font font = this.m_oFont != null ? this.m_oFont : paintContext.Font;
      Rectangle bounds = part.CalculatedProperties.GetBounds(this.m_eDrawOnBounds);
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return;
      if (flag)
      {
        int options = QPartTextPainter.CreateNativeDrawTextOptions(this.m_oStringFormat != null ? this.m_oStringFormat : paintContext.StringFormat) | this.GetNativeHorizontalTextAlignment(part.Properties.GetContentAlignmentHorizontal(part)) | this.GetNativeVerticalTextAlignment(part.Properties.GetContentAlignmentVertical(part));
        NativeHelper.DrawText(text, font, bounds, this.m_oTextColor, (QDrawTextOptions) options, paintContext.Graphics);
      }
      else
      {
        Brush brush = (Brush) new SolidBrush(this.m_oTextColor);
        StringFormat format = this.m_oStringFormat != null ? this.m_oStringFormat : paintContext.StringFormat;
        StringAlignment alignment = format.Alignment;
        StringAlignment lineAlignment = format.LineAlignment;
        format.Alignment = this.GetStringAlignment(part.Properties.GetContentAlignmentHorizontal(part));
        format.LineAlignment = this.GetStringAlignment(part.Properties.GetContentAlignmentVertical(part));
        QControlPaint.DrawString(text, font, bounds, this.m_oOrientation, brush, format, paintContext.Graphics);
        format.Alignment = alignment;
        format.LineAlignment = lineAlignment;
        brush.Dispose();
      }
    }

    private int GetNativeHorizontalTextAlignment(QPartAlignment alignment)
    {
      switch (alignment)
      {
        case QPartAlignment.Near:
          return 0;
        case QPartAlignment.Centered:
          return 1;
        case QPartAlignment.Far:
          return 2;
        default:
          return 0;
      }
    }

    private int GetNativeVerticalTextAlignment(QPartAlignment alignment)
    {
      switch (alignment)
      {
        case QPartAlignment.Near:
          return 0;
        case QPartAlignment.Centered:
          return 4;
        case QPartAlignment.Far:
          return 8;
        default:
          return 0;
      }
    }

    private StringAlignment GetStringAlignment(QPartAlignment alignment)
    {
      switch (alignment)
      {
        case QPartAlignment.Near:
          return StringAlignment.Near;
        case QPartAlignment.Centered:
          return StringAlignment.Center;
        case QPartAlignment.Far:
          return StringAlignment.Far;
        default:
          return StringAlignment.Near;
      }
    }
  }
}
