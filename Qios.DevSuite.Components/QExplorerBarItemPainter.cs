// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarItemPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QExplorerBarItemPainter : QCommandPainter
  {
    internal virtual Size MeasureText(
      string textValue,
      StringFormat format,
      Control destinationControl,
      QCommandConfiguration configuration,
      Graphics graphics,
      int size)
    {
      Font font = (Font) null;
      QExplorerBarGroupItemConfiguration itemConfiguration1 = configuration as QExplorerBarGroupItemConfiguration;
      QExplorerBarItemConfiguration itemConfiguration2 = configuration as QExplorerBarItemConfiguration;
      if (destinationControl != null)
      {
        switch (textValue)
        {
          case "":
          case null:
            break;
          default:
            if (itemConfiguration1 != null)
              font = QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(itemConfiguration1.Font, itemConfiguration1.FontExpanded, graphics), itemConfiguration1.FontHot, graphics), itemConfiguration1.FontPressed, graphics);
            else if (itemConfiguration2 != null)
              font = QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(itemConfiguration2.Font, itemConfiguration2.FontExpanded, graphics), itemConfiguration2.FontHot, graphics), itemConfiguration2.FontPressed, graphics);
            Size size1;
            if (font != null)
            {
              SizeF sizeF = graphics.MeasureString(textValue, font, size, format);
              size1 = new Size((int) Math.Ceiling((double) sizeF.Width), (int) Math.Ceiling((double) sizeF.Height));
            }
            else
              size1 = base.MeasureText(textValue, format, destinationControl, configuration, graphics);
            return size1;
        }
      }
      return Size.Empty;
    }

    protected override Size MeasureText(
      string textValue,
      StringFormat format,
      Control destinationControl,
      QCommandConfiguration configuration,
      Graphics graphics)
    {
      Font font = (Font) null;
      if (destinationControl != null)
      {
        switch (textValue)
        {
          case "":
          case null:
            break;
          default:
            QExplorerBarGroupItemConfiguration itemConfiguration1 = configuration as QExplorerBarGroupItemConfiguration;
            QExplorerBarItemConfiguration itemConfiguration2 = configuration as QExplorerBarItemConfiguration;
            if (itemConfiguration1 != null)
              font = QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(itemConfiguration1.Font, itemConfiguration1.FontExpanded, graphics), itemConfiguration1.FontHot, graphics), itemConfiguration1.FontPressed, graphics);
            else if (itemConfiguration2 != null)
              font = QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(QControlPaint.GetBiggestFont(itemConfiguration2.Font, itemConfiguration2.FontExpanded, graphics), itemConfiguration2.FontHot, graphics), itemConfiguration2.FontPressed, graphics);
            Size size;
            if (font != null)
            {
              SizeF sizeF = graphics.MeasureString(textValue, font, PointF.Empty, format);
              size = new Size((int) Math.Ceiling((double) sizeF.Width), (int) Math.Ceiling((double) sizeF.Height));
            }
            else
              size = base.MeasureText(textValue, format, destinationControl, configuration, graphics);
            return size;
        }
      }
      return Size.Empty;
    }
  }
}
