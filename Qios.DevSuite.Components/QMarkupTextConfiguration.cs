// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextConfiguration : QFastPropertyBagHost
  {
    protected const int PropMarkupPadding = 0;
    protected const int PropBiggerSmallerStep = 1;
    protected const int PropTextAlign = 2;
    protected const int PropWrapText = 3;
    protected const int PropMaximumSize = 4;
    protected const int PropAdjustHeightToTextSize = 5;
    protected const int PropAdjustWidthToTextSize = 6;
    protected const int PropDrawTextOptions = 7;
    protected const int PropCanFocus = 8;
    protected const int CurrentPropertyCount = 9;
    protected const int TotalPropertyCount = 9;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QWeakDelegate m_oConfigurationChangedDelegate;

    public QMarkupTextConfiguration()
    {
      this.Properties.DefineProperty(0, (object) QPadding.Empty);
      this.Properties.DefineProperty(1, (object) 2);
      this.Properties.DefineProperty(2, (object) ContentAlignment.TopLeft);
      this.Properties.DefineProperty(3, (object) true);
      this.Properties.DefineProperty(4, (object) new Size(-1, -1));
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineProperty(6, (object) false);
      this.Properties.DefineProperty(7, (object) QDrawTextOptions.IgnorePrefix);
      this.Properties.DefineProperty(8, (object) true);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 9;

    [QWeakEvent]
    [Description("Gets raised when a property of the configuration is changed")]
    [Category("QEvents")]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [QPropertyIndex(8)]
    [Description("Gets or sets if the user can focus the control.")]
    [Category("QAppearance")]
    public bool CanFocus
    {
      get => (bool) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [QPropertyIndex(0)]
    [Description("Gets or sets the padding of the MarkupLabel.")]
    [Category("QAppearance")]
    public QPadding MarkupPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Description("Gets or sets the padding of the MarkupLabel.")]
    [QPropertyIndex(1)]
    [Category("QAppearance")]
    public int BiggerSmallerStep
    {
      get => (int) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [QPropertyIndex(2)]
    [Description("Gets or sets the alignment of the text.")]
    [Category("QAppearance")]
    public ContentAlignment TextAlign
    {
      get => (ContentAlignment) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Description("Gets or sets whether the text must be wrapped.")]
    [QPropertyIndex(3)]
    [Category("QAppearance")]
    public bool WrapText
    {
      get => (bool) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [QPropertyIndex(4)]
    [Description("Gets or sets the maximumSize. If this is set to -1 the maximumSize is automatically determined based on the Size and the the AdjustHeight and Width properties.")]
    [Category("QAppearance")]
    public Size MaximumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [Description("Gets or sets whether the height of the QSMartLabel must be adjusted to the size of the text.")]
    [QPropertyIndex(5)]
    [Category("QAppearance")]
    public bool AdjustHeightToTextSize
    {
      get => (bool) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [QPropertyIndex(6)]
    [Description("Gets or sets whether the width of the QSMartLabel must be adjusted to the size of the text.")]
    [Category("QAppearance")]
    public bool AdjustWidthToTextSize
    {
      get => (bool) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [Description("Gets or sets additional options for drawing text.")]
    [QPropertyIndex(7)]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [Category("QAppearance")]
    public QDrawTextOptions DrawTextOptions
    {
      get => (QDrawTextOptions) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
