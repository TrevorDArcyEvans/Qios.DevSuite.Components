// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QTabButtonConfiguration : QFastPropertyBagHost
  {
    protected const int PropAlignment = 0;
    protected const int PropPadding = 1;
    protected const int PropSpacing = 2;
    protected const int PropIconSpacing = 3;
    protected const int PropIconSize = 4;
    protected const int PropTextSpacing = 5;
    protected const int PropOrientation = 6;
    protected const int PropContentOrder = 7;
    protected const int PropTextAlignment = 8;
    protected const int PropIconAlignment = 9;
    protected const int PropGrayscaleDisabledIcon = 10;
    protected const int PropMinimumSize = 11;
    protected const int PropMaximumSize = 12;
    protected const int PropBackgroundImageAlign = 13;
    protected const int PropBackgroundImageClip = 14;
    protected const int PropAppearance = 15;
    protected const int PropAppearanceHot = 16;
    protected const int PropAppearanceActive = 17;
    protected const int CurrentPropertyCount = 18;
    protected const int TotalPropertyCount = 18;
    private EventHandler m_oAppearanceChangedHandler;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    public QTabButtonConfiguration()
    {
      this.m_oAppearanceChangedHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.Properties.DefineProperty(0, (object) QTabButtonAlignment.Near);
      this.Properties.DefineProperty(1, (object) new QPadding(3, 1, 1, 3));
      this.Properties.DefineProperty(2, (object) new QSpacing(0, 0));
      this.Properties.DefineProperty(3, (object) new QSpacing(0, 2));
      this.Properties.DefineProperty(4, (object) new Size(16, 16));
      this.Properties.DefineProperty(5, (object) new QSpacing(0, 0));
      this.Properties.DefineProperty(6, (object) QContentOrientation.Horizontal);
      this.Properties.DefineProperty(7, (object) QTabButtonContentOrder.IconText);
      this.Properties.DefineProperty(8, (object) ContentAlignment.MiddleLeft);
      this.Properties.DefineProperty(9, (object) ContentAlignment.MiddleLeft);
      this.Properties.DefineProperty(10, (object) true);
      this.Properties.DefineProperty(11, (object) new Size(-1, -1));
      this.Properties.DefineProperty(12, (object) new Size(-1, -1));
      this.Properties.DefineProperty(13, (object) QImageAlign.Centered);
      this.Properties.DefineProperty(14, (object) true);
      this.Properties.DefineResettableProperty(15, (IQResettableValue) this.CreateAppearance());
      this.Properties.DefineResettableProperty(16, (IQResettableValue) this.CreateAppearanceHot());
      this.Properties.DefineResettableProperty(17, (IQResettableValue) this.CreateAppearanceActive());
      this.AppearanceHot.Properties.BaseProperties = this.Appearance.Properties;
      this.AppearanceActive.Properties.BaseProperties = this.Appearance.Properties;
      this.Appearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.AppearanceHot.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.AppearanceActive.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 18;

    protected virtual QTabButtonAppearance CreateAppearance() => new QTabButtonAppearance();

    protected virtual QTabButtonAppearance CreateAppearanceHot() => new QTabButtonAppearance();

    protected virtual QTabButtonAppearance CreateAppearanceActive() => new QTabButtonAppearance();

    [Description("Gets raised when a property of the configuration is changed")]
    [QWeakEvent]
    [Category("QEvents")]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [Description("Contains the appearance of a regular button. For the configuration on a QTabButton or QTabPage, this property inherits its default values from the Appeareance property from a parenting QTabStripConfiguration.")]
    [QPropertyIndex(15)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QTabButtonAppearance Appearance => this.Properties.GetProperty(15) as QTabButtonAppearance;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the appearance of a hot button. For the configuration on the TabStrip, this AppearanceHot inherits its default values from the Appearance property. For the configuration on a QTabButton or QTabPage, this property inherits its default values from the AppeareanceHot property from a parenting QTabStripConfiguration.")]
    [Category("QAppearance")]
    [QPropertyIndex(16)]
    public QTabButtonAppearance AppearanceHot => this.Properties.GetProperty(16) as QTabButtonAppearance;

    [Description("Gets the appearance of an active button. For the configuration on the TabStrip, this AppearanceActive inherits its default values from the Appearance property. For the configuration on a QTabButton or QTabPage, this property inherits its default values from the AppearanceActive property from a parenting QTabStrpConfiguration.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    public QTabButtonAppearance AppearanceActive => this.Properties.GetProperty(17) as QTabButtonAppearance;

    [QPropertyIndex(1)]
    [Description("Contains the padding between the edge of a button and its contents.")]
    [Category("QAppearance")]
    public QPadding Padding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(2)]
    [Description("Contains the spacing that must be used before and after the button.")]
    public QSpacing Spacing
    {
      get => (QSpacing) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [QPropertyIndex(3)]
    [Description("Contains the spacing before and after the icon.")]
    [Category("QAppearance")]
    public QSpacing IconSpacing
    {
      get => (QSpacing) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [Description("Contains the size of the icons.")]
    [QPropertyIndex(4)]
    [Category("QAppearance")]
    public Size IconSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [QPropertyIndex(5)]
    [Description("Contains the Spacing before and after the text.")]
    [Category("QAppearance")]
    public QSpacing TextSpacing
    {
      get => (QSpacing) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [QPropertyIndex(6)]
    [Description("Indicates the Orientation of a QTabButton. When changing the orientation to VerticalDown or VerticalUp, then the spacings, paddings, sizes, contentOrder and contentAlignments change with it.")]
    [Category("QAppearance")]
    public QContentOrientation Orientation
    {
      get => (QContentOrientation) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [Description("Gets or sets the minimumSize of a button. A value of -1 means it depends on the MinimumSize that is needed to render the QShape associated with the button.")]
    [QPropertyIndex(11)]
    [Category("QAppearance")]
    [Localizable(true)]
    public Size MinimumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(11);
      set => this.Properties.SetProperty(11, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(12)]
    [Description("Gets or sets the maximumSize of a button. A value of -1 means no maximum.")]
    [Localizable(true)]
    public Size MaximumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(12);
      set => this.Properties.SetProperty(12, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(13)]
    [Description("Gets or sets the alignment of the background image. The alignment rotates on the Orientation")]
    public QImageAlign BackgroundImageAlign
    {
      get => (QImageAlign) this.Properties.GetPropertyAsValueType(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    [Description("Gets or sets the whether the backgroundImage must be clipped on the Shape.")]
    [Category("QAppearance")]
    [QPropertyIndex(14)]
    public bool BackgroundImageClip
    {
      get => (bool) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [QPropertyIndex(0)]
    [Category("QAppearance")]
    [Description("Gets or sets the alignment of a QTabButton")]
    public QTabButtonAlignment Alignment
    {
      get => (QTabButtonAlignment) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Description("Contains the Alignment of the Icon. The actual order of the Text and Icon depend on ContentOrder. So if you want to inverse the order you have to set that property.")]
    [QPropertyIndex(9)]
    [Category("QAppearance")]
    public ContentAlignment IconAlignment
    {
      get => (ContentAlignment) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(8)]
    [Description("Gets or sets the Alignment of the Text. The actual order of the Text and Icon depend on ContentOrder. So if you want to inverse the order you have to set that property.")]
    public ContentAlignment TextAlignment
    {
      get => (ContentAlignment) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [QPropertyIndex(10)]
    [Category("QAppearance")]
    [Description("Gets or sets whether the disabled icon must be grayscaled.")]
    public bool GrayscaleDisabledIcon
    {
      get => (bool) this.Properties.GetPropertyAsValueType(10);
      set => this.Properties.SetProperty(10, (object) value);
    }

    [QPropertyIndex(7)]
    [Description("Gets or sets the order of the icon and text of a QTabButton.")]
    [Category("QAppearance")]
    [DefaultValue(QTabButtonContentOrder.IconText)]
    public QTabButtonContentOrder ContentOrder
    {
      get => (QTabButtonContentOrder) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    public Size CalculateBiggestShape(bool horizontal)
    {
      int width = this.Appearance.Shape.MinimumSize.Width;
      int height = this.Appearance.Shape.MinimumSize.Height;
      int val2_1 = Math.Max(this.AppearanceHot.Shape.MinimumSize.Width, width);
      int val2_2 = Math.Max(this.AppearanceHot.Shape.MinimumSize.Height, height);
      int num1 = Math.Max(this.AppearanceActive.Shape.MinimumSize.Width, val2_1);
      int num2 = Math.Max(this.AppearanceActive.Shape.MinimumSize.Height, val2_2);
      return horizontal ? new Size(num1, num2) : new Size(num2, num1);
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Appearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
