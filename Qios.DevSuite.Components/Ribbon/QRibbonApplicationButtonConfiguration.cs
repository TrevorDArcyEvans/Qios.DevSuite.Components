// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonApplicationButtonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonApplicationButtonConfiguration : QCompositeItemConfiguration
  {
    protected const int PropOverlay = 23;
    protected const int PropButtonStyle = 24;
    protected const int PropCustomPaintSize = 25;
    protected new const int CurrentPropertyCount = 3;
    protected new const int TotalPropertyCount = 26;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonApplicationButtonConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(23, (object) new QMargin(0, 0, -20, 0));
      this.Properties.DefineProperty(24, (object) QRibbonApplicationButtonStyle.CustomPaint);
      this.Properties.DefineProperty(25, (object) new Size(40, 40));
    }

    protected override int GetRequestedCount() => 26;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QPropertyIndex(15)]
    public override QShapeAppearance Appearance => (QShapeAppearance) null;

    [QPropertyIndex(23)]
    [Description("Gets or sets the amount of overlay the QRibbonApplicationButton has. If the button has any overlay, a QTranslucentWindow is created that hovers above the button (and possible other controlsIf it doesn't have any overlay the button is just painted on it's parent composite.")]
    [Category("QAppearance")]
    public QMargin Overlay
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(24)]
    [Description("Gets or sets the rendering style of the QRibbonApplicationButton")]
    public QRibbonApplicationButtonStyle ButtonStyle
    {
      get => (QRibbonApplicationButtonStyle) this.Properties.GetPropertyAsValueType(24);
      set => this.Properties.SetProperty(24, (object) value);
    }

    [QPropertyIndex(25)]
    [Category("QAppearance")]
    [Description("Gets or sets the size when the QRibbonApplicationButton.ButtonStyle is set CustomPaint.")]
    public Size CustomPaintSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(25);
      set => this.Properties.SetProperty(25, (object) value);
    }

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
