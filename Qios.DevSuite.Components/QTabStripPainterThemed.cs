// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStripPainterThemed
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QTabStripPainterThemed : QTabStripPainter
  {
    protected override Size CalculateButtonSize(QTabButton tabButton, Graphics graphics) => base.CalculateButtonSize(tabButton, graphics);

    protected override void DrawTabButtonBackground(
      QTabButton button,
      QTabButtonConfiguration buttonConfiguration,
      QTabStripConfiguration stripConfiguration,
      QTabButtonAppearance buttonAppearance,
      Rectangle buttonBounds,
      Rectangle controlAndButtonBounds,
      DockStyle dockStyle,
      Color backColor1,
      Color backColor2,
      Color borderColor,
      Graphics graphics)
    {
      Rectangle rectangle = buttonBounds;
      Border3DSide sides = Border3DSide.All;
      switch (dockStyle)
      {
        case DockStyle.Top:
          sides = Border3DSide.Left | Border3DSide.Top | Border3DSide.Right;
          if (button.IsActivated)
          {
            rectangle.Y -= 2;
            rectangle.Height += 3;
            break;
          }
          break;
        case DockStyle.Bottom:
          sides = Border3DSide.Left | Border3DSide.Right | Border3DSide.Bottom;
          if (button.IsActivated)
          {
            --rectangle.Y;
            rectangle.Height += 3;
            break;
          }
          break;
        case DockStyle.Left:
          sides = Border3DSide.Left | Border3DSide.Top | Border3DSide.Bottom;
          if (button.IsActivated)
          {
            rectangle.X -= 2;
            rectangle.Width += 3;
            break;
          }
          break;
        case DockStyle.Right:
          sides = Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom;
          if (button.IsActivated)
          {
            --rectangle.X;
            rectangle.Width += 3;
            break;
          }
          break;
      }
      if (this.WindowsXPThemeHandle != IntPtr.Zero)
      {
        int iStateId = 1;
        if (!button.Enabled)
          iStateId = 4;
        else if (button.IsActivated)
          iStateId = 3;
        else if (button.IsHot)
          iStateId = 2;
        if (!button.TabStrip.IsHorizontal)
        {
          Size size1 = new Size(rectangle.Height, rectangle.Width);
        }
        else
        {
          Size size2 = rectangle.Size;
        }
        NativeMethods.RECT rect1 = NativeHelper.CreateRECT(rectangle);
        NativeMethods.RECT rect2 = NativeHelper.CreateRECT((int) graphics.ClipBounds.Left, (int) graphics.ClipBounds.Top, (int) graphics.ClipBounds.Width, (int) graphics.ClipBounds.Height);
        IntPtr hdc = graphics.GetHdc();
        NativeMethods.DrawThemeBackground(this.WindowsXPThemeHandle, hdc, 5, iStateId, ref rect1, ref rect2);
        graphics.ReleaseHdc(hdc);
      }
      else
      {
        graphics.FillRectangle(SystemBrushes.Control, rectangle);
        ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.RaisedInner, sides);
      }
    }

    protected override void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
      base.Dispose(disposing);
    }
  }
}
