// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QBalloonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QBalloonConfiguration : QFastPropertyBagHost
  {
    protected internal const int PropBalloonWindowClassReference = 0;
    protected internal const int PropInheritWindowsSettings = 1;
    protected internal const int PropAutoPopDelay = 2;
    protected internal const int PropInitialDelay = 3;
    protected internal const int PropReshowDelay = 4;
    protected internal const int PropShowAlways = 5;
    protected internal const int PropFlipWindow = 6;
    protected internal const int PropShowOnFocus = 7;
    protected internal const int PropShowOnHover = 8;
    protected internal const int PropAutoHide = 9;
    protected internal const int PropAnimateWindow = 10;
    protected internal const int PropAnimateTime = 11;
    protected internal const int PropColorSchemeSource = 12;
    protected internal const int PropFontSource = 13;
    protected internal const int PropEnabled = 14;
    protected internal const int PropBalloonWindowConfiguration = 15;
    protected internal const int PropBalloonWindowAppearance = 16;
    protected internal const int CurrentPropertyCount = 17;
    protected const int TotalPropertyCount = 17;
    private EventHandler m_oBalloonWindowConfigurationChangedEventHandler;
    private EventHandler m_oBalloonWindowAppearanceChangedEventHandler;
    private QWeakDelegate m_oConfigurationChangedDelegate;
    private QWeakDelegate m_oBalloonWindowAppearanceChangedDelegate;
    private QWeakDelegate m_oBalloonWindowConfigurationChangedDelegate;
    private QWeakDelegate m_oBalloonWindowShapeChangedDelegate;

    public QBalloonConfiguration()
    {
      this.m_oBalloonWindowConfigurationChangedEventHandler = new EventHandler(this.Balloon_BalloonWindowConfigurationChanged);
      this.m_oBalloonWindowAppearanceChangedEventHandler = new EventHandler(this.BalloonWindowAppearance_AppearanceChanged);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
      this.Properties.DefineProperty(0, (object) null);
      this.Properties.DefineProperty(13, (object) QBalloonWindowPropertySource.QBalloon);
      this.Properties.DefineProperty(12, (object) QBalloonWindowPropertySource.QBalloon);
      this.Properties.DefineProperty(1, (object) false);
      this.Properties.DefineProperty(6, (object) true);
      this.Properties.DefineProperty(10, (object) true);
      this.Properties.DefineProperty(11, (object) 200);
      this.Properties.DefineProperty(9, (object) true);
      this.Properties.DefineProperty(7, (object) false);
      this.Properties.DefineProperty(8, (object) true);
      this.Properties.DefineProperty(2, (object) 5000);
      this.Properties.DefineProperty(3, (object) 500);
      this.Properties.DefineProperty(4, (object) 500);
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(14, (object) true);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) this.CreateBalloonWindowConfigurationInstance());
      this.BalloonWindowConfiguration.ConfigurationChanged += this.m_oBalloonWindowConfigurationChangedEventHandler;
      this.Properties.DefineResettableProperty(16, (IQResettableValue) this.CreateBalloonWindowAppearanceInstance());
      this.BalloonWindowAppearance.AppearanceChanged += this.m_oBalloonWindowAppearanceChangedEventHandler;
    }

    protected override int GetRequestedCount() => 17;

    protected virtual QBalloonWindowConfiguration CreateBalloonWindowConfigurationInstance() => new QBalloonWindowConfiguration();

    protected virtual QBalloonWindowAppearance CreateBalloonWindowAppearanceInstance() => new QBalloonWindowAppearance();

    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler BalloonWindowAppearanceChanged
    {
      add => this.m_oBalloonWindowAppearanceChangedDelegate = QWeakDelegate.Combine(this.m_oBalloonWindowAppearanceChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oBalloonWindowAppearanceChangedDelegate = QWeakDelegate.Remove(this.m_oBalloonWindowAppearanceChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    public event EventHandler BalloonWindowConfigurationChanged
    {
      add => this.m_oBalloonWindowConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oBalloonWindowConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oBalloonWindowConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oBalloonWindowConfigurationChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    internal event EventHandler BalloonWindowShapeChanged
    {
      add => this.m_oBalloonWindowShapeChangedDelegate = QWeakDelegate.Combine(this.m_oBalloonWindowShapeChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oBalloonWindowShapeChangedDelegate = QWeakDelegate.Remove(this.m_oBalloonWindowShapeChangedDelegate, (Delegate) value);
    }

    [QPropertyIndex(13)]
    [Localizable(true)]
    [Category("QAppearance")]
    [Description("Gets or sets the source of the Font properties. The used font properties of the QBalloonWindow can be from QBalloon or QBalloonWindow itself.")]
    public QBalloonWindowPropertySource FontSource
    {
      get => (QBalloonWindowPropertySource) this.Properties.GetPropertyAsValueType(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    [Description("Gets or sets the source of the ColorScheme. The used ColorScheme of the QBalloonWindow can be from QBalloon or QBalloonWindow itself.")]
    [DefaultValue(QBalloonWindowPropertySource.QBalloon)]
    [Category("QAppearance")]
    [QPropertyIndex(12)]
    public QBalloonWindowPropertySource ColorSchemeSource
    {
      get => (QBalloonWindowPropertySource) this.Properties.GetPropertyAsValueType(12);
      set => this.Properties.SetProperty(12, (object) value);
    }

    [QPropertyIndex(0)]
    [Category("QAppearance")]
    [Description("Gets or sets the class reference of the QBalloonWindow")]
    public string BalloonWindowClassReference
    {
      get => this.Properties.GetProperty(0) as string;
      set
      {
        this.Properties.SetProperty(0, (object) value);
        this.OnBalloonWindowShapeChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [QPropertyIndex(15)]
    [Description("Gets the QBalloonWindowConfiguration for the QBalloon. This QBalloonWindowConfiguration will be the parent configuration for the QBalloonWindowConfiguration of the QBalloonWindow.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QBalloonWindowConfiguration BalloonWindowConfiguration
    {
      get => this.Properties.GetProperty(15) as QBalloonWindowConfiguration;
      set
      {
        if (this.BalloonWindowConfiguration == value)
          return;
        if (this.BalloonWindowConfiguration != null)
          this.BalloonWindowConfiguration.ConfigurationChanged -= this.m_oBalloonWindowConfigurationChangedEventHandler;
        this.Properties.SetProperty(15, (object) value);
        if (this.BalloonWindowConfiguration == null)
          return;
        this.BalloonWindowConfiguration.ConfigurationChanged += this.m_oBalloonWindowConfigurationChangedEventHandler;
        this.OnBalloonWindowConfigurationChanged(EventArgs.Empty);
      }
    }

    [QPropertyIndex(16)]
    [Description("Gets the QBalloonWindowAppearance for the QBalloon. This QBalloonWindowAppearance will be the parent configuration for the QBalloonWindowAppearance of the QBalloonWindow.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QBalloonWindowAppearance BalloonWindowAppearance => this.Properties.GetProperty(16) as QBalloonWindowAppearance;

    [Description("Gets or sets whether the QBalloon should inherit WindowsSettings like drawing animating the balloon")]
    [QPropertyIndex(1)]
    [Category("QAppearance")]
    public virtual bool InheritWindowsSettings
    {
      get => (bool) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(6)]
    [Description("Gets or sets if the QBalloonWindow is flipped so that the topleft corner of the shape always points to the cursor when it doesnt fit on the screen.")]
    public virtual bool FlipWindow
    {
      get => (bool) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [Description("Gets or sets whether the window fades in and out")]
    [Category("QAppearance")]
    [QPropertyIndex(10)]
    public virtual bool AnimateWindow
    {
      get => (bool) this.Properties.GetPropertyAsValueType(10);
      set => this.Properties.SetProperty(10, (object) value);
    }

    [Browsable(false)]
    internal bool UsedAnimateWindow
    {
      get
      {
        if (!this.InheritWindowsSettings)
          return this.AnimateWindow;
        return NativeHelper.AnimateTooltips;
      }
    }

    [QPropertyIndex(11)]
    [Category("QAppearance")]
    [Description("Gets or sets the time of the window animation")]
    public virtual int AnimateTime
    {
      get => (int) this.Properties.GetPropertyAsValueType(11);
      set => this.Properties.SetProperty(11, (object) value);
    }

    [Description("Gets or sets if the QBalloonWindow automatically hides or only waits until the AutoPopupDelay has passed")]
    [QPropertyIndex(9)]
    [Category("QAppearance")]
    public virtual bool AutoHide
    {
      get => (bool) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    [Description("Gets or sets if the QBalloonWindow will be shown when the target control gets the focus")]
    [Category("QAppearance")]
    [QPropertyIndex(7)]
    public bool ShowOnFocus
    {
      get => (bool) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    [Description("Gets or sets if the QBalloonWindow will be shown when user hovers over the target control")]
    [DefaultValue(true)]
    [Category("QAppearance")]
    [QPropertyIndex(8)]
    public bool ShowOnHover
    {
      get => (bool) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [QPropertyIndex(14)]
    [Category("QBehavior")]
    [Description("Gets or sets whether the ToolTip is enabled.")]
    public virtual bool Enabled
    {
      get => (bool) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [Description("Gets or sets the period of time (in milliseconds) that the ToolTip remains visible when the mouse pointer is stationary within a control.")]
    [QPropertyIndex(2)]
    [Category("QBehavior")]
    public virtual int AutoPopDelay
    {
      get => (int) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [QPropertyIndex(3)]
    [Description("Gets or sets the period of time (in milliseconds) that the mouse pointer must remain stationary within a control before the ToolTip window is displayed.")]
    [Category("QBehavior")]
    public virtual int InitialDelay
    {
      get => (int) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [QPropertyIndex(4)]
    [Description("Gets or sets the length of time that must transpire before subsequent ToolTip windows appear as the mouse pointer moves from one control to another.")]
    [Category("QBehavior")]
    public virtual int ReshowDelay
    {
      get => (int) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [QPropertyIndex(5)]
    [Description("Gets or sets a value indicating whether a ToolTip window is displayed even when its parent control is not active.")]
    [Category("QBehavior")]
    public virtual bool ShowAlways
    {
      get => (bool) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    public void ConfigureToolTip(ToolTip toolTip)
    {
      if (toolTip == null)
        return;
      if (toolTip.AutoPopDelay != this.AutoPopDelay)
        toolTip.AutoPopDelay = this.AutoPopDelay;
      if (toolTip.InitialDelay != this.InitialDelay)
        toolTip.InitialDelay = this.InitialDelay;
      if (toolTip.ReshowDelay != this.ReshowDelay)
        toolTip.ReshowDelay = this.ReshowDelay;
      if (toolTip.ShowAlways == this.ShowAlways)
        return;
      toolTip.ShowAlways = this.ShowAlways;
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    protected virtual void OnBalloonWindowShapeChanged(EventArgs e) => this.m_oBalloonWindowShapeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oBalloonWindowShapeChangedDelegate, (object) this, (object) e);

    protected virtual void OnBalloonWindowConfigurationChanged(EventArgs e) => this.m_oBalloonWindowConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oBalloonWindowConfigurationChangedDelegate, (object) this, (object) e);

    protected virtual void OnBalloonWindowAppearanceChanged(EventArgs e) => this.m_oBalloonWindowAppearanceChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oBalloonWindowAppearanceChangedDelegate, (object) this, (object) e);

    private void Balloon_BalloonWindowConfigurationChanged(object sender, EventArgs e) => this.OnBalloonWindowConfigurationChanged(EventArgs.Empty);

    private void BalloonWindowAppearance_AppearanceChanged(object sender, EventArgs e) => this.OnBalloonWindowAppearanceChanged(EventArgs.Empty);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
