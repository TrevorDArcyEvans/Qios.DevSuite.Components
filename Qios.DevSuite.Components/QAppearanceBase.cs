// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppearanceBase
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QAppearanceBase : 
    QFastPropertyBagHost,
    IQAppearance,
    IQMetallicAppearance,
    IQGradientAppearance
  {
    protected const int PropWeakEventHandlers = 0;
    protected internal const int PropBackgroundStyle = 1;
    protected internal const int PropGradientAngle = 2;
    protected internal const int PropGradientBlendPosition = 3;
    protected internal const int PropGradientBlendFactor = 4;
    protected internal const int PropMetallicOffset = 5;
    protected internal const int PropMetallicOffsetUnit = 6;
    protected internal const int PropMetallicNearIntensity = 7;
    protected internal const int PropMetallicFarIntensity = 8;
    protected internal const int PropMetallicInnerGlowWidth = 9;
    protected internal const int PropMetallicDirection = 10;
    protected internal const int PropMetallicShineIntensity = 11;
    protected internal const int PropMetallicShineSaturation = 12;
    protected internal const int PropMetallicAutomaticColorOrder = 13;
    protected const int CurrentPropertyCount = 14;
    protected const int TotalPropertyCount = 14;
    private QWeakDelegate m_oAppearanceChangedDelegate;

    public QAppearanceBase()
    {
      this.Properties.DefineProperty(0, (object) true);
      this.Properties.DefineProperty(1, (object) QColorStyle.Gradient);
      this.Properties.DefineProperty(2, (object) 0);
      this.Properties.DefineProperty(3, (object) 100);
      this.Properties.DefineProperty(4, (object) 100);
      this.Properties.DefineProperty(5, (object) 40);
      this.Properties.DefineProperty(6, (object) QAppearanceUnit.Percent);
      this.Properties.DefineProperty(7, (object) 50);
      this.Properties.DefineProperty(8, (object) 50);
      this.Properties.DefineProperty(9, (object) 0);
      this.Properties.DefineProperty(10, (object) QMetallicAppearanceDirection.Horizontal);
      this.Properties.DefineProperty(11, (object) 70);
      this.Properties.DefineProperty(12, (object) 100);
      this.Properties.DefineProperty(13, (object) true);
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.PropertyBag_PropertyChanged);
    }

    protected override int GetRequestedCount() => 14;

    [QWeakEvent]
    public event EventHandler AppearanceChanged
    {
      add => this.m_oAppearanceChangedDelegate = QWeakDelegate.Combine(this.m_oAppearanceChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oAppearanceChangedDelegate = QWeakDelegate.Remove(this.m_oAppearanceChangedDelegate, (Delegate) value);
    }

    [QPropertyIndex(1)]
    [Description("Gets or sets the BackgroundStyle. The BackgroundStyle defines if the background should be drawn as Gradient or Solid")]
    public QColorStyle BackgroundStyle
    {
      get => (QColorStyle) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Description("Gets or sets the angle of the gradient. This property only applies when the BackgroundStyle is set to Gradient")]
    [QPropertyIndex(2)]
    public int GradientAngle
    {
      get => (int) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Description("Gets or sets the position for which the BlendFactor should be applied. This property only applies when the BackgroundStyle is set to Gradient")]
    [QPropertyIndex(3)]
    public int GradientBlendPosition
    {
      get => (int) this.Properties.GetPropertyAsValueType(3);
      set
      {
        if (value < 0 || value > 100)
          throw new InvalidOperationException(QResources.GetException("QAppearanceBase_Between_Invalid", (object) 0, (object) 100));
        this.Properties.SetProperty(3, (object) value);
      }
    }

    [QPropertyIndex(4)]
    [Description("Gets or sets factor for the BlendPosition. This is the factor from the two colors that should be visible at the specified BlendPosition. This property only applies when the BackgroundStyle is set to Gradient")]
    public int GradientBlendFactor
    {
      get => (int) this.Properties.GetPropertyAsValueType(4);
      set
      {
        if (value < 0 || value > 100)
          throw new InvalidOperationException(QResources.GetException("QAppearanceBase_Between_Invalid", (object) 0, (object) 100));
        this.Properties.SetProperty(4, (object) value);
      }
    }

    [Description("Gets or sets the metallic offset. This is where the metallic flips in percentages. This is only used when BackgroundStyle is set to Metallic")]
    [QPropertyIndex(5)]
    public int MetallicOffset
    {
      get => (int) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [QPropertyIndex(6)]
    [Description("Gets or sets the unit used for MetallicOffsetFactor.")]
    public QAppearanceUnit MetallicOffsetUnit
    {
      get => (QAppearanceUnit) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [QPropertyIndex(9)]
    [Description("Gets or sets the width of a possible inner glow. This is only used when BackgroundStyle is set to Metallic")]
    public int MetallicInnerGlowWidth
    {
      get => (int) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    [QPropertyIndex(10)]
    [Description(" Gets or sets the direction of the metallic appearance.. This is only used when BackgroundStyle is set to Metallic")]
    public QMetallicAppearanceDirection MetallicDirection
    {
      get => (QMetallicAppearanceDirection) this.Properties.GetPropertyAsValueType(10);
      set => this.Properties.SetProperty(10, (object) value);
    }

    [QPropertyIndex(7)]
    [Description("Gets the intensity of the color at the split. 0 is full Color A, 100 is full Color B")]
    public int MetallicNearIntensity
    {
      get => (int) this.Properties.GetPropertyAsValueType(7);
      set
      {
        if (value < 0 || value > 100)
          throw new InvalidOperationException(QResources.GetException("QAppearanceBase_Between_Invalid", (object) 0, (object) 100));
        this.Properties.SetProperty(7, (object) value);
      }
    }

    [QPropertyIndex(8)]
    [Description("Gets the intensity of the color at the end. 0 is full Color A, 100 is full Color B")]
    public int MetallicFarIntensity
    {
      get => (int) this.Properties.GetPropertyAsValueType(8);
      set
      {
        if (value < 0 || value > 100)
          throw new InvalidOperationException(QResources.GetException("QAppearanceBase_Between_Invalid", (object) 0, (object) 100));
        this.Properties.SetProperty(8, (object) value);
      }
    }

    [Description("Gets the intensity of the metallic shine effect. 0 is no shine effect, 100 is a strong shine effect.")]
    [QPropertyIndex(11)]
    public int MetallicShineIntensity
    {
      get => (int) this.Properties.GetPropertyAsValueType(11);
      set
      {
        if (value < 0 || value > 100)
          throw new InvalidOperationException(QResources.GetException("QAppearanceBase_Between_Invalid", (object) 0, (object) 100));
        this.Properties.SetProperty(11, (object) value);
      }
    }

    [QPropertyIndex(12)]
    [Description("Gets the saturation of the metallic shine effect. 0 is the first background color effect, 100 is a more colorful color.")]
    public int MetallicShineSaturation
    {
      get => (int) this.Properties.GetPropertyAsValueType(12);
      set
      {
        if (value < 0 || value > 100)
          throw new InvalidOperationException(QResources.GetException("QAppearanceBase_Between_Invalid", (object) 0, (object) 100));
        this.Properties.SetProperty(12, (object) value);
      }
    }

    [QPropertyIndex(13)]
    [Description("Gets or sets whether the order of the colors must happen automatically. When automatically, the darkest color will be the lowest color.")]
    public bool MetallicAutomaticColorOrder
    {
      get => (bool) this.Properties.GetPropertyAsValueType(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    protected virtual void OnAppearanceChanged(EventArgs e) => this.m_oAppearanceChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oAppearanceChangedDelegate, (object) this, (object) e);

    private void PropertyBag_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnAppearanceChanged(EventArgs.Empty);
  }
}
