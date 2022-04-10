// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarGroupItemConfiguration
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
  public class QExplorerBarGroupItemConfiguration : QCommandConfiguration, IDisposable
  {
    private bool m_bDisposed;
    private QExplorerBarItemAlternateConfiguration m_oAlternateConfiguration;
    private QCommandOrientation m_eItemOrientation;
    private bool m_bShadeVisible;
    private Point m_oShadePosition;
    private int m_iShadeGradientSize;
    private Image m_oDepersonalizeMenuMask;
    private static Image m_oDefaultDepersonalizeMenuMask;
    private bool m_bIconOverlaps;
    private bool m_bItemWrapText;
    private bool m_bItemStretched;
    private bool m_bCanExpandMultiple;
    private bool m_bAnimateGroupItem;
    private int m_iAnimateTime = 100;
    private QExplorerItemAnimationType m_eAnimationType = QExplorerItemAnimationType.Slide;
    private Font m_oBaseFont;
    private Font m_oFont;
    private Font m_oFontHot;
    private Font m_oFontPressed;
    private Font m_oFontExpanded;
    private QFontStyle m_eFontStyle;
    private QFontStyle m_eFontStyleHot;
    private QFontStyle m_eFontStylePressed;
    private QFontStyle m_eFontStyleExpanded;
    private QExplorerBarPanelAppearance m_oPanelAppearance;
    private QExplorerBarItemAppearance m_oItemAppearance;
    private int m_iItemSpacing;
    private int m_iItemRoundedCornerSize;
    private bool m_bItemRoundedCornerTopLeft;
    private bool m_bItemRoundedCornerTopRight;
    private bool m_bItemRoundedCornerBottomLeft;
    private bool m_bItemRoundedCornerBottomRight;
    private int m_iPanelRoundedCornerSize;
    private bool m_bPanelRoundedCornerTopLeft;
    private bool m_bPanelRoundedCornerTopRight;
    private bool m_bPanelRoundedCornerBottomLeft;
    private bool m_bPanelRoundedCornerBottomRight;
    private QPadding m_oPanelPadding;
    private QMargin m_oPanelMargin;

    public QExplorerBarGroupItemConfiguration()
    {
      this.m_oAlternateConfiguration = new QExplorerBarItemAlternateConfiguration(QExplorerItemType.GroupItem);
      this.m_oAlternateConfiguration.ConfigurationChanged += new EventHandler(this.m_oAlternateConfiguration_ConfigurationChanged);
      if (QExplorerBarGroupItemConfiguration.m_oDefaultDepersonalizeMenuMask != null)
        return;
      QExplorerBarGroupItemConfiguration.m_oDefaultDepersonalizeMenuMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.ExplorerBarDepersonalizeMask.png"));
    }

    ~QExplorerBarGroupItemConfiguration() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bDisposed)
        return;
      this.m_bDisposed = true;
    }

    internal QExplorerBarItemAlternateConfiguration AlternateConfiguration => this.m_oAlternateConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ExpandingDelay
    {
      get => base.ExpandingDelay;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool UseExpandingDelay
    {
      get => base.UseExpandingDelay;
      set
      {
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the group items have shading")]
    [DefaultValue(true)]
    public bool ShadeVisible
    {
      get => this.m_bShadeVisible;
      set
      {
        this.m_bShadeVisible = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the position of the group item shade.")]
    [Category("QAppearance")]
    [DefaultValue(typeof (Point), "3,3")]
    public Point ShadePosition
    {
      get => this.m_oShadePosition;
      set
      {
        this.m_oShadePosition = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(3)]
    [Description("Contains the size in pixels of the gradient (edges) of the shade.")]
    [Category("QAppearance")]
    public int ShadeGradientSize
    {
      get => this.m_iShadeGradientSize;
      set
      {
        this.m_iShadeGradientSize = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the orientation of an item")]
    [Category("QAppearance")]
    [DefaultValue(typeof (QCommandOrientation), "Horizontal")]
    public QCommandOrientation ItemOrientation
    {
      get => this.m_eItemOrientation;
      set
      {
        this.m_eItemOrientation = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    [Category("QAppearance")]
    [Description("Gets or sets whether the icon should overlap.")]
    public bool IconOverlaps
    {
      get => this.m_bIconOverlaps;
      set
      {
        this.m_bIconOverlaps = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (Size), "32,32")]
    [Category("QAppearance")]
    [Description("Gets or sets the the prefered IconSize")]
    public override Size IconSize
    {
      get => base.IconSize;
      set => base.IconSize = value;
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether the text of group items should be wrapped")]
    [Category("QAppearance")]
    public virtual bool ItemWrapText
    {
      get => this.m_bItemWrapText;
      set
      {
        this.m_bItemWrapText = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets whether group items should be stretched")]
    [DefaultValue(false)]
    public virtual bool ItemStretched
    {
      get => this.m_bItemStretched;
      set
      {
        this.m_bItemStretched = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether multiple group items may be expanded simultaneously")]
    public virtual bool CanExpandMultiple
    {
      get => this.m_bCanExpandMultiple;
      set
      {
        this.m_bCanExpandMultiple = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets whether the QExplorerItem group items should use animation, this is ignored when the InheritWindowsSettings is true")]
    [Category("QAppearance")]
    public virtual bool AnimateGroupItem
    {
      get => this.m_bAnimateGroupItem;
      set
      {
        this.m_bAnimateGroupItem = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the time that is used for animation (in milliseconds)")]
    [DefaultValue(200)]
    [Category("QAppearance")]
    public virtual int AnimateTime
    {
      get => this.m_iAnimateTime;
      set
      {
        this.m_iAnimateTime = value >= 1 && value <= 10000 ? value : throw new InvalidOperationException(QResources.GetException("QExplorerBar_AnimateTime_InvalidValue"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(QExplorerItemAnimationType.Both)]
    [Description("Gets or sets the type of animation to use. This is ignored when InheritWindowsSettings is true.")]
    [Category("QAppearance")]
    public virtual QExplorerItemAnimationType AnimationType
    {
      get => this.m_eAnimationType;
      set
      {
        this.m_eAnimationType = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual QExplorerItemAnimationType UsedAnimationType
    {
      get
      {
        if (this.InheritWindowsSettings)
        {
          if (NativeHelper.AnimateMenus)
            return QExplorerItemAnimationType.Both;
        }
        else if (this.AnimateGroupItem)
          return this.AnimationType;
        return QExplorerItemAnimationType.None;
      }
    }

    [Browsable(false)]
    public Font Font => this.m_oFont;

    [Browsable(false)]
    public Font FontHot => this.m_oFontHot;

    [Browsable(false)]
    public Font FontPressed => this.m_oFontPressed;

    [Browsable(false)]
    public Font FontExpanded => this.m_oFontExpanded;

    [Description("Gets or sets the style of the font")]
    [DefaultValue(typeof (QFontStyle), "Bold")]
    [Category("QAppearance")]
    public QFontStyle FontStyle
    {
      get => this.m_eFontStyle;
      set
      {
        if (this.m_eFontStyle == value)
          return;
        this.m_eFontStyle = value;
        if (this.m_oBaseFont != null)
          this.m_oFont = new QFontDefinition(this.m_eFontStyle).GetFontFromCache(this.m_oBaseFont);
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QFontStyle), "Bold")]
    [Description("Gets or sets the style of the font for hot items")]
    [Category("QAppearance")]
    public QFontStyle FontStyleHot
    {
      get => this.m_eFontStyleHot;
      set
      {
        if (this.m_eFontStyleHot == value)
          return;
        this.m_eFontStyleHot = value;
        if (this.m_oBaseFont != null)
          this.m_oFontHot = new QFontDefinition(this.m_eFontStyleHot).GetFontFromCache(this.m_oBaseFont);
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QFontStyle), "Bold")]
    [Description("Gets or sets the style of the font for pressed items")]
    [Category("QAppearance")]
    public QFontStyle FontStylePressed
    {
      get => this.m_eFontStylePressed;
      set
      {
        if (this.m_eFontStylePressed == value)
          return;
        this.m_eFontStylePressed = value;
        if (this.m_oBaseFont != null)
          this.m_oFontPressed = new QFontDefinition(this.m_eFontStylePressed).GetFontFromCache(this.m_oBaseFont);
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QFontStyle), "Bold")]
    [Description("Gets or sets the style of the font for expanded items")]
    [Category("QAppearance")]
    public QFontStyle FontStyleExpanded
    {
      get => this.m_eFontStyleExpanded;
      set
      {
        if (this.m_eFontStyleExpanded == value)
          return;
        this.m_eFontStyleExpanded = value;
        if (this.m_oBaseFont != null)
          this.m_oFontExpanded = new QFontDefinition(this.m_eFontStyleExpanded).GetFontFromCache(this.m_oBaseFont);
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the spacing between group items")]
    [Category("QAppearance")]
    [DefaultValue(10)]
    public virtual int ItemSpacing
    {
      get => this.m_iItemSpacing;
      set
      {
        this.m_iItemSpacing = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the size of the group items rounded corners")]
    [Category("QAppearance")]
    [DefaultValue(6)]
    public virtual int ItemRoundedCornerSize
    {
      get => this.m_iItemRoundedCornerSize;
      set
      {
        this.m_iItemRoundedCornerSize = value >= 0 ? value : throw new InvalidOperationException(QResources.GetException("QExplorerBarConfiguration_RoundedCornerSize_Not_Negative"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the top left corner should be rounded")]
    [DefaultValue(true)]
    public virtual bool ItemRoundedCornerTopLeft
    {
      get => this.m_bItemRoundedCornerTopLeft;
      set
      {
        this.m_bItemRoundedCornerTopLeft = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the top right corner should be rounded")]
    [DefaultValue(true)]
    public virtual bool ItemRoundedCornerTopRight
    {
      get => this.m_bItemRoundedCornerTopRight;
      set
      {
        this.m_bItemRoundedCornerTopRight = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets if the bottom left corner should be rounded")]
    [Category("QAppearance")]
    [DefaultValue(false)]
    public virtual bool ItemRoundedCornerBottomLeft
    {
      get => this.m_bItemRoundedCornerBottomLeft;
      set
      {
        this.m_bItemRoundedCornerBottomLeft = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the bottom right corner should be rounded")]
    [DefaultValue(false)]
    public virtual bool ItemRoundedCornerBottomRight
    {
      get => this.m_bItemRoundedCornerBottomRight;
      set
      {
        this.m_bItemRoundedCornerBottomRight = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the size of the group panels rounded corners")]
    [Category("QAppearance")]
    [DefaultValue(6)]
    public virtual int PanelRoundedCornerSize
    {
      get => this.m_iPanelRoundedCornerSize;
      set
      {
        this.m_iPanelRoundedCornerSize = value >= 0 ? value : throw new InvalidOperationException(QResources.GetException("QExplorerBarConfiguration_RoundedCornerSize_Not_Negative"));
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the top left corner should be rounded")]
    [DefaultValue(false)]
    public virtual bool PanelRoundedCornerTopLeft
    {
      get => this.m_bPanelRoundedCornerTopLeft;
      set
      {
        this.m_bPanelRoundedCornerTopLeft = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets if the top right corner should be rounded")]
    [Category("QAppearance")]
    [DefaultValue(false)]
    public virtual bool PanelRoundedCornerTopRight
    {
      get => this.m_bPanelRoundedCornerTopRight;
      set
      {
        this.m_bPanelRoundedCornerTopRight = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets if the bottom left corner should be rounded")]
    [DefaultValue(false)]
    [Category("QAppearance")]
    public virtual bool PanelRoundedCornerBottomLeft
    {
      get => this.m_bPanelRoundedCornerBottomLeft;
      set
      {
        this.m_bPanelRoundedCornerBottomLeft = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets if the bottom right corner should be rounded")]
    [DefaultValue(false)]
    public virtual bool PanelRoundedCornerBottomRight
    {
      get => this.m_bPanelRoundedCornerBottomRight;
      set
      {
        this.m_bPanelRoundedCornerBottomRight = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the padding of the group panel")]
    [Category("QAppearance")]
    [DefaultValue(typeof (QPadding), "2,2,2,2")]
    public QPadding PanelPadding
    {
      get => this.m_oPanelPadding;
      set
      {
        this.m_oPanelPadding = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(typeof (QMargin), "1,1,1,1")]
    [Category("QAppearance")]
    [Description("Gets or sets the margin of the group panel")]
    public QMargin PanelMargin
    {
      get => this.m_oPanelMargin;
      set
      {
        this.m_oPanelMargin = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    public void ResetPanelAppearance() => this.PanelAppearance.SetToDefaultValues();

    public bool ShouldSerializePanelAppearance() => !this.PanelAppearance.IsSetToDefaultValues();

    [Description("Gets the appearance for the group panels")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QExplorerBarPanelAppearance PanelAppearance
    {
      get
      {
        if (this.m_oPanelAppearance == null)
        {
          this.m_oPanelAppearance = new QExplorerBarPanelAppearance();
          this.m_oPanelAppearance.AppearanceChanged += new EventHandler(this.m_oPanelAppearance_AppearanceChanged);
        }
        return this.m_oPanelAppearance;
      }
    }

    [Description("Gets the appearance for the group items")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QExplorerBarItemAppearance ItemAppearance
    {
      get
      {
        if (this.m_oItemAppearance == null)
        {
          this.m_oItemAppearance = new QExplorerBarItemAppearance();
          this.m_oItemAppearance.AppearanceChanged += new EventHandler(this.m_oItemAppearance_AppearanceChanged);
        }
        return this.m_oItemAppearance;
      }
    }

    [Browsable(false)]
    public Image UsedDepersonalizeMenuMask => this.m_oDepersonalizeMenuMask != null ? this.m_oDepersonalizeMenuMask : QExplorerBarGroupItemConfiguration.m_oDefaultDepersonalizeMenuMask;

    [Description("Contains the base image that is used to show at the bottom of a QExplorerItem group when it has DepersonalizedMenuItems that are not shown. In the Mask the Color Red is replaced by the TextColor and Black till white are replaced by the DepersonalizeColors Color.")]
    [Category("QAppearance")]
    [DefaultValue(null)]
    public virtual Image DepersonalizeMenuMask
    {
      get => this.m_oDepersonalizeMenuMask;
      set
      {
        this.m_oDepersonalizeMenuMask = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public virtual Image UsedHasChildItemsMaskReverse => this.m_oAlternateConfiguration != null ? this.m_oAlternateConfiguration.UsedHasChildItemsMaskReverse : (Image) null;

    [Browsable(false)]
    public override Image UsedHasChildItemsMask => this.m_oAlternateConfiguration != null ? this.m_oAlternateConfiguration.UsedHasChildItemsMask : (Image) null;

    [DefaultValue(null)]
    [Description("Gets or sets the base image that is used when a QExplorerBar has more items then visible. In the Mask the Color Red is replaced by the ExplorerBarHasMoreChildItemsColor.")]
    [Category("QAppearance")]
    public virtual Image HasChildItemsMaskReverse
    {
      get => this.m_oAlternateConfiguration != null ? this.m_oAlternateConfiguration.HasChildItemsMaskReverse : (Image) null;
      set => this.m_oAlternateConfiguration.HasChildItemsMaskReverse = value;
    }

    [Description("Gets or sets the base image that is used when a QExplorerBar has more items then visible. In the Mask the Color Red is replaced by the ExplorerBarHasMoreChildItemsColor.")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public override Image HasChildItemsMask
    {
      get => this.m_oAlternateConfiguration != null ? this.m_oAlternateConfiguration.HasChildItemsMask : (Image) null;
      set => this.m_oAlternateConfiguration.HasChildItemsMask = value;
    }

    public void PutFont(Font font)
    {
      this.m_oBaseFont = font;
      this.m_oFont = new QFontDefinition(this.m_eFontStyle).GetFontFromCache(this.m_oBaseFont);
      this.m_oFontExpanded = new QFontDefinition(this.m_eFontStyleExpanded).GetFontFromCache(this.m_oBaseFont);
      this.m_oFontPressed = new QFontDefinition(this.m_eFontStylePressed).GetFontFromCache(this.m_oBaseFont);
      this.m_oFontHot = new QFontDefinition(this.m_eFontStyleHot).GetFontFromCache(this.m_oBaseFont);
    }

    protected override QCommandAppearance CreateCommandAppearance() => (QCommandAppearance) new QExplorerBarItemAppearance();

    private void m_oPanelAppearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void m_oItemAppearance_AppearanceChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);

    private void m_oAlternateConfiguration_ConfigurationChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
