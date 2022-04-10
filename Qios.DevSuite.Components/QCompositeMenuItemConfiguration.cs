// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenuItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QCompositeMenuItemConfiguration : QCompositeItemConfiguration
  {
    protected const int PropIconConfiguration = 23;
    protected const int PropTitleConfiguration = 24;
    protected const int PropControlConfiguration = 25;
    protected const int PropShortcutConfiguration = 26;
    protected const int PropDropDownConfiguration = 27;
    protected const int PropDropDownSplitConfiguration = 28;
    protected const int PropDropDownSeparated = 29;
    protected const int PropCheckBehaviour = 30;
    protected new const int CurrentPropertyCount = 8;
    protected new const int TotalPropertyCount = 31;
    private static Image m_oDefaultDropDownMask;
    private static Image m_oDefaultDropDownSplitMask;
    private string m_sDefaultContentLayoutOrder = "Icon, Content, Shortcut, DropDownSplit, DropDownButton";
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QCompositeMenuItemConfiguration()
    {
      if (QCompositeMenuItemConfiguration.m_oDefaultDropDownMask == null)
      {
        QCompositeMenuItemConfiguration.m_oDefaultDropDownMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.SmallArrowRightMask.png"));
        QCompositeMenuItemConfiguration.m_oDefaultDropDownSplitMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonDropDownSplitMask.png"));
      }
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(17, (object) QTristateBool.True);
      this.Properties.DefineProperty(18, (object) QTristateBool.True);
      this.Properties.DefineProperty(8, (object) new Size(22, 22));
      this.Properties.DefineProperty(29, (object) false);
      this.Properties.DefineProperty(30, (object) QCompositeMenuItemCheckBehaviour.CheckIcon);
      this.Properties.DefineResettableProperty(23, (IQResettableValue) this.CreateIconConfiguration());
      if (this.IconConfiguration != null)
      {
        this.IconConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.IconConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.IconConfiguration.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
        this.IconConfiguration.Properties.DefineProperty(1, (object) new QPadding(1, 1, 1, 1));
        this.IconConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      }
      this.Properties.DefineResettableProperty(24, (IQResettableValue) this.CreateTitleConfiguration());
      if (this.TitleConfiguration != null)
      {
        this.TitleConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.TitleConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.TitleConfiguration.Properties.DefineProperty(0, (object) new QMargin(3, 2, 2, 3));
        this.TitleConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      }
      this.Properties.DefineResettableProperty(25, (IQResettableValue) this.CreateControlConfiguration());
      if (this.ControlConfiguration != null)
      {
        this.ControlConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.ControlConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.ControlConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      }
      this.Properties.DefineResettableProperty(26, (IQResettableValue) this.CreateShortcutConfiguration());
      if (this.ShortcutConfiguration != null)
      {
        this.ShortcutConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.ShortcutConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.ShortcutConfiguration.Properties.DefineProperty(0, (object) new QMargin(3, 2, 2, 3));
        this.ShortcutConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      }
      this.Properties.DefineResettableProperty(27, (IQResettableValue) this.CreateDropDownConfiguration());
      if (this.DropDownButtonConfiguration != null)
      {
        this.DropDownButtonConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
        this.DropDownButtonConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
        this.DropDownButtonConfiguration.Properties.DefineProperty(0, (object) new QMargin(3, 2, 2, 3));
        this.DropDownButtonConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      }
      this.Properties.DefineResettableProperty(28, (IQResettableValue) this.CreateDropDownSplitConfiguration());
      if (this.DropDownSplitConfiguration == null)
        return;
      this.DropDownSplitConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.DropDownSplitConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.DropDownSplitConfiguration.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.DropDownSplitConfiguration.Properties.DefineProperty(6, (object) QPartAlignment.Centered);
      this.DropDownSplitConfiguration.Properties.DefineProperty(7, (object) QPartAlignment.Centered);
      this.DropDownSplitConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected override int GetRequestedCount() => 31;

    protected virtual QCompositeIconConfiguration CreateIconConfiguration() => new QCompositeIconConfiguration();

    protected virtual QCompositeTextConfiguration CreateTitleConfiguration() => new QCompositeTextConfiguration();

    protected virtual QCompositeItemControlConfiguration CreateControlConfiguration() => new QCompositeItemControlConfiguration();

    protected virtual QCompositeTextConfiguration CreateShortcutConfiguration() => new QCompositeTextConfiguration();

    protected virtual QCompositeMaskConfiguration CreateDropDownConfiguration() => new QCompositeMaskConfiguration(QCompositeMenuItemConfiguration.m_oDefaultDropDownMask);

    protected virtual QCompositeSeparatorConfiguration CreateDropDownSplitConfiguration() => new QCompositeSeparatorConfiguration(QCompositeMenuItemConfiguration.m_oDefaultDropDownSplitMask);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public override string ContentLayoutOrder
    {
      get => base.ContentLayoutOrder;
      set => base.ContentLayoutOrder = value;
    }

    [QPropertyIndex(30)]
    [Description("Gets or sets the behaviour for checking items")]
    [Category("QAppearance")]
    public QCompositeMenuItemCheckBehaviour CheckBehaviour
    {
      get => (QCompositeMenuItemCheckBehaviour) this.Properties.GetPropertyAsValueType(30);
      set => this.Properties.SetProperty(30, (object) value);
    }

    [Description("Gets or sets the dropdown part has a seperate hot and pressed state")]
    [QPropertyIndex(29)]
    [Category("QAppearance")]
    public bool DropDownSeparated
    {
      get => (bool) this.Properties.GetPropertyAsValueType(29);
      set => this.Properties.SetProperty(29, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets the configuration of the icon.")]
    [QPropertyIndex(23)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeIconConfiguration IconConfiguration => this.Properties.GetProperty(23) as QCompositeIconConfiguration;

    [Description("Gets the configuration of the title.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(24)]
    [Category("QAppearance")]
    public virtual QCompositeTextConfiguration TitleConfiguration => this.Properties.GetProperty(24) as QCompositeTextConfiguration;

    [Description("Gets the configuration of the Control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(25)]
    [Category("QAppearance")]
    public virtual QCompositeItemControlConfiguration ControlConfiguration => this.Properties.GetProperty(25) as QCompositeItemControlConfiguration;

    [Category("QAppearance")]
    [Description("Gets the configuration of the shortcut.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(26)]
    public QCompositeTextConfiguration ShortcutConfiguration => this.Properties.GetProperty(26) as QCompositeTextConfiguration;

    [Description("Gets the configuration of the drop down button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(27)]
    [Category("QAppearance")]
    public QCompositeMaskConfiguration DropDownButtonConfiguration => this.Properties.GetProperty(27) as QCompositeMaskConfiguration;

    [Category("QAppearance")]
    [QPropertyIndex(28)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the drop down separator.")]
    public QCompositeSeparatorConfiguration DropDownSplitConfiguration => this.Properties.GetProperty(28) as QCompositeSeparatorConfiguration;

    protected override string DefaultContentPartLayoutOrder => this.m_sDefaultContentLayoutOrder;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
