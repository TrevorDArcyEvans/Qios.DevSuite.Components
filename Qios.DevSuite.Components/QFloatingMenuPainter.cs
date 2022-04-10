// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingMenuPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QFloatingMenuPainter : QCommandContainerPainter
  {
    private static QAppearanceWrapper m_oIconBackgroundAppearance;
    private static QAppearanceWrapper m_oCheckedItemAppearance;

    public QFloatingMenuPainter()
    {
      this.CommandPainter = (QCommandPainter) new QFloatinMenuItemPainter();
      if (QFloatingMenuPainter.m_oIconBackgroundAppearance == null)
      {
        QFloatingMenuPainter.m_oIconBackgroundAppearance = new QAppearanceWrapper((IQAppearance) null);
        QFloatingMenuPainter.m_oIconBackgroundAppearance.BackgroundStyle = QColorStyle.Gradient;
        QFloatingMenuPainter.m_oIconBackgroundAppearance.GradientAngle = 180;
        QFloatingMenuPainter.m_oIconBackgroundAppearance.GradientBlendPosition = 70;
        QFloatingMenuPainter.m_oIconBackgroundAppearance.GradientBlendFactor = 100;
      }
      if (QFloatingMenuPainter.m_oCheckedItemAppearance != null)
        return;
      QFloatingMenuPainter.m_oCheckedItemAppearance = new QAppearanceWrapper((IQAppearance) null);
      QFloatingMenuPainter.m_oCheckedItemAppearance.BackgroundStyle = QColorStyle.Gradient;
      QFloatingMenuPainter.m_oCheckedItemAppearance.GradientAngle = 0;
    }

    public QFloatinMenuItemPainter ItemPainter => (QFloatinMenuItemPainter) this.CommandPainter;

    public void CalculateRequestedSize(
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      QCommandCollection commands,
      Graphics graphics)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      int num1 = qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Top;
      this.CalculateRangesAndItemOuterBounds(paintParams, configuration, destinationControl, commands, graphics);
      for (int index = 0; index < commands.Count; ++index)
      {
        QMenuItem qmenuItem = (QMenuItem) QMisc.AssertObjectOfType((object) commands.GetCommand(index), "Command", typeof (QMenuItem));
        if (qmenuItem.IsVisible)
          num1 += qmenuItem.OuterBounds.Height;
        else
          qmenuItem.Bounds = Rectangle.Empty;
      }
      if (qfloatingMenuPaintParams.ShowDepersonalizeItem)
      {
        int num2 = menuConfiguration.UsedDepersonalizeMenuMask.Height + menuConfiguration.ItemMargin.Vertical + menuConfiguration.ItemPadding.Vertical;
        num1 += num2;
      }
      int height = num1 + qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Bottom;
      qfloatingMenuPaintParams.RequestedSize = new Size(qfloatingMenuPaintParams.BorderWidth * 2 + menuConfiguration.FloatingMenuPadding.Horizontal + qfloatingMenuPaintParams.ItemOuterBoundsWidth, height);
    }

    public void CalculateRangesAndItemOuterBounds(
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      QCommandCollection commands,
      Graphics graphics)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration configuration1 = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      int num1 = 0;
      int num2 = 0;
      int size1 = 0;
      int size2 = 0;
      int val1 = 0;
      QSpacing qspacing1 = QSpacing.Empty;
      QSpacing qspacing2 = QSpacing.Empty;
      QSpacing qspacing3 = QSpacing.Empty;
      QSpacing qspacing4 = QSpacing.Empty;
      if (configuration1.IconsVisible)
      {
        qspacing1 = configuration1.IconSpacing;
        size2 = configuration1.IconSize.Width;
      }
      if (configuration1.IconsVisible && configuration1.IconBackgroundVisible)
        qfloatingMenuPaintParams.IconBackgroundRange = new QRange(0, size2 + configuration1.ItemPadding.Left + configuration1.ItemMargin.Left + qspacing1.All);
      for (int index = 0; index < commands.Count; ++index)
      {
        QMenuItem command = (QMenuItem) commands.GetCommand(index);
        command.EmptyCachedObjects();
        if (!command.IsSeparator)
        {
          command.Orientation = QCommandOrientation.Horizontal;
          command.Padding = configuration1.ItemPadding;
          command.Margin = configuration1.ItemMargin;
          this.ItemPainter.CalculateItemContents((QCommand) command, (QCommandConfiguration) configuration1, configuration1.MinimumItemHeight, qfloatingMenuPaintParams.StringFormat, destinationControl, graphics);
          bool flag1 = this.ItemPainter.ShowTitle((QCommand) command, configuration, destinationControl);
          bool flag2 = this.ItemPainter.ShowShortcut((QCommand) command, configuration, destinationControl);
          QSpacing qspacing5 = this.ItemPainter.UsedControlSpacing((QCommand) command, configuration, destinationControl);
          if (configuration1.ShortcutLayout == QFloatingMenuShortcutLayout.InSeparateColumn)
          {
            if (flag1)
            {
              num1 = Math.Max(num1, command.TextBounds.Width + (command.ControlBounds.Width + qspacing5.All));
              qspacing2 = configuration1.TitleSpacing;
            }
            if (flag2)
            {
              num2 = Math.Max(command.ShortcutBounds.Width, num2);
              qspacing3 = configuration1.ShortcutSpacing;
            }
          }
          else if (configuration1.ShortcutLayout == QFloatingMenuShortcutLayout.InTitleColumn)
          {
            if (flag1)
              qspacing2 = configuration1.TitleSpacing;
            if (flag2)
              qspacing3 = configuration1.ShortcutSpacing;
            val1 = Math.Max(val1, command.TextBounds.Width + command.ShortcutBounds.Width + (command.ControlBounds.Width + qspacing5.All));
          }
          if (this.ItemPainter.ShowHasChildItems((QCommand) command, configuration, destinationControl))
          {
            size1 = configuration1.UsedHasChildItemsMask.Width;
            qspacing4 = configuration1.HasChildItemsSpacing;
          }
          if (this.ItemPainter.ShowControl((QCommand) command, configuration, destinationControl))
            command.SetControlParent();
        }
        else
        {
          command.Orientation = QCommandOrientation.Horizontal;
          command.Padding = QPadding.Empty;
          command.Margin = new QMargin(0, configuration1.SeparatorSpacing.Before, configuration1.SeparatorSpacing.After, 0);
          command.PutSeparatorBounds(new Rectangle(0, 0, 0, configuration1.CalculateSeparatorSize(QCommandOrientation.Vertical)));
          this.ItemPainter.CalculateItemContents((QCommand) command, (QCommandConfiguration) configuration1, configuration1.MinimumItemHeight, qfloatingMenuPaintParams.StringFormat, destinationControl, graphics);
        }
      }
      int before = qspacing1.Before;
      qfloatingMenuPaintParams.IconRange = new QRange(before, size2);
      int start1 = before + (qfloatingMenuPaintParams.IconRange.Size + qspacing1.After + qspacing2.Before);
      if (configuration1.ShortcutLayout == QFloatingMenuShortcutLayout.InSeparateColumn)
      {
        qfloatingMenuPaintParams.TitleRange = new QRange(start1, num1);
        int start2 = start1 + (qfloatingMenuPaintParams.TitleRange.Size + qspacing2.After + qspacing3.Before);
        qfloatingMenuPaintParams.ShortcutRange = new QRange(start2, num2);
        start1 = start2 + (qfloatingMenuPaintParams.ShortcutRange.Size + qspacing3.After + qspacing4.Before);
      }
      else if (configuration1.ShortcutLayout == QFloatingMenuShortcutLayout.InTitleColumn)
      {
        qfloatingMenuPaintParams.TitleRange = new QRange(start1, val1 + qspacing2.After + qspacing3.Before);
        qfloatingMenuPaintParams.ShortcutRange = qfloatingMenuPaintParams.TitleRange;
        start1 += qfloatingMenuPaintParams.ShortcutRange.Size + qspacing3.After + qspacing4.Before;
      }
      qfloatingMenuPaintParams.HasChildItemsIconRange = new QRange(start1, size1);
      int num3 = start1 + (qfloatingMenuPaintParams.HasChildItemsIconRange.Size + qspacing4.After);
      qfloatingMenuPaintParams.ItemOuterBoundsWidth = configuration.ItemPadding.Horizontal + configuration.ItemMargin.Horizontal + num3;
    }

    public override void LayoutHorizontal(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      base.LayoutHorizontal(rectangle, configuration, paintParams, destinationControl, commands);
      Graphics graphics = destinationControl.CreateGraphics();
      this.CalculateRangesAndItemOuterBounds(paintParams, configuration, destinationControl, commands, graphics);
      int height1 = qfloatingMenuPaintParams.ShowDepersonalizeItem ? menuConfiguration.UsedDepersonalizeMenuMask.Height + menuConfiguration.ItemMargin.Vertical + menuConfiguration.ItemPadding.Vertical : 0;
      int size = menuConfiguration.IconsVisible ? menuConfiguration.IconSize.Width : 0;
      Rectangle rectangle1 = new Rectangle(qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Left, qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Top, destinationControl.ClientSize.Width - (qfloatingMenuPaintParams.BorderWidth * 2 + menuConfiguration.FloatingMenuPadding.Horizontal), destinationControl.ClientSize.Height - (qfloatingMenuPaintParams.BorderWidth * 2 + menuConfiguration.FloatingMenuPadding.Vertical));
      int top = rectangle1.Top;
      int height2 = menuConfiguration.UsedHasMoreItemsUpMask.Height;
      int height3 = menuConfiguration.UsedHasMoreItemsDownMask.Height;
      if (qfloatingMenuPaintParams.FirstVisibleItem > 0)
      {
        for (int index = 0; index < qfloatingMenuPaintParams.FirstVisibleItem; ++index)
          commands.GetCommand(index).Bounds = Rectangle.Empty;
        qfloatingMenuPaintParams.HasMoreItemsUpBounds = new Rectangle(rectangle1.Left, top, rectangle1.Width, height2);
        top += height2;
      }
      else
        qfloatingMenuPaintParams.HasMoreItemsUpBounds = Rectangle.Empty;
      for (int firstVisibleItem = qfloatingMenuPaintParams.FirstVisibleItem; firstVisibleItem < commands.Count; ++firstVisibleItem)
      {
        QMenuItem command = (QMenuItem) commands.GetCommand(firstVisibleItem);
        if (command.IsVisible)
        {
          command.PutOuterBounds(new Rectangle(rectangle1.Left, top, rectangle1.Width, command.OuterBounds.Height));
          if (!command.IsSeparator)
          {
            command.PutIconBounds(new Rectangle(QMath.GetStartForCenter(qfloatingMenuPaintParams.IconRange.Start, qfloatingMenuPaintParams.IconRange.End, size), 0, command.IconBounds.Width, command.IconBounds.Height));
            command.PutTextBounds(new Rectangle(qfloatingMenuPaintParams.TitleRange.Start, 0, command.TextBounds.Width, command.TextBounds.Height));
            command.PutShortcutBounds(new Rectangle(qfloatingMenuPaintParams.ShortcutRange.End - command.ShortcutBounds.Width, 0, command.ShortcutBounds.Width, command.ShortcutBounds.Height));
            command.PutHasChildItemsBounds(new Rectangle(qfloatingMenuPaintParams.HasChildItemsIconRange.Start, 0, command.HasChildItemsBounds.Width, command.HasChildItemsBounds.Height));
            QSpacing qspacing = this.ItemPainter.UsedTitleSpacing((QCommand) command, configuration, destinationControl);
            command.PutControlBounds(new Rectangle(command.TextBounds.Right + qspacing.After + menuConfiguration.ControlSpacing.Before, 0, command.ControlBounds.Width, command.ControlBounds.Height));
            this.ItemPainter.AlignContentsElements((QCommand) command, StringAlignment.Center);
            command.CalculateControlBoundsProperties(false);
          }
          else
          {
            int x = menuConfiguration.ItemPadding.Left + menuConfiguration.ItemMargin.Left + menuConfiguration.SeparatorRelativeStart + (!menuConfiguration.IconBackgroundVisible || !menuConfiguration.IconsVisible ? 0 : qfloatingMenuPaintParams.TitleRange.Start);
            int width = rectangle1.Width - x + menuConfiguration.SeparatorRelativeEnd;
            command.PutSeparatorBounds(new Rectangle(x, command.SeparatorBounds.Top, width, command.SeparatorBounds.Height));
          }
          top += command.OuterBounds.Height;
        }
        else
          command.PutOuterBounds(Rectangle.Empty);
      }
      if (height1 > 0)
      {
        qfloatingMenuPaintParams.DepersonalizeItemBounds = new Rectangle(rectangle1.Left, top, rectangle1.Width, height1);
        top += height1;
      }
      else
        qfloatingMenuPaintParams.DepersonalizeItemBounds = Rectangle.Empty;
      if (top > rectangle1.Bottom)
      {
        rectangle1.Height -= height3;
        if (height1 > 0)
        {
          top -= height1;
          qfloatingMenuPaintParams.DepersonalizeItemBounds = Rectangle.Empty;
        }
        int index = commands.Count - 1;
        while (index >= 0 && top > rectangle1.Bottom)
        {
          QMenuItem command = (QMenuItem) commands.GetCommand(index);
          top -= command.OuterBounds.Height;
          --index;
          qfloatingMenuPaintParams.LastVisibleItem = index;
        }
        qfloatingMenuPaintParams.HasMoreItemsDownBounds = new Rectangle(rectangle1.Left, rectangle1.Bottom, rectangle1.Width, height3);
        int num = top + height3;
      }
      else
        qfloatingMenuPaintParams.HasMoreItemsDownBounds = Rectangle.Empty;
    }

    public virtual void DrawHasMoreItemsUpImage(
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Graphics graphics)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      Rectangle rectangle = new Rectangle(QMath.GetStartForCenter(qfloatingMenuPaintParams.HasMoreItemsUpBounds.Left + menuConfiguration.ItemMargin.Left + menuConfiguration.ItemPadding.Left, qfloatingMenuPaintParams.HasMoreItemsUpBounds.Right, menuConfiguration.UsedHasMoreItemsUpMask.Width), qfloatingMenuPaintParams.HasMoreItemsUpBounds.Top, menuConfiguration.UsedHasMoreItemsUpMask.Width, menuConfiguration.UsedHasMoreItemsUpMask.Height);
      QControlPaint.DrawImage(graphics, menuConfiguration.UsedHasMoreItemsUpMask, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.TextColor, rectangle);
    }

    public virtual void DrawHasMoreItemsDownImage(
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Graphics graphics)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      Rectangle rectangle = new Rectangle(QMath.GetStartForCenter(qfloatingMenuPaintParams.HasMoreItemsDownBounds.Left + menuConfiguration.ItemMargin.Left + menuConfiguration.ItemPadding.Left, qfloatingMenuPaintParams.HasMoreItemsDownBounds.Right, menuConfiguration.UsedHasMoreItemsDownMask.Width), qfloatingMenuPaintParams.HasMoreItemsDownBounds.Top, menuConfiguration.UsedHasMoreItemsDownMask.Width, menuConfiguration.UsedHasMoreItemsDownMask.Height);
      QControlPaint.DrawImage(graphics, menuConfiguration.UsedHasMoreItemsDownMask, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.TextColor, rectangle);
    }

    public virtual void DrawDepersonalizeMenuImage(
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QButtonState depersonalizeButtonState,
      Graphics graphics)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      QCommandAppearance activatedItemAppearance = configuration.ActivatedItemAppearance;
      Rectangle bounds = new Rectangle(qfloatingMenuPaintParams.DepersonalizeItemBounds.Left + menuConfiguration.ItemMargin.Left, qfloatingMenuPaintParams.DepersonalizeItemBounds.Top + menuConfiguration.ItemMargin.Top, qfloatingMenuPaintParams.DepersonalizeItemBounds.Width - menuConfiguration.ItemMargin.Horizontal, qfloatingMenuPaintParams.DepersonalizeItemBounds.Height - menuConfiguration.ItemMargin.Vertical);
      QColorSet colors = (QColorSet) null;
      switch (depersonalizeButtonState)
      {
        case QButtonState.Hot:
          colors = new QColorSet(qfloatingMenuPaintParams.HotItemBackground1Color, qfloatingMenuPaintParams.HotItemBackground2Color, qfloatingMenuPaintParams.HotItemBorderColor);
          break;
        case QButtonState.Pressed:
          colors = new QColorSet(qfloatingMenuPaintParams.PressedItemBackground1Color, qfloatingMenuPaintParams.PressedItemBackground2Color, qfloatingMenuPaintParams.PressedItemBorderColor);
          break;
      }
      if (colors != null)
        QRectanglePainter.Default.Paint(bounds, (IQAppearance) activatedItemAppearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      Rectangle rectangle = new Rectangle(bounds.Left + menuConfiguration.ItemPadding.Left, bounds.Top + menuConfiguration.ItemPadding.Top, bounds.Width - menuConfiguration.ItemPadding.Horizontal, bounds.Height - menuConfiguration.ItemPadding.Vertical);
      QControlPaint.DrawImage(QControlPaint.CreateColorizedImage(menuConfiguration.UsedDepersonalizeMenuMask, qfloatingMenuPaintParams.DepersonalizeImageBackground, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.DepersonalizeImageForeground), QImageAlign.Centered, rectangle, graphics);
    }

    public override void DrawControlBackgroundHorizontal(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
      base.DrawControlBackgroundHorizontal(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      int num = qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Left;
      if (menuConfiguration.IconsVisible && menuConfiguration.IconBackgroundVisible)
        QRectanglePainter.Default.FillBackground(new Rectangle(num + qfloatingMenuPaintParams.IconBackgroundRange.Start, qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Top, qfloatingMenuPaintParams.IconBackgroundRange.Size, destinationControl.Height - (qfloatingMenuPaintParams.BorderWidth * 2 + menuConfiguration.FloatingMenuPadding.Vertical)), (IQAppearance) QFloatingMenuPainter.m_oIconBackgroundAppearance, new QColorSet(qfloatingMenuPaintParams.IconBackground2Color, qfloatingMenuPaintParams.IconBackground1Color), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      if (!(qfloatingMenuPaintParams.ParentIntersectionBounds != Rectangle.Empty))
        return;
      Brush brush = (Brush) new SolidBrush(qfloatingMenuPaintParams.ParentMenuIntersectColor);
      graphics.FillRectangle(brush, qfloatingMenuPaintParams.ParentIntersectionBounds);
      brush.Dispose();
    }

    public override void DrawItem(
      QCommand command,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QCommandContainer parentContainer,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
      QFloatingMenuPaintParams qfloatingMenuPaintParams = (QFloatingMenuPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QFloatingMenuPaintParams));
      QFloatingMenuConfiguration menuConfiguration = (QFloatingMenuConfiguration) QMisc.AssertObjectOfType((object) configuration, nameof (configuration), typeof (QFloatingMenuConfiguration));
      QMenuItem qmenuItem = (QMenuItem) QMisc.AssertObjectOfType((object) command, nameof (command), typeof (QMenuItem));
      base.DrawItem(command, configuration, paintParams, parentContainer, flags, graphics);
      if (!qmenuItem.IsVisible)
        return;
      bool flag1 = (flags & QCommandPaintOptions.Hot) == QCommandPaintOptions.Hot;
      bool flag2 = (flags & QCommandPaintOptions.Expanded) == QCommandPaintOptions.Expanded;
      bool flag3 = (flags & QCommandPaintOptions.Pressed) == QCommandPaintOptions.Pressed;
      if (menuConfiguration.IconsVisible && menuConfiguration.IconBackgroundVisible && !qmenuItem.VisibleWhenPersonalized)
        QRectanglePainter.Default.FillBackground(new Rectangle(qfloatingMenuPaintParams.BorderWidth + menuConfiguration.FloatingMenuPadding.Left + qfloatingMenuPaintParams.IconBackgroundRange.Start, qmenuItem.OuterBounds.Top, qfloatingMenuPaintParams.IconBackgroundRange.Size, qmenuItem.OuterBounds.Height), (IQAppearance) QFloatingMenuPainter.m_oIconBackgroundAppearance, new QColorSet(qfloatingMenuPaintParams.IconBackgroundDepersonalized2Color, qfloatingMenuPaintParams.IconBackgroundDepersonalized1Color), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
      if (!qmenuItem.IsSeparator)
      {
        if (flag3 || flag1 || flag2)
        {
          QCommandAppearance activatedItemAppearance = configuration.ActivatedItemAppearance;
          int backgroundOptions = (int) activatedItemAppearance.GetDrawControlBackgroundOptions(true);
          if (qmenuItem.IsEnabled)
          {
            QColorSet colors = (QColorSet) null;
            if (flag3)
              colors = new QColorSet(qfloatingMenuPaintParams.PressedItemBackground1Color, qfloatingMenuPaintParams.PressedItemBackground2Color, qfloatingMenuPaintParams.PressedItemBorderColor);
            else if (flag1 || flag2)
              colors = new QColorSet(qfloatingMenuPaintParams.HotItemBackground1Color, qfloatingMenuPaintParams.HotItemBackground2Color, qfloatingMenuPaintParams.HotItemBorderColor);
            if (colors != null)
              QRectanglePainter.Default.Paint(qmenuItem.Bounds, (IQAppearance) activatedItemAppearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          else if (!qmenuItem.MouseIsOverMenuItem)
            QRectanglePainter.Default.FillForeground(qmenuItem.Bounds, (IQAppearance) activatedItemAppearance, new QColorSet(Color.Empty, Color.Empty, qfloatingMenuPaintParams.HotItemBorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        if (menuConfiguration.IconsVisible)
        {
          if (qmenuItem.Checked)
          {
            int num = 2;
            Rectangle parent = qmenuItem.ContentsRectangleToParent(qmenuItem.IconBounds);
            parent.Inflate(num, num);
            QRectanglePainter.Default.Paint(parent, (IQAppearance) QFloatingMenuPainter.m_oCheckedItemAppearance, new QColorSet(qfloatingMenuPaintParams.CheckedItemBackground1Color, qfloatingMenuPaintParams.CheckedItemBackground2Color, qfloatingMenuPaintParams.CheckedItemBorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          if (qmenuItem.UsedIcon != null)
          {
            if (qmenuItem.IsEnabled || !qmenuItem.DisabledIconGrayScaled)
            {
              Color replaceColorWith = flag1 || flag2 ? qfloatingMenuPaintParams.TextActiveColor : qfloatingMenuPaintParams.TextColor;
              QControlPaint.DrawIcon(graphics, qmenuItem.UsedIcon, qmenuItem.UsedIconReplaceColor, replaceColorWith, qmenuItem.ContentsRectangleToParent(qmenuItem.IconBounds));
            }
            else
              QControlPaint.DrawIconDisabled(graphics, qmenuItem.UsedIcon, qmenuItem.ContentsRectangleToParent(qmenuItem.IconBounds));
          }
        }
        Brush brush = !qmenuItem.IsEnabled || !menuConfiguration.TitlesVisible && !menuConfiguration.ShortcutsVisible ? (Brush) new SolidBrush(qfloatingMenuPaintParams.TextDisabledColor) : (flag1 || flag2 ? (Brush) new SolidBrush(qfloatingMenuPaintParams.TextActiveColor) : (Brush) new SolidBrush(qfloatingMenuPaintParams.TextColor));
        if (menuConfiguration.TitlesVisible)
          graphics.DrawString(qmenuItem.Title, qfloatingMenuPaintParams.Font, brush, (RectangleF) qmenuItem.ContentsRectangleToParent(qmenuItem.TextBounds), qfloatingMenuPaintParams.StringFormat);
        if (menuConfiguration.ShortcutsVisible && qmenuItem.ShortcutKeys != Keys.None)
          graphics.DrawString(qmenuItem.ShortcutString, qfloatingMenuPaintParams.Font, brush, (RectangleF) qmenuItem.ContentsRectangleToParent(qmenuItem.ShortcutBounds), qfloatingMenuPaintParams.StringFormat);
        if (menuConfiguration.HasChildItemsImageVisible && qmenuItem.HasChildItems)
        {
          Image image = !qmenuItem.IsEnabled ? QControlPaint.CreateColorizedImage(menuConfiguration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.TextDisabledColor) : (flag1 || flag2 ? QControlPaint.CreateColorizedImage(menuConfiguration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.TextActiveColor) : QControlPaint.CreateColorizedImage(menuConfiguration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.TextColor));
          graphics.DrawImage(image, qmenuItem.ContentsRectangleToParent(qmenuItem.HasChildItemsBounds));
        }
        brush?.Dispose();
      }
      else if (menuConfiguration.SeparatorMask != null)
      {
        QControlPaint.DrawImage(menuConfiguration.SeparatorMask, Color.FromArgb((int) byte.MaxValue, 0, 0), qfloatingMenuPaintParams.SeparatorColor, QImageAlign.RepeatedHorizontal, qmenuItem.ContentsRectangleToParent(qmenuItem.SeparatorBounds), menuConfiguration.SeparatorMask.Size, graphics);
      }
      else
      {
        Brush brush = (Brush) new SolidBrush(qfloatingMenuPaintParams.SeparatorColor);
        graphics.FillRectangle(brush, qmenuItem.ContentsRectangleToParent(qmenuItem.SeparatorBounds));
        brush.Dispose();
      }
    }
  }
}
