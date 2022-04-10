// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QScrollBarConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QScrollBarConfiguration : QFastPropertyBagHost
  {
    protected internal const int PropButtonAppearance = 0;
    protected internal const int PropButtonMask = 1;
    protected internal const int PropButtonPadding = 2;
    protected internal const int PropButtonMargin = 3;
    protected internal const int PropScrollAnimated = 4;
    protected internal const int PropScrollOnMouseOver = 5;
    protected internal const int PropTrackButtonMask = 6;
    protected internal const int PropAlwaysDrawBackground = 7;
    protected internal const int PropTrackButtonAppearance = 8;
    protected internal const int PropDirection = 9;
    protected const int CurrentPropertyCount = 10;
    protected const int TotalPropertyCount = 10;
    private static Image m_oDefaultButtonMask;
    private static Image m_oDefaultTrackButtonMask;
    private QWeakDelegate m_oConfigurationChangedDelegate;
    private EventHandler m_oChildObjectsChangedEventHandler;

    public QScrollBarConfiguration()
    {
      if (QScrollBarConfiguration.m_oDefaultButtonMask == null)
        QScrollBarConfiguration.m_oDefaultButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.VerySmallArrowUpMask.png"));
      if (QScrollBarConfiguration.m_oDefaultTrackButtonMask == null)
        QScrollBarConfiguration.m_oDefaultTrackButtonMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.TrackButtonMask.png"));
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
      this.m_oChildObjectsChangedEventHandler = new EventHandler(this.ChildObjects_ObjectChanged);
      this.Properties.DefineProperty(2, (object) new QPadding(4, 4, 4, 4));
      this.Properties.DefineProperty(3, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(1, (object) null);
      this.Properties.DefineProperty(4, (object) true);
      this.Properties.DefineProperty(5, (object) false);
      this.Properties.DefineProperty(6, (object) null);
      this.Properties.DefineProperty(7, (object) QCompositeScrollBarItem.ScrollBar);
      this.Properties.DefineProperty(9, (object) QScrollBarDirection.Vertical);
      this.Properties.DefineResettableProperty(0, (IQResettableValue) this.CreateButtonAppearance());
      this.Properties.DefineResettableProperty(8, (IQResettableValue) this.CreateTrackButtonAppearance());
      this.ButtonAppearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
      this.TrackButtonAppearance.AppearanceChanged += this.m_oChildObjectsChangedEventHandler;
    }

    protected virtual QScrollBarButtonAppearance CreateTrackButtonAppearance() => new QScrollBarButtonAppearance();

    protected virtual QScrollBarButtonAppearance CreateButtonAppearance() => new QScrollBarButtonAppearance();

    protected override int GetRequestedCount() => 10;

    [Description("Gets raised when a property of the configuration is changed")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    [QPropertyIndex(9)]
    [Description("Gets or sets the direction of the scrollbar.")]
    [Category("QAppearance")]
    public virtual QScrollBarDirection Direction
    {
      get => (QScrollBarDirection) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [QPropertyIndex(7)]
    [Category("QAppearance")]
    [Description("Gets or sets which items have a seperate hot style.")]
    public virtual QCompositeScrollBarItem AlwaysDrawBackground
    {
      get => (QCompositeScrollBarItem) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    [QPropertyIndex(2)]
    [Description("Gets or sets the padding of the scrollbutton.")]
    [Category("QAppearance")]
    public virtual QPadding ButtonPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [QPropertyIndex(3)]
    [Description("Gets or sets the margin of the scrollbutton.")]
    [Category("QAppearance")]
    public virtual QMargin ButtonMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [QPropertyIndex(4)]
    [Category("QAppearance")]
    [Description("Gets or sets whether scrolling must happen animated.")]
    public virtual bool ScrollAnimated
    {
      get => (bool) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(5)]
    [Description("Gets or sets whether there must be scrolled when the user hovers with the mouse over a scroll button")]
    public virtual bool ScrollOnMouseOver
    {
      get => (bool) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [QPropertyIndex(1)]
    [Description("Gets or sets the mask of the scrollbutton.")]
    [Category("QAppearance")]
    public virtual Image ButtonMask
    {
      get => this.Properties.GetProperty(1) as Image;
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Browsable(false)]
    public Image UsedButtonMask => this.ButtonMask == null ? QScrollBarConfiguration.m_oDefaultButtonMask : this.ButtonMask;

    [Description("Gets or sets the mask of the trackbutton.")]
    [Category("QAppearance")]
    [QPropertyIndex(6)]
    public virtual Image TrackButtonMask
    {
      get => this.Properties.GetProperty(6) as Image;
      set => this.Properties.SetProperty(6, (object) value);
    }

    [Browsable(false)]
    public Image UsedTrackButtonMask => this.TrackButtonMask == null ? QScrollBarConfiguration.m_oDefaultTrackButtonMask : this.TrackButtonMask;

    [Description("Gets the appearance of a trackbutton on a QComposite or QCompositeGroup.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(8)]
    public virtual QShapeAppearance TrackButtonAppearance => this.Properties.GetProperty(8) as QShapeAppearance;

    [Category("QAppearance")]
    [Description("Gets the appearance of a scrollbutton on a QComposite or QCompositeGroup.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(0)]
    public virtual QShapeAppearance ButtonAppearance => this.Properties.GetProperty(0) as QShapeAppearance;

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Properties_PropertyChanged(object sender, QFastPropertyChangedEventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void ChildObjects_ObjectChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
