// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeOrderedConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeOrderedConfiguration : QCompositeConfiguration
  {
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [QPropertyIndex(12)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the content layout order of the child parts. This defines when a part is layed out. The actual position is also determined by the alignment of a part. Set this string to String.Empty (not null) when you want to order manually or want to add parts.")]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }
  }
}
