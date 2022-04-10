// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabControlContentAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QTabControlContentAppearance : QShadedShapeAppearance
  {
    protected const int PropUseControlBackgroundForShape = 23;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 24;

    public QTabControlContentAppearance()
    {
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.TabControlContent]);
      this.Properties.DefineProperty(19, (object) false);
      this.Properties.DefineProperty(23, (object) false);
    }

    protected override int GetRequestedCount() => 24;

    [QPropertyIndex(23)]
    [Description("Gets or sets whether the Control (QTabPage) background must be used to fill the content Shape")]
    [Category("QAppearance")]
    public virtual bool UseControlBackgroundForShape
    {
      get => (bool) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    [Description("Gets or sets the shape in this appearance")]
    [Category("QAppearance")]
    [QShapeDesignVisible(QShapeType.Content)]
    public override QShape Shape
    {
      get => base.Shape;
      set => base.Shape = value;
    }
  }
}
