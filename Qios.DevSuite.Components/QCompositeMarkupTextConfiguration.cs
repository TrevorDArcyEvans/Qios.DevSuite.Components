// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMarkupTextConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeMarkupTextConfiguration : QContentPartConfiguration
  {
    protected internal const int PropFontDefinition = 15;
    protected new const int CurrentPropertyCount = 1;
    protected new const int TotalPropertyCount = 16;

    public QCompositeMarkupTextConfiguration() => this.Properties.DefineProperty(15, (object) QFontDefinition.Empty);

    protected override int GetRequestedCount() => 16;

    [Category("QAppearance")]
    [Description("Gets or sets the default FontDefinition")]
    [QPropertyIndex(15)]
    public QFontDefinition FontDefinition
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QPropertyIndex(13)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override QPartAlignment ContentAlignmentHorizontal
    {
      get => base.ContentAlignmentHorizontal;
      set => base.ContentAlignmentHorizontal = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QPropertyIndex(14)]
    [Browsable(false)]
    public override QPartAlignment ContentAlignmentVertical
    {
      get => base.ContentAlignmentVertical;
      set => base.ContentAlignmentVertical = value;
    }
  }
}
