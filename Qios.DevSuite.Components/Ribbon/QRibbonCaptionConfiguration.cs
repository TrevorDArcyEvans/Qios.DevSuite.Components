// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCaptionConfiguration : QCompositeConfiguration
  {
    private const string m_sDefaultContentPartLayoutOrder = "Icon, ApplicationButtonArea, LaunchBarArea, Text, ItemArea, ButtonArea";
    protected const int PropIconConfiguration = 29;
    protected const int PropApplicationButtonAreaConfiguration = 30;
    protected const int PropLaunchBarAreaConfiguration = 31;
    protected const int PropTextAreaConfiguration = 32;
    protected const int PropItemAreaConfiguration = 33;
    protected const int PropButtonAreaConfiguration = 34;
    protected const int PropAutoUpdateText = 35;
    protected new const int CurrentPropertyCount = 7;
    protected new const int TotalPropertyCount = 36;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonCaptionConfiguration()
    {
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(8, (object) new Size(0, SystemInformation.CaptionHeight));
      this.Properties.DefineProperty(1, (object) new QPadding(3, 1, 1, 0));
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(3, (object) false);
      this.Properties.DefineProperty(22, (object) QCompositeExpandDirection.Down);
      this.Properties.DefineProperty(17, (object) QCompositeExpandBehavior.CloseExpandedItemOnClick);
      this.Properties.DefineResettableProperty(29, (IQResettableValue) new QCompositeIconConfiguration());
      this.IconConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.IconConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.IconConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(30, (IQResettableValue) new QRibbonCaptionGroupConfiguration());
      this.ApplicationButtonAreaConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Near);
      this.ApplicationButtonAreaConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 4, 0, 0));
      this.ApplicationButtonAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(31, (IQResettableValue) new QRibbonCaptionGroupConfiguration());
      this.LaunchBarAreaConfiguration.Properties.DefineProperty(2, (object) true);
      this.LaunchBarAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(32, (IQResettableValue) new QRibbonCaptionTextConfiguration());
      this.TextAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(33, (IQResettableValue) new QRibbonCaptionGroupConfiguration());
      this.ItemAreaConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Far);
      this.ItemAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(34, (IQResettableValue) new QRibbonCaptionButtonAreaConfiguration());
      this.ButtonAreaConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineProperty(35, (object) true);
    }

    protected override int GetRequestedCount() => 36;

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonCaptionAppearance();

    protected override string DefaultContentPartLayoutOrder => "Icon, ApplicationButtonArea, LaunchBarArea, Text, ItemArea, ButtonArea";

    [Category("QAppearance")]
    [QPropertyIndex(29)]
    [Description("Gets the configuration of the icon of this QRibbonCaption.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeIconConfiguration IconConfiguration => this.Properties.GetProperty(29) as QCompositeIconConfiguration;

    [Description("Gets the configuration of the ApplicationButton on this QRibbonCaption.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(30)]
    [Category("QAppearance")]
    public QRibbonCaptionGroupConfiguration ApplicationButtonAreaConfiguration => this.Properties.GetProperty(30) as QRibbonCaptionGroupConfiguration;

    [QPropertyIndex(31)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the LaunchBar area of this QRibbonCaption.")]
    [Category("QAppearance")]
    public QRibbonCaptionGroupConfiguration LaunchBarAreaConfiguration => this.Properties.GetProperty(31) as QRibbonCaptionGroupConfiguration;

    [Description("Gets the configuration of the text of this QRibbonCaption.")]
    [QPropertyIndex(32)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QRibbonCaptionTextConfiguration TextAreaConfiguration => this.Properties.GetProperty(32) as QRibbonCaptionTextConfiguration;

    [QPropertyIndex(33)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration of the custom iteam area of this QRibbonCaption.")]
    public QRibbonCaptionGroupConfiguration ItemAreaConfiguration => this.Properties.GetProperty(33) as QRibbonCaptionGroupConfiguration;

    [Description("Gets the configuration of the button area of this QRibbonCaption.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(34)]
    [Category("QAppearance")]
    public QRibbonCaptionButtonAreaConfiguration ButtonAreaConfiguration => this.Properties.GetProperty(34) as QRibbonCaptionButtonAreaConfiguration;

    [QPropertyIndex(35)]
    [Description("Gets the configuration of the button area of this QRibbonCaption.")]
    [Category("QAppearance")]
    public bool AutoUpdateText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(35);
      set => this.Properties.SetProperty(35, (object) value);
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [QPropertyIndex(12)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override QMargin IconBackgroundMargin
    {
      get => base.IconBackgroundMargin;
      set => base.IconBackgroundMargin = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override int IconBackgroundSize
    {
      get => base.IconBackgroundSize;
      set => base.IconBackgroundSize = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool IconBackgroundVisible
    {
      get => base.IconBackgroundVisible;
      set => base.IconBackgroundVisible = value;
    }

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
