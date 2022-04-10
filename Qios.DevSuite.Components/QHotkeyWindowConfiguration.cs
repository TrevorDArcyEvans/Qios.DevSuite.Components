// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QHotkeyWindowConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QHotkeyWindowConfiguration : QFastPropertyBagHost
  {
    protected const int PropPadding = 0;
    protected const int PropAppearance = 1;
    protected const int CurrentPropertyCount = 2;
    protected const int TotalPropertyCount = 2;
    private EventHandler m_oChildObjectsChangedHandler;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    public QHotkeyWindowConfiguration()
    {
      this.m_oChildObjectsChangedHandler = new EventHandler(this.ChildObjects_ChildObjectChanged);
      this.Properties.DefineProperty(0, (object) new QPadding(3, 1, 1, 3));
      this.Properties.DefineResettableProperty(1, (IQResettableValue) new QHotkeyWindowAppearance());
    }

    protected override int GetRequestedCount() => 2;

    [Category("QEvents")]
    [QWeakEvent]
    [Description("Gets raised when a property of the configuration is changed")]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [Category("QAppearance")]
    [Description("Contains the padding between the edge of a button and its contents.")]
    [QPropertyIndex(0)]
    public QPadding Padding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Description("Contains the appearance of the RibbonHotkeyWindow.")]
    [QPropertyIndex(1)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QHotkeyWindowAppearance Appearance => this.Properties.GetProperty(1) as QHotkeyWindowAppearance;

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void ChildObjects_ChildObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
