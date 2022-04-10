// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonSubMenuWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QRibbonSubMenuWindow.bmp")]
  [ToolboxItem(true)]
  public class QRibbonSubMenuWindow : QCompositeWindow
  {
    private QCompositeGroup m_oCaption;
    private QCompositeSeparator m_oSeparator;
    private QCompositeGroup m_oItemArea;
    private QCompositeText m_oCaptionText;

    public QRibbonSubMenuWindow() => this.InternalConstruct();

    public QRibbonSubMenuWindow(
      IQPart parentPart,
      QPartCollection items,
      QColorScheme colorScheme,
      IWin32Window ownerWindow)
      : base(parentPart, items, colorScheme, ownerWindow)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
      this.MinimumClientSize = new Size(100, 100);
      this.m_oCaption = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oCaption.ItemName = "Caption";
      this.m_oCaption.Configuration = (QCompositeGroupConfiguration) this.CompositeConfiguration.CaptionConfiguration;
      this.m_oCaption.ColorScheme.CompositeGroupBackground1.ColorReference = "@RibbonMenuCaptionBackground1";
      this.m_oCaption.ColorScheme.CompositeGroupBackground2.ColorReference = "@RibbonMenuCaptionBackground2";
      this.m_oCaption.ColorScheme.CompositeGroupBorder.ColorReference = "@RibbonMenuCaptionBorder";
      this.m_oSeparator = new QCompositeSeparator(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oSeparator.ItemName = "Separator";
      this.m_oSeparator.Configuration = this.CompositeConfiguration.SeparatorConfiguration;
      this.m_oItemArea = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oItemArea.ItemName = "ItemArea";
      this.m_oItemArea.Configuration = this.CompositeConfiguration.ItemAreaConfiguration;
      this.m_oCaptionText = new QCompositeText();
      this.m_oCaptionText.ItemName = "CaptionText";
      this.m_oCaptionText.Configuration = this.CompositeConfiguration.CaptionConfiguration.TextConfiguration;
      this.m_oCaption.Items.SuspendChangeNotification();
      this.m_oCaption.Items.Add((IQPart) this.m_oCaptionText, false);
      this.m_oCaption.Items.ResumeChangeNotification(false);
      this.Composite.Parts.SuspendChangeNotification();
      this.Composite.Parts.Add((IQPart) this.m_oCaption, false);
      this.Composite.Parts.Add((IQPart) this.m_oSeparator, false);
      this.Composite.Parts.Add((IQPart) this.m_oItemArea, false);
      this.Composite.Parts.ResumeChangeNotification(false);
    }

    protected override QCompositeConfiguration CreateCompositeConfiguration() => (QCompositeConfiguration) new QRibbonSubMenuWindow.QRibbonSubMenuWindowCompositeConfiguration();

    [DefaultValue(typeof (Size), "100, 100")]
    public override Size MinimumClientSize
    {
      get => base.MinimumClientSize;
      set => base.MinimumClientSize = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Contains the Configuration for the QComposite.")]
    public QRibbonSubMenuWindow.QRibbonSubMenuWindowCompositeConfiguration CompositeConfiguration
    {
      get => base.CompositeConfiguration as QRibbonSubMenuWindow.QRibbonSubMenuWindowCompositeConfiguration;
      set => this.CompositeConfiguration = (QCompositeConfiguration) value;
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonMenuWindow)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the caption of the document area.")]
    [Localizable(true)]
    public string CaptionText
    {
      get => this.m_oCaptionText.Title;
      set => this.m_oCaptionText.Title = value;
    }

    public override QPartCollection Items => this.m_oItemArea.Items;

    [Browsable(false)]
    public QCompositeGroup Caption => this.m_oCaption;

    [Browsable(false)]
    public QCompositeGroup ItemArea => this.m_oItemArea;

    [Browsable(false)]
    public QCompositeSeparator Separator => this.m_oSeparator;

    [Browsable(false)]
    public QCompositeText CaptionTextItem => this.m_oCaptionText;

    public class QRibbonSubMenuWindowCompositeConfiguration : QCompositeOrderedConfiguration
    {
      private const string m_sDefaultContentLayoutOrder = "Caption, Separator, ItemArea";
      protected const int PropCaptionConfiguration = 29;
      protected const int PropSeparatorConfiguration = 30;
      protected const int PropItemAreaConfiguration = 31;
      protected new const int CurrentPropertyCount = 3;
      protected new const int TotalPropertyCount = 32;
      private EventHandler m_oChildObjectChangedHandler;

      public QRibbonSubMenuWindowCompositeConfiguration()
      {
        this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
        this.Properties.DefineProperty(17, (object) (QCompositeExpandBehavior.AutoExpand | QCompositeExpandBehavior.AutoChangeExpand | QCompositeExpandBehavior.ExpandOnNavigationKeys | QCompositeExpandBehavior.CloseOnNavigationKeys));
        this.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.Properties.DefineProperty(1, (object) new QPadding(0, 0, 0, 0));
        this.Properties.DefineResettableProperty(29, (IQResettableValue) new QRibbonSubMenuWindow.QRibbonSubMenuWindowCaptionConfiguration());
        this.CaptionConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
        this.CaptionConfiguration.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
        this.CaptionConfiguration.Properties.DefineProperty(8, (object) new Size(0, 16));
        this.CaptionConfiguration.Properties.DefineProperty(2, (object) true);
        this.CaptionConfiguration.Properties.DefineProperty(4, (object) true);
        this.CaptionConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.CaptionConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(30, (IQResettableValue) new QCompositeSeparatorConfiguration());
        this.SeparatorConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, -1, 0, 0));
        this.SeparatorConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(31, (IQResettableValue) new QCompositeGroupConfiguration());
        this.ItemAreaConfiguration.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.ItemAreaConfiguration.Properties.DefineProperty(16, (object) QCompositeItemLayout.Table);
        this.ItemAreaConfiguration.Properties.DefineProperty(1, (object) new QPadding(3, 3, 3, 3));
        this.ItemAreaConfiguration.Properties.DefineProperty(4, (object) true);
        this.ItemAreaConfiguration.Properties.DefineProperty(2, (object) true);
        this.ItemAreaConfiguration.Properties.DefineProperty(5, (object) true);
        this.ItemAreaConfiguration.Properties.DefineProperty(3, (object) true);
        this.ItemAreaConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.ItemAreaConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
      }

      protected override string DefaultContentPartLayoutOrder => "Caption, Separator, ItemArea";

      protected override int GetRequestedCount() => 32;

      [QPropertyIndex(29)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [Description("Contains the configuration of the caption")]
      public QRibbonSubMenuWindow.QRibbonSubMenuWindowCaptionConfiguration CaptionConfiguration => this.Properties.GetProperty(29) as QRibbonSubMenuWindow.QRibbonSubMenuWindowCaptionConfiguration;

      [QPropertyIndex(30)]
      [Description("Contains the configuration of the separator")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public QCompositeSeparatorConfiguration SeparatorConfiguration => this.Properties.GetProperty(30) as QCompositeSeparatorConfiguration;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [QPropertyIndex(31)]
      [Description("Contains the configuration of the item area")]
      public QCompositeGroupConfiguration ItemAreaConfiguration => this.Properties.GetProperty(31) as QCompositeGroupConfiguration;

      private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(e);
    }

    public class QRibbonSubMenuWindowCaptionConfiguration : QCompositeGroupConfiguration
    {
      protected const int PropTextConfiguration = 18;
      protected new const int CurrentPropertyCount = 1;
      protected new const int TotalPropertyCount = 19;
      private EventHandler m_oChildObjectChangedHandler;

      public QRibbonSubMenuWindowCaptionConfiguration()
      {
        this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
        this.Properties.DefineProperty(18, (object) new QCompositeTextConfiguration());
        this.TextConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
        this.TextConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Near);
        this.TextConfiguration.Properties.DefineProperty(1, (object) new QPadding(2, 2, 2, 2));
        this.TextConfiguration.Properties.DefineProperty(16, (object) new QFontDefinition((string) null, true, false, false, false, -1f));
        this.TextConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
      }

      protected override int GetRequestedCount() => 19;

      [Description("Contains the configuration of the text")]
      [QPropertyIndex(18)]
      public QCompositeTextConfiguration TextConfiguration => this.Properties.GetProperty(18) as QCompositeTextConfiguration;

      private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(e);
    }
  }
}
