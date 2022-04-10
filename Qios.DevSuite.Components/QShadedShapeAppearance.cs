// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShadedShapeAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QShadedShapeAppearance : 
    QShapeAppearance,
    IQShadedShapeAppearance,
    IQShadedAppearance,
    IQShapeAppearance
  {
    protected const int PropShadeOffset = 17;
    protected const int PropShadeGradientSize = 18;
    protected const int PropShadeVisible = 19;
    protected const int PropShadeClipToShapeBounds = 20;
    protected const int PropShadeClipMargin = 21;
    protected const int PropShadeGrowPadding = 22;
    protected new const int CurrentPropertyCount = 6;
    protected new const int TotalPropertyCount = 23;

    public QShadedShapeAppearance()
    {
      this.Properties.DefineProperty(17, (object) new Point(3, 3));
      this.Properties.DefineProperty(18, (object) 3);
      this.Properties.DefineProperty(19, (object) false);
      this.Properties.DefineProperty(20, (object) false);
      this.Properties.DefineProperty(21, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(22, (object) new QPadding(0, 0, 0, 0));
    }

    protected override int GetRequestedCount() => 23;

    [Category("QAppearance")]
    [QPropertyIndex(19)]
    [Description("Gets or sets whether the shade is visible.")]
    public bool ShadeVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Description("Gets or sets the offset of the shade.")]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    public Point ShadeOffset
    {
      get => (Point) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [QPropertyIndex(20)]
    [Description("Gets or sets whether the shade must be clipped to the bounds of the shape. This is usefull to make sure a shade does not overlap some underlaying element. If the clipping is to small adjust the ShadeClipMargin or make the bounds of the shape bigger.")]
    [Category("QAppearance")]
    public bool ShadeClipToShapeBounds
    {
      get => (bool) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [QPropertyIndex(21)]
    [Description("Gets or sets the margin to correct shade-clipping. This is used with ShadeClipToShapeBounds.")]
    [Category("QAppearance")]
    public QMargin ShadeClipMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }

    [Description("Gets or sets the amount of padding that the shade must grow relative to the Shape. This is usefull for example when a Shape is drawn with Dock = Right and the shade must be drawn till the left of the shape.")]
    [Category("QAppearance")]
    [QPropertyIndex(22)]
    public QPadding ShadeGrowPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(18)]
    [Description("Gets or sets the of the gradient of the shade.")]
    public int ShadeGradientSize
    {
      get => (int) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }
  }
}
