// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.NativeHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal sealed class NativeHelper
  {
    private static Version m_oWinXPVersion = new Version(5, 1, 0, 0);
    private static Version m_oWin2000Version = new Version(5, 0, 0, 0);
    private static Version m_oWin98Version = new Version(5, 1, 0, 0);
    private static IntPtr m_hHalfToneBrush = IntPtr.Zero;

    private NativeHelper()
    {
    }

    internal static NativeMethods.RECT CreateRECT(Rectangle rectangle) => NativeHelper.CreateRECT(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);

    internal static NativeMethods.RECT CreateRECT(
      int left,
      int top,
      int width,
      int height)
    {
      return new NativeMethods.RECT()
      {
        left = left,
        top = top,
        bottom = top + height,
        right = left + width
      };
    }

    internal static NativeMethods.POINT CreatePoint(Point point) => new NativeMethods.POINT(point.X, point.Y);

    internal static NativeMethods.SIZE CreateSize(Size size) => new NativeMethods.SIZE(size.Width, size.Height);

    internal static Rectangle CreateRectangle(NativeMethods.RECT rectangle) => new Rectangle()
    {
      X = rectangle.left,
      Y = rectangle.top,
      Width = rectangle.right - rectangle.left,
      Height = rectangle.bottom - rectangle.top
    };

    internal static short SecureAsShort(int value)
    {
      if (value > (int) short.MaxValue)
        return short.MaxValue;
      return value < (int) short.MinValue ? short.MinValue : (short) value;
    }

    public static Control GetActiveMdiChild(MdiClient client)
    {
      if (client == null || !client.IsHandleCreated)
        return (Control) null;
      IntPtr wParam = new IntPtr();
      IntPtr lParam = new IntPtr();
      return Control.FromHandle(NativeMethods.SendMessage(client.Handle, 553, wParam, lParam));
    }

    public static FormWindowState GetCurrentFormState(Form form)
    {
      if (form == null)
        return FormWindowState.Normal;
      return form.IsHandleCreated ? NativeHelper.GetCurrentWindowState((IWin32Window) form) : form.WindowState;
    }

    public static FormWindowState GetCurrentWindowState(IWin32Window window)
    {
      if (window == null)
        return FormWindowState.Normal;
      NativeMethods.WINDOWPLACEMENT lpwndpl = new NativeMethods.WINDOWPLACEMENT();
      lpwndpl.length = Marshal.SizeOf((object) lpwndpl);
      NativeMethods.GetWindowPlacement(window.Handle, ref lpwndpl);
      switch (lpwndpl.showCmd)
      {
        case 2:
        case 6:
        case 7:
          return FormWindowState.Minimized;
        case 3:
          return FormWindowState.Maximized;
        default:
          return FormWindowState.Normal;
      }
    }

    public static bool IsWindowVisible(Control control) => NativeHelper.IsWindowVisible(control, false);

    public static bool IsWindowVisible(Control control, bool forceHandleCreation)
    {
      if (control == null || control.IsDisposed)
        return false;
      return forceHandleCreation || control.IsHandleCreated ? (NativeMethods.GetWindowLong(control.Handle, -16) & 268435456) == 268435456 : control.Visible;
    }

    public static void SetWindowVisibleWithoutNotice(Control control, bool visible) => NativeMethods.SetWindowPos(control.Handle, IntPtr.Zero, 0, 0, 0, 0, (uint) ((visible ? 64 : 128) | 4 | 8 | 1024 | 1 | 2));

    public static Rectangle GetWindowBounds(Control control)
    {
      if (control.IsDisposed)
        return Rectangle.Empty;
      if (!control.IsHandleCreated)
        return control.Bounds;
      NativeMethods.RECT lpRect;
      NativeMethods.GetWindowRect(control.Handle, out lpRect);
      return NativeHelper.CreateRectangle(lpRect);
    }

    public static int GetAnimateWindowFlagsFromDirection(QCommandDirections direction)
    {
      int flagsFromDirection = 0;
      if ((direction & QCommandDirections.Down) == QCommandDirections.Down)
        flagsFromDirection |= 4;
      if ((direction & QCommandDirections.Up) == QCommandDirections.Up)
        flagsFromDirection |= 8;
      if ((direction & QCommandDirections.Left) == QCommandDirections.Left)
        flagsFromDirection |= 2;
      if ((direction & QCommandDirections.Right) == QCommandDirections.Right)
        flagsFromDirection |= 1;
      return flagsFromDirection;
    }

    public static QThemeInfo GetCurrentThemeInfo()
    {
      int num = 1024;
      StringBuilder pszThemeFileName = new StringBuilder(num);
      StringBuilder pszColorBuff = new StringBuilder(num);
      StringBuilder pszSizeBuff = new StringBuilder(num);
      try
      {
        Marshal.ThrowExceptionForHR(NativeMethods.GetCurrentThemeName(pszThemeFileName, num, pszColorBuff, num, pszSizeBuff, num));
      }
      catch (DllNotFoundException ex)
      {
        return (QThemeInfo) null;
      }
      catch (COMException ex)
      {
        return (QThemeInfo) null;
      }
      catch (MarshalDirectiveException ex)
      {
        return (QThemeInfo) null;
      }
      return new QThemeInfo()
      {
        FileName = Path.GetFileName(pszThemeFileName.ToString()),
        WindowsSchemeName = pszColorBuff.ToString()
      };
    }

    public static int ToolWindowCaptionButtonSpacing => 2;

    public static bool AnimateTooltips
    {
      get
      {
        if (!NativeHelper.Windows2000)
          return false;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (bool)));
        NativeMethods.SystemParametersInfo(4118, 0, num, 0);
        bool valueType = (bool) QMisc.PtrToValueType(num, typeof (bool));
        Marshal.FreeHGlobal(num);
        return valueType;
      }
    }

    public static bool AnimateMenus
    {
      get
      {
        if (!NativeHelper.Windows98 && !NativeHelper.Windows2000)
          return false;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (bool)));
        NativeMethods.SystemParametersInfo(4098, 0, num, 0);
        bool valueType = (bool) QMisc.PtrToValueType(num, typeof (bool));
        Marshal.FreeHGlobal(num);
        return valueType;
      }
    }

    public static bool AnimateMenusWithFading
    {
      get
      {
        if (!NativeHelper.Windows2000)
          return false;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (bool)));
        NativeMethods.SystemParametersInfo(4114, 0, num, 0);
        bool valueType = (bool) QMisc.PtrToValueType(num, typeof (bool));
        Marshal.FreeHGlobal(num);
        return valueType;
      }
    }

    public static bool ShowShadows
    {
      get
      {
        if (!NativeHelper.WindowsXP)
          return false;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (bool)));
        NativeMethods.SystemParametersInfo(4132, 0, num, 0);
        bool valueType = (bool) QMisc.PtrToValueType(num, typeof (bool));
        Marshal.FreeHGlobal(num);
        return valueType;
      }
    }

    public static bool LettersAlwaysUnderlined
    {
      get
      {
        if (!NativeHelper.Windows98 && !NativeHelper.Windows2000)
          return false;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (bool)));
        NativeMethods.SystemParametersInfo(4106, 0, num, 0);
        bool valueType = (bool) QMisc.PtrToValueType(num, typeof (bool));
        Marshal.FreeHGlobal(num);
        return valueType;
      }
    }

    public static int MenuShowDelay
    {
      get
      {
        if (!NativeHelper.Windows98 && !NativeHelper.WindowsNT)
          return 0;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (int)));
        NativeMethods.SystemParametersInfo(106, 0, num, 0);
        int valueType = (int) QMisc.PtrToValueType(num, typeof (int));
        Marshal.FreeHGlobal(num);
        return valueType;
      }
    }

    public static int HighOrderWord(int value) => value >> 16 & (int) ushort.MaxValue;

    public static int LowOrderWord(int value) => value & (int) ushort.MaxValue;

    public static byte LowOrderByte(short value) => (byte) ((uint) value & (uint) byte.MaxValue);

    public static byte HighOrderByte(short value) => (byte) ((uint) value >> 8);

    public static Size GetToolWindowCaptionButtonSize(bool windowsXPTheme)
    {
      Size captionButtonSize = new Size(SystemInformation.ToolWindowCaptionButtonSize.Width, SystemInformation.ToolWindowCaptionButtonSize.Height);
      if (windowsXPTheme)
        captionButtonSize.Width -= 4;
      else
        captionButtonSize.Width -= 2;
      captionButtonSize.Height -= 4;
      return captionButtonSize;
    }

    public static Size GetCaptionButtonSize(bool windowsXPTheme)
    {
      Size captionButtonSize = new Size(SystemInformation.CaptionButtonSize.Width, SystemInformation.CaptionButtonSize.Height);
      if (windowsXPTheme)
        captionButtonSize.Width -= 4;
      else
        captionButtonSize.Width -= 2;
      captionButtonSize.Height -= 4;
      return captionButtonSize;
    }

    public static void DrawDragRectangle(Rectangle rectangle, int indent)
    {
      IntPtr rectangleRegion = NativeHelper.CreateRectangleRegion(rectangle, indent);
      IntPtr dc = NativeMethods.GetDC(IntPtr.Zero);
      NativeMethods.SelectClipRgn(dc, rectangleRegion);
      NativeMethods.RECT lprc = new NativeMethods.RECT();
      NativeMethods.GetClipBox(dc, ref lprc);
      IntPtr hObject1 = NativeHelper.RetrieveHalfToneBrush();
      IntPtr hObject2 = NativeMethods.SelectObject(dc, hObject1);
      NativeMethods.PatBlt(dc, lprc.left, lprc.top, lprc.right - lprc.left, lprc.bottom - lprc.top, 5898313);
      NativeMethods.SelectObject(dc, hObject2);
      NativeMethods.SelectClipRgn(dc, IntPtr.Zero);
      NativeMethods.DeleteObject(rectangleRegion);
      NativeMethods.ReleaseDC(IntPtr.Zero, dc);
    }

    public static IntPtr CreateRectangleRegion(Rectangle rectangle, int indent)
    {
      NativeMethods.RECT rect = NativeHelper.CreateRECT(rectangle);
      IntPtr rectRgnIndirect1 = NativeMethods.CreateRectRgnIndirect(ref rect);
      if (indent <= 0 || rectangle.Width <= indent || rectangle.Height <= indent)
        return rectRgnIndirect1;
      rect.left += indent;
      rect.top += indent;
      rect.right -= indent;
      rect.bottom -= indent;
      IntPtr rectRgnIndirect2 = NativeMethods.CreateRectRgnIndirect(ref rect);
      var lprc = new NativeMethods.RECT()
      {
        left = 0,
        top = 0,
        right = 0,
        bottom = 0
      };
      IntPtr rectRgnIndirect3 = NativeMethods.CreateRectRgnIndirect(ref lprc);
      NativeMethods.CombineRgn(rectRgnIndirect3, rectRgnIndirect1, rectRgnIndirect2, 3);
      NativeMethods.DeleteObject(rectRgnIndirect1);
      NativeMethods.DeleteObject(rectRgnIndirect2);
      return rectRgnIndirect3;
    }

    public static IntPtr RetrieveHalfToneBrush()
    {
      if (NativeHelper.m_hHalfToneBrush == IntPtr.Zero)
      {
        Bitmap bitmap = new Bitmap(8, 8, PixelFormat.Format32bppArgb);
        Color color1 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        Color color2 = Color.FromArgb((int) byte.MaxValue, 0, 0, 0);
        bool flag = true;
        int x = 0;
        while (x < 8)
        {
          int y = 0;
          while (y < 8)
          {
            bitmap.SetPixel(x, y, flag ? color1 : color2);
            ++y;
            flag = !flag;
          }
          ++x;
          flag = !flag;
        }
        IntPtr hbitmap = bitmap.GetHbitmap();
        var logbrush = new NativeMethods.LOGBRUSH()
        {
          lbStyle = 3U,
          lbHatch = (uint) (int) hbitmap
        };
        NativeHelper.m_hHalfToneBrush = NativeMethods.CreateBrushIndirect(ref logbrush);
      }
      return NativeHelper.m_hHalfToneBrush;
    }

    public static bool IsTopMost(IntPtr handle) => !(handle == IntPtr.Zero) && (NativeMethods.GetWindowLong(handle, -20) & 8) == 8;

    public static bool WindowsXP => Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= NativeHelper.m_oWinXPVersion;

    public static bool Windows2000 => Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= NativeHelper.m_oWin2000Version;

    public static bool Windows98 => Environment.OSVersion.Platform == PlatformID.Win32Windows && Environment.OSVersion.Version >= NativeHelper.m_oWin98Version || NativeHelper.Windows2000;

    public static bool WindowsNT => Environment.OSVersion.Platform == PlatformID.Win32NT;

    public static unsafe Bitmap ConvertIconToBitmap(Icon icon)
    {
      if (icon == null)
        return (Bitmap) null;
      if (icon.Width == 0 || icon.Height == 0)
        return (Bitmap) null;
      NativeMethods.ICONINFO iconInfo1 = new NativeMethods.ICONINFO();
      bool iconInfo2;
      try
      {
        iconInfo2 = NativeMethods.GetIconInfo(icon.Handle, ref iconInfo1);
      }
      catch
      {
        return (Bitmap) null;
      }
      if (!iconInfo2)
        return icon.ToBitmap();
      if (!iconInfo1.fIcon)
      {
        if (iconInfo1.hbmColor != IntPtr.Zero)
          NativeMethods.DeleteObject(iconInfo1.hbmColor);
        if (iconInfo1.hbmMask != IntPtr.Zero)
          NativeMethods.DeleteObject(iconInfo1.hbmMask);
        return icon.ToBitmap();
      }
      if (iconInfo1.hbmColor == IntPtr.Zero)
      {
        if (iconInfo1.hbmColor != IntPtr.Zero)
          NativeMethods.DeleteObject(iconInfo1.hbmColor);
        return icon.ToBitmap();
      }
      if (iconInfo1.hbmMask == IntPtr.Zero)
      {
        if (iconInfo1.hbmMask != IntPtr.Zero)
          NativeMethods.DeleteObject(iconInfo1.hbmMask);
        return icon.ToBitmap();
      }
      Rectangle rect = new Rectangle(0, 0, icon.Width, icon.Height);
      Bitmap bitmap1 = Image.FromHbitmap(iconInfo1.hbmColor);
      bool flag = bitmap1 != null;
      Bitmap bitmap2 = (Bitmap) null;
      if (flag)
      {
        bitmap2 = Image.FromHbitmap(iconInfo1.hbmMask);
        flag = bitmap2 != null;
      }
      Bitmap bitmap3 = (Bitmap) null;
      if (flag)
      {
        bitmap3 = new Bitmap(bitmap1.Width, bitmap1.Height, PixelFormat.Format32bppArgb);
        flag = bitmap3 != null;
      }
      BitmapData bitmapdata1 = (BitmapData) null;
      if (flag)
      {
        bitmapdata1 = bitmap1.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
        flag = bitmapdata1 != null;
      }
      BitmapData bitmapdata2 = (BitmapData) null;
      if (flag)
      {
        bitmapdata2 = bitmap2.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
        flag = bitmapdata2 != null;
      }
      BitmapData bitmapdata3 = (BitmapData) null;
      if (flag)
      {
        bitmapdata3 = bitmap3.LockBits(rect, ImageLockMode.WriteOnly, bitmap3.PixelFormat);
        flag = bitmapdata3 != null;
      }
      if (flag)
      {
        uint* pointer1 = (uint*) bitmapdata1.Scan0.ToPointer();
        uint* pointer2 = (uint*) bitmapdata2.Scan0.ToPointer();
        uint* pointer3 = (uint*) bitmapdata3.Scan0.ToPointer();
        for (int index1 = 0; index1 < bitmapdata1.Height; ++index1)
        {
          for (int index2 = 0; index2 < bitmapdata1.Width; ++index2)
          {
            uint num1 = pointer1[index1 * (bitmapdata1.Stride / 4) + index2];
            uint num2 = pointer2[index1 * (bitmapdata2.Stride / 4) + index2];
            uint num3 = (((int) num1 & -16777216) == 0 ? num1 | 4278190080U : num1) & (uint) ~((int) num2 << 8 | (int) num2 & (int) byte.MaxValue);
            pointer3[index1 * (bitmapdata3.Stride / 4) + index2] = num3;
          }
        }
      }
      if (bitmap1 != null && bitmapdata1 != null)
        bitmap1.UnlockBits(bitmapdata1);
      if (bitmap2 != null && bitmapdata2 != null)
        bitmap2.UnlockBits(bitmapdata2);
      if (bitmap3 != null && bitmapdata3 != null)
        bitmap3.UnlockBits(bitmapdata3);
      if (iconInfo1.hbmColor != IntPtr.Zero)
        NativeMethods.DeleteObject(iconInfo1.hbmColor);
      if (iconInfo1.hbmMask != IntPtr.Zero)
        NativeMethods.DeleteObject(iconInfo1.hbmMask);
      return bitmap3;
    }

    public static int CreateCOLORREF(Color color) => (int) color.B << 16 | (int) color.G << 8 | (int) color.R;

    public static NativeMethods.TEXTMETRIC GetTextMetrics(Graphics graphics, Font font)
    {
      IntPtr hdc = graphics.GetHdc();
      IntPtr hfont = font.ToHfont();
      IntPtr hObject = NativeMethods.SelectObject(hdc, hfont);
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (NativeMethods.TEXTMETRIC)));
      NativeMethods.TEXTMETRIC structure;
      try
      {
        NativeMethods.GetTextMetrics(hdc, num);
        structure = (NativeMethods.TEXTMETRIC) Marshal.PtrToStructure(num, typeof (NativeMethods.TEXTMETRIC));
        Marshal.FreeHGlobal(num);
      }
      finally
      {
        NativeMethods.SelectObject(hdc, hObject);
        NativeMethods.DeleteObject(hfont);
        graphics.ReleaseHdc(hdc);
      }
      return structure;
    }

    public static int CalculateFontHeight(Graphics graphics, Font font) => NativeHelper.GetTextMetrics(graphics, font).tmHeight;

    public static int CalculateBaseLine(Graphics graphics, Font font) => NativeHelper.GetTextMetrics(graphics, font).tmAscent;

    public static Size CalculateTextSize(
      string text,
      Font font,
      int maximumWidth,
      QDrawTextOptions options,
      Graphics graphics)
    {
      Size empty = Size.Empty;
      IntPtr hfont = font.ToHfont();
      NativeMethods.RECT lpRect = new NativeMethods.RECT(0, 0, maximumWidth, (int) short.MaxValue);
      IntPtr hdc = graphics.GetHdc();
      IntPtr hObject = NativeMethods.SelectObject(hdc, hfont);
      try
      {
        int height = NativeMethods.DrawText(hdc, text, text.Length, ref lpRect, (int) (options | (QDrawTextOptions) 1024));
        return new Size(lpRect.right - lpRect.left, height);
      }
      finally
      {
        NativeMethods.SelectObject(hdc, hObject);
        NativeMethods.DeleteObject(hfont);
        graphics.ReleaseHdc(hdc);
      }
    }

    public static Size CalculateTextExtentPoint(
      string text,
      Font font,
      int angle,
      Graphics graphics)
    {
      switch (text)
      {
        case "":
        case null:
          return Size.Empty;
        default:
          IntPtr hdc = graphics.GetHdc();
          IntPtr zero = IntPtr.Zero;
          IntPtr hObject1;
          if (angle == 0)
          {
            hObject1 = font.ToHfont();
          }
          else
          {
            object logFont = (object) new NativeMethods.LOGFONT();
            font.ToLogFont(logFont);
            NativeMethods.LOGFONT lplf = (NativeMethods.LOGFONT) logFont with
            {
              lfEscapement = angle * 10
            };
            hObject1 = NativeMethods.CreateFontIndirect(ref lplf);
          }
          IntPtr hObject2 = NativeMethods.SelectObject(hdc, hObject1);
          NativeMethods.SIZE lpSize = new NativeMethods.SIZE(0, 0);
          try
          {
            NativeMethods.GetTextExtentPoint32(hdc, text, text.Length, ref lpSize);
          }
          finally
          {
            NativeMethods.SelectObject(hdc, hObject2);
            NativeMethods.DeleteObject(hObject1);
            graphics.ReleaseHdc(hdc);
          }
          return new Size(lpSize.cx, lpSize.cy);
      }
    }

    public static Size CalculateTextExtent(
      string text,
      Font font,
      int maximumWidth,
      Graphics graphics,
      out int maximumCharactersThatFit,
      out int[] stringWidths)
    {
      maximumCharactersThatFit = 0;
      stringWidths = new int[0];
      switch (text)
      {
        case "":
        case null:
          return Size.Empty;
        default:
          IntPtr hdc = graphics.GetHdc();
          IntPtr hfont = font.ToHfont();
          IntPtr hObject = NativeMethods.SelectObject(hdc, hfont);
          NativeMethods.SIZE lpSize = new NativeMethods.SIZE(0, 0);
          IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (int)) * text.Length);
          short lpnFit = 0;
          try
          {
            NativeMethods.GetTextExtentExPoint(hdc, text, text.Length, maximumWidth, ref lpnFit, num, ref lpSize);
          }
          finally
          {
            NativeMethods.SelectObject(hdc, hObject);
            NativeMethods.DeleteObject(hfont);
            graphics.ReleaseHdc(hdc);
          }
          maximumCharactersThatFit = (int) lpnFit;
          Size textExtent = new Size(lpSize.cx, lpSize.cy);
          stringWidths = new int[maximumCharactersThatFit];
          Marshal.Copy(num, stringWidths, 0, stringWidths.Length);
          Marshal.FreeHGlobal(num);
          return textExtent;
      }
    }

    public static Size CalculateTextSize(string text, Font font, Graphics graphics)
    {
      int[] stringWidths = (int[]) null;
      int maximumCharactersThatFit = 0;
      return NativeHelper.CalculateTextExtent(text, font, int.MaxValue, graphics, out maximumCharactersThatFit, out stringWidths);
    }

    public static void DrawText(
      string text,
      Font font,
      Rectangle bounds,
      Color textColor,
      QDrawTextOptions options,
      Graphics graphics)
    {
      NativeHelper.DrawText(text, font, bounds, textColor, options, 0, graphics);
    }

    public static void DrawText(
      string text,
      Font font,
      Rectangle bounds,
      Color textColor,
      QDrawTextOptions options,
      int angle,
      Graphics graphics)
    {
      NativeHelper.DrawText(text, font, bounds, textColor, options, angle, HorizontalAlignment.Left, false, graphics);
    }

    public static void DrawText(
      string text,
      Font font,
      Rectangle bounds,
      Color textColor,
      QDrawTextOptions options,
      int angle,
      HorizontalAlignment horizontalAlignment,
      bool wrap,
      Graphics graphics)
    {
      switch (text)
      {
        case "":
          break;
        case null:
          break;
        default:
          bounds = new Rectangle((int) NativeHelper.SecureAsShort(bounds.Left), (int) NativeHelper.SecureAsShort(bounds.Top), (int) NativeHelper.SecureAsShort(bounds.Width), (int) NativeHelper.SecureAsShort(bounds.Height));
          int colorref = NativeHelper.CreateCOLORREF(textColor);
          bounds.Offset((int) graphics.Transform.OffsetX, (int) graphics.Transform.OffsetY);
          IntPtr hrgn = graphics.Clip.GetHrgn(graphics);
          IntPtr hdc = graphics.GetHdc();
          IntPtr zero = IntPtr.Zero;
          IntPtr hObject1;
          if (angle == 0)
          {
            hObject1 = font.ToHfont();
          }
          else
          {
            object logFont = (object) new NativeMethods.LOGFONT();
            font.ToLogFont(logFont);
            NativeMethods.LOGFONT lplf = (NativeMethods.LOGFONT) logFont with
            {
              lfEscapement = angle * 10
            };
            hObject1 = NativeMethods.CreateFontIndirect(ref lplf);
          }
          NativeMethods.SelectClipRgn(hdc, hrgn);
          IntPtr hObject2 = NativeMethods.SelectObject(hdc, hObject1);
          int crColor = NativeMethods.SetTextColor(hdc, colorref);
          int iBkMode = NativeMethods.SetBkMode(hdc, 1);
          NativeMethods.RECT rect = NativeHelper.CreateRECT(bounds);
          int uFormat = angle == 0 ? (int) options : (int) (options | (QDrawTextOptions) 256);
          switch (horizontalAlignment)
          {
            case HorizontalAlignment.Right:
              uFormat |= 2;
              break;
            case HorizontalAlignment.Center:
              uFormat |= 1;
              break;
          }
          if (wrap)
            uFormat |= 16;
          try
          {
            NativeMethods.DrawText(hdc, text, text.Length, ref rect, uFormat);
            break;
          }
          finally
          {
            NativeMethods.SelectClipRgn(hdc, IntPtr.Zero);
            NativeMethods.SetBkMode(hdc, iBkMode);
            NativeMethods.SetTextColor(hdc, crColor);
            NativeMethods.SelectObject(hdc, hObject2);
            NativeMethods.DeleteObject(hObject1);
            NativeMethods.DeleteObject(hrgn);
            graphics.ReleaseHdc(hdc);
          }
      }
    }

    public static NativeMethods.NONCLIENTMETRICS GetNonClientMetrics()
    {
      NativeMethods.NONCLIENTMETRICS structure = new NativeMethods.NONCLIENTMETRICS();
      structure.cbSize = Marshal.SizeOf((object) structure);
      IntPtr num = Marshal.AllocHGlobal(structure.cbSize);
      Marshal.StructureToPtr((object) structure, num, true);
      NativeMethods.SystemParametersInfo(41, structure.cbSize, num, 0);
      NativeMethods.NONCLIENTMETRICS valueType = (NativeMethods.NONCLIENTMETRICS) QMisc.PtrToValueType(num, typeof (NativeMethods.NONCLIENTMETRICS));
      Marshal.FreeHGlobal(num);
      return valueType;
    }

    public static Font GetCaptionFont(IntPtr windowsXPTheme)
    {
      NativeMethods.LOGFONT pFont;
      if (windowsXPTheme != IntPtr.Zero)
        NativeMethods.GetThemeSysFont(windowsXPTheme, 801, out pFont);
      else
        pFont = NativeHelper.GetNonClientMetrics().lfCaptionFont;
      return Font.FromLogFont((object) pFont);
    }

    public static Font GetSmallCaptionFont(IntPtr windowsXPTheme)
    {
      NativeMethods.LOGFONT pFont;
      if (windowsXPTheme != IntPtr.Zero)
        NativeMethods.GetThemeSysFont(windowsXPTheme, 802, out pFont);
      else
        pFont = NativeHelper.GetNonClientMetrics().lfSmCaptionFont;
      return Font.FromLogFont((object) pFont);
    }

    internal static MouseButtons GetXButton(int wparam)
    {
      switch (wparam)
      {
        case 1:
          return MouseButtons.XButton1;
        case 2:
          return MouseButtons.XButton2;
        default:
          return MouseButtons.None;
      }
    }

    internal static IntPtr AllocateMemoryInOtherProcess(
      IntPtr hwndWindowHandle,
      int size,
      ref IntPtr processHandle)
    {
      if (processHandle == IntPtr.Zero)
      {
        int lpdwProcessId = 0;
        NativeMethods.GetWindowThreadProcessId(hwndWindowHandle, ref lpdwProcessId);
        processHandle = lpdwProcessId != 0 ? NativeMethods.OpenProcess(56, false, lpdwProcessId) : throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetWindowThreadProcessID"));
        if (processHandle == IntPtr.Zero)
          throw new InvalidOperationException(QResources.GetException("General_ReturnValueWasWindowsError", (object) "OpenProcess", (object) Marshal.GetLastWin32Error()));
      }
      IntPtr num = NativeMethods.VirtualAllocEx(processHandle, IntPtr.Zero, size, 4096, 4);
      if (num == IntPtr.Zero)
      {
        if (processHandle != IntPtr.Zero)
          NativeHelper.CloseProcessHandle(processHandle);
        throw new InvalidOperationException(QResources.GetException("General_ReturnValueWasWindowsError", (object) "VirtualAllocEx", (object) Marshal.GetLastWin32Error()));
      }
      return num;
    }

    internal static object ReadMemoryFromOtherProcess(
      IntPtr processHandle,
      IntPtr memoryHandle,
      System.Type objectType)
    {
      int num1 = Marshal.SizeOf(objectType);
      IntPtr num2 = Marshal.AllocHGlobal(num1);
      int lpNumberOfBytesWritten = 0;
      if (!NativeMethods.ReadProcessMemory(processHandle, memoryHandle, num2, num1, ref lpNumberOfBytesWritten) || lpNumberOfBytesWritten != num1)
      {
        Marshal.FreeHGlobal(num2);
        throw new InvalidOperationException(QResources.GetException("General_ReturnValueWasWindowsError", (object) "ReadProcessMemory", (object) Marshal.GetLastWin32Error()));
      }
      try
      {
        return Marshal.PtrToStructure(num2, objectType);
      }
      finally
      {
        Marshal.FreeHGlobal(num2);
      }
    }

    internal static void FreeMemory(IntPtr processHandle, IntPtr memoryHandle)
    {
      if (memoryHandle != IntPtr.Zero && processHandle != IntPtr.Zero && !NativeMethods.VirtualFreeEx(processHandle, memoryHandle, 0, 32768))
        throw new InvalidOperationException(QResources.GetException("General_ReturnValueWasWindowsError", (object) "VirtualFreeEx", (object) Marshal.GetLastWin32Error()));
    }

    internal static void CloseProcessHandle(IntPtr processHandle)
    {
      if (processHandle != IntPtr.Zero && !NativeMethods.CloseHandle(processHandle))
        throw new InvalidOperationException(QResources.GetException("General_ReturnValueWasWindowsError", (object) "CloseHandle", (object) Marshal.GetLastWin32Error()));
    }
  }
}
