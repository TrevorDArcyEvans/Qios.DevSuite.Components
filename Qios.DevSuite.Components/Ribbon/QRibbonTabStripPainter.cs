// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonTabStripPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(false)]
  public class QRibbonTabStripPainter : QTabStripPainter
  {
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
      base.DrawTabButtonBackground(button, buttonConfiguration, stripConfiguration, buttonAppearance, buttonBounds, controlAndButtonBounds, dockStyle, backColor1, backColor2, borderColor, graphics);
      if (!button.IsHot || !(button.Control is QRibbonPage control))
        return;
      Color color = control.RetrieveFirstDefinedColor("RibbonTabButtonHotOutline");
      GraphicsPath graphicsPath = buttonAppearance.Shape.CreateGraphicsPath(buttonBounds, dockStyle, QShapePathOptions.VisibleLines, (Matrix) null);
      Pen pen = new Pen(Color.Black, (float) (buttonAppearance.BorderWidth * 2));
      graphicsPath.Widen(pen);
      pen.Dispose();
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = QMisc.GetSmoothingMode(buttonAppearance.SmoothingMode);
      Brush brush = (Brush) new SolidBrush(color);
      graphics.FillPath(brush, graphicsPath);
      graphics.SmoothingMode = smoothingMode;
      brush.Dispose();
      graphicsPath.Dispose();
    }
  }
}
