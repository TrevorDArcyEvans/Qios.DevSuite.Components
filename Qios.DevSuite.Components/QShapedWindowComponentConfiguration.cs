// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindowComponentConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QShapedWindowComponentConfiguration : QTranslucentWindowComponentConfiguration
  {
    protected const int PropShapedWindowConfiguration = 4;
    protected const int PropShapedWindowAppearance = 5;
    protected const int PropFlipHorizontal = 6;
    protected const int PropFlipVertical = 7;
    protected const int PropSize = 8;
    protected new const int CurrentPropertyCount = 5;
    protected new const int TotalPropertyCount = 9;
    private EventHandler m_oShapedWindowConfigurationChangedEventHandler;
    private EventHandler m_oShapedWindowAppearanceChangedEventHandler;
    private QWeakDelegate m_oShapedWindowConfigurationChangedDelegate;
    private QWeakDelegate m_oShapedWindowAppearanceChangedDelegate;

    public QShapedWindowComponentConfiguration()
    {
      QShapedWindowConfiguration configurationInstance = this.CreateShapedWindowConfigurationInstance();
      this.m_oShapedWindowConfigurationChangedEventHandler = new EventHandler(this.ComponentConfiguration_ShapedWindowConfigurationChanged);
      this.Properties.DefineResettableProperty(4, (IQResettableValue) configurationInstance);
      configurationInstance.ConfigurationChanged += this.m_oShapedWindowConfigurationChangedEventHandler;
      QShapedWindowAppearance appearanceInstance = this.CreateShapedWindowAppearanceInstance();
      this.m_oShapedWindowAppearanceChangedEventHandler = new EventHandler(this.ComponentConfiguration_ShapedWindowAppearanceChanged);
      this.Properties.DefineResettableProperty(5, (IQResettableValue) appearanceInstance);
      appearanceInstance.AppearanceChanged += this.m_oShapedWindowAppearanceChangedEventHandler;
      this.Properties.DefineProperty(6, (object) false);
      this.Properties.DefineProperty(7, (object) false);
      this.Properties.DefineProperty(8, (object) new Size(100, 100));
    }

    protected override int GetRequestedCount() => 9;

    [QWeakEvent]
    public event EventHandler ShapedWindowConfigurationChanged
    {
      add => this.m_oShapedWindowConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oShapedWindowConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oShapedWindowConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oShapedWindowConfigurationChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler ShapedWindowAppearanceChanged
    {
      add => this.m_oShapedWindowAppearanceChangedDelegate = QWeakDelegate.Combine(this.m_oShapedWindowAppearanceChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oShapedWindowAppearanceChangedDelegate = QWeakDelegate.Remove(this.m_oShapedWindowAppearanceChangedDelegate, (Delegate) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(8)]
    [Description("Determines the initial size of the window")]
    public Size Size
    {
      get => (Size) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(6)]
    [Description("Determines if the shape must be flipped horizontally")]
    public bool FlipHorizontal
    {
      get => (bool) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [QPropertyIndex(7)]
    [Description("Determines if the shape must be flipped vertically")]
    [Category("QAppearance")]
    public bool FlipVertical
    {
      get => (bool) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets the QShapedWindowConfiguration for the QShapedWindow.")]
    [QPropertyIndex(4)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QShapedWindowConfiguration ShapedWindowConfiguration
    {
      get => (QShapedWindowConfiguration) this.Properties.GetProperty(4);
      set
      {
        QShapedWindowConfiguration property = (QShapedWindowConfiguration) this.Properties.GetProperty(4);
        if (property == value)
          return;
        if (property != null)
          property.ConfigurationChanged -= this.m_oShapedWindowConfigurationChangedEventHandler;
        this.Properties.SetProperty(4, (object) value);
        QShapedWindowConfiguration windowConfiguration = value;
        if (windowConfiguration == null)
          return;
        windowConfiguration.ConfigurationChanged += this.m_oShapedWindowConfigurationChangedEventHandler;
        this.ComponentConfiguration_ShapedWindowConfigurationChanged((object) this, EventArgs.Empty);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [QPropertyIndex(5)]
    [Description("Gets the QShapedWindowAppearance for the QShapedWindow.")]
    public virtual QShapedWindowAppearance ShapedWindowAppearance
    {
      get => (QShapedWindowAppearance) this.Properties.GetProperty(5);
      set
      {
        QShapedWindowAppearance property = (QShapedWindowAppearance) this.Properties.GetProperty(5);
        if (property == value)
          return;
        if (property != null)
          property.AppearanceChanged -= this.m_oShapedWindowAppearanceChangedEventHandler;
        this.Properties.SetProperty(5, (object) value);
        QShapedWindowAppearance windowAppearance = value;
        if (windowAppearance == null)
          return;
        windowAppearance.AppearanceChanged += this.m_oShapedWindowAppearanceChangedEventHandler;
        this.ComponentConfiguration_ShapedWindowAppearanceChanged((object) this, EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the background image of the QShapedWindow. This is used to create a shaped window.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set => base.BackgroundImage = value;
    }

    [Category("QAppearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets if the QTranslucentWindow must be topmost. Use the TopMost property of the QShapedWindowConfiguration.")]
    public override bool TopMost
    {
      get => base.TopMost;
      set => base.TopMost = value;
    }

    private void ComponentConfiguration_ShapedWindowConfigurationChanged(object sender, EventArgs e) => this.OnShapedWindowConfigurationChanged(EventArgs.Empty);

    private void ComponentConfiguration_ShapedWindowAppearanceChanged(object sender, EventArgs e) => this.OnShapedWindowAppearanceChanged(EventArgs.Empty);

    protected virtual void OnShapedWindowConfigurationChanged(EventArgs e) => this.m_oShapedWindowConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oShapedWindowConfigurationChangedDelegate, (object) this, (object) e);

    protected virtual void OnShapedWindowAppearanceChanged(EventArgs e) => this.m_oShapedWindowAppearanceChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oShapedWindowAppearanceChangedDelegate, (object) this, (object) e);

    protected virtual QShapedWindowConfiguration CreateShapedWindowConfigurationInstance() => new QShapedWindowConfiguration();

    protected virtual QShapedWindowAppearance CreateShapedWindowAppearanceInstance() => new QShapedWindowAppearance();

    internal override void ApplyToTranslucentWindow(QTranslucentWindow window)
    {
      base.ApplyToTranslucentWindow(window);
      if (!(window is QShapedWindow qshapedWindow))
        return;
      qshapedWindow.Configuration = this.ShapedWindowConfiguration;
      qshapedWindow.Appearance = this.ShapedWindowAppearance;
      qshapedWindow.FlipHorizontal = this.FlipHorizontal;
      qshapedWindow.FlipVertical = this.FlipVertical;
      qshapedWindow.Size = this.Size;
    }
  }
}
