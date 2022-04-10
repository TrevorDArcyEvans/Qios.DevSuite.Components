// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPanelConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QPanelConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropShowText = 0;
    protected const int PropTextDock = 1;
    protected const int PropTextAlignment = 2;
    protected const int PropTextSpacing = 3;
    protected const int PropTextPadding = 4;
    protected const int PropVerticalTextOrientation = 5;
    protected const int CurrentPropertyCount = 6;
    protected const int TotalPropertyCount = 6;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public QPanelConfiguration()
    {
      this.Properties.DefineProperty(0, (object) false);
      this.Properties.DefineProperty(1, (object) DockStyle.Top);
      this.Properties.DefineProperty(2, (object) StringAlignment.Near);
      this.Properties.DefineProperty(3, (object) new QSpacing(10, 10));
      this.Properties.DefineProperty(4, (object) new QPadding(3, 0, 0, 3));
      this.Properties.DefineProperty(5, (object) QVerticalTextOrientation.VerticalDown);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 6;

    [Category("QAppearance")]
    [Description("Gets or sets the orientation of the text when its docked left or right")]
    [QPropertyIndex(5)]
    public QVerticalTextOrientation VerticalTextOrientation
    {
      get => (QVerticalTextOrientation) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the spacing in pixels from the closest side")]
    [QPropertyIndex(3)]
    public QSpacing TextSpacing
    {
      get => (QSpacing) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(4)]
    [Description("Gets or sets the padding of the text")]
    public QPadding TextPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [QPropertyIndex(0)]
    [Description("Gets or sets if the text must be visible")]
    [Category("QAppearance")]
    public bool ShowText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(2)]
    [Description("Gets or sets the alignment of the text")]
    public StringAlignment TextAlignment
    {
      get => (StringAlignment) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(1)]
    [Description("Gets or sets the dockstyle of the text")]
    public DockStyle TextDock
    {
      get => (DockStyle) this.Properties.GetPropertyAsValueType(1);
      set
      {
        if (value == DockStyle.Fill || value == DockStyle.None)
          throw new InvalidOperationException(QResources.GetException("QPanel_Dock_Invalid"));
        this.Properties.SetProperty(1, (object) value);
      }
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

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
