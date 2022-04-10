// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QShapeAppearance : QAppearanceBase, IQBorderedAppearance, IQShapeAppearance
  {
    protected internal const int PropBorderWidth = 14;
    protected internal const int PropSmoothingMode = 15;
    protected internal const int PropShape = 16;
    protected new const int CurrentPropertyCount = 3;
    protected new const int TotalPropertyCount = 17;
    private EventHandler m_oShapeChangedEventHander;

    public QShapeAppearance()
    {
      this.m_oShapeChangedEventHander = new EventHandler(this.Shape_ShapeChanged);
      this.Properties.DefineProperty(14, (object) 1);
      this.Properties.DefineProperty(15, (object) QSmoothingMode.AntiAlias);
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RectangleShape]);
    }

    protected override int GetRequestedCount() => 17;

    [QPropertyIndex(16)]
    [Category("QAppearance")]
    [Description("Gets or sets the shape for this tabButton in this appearance")]
    public virtual QShape Shape
    {
      get => this.Properties.GetProperty(16) as QShape;
      set
      {
        if (this.Shape != null)
          this.Shape.ShapeChanged -= this.m_oShapeChangedEventHander;
        this.Properties.SetProperty(16, (object) value);
        if (this.Shape == null)
          return;
        this.Shape.ShapeChanged += this.m_oShapeChangedEventHander;
      }
    }

    [Description("Gets or sets the borderWidth")]
    [Category("QAppearance")]
    [QPropertyIndex(14)]
    public int BorderWidth
    {
      get => (int) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [QPropertyIndex(15)]
    [Description("Gets or sets the SmoothingMode")]
    [Category("QAppearance")]
    public QSmoothingMode SmoothingMode
    {
      get => (QSmoothingMode) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    private void Shape_ShapeChanged(object sender, EventArgs e) => this.OnAppearanceChanged(EventArgs.Empty);
  }
}
