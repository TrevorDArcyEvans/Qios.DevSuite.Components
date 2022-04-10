// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonMenuWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QRibbonMenuWindow.bmp")]
  public class QRibbonMenuWindow : QCompositeWindow, IQItemColorHost
  {
    private const string m_sDefaultDocumentCaption = "Recent Documents";
    private QCompositeGroup m_oHeader;
    private QCompositeGroup m_oContent;
    private QCompositeGroup m_oFooter;
    private QCompositeGroup m_oItemArea;
    private QCompositeSeparator m_oContentSeparator;
    private QCompositeGroup m_oDocumentArea;
    private QCompositeText m_oDocumentCaption;
    private QCompositeSeparator m_oDocumentAreaSeparator;
    private QCompositeGroup m_oDocuments;

    public QRibbonMenuWindow() => this.InternalConstruct();

    public QRibbonMenuWindow(
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
      this.Composite.ColorHost = (IQItemColorHost) this;
      this.m_oHeader = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oHeader.ColorScheme.CompositeGroupBackground1.ColorReference = "@RibbonMenuHeaderFooter1";
      this.m_oHeader.ColorScheme.CompositeGroupBackground2.ColorReference = "@RibbonMenuHeaderFooter2";
      this.m_oHeader.ColorScheme.CompositeGroupBorder.ColorReference = "@RibbonMenuHeaderFooterBorder";
      this.m_oHeader.ItemName = "Header";
      this.m_oHeader.Configuration = this.CompositeConfiguration.HeaderConfiguration;
      this.m_oContent = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oContent.ItemName = "Content";
      this.m_oContent.ColorScheme.CompositeGroupBackground1.ColorReference = "@RibbonMenuContentAreaBackground1";
      this.m_oContent.ColorScheme.CompositeGroupBackground2.ColorReference = "@RibbonMenuContentAreaBackground2";
      this.m_oContent.ColorScheme.CompositeGroupBorder.ColorReference = "@RibbonMenuContentAreaBorder";
      this.m_oContent.Configuration = (QCompositeGroupConfiguration) this.CompositeConfiguration.ContentConfiguration;
      this.m_oFooter = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oFooter.ColorScheme.CompositeGroupBackground1.ColorReference = "@RibbonMenuHeaderFooter1";
      this.m_oFooter.ColorScheme.CompositeGroupBackground2.ColorReference = "@RibbonMenuHeaderFooter2";
      this.m_oFooter.ColorScheme.CompositeGroupBorder.ColorReference = "@RibbonMenuHeaderFooterBorder";
      this.m_oFooter.ItemName = "Footer";
      this.m_oFooter.Configuration = this.CompositeConfiguration.FooterConfiguration;
      this.m_oItemArea = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oItemArea.ItemName = "ItemArea";
      this.m_oItemArea.Configuration = this.CompositeConfiguration.ContentConfiguration.ItemAreaConfiguration;
      this.m_oContentSeparator = new QCompositeSeparator(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oContentSeparator.ItemName = "Separator";
      this.m_oContentSeparator.Configuration = this.CompositeConfiguration.ContentConfiguration.SeparatorConfiguration;
      this.m_oDocumentArea = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oDocumentArea.ItemName = "DocumentArea";
      this.m_oDocumentArea.ColorScheme.CompositeGroupBackground1.ColorReference = "@RibbonMenuDocumentAreaBackground1";
      this.m_oDocumentArea.ColorScheme.CompositeGroupBackground2.ColorReference = "@RibbonMenuDocumentAreaBackground2";
      this.m_oDocumentArea.ColorScheme.CompositeGroupBorder.ColorReference = "@RibbonMenuDocumentAreaBorder";
      this.m_oDocumentArea.Configuration = (QCompositeGroupConfiguration) this.CompositeConfiguration.ContentConfiguration.DocumentAreaConfiguration;
      this.m_oDocumentCaption = new QCompositeText(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oDocumentCaption.Title = "Recent Documents";
      this.m_oDocumentCaption.ItemName = "Caption";
      this.m_oDocumentCaption.Configuration = this.CompositeConfiguration.ContentConfiguration.DocumentAreaConfiguration.CaptionConfiguration;
      this.m_oDocumentAreaSeparator = new QCompositeSeparator(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oDocumentAreaSeparator.ItemName = "Separator";
      this.m_oDocumentAreaSeparator.Configuration = this.CompositeConfiguration.ContentConfiguration.DocumentAreaConfiguration.SeparatorConfiguration;
      this.m_oDocuments = new QCompositeGroup(QCompositeItemCreationOptions.CreateItemsCollection | QCompositeItemCreationOptions.CreateChildItemsCollection | QCompositeItemCreationOptions.CreateColorScheme);
      this.m_oDocuments.ItemName = "Documents";
      this.m_oDocuments.Configuration = this.CompositeConfiguration.ContentConfiguration.DocumentAreaConfiguration.DocumentsConfiguration;
      this.m_oDocumentArea.Parts.SuspendChangeNotification();
      this.m_oDocumentArea.Parts.Add((IQPart) this.m_oDocumentCaption, false);
      this.m_oDocumentArea.Parts.Add((IQPart) this.m_oDocumentAreaSeparator, false);
      this.m_oDocumentArea.Parts.Add((IQPart) this.m_oDocuments, false);
      this.m_oDocumentArea.Parts.ResumeChangeNotification(false);
      this.m_oContent.Parts.SuspendChangeNotification();
      this.m_oContent.Parts.Add((IQPart) this.m_oItemArea, false);
      this.m_oContent.Parts.Add((IQPart) this.m_oContentSeparator, false);
      this.m_oContent.Parts.Add((IQPart) this.m_oDocumentArea, false);
      this.m_oContent.Parts.ResumeChangeNotification(false);
      this.Composite.Parts.SuspendChangeNotification();
      this.Composite.Parts.Add((IQPart) this.m_oHeader, false);
      this.Composite.Parts.Add((IQPart) this.m_oContent, false);
      this.Composite.Parts.Add((IQPart) this.m_oFooter, false);
      this.Composite.Parts.ResumeChangeNotification(false);
    }

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonMenuWindow)]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [Description("The ColorScheme that is used.")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    protected override QCompositeConfiguration CreateCompositeConfiguration() => (QCompositeConfiguration) new QRibbonMenuWindow.QRibbonMenuWindowCompositeConfiguration();

    protected override QFloatingWindowConfiguration CreateConfigurationInstance() => (QFloatingWindowConfiguration) new QRibbonMenuWindow.QRibbonMenuWindowConfiguration();

    [DefaultValue(typeof (Size), "100, 100")]
    public override Size MinimumClientSize
    {
      get => base.MinimumClientSize;
      set => base.MinimumClientSize = value;
    }

    [Description("Contains the Configuration for the QComposite.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QRibbonMenuWindow.QRibbonMenuWindowCompositeConfiguration CompositeConfiguration
    {
      get => base.CompositeConfiguration as QRibbonMenuWindow.QRibbonMenuWindowCompositeConfiguration;
      set => this.CompositeConfiguration = (QCompositeConfiguration) value;
    }

    public QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return destinationObject == this.Composite ? new QColorSet(this.BackColor, this.BackColor2, this.BorderColor) : this.Composite.GetItemColorSet(destinationObject, state, additionalProperties);
    }

    [DefaultValue("Recent Documents")]
    [Category("QAppearance")]
    [Localizable(true)]
    [Description("Gets or sets the caption of the document area.")]
    public string DocumentCaptionText
    {
      get => this.m_oDocumentCaption.Title;
      set => this.m_oDocumentCaption.Title = value;
    }

    public override QPartCollection Items => this.m_oItemArea.Items;

    [Description("Gets the collection of items presented on the document area.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    [Category("QBehavior")]
    public QPartCollection DocumentItems => this.m_oDocuments.Items;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    [Category("QBehavior")]
    [Description("Gets the collection of items presented on the document area.")]
    [Browsable(true)]
    public QPartCollection FooterItems => this.m_oFooter.Items;

    [Description("Gets the collection of items presented on the document area.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QBehavior")]
    [Browsable(true)]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    public QPartCollection HeaderItems => this.m_oHeader.Items;

    protected override IList AssociatedComponents
    {
      get
      {
        ArrayList associatedComponents = new ArrayList();
        this.Items.CopyTo((IList) associatedComponents);
        this.DocumentItems.CopyTo((IList) associatedComponents);
        this.FooterItems.CopyTo((IList) associatedComponents);
        this.HeaderItems.CopyTo((IList) associatedComponents);
        return (IList) associatedComponents;
      }
    }

    [Browsable(false)]
    public QCompositeGroup Header => this.m_oHeader;

    [Browsable(false)]
    public QCompositeGroup Content => this.m_oContent;

    [Browsable(false)]
    public QCompositeGroup Footer => this.m_oFooter;

    [Browsable(false)]
    public QCompositeGroup ItemArea => this.m_oItemArea;

    [Browsable(false)]
    public QCompositeSeparator ContentSeparator => this.m_oContentSeparator;

    [Browsable(false)]
    public QCompositeGroup DocumentArea => this.m_oDocumentArea;

    [Browsable(false)]
    public QCompositeText DocumentCaption => this.m_oDocumentCaption;

    [Browsable(false)]
    public QCompositeSeparator DocumentAreaSeparator => this.m_oDocumentAreaSeparator;

    [Browsable(false)]
    public QCompositeGroup Documents => this.m_oDocuments;

    protected override string BackColorPropertyName => "RibbonMenuBackground1";

    protected override string BackColor2PropertyName => "RibbonMenuBackground2";

    protected override string BorderColorPropertyName => "RibbonMenuBorder";

    private bool IsItemAreaChild(IQPart part)
    {
      for (; part != null && !(part is IQManagedLayoutParent); part = part.ParentPart)
      {
        if (part.ParentPart == this.m_oItemArea)
          return true;
      }
      return false;
    }

    protected override void OnItemExpanding(QCompositeExpandingCancelEventArgs e)
    {
      if (this.CompositeConfiguration.ShowSubMenusAboveDocumentArea && QPartHelper.IsPartHierarchyVisible((IQPart) this.m_oDocumentArea, QPartVisibilitySelectionTypes.IncludeAll) && this.IsItemAreaChild((IQPart) e.Item))
      {
        QCompositeItemBase qcompositeItemBase = e.Item;
        e.Bounds = this.CompositeConfiguration.ShowSubMenusMargin.InflateRectangleWithMargin(this.RectangleToScreen(this.m_oDocumentArea.CalculatedProperties.Bounds), false, true);
      }
      base.OnItemExpanding(e);
    }

    public class QRibbonMenuWindowConfiguration : QCompositeWindowConfiguration
    {
      public QRibbonMenuWindowConfiguration() => this.Properties.DefineProperty(7, (object) true);
    }

    public class QRibbonMenuWindowCompositeConfiguration : QCompositeOrderedConfiguration
    {
      private const string m_sDefaultContentLayoutOrder = "Header, Content, Footer";
      protected const int PropHeaderConfiguration = 29;
      protected const int PropContentConfiguration = 30;
      protected const int PropFooterConfiguration = 31;
      protected const int PropShowSubMenusAboveDocumentArea = 32;
      protected const int PropShowSubMenusMargin = 33;
      protected new const int CurrentPropertyCount = 5;
      protected new const int TotalPropertyCount = 34;
      private EventHandler m_oChildObjectChangedHandler;

      public QRibbonMenuWindowCompositeConfiguration()
      {
        this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
        this.Properties.DefineProperty(17, (object) (QCompositeExpandBehavior.AutoExpand | QCompositeExpandBehavior.AutoChangeExpand | QCompositeExpandBehavior.ExpandOnNavigationKeys | QCompositeExpandBehavior.CloseOnNavigationKeys));
        this.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.Properties.DefineProperty(1, (object) new QPadding(3, 3, 3, 3));
        this.Properties.DefineResettableProperty(29, (IQResettableValue) new QCompositeGroupConfiguration());
        this.HeaderConfiguration.Properties.DefineProperty(0, (object) new QMargin(-3, 0, 0, -3));
        this.HeaderConfiguration.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
        this.HeaderConfiguration.Properties.DefineProperty(8, (object) new Size(0, 16));
        this.HeaderConfiguration.Properties.DefineProperty(2, (object) true);
        this.HeaderConfiguration.Properties.DefineProperty(4, (object) true);
        this.HeaderConfiguration.Appearance.Properties.DefineProperty(1, (object) QColorStyle.Metallic);
        this.HeaderConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.HeaderConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(30, (IQResettableValue) new QRibbonMenuWindow.QRibbonMenuWindowContentConfiguration());
        this.ContentConfiguration.Properties.DefineProperty(4, (object) true);
        this.ContentConfiguration.Properties.DefineProperty(2, (object) true);
        this.ContentConfiguration.Properties.DefineProperty(5, (object) true);
        this.ContentConfiguration.Properties.DefineProperty(3, (object) true);
        this.ContentConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.ContentConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(31, (IQResettableValue) new QCompositeGroupConfiguration());
        this.FooterConfiguration.Properties.DefineProperty(0, (object) new QMargin(-3, 0, 0, -3));
        this.FooterConfiguration.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
        this.FooterConfiguration.Properties.DefineProperty(8, (object) new Size(0, 16));
        this.FooterConfiguration.Properties.DefineProperty(2, (object) true);
        this.FooterConfiguration.Properties.DefineProperty(4, (object) true);
        this.FooterConfiguration.Appearance.Properties.DefineProperty(1, (object) QColorStyle.Metallic);
        this.FooterConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.FooterConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineProperty(32, (object) true);
        this.Properties.DefineProperty(33, (object) new QMargin(1, 1, 3, 3));
      }

      protected override string DefaultContentPartLayoutOrder => "Header, Content, Footer";

      protected override int GetRequestedCount() => 34;

      [QPropertyIndex(32)]
      [Description("Gets or sets whether items of the ItemArea should show their sub menu's above the Document area.")]
      public bool ShowSubMenusAboveDocumentArea
      {
        get => (bool) this.Properties.GetPropertyAsValueType(32);
        set => this.Properties.SetProperty(32, (object) value);
      }

      [Description("Gets or sets the margin to use when ShowSubMenusAboveDocumentArea is set.")]
      [QPropertyIndex(33)]
      public QMargin ShowSubMenusMargin
      {
        get => (QMargin) this.Properties.GetPropertyAsValueType(33);
        set => this.Properties.SetProperty(33, (object) value);
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [Description("Contains the configuration of the header")]
      [QPropertyIndex(29)]
      public QCompositeGroupConfiguration HeaderConfiguration => this.Properties.GetProperty(29) as QCompositeGroupConfiguration;

      [Description("Contains the configuration of the header")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [QPropertyIndex(30)]
      public QRibbonMenuWindow.QRibbonMenuWindowContentConfiguration ContentConfiguration => this.Properties.GetProperty(30) as QRibbonMenuWindow.QRibbonMenuWindowContentConfiguration;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [Description("Contains the configuration of the footer")]
      [QPropertyIndex(31)]
      public QCompositeGroupConfiguration FooterConfiguration => this.Properties.GetProperty(31) as QCompositeGroupConfiguration;

      private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(e);
    }

    public class QRibbonMenuWindowContentConfiguration : QCompositeGroupOrderedConfiguration
    {
      private const string m_sDefaultContentLayoutOrder = "ItemArea, Separator, DocumentArea";
      protected const int PropItemAreaConfiguration = 18;
      protected const int PropSeparatorConfiguration = 19;
      protected const int PropDocumentAreaConfiguration = 20;
      protected new const int CurrentPropertyCount = 3;
      protected new const int TotalPropertyCount = 21;
      private EventHandler m_oChildObjectChangedHandler;

      public QRibbonMenuWindowContentConfiguration()
      {
        this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
        this.Properties.DefineResettableProperty(18, (IQResettableValue) new QCompositeGroupConfiguration());
        this.ItemAreaConfiguration.ScrollConfiguration.Properties.DefineProperty(13, (object) QCompositeScrollVisibility.Automatic);
        this.ItemAreaConfiguration.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.ItemAreaConfiguration.Properties.DefineProperty(16, (object) QCompositeItemLayout.Table);
        this.ItemAreaConfiguration.Properties.DefineProperty(8, (object) new Size(50, 0));
        this.ItemAreaConfiguration.Properties.DefineProperty(5, (object) true);
        this.ItemAreaConfiguration.Properties.DefineProperty(3, (object) true);
        this.ItemAreaConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.ItemAreaConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(19, (IQResettableValue) new QCompositeSeparatorConfiguration());
        this.SeparatorConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, -1, -1));
        this.SeparatorConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(20, (IQResettableValue) new QRibbonMenuWindow.QRibbonMenuWindowDocumentAreaConfiguration());
        this.DocumentAreaConfiguration.ScrollConfiguration.Properties.DefineProperty(13, (object) QCompositeScrollVisibility.Automatic);
        this.DocumentAreaConfiguration.Properties.DefineProperty(8, (object) new Size(50, 0));
        this.DocumentAreaConfiguration.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.DocumentAreaConfiguration.Properties.DefineProperty(2, (object) true);
        this.DocumentAreaConfiguration.Properties.DefineProperty(3, (object) true);
        this.DocumentAreaConfiguration.Properties.DefineProperty(4, (object) true);
        this.DocumentAreaConfiguration.Properties.DefineProperty(5, (object) true);
        this.DocumentAreaConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.DocumentAreaConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
      }

      protected override string DefaultContentPartLayoutOrder => "ItemArea, Separator, DocumentArea";

      [Description("Contains the configuration of the item area")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [QPropertyIndex(18)]
      public QCompositeGroupConfiguration ItemAreaConfiguration => this.Properties.GetProperty(18) as QCompositeGroupConfiguration;

      [Description("Contains the configuration of the separator")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [QPropertyIndex(19)]
      public QCompositeSeparatorConfiguration SeparatorConfiguration => this.Properties.GetProperty(19) as QCompositeSeparatorConfiguration;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [Description("Contains the configuration of the document area")]
      [QPropertyIndex(20)]
      public QRibbonMenuWindow.QRibbonMenuWindowDocumentAreaConfiguration DocumentAreaConfiguration => this.Properties.GetProperty(20) as QRibbonMenuWindow.QRibbonMenuWindowDocumentAreaConfiguration;

      protected override int GetRequestedCount() => 21;

      private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(e);
    }

    public class QRibbonMenuWindowDocumentAreaConfiguration : QCompositeGroupOrderedConfiguration
    {
      private const string m_sDefaultContentLayoutOrder = "Caption, Separator, Documents";
      protected const int PropCaptionConfiguration = 18;
      protected const int PropSeparatorConfiguration = 19;
      protected const int PropDocumentsConfiguration = 20;
      protected new const int CurrentPropertyCount = 3;
      protected new const int TotalPropertyCount = 21;
      private EventHandler m_oChildObjectChangedHandler;

      public QRibbonMenuWindowDocumentAreaConfiguration()
      {
        this.m_oChildObjectChangedHandler = new EventHandler(this.ChildObject_ObjectChanged);
        this.Properties.DefineResettableProperty(18, (IQResettableValue) new QCompositeTextConfiguration());
        this.CaptionConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
        this.CaptionConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Near);
        this.CaptionConfiguration.Properties.DefineProperty(1, (object) new QPadding(2, 2, 2, 2));
        this.CaptionConfiguration.Properties.DefineProperty(16, (object) new QFontDefinition((string) null, true, false, false, false, -1f));
        this.CaptionConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(19, (IQResettableValue) new QCompositeSeparatorConfiguration());
        this.SeparatorConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
        this.Properties.DefineResettableProperty(20, (IQResettableValue) new QCompositeGroupConfiguration());
        this.DocumentsConfiguration.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.DocumentsConfiguration.Properties.DefineProperty(16, (object) QCompositeItemLayout.Table);
        this.DocumentsConfiguration.Properties.DefineProperty(2, (object) true);
        this.DocumentsConfiguration.Properties.DefineProperty(3, (object) true);
        this.DocumentsConfiguration.Properties.DefineProperty(4, (object) true);
        this.DocumentsConfiguration.Properties.DefineProperty(5, (object) true);
        this.DocumentsConfiguration.Appearance.Properties.DefineProperty(16, (object) QShape.BaseShapes[QBaseShapeType.SquareContent]);
        this.DocumentsConfiguration.ConfigurationChanged += this.m_oChildObjectChangedHandler;
      }

      protected override string DefaultContentPartLayoutOrder => "Caption, Separator, Documents";

      [QPropertyIndex(18)]
      [Description("Contains the configuration of the item area")]
      public QCompositeTextConfiguration CaptionConfiguration => this.Properties.GetProperty(18) as QCompositeTextConfiguration;

      [Description("Contains the configuration of the separator")]
      [QPropertyIndex(19)]
      public QCompositeSeparatorConfiguration SeparatorConfiguration => this.Properties.GetProperty(19) as QCompositeSeparatorConfiguration;

      [QPropertyIndex(20)]
      [Description("Contains the configuration of the documents")]
      public QCompositeGroupConfiguration DocumentsConfiguration => this.Properties.GetProperty(20) as QCompositeGroupConfiguration;

      protected override int GetRequestedCount() => 21;

      private void ChildObject_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(e);
    }
  }
}
