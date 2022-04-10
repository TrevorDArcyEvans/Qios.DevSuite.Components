// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonTabStripConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(false)]
  public class QRibbonTabStripConfiguration : QTabStripConfiguration
  {
    protected const int PropMdiCloseMask = 30;
    protected const int PropMdiRestoreMask = 31;
    protected const int PropMdiMinimizeMask = 32;
    protected const int PropHelpMask = 33;
    protected const int PropMdiCloseButtonVisible = 34;
    protected const int PropMdiRestoreButtonVisible = 35;
    protected const int PropMdiMinimizeButtonVisible = 36;
    protected const int PropHelpButtonVisible = 37;
    protected new const int CurrentPropertyCount = 8;
    protected new const int TotalPropertyCount = 38;
    private static Image m_oDefaultMdiCloseMask;
    private static Image m_oDefaultMdiRestoreMask;
    private static Image m_oDefaultMdiMinimizeMask;
    private static Image m_oDefaultHelpMask;

    public QRibbonTabStripConfiguration(Font font)
      : base(font)
    {
      if (QRibbonTabStripConfiguration.m_oDefaultMdiCloseMask == null)
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        QRibbonTabStripConfiguration.m_oDefaultMdiMinimizeMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionMinimizeMask.png"));
        QRibbonTabStripConfiguration.m_oDefaultMdiRestoreMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionRestoreMask.png"));
        QRibbonTabStripConfiguration.m_oDefaultMdiCloseMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionCloseMask.png"));
        QRibbonTabStripConfiguration.m_oDefaultHelpMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonHelp.png"));
      }
      this.Properties.DefineProperty(30, (object) null);
      this.Properties.DefineProperty(31, (object) null);
      this.Properties.DefineProperty(32, (object) null);
      this.Properties.DefineProperty(33, (object) null);
      this.Properties.DefineProperty(34, (object) true);
      this.Properties.DefineProperty(35, (object) true);
      this.Properties.DefineProperty(36, (object) true);
      this.Properties.DefineProperty(37, (object) true);
      this.Properties.DefineProperty(7, (object) true);
      this.Properties.DefineProperty(8, (object) 2);
      this.Properties.DefineProperty(9, (object) QTabStripSizeBehaviors.Shrink);
      this.Properties.DefineProperty(10, (object) (QFontStyle) FontStyle.Regular);
      this.Properties.DefineProperty(11, (object) (QFontStyle) FontStyle.Regular);
      this.Properties.DefineProperty(4, (object) new QPadding(40, 0, 0, 5));
      this.Properties.DefineProperty(5, (object) new QMargin(5, 0, 0, 7));
      this.Properties.DefineProperty(29, (object) new QPadding(2, 2, 2, 2));
      this.Properties.DefineProperty(14, (object) new QPadding(0, 0, -1, 0));
      this.PutFont(font);
    }

    protected override int GetRequestedCount() => 38;

    protected override QTabStripAppearance CreateTabStripAppearance() => (QTabStripAppearance) new QRibbonTabStripAppearance();

    protected override QAppearanceBase CreateNavigationButtonHotAppearance() => (QAppearanceBase) new QRibbonTabStripNavigationButtonAppearance();

    protected override QTabButtonConfiguration CreateTabButtonConfiguration() => (QTabButtonConfiguration) new QRibbonTabButtonConfiguration();

    [Category("QAppearance")]
    [Description("Gets the appearance of the TabStrip.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(25)]
    public QRibbonTabStripAppearance Appearance
    {
      get => base.Appearance as QRibbonTabStripAppearance;
      set => this.Appearance = (QTabStripAppearance) value;
    }

    [Description("Gets or sets the appearance a navigation button of the TabStrip")]
    [QPropertyIndex(27)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QShapeAppearance NavigationButtonHotAppearance
    {
      get => this.UsedNavigationButtonHotAppearance as QShapeAppearance;
      set => this.UsedNavigationButtonHotAppearance = (QAppearanceBase) value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(28)]
    [Description("Gets or sets the configuration of the TabButtons")]
    [Category("QAppearance")]
    public QRibbonTabButtonConfiguration ButtonConfiguration
    {
      get => base.ButtonConfiguration as QRibbonTabButtonConfiguration;
      set => this.ButtonConfiguration = (QTabButtonConfiguration) value;
    }

    [Description("Gets or sets whether the MDI restore button is visible")]
    [Category("QAppearance")]
    [QPropertyIndex(35)]
    [DefaultValue(true)]
    public bool MdiRestoreButtonVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(35);
      set => this.Properties.SetProperty(35, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(36)]
    [Description("Gets or sets whether the MDI minimize button is visible")]
    [DefaultValue(true)]
    public bool MdiMinimizeButtonVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(36);
      set => this.Properties.SetProperty(36, (object) value);
    }

    [QPropertyIndex(34)]
    [Category("QAppearance")]
    [Description("Gets or sets whether the MDI close button is visible")]
    [DefaultValue(true)]
    public bool MdiCloseButtonVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(34);
      set => this.Properties.SetProperty(34, (object) value);
    }

    [DefaultValue(true)]
    [Category("QAppearance")]
    [QPropertyIndex(37)]
    [Description("Gets or sets whether the Help button is visible")]
    public bool HelpButtonVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(37);
      set => this.Properties.SetProperty(37, (object) value);
    }

    [QPropertyIndex(30)]
    [Description("Contains the base image that is used to for the MDI Close button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    [Category("QAppearance")]
    public Image MdiCloseMask
    {
      get => this.Properties.GetProperty(30) as Image;
      set => this.Properties.SetProperty(30, (object) value);
    }

    [Description("Contains the base image that is used to for the MDI Restore button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    [QPropertyIndex(31)]
    [Category("QAppearance")]
    public Image MdiRestoreMask
    {
      get => this.Properties.GetProperty(31) as Image;
      set => this.Properties.SetProperty(31, (object) value);
    }

    [QPropertyIndex(32)]
    [Category("QAppearance")]
    [Description("Contains the base image that is used to for the MDI Minimize button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    public Image MdiMinimizeMask
    {
      get => this.Properties.GetProperty(32) as Image;
      set => this.Properties.SetProperty(32, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(33)]
    [Description("Contains the base image that is used to for the help button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    public Image HelpMask
    {
      get => this.Properties.GetProperty(33) as Image;
      set => this.Properties.SetProperty(33, (object) value);
    }

    [Browsable(false)]
    public Image UsedMdiCloseMask => this.MdiCloseMask == null ? QRibbonTabStripConfiguration.m_oDefaultMdiCloseMask : this.MdiCloseMask;

    [Browsable(false)]
    public Image UsedMdiRestoreMask => this.MdiRestoreMask == null ? QRibbonTabStripConfiguration.m_oDefaultMdiRestoreMask : this.MdiRestoreMask;

    [Browsable(false)]
    public Image UsedMdiMinimizeMask => this.MdiMinimizeMask == null ? QRibbonTabStripConfiguration.m_oDefaultMdiMinimizeMask : this.MdiMinimizeMask;

    [Browsable(false)]
    public Image UsedHelpMask => this.HelpMask == null ? QRibbonTabStripConfiguration.m_oDefaultHelpMask : this.HelpMask;
  }
}
