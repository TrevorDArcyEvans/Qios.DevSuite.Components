// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControlPaint
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public sealed class QControlPaint
  {
    private static string m_sFontCompareString;

    private QControlPaint()
    {
    }

    private static string FontCompareString
    {
      get
      {
        if (QControlPaint.m_sFontCompareString == null)
          QControlPaint.m_sFontCompareString = QResources.GetGeneral("QControlPaint_FontCompareString");
        return QControlPaint.m_sFontCompareString;
      }
    }

    public static Bitmap ConvertIconToBitmap(Icon icon)
    {
      QImageCacheKey key = new QImageCacheKey(QImageCacheType.BitmapOfIcon, icon, new object[0]);
      if (!(QImageCache.FindImage(key) is Bitmap cachedImage))
      {
        cachedImage = NativeHelper.ConvertIconToBitmap(icon);
        if (cachedImage != null)
          QImageCache.StoreImage(key, (Image) cachedImage);
      }
      return cachedImage;
    }

    public static void DrawIcon(Icon icon, Rectangle rectangle, Graphics graphics)
    {
      Bitmap bitmap = QControlPaint.ConvertIconToBitmap(icon);
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (bitmap == null)
        return;
      graphics.DrawImage((Image) bitmap, rectangle);
    }

    public static void DrawIcon(Icon icon, int left, int top, Graphics graphics)
    {
      if (icon == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (icon)));
      QControlPaint.DrawIcon(icon, new Rectangle(left, top, icon.Width, icon.Height), graphics);
    }

    public static void DrawIcon(
      Graphics graphics,
      Icon icon,
      Color replaceColor,
      Color replaceColorWith,
      Rectangle rectangle)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      if (icon == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (icon)));
      if (replaceColor != Color.Empty)
      {
        Bitmap bitmap = QControlPaint.ConvertIconToBitmap(icon);
        QControlPaint.DrawImage(graphics, (Image) bitmap, replaceColor, replaceColorWith, rectangle);
      }
      else
        QControlPaint.DrawIcon(icon, rectangle, graphics);
    }

    public static void DrawIconDisabled(Graphics graphics, Icon icon, Rectangle rectangle)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0 || icon == null || graphics == null)
        return;
      Bitmap bitmap = QControlPaint.ConvertIconToBitmap(icon);
      if (bitmap == null)
        return;
      QControlPaint.DrawImageDisabled(graphics, (Image) bitmap, rectangle);
    }

    public static void DrawImage(
      Graphics graphics,
      Image image,
      Color replaceColor,
      Color replaceColorWith,
      Rectangle rectangle)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (image == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (image)));
      ImageAttributes imageAttr = new ImageAttributes();
      if (replaceColor != Color.Empty)
        imageAttr.SetRemapTable(new ColorMap[1]
        {
          new ColorMap()
          {
            OldColor = replaceColor,
            NewColor = replaceColorWith
          }
        });
      graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
      imageAttr.ClearRemapTable();
    }

    public static void DrawImageDisabled(Graphics graphics, Image image, Rectangle rectangle)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (image == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (image)));
      float num1 = 0.2f;
      float num2 = 0.4f;
      float[][] newColorMatrix = new float[5][]
      {
        new float[5]{ num1, num1, num1, 0.0f, 0.0f },
        new float[5]{ num1, num1, num1, 0.0f, 0.0f },
        new float[5]{ num1, num1, num1, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 1f, 0.0f },
        new float[5]{ num2, num2, num2, 0.0f, 1f }
      };
      ImageAttributes imageAttr = new ImageAttributes();
      imageAttr.SetColorMatrix(new ColorMatrix(newColorMatrix));
      graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
      imageAttr.ClearColorMatrix();
    }

    public static void DrawImage(
      Image image,
      QImageAlign imageAlign,
      Rectangle rectangle,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (image == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (image)));
      QControlPaint.DrawImage(image, imageAlign, Point.Empty, rectangle, image.Size, graphics, (ImageAttributes) null, true);
    }

    public static Rectangle DrawImage(
      Image image,
      QImageAlign imageAlign,
      Rectangle rectangle,
      Size size,
      Graphics graphics)
    {
      return QControlPaint.DrawImage(image, imageAlign, Point.Empty, rectangle, size, graphics, (ImageAttributes) null, true);
    }

    public static Rectangle DrawImage(
      Image image,
      Color replaceColor,
      Color replaceColorWith,
      QImageAlign imageAlign,
      Rectangle rectangle,
      Size size,
      Graphics graphics)
    {
      ImageAttributes attributes = (ImageAttributes) null;
      if (replaceColor != Color.Empty)
      {
        attributes = new ImageAttributes();
        attributes.SetRemapTable(new ColorMap[1]
        {
          new ColorMap()
          {
            OldColor = replaceColor,
            NewColor = replaceColorWith
          }
        });
      }
      return QControlPaint.DrawImage(image, imageAlign, Point.Empty, rectangle, size, graphics, attributes, true);
    }

    public static Rectangle DrawImage(
      Image image,
      QImageAlign imageAlign,
      Rectangle rectangle,
      Size size,
      Graphics graphics,
      ImageAttributes attributes)
    {
      return QControlPaint.DrawImage(image, imageAlign, Point.Empty, rectangle, size, graphics, attributes, true);
    }

    public static Rectangle DrawImage(
      Image image,
      QImageAlign imageAlign,
      Rectangle rectangle,
      Size size,
      Graphics graphics,
      ImageAttributes attributes,
      bool setClippingRegion)
    {
      return QControlPaint.DrawImage(image, imageAlign, Point.Empty, rectangle, size, graphics, attributes, setClippingRegion);
    }

    public static Rectangle DrawImage(
      Image image,
      QImageAlign imageAlign,
      Point relativeOffset,
      Rectangle rectangle,
      Size size,
      Graphics graphics,
      ImageAttributes attributes,
      bool setClippingRegion)
    {
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return rectangle;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      if (image == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (image)));
      int left = rectangle.Left;
      int top = rectangle.Top;
      int width = 0;
      int height = 0;
      int num1 = rectangle.Left + (int) Math.Round((double) rectangle.Width / 2.0 - (double) size.Width / 2.0);
      int num2 = rectangle.Top + (int) Math.Round((double) rectangle.Height / 2.0 - (double) size.Height / 2.0);
      Region region = (Region) null;
      if (setClippingRegion)
      {
        region = graphics.Clip;
        graphics.SetClip(rectangle);
      }
      Rectangle destRect = Rectangle.Empty;
      switch (imageAlign)
      {
        case QImageAlign.RepeatedVertical:
          int y1 = top + relativeOffset.Y;
          while (y1 < rectangle.Top + rectangle.Height)
          {
            graphics.DrawImage(image, new Rectangle(rectangle.Left + relativeOffset.X, y1, size.Width, size.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            y1 += size.Height;
            height += size.Height;
          }
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, rectangle.Top + relativeOffset.Y, size.Width, height);
          break;
        case QImageAlign.RepeatedHorizontal:
          int x1 = left + relativeOffset.X;
          while (x1 < rectangle.Left + rectangle.Width)
          {
            graphics.DrawImage(image, new Rectangle(x1, rectangle.Top + relativeOffset.Y, size.Width, size.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            x1 += size.Width;
            width += size.Width;
          }
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, rectangle.Top + relativeOffset.Y, width, size.Height);
          break;
        case QImageAlign.RepeatedBoth:
          int y2 = top + relativeOffset.Y;
          while (y2 < rectangle.Top + rectangle.Height)
          {
            int x2 = rectangle.Left + relativeOffset.X;
            width = 0;
            while (x2 < rectangle.Left + rectangle.Width)
            {
              graphics.DrawImage(image, new Rectangle(x2, y2, size.Width, size.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
              x2 += size.Width;
              width += size.Width;
            }
            y2 += size.Height;
            height += size.Height;
          }
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, rectangle.Top + relativeOffset.Y, width, height);
          break;
        case QImageAlign.Stretched:
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, rectangle.Top + relativeOffset.Y, rectangle.Width, rectangle.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.Centered:
          destRect = new Rectangle(num1 + relativeOffset.X, num2 + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.TopLeft:
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, rectangle.Top + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.CenterLeft:
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, num2 + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.BottomLeft:
          destRect = new Rectangle(rectangle.Left + relativeOffset.X, rectangle.Top + rectangle.Height - size.Height + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.TopRight:
          destRect = new Rectangle(rectangle.Left + rectangle.Width - size.Width + relativeOffset.X, rectangle.Top + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.CenterRight:
          destRect = new Rectangle(rectangle.Left + rectangle.Width - size.Width + relativeOffset.X, num2 + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.BottomRight:
          destRect = new Rectangle(rectangle.Left + rectangle.Width - size.Width + relativeOffset.X, rectangle.Top + rectangle.Height - size.Height + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.TopMiddle:
          destRect = new Rectangle(num1 + relativeOffset.X, rectangle.Top + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
        case QImageAlign.BottomMiddle:
          destRect = new Rectangle(num1 + relativeOffset.X, rectangle.Top + rectangle.Height - size.Height + relativeOffset.Y, size.Width, size.Height);
          graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
          break;
      }
      if (setClippingRegion)
        graphics.SetClip(region, CombineMode.Replace);
      return destRect;
    }

    public static bool ContainsTransparentAreas(
      Color color1,
      Color color2,
      QAppearanceBase appearance)
    {
      if (appearance == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (appearance)));
      bool flag1 = color1.IsEmpty || color1.A < byte.MaxValue;
      bool flag2 = color2.IsEmpty || color2.A < byte.MaxValue;
      return appearance.BackgroundStyle == QColorStyle.Solid && flag1 || flag1 || flag2;
    }

    public static bool IsSolid(Color color1, Color color2, QAppearanceBase appearance)
    {
      if (appearance == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (appearance)));
      return appearance.BackgroundStyle == QColorStyle.Solid || (int) color1.A == (int) color2.A && (int) color1.R == (int) color2.R && (int) color1.G == (int) color2.G && (int) color1.B == (int) color2.B;
    }

    public static void DrawSmallCaptionText(
      IntPtr windowsXPTheme,
      string caption,
      bool active,
      RectangleF area,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      Brush brush = (Brush) new SolidBrush(active ? SystemColors.ActiveCaptionText : SystemColors.InactiveCaptionText);
      StringFormat format = new StringFormat(StringFormat.GenericDefault);
      format.LineAlignment = StringAlignment.Center;
      format.Alignment = StringAlignment.Near;
      format.Trimming = StringTrimming.EllipsisCharacter;
      format.FormatFlags = StringFormatFlags.NoWrap;
      NativeMethods.LOGFONT pFont;
      if (windowsXPTheme != IntPtr.Zero)
      {
        NativeMethods.GetThemeSysFont(windowsXPTheme, 802, out pFont);
      }
      else
      {
        NativeMethods.NONCLIENTMETRICS structure = new NativeMethods.NONCLIENTMETRICS();
        structure.cbSize = Marshal.SizeOf((object) structure);
        IntPtr num = Marshal.AllocHGlobal(structure.cbSize);
        Marshal.StructureToPtr((object) structure, num, true);
        NativeMethods.SystemParametersInfo(41, structure.cbSize, num, 0);
        structure = (NativeMethods.NONCLIENTMETRICS) QMisc.PtrToValueType(num, typeof (NativeMethods.NONCLIENTMETRICS));
        Marshal.FreeHGlobal(num);
        pFont = structure.lfSmCaptionFont;
      }
      Font font = Font.FromLogFont((object) pFont);
      graphics.DrawString(caption, font, brush, area, format);
      format.Dispose();
      font.Dispose();
      brush.Dispose();
    }

    public static void DrawCaption(Rectangle area, Graphics graphics, bool active)
    {
      if (area.Width == 0 || area.Height == 0)
        return;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      Color color;
      Color color2;
      if (NativeHelper.Windows2000 || NativeHelper.Windows98)
      {
        color = ColorTranslator.FromWin32(NativeMethods.GetSysColor(active ? 2 : 3));
        color2 = ColorTranslator.FromWin32(NativeMethods.GetSysColor(active ? 27 : 28));
      }
      else
      {
        color = ColorTranslator.FromWin32(NativeMethods.GetSysColor(active ? 2 : 3));
        color2 = color;
      }
      Brush brush = !(color == color2) ? (Brush) new LinearGradientBrush(new Rectangle(area.Left - 1, area.Top - 1, area.Width + 2, area.Height + 2), color, color2, 0.0f) : (Brush) new SolidBrush(color);
      graphics.FillRectangle(brush, area);
      brush.Dispose();
    }

    public static Color DesaturateColor(Color color)
    {
      int num = (int) ((double) byte.MaxValue * (double) color.GetBrightness());
      if (num < 0)
        num = 0;
      if (num > (int) byte.MaxValue)
        num = (int) byte.MaxValue;
      return Color.FromArgb(num, num, num);
    }

    public static Color AdjustColorBrightness(Color color, int percentage)
    {
      float num1 = (float) color.R / (float) byte.MaxValue;
      float num2 = (float) color.B / (float) byte.MaxValue;
      float num3 = (float) color.G / (float) byte.MaxValue;
      float num4 = (float) percentage / 100f;
      return Color.FromArgb(Math.Min((int) ((double) num1 * (double) num4 * (double) byte.MaxValue), (int) byte.MaxValue), Math.Min((int) ((double) num3 * (double) num4 * (double) byte.MaxValue), (int) byte.MaxValue), Math.Min((int) ((double) num2 * (double) num4 * (double) byte.MaxValue), (int) byte.MaxValue));
    }

    public static Color AddValueToColor(Color color, int value) => Color.FromArgb(Math.Max(Math.Min((int) color.R + value, (int) byte.MaxValue), 0), Math.Max(Math.Min((int) color.G + value, (int) byte.MaxValue), 0), Math.Max(Math.Min((int) color.B + value, (int) byte.MaxValue), 0));

    public static void ReplaceColor(Bitmap bitmap, Color colorToReplace, Color replaceColorWith)
    {
      if (bitmap == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (bitmap)));
      for (int x = 0; x < bitmap.Width; ++x)
      {
        for (int y = 0; y < bitmap.Height; ++y)
        {
          if (bitmap.GetPixel(x, y) == colorToReplace)
            bitmap.SetPixel(x, y, replaceColorWith);
        }
      }
    }

    public static Image CreateColorizedImage(
      Image grayScaledImage,
      Color colorizeColor,
      Color replaceColor,
      Color replaceColorWith)
    {
      QImageCacheKey key = new QImageCacheKey(QImageCacheType.ColorizedBitmapOfBitmap, grayScaledImage, new object[3]
      {
        (object) colorizeColor,
        (object) replaceColor,
        (object) replaceColorWith
      });
      if (QImageCache.FindImage(key) is Bitmap image)
        return (Image) image;
      bool flag1 = replaceColor != Color.Empty;
      bool flag2 = colorizeColor != Color.Empty;
      Bitmap cachedImage = new Bitmap(grayScaledImage);
      Color color1 = Color.FromArgb((int) colorizeColor.A, (int) byte.MaxValue - (int) colorizeColor.R, (int) byte.MaxValue - (int) colorizeColor.G, (int) byte.MaxValue - (int) colorizeColor.B);
      float num1 = (float) color1.R / (float) byte.MaxValue;
      float num2 = (float) color1.B / (float) byte.MaxValue;
      float num3 = (float) color1.G / (float) byte.MaxValue;
      float num4 = (float) color1.A / (float) byte.MaxValue;
      for (int x = 0; x < cachedImage.Width; ++x)
      {
        for (int y = 0; y < cachedImage.Height; ++y)
        {
          Color color2 = cachedImage.GetPixel(x, y);
          if (flag2 && (color2 != replaceColor || !flag1))
          {
            color2 = Color.FromArgb((int) ((double) color2.A * (double) num4), (int) ((double) ((int) byte.MaxValue - (int) color2.R) * (double) num1), (int) ((double) ((int) byte.MaxValue - (int) color2.G) * (double) num3), (int) ((double) ((int) byte.MaxValue - (int) color2.B) * (double) num2));
            color2 = Color.FromArgb((int) color2.A, (int) byte.MaxValue - (int) color2.R, (int) byte.MaxValue - (int) color2.G, (int) byte.MaxValue - (int) color2.B);
            cachedImage.SetPixel(x, y, color2);
          }
          else if (flag1 && color2 == replaceColor)
            cachedImage.SetPixel(x, y, replaceColorWith);
        }
      }
      QImageCache.StoreImage(key, (Image) cachedImage);
      return (Image) cachedImage;
    }

    public static Bitmap AddShadeToImage(
      Bitmap bitmap,
      Color shadeColor,
      int offsetX,
      int offsetY)
    {
      if (bitmap == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (bitmap)));
      Color color = Color.FromArgb((int) shadeColor.A, (int) shadeColor.R, (int) shadeColor.G, (int) shadeColor.B);
      QImageCacheKey key = new QImageCacheKey(QImageCacheType.ShadedBitmapOfBitmap, (Image) bitmap, new object[3]
      {
        (object) color,
        (object) offsetX,
        (object) offsetY
      });
      if (QImageCache.FindImage(key) is Bitmap image)
        return image;
      Bitmap cachedImage = new Bitmap((Image) bitmap);
      for (int x1 = 0; x1 < bitmap.Width; ++x1)
      {
        for (int y1 = 0; y1 < bitmap.Height; ++y1)
        {
          int x2 = x1 + offsetX;
          int y2 = y1 + offsetY;
          if (x2 >= 0 && x2 < bitmap.Width && y2 >= 0 && y2 < bitmap.Height)
          {
            Color pixel1 = bitmap.GetPixel(x1, y1);
            Color pixel2 = bitmap.GetPixel(x2, y2);
            if (pixel1.A != (byte) 0 && pixel1 != color && pixel2.A == (byte) 0)
              cachedImage.SetPixel(x2, y2, color);
          }
        }
      }
      QImageCache.StoreImage(key, (Image) cachedImage);
      return cachedImage;
    }

    public static Image RotateFlipImage(Image image, DockStyle dockStyle)
    {
      switch (dockStyle)
      {
        case DockStyle.Bottom:
          return (Image) QControlPaint.RotateFlipImage(image, RotateFlipType.Rotate180FlipNone);
        case DockStyle.Left:
          return (Image) QControlPaint.RotateFlipImage(image, RotateFlipType.Rotate270FlipNone);
        case DockStyle.Right:
          return (Image) QControlPaint.RotateFlipImage(image, RotateFlipType.Rotate90FlipNone);
        default:
          return image;
      }
    }

    public static Bitmap RotateFlipImage(Image image, RotateFlipType rotateFlipType)
    {
      QImageCacheKey key = new QImageCacheKey(QImageCacheType.RotatedBitmapOfBitmap, image, new object[1]
      {
        (object) rotateFlipType
      });
      if (QImageCache.FindImage(key) is Bitmap image1)
        return image1;
      Bitmap cachedImage = new Bitmap(image);
      cachedImage.RotateFlip(rotateFlipType);
      QImageCache.StoreImage(key, (Image) cachedImage);
      return cachedImage;
    }

    public static Font GetBiggestFont(Font font1, Font font2, Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      return font1 == null || font2 != null && font1 != font2 && (double) graphics.MeasureString(QControlPaint.FontCompareString, font1).Width < (double) graphics.MeasureString(QControlPaint.FontCompareString, font2).Width ? font2 : font1;
    }

    public static Size MeasureString(
      string text,
      Font font,
      bool horizontal,
      StringFormat format,
      Graphics graphics)
    {
      if (QMisc.IsEmpty((object) text))
        return Size.Empty;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      SizeF sizeF = format == null ? graphics.MeasureString(text, font) : graphics.MeasureString(text, font, PointF.Empty, format);
      return horizontal ? new Size((int) Math.Ceiling((double) sizeF.Width), (int) Math.Ceiling((double) sizeF.Height)) : new Size((int) Math.Ceiling((double) sizeF.Height), (int) Math.Ceiling((double) sizeF.Width));
    }

    public static Size MeasureNativeCharacterRanges(
      string text,
      Font font,
      int maximumWidth,
      Graphics graphics,
      out int maximumCharactersThatFit,
      out int[] stringWidths)
    {
      return NativeHelper.CalculateTextExtent(text, font, maximumWidth, graphics, out maximumCharactersThatFit, out stringWidths);
    }

    public static Rectangle CalculateTextBoundsForAlignment(
      Rectangle initialBounds,
      Size textSize,
      StringFormat format)
    {
      if (format.Alignment == StringAlignment.Far)
      {
        if (initialBounds.Width > textSize.Width)
          initialBounds.X += initialBounds.Width - textSize.Width;
      }
      else if (format.Alignment == StringAlignment.Center && initialBounds.Width > textSize.Width)
        initialBounds.X += (initialBounds.Width - textSize.Width) / 2;
      if (format.LineAlignment == StringAlignment.Far)
      {
        if (initialBounds.Height > textSize.Height)
          initialBounds.Y += initialBounds.Height - textSize.Height;
      }
      else if (format.LineAlignment == StringAlignment.Center && initialBounds.Height > textSize.Height)
        initialBounds.Y += (initialBounds.Height - textSize.Height) / 2;
      return initialBounds;
    }

    public static void DrawNativeString(
      string text,
      Font font,
      Rectangle bounds,
      Brush brush,
      StringFormat format,
      QDrawTextOptions options,
      Graphics graphics)
    {
      Color textColor = SystemColors.ControlText;
      if (brush is SolidBrush)
        textColor = ((SolidBrush) brush).Color;
      if (format.Trimming == StringTrimming.EllipsisCharacter)
        options |= QDrawTextOptions.EndEllipsis;
      else if (format.Trimming == StringTrimming.EllipsisWord)
        options |= QDrawTextOptions.WordEllipsis;
      else if (format.Trimming == StringTrimming.EllipsisPath)
        options |= QDrawTextOptions.PathEllipsis;
      Size textExtent = NativeHelper.CalculateTextExtent(text, font, bounds.Width, graphics, out int _, out int[] _);
      bounds = QControlPaint.CalculateTextBoundsForAlignment(bounds, textExtent, format);
      NativeHelper.DrawText(text, font, bounds, textColor, options, graphics);
    }

    public static void DrawString(
      string text,
      Font font,
      Rectangle bounds,
      QContentOrientation orientation,
      Brush brush,
      StringFormat format,
      Graphics graphics)
    {
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      switch (orientation)
      {
        case QContentOrientation.Horizontal:
          graphics.DrawString(text, font, brush, (RectangleF) bounds, format);
          break;
        case QContentOrientation.VerticalDown:
          graphics.TranslateTransform((float) bounds.Right, (float) bounds.Top);
          graphics.RotateTransform(90f);
          graphics.DrawString(text, font, brush, (RectangleF) new Rectangle(0, 0, bounds.Height, bounds.Width), format);
          graphics.RotateTransform(-90f);
          graphics.TranslateTransform((float) -bounds.Right, (float) -bounds.Top);
          break;
        case QContentOrientation.VerticalUp:
          graphics.TranslateTransform((float) bounds.Left, (float) bounds.Bottom);
          graphics.RotateTransform(-90f);
          graphics.DrawString(text, font, brush, (RectangleF) new Rectangle(0, 0, bounds.Height, bounds.Width), format);
          graphics.RotateTransform(90f);
          graphics.TranslateTransform((float) -bounds.Left, (float) -bounds.Bottom);
          break;
      }
    }

    public static void PaintTransparentBackground(Control control, PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Control parent = control.Parent;
      if (parent != null)
      {
        PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, new Rectangle(control.Location, control.Size));
        GraphicsState gstate = graphics.Save();
        try
        {
          graphics.TranslateTransform((float) -control.Left, (float) -control.Top);
          System.Type type = typeof (Control);
          (type.GetMethod("InvokePaintBackground", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetMethod(\"PaintTransparentBackground\")"))).Invoke((object) control, new object[2]
          {
            (object) parent,
            (object) paintEventArgs
          });
          graphics.Restore(gstate);
          gstate = graphics.Save();
          graphics.TranslateTransform((float) -control.Left, (float) -control.Top);
          (type.GetMethod("InvokePaint", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetMethod(\"PaintTransparentBackground\")"))).Invoke((object) control, new object[2]
          {
            (object) parent,
            (object) paintEventArgs
          });
        }
        finally
        {
          graphics.Restore(gstate);
        }
      }
      else
        graphics.FillRectangle(SystemBrushes.Control, e.ClipRectangle);
    }

    internal static Region AdjustClip(
      Graphics graphics,
      Region region,
      CombineMode combineMode)
    {
      Region region1 = graphics.Clip.Clone();
      graphics.SetClip(region, combineMode);
      return region1;
    }

    internal static void RestoreClip(Graphics graphics, Region savedRegion)
    {
      Region clip = graphics.Clip;
      graphics.SetClip(savedRegion, CombineMode.Replace);
      clip.Dispose();
    }

    public static void DrawPathShade(
      GraphicsPath path,
      Point shadeOffset,
      int shadeSize,
      Color shadeColor,
      Rectangle pathBounds,
      Graphics graphics)
    {
      if (pathBounds.Width == 0 || pathBounds.Height == 0)
        return;
      graphics.TranslateTransform((float) shadeOffset.X, (float) shadeOffset.Y);
      int num1 = shadeSize;
      PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
      pathGradientBrush.CenterPoint = (PointF) new Point(pathBounds.Left + pathBounds.Width / 2, pathBounds.Top + pathBounds.Height / 2);
      pathGradientBrush.CenterColor = shadeColor;
      pathGradientBrush.SurroundColors = new Color[1]
      {
        Color.Transparent
      };
      float num2 = (float) checked (num1 * 2);
      pathGradientBrush.FocusScales = new PointF((float) (1.0 - (double) num2 / (double) pathBounds.Width), (float) (1.0 - (double) num2 / (double) pathBounds.Height));
      graphics.FillPath((Brush) pathGradientBrush, path);
      graphics.TranslateTransform((float) -shadeOffset.X, (float) -shadeOffset.Y);
      pathGradientBrush.Dispose();
    }

    public static void DrawShapeShade(
      IQShadedShapeAppearance appearance,
      Rectangle shapeBounds,
      DockStyle shapeDockStyle,
      Color shadeColor,
      Graphics graphics)
    {
      QControlPaint.DrawShapeShade(appearance, shapeBounds, shapeDockStyle, shadeColor, graphics, (Matrix) null);
    }

    internal static void DrawShapeShade(
      IQShadedShapeAppearance appearance,
      Rectangle shapeBounds,
      DockStyle shapeDockStyle,
      Color shadeColor,
      Graphics graphics,
      Matrix matrix)
    {
      if (!appearance.ShadeVisible || !(appearance.ShadeOffset != Point.Empty) || !(shadeColor != Color.Empty))
        return;
      Region region = (Region) null;
      if (appearance.ShadeClipToShapeBounds)
      {
        Rectangle rect = appearance.ShadeClipMargin.InflateRectangleWithMargin(shapeBounds, true, shapeDockStyle);
        region = graphics.Clip.Clone();
        graphics.SetClip(rect, CombineMode.Intersect);
      }
      Rectangle rectangle = appearance.ShadeGrowPadding.InflateRectangleWithPadding(shapeBounds, true, shapeDockStyle);
      GraphicsPath graphicsPath = appearance.Shape.CreateGraphicsPath(rectangle, shapeDockStyle, QShapePathOptions.AllLines, (Matrix) null);
      if (matrix != null)
        graphicsPath.Transform(matrix);
      if (graphicsPath != null)
      {
        QControlPaint.DrawPathShade(graphicsPath, appearance.ShadeOffset, appearance.ShadeGradientSize, shadeColor, rectangle, graphics);
        graphicsPath.Dispose();
      }
      if (region == null)
        return;
      graphics.SetClip(region, CombineMode.Replace);
    }

    public static void DrawRoundedShade(
      Rectangle rectangle,
      int cornerSize,
      int gradientLength,
      Color shadeColor,
      QDrawRoundedRectangleOptions options,
      int horizontalOffset,
      int verticalOffset,
      Graphics graphics)
    {
      if (rectangle.Width < 1 || rectangle.Height < 1)
        return;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      rectangle.Offset(horizontalOffset, verticalOffset);
      GraphicsPath roundedRectanglePath = QRoundedRectanglePainter.Default.GetRoundedRectanglePath(rectangle, cornerSize, QAppearanceForegroundOptions.DrawAllBorders, options);
      PathGradientBrush pathGradientBrush = new PathGradientBrush(roundedRectanglePath);
      pathGradientBrush.CenterPoint = (PointF) new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
      pathGradientBrush.CenterColor = shadeColor;
      pathGradientBrush.SurroundColors = new Color[1]
      {
        Color.Transparent
      };
      float num = (float) checked (gradientLength * 2);
      pathGradientBrush.FocusScales = new PointF((float) (1.0 - (double) num / (double) rectangle.Width), (float) (1.0 - (double) num / (double) rectangle.Height));
      graphics.FillPath((Brush) pathGradientBrush, roundedRectanglePath);
      pathGradientBrush.Dispose();
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static QDrawControlBackgroundOptions CreateDrawControlBackgroundFlags(
      QColorSchemeBase colorScheme,
      QAppearanceBase appearance)
    {
      QDrawControlBackgroundOptions controlBackgroundFlags = QDrawControlBackgroundOptions.None;
      bool flag = colorScheme != null && colorScheme.UseHighContrast;
      if (appearance is QAppearance qappearance)
      {
        if (flag)
          controlBackgroundFlags |= QDrawControlBackgroundOptions.DrawAllBorders;
        else if (qappearance.ShowBorders)
        {
          if (qappearance.ShowBorderLeft)
            controlBackgroundFlags |= QDrawControlBackgroundOptions.DrawLeftBorder;
          if (qappearance.ShowBorderRight)
            controlBackgroundFlags |= QDrawControlBackgroundOptions.DrawRightBorder;
          if (qappearance.ShowBorderBottom)
            controlBackgroundFlags |= QDrawControlBackgroundOptions.DrawBottomBorder;
          if (qappearance.ShowBorderTop)
            controlBackgroundFlags |= QDrawControlBackgroundOptions.DrawTopBorder;
        }
      }
      return controlBackgroundFlags;
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static Brush CreateBrush(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      Color color1,
      Color color2)
    {
      if (appearance == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (appearance)));
      QColorStyle colorStyle = colorScheme == null || !colorScheme.UseHighContrast ? appearance.BackgroundStyle : QColorStyle.Solid;
      return QControlPaint.CreateBrush(rectangle, colorStyle, appearance.GradientAngle, appearance.GradientBlendPosition, appearance.GradientBlendFactor, color1, color2);
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static Brush CreateBrush(
      Rectangle rectangle,
      QColorStyle colorStyle,
      int gradientAngle,
      int gradientBlendPosition,
      int gradientBlendFactor,
      Color color1,
      Color color2)
    {
      QColorStyle qcolorStyle = colorStyle;
      if (rectangle.Width <= 0 || rectangle.Height <= 0)
        return (Brush) new SolidBrush(color1);
      if (qcolorStyle == QColorStyle.Gradient && color1 == color2)
        qcolorStyle = QColorStyle.Solid;
      if (qcolorStyle == QColorStyle.Gradient)
      {
        LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(rectangle.Left - 1, rectangle.Top - 1, rectangle.Width + 1, rectangle.Height + 1), color1, color2, (float) gradientAngle, false);
        Blend blend = new Blend();
        float num1 = gradientBlendFactor > 0 ? (float) gradientBlendFactor / 100f : 0.0f;
        if ((double) num1 > 1.0)
          num1 = 1f;
        blend.Factors = new float[3]{ 0.0f, num1, 1f };
        float num2 = gradientBlendPosition > 0 ? (float) gradientBlendPosition / 100f : 0.0f;
        if ((double) num2 > 1.0)
          num2 = 1f;
        blend.Positions = new float[3]{ 0.0f, num2, 1f };
        brush.Blend = blend;
        return (Brush) brush;
      }
      return qcolorStyle == QColorStyle.Solid && color1 != Color.Empty ? (Brush) new SolidBrush(color1) : (Brush) null;
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static void DrawBorders(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      Color borderColor,
      Graphics graphics)
    {
      QDrawControlBackgroundOptions controlBackgroundFlags = QControlPaint.CreateDrawControlBackgroundFlags(colorScheme, appearance);
      int borderWidth = appearance is QAppearance qappearance ? qappearance.BorderWidth : 0;
      QControlPaint.DrawBorders(rectangle, borderColor, borderWidth, controlBackgroundFlags, graphics);
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static void DrawBorders(
      Rectangle rectangle,
      Color borderColor,
      int borderWidth,
      QDrawControlBackgroundOptions flags,
      Graphics graphics)
    {
      if ((flags & QDrawControlBackgroundOptions.NeverDrawBorders) == QDrawControlBackgroundOptions.NeverDrawBorders)
        return;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      bool flag1 = (flags & QDrawControlBackgroundOptions.DrawLeftBorder) == QDrawControlBackgroundOptions.DrawLeftBorder;
      bool flag2 = (flags & QDrawControlBackgroundOptions.DrawTopBorder) == QDrawControlBackgroundOptions.DrawTopBorder;
      bool flag3 = (flags & QDrawControlBackgroundOptions.DrawBottomBorder) == QDrawControlBackgroundOptions.DrawBottomBorder;
      bool flag4 = (flags & QDrawControlBackgroundOptions.DrawRightBorder) == QDrawControlBackgroundOptions.DrawRightBorder;
      Brush brush = (Brush) new SolidBrush(borderColor);
      if (flag2)
        graphics.FillRectangle(brush, rectangle.Left, rectangle.Top, rectangle.Width, borderWidth);
      if (flag3)
        graphics.FillRectangle(brush, rectangle.Left, rectangle.Bottom - borderWidth, rectangle.Width, borderWidth);
      if (flag1)
        graphics.FillRectangle(brush, rectangle.Left, rectangle.Top, borderWidth, rectangle.Height);
      if (flag4)
        graphics.FillRectangle(brush, rectangle.Right - borderWidth, rectangle.Top, borderWidth, rectangle.Height);
      brush.Dispose();
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static void DrawControlBackground(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      Color borderColor,
      Color backgroundColor1,
      Color backgroundColor2,
      QDrawControlBackgroundOptions flags,
      Graphics graphics)
    {
      QControlPaint.DrawControlBackground(rectangle, appearance, colorScheme, borderColor, backgroundColor1, backgroundColor2, (Image) null, QImageAlign.TopLeft, flags, graphics);
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static void DrawControlBackground(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      Color borderColor,
      Color backgroundColor1,
      Color backgroundColor2,
      Image backgroundImage,
      QImageAlign imageAlign,
      QDrawControlBackgroundOptions flags,
      Graphics graphics)
    {
      if (appearance == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (appearance)));
      if (rectangle.Width == 0 || rectangle.Height == 0)
        return;
      int borderWidth = appearance is QAppearance qappearance ? qappearance.BorderWidth : 0;
      QDrawControlBackgroundOptions backgroundOptions = flags;
      QColorStyle colorStyle = colorScheme == null || !colorScheme.UseHighContrast ? appearance.BackgroundStyle : QColorStyle.Solid;
      QDrawControlBackgroundOptions flags1 = backgroundOptions | QControlPaint.CreateDrawControlBackgroundFlags(colorScheme, appearance);
      QControlPaint.DrawControlBackground(rectangle, colorStyle, appearance.GradientAngle, appearance.GradientBlendPosition, appearance.GradientBlendFactor, borderWidth, borderColor, backgroundColor1, backgroundColor2, backgroundImage, imageAlign, flags1, graphics);
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint objects, use the various painters like QRectanglePainter or QShapePainter")]
    public static void DrawControlBackground(
      Rectangle rectangle,
      QColorStyle colorStyle,
      int gradientAngle,
      int gradientBlendPosition,
      int gradientBlendFactor,
      int borderWidth,
      Color borderColor,
      Color backgroundColor1,
      Color backgroundColor2,
      Image backgroundImage,
      QImageAlign imageAlign,
      QDrawControlBackgroundOptions flags,
      Graphics graphics)
    {
      if (rectangle.Width <= 0 || rectangle.Height <= 0)
        return;
      if (graphics == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      Brush brush = QControlPaint.CreateBrush(rectangle, colorStyle, gradientAngle, gradientBlendPosition, gradientBlendFactor, backgroundColor1, backgroundColor2);
      if (brush != null)
      {
        graphics.FillRectangle(brush, rectangle);
        brush.Dispose();
      }
      if (backgroundImage != null)
        QControlPaint.DrawImage(backgroundImage, imageAlign, rectangle, graphics);
      QControlPaint.DrawBorders(rectangle, borderColor, borderWidth, flags, graphics);
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint rounded objects, use QRoundedRectanglePainter")]
    public static void DrawRoundedBorders(
      Rectangle rectangle,
      int cornerSize,
      Color borderColor,
      int borderWidth,
      QDrawControlBackgroundOptions flags,
      QDrawRoundedRectangleOptions roundedRectangleFlags,
      Graphics graphics)
    {
      SmoothingMode smoothingMode = graphics != null ? graphics.SmoothingMode : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      GraphicsPath roundedRectanglePath = QRoundedRectanglePainter.Default.GetRoundedRectanglePath(new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1), cornerSize, QAppearanceForegroundOptions.DrawAllBorders, roundedRectangleFlags);
      Pen pen = new Pen(borderColor, (float) borderWidth);
      graphics.DrawPath(pen, roundedRectanglePath);
      roundedRectanglePath.Dispose();
      graphics.SmoothingMode = smoothingMode;
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint rounded objects, use QRoundedRectanglePainter")]
    public static void FillRoundedRectangle(
      Rectangle rectangle,
      int cornerSize,
      Brush brush,
      Graphics graphics)
    {
      QControlPaint.FillRoundedRectangle(rectangle, cornerSize, brush, QDrawRoundedRectangleOptions.All, graphics);
    }

    [Obsolete("Is obsolete since Qios.DevSuite 2.0. To paint rounded objects, use QRoundedRectanglePainter")]
    public static void FillRoundedRectangle(
      Rectangle rectangle,
      int cornerSize,
      Brush brush,
      QDrawRoundedRectangleOptions flags,
      Graphics graphics)
    {
      SmoothingMode smoothingMode = graphics != null ? graphics.SmoothingMode : throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (graphics)));
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle1 = new Rectangle(rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
      Region clip = graphics.Clip;
      Region region = graphics.Clip.Clone();
      region.Intersect(new Region(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)));
      graphics.Clip = region;
      GraphicsPath roundedRectanglePath = QRoundedRectanglePainter.Default.GetRoundedRectanglePath(rectangle1, cornerSize, QAppearanceForegroundOptions.DrawAllBorders, flags);
      graphics.FillPath(brush, roundedRectanglePath);
      roundedRectanglePath.Dispose();
      graphics.SmoothingMode = smoothingMode;
      graphics.Clip = clip;
    }
  }
}
