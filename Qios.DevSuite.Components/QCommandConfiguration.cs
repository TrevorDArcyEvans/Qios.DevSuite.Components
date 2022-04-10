// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QCommandConfigurationTypeConverter))]
  public class QCommandConfiguration : IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bInheritWindowsSettings;
    private QCommandAppearance m_oActivatedItemAppearance;
    private QHotkeyVisibilityType m_eHotkeyVisibilityType;
    private int m_iExpandingDelay = 200;
    private int m_iDepersonalizeDelay = 2000;
    private bool m_bUseExpandingDelay = true;
    private int m_iMinimumItemHeight = 18;
    private QSpacing m_oControlSpacing;
    private QSpacing m_oIconSpacing;
    private QSpacing m_oTitleSpacing;
    private QSpacing m_oShortcutSpacing;
    private int m_iSeparatorSize;
    private int m_iSeparatorRelativeStart;
    private int m_iSeparatorRelativeEnd;
    private QSpacing m_oSeparatorSpacing;
    private QMargin m_oItemMargin;
    private QPadding m_oItemPadding;
    private Size m_oIconSize;
    private bool m_bIconsVisible;
    private bool m_bTitlesVisible;
    private bool m_bShortcutsVisible;
    private Image m_oDefaultHasChildItemsMask;
    private Image m_oHasChildItemsMask;
    private QSpacing m_oHasChildItemsSpacing;
    private bool m_bHasChildItemsImageVisible;
    private Image m_oSeparatorMask;
    private QPersonalizedItemBehavior m_ePersonalizedItemBehavior;
    private QWeakDelegate m_oConfigurationChangedDelegate;

    [Description("Gets raised when a property of the configuration is changed")]
    [Category("QEvents")]
    [QWeakEvent]
    public event EventHandler ConfigurationChanged
    {
      add => this.m_oConfigurationChangedDelegate = QWeakDelegate.Combine(this.m_oConfigurationChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oConfigurationChangedDelegate = QWeakDelegate.Remove(this.m_oConfigurationChangedDelegate, (Delegate) value);
    }

    public QCommandConfiguration() => this.SetToDefaultValues();

    protected virtual QCommandAppearance CreateCommandAppearance() => new QCommandAppearance();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public bool ShouldSerializeActivatedItemAppearance() => !this.ActivatedItemAppearance.IsSetToDefaultValues();

    public void ResetActivatedItemAppearance() => this.ActivatedItemAppearance.SetToDefaultValues();

    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the appearance of activated items")]
    public QCommandAppearance ActivatedItemAppearance
    {
      get
      {
        if (this.m_oActivatedItemAppearance == null)
        {
          this.m_oActivatedItemAppearance = this.CreateCommandAppearance();
          this.m_oActivatedItemAppearance.AppearanceChanged += new EventHandler(this.ActivatedItemAppearance_AppearanceChanged);
        }
        return this.m_oActivatedItemAppearance;
      }
    }

    [Description("Gets or sets the the prefered IconSize")]
    [Category("QAppearance")]
    [DefaultValue(typeof (Size), "16,16")]
    public virtual Size IconSize
    {
      get => this.m_oIconSize;
      set
      {
        this.m_oIconSize = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets if the Hotkey should always be visible, only when Alt is pressed or never. This is not used when the InheritWindowsSettings is true.")]
    [DefaultValue(QHotkeyVisibilityType.Always)]
    [Category("QAppearance")]
    public QHotkeyVisibilityType HotkeyVisibilityType
    {
      get => this.m_eHotkeyVisibilityType;
      set
      {
        this.m_eHotkeyVisibilityType = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public QHotkeyVisibilityType UsedHotkeyVisibilityType
    {
      get
      {
        if (!this.InheritWindowsSettings)
          return this.HotkeyVisibilityType;
        return NativeHelper.LettersAlwaysUnderlined ? QHotkeyVisibilityType.Always : QHotkeyVisibilityType.WhenAltIsPressed;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether the QMenu should inherit WindowsSettings like drawing a shade or animating a menu.")]
    [Category("QAppearance")]
    public virtual bool InheritWindowsSettings
    {
      get => this.m_bInheritWindowsSettings;
      set
      {
        this.m_bInheritWindowsSettings = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether icons should be shown.")]
    public virtual bool IconsVisible
    {
      get => this.m_bIconsVisible;
      set
      {
        this.m_bIconsVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets if titles should be shown.")]
    [DefaultValue(true)]
    [Category("QAppearance")]
    public virtual bool TitlesVisible
    {
      get => this.m_bTitlesVisible;
      set
      {
        this.m_bTitlesVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if shortcuts should be shown.")]
    [DefaultValue(true)]
    public virtual bool ShortcutsVisible
    {
      get => this.m_bShortcutsVisible;
      set
      {
        this.m_bShortcutsVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(typeof (QMargin), "1,1,1,1")]
    [Description("Gets or sets the margin between separate items")]
    public virtual QMargin ItemMargin
    {
      get => this.m_oItemMargin;
      set
      {
        this.m_oItemMargin = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the padding between the bounds of an item and the contents of the item")]
    [DefaultValue(typeof (QPadding), "2,2,2,2")]
    [Category("QAppearance")]
    public virtual QPadding ItemPadding
    {
      get => this.m_oItemPadding;
      set
      {
        this.m_oItemPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QSpacing), "1,3")]
    [Description("Gets or sets the spacing for the icons")]
    [Category("QAppearance")]
    public virtual QSpacing IconSpacing
    {
      get => this.m_oIconSpacing;
      set
      {
        this.m_oIconSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QSpacing), "0,0")]
    [Category("QAppearance")]
    [Description("Gets or sets the spacing for the control")]
    public virtual QSpacing ControlSpacing
    {
      get => this.m_oControlSpacing;
      set
      {
        this.m_oControlSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(typeof (QSpacing), "5,10")]
    [Description("Gets or sets the spacing for the titles")]
    public virtual QSpacing TitleSpacing
    {
      get => this.m_oTitleSpacing;
      set
      {
        this.m_oTitleSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the spacing for the shortcuts")]
    [DefaultValue(typeof (QSpacing), "1,1")]
    [Category("QAppearance")]
    public virtual QSpacing ShortcutSpacing
    {
      get => this.m_oShortcutSpacing;
      set
      {
        this.m_oShortcutSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("gets or sets the spacing for the separators")]
    [Category("QAppearance")]
    [DefaultValue(typeof (QSpacing), "0,0")]
    public virtual QSpacing SeparatorSpacing
    {
      get => this.m_oSeparatorSpacing;
      set
      {
        this.m_oSeparatorSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the time that should pass before a hot Item becomes an expanded Item. This is ignored when InheritWindowsSettings is true or UseExpandingDelay is false.")]
    [Category("QAppearance")]
    [DefaultValue(200)]
    public virtual int ExpandingDelay
    {
      get => this.m_iExpandingDelay;
      set
      {
        this.m_iExpandingDelay = value >= 0 ? value : throw new InvalidOperationException(QResources.GetException("QCommandConfiguration_ExpandingDelay_Not_Negative"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the QCommandContainer should use the ExpandingDelay.")]
    public virtual bool UseExpandingDelay
    {
      get => this.m_bUseExpandingDelay;
      set
      {
        this.m_bUseExpandingDelay = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(18)]
    [Description("Gets or sets the minimum height of an Item")]
    public virtual int MinimumItemHeight
    {
      get => this.m_iMinimumItemHeight;
      set
      {
        this.m_iMinimumItemHeight = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(1)]
    [Description("Gets or sets the size of a separator")]
    public virtual int SeparatorSize
    {
      get => this.m_iSeparatorSize;
      set
      {
        this.m_iSeparatorSize = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    public int CalculateSeparatorSize(QCommandOrientation separatorOrientation)
    {
      if (this.SeparatorMask == null)
        return this.SeparatorSize;
      return separatorOrientation == QCommandOrientation.Horizontal ? this.SeparatorMask.Width : this.SeparatorMask.Height;
    }

    [Description("Gets or sets the start of the separator line relative to the default start position.")]
    [DefaultValue(0)]
    [Category("QAppearance")]
    public virtual int SeparatorRelativeStart
    {
      get => this.m_iSeparatorRelativeStart;
      set
      {
        this.m_iSeparatorRelativeStart = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(0)]
    [Description("Gets or sets the end of the separator line relative to the default end position.")]
    [Category("QAppearance")]
    public virtual int SeparatorRelativeEnd
    {
      get => this.m_iSeparatorRelativeEnd;
      set
      {
        this.m_iSeparatorRelativeEnd = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual int UsedExpandingDelay
    {
      get
      {
        if (!this.UseExpandingDelay)
          return 0;
        return this.InheritWindowsSettings ? NativeHelper.MenuShowDelay : this.ExpandingDelay;
      }
    }

    [DefaultValue(2000)]
    [Description("Gets or sets the time a use has to hover above the DepersonalizeItem of a QMenu before the menu gets unpersonalized")]
    [Category("QAppearance")]
    public virtual int DepersonalizeDelay
    {
      get => this.m_iDepersonalizeDelay;
      set
      {
        this.m_iDepersonalizeDelay = value >= 0 ? value : throw new InvalidOperationException(QResources.GetException("QCommandConfiguration_DepersonalizeDelay_Not_Negative"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual Image UsedHasChildItemsMask
    {
      get
      {
        if (this.m_oHasChildItemsMask != null)
          return this.m_oHasChildItemsMask;
        if (this.m_oDefaultHasChildItemsMask == null)
          this.m_oDefaultHasChildItemsMask = this.RetrieveDefaultHasChildItemsMask();
        return this.m_oDefaultHasChildItemsMask;
      }
    }

    [DefaultValue(null)]
    [Description("Contains the Base Image that is used to show if a MenuItem has ChildItems")]
    [Category("QAppearance")]
    public virtual Image HasChildItemsMask
    {
      get => this.m_oHasChildItemsMask;
      set
      {
        this.m_oHasChildItemsMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(null)]
    [Description("Contains the base image that is used for the separator. The mask is repeated for the length of the separator. In the Mask the Color Red is replaced by the SeparatorColor. When this is not set a line is drawn.")]
    [Category("QAppearance")]
    public virtual Image SeparatorMask
    {
      get => this.m_oSeparatorMask;
      set
      {
        this.m_oSeparatorMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the Spacing for the HasChildItemsIcon")]
    [Category("QAppearance")]
    [DefaultValue(typeof (QSpacing), "1,1")]
    public virtual QSpacing HasChildItemsSpacing
    {
      get => this.m_oHasChildItemsSpacing;
      set
      {
        this.m_oHasChildItemsSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    [Category("QAppearance")]
    [Description("Gets if the HasChildItemsIcons should be shown when there are child items.")]
    public virtual bool HasChildItemsImageVisible
    {
      get => this.m_bHasChildItemsImageVisible;
      set
      {
        this.m_bHasChildItemsImageVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(QPersonalizedItemBehavior.DependsOnPersonalized)]
    [Description("Gets or sets the behavior on how to deal with personalized items.")]
    public virtual QPersonalizedItemBehavior PersonalizedItemBehavior
    {
      get => this.m_ePersonalizedItemBehavior;
      set
      {
        this.m_ePersonalizedItemBehavior = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    protected virtual Image RetrieveDefaultHasChildItemsMask() => (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.VerySmallArrowDownMask.png"));

    public void SetToDefaultValues() => QMisc.SetToDefaultValues((object) this);

    public bool IsSetToDefaultValues() => QMisc.IsSetToDefaultValues((object) this);

    protected virtual void OnConfigurationChanged(EventArgs e) => this.m_oConfigurationChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oConfigurationChangedDelegate, (object) this, (object) e);

    private void ActivatedItemAppearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
