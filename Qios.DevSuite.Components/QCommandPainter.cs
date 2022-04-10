// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QCommandPainter
  {
    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual bool ShowTitle(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      QToolItem qtoolItem = command as QToolItem;
      QMenuItem qmenuItem = command as QMenuItem;
      return qtoolItem != null && destinationControl is QToolBar ? (qtoolItem.ItemType & QToolItemType.Title) == QToolItemType.Title && (configuration == null || configuration.TitlesVisible) && qtoolItem.Title != null && qtoolItem.Title.Length > 0 : qmenuItem != null && (configuration == null || configuration.TitlesVisible) && qmenuItem.Title != null && qmenuItem.Title.Length > 0;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual bool ShowIcon(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      QToolItem qtoolItem = command as QToolItem;
      QMenuItem qmenuItem = command as QMenuItem;
      return qtoolItem != null && destinationControl is QToolBar ? (qtoolItem.ItemType & QToolItemType.Icon) == QToolItemType.Icon && (configuration == null || configuration.IconsVisible) && qtoolItem.UsedIcon != null : qmenuItem != null && (configuration == null || configuration.IconsVisible) && qmenuItem.UsedIcon != null;
    }

    public virtual bool ShowControl(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return command is QMenuItem qmenuItem && qmenuItem.Control != null;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual bool ShowShortcut(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      QToolItem qtoolItem = command as QToolItem;
      QMenuItem qmenuItem = command as QMenuItem;
      return qtoolItem != null && destinationControl is QToolBar ? (qtoolItem.ItemType & QToolItemType.Shortcut) == QToolItemType.Shortcut && (configuration == null || configuration.ShortcutsVisible) && qtoolItem.ShortcutKeys != Keys.None : qmenuItem != null && (configuration == null || configuration.ShortcutsVisible) && qmenuItem.ShortcutKeys != Keys.None;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual bool ShowHasChildItems(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return command is QMenuItem qmenuItem && (configuration == null || configuration.HasChildItemsImageVisible) && qmenuItem.HasChildItems;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual QSpacing UsedTitleSpacing(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return configuration == null || !this.ShowTitle(command, configuration, destinationControl) ? QSpacing.Empty : configuration.TitleSpacing;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual QSpacing UsedIconSpacing(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return configuration == null || !this.ShowIcon(command, configuration, destinationControl) ? QSpacing.Empty : configuration.IconSpacing;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual QSpacing UsedShortcutSpacing(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return configuration == null || !this.ShowShortcut(command, configuration, destinationControl) ? QSpacing.Empty : configuration.ShortcutSpacing;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual QSpacing UsedControlSpacing(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return configuration == null || !this.ShowControl(command, configuration, destinationControl) ? QSpacing.Empty : configuration.ControlSpacing;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual QSpacing UsedHasChildItemsSpacing(
      QCommand command,
      QCommandConfiguration configuration,
      Control destinationControl)
    {
      return configuration == null || !this.ShowHasChildItems(command, configuration, destinationControl) ? QSpacing.Empty : configuration.HasChildItemsSpacing;
    }

    protected virtual Size MeasureText(
      string textValue,
      StringFormat format,
      Control destinationControl,
      QCommandConfiguration configuration,
      Graphics graphics)
    {
      return QCommandPainter.MeasureTextOnParentControl(textValue, format, destinationControl, graphics);
    }

    public static Size MeasureTextOnParentControl(
      string textValue,
      StringFormat format,
      Control destinationControl,
      Graphics graphics)
    {
      if (destinationControl != null)
      {
        switch (textValue)
        {
          case "":
          case null:
            break;
          default:
            SizeF sizeF = graphics.MeasureString(textValue, destinationControl.Font, PointF.Empty, format);
            return new Size((int) Math.Ceiling((double) sizeF.Width), (int) Math.Ceiling((double) sizeF.Height));
        }
      }
      return Size.Empty;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual void CalculateItemContents(
      QCommand command,
      QCommandConfiguration configuration,
      int minimumContentsHeight,
      StringFormat stringFormat,
      Control destinationControl,
      Graphics graphics)
    {
      int num1 = 0;
      int num2 = 0;
      QMenuItem command1 = command as QMenuItem;
      QExplorerBar qexplorerBar = destinationControl as QExplorerBar;
      QFloatingMenu qfloatingMenu = destinationControl as QFloatingMenu;
      if (command1 == null)
        return;
      QSpacing qspacing1 = this.UsedIconSpacing((QCommand) command1, configuration, destinationControl);
      QSpacing qspacing2 = this.UsedTitleSpacing((QCommand) command1, configuration, destinationControl);
      QSpacing qspacing3 = this.UsedShortcutSpacing((QCommand) command1, configuration, destinationControl);
      QSpacing qspacing4 = this.UsedControlSpacing((QCommand) command1, configuration, destinationControl);
      QSpacing qspacing5 = this.UsedHasChildItemsSpacing((QCommand) command1, configuration, destinationControl);
      if (command1.Orientation == QCommandOrientation.Horizontal)
      {
        num2 = minimumContentsHeight;
        if (!command1.IsSeparator)
        {
          if (this.ShowIcon((QCommand) command1, configuration, destinationControl))
          {
            int x = num1 + qspacing1.Before;
            Size iconSizes = command1.CalculateIconSizes(configuration.IconSize);
            command1.PutIconBounds(new Rectangle(x, 0, iconSizes.Width, iconSizes.Height));
            num2 = Math.Max(num2, iconSizes.Height);
            num1 = x + (command1.IconBounds.Width + qspacing1.After);
          }
          if (this.ShowTitle((QCommand) command1, configuration, destinationControl))
          {
            int x = num1 + qspacing2.Before;
            Size size = this.MeasureText(command1.Title, stringFormat, destinationControl, configuration, graphics);
            command1.PutTextBounds(new Rectangle(x, 0, size.Width, size.Height));
            if (qexplorerBar == null)
              num2 = Math.Max(num2, size.Height);
            num1 = x + (command1.TextBounds.Width + qspacing2.After);
          }
          if (this.ShowControl((QCommand) command1, configuration, destinationControl))
          {
            int x = num1 + qspacing4.Before;
            command1.PutControlBounds(new Rectangle(x, 0, command1.Control.Width, command1.Control.Height));
            num2 = Math.Max(num2, command1.Control.Height);
            num1 = x + (command1.Control.Width + qspacing4.After);
          }
          if (this.ShowShortcut((QCommand) command1, configuration, destinationControl))
          {
            int x = num1 + qspacing3.Before;
            Size size = this.MeasureText(command1.ShortcutString, stringFormat, destinationControl, configuration, graphics);
            command1.PutShortcutBounds(new Rectangle(x, 0, size.Width, size.Height));
            num2 = Math.Max(num2, size.Height);
            num1 = x + (command1.ShortcutBounds.Width + qspacing3.After);
          }
          if (this.ShowHasChildItems((QCommand) command1, configuration, destinationControl))
          {
            int x = num1 + qspacing5.Before;
            Size size = configuration.UsedHasChildItemsMask.Size;
            command1.PutHasChildItemsBounds(new Rectangle(x, 0, size.Width, size.Height));
            num2 = Math.Max(num2, size.Height);
            num1 = x + (command1.HasChildItemsBounds.Width + qspacing5.After);
          }
        }
        else if (qfloatingMenu != null)
        {
          num1 = 0;
          num2 = command1.SeparatorBounds.Height;
        }
        else
        {
          num2 = 0;
          num1 = command1.SeparatorBounds.Width;
        }
      }
      else if (command1.Orientation == QCommandOrientation.Vertical)
      {
        if (!command1.OrientationForFlow)
          num1 = configuration.MinimumItemHeight;
        if (!command1.IsSeparator)
        {
          if (this.ShowIcon((QCommand) command1, configuration, destinationControl))
          {
            int y = num2 + qspacing1.Before;
            Size iconSizes = command1.CalculateIconSizes(configuration.IconSize);
            command1.PutIconBounds(new Rectangle(0, y, iconSizes.Width, iconSizes.Height));
            num1 = Math.Max(num1, iconSizes.Width);
            num2 = y + (command1.IconBounds.Height + qspacing1.After);
          }
          if (this.ShowTitle((QCommand) command1, configuration, destinationControl))
          {
            int y = num2 + qspacing2.Before;
            Size size = this.MeasureText(command1.Title, stringFormat, destinationControl, configuration, graphics);
            command1.PutTextBounds(new Rectangle(0, y, size.Width, size.Height));
            if (qexplorerBar == null)
              num1 = Math.Max(num1, size.Width);
            num2 = y + (command1.TextBounds.Height + qspacing2.After);
          }
          if (this.ShowControl((QCommand) command1, configuration, destinationControl))
          {
            int y = num2 + qspacing4.Before;
            command1.PutControlBounds(new Rectangle(0, y, command1.Control.Width, command1.Control.Height));
            num1 = Math.Max(num1, command1.Control.Width);
            num2 = y + (command1.Control.Height + qspacing4.After);
          }
          if (this.ShowShortcut((QCommand) command1, configuration, destinationControl))
          {
            int y = num2 + qspacing3.Before;
            Size size = this.MeasureText(command1.ShortcutString, stringFormat, destinationControl, configuration, graphics);
            command1.PutShortcutBounds(new Rectangle(0, y, size.Width, size.Height));
            num1 = Math.Max(num1, size.Width);
            num2 = y + (command1.ShortcutBounds.Height + qspacing3.After);
          }
          if (this.ShowHasChildItems((QCommand) command1, configuration, destinationControl))
          {
            Size size = configuration.UsedHasChildItemsMask.Size;
            if (destinationControl is QToolBar || qexplorerBar != null)
            {
              int num3 = num1 + qspacing5.Before;
              command1.PutHasChildItemsBounds(new Rectangle(0, 0, size.Width, size.Height));
              num1 = num3 + (size.Width + qspacing5.After);
              num2 = Math.Max(size.Height, num2);
            }
            else
            {
              int y = num2 + qspacing5.Before;
              command1.PutHasChildItemsBounds(new Rectangle(0, y, size.Width, size.Height));
              num1 = Math.Max(num1, size.Width);
              num2 = y + (command1.HasChildItemsBounds.Width + qspacing5.After);
            }
          }
          if (command1.OrientationForFlow)
            num2 = Math.Max(num2, configuration.MinimumItemHeight);
        }
        else if (qfloatingMenu == null)
        {
          num1 = 0;
          num2 = command1.SeparatorBounds.Height;
        }
      }
      command1.PutContentsBounds(new Rectangle(0, 0, num1, num2));
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    public virtual void AlignContentsElements(QCommand command, StringAlignment alignment)
    {
      if (!(command is QMenuItem qmenuItem))
        return;
      if (qmenuItem.Orientation == QCommandOrientation.Horizontal)
      {
        switch (alignment)
        {
          case StringAlignment.Near:
            qmenuItem.PutIconBounds(QMath.SetY(qmenuItem.IconBounds, 0));
            qmenuItem.PutTextBounds(QMath.SetY(qmenuItem.TextBounds, 0));
            qmenuItem.PutControlBounds(QMath.SetY(qmenuItem.ControlBounds, 0));
            qmenuItem.PutShortcutBounds(QMath.SetY(qmenuItem.ShortcutBounds, 0));
            qmenuItem.PutHasChildItemsBounds(QMath.SetY(qmenuItem.HasChildItemsBounds, 0));
            qmenuItem.PutSeparatorBounds(QMath.SetY(qmenuItem.SeparatorBounds, 0));
            break;
          case StringAlignment.Center:
            qmenuItem.PutIconBounds(QMath.SetY(qmenuItem.IconBounds, QMath.GetStartForCenter(0, qmenuItem.ContentsBounds.Height, qmenuItem.IconBounds.Height)));
            qmenuItem.PutTextBounds(QMath.SetY(qmenuItem.TextBounds, QMath.GetStartForCenter(0, qmenuItem.ContentsBounds.Height, qmenuItem.TextBounds.Height)));
            qmenuItem.PutControlBounds(QMath.SetY(qmenuItem.ControlBounds, QMath.GetStartForCenter(0, qmenuItem.ContentsBounds.Height, qmenuItem.ControlBounds.Height)));
            qmenuItem.PutShortcutBounds(QMath.SetY(qmenuItem.ShortcutBounds, QMath.GetStartForCenter(0, qmenuItem.ContentsBounds.Height, qmenuItem.ShortcutBounds.Height)));
            qmenuItem.PutHasChildItemsBounds(QMath.SetY(qmenuItem.HasChildItemsBounds, QMath.GetStartForCenter(0, qmenuItem.ContentsBounds.Height, qmenuItem.HasChildItemsBounds.Height)));
            qmenuItem.PutSeparatorBounds(QMath.SetY(qmenuItem.SeparatorBounds, QMath.GetStartForCenter(0, qmenuItem.ContentsBounds.Height, qmenuItem.SeparatorBounds.Height)));
            break;
          case StringAlignment.Far:
            qmenuItem.PutIconBounds(QMath.SetY(qmenuItem.IconBounds, qmenuItem.ContentsBounds.Height - qmenuItem.IconBounds.Height));
            qmenuItem.PutTextBounds(QMath.SetY(qmenuItem.TextBounds, qmenuItem.ContentsBounds.Height - qmenuItem.TextBounds.Height));
            qmenuItem.PutControlBounds(QMath.SetY(qmenuItem.ControlBounds, qmenuItem.ContentsBounds.Height - qmenuItem.ControlBounds.Height));
            qmenuItem.PutShortcutBounds(QMath.SetY(qmenuItem.ShortcutBounds, qmenuItem.ContentsBounds.Height - qmenuItem.ShortcutBounds.Height));
            qmenuItem.PutHasChildItemsBounds(QMath.SetY(qmenuItem.HasChildItemsBounds, qmenuItem.ContentsBounds.Height - qmenuItem.HasChildItemsBounds.Height));
            qmenuItem.PutSeparatorBounds(QMath.SetY(qmenuItem.SeparatorBounds, qmenuItem.ContentsBounds.Height - qmenuItem.SeparatorBounds.Height));
            break;
        }
      }
      else
      {
        if (qmenuItem.Orientation != QCommandOrientation.Vertical)
          return;
        bool flag = qmenuItem.ParentContainer is QToolBar || qmenuItem.ParentContainer is QExplorerBar;
        Rectangle rectangle = qmenuItem.ContentsBounds;
        if (flag)
        {
          rectangle = new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - qmenuItem.HasChildItemsHotBounds.Width, rectangle.Height);
          qmenuItem.PutHasChildItemsBounds(QMath.SetY(qmenuItem.HasChildItemsBounds, QMath.GetStartForCenter(0, rectangle.Height, qmenuItem.HasChildItemsBounds.Height)));
        }
        switch (alignment)
        {
          case StringAlignment.Near:
            qmenuItem.PutIconBounds(QMath.SetX(qmenuItem.IconBounds, rectangle.Width - qmenuItem.IconBounds.Width));
            qmenuItem.PutTextBounds(QMath.SetX(qmenuItem.TextBounds, rectangle.Width - qmenuItem.TextBounds.Width));
            qmenuItem.PutControlBounds(QMath.SetX(qmenuItem.ControlBounds, rectangle.Width - qmenuItem.ControlBounds.Width));
            qmenuItem.PutShortcutBounds(QMath.SetX(qmenuItem.ShortcutBounds, rectangle.Width - qmenuItem.ShortcutBounds.Width));
            qmenuItem.PutSeparatorBounds(QMath.SetX(qmenuItem.SeparatorBounds, rectangle.Width - qmenuItem.SeparatorBounds.Width));
            if (flag)
              break;
            qmenuItem.PutHasChildItemsBounds(QMath.SetX(qmenuItem.HasChildItemsBounds, rectangle.Width - qmenuItem.HasChildItemsBounds.Width));
            break;
          case StringAlignment.Center:
            qmenuItem.PutIconBounds(QMath.SetX(qmenuItem.IconBounds, QMath.GetStartForCenter(0, rectangle.Width, qmenuItem.IconBounds.Width)));
            qmenuItem.PutTextBounds(QMath.SetX(qmenuItem.TextBounds, QMath.GetStartForCenter(0, rectangle.Width, qmenuItem.TextBounds.Width)));
            qmenuItem.PutControlBounds(QMath.SetX(qmenuItem.ControlBounds, QMath.GetStartForCenter(0, rectangle.Width, qmenuItem.ControlBounds.Width)));
            qmenuItem.PutShortcutBounds(QMath.SetX(qmenuItem.ShortcutBounds, QMath.GetStartForCenter(0, rectangle.Width, qmenuItem.ShortcutBounds.Width)));
            qmenuItem.PutSeparatorBounds(QMath.SetX(qmenuItem.SeparatorBounds, QMath.GetStartForCenter(0, rectangle.Width, qmenuItem.SeparatorBounds.Width)));
            if (flag)
              break;
            qmenuItem.PutHasChildItemsBounds(QMath.SetX(qmenuItem.HasChildItemsBounds, QMath.GetStartForCenter(0, rectangle.Width, qmenuItem.HasChildItemsBounds.Width)));
            break;
          case StringAlignment.Far:
            qmenuItem.PutIconBounds(QMath.SetX(qmenuItem.IconBounds, 0));
            qmenuItem.PutTextBounds(QMath.SetX(qmenuItem.TextBounds, 0));
            qmenuItem.PutControlBounds(QMath.SetX(qmenuItem.ControlBounds, 0));
            qmenuItem.PutShortcutBounds(QMath.SetX(qmenuItem.ShortcutBounds, 0));
            qmenuItem.PutSeparatorBounds(QMath.SetX(qmenuItem.SeparatorBounds, 0));
            if (flag)
              break;
            qmenuItem.PutHasChildItemsBounds(QMath.SetX(qmenuItem.HasChildItemsBounds, 0));
            break;
        }
      }
    }
  }
}
