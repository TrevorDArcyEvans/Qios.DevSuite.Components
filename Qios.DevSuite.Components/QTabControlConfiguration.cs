// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabControlConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QTabControlConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropContentMargin = 0;
    protected const int PropContentAppearance = 1;
    protected const int PropAnimationInterval = 2;
    protected const int CurrentPropertyCount = 3;
    protected const int TotalPropertyCount = 3;
    private QWeakDelegate m_oConfigurationChangedDelegate;
    private EventHandler m_oAppearanceChangedHandler;

    public QTabControlConfiguration()
    {
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
      this.m_oAppearanceChangedHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.Properties.DefineProperty(0, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(2, (object) 10);
      this.Properties.DefineResettableProperty(1, (IQResettableValue) this.CreateContentApperearance());
      this.ContentAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
    }

    protected override int GetRequestedCount() => 3;

    protected virtual QTabControlContentAppearance CreateContentApperearance() => new QTabControlContentAppearance();

    [Description("Gets raised when a property of the configuration is changed")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [Description("Gets or sets the internal for the animation timer")]
    [QPropertyIndex(2)]
    [Category("QAppearance")]
    public virtual int AnimationInterval
    {
      get => (int) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(0)]
    [Description("Gets or sets the margin used between the TabStrips and the ContentShape or TabPages.")]
    public virtual QMargin ContentMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [QPropertyIndex(1)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the appearance of the content of a QTabControl. The content is the part between the TabStrips and the TabPages.")]
    public QTabControlContentAppearance ContentAppearance
    {
      get => this.Properties.GetProperty(1) as QTabControlContentAppearance;
      set
      {
        if (this.ContentAppearance == value)
          return;
        if (this.ContentAppearance != null)
          this.ContentAppearance.AppearanceChanged -= this.m_oAppearanceChangedHandler;
        this.Properties.SetProperty(1, (object) value);
        if (this.ContentAppearance == null)
          return;
        this.ContentAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      }
    }

    private void Appearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    ~QTabControlConfiguration() => this.Dispose(false);
  }
}
