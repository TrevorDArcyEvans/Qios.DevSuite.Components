// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarConfiguration : QCompositeGroupConfiguration
  {
    private const string m_sDefaultContentLayoutOrder = "";
    protected const int PropDefaultItemConfiguration = 18;
    protected const int PropCustomizeItemConfiguration = 19;
    protected const int PropShowMoreItemConfiguration = 20;
    protected const int PropDrawShape = 21;
    protected const int PropResizeBehavior = 22;
    protected new const int CurrentPropertyCount = 5;
    protected new const int TotalPropertyCount = 23;
    private static Image m_oDefaultCustomizeMask;
    private static Image m_oDefaultShowMoreMask;
    private EventHandler m_oChildObjectsChangedHandler;

    public QRibbonLaunchBarConfiguration()
    {
      this.m_oChildObjectsChangedHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      if (QRibbonLaunchBarConfiguration.m_oDefaultCustomizeMask == null)
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        QRibbonLaunchBarConfiguration.m_oDefaultCustomizeMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonLaunchBarCustomizeMask.png"));
        QRibbonLaunchBarConfiguration.m_oDefaultShowMoreMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonLaunchBarMoreItemsMask.png"));
      }
      this.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
      this.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
      this.Properties.DefineProperty(8, (object) new Size(0, 22));
      this.Properties.DefineProperty(2, (object) true);
      this.Properties.DefineResettableProperty(18, (IQResettableValue) this.CreateItemConfiguration());
      this.DefaultItemConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedHandler;
      this.Properties.DefineResettableProperty(19, (IQResettableValue) new QRibbonLaunchBarItemConfiguration(QRibbonLaunchBarConfiguration.m_oDefaultCustomizeMask));
      this.CustomizeItemConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedHandler;
      this.Properties.DefineResettableProperty(20, (IQResettableValue) new QRibbonLaunchBarShowMoreItemConfiguration(QRibbonLaunchBarConfiguration.m_oDefaultShowMoreMask));
      this.ShowMoreItemConfiguration.Properties.SetProperty(6, (object) QPartAlignment.Far);
      this.ShowMoreItemConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedHandler;
      this.Properties.DefineProperty(21, (object) QTristateBool.Undefined);
      this.Properties.DefineProperty(22, (object) QRibbonLaunchBarResizeBehavior.ShowMoreItem);
    }

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QRibbonLaunchBarAppearance();

    protected override int GetRequestedCount() => 23;

    protected virtual QRibbonItemConfiguration CreateItemConfiguration() => new QRibbonItemConfiguration(QRibbonItemConfigurationType.RibbonLaunchBar);

    protected override string DefaultContentPartLayoutOrder => "";

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [Category("QAppearance")]
    [Description("Gets the configuration for QRibbonItems used on this bar.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(18)]
    public QRibbonItemConfiguration DefaultItemConfiguration => this.Properties.GetProperty(18) as QRibbonItemConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets the configuration for the customize item used on this bar.")]
    [QPropertyIndex(19)]
    public QRibbonLaunchBarItemConfiguration CustomizeItemConfiguration => this.Properties.GetProperty(19) as QRibbonLaunchBarItemConfiguration;

    [Description("Gets the configuration for the show more items item used on this bar.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(20)]
    public QRibbonLaunchBarShowMoreItemConfiguration ShowMoreItemConfiguration => this.Properties.GetProperty(20) as QRibbonLaunchBarShowMoreItemConfiguration;

    [QPropertyIndex(21)]
    [Description("Gets or sets whether the shape should be drawn. When this is undefined it is based on QRibbonLaunchBar.DrawShape")]
    [Category("QAppearance")]
    public QTristateBool DrawShape
    {
      get => (QTristateBool) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }

    [Description("Gets or sets the custom behavior that must be applied when the items doesn't fit. If this is turned off default resize behaviors like scrolling can be applied.")]
    [Category("QAppearance")]
    [QPropertyIndex(22)]
    public QRibbonLaunchBarResizeBehavior ResizeBehavior
    {
      get => (QRibbonLaunchBarResizeBehavior) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    public override IQPadding[] GetPaddings(IQPart part) => part is QRibbonLaunchBar qribbonLaunchBar && !qribbonLaunchBar.ShouldDrawShape ? QPartHelper.GetDefaultPaddings(part, this.Padding, (QShapeAppearance) null) : QPartHelper.GetDefaultPaddings(part, this.Padding, this.Appearance);

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
