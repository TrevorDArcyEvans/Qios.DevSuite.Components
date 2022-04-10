// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QControl
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QControlDesigner), typeof (IDesigner))]
  [ToolboxItem(false)]
  public abstract class QControl : Control, IQWeakEventPublisher, IQDesignableContainerControl
  {
    private bool m_bWeakEventHandlers = true;
    private bool m_bInitializingDocumentDesigner;
    private IntPtr m_hWindowsXPTheme = IntPtr.Zero;
    private bool m_bWindowsXPThemeTried;
    private QControlStyles m_eStyles;
    private ArrayList m_aSetTimers;
    private bool m_bReturnTransparentAsBackColor;
    private bool m_bChangingBackgroundImage;
    private QImageAlign m_eBackgroundImageAlign = QImageAlign.Centered;
    private Point m_oBackgroundImageOffset = Point.Empty;
    private QColorScheme m_oColorScheme;
    private QAppearanceBase m_oAppearance;
    private QFontScope m_eFontScope;
    private Font m_oLocalFont;
    private EventHandler m_oColorSchemeColorsChangedEventHandler;
    private EventHandler m_oToolTipConfigurationChangedEventHandler;
    private EventHandler m_oAppearanceChangedEventHandler;
    private QBalloon m_oBalloon;
    private string m_sToolTipText;
    private ArrayList m_oAdditionalToolTipControls;
    private QToolTipConfiguration m_oToolTipConfiguration;
    private QWeakDelegate m_oWindowsXPThemeChangedDelegate;
    private QWeakDelegate m_oColorsChangedDelegate;
    private QWeakDelegate m_oTimerElapsedDelegate;

    protected QControl()
    {
      this.SuspendLayout();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
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

    [Browsable(true)]
    [Description("Contains a possible background image. Be aware that on a lot of QControls the control itself is painted after the BackgroundImage, so to see the image you need to set the Background colors of the control to Transparent or semi-transparent.")]
    public override Image BackgroundImage
    {
      get => base.BackgroundImage;
      set => base.BackgroundImage = value;
    }

    [Description("Gets the or sets the Alignment of the BackgroundImage for the QControl.")]
    [Category("Appearance")]
    [DefaultValue(QImageAlign.Centered)]
    public QImageAlign BackgroundImageAlign
    {
      get => this.m_eBackgroundImageAlign;
      set
      {
        this.m_eBackgroundImageAlign = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets a relative offset to add to the BackgroundImage position")]
    [Category("Appearance")]
    [DefaultValue(typeof (Point), "0,0")]
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

    [Browsable(false)]
    public new Padding Padding
    {
      get => base.Padding;
      set => base.Padding = value;
    }

    [Browsable(false)]
    public override RightToLeft RightToLeft
    {
      get => base.RightToLeft;
      set => base.RightToLeft = value;
    }

    [Description("Gets the FontScope to initialize the QControl with")]
    [Browsable(false)]
    protected virtual QFontScope InitialFontScope => QFontScope.Global;

    protected void SetQControlStyles(QControlStyles styles, bool value)
    {
      if (value)
        this.m_eStyles |= styles;
      else
        this.m_eStyles &= ~styles;
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Is raised when the Windows XP theme is changed")]
    public event EventHandler WindowsXPThemeChanged
    {
      add => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Combine(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.Remove(this.m_oWindowsXPThemeChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when the colors or the QColorScheme changes")]
    public event EventHandler ColorsChanged
    {
      add => this.m_oColorsChangedDelegate = QWeakDelegate.Combine(this.m_oColorsChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oColorsChangedDelegate = QWeakDelegate.Remove(this.m_oColorsChangedDelegate, (Delegate) value);
    }

    [QWeakEvent]
    [Category("QEvents")]
    [Description("Gets raised when a timer that was set with the StartTimer method elapses")]
    public event QControlTimerEventHandler TimerElapsed
    {
      add => this.m_oTimerElapsedDelegate = QWeakDelegate.Combine(this.m_oTimerElapsedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oTimerElapsedDelegate = QWeakDelegate.Remove(this.m_oTimerElapsedDelegate, (Delegate) value);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    [Browsable(false)]
    public Form ParentForm
    {
      get
      {
        Control parent = this.Parent;
        Form parentForm;
        for (parentForm = parent as Form; parent != null && parentForm == null; parentForm = parent as Form)
          parent = parent.Parent;
        return parentForm;
      }
    }

    public bool ShouldSerializeColorScheme() => this.ColorScheme.ShouldSerialize();

    public void ResetColorScheme() => this.ColorScheme.Reset();

    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
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

    public bool ShouldSerializeAppearance() => this.m_oAppearance != null && !this.Appearance.IsSetToDefaultValues();

    public void ResetAppearance()
    {
      if (this.m_oAppearance == null)
        return;
      this.m_oAppearance.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance for the QControl.")]
    public virtual QAppearanceBase Appearance => this.m_oAppearance;

    [Description("Gets or sets the ToolTipText. This must contain Xml as used with QMarkupText. The ToolTip, see ToolTipConfiguration, must be enabled for this to show.")]
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
          this.SetBalloonMarkupText();
      }
    }

    public bool ShouldSerializeToolTipConfiguration() => this.m_oToolTipConfiguration != null && !this.m_oToolTipConfiguration.IsSetToDefaultValues();

    public void ResetToolTipConfiguration()
    {
      if (this.m_oToolTipConfiguration == null)
        return;
      this.m_oToolTipConfiguration.SetToDefaultValues();
    }

    [Category("QAppearance")]
    [Description("Gets or sets the QToolTipConfiguration for the QControl.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

    [Description("The scope of the font. When the scope is set to Local the LocalFont is used. Else the Font is defined by Windows or the QGlobalFont.")]
    [DefaultValue(QFontScope.Global)]
    [Category("QAppearance")]
    [Localizable(true)]
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
    [Category("QAppearance")]
    [Description("The LocalFont is used when the FontScope is set to Local")]
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

    [Category("Appearance")]
    [Description("Gets or sets the backcolor of this Control")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public override Color BackColor
    {
      get => this.m_bReturnTransparentAsBackColor ? Color.Transparent : this.ColorScheme[this.BackColorPropertyName].Current;
      set => this.ColorScheme[this.BackColorPropertyName].Current = value;
    }

    public bool ShouldSerializeBackColor2() => false;

    public void ResetBackColor2() => this.ColorScheme[this.BackColor2PropertyName].Reset();

    [Description("Gets or sets the second backcolor of this Control. This Color is used when the Appearance is set to Gradient.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public virtual Color BackColor2
    {
      get => this.ColorScheme[this.BackColor2PropertyName].Current;
      set => this.ColorScheme[this.BackColor2PropertyName].Current = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    public bool ShouldSerializeBorderColor() => false;

    public void ResetBorderColor() => this.ColorScheme[this.BorderColorPropertyName].Reset();

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets or sets the bordercolor of this control.")]
    public virtual Color BorderColor
    {
      get => this.ColorScheme[this.BorderColorPropertyName].Current;
      set => this.ColorScheme[this.BorderColorPropertyName].Current = value;
    }

    internal bool IsLayered => (this.CreateParams.ExStyle & 524288) == 524288;

    public void AddAdditionalToolTipControl(Control control)
    {
      if (this.m_oAdditionalToolTipControls == null)
        this.m_oAdditionalToolTipControls = new ArrayList();
      if (!this.m_oAdditionalToolTipControls.Contains((object) control))
        this.m_oAdditionalToolTipControls.Add((object) control);
      if (this.m_oBalloon == null)
        return;
      this.m_oBalloon.SetMarkupText(control, this.m_sToolTipText);
    }

    public void RemoveAdditionalToolTipControl(Control control)
    {
      if (this.m_oAdditionalToolTipControls != null)
        this.m_oAdditionalToolTipControls.Remove((object) control);
      if (this.m_oBalloon == null)
        return;
      this.m_oBalloon.SetMarkupText(control, (string) null);
    }

    public void StartTimer(int timerId, int interval)
    {
      if (this.IsHandleCreated)
        NativeMethods.SetTimer(this.Handle, new IntPtr(timerId), (uint) interval, (QTimerCallbackDelegate) null);
      QTimerDefinition timerWithId = this.FindTimerWithID(timerId);
      if (timerWithId != null)
      {
        timerWithId.TimerInteval = interval;
      }
      else
      {
        if (this.m_aSetTimers == null)
          this.m_aSetTimers = new ArrayList();
        this.m_aSetTimers.Add((object) new QTimerDefinition(timerId, interval));
      }
    }

    public void StopTimer(int timerId)
    {
      if (this.IsHandleCreated)
        NativeMethods.KillTimer(this.Handle, new IntPtr(timerId));
      QTimerDefinition timerWithId = this.FindTimerWithID(timerId);
      if (timerWithId == null)
        return;
      this.m_aSetTimers.Remove((object) timerWithId);
    }

    private QTimerDefinition FindTimerWithID(int timerID)
    {
      if (this.m_aSetTimers == null)
        return (QTimerDefinition) null;
      for (int index = 0; index < this.m_aSetTimers.Count; ++index)
      {
        QTimerDefinition aSetTimer = (QTimerDefinition) this.m_aSetTimers[index];
        if (aSetTimer.TimerID == timerID)
          return aSetTimer;
      }
      return (QTimerDefinition) null;
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
      this.Invalidate();
    }

    private void QGlobalFontInstance_FontChanged(object sender, EventArgs e)
    {
      if (this.FontScope != QFontScope.Global || this.IsDisposed)
        return;
      this.SetFontToFontScope();
      this.PerformLayout();
      this.Invalidate();
    }

    private void ToolTip_ConfigurationChanged(object sender, EventArgs e) => this.SetBalloonToConfiguration();

    private void Appearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Refresh();
    }

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

    protected void DefaultOnPaintBackground(PaintEventArgs e) => base.OnPaintBackground(e);

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get => base.CreateParams;
    }

    protected void DefaultPaintBackground(
      QColorSet colorSet,
      QAppearanceBase appearance,
      PaintEventArgs pevent)
    {
      bool flag1 = QControlPaint.ContainsTransparentAreas(colorSet.Background1, colorSet.Background2, appearance);
      bool flag2 = !this.IsLayered && QControlPaint.IsSolid(colorSet.Background1, colorSet.Background2, appearance);
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
        QRectanglePainter.Default.FillBackground(this.ClientRectangle, (IQAppearance) appearance, colorSet, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, pevent.Graphics);
      if (this.BackgroundImage != null)
        QControlPaint.DrawImage(this.BackgroundImage, this.BackgroundImageAlign, this.m_oBackgroundImageOffset, this.ClientRectangle, this.BackgroundImage.Size, pevent.Graphics, (ImageAttributes) null, false);
      if (!this.DrawBorders)
        return;
      QRectanglePainter.Default.FillForeground(this.ClientRectangle, (IQAppearance) appearance, colorSet, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, pevent.Graphics);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent) => this.DefaultPaintBackground(new QColorSet(this.BackColor, this.BackColor2, this.BorderColor), this.Appearance, pevent);

    [Browsable(false)]
    protected virtual bool DrawBorders => true;

    protected virtual QAppearanceBase CreateAppearanceInstance() => new QAppearanceBase();

    protected virtual QToolTipConfiguration CreateToolTipConfigurationInstance() => new QToolTipConfiguration();

    protected abstract string BackColorPropertyName { get; }

    protected abstract string BackColor2PropertyName { get; }

    protected abstract string BorderColorPropertyName { get; }

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

    private void SetBalloonToConfiguration()
    {
      if (this.m_oToolTipConfiguration != null && this.m_oToolTipConfiguration.Enabled)
      {
        this.SecureBalloon();
        this.m_oBalloon.Configuration = (QBalloonConfiguration) this.m_oToolTipConfiguration;
        this.m_oBalloon.ColorScheme = this.ColorScheme;
        this.m_oBalloon.FontScope = this.FontScope;
        this.m_oBalloon.LocalFont = this.LocalFont;
        this.SetBalloonMarkupText();
      }
      else
      {
        if (this.m_oBalloon == null)
          return;
        this.m_oBalloon.Dispose();
        this.m_oBalloon = (QBalloon) null;
      }
    }

    private void SetBalloonMarkupText()
    {
      if (this.m_oBalloon == null)
        return;
      this.m_oBalloon.SetMarkupText((Control) this, this.m_sToolTipText);
      if (this.m_oAdditionalToolTipControls == null)
        return;
      for (int index = 0; index < this.m_oAdditionalToolTipControls.Count; ++index)
        this.m_oBalloon.SetMarkupText(this.m_oAdditionalToolTipControls[index] as Control, this.m_sToolTipText);
    }

    protected string WindowsXPThemeClass => "Window";

    protected IntPtr WindowsXPTheme
    {
      get
      {
        this.SecureWindowsXpTheme();
        return this.m_hWindowsXPTheme;
      }
    }

    private void SecureWindowsXpTheme()
    {
      if (this.m_bWindowsXPThemeTried || !NativeHelper.WindowsXP)
        return;
      this.m_hWindowsXPTheme = NativeMethods.OpenThemeData(this.Handle, this.WindowsXPThemeClass);
      this.m_bWindowsXPThemeTried = true;
    }

    private void CloseWindowsXpTheme()
    {
      if (this.m_hWindowsXPTheme != IntPtr.Zero)
      {
        NativeMethods.CloseThemeData(this.m_hWindowsXPTheme);
        this.m_hWindowsXPTheme = IntPtr.Zero;
      }
      this.m_bWindowsXPThemeTried = false;
    }

    protected override void Dispose(bool disposing)
    {
      this.CloseWindowsXpTheme();
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

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 794)
      {
        this.CloseWindowsXpTheme();
        this.OnWindowsXPThemeChanged(EventArgs.Empty);
      }
      else if (m.Msg == 275)
        this.OnTimerElapsed(new QControlTimerEventArgs(m.WParam.ToInt32()));
      else if (m.Msg == 70)
      {
        NativeMethods.WINDOWPOS valueType = (NativeMethods.WINDOWPOS) QMisc.PtrToValueType(m.LParam, typeof (NativeMethods.WINDOWPOS));
        valueType.flags |= (this.m_eStyles & QControlStyles.NeverCopyBitsOnBoundsChange) == QControlStyles.NeverCopyBitsOnBoundsChange ? 256U : 0U;
        valueType.flags |= (this.m_eStyles & QControlStyles.NeverRedrawOnBoundsChange) == QControlStyles.NeverRedrawOnBoundsChange ? 8U : 0U;
        Marshal.StructureToPtr((object) valueType, m.LParam, true);
      }
      base.WndProc(ref m);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      if (this.m_aSetTimers == null)
        return;
      for (int index = 0; index < this.m_aSetTimers.Count; ++index)
      {
        QTimerDefinition aSetTimer = (QTimerDefinition) this.m_aSetTimers[index];
        NativeMethods.SetTimer(this.Handle, new IntPtr(aSetTimer.TimerID), (uint) aSetTimer.TimerInteval, (QTimerCallbackDelegate) null);
      }
    }

    protected virtual void OnWindowsXPThemeChanged(EventArgs e) => this.m_oWindowsXPThemeChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oWindowsXPThemeChangedDelegate, (object) this, (object) e);

    protected virtual void OnTimerElapsed(QControlTimerEventArgs e) => this.m_oTimerElapsedDelegate = QWeakDelegate.InvokeDelegate(this.m_oTimerElapsedDelegate, (object) this, (object) e);

    protected virtual void OnColorsChanged(EventArgs e) => this.m_oColorsChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oColorsChangedDelegate, (object) this, (object) e);

    bool IQDesignableContainerControl.InitializingDocumentDesigner
    {
      get => this.m_bInitializingDocumentDesigner;
      set => this.m_bInitializingDocumentDesigner = value;
    }
  }
}
