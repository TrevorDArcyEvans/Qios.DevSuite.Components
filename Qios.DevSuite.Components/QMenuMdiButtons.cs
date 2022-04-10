// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMenuMdiButtons
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QMenuMdiButtons : Component
  {
    private const int m_iMinimizeButtonIndex = 0;
    private const int m_iRestoreButtonIndex = 1;
    private const int m_iCloseButtonIndex = 2;
    private const int m_iButtonCount = 3;
    private QMenuMdiButton[] m_aButtons;
    private Size m_oSize;
    private Size m_oRequestedSize;
    private QMainMenu m_oMainMenu;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private bool m_bWindowsXPThemeTried;

    public QMenuMdiButtons(QMainMenu mainMenu, IContainer components)
    {
      components.Add((IComponent) this);
      this.m_oMainMenu = mainMenu;
      this.m_oMainMenu.WindowsXPThemeChanged += new EventHandler(this.MainMenu_WindowsXPThemeChanged);
      this.m_aButtons = new QMenuMdiButton[3];
      this.m_aButtons[0] = new QMenuMdiButton(this, QMenuMdiButtonType.Minimize);
      this.m_aButtons[1] = new QMenuMdiButton(this, QMenuMdiButtonType.Restore);
      this.m_aButtons[2] = new QMenuMdiButton(this, QMenuMdiButtonType.Close);
    }

    public QMainMenu MainMenu => this.m_oMainMenu == null || this.m_oMainMenu.IsDisposed ? (QMainMenu) null : this.m_oMainMenu;

    public bool HasWindowsXPTheme
    {
      get
      {
        this.SecureWindowsXpTheme();
        return this.m_hWindowsXPTheme != IntPtr.Zero;
      }
    }

    public IntPtr WindowsXPTheme
    {
      get
      {
        this.SecureWindowsXpTheme();
        return this.m_hWindowsXPTheme;
      }
    }

    public Size RequestedSize => this.m_oRequestedSize;

    public Size Size => this.m_oSize;

    public Point Location => this[0].Location;

    public QMenuMdiButton GetButtonAtLocation(int x, int y)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Bounds.Contains(x, y))
          return this[index];
      }
      return (QMenuMdiButton) null;
    }

    public void SetAllButtonStatesTo(QButtonState buttonState, bool refreshMainMenu)
    {
      bool flag = false;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ButtonState != buttonState)
        {
          this[index].ButtonState = buttonState;
          flag = true;
        }
      }
      if (!refreshMainMenu || !flag)
        return;
      this.RefreshMainMenu();
    }

    public void SetButtonToState(QMenuMdiButton button, QButtonState buttonState)
    {
      if (button.ButtonState == buttonState)
        return;
      this.SetAllButtonStatesTo(QButtonState.Normal, false);
      button.ButtonState = buttonState;
      this.RefreshMainMenu();
    }

    private void RefreshMainMenu()
    {
      if (this.MainMenu == null)
        return;
      this.MainMenu.Invalidate(new Rectangle(this.Location, this.Size), false);
    }

    private void SecureWindowsXpTheme()
    {
      if (this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.m_oMainMenu.Handle, "Window");
      this.m_bWindowsXPThemeTried = true;
    }

    public void CloseWindowsXpTheme()
    {
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        NativeMethods.CloseThemeData(this.m_hWindowsXPTheme);
        this.m_hWindowsXPTheme = IntPtr.Zero;
      }
      this.m_bWindowsXPThemeTried = false;
    }

    public void SetBounds(Rectangle bounds)
    {
      bool flag = this.m_oMainMenu.Orientation == QCommandContainerOrientation.Vertical;
      int left = bounds.Left;
      int top = bounds.Top;
      this.m_oSize = bounds.Size;
      Size size = Size.Empty;
      for (int index = 0; index < this.Count; ++index)
      {
        if (flag)
        {
          size = bounds.Height != this.m_oRequestedSize.Height ? new Size(bounds.Width, bounds.Height / this.Count) : new Size(bounds.Width, this[index].RequestedSize.Height);
          this[index].Location = new Point(left, top);
          this[index].Size = size;
          top += this[index].Size.Height;
        }
        else
        {
          size = bounds.Width != this.m_oRequestedSize.Width ? new Size(bounds.Width / this.Count, bounds.Height) : new Size(this[index].RequestedSize.Width, bounds.Height);
          this[index].Location = new Point(left, top);
          this[index].Size = size;
          left += this[index].Size.Width;
        }
      }
    }

    public void CalculateRequestedSize(Size requestedSize)
    {
      int num1 = 0;
      int num2 = 0;
      int visibleButtonsCount = this.VisibleButtonsCount;
      this.EmptyCachedObjects();
      if (visibleButtonsCount == 0)
        return;
      bool flag = this.m_oMainMenu.Orientation == QCommandContainerOrientation.Vertical;
      Size requestedSize1 = Size.Empty;
      requestedSize1 = !flag ? new Size(requestedSize.Width / visibleButtonsCount, requestedSize.Height) : new Size(requestedSize.Width, requestedSize.Height / visibleButtonsCount);
      Graphics graphics = this.m_oMainMenu.CreateGraphics();
      for (int index = 0; index < this.Count; ++index)
      {
        this[index].CalculateRequestedSize(graphics, requestedSize1);
        if (flag)
        {
          num1 += this[index].RequestedSize.Height;
          num2 = Math.Max(num2, this[index].RequestedSize.Width);
        }
        else
        {
          num2 += this[index].RequestedSize.Width;
          num1 = Math.Max(num1, this[index].RequestedSize.Height);
        }
      }
      this.m_oRequestedSize = new Size(num2, num1);
      graphics.Dispose();
    }

    public void EmptyCachedObjects()
    {
      this.m_oRequestedSize = Size.Empty;
      this.m_oSize = Size.Empty;
      for (int index = 0; index < this.Count; ++index)
        this[index].EmptyCachedObjects();
    }

    public QMenuMdiButton this[int index] => this.m_aButtons[index];

    public int Count => this.m_aButtons.Length;

    public bool IsVisible => this.VisibleButtonsCount != 0;

    public int VisibleButtonsCount
    {
      get
      {
        int visibleButtonsCount = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].IsVisible)
            ++visibleButtonsCount;
        }
        return visibleButtonsCount;
      }
    }

    public void Draw(Graphics graphics)
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].Draw(graphics);
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        this.CloseWindowsXpTheme();
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    private void MainMenu_WindowsXPThemeChanged(object sender, EventArgs e) => this.CloseWindowsXpTheme();
  }
}
