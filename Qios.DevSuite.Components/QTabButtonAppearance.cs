// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonAppearance
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QTabButtonAppearance : QShadedShapeAppearance
  {
    protected const int PropUseControlBackgroundForTabButton = 23;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 24;

    public QTabButtonAppearance()
    {
      this.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.RoundedTab]);
      this.Properties.DefineProperty(20, (object) true);
      this.Properties.DefineProperty(21, (object) new QMargin(5, 5, 0, 5));
      this.Properties.DefineProperty(23, (object) false);
    }

    protected override int GetRequestedCount() => 24;

    [Category("QAppearance")]
    [QShapeDesignVisible(QShapeType.TabButton)]
    [Description("Gets or sets the shape in this appearance")]
    public override QShape Shape
    {
      get => base.Shape;
      set => base.Shape = value;
    }

    [QPropertyIndex(23)]
    [Description("Gets or sets whether the TabButton must extend the backgroundColor of the TabPage")]
    [Category("QAppearance")]
    public virtual bool UseControlBackgroundForTabButton
    {
      get => (bool) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }
  }
}
