// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionButtonAreaConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCaptionButtonAreaConfiguration : QGroupPartConfiguration
  {
    protected const int PropMinimizeButtonConfiguration = 15;
    protected const int PropRestoreButtonConfiguration = 16;
    protected const int PropMaximizeButtonConfiguration = 17;
    protected const int PropCloseButtonConfiguration = 18;
    protected new const int CurrentPropertyCount = 4;
    protected new const int TotalPropertyCount = 19;
    private static Image m_oDefaultMinimizeMask;
    private static Image m_oDefaultRestoreMask;
    private static Image m_oDefaultMaximizeMask;
    private static Image m_oDefaultCloseMask;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QRibbonCaptionButtonAreaConfiguration()
    {
      if (QRibbonCaptionButtonAreaConfiguration.m_oDefaultMinimizeMask == null)
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        QRibbonCaptionButtonAreaConfiguration.m_oDefaultMinimizeMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionMinimizeMask.png"));
        QRibbonCaptionButtonAreaConfiguration.m_oDefaultRestoreMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionRestoreMask.png"));
        QRibbonCaptionButtonAreaConfiguration.m_oDefaultMaximizeMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionMaximizeMask.png"));
        QRibbonCaptionButtonAreaConfiguration.m_oDefaultCloseMask = (Image) new Bitmap(executingAssembly.GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.RibbonCaptionCloseMask.png"));
      }
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(6, (object) QPartAlignment.Far);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) new QRibbonCaptionButtonConfiguration(QRibbonCaptionButtonAreaConfiguration.m_oDefaultMinimizeMask));
      this.MinimizeButtonConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(16, (IQResettableValue) new QRibbonCaptionButtonConfiguration(QRibbonCaptionButtonAreaConfiguration.m_oDefaultRestoreMask));
      this.RestoreButtonConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(17, (IQResettableValue) new QRibbonCaptionButtonConfiguration(QRibbonCaptionButtonAreaConfiguration.m_oDefaultMaximizeMask));
      this.MaximizeButtonConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
      this.Properties.DefineResettableProperty(18, (IQResettableValue) new QRibbonCaptionButtonConfiguration(QRibbonCaptionButtonAreaConfiguration.m_oDefaultCloseMask));
      this.CloseButtonConfiguration.ConfigurationChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected override int GetRequestedCount() => 19;

    [Category("QAppearance")]
    [Description("Gets the configuration of the minimize button on this QRibbonCaption.")]
    [QPropertyIndex(15)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonCaptionButtonConfiguration MinimizeButtonConfiguration => this.Properties.GetProperty(15) as QRibbonCaptionButtonConfiguration;

    [Description("Gets the configuration of the restore button on this QRibbonCaption.")]
    [QPropertyIndex(16)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QRibbonCaptionButtonConfiguration RestoreButtonConfiguration => this.Properties.GetProperty(16) as QRibbonCaptionButtonConfiguration;

    [Category("QAppearance")]
    [QPropertyIndex(17)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the restore button on this QRibbonCaption.")]
    public QRibbonCaptionButtonConfiguration MaximizeButtonConfiguration => this.Properties.GetProperty(17) as QRibbonCaptionButtonConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the configuration of the close button on this QRibbonCaption.")]
    [QPropertyIndex(18)]
    [Category("QAppearance")]
    public QRibbonCaptionButtonConfiguration CloseButtonConfiguration => this.Properties.GetProperty(18) as QRibbonCaptionButtonConfiguration;

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
