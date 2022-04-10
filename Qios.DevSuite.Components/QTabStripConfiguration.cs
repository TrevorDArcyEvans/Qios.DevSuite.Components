// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStripConfiguration
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
  public class QTabStripConfiguration : QFastPropertyBagHost, IDisposable
  {
    protected const int PropAllowDrop = 0;
    protected const int PropAllowDrag = 1;
    protected const int PropStripVisibleWithoutButtons = 2;
    protected const int PropStripMinimumHeight = 3;
    protected const int PropStripPadding = 4;
    protected const int PropStripMargin = 5;
    protected const int PropButtonAreaMargin = 6;
    protected const int PropButtonAreaClip = 7;
    protected const int PropButtonSpacing = 8;
    protected const int PropSizeBehavior = 9;
    protected const int PropFontStyleHot = 10;
    protected const int PropFontStyleActive = 11;
    protected const int PropCloseButtonVisible = 12;
    protected const int PropScrollButtonsAlwaysVisible = 13;
    protected const int PropNavigationAreaPadding = 14;
    protected const int PropNavigationAreaMargin = 15;
    protected const int PropNavigationAreaAlignment = 16;
    protected const int PropNavigationAreaContentAlignment = 17;
    protected const int PropCloseMask = 18;
    protected const int PropScrollLeftMask = 19;
    protected const int PropScrollRightMask = 20;
    protected const int PropScrollStep = 21;
    protected const int PropScrollMargin = 22;
    protected const int PropScrollBehavior = 23;
    protected const int PropStackBehavior = 24;
    protected const int PropAppearance = 25;
    protected const int PropNavigationAreaAppearance = 26;
    protected const int PropNavigationButtonHotAppearance = 27;
    protected const int PropButtonConfiguration = 28;
    protected const int PropNavigationButtonPadding = 29;
    protected const int CurrentPropertyCount = 30;
    protected const int TotalPropertyCount = 30;
    private Font m_oFont;
    private Font m_oFontHot;
    private Font m_oFontActive;
    private static Image m_oDefaultScrollLeftMask;
    private static Image m_oDefaultScrollRightMask;
    private static Image m_oDefaultCloseTabMask;
    private EventHandler m_oAppearanceChangedHandler;
    private EventHandler m_oButtonConfigurationChangedHandler;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a property of the configuration is changed")]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public QTabStripConfiguration(Font font)
    {
      if (QTabStripConfiguration.m_oDefaultCloseTabMask == null)
      {
        QTabStripConfiguration.m_oDefaultCloseTabMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.TabCloseMask.png"));
        QTabStripConfiguration.m_oDefaultScrollLeftMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.TabScrollLeftMask.png"));
        QTabStripConfiguration.m_oDefaultScrollRightMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.TabScrollRightMask.png"));
      }
      this.m_oAppearanceChangedHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.m_oButtonConfigurationChangedHandler = new EventHandler(this.ButtonConfiguration_ConfigurationChanged);
      this.Properties.DefineProperty(0, (object) true);
      this.Properties.DefineProperty(1, (object) true);
      this.Properties.DefineProperty(2, (object) false);
      this.Properties.DefineProperty(3, (object) 20);
      this.Properties.DefineProperty(4, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineProperty(5, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(6, (object) new QMargin(0, 5, 0, 0));
      this.Properties.DefineProperty(7, (object) false);
      this.Properties.DefineProperty(8, (object) -15);
      this.Properties.DefineProperty(9, (object) QTabStripSizeBehaviors.Scroll);
      this.Properties.DefineProperty(10, (object) (QFontStyle) FontStyle.Underline);
      this.Properties.DefineProperty(11, (object) (QFontStyle) FontStyle.Bold);
      this.Properties.DefineProperty(12, (object) false);
      this.Properties.DefineProperty(13, (object) false);
      this.Properties.DefineProperty(14, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineProperty(15, (object) new QMargin(0, 0, 0, 0));
      this.Properties.DefineProperty(16, (object) QContentAlignment.Stretched);
      this.Properties.DefineProperty(17, (object) QContentAlignment.Far);
      this.Properties.DefineProperty(18, (object) null);
      this.Properties.DefineProperty(19, (object) null);
      this.Properties.DefineProperty(20, (object) null);
      this.Properties.DefineProperty(21, (object) 20);
      this.Properties.DefineProperty(22, (object) 20);
      this.Properties.DefineProperty(23, (object) QTabStripScrollBehaviors.Animate);
      this.Properties.DefineProperty(24, (object) (QTabStripStackBehaviors.ExtendButtonBackground | QTabStripStackBehaviors.MoveRowToFrontOnActivate));
      this.Properties.DefineProperty(29, (object) new QPadding(0, 0, 0, 0));
      this.Properties.DefineResettableProperty(25, (IQResettableValue) this.CreateTabStripAppearance());
      this.Properties.DefineResettableProperty(26, (IQResettableValue) this.CreateNavigationAreaAppearance());
      this.Properties.DefineResettableProperty(27, (IQResettableValue) this.CreateNavigationButtonHotAppearance());
      this.Properties.DefineResettableProperty(28, (IQResettableValue) this.CreateTabButtonConfiguration());
      this.Appearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.NavigationAreaAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.UsedNavigationButtonHotAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      this.ButtonConfiguration.ConfigurationChanged += this.m_oButtonConfigurationChangedHandler;
      this.Properties.PropertyChanged += new QFastPropertyChangedEventHandler(this.Properties_PropertyChanged);
      this.PutFont(font);
    }

    protected override int GetRequestedCount() => 30;

    protected virtual QTabStripAppearance CreateTabStripAppearance() => new QTabStripAppearance();

    protected virtual QTabButtonConfiguration CreateTabButtonConfiguration() => new QTabButtonConfiguration();

    protected virtual QTabStripNavigationAreaAppearance CreateNavigationAreaAppearance() => new QTabStripNavigationAreaAppearance();

    protected virtual QAppearanceBase CreateNavigationButtonHotAppearance() => (QAppearanceBase) new QAppearance();

    [QPropertyIndex(1)]
    [Description("Gets or sets a value indicating whether the user can drag QTabButtons from this QTabControl.")]
    [Category("QBehavior")]
    public bool AllowDrag
    {
      get => (bool) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [QPropertyIndex(0)]
    [Category("QBehavior")]
    [Description("Gets or sets a value indicating whether the user can drag QTabButtons on this QTabControl.")]
    public bool AllowDrop
    {
      get => (bool) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Description("Contains the padding between the edge of the TabStrip and the buttons. The bottomPadding, when the strip is docked top, is not used, visa versa.")]
    [Category("QAppearance")]
    [QPropertyIndex(4)]
    public QPadding StripPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(5)]
    [Description("Contains the margin between the edge of the TabControl and the TabStrip. The BottomMargin, when the strip is docked top, is not used, visa versa.")]
    public QMargin StripMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(6)]
    [Description("Contains the margin between the of the content rectangle (plus padding) and the start of the buttons. The BottomMargin, when the strip is docked top, is not used, visa versa.")]
    public QMargin ButtonAreaMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [QPropertyIndex(7)]
    [Description("Gets or sets whether the buttons should be clipped when exceeding left or right in the buttonarea.")]
    [Category("QAppearance")]
    public bool ButtonAreaClip
    {
      get => (bool) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    [Description("Contains the minimumHeight of the strip")]
    [QPropertyIndex(3)]
    [Category("QAppearance")]
    public int StripMinimumHeight
    {
      get => (int) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [Description("Contains whether the strip must be visible when no buttons are shown.")]
    [QPropertyIndex(2)]
    [Category("QAppearance")]
    public bool StripVisibleWithoutButtons
    {
      get => (bool) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(8)]
    [Description("Contains the spacing between buttons. This spacing is not used before the first and after the last button.")]
    public int ButtonSpacing
    {
      get => (int) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [QPropertyIndex(9)]
    [Description("Defines how the QTabStrip should behave when the buttons do not fit.")]
    [Category("QAppearance")]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    public QTabStripSizeBehaviors SizeBehavior
    {
      get => (QTabStripSizeBehaviors) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }

    internal bool IsScroll => (this.SizeBehavior & QTabStripSizeBehaviors.Scroll) == QTabStripSizeBehaviors.Scroll;

    internal bool IsShrink => (this.SizeBehavior & QTabStripSizeBehaviors.Shrink) == QTabStripSizeBehaviors.Shrink;

    internal bool IsStack => (this.SizeBehavior & QTabStripSizeBehaviors.Stack) == QTabStripSizeBehaviors.Stack;

    internal bool IsGrow => (this.SizeBehavior & QTabStripSizeBehaviors.Grow) == QTabStripSizeBehaviors.Grow;

    [QPropertyIndex(28)]
    [Description("Gets or sets the configuration of the buttons.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QTabButtonConfiguration ButtonConfiguration
    {
      get => this.Properties.GetProperty(28) as QTabButtonConfiguration;
      set
      {
        if (this.ButtonConfiguration == value)
          return;
        if (this.ButtonConfiguration != null)
          this.ButtonConfiguration.ConfigurationChanged -= this.m_oButtonConfigurationChangedHandler;
        this.Properties.SetProperty(28, (object) value);
        if (this.ButtonConfiguration == null)
          return;
        this.ButtonConfiguration.ConfigurationChanged += this.m_oButtonConfigurationChangedHandler;
      }
    }

    [Category("QAppearance")]
    [Description("Gets the appearance of the TabStrip.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(25)]
    public QTabStripAppearance Appearance
    {
      get => this.Properties.GetProperty(25) as QTabStripAppearance;
      set
      {
        if (this.Appearance == value)
          return;
        if (this.Appearance != null)
          this.Appearance.AppearanceChanged -= this.m_oAppearanceChangedHandler;
        this.Properties.SetProperty(25, (object) value);
        if (this.Appearance == null)
          return;
        this.Appearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the appearance of the TabStrip.")]
    [Category("QAppearance")]
    [QPropertyIndex(26)]
    public QTabStripNavigationAreaAppearance NavigationAreaAppearance
    {
      get => this.Properties.GetProperty(26) as QTabStripNavigationAreaAppearance;
      set
      {
        if (this.NavigationAreaAppearance == value)
          return;
        if (this.NavigationAreaAppearance != null)
          this.NavigationAreaAppearance.AppearanceChanged -= this.m_oAppearanceChangedHandler;
        this.Properties.SetProperty(26, (object) value);
        if (this.NavigationAreaAppearance == null)
          return;
        this.NavigationAreaAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QPropertyIndex(27)]
    [Description("Gets the appearance of the hot navigation buttons.")]
    [Category("QAppearance")]
    public QAppearance NavigationButtonHotAppearance
    {
      get => this.Properties.GetProperty(27) as QAppearance;
      set => this.UsedNavigationButtonHotAppearance = (QAppearanceBase) value;
    }

    [Browsable(false)]
    public QAppearanceBase UsedNavigationButtonHotAppearance
    {
      get => this.Properties.GetProperty(27) as QAppearanceBase;
      set
      {
        if (this.UsedNavigationButtonHotAppearance == value)
          return;
        if (this.UsedNavigationButtonHotAppearance != null)
          this.UsedNavigationButtonHotAppearance.AppearanceChanged -= this.m_oAppearanceChangedHandler;
        this.Properties.SetProperty(27, (object) value);
        if (this.UsedNavigationButtonHotAppearance == null)
          return;
        this.UsedNavigationButtonHotAppearance.AppearanceChanged += this.m_oAppearanceChangedHandler;
      }
    }

    [Description("Gets or sets the style of the font when the button is hot.")]
    [QPropertyIndex(10)]
    [Category("QAppearance")]
    public QFontStyle FontStyleHot
    {
      get => (QFontStyle) this.Properties.GetPropertyAsValueType(10);
      set
      {
        if (this.FontStyleHot == value)
          return;
        if (this.m_oFont != null)
          this.m_oFontHot = new QFontDefinition(value).GetFontFromCache(this.m_oFont);
        this.Properties.SetProperty(10, (object) value);
      }
    }

    [Description("Gets or sets the style of the font when the button is activated.")]
    [Category("QAppearance")]
    [QPropertyIndex(11)]
    public QFontStyle FontStyleActive
    {
      get => (QFontStyle) this.Properties.GetPropertyAsValueType(11);
      set
      {
        if (this.FontStyleActive == value)
          return;
        if (this.m_oFont != null)
          this.m_oFontActive = new QFontDefinition(value).GetFontFromCache(this.m_oFont);
        this.Properties.SetProperty(11, (object) value);
      }
    }

    [Browsable(false)]
    public Font Font => this.m_oFont;

    [Browsable(false)]
    public Font FontActive => this.m_oFontActive;

    [Browsable(false)]
    public Font FontHot => this.m_oFontHot;

    public void PutFont(Font font)
    {
      this.m_oFont = font;
      this.m_oFontHot = new QFontDefinition(this.FontStyleHot).GetFontFromCache(this.m_oFont);
      this.m_oFontActive = new QFontDefinition(this.FontStyleActive).GetFontFromCache(this.m_oFont);
      this.OnConfigurationChanged(EventArgs.Empty);
    }

    [QPropertyIndex(12)]
    [Description("Gets or sets whether the buttons can be closed. To actually be able to close a button QTabButton.CanClose must be specified.")]
    [Category("QAppearance")]
    public bool CloseButtonVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(12);
      set => this.Properties.SetProperty(12, (object) value);
    }

    [QPropertyIndex(13)]
    [Description("Defines whether the scrollbuttons must always be visible. This is ignored if the SizeBehavior does not contain QTabStripSizeBehaviors.Scroll.")]
    [Category("QAppearance")]
    public bool ScrollButtonsAlwaysVisible
    {
      get => (bool) this.Properties.GetPropertyAsValueType(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(14)]
    [Description("Defines the padding used for the navigation buttons like the scroll and close buttons.")]
    public QPadding NavigationAreaPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [QPropertyIndex(15)]
    [Category("QAppearance")]
    [Description("Gets or sets the margin used between the actual content area of the TabStrip and the start of the NavigationArea.")]
    public QMargin NavigationAreaMargin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(15);
      set => this.Properties.SetProperty(15, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(16)]
    [Description("Defines the alignment for bounding shape of custom buttons like the scroll and close buttons.")]
    public QContentAlignment NavigationAreaAlignment
    {
      get => (QContentAlignment) this.Properties.GetPropertyAsValueType(16);
      set => this.Properties.SetProperty(16, (object) value);
    }

    [Description("Defines the alignment for the custom buttons like the scroll and close buttons.")]
    [Category("QAppearance")]
    [QPropertyIndex(17)]
    public QContentAlignment NavigationAreaContentAlignment
    {
      get => (QContentAlignment) this.Properties.GetPropertyAsValueType(17);
      set
      {
        if (value == QContentAlignment.Stretched)
          throw new InvalidOperationException(QResources.GetException("QTabStripConfiguration_NavigationAreaContentAlignment_CannotStretch"));
        this.Properties.SetProperty(17, (object) value);
      }
    }

    [Browsable(false)]
    public virtual Image UsedCloseMask => this.CloseMask != null ? this.CloseMask : QTabStripConfiguration.m_oDefaultCloseTabMask;

    [Description("Contains the base image that is used to for the Close button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    [Category("QAppearance")]
    [QPropertyIndex(18)]
    public Image CloseMask
    {
      get => this.Properties.GetProperty(18) as Image;
      set => this.Properties.SetProperty(18, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedScrollLeftMask => this.ScrollLeftMask != null ? this.ScrollLeftMask : QTabStripConfiguration.m_oDefaultScrollLeftMask;

    [Description("Contains the base image that is used to for the Scroll left button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    [Category("QAppearance")]
    [QPropertyIndex(19)]
    public Image ScrollLeftMask
    {
      get => this.Properties.GetProperty(19) as Image;
      set => this.Properties.SetProperty(19, (object) value);
    }

    [Browsable(false)]
    public virtual Image UsedScrollRightMask => this.ScrollRightMask != null ? this.ScrollRightMask : QTabStripConfiguration.m_oDefaultScrollRightMask;

    [QPropertyIndex(20)]
    [Category("QAppearance")]
    [Description("Contains the base image that is used to for the Scroll right button. In the Mask the Color Red is replaced by the TabButtonTextColor.")]
    public Image ScrollRightMask
    {
      get => this.Properties.GetProperty(20) as Image;
      set => this.Properties.SetProperty(20, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(21)]
    [Description("Contains the default scroll step when scrolling is used.")]
    public int ScrollStep
    {
      get => (int) this.Properties.GetPropertyAsValueType(21);
      set => this.Properties.SetProperty(21, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(22)]
    [Description("Contains the margin that is used when scrolling a button into the view.")]
    public int ScrollMargin
    {
      get => (int) this.Properties.GetPropertyAsValueType(22);
      set => this.Properties.SetProperty(22, (object) value);
    }

    [QPropertyIndex(23)]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [Category("QAppearance")]
    [Description("Defines the scroll behavior when scrolling is used.")]
    public QTabStripScrollBehaviors ScrollBehavior
    {
      get => (QTabStripScrollBehaviors) this.Properties.GetPropertyAsValueType(23);
      set => this.Properties.SetProperty(23, (object) value);
    }

    [QPropertyIndex(24)]
    [Category("QAppearance")]
    [Description("the stack behavior when stacking is used.")]
    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    public QTabStripStackBehaviors StackBehavior
    {
      get => (QTabStripStackBehaviors) this.Properties.GetPropertyAsValueType(24);
      set => this.Properties.SetProperty(24, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the padding used for buttons on the NavigationArea.")]
    [QPropertyIndex(29)]
    public QPadding NavigationButtonPadding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(29);
      set => this.Properties.SetProperty(29, (object) value);
    }

    [Browsable(false)]
    public bool UseStackExtendBackground => this.IsStack && (this.StackBehavior & QTabStripStackBehaviors.ExtendButtonBackground) == QTabStripStackBehaviors.ExtendButtonBackground;

    [Browsable(false)]
    public bool UseStackMoveToFront => this.IsStack && (this.StackBehavior & QTabStripStackBehaviors.MoveRowToFrontOnActivate) == QTabStripStackBehaviors.MoveRowToFrontOnActivate;

    [Browsable(false)]
    public bool UseScrollAnimation => this.IsScroll && (this.ScrollBehavior & QTabStripScrollBehaviors.Animate) == QTabStripScrollBehaviors.Animate;

    [Browsable(false)]
    public bool UseScrollOneButton => this.IsScroll && (this.ScrollBehavior & QTabStripScrollBehaviors.OneButton) == QTabStripScrollBehaviors.OneButton;

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void Appearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void ButtonConfiguration_ConfigurationChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

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

    ~QTabStripConfiguration() => this.Dispose(false);
  }
}
