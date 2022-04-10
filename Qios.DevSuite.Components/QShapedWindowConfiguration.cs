// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapedWindowConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QShapedWindowConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropAutoMinimumSize = 0;
    protected const int PropShadeVisible = 1;
    protected const int PropShadePosition = 2;
    protected const int PropShadeGradientSize = 3;
    protected const int PropCanSize = 4;
    protected const int PropCanMove = 5;
    protected const int PropCanClose = 6;
    protected const int PropLayered = 7;
    protected const int PropTopMost = 8;
    protected const int PropMinimumSize = 9;
    protected const int PropMaximumSize = 10;
    protected const int PropCloseButtonOffset = 11;
    protected const int PropInheritWindowsSettings = 12;
    protected const int PropCustomCloseButtonMask = 13;
    protected const int PropCloseButtonStyle = 14;
    protected const int PropCustomCloseButtonSize = 15;
    protected const int PropButtonAppearance = 16;
    protected const int PropReceivesMouseMessages = 17;
    protected const int CurrentPropertyCount = 18;
    protected const int TotalPropertyCount = 18;
    internal const int PropIntShadePosition = 2;
    internal const int PropIntShadeVisible = 1;
    internal const int PropIntShadeGradientSize = 3;
    private int m_iSuspendChangedEvent;
    private static Image m_oDefaultCloseButtonMask;
    private EventHandler m_oAppearanceChangedHandler;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public QShapedWindowConfiguration()
    {
      if (QShapedWindowConfiguration.m_oDefaultCloseButtonMask == null)
        QShapedWindowConfiguration.m_oDefaultCloseButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ShapedWindowCloseMask.png"));
      this.m_oAppearanceChangedHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.Properties.DefineProperty(0, (object) true);
      this.Properties.DefineProperty(1, (object) true);
      this.Properties.DefineProperty(2, (object) new Point(3, 3));
      this.Properties.DefineProperty(3, (object) 3);
      this.Properties.DefineProperty(4, (object) true);
      this.Properties.DefineProperty(5, (object) true);
      this.Properties.DefineProperty(6, (object) true);
      this.Properties.DefineProperty(7, (object) true);
      this.Properties.DefineProperty(8, (object) false);
      this.Properties.DefineProperty(9, (object) Size.Empty);
      this.Properties.DefineProperty(10, (object) Size.Empty);
      this.Properties.DefineProperty(11, (object) Point.Empty);
      this.Properties.DefineProperty(12, (object) false);
      this.Properties.DefineProperty(17, (object) true);
      this.Properties.DefineProperty(13, (object) null);
      this.Properties.DefineProperty(14, (object) QButtonStyle.Windows);
      this.Properties.DefineProperty(15, (object) new Size(16, 16));
      this.Properties.DefineResettableProperty(16, (IQResettableValue) new QShapedWindowButtonAppearance());
      this.ButtonAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
    }

    protected override int GetRequestedCount() => 18;

    [QPropertyIndex(16)]
    [Category("QAppearance")]
    [Description("Gets or sets the button appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QShapedWindowButtonAppearance ButtonAppearance
    {
      get => this.Properties.GetProperty(16) as QShapedWindowButtonAppearance;
      set
      {
        if (this.ButtonAppearance == value)
          return;
        if (this.ButtonAppearance != null)
          this.ButtonAppearance.AppearanceChanged -= this.m_oAppearanceChangedHandler;
        this.Properties.SetProperty(16, (object) value);
        if (this.ButtonAppearance == null)
          return;
        this.ButtonAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      }
    }

    [Description("Gets or sets the size of the custom close button")]
    [QPropertyIndex(15)]
    [Category("QAppearance")]
    public Size CustomCloseButtonSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(14)]
    [Description("Gets or sets the style of the close button")]
    public QButtonStyle CloseButtonStyle
    {
      get => (QButtonStyle) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [Browsable(false)]
    public Image UsedCustomCloseButtonMask => this.CustomCloseButtonMask != null ? this.CustomCloseButtonMask : QShapedWindowConfiguration.m_oDefaultCloseButtonMask;

    [QPropertyIndex(13)]
    [Category("QAppearance")]
    [Description("Gets or sets the CloseButton Mask when custom buttons are drawn. In the Mask the Color Red is replaced by the TextColor.")]
    public Image CustomCloseButtonMask
    {
      get => this.Properties.GetProperty(13) as Image;
      set => this.Properties.SetProperty(13, (object) value);
    }

    [Localizable(true)]
    [QPropertyIndex(9)]
    [Description("Gets or sets the minimum size of the window. This will not be used when AutoMinimumSize is true")]
    [Category("QAppearance")]
    public Size MinimumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    [Localizable(true)]
    [Description("Gets or sets the maximum size of the window. This will not be used when AutoMinimumSize is true")]
    [QPropertyIndex(10)]
    [Category("QAppearance")]
    public Size MaximumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(10);
      set => this.Properties.SetProperty(10, (object) value);
    }

    [QPropertyIndex(17)]
    [Category("QAppearance")]
    [Description("Gets or sets if the QShapedWindow receives mouse messages or that the underlying window receives them.")]
    public bool ReceivesMouseMessages
    {
      get => (bool) this.Properties.GetPropertyAsValueType(17);
      set => this.Properties.SetProperty(17, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(7)]
    [Description("Gets or sets if the window is a layered form. Non-layered forms are not anti-aliased, and do not have a dropshadow.")]
    public bool Layered
    {
      get => (bool) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    [QPropertyIndex(8)]
    [Description("Gets or sets whether the window is topmost. This can be used to display the window above the taskbar.")]
    [Category("QAppearance")]
    public bool TopMost
    {
      get => (bool) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [DefaultValue(false)]
    [QPropertyIndex(12)]
    [Category("QAppearance")]
    [Description("Gets or sets whether the QBalloonWindow should inherit WindowsSettings like drawing a shade.")]
    public virtual bool InheritWindowsSettings
    {
      get => (bool) this.Properties.GetPropertyAsValueType(12);
      set => this.Properties.SetProperty(12, (object) value);
    }

    [Description("Gets or sets if the QShapedWindow may set its own MinimumSize, based on the shape and the configuration")]
    [Category("QAppearance")]
    [QPropertyIndex(0)]
    public bool AutoMinimumSize
    {
      get => (bool) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Obsolete("Obsolete since version 1.2.0.40, use the shade properties from the appearance property.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QPropertyIndex(1)]
    [Description("Gets or sets if the window has shading")]
    [Category("QAppearance")]
    public bool ShadeVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Description("Gets or sets the position of the shade.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("QAppearance")]
    [QPropertyIndex(2)]
    [Obsolete("Obsolete since version 1.2.0.40, use the shade properties from the appearance property.")]
    public Point ShadePosition
    {
      get => (Point) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [QPropertyIndex(3)]
    [Description("Contains the size in pixels of the gradient (edges) of the shade.")]
    [Obsolete("Obsolete since version 1.2.0.40, use the shade properties from the appearance property.")]
    public int ShadeGradientSize
    {
      get => (int) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [Description("Gets or sets if the window can size")]
    [Category("QBehavior")]
    [QPropertyIndex(4)]
    public bool CanSize
    {
      get => (bool) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [QPropertyIndex(5)]
    [Category("QBehavior")]
    [Description("Gets or sets if the window can move")]
    public bool CanMove
    {
      get => (bool) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [Description("Gets or sets if the user can close the QShapedWindow.")]
    [Category("QBehavior")]
    [QPropertyIndex(6)]
    public bool CanClose
    {
      get => (bool) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [Category("QBehavior")]
    [QPropertyIndex(11)]
    [Description("Gets or sets the close button offset.")]
    public Point CloseButtonOffset
    {
      get => (Point) this.Properties.GetPropertyAsValueType(11);
      set => this.Properties.SetProperty(11, (object) value);
    }

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    internal void RaiseConfigurationChanged()
    {
      if (this.m_iSuspendChangedEvent != 0)
        return;
      this.OnConfigurationChanged(EventArgs.Empty);
    }

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.RaiseConfigurationChanged();

    private void Appearance_AppearanceChanged(object sender, EventArgs e) => this.RaiseConfigurationChanged();

    internal void SuspendChangedEvent() => ++this.m_iSuspendChangedEvent;

    internal void ResumeChangedEvent()
    {
      if (this.m_iSuspendChangedEvent <= 0)
        return;
      --this.m_iSuspendChangedEvent;
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
  }
}
