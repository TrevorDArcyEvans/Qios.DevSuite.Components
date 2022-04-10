// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTranslucentWindowComponentConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QTranslucentWindowComponentConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropTopMost = 0;
    protected const int PropBackgroundImage = 1;
    protected const int PropOpacity = 2;
    protected const int PropCursor = 3;
    protected const int CurrentPropertyCount = 4;
    protected const int TotalPropertyCount = 4;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public QTranslucentWindowComponentConfiguration()
    {
      this.Properties.DefineProperty(0, (object) false);
      this.Properties.DefineProperty(1, (object) null);
      this.Properties.DefineProperty(2, (object) 1.0);
      this.Properties.DefineProperty(3, (object) Cursors.Default);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 4;

    [Description("Gets or sets if the QTranslucentWindow must be topmost")]
    [QPropertyIndex(0)]
    [Category("QAppearance")]
    public virtual bool TopMost
    {
      get => (bool) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Description("Gets or sets the background image of the QTranslucentWindow")]
    [Category("QAppearance")]
    [QPropertyIndex(1)]
    public virtual Image BackgroundImage
    {
      get => (Image) this.Properties.GetProperty(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(2)]
    [Description("Gets or sets the opacity image of the QTranslucentWindow")]
    public double Opacity
    {
      get => (double) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Description("Gets or sets the mouse cursor of the QTranslucentWindow")]
    [QPropertyIndex(3)]
    [Category("QAppearance")]
    public Cursor Cursor
    {
      get => (Cursor) this.Properties.GetProperty(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    internal void RaiseConfigurationChanged() => this.OnConfigurationChanged(EventArgs.Empty);

    internal virtual void ApplyToTranslucentWindow(QTranslucentWindow window)
    {
      if (window == null || window.IsDisposed)
        return;
      window.TopMost = this.TopMost;
      window.BackgroundImage = this.BackgroundImage;
      window.Opacity = this.Opacity;
      window.Cursor = this.Cursor;
    }

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
  }
}
