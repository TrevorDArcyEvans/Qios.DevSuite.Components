// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarConfiguration
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
  public class QToolBarConfiguration : QMenuConfiguration
  {
    private bool m_bExpandOnItemClick = true;
    private static Image m_oDefaultHasMoreItemsMask;
    private Image m_oHasMoreItemsMask;
    private QShortcutScope m_eShortcutScope = QShortcutScope.Application;
    private static Image m_oDefaultCustomizeToolBarMask;
    private Image m_oCustomizeToolBarMask;
    private static Image m_oDefaultFormCustomizeToolBarMask;
    private Image m_oFormCustomizeToolBarMask;
    private static Image m_oDefaultFormCloseToolBarMask;
    private Image m_oFormCloseToolBarMask;
    private int m_iRoundedBevelCornerSize;
    private QToolBarLayoutType m_eToolBarLayoutType;
    private bool m_bCanMove;
    private bool m_bCanFloat;
    private bool m_bShowCustomizeBar;
    private bool m_bCanCustomize;
    private bool m_bCanClose;
    private bool m_bStretched;
    private bool m_bHideOnClose = true;
    private QSizingGripStyle m_eSizingGripStyle;
    private int m_iHasMoreItemsAreaWidth;
    private int m_iSizingGripWidth;
    private QToolBarStyle m_eToolBarStyle = QToolBarStyle.Beveled;
    private QPadding m_oToolBarPadding;
    private QPadding m_oSizingGripPadding;
    private QPadding m_oFormButtonsPadding;
    private QSpacing m_oItemsSpacing;
    private string m_sCustomizeItemTitle;
    private string m_sCustomizeItemTooltip;
    private bool m_bShowDefaultCustomizeItems;

    public QToolBarConfiguration()
    {
      if (QToolBarConfiguration.m_oDefaultCustomizeToolBarMask != null)
        return;
      QToolBarConfiguration.m_oDefaultCustomizeToolBarMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ToolBarCustomize.png"));
      QToolBarConfiguration.m_oDefaultHasMoreItemsMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ToolBarHasMoreItems.png"));
      QToolBarConfiguration.m_oDefaultFormCloseToolBarMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.MdiCloseMask.png"));
      QToolBarConfiguration.m_oDefaultFormCustomizeToolBarMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.SmallArrowDownMask.png"));
    }

    [Description("Gets or sets whether the item expands if the use clicks on it, or only if the user clicks on the HasChildItemsImage")]
    [DefaultValue(false)]
    [Category("QAppearance")]
    public virtual bool ExpandOnItemClick
    {
      get => this.m_bExpandOnItemClick;
      set
      {
        this.m_bExpandOnItemClick = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(QShortcutScope.Application)]
    [Description("Gets or sets when Shortcuts must react. This can be on the complete application or parent Form.")]
    public virtual QShortcutScope ShortcutScope
    {
      get => this.m_eShortcutScope;
      set
      {
        this.m_eShortcutScope = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual Image UsedCustomizeToolBarMask => this.m_oCustomizeToolBarMask != null ? this.m_oCustomizeToolBarMask : QToolBarConfiguration.m_oDefaultCustomizeToolBarMask;

    [Category("QAppearance")]
    [Description("Contains the base image that is used to customize the QToolBar. In the Mask the Color Red is replaced by the ToolBarTextColor")]
    [DefaultValue(null)]
    public virtual Image CustomizeToolBarMask
    {
      get => this.m_oCustomizeToolBarMask;
      set
      {
        this.m_oCustomizeToolBarMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual Image UsedHasMoreItemsMask => this.m_oHasMoreItemsMask != null ? this.m_oHasMoreItemsMask : QToolBarConfiguration.m_oDefaultHasMoreItemsMask;

    [Description("Gets or sets the base image that is used when a QToolBar has more items then visible. In the Mask the Color Red is replaced by the ToolBarTextColor.")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public virtual Image HasMoreItemsMask
    {
      get => this.m_oHasMoreItemsMask;
      set
      {
        this.m_oHasMoreItemsMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual Image UsedFormCustomizeToolBarMask => this.m_oFormCustomizeToolBarMask != null ? this.m_oFormCustomizeToolBarMask : QToolBarConfiguration.m_oDefaultFormCustomizeToolBarMask;

    [Category("QAppearance")]
    [Description("Gets or sets the base image that is used for customizing when a QToolBar is placed on a Form. In the Mask the Color Red is replaced by the ToolBarTextColor.")]
    [DefaultValue(null)]
    public virtual Image FormCustomizeToolBarMask
    {
      get => this.m_oFormCustomizeToolBarMask;
      set
      {
        this.m_oFormCustomizeToolBarMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual Image UsedFormCloseToolBarMask => this.m_oFormCloseToolBarMask != null ? this.m_oFormCloseToolBarMask : QToolBarConfiguration.m_oDefaultFormCloseToolBarMask;

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the base image that is used for closing when a QToolBar is placed on a Form. In the Mask the Color Red is replaced by the ToolBarTextColor.")]
    public virtual Image FormCloseToolBarMask
    {
      get => this.m_oFormCloseToolBarMask;
      set
      {
        this.m_oFormCloseToolBarMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(3)]
    [Description("Gets or sets the width of the SizingGrip")]
    public virtual int SizingGripWidth
    {
      get => this.m_iSizingGripWidth;
      set
      {
        this.m_iSizingGripWidth = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the size of the rounded corners when the QToolBar is displayed as a RoundedBevel. This depends on the ToolBarStyle.")]
    [DefaultValue(6)]
    public virtual int RoundedBevelCornerSize
    {
      get => this.m_iRoundedBevelCornerSize;
      set
      {
        this.m_iRoundedBevelCornerSize = value >= 0 ? value : throw new InvalidOperationException(QResources.GetException("QToolBarConfiguration_RoundedBevelCornerSize_Not_Negative"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(QToolBarLayoutType.SetMoreItemsOnNoFit)]
    [Category("QAppearance")]
    [Description("Gets or sets how the items of a QToolBar should be threated when they don't fit.")]
    public virtual QToolBarLayoutType LayoutType
    {
      get => this.m_eToolBarLayoutType;
      set
      {
        this.m_eToolBarLayoutType = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the QToolBar can move. When this is true it must be hosted on a QToolBarHost")]
    [Category("QAppearance")]
    public virtual bool CanMove
    {
      get => this.m_bCanMove;
      set
      {
        this.m_bCanMove = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets whether the QToolBar can float.  When this is true it must be hosted on a QToolBarHost.")]
    [DefaultValue(true)]
    [Category("QAppearance")]
    public virtual bool CanFloat
    {
      get => this.m_bCanFloat;
      set => this.m_bCanFloat = value;
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the QToolBar can customize.")]
    public virtual bool CanCustomize
    {
      get => this.m_bCanCustomize;
      set
      {
        this.m_bCanCustomize = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether the QToolBar will show its CustomizeBar. If you want to hide the CustomizeBar, also specify false to CanCustomize and set the LayoutType to ExpandOnNoFit.")]
    [DefaultValue(true)]
    public virtual bool ShowCustomizeBar
    {
      get => this.m_bShowCustomizeBar;
      set
      {
        this.m_bShowCustomizeBar = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the QToolBar can close.")]
    public virtual bool CanClose
    {
      get => this.m_bCanClose;
      set
      {
        this.m_bCanClose = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether the QToolBar should be stretched.")]
    [DefaultValue(false)]
    public virtual bool Stretched
    {
      get => this.m_bStretched;
      set
      {
        this.m_bStretched = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets whether the QToolBar should should be hidden when the closebutton is pressed instead of disposed.")]
    [Category("QAppearance")]
    [DefaultValue(true)]
    public virtual bool HideOnClose
    {
      get => this.m_bHideOnClose;
      set
      {
        this.m_bHideOnClose = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the style of the SizingGrip")]
    [DefaultValue(QSizingGripStyle.Dots)]
    public QSizingGripStyle SizingGripStyle
    {
      get => this.m_eSizingGripStyle;
      set
      {
        this.m_eSizingGripStyle = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets width of the HasMoreItemsArea")]
    [Category("QAppearance")]
    [DefaultValue(11)]
    public virtual int HasMoreItemsAreaWidth
    {
      get => this.m_iHasMoreItemsAreaWidth;
      set
      {
        this.m_iHasMoreItemsAreaWidth = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(typeof (QPadding), "1,1,1,1")]
    [Description("Gets or sets the padding between the edge and the contents of a QToolBar")]
    public virtual QPadding ToolBarPadding
    {
      get => this.m_oToolBarPadding;
      set
      {
        this.m_oToolBarPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the padding of the SizingGrip of a QToolBar")]
    [DefaultValue(typeof (QPadding), "4,3,0,4")]
    public virtual QPadding SizingGripPadding
    {
      get => this.m_oSizingGripPadding;
      set
      {
        this.m_oSizingGripPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(typeof (QPadding), "1,1,1,1")]
    [Description("Gets or sets the padding of the buttons used on the Form when a QToolBar is floating.")]
    public virtual QPadding FormButtonsPadding
    {
      get => this.m_oFormButtonsPadding;
      set
      {
        this.m_oFormButtonsPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(typeof (QSpacing), "4,4")]
    [Description("Gets or sets the spacing before and after the items.")]
    public virtual QSpacing ItemsSpacing
    {
      get => this.m_oItemsSpacing;
      set
      {
        this.m_oItemsSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the ToolBarStyle.")]
    [Category("QAppearance")]
    [DefaultValue(QToolBarStyle.Beveled)]
    public virtual QToolBarStyle ToolBarStyle
    {
      get => this.m_eToolBarStyle;
      set
      {
        this.m_eToolBarStyle = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QMargin), "2,0,0,2")]
    public override QMargin ItemMargin
    {
      get => base.ItemMargin;
      set => base.ItemMargin = value;
    }

    [DefaultValue(typeof (QPadding), "3,1,1,3")]
    public override QPadding ItemPadding
    {
      get => base.ItemPadding;
      set => base.ItemPadding = value;
    }

    [DefaultValue(typeof (QSpacing), "1,1")]
    public override QSpacing IconSpacing
    {
      get => base.IconSpacing;
      set => base.IconSpacing = value;
    }

    [DefaultValue(typeof (QSpacing), "0,0")]
    public override QSpacing TitleSpacing
    {
      get => base.TitleSpacing;
      set => base.TitleSpacing = value;
    }

    [Category("QAppearance")]
    [Description("Indicates if Shortcuts should be shown")]
    [DefaultValue(false)]
    public override bool ShortcutsVisible
    {
      get => base.ShortcutsVisible;
      set => base.ShortcutsVisible = value;
    }

    [DefaultValue(typeof (QSpacing), "2,2")]
    public override QSpacing HasChildItemsSpacing
    {
      get => base.HasChildItemsSpacing;
      set => base.HasChildItemsSpacing = value;
    }

    [DefaultValue(typeof (QSpacing), "4,4")]
    public override QSpacing SeparatorSpacing
    {
      get => base.SeparatorSpacing;
      set => base.SeparatorSpacing = value;
    }

    [DefaultValue(QPersonalizedItemBehavior.NeverVisible)]
    [Description("Gets or sets the personalized behavior. The personalized behavior of a QMainMenu or QToolBar cannot be set to DependsOnPersonalized.")]
    public override QPersonalizedItemBehavior PersonalizedItemBehavior
    {
      get => base.PersonalizedItemBehavior;
      set => base.PersonalizedItemBehavior = value != QPersonalizedItemBehavior.DependsOnPersonalized ? value : throw new InvalidOperationException(QResources.GetException("QToolBar_PersonalizedBehaviorCannotDepend"));
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether the default CustomizeItems must be added to the CustomizeMenu.")]
    [DefaultValue(true)]
    public bool ShowDefaultCustomizeItems
    {
      get => this.m_bShowDefaultCustomizeItems;
      set => this.m_bShowDefaultCustomizeItems = value;
    }

    [Localizable(true)]
    [DefaultValue("&Add or remove buttons")]
    [Category("QAppearance")]
    [Description("Gets or sets the title of the CustomizeItem")]
    public string CustomizeItemTitle
    {
      get => this.m_sCustomizeItemTitle;
      set
      {
        this.m_sCustomizeItemTitle = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the tooltip of the CustomizeItem. This must contain Xml as used with QMarkupText")]
    [Category("QAppearance")]
    [DefaultValue("Toolbar options")]
    [Localizable(true)]
    public string CustomizeItemTooltip
    {
      get => this.m_sCustomizeItemTooltip;
      set
      {
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sCustomizeItemTooltip = value;
      }
    }
  }
}
