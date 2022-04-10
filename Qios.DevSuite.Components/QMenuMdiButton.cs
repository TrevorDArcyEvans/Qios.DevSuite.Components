// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuMdiButton
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Qios.DevSuite.Components
{
  internal class QMenuMdiButton
  {
    private QMenuMdiButtons m_oParentMdiButtons;
    private QMenuMdiButtonType m_eButtonType;
    private QButtonState m_eButtonState;
    private Size m_oSize;
    private Size m_oRequestedSize;
    private Point m_oLocation;

    public QMenuMdiButton(QMenuMdiButtons parentMdiButtons, QMenuMdiButtonType buttonType)
    {
      this.m_oParentMdiButtons = parentMdiButtons;
      this.m_eButtonType = buttonType;
      this.m_eButtonState = QButtonState.Normal;
    }

    public int Left => this.m_oLocation.X;

    public int Top => this.m_oLocation.Y;

    public int Width => this.m_oSize.Width;

    public int Height => this.m_oSize.Height;

    public Rectangle Bounds => new Rectangle(this.m_oLocation, this.m_oSize);

    public Size Size
    {
      get => this.m_oSize;
      set => this.m_oSize = value;
    }

    public Size RequestedSize => this.m_oRequestedSize;

    public Point Location
    {
      get => this.m_oLocation;
      set => this.m_oLocation = value;
    }

    public QButtonState ButtonState
    {
      get => this.m_eButtonState;
      set => this.m_eButtonState = value;
    }

    public bool IsCloseButton => this.m_eButtonType == QMenuMdiButtonType.Close;

    public bool IsRestoreButton => this.m_eButtonType == QMenuMdiButtonType.Restore;

    public bool IsMimimizeButton => this.m_eButtonType == QMenuMdiButtonType.Minimize;

    public bool IsVisible => this.MainMenuConfiguration == null || (!this.IsCloseButton || this.MainMenuConfiguration.MdiCloseButtonVisible) && (!this.IsRestoreButton || this.MainMenuConfiguration.MdiRestoreButtonVisible) && (!this.IsMimimizeButton || this.MainMenuConfiguration.MdiMinimizeButtonVisible);

    public QMainMenu MainMenu => this.m_oParentMdiButtons != null ? this.m_oParentMdiButtons.MainMenu : (QMainMenu) null;

    public QMainMenuConfiguration MainMenuConfiguration => this.MainMenu != null ? this.MainMenu.Configuration : (QMainMenuConfiguration) null;

    public static int GetWinButtonType(QMenuMdiButtonType buttonType)
    {
      switch (buttonType)
      {
        case QMenuMdiButtonType.Minimize:
          return 1;
        case QMenuMdiButtonType.Restore:
          return 3;
        case QMenuMdiButtonType.Close:
          return 0;
        default:
          return 0;
      }
    }

    public static int GetWinXpButtonState(bool enabled, QButtonState buttonState)
    {
      if (!enabled)
        return 4;
      switch (buttonState)
      {
        case QButtonState.Normal:
          return 1;
        case QButtonState.Hot:
          return 2;
        case QButtonState.Pressed:
          return 3;
        default:
          return 0;
      }
    }

    public static int GetWinXpButtonPart(QMenuMdiButtonType buttonType)
    {
      switch (buttonType)
      {
        case QMenuMdiButtonType.Minimize:
          return 16;
        case QMenuMdiButtonType.Restore:
          return 22;
        case QMenuMdiButtonType.Close:
          return 20;
        default:
          return 0;
      }
    }

    public void EmptyCachedObjects()
    {
      this.m_oLocation = Point.Empty;
      this.m_oSize = Size.Empty;
      this.m_oRequestedSize = Size.Empty;
    }

    public void CalculateRequestedSize(Graphics graphics, Size requestedSize)
    {
      if (!this.IsVisible || this.MainMenuConfiguration == null)
        return;
      if (this.MainMenuConfiguration.MdiButtonsStyle == QMainMenuMdiButtonsStyle.Windows)
      {
        if (this.m_oParentMdiButtons.HasWindowsXPTheme)
        {
          int winXpButtonPart = QMenuMdiButton.GetWinXpButtonPart(this.m_eButtonType);
          int winXpButtonState = QMenuMdiButton.GetWinXpButtonState(true, this.m_eButtonState);
          NativeMethods.SIZE psz = new NativeMethods.SIZE();
          NativeMethods.RECT rect = NativeHelper.CreateRECT(0, 0, requestedSize.Width, requestedSize.Height);
          IntPtr hdc = graphics.GetHdc();
          Marshal.ThrowExceptionForHR(NativeMethods.GetThemePartSize(this.m_oParentMdiButtons.WindowsXPTheme, hdc, winXpButtonPart, winXpButtonState, ref rect, NativeMethods.THEMESIZE.TS_DRAW, ref psz));
          graphics.ReleaseHdc(hdc);
          this.m_oRequestedSize = new Size(psz.cx, psz.cy);
        }
        else
          this.m_oRequestedSize = NativeHelper.GetCaptionButtonSize(false);
      }
      else
        this.m_oRequestedSize = requestedSize;
    }

    public void Draw(Graphics graphics)
    {
      if (!this.IsVisible || this.MainMenuConfiguration == null)
        return;
      if (this.MainMenuConfiguration.MdiButtonsStyle == QMainMenuMdiButtonsStyle.Windows)
      {
        IntPtr hdc = graphics.GetHdc();
        NativeMethods.RECT rect = NativeHelper.CreateRECT(this.Left, this.Top, this.Width, this.Height);
        if (this.m_oParentMdiButtons.HasWindowsXPTheme)
        {
          int winXpButtonPart = QMenuMdiButton.GetWinXpButtonPart(this.m_eButtonType);
          int winXpButtonState = QMenuMdiButton.GetWinXpButtonState(true, this.m_eButtonState);
          NativeMethods.DrawThemeBackground(this.m_oParentMdiButtons.WindowsXPTheme, hdc, winXpButtonPart, winXpButtonState, ref rect, ref rect);
        }
        else
        {
          int winButtonType = QMenuMdiButton.GetWinButtonType(this.m_eButtonType);
          int num = this.m_eButtonState == QButtonState.Pressed ? 512 : 0;
          NativeMethods.DrawFrameControl(hdc, ref rect, 1, winButtonType | num);
        }
        graphics.ReleaseHdc(hdc);
      }
      else
      {
        if (this.m_eButtonState == QButtonState.Hot || this.m_eButtonState == QButtonState.Pressed)
          QRectanglePainter.Default.Paint(this.Bounds, (IQAppearance) new QAppearanceWrapper((IQAppearance) null)
          {
            GradientAngle = this.MainMenu.MenuItemGradientAngle
          }, new QColorSet((Color) this.MainMenu.ColorScheme.MainMenuHotItemBackground1, (Color) this.MainMenu.ColorScheme.MainMenuHotItemBackground2, (Color) this.MainMenu.ColorScheme.MainMenuHotItemBorder), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        Image image = (Image) null;
        if (this.IsCloseButton)
          image = QControlPaint.CreateColorizedImage(this.MainMenuConfiguration.UsedCustomMdiCloseButtonMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.MainMenu.ColorScheme.MenuText);
        else if (this.IsMimimizeButton)
          image = QControlPaint.CreateColorizedImage(this.MainMenuConfiguration.UsedCustomMdiMinimizeButtonMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.MainMenu.ColorScheme.MenuText);
        else if (this.IsRestoreButton)
          image = QControlPaint.CreateColorizedImage(this.MainMenuConfiguration.UsedCustomMdiRestoreButtonMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) this.MainMenu.ColorScheme.MenuText);
        QControlPaint.DrawImage(image, QImageAlign.Centered, this.Bounds, this.Size, graphics);
      }
    }
  }
}
