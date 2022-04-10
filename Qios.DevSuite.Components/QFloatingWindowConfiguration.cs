// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingWindowConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QFloatingWindowConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropInheritWindowsSettings = 0;
    protected const int PropCloseOnMouseUp = 1;
    protected const int PropCloseOnMouseDown = 2;
    protected const int PropShowBackgroundShade = 3;
    protected const int PropAnimateShowTime = 4;
    protected const int PropAnimateShowType = 5;
    protected const int CurrentPropertyCount = 6;
    protected const int TotalPropertyCount = 6;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public QFloatingWindowConfiguration()
    {
      this.Properties.DefineProperty(3, (object) true);
      this.Properties.DefineProperty(0, (object) false);
      this.Properties.DefineProperty(2, (object) true);
      this.Properties.DefineProperty(1, (object) true);
      this.Properties.DefineProperty(4, (object) 100);
      this.Properties.DefineProperty(5, (object) QMenuAnimationType.Fade);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 6;

    [Description("Gets or sets whether the QFloatingWindow should be closed on a MouseDown event")]
    [QPropertyIndex(2)]
    [Category("QAppearance")]
    public bool CloseOnMouseDown
    {
      get => (bool) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [QPropertyIndex(1)]
    [Description("Gets or sets whether the QFloatingWindow should be closed on a MouseUp event")]
    [Category("QAppearance")]
    public bool CloseOnMouseUp
    {
      get => (bool) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Description("Gets or sets whether the QFloatingWindow should contain a BackgroundShade. This is ignored when the InheritWindowsSettings is true.")]
    [QPropertyIndex(3)]
    [Category("QAppearance")]
    public bool ShowBackgroundShade
    {
      get => (bool) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [Description("Gets or sets the time (in milliseconds) that is used for animation when showing the window.")]
    [QPropertyIndex(4)]
    [Category("QAppearance")]
    public int AnimateShowTime
    {
      get => (int) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [QPropertyIndex(5)]
    [Description("Gets or sets the type of animation when showing. This is ignored when InheritWindowsSettings is true.")]
    [Category("QAppearance")]
    public QMenuAnimationType AnimateShowType
    {
      get => (QMenuAnimationType) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [Browsable(false)]
    internal QMenuAnimationType UsedAnimateShowType
    {
      get
      {
        if (!this.InheritWindowsSettings)
          return this.AnimateShowType;
        if (!NativeHelper.AnimateMenus)
          return QMenuAnimationType.None;
        return NativeHelper.AnimateMenusWithFading ? QMenuAnimationType.Fade : QMenuAnimationType.Slide;
      }
    }

    [QPropertyIndex(0)]
    [Category("QAppearance")]
    [Description("Gets or sets whether the QFloatingWindow should inherit WindowsSettings like drawing a shade or animating a window.")]
    public bool InheritWindowsSettings
    {
      get => (bool) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Browsable(false)]
    internal bool UsedShowBackgroundShade
    {
      get
      {
        if (!this.InheritWindowsSettings)
          return this.ShowBackgroundShade;
        return NativeHelper.ShowShadows;
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      int num = disposing ? 1 : 0;
    }

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.RaiseConfigurationChanged();

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    internal void RaiseConfigurationChanged() => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
