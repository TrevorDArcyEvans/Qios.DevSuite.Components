// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QOffscreenBitmapSet
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Qios.DevSuite.Components
{
  internal class QOffscreenBitmapSet : IDisposable
  {
    private int m_iLocked;
    private bool m_bIsDisposed;
    private static int m_iMinimumControlSize = 100;
    private Size m_oOffscreen32BitNativeBitmapSize = Size.Empty;
    private IntPtr m_hOffscreen32BitNativeBitmap = IntPtr.Zero;
    private Size m_oOffscreenDesktopBitmapSize = Size.Empty;
    private IntPtr m_hOffscreenDesktopBitmap = IntPtr.Zero;
    private Size m_oOffscreenMaskBitmapSize;
    private IntPtr m_hOffscreenMaskBitmap = IntPtr.Zero;
    private Bitmap m_oOffscreen32BitBitmap;

    internal QOffscreenBitmapSet()
    {
    }

    internal bool Locked
    {
      get => this.m_iLocked == 1;
      set => Interlocked.Exchange(ref this.m_iLocked, value ? 1 : 0);
    }

    internal bool Disposed => this.m_bIsDisposed;

    internal IntPtr OffscreenDesktopBitmap => this.m_hOffscreenDesktopBitmap;

    internal Size OffscreenDesktopBitmapSize => this.m_oOffscreenDesktopBitmapSize;

    internal IntPtr SecureOffscreenDesktopBitmap(Size controlSize)
    {
      if (controlSize.Width > this.m_oOffscreenDesktopBitmapSize.Width || controlSize.Height > this.m_oOffscreenDesktopBitmapSize.Height)
      {
        if (this.m_hOffscreenDesktopBitmap != IntPtr.Zero)
          NativeMethods.DeleteObject(this.m_hOffscreenDesktopBitmap);
        this.m_oOffscreenDesktopBitmapSize = new Size(controlSize.Width + QOffscreenBitmapSet.m_iMinimumControlSize, controlSize.Height + QOffscreenBitmapSet.m_iMinimumControlSize);
        IntPtr dc = NativeMethods.GetDC(IntPtr.Zero);
        this.m_hOffscreenDesktopBitmap = NativeMethods.CreateCompatibleBitmap(dc, this.m_oOffscreenDesktopBitmapSize.Width, this.m_oOffscreenDesktopBitmapSize.Height);
        NativeMethods.ReleaseDC(IntPtr.Zero, dc);
      }
      GC.KeepAlive((object) this);
      return this.m_hOffscreenDesktopBitmap;
    }

    internal IntPtr SecureOffscreenMaskBitmap(Size controlSize)
    {
      if (controlSize.Width > this.m_oOffscreenMaskBitmapSize.Width || controlSize.Height > this.m_oOffscreenMaskBitmapSize.Height)
      {
        if (this.m_hOffscreenMaskBitmap != IntPtr.Zero)
          NativeMethods.DeleteObject(this.m_hOffscreenMaskBitmap);
        this.m_oOffscreenMaskBitmapSize = new Size(controlSize.Width + QOffscreenBitmapSet.m_iMinimumControlSize, controlSize.Height + QOffscreenBitmapSet.m_iMinimumControlSize);
        this.m_hOffscreenMaskBitmap = NativeMethods.CreateBitmap(this.m_oOffscreenMaskBitmapSize.Width, this.m_oOffscreenMaskBitmapSize.Height, 1, 1, (short[]) null);
      }
      GC.KeepAlive((object) this);
      return this.m_hOffscreenMaskBitmap;
    }

    internal Bitmap SecureOffscreen32BitBitmap(Size controlSize)
    {
      if (this.m_oOffscreen32BitBitmap == null || controlSize.Width > this.m_oOffscreen32BitBitmap.Width || controlSize.Height > this.m_oOffscreen32BitBitmap.Height)
      {
        if (this.m_oOffscreen32BitBitmap != null)
          this.m_oOffscreen32BitBitmap.Dispose();
        this.m_oOffscreen32BitBitmap = new Bitmap(controlSize.Width + QOffscreenBitmapSet.m_iMinimumControlSize, controlSize.Height + QOffscreenBitmapSet.m_iMinimumControlSize, PixelFormat.Format32bppArgb);
      }
      return this.m_oOffscreen32BitBitmap;
    }

    internal IntPtr SecureOffscreen32BitNativeBitmap(Size controlSize)
    {
      if (controlSize.Width > this.m_oOffscreen32BitNativeBitmapSize.Width || controlSize.Height > this.m_oOffscreen32BitNativeBitmapSize.Height)
      {
        if (this.m_hOffscreen32BitNativeBitmap != IntPtr.Zero)
          NativeMethods.DeleteObject(this.m_hOffscreen32BitNativeBitmap);
        this.m_oOffscreen32BitNativeBitmapSize = new Size(controlSize.Width + QOffscreenBitmapSet.m_iMinimumControlSize, controlSize.Height + QOffscreenBitmapSet.m_iMinimumControlSize);
        Bitmap bitmap = new Bitmap(this.m_oOffscreen32BitNativeBitmapSize.Width, this.m_oOffscreen32BitNativeBitmapSize.Height, PixelFormat.Format32bppArgb);
        this.m_hOffscreen32BitNativeBitmap = bitmap.GetHbitmap();
        bitmap.Dispose();
      }
      return this.m_hOffscreen32BitNativeBitmap;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (disposing && this.m_oOffscreen32BitBitmap != null)
        this.m_oOffscreen32BitBitmap.Dispose();
      if (this.m_hOffscreenDesktopBitmap != IntPtr.Zero)
      {
        NativeMethods.DeleteObject(this.m_hOffscreenDesktopBitmap);
        this.m_hOffscreenDesktopBitmap = IntPtr.Zero;
      }
      if (this.m_hOffscreen32BitNativeBitmap != IntPtr.Zero)
      {
        NativeMethods.DeleteObject(this.m_hOffscreen32BitNativeBitmap);
        this.m_hOffscreen32BitNativeBitmap = IntPtr.Zero;
      }
      if (this.m_hOffscreenMaskBitmap != IntPtr.Zero)
      {
        NativeMethods.DeleteObject(this.m_hOffscreenMaskBitmap);
        this.m_hOffscreenMaskBitmap = IntPtr.Zero;
      }
      this.m_bIsDisposed = true;
      GC.KeepAlive((object) this);
    }

    ~QOffscreenBitmapSet() => this.Dispose(false);
  }
}
