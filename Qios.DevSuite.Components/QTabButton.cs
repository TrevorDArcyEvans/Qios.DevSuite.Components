// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButton
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QTabButton : IDisposable, IQPaintBackgroundObjectsProvider
  {
    private bool m_bDrawingOnBitmap;
    private IQTabButtonSource m_oTabButtonSource;
    private bool m_bDisposeFields;
    private QTabButtonCollection m_oTabButtonCollection;
    private QTabButtonRow m_oTabButtonRow;
    private QTabButtonConfiguration m_oConfiguration;
    private Size m_oLastCalculatedIconSize = Size.Empty;
    private Image m_oBackgroundImage;
    private string m_sBackgroundImageResourceName;
    private bool m_bBackgroundImageLoadedFromResource;
    private Icon m_oIcon;
    private string m_sIconResourceName;
    private bool m_bIconLoadedFromResource;
    private Icon m_oDisabledIcon;
    private string m_sDisabledIconResourceName;
    private bool m_bDisabledIconLoadedFromResource;
    private Size m_oLastCalculatedDisabledIconSize = Size.Empty;
    private Control m_oControl;
    private string m_sText;
    private string m_sToolTipText;
    private Rectangle m_oBounds = Rectangle.Empty;
    private int m_iButtonOrder = -1;
    private Size m_oCalculatedMinimumSize;
    private Size m_oCalculatedMaximumSize;
    private bool m_bFocused;
    private bool m_bVisible = true;
    private bool m_bEnabled = true;
    private bool m_bCanClose = true;
    private bool m_bIsDisposed;
    private GraphicsPath m_oLastDrawnGraphicsPath;
    private EventHandler m_oConfigurationChangedEventHandler;
    private object m_oAdditionalData;

    internal QTabButton()
    {
    }

    internal QTabButton(IQTabButtonSource source)
    {
      this.m_oConfigurationChangedEventHandler = new EventHandler(this.TabButtonConfiguration_ConfigurationChanged);
      this.m_oConfiguration = this.CreateTabButtonConfiguration();
      this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
      this.DisposeFields = false;
      this.m_oTabButtonSource = source;
      this.m_oControl = this.m_oTabButtonSource as Control;
      if (!(this.m_oTabButtonSource is QContainerControl oTabButtonSource))
        return;
      oTabButtonSource.PaintBackgroundObjectsProvider = (IQPaintBackgroundObjectsProvider) this;
    }

    protected virtual QTabButtonConfiguration CreateTabButtonConfiguration() => new QTabButtonConfiguration();

    internal bool DrawingOnBitmap
    {
      get => this.m_bDrawingOnBitmap;
      set => this.m_bDrawingOnBitmap = value;
    }

    internal bool DisposeFields
    {
      get => this.m_bDisposeFields;
      set => this.m_bDisposeFields = value;
    }

    internal IQTabButtonSource TabButtonSource => this.m_oTabButtonSource;

    public QTabStrip TabStrip => this.m_oTabButtonCollection == null ? (QTabStrip) null : this.m_oTabButtonCollection.TabStrip;

    public QTabButtonCollection TabButtonCollection => this.m_oTabButtonCollection;

    public object AdditionalData
    {
      get => this.m_oAdditionalData;
      set => this.m_oAdditionalData = value;
    }

    internal void PutTabButtonCollection(QTabButtonCollection collection)
    {
      this.m_oTabButtonCollection = collection;
      if (this.TabStrip != null)
        this.Configuration.Properties.BaseProperties = this.TabStrip.Configuration.ButtonConfiguration.Properties;
      else
        this.Configuration.Properties.BaseProperties = (QFastPropertyBag) null;
    }

    public QTabButtonRow TabButtonRow => this.m_oTabButtonRow;

    internal void PutTabButtonRow(QTabButtonRow tabButtonRow) => this.m_oTabButtonRow = tabButtonRow;

    public bool Enabled
    {
      get => this.m_bEnabled;
      set
      {
        this.m_bEnabled = value;
        this.NotifyTabStripOfChange(QCommandUIRequest.Redraw);
        if (!this.IsActivated || value)
          return;
        this.TabStrip.SetActiveButton(this.TabStrip.GetNextAccessibleButtonForNavigation(this, false) ?? this.TabStrip.GetPreviousAccessibleButtonForNavigation(this, false), true, true, true);
      }
    }

    public bool CanClose
    {
      get => this.m_bCanClose;
      set
      {
        if (this.m_bCanClose == value)
          return;
        this.m_bCanClose = value;
        if (this.TabStrip == null || !this.TabStrip.NavigationArea.Close.Visible)
          return;
        this.NotifyTabStripOfChange(QCommandUIRequest.Redraw, this.TabStrip.NavigationArea.CalculateBoundsToControl(this.TabStrip.NavigationArea.Close.Bounds));
      }
    }

    public bool Visible
    {
      get => this.m_bVisible;
      set
      {
        this.m_bVisible = value;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
        if (!this.IsActivated || value)
          return;
        this.TabStrip.SetActiveButton(this.TabStrip.GetNextAccessibleButtonForNavigation(this, false) ?? this.TabStrip.GetPreviousAccessibleButtonForNavigation(this, false), true, true, true);
      }
    }

    public bool Focused => this.m_bFocused;

    internal void PutFocused(bool value)
    {
      if (this.m_bFocused == value)
        return;
      this.m_bFocused = value;
      this.NotifyTabStripOfChange(QCommandUIRequest.Redraw, this.BoundsToControl);
    }

    public QTabButtonConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration == value)
          return;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= this.m_oConfigurationChangedEventHandler;
        this.m_oConfiguration = value;
        if (this.m_oConfiguration != null)
        {
          this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
          if (this.TabStrip != null)
            this.m_oConfiguration.Properties.BaseProperties = this.TabStrip.Configuration.ButtonConfiguration.Properties;
        }
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    public Control Control => this.m_oControl;

    public int ButtonOrder
    {
      get => this.m_iButtonOrder;
      set => this.SetButtonOrder(value, true);
    }

    internal void SetButtonOrder(int buttonOrder, bool updateCollection)
    {
      if (updateCollection && this.m_oTabButtonCollection != null)
        this.m_oTabButtonCollection.SetButtonOrder(this, buttonOrder);
      else
        this.m_iButtonOrder = buttonOrder;
    }

    [Localizable(true)]
    public string Text
    {
      get => this.m_sText;
      set
      {
        if (this.m_sText == value)
          return;
        this.m_sText = value;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Localizable(true)]
    public string ToolTipText
    {
      get => this.m_sToolTipText;
      set
      {
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sToolTipText = value;
      }
    }

    public string UsedToolTipText => !QMisc.IsEmpty((object) this.m_sToolTipText) ? this.m_sToolTipText : HttpUtility.HtmlEncode(this.m_sText);

    public bool ShouldSerializeBackgroundImage() => this.m_oBackgroundImage != null && !this.m_bBackgroundImageLoadedFromResource;

    public void ResetBackgroundImage() => this.BackgroundImage = (Image) null;

    [Description("Gets or sets the BackgroundImage of the QTabButton")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QButtonAppearance")]
    public Image BackgroundImage
    {
      get => this.m_oBackgroundImage;
      set
      {
        this.m_oBackgroundImage = value;
        this.m_bBackgroundImageLoadedFromResource = false;
        this.m_sBackgroundImageResourceName = (string) null;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QButtonAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.ImageName.extension, AssemblyName'")]
    public string BackgroundImageResourceName
    {
      get => this.m_sBackgroundImageResourceName;
      set
      {
        if (this.m_sBackgroundImageResourceName == value)
          return;
        this.m_sBackgroundImageResourceName = value;
        this.m_bBackgroundImageLoadedFromResource = true;
        this.m_oBackgroundImage = QResources.LoadImageFromResource(this.m_sBackgroundImageResourceName);
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    public bool ShouldSerializeIcon() => this.m_oIcon != null && !this.m_bIconLoadedFromResource;

    public void ResetIcon() => this.Icon = (Icon) null;

    [Description("Gets or sets the Icon of the QTabButton")]
    [Category("QButtonAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Icon Icon
    {
      get => this.m_oIcon;
      set
      {
        this.m_oIcon = value;
        this.m_bIconLoadedFromResource = false;
        this.m_sIconResourceName = (string) null;
        this.m_oLastCalculatedIconSize = Size.Empty;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [DefaultValue(null)]
    [Category("QButtonAppearance")]
    public string IconResourceName
    {
      get => this.m_sIconResourceName;
      set
      {
        if (this.m_sIconResourceName == value)
          return;
        this.m_sIconResourceName = value;
        this.m_bIconLoadedFromResource = true;
        this.m_oIcon = QResources.LoadIconFromResource(this.m_sIconResourceName);
        this.m_oLastCalculatedIconSize = Size.Empty;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Browsable(false)]
    [Description("Gets whether the Icon is loaded from a resource")]
    public bool IconLoadedFromResource => this.m_bIconLoadedFromResource;

    public bool ShouldSerializeDisabledIcon() => this.m_oDisabledIcon != null && !this.m_bDisabledIconLoadedFromResource;

    public void ResetDisabledIcon() => this.DisabledIcon = (Icon) null;

    [Description("Gets or sets the DisabledIcon of the QTabButton. When this is not set the default Icon is used for painting.")]
    [Category("QButtonAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Icon DisabledIcon
    {
      get => this.m_oDisabledIcon;
      set
      {
        this.m_oDisabledIcon = value;
        this.m_bDisabledIconLoadedFromResource = false;
        this.m_sDisabledIconResourceName = (string) null;
        this.m_oLastCalculatedDisabledIconSize = Size.Empty;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets or sets a possible resource name to load the DisabledIcon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [Category("QButtonAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public string DisabledIconResourceName
    {
      get => this.m_sDisabledIconResourceName;
      set
      {
        if (this.m_sDisabledIconResourceName == value)
          return;
        this.m_sDisabledIconResourceName = value;
        this.m_bDisabledIconLoadedFromResource = true;
        this.m_oDisabledIcon = QResources.LoadIconFromResource(this.m_sDisabledIconResourceName);
        this.m_oLastCalculatedDisabledIconSize = Size.Empty;
        this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);
      }
    }

    [Description("Gets whether the DisabledIcon is loaded from a resource")]
    [Browsable(false)]
    public bool DisabledIconLoadedFromResource => this.m_bDisabledIconLoadedFromResource;

    public Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    public GraphicsPath LastDrawnGraphicsPath
    {
      get => this.m_oLastDrawnGraphicsPath;
      set
      {
        if (this.m_oLastDrawnGraphicsPath == value)
          return;
        if (this.m_oLastDrawnGraphicsPath != null)
          this.m_oLastDrawnGraphicsPath.Dispose();
        this.m_oLastDrawnGraphicsPath = value;
      }
    }

    public Point Location
    {
      get => this.m_oBounds.Location;
      set => this.m_oBounds.Location = value;
    }

    public Size Size
    {
      get => this.m_oBounds.Size;
      set => this.m_oBounds.Size = value;
    }

    public int Left
    {
      get => this.m_oBounds.X;
      set => this.m_oBounds.X = value;
    }

    public int Top
    {
      get => this.m_oBounds.Y;
      set => this.m_oBounds.Y = value;
    }

    public int Width
    {
      get => this.m_oBounds.Width;
      set => this.m_oBounds.Width = value;
    }

    public int Height
    {
      get => this.m_oBounds.Height;
      set => this.m_oBounds.Height = value;
    }

    public Size CalculatedMinimumSize
    {
      get => this.m_oCalculatedMinimumSize;
      set => this.m_oCalculatedMinimumSize = value;
    }

    public Size CalculatedMaximumSize
    {
      get => this.m_oCalculatedMaximumSize;
      set => this.m_oCalculatedMaximumSize = value;
    }

    public bool IsActivated => this.TabStrip != null && this.TabStrip.ActiveButton == this;

    public bool IsHot => this.TabStrip != null && this.TabStrip.HotButton == this;

    public bool IsAccessible => this.MatchesSelection(QTabButtonSelectionTypes.MustBeVisible | QTabButtonSelectionTypes.MustBeEnabled, (QTabButtonRow) null);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QTabButtonAppearance CurrentAppearance
    {
      get
      {
        if (this.IsActivated)
          return this.Configuration.AppearanceActive;
        return this.IsHot ? this.Configuration.AppearanceHot : this.Configuration.Appearance;
      }
    }

    public Rectangle BoundsToControl
    {
      get
      {
        if (this.TabStrip == null)
          return this.Bounds;
        return this.TabButtonRow != null ? this.TabButtonRow.CalculateBoundsToControl(this.Bounds, true) : this.TabStrip.CalculateBoundsToControl(this.Bounds, true);
      }
    }

    public bool IsDisposed => this.m_bIsDisposed;

    public Icon RetrieveIcon(Size requestedSize, bool enabled)
    {
      if (enabled || this.m_oDisabledIcon == null)
      {
        if (requestedSize == this.m_oLastCalculatedIconSize)
          return this.m_oIcon;
        if (this.m_oIcon != null && this.m_oIcon.Size != requestedSize)
          this.m_oIcon = new Icon(this.m_oIcon, requestedSize);
        this.m_oLastCalculatedIconSize = requestedSize;
        return this.m_oIcon;
      }
      if (requestedSize == this.m_oLastCalculatedDisabledIconSize)
        return this.m_oDisabledIcon;
      if (this.m_oDisabledIcon != null && this.m_oDisabledIcon.Size != requestedSize)
        this.m_oDisabledIcon = new Icon(this.m_oDisabledIcon, requestedSize);
      this.m_oLastCalculatedDisabledIconSize = requestedSize;
      return this.m_oDisabledIcon;
    }

    public QTabButtonPaintParams RetrievePaintParams() => this.m_oTabButtonSource != null ? this.m_oTabButtonSource.RetrieveTabButtonPaintParams() : (QTabButtonPaintParams) null;

    public bool MatchesSelection(QTabButtonSelectionTypes selection, QTabButtonRow row)
    {
      bool flag1 = (selection & QTabButtonSelectionTypes.MustBeVisible) == QTabButtonSelectionTypes.MustBeVisible;
      bool flag2 = (selection & QTabButtonSelectionTypes.MustBeEnabled) == QTabButtonSelectionTypes.MustBeEnabled;
      bool flag3 = (selection & QTabButtonSelectionTypes.MustBeNearAligned) == QTabButtonSelectionTypes.MustBeNearAligned;
      bool flag4 = (selection & QTabButtonSelectionTypes.MustBeFarAligned) == QTabButtonSelectionTypes.MustBeFarAligned;
      bool flag5 = (selection & QTabButtonSelectionTypes.MustBeInRow) == QTabButtonSelectionTypes.MustBeInRow;
      if (flag1 && !this.Visible || flag2 && !this.Enabled || flag3 && this.Configuration.Alignment != QTabButtonAlignment.Near || flag4 && this.Configuration.Alignment != QTabButtonAlignment.Far)
        return false;
      return !flag5 || this.TabButtonRow == row;
    }

    public bool ContainsControlPoint(Point controlPoint) => this.m_oLastDrawnGraphicsPath != null ? this.m_oLastDrawnGraphicsPath.IsVisible(controlPoint) : this.BoundsToControl.Contains(controlPoint);

    public void PerformLayout() => this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);

    internal void NotifyTabStripOfChange(QCommandUIRequest changeRequest) => this.NotifyTabStripOfChange(changeRequest, Rectangle.Empty);

    internal void NotifyTabStripOfChange(
      QCommandUIRequest changeRequest,
      Rectangle invalidateBounds)
    {
      if (this.TabStrip == null || this.TabStrip.IsDisposed)
        return;
      this.TabStrip.HandleTabStripChanged(changeRequest, invalidateBounds);
    }

    private void TabButtonConfiguration_ConfigurationChanged(object sender, EventArgs e) => this.NotifyTabStripOfChange(QCommandUIRequest.PerformLayout);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Dispose(bool disposing)
    {
      if (!this.IsDisposed)
      {
        if (disposing)
        {
          if (this.m_oLastDrawnGraphicsPath != null)
          {
            this.m_oLastDrawnGraphicsPath.Dispose();
            this.m_oLastDrawnGraphicsPath = (GraphicsPath) null;
          }
          if (this.m_oDisabledIcon != null)
          {
            this.m_oDisabledIcon.Dispose();
            this.m_oDisabledIcon = (Icon) null;
          }
          if (this.m_oIcon != null)
          {
            this.m_oIcon.Dispose();
            this.m_oIcon = (Icon) null;
          }
          if (this.m_oBackgroundImage != null)
          {
            this.m_oBackgroundImage.Dispose();
            this.m_oBackgroundImage = (Image) null;
          }
        }
        if (this.m_bDisposeFields && this.m_oControl != null)
          this.m_oControl.Dispose();
      }
      this.m_bIsDisposed = true;
    }

    ~QTabButton() => this.Dispose(false);

    QPaintBackgroundObjects IQPaintBackgroundObjectsProvider.GetBackgroundObjects(
      QPaintBackgroundObjects currentObjects,
      Control translateBoundsToControl)
    {
      if (this.TabStrip == null || this.Control == null || !(this.TabStrip.TabStripHost is Control tabStripHost) || this.TabStrip == null || !this.CurrentAppearance.UseControlBackgroundForTabButton)
        return currentObjects;
      Rectangle andControlBounds = this.TabStrip.Painter.GetButtonAndControlBounds(this);
      Rectangle screen = tabStripHost.RectangleToScreen(andControlBounds);
      Rectangle client = translateBoundsToControl.RectangleToClient(screen);
      currentObjects.BackgroundBounds = client;
      return currentObjects;
    }
  }
}
