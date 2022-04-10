// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QButtonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QButtonConfiguration : QGroupPartConfiguration
  {
    protected const int PropImageConfiguration = 15;
    protected const int PropTextConfiguration = 16;
    protected const int PropFocusRectangleMargin = 17;
    protected const int PropFontDefinition = 18;
    protected const int PropFontDefinitionHot = 19;
    protected const int PropFontDefinitionPressed = 20;
    protected const int PropFontDefinitionFocused = 21;
    protected const int PropDrawTextOptions = 22;
    protected const int PropDrawImageGrayWhenDisabled = 24;
    protected const int PropWrapText = 23;
    protected new const int CurrentPropertyCount = 10;
    protected new const int TotalPropertyCount = 25;
    private string m_sDefaultContentLayoutOrder = "Image, Text";

    public QButtonConfiguration()
    {
      this.Properties.DefineProperty(4, (object) true);
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineProperty(2, (object) true);
      this.Properties.DefineProperty(3, (object) true);
      this.Properties.DefineProperty(18, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(19, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(20, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(21, (object) QFontDefinition.Empty);
      this.Properties.DefineProperty(22, (object) QDrawTextOptions.EndEllipsis);
      this.Properties.DefineProperty(24, (object) true);
      this.Properties.DefineProperty(23, (object) true);
      this.Properties.DefineProperty(17, (object) new QMargin(-3, -3, -3, -3));
      this.Properties.DefineResettableProperty(15, (IQResettableValue) this.CreateImageConfiguration());
      if (this.ImageConfiguration != null)
      {
        this.ImageConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.ImageConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.ImageConfiguration.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
        this.ImageConfiguration.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
        this.ImageConfiguration.Properties.HideProperties(13, 14, 4, 5, 2, 3);
        this.ImageConfiguration.ConfigurationChanged += new EventHandler(this.ChildObjects_ObjectChanged);
      }
      this.Properties.DefineResettableProperty(16, (IQResettableValue) this.CreateTextConfiguration());
      if (this.TextConfiguration != null)
      {
        this.TextConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.TextConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.TextConfiguration.Properties.DefineProperty(0, (object) new QMargin(3, 2, 2, 3));
        this.TextConfiguration.Properties.DefineProperty(2, (object) true);
        this.TextConfiguration.Properties.DefineProperty(3, (object) true);
        this.TextConfiguration.Properties.HideProperties(14, 4, 5, 2, 3);
        this.TextConfiguration.ConfigurationChanged += new EventHandler(this.ChildObjects_ObjectChanged);
      }
      this.Properties.HideProperties(6, 7, 4, 5, 2, 3, 10, 0);
    }

    protected override int GetRequestedCount() => 25;

    protected override string DefaultContentPartLayoutOrder => this.m_sDefaultContentLayoutOrder;

    protected virtual QContentPartConfiguration CreateImageConfiguration() => new QContentPartConfiguration();

    protected virtual QContentPartConfiguration CreateTextConfiguration() => new QContentPartConfiguration();

    [QPropertyIndex(18)]
    [Category("QAppearance")]
    [Description("Gets or sets the default FontDefinition")]
    public QFontDefinition FontDefinition
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }

    [QPropertyIndex(19)]
    [Category("QAppearance")]
    [Description("Gets or sets the FontDefinition for the hot state")]
    public QFontDefinition FontDefinitionHot
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Description("Gets or sets the FontDefinition for the pressed state")]
    [QPropertyIndex(20)]
    [Category("QAppearance")]
    public QFontDefinition FontDefinitionPressed
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [QPropertyIndex(21)]
    [Category("QAppearance")]
    [Description("Gets or sets the FontDefinition for the focused state")]
    public QFontDefinition FontDefinitionFocused
    {
      get => (QFontDefinition) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }

    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [Description("Gets or sets additional options for drawing text.")]
    [QPropertyIndex(22)]
    [Category("QAppearance")]
    public QDrawTextOptions DrawTextOptions
    {
      get => (QDrawTextOptions) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether the image of the button must be drawn gray if the button is disabled.")]
    [QPropertyIndex(24)]
    public bool DrawImageGrayWhenDisabled
    {
      get => (bool) this.Properties.GetPropertyAsValueType(24);
      set => this.Properties.SetProperty(24, (object) value);
    }

    [QPropertyIndex(23)]
    [Category("QAppearance")]
    [Description("Gets or sets wether text must be wrapped when it doesn't fit.")]
    public bool WrapText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    [QPropertyIndex(17)]
    [Description("Defines how much margin there must be between the border of the button and the focus rectangle.")]
    [Category("QAppearance")]
    public QMargin FocusRectangleMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [Description("Gets the configuration of the image.")]
    [QPropertyIndex(15)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QContentPartConfiguration ImageConfiguration => this.Properties.GetProperty(15) as QContentPartConfiguration;

    [QPropertyIndex(16)]
    [Description("Gets the configuration of the text.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QContentPartConfiguration TextConfiguration => this.Properties.GetProperty(16) as QContentPartConfiguration;

    public override IQPadding[] GetPaddings(IQPart part) => part.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter ? QPartHelper.GetDefaultPaddings(part, this.Padding, objectPainter.Appearance) : base.GetPaddings(part);

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
