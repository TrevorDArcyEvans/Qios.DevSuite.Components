// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeInputBoxItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeInputBoxItemConfiguration : QCompositeItemConfiguration
  {
    public QCompositeInputBoxItemConfiguration()
    {
      this.Properties.DefineProperty(4, (object) true);
      this.Properties.DefineProperty(17, (object) QTristateBool.True);
      this.Properties.DefineProperty(18, (object) QTristateBool.True);
      this.Properties.DefineProperty(1, (object) new QPadding(0, 0, 0, 0));
    }

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QCompositeInputBoxItemAppearance();

    [Browsable(false)]
    public override QPartAlignment AlignmentHorizontal
    {
      get => base.AlignmentHorizontal;
      set => base.AlignmentHorizontal = value;
    }

    [Browsable(false)]
    public override QPartAlignment AlignmentVertical
    {
      get => base.AlignmentVertical;
      set => base.AlignmentVertical = value;
    }

    [Browsable(false)]
    public override QPartDirection Direction
    {
      get => base.Direction;
      set => base.Direction = value;
    }

    [Browsable(false)]
    public override bool StretchHorizontal
    {
      get => base.StretchHorizontal;
      set => base.StretchHorizontal = value;
    }

    [Browsable(false)]
    public override bool StretchVertical
    {
      get => base.StretchVertical;
      set => base.StretchVertical = value;
    }

    [Browsable(false)]
    public override bool ShrinkHorizontal
    {
      get => base.ShrinkHorizontal;
      set => base.ShrinkHorizontal = value;
    }

    [Browsable(false)]
    public override bool ShrinkVertical
    {
      get => base.ShrinkVertical;
      set => base.ShrinkVertical = value;
    }

    [Browsable(false)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [Browsable(false)]
    public override QTristateBool Visible
    {
      get => base.Visible;
      set => base.Visible = value;
    }

    [Browsable(false)]
    public override QCompositeItemLayout Layout
    {
      get => base.Layout;
      set => base.Layout = value;
    }
  }
}
