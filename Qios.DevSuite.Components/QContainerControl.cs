// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QContainerControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DesignerCategory("UserControl")]
  [Designer(typeof (QContainerControlDesigner), typeof (IDesigner))]
  [Designer(typeof (QContainerControlDocumentDesigner), typeof (IRootDesigner))]
  [ToolboxItem(false)]
  public class QContainerControl : QContainerControlBase, IQDesignableContainerControl
  {
    private bool m_bReturnTransparentAsBackColor;
    private bool m_bChangingBackgroundImage;
    private QImageAlign m_eBackgroundImageAlign = QImageAlign.Centered;
    private Point m_oBackgroundImageOffset = Point.Empty;
    private QColorScheme m_oColorScheme;
    private QAppearanceBase m_oAppearance;
    private QFontScope m_eFontScope;
    private Font m_oLocalFont;
    private QBalloon m_oBalloon;
    private string m_sToolTipText;
    private QToolTipConfiguration m_oToolTipConfiguration;
    private IQPaintBackgroundObjectsProvider m_oPaintBackgroundObjectsProvider;
    private bool m_bInitializingDocumentDesigner;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;
    private EventHandler m_oAppearanceChangedEventHandler;
    private EventHandler m_oToolTipConfigurationChangedEventHandler;
    private QWeakDelegate m_oColorsChangedDelegate;
    private QWeakDelegate m_oAppearanceChangedDelegate;

    public QContainerControl() => this.InternalConstruct();

    public QContainerControl(IWin32Window owner)
      : base(owner)
    {
      this.InternalConstruct();
    }

    bool IQDesignableContainerControl.InitializingDocumentDesigner
    {
      get => this.m_bInitializingDocumentDesigner;
      set => this.m_bInitializingDocumentDesigner = value;
    }

    private void InternalConstruct()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      this.m_oAppearanceChangedEventHandler = new EventHandler(this.Appearance_AppearanceChanged);
      this.m_oAppearance = this.CreateAppearanceInstance();
      if (this.m_oAppearance != null)
        this.m_oAppearance.AppearanceChanged += this.m_oAppearanceChangedEventHandler;
      this.m_oToolTipConfigurationChangedEventHandler = new EventHandler(this.ToolTip_ConfigurationChanged);
      this.m_oToolTipConfiguration = this.CreateToolTipConfigurationInstance();
      if (this.m_oToolTipConfiguration != null)
        this.m_oToolTipConfiguration.ConfigurationChanged += this.m_oToolTipConfigurationChangedEventHandler;
      this.m_oColorSchemeColorsChangedEventHandler = new EventHandler(this.ColorScheme_ColorsChanged);
      this.m_oColorScheme = new QColorScheme();
      this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
      QGlobalFont.Instance.FontChanged += new EventHandler(this.QGlobalFontInstance_FontChanged);
      this.m_oLocalFont = Control.DefaultFont;
      this.m_eFontScope = this.InitialFontScope;
      this.SetFontToFontScope();
      this.ResumeLayout(false);
    }

    [Description("Gets the FontScope to initialize the QContainerControl with")]
    [Browsable(false)]
    protected virtual QFontScope InitialFontScope => QFontScope.Global;

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the colors or the QColorScheme changes")]
    public event EventHandler ColorsChanged
    {
      add => this.m_oColorsChangedDelegate = QWeakDelegate.Combine(this.m_oColorsChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oColorsChangedDelegate = QWeakDelegate.Remove(this.m_oColorsChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Description("Gets raised when the Appearance changes")]
    [Category("QEvents")]
    public event EventHandler AppearanceChanged
    {
      add => this.m_oAppearanceChangedDelegate = QWeakDelegate.Combine(this.m_oAppearanceChangedDelegate, (Delegate) value, this.WeakEventHandlers);
      remove => this.m_oAppearanceChangedDelegate = QWeakDelegate.Remove(this.m_oAppearanceChangedDelegate, (Delegate) value);
    }

    [Description("Contains a possible background image. Be aware that on a lot of QControls the control itself is painted after the BackgroundImage, so to see the image you need to set the Background colors of the control to Transparent or semi-transparent.")]
    [Browsable(true)]
    public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set => base.BackgroundImage = value;
    }

    [DefaultValue(QImageAlign.Centered)]
    [Category("Appearance")]
    [Description("Gets or sets the alignment of the BackgroundImage for this QContainerControl")]
    public QImageAlign BackgroundImageAlign
    {
      get => this.m_eBackgroundImageAlign;
      set
      {
        this.m_eBackgroundImageAlign = value;
        this.Invalidate();
      }
    }

    [DefaultValue(typeof (Point), "0,0")]
    [Description("Gets or sets a relative offset to add to the BackgroundImage position")]
    [Category("Appearance")]
    public Point BackgroundImageOffset
    {
      get => this.m_oBackgroundImageOffset;
      set
      {
        this.m_oBackgroundImageOffset = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    public override ImageLayout BackgroundImageLayout
    {
      get => base.BackgroundImageLayout;
      set => base.BackgroundImageLayout = value;
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [Description("Gets or sets the QColorScheme that is used")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set
      {
        if (this.m_oColorScheme == value)
          return;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
        this.m_oColorScheme = value;
        if (this.m_oColorScheme != null)
          this.m_oColorScheme.ColorsChanged += this.m_oColorSchemeColorsChangedEventHandler;
        this.OnColorsChanged(EventArgs.Empty);
        if (this.m_oColorScheme != null && !this.IsDisposed)
          this.Refresh();
        this.SetBalloonToConfiguration();
      }
    }

    public bool ShouldSerializeAppearance() => this.Appearance != null && !this.Appearance.IsSetToDefaultValues();

    public void ResetAppearance()
    {
      if (this.Appearance == null)
        return;
      this.Appearance.SetToDefaultValues();
    }

    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QAppearanceBase Appearance => this.m_oAppearance;

    [Description("Gets or sets the ToolTipText. This must contain Xml as used with QMarkupText The ToolTip, see ToolTipConfiguration, must be enabled for this to show.")]
    [Category("QAppearance")]
    [DefaultValue(null)]
    [Localizable(true)]
    public virtual string ToolTipText
    {
      get => this.m_sToolTipText;
      set
      {
        if (this.m_sToolTipText == value)
          return;
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sToolTipText = value;
        if (this.m_oBalloon == null)
          this.SetBalloonToConfiguration();
        else
          this.m_oBalloon.SetMarkupText((Control) this, this.m_sToolTipText);
      }
    }

    public bool ShouldSerializeToolTipConfiguration() => this.m_oToolTipConfiguration != null && !this.m_oToolTipConfiguration.IsSetToDefaultValues();

    public void ResetToolTipConfiguration()
    {
      if (this.m_oToolTipConfiguration == null)
        return;
      this.m_oToolTipConfiguration.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QToolTipConfiguration for the QContainerControl.")]
    public virtual QToolTipConfiguration ToolTipConfiguration
    {
      get => this.m_oToolTipConfiguration;
      set
      {
        if (this.m_oToolTipConfiguration == value)
          return;
        if (this.m_oToolTipConfiguration != null)
          this.m_oToolTipConfiguration.ConfigurationChanged -= this.m_oToolTipConfigurationChangedEventHandler;
        this.m_oToolTipConfiguration = value;
        if (this.m_oToolTipConfiguration != null)
          this.m_oToolTipConfiguration.ConfigurationChanged += this.m_oToolTipConfigurationChangedEventHandler;
        this.SetBalloonToConfiguration();
      }
    }

    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Font Font
    {
      get => base.Font;
      set => base.Font = value;
    }

    [Localizable(true)]
    [Description("The scope of the font. When the scope is set to Local the LocalFont is used. Else the Font is defined by Windows or QGlobalFont.")]
    [Category("QAppearance")]
    [DefaultValue(QFontScope.Global)]
    public QFontScope FontScope
    {
      get => this.m_eFontScope;
      set
      {
        if (this.m_eFontScope == value)
          return;
        this.m_eFontScope = value;
        this.SetFontToFontScope();
        this.PerformLayout();
        this.Invalidate();
      }
    }

    public bool ShouldSerializeLocalFont() => !object.Equals((object) this.LocalFont, (object) Control.DefaultFont);

    public void ResetLocalFont() => this.LocalFont = Control.DefaultFont;

    [Localizable(true)]
    [Description("The LocalFont is used when the FontScope is set to Local")]
    [Category("QAppearance")]
    public Font LocalFont
    {
      get => this.m_oLocalFont;
      set
      {
        if (this.m_oLocalFont == value)
          return;
        this.m_oLocalFont = value;
        this.SetFontToFontScope();
        this.PerformLayout();
        this.Invalidate();
      }
    }

    public new bool ShouldSerializeBackColor() => this.m_bInitializingDocumentDesigner;

    public override void ResetBackColor() => this.ColorScheme[this.BackColorPropertyName].Reset();

    [Description("Gets or sets the backcolor of this Control")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public override Color BackColor
    {
      get => this.m_bReturnTransparentAsBackColor ? Color.Transparent : this.ColorScheme[this.BackColorPropertyName].Current;
      set => this.ColorScheme[this.BackColorPropertyName].Current = value;
    }

    internal Color BackColorInternal => this.ColorScheme[this.BackColorPropertyName].Current;

    public bool ShouldSerializeBackColor2() => false;

    public void ResetBackColor2() => this.ColorScheme[this.BackColor2PropertyName].Reset();

    [Category("Appearance")]
    [Description("Gets or sets the second background color of this QContainerControl. This color is used when the Appearance is set to Gradient.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual Color BackColor2
    {
      get => this.ColorScheme[this.BackColor2PropertyName].Current;
      set => this.ColorScheme[this.BackColor2PropertyName].Current = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    public bool ShouldSerializeBorderColor() => false;

    public void ResetBorderColor() => this.ColorScheme[this.BorderColorPropertyName].Reset();

    [Description(" Gets or sets the border color of this QContainerControl. ")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public virtual Color BorderColor
    {
      get => this.ColorScheme[this.BorderColorPropertyName].Current;
      set => this.ColorScheme[this.BorderColorPropertyName].Current = value;
    }

    internal bool IsLayered => (this.CreateParams.ExStyle & 524288) == 524288;

    internal IQPaintBackgroundObjectsProvider PaintBackgroundObjectsProvider
    {
      get => this.m_oPaintBackgroundObjectsProvider;
      set => this.m_oPaintBackgroundObjectsProvider = value;
    }

    public override void Refresh()
    {
      base.Refresh();
      this.RefreshNoClientArea();
    }

    internal new void UpdateBounds() => base.UpdateBounds();

    private void SetBalloonToConfiguration()
    {
      if (this.m_oToolTipConfiguration != null && this.m_oToolTipConfiguration.Enabled)
      {
        this.SecureBalloon();
        this.m_oBalloon.Configuration = (QBalloonConfiguration) this.m_oToolTipConfiguration;
        this.m_oBalloon.ColorScheme = this.ColorScheme;
        this.m_oBalloon.FontScope = this.FontScope;
        this.m_oBalloon.LocalFont = this.LocalFont;
        this.m_oBalloon.SetMarkupText((Control) this, this.m_sToolTipText);
      }
      else
      {
        if (this.m_oBalloon == null)
          return;
        this.m_oBalloon.Dispose();
        this.m_oBalloon = (QBalloon) null;
      }
    }

    protected virtual QBalloon CreateBalloon() => new QBalloon();

    private void SecureBalloon()
    {
      if (this.m_oBalloon != null)
        return;
      this.m_oBalloon = this.CreateBalloon();
      if (this.m_sToolTipText == null || this.m_sToolTipText.Length <= 0 || this.m_sToolTipText == null || !(this.m_sToolTipText != ""))
        return;
      this.m_oBalloon.AddListener((Control) this, this.m_sToolTipText);
    }

    protected virtual void SetFontToFontScope()
    {
      if (this.m_eFontScope == QFontScope.Windows)
        this.Font = SystemInformation.MenuFont;
      else if (this.m_eFontScope == QFontScope.Global)
      {
        this.Font = QGlobalFont.Instance.Font;
      }
      else
      {
        if (this.m_eFontScope != QFontScope.Local)
          return;
        this.Font = this.m_oLocalFont;
      }
    }

    private void ColorScheme_ColorsChanged(object sender, EventArgs e)
    {
      if (this.IsDisposed)
        return;
      this.SetFontToFontScope();
      this.PerformLayout();
      this.OnColorsChanged(EventArgs.Empty);
      this.Refresh();
    }

    private void QGlobalFontInstance_FontChanged(object sender, EventArgs e)
    {
      if (this.FontScope != QFontScope.Global || this.IsDisposed)
        return;
      this.SetFontToFontScope();
      this.PerformLayout();
      this.Invalidate();
    }

    private void Appearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformNonClientAreaLayout();
      this.PerformLayout();
      this.OnAppearanceChanged(EventArgs.Empty);
      this.Refresh();
    }

    private void ToolTip_ConfigurationChanged(object sender, EventArgs e) => this.SetBalloonToConfiguration();

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      if (this.m_oBalloon == null)
        return;
      this.m_oBalloon.Font = this.Font;
    }

    protected override void OnBackgroundImageChanged(EventArgs e)
    {
      if (this.m_bChangingBackgroundImage)
        return;
      base.OnBackgroundImageChanged(e);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      bool flag1 = QControlPaint.ContainsTransparentAreas(this.BackColor, this.BackColor2, this.Appearance);
      bool flag2 = !this.IsLayered && QControlPaint.IsSolid(this.BackColor, this.BackColor2, this.Appearance);
      if (flag1 || flag2)
      {
        Image image = (Image) null;
        if (this.BackgroundImage != null)
        {
          this.m_bChangingBackgroundImage = true;
          image = base.BackgroundImage;
          base.BackgroundImage = (Image) null;
          this.m_bChangingBackgroundImage = false;
        }
        if (flag1 && !flag2)
          this.m_bReturnTransparentAsBackColor = true;
        base.OnPaintBackground(pevent);
        if (flag1 && !flag2)
          this.m_bReturnTransparentAsBackColor = false;
        if (image != null)
        {
          this.m_bChangingBackgroundImage = true;
          base.BackgroundImage = image;
          this.m_bChangingBackgroundImage = false;
        }
      }
      if (!flag2)
      {
        QPaintBackgroundObjects currentObjects = new QPaintBackgroundObjects();
        Rectangle client = this.NonClientRectangleToClient(new Rectangle(Point.Empty, this.CurrentBounds.Size));
        currentObjects.BackgroundBounds = client;
        QAppearanceFillerProperties fillerProperties = new QAppearanceFillerProperties();
        if (this.m_oPaintBackgroundObjectsProvider != null)
          currentObjects = this.m_oPaintBackgroundObjectsProvider.GetBackgroundObjects(currentObjects, (Control) this);
        fillerProperties.AlternativeBoundsForBrushCreation = currentObjects.BackgroundBounds;
        QRectanglePainter.Default.FillBackground(client, (IQAppearance) this.Appearance, new QColorSet(this.BackColor, this.BackColor2, this.BorderColor, this.ForeColor), QRectanglePainterProperties.Default, fillerProperties, QPainterOptions.FillBackground, pevent.Graphics);
      }
      if (this.BackgroundImage == null)
        return;
      QControlPaint.DrawImage(this.BackgroundImage, this.BackgroundImageAlign, this.m_oBackgroundImageOffset, this.ClientRectangle, this.BackgroundImage.Size, pevent.Graphics, (ImageAttributes) null, false);
    }

    protected override void OnPaintNonClientArea(PaintEventArgs e)
    {
      base.OnPaintNonClientArea(e);
      e.Graphics.Clear(this.BorderColor);
    }

    protected virtual QAppearanceBase CreateAppearanceInstance() => new QAppearanceBase();

    protected virtual QToolTipConfiguration CreateToolTipConfigurationInstance() => new QToolTipConfiguration();

    protected virtual string BackColorPropertyName => "ContainerControlBackground1";

    protected virtual string BackColor2PropertyName => "ContainerControlBackground2";

    protected virtual string BorderColorPropertyName => "ContainerControlBorder";

    protected virtual void OnColorsChanged(EventArgs e) => this.m_oColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oColorsChangedDelegate, (object) this, (object) e);

    protected virtual void OnAppearanceChanged(EventArgs e) => this.m_oAppearanceChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oAppearanceChangedDelegate, (object) this, (object) e);

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_oBalloon != null)
        {
          this.m_oBalloon.Dispose();
          this.m_oBalloon = (QBalloon) null;
        }
        if (this.m_oColorScheme != null && !this.m_oColorScheme.IsDisposed)
        {
          this.m_oColorScheme.ColorsChanged -= this.m_oColorSchemeColorsChangedEventHandler;
          this.m_oColorScheme.Dispose();
        }
      }
      base.Dispose(disposing);
    }
  }
}
