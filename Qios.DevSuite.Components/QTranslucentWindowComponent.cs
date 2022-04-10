// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTranslucentWindowComponent
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QTranslucentWindowComponent), "Resources.ControlImages.QTranslucentWindow.bmp")]
  [ToolboxItem(true)]
  public class QTranslucentWindowComponent : QControlComponent
  {
    private Container components;
    private EventHandler m_oConfigurationChangedEventHandler;
    private QTranslucentWindowComponentConfiguration m_oConfiguration;
    private QWeakDelegate m_oTranslucentWindowCreatedDelegate;

    public QTranslucentWindowComponent(IContainer container)
    {
      container?.Add((IComponent) this);
      this.InitializeComponent();
      this.Initialize();
    }

    public QTranslucentWindowComponent()
      : this((IContainer) null)
    {
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the QShapedWindow is created")]
    public event QTranslucentWindowEventHandler TranslucentWindowCreated
    {
      add => this.m_oTranslucentWindowCreatedDelegate = QWeakDelegate.Combine(this.m_oTranslucentWindowCreatedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oTranslucentWindowCreatedDelegate = QWeakDelegate.Remove(this.m_oTranslucentWindowCreatedDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QTranslucentWindowComponentConfiguration for the component.")]
    public virtual QTranslucentWindowComponentConfiguration Configuration
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
        this.TranslucentWindowComponent_ConfigurationChanged((object) this.m_oConfiguration, EventArgs.Empty);
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    public QTranslucentWindow Show()
    {
      QTranslucentWindow translucentWindowInstance = this.CreateTranslucentWindowInstance();
      this.OnTranslucentWindowCreated(new QTranslucentWindowEventArgs(translucentWindowInstance));
      translucentWindowInstance?.Show();
      return translucentWindowInstance;
    }

    public QTranslucentWindow ShowCenteredOnScreen()
    {
      QTranslucentWindow translucentWindowInstance = this.CreateTranslucentWindowInstance();
      this.OnTranslucentWindowCreated(new QTranslucentWindowEventArgs(translucentWindowInstance));
      translucentWindowInstance?.ShowCenteredOnScreen();
      return translucentWindowInstance;
    }

    protected virtual void OnTranslucentWindowCreated(QTranslucentWindowEventArgs e) => this.m_oTranslucentWindowCreatedDelegate = QWeakDelegate.InvokeDelegate(this.m_oTranslucentWindowCreatedDelegate, (object) this, (object) e);

    protected virtual QTranslucentWindowComponentConfiguration CreateConfigurationInstance() => new QTranslucentWindowComponentConfiguration();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    protected virtual QTranslucentWindow CreateTranslucentWindowInstance()
    {
      QTranslucentWindow window = new QTranslucentWindow();
      if (this.m_oConfiguration != null)
        this.m_oConfiguration.ApplyToTranslucentWindow(window);
      return window;
    }

    private void Initialize()
    {
      this.m_oConfigurationChangedEventHandler = new EventHandler(this.TranslucentWindowComponent_ConfigurationChanged);
      this.m_oConfiguration = this.CreateConfigurationInstance();
      this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
    }

    private void TranslucentWindowComponent_ConfigurationChanged(object sender, EventArgs e)
    {
    }

    private void InitializeComponent() => this.components = new Container();
  }
}
