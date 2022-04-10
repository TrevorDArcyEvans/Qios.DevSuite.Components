// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarItemConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [TypeConverter(typeof (QCommandConfigurationTypeConverter))]
  public class QExplorerBarItemConfiguration : QCommandConfiguration, IDisposable
  {
    private bool m_bDisposed;
    private QExplorerBarItemAlternateConfiguration m_oAlternateConfiguration;
    private bool m_bExpandOnItemClick = true;
    private bool m_bItemWrapText;
    private QBackgroundBoundsType m_oBackgroundBoundsType;
    private QCommandOrientation m_eItemOrientation;
    private Font m_oBaseFont;
    private Font m_oFont;
    private Font m_oFontHot;
    private Font m_oFontPressed;
    private Font m_oFontExpanded;
    private QFontStyle m_eFontStyle;
    private QFontStyle m_eFontStyleHot;
    private QFontStyle m_eFontStylePressed;
    private QFontStyle m_eFontStyleExpanded;

    public QExplorerBarItemConfiguration()
    {
      this.m_oAlternateConfiguration = new QExplorerBarItemAlternateConfiguration(QExplorerItemType.MenuItem);
      this.m_oAlternateConfiguration.ConfigurationChanged += new EventHandler(this.m_oAlternateConfiguration_ConfigurationChanged);
    }

    public void Dispose()
    {
      if (this.m_bDisposed)
        return;
      this.Dispose(true);
    }

    ~QExplorerBarItemConfiguration()
    {
      this.Dispose(false);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bDisposed)
        return;
      this.m_bDisposed = true;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal QExplorerBarItemAlternateConfiguration AlternateConfiguration => this.m_oAlternateConfiguration;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override int DepersonalizeDelay
    {
      get => base.DepersonalizeDelay;
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override int ExpandingDelay
    {
      get => base.ExpandingDelay;
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool UseExpandingDelay
    {
      get => base.UseExpandingDelay;
      set
      {
      }
    }

    [Browsable(false)]
    public override Image UsedHasChildItemsMask => this.m_oAlternateConfiguration != null ? this.m_oAlternateConfiguration.UsedHasChildItemsMask : (Image) null;

    [Description("Gets or sets the base image that is used when a QExplorerBar has more items then visible. In the Mask the Color Red is replaced by the ExplorerBarHasMoreChildItemsColor.")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public override Image HasChildItemsMask
    {
      get => this.m_oAlternateConfiguration != null ? this.m_oAlternateConfiguration.HasChildItemsMask : (Image) null;
      set => this.m_oAlternateConfiguration.HasChildItemsMask = value;
    }

    [Description("Gets or sets whether the item expands if the use clicks on it, or only if the user clicks on the HasChildItemsImage")]
    [DefaultValue(true)]
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

    [DefaultValue(false)]
    [Description("Gets or sets whether the text of items should be wrapped")]
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
    [Description("Gets or sets the bounds type of an item")]
    [DefaultValue(typeof (QBackgroundBoundsType), "Normal")]
    public QBackgroundBoundsType BackgroundBoundsType
    {
      get => this.m_oBackgroundBoundsType;
      set
      {
        this.m_oBackgroundBoundsType = value;
        this.OnConfigurationChanged(EventArgs.Empty);
      }
    }

    [Category("QAppearance")]
    [Description("Gets or sets the orientation of an item")]
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

    [Browsable(false)]
    public Font Font => this.m_oFont;

    [Browsable(false)]
    public Font FontHot => this.m_oFontHot;

    [Browsable(false)]
    public Font FontPressed => this.m_oFontPressed;

    [Browsable(false)]
    public Font FontExpanded => this.m_oFontExpanded;

    [DefaultValue(typeof (QFontStyle), "Regular")]
    [Category("QAppearance")]
    [Description("Gets or sets the style of the font")]
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

    [Description("Gets or sets the style of the font for hot items")]
    [DefaultValue(typeof (QFontStyle), "Underline")]
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

    [DefaultValue(typeof (QFontStyle), "Underline")]
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

    [Category("QAppearance")]
    [DefaultValue(typeof (QFontStyle), "Regular")]
    [Description("Gets or sets the style of the font for expanded items")]
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

    public void PutFont(Font font)
    {
      this.m_oBaseFont = font;
      this.m_oFont = new QFontDefinition(this.m_eFontStyle).GetFontFromCache(this.m_oBaseFont);
      this.m_oFontExpanded = new QFontDefinition(this.m_eFontStyleExpanded).GetFontFromCache(this.m_oBaseFont);
      this.m_oFontPressed = new QFontDefinition(this.m_eFontStylePressed).GetFontFromCache(this.m_oBaseFont);
      this.m_oFontHot = new QFontDefinition(this.m_eFontStyleHot).GetFontFromCache(this.m_oBaseFont);
    }

    private void m_oAlternateConfiguration_ConfigurationChanged(object sender, EventArgs e) => this.OnConfigurationChanged(EventArgs.Empty);
  }
}
