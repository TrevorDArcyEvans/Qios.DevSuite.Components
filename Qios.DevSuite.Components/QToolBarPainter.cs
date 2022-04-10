// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QToolBarPainter : QCommandContainerPainter
  {
    private Size m_oLastItemsSize;

    public QToolBarPainter() => this.CommandPainter = (QCommandPainter) new QToolBarItemPainter();

    public QToolBarItemPainter ItemPainter => (QToolBarItemPainter) this.CommandPainter;

    private static Size CalculateRightAlignedObjectsHorizontal(QToolBarPaintParams paintParams)
    {
      int width = 0;
      int num = 0;
      if (paintParams.MdiButtons != null)
      {
        Size requestedSize1 = new Size(paintParams.MdiButtonsSize.Width * paintParams.MdiButtons.VisibleButtonsCount, paintParams.MdiButtonsSize.Height);
        paintParams.MdiButtons.CalculateRequestedSize(requestedSize1);
        Size requestedSize2 = paintParams.MdiButtons.RequestedSize;
        paintParams.MdiButtonsBounds = new Rectangle(0, 0, requestedSize2.Width, requestedSize2.Height);
        width += paintParams.MdiButtonsPadding.Horizontal + requestedSize2.Width;
        num = Math.Max(num, paintParams.MdiButtonsPadding.Vertical + requestedSize2.Height);
      }
      else
        paintParams.MdiButtonsBounds = Rectangle.Empty;
      if (paintParams.ShowCustomizeBar)
      {
        width += paintParams.HasMoreItemsAreaWidth;
        paintParams.CustomizeBounds = new Rectangle(0, 0, paintParams.HasMoreItemsAreaWidth, 0);
      }
      else
        paintParams.CustomizeBounds = Rectangle.Empty;
      return new Size(width, num);
    }

    private static void LayoutRightAlignedObjectsHorizontal(
      Rectangle rectangle,
      int start,
      QToolBarPaintParams paintParams)
    {
      if (paintParams.MdiButtons != null)
      {
        paintParams.MdiButtonsBounds = new Rectangle(start + paintParams.MdiButtonsPadding.Left, rectangle.Top + paintParams.MdiButtonsPadding.Top, paintParams.MdiButtonsBounds.Width, paintParams.MdiButtonsBounds.Height);
        start += paintParams.MdiButtonsBounds.Width + paintParams.MdiButtonsPadding.Horizontal;
      }
      else
        paintParams.MdiButtonsBounds = Rectangle.Empty;
      if (paintParams.ShowCustomizeBar)
      {
        paintParams.CustomizeBounds = new Rectangle(start, rectangle.Top, paintParams.HasMoreItemsAreaWidth, rectangle.Height);
        start += paintParams.HasMoreItemsAreaWidth;
      }
      else
        paintParams.CustomizeBounds = Rectangle.Empty;
    }

    public override void LayoutHorizontal(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      base.LayoutHorizontal(rectangle, configuration, paintParams, destinationControl, commands);
      QToolBarPaintParams paintParams1 = (QToolBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QToolBarPaintParams));
      bool flag1 = (paintParams1.LayoutFlags & QToolBarLayoutFlags.FixedWidth) == QToolBarLayoutFlags.FixedWidth;
      bool flag2 = (paintParams1.LayoutFlags & QToolBarLayoutFlags.FixedHeight) == QToolBarLayoutFlags.FixedHeight;
      bool flag3 = (paintParams1.LayoutFlags & QToolBarLayoutFlags.MaximumWidth) == QToolBarLayoutFlags.MaximumWidth;
      int val2_1 = configuration.MinimumItemHeight;
      int val2_2 = 0;
      int num1 = 0;
      Size empty = Size.Empty;
      int num2 = 0;
      bool flag4 = false;
      int num3 = rectangle.Left;
      Graphics graphics = destinationControl.CreateGraphics();
      paintParams1.LastVisibleItem = -1;
      Rectangle rectangle1;
      if (paintParams1.ShowSizingGrip)
      {
        int x = num3 + paintParams1.SizingGripPadding.Left;
        paintParams1.SizingGripBounds = new Rectangle(x, 0, paintParams1.SizingGripWidth, 0);
        int num4 = x;
        rectangle1 = paintParams1.SizingGripBounds;
        int num5 = rectangle1.Width + paintParams1.SizingGripPadding.Right;
        num3 = num4 + num5;
      }
      else
        paintParams1.SizingGripBounds = Rectangle.Empty;
      if (paintParams1.ActiveMdiIcon != null)
      {
        int x = num3 + paintParams1.ActiveMdiIconPadding.Left;
        val2_1 = Math.Max(paintParams1.ActiveMdiIconPadding.Vertical + configuration.IconSize.Height, val2_1);
        paintParams1.ActiveMdiIconBounds = new Rectangle(x, rectangle.Top + paintParams1.ActiveMdiIconPadding.Top, configuration.IconSize.Width, configuration.IconSize.Height);
        num3 = x + (paintParams1.ActiveMdiIconBounds.Width + paintParams1.ActiveMdiIconPadding.Right);
      }
      else
        paintParams1.ActiveMdiIconBounds = Rectangle.Empty;
      int num6 = num3 + paintParams1.ItemsSpacing.Before;
      Size objectsHorizontal = QToolBarPainter.CalculateRightAlignedObjectsHorizontal(paintParams1);
      int num7 = Math.Max(objectsHorizontal.Height, val2_1);
      paintParams1.ItemsBounds = flag1 || flag3 ? Rectangle.FromLTRB(num6, rectangle.Top, rectangle.Right - (objectsHorizontal.Width + paintParams1.ItemsSpacing.After), rectangle.Bottom) : new Rectangle(num6, rectangle.Top, int.MaxValue, rectangle.Height);
      int num8 = paintParams1.ItemsBounds.Left;
      int top1 = rectangle.Top;
      int index1 = 0;
      int index2 = 0;
      object[] objArray = new object[commands.Count];
      for (int index3 = 0; index3 < commands.Count; ++index3)
      {
        QMenuItem command = (QMenuItem) commands.GetCommand(index3);
        command.EmptyCachedObjects();
        if (command.IsVisible)
        {
          command.Orientation = QCommandOrientation.Horizontal;
          if (command.IsSeparator)
          {
            command.Padding = new QPadding(0, configuration.ItemPadding.Top, configuration.ItemPadding.Bottom, 0);
            command.Margin = new QMargin(configuration.SeparatorSpacing.Before, configuration.ItemMargin.Top, configuration.ItemMargin.Bottom, configuration.SeparatorSpacing.After);
            int width = configuration.SeparatorMask == null ? (paintParams1.ShadeSeparator ? configuration.SeparatorSize + 1 : configuration.SeparatorSize) : configuration.CalculateSeparatorSize(QCommandOrientation.Vertical);
            command.PutSeparatorBounds(new Rectangle(0, 0, width, 0));
          }
          else
          {
            command.Padding = configuration.ItemPadding;
            command.Margin = new QMargin(configuration.ItemMargin.Left, configuration.ItemMargin.Top, configuration.ItemMargin.Bottom, configuration.ItemMargin.Right);
          }
          this.ItemPainter.CalculateItemContents((QCommand) command, configuration, 0, paintParams1.StringFormat, destinationControl, graphics);
          num7 = Math.Max(command.OuterBounds.Height, num7);
          val2_2 = Math.Max(command.ContentsBounds.Height, val2_2);
          if (!flag4)
          {
            num2 = command.OuterBounds.Width;
            flag4 = true;
          }
          num1 += command.OuterBounds.Width;
          if (index3 > 0 && (flag1 || flag3) && num8 + command.OuterBounds.Width > paintParams1.ItemsBounds.Right)
          {
            if (paintParams1.LayoutType == QToolBarLayoutType.ExpandOnNoFit)
            {
              num8 = paintParams1.ItemsBounds.Left;
              index2 = 0;
              ++index1;
            }
            else if (paintParams1.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit && paintParams1.LastVisibleItem < 0)
              paintParams1.LastVisibleItem = index3 - 1;
          }
          if (objArray[index1] == null)
            objArray[index1] = (object) new QMenuItem[commands.Count];
          command.ForceInvisibility = paintParams1.LastVisibleItem >= 0 && this.ItemPainter.ShowControl((QCommand) command, configuration, destinationControl);
          ((QMenuItem[]) objArray[index1])[index2] = command;
          int num9 = num8;
          rectangle1 = command.OuterBounds;
          int width1 = rectangle1.Width;
          num8 = num9 + width1;
          ++index2;
        }
        else
          command.PutOuterBounds(Rectangle.Empty);
      }
      paintParams1.ItemsBounds = QMath.SetHeight(paintParams1.ItemsBounds, num7 * (index1 + 1));
      if (flag2)
      {
        rectangle1 = paintParams1.ItemsBounds;
        if (rectangle1.Height != rectangle.Height)
        {
          QToolBarPaintParams qtoolBarPaintParams = paintParams1;
          Rectangle itemsBounds = paintParams1.ItemsBounds;
          int top2 = rectangle.Top;
          int bottom = rectangle.Bottom;
          rectangle1 = paintParams1.ItemsBounds;
          int height = rectangle1.Height;
          int startForCenter = QMath.GetStartForCenter(top2, bottom, height);
          Rectangle rectangle2 = QMath.SetY(itemsBounds, startForCenter);
          qtoolBarPaintParams.ItemsBounds = rectangle2;
        }
      }
      rectangle1 = paintParams1.ItemsBounds;
      int num10 = rectangle1.Left;
      rectangle1 = paintParams1.ItemsBounds;
      int top3 = rectangle1.Top;
      int index4 = 0;
      int index5 = 0;
      int num11 = 0;
      for (bool flag5 = false; index4 < objArray.Length && objArray[index4] != null && !flag5; ++index4)
      {
        QMenuItem[] qmenuItemArray = (QMenuItem[]) objArray[index4];
        while (index5 < qmenuItemArray.Length && qmenuItemArray[index5] != null && !flag5)
        {
          QMenuItem command = qmenuItemArray[index5];
          if (command.IsSeparator)
          {
            QMenuItem qmenuItem = command;
            rectangle1 = command.SeparatorBounds;
            int left = rectangle1.Left;
            rectangle1 = command.SeparatorBounds;
            int y = rectangle1.Top + configuration.SeparatorRelativeStart;
            rectangle1 = command.SeparatorBounds;
            int width = rectangle1.Width;
            int height = val2_2 - configuration.SeparatorRelativeStart + configuration.SeparatorRelativeEnd;
            Rectangle rectangle3 = new Rectangle(left, y, width, height);
            qmenuItem.PutSeparatorBounds(rectangle3);
          }
          QMenuItem qmenuItem1 = command;
          int x1 = num10;
          int y1 = top3;
          rectangle1 = command.OuterBounds;
          int width2 = rectangle1.Width;
          int height1 = num7;
          Rectangle rectangle4 = new Rectangle(x1, y1, width2, height1);
          qmenuItem1.PutOuterBounds(rectangle4);
          if (this.ItemPainter.ShowHasChildItems((QCommand) command, configuration, destinationControl) && destinationControl is QToolBar)
          {
            int all = this.ItemPainter.UsedHasChildItemsSpacing((QCommand) command, configuration, destinationControl).All;
            rectangle1 = command.HasChildItemsBounds;
            int width3 = rectangle1.Width;
            int num12 = all + width3;
            QMenuItem qmenuItem2 = command;
            rectangle1 = command.Bounds;
            int x2 = rectangle1.Right - num12;
            rectangle1 = command.Bounds;
            int top4 = rectangle1.Top;
            int width4 = num12;
            rectangle1 = command.Bounds;
            int height2 = rectangle1.Height;
            Rectangle rectangle5 = new Rectangle(x2, top4, width4, height2);
            qmenuItem2.PutHasChildItemsHotBounds(rectangle5);
            QMenuItem qmenuItem3 = command;
            Rectangle childItemsBounds = command.HasChildItemsBounds;
            rectangle1 = command.HasChildItemsBounds;
            int num13 = rectangle1.Left + command.Padding.Right;
            Rectangle rectangle6 = QMath.SetX(childItemsBounds, num13);
            qmenuItem3.PutHasChildItemsBounds(rectangle6);
          }
          if (!command.IsSeparator)
            this.ItemPainter.AlignContentsElements((QCommand) command, StringAlignment.Center);
          int num14 = num10;
          rectangle1 = command.OuterBounds;
          int width5 = rectangle1.Width;
          num10 = num14 + width5;
          if (paintParams1.LastVisibleItem >= 0 && num11 == paintParams1.LastVisibleItem)
            flag5 = true;
          if (this.ItemPainter.ShowControl((QCommand) command, configuration, destinationControl))
          {
            command.SetControlParent();
            command.CalculateControlBoundsProperties(false);
          }
          ++index5;
          ++num11;
        }
        index5 = 0;
        rectangle1 = paintParams1.ItemsBounds;
        num10 = rectangle1.Left;
        top3 += num7;
      }
      int height3;
      if (!flag2)
      {
        rectangle1 = paintParams1.ItemsBounds;
        height3 = rectangle1.Height;
      }
      else
        height3 = rectangle.Height;
      int height4 = height3;
      QSpacing itemsSpacing;
      if (paintParams1.Stretched)
      {
        paintParams1.RequestedSize = new Size(rectangle.Width, num7);
      }
      else
      {
        QToolBarPaintParams qtoolBarPaintParams = paintParams1;
        int num15 = num6 - rectangle.Left + num1 + objectsHorizontal.Width;
        itemsSpacing = paintParams1.ItemsSpacing;
        int after = itemsSpacing.After;
        Size size = new Size(num15 + after, num7);
        qtoolBarPaintParams.RequestedSize = size;
      }
      if (flag1)
        paintParams1.ProposedSize = new Size(rectangle.Width, height4);
      else if (flag3)
        paintParams1.ProposedSize = new Size(Math.Min(paintParams1.RequestedSize.Width, rectangle.Width), height4);
      else
        paintParams1.ProposedSize = paintParams1.RequestedSize;
      Rectangle rectangle7 = new Rectangle(rectangle.Location, paintParams1.ProposedSize);
      QToolBarPaintParams qtoolBarPaintParams1 = paintParams1;
      Rectangle itemsBounds1 = paintParams1.ItemsBounds;
      int right1 = rectangle7.Right;
      int width6 = objectsHorizontal.Width;
      itemsSpacing = paintParams1.ItemsSpacing;
      int after1 = itemsSpacing.After;
      int num16 = width6 + after1;
      int right2 = right1 - num16;
      Rectangle rectangle8 = QMath.SetRight(itemsBounds1, right2);
      qtoolBarPaintParams1.ItemsBounds = rectangle8;
      rectangle1 = paintParams1.ItemsBounds;
      if (!rectangle1.Size.Equals((object) this.m_oLastItemsSize))
      {
        for (int index6 = 0; index6 < commands.Count; ++index6)
        {
          QMenuItem command = (QMenuItem) commands.GetCommand(index6);
          if (command != null && command.Control != null)
            command.Control.ResetBackground();
        }
      }
      rectangle1 = paintParams1.ItemsBounds;
      this.m_oLastItemsSize = rectangle1.Size;
      QToolBarPaintParams qtoolBarPaintParams2 = paintParams1;
      rectangle1 = paintParams1.ItemsBounds;
      int num17 = rectangle1.Left - rectangle.Left + num2;
      itemsSpacing = paintParams1.ItemsSpacing;
      int after2 = itemsSpacing.After;
      Size size1 = new Size(num17 + after2 + objectsHorizontal.Width, num7);
      qtoolBarPaintParams2.MinimumSize = size1;
      QToolBarPainter.LayoutRightAlignedObjectsHorizontal(rectangle7, rectangle7.Right - objectsHorizontal.Width, paintParams1);
      QToolBarPaintParams qtoolBarPaintParams3 = paintParams1;
      rectangle1 = paintParams1.SizingGripBounds;
      int left1 = rectangle1.Left;
      int y2 = rectangle.Top + paintParams1.SizingGripPadding.Top;
      rectangle1 = paintParams1.SizingGripBounds;
      int width7 = rectangle1.Width;
      int height5 = Math.Max(height4 - paintParams1.SizingGripPadding.Vertical, 0);
      Rectangle rectangle9 = new Rectangle(left1, y2, width7, height5);
      qtoolBarPaintParams3.SizingGripBounds = rectangle9;
      graphics.Dispose();
    }

    private static Size CalculateRightAlignedObjectsVertical(QToolBarPaintParams paintParams)
    {
      int num = 0;
      int height = 0;
      if (paintParams.MdiButtons != null)
      {
        Size requestedSize1 = new Size(paintParams.MdiButtonsSize.Width, paintParams.MdiButtonsSize.Height * paintParams.MdiButtons.VisibleButtonsCount);
        paintParams.MdiButtons.CalculateRequestedSize(requestedSize1);
        Size requestedSize2 = paintParams.MdiButtons.RequestedSize;
        paintParams.MdiButtonsBounds = new Rectangle(0, 0, requestedSize2.Width, requestedSize2.Height);
        height += paintParams.MdiButtonsPadding.Horizontal + requestedSize2.Height;
        num = Math.Max(num, paintParams.MdiButtonsPadding.Vertical + requestedSize2.Width);
      }
      else
        paintParams.MdiButtonsBounds = Rectangle.Empty;
      if (paintParams.ShowCustomizeBar)
      {
        height += paintParams.HasMoreItemsAreaWidth;
        paintParams.CustomizeBounds = new Rectangle(0, 0, 0, paintParams.HasMoreItemsAreaWidth);
      }
      else
        paintParams.CustomizeBounds = Rectangle.Empty;
      return new Size(num, height);
    }

    private static void LayoutRightAlignedObjectsVertical(
      Rectangle rectangle,
      int start,
      QToolBarPaintParams paintParams)
    {
      if (paintParams.MdiButtons != null)
      {
        paintParams.MdiButtonsBounds = new Rectangle(rectangle.Left + paintParams.MdiButtonsPadding.Bottom, start + paintParams.MdiButtonsPadding.Left, paintParams.MdiButtonsBounds.Width, paintParams.MdiButtonsBounds.Height);
        start += paintParams.MdiButtonsBounds.Height + paintParams.MdiButtonsPadding.Horizontal;
      }
      else
        paintParams.MdiButtonsBounds = Rectangle.Empty;
      if (paintParams.ShowCustomizeBar)
      {
        paintParams.CustomizeBounds = new Rectangle(rectangle.Left, start, rectangle.Width, paintParams.HasMoreItemsAreaWidth);
        start += paintParams.HasMoreItemsAreaWidth;
      }
      else
        paintParams.CustomizeBounds = Rectangle.Empty;
    }

    public override void LayoutVertical(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
      base.LayoutVertical(rectangle, configuration, paintParams, destinationControl, commands);
      QToolBarPaintParams paintParams1 = (QToolBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QToolBarPaintParams));
      bool flag1 = (paintParams1.LayoutFlags & QToolBarLayoutFlags.FixedWidth) == QToolBarLayoutFlags.FixedWidth;
      bool flag2 = (paintParams1.LayoutFlags & QToolBarLayoutFlags.FixedHeight) == QToolBarLayoutFlags.FixedHeight;
      bool flag3 = (paintParams1.LayoutFlags & QToolBarLayoutFlags.MaximumWidth) == QToolBarLayoutFlags.MaximumWidth;
      int val2_1 = configuration.MinimumItemHeight;
      int val2_2 = 0;
      int num1 = 0;
      Size empty = Size.Empty;
      int num2 = 0;
      bool flag4 = false;
      int num3 = rectangle.Top;
      Graphics graphics = destinationControl.CreateGraphics();
      paintParams1.LastVisibleItem = -1;
      Rectangle rectangle1;
      if (paintParams1.ShowSizingGrip)
      {
        int y = num3 + paintParams1.SizingGripPadding.Left;
        paintParams1.SizingGripBounds = new Rectangle(0, y, 0, paintParams1.SizingGripWidth);
        int num4 = y;
        rectangle1 = paintParams1.SizingGripBounds;
        int num5 = rectangle1.Height + paintParams1.SizingGripPadding.Right;
        num3 = num4 + num5;
      }
      else
        paintParams1.SizingGripBounds = Rectangle.Empty;
      if (paintParams1.ActiveMdiIcon != null)
      {
        int y = num3 + paintParams1.ActiveMdiIconPadding.Top;
        val2_1 = Math.Max(paintParams1.ActiveMdiIconPadding.Vertical + configuration.IconSize.Width, val2_1);
        paintParams1.ActiveMdiIconBounds = new Rectangle(rectangle.Left + paintParams1.ActiveMdiIconPadding.Top, y, configuration.IconSize.Width, configuration.IconSize.Height);
        num3 = y + (paintParams1.ActiveMdiIconBounds.Height + paintParams1.ActiveMdiIconPadding.Right);
      }
      else
        paintParams1.ActiveMdiIconBounds = Rectangle.Empty;
      int num6 = num3 + paintParams1.ItemsSpacing.Before;
      Size alignedObjectsVertical = QToolBarPainter.CalculateRightAlignedObjectsVertical(paintParams1);
      int num7 = Math.Max(alignedObjectsVertical.Width, val2_1);
      paintParams1.ItemsBounds = flag1 || flag3 ? Rectangle.FromLTRB(rectangle.Left, num6, rectangle.Right, rectangle.Bottom - (alignedObjectsVertical.Height + paintParams1.ItemsSpacing.After)) : new Rectangle(rectangle.Top, num6, rectangle.Width, int.MaxValue);
      int left1 = rectangle.Left;
      int num8 = paintParams1.ItemsBounds.Top;
      int index1 = 0;
      int index2 = 0;
      object[] objArray = new object[commands.Count];
      for (int index3 = 0; index3 < commands.Count; ++index3)
      {
        QMenuItem command = (QMenuItem) commands.GetCommand(index3);
        command.EmptyCachedObjects();
        if (command.IsVisible)
        {
          command.Orientation = QCommandOrientation.Vertical;
          if (command.IsSeparator)
          {
            command.Padding = new QPadding(0, configuration.ItemPadding.Top, configuration.ItemPadding.Bottom, 0);
            command.Margin = new QMargin(configuration.SeparatorSpacing.Before, configuration.ItemMargin.Top, configuration.ItemMargin.Bottom, configuration.SeparatorSpacing.After);
            int height = configuration.SeparatorMask == null ? (paintParams1.ShadeSeparator ? configuration.SeparatorSize + 1 : configuration.SeparatorSize) : configuration.CalculateSeparatorSize(QCommandOrientation.Vertical);
            command.PutSeparatorBounds(new Rectangle(0, 0, 0, height));
          }
          else
          {
            command.Padding = configuration.ItemPadding;
            command.Margin = new QMargin(configuration.ItemMargin.Left, configuration.ItemMargin.Top, configuration.ItemMargin.Bottom, configuration.ItemMargin.Right);
          }
          this.ItemPainter.CalculateItemContents((QCommand) command, configuration, 0, paintParams1.StringFormat, destinationControl, graphics);
          num7 = Math.Max(command.OuterBounds.Width, num7);
          val2_2 = Math.Max(command.ContentsBounds.Width, val2_2);
          if (!flag4)
          {
            num2 = command.OuterBounds.Height;
            flag4 = true;
          }
          num1 += command.OuterBounds.Height;
          if (index3 > 0 && (flag1 || flag3) && num8 + command.OuterBounds.Height > paintParams1.ItemsBounds.Bottom)
          {
            if (paintParams1.LayoutType == QToolBarLayoutType.ExpandOnNoFit)
            {
              num8 = paintParams1.ItemsBounds.Left;
              index2 = 0;
              ++index1;
            }
            else if (paintParams1.LayoutType == QToolBarLayoutType.SetMoreItemsOnNoFit && paintParams1.LastVisibleItem < 0)
              paintParams1.LastVisibleItem = index3 - 1;
          }
          if (objArray[index1] == null)
            objArray[index1] = (object) new QMenuItem[commands.Count];
          ((QMenuItem[]) objArray[index1])[index2] = command;
          int num9 = num8;
          rectangle1 = command.OuterBounds;
          int height1 = rectangle1.Height;
          num8 = num9 + height1;
          ++index2;
          if (paintParams1.LastVisibleItem >= 0 && this.ItemPainter.ShowControl((QCommand) command, configuration, destinationControl))
            command.Control.Visible = false;
        }
        else
          command.PutOuterBounds(Rectangle.Empty);
      }
      paintParams1.ItemsBounds = QMath.SetWidth(paintParams1.ItemsBounds, num7 * (index1 + 1));
      if (flag2)
      {
        rectangle1 = paintParams1.ItemsBounds;
        if (rectangle1.Width != rectangle.Width)
        {
          QToolBarPaintParams qtoolBarPaintParams = paintParams1;
          Rectangle itemsBounds = paintParams1.ItemsBounds;
          int left2 = rectangle.Left;
          int right = rectangle.Right;
          rectangle1 = paintParams1.ItemsBounds;
          int width = rectangle1.Width;
          int startForCenter = QMath.GetStartForCenter(left2, right, width);
          Rectangle rectangle2 = QMath.SetX(itemsBounds, startForCenter);
          qtoolBarPaintParams.ItemsBounds = rectangle2;
        }
      }
      rectangle1 = paintParams1.ItemsBounds;
      int left3 = rectangle1.Left;
      rectangle1 = paintParams1.ItemsBounds;
      int num10 = rectangle1.Top;
      int index4 = 0;
      int index5 = 0;
      int num11 = 0;
      for (bool flag5 = false; index4 < objArray.Length && objArray[index4] != null && !flag5; ++index4)
      {
        QMenuItem[] qmenuItemArray = (QMenuItem[]) objArray[index4];
        while (index5 < qmenuItemArray.Length && qmenuItemArray[index5] != null && !flag5)
        {
          QMenuItem command = qmenuItemArray[index5];
          if (command.IsSeparator)
          {
            QMenuItem qmenuItem = command;
            rectangle1 = command.SeparatorBounds;
            int x = rectangle1.Left - configuration.SeparatorRelativeEnd;
            rectangle1 = command.SeparatorBounds;
            int top = rectangle1.Top;
            int width = val2_2 + configuration.SeparatorRelativeEnd - configuration.SeparatorRelativeStart;
            rectangle1 = command.SeparatorBounds;
            int height = rectangle1.Height;
            Rectangle rectangle3 = new Rectangle(x, top, width, height);
            qmenuItem.PutSeparatorBounds(rectangle3);
          }
          QMenuItem qmenuItem1 = command;
          int x1 = left3;
          int y = num10;
          int width1 = num7;
          rectangle1 = command.OuterBounds;
          int height2 = rectangle1.Height;
          Rectangle rectangle4 = new Rectangle(x1, y, width1, height2);
          qmenuItem1.PutOuterBounds(rectangle4);
          if (this.ItemPainter.ShowHasChildItems((QCommand) command, configuration, destinationControl) && destinationControl is QToolBar)
          {
            QSpacing qspacing = this.ItemPainter.UsedHasChildItemsSpacing((QCommand) command, configuration, destinationControl);
            int all = qspacing.All;
            rectangle1 = command.HasChildItemsBounds;
            int width2 = rectangle1.Width;
            int num12 = all + width2;
            QMenuItem qmenuItem2 = command;
            rectangle1 = command.Bounds;
            int x2 = rectangle1.Right - num12;
            rectangle1 = command.Bounds;
            int top = rectangle1.Top;
            int width3 = num12;
            rectangle1 = command.Bounds;
            int height3 = rectangle1.Height;
            Rectangle rectangle5 = new Rectangle(x2, top, width3, height3);
            qmenuItem2.PutHasChildItemsHotBounds(rectangle5);
            QMenuItem qmenuItem3 = command;
            Rectangle childItemsBounds = command.HasChildItemsBounds;
            rectangle1 = command.Bounds;
            int num13 = rectangle1.Right - qspacing.After;
            rectangle1 = command.HasChildItemsBounds;
            int width4 = rectangle1.Width;
            int num14 = num13 - width4;
            rectangle1 = command.ContentsBounds;
            int left4 = rectangle1.Left;
            int num15 = num14 - left4;
            Rectangle rectangle6 = QMath.SetX(childItemsBounds, num15);
            qmenuItem3.PutHasChildItemsBounds(rectangle6);
          }
          if (!command.IsSeparator)
            this.ItemPainter.AlignContentsElements((QCommand) command, StringAlignment.Center);
          int num16 = num10;
          rectangle1 = command.OuterBounds;
          int height4 = rectangle1.Height;
          num10 = num16 + height4;
          if (paintParams1.LastVisibleItem >= 0 && num11 == paintParams1.LastVisibleItem)
            flag5 = true;
          if (this.ItemPainter.ShowControl((QCommand) command, configuration, destinationControl))
          {
            command.SetControlParent();
            command.CalculateControlBoundsProperties(false);
          }
          ++index5;
          ++num11;
        }
        index5 = 0;
        rectangle1 = paintParams1.ItemsBounds;
        num10 = rectangle1.Top;
        left3 += num7;
      }
      int width5;
      if (!flag2)
      {
        rectangle1 = paintParams1.ItemsBounds;
        width5 = rectangle1.Width;
      }
      else
        width5 = rectangle.Width;
      int width6 = width5;
      QSpacing itemsSpacing;
      if (paintParams1.Stretched)
      {
        paintParams1.RequestedSize = new Size(num7, rectangle.Height);
      }
      else
      {
        QToolBarPaintParams qtoolBarPaintParams = paintParams1;
        int width7 = num7;
        int num17 = num6 - rectangle.Top + num1 + alignedObjectsVertical.Height;
        itemsSpacing = paintParams1.ItemsSpacing;
        int after = itemsSpacing.After;
        int height = num17 + after;
        Size size = new Size(width7, height);
        qtoolBarPaintParams.RequestedSize = size;
      }
      if (flag1)
        paintParams1.ProposedSize = new Size(width6, rectangle.Height);
      else if (flag3)
        paintParams1.ProposedSize = new Size(width6, Math.Min(paintParams1.RequestedSize.Height, rectangle.Height));
      else
        paintParams1.ProposedSize = paintParams1.RequestedSize;
      Rectangle rectangle7 = new Rectangle(rectangle.Location, paintParams1.ProposedSize);
      QToolBarPaintParams qtoolBarPaintParams1 = paintParams1;
      Rectangle itemsBounds1 = paintParams1.ItemsBounds;
      int bottom1 = rectangle7.Bottom;
      int height5 = alignedObjectsVertical.Height;
      itemsSpacing = paintParams1.ItemsSpacing;
      int after1 = itemsSpacing.After;
      int num18 = height5 + after1;
      int bottom2 = bottom1 - num18;
      Rectangle rectangle8 = QMath.SetBottom(itemsBounds1, bottom2);
      qtoolBarPaintParams1.ItemsBounds = rectangle8;
      rectangle1 = paintParams1.ItemsBounds;
      if (!rectangle1.Size.Equals((object) this.m_oLastItemsSize))
      {
        for (int index6 = 0; index6 < commands.Count; ++index6)
        {
          QMenuItem command = (QMenuItem) commands.GetCommand(index6);
          if (command != null && command.Control != null)
            command.Control.ResetBackground();
        }
      }
      rectangle1 = paintParams1.ItemsBounds;
      this.m_oLastItemsSize = rectangle1.Size;
      QToolBarPaintParams qtoolBarPaintParams2 = paintParams1;
      int width8 = num7;
      rectangle1 = paintParams1.ItemsBounds;
      int num19 = rectangle1.Top - rectangle.Top + num2;
      itemsSpacing = paintParams1.ItemsSpacing;
      int after2 = itemsSpacing.After;
      int height6 = num19 + after2 + alignedObjectsVertical.Height;
      Size size1 = new Size(width8, height6);
      qtoolBarPaintParams2.MinimumSize = size1;
      QToolBarPainter.LayoutRightAlignedObjectsVertical(rectangle7, rectangle7.Bottom - alignedObjectsVertical.Height, paintParams1);
      QToolBarPaintParams qtoolBarPaintParams3 = paintParams1;
      int x3 = rectangle.Left + paintParams1.SizingGripPadding.Top;
      rectangle1 = paintParams1.SizingGripBounds;
      int top1 = rectangle1.Top;
      int width9 = Math.Max(width6 - paintParams1.SizingGripPadding.Vertical, 0);
      rectangle1 = paintParams1.SizingGripBounds;
      int height7 = rectangle1.Height;
      Rectangle rectangle9 = new Rectangle(x3, top1, width9, height7);
      qtoolBarPaintParams3.SizingGripBounds = rectangle9;
      graphics.Dispose();
    }

    public override void DrawItem(
      QCommand command,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QCommandContainer parentContainer,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
      if (command.Bounds.Width <= 0 || command.Bounds.Height <= 0)
        return;
      QMenuItem command1 = (QMenuItem) QMisc.AssertObjectOfType((object) command, nameof (command), typeof (QMenuItem));
      QToolBarPaintParams qtoolBarPaintParams = (QToolBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QToolBarPaintParams));
      bool flag1 = (flags & QCommandPaintOptions.Hot) == QCommandPaintOptions.Hot;
      bool flag2 = (flags & QCommandPaintOptions.Expanded) == QCommandPaintOptions.Expanded;
      bool flag3 = (flags & QCommandPaintOptions.Pressed) == QCommandPaintOptions.Pressed;
      command1.Flags = flags;
      if (!command1.IsSeparator)
      {
        QAppearanceWrapper appearance = new QAppearanceWrapper((IQAppearance) configuration.ActivatedItemAppearance);
        if (command1.Orientation == QCommandOrientation.Vertical)
        {
          appearance.AdjustBordersForVerticalOrientation();
          appearance.AdjustBackgroundOrientationsToVertical();
        }
        if (command1.IsEnabled)
        {
          QColorSet colors = (QColorSet) null;
          if (flag2)
            colors = new QColorSet(qtoolBarPaintParams.ExpandedItemBackground1Color, qtoolBarPaintParams.ExpandedItemBackground2Color, qtoolBarPaintParams.ExpandedItemBorderColor);
          else if (flag3 && flag1)
            colors = new QColorSet(qtoolBarPaintParams.PressedItemBackground1Color, qtoolBarPaintParams.PressedItemBackground2Color, qtoolBarPaintParams.PressedItemBorderColor);
          else if (flag3 || flag1)
            colors = new QColorSet(qtoolBarPaintParams.HotItemBackground1Color, qtoolBarPaintParams.HotItemBackground2Color, qtoolBarPaintParams.HotItemBorderColor);
          if (colors != null)
            QRectanglePainter.Default.Paint(command1.Bounds, (IQAppearance) appearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        else if (!command1.MouseIsOverMenuItem)
        {
          QColorSet colors = (QColorSet) null;
          if (flag2)
            colors = new QColorSet(Color.Empty, Color.Empty, qtoolBarPaintParams.ExpandedItemBorderColor);
          else if (flag1)
            colors = new QColorSet(Color.Empty, Color.Empty, qtoolBarPaintParams.HotItemBorderColor);
          if (colors != null)
            QRectanglePainter.Default.FillForeground(command1.Bounds, (IQAppearance) appearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        }
        if (this.ItemPainter.ShowIcon((QCommand) command1, configuration, (Control) parentContainer))
        {
          if (command1.Checked)
          {
            int num = 2;
            Rectangle parent = command1.ContentsRectangleToParent(command1.IconBounds);
            parent.Inflate(num, num);
            QRectanglePainter.Default.Paint(parent, (IQAppearance) new QAppearanceWrapper((IQAppearance) null)
            {
              BackgroundStyle = QColorStyle.Gradient,
              GradientAngle = qtoolBarPaintParams.ItemGradientAngle
            }, new QColorSet(qtoolBarPaintParams.CheckedItemBackground1Color, qtoolBarPaintParams.CheckedItemBackground2Color, qtoolBarPaintParams.CheckedItemBorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          }
          if (command1.UsedIcon != null)
          {
            if (command1.IsEnabled || !command1.DisabledIconGrayScaled)
            {
              Color replaceColorWith = flag1 || flag2 ? qtoolBarPaintParams.TextActiveColor : qtoolBarPaintParams.TextColor;
              QControlPaint.DrawIcon(graphics, command1.UsedIcon, command1.UsedIconReplaceColor, replaceColorWith, command1.ContentsRectangleToParent(command1.IconBounds));
            }
            else
              QControlPaint.DrawIconDisabled(graphics, command1.UsedIcon, command1.ContentsRectangleToParent(command1.IconBounds));
          }
        }
        Brush brush = !command1.IsEnabled || !this.ItemPainter.ShowTitle((QCommand) command1, configuration, (Control) parentContainer) && !this.ItemPainter.ShowShortcut((QCommand) command1, configuration, (Control) parentContainer) ? (Brush) new SolidBrush(qtoolBarPaintParams.TextDisabledColor) : (flag1 || flag2 || flag3 ? (Brush) new SolidBrush(qtoolBarPaintParams.TextActiveColor) : (Brush) new SolidBrush(qtoolBarPaintParams.TextColor));
        if (this.ItemPainter.ShowTitle((QCommand) command1, configuration, (Control) parentContainer))
          graphics.DrawString(command1.Title, qtoolBarPaintParams.Font, brush, (RectangleF) command1.ContentsRectangleToParent(command1.TextBounds), qtoolBarPaintParams.StringFormat);
        if (this.ItemPainter.ShowShortcut((QCommand) command1, configuration, (Control) parentContainer))
          graphics.DrawString(command1.ShortcutString, qtoolBarPaintParams.Font, brush, (RectangleF) command1.ContentsRectangleToParent(command1.ShortcutBounds), qtoolBarPaintParams.StringFormat);
        brush?.Dispose();
        if (!this.ItemPainter.ShowHasChildItems((QCommand) command1, configuration, (Control) parentContainer))
          return;
        Image image;
        if (command1.IsEnabled)
        {
          if (flag1 && !flag2 && parentContainer is QToolBar && !qtoolBarPaintParams.ExpandOnItemClick)
            QRectanglePainter.Default.Paint(command1.HasChildItemsHotBounds, (IQAppearance) appearance, new QColorSet(qtoolBarPaintParams.HotItemBackground1Color, qtoolBarPaintParams.HotItemBackground2Color, qtoolBarPaintParams.HotItemBorderColor), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
          image = flag1 || flag2 || flag3 ? QControlPaint.CreateColorizedImage(configuration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), qtoolBarPaintParams.TextActiveColor) : QControlPaint.CreateColorizedImage(configuration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), qtoolBarPaintParams.TextColor);
        }
        else
          image = QControlPaint.CreateColorizedImage(configuration.UsedHasChildItemsMask, Color.Empty, Color.FromArgb((int) byte.MaxValue, 0, 0), qtoolBarPaintParams.TextDisabledColor);
        graphics.DrawImage(image, command1.ContentsRectangleToParent(command1.HasChildItemsBounds));
      }
      else if (configuration.SeparatorMask != null)
      {
        Image image = configuration.SeparatorMask;
        if (command1.Orientation == QCommandOrientation.Vertical)
          image = (Image) QControlPaint.RotateFlipImage(image, RotateFlipType.Rotate90FlipNone);
        QControlPaint.DrawImage(image, Color.FromArgb((int) byte.MaxValue, 0, 0), qtoolBarPaintParams.SeparatorColor, command1.Orientation == QCommandOrientation.Horizontal ? QImageAlign.RepeatedVertical : QImageAlign.RepeatedHorizontal, command1.ContentsRectangleToParent(command1.SeparatorBounds), configuration.SeparatorMask.Size, graphics);
        if (image == configuration.SeparatorMask)
          return;
        image.Dispose();
      }
      else
        this.DrawSeparator(command1, qtoolBarPaintParams.SeparatorColor, qtoolBarPaintParams.SizingGripLightColor, qtoolBarPaintParams.ShadeSeparator, graphics);
    }

    public virtual void DrawSeparator(
      QMenuItem item,
      Color separatorColor,
      Color shadeColor,
      bool addShade,
      Graphics graphics)
    {
      Brush brush1 = (Brush) new SolidBrush(separatorColor);
      int num = 1;
      graphics.FillRectangle(brush1, item.ContentsRectangleToParent(item.SeparatorBounds));
      brush1.Dispose();
      if (!addShade)
        return;
      Brush brush2 = (Brush) new SolidBrush(shadeColor);
      Rectangle rect = Rectangle.Empty;
      if (item.Orientation == QCommandOrientation.Horizontal)
        rect = item.ContentsRectangleToParent(new Rectangle(item.SeparatorBounds.Right - num, item.SeparatorBounds.Top, num, item.SeparatorBounds.Height));
      else if (item.Orientation == QCommandOrientation.Vertical)
        rect = item.ContentsRectangleToParent(new Rectangle(item.SeparatorBounds.Left, item.SeparatorBounds.Bottom - num, item.SeparatorBounds.Width, num));
      graphics.FillRectangle(brush2, rect);
      brush2.Dispose();
    }

    public virtual void DrawToolBarSizingGrip(
      Rectangle rectangle,
      Color foreColor,
      Color backColor,
      QSizingGripStyle sizingGripStyle,
      bool addShade,
      QCommandContainerOrientation orientation,
      Graphics graphics)
    {
      Brush brush1 = (Brush) new SolidBrush(foreColor);
      Brush brush2 = (Brush) new SolidBrush(backColor);
      switch (sizingGripStyle)
      {
        case QSizingGripStyle.Dots:
          int num1 = 2;
          int num2 = addShade ? 1 : 0;
          int num3 = addShade ? 2 : 1;
          for (int top = rectangle.Top; top + num1 + num2 <= rectangle.Bottom; top += num1 + num3)
          {
            for (int left = rectangle.Left; left + num1 + num2 <= rectangle.Right; left += num1 + num3)
            {
              if (addShade)
                graphics.FillRectangle(brush2, left + num2, top + num2, num1, num1);
              graphics.FillRectangle(brush1, left, top, num1, num1);
            }
          }
          break;
        case QSizingGripStyle.Lines:
          int num4 = 1;
          int num5 = addShade ? 1 : 0;
          int num6 = addShade ? 2 : 1;
          if (orientation == QCommandContainerOrientation.Horizontal)
          {
            for (int top = rectangle.Top; top + num4 <= rectangle.Bottom; top += num4 + num6)
            {
              if (addShade)
                graphics.FillRectangle(brush2, rectangle.Left, top + num5, rectangle.Width, num4);
              graphics.FillRectangle(brush1, rectangle.Left, top, rectangle.Width, num4);
            }
            break;
          }
          for (int left = rectangle.Left; left + num4 < rectangle.Right; left += num4 + num6)
          {
            if (addShade)
              graphics.FillRectangle(brush2, left + num5, rectangle.Top, num4, rectangle.Height);
            graphics.FillRectangle(brush1, left, rectangle.Top, num4, rectangle.Height);
          }
          break;
      }
    }

    public virtual int GetSizingGripMargin(int cornerSize) => cornerSize / 2 + 1;

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
      QToolBarPaintParams qtoolBarPaintParams = (QToolBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QToolBarPaintParams));
      base.DrawControlBackgroundHorizontal(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
      bool flag1 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawMoreItems) == QDrawToolBarFlags.DrawMoreItems;
      bool flag2 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawCustomizeButton) == QDrawToolBarFlags.DrawCustomizeButton;
      bool flag3 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawCustomizeBar) == QDrawToolBarFlags.DrawCustomizeBar;
      bool flag4 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawSizingGrip) == QDrawToolBarFlags.DrawSizingGrip;
      Rectangle bounds1;
      if (flag3)
      {
        int width = Math.Min(rectangle.Width, qtoolBarPaintParams.HasMoreItemsAreaWidth + qtoolBarPaintParams.CornerSize);
        Rectangle bounds2 = new Rectangle(rectangle.Right - width, rectangle.Top, width, rectangle.Height);
        Color background1 = qtoolBarPaintParams.MoreItemsColor1;
        Color background2 = qtoolBarPaintParams.MoreItemsColor2;
        if (flag2 || flag1)
        {
          if (qtoolBarPaintParams.CustomizeAreaState == QButtonState.Hot)
          {
            background1 = qtoolBarPaintParams.HotItemBackground1Color;
            background2 = qtoolBarPaintParams.HotItemBackground2Color;
          }
          else if (qtoolBarPaintParams.CustomizeAreaState == QButtonState.Pressed)
          {
            background1 = qtoolBarPaintParams.PressedItemBackground1Color;
            background2 = qtoolBarPaintParams.PressedItemBackground2Color;
          }
        }
        QRoundedRectanglePainter.Default.FillBackground(bounds2, (IQAppearance) appearance, new QColorSet(background1, background2), new QRoundedRectanglePainterProperties(QDrawRoundedRectangleOptions.TopRight | QDrawRoundedRectangleOptions.BottomRight, qtoolBarPaintParams.CornerSize), new QAppearanceFillerProperties()
        {
          AlternativeBoundsForBrushCreation = rectangle
        }, QPainterOptions.Default, graphics);
        if (flag1 && qtoolBarPaintParams.HasMoreItemsImage != null)
        {
          int num = rectangle.Height / 2;
          int startForCenter1 = QMath.GetStartForCenter(rectangle.Top, rectangle.Top + num, qtoolBarPaintParams.HasMoreItemsImage.Height);
          int startForCenter2 = QMath.GetStartForCenter(rectangle.Right - qtoolBarPaintParams.HasMoreItemsAreaWidth, rectangle.Right, qtoolBarPaintParams.HasMoreItemsImage.Width);
          graphics.DrawImage((Image) qtoolBarPaintParams.HasMoreItemsImage, startForCenter2, startForCenter1, qtoolBarPaintParams.HasMoreItemsImage.Width, qtoolBarPaintParams.HasMoreItemsImage.Height);
        }
        if (flag2 && qtoolBarPaintParams.CustomizeImage != null)
        {
          int num = rectangle.Height / 2;
          int position1 = rectangle.Top + rectangle.Height / 2;
          int startForCenter3 = QMath.GetStartForCenter(position1, position1 + num, qtoolBarPaintParams.CustomizeImage.Height);
          int startForCenter4 = QMath.GetStartForCenter(rectangle.Right - qtoolBarPaintParams.HasMoreItemsAreaWidth, rectangle.Right, qtoolBarPaintParams.CustomizeImage.Width);
          graphics.DrawImage((Image) qtoolBarPaintParams.CustomizeImage, startForCenter4, startForCenter3, qtoolBarPaintParams.CustomizeImage.Width, qtoolBarPaintParams.CustomizeImage.Height);
        }
        bounds1 = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - qtoolBarPaintParams.HasMoreItemsAreaWidth, rectangle.Height);
      }
      else
        bounds1 = rectangle;
      if (qtoolBarPaintParams.ToolBarStyle == QToolBarStyle.Beveled && bounds1.Width > 0 && bounds1.Height > 0)
      {
        QRoundedRectanglePainter.Default.FillBackground(bounds1, (IQAppearance) appearance, new QColorSet(qtoolBarPaintParams.BackgroundColor1, qtoolBarPaintParams.BackgroundColor2), new QRoundedRectanglePainterProperties(QDrawRoundedRectangleOptions.All, qtoolBarPaintParams.CornerSize), QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        int width = 1;
        int num = Math.Max(0, qtoolBarPaintParams.CornerSize - 3);
        Pen pen = new Pen(qtoolBarPaintParams.ShadedLineColor, (float) width);
        graphics.DrawLine(pen, bounds1.Left + num, bounds1.Bottom - width, bounds1.Right - num, bounds1.Bottom - width);
        pen.Dispose();
      }
      if (!flag4)
        return;
      this.DrawToolBarSizingGrip(qtoolBarPaintParams.SizingGripBounds, qtoolBarPaintParams.SizingGripDarkColor, qtoolBarPaintParams.SizingGripLightColor, qtoolBarPaintParams.SizingGripStyle, qtoolBarPaintParams.ShadeSizingGrip, QCommandContainerOrientation.Horizontal, graphics);
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
      QToolBarPaintParams qtoolBarPaintParams = (QToolBarPaintParams) QMisc.AssertObjectOfType((object) paintParams, nameof (paintParams), typeof (QToolBarPaintParams));
      base.DrawControlBackgroundVertical(rectangle, appearance, colorScheme, paintParams, configuration, destinationControl, graphics);
      bool flag1 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawMoreItems) == QDrawToolBarFlags.DrawMoreItems;
      bool flag2 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawCustomizeButton) == QDrawToolBarFlags.DrawCustomizeButton;
      bool flag3 = (qtoolBarPaintParams.Flags & QDrawToolBarFlags.DrawSizingGrip) == QDrawToolBarFlags.DrawSizingGrip;
      QAppearanceWrapper appearance1 = new QAppearanceWrapper((IQAppearance) appearance);
      appearance1.AdjustBackgroundOrientationsToVertical();
      Rectangle bounds1;
      if (flag2)
      {
        int height = Math.Min(rectangle.Height, qtoolBarPaintParams.HasMoreItemsAreaWidth + qtoolBarPaintParams.CornerSize);
        Rectangle bounds2 = new Rectangle(rectangle.Left, rectangle.Bottom - height, rectangle.Width, height);
        Color background1 = qtoolBarPaintParams.MoreItemsColor1;
        Color background2 = qtoolBarPaintParams.MoreItemsColor2;
        if (flag1 || flag2)
        {
          if (qtoolBarPaintParams.CustomizeAreaState == QButtonState.Hot)
          {
            background1 = qtoolBarPaintParams.HotItemBackground1Color;
            background2 = qtoolBarPaintParams.HotItemBackground2Color;
          }
          else if (qtoolBarPaintParams.CustomizeAreaState == QButtonState.Pressed)
          {
            background1 = qtoolBarPaintParams.PressedItemBackground1Color;
            background2 = qtoolBarPaintParams.PressedItemBackground2Color;
          }
        }
        QRoundedRectanglePainter.Default.FillBackground(bounds2, (IQAppearance) appearance1, new QColorSet(background1, background2), new QRoundedRectanglePainterProperties(QDrawRoundedRectangleOptions.BottomLeft | QDrawRoundedRectangleOptions.BottomRight, qtoolBarPaintParams.CornerSize), new QAppearanceFillerProperties()
        {
          AlternativeBoundsForBrushCreation = rectangle
        }, QPainterOptions.Default, graphics);
        if (flag1 && qtoolBarPaintParams.HasMoreItemsImage != null)
        {
          int num = rectangle.Width / 2;
          int startForCenter1 = QMath.GetStartForCenter(rectangle.Left, rectangle.Left + num, qtoolBarPaintParams.HasMoreItemsImage.Width);
          int startForCenter2 = QMath.GetStartForCenter(rectangle.Bottom - qtoolBarPaintParams.HasMoreItemsAreaWidth, rectangle.Bottom, qtoolBarPaintParams.HasMoreItemsImage.Height);
          graphics.DrawImage((Image) qtoolBarPaintParams.HasMoreItemsImage, startForCenter1, startForCenter2, qtoolBarPaintParams.HasMoreItemsImage.Width, qtoolBarPaintParams.HasMoreItemsImage.Height);
        }
        if (flag2 && qtoolBarPaintParams.CustomizeImage != null)
        {
          int num = rectangle.Width / 2;
          int position1 = rectangle.Left + rectangle.Width / 2;
          int startForCenter3 = QMath.GetStartForCenter(position1, position1 + num, qtoolBarPaintParams.CustomizeImage.Width);
          int startForCenter4 = QMath.GetStartForCenter(rectangle.Bottom - qtoolBarPaintParams.HasMoreItemsAreaWidth, rectangle.Bottom, qtoolBarPaintParams.CustomizeImage.Height);
          graphics.DrawImage((Image) qtoolBarPaintParams.CustomizeImage, startForCenter3, startForCenter4, qtoolBarPaintParams.CustomizeImage.Width, qtoolBarPaintParams.CustomizeImage.Height);
        }
        bounds1 = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height - qtoolBarPaintParams.HasMoreItemsAreaWidth);
      }
      else
        bounds1 = rectangle;
      if (qtoolBarPaintParams.ToolBarStyle == QToolBarStyle.Beveled)
      {
        QRoundedRectanglePainter.Default.FillBackground(bounds1, (IQAppearance) appearance1, new QColorSet(qtoolBarPaintParams.BackgroundColor1, qtoolBarPaintParams.BackgroundColor2), new QRoundedRectanglePainterProperties(QDrawRoundedRectangleOptions.All, qtoolBarPaintParams.CornerSize), QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
        int width = 1;
        int num = Math.Max(0, qtoolBarPaintParams.CornerSize - 3);
        Pen pen = new Pen(qtoolBarPaintParams.ShadedLineColor, (float) width);
        graphics.DrawLine(pen, bounds1.Right - width, bounds1.Top + num, bounds1.Right - width, bounds1.Bottom - num);
        pen.Dispose();
      }
      if (!flag3)
        return;
      this.DrawToolBarSizingGrip(qtoolBarPaintParams.SizingGripBounds, qtoolBarPaintParams.SizingGripDarkColor, qtoolBarPaintParams.SizingGripLightColor, qtoolBarPaintParams.SizingGripStyle, qtoolBarPaintParams.ShadeSizingGrip, QCommandContainerOrientation.Vertical, graphics);
    }
  }
}
