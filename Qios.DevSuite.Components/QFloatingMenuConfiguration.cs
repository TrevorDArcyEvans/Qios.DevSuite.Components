// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingMenuConfiguration
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
  [TypeConverter(typeof (QMenuConfigurationTypeConverter))]
  public class QFloatingMenuConfiguration : QMenuConfiguration
  {
    private static Image m_oDefaultDepersonalizeMenuMask;
    private static Image m_oDefaultHasMoreItemsDownMask;
    private static Image m_oDefaultHasMoreItemsUpMask;
    private Image m_oDepersonalizeMenuMask;
    private Image m_oHasMoreItemsDownMask;
    private Image m_oHasMoreItemsUpMask;
    private bool m_bShowToolTips;
    private bool m_bTopMost;
    private bool m_bAnimateMenu = true;
    private bool m_bShowBackgroundShade = true;
    private int m_iAnimateTime = 100;
    private QMenuAnimationType m_eAnimationType = QMenuAnimationType.Slide;
    private bool m_bIconBackgroundVisible;
    private QPadding m_oFloatingMenuPadding = QPadding.Empty;
    private QFloatingMenuShortcutLayout m_eShortcutLayout;

    public QFloatingMenuConfiguration()
    {
      if (QFloatingMenuConfiguration.m_oDefaultDepersonalizeMenuMask != null)
        return;
      QFloatingMenuConfiguration.m_oDefaultDepersonalizeMenuMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.MenuDepersonalizeMask.png"));
      QFloatingMenuConfiguration.m_oDefaultHasMoreItemsDownMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.SmallArrowDownMask.png"));
      QFloatingMenuConfiguration.m_oDefaultHasMoreItemsUpMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.SmallArrowUpMask.png"));
    }

    protected override Image RetrieveDefaultHasChildItemsMask() => (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.SmallArrowRightMask.png"));

    [Description("Gets or sets whether the QFloatingMenu should display tooltips")]
    [RefreshProperties(RefreshProperties.All)]
    [Category("QAppearance")]
    [DefaultValue(false)]
    public virtual bool ShowToolTips
    {
      get => this.m_bShowToolTips;
      set
      {
        this.m_bShowToolTips = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets whether the QFloatingMenu should always be on top")]
    [DefaultValue(false)]
    [Category("QAppearance")]
    public virtual bool TopMost
    {
      get => this.m_bTopMost;
      set
      {
        this.m_bTopMost = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the QFloatingMenu should use animation, this is ignored when the InheritWindowsSettings is true")]
    [Category("QAppearance")]
    public virtual bool AnimateMenu
    {
      get => this.m_bAnimateMenu;
      set
      {
        this.m_bAnimateMenu = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the QFloatingMenu should contain a BackgroundShade. This is ignored when the InheritWindowsSettings is true.")]
    public virtual bool ShowBackgroundShade
    {
      get => this.m_bShowBackgroundShade;
      set
      {
        this.m_bShowBackgroundShade = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public bool UsedShowBackgroundShade
    {
      get
      {
        if (!this.InheritWindowsSettings)
          return this.ShowBackgroundShade;
        return NativeHelper.ShowShadows;
      }
    }

    [DefaultValue(100)]
    [Description("Gets or sets the time that is used for animation (in miliseconds)")]
    [Category("QAppearance")]
    public virtual int AnimateTime
    {
      get => this.m_iAnimateTime;
      set
      {
        this.m_iAnimateTime = value >= 0 ? value : throw new InvalidOperationException(QResources.GetException("QFloatingMenuConfiguration_AnimateTime_NotNegative"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(QMenuAnimationType.Slide)]
    [Description("Gets or sets the type of animation to use. This is ignored when InheritWindowsSettings is true.")]
    public virtual QMenuAnimationType AnimationType
    {
      get => this.m_eAnimationType;
      set
      {
        this.m_eAnimationType = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual QMenuAnimationType UsedAnimationType
    {
      get
      {
        if (this.InheritWindowsSettings)
        {
          if (NativeHelper.AnimateMenus)
            return NativeHelper.AnimateMenusWithFading ? QMenuAnimationType.Fade : QMenuAnimationType.Slide;
        }
        else if (this.AnimateMenu)
          return this.AnimationType;
        return QMenuAnimationType.None;
      }
    }

    [Category("QAppearance")]
    [Description("Indicates if the icon background should be shown")]
    [DefaultValue(true)]
    public virtual bool IconBackgroundVisible
    {
      get => this.m_bIconBackgroundVisible;
      set
      {
        this.m_bIconBackgroundVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public Image UsedDepersonalizeMenuMask => this.m_oDepersonalizeMenuMask != null ? this.m_oDepersonalizeMenuMask : QFloatingMenuConfiguration.m_oDefaultDepersonalizeMenuMask;

    [Description("Contains the base image that is used to show at the bottom of a Menu when it has DepersonalizedMenuItems that are not shown. In the Mask the Color Red is replaced by the TextColor and Black till white are replaced by the DepersonalizeColors Color.")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public virtual Image DepersonalizeMenuMask
    {
      get => this.m_oDepersonalizeMenuMask;
      set
      {
        this.m_oDepersonalizeMenuMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Contains the base image that is used when there are more menu items then visible. This is the case when the menu does not fit vertical on the screen. In the Mask the Color Red is replaced by the TextColor.")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public virtual Image HasMoreItemsDownMask
    {
      get => this.m_oHasMoreItemsDownMask;
      set
      {
        this.m_oHasMoreItemsDownMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Contains the base image that is used when there are more menu items then visible. This is the case when the menu does not fit vertical on the screen. In the Mask the Color Red is replaced by the TextColor.")]
    public virtual Image HasMoreItemsUpMask
    {
      get => this.m_oHasMoreItemsUpMask;
      set
      {
        this.m_oHasMoreItemsUpMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the padding for the FloatingMenu. This padding is the space between the items and the edge of the menu.")]
    [Category("QAppearance")]
    [DefaultValue(typeof (QPadding), "0,0,0,0")]
    public QPadding FloatingMenuPadding
    {
      get => this.m_oFloatingMenuPadding;
      set
      {
        this.m_oFloatingMenuPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(QFloatingMenuShortcutLayout.InTitleColumn)]
    [Description("Gets or sets the ShorcutLayout. This defines if the shortcut is right-aligned in the same column as the Title or in a separate column.")]
    [Category("QAppearance")]
    public QFloatingMenuShortcutLayout ShortcutLayout
    {
      get => this.m_eShortcutLayout;
      set
      {
        this.m_eShortcutLayout = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public Image UsedHasMoreItemsDownMask => this.m_oHasMoreItemsDownMask != null ? this.m_oHasMoreItemsDownMask : QFloatingMenuConfiguration.m_oDefaultHasMoreItemsDownMask;

    [Browsable(false)]
    public Image UsedHasMoreItemsUpMask => this.m_oHasMoreItemsUpMask != null ? this.m_oHasMoreItemsUpMask : QFloatingMenuConfiguration.m_oDefaultHasMoreItemsUpMask;
  }
}
