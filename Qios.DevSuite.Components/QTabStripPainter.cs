// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStripPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QTabStripPainter : IDisposable
  {
    private const int DropArrowSize = 9;
    private StringFormat m_oStringFormat;
    private Size m_oLastCalculatedButtonsSize;
    private Size m_oLastCalculatedAvailableButtonsSize;
    private Rectangle m_oLastCalculatedStripContentBounds;
    private Rectangle m_oLastCalculatedButtonAreaBounds;
    private IWin32Window m_oWin32Window;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private bool m_bWindowsXPThemeTried;

    public QTabStripPainter()
    {
      this.m_oStringFormat = new StringFormat(StringFormat.GenericDefault);
      this.m_oStringFormat.LineAlignment = StringAlignment.Near;
      this.m_oStringFormat.Alignment = StringAlignment.Near;
      this.m_oStringFormat.Trimming = StringTrimming.EllipsisCharacter;
      this.m_oStringFormat.FormatFlags = StringFormatFlags.NoWrap;
    }

    public Size LastCalculatedAvailableButtonsSize => this.m_oLastCalculatedAvailableButtonsSize;

    public IWin32Window Win32Window
    {
      get => this.m_oWin32Window;
      set
      {
        if (this.m_oWin32Window == value)
          return;
        this.m_oWin32Window = value;
        this.CloseWindowsXPTheme();
      }
    }

    public IntPtr WindowsXPThemeHandle
    {
      get
      {
        this.SecureWindowsXpTheme();
        return this.m_hWindowsXPTheme;
      }
    }

    private void SecureWindowsXpTheme()
    {
      if (!(this.m_oWin32Window.Handle != IntPtr.Zero) || this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.m_oWin32Window.Handle, "Tab");
      this.m_bWindowsXPThemeTried = true;
    }

    private void CloseWindowsXPTheme()
    {
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        NativeMethods.CloseThemeData(this.m_hWindowsXPTheme);
        this.m_hWindowsXPTheme = IntPtr.Zero;
      }
      this.m_bWindowsXPThemeTried = false;
    }

    protected virtual void PutLastCalculatedAvailableButtonsSize(Size size) => this.m_oLastCalculatedAvailableButtonsSize = size;

    public Size LastCalculatedButtonsSize => this.m_oLastCalculatedButtonsSize;

    protected virtual void PutLastCalculatedButtonsSize(Size size) => this.m_oLastCalculatedButtonsSize = size;

    public Rectangle LastCalculatedStripContentBounds => this.m_oLastCalculatedStripContentBounds;

    protected virtual void PutLastCalculatedStripContentBounds(Rectangle bounds) => this.m_oLastCalculatedStripContentBounds = bounds;

    public Rectangle LastCalculatedButtonAreaBounds => this.m_oLastCalculatedButtonAreaBounds;

    protected virtual void PutLastCalculatedButtonAreaBounds(Rectangle bounds) => this.m_oLastCalculatedButtonAreaBounds = bounds;

    protected virtual StringFormat StringFormat => this.m_oStringFormat;

    private Size ApplyGrowShrinkSizingBehaviorInRow(
      QTabStrip tabStrip,
      QTabButtonRow row,
      bool grow,
      bool shrink,
      Size availableButtonsSize)
    {
      QTabButtonRow row1 = row;
      Size size = availableButtonsSize;
      QTabButtonSelectionTypes selectionType = QTabButtonSelectionTypes.MustBeVisible;
      if (row1 != null)
        selectionType |= QTabButtonSelectionTypes.MustBeInRow;
      Size buttonSizes1 = this.CalculateButtonSizes(tabStrip, selectionType, row1, tabStrip.IsHorizontal, false, true, (Graphics) null);
      Size buttonSizes2 = this.CalculateButtonSizes(tabStrip, selectionType, row1, tabStrip.IsHorizontal, false, false, (Graphics) null);
      int thatMatchSelection = tabStrip.TabButtons.GetButtonCountThatMatchSelection(selectionType, row1);
      if (tabStrip.IsHorizontal)
      {
        int width = 0;
        int num1 = buttonSizes1.Width - buttonSizes2.Width;
        int num2 = size.Width - num1;
        bool flag = false;
        int num3 = 0;
        if (buttonSizes1.Width < size.Width && grow)
        {
          int num4 = 0;
          int num5 = 0;
          int val1 = (int) Math.Ceiling((double) num2 / (double) thatMatchSelection);
          int num6 = size.Width - buttonSizes1.Width;
          QTabButton nextButton = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
          while (nextButton != null && !flag && num6 > 0)
          {
            if (nextButton.Width < val1)
            {
              int num7 = Math.Min(Math.Min(val1, nextButton.Width + num6), nextButton.CalculatedMaximumSize.Width);
              if (num7 == nextButton.CalculatedMaximumSize.Width)
              {
                ++num5;
                num4 += num7;
              }
              if (num7 > nextButton.Width)
              {
                num6 -= num7 - nextButton.Width;
                nextButton.Width = num7;
              }
              else
                ++num3;
            }
            else
              ++num3;
            width = width + nextButton.Width + nextButton.Configuration.Spacing.All;
            nextButton = tabStrip.GetNextButton(nextButton, selectionType, row1, false);
            if (nextButton == null)
            {
              flag = num3 == thatMatchSelection;
              if (!flag && num6 > 0)
              {
                val1 = (int) Math.Floor((double) (num2 - num4) / (double) (thatMatchSelection - num5));
                num3 = 0;
                width = 0;
                nextButton = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
              }
            }
            else
              width += tabStrip.Configuration.ButtonSpacing;
          }
          return new Size(width, buttonSizes1.Height);
        }
        if (buttonSizes1.Width <= size.Width || !shrink)
          return buttonSizes1;
        int num8 = 0;
        int num9 = 0;
        int num10 = buttonSizes1.Width - size.Width;
        int val1_1 = (int) Math.Floor((double) num2 / (double) thatMatchSelection);
        QTabButton nextButton1 = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
        while (nextButton1 != null && !flag && num10 > 0)
        {
          if (nextButton1.Width > val1_1)
          {
            int num11 = Math.Max(Math.Max(val1_1, nextButton1.Width - num10), nextButton1.CalculatedMinimumSize.Width);
            if (num11 == nextButton1.CalculatedMinimumSize.Width)
            {
              ++num9;
              num8 += num11;
            }
            if (num11 < nextButton1.Width)
            {
              num10 -= nextButton1.Width - num11;
              nextButton1.Width = num11;
            }
            else
              ++num3;
          }
          else
            ++num3;
          width = width + nextButton1.Width + nextButton1.Configuration.Spacing.All;
          nextButton1 = tabStrip.GetNextButton(nextButton1, selectionType, row1, false);
          if (nextButton1 == null)
          {
            flag = num3 == thatMatchSelection;
            if (!flag && num10 > 0)
            {
              val1_1 = (int) Math.Floor((double) (num2 - num8) / (double) (thatMatchSelection - num9));
              num3 = 0;
              width = 0;
              nextButton1 = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
            }
          }
          else
            width += tabStrip.Configuration.ButtonSpacing;
        }
        return new Size(width, buttonSizes1.Height);
      }
      int height = 0;
      int num12 = buttonSizes1.Height - buttonSizes2.Height;
      int num13 = size.Height - num12;
      if (buttonSizes1.Height < size.Height && grow)
      {
        int num14 = 0;
        int num15 = 0;
        bool flag = false;
        int num16 = 0;
        int val1 = (int) Math.Ceiling((double) num13 / (double) thatMatchSelection);
        int num17 = size.Height - buttonSizes1.Height;
        QTabButton nextButton = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
        while (nextButton != null && !flag && num17 > 0)
        {
          if (nextButton.Height < val1)
          {
            int num18 = Math.Min(Math.Min(val1, nextButton.Height + num17), nextButton.CalculatedMaximumSize.Height);
            if (num18 == nextButton.CalculatedMaximumSize.Height)
            {
              ++num15;
              num14 += num18;
            }
            if (num18 > nextButton.Height)
            {
              num17 -= num18 - nextButton.Height;
              nextButton.Height = num18;
            }
            else
              ++num16;
          }
          else
            ++num16;
          height = height + nextButton.Height + nextButton.Configuration.Spacing.All;
          nextButton = tabStrip.GetNextButton(nextButton, selectionType, row1, false);
          if (nextButton == null)
          {
            flag = num16 == thatMatchSelection;
            if (!flag && num17 > 0)
            {
              val1 = (int) Math.Floor((double) (num13 - num14) / (double) (thatMatchSelection - num15));
              num16 = 0;
              height = 0;
              nextButton = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
            }
          }
          else
            height += tabStrip.Configuration.ButtonSpacing;
        }
        return new Size(buttonSizes1.Width, height);
      }
      if (buttonSizes1.Height <= size.Height || !shrink)
        return buttonSizes1;
      int num19 = buttonSizes1.Height - size.Height;
      bool flag1 = false;
      int num20 = 0;
      int num21 = 0;
      int num22 = 0;
      int val1_2 = (int) Math.Floor((double) num13 / (double) thatMatchSelection);
      QTabButton nextButton2 = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
      while (nextButton2 != null && !flag1 && num19 > 0)
      {
        if (nextButton2.Height > val1_2)
        {
          int num23 = Math.Max(Math.Max(val1_2, nextButton2.Height - num19), nextButton2.CalculatedMinimumSize.Height);
          if (num23 == nextButton2.CalculatedMinimumSize.Height)
          {
            ++num22;
            num21 += num23;
          }
          if (num23 < nextButton2.Height)
          {
            num19 -= nextButton2.Height - num23;
            nextButton2.Height = num23;
          }
          else
            ++num20;
        }
        else
          ++num20;
        height = height + nextButton2.Height + nextButton2.Configuration.Spacing.All;
        nextButton2 = tabStrip.GetNextButton(nextButton2, selectionType, row1, false);
        if (nextButton2 == null)
        {
          flag1 = num20 == thatMatchSelection;
          if (!flag1 && num19 > 0)
          {
            val1_2 = (int) Math.Floor((double) (num13 - num21) / (double) (thatMatchSelection - num22));
            num20 = 0;
            height = 0;
            nextButton2 = tabStrip.GetNextButton((QTabButton) null, selectionType, row1, false);
          }
        }
        else
          height += tabStrip.Configuration.ButtonSpacing;
      }
      return new Size(buttonSizes1.Width, height);
    }

    private Size ApplyGrowShinkBehavior(
      QTabStrip tabStrip,
      bool grow,
      bool shrink,
      Size neededSize,
      Size availableStripSize)
    {
      Size availableButtonsSize = this.CalculateAvailableButtonsSize(tabStrip, availableStripSize, tabStrip.IsHorizontal);
      Size empty = Size.Empty;
      if (tabStrip.TabButtonRows.Count > 0)
      {
        for (int index = 0; index < tabStrip.TabButtonRows.Count; ++index)
        {
          Size size = this.ApplyGrowShrinkSizingBehaviorInRow(tabStrip, tabStrip.TabButtonRows[index], grow, shrink, availableButtonsSize);
          if (tabStrip.IsHorizontal)
          {
            empty.Width = Math.Max(empty.Width, size.Width);
            empty.Height += size.Height;
          }
          else
          {
            empty.Width += size.Width;
            empty.Height = Math.Max(empty.Height, size.Height);
          }
        }
      }
      else
      {
        Size size = this.ApplyGrowShrinkSizingBehaviorInRow(tabStrip, (QTabButtonRow) null, grow, shrink, availableButtonsSize);
        if (tabStrip.IsHorizontal)
        {
          empty.Width = Math.Max(empty.Width, size.Width);
          empty.Height += size.Height;
        }
        else
        {
          empty.Width += size.Width;
          empty.Height = Math.Max(empty.Height, size.Height);
        }
      }
      return empty;
    }

    private Size ApplyStackSizingBehavior(
      QTabStrip tabStrip,
      Size neededSize,
      Size availableStripSize)
    {
      Size availableButtonsSize = this.CalculateAvailableButtonsSize(tabStrip, availableStripSize, tabStrip.IsHorizontal);
      Size size = neededSize;
      if (tabStrip.IsHorizontal)
      {
        if (neededSize.Width > availableButtonsSize.Width)
        {
          QTabButtonRow tabButtonRow = (QTabButtonRow) null;
          QTabButton nextVisibleButton = tabStrip.GetNextVisibleButton((QTabButton) null, false);
          int val1 = 0;
          int height = 0;
          int val2 = 0;
          for (; nextVisibleButton != null; nextVisibleButton = tabStrip.GetNextVisibleButton(nextVisibleButton, false))
          {
            if (tabButtonRow == null || val1 + nextVisibleButton.Width > availableButtonsSize.Width && availableButtonsSize.Height > height + tabButtonRow.Size)
            {
              if (tabButtonRow != null)
                height += tabButtonRow.Size;
              val2 = Math.Max(val1, val2);
              val1 = 0;
              tabButtonRow = new QTabButtonRow(tabStrip);
              tabStrip.TabButtonRows.Add(tabButtonRow);
            }
            nextVisibleButton.PutTabButtonRow(tabButtonRow);
            val1 = val1 + nextVisibleButton.Width + nextVisibleButton.Configuration.Spacing.All;
            tabButtonRow.Size = Math.Max(tabButtonRow.Size, nextVisibleButton.Height);
            if (nextVisibleButton != null && nextVisibleButton.TabButtonRow == tabButtonRow)
              val1 += tabStrip.Configuration.ButtonSpacing;
          }
          if (tabButtonRow != null)
            height += tabButtonRow.Size;
          size = new Size(Math.Max(val1, val2), height);
        }
      }
      else if (neededSize.Height > availableButtonsSize.Height)
      {
        QTabButtonRow tabButtonRow = (QTabButtonRow) null;
        QTabButton nextVisibleButton = tabStrip.GetNextVisibleButton((QTabButton) null, false);
        int val1 = 0;
        int width = 0;
        int val2 = 0;
        for (; nextVisibleButton != null; nextVisibleButton = tabStrip.GetNextVisibleButton(nextVisibleButton, false))
        {
          if (tabButtonRow == null || val1 + nextVisibleButton.Height > availableButtonsSize.Height && availableButtonsSize.Width > width + tabButtonRow.Size)
          {
            if (tabButtonRow != null)
              width += tabButtonRow.Size;
            val2 = Math.Max(val1, val2);
            val1 = 0;
            tabButtonRow = new QTabButtonRow(tabStrip);
            tabStrip.TabButtonRows.Add(tabButtonRow);
          }
          nextVisibleButton.PutTabButtonRow(tabButtonRow);
          val1 = val1 + nextVisibleButton.Height + nextVisibleButton.Configuration.Spacing.All;
          tabButtonRow.Size = Math.Max(tabButtonRow.Size, nextVisibleButton.Width);
          if (nextVisibleButton != null && nextVisibleButton.TabButtonRow == tabButtonRow)
            val1 += tabStrip.Configuration.ButtonSpacing;
        }
        if (tabButtonRow != null)
          width += tabButtonRow.Size;
        int height = Math.Max(val1, val2);
        size = new Size(width, height);
      }
      return size;
    }

    private Size ApplyScrollSizingBehavior(
      QTabStrip tabStrip,
      Size neededSize,
      Size availableStripSize)
    {
      Size empty = Size.Empty;
      Size availableButtonsSize = this.CalculateAvailableButtonsSize(tabStrip, availableStripSize, tabStrip.IsHorizontal);
      if ((tabStrip.IsHorizontal && neededSize.Width > availableButtonsSize.Width || !tabStrip.IsHorizontal && neededSize.Height > availableButtonsSize.Height) && !tabStrip.NavigationArea.ScrollButtonsVisible)
      {
        tabStrip.NavigationArea.ScrollButtonsVisible = true;
        if (tabStrip.Configuration.IsShrink)
          neededSize = this.ApplyGrowShinkBehavior(tabStrip, false, true, neededSize, availableStripSize);
      }
      return neededSize;
    }

    private Size ApplySizingBehavior(
      QTabStrip tabStrip,
      Size neededButtonsSize,
      Size availableStripSize)
    {
      QTabStripConfiguration configuration = tabStrip.Configuration;
      if (configuration.IsShrink)
        neededButtonsSize = this.ApplyGrowShinkBehavior(tabStrip, false, true, neededButtonsSize, availableStripSize);
      if (configuration.IsStack)
        neededButtonsSize = this.ApplyStackSizingBehavior(tabStrip, neededButtonsSize, availableStripSize);
      if (configuration.IsGrow)
        neededButtonsSize = this.ApplyGrowShinkBehavior(tabStrip, true, false, neededButtonsSize, availableStripSize);
      if (configuration.IsScroll)
        neededButtonsSize = this.ApplyScrollSizingBehavior(tabStrip, neededButtonsSize, availableStripSize);
      return neededButtonsSize;
    }

    private Rectangle CalculateContentBounds(QTabStrip tabStrip, Size stripSize) => this.CalculateContentBounds(tabStrip, stripSize, QMargin.Empty);

    private Rectangle CalculateContentBoundsFromOuterSize(
      QTabStrip tabStrip,
      Size outerStripSize)
    {
      return this.CalculateContentBoundsFromOuterSize(tabStrip, outerStripSize, QMargin.Empty);
    }

    private Rectangle CalculateContentBoundsFromOuterSize(
      QTabStrip tabStrip,
      Size outerStripSize,
      QMargin useMargin)
    {
      Rectangle shapeBounds = tabStrip.Configuration.StripMargin.InflateRectangleWithMargin(new Rectangle(Point.Empty, outerStripSize), false, tabStrip.Dock);
      Rectangle contentBounds = tabStrip.Configuration.Appearance.Shape.CalculateContentBounds(shapeBounds, tabStrip.Dock);
      Rectangle rectangle = tabStrip.Configuration.StripPadding.InflateRectangleWithPadding(contentBounds, false, tabStrip.Dock);
      if (useMargin != QMargin.Empty)
        rectangle = useMargin.InflateRectangleWithMargin(rectangle, false, tabStrip.Dock);
      return rectangle;
    }

    private Rectangle CalculateContentBounds(
      QTabStrip tabStrip,
      Size stripSize,
      QMargin useMargin)
    {
      Rectangle contentBounds = tabStrip.Configuration.Appearance.Shape.CalculateContentBounds(new Rectangle(Point.Empty, stripSize), tabStrip.Dock);
      Rectangle rectangle = tabStrip.Configuration.StripPadding.InflateRectangleWithPadding(contentBounds, false, tabStrip.Dock);
      if (useMargin != QMargin.Empty)
        rectangle = useMargin.InflateRectangleWithMargin(rectangle, false, tabStrip.Dock);
      return rectangle;
    }

    private Size CalculateAvailableButtonsSize(
      QTabStrip tabStrip,
      Size availableSize,
      bool horizontal)
    {
      Size size = this.CalculateContentBoundsFromOuterSize(tabStrip, availableSize, tabStrip.Configuration.ButtonAreaMargin).Size;
      Size navigationAreaSize = this.CalculateNavigationAreaSize(tabStrip, true);
      if (horizontal)
        size.Width -= navigationAreaSize.Width;
      else
        size.Height -= navigationAreaSize.Height;
      return size;
    }

    private Size CalculateButtonSizes(QTabStrip tabStrip, bool horizontal, Graphics graphics) => this.CalculateButtonSizes(tabStrip, QTabButtonSelectionTypes.MustBeVisible, (QTabButtonRow) null, horizontal, true, true, graphics);

    private Size CalculateButtonSizes(
      QTabStrip tabStrip,
      QTabButtonSelectionTypes selectionType,
      QTabButtonRow row,
      bool horizontal,
      bool calculateButtonSize,
      bool includeButtonSpacing,
      Graphics graphics)
    {
      int width = 0;
      int height = 0;
      int num1 = 0;
      int num2 = 0;
      QTabButton nextButton = tabStrip.GetNextButton((QTabButton) null, selectionType, row, false);
      while (nextButton != null)
      {
        if (calculateButtonSize)
          nextButton.Size = this.CalculateButtonSize(nextButton, graphics);
        if (horizontal)
        {
          width += nextButton.Width;
          if (includeButtonSpacing)
            width += nextButton.Configuration.Spacing.All;
          num2 = Math.Max(nextButton.Height, num2);
        }
        else
        {
          height += nextButton.Height;
          if (includeButtonSpacing)
            height += nextButton.Configuration.Spacing.All;
          num1 = Math.Max(nextButton.Width, num1);
        }
        nextButton = tabStrip.GetNextButton(nextButton, selectionType, row, false);
        if (nextButton != null && includeButtonSpacing)
        {
          if (horizontal)
            width += tabStrip.Configuration.ButtonSpacing;
          else
            height += tabStrip.Configuration.ButtonSpacing;
        }
      }
      return horizontal ? new Size(width, num2) : new Size(num1, height);
    }

    private void ClearRows(QTabStrip tabStrip)
    {
      tabStrip.TabButtonRows.Clear();
      for (int index = 0; index < tabStrip.TabButtons.Count; ++index)
        tabStrip.TabButtons[index].PutTabButtonRow((QTabButtonRow) null);
    }

    internal void CalculateRowRanges(QTabStrip tabStrip, Rectangle tabStripContentBounds)
    {
      if (tabStrip.Dock == DockStyle.Top)
      {
        int num = tabStripContentBounds.Bottom;
        for (int index = 0; index < tabStrip.TabButtonRows.Count; ++index)
        {
          tabStrip.TabButtonRows[index].Start = num - tabStrip.TabButtonRows[index].Size;
          num = tabStrip.TabButtonRows[index].Start;
        }
      }
      else if (tabStrip.Dock == DockStyle.Bottom)
      {
        int num = tabStripContentBounds.Top;
        for (int index = 0; index < tabStrip.TabButtonRows.Count; ++index)
        {
          tabStrip.TabButtonRows[index].Start = num;
          num = tabStrip.TabButtonRows[index].End;
        }
      }
      else if (tabStrip.Dock == DockStyle.Left)
      {
        int num = tabStripContentBounds.Right;
        for (int index = 0; index < tabStrip.TabButtonRows.Count; ++index)
        {
          tabStrip.TabButtonRows[index].Start = num - tabStrip.TabButtonRows[index].Size;
          num = tabStrip.TabButtonRows[index].Start;
        }
      }
      else
      {
        if (tabStrip.Dock != DockStyle.Right)
          return;
        int num = tabStripContentBounds.Left;
        for (int index = 0; index < tabStrip.TabButtonRows.Count; ++index)
        {
          tabStrip.TabButtonRows[index].Start = num;
          num = tabStrip.TabButtonRows[index].End;
        }
      }
    }

    private Size CalculateStripLayoutHorizontal(
      QTabStrip tabStrip,
      Size availableSize,
      Graphics graphics)
    {
      Size buttonSizes = this.CalculateButtonSizes(tabStrip, tabStrip.IsHorizontal, graphics);
      Size size1 = this.ApplySizingBehavior(tabStrip, buttonSizes, availableSize);
      Size availableButtonsSize = this.CalculateAvailableButtonsSize(tabStrip, availableSize, tabStrip.IsHorizontal);
      Size size2 = tabStrip.Configuration.StripPadding.InflateSizeWithPadding(size1, true, tabStrip.IsHorizontal);
      Size contentSize = tabStrip.Configuration.ButtonAreaMargin.InflateSizeWithMargin(size2, true, tabStrip.IsHorizontal);
      Size shapeSize = tabStrip.Configuration.Appearance.Shape.CalculateShapeSize(contentSize, tabStrip.IsHorizontal);
      shapeSize.Height = Math.Max(shapeSize.Height, tabStrip.Configuration.StripMinimumHeight);
      shapeSize.Width = availableSize.Width;
      shapeSize.Width -= tabStrip.Configuration.StripMargin.Horizontal;
      Rectangle contentBounds = this.CalculateContentBounds(tabStrip, shapeSize);
      Rectangle rectangle = tabStrip.Configuration.ButtonAreaMargin.InflateRectangleWithMargin(contentBounds, false, tabStrip.Dock) with
      {
        Width = availableButtonsSize.Width
      };
      this.CalculateRowRanges(tabStrip, rectangle);
      QTabButtonSelectionTypes selectionType1 = QTabButtonSelectionTypes.MustBeVisible;
      if (availableButtonsSize.Width > size1.Width)
        selectionType1 |= QTabButtonSelectionTypes.MustBeNearAligned;
      int num1 = rectangle.Left;
      int y = rectangle.Top;
      int num2 = rectangle.Bottom;
      QTabButton nextButton = tabStrip.GetNextButton((QTabButton) null, selectionType1, (QTabButtonRow) null, false);
      QTabButtonRow qtabButtonRow = (QTabButtonRow) null;
      for (; nextButton != null; nextButton = tabStrip.GetNextButton(nextButton, selectionType1, (QTabButtonRow) null, false))
      {
        if (qtabButtonRow != nextButton.TabButtonRow)
        {
          num1 = rectangle.Left;
          y = 0;
          num2 = nextButton.TabButtonRow.Size;
          qtabButtonRow = nextButton.TabButtonRow;
        }
        int x = num1 + nextButton.Configuration.Spacing.Before;
        if (tabStrip.Dock == DockStyle.Top)
          nextButton.Location = new Point(x, num2 - nextButton.Height);
        else if (tabStrip.Dock == DockStyle.Bottom)
          nextButton.Location = new Point(x, y);
        num1 = x + nextButton.Width + tabStrip.Configuration.ButtonSpacing + nextButton.Configuration.Spacing.After;
      }
      this.CalculateNavigationAreaLayout(tabStrip, shapeSize);
      if ((selectionType1 & QTabButtonSelectionTypes.MustBeNearAligned) == QTabButtonSelectionTypes.MustBeNearAligned)
      {
        QTabButtonSelectionTypes selectionType2 = QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeFarAligned;
        int num3 = tabStrip.NavigationArea.Bounds.Left - tabStrip.Configuration.ButtonAreaMargin.Right;
        for (QTabButton previousButton = tabStrip.GetPreviousButton((QTabButton) null, selectionType2, (QTabButtonRow) null, false); previousButton != null; previousButton = tabStrip.GetPreviousButton(previousButton, selectionType2, (QTabButtonRow) null, false))
        {
          int num4 = num3 - previousButton.Configuration.Spacing.After;
          if (tabStrip.Dock == DockStyle.Top)
            previousButton.Location = new Point(num4 - previousButton.Width, rectangle.Bottom - previousButton.Height);
          else if (tabStrip.Dock == DockStyle.Bottom)
            previousButton.Location = new Point(num4 - previousButton.Width, rectangle.Top);
          num3 = num4 - previousButton.Width - previousButton.Configuration.Spacing.Before - tabStrip.Configuration.ButtonSpacing;
        }
      }
      this.PutLastCalculatedAvailableButtonsSize(availableButtonsSize);
      this.PutLastCalculatedButtonsSize(size1);
      this.PutLastCalculatedStripContentBounds(contentBounds);
      this.PutLastCalculatedButtonAreaBounds(rectangle);
      return shapeSize;
    }

    private Size CalculateStripLayoutVertical(
      QTabStrip tabStrip,
      Size availableSize,
      Graphics graphics)
    {
      Size buttonSizes = this.CalculateButtonSizes(tabStrip, tabStrip.IsHorizontal, graphics);
      Size size1 = this.ApplySizingBehavior(tabStrip, buttonSizes, availableSize);
      Size availableButtonsSize = this.CalculateAvailableButtonsSize(tabStrip, availableSize, tabStrip.IsHorizontal);
      Size size2 = tabStrip.Configuration.StripPadding.InflateSizeWithPadding(size1, true, tabStrip.IsHorizontal);
      Size contentSize = tabStrip.Configuration.ButtonAreaMargin.InflateSizeWithMargin(size2, true, tabStrip.IsHorizontal);
      Size shapeSize = tabStrip.Configuration.Appearance.Shape.CalculateShapeSize(contentSize, tabStrip.IsHorizontal);
      shapeSize.Width = Math.Max(shapeSize.Width, tabStrip.Configuration.StripMinimumHeight);
      shapeSize.Height = availableSize.Height;
      shapeSize.Height -= tabStrip.Configuration.StripMargin.Horizontal;
      Rectangle contentBounds = this.CalculateContentBounds(tabStrip, shapeSize);
      Rectangle rectangle = tabStrip.Configuration.ButtonAreaMargin.InflateRectangleWithMargin(contentBounds, false, tabStrip.Dock) with
      {
        Height = availableButtonsSize.Height
      };
      this.CalculateRowRanges(tabStrip, rectangle);
      QTabButtonSelectionTypes selectionType1 = QTabButtonSelectionTypes.MustBeVisible;
      if (availableButtonsSize.Height > size1.Height)
        selectionType1 |= QTabButtonSelectionTypes.MustBeNearAligned;
      int num1 = rectangle.Top;
      int x = rectangle.Left;
      int num2 = rectangle.Right;
      QTabButton fromButton = tabStrip.GetNextButton((QTabButton) null, selectionType1, (QTabButtonRow) null, false);
      QTabButtonRow qtabButtonRow = (QTabButtonRow) null;
      for (; fromButton != null; fromButton = tabStrip.GetNextVisibleButton(fromButton, false))
      {
        if (qtabButtonRow != fromButton.TabButtonRow)
        {
          num1 = rectangle.Top;
          x = 0;
          num2 = fromButton.TabButtonRow.Size;
          qtabButtonRow = fromButton.TabButtonRow;
        }
        int y = num1 + fromButton.Configuration.Spacing.Before;
        if (tabStrip.Dock == DockStyle.Left)
          fromButton.Location = new Point(num2 - fromButton.Width, y);
        else if (tabStrip.Dock == DockStyle.Right)
          fromButton.Location = new Point(x, y);
        num1 = y + fromButton.Height + fromButton.Configuration.Spacing.After + tabStrip.Configuration.ButtonSpacing;
      }
      this.CalculateNavigationAreaLayout(tabStrip, shapeSize);
      if ((selectionType1 & QTabButtonSelectionTypes.MustBeNearAligned) == QTabButtonSelectionTypes.MustBeNearAligned)
      {
        QTabButtonSelectionTypes selectionType2 = QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeFarAligned;
        int num3 = tabStrip.NavigationArea.Bounds.Top - tabStrip.Configuration.ButtonAreaMargin.Right;
        for (QTabButton previousButton = tabStrip.GetPreviousButton((QTabButton) null, selectionType2, (QTabButtonRow) null, false); previousButton != null; previousButton = tabStrip.GetPreviousButton(previousButton, selectionType2, (QTabButtonRow) null, false))
        {
          int num4 = num3 - previousButton.Configuration.Spacing.After;
          if (tabStrip.Dock == DockStyle.Left)
            previousButton.Location = new Point(rectangle.Right - previousButton.Width, num4 - previousButton.Height);
          else if (tabStrip.Dock == DockStyle.Right)
            previousButton.Location = new Point(rectangle.Left, num4 - previousButton.Height);
          num3 = num4 - previousButton.Height - previousButton.Configuration.Spacing.Before - tabStrip.Configuration.ButtonSpacing;
        }
      }
      this.PutLastCalculatedAvailableButtonsSize(availableButtonsSize);
      this.PutLastCalculatedButtonsSize(size1);
      this.PutLastCalculatedStripContentBounds(contentBounds);
      this.PutLastCalculatedButtonAreaBounds(rectangle);
      return shapeSize;
    }

    private Size CalculateButtonContentSizeHorizontal(QTabButton tabButton, Graphics graphics)
    {
      int num1 = 0;
      QTabStripConfiguration configuration1 = tabButton.TabStrip.Configuration;
      QTabButtonConfiguration configuration2 = tabButton.Configuration;
      StringFormat stringFormat = this.StringFormat;
      Font biggestFont = QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(configuration1.Font, configuration1.FontActive, graphics), configuration1.FontHot, graphics);
      string text = tabButton.Text;
      Icon icon = tabButton.RetrieveIcon(configuration2.IconSize, tabButton.Enabled);
      Size size1 = QControlPaint.MeasureString(text, biggestFont, true, stringFormat, graphics);
      Size size2 = icon != null ? configuration2.IconSize : Size.Empty;
      QSpacing qspacing1 = icon != null ? configuration2.IconSpacing : QSpacing.Empty;
      QSpacing qspacing2 = !QMisc.IsEmpty((object) text) ? configuration2.TextSpacing : QSpacing.Empty;
      int num2 = num1 + configuration2.Padding.Horizontal;
      return new Size(configuration2.ContentOrder != QTabButtonContentOrder.None ? num2 + (qspacing1.All + size2.Width) + (qspacing2.All + size1.Width) : num2 + Math.Max(qspacing1.All + size2.Width, qspacing2.All + size1.Width), configuration2.Padding.Vertical + Math.Max(size1.Height, size2.Height));
    }

    private Size CalculateButtonSizeHorizontal(QTabButton tabButton, Graphics graphics)
    {
      QTabStrip tabStrip = tabButton.TabStrip;
      QTabStripConfiguration configuration1 = tabStrip.Configuration;
      QTabButtonConfiguration configuration2 = tabButton.Configuration;
      Size buttonContentSize = this.CalculateButtonContentSize(tabButton, graphics);
      Size shapeSize = configuration2.Appearance.Shape.CalculateShapeSize(buttonContentSize, tabStrip.IsHorizontal);
      Size biggestShape = configuration2.CalculateBiggestShape(tabStrip.IsHorizontal);
      Size size = new Size(configuration2.MinimumSize.Width < 0 ? biggestShape.Width : 0, configuration2.MinimumSize.Height < 0 ? biggestShape.Height : 0);
      size.Width = Math.Max(configuration2.MinimumSize.Width, size.Width);
      size.Height = Math.Max(configuration2.MinimumSize.Height, size.Height);
      Size maximumSize = configuration2.MaximumSize;
      if (maximumSize.Width >= 0)
        shapeSize.Width = Math.Min(maximumSize.Width, shapeSize.Width);
      if (size.Width >= 0)
        shapeSize.Width = Math.Max(size.Width, shapeSize.Width);
      if (maximumSize.Height >= 0)
        shapeSize.Height = Math.Min(maximumSize.Height, shapeSize.Height);
      if (size.Height >= 0)
        shapeSize.Height = Math.Max(size.Height, shapeSize.Height);
      tabButton.CalculatedMinimumSize = size;
      tabButton.CalculatedMaximumSize = new Size(maximumSize.Width >= 0 ? Math.Max(maximumSize.Width, size.Width) : int.MaxValue, maximumSize.Height >= 0 ? Math.Max(maximumSize.Height, size.Height) : int.MaxValue);
      return shapeSize;
    }

    private Size CalculateButtonContentSizeVertical(QTabButton tabButton, Graphics graphics)
    {
      int num1 = 0;
      QTabStripConfiguration configuration1 = tabButton.TabStrip.Configuration;
      QTabButtonConfiguration configuration2 = tabButton.Configuration;
      StringFormat stringFormat = this.StringFormat;
      Font biggestFont = QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(configuration1.Font, configuration1.FontActive, graphics), configuration1.FontHot, graphics);
      string text = tabButton.Text;
      Icon icon = tabButton.RetrieveIcon(configuration2.IconSize, tabButton.Enabled);
      Size size1 = icon != null ? configuration2.IconSize : Size.Empty;
      Size size2 = QControlPaint.MeasureString(text, biggestFont, false, stringFormat, graphics);
      QSpacing qspacing1 = icon != null ? configuration2.IconSpacing : QSpacing.Empty;
      QSpacing qspacing2 = !QMisc.IsEmpty((object) text) ? configuration2.TextSpacing : QSpacing.Empty;
      int num2 = num1 + configuration2.Padding.Horizontal;
      int height = configuration2.ContentOrder != QTabButtonContentOrder.None ? num2 + (qspacing1.All + size1.Height) + (qspacing2.All + size2.Height) : num2 + Math.Max(qspacing1.All + size1.Height, qspacing2.All + size2.Height);
      return new Size(configuration2.Padding.Vertical + Math.Max(size2.Width, size1.Width), height);
    }

    private Size CalculateButtonSizeVertical(QTabButton tabButton, Graphics graphics)
    {
      QTabStrip tabStrip = tabButton.TabStrip;
      QTabStripConfiguration configuration1 = tabStrip.Configuration;
      QTabButtonConfiguration configuration2 = tabButton.Configuration;
      Size buttonContentSize = this.CalculateButtonContentSize(tabButton, graphics);
      Size shapeSize = configuration2.Appearance.Shape.CalculateShapeSize(buttonContentSize, tabStrip.IsHorizontal);
      Size biggestShape = configuration2.CalculateBiggestShape(tabStrip.IsHorizontal);
      Size size1 = new Size(configuration2.MinimumSize.Height < 0 ? biggestShape.Width : 0, configuration2.MinimumSize.Width < 0 ? biggestShape.Height : 0);
      size1.Width = Math.Max(configuration2.MinimumSize.Height, size1.Width);
      size1.Height = Math.Max(configuration2.MinimumSize.Width, size1.Height);
      Size size2 = new Size(configuration2.MaximumSize.Height, configuration2.MaximumSize.Width);
      if (size2.Width >= 0)
        shapeSize.Width = Math.Min(size2.Width, shapeSize.Width);
      if (size1.Width >= 0)
        shapeSize.Width = Math.Max(size1.Width, shapeSize.Width);
      if (size2.Height >= 0)
        shapeSize.Height = Math.Min(size2.Height, shapeSize.Height);
      if (size1.Height >= 0)
        shapeSize.Height = Math.Max(size1.Height, shapeSize.Height);
      tabButton.CalculatedMinimumSize = size1;
      tabButton.CalculatedMaximumSize = new Size(size2.Width >= 0 ? Math.Max(size2.Width, size1.Width) : int.MaxValue, size2.Height >= 0 ? Math.Max(size2.Height, size1.Height) : int.MaxValue);
      return shapeSize;
    }

    protected virtual Size CalculateNavigationAreaSize(
      QTabStrip tabStrip,
      bool inflateWithMargin)
    {
      if (tabStrip.NavigationArea.VisibleButtonCount == 0)
        return Size.Empty;
      Size empty = Size.Empty;
      QTabStripConfiguration configuration = tabStrip.Configuration;
      for (int index = 0; index < tabStrip.NavigationArea.ButtonAreas.Length; ++index)
      {
        if (tabStrip.NavigationArea.ButtonAreas[index].Visible)
        {
          empty.Width += tabStrip.NavigationArea.ButtonAreas[index].RequestedSize.Width;
          empty.Height = Math.Max(empty.Height, tabStrip.NavigationArea.ButtonAreas[index].RequestedSize.Height);
        }
      }
      Size size1 = configuration.NavigationAreaPadding.InflateSizeWithPadding(empty, true, true);
      Size contentSize = tabStrip.IsHorizontal ? size1 : new Size(size1.Height, size1.Width);
      Size size2 = configuration.NavigationAreaAppearance.Shape.CalculateShapeSize(contentSize, tabStrip.IsHorizontal);
      if (inflateWithMargin)
        size2 = configuration.NavigationAreaMargin.InflateSizeWithMargin(size2, true, tabStrip.IsHorizontal);
      return size2;
    }

    protected virtual void CalculateNavigationAreaLayout(QTabStrip tabStrip, Size tabStripSize)
    {
      Rectangle contentBounds1 = this.CalculateContentBounds(tabStrip, tabStripSize, tabStrip.Configuration.NavigationAreaMargin);
      Size navigationAreaSize = this.CalculateNavigationAreaSize(tabStrip, false);
      if (tabStrip.IsHorizontal)
      {
        if (tabStrip.Configuration.NavigationAreaAlignment == QContentAlignment.Stretched)
          navigationAreaSize.Height = contentBounds1.Height;
        QRange qrange1 = QMath.AlignElement(navigationAreaSize.Height, tabStrip.Configuration.NavigationAreaAlignment, tabStrip.Dock == DockStyle.Top, QRange.FromStartEnd(contentBounds1.Top, contentBounds1.Bottom));
        tabStrip.NavigationArea.Bounds = new Rectangle(contentBounds1.Right - navigationAreaSize.Width, qrange1.Start, navigationAreaSize.Width, navigationAreaSize.Height);
        Rectangle contentBounds2 = tabStrip.Configuration.NavigationAreaAppearance.Shape.CalculateContentBounds(new Rectangle(Point.Empty, tabStrip.NavigationArea.Size), tabStrip.Dock);
        Rectangle rectangle = tabStrip.Configuration.NavigationAreaPadding.InflateRectangleWithPadding(contentBounds2, false, tabStrip.Dock);
        int right = rectangle.Right;
        for (int index = tabStrip.NavigationArea.ButtonAreas.Length - 1; index >= 0; --index)
        {
          QButtonArea buttonArea = tabStrip.NavigationArea.ButtonAreas[index];
          if (buttonArea.Visible)
          {
            QRange qrange2 = QMath.AlignElement(buttonArea.RequestedSize.Height, tabStrip.Configuration.NavigationAreaContentAlignment, tabStrip.Dock == DockStyle.Top, QRange.FromStartEnd(rectangle.Top, rectangle.Bottom));
            buttonArea.Bounds = new Rectangle(right - buttonArea.RequestedSize.Width, qrange2.Start, buttonArea.RequestedSize.Width, buttonArea.RequestedSize.Height);
            right -= buttonArea.Size.Width;
          }
        }
      }
      else
      {
        if (tabStrip.Configuration.NavigationAreaAlignment == QContentAlignment.Stretched)
          navigationAreaSize.Width = contentBounds1.Width;
        QRange qrange3 = QMath.AlignElement(navigationAreaSize.Width, tabStrip.Configuration.NavigationAreaAlignment, tabStrip.Dock == DockStyle.Left, QRange.FromStartEnd(contentBounds1.Left, contentBounds1.Right));
        tabStrip.NavigationArea.Bounds = new Rectangle(qrange3.Start, contentBounds1.Bottom - navigationAreaSize.Height, navigationAreaSize.Width, navigationAreaSize.Height);
        Rectangle contentBounds3 = tabStrip.Configuration.NavigationAreaAppearance.Shape.CalculateContentBounds(new Rectangle(Point.Empty, tabStrip.NavigationArea.Size), tabStrip.Dock);
        Rectangle rectangle = tabStrip.Configuration.NavigationAreaPadding.InflateRectangleWithPadding(contentBounds3, false, tabStrip.Dock);
        int bottom = rectangle.Bottom;
        for (int index = tabStrip.NavigationArea.ButtonAreas.Length - 1; index >= 0; --index)
        {
          QButtonArea buttonArea = tabStrip.NavigationArea.ButtonAreas[index];
          if (buttonArea.Visible)
          {
            QRange qrange4 = QMath.AlignElement(buttonArea.RequestedSize.Height, tabStrip.Configuration.NavigationAreaContentAlignment, tabStrip.Dock == DockStyle.Left, QRange.FromStartEnd(rectangle.Left, rectangle.Right));
            buttonArea.Bounds = new Rectangle(qrange4.Start, bottom - buttonArea.RequestedSize.Width, buttonArea.RequestedSize.Height, buttonArea.RequestedSize.Width);
            bottom -= buttonArea.Size.Height;
          }
        }
      }
    }

    protected virtual Size CalculateButtonSize(QTabButton tabButton, Graphics graphics)
    {
      if (tabButton.IsDisposed)
        return Size.Empty;
      if (tabButton.Configuration.Orientation == QContentOrientation.Horizontal)
        return this.CalculateButtonSizeHorizontal(tabButton, graphics);
      return tabButton.Configuration.Orientation == QContentOrientation.VerticalUp || tabButton.Configuration.Orientation == QContentOrientation.VerticalDown ? this.CalculateButtonSizeVertical(tabButton, graphics) : Size.Empty;
    }

    protected virtual Size CalculateButtonContentSize(QTabButton tabButton, Graphics graphics)
    {
      if (tabButton.IsDisposed)
        return Size.Empty;
      if (tabButton.Configuration.Orientation == QContentOrientation.Horizontal)
        return this.CalculateButtonContentSizeHorizontal(tabButton, graphics);
      return tabButton.Configuration.Orientation == QContentOrientation.VerticalUp || tabButton.Configuration.Orientation == QContentOrientation.VerticalDown ? this.CalculateButtonContentSizeVertical(tabButton, graphics) : Size.Empty;
    }

    public virtual Size CalculateStripLayout(
      QTabStrip tabStrip,
      Size availableSize,
      Graphics graphics)
    {
      Size empty = Size.Empty;
      this.ClearRows(tabStrip);
      tabStrip.TabButtons.SortWhenRequired();
      tabStrip.NavigationArea.ScrollButtonsVisible = tabStrip.Configuration.IsScroll && tabStrip.Configuration.ScrollButtonsAlwaysVisible;
      return !tabStrip.IsHorizontal ? this.CalculateStripLayoutVertical(tabStrip, availableSize, graphics) : this.CalculateStripLayoutHorizontal(tabStrip, availableSize, graphics);
    }

    internal void DrawDropArea(Graphics graphics, QTabStrip tabStrip, QTabButtonDropArea dropArea)
    {
      if (dropArea == null || !dropArea.AllowDrop)
        return;
      QTabStripPaintParams stripPaintParams = tabStrip.RetrievePaintParams();
      if (stripPaintParams == null)
        return;
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = QMisc.GetSmoothingMode(tabStrip.Configuration.Appearance.SmoothingMode);
      Pen pen = new Pen(stripPaintParams.DropIndicatorBorder, 1f);
      Brush brush = (Brush) new SolidBrush(stripPaintParams.DropIndicatorBackground);
      bool flag1 = false;
      bool flag2 = false;
      Rectangle bounds = dropArea.Bounds;
      if (dropArea.LastInRow && dropArea.Button != null && dropArea.Dock == QTabButtonDockStyle.Bottom)
      {
        int num1 = dropArea.Button.BoundsToControl.Top - bounds.Top;
        int num2 = dropArea.Button.Height + num1 * 2;
        if (bounds.Height > num2)
          bounds.Height = num2;
      }
      else if (dropArea.LastInRow && dropArea.Button != null && dropArea.Dock == QTabButtonDockStyle.Right)
      {
        int num3 = dropArea.Button.BoundsToControl.Left - bounds.Left;
        int num4 = dropArea.Button.Width + num3 * 2;
        if (bounds.Width > num4)
          bounds.Width = num4;
      }
      else if (dropArea.FirstInRow && dropArea.Button != null && dropArea.Dock == QTabButtonDockStyle.Left)
      {
        int num5 = bounds.Right - dropArea.Button.BoundsToControl.Right;
        int num6 = dropArea.Button.Width + num5 * 2;
        if (bounds.Width > num6)
        {
          bounds.Width = num6;
          bounds.X = dropArea.Button.BoundsToControl.X - num5;
        }
      }
      else if (dropArea.FirstInRow && dropArea.Button != null && dropArea.Dock == QTabButtonDockStyle.Top)
      {
        int num7 = bounds.Bottom - dropArea.Button.BoundsToControl.Bottom;
        int num8 = dropArea.Button.Height + num7 * 2;
        if (bounds.Height > num8)
        {
          bounds.Height = num8;
          bounds.Y = dropArea.Button.BoundsToControl.Y - num7;
        }
      }
      Point pt1 = Point.Empty;
      Point pt2 = Point.Empty;
      switch (dropArea.Dock)
      {
        case QTabButtonDockStyle.Top:
          flag1 = dropArea.FirstInRow;
          pt1 = new Point(bounds.Left, bounds.Top);
          pt2 = new Point(bounds.Right, bounds.Top);
          break;
        case QTabButtonDockStyle.Right:
          flag2 = dropArea.LastInRow;
          pt1 = new Point(bounds.Right, bounds.Top);
          pt2 = new Point(bounds.Right, bounds.Bottom);
          break;
        case QTabButtonDockStyle.Bottom:
          flag2 = dropArea.LastInRow;
          pt1 = new Point(bounds.Left, bounds.Bottom);
          pt2 = new Point(bounds.Right, bounds.Bottom);
          break;
        case QTabButtonDockStyle.Left:
          flag1 = dropArea.FirstInRow;
          pt1 = new Point(bounds.Left, bounds.Top);
          pt2 = new Point(bounds.Left, bounds.Bottom);
          break;
      }
      if (dropArea.Button == null)
      {
        flag1 = dropArea.TabStrip.Configuration.ButtonConfiguration.Alignment == QTabButtonAlignment.Near;
        flag2 = !flag1;
      }
      graphics.DrawLine(pen, pt1, pt2);
      if (dropArea.Dock == QTabButtonDockStyle.Top || dropArea.Dock == QTabButtonDockStyle.Bottom)
      {
        if (!flag1)
          this.DrawArrow(graphics, new Point(pt1.X + (pt2.X - pt1.X - 9) / 2, pt1.Y - 11), DockStyle.Bottom, brush, pen);
        if (!flag2)
          this.DrawArrow(graphics, new Point(pt1.X + (pt2.X - pt1.X - 9) / 2, pt1.Y + 2), DockStyle.Top, brush, pen);
      }
      else
      {
        if (!flag1)
          this.DrawArrow(graphics, new Point(pt1.X - 11, pt1.Y + (pt2.Y - pt1.Y - 9) / 2), DockStyle.Right, brush, pen);
        if (!flag2)
          this.DrawArrow(graphics, new Point(pt1.X + 2, pt1.Y + (pt2.Y - pt1.Y - 9) / 2), DockStyle.Left, brush, pen);
      }
      graphics.SmoothingMode = smoothingMode;
      pen.Dispose();
      brush.Dispose();
    }

    private void DrawArrow(
      Graphics graphics,
      Point location,
      DockStyle pointAt,
      Brush brush,
      Pen pen)
    {
      if (pointAt == DockStyle.None || pointAt == DockStyle.Fill)
        return;
      float num = 4.5f;
      float x = 3f;
      GraphicsPath path = new GraphicsPath();
      path.AddPolygon(new PointF[7]
      {
        this.DockArrowPoint(new PointF(x, 0.0f), location, pointAt),
        this.DockArrowPoint(new PointF(2f * x, 0.0f), location, pointAt),
        this.DockArrowPoint(new PointF(2f * x, num), location, pointAt),
        this.DockArrowPoint(new PointF(9f, num), location, pointAt),
        this.DockArrowPoint(new PointF(num, 9f), location, pointAt),
        this.DockArrowPoint(new PointF(0.0f, num), location, pointAt),
        this.DockArrowPoint(new PointF(x, num), location, pointAt)
      });
      path.CloseAllFigures();
      graphics.FillPath(brush, path);
      graphics.DrawPath(pen, path);
    }

    private PointF DockArrowPoint(PointF point, Point location, DockStyle pointAt)
    {
      if (pointAt == DockStyle.Top || pointAt == DockStyle.Left)
        point.Y = Math.Abs(point.Y - 9f);
      if (pointAt == DockStyle.Right || pointAt == DockStyle.Left)
      {
        point.Y += point.X;
        point.X = point.Y - point.X;
        point.Y -= point.X;
      }
      point.X += (float) location.X;
      point.Y += (float) location.Y;
      return point;
    }

    public virtual void DrawTabStrip(QTabStrip tabStrip, Graphics graphics)
    {
      QTabStripPaintParams stripPaintParams = tabStrip.RetrievePaintParams();
      if (stripPaintParams == null)
        return;
      QTabStripAppearance appearance = tabStrip.Configuration.Appearance;
      if (appearance == null)
        return;
      QShape shape = appearance.Shape;
      QAppearanceFillerProperties fillerProperties = new QAppearanceFillerProperties();
      fillerProperties.DockStyle = tabStrip.Dock;
      QColorSet colors = new QColorSet(stripPaintParams.Background1, stripPaintParams.Background2, stripPaintParams.Border);
      tabStrip.LastDrawnGraphicsPath = QShapePainter.Default.FillBackground(tabStrip.Bounds, shape, (IQAppearance) appearance, colors, new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape), fillerProperties, QPainterOptions.Default, graphics);
      Region clip = graphics.Clip;
      Rectangle bounds = tabStrip.Bounds;
      Rectangle rectangle = tabStrip.CalculateBoundsToControl(tabStrip.CalculatedContentBounds, false);
      if (tabStrip.Configuration.ButtonAreaClip)
        rectangle = Rectangle.Intersect(rectangle, tabStrip.CalculateBoundsToControl(tabStrip.CalculatedButtonAreaBounds, false));
      if (tabStrip.Dock == DockStyle.Top)
        rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, bounds.Bottom + 1);
      else if (tabStrip.Dock == DockStyle.Left)
        rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, bounds.Right + 1, rectangle.Bottom);
      else if (tabStrip.Dock == DockStyle.Right)
        rectangle = Rectangle.FromLTRB(bounds.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
      else if (tabStrip.Dock == DockStyle.Bottom)
        rectangle = Rectangle.FromLTRB(rectangle.Left, bounds.Top, rectangle.Right, rectangle.Bottom);
      Region region = new Region(rectangle);
      if (clip != null)
        region.Intersect(clip);
      graphics.Clip = region;
      for (QTabButton previousVisibleButton = tabStrip.GetPreviousVisibleButton((QTabButton) null, false); previousVisibleButton != null; previousVisibleButton = tabStrip.GetPreviousVisibleButton(previousVisibleButton, false))
      {
        if (tabStrip.ActiveButton != previousVisibleButton)
          this.DrawTabButton(previousVisibleButton, graphics);
      }
      graphics.Clip = clip;
      QShapePainter.Default.FillForeground(tabStrip.Bounds, shape, (IQAppearance) appearance, colors, QShapePainterProperties.Default, fillerProperties, QPainterOptions.Default, graphics);
      graphics.Clip = region;
      if (tabStrip.ActiveButton != null)
        this.DrawTabButton(tabStrip.ActiveButton, graphics);
      graphics.Clip = clip;
      this.DrawNavigationArea(tabStrip, stripPaintParams, graphics);
      region.Dispose();
    }

    protected internal virtual void DrawTabButton(QTabButton button, Graphics graphics)
    {
      if (button.IsDisposed || button.TabStrip == null)
        return;
      QTabStrip tabStrip = button.TabStrip;
      if (tabStrip == null)
        return;
      QTabButtonPaintParams buttonPaintParams = button.RetrievePaintParams();
      if (buttonPaintParams == null)
        return;
      QTabStripConfiguration configuration1 = tabStrip.Configuration;
      if (configuration1 == null)
        return;
      QTabButtonConfiguration configuration2 = button.Configuration;
      if (configuration2 == null)
        return;
      string text = button.Text;
      Icon icon = button.RetrieveIcon(configuration2.IconSize, button.Enabled);
      Color textColor = buttonPaintParams.ButtonText;
      Font font = configuration1.Font;
      Color backColor1 = buttonPaintParams.ButtonBackground1;
      Color backColor2 = buttonPaintParams.ButtonBackground2;
      Color borderColor = buttonPaintParams.ButtonBorder;
      QTabButtonAppearance buttonAppearance = configuration2.Appearance;
      int num1 = buttonAppearance.UseControlBackgroundForTabButton ? 1 : 0;
      if (button.IsActivated)
      {
        backColor1 = buttonPaintParams.ButtonActiveBackground1;
        backColor2 = buttonPaintParams.ButtonActiveBackground2;
        borderColor = buttonPaintParams.ButtonActiveBorder;
        buttonAppearance = configuration2.AppearanceActive;
        int num2 = buttonAppearance.UseControlBackgroundForTabButton ? 1 : 0;
        textColor = buttonPaintParams.ButtonActiveText;
        font = configuration1.FontActive;
      }
      else if (button.IsHot)
      {
        backColor1 = buttonPaintParams.ButtonHotBackground1;
        backColor2 = buttonPaintParams.ButtonHotBackground2;
        borderColor = buttonPaintParams.ButtonHotBorder;
        buttonAppearance = configuration2.AppearanceHot;
        int num3 = buttonAppearance.UseControlBackgroundForTabButton ? 1 : 0;
        textColor = buttonPaintParams.ButtonHotText;
        font = configuration1.FontHot;
      }
      else if (!button.Enabled)
        textColor = buttonPaintParams.ButtonTextDisabled;
      Rectangle rectangle1 = Rectangle.Empty;
      if (configuration1.UseStackExtendBackground)
        rectangle1 = tabStrip.CalculateBoundsToControl(tabStrip.CalculatedContentBounds, true);
      Rectangle rectangle2 = button.BoundsToControl;
      Rectangle contentBounds = buttonAppearance.Shape.CalculateContentBounds(rectangle2, tabStrip.Dock);
      Rectangle bounds = configuration2.Padding.InflateRectangleWithPadding(contentBounds, false, configuration2.Orientation);
      if (configuration1.UseStackExtendBackground)
      {
        if (tabStrip.Dock == DockStyle.Top)
          rectangle2 = Rectangle.FromLTRB(rectangle2.Left, rectangle2.Top, rectangle2.Right, rectangle1.Bottom);
        else if (tabStrip.Dock == DockStyle.Left)
          rectangle2 = Rectangle.FromLTRB(rectangle2.Left, rectangle2.Top, rectangle1.Right, rectangle2.Bottom);
        else if (tabStrip.Dock == DockStyle.Right)
          rectangle2 = Rectangle.FromLTRB(rectangle1.Left, rectangle2.Top, rectangle2.Right, rectangle2.Bottom);
        else if (tabStrip.Dock == DockStyle.Bottom)
          rectangle2 = Rectangle.FromLTRB(rectangle2.Left, rectangle1.Top, rectangle2.Right, rectangle2.Bottom);
      }
      Rectangle andControlBounds = this.GetButtonAndControlBounds(button);
      if (rectangle2.Width > 0 && rectangle2.Height > 0)
      {
        this.DrawTabButtonShade(button, buttonAppearance, rectangle2, tabStrip.Dock, buttonPaintParams.ButtonShade, graphics);
        this.DrawTabButtonBackground(button, configuration2, configuration1, buttonAppearance, rectangle2, andControlBounds, tabStrip.Dock, backColor1, backColor2, borderColor, graphics);
      }
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return;
      this.DrawTabButtonContent(button, configuration2, text, icon, buttonPaintParams.IconReplace, buttonPaintParams.IconReplaceColorWith, textColor, font, bounds, graphics);
    }

    protected virtual void DrawTabButtonShade(
      QTabButton button,
      QTabButtonAppearance buttonAppearance,
      Rectangle buttonBounds,
      DockStyle dockStyle,
      Color color,
      Graphics graphics)
    {
      QControlPaint.DrawShapeShade((IQShadedShapeAppearance) buttonAppearance, buttonBounds, dockStyle, color, graphics);
    }

    protected virtual void DrawTabButtonBackground(
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
      QShape shape = buttonAppearance.Shape;
      QColorSet colors = new QColorSet(backColor1, backColor2, borderColor);
      QAppearanceFillerProperties fillerProperties = new QAppearanceFillerProperties();
      fillerProperties.DockStyle = dockStyle;
      if (buttonAppearance.UseControlBackgroundForTabButton)
      {
        if (!(button.Control is QContainerControl control))
          throw new InvalidOperationException(QResources.GetException("QTabButton_CannotUseControlBackground"));
        fillerProperties.AlternativeBoundsForBrushCreation = controlAndButtonBounds;
        button.LastDrawnGraphicsPath = QShapePainter.Default.FillBackground(buttonBounds, shape, (IQAppearance) control.Appearance, new QColorSet(control.BackColor, control.BackColor2), new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape), fillerProperties, QPainterOptions.Default, graphics);
      }
      else
      {
        Math.Ceiling((double) buttonAppearance.BorderWidth / 2.0);
        button.LastDrawnGraphicsPath = QShapePainter.Default.FillBackground(buttonBounds, shape, (IQAppearance) buttonAppearance, colors, new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape), fillerProperties, QPainterOptions.Default, graphics);
      }
      if (button.BackgroundImage == null || !buttonConfiguration.BackgroundImageClip)
      {
        if (button.BackgroundImage != null)
        {
          QImageAlign imageAlign = QMath.RotateImageAlign(buttonConfiguration.BackgroundImageAlign, buttonConfiguration.Orientation);
          QControlPaint.DrawImage(button.BackgroundImage, imageAlign, buttonBounds, button.BackgroundImage.Size, graphics, (ImageAttributes) null, false);
        }
      }
      else
      {
        Region savedRegion = QControlPaint.AdjustClip(graphics, new Region(button.LastDrawnGraphicsPath), CombineMode.Intersect);
        QImageAlign imageAlign = QMath.RotateImageAlign(buttonConfiguration.BackgroundImageAlign, buttonConfiguration.Orientation);
        QControlPaint.DrawImage(button.BackgroundImage, imageAlign, buttonBounds, button.BackgroundImage.Size, graphics, (ImageAttributes) null, false);
        QControlPaint.RestoreClip(graphics, savedRegion);
      }
      QShapePainter.Default.FillForeground(buttonBounds, shape, (IQAppearance) buttonAppearance, colors, QShapePainterProperties.Default, fillerProperties, QPainterOptions.Default, graphics);
    }

    protected virtual void DrawTabButtonContent(
      QTabButton button,
      QTabButtonConfiguration buttonConfiguration,
      string text,
      Icon icon,
      Color replaceColor,
      Color replaceColorWith,
      Color textColor,
      Font font,
      Rectangle bounds,
      Graphics graphics)
    {
      bool horizontal = buttonConfiguration.Orientation == QContentOrientation.Horizontal;
      StringFormat stringFormat = this.StringFormat;
      Size size1 = QControlPaint.MeasureString(text, font, horizontal, stringFormat, graphics);
      Size size2 = icon != null ? buttonConfiguration.IconSize : Size.Empty;
      QSpacing qspacing1 = icon != null ? buttonConfiguration.IconSpacing : QSpacing.Empty;
      QSpacing qspacing2 = !QMisc.IsEmpty((object) text) ? buttonConfiguration.TextSpacing : QSpacing.Empty;
      Rectangle empty1 = Rectangle.Empty;
      Rectangle empty2 = Rectangle.Empty;
      Rectangle rectangle1 = QMath.AlignElement(qspacing1.InflateSizeWithSpacing(size2, true, buttonConfiguration.Orientation), QMath.RotateAlignment(buttonConfiguration.IconAlignment, buttonConfiguration.Orientation), bounds, false);
      Rectangle rectangle2 = QMath.AlignElement(qspacing2.InflateSizeWithSpacing(size1, true, buttonConfiguration.Orientation), QMath.RotateAlignment(buttonConfiguration.TextAlignment, buttonConfiguration.Orientation), bounds, false);
      QRange qrange1;
      QRange qrange2;
      QRange fullRange;
      if (horizontal)
      {
        qrange1 = new QRange(rectangle1.Left, rectangle1.Width);
        qrange2 = new QRange(rectangle2.Left, rectangle2.Width);
        fullRange = new QRange(bounds.Left, bounds.Width);
      }
      else
      {
        qrange1 = new QRange(rectangle1.Top, rectangle1.Height);
        qrange2 = new QRange(rectangle2.Top, rectangle2.Height);
        fullRange = new QRange(bounds.Top, bounds.Height);
      }
      if (buttonConfiguration.Orientation == QContentOrientation.Horizontal || buttonConfiguration.Orientation == QContentOrientation.VerticalDown)
      {
        if (buttonConfiguration.ContentOrder == QTabButtonContentOrder.IconText)
          QMath.AlignRangesNextToEachOther(ref qrange1, ref qrange2, fullRange, true);
        else if (buttonConfiguration.ContentOrder == QTabButtonContentOrder.TextIcon)
          QMath.AlignRangesNextToEachOther(ref qrange2, ref qrange1, fullRange, false);
      }
      else if (buttonConfiguration.Orientation == QContentOrientation.VerticalUp)
      {
        if (buttonConfiguration.ContentOrder == QTabButtonContentOrder.IconText)
          QMath.AlignRangesNextToEachOther(ref qrange2, ref qrange1, fullRange, false);
        else if (buttonConfiguration.ContentOrder == QTabButtonContentOrder.TextIcon)
          QMath.AlignRangesNextToEachOther(ref qrange1, ref qrange2, fullRange, true);
      }
      if (icon != null)
      {
        rectangle1 = !horizontal ? new Rectangle(rectangle1.Left, qrange1.Start, rectangle1.Width, qrange1.Size) : new Rectangle(qrange1.Start, rectangle1.Top, qrange1.Size, rectangle1.Height);
        rectangle1 = qspacing1.InflateRectangleWithSpacing(rectangle1, false, buttonConfiguration.Orientation);
        if (rectangle1.Width > 0 && rectangle1.Height > 0)
        {
          if (!button.Enabled && buttonConfiguration.GrayscaleDisabledIcon)
            QControlPaint.DrawIconDisabled(graphics, icon, rectangle1);
          else
            QControlPaint.DrawIcon(graphics, icon, replaceColor, replaceColorWith, rectangle1);
        }
      }
      if (!QMisc.IsEmpty((object) text))
      {
        rectangle2 = !horizontal ? new Rectangle(rectangle2.Left, qrange2.Start, rectangle2.Width, qrange2.Size) : new Rectangle(qrange2.Start, rectangle2.Top, qrange2.Size, rectangle2.Height);
        rectangle2 = qspacing2.InflateRectangleWithSpacing(rectangle2, false, buttonConfiguration.Orientation);
        if (rectangle2.Width > 0 && rectangle2.Height > 0)
        {
          Brush brush = (Brush) new SolidBrush(textColor);
          QControlPaint.DrawString(text, font, rectangle2, buttonConfiguration.Orientation, brush, stringFormat, graphics);
          brush.Dispose();
        }
      }
      if (!button.Focused || button.DrawingOnBitmap)
        return;
      ControlPaint.DrawFocusRectangle(graphics, bounds, textColor, SystemColors.Control);
    }

    public virtual Rectangle GetButtonAndControlBounds(QTabButton button)
    {
      if (button.TabStrip == null)
        return Rectangle.Empty;
      Rectangle boundsToControl = button.BoundsToControl;
      int borderWidth = button.Configuration.Appearance.BorderWidth;
      int val2 = Math.Max(button.Configuration.AppearanceActive.BorderWidth, borderWidth);
      Math.Ceiling((double) Math.Max(button.Configuration.AppearanceHot.BorderWidth, val2) / 2.0);
      Rectangle empty = Rectangle.Empty;
      Rectangle forBackgroundFill = button.TabButtonSource.GetBoundsForBackgroundFill();
      return Rectangle.Union(boundsToControl, forBackgroundFill);
    }

    protected virtual void DrawNavigationArea(
      QTabStrip tabStrip,
      QTabStripPaintParams stripPaintParams,
      Graphics graphics)
    {
      QTabStripNavigationAreaAppearance navigationAreaAppearance = tabStrip.Configuration.NavigationAreaAppearance;
      if (tabStrip.NavigationArea.VisibleButtonCount == 0)
        return;
      Rectangle boundsToControl1 = tabStrip.NavigationArea.BoundsToControl;
      int num = (int) Math.Ceiling((double) navigationAreaAppearance.BorderWidth / 2.0);
      Rectangle rectangle1 = new Rectangle(boundsToControl1.Left - num, boundsToControl1.Top - num, boundsToControl1.Width + num * 2, boundsToControl1.Height + num * 2);
      tabStrip.NavigationArea.LastDrawnGraphicsPath = QShapePainter.Default.Paint(boundsToControl1, navigationAreaAppearance.Shape, (IQAppearance) navigationAreaAppearance, new QColorSet(stripPaintParams.NavigationAreaBackground1, stripPaintParams.NavigationAreaBackground2, stripPaintParams.NavigationAreaBorder), new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape), new QAppearanceFillerProperties()
      {
        AlternativeBoundsForBrushCreation = rectangle1,
        DockStyle = tabStrip.Dock
      }, QPainterOptions.Default, graphics);
      for (int index = 0; index < tabStrip.NavigationArea.ButtonAreas.Length; ++index)
      {
        QButtonArea buttonArea = tabStrip.NavigationArea.ButtonAreas[index];
        if (buttonArea.Visible)
        {
          Image image = buttonArea.AdditionalData as Image;
          if (!tabStrip.IsHorizontal)
            image = (Image) QControlPaint.RotateFlipImage(image, RotateFlipType.Rotate90FlipNone);
          Rectangle boundsToControl2 = tabStrip.NavigationArea.CalculateBoundsToControl(buttonArea.Bounds);
          Rectangle rectangle2 = tabStrip.Configuration.NavigationButtonPadding.InflateRectangleWithPadding(boundsToControl2, false, tabStrip.Dock);
          if (buttonArea.Enabled)
          {
            QColorSet colors = (QColorSet) null;
            Color replaceColorWith = stripPaintParams.NavigationButtonReplaceWith;
            if (buttonArea.State == QButtonState.Hot)
            {
              colors = new QColorSet(stripPaintParams.NavigationButtonBackground1Hot, stripPaintParams.NavigationButtonBackground2Hot, stripPaintParams.NavigationButtonBorderHot);
              replaceColorWith = stripPaintParams.NavigationButtonReplaceWithHot;
            }
            else if (buttonArea.State == QButtonState.Pressed)
            {
              colors = new QColorSet(stripPaintParams.NavigationButtonBackground1Active, stripPaintParams.NavigationButtonBackground2Active, stripPaintParams.NavigationButtonBorderActive);
              replaceColorWith = stripPaintParams.NavigationButtonReplaceWithActive;
            }
            if (colors != null)
            {
              QShapeAppearance buttonHotAppearance1 = tabStrip.Configuration.UsedNavigationButtonHotAppearance as QShapeAppearance;
              QAppearance buttonHotAppearance2 = tabStrip.Configuration.UsedNavigationButtonHotAppearance as QAppearance;
              if (buttonHotAppearance1 != null)
              {
                QShapePainterProperties properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
                QAppearanceFillerProperties fillerProperties = new QAppearanceFillerProperties();
                fillerProperties.DockStyle = tabStrip.Dock;
                rectangle2 = buttonHotAppearance1.Shape.InflateRectangle(rectangle2, false, tabStrip.Dock);
                QShapePainter.Default.Paint(boundsToControl2, buttonHotAppearance1.Shape, (IQAppearance) buttonHotAppearance1, colors, properties, fillerProperties, QPainterOptions.Default, graphics);
              }
              else if (buttonHotAppearance2 != null)
                QRectanglePainter.Default.Paint(boundsToControl2, (IQAppearance) buttonHotAppearance2, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
            }
            QControlPaint.DrawImage(graphics, image, stripPaintParams.NavigationButtonReplace, replaceColorWith, rectangle2);
          }
          else
            QControlPaint.DrawImage(graphics, image, stripPaintParams.NavigationButtonReplace, stripPaintParams.NavigationButtonReplaceWithDisabled, rectangle2);
        }
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      this.CloseWindowsXPTheme();
      if (!disposing)
        return;
      if (this.m_oStringFormat != null)
        this.m_oStringFormat.Dispose();
      this.m_oStringFormat = (StringFormat) null;
      if (this.m_oStringFormat != null)
        this.m_oStringFormat.Dispose();
      this.m_oStringFormat = (StringFormat) null;
    }

    ~QTabStripPainter() => this.Dispose(false);
  }
}
