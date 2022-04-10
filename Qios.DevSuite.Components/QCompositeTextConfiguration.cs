// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeTextConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QCompositeTextConfiguration : QContentPartConfiguration
  {
    protected internal const int PropDrawTextOptions = 15;
    protected internal const int PropFontDefinition = 16;
    protected internal const int PropFontDefinitionHot = 17;
    protected internal const int PropFontDefinitionPressed = 18;
    protected internal const int PropFontDefinitionExpanded = 19;
    protected internal const int PropWrapText = 20;
    protected internal const int PropOrientation = 21;
    protected new const int CurrentPropertyCount = 7;
    protected new const int TotalPropertyCount = 22;

    public QCompositeTextConfiguration()
    {
      this.Properties.DefineProperty(16, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(17, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(18, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(19, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(15, (object) QDrawTextOptions.None);
      this.Properties.DefineProperty(21, (object) QContentOrientation.Horizontal);
      this.Properties.DefineProperty(20, (object) false);
    }

    protected override int GetRequestedCount() => 22;

    [QPropertyIndex(16)]
    [Description("Gets or sets the default FontDefinition")]
    [Category("QAppearance")]
    public QFontDefinition FontDefinition
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(17)]
    [Description("Gets or sets the FontDefinition for the hot state")]
    public QFontDefinition FontDefinitionHot
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [QPropertyIndex(18)]
    [Description("Gets or sets the FontDefinition for the pressed state")]
    [Category("QAppearance")]
    public QFontDefinition FontDefinitionPressed
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the FontDefinition for the expanded state")]
    [QPropertyIndex(19)]
    public QFontDefinition FontDefinitionExpanded
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(15)]
    [Description("Gets or sets additional options for drawing text.")]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    public QDrawTextOptions DrawTextOptions
    {
      get => (QDrawTextOptions) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(20)]
    [Description("Gets or sets wether text must be wrapped when it doesn't fit.")]
    public bool WrapText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [Description("Gets or sets the orientation")]
    [QPropertyIndex(21)]
    [Category("QAppearance")]
    public QContentOrientation Orientation
    {
      get => (QContentOrientation) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }
  }
}
