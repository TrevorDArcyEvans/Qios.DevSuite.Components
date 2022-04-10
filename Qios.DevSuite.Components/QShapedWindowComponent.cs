// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindowComponent
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QShapedWindowComponent), "Resources.ControlImages.QShapedWindow.bmp")]
  public class QShapedWindowComponent : QControlComponent
  {
    private Container components;
    private EventHandler m_oConfigurationChangedEventHandler;
    private QShapedWindowComponentConfiguration m_oConfiguration;
    private QWeakDelegate m_oShapedWindowCreatedDelegate;

    public QShapedWindowComponent(IContainer container)
    {
      container?.Add((IComponent) this);
      this.InitializeComponent();
      this.Initialize();
    }

    public QShapedWindowComponent()
      : this((IContainer) null)
    {
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QShapedWindow is created")]
    public event QShapedWindowEventHandler ShapedWindowCreated
    {
      add => this.m_oShapedWindowCreatedDelegate = QWeakDelegate.Combine(this.m_oShapedWindowCreatedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oShapedWindowCreatedDelegate = QWeakDelegate.Remove(this.m_oShapedWindowCreatedDelegate, (Delegate) value);
    }

    [Description("Gets or sets the QShapedWindowComponentConfiguration for the component.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QShapedWindowComponentConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration == value)
          return;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= this.m_oConfigurationChangedEventHandler;
        this.m_oConfiguration = value;
        if (this.m_oConfiguration == null)
          return;
        this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
        this.ShapedWindowComponent_ConfigurationChanged((object) this.m_oConfiguration, EventArgs.Empty);
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public QShapedWindow Show()
    {
      QShapedWindow shapedWindowInstance = this.CreateShapedWindowInstance();
      this.OnShapedWindowCreated(new QShapedWindowEventArgs(shapedWindowInstance));
      shapedWindowInstance?.Show();
      return shapedWindowInstance;
    }

    public QShapedWindow ShowCenteredOnScreen()
    {
      QShapedWindow shapedWindowInstance = this.CreateShapedWindowInstance();
      this.OnShapedWindowCreated(new QShapedWindowEventArgs(shapedWindowInstance));
      shapedWindowInstance?.ShowCenteredOnScreen();
      return shapedWindowInstance;
    }

    protected virtual void OnShapedWindowCreated(QShapedWindowEventArgs e) => this.m_oShapedWindowCreatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oShapedWindowCreatedDelegate, (object) this, (object) e);

    protected virtual QShapedWindowComponentConfiguration CreateConfigurationInstance() => new QShapedWindowComponentConfiguration();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    protected virtual QShapedWindow CreateShapedWindowInstance()
    {
      QShapedWindow window = new QShapedWindow();
      if (window != null)
      {
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ApplyToTranslucentWindow((QTranslucentWindow) window);
        if (this.ColorScheme != null)
          window.ColorScheme = this.ColorScheme;
      }
      return window;
    }

    private void Initialize()
    {
      this.m_oConfigurationChangedEventHandler = new EventHandler(this.ShapedWindowComponent_ConfigurationChanged);
      this.m_oConfiguration = this.CreateConfigurationInstance();
      this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
    }

    private void ShapedWindowComponent_ConfigurationChanged(object sender, EventArgs e)
    {
    }

    private void InitializeComponent() => this.components = new Container();
  }
}
