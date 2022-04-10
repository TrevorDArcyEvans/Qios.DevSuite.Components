// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarPainter
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
  internal class QExplorerBarPainter : QCommandContainerPainter
  {
    private Size m_oScrollSize;
    private bool m_bSuppressControlMovement;

    public QExplorerBarPainter() => this.CommandPainter = (QCommandPainter) new QExplorerBarItemPainter();

    public QExplorerBarItemPainter ItemPainter => (QExplorerBarItemPainter) this.CommandPainter;

    public override void DrawControlBackgroundHorizontal(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
      if (rectangle.Width <= 0 || rectangle.Height <= 0)
        return;
      QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QExplorerBarPaintParams));
      base.DrawControlBackgroundHorizontal(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
    }

    public override void DrawControlBackgroundVertical(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
      if (rectangle.Width <= 0 || rectangle.Height <= 0)
        return;
      QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QExplorerBarPaintParams));
      base.DrawControlBackgroundVertical(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
    }

    public override void LayoutHorizontal(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      base.LayoutHorizontal(rectangle, configuration, paintParams, destinationControl, commands);
    }

    public override void LayoutVertical(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      base.LayoutVertical(rectangle, configuration, paintParams, destinationControl, commands);
      QExplorerBarPaintParams qexplorerBarPaintParams = (QExplorerBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QExplorerBarPaintParams));
      Graphics graphics1 = destinationControl.CreateGraphics();
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int left1 = rectangle.Left;
      int num5 = rectangle.Top;
      int width1 = rectangle.Width;
      int num6 = 0;
      bool flag1 = true;
      QRange qrange = new QRange(0, 0);
      QExplorerBar qexplorerBar = destinationControl as QExplorerBar;
      QPadding qpadding;
      QMargin qmargin;
      Rectangle rectangle1;
      for (int index1 = 0; index1 < commands.Count; ++index1)
      {
        if (commands.GetCommand(index1) is QExplorerItem)
        {
          QExplorerItem command = (QExplorerItem) commands.GetCommand(index1);
          QCommandConfiguration configuration1 = command.ItemType == QExplorerItemType.GroupItem ? (QCommandConfiguration) qexplorerBarPaintParams.GroupItemConfiguration : (QCommandConfiguration) qexplorerBarPaintParams.ItemConfiguration;
          if (command.IsVisible)
          {
            command.Orientation = command.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemConfiguration.ItemOrientation : qexplorerBarPaintParams.ItemConfiguration.ItemOrientation;
            if (command.IsSeparator)
            {
              qpadding = configuration1.ItemPadding;
              int left2 = qpadding.Left;
              qmargin = configuration1.ItemMargin;
              int left3 = qmargin.Left;
              int x = left2 + left3 + configuration1.SeparatorRelativeStart;
              int width2 = rectangle.Width - x + configuration1.SeparatorRelativeEnd - (configuration1.ItemPadding.Right + configuration1.ItemMargin.Right);
              int num7 = command.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemConfiguration.ItemSpacing : 0;
              command.Orientation = QCommandOrientation.Horizontal;
              command.Padding = QPadding.Empty;
              command.Margin = new QMargin(0, configuration1.SeparatorSpacing.Before + num7, configuration1.SeparatorSpacing.After, 0);
              command.PutSeparatorBounds(new Rectangle(x, 0, width2, configuration1.CalculateSeparatorSize(QCommandOrientation.Vertical)));
              command.PutContentsBounds(command.SeparatorBounds);
            }
            else
            {
              if (command.ItemType == QExplorerItemType.GroupItem)
              {
                command.Padding = qexplorerBarPaintParams.GroupItemConfiguration.ItemPadding;
                command.Margin = new QMargin(qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin.Left, qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin.Top + (flag1 ? 0 : qexplorerBarPaintParams.GroupItemConfiguration.ItemSpacing), qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin.Bottom, qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin.Right);
                if (command.Expanded)
                  ++num2;
                if (command.ItemState == QExplorerItemState.Expanded)
                  ++num1;
              }
              else
              {
                command.Padding = new QPadding(qexplorerBarPaintParams.ItemConfiguration.ItemPadding.Left, qexplorerBarPaintParams.ItemConfiguration.ItemPadding.Top, qexplorerBarPaintParams.ItemConfiguration.ItemPadding.Bottom, qexplorerBarPaintParams.ItemConfiguration.ItemPadding.Right);
                command.Margin = new QMargin(qexplorerBarPaintParams.ItemConfiguration.ItemMargin.Left, qexplorerBarPaintParams.ItemConfiguration.ItemMargin.Top, qexplorerBarPaintParams.ItemConfiguration.ItemMargin.Bottom, qexplorerBarPaintParams.ItemConfiguration.ItemMargin.Right);
              }
              command.PutTextBounds(Rectangle.Empty);
              this.ItemPainter.CalculateItemContents(commands.GetCommand(index1), command.ItemType == QExplorerItemType.GroupItem ? (QCommandConfiguration) qexplorerBarPaintParams.GroupItemConfiguration : (QCommandConfiguration) qexplorerBarPaintParams.ItemConfiguration, command.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemConfiguration.MinimumItemHeight : qexplorerBarPaintParams.ItemConfiguration.MinimumItemHeight, command.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemStringFormat : qexplorerBarPaintParams.ItemStringFormat, destinationControl, graphics1);
              if (this.ItemPainter.ShowShortcut((QCommand) command, configuration1, destinationControl))
              {
                int start = qrange.Start;
                rectangle1 = command.ShortcutBounds;
                int x1 = rectangle1.X;
                if (start < x1)
                {
                  ref QRange local = ref qrange;
                  rectangle1 = command.ShortcutBounds;
                  int x2 = rectangle1.X;
                  local.Start = x2;
                }
              }
              if (command.ItemType == QExplorerItemType.GroupItem)
              {
                QExplorerItem qexplorerItem = command;
                Rectangle iconBounds = command.IconBounds;
                rectangle1 = command.IconBoundsForPaint;
                int height1 = rectangle1.Height;
                rectangle1 = command.ContentsBounds;
                int height2 = rectangle1.Height;
                int height3 = Math.Min(height1, height2);
                Rectangle rectangle2 = QMath.SetHeight(iconBounds, height3);
                qexplorerItem.PutIconBounds(rectangle2);
              }
              if (this.ItemPainter.ShowIcon((QCommand) command, configuration1, destinationControl))
                num5 += command.IconBoundsOverlap;
              Rectangle rectangle3;
              ref Rectangle local1 = ref rectangle3;
              int x3 = rectangle.X;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin;
              int left4 = qmargin.Left;
              int x4 = x3 + left4;
              int num8 = num5;
              rectangle1 = command.OuterBounds;
              int height4 = rectangle1.Height;
              int num9 = num8 + height4;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin;
              int bottom = qmargin.Bottom;
              int y = num9 - bottom;
              int width3 = rectangle.Width;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.ItemMargin;
              int horizontal = qmargin.Horizontal;
              int width4 = width3 - horizontal;
              int num10 = rectangle.Height - num5 - rectangle.Top;
              rectangle1 = command.OuterBounds;
              int height5 = rectangle1.Height;
              int height6 = num10 + height5;
              local1 = new Rectangle(x4, y, width4, height6);
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
              Rectangle rectangle4 = qmargin.InflateRectangleWithMargin(rectangle3, false, true);
              qpadding = qexplorerBarPaintParams.GroupItemConfiguration.PanelPadding;
              rectangle3 = qpadding.InflateRectangleWithPadding(rectangle4, false, true);
              if (command.Expanded && command.ItemType == QExplorerItemType.GroupItem)
              {
                this.LayoutVertical(rectangle3, configuration, paintParams, destinationControl, command.Commands);
                num6 = 0;
                for (int index2 = 0; index2 < command.Commands.Count; ++index2)
                {
                  int num11 = num6;
                  rectangle1 = command.MenuItems[index2].OuterBounds;
                  int height7 = rectangle1.Height;
                  num6 = num11 + height7;
                }
              }
              else
                num6 = 0;
              if (command.ShowDepersonalizeItem)
              {
                int height8 = qexplorerBarPaintParams.GroupItemConfiguration.UsedDepersonalizeMenuMask.Height;
                qmargin = qexplorerBarPaintParams.ItemConfiguration.ItemMargin;
                int vertical1 = qmargin.Vertical;
                int num12 = height8 + vertical1;
                qpadding = qexplorerBarPaintParams.ItemConfiguration.ItemPadding;
                int vertical2 = qpadding.Vertical;
                int num13 = num12 + vertical2;
                num6 += num13;
              }
            }
            QExplorerItem qexplorerItem1 = command;
            int x5 = left1;
            int y1 = num5;
            rectangle1 = command.OuterBounds;
            int width5 = rectangle1.Width;
            rectangle1 = command.OuterBounds;
            int height9 = rectangle1.Height;
            Rectangle rectangle5 = new Rectangle(x5, y1, width5, height9);
            qexplorerItem1.PutOuterBounds(rectangle5);
            if (QExplorerBarPainter.RenderChildren(command))
            {
              QExplorerItem qexplorerItem2 = command;
              rectangle1 = command.Bounds;
              int x6 = rectangle1.X;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
              int left5 = qmargin.Left;
              int x7 = x6 + left5;
              rectangle1 = command.Bounds;
              int y2 = rectangle1.Y;
              rectangle1 = command.Bounds;
              int height10 = rectangle1.Height;
              int num14 = y2 + height10;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
              int top = qmargin.Top;
              int y3 = num14 + top;
              rectangle1 = command.Bounds;
              int width6 = rectangle1.Width;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
              int horizontal = qmargin.Horizontal;
              int width7 = width6 - horizontal;
              int num15 = num6;
              qpadding = qexplorerBarPaintParams.GroupItemConfiguration.PanelPadding;
              int vertical3 = qpadding.Vertical;
              int height11 = num15 + vertical3;
              Rectangle rectangle6 = new Rectangle(x7, y3, width7, height11);
              qexplorerItem2.PutPanelBounds(rectangle6);
              int num16 = num5;
              qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
              int vertical4 = qmargin.Vertical;
              num5 = num16 + vertical4;
            }
            else
              command.PutPanelBounds(Rectangle.Empty);
            int num17 = num5;
            rectangle1 = command.OuterBounds;
            int height12 = rectangle1.Height;
            rectangle1 = command.PanelBounds;
            int height13 = rectangle1.Height;
            int num18 = height12 + height13;
            num5 = num17 + num18;
          }
          else
            command.PutOuterBounds(Rectangle.Empty);
          if (command.IsVisible)
            flag1 = false;
        }
      }
      int num19 = Math.Max(0, rectangle.Bottom - num5);
      QSpacing qspacing;
      for (int index = 0; index < commands.Count; ++index)
      {
        if (commands.GetCommand(index) is QExplorerItem)
        {
          QExplorerItem command = (QExplorerItem) commands.GetCommand(index);
          QCommandConfiguration configuration2 = command.ItemType == QExplorerItemType.GroupItem ? (QCommandConfiguration) qexplorerBarPaintParams.GroupItemConfiguration : (QCommandConfiguration) qexplorerBarPaintParams.ItemConfiguration;
          bool flag2 = this.ItemPainter.ShowHasChildItems((QCommand) command, configuration2, destinationControl);
          if (command.IsVisible && !command.IsSeparator)
          {
            command.PutOuterBounds(QMath.SetWidth(command.OuterBounds, width1));
            QExplorerItem qexplorerItem3 = command;
            Rectangle panelBounds = command.PanelBounds;
            rectangle1 = command.Bounds;
            int width8 = rectangle1.Width;
            qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
            int horizontal1 = qmargin.Horizontal;
            int width9 = width8 - horizontal1;
            Rectangle rectangle7 = QMath.SetWidth(panelBounds, width9);
            qexplorerItem3.PutPanelBounds(rectangle7);
            QExplorerItem qexplorerItem4 = command;
            Rectangle childItemsBounds = command.HasChildItemsBounds;
            rectangle1 = command.OuterBounds;
            int width10 = rectangle1.Width;
            qmargin = configuration2.ItemMargin;
            int horizontal2 = qmargin.Horizontal;
            int num20 = width10 - horizontal2;
            int num21;
            if (!flag2)
            {
              num21 = 0;
            }
            else
            {
              rectangle1 = command.HasChildItemsBounds;
              int width11 = rectangle1.Width;
              qspacing = configuration2.HasChildItemsSpacing;
              int after = qspacing.After;
              num21 = width11 + after;
            }
            int num22 = num20 - num21;
            qpadding = configuration2.ItemPadding;
            int horizontal3 = qpadding.Horizontal;
            int num23 = num22 - horizontal3;
            Rectangle rectangle8 = QMath.SetX(childItemsBounds, num23);
            qexplorerItem4.PutHasChildItemsBounds(rectangle8);
            if (this.ItemPainter.ShowShortcut((QCommand) command, configuration2, destinationControl))
            {
              int end = qrange.End;
              rectangle1 = command.HasChildItemsBounds;
              int x8 = rectangle1.X;
              int num24;
              if (!flag2)
              {
                num24 = 0;
              }
              else
              {
                qspacing = configuration2.HasChildItemsSpacing;
                num24 = qspacing.Before;
              }
              int num25 = x8 - num24;
              qspacing = configuration2.ShortcutSpacing;
              int after1 = qspacing.After;
              int num26 = num25 - after1;
              if (end > num26 || qrange.Start == qrange.End)
              {
                ref QRange local = ref qrange;
                rectangle1 = command.HasChildItemsBounds;
                int x9 = rectangle1.X;
                int num27;
                if (!flag2)
                {
                  num27 = 0;
                }
                else
                {
                  qspacing = configuration2.HasChildItemsSpacing;
                  num27 = qspacing.Before;
                }
                int num28 = x9 - num27;
                qspacing = configuration2.ShortcutSpacing;
                int after2 = qspacing.After;
                int num29 = num28 - after2;
                local.End = num29;
              }
            }
            if (qexplorerBar != null && this.ItemPainter.ShowHasChildItems((QCommand) command, configuration2, destinationControl))
            {
              int all = this.ItemPainter.UsedHasChildItemsSpacing((QCommand) command, configuration2, destinationControl).All;
              rectangle1 = command.HasChildItemsBounds;
              int width12 = rectangle1.Width;
              int num30 = all + width12;
              QExplorerItem qexplorerItem5 = command;
              rectangle1 = command.Bounds;
              int x = rectangle1.Right - num30;
              rectangle1 = command.Bounds;
              int top = rectangle1.Top;
              int width13 = num30;
              rectangle1 = command.Bounds;
              int height = rectangle1.Height;
              Rectangle rectangle9 = new Rectangle(x, top, width13, height);
              qexplorerItem5.PutHasChildItemsHotBounds(rectangle9);
            }
          }
        }
      }
      for (int index = 0; index < commands.Count; ++index)
      {
        if (commands.GetCommand(index) is QExplorerItem)
        {
          QExplorerItem command = (QExplorerItem) commands.GetCommand(index);
          QCommandConfiguration configuration3 = command.ItemType == QExplorerItemType.GroupItem ? (QCommandConfiguration) qexplorerBarPaintParams.GroupItemConfiguration : (QCommandConfiguration) qexplorerBarPaintParams.ItemConfiguration;
          QExplorerBarGroupItemConfiguration itemConfiguration1 = configuration3 as QExplorerBarGroupItemConfiguration;
          QExplorerBarItemConfiguration itemConfiguration2 = configuration3 as QExplorerBarItemConfiguration;
          bool flag3 = this.ItemPainter.ShowControl((QCommand) command, configuration3, destinationControl);
          bool flag4 = this.ItemPainter.ShowShortcut((QCommand) command, configuration3, destinationControl);
          bool flag5 = this.ItemPainter.ShowHasChildItems((QCommand) command, configuration, destinationControl);
          bool flag6 = this.ItemPainter.ShowTitle((QCommand) command, configuration, destinationControl);
          bool flag7 = false;
          if (itemConfiguration1 != null)
            flag7 = itemConfiguration1.ItemOrientation == QCommandOrientation.Vertical;
          else if (itemConfiguration2 != null)
            flag7 = itemConfiguration2.ItemOrientation == QCommandOrientation.Vertical;
          if (command.IsVisible)
          {
            if (command.IsSeparator)
            {
              if (num3 > 0 || num4 > 0)
                command.Offset(0, num3 + num4);
            }
            else
            {
              if (flag4)
              {
                QExplorerItem qexplorerItem = command;
                int end = qrange.End;
                rectangle1 = command.ShortcutBounds;
                int width14 = rectangle1.Width;
                int x = end - width14;
                rectangle1 = command.ShortcutBounds;
                int y = rectangle1.Y;
                rectangle1 = command.ShortcutBounds;
                int width15 = rectangle1.Width;
                rectangle1 = command.ShortcutBounds;
                int height = rectangle1.Height;
                Rectangle rectangle10 = new Rectangle(x, y, width15, height);
                qexplorerItem.PutShortcutBounds(rectangle10);
              }
              int num31 = 0;
              Size preferredSize;
              if (flag3)
              {
                if (flag4)
                {
                  rectangle1 = command.ShortcutBounds;
                  int x = rectangle1.X;
                  qspacing = configuration3.ShortcutSpacing;
                  int before = qspacing.Before;
                  int num32 = x - before;
                  qspacing = configuration3.ControlSpacing;
                  int after = qspacing.After;
                  int num33 = num32 - after;
                  int num34;
                  if (!command.ControlStretched)
                  {
                    preferredSize = command.Control.PreferredSize;
                    num34 = preferredSize.Width;
                  }
                  else
                    num34 = 0;
                  num31 = num33 - num34;
                }
                else
                {
                  rectangle1 = command.HasChildItemsBounds;
                  int x = rectangle1.X;
                  qspacing = configuration3.HasChildItemsSpacing;
                  int before = qspacing.Before;
                  int num35 = x - before;
                  qspacing = configuration3.ControlSpacing;
                  int after = qspacing.After;
                  int num36 = num35 - after;
                  int num37;
                  if (!command.ControlStretched)
                  {
                    preferredSize = command.Control.PreferredSize;
                    num37 = preferredSize.Width;
                  }
                  else
                    num37 = 0;
                  num31 = num36 - num37;
                }
              }
              if (!flag7)
              {
                int num38;
                if (flag3 && !command.ControlStretched)
                {
                  int num39 = num31;
                  qspacing = configuration3.ControlSpacing;
                  int before = qspacing.Before;
                  int num40 = num39 - before;
                  rectangle1 = command.TextBounds;
                  int x = rectangle1.X;
                  num38 = num40 - x;
                }
                else if (flag4)
                {
                  rectangle1 = command.ShortcutBounds;
                  int x10 = rectangle1.X;
                  qspacing = configuration3.ShortcutSpacing;
                  int before = qspacing.Before;
                  int num41 = x10 - before;
                  rectangle1 = command.TextBounds;
                  int x11 = rectangle1.X;
                  num38 = num41 - x11;
                }
                else
                {
                  rectangle1 = command.HasChildItemsBounds;
                  int x12 = rectangle1.X;
                  qspacing = configuration3.HasChildItemsSpacing;
                  int before = qspacing.Before;
                  int num42 = x12 - before;
                  rectangle1 = command.TextBounds;
                  int x13 = rectangle1.X;
                  num38 = num42 - x13;
                }
                QExplorerItem qexplorerItem6 = command;
                Rectangle textBounds = command.TextBounds;
                rectangle1 = command.TextBounds;
                int width16 = rectangle1.Width;
                int num43 = num38;
                qspacing = configuration3.TitleSpacing;
                int after = qspacing.After;
                int val2 = num43 - after;
                int width17 = Math.Max(0, Math.Min(width16, val2));
                Rectangle rectangle11 = QMath.SetWidth(textBounds, width17);
                qexplorerItem6.PutTextBounds(rectangle11);
                if (flag3)
                {
                  rectangle1 = command.TextBounds;
                  int x = rectangle1.X;
                  rectangle1 = command.TextBounds;
                  int width18 = rectangle1.Width;
                  int num44 = x + width18;
                  int num45;
                  if (!flag6)
                  {
                    num45 = 0;
                  }
                  else
                  {
                    qspacing = configuration3.TitleSpacing;
                    num45 = qspacing.After;
                  }
                  int num46 = num44 + num45;
                  qspacing = configuration3.ControlSpacing;
                  int before = qspacing.Before;
                  int num47 = num46 + before;
                  if (command.ControlStretched)
                    command.PutControlBounds(QMath.SetWidth(command.ControlBounds, num31 - num47));
                  else if (num47 > num31)
                  {
                    QExplorerItem qexplorerItem7 = command;
                    Rectangle controlBounds = command.ControlBounds;
                    preferredSize = command.Control.PreferredSize;
                    int width19 = preferredSize.Width - (num47 - num31);
                    Rectangle rectangle12 = QMath.SetWidth(controlBounds, width19);
                    qexplorerItem7.PutControlBounds(rectangle12);
                  }
                  else
                  {
                    QExplorerItem qexplorerItem8 = command;
                    Rectangle controlBounds = command.ControlBounds;
                    preferredSize = command.Control.PreferredSize;
                    int width20 = preferredSize.Width;
                    Rectangle rectangle13 = QMath.SetWidth(controlBounds, width20);
                    qexplorerItem8.PutControlBounds(rectangle13);
                  }
                  command.PutControlBounds(QMath.SetX(command.ControlBounds, num47));
                }
              }
              else
              {
                QExplorerItem qexplorerItem = command;
                Rectangle textBounds = command.TextBounds;
                rectangle1 = command.ContentsBounds;
                int width21 = rectangle1.Width;
                qpadding = configuration3.ItemPadding;
                int horizontal = qpadding.Horizontal;
                int num48 = width21 - horizontal;
                int num49;
                if (!flag5)
                {
                  num49 = 0;
                }
                else
                {
                  rectangle1 = command.HasChildItemsBounds;
                  int width22 = rectangle1.Width;
                  qspacing = configuration3.HasChildItemsSpacing;
                  int all = qspacing.All;
                  num49 = width22 + all;
                }
                int width23 = num48 - num49;
                Rectangle rectangle14 = QMath.SetWidth(textBounds, width23);
                qexplorerItem.PutTextBounds(rectangle14);
              }
              if (num3 > 0 || num4 > 0)
                command.Offset(0, num3 + num4);
              if (itemConfiguration1 != null && itemConfiguration1.ItemWrapText || itemConfiguration2 != null && itemConfiguration2.ItemWrapText)
              {
                QExplorerBarItemPainter itemPainter = this.ItemPainter;
                string title = command.Title;
                StringFormat format = command.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemStringFormat : qexplorerBarPaintParams.ItemStringFormat;
                Control destinationControl1 = destinationControl;
                QCommandConfiguration configuration4 = configuration3;
                Graphics graphics2 = graphics1;
                rectangle1 = command.TextBounds;
                int width24 = rectangle1.Width;
                Size size = itemPainter.MeasureText(title, format, destinationControl1, configuration4, graphics2, width24);
                int height14;
                int y;
                if (flag7)
                {
                  qpadding = configuration3.ItemPadding;
                  int vertical = qpadding.Vertical;
                  qspacing = this.ItemPainter.UsedTitleSpacing((QCommand) command, configuration3, destinationControl);
                  int all1 = qspacing.All;
                  int num50 = vertical + all1 + size.Height;
                  qspacing = this.ItemPainter.UsedShortcutSpacing((QCommand) command, configuration3, destinationControl);
                  int all2 = qspacing.All;
                  int num51 = num50 + all2;
                  int num52;
                  if (!flag4)
                  {
                    num52 = 0;
                  }
                  else
                  {
                    rectangle1 = command.ShortcutBounds;
                    num52 = rectangle1.Height;
                  }
                  int num53 = num51 + num52;
                  qspacing = this.ItemPainter.UsedIconSpacing((QCommand) command, configuration3, destinationControl);
                  int all3 = qspacing.All;
                  int num54 = num53 + all3;
                  int num55;
                  if (!this.ItemPainter.ShowIcon((QCommand) command, configuration3, destinationControl))
                  {
                    num55 = 0;
                  }
                  else
                  {
                    rectangle1 = command.IconBounds;
                    num55 = rectangle1.Height;
                  }
                  int val1 = num54 + num55;
                  rectangle1 = command.ContentsBounds;
                  int val2 = Math.Max(rectangle1.Height, configuration3.MinimumItemHeight);
                  height14 = Math.Max(val1, val2);
                  int num56 = height14;
                  rectangle1 = command.ContentsBounds;
                  int height15 = rectangle1.Height;
                  y = num56 - height15;
                }
                else
                {
                  int height16 = size.Height;
                  qpadding = configuration3.ItemPadding;
                  int vertical = qpadding.Vertical;
                  int val1 = Math.Max(height16 + vertical, configuration3.MinimumItemHeight);
                  rectangle1 = command.ContentsBounds;
                  int height17 = rectangle1.Height;
                  height14 = Math.Max(val1, height17);
                  int num57 = height14;
                  rectangle1 = command.ContentsBounds;
                  int height18 = rectangle1.Height;
                  y = num57 - height18;
                }
                command.PutTextBounds(QMath.SetHeight(command.TextBounds, size.Height));
                if (height14 > 0)
                {
                  num4 += y;
                  command.PutContentsBounds(QMath.SetHeight(command.ContentsBounds, height14));
                  QExplorerItem qexplorerItem9 = command;
                  Rectangle panelBounds = command.PanelBounds;
                  rectangle1 = command.PanelBounds;
                  int num58 = rectangle1.Y + y;
                  Rectangle rectangle15 = QMath.SetY(panelBounds, num58);
                  qexplorerItem9.PutPanelBounds(rectangle15);
                  QExplorerItem qexplorerItem10 = command;
                  Rectangle childItemsHotBounds = command.HasChildItemsHotBounds;
                  rectangle1 = command.HasChildItemsHotBounds;
                  int num59 = rectangle1.Y + y;
                  Rectangle rectangle16 = QMath.SetY(childItemsHotBounds, num59);
                  qexplorerItem10.PutHasChildItemsHotBounds(rectangle16);
                  command.Offset(0, y, true);
                  if (flag7)
                  {
                    QExplorerItem qexplorerItem11 = command;
                    Rectangle shortcutBounds = command.ShortcutBounds;
                    rectangle1 = command.ShortcutBounds;
                    int num60 = rectangle1.Y + y;
                    Rectangle rectangle17 = QMath.SetY(shortcutBounds, num60);
                    qexplorerItem11.PutShortcutBounds(rectangle17);
                  }
                }
              }
              this.ItemPainter.AlignContentsElements((QCommand) command, StringAlignment.Center);
              if (command.ItemType == QExplorerItemType.GroupItem && qexplorerBarPaintParams.GroupItemConfiguration.ItemStretched && num2 > 0 && command.Expanded)
              {
                QExplorerItem qexplorerItem = command;
                Rectangle panelBounds = command.PanelBounds;
                rectangle1 = command.PanelBounds;
                int height = rectangle1.Height + num19 / num2;
                Rectangle rectangle18 = QMath.SetHeight(panelBounds, height);
                qexplorerItem.PutPanelBounds(rectangle18);
                num3 += num19 / num2;
              }
              if (QExplorerBarPainter.RenderChildren(command))
              {
                QExplorerItem qexplorerItem = command;
                rectangle1 = command.Bounds;
                int x = rectangle1.X;
                rectangle1 = command.Bounds;
                int y4 = rectangle1.Y;
                rectangle1 = command.Bounds;
                int width25 = rectangle1.Width;
                rectangle1 = command.PanelBounds;
                int y5 = rectangle1.Y;
                rectangle1 = command.Bounds;
                int y6 = rectangle1.Y;
                int num61 = y5 - y6;
                rectangle1 = command.PanelBounds;
                int height19 = rectangle1.Height;
                int num62 = num61 + height19;
                qmargin = qexplorerBarPaintParams.GroupItemConfiguration.PanelMargin;
                int bottom = qmargin.Bottom;
                int height20 = num62 + bottom;
                Rectangle rectangle19 = new Rectangle(x, y4, width25, height20);
                qexplorerItem.PutGroupBounds(rectangle19);
              }
              else
                command.PutGroupBounds(command.Bounds);
              if (qexplorerBar != null && command.ItemType == QExplorerItemType.GroupItem && this.ItemPainter.ShowHasChildItems((QCommand) command, configuration3, destinationControl))
                command.PutHasChildItemsHotBounds(command.ContentsBounds);
            }
          }
          if (command.ShowDepersonalizeItem)
          {
            int height21 = qexplorerBarPaintParams.GroupItemConfiguration.UsedDepersonalizeMenuMask.Height;
            qpadding = qexplorerBarPaintParams.ItemConfiguration.ItemPadding;
            int vertical = qpadding.Vertical;
            int num63 = height21 + vertical;
            QExplorerItem qexplorerItem = command;
            rectangle1 = command.PanelBounds;
            int x14 = rectangle1.X;
            qpadding = qexplorerBarPaintParams.GroupItemConfiguration.PanelPadding;
            int left6 = qpadding.Left;
            int num64 = x14 + left6;
            qmargin = qexplorerBarPaintParams.ItemConfiguration.ItemMargin;
            int left7 = qmargin.Left;
            int x15 = num64 + left7;
            rectangle1 = command.PanelBounds;
            int y7 = rectangle1.Y;
            rectangle1 = command.PanelBounds;
            int height22 = rectangle1.Height;
            int num65 = y7 + height22;
            int num66 = num63;
            qpadding = qexplorerBarPaintParams.GroupItemConfiguration.PanelPadding;
            int bottom1 = qpadding.Bottom;
            int num67 = num66 + bottom1;
            qmargin = qexplorerBarPaintParams.ItemConfiguration.ItemMargin;
            int bottom2 = qmargin.Bottom;
            int num68 = num67 + bottom2;
            int y8 = num65 - num68;
            rectangle1 = command.PanelBounds;
            int width26 = rectangle1.Width;
            qpadding = qexplorerBarPaintParams.GroupItemConfiguration.PanelPadding;
            int horizontal4 = qpadding.Horizontal;
            qmargin = qexplorerBarPaintParams.ItemConfiguration.ItemMargin;
            int horizontal5 = qmargin.Horizontal;
            int num69 = horizontal4 + horizontal5 + 1;
            int width27 = width26 - num69;
            int height23 = num63;
            Rectangle rectangle20 = new Rectangle(x15, y8, width27, height23);
            qexplorerItem.PutDepersonalizeItemBounds(rectangle20);
          }
          else
            command.PutDepersonalizeItemBounds(Rectangle.Empty);
          command.SetControlParent();
          if ((!command.InMotion || qexplorerBarPaintParams.GroupItemConfiguration.UsedAnimationType == QExplorerItemAnimationType.None) && !this.SuppressControlMovement)
            command.CalculateControlBoundsProperties(false);
        }
      }
      for (int index = 0; index < commands.Count; ++index)
      {
        if (commands.GetCommand(index) is QExplorerItem)
          ((QExplorerItem) commands.GetCommand(index)).ApplyPositionOffset();
      }
      int num70 = num5;
      qpadding = qexplorerBarPaintParams.Configuration.ExplorerBarPadding;
      int bottom3 = qpadding.Bottom;
      this.m_oScrollSize = new Size(0, num70 + bottom3);
      graphics1.Dispose();
    }

    public override void DrawItem(
      QCommand command,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QCommandContainer parentContainer,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
      this.DrawItem(command, configuration, paintParams, parentContainer, flags, graphics, false, false, (Bitmap) null);
    }

    internal void DrawItem(
      QCommand command,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QCommandContainer parentContainer,
      QCommandPaintOptions flags,
      Graphics graphics,
      bool forceDrawChildren,
      bool forceDrawControl,
      Bitmap paintingBitmap)
    {
      if (!(command is QExplorerItem))
        return;
      QExplorerItem command1 = (QExplorerItem) QMisc.AssertObjectOfType((object) command, nameof (command), typeof (QExplorerItem));
      Rectangle rectangle1 = command.Bounds;
      if (rectangle1.Width <= 0 || command.Bounds.Height <= 0)
        return;
      QExplorerBarPaintParams qexplorerBarPaintParams = (QExplorerBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QExplorerBarPaintParams));
      QCommandConfiguration configuration1 = command1.ItemType == QExplorerItemType.GroupItem ? (QCommandConfiguration) qexplorerBarPaintParams.GroupItemConfiguration : (QCommandConfiguration) qexplorerBarPaintParams.ItemConfiguration;
      qexplorerBarPaintParams.UpdateColors(command1);
      QExplorerBar qexplorerBar = parentContainer as QExplorerBar;
      bool flag1 = (flags & QCommandPaintOptions.Hot) == QCommandPaintOptions.Hot;
      bool flag2 = (flags & QCommandPaintOptions.Expanded) == QCommandPaintOptions.Expanded;
      bool flag3 = (flags & QCommandPaintOptions.Pressed) == QCommandPaintOptions.Pressed;
      if (command1.InMotion)
        flag2 = true;
      command1.Flags = flags;
      if (!command1.IsSeparator)
      {
        Font font1;
        if (command1.ItemType == QExplorerItemType.GroupItem)
        {
          if (!command1.IsEnabled)
          {
            Font font2 = qexplorerBarPaintParams.GroupItemConfiguration.Font;
          }
          font1 = !flag3 ? (!flag1 ? (!flag2 ? qexplorerBarPaintParams.GroupItemConfiguration.Font : qexplorerBarPaintParams.GroupItemConfiguration.FontExpanded) : qexplorerBarPaintParams.GroupItemConfiguration.FontHot) : qexplorerBarPaintParams.GroupItemConfiguration.FontPressed;
        }
        else
          font1 = command1.IsEnabled ? (!flag3 ? (!flag1 ? (!flag2 ? qexplorerBarPaintParams.ItemConfiguration.Font : qexplorerBarPaintParams.ItemConfiguration.FontExpanded) : qexplorerBarPaintParams.ItemConfiguration.FontHot) : qexplorerBarPaintParams.ItemConfiguration.FontPressed) : qexplorerBarPaintParams.ItemConfiguration.Font;
        Color color = command1.IsEnabled ? (!flag3 ? (!flag1 ? (!flag2 ? qexplorerBarPaintParams.TextColor : qexplorerBarPaintParams.TextExpandedColor) : qexplorerBarPaintParams.TextHotColor) : qexplorerBarPaintParams.TextPressedColor) : qexplorerBarPaintParams.TextDisabledColor;
        QCommandAppearance appearance1 = command1.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemConfiguration.ActivatedItemAppearance : qexplorerBarPaintParams.ItemConfiguration.ActivatedItemAppearance;
        QCommandAppearance appearance2 = command1.ItemType == QExplorerItemType.GroupItem ? (QCommandAppearance) qexplorerBarPaintParams.GroupItemConfiguration.ItemAppearance : qexplorerBarPaintParams.ItemConfiguration.ActivatedItemAppearance;
        QDrawRoundedRectangleOptions rectangleOptions1 = QDrawRoundedRectangleOptions.None;
        if (qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerTopLeft)
          rectangleOptions1 |= QDrawRoundedRectangleOptions.TopLeft;
        if (qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerTopRight)
          rectangleOptions1 |= QDrawRoundedRectangleOptions.TopRight;
        if (qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerBottomLeft)
          rectangleOptions1 |= QDrawRoundedRectangleOptions.BottomLeft;
        if (qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerBottomRight)
          rectangleOptions1 |= QDrawRoundedRectangleOptions.BottomRight;
        QDrawRoundedRectangleOptions rectangleOptions2 = QDrawRoundedRectangleOptions.None;
        if (qexplorerBarPaintParams.GroupItemConfiguration.PanelRoundedCornerTopLeft)
          rectangleOptions2 |= QDrawRoundedRectangleOptions.TopLeft;
        if (qexplorerBarPaintParams.GroupItemConfiguration.PanelRoundedCornerTopRight)
          rectangleOptions2 |= QDrawRoundedRectangleOptions.TopRight;
        if (qexplorerBarPaintParams.GroupItemConfiguration.PanelRoundedCornerBottomLeft)
          rectangleOptions2 |= QDrawRoundedRectangleOptions.BottomLeft;
        if (qexplorerBarPaintParams.GroupItemConfiguration.PanelRoundedCornerBottomRight)
          rectangleOptions2 |= QDrawRoundedRectangleOptions.BottomRight;
        if (flag2 && command1.IsEnabled)
        {
          if (command1.ItemType == QExplorerItemType.GroupItem)
          {
            QColorSet colors = !flag3 || !flag1 ? (flag3 || flag1 ? new QColorSet(qexplorerBarPaintParams.HotGroupItemBackground1Color, qexplorerBarPaintParams.HotGroupItemBackground2Color, qexplorerBarPaintParams.HotGroupItemBorderColor) : new QColorSet(qexplorerBarPaintParams.ExpandedGroupItemBackground1Color, qexplorerBarPaintParams.ExpandedGroupItemBackground2Color, qexplorerBarPaintParams.ExpandedGroupItemBorderColor)) : new QColorSet(qexplorerBarPaintParams.PressedGroupItemBackground1Color, qexplorerBarPaintParams.PressedGroupItemBackground2Color, qexplorerBarPaintParams.PressedGroupItemBorderColor);
            if (qexplorerBarPaintParams.GroupItemConfiguration.ShadeVisible)
              QControlPaint.DrawRoundedShade(command1.GetGroupBoundsForPaint(forceDrawChildren), qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize, qexplorerBarPaintParams.GroupItemConfiguration.ShadeGradientSize, qexplorerBarPaintParams.GroupItemShade, rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.X, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.Y, graphics);
            QRoundedRectanglePainterProperties properties = new QRoundedRectanglePainterProperties(rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize);
            QRoundedRectanglePainter.Default.Paint(command1.GetGroupBoundsForPaint(forceDrawChildren), (IQAppearance) appearance1, colors, properties, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          else if (qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType != QBackgroundBoundsType.Icon || this.ItemPainter.ShowIcon((QCommand) command1, configuration1, (Control) parentContainer))
            QRectanglePainter.Default.Paint(qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType == QBackgroundBoundsType.Icon ? command1.ContentsRectangleToParent(command1.GetIconBackgroundBounds(configuration1)) : command1.Bounds, (IQAppearance) appearance1, new QColorSet(qexplorerBarPaintParams.ExpandedItemBackground1Color, qexplorerBarPaintParams.ExpandedItemBackground2Color, qexplorerBarPaintParams.ExpandedItemBorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        else if (flag3 && command1.IsEnabled)
        {
          if (command1.ItemType == QExplorerItemType.GroupItem)
          {
            if (qexplorerBarPaintParams.GroupItemConfiguration.ShadeVisible)
              QControlPaint.DrawRoundedShade(command1.GetGroupBoundsForPaint(forceDrawChildren), qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize, qexplorerBarPaintParams.GroupItemConfiguration.ShadeGradientSize, qexplorerBarPaintParams.GroupItemShade, rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.X, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.Y, graphics);
            QRoundedRectanglePainterProperties properties = new QRoundedRectanglePainterProperties(rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize);
            QRoundedRectanglePainter.Default.Paint(command1.GetGroupBoundsForPaint(forceDrawChildren), (IQAppearance) appearance1, new QColorSet(qexplorerBarPaintParams.PressedGroupItemBackground1Color, qexplorerBarPaintParams.PressedGroupItemBackground2Color, qexplorerBarPaintParams.PressedGroupItemBorderColor), properties, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          else
          {
            QColorSet colors = (QColorSet) null;
            if (command1.MouseIsOverMenuItemHasChildItemsHotBounds && !qexplorerBarPaintParams.ItemConfiguration.ExpandOnItemClick)
            {
              if (qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType != QBackgroundBoundsType.Icon || this.ItemPainter.ShowIcon((QCommand) command1, configuration1, (Control) parentContainer))
                colors = new QColorSet(qexplorerBarPaintParams.HotItemBackground1Color, qexplorerBarPaintParams.HotItemBackground2Color, qexplorerBarPaintParams.HotItemBorderColor);
            }
            else if (qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType != QBackgroundBoundsType.Icon || this.ItemPainter.ShowIcon((QCommand) command1, configuration1, (Control) parentContainer))
              colors = new QColorSet(qexplorerBarPaintParams.PressedItemBackground1Color, qexplorerBarPaintParams.PressedItemBackground2Color, qexplorerBarPaintParams.PressedItemBorderColor);
            if (colors != null)
              QRectanglePainter.Default.Paint(qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType == QBackgroundBoundsType.Icon ? command1.ContentsRectangleToParent(command1.GetIconBackgroundBounds(configuration1)) : command1.Bounds, (IQAppearance) appearance1, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
        }
        else if (flag1 && command1.IsEnabled)
        {
          if (command1.ItemType == QExplorerItemType.GroupItem)
          {
            if (qexplorerBarPaintParams.GroupItemConfiguration.ShadeVisible)
              QControlPaint.DrawRoundedShade(command1.GetGroupBoundsForPaint(forceDrawChildren), qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize, qexplorerBarPaintParams.GroupItemConfiguration.ShadeGradientSize, qexplorerBarPaintParams.GroupItemShade, rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.X, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.Y, graphics);
            QRoundedRectanglePainterProperties properties = new QRoundedRectanglePainterProperties(rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize);
            QRoundedRectanglePainter.Default.Paint(command1.GetGroupBoundsForPaint(forceDrawChildren), (IQAppearance) appearance1, new QColorSet(qexplorerBarPaintParams.HotGroupItemBackground1Color, qexplorerBarPaintParams.HotGroupItemBackground2Color, qexplorerBarPaintParams.HotGroupItemBorderColor), properties, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          else if (qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType != QBackgroundBoundsType.Icon || this.ItemPainter.ShowIcon((QCommand) command1, configuration1, (Control) parentContainer))
            QRectanglePainter.Default.Paint(qexplorerBarPaintParams.ItemConfiguration.BackgroundBoundsType == QBackgroundBoundsType.Icon ? command1.ContentsRectangleToParent(command1.GetIconBackgroundBounds(configuration1)) : command1.Bounds, (IQAppearance) appearance1, new QColorSet(qexplorerBarPaintParams.HotItemBackground1Color, qexplorerBarPaintParams.HotItemBackground2Color, qexplorerBarPaintParams.HotItemBorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        else if (command1.ItemType == QExplorerItemType.GroupItem)
        {
          if (qexplorerBarPaintParams.GroupItemConfiguration.ShadeVisible)
            QControlPaint.DrawRoundedShade(command1.GetGroupBoundsForPaint(forceDrawChildren), qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize, qexplorerBarPaintParams.GroupItemConfiguration.ShadeGradientSize, qexplorerBarPaintParams.GroupItemShade, rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.X, qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.Y, graphics);
          QRoundedRectanglePainterProperties properties = new QRoundedRectanglePainterProperties(rectangleOptions1, qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize);
          QRoundedRectanglePainter.Default.Paint(command1.GetGroupBoundsForPaint(forceDrawChildren), (IQAppearance) appearance2, new QColorSet(qexplorerBarPaintParams.GroupItemBackground1Color, qexplorerBarPaintParams.GroupItemBackground2Color, qexplorerBarPaintParams.GroupItemBorderColor), properties, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        if (command1.ItemType == QExplorerItemType.GroupItem && (!command1.InMotion || forceDrawChildren) && command1.PanelBounds.Width > 0 && command1.PanelBounds.Height > 0)
        {
          QColorSet colors = new QColorSet(qexplorerBarPaintParams.GroupPanelBackground1Color, qexplorerBarPaintParams.GroupPanelBackground2Color, qexplorerBarPaintParams.GroupPanelBorderColor);
          QRoundedRectanglePainterProperties properties = new QRoundedRectanglePainterProperties(rectangleOptions2, qexplorerBarPaintParams.GroupItemConfiguration.PanelRoundedCornerSize);
          QRoundedRectanglePainter.Default.FillBackground(command1.PanelBounds, (IQAppearance) qexplorerBarPaintParams.GroupItemConfiguration.PanelAppearance, colors, properties, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          if (command1.GroupPanelBackgroundImage != null)
          {
            Region clip = graphics.Clip;
            GraphicsPath roundedRectanglePath = QRoundedRectanglePainter.Default.GetRoundedRectanglePath(command1.PanelBounds, qexplorerBarPaintParams.GroupItemConfiguration.PanelRoundedCornerSize, QAppearanceForegroundOptions.DrawAllBorders, rectangleOptions2);
            graphics.Clip = new Region(roundedRectanglePath);
            QControlPaint.DrawImage(command1.GroupPanelBackgroundImage, command1.GroupPanelBackgroundImageAlign, command1.PanelBounds, command1.GroupPanelBackgroundImage.Size, graphics, new ImageAttributes(), false);
            graphics.Clip = clip;
          }
          QRoundedRectanglePainter.Default.FillForeground(command1.PanelBounds, (IQAppearance) qexplorerBarPaintParams.GroupItemConfiguration.PanelAppearance, colors, properties, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        if (command1.InMotion && command1.ExpandedBitmap != null && command1.ExpandedBitmap != paintingBitmap)
        {
          Region clip = graphics.Clip;
          Region region = clip.Clone();
          region.Intersect(new Region(command1.GetExpandedBitmapBoundsForPaint(qexplorerBarPaintParams.GroupItemConfiguration, command1.ExpandedBitmap.Width)));
          graphics.Clip = region;
          graphics.ExcludeClip(new Region(QRoundedRectanglePainter.Default.GetRoundedRectanglePath(command1.Bounds, qexplorerBarPaintParams.GroupItemConfiguration.ItemRoundedCornerSize, QAppearanceForegroundOptions.DrawAllBorders, rectangleOptions1)));
          ImageAttributes imageAttr = new ImageAttributes();
          if (qexplorerBarPaintParams.GroupItemConfiguration.UsedAnimationType == QExplorerItemAnimationType.Fade || qexplorerBarPaintParams.GroupItemConfiguration.UsedAnimationType == QExplorerItemAnimationType.Both)
            imageAttr.SetColorMatrix(new ColorMatrix()
            {
              Matrix33 = command1.ExpandedBitmapOpacityForPaint
            });
          bool flag4 = qexplorerBarPaintParams.GroupItemConfiguration.UsedAnimationType == QExplorerItemAnimationType.Both || qexplorerBarPaintParams.GroupItemConfiguration.UsedAnimationType == QExplorerItemAnimationType.Slide;
          if (command1.ItemState == QExplorerItemState.Expanding)
            graphics.DrawImage((Image) command1.ExpandedBitmap, new Rectangle(0, command1.GroupBounds.Y - Math.Abs(qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.X) - (flag4 ? command1.MotionHeight - command1.MotionOffset : 0) - 1, command1.ExpandedBitmap.Width, command1.ExpandedBitmap.Height), 0, 0, command1.ExpandedBitmap.Width, command1.ExpandedBitmap.Height, GraphicsUnit.Pixel, imageAttr);
          else
            graphics.DrawImage((Image) command1.ExpandedBitmap, new Rectangle(0, command1.GroupBounds.Y - Math.Abs(qexplorerBarPaintParams.GroupItemConfiguration.ShadePosition.X) - (flag4 ? command1.MotionOffset : 0) - 1, command1.ExpandedBitmap.Width, command1.ExpandedBitmap.Height), 0, 0, command1.ExpandedBitmap.Width, command1.ExpandedBitmap.Height, GraphicsUnit.Pixel, imageAttr);
          graphics.Clip = clip;
        }
        if (this.ItemPainter.ShowIcon((QCommand) command1, configuration1, (Control) parentContainer) && QExplorerBarPainter.IconFits(command1))
        {
          if (command1.Checked)
          {
            int num = 2;
            Rectangle parent = command1.ContentsRectangleToParent(command1.IconBoundsForPaint);
            parent.Inflate(num, num);
            QAppearanceWrapper appearance3 = new QAppearanceWrapper((IQAppearance) null);
            appearance3.BackgroundStyle = QColorStyle.Gradient;
            appearance3.GradientAngle = appearance1.GradientAngle;
            QColorSet colors = command1.ItemType != QExplorerItemType.GroupItem ? new QColorSet(qexplorerBarPaintParams.CheckedItemBackground1Color, qexplorerBarPaintParams.CheckedItemBackground2Color, qexplorerBarPaintParams.CheckedItemBorderColor) : new QColorSet(qexplorerBarPaintParams.CheckedGroupItemBackground1Color, qexplorerBarPaintParams.CheckedGroupItemBackground2Color, qexplorerBarPaintParams.CheckedGroupItemBorderColor);
            QRectanglePainter.Default.Paint(parent, (IQAppearance) appearance3, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          if (command1.UsedIcon != null)
          {
            if (command1.IsEnabled || !command1.DisabledIconGrayScaled)
            {
              Color replaceColorWith = color;
              QControlPaint.DrawIcon(graphics, command1.UsedIcon, command1.UsedIconReplaceColor, replaceColorWith, command1.ContentsRectangleToParent(command1.IconBoundsForPaint));
            }
            else
              QControlPaint.DrawIconDisabled(graphics, command1.UsedIcon, command1.ContentsRectangleToParent(command1.IconBoundsForPaint));
          }
        }
        Brush brush = !command1.IsEnabled || !this.ItemPainter.ShowTitle((QCommand) command1, configuration1, (Control) parentContainer) && !this.ItemPainter.ShowShortcut((QCommand) command1, configuration1, (Control) parentContainer) ? (Brush) new SolidBrush(qexplorerBarPaintParams.TextDisabledColor) : (Brush) new SolidBrush(color);
        if (this.ItemPainter.ShowTitle((QCommand) command1, configuration1, (Control) parentContainer) && command1.TextBounds.Width > 0)
        {
          rectangle1 = command1.TextBounds;
          if (rectangle1.Height > 0)
            graphics.DrawString(command1.Title, font1, brush, (RectangleF) command1.ContentsRectangleToParent(command1.TextBounds), command1.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemStringFormat : qexplorerBarPaintParams.ItemStringFormat);
        }
        if (this.ItemPainter.ShowShortcut((QCommand) command1, configuration1, (Control) parentContainer) && QExplorerBarPainter.ShortcutFits(command1))
          graphics.DrawString(command1.ShortcutString, font1, brush, (RectangleF) command1.ContentsRectangleToParent(command1.ShortcutBounds), command1.ItemType == QExplorerItemType.GroupItem ? qexplorerBarPaintParams.GroupItemStringFormat : qexplorerBarPaintParams.ItemStringFormat);
        brush?.Dispose();
        if (this.ItemPainter.ShowHasChildItems((QCommand) command1, configuration1, (Control) parentContainer) && QExplorerBarPainter.HasChildItemsFits(command1))
        {
          if (command1.IsEnabled && command1.ItemType != QExplorerItemType.GroupItem && qexplorerBar != null && !qexplorerBarPaintParams.ItemConfiguration.ExpandOnItemClick)
          {
            QColorSet colors = (QColorSet) null;
            if (flag3 && flag1 && !flag2 && command1.MouseIsOverMenuItemHasChildItemsHotBounds)
              colors = new QColorSet(qexplorerBarPaintParams.PressedItemBackground1Color, qexplorerBarPaintParams.PressedItemBackground2Color, qexplorerBarPaintParams.PressedItemBorderColor);
            else if (flag1 && !flag2)
              colors = new QColorSet(qexplorerBarPaintParams.HotItemBackground1Color, qexplorerBarPaintParams.HotItemBackground2Color, qexplorerBarPaintParams.HotItemBorderColor);
            if (colors != null)
              QRectanglePainter.Default.Paint(command1.HasChildItemsHotBounds, (IQAppearance) appearance1, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          Image image = command1.ItemType != QExplorerItemType.GroupItem ? QControlPaint.CreateColorizedImage(command1.UsedConfiguration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), color) : QControlPaint.CreateColorizedImage(command1.Expanded ? command1.UsedConfiguration.UsedHasChildItemsMaskReverse : command1.UsedConfiguration.UsedHasChildItemsMask, qexplorerBarPaintParams.HasMoreChildItemsColor, Color.FromArgb((int) byte.MaxValue, 0, 0), color);
          graphics.DrawImage(image, command1.ContentsRectangleToParent(command1.HasChildItemsBounds));
        }
      }
      else if (configuration1.SeparatorMask != null)
      {
        Image image = configuration1.SeparatorMask;
        if (command1.Orientation == QCommandOrientation.Vertical)
          image = (Image) QControlPaint.RotateFlipImage(image, RotateFlipType.Rotate90FlipNone);
        QControlPaint.DrawImage(image, Color.FromArgb((int) byte.MaxValue, 0, 0), qexplorerBarPaintParams.SeparatorColor, QImageAlign.RepeatedHorizontal, command1.ContentsRectangleToParent(command1.SeparatorBounds), configuration1.SeparatorMask.Size, graphics);
        if (image != configuration1.SeparatorMask)
          image.Dispose();
      }
      else if (configuration1.SeparatorMask != null)
      {
        QControlPaint.DrawImage(configuration1.SeparatorMask, Color.FromArgb((int) byte.MaxValue, 0, 0), qexplorerBarPaintParams.SeparatorColor, QImageAlign.RepeatedHorizontal, command1.ContentsRectangleToParent(command1.SeparatorBounds), configuration1.SeparatorMask.Size, graphics);
      }
      else
      {
        Brush brush = (Brush) new SolidBrush(qexplorerBarPaintParams.SeparatorColor);
        graphics.FillRectangle(brush, command1.ContentsRectangleToParent(command1.SeparatorBounds));
        brush.Dispose();
      }
      if (command1.Expanded && command1.ItemType == QExplorerItemType.GroupItem && (!command1.InMotion || forceDrawChildren))
      {
        for (int index = 0; index < command1.MenuItems.Count; ++index)
        {
          if (command1.MenuItems[index].IsVisible)
          {
            QCommandPaintOptions flags1 = QCommandPaintOptions.None;
            if (command1.MenuItems[index] == qexplorerBar.ExpandedItem)
              flags1 |= QCommandPaintOptions.Expanded;
            if (command1.MenuItems[index] == qexplorerBar.MouseDownItem && !command1.MenuItems[index].IsInformationOnly)
              flags1 |= QCommandPaintOptions.Pressed;
            if (command1.MenuItems[index] == qexplorerBar.HotItem && !command1.MenuItems[index].IsInformationOnly)
              flags1 |= QCommandPaintOptions.Hot;
            this.DrawItem((QCommand) command1.MenuItems[index], configuration, paintParams, parentContainer, flags1, graphics, false, forceDrawControl, paintingBitmap);
          }
        }
      }
      if (command1.ShowDepersonalizeItem && (!command1.InMotion || forceDrawChildren))
      {
        QButtonState qbuttonState = QButtonState.Normal;
        if (qexplorerBar != null && qexplorerBar.DepersonalizeMenuItemBounds == command1.DepersonalizeItemBounds)
          qbuttonState = qexplorerBar.DepersonalizeMenuItemState;
        QColorSet colors = (QColorSet) null;
        switch (qbuttonState)
        {
          case QButtonState.Hot:
            colors = new QColorSet(qexplorerBarPaintParams.HotItemBackground1Color, qexplorerBarPaintParams.HotItemBackground2Color, qexplorerBarPaintParams.HotItemBorderColor);
            break;
          case QButtonState.Pressed:
            colors = new QColorSet(qexplorerBarPaintParams.PressedItemBackground1Color, qexplorerBarPaintParams.PressedItemBackground2Color, qexplorerBarPaintParams.PressedItemBorderColor);
            break;
        }
        if (colors != null)
          QRectanglePainter.Default.Paint(command1.DepersonalizeItemBounds, (IQAppearance) qexplorerBarPaintParams.ItemConfiguration.ActivatedItemAppearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        Rectangle rectangle2;
        ref Rectangle local = ref rectangle2;
        rectangle1 = command1.DepersonalizeItemBounds;
        int left1 = rectangle1.Left;
        QPadding itemPadding = qexplorerBarPaintParams.ItemConfiguration.ItemPadding;
        int left2 = itemPadding.Left;
        int x = left1 + left2;
        rectangle1 = command1.DepersonalizeItemBounds;
        int top1 = rectangle1.Top;
        itemPadding = qexplorerBarPaintParams.ItemConfiguration.ItemPadding;
        int top2 = itemPadding.Top;
        int y = top1 + top2;
        rectangle1 = command1.DepersonalizeItemBounds;
        int width1 = rectangle1.Width;
        itemPadding = qexplorerBarPaintParams.ItemConfiguration.ItemPadding;
        int horizontal = itemPadding.Horizontal;
        int width2 = width1 - horizontal;
        rectangle1 = command1.DepersonalizeItemBounds;
        int height1 = rectangle1.Height;
        itemPadding = qexplorerBarPaintParams.ItemConfiguration.ItemPadding;
        int vertical = itemPadding.Vertical;
        int height2 = height1 - vertical;
        local = new Rectangle(x, y, width2, height2);
        QControlPaint.DrawImage(QControlPaint.CreateColorizedImage(qexplorerBarPaintParams.GroupItemConfiguration.UsedDepersonalizeMenuMask, qexplorerBarPaintParams.DepersonalizeImageBackground, Color.FromArgb((int) byte.MaxValue, 0, 0), qexplorerBarPaintParams.DepersonalizeImageForeground), QImageAlign.Centered, rectangle2, graphics);
      }
      if (command1.ParentCommand != null && command1.ParentCommand is QExplorerItem && forceDrawControl && command1.Control != null)
      {
        command1.CalculateControlBoundsProperties(false);
        QCommandControlContainer control = command1.Control;
        rectangle1 = command1.ContentsRectangleToParent(command1.ControlBounds);
        Point location = rectangle1.Location;
        Graphics graphics1 = graphics;
        control.PaintOnGraphics(location, graphics1);
      }
      if (this.ItemPainter.ShowControl((QCommand) command1, configuration, (Control) parentContainer) && command1.Control.Bitmap != null && (command1.InMovement || !command1.Control.Visible) && !command1.Control.CreatingBitmap)
        graphics.DrawImageUnscaled((Image) command1.Control.Bitmap, command1.ContentsRectangleToParent(command1.ControlBounds));
      if (qexplorerBar == null || !qexplorerBarPaintParams.Configuration.FocusRectangleVisible || !qexplorerBar.Focused || qexplorerBar.FocusedMenuItem != command1)
        return;
      ControlPaint.DrawFocusRectangle(graphics, command1.ContentsBounds);
    }

    private static bool IconFits(QExplorerItem item) => item != null && item.ContentsBounds.Contains(item.ContentsRectangleToParent(item.IconBounds));

    private static bool ShortcutFits(QExplorerItem item) => item != null && item.ContentsBounds.Contains(item.ContentsRectangleToParent(item.ShortcutBounds));

    private static bool HasChildItemsFits(QExplorerItem item) => item != null && item.ContentsBounds.Contains(item.ContentsRectangleToParent(item.HasChildItemsBounds));

    private static bool RenderChildren(QExplorerItem item)
    {
      if (item == null || !item.Expanded || item.ItemType != QExplorerItemType.GroupItem)
        return false;
      for (int index = 0; index < item.MenuItems.Count; ++index)
      {
        if (item.MenuItems[index].IsVisible)
          return true;
      }
      return item.MenuItems.HasPersonalizedItems(false);
    }

    internal Size ScrollSize => this.m_oScrollSize;

    internal bool SuppressControlMovement
    {
      get => this.m_bSuppressControlMovement;
      set => this.m_bSuppressControlMovement = value;
    }
  }
}
