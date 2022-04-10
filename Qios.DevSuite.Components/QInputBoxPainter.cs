// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QInputBoxPainter : IDisposable
  {
    private IWin32Window m_oWin32Window;
    private int m_iThemedBorderSize;
    private bool m_bWindowsXPThemeTried;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private IntPtr m_hButtonWindowsXPTheme = IntPtr.Zero;

    public virtual void LayoutInputBox(QInputBox inputBox, QInputBoxPaintParams paintParams)
    {
      this.SecureWindowsXpTheme(inputBox);
      QMargin qmargin = new QMargin();
      QPadding qpadding;
      if (inputBox.Configuration.InputBoxStyle == QButtonStyle.Custom)
      {
        qpadding = inputBox.Configuration.InputBoxPadding;
        if (qpadding.Left > 0)
          qmargin.Left += inputBox.Configuration.InputBoxPadding.Left;
        if (inputBox.Configuration.InputBoxPadding.Right > 0)
          qmargin.Right += inputBox.Configuration.InputBoxPadding.Right;
        if (inputBox.Configuration.InputBoxPadding.Top > 0)
          qmargin.Top += inputBox.Configuration.InputBoxPadding.Top;
        if (inputBox.Configuration.InputBoxPadding.Bottom > 0)
          qmargin.Bottom += inputBox.Configuration.InputBoxPadding.Bottom;
        qmargin.Left += inputBox.Appearance.Shape.ContentBounds.Left;
        qmargin.Right += inputBox.Appearance.Shape.Size.Width - inputBox.Appearance.Shape.ContentBounds.Right;
        qmargin.Top += inputBox.Appearance.Shape.ContentBounds.Top;
        qmargin.Bottom += inputBox.Appearance.Shape.Size.Height - inputBox.Appearance.Shape.ContentBounds.Bottom;
      }
      else if (inputBox.Configuration.InputBoxStyle == QButtonStyle.Windows)
      {
        qmargin.Left += SystemInformation.Border3DSize.Width;
        qmargin.Right += SystemInformation.Border3DSize.Width;
        qmargin.Top += SystemInformation.Border3DSize.Height;
        qmargin.Bottom += SystemInformation.Border3DSize.Height;
      }
      QMargin buttonMargin;
      Rectangle rectangle1;
      if (inputBox.Configuration.InputStyle == QInputBoxStyle.DropDown || inputBox.Configuration.InputStyle == QInputBoxStyle.DropDownList || inputBox.Configuration.InputStyle == QInputBoxStyle.UpDown)
      {
        if (inputBox.Configuration.InputBoxStyle == QButtonStyle.Custom)
        {
          QShape shape = inputBox.Configuration.ButtonAppearance.Shape;
          buttonMargin = inputBox.Configuration.ButtonMargin;
          int num = buttonMargin.Horizontal + inputBox.Configuration.ButtonPadding.Horizontal + inputBox.Configuration.UsedButtonMask.Width + (shape.Size.Width - shape.ContentBounds.Width);
          if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.RightOutside)
          {
            paintParams.DropDownButtonBounds = new Rectangle(inputBox.Width - (num - inputBox.Configuration.ButtonMargin.Left), inputBox.Configuration.ButtonMargin.Top, num - inputBox.Configuration.ButtonMargin.Horizontal, inputBox.Height - inputBox.Configuration.ButtonMargin.Vertical);
            qmargin.Right += num;
          }
          else if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.LeftOutside)
          {
            paintParams.DropDownButtonBounds = new Rectangle(inputBox.Configuration.ButtonMargin.Left, inputBox.Configuration.ButtonMargin.Top, num - inputBox.Configuration.ButtonMargin.Horizontal, inputBox.Height - inputBox.Configuration.ButtonMargin.Vertical);
            qmargin.Left += num;
          }
          else
          {
            paintParams.DropDownButtonBounds = new Rectangle(0, qmargin.Top + inputBox.Configuration.ButtonMargin.Top, num - inputBox.Configuration.ButtonMargin.Horizontal, inputBox.Height - (qmargin.Vertical + inputBox.Configuration.ButtonMargin.Vertical));
            if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.Right || inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.RightOutside)
            {
              qmargin.Right += num;
              paintParams.DropDownButtonBounds = QMath.SetX(paintParams.DropDownButtonBounds, inputBox.Width - qmargin.Right + inputBox.Configuration.ButtonMargin.Left);
            }
            else
            {
              paintParams.DropDownButtonBounds = QMath.SetX(paintParams.DropDownButtonBounds, qmargin.Left + inputBox.Configuration.ButtonMargin.Left);
              qmargin.Left += num;
            }
          }
          paintParams.DropDownImageBounds = new Rectangle(paintParams.DropDownButtonBounds.X + inputBox.Configuration.ButtonPadding.Left + shape.ContentBounds.Left, paintParams.DropDownButtonBounds.Y + inputBox.Configuration.ButtonPadding.Top + shape.ContentBounds.Top, paintParams.DropDownButtonBounds.Width - inputBox.Configuration.ButtonPadding.Horizontal - (shape.Size.Width - shape.ContentBounds.Width), paintParams.DropDownButtonBounds.Height - inputBox.Configuration.ButtonPadding.Vertical - (shape.Size.Height - shape.ContentBounds.Height));
        }
        else if (this.m_hWindowsXPTheme != IntPtr.Zero && this.m_hButtonWindowsXPTheme != IntPtr.Zero)
        {
          if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.LeftOutside)
          {
            paintParams.DropDownButtonBounds = new Rectangle(0, 0, SystemInformation.VerticalScrollBarWidth, inputBox.Height);
            qmargin.Left += SystemInformation.VerticalScrollBarWidth;
          }
          else if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.RightOutside)
          {
            paintParams.DropDownButtonBounds = new Rectangle(inputBox.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, inputBox.Height);
            qmargin.Right += SystemInformation.VerticalScrollBarWidth;
          }
          else
          {
            int num = this.m_iThemedBorderSize + SystemInformation.VerticalScrollBarWidth;
            paintParams.DropDownButtonBounds = new Rectangle(0, qmargin.Top + this.m_iThemedBorderSize - SystemInformation.Border3DSize.Height, SystemInformation.VerticalScrollBarWidth, SystemInformation.Border3DSize.Height * 2 + (inputBox.Height - qmargin.Vertical) - this.m_iThemedBorderSize * 2);
            if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.Right)
            {
              qmargin.Right += num;
              paintParams.DropDownButtonBounds = QMath.SetX(paintParams.DropDownButtonBounds, inputBox.Width - qmargin.Right + SystemInformation.Border3DSize.Width);
            }
            else
            {
              paintParams.DropDownButtonBounds = QMath.SetX(paintParams.DropDownButtonBounds, qmargin.Left + this.m_iThemedBorderSize - SystemInformation.Border3DSize.Width);
              qmargin.Left += num;
            }
          }
          paintParams.DropDownImageBounds = Rectangle.Empty;
        }
        else
        {
          if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.LeftOutside)
          {
            paintParams.DropDownButtonBounds = new Rectangle(0, 0, SystemInformation.VerticalScrollBarWidth, inputBox.Height);
            qmargin.Left += SystemInformation.VerticalScrollBarWidth;
          }
          else if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.RightOutside)
          {
            paintParams.DropDownButtonBounds = new Rectangle(inputBox.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, inputBox.Height);
            qmargin.Right += SystemInformation.VerticalScrollBarWidth;
          }
          else
          {
            paintParams.DropDownButtonBounds = new Rectangle(0, qmargin.Top, SystemInformation.VerticalScrollBarWidth, inputBox.Height - qmargin.Vertical);
            if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.Right || inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.RightOutside)
            {
              qmargin.Right += SystemInformation.VerticalScrollBarWidth;
              paintParams.DropDownButtonBounds = QMath.SetX(paintParams.DropDownButtonBounds, inputBox.Width - qmargin.Right);
            }
            else
            {
              paintParams.DropDownButtonBounds = QMath.SetX(paintParams.DropDownButtonBounds, qmargin.Left);
              qmargin.Left += SystemInformation.VerticalScrollBarWidth;
            }
          }
          paintParams.DropDownImageBounds = Rectangle.Empty;
        }
        if (inputBox.Configuration.InputStyle == QInputBoxStyle.UpDown)
        {
          int num1 = paintParams.DropDownButtonBounds.Height / 2;
          int num2 = paintParams.DropDownButtonBounds.Height % 2;
          QInputBoxPaintParams qinputBoxPaintParams1 = paintParams;
          Rectangle downButtonBounds1 = paintParams.DropDownButtonBounds;
          int x1 = downButtonBounds1.X;
          downButtonBounds1 = paintParams.DropDownButtonBounds;
          int y1 = downButtonBounds1.Y;
          int width1 = paintParams.DropDownButtonBounds.Width;
          int height1 = num1;
          Rectangle rectangle2 = new Rectangle(x1, y1, width1, height1);
          qinputBoxPaintParams1.UpButtonBounds = rectangle2;
          QInputBoxPaintParams qinputBoxPaintParams2 = paintParams;
          Rectangle downButtonBounds2 = paintParams.DropDownButtonBounds;
          int x2 = downButtonBounds2.X;
          downButtonBounds2 = paintParams.DropDownButtonBounds;
          int y2 = downButtonBounds2.Y + num1;
          rectangle1 = paintParams.DropDownButtonBounds;
          int width2 = rectangle1.Width;
          int height2 = num1 + num2;
          Rectangle rectangle3 = new Rectangle(x2, y2, width2, height2);
          qinputBoxPaintParams2.DownButtonBounds = rectangle3;
          if (inputBox.Configuration.InputBoxStyle == QButtonStyle.Custom)
          {
            QShape shape = inputBox.Configuration.ButtonAppearance.Shape;
            QInputBoxPaintParams qinputBoxPaintParams3 = paintParams;
            rectangle1 = paintParams.UpButtonBounds;
            int x3 = rectangle1.X;
            qpadding = inputBox.Configuration.ButtonPadding;
            int left1 = qpadding.Left;
            int num3 = x3 + left1;
            rectangle1 = shape.ContentBounds;
            int left2 = rectangle1.Left;
            int x4 = num3 + left2;
            rectangle1 = paintParams.UpButtonBounds;
            int y3 = rectangle1.Y;
            qpadding = inputBox.Configuration.ButtonPadding;
            int top1 = qpadding.Top;
            int num4 = y3 + top1;
            rectangle1 = shape.ContentBounds;
            int top2 = rectangle1.Top;
            int y4 = num4 + top2;
            rectangle1 = paintParams.UpButtonBounds;
            int width3 = rectangle1.Width;
            qpadding = inputBox.Configuration.ButtonPadding;
            int horizontal1 = qpadding.Horizontal;
            int num5 = width3 - horizontal1;
            int width4 = shape.Size.Width;
            rectangle1 = shape.ContentBounds;
            int width5 = rectangle1.Width;
            int num6 = width4 - width5;
            int width6 = num5 - num6;
            rectangle1 = paintParams.UpButtonBounds;
            int height3 = rectangle1.Height;
            qpadding = inputBox.Configuration.ButtonPadding;
            int vertical1 = qpadding.Vertical;
            int num7 = height3 - vertical1;
            int height4 = shape.Size.Height;
            rectangle1 = shape.ContentBounds;
            int height5 = rectangle1.Height;
            int num8 = height4 - height5;
            int height6 = num7 - num8;
            Rectangle rectangle4 = new Rectangle(x4, y4, width6, height6);
            qinputBoxPaintParams3.UpImageBounds = rectangle4;
            QInputBoxPaintParams qinputBoxPaintParams4 = paintParams;
            rectangle1 = paintParams.DownButtonBounds;
            int x5 = rectangle1.X;
            qpadding = inputBox.Configuration.ButtonPadding;
            int left3 = qpadding.Left;
            int num9 = x5 + left3;
            rectangle1 = shape.ContentBounds;
            int left4 = rectangle1.Left;
            int x6 = num9 + left4;
            rectangle1 = paintParams.DownButtonBounds;
            int y5 = rectangle1.Y;
            qpadding = inputBox.Configuration.ButtonPadding;
            int top3 = qpadding.Top;
            int num10 = y5 + top3;
            rectangle1 = shape.ContentBounds;
            int top4 = rectangle1.Top;
            int y6 = num10 + top4;
            rectangle1 = paintParams.DownButtonBounds;
            int width7 = rectangle1.Width;
            qpadding = inputBox.Configuration.ButtonPadding;
            int horizontal2 = qpadding.Horizontal;
            int num11 = width7 - horizontal2;
            int width8 = shape.Size.Width;
            rectangle1 = shape.ContentBounds;
            int width9 = rectangle1.Width;
            int num12 = width8 - width9;
            int width10 = num11 - num12;
            rectangle1 = paintParams.DownButtonBounds;
            int height7 = rectangle1.Height;
            qpadding = inputBox.Configuration.ButtonPadding;
            int vertical2 = qpadding.Vertical;
            int num13 = height7 - vertical2;
            int height8 = shape.Size.Height;
            rectangle1 = shape.ContentBounds;
            int height9 = rectangle1.Height;
            int num14 = height8 - height9;
            int height10 = num13 - num14;
            Rectangle rectangle5 = new Rectangle(x6, y6, width10, height10);
            qinputBoxPaintParams4.DownImageBounds = rectangle5;
          }
        }
      }
      else
        paintParams.DropDownButtonBounds = Rectangle.Empty;
      if (!inputBox.Multiline)
      {
        paintParams.TextPaddingBounds = new Rectangle(qmargin.Left, qmargin.Top, inputBox.Width - qmargin.Horizontal, inputBox.Height - qmargin.Vertical);
        ref QMargin local1 = ref qmargin;
        int left5 = local1.Left;
        qpadding = inputBox.Configuration.InputBoxTextPadding;
        int left6 = qpadding.Left;
        local1.Left = left5 + left6;
        ref QMargin local2 = ref qmargin;
        int right1 = local2.Right;
        qpadding = inputBox.Configuration.InputBoxTextPadding;
        int right2 = qpadding.Right;
        local2.Right = right1 + right2;
        ref QMargin local3 = ref qmargin;
        int top5 = local3.Top;
        qpadding = inputBox.Configuration.InputBoxTextPadding;
        int top6 = qpadding.Top;
        local3.Top = top5 + top6;
        ref QMargin local4 = ref qmargin;
        int bottom1 = local4.Bottom;
        qpadding = inputBox.Configuration.InputBoxTextPadding;
        int bottom2 = qpadding.Bottom;
        local4.Bottom = bottom1 + bottom2;
      }
      else
        paintParams.TextPaddingBounds = Rectangle.Empty;
      if (inputBox.Configuration.InputStyle == QInputBoxStyle.DropDown || inputBox.Configuration.InputStyle == QInputBoxStyle.DropDownList || inputBox.Configuration.InputStyle == QInputBoxStyle.UpDown)
      {
        if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.LeftOutside)
        {
          QInputBoxPaintParams qinputBoxPaintParams = paintParams;
          rectangle1 = paintParams.DropDownButtonBounds;
          int width11 = rectangle1.Width;
          int num15;
          if (inputBox.Configuration.InputBoxStyle != QButtonStyle.Custom)
          {
            num15 = 0;
          }
          else
          {
            buttonMargin = inputBox.Configuration.ButtonMargin;
            num15 = buttonMargin.Horizontal;
          }
          int x = width11 + num15;
          int width12 = inputBox.Width;
          rectangle1 = paintParams.DropDownButtonBounds;
          int width13 = rectangle1.Width;
          int num16;
          if (inputBox.Configuration.InputBoxStyle != QButtonStyle.Custom)
          {
            num16 = 0;
          }
          else
          {
            buttonMargin = inputBox.Configuration.ButtonMargin;
            num16 = buttonMargin.Horizontal;
          }
          int num17 = width13 + num16;
          int width14 = width12 - num17;
          int height = inputBox.Height;
          Rectangle rectangle6 = new Rectangle(x, 0, width14, height);
          qinputBoxPaintParams.BackgroundBounds = rectangle6;
        }
        else if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.RightOutside)
        {
          QInputBoxPaintParams qinputBoxPaintParams = paintParams;
          int width15 = inputBox.Width;
          rectangle1 = paintParams.DropDownButtonBounds;
          int width16 = rectangle1.Width;
          int num18;
          if (inputBox.Configuration.InputBoxStyle != QButtonStyle.Custom)
          {
            num18 = 0;
          }
          else
          {
            buttonMargin = inputBox.Configuration.ButtonMargin;
            num18 = buttonMargin.Horizontal;
          }
          int num19 = width16 + num18;
          Rectangle rectangle7 = new Rectangle(0, 0, width15 - num19, inputBox.Height);
          qinputBoxPaintParams.BackgroundBounds = rectangle7;
        }
        else
          paintParams.BackgroundBounds = new Rectangle(0, 0, inputBox.Width, inputBox.Height);
      }
      else
        paintParams.BackgroundBounds = new Rectangle(0, 0, inputBox.Width, inputBox.Height);
      paintParams.ClientAreaMargin = qmargin;
    }

    public virtual void DrawInputBox(
      QInputBox inputBox,
      QInputBoxPaintParams paintParams,
      PaintEventArgs e)
    {
      this.DrawInputBoxBackground(inputBox, paintParams, e);
      this.DrawInputBoxButtons(inputBox, paintParams, e);
    }

    public virtual void DrawInputBoxButtons(
      QInputBox inputBox,
      QInputBoxPaintParams paintParams,
      PaintEventArgs e)
    {
      if (inputBox.Configuration.InputStyle != QInputBoxStyle.DropDown && inputBox.Configuration.InputStyle != QInputBoxStyle.DropDownList && inputBox.Configuration.InputStyle != QInputBoxStyle.UpDown)
        return;
      if (inputBox.Configuration.InputStyle == QInputBoxStyle.UpDown)
      {
        this.DrawInputBoxButton(QInputBoxButtonType.UpButton, inputBox.UpButtonDrawType, paintParams.UpButtonBounds, paintParams.UpImageBounds, inputBox.Configuration.UsedButtonMaskReverse, inputBox, e);
        this.DrawInputBoxButton(QInputBoxButtonType.DownButton, inputBox.DownButtonDrawType, paintParams.DownButtonBounds, paintParams.DownImageBounds, inputBox.Configuration.UsedButtonMask, inputBox, e);
      }
      else
        this.DrawInputBoxButton(QInputBoxButtonType.DropDownButton, inputBox.DropDownButtonDrawType, paintParams.DropDownButtonBounds, paintParams.DropDownImageBounds, inputBox.Configuration.UsedButtonMask, inputBox, e);
    }

    public virtual void DrawInputBoxButton(
      QInputBoxButtonType buttonType,
      QInputBoxButtonDrawType buttonDrawType,
      Rectangle buttonBounds,
      Rectangle imageBounds,
      Image imageMask,
      QInputBox inputBox,
      PaintEventArgs e)
    {
      if (inputBox.Configuration.InputBoxStyle == QButtonStyle.Custom)
      {
        QColorSet colors = (QColorSet) null;
        QInputBoxButtonShapeAppearance appearance = (QInputBoxButtonShapeAppearance) null;
        switch (buttonDrawType)
        {
          case QInputBoxButtonDrawType.DrawButtonDisabled:
            colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.DisabledButtonBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.DisabledButtonBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.DisabledButtonBorderColorPropertyName]);
            appearance = inputBox.Configuration.ButtonAppearance;
            break;
          case QInputBoxButtonDrawType.DrawButtonNormal:
            colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.ButtonBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.ButtonBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.ButtonBorderColorPropertyName]);
            appearance = inputBox.Configuration.ButtonAppearance;
            break;
          case QInputBoxButtonDrawType.DrawButtonHot:
            colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.HotButtonBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.HotButtonBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.HotButtonBorderColorPropertyName]);
            appearance = inputBox.Configuration.ButtonAppearanceHot;
            break;
          case QInputBoxButtonDrawType.DrawButtonPressed:
            colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.PressedButtonBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.PressedButtonBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.PressedButtonBorderColorPropertyName]);
            appearance = inputBox.Configuration.ButtonAppearancePressed;
            break;
        }
        if (colors != null && appearance != null)
          QShapePainter.Default.Paint(buttonBounds, appearance.Shape, (IQAppearance) appearance, colors, new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds), QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
        if (imageMask == null)
          return;
        Region clip = e.Graphics.Clip;
        e.Graphics.Clip = new Region(imageBounds);
        QControlPaint.DrawImage(imageMask, Color.FromArgb((int) byte.MaxValue, 0, 0), (Color) inputBox.ColorScheme["TextColor"], inputBox.Configuration.ButtonMaskAlign, imageBounds, inputBox.Configuration.UsedButtonMask.Size, e.Graphics);
        e.Graphics.Clip = clip;
      }
      else
      {
        this.SecureWindowsXpTheme(inputBox);
        if (this.m_hWindowsXPTheme != IntPtr.Zero && this.m_hButtonWindowsXPTheme != IntPtr.Zero)
        {
          IntPtr hdc = e.Graphics.GetHdc();
          NativeMethods.RECT rect1 = NativeHelper.CreateRECT(new Rectangle(0, 0, inputBox.Width, inputBox.Height));
          NativeMethods.RECT rect2 = NativeHelper.CreateRECT(buttonBounds);
          if (inputBox.Configuration.InputStyle != QInputBoxStyle.UpDown)
            NativeMethods.DrawThemeBackground(this.m_hWindowsXPTheme, hdc, 0, this.GetButtonStyle(buttonDrawType), ref rect1, ref rect2);
          NativeMethods.RECT rect3 = NativeHelper.CreateRECT(buttonBounds);
          NativeMethods.DTBGOPTS pOptions = new NativeMethods.DTBGOPTS();
          pOptions.rcClip = rect3;
          if (inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.Left || inputBox.Configuration.ButtonAlign == QInputBoxButtonAlign.LeftOutside)
            pOptions.dwFlags = 32;
          pOptions.dwSize = Marshal.SizeOf((object) pOptions);
          switch (buttonType)
          {
            case QInputBoxButtonType.UpButton:
              NativeMethods.DrawThemeBackgroundEx(this.m_hButtonWindowsXPTheme, hdc, 1, this.GetButtonStyle(buttonDrawType), ref rect3, ref pOptions);
              break;
            case QInputBoxButtonType.DownButton:
              NativeMethods.DrawThemeBackgroundEx(this.m_hButtonWindowsXPTheme, hdc, 2, this.GetButtonStyle(buttonDrawType), ref rect3, ref pOptions);
              break;
            case QInputBoxButtonType.DropDownButton:
              NativeMethods.DrawThemeBackgroundEx(this.m_hButtonWindowsXPTheme, hdc, 1, this.GetButtonStyle(buttonDrawType), ref rect3, ref pOptions);
              break;
          }
          e.Graphics.ReleaseHdc(hdc);
        }
        else
          ControlPaint.DrawComboButton(e.Graphics, buttonBounds, this.GetButtonState(buttonDrawType));
      }
    }

    public virtual void DrawInputBoxBackground(
      QInputBox inputBox,
      QInputBoxPaintParams paintParams,
      PaintEventArgs e)
    {
      if (inputBox.Configuration.InputBoxStyle == QButtonStyle.Custom)
      {
        QInputBoxStates inputBoxState = inputBox.InputBoxState;
        QColorSet colors;
        QInputBoxAppearance appearance;
        if ((inputBoxState & QInputBoxStates.Inactive) == QInputBoxStates.Inactive)
        {
          colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.DisabledOuterBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.DisabledOuterBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.DisabledOuterBorderColorPropertyName]);
          appearance = inputBox.Appearance;
        }
        else if ((inputBoxState & QInputBoxStates.Focused) == QInputBoxStates.Focused)
        {
          colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.FocusedOuterBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.FocusedOuterBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.FocusedOuterBorderColorPropertyName]);
          appearance = inputBox.AppearanceFocused;
        }
        else if ((inputBoxState & QInputBoxStates.Hot) == QInputBoxStates.Hot)
        {
          colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.HotOuterBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.HotOuterBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.HotOuterBorderColorPropertyName]);
          appearance = inputBox.AppearanceHot;
        }
        else
        {
          colors = new QColorSet((Color) inputBox.ColorScheme[inputBox.OuterBackColor1PropertyName], (Color) inputBox.ColorScheme[inputBox.OuterBackColor2PropertyName], (Color) inputBox.ColorScheme[inputBox.OuterBorderColorPropertyName]);
          appearance = inputBox.Appearance;
        }
        if (paintParams.PaintTransparentBackground)
          QControlPaint.PaintTransparentBackground((Control) inputBox, e);
        else if (inputBox.Parent != null)
          e.Graphics.Clear(inputBox.Parent.BackColor);
        QShapePainter.Default.Paint(paintParams.BackgroundBounds, appearance.Shape, (IQAppearance) appearance, colors, new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds), QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
      }
      else
      {
        this.SecureWindowsXpTheme(inputBox);
        if (this.m_hWindowsXPTheme != IntPtr.Zero && this.m_hButtonWindowsXPTheme != IntPtr.Zero)
        {
          if (paintParams.PaintTransparentBackground)
            QControlPaint.PaintTransparentBackground((Control) inputBox, e);
          else if (inputBox.Parent != null)
            e.Graphics.Clear(inputBox.Parent.BackColor);
          Rectangle backgroundBounds = paintParams.BackgroundBounds;
          IntPtr hdc = e.Graphics.GetHdc();
          NativeMethods.RECT rect = NativeHelper.CreateRECT(backgroundBounds);
          NativeMethods.DrawThemeBackground(this.m_hWindowsXPTheme, hdc, QInputBoxPainter.GetPart(inputBox.Configuration.InputStyle), QInputBoxPainter.GetButtonStyle(inputBox.InputBoxState), ref rect, ref rect);
          e.Graphics.ReleaseHdc(hdc);
        }
        else
          ControlPaint.DrawBorder3D(e.Graphics, paintParams.BackgroundBounds, Border3DStyle.Sunken, Border3DSide.All);
      }
      if (paintParams.TextPaddingBounds.IsEmpty && !inputBox.BackColorContainsTransparency)
        return;
      Brush brush = (Brush) new SolidBrush(inputBox.BackColor);
      if (!paintParams.TextPaddingBounds.IsEmpty)
      {
        e.Graphics.FillRectangle(brush, paintParams.TextPaddingBounds);
      }
      else
      {
        QMargin clientAreaMargin = inputBox.GetClientAreaMargin();
        e.Graphics.FillRectangle(brush, new Rectangle(clientAreaMargin.Left, clientAreaMargin.Top, inputBox.ClientSize.Width, inputBox.ClientSize.Height));
      }
      brush.Dispose();
    }

    public IWin32Window Win32Window
    {
      get => this.m_oWin32Window;
      set
      {
        if (this.m_oWin32Window == value)
          return;
        this.m_oWin32Window = value;
        this.CloseWindowsXpTheme();
      }
    }

    internal void SecureWindowsXpTheme(QInputBox inputBox)
    {
      if (this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      if (inputBox.Configuration.InputStyle == QInputBoxStyle.UpDown)
      {
        this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.m_oWin32Window.Handle, "Edit");
        this.m_hButtonWindowsXPTheme = NativeMethods.OpenThemeData(this.m_oWin32Window.Handle, "Spin");
        if (this.m_hWindowsXPTheme != IntPtr.Zero)
          NativeMethods.GetThemeMetric(this.m_hWindowsXPTheme, IntPtr.Zero, 0, 0, 2403, ref this.m_iThemedBorderSize);
        else
          this.m_iThemedBorderSize = 0;
      }
      else if (inputBox.Configuration.InputStyle == QInputBoxStyle.TextBox)
      {
        this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.m_oWin32Window.Handle, "Edit");
        this.m_hButtonWindowsXPTheme = this.m_hWindowsXPTheme;
        this.m_iThemedBorderSize = 0;
      }
      else
      {
        this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.m_oWin32Window.Handle, "ComboBox");
        this.m_hButtonWindowsXPTheme = this.m_hWindowsXPTheme;
        if (this.m_hWindowsXPTheme != IntPtr.Zero)
          NativeMethods.GetThemeMetric(this.m_hWindowsXPTheme, IntPtr.Zero, 0, 0, 2403, ref this.m_iThemedBorderSize);
        else
          this.m_iThemedBorderSize = 0;
      }
      this.m_bWindowsXPThemeTried = true;
    }

    internal void CloseWindowsXpTheme()
    {
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        if (this.m_hWindowsXPTheme != this.m_hButtonWindowsXPTheme)
          NativeMethods.CloseThemeData(this.m_hButtonWindowsXPTheme);
        NativeMethods.CloseThemeData(this.m_hWindowsXPTheme);
        this.m_hWindowsXPTheme = IntPtr.Zero;
        this.m_hButtonWindowsXPTheme = IntPtr.Zero;
      }
      this.m_bWindowsXPThemeTried = false;
    }

    private bool HasWindowsXPTheme => this.m_hWindowsXPTheme != IntPtr.Zero && this.m_hButtonWindowsXPTheme != IntPtr.Zero;

    private static int GetButtonStyle(QInputBoxStates state)
    {
      if ((state & QInputBoxStates.Inactive) == QInputBoxStates.Inactive)
        return 4;
      if ((state & QInputBoxStates.Focused) == QInputBoxStates.Focused)
        return 3;
      return (state & QInputBoxStates.Hot) == QInputBoxStates.Hot ? 2 : 1;
    }

    private static int GetPart(QInputBoxStyle style) => style != QInputBoxStyle.TextBox ? 0 : 1;

    private int GetButtonStyle(QInputBoxButtonDrawType drawType)
    {
      switch (drawType)
      {
        case QInputBoxButtonDrawType.DontDrawButtonBackground:
        case QInputBoxButtonDrawType.DrawButtonNormal:
          return 1;
        case QInputBoxButtonDrawType.DrawButtonDisabled:
          return 4;
        case QInputBoxButtonDrawType.DrawButtonHot:
          return 2;
        case QInputBoxButtonDrawType.DrawButtonPressed:
          return 3;
        default:
          return 0;
      }
    }

    private int GetButtonStyle(QButtonState state)
    {
      switch (state)
      {
        case QButtonState.Inactive:
          return 4;
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

    private ButtonState GetButtonState(QInputBoxButtonDrawType drawType)
    {
      switch (drawType)
      {
        case QInputBoxButtonDrawType.DontDrawButtonBackground:
        case QInputBoxButtonDrawType.DrawButtonNormal:
          return ButtonState.Normal;
        case QInputBoxButtonDrawType.DrawButtonDisabled:
          return ButtonState.Inactive;
        case QInputBoxButtonDrawType.DrawButtonHot:
          return ButtonState.Normal;
        case QInputBoxButtonDrawType.DrawButtonPressed:
          return ButtonState.Pushed;
        default:
          return ButtonState.Normal;
      }
    }

    ~QInputBoxPainter() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }
  }
}
