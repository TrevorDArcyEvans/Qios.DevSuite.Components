// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindowAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QShapedWindowAppearance : QShadedShapeAppearance
  {
    public QShapedWindowAppearance()
    {
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RectangleShapedWindow]);
      this.Properties.DefineProperty(17, (object) new Point(3, 3));
      this.Properties.DefineProperty(20, (object) false);
    }

    [QShapeDesignVisible(QShapeType.ShapedWindow)]
    [Category("QAppearance")]
    [Description("Gets or sets the shape for this QShapedWindow in this appearance")]
    public override QShape Shape
    {
      get => base.Shape;
      set => base.Shape = value;
    }

    internal void RaiseAppearanceChanged()
    {
      if (this.ChangeNotificationSuspended != 0)
        return;
      this.OnAppearanceChanged(EventArgs.Empty);
    }
  }
}
