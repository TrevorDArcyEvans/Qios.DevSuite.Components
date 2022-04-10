// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemGroupConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonItemGroupConfiguration : QCompositeGroupConfiguration
  {
    public QRibbonItemGroupConfiguration()
    {
      this.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(1, (object) new QPadding(0, 0, 0, 0));
    }

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) null;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QShapeAppearance Appearance => (QShapeAppearance) null;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string ContentLayoutOrder
    {
      get => (string) null;
      set
      {
      }
    }
  }
}
