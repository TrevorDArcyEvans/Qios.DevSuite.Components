// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeLargeMenuItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeLargeMenuItem : QCompositeMenuItem
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QCompositeText m_oDescription;

    protected QCompositeLargeMenuItem(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
    }

    public QCompositeLargeMenuItem()
    {
    }

    protected override void SecureContentPart(
      QCompositeMenuItemContentType contentType,
      bool sortParts)
    {
      if (this.CurrentContentPart != null)
        return;
      this.SecureTitlePart();
      this.TitlePart.ItemName = "Title";
      this.SecureControlPart();
      this.ControlPart.ItemName = "Control";
      QPart part = new QPart("Content", false, new IQPart[0]);
      part.Properties = (IQPartSharedProperties) this.Configuration.ContentConfiguration;
      this.m_oDescription = new QCompositeText(QCompositeItemCreationOptions.None);
      this.m_oDescription.Configuration = this.Configuration.ContentConfiguration.DescriptionConfiguration;
      this.m_oDescription.ItemName = "Description";
      part.Parts.SuspendChangeNotification();
      part.Parts.Add((IQPart) this.TitlePart, false);
      part.Parts.Add((IQPart) this.m_oDescription, false);
      part.Parts.Add((IQPart) this.ControlPart, false);
      part.Parts.ResumeChangeNotification(false);
      this.Items.Add((IQPart) part, false);
      this.PutCurrentContentPart((IQPart) part);
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeLargeMenuItem.QCompositeLargeMenuItemConfiguration();

    private void ConfigureSharedItemsProperties()
    {
      for (int index = 0; index < this.CurrentContentPart.Parts.Count; ++index)
      {
        if (this.CurrentContentPart.Parts[index] is QCompositeItemBase part)
          part.ColorHost = (IQItemColorHost) this;
      }
    }

    public override object Clone()
    {
      QCompositeLargeMenuItem qcompositeLargeMenuItem = base.Clone() as QCompositeLargeMenuItem;
      qcompositeLargeMenuItem.PutTitlePart(qcompositeLargeMenuItem.CurrentContentPart.Parts["Title"] as QCompositeText);
      if (qcompositeLargeMenuItem.TitlePart != null)
        qcompositeLargeMenuItem.TitlePart.Configuration = qcompositeLargeMenuItem.Configuration.ContentConfiguration.TitleConfiguration;
      qcompositeLargeMenuItem.PutControlPart(qcompositeLargeMenuItem.CurrentContentPart.Parts["Control"] as QCompositeItemControl);
      if (qcompositeLargeMenuItem.ControlPart != null)
        qcompositeLargeMenuItem.ControlPart.Configuration = qcompositeLargeMenuItem.Configuration.ContentConfiguration.ControlConfiguration;
      qcompositeLargeMenuItem.PutDescriptionPart(qcompositeLargeMenuItem.CurrentContentPart.Parts["Description"] as QCompositeText);
      if (qcompositeLargeMenuItem.DescriptionPart != null)
        qcompositeLargeMenuItem.DescriptionPart.Configuration = qcompositeLargeMenuItem.Configuration.ContentConfiguration.DescriptionConfiguration;
      this.ConfigureSharedItemsProperties();
      return (object) qcompositeLargeMenuItem;
    }

    [Localizable(true)]
    [System.ComponentModel.Description("Gets or sets the description of the item")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string Description
    {
      get => this.m_oDescription.Title;
      set => this.m_oDescription.Title = value;
    }

    [System.ComponentModel.Description("Contains the Configuration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QCompositeLargeMenuItem.QCompositeLargeMenuItemConfiguration Configuration
    {
      get => base.Configuration as QCompositeLargeMenuItem.QCompositeLargeMenuItemConfiguration;
      set => this.Configuration = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QCompositeText DescriptionPart => this.m_oDescription;

    internal void PutDescriptionPart(QCompositeText value) => this.m_oDescription = value;

    protected override void SetChildPartsVisibility(QPartLayoutStage layoutStage)
    {
      base.SetChildPartsVisibility(layoutStage);
      ((QPart) this.CurrentContentPart).PutVisible(this.TitlePart.IsVisible(QPartVisibilitySelectionTypes.IncludeAll) || this.DescriptionPart.IsVisible(QPartVisibilitySelectionTypes.IncludeAll) || this.ControlPart.IsVisible(QPartVisibilitySelectionTypes.IncludeAll));
    }

    public class QCompositeLargeMenuItemConfiguration : QCompositeMenuItemConfiguration
    {
      protected const int PropContentConfiguration = 31;
      protected new const int CurrentPropertyCount = 1;
      protected new const int TotalPropertyCount = 32;
      private string m_sDefaultContentLayoutOrder = "Icon, Content, Shortcut, DropDownSplit, DropDownButton";
      private EventHandler m_oChildObjectsChangedEventHandler;

      public QCompositeLargeMenuItemConfiguration()
      {
        this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
        this.Properties.DefineResettableProperty(31, (IQResettableValue) new QCompositeLargeMenuItem.QCompositeLargeMenuItemContentConfiguration());
        this.ContentConfiguration.Properties.DefineProperty(4, (object) true);
        this.ContentConfiguration.Properties.DefineProperty(2, (object) true);
        this.ContentConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.ContentConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.ContentConfiguration.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
        this.ContentConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
        this.Properties.DefineProperty(1, (object) new QPadding(3, 3, 3, 3));
        this.Properties.DefineProperty(4, (object) true);
        this.Properties.DefineProperty(2, (object) true);
        this.SuspendChangeNotification();
        this.IconConfiguration.Properties.DefineProperty(15, (object) new Size(32, 32));
        this.ResumeChangeNotification(false);
      }

      protected override int GetRequestedCount() => 32;

      protected override string DefaultContentPartLayoutOrder => this.m_sDefaultContentLayoutOrder;

      protected override QCompositeTextConfiguration CreateTitleConfiguration() => (QCompositeTextConfiguration) null;

      protected override QCompositeItemControlConfiguration CreateControlConfiguration() => (QCompositeItemControlConfiguration) null;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      [Browsable(false)]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override QCompositeTextConfiguration TitleConfiguration => !this.Properties.IsDefined(31) ? (QCompositeTextConfiguration) null : this.ContentConfiguration.TitleConfiguration;

      [EditorBrowsable(EditorBrowsableState.Never)]
      [Browsable(false)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public override QCompositeItemControlConfiguration ControlConfiguration => !this.Properties.IsDefined(31) ? (QCompositeItemControlConfiguration) null : this.ContentConfiguration.ControlConfiguration;

      [Category("QAppearance")]
      [System.ComponentModel.Description("Gets the configuration of the content.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [QPropertyIndex(31)]
      public QCompositeLargeMenuItem.QCompositeLargeMenuItemContentConfiguration ContentConfiguration => this.Properties.GetProperty(31) as QCompositeLargeMenuItem.QCompositeLargeMenuItemContentConfiguration;

      private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
    }

    public class QCompositeLargeMenuItemContentConfiguration : QGroupPartConfiguration
    {
      protected const int PropTitleConfiguration = 15;
      protected const int PropDescriptionConfiguration = 16;
      protected const int PropControlConfiguration = 17;
      protected new const int CurrentPropertyCount = 3;
      protected new const int TotalPropertyCount = 18;
      private string m_sDefaultContentLayoutOrder = "Title, Description, Control";
      private EventHandler m_oChildObjectsChangedEventHandler;

      public QCompositeLargeMenuItemContentConfiguration()
      {
        this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
        this.Properties.DefineResettableProperty(15, (IQResettableValue) new QCompositeTextConfiguration());
        this.TitleConfiguration.Properties.DefineProperty(4, (object) true);
        this.TitleConfiguration.Properties.DefineProperty(2, (object) true);
        this.TitleConfiguration.Properties.DefineProperty(16, (object) new QFontDefinition((string) null, true, false, false, false, -1f));
        this.TitleConfiguration.Properties.DefineProperty(17, (object) new QFontDefinition((string) null, true, false, false, false, -1f));
        this.TitleConfiguration.Properties.DefineProperty(18, (object) new QFontDefinition((string) null, true, false, false, false, -1f));
        this.TitleConfiguration.Properties.DefineProperty(19, (object) new QFontDefinition((string) null, true, false, false, false, -1f));
        this.TitleConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
        this.TitleConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
        this.Properties.DefineResettableProperty(16, (IQResettableValue) new QCompositeTextConfiguration());
        this.DescriptionConfiguration.Properties.DefineProperty(4, (object) true);
        this.DescriptionConfiguration.Properties.DefineProperty(2, (object) true);
        this.DescriptionConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
        this.DescriptionConfiguration.Properties.DefineProperty(20, (object) true);
        this.DescriptionConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
        this.Properties.DefineResettableProperty(17, (IQResettableValue) new QCompositeItemControlConfiguration());
        this.ControlConfiguration.Properties.DefineProperty(4, (object) true);
        this.ControlConfiguration.Properties.DefineProperty(2, (object) true);
        this.ControlConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Near);
        this.ControlConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      }

      protected override string DefaultContentPartLayoutOrder => this.m_sDefaultContentLayoutOrder;

      protected override int GetRequestedCount() => 18;

      [System.ComponentModel.Description("Gets the configuration of the title.")]
      [Category("QAppearance")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [QPropertyIndex(15)]
      public QCompositeTextConfiguration TitleConfiguration => this.Properties.GetProperty(15) as QCompositeTextConfiguration;

      [QPropertyIndex(16)]
      [System.ComponentModel.Description("Gets the configuration of the title.")]
      [Category("QAppearance")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public QCompositeTextConfiguration DescriptionConfiguration => this.Properties.GetProperty(16) as QCompositeTextConfiguration;

      [QPropertyIndex(17)]
      [System.ComponentModel.Description("Gets the configuration of the Control.")]
      [Category("QAppearance")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public QCompositeItemControlConfiguration ControlConfiguration => this.Properties.GetProperty(17) as QCompositeItemControlConfiguration;

      private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
    }
  }
}
