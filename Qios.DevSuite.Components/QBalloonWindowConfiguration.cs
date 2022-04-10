// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QBalloonWindowConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QBalloonWindowConfiguration : QShapedWindowConfiguration
  {
    protected const int PropWrapText = 18;
    protected const int PropDrawTextOptions = 19;
    protected const int PropTextAlign = 20;
    protected const int PropTextPadding = 21;
    protected const int PropDrawText = 22;
    protected const int PropAutoSize = 23;
    protected const int PropCanActivate = 24;
    protected const int PropMarkupTextEnabled = 25;
    protected const int PropLocationOffset = 26;
    protected new const int CurrentPropertyCount = 9;
    protected new const int TotalPropertyCount = 27;

    public QBalloonWindowConfiguration()
    {
      this.Properties.DefineProperty(18, (object) true);
      this.Properties.DefineProperty(19, (object) QDrawTextOptions.IgnorePrefix);
      this.Properties.DefineProperty(20, (object) ContentAlignment.TopLeft);
      this.Properties.DefineProperty(21, (object) new QPadding(1, 0, 0, 1));
      this.Properties.DefineProperty(22, (object) true);
      this.Properties.DefineProperty(23, (object) true);
      this.Properties.DefineProperty(24, (object) false);
      this.Properties.DefineProperty(25, (object) true);
      this.Properties.DefineProperty(26, (object) Point.Empty);
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(4, (object) false);
      this.Properties.DefineProperty(6, (object) false);
    }

    protected override int GetRequestedCount() => 27;

    [Description("Gets or sets a location offset of the QBalloonWindow. This offset can be used to adjust the starting location of a QBalloonWindow. The offset is applied after calculating the preferred position, so no flipping of repositioning will occur because of the offset.")]
    [QPropertyIndex(26)]
    [Category("QAppearance")]
    [DefaultValue(typeof (Point), "0,0")]
    public Point LocationOffset
    {
      get => (Point) this.Properties.GetPropertyAsValueType(26);
      set => this.Properties.SetProperty(26, (object) value);
    }

    [QPropertyIndex(25)]
    [Description("Gets or sets whether the MarkupText object is enabled.")]
    [Category("QAppearance")]
    public bool MarkupTextEnabled
    {
      get => (bool) this.Properties.GetPropertyAsValueType(25);
      set => this.Properties.SetProperty(25, (object) value);
    }

    [QPropertyIndex(20)]
    [Category("QAppearance")]
    [Description("Gets or sets the alignment of the text.")]
    public ContentAlignment TextAlign
    {
      get => (ContentAlignment) this.Properties.GetPropertyAsValueType(20);
      set => this.Properties.SetProperty(20, (object) value);
    }

    [Description("Gets or sets whether the text must be wrapped.")]
    [Category("QAppearance")]
    [QPropertyIndex(18)]
    public bool WrapText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(18);
      set => this.Properties.SetProperty(18, (object) value);
    }

    [Category("QAppearance")]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [QPropertyIndex(19)]
    [Description("Gets or sets additional options for drawing text.")]
    public QDrawTextOptions DrawTextOptions
    {
      get => (QDrawTextOptions) this.Properties.GetPropertyAsValueType(19);
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the window can be activated by the mouse")]
    [QPropertyIndex(24)]
    public bool CanActivate
    {
      get => (bool) this.Properties.GetPropertyAsValueType(24);
      set => this.Properties.SetProperty(24, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the QBalloon draws its Text property")]
    [QPropertyIndex(22)]
    public bool DrawText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    [Description("Gets or sets if the QBalloon automatically sizes based on the text in the Text property")]
    [Category("QAppearance")]
    [QPropertyIndex(23)]
    public bool AutoSize
    {
      get => (bool) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(21)]
    [Description("Gets or sets the padding of the text")]
    public QPadding TextPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }
  }
}
