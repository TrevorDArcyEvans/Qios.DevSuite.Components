// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStatusBarPanel
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  [TypeConverter(typeof (QStatusBarPanelConverter))]
  public class QStatusBarPanel : Component
  {
    private QAppearanceType m_eAppearanceType = QAppearanceType.Inherited;
    private QStatusBar m_oParent;
    private bool m_bIsDisposed;
    private Icon m_oIcon;
    private HorizontalAlignment m_eIconAlignment;
    private string m_sText;
    private int m_iMinWidth;
    private int m_iRequestedWidth = 50;
    private QPadding m_oPadding;
    private StringAlignment m_eAlignment;
    private StatusBarPanelAutoSize m_eAutoSize = StatusBarPanelAutoSize.None;
    private string m_sToolTipText;
    private bool m_bEnabled = true;
    private Rectangle m_oBounds;
    private QAppearance m_oCustomAppearance;
    private EventHandler m_oCustomAppearanceChangedEventHandler;

    public QStatusBarPanel()
    {
      this.m_oPadding = new QPadding(1, 1, 1, 1);
      this.m_oCustomAppearanceChangedEventHandler = new EventHandler(this.CustomAppearance_AppearanceChanged);
      this.CustomAppearance = new QAppearance();
    }

    [DefaultValue(HorizontalAlignment.Left)]
    [Description("Gets or sets the alignment of the icon")]
    [Category("QAppearance")]
    public HorizontalAlignment IconAlignment
    {
      get => this.m_eIconAlignment;
      set
      {
        this.m_eIconAlignment = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
      }
    }

    [Category("QBehavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the Icon of the QStatusBarPanel")]
    public Icon Icon
    {
      get => this.m_oIcon;
      set
      {
        this.m_oIcon = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
      }
    }

    [Description("Gets or sets the text of the QStatusBarpanel.")]
    [DefaultValue(null)]
    [Localizable(true)]
    [Category("QBehavior")]
    public string Text
    {
      get => this.m_sText;
      set
      {
        this.m_sText = value;
        if (this.Parent == null || this.Parent.IsDisposed)
          return;
        this.Parent.SetToolTipOfPanel(this);
        if (this.Parent.PerformingLayout)
          return;
        this.Parent.PerformLayout();
      }
    }

    [DefaultValue(null)]
    [Category("QBehavior")]
    [Description("Gets or sets the ToolTipText of the QStatusBarPanel. This must contain Xml as used with QMarkupText")]
    [Localizable(true)]
    public string ToolTipText
    {
      get => this.m_sToolTipText;
      set
      {
        this.m_sToolTipText = value;
        if (this.Parent == null || this.Parent.IsDisposed)
          return;
        this.Parent.SetToolTipOfPanel(this);
      }
    }

    [Browsable(false)]
    public string UsedToolTipText => !QMisc.IsEmpty((object) this.m_sToolTipText) ? this.m_sToolTipText : HttpUtility.HtmlEncode(this.m_sText);

    [Description("Gets or sets the alignment of the text on the QStatusBarPanel")]
    [Category("QAppearance")]
    [DefaultValue(StringAlignment.Near)]
    public StringAlignment Alignment
    {
      get => this.m_eAlignment;
      set
      {
        if (this.m_eAlignment == value)
          return;
        this.m_eAlignment = value;
        if (this.Parent == null || this.Parent.IsDisposed)
          return;
        this.Parent.Refresh();
      }
    }

    [Description("Gets or sets the type of autosize of the QStatusBarPanel")]
    [Category("QAppearance")]
    [DefaultValue(StatusBarPanelAutoSize.None)]
    public StatusBarPanelAutoSize AutoSize
    {
      get => this.m_eAutoSize;
      set
      {
        if (this.m_eAutoSize == value)
          return;
        this.m_eAutoSize = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
      }
    }

    [Browsable(false)]
    public Rectangle Bounds => this.m_oBounds;

    [Browsable(false)]
    public QStatusBar Parent => this.m_oParent;

    [Category("QAppearance")]
    [DefaultValue(0)]
    [Localizable(true)]
    [Description("Gets or sets the minimum width of the QStatusBarPanel")]
    public int MinWidth
    {
      get => this.m_iMinWidth;
      set
      {
        if (this.m_iMinWidth == value)
          return;
        this.m_iMinWidth = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
      }
    }

    [Localizable(true)]
    [Description("Gets or sets the requested width. This width is used when the AutoSize property is set to None")]
    [DefaultValue(50)]
    [Category("QAppearance")]
    public int RequestedWidth
    {
      get => this.m_iRequestedWidth;
      set
      {
        if (this.m_iRequestedWidth == value)
          return;
        this.m_iRequestedWidth = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
      }
    }

    [Description("Gets or sets the Width of the Panel")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Width
    {
      get => this.m_oBounds.Width;
      set
      {
        if (this.m_oBounds.Width == value)
          return;
        this.m_oBounds.Width = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
      }
    }

    [Browsable(false)]
    public bool IsDisposed => this.m_bIsDisposed;

    [Description("Gets or sets whether the text of this QStatusBarPanel is enabled.")]
    [DefaultValue(true)]
    [Category("QBehavior")]
    public bool Enabled
    {
      get => this.m_bEnabled;
      set
      {
        if (this.m_bEnabled == value)
          return;
        this.m_bEnabled = value;
        if (this.Parent == null || this.Parent.IsDisposed)
          return;
        this.Parent.Refresh();
      }
    }

    [DefaultValue(typeof (QPadding), "1,1,1,1")]
    [Description("Gets or sets the padding between the contents and the edge of the QStatusBarPanel")]
    [Category("QAppearance")]
    public QPadding Padding
    {
      get => this.m_oPadding;
      set
      {
        this.m_oPadding = value;
        if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
          return;
        this.Parent.PerformLayout();
        this.Parent.Refresh();
      }
    }

    [Description("Gets or sets the AppearanceType of the Panel. If the type is set to Custom CustomAppearance is used")]
    [DefaultValue(QAppearanceType.Inherited)]
    [Category("QAppearance")]
    public QAppearanceType AppearanceType
    {
      get => this.m_eAppearanceType;
      set
      {
        this.m_eAppearanceType = value;
        if (this.Parent == null || this.Parent.IsDisposed)
          return;
        this.Parent.Refresh();
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void SetBounds(Rectangle rectangle)
    {
      this.m_oBounds = rectangle;
      if (this.Parent == null || this.Parent.PerformingLayout || this.Parent.IsDisposed)
        return;
      this.Parent.PerformLayout();
    }

    internal void SetParent(QStatusBar parent) => this.m_oParent = parent;

    public bool ShouldSerializeCustomAppearance() => !this.m_oCustomAppearance.IsSetToDefaultValues();

    public void ResetCustomAppearance() => this.m_oCustomAppearance.SetToDefaultValues();

    [Category("QAppearance")]
    [Description("Gets or sets the custom appearance of this QStatusBarPanel. This property is used when the AppearanceType is set to Custom")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QAppearance CustomAppearance
    {
      get => this.m_oCustomAppearance;
      set
      {
        if (this.m_oCustomAppearance == value)
          return;
        if (this.m_oCustomAppearance != null)
          this.m_oCustomAppearance.AppearanceChanged -= this.m_oCustomAppearanceChangedEventHandler;
        this.m_oCustomAppearance = value;
        if (this.m_oCustomAppearance != null)
          this.m_oCustomAppearance.AppearanceChanged += this.m_oCustomAppearanceChangedEventHandler;
        if (this.Parent == null || this.Parent.IsDisposed)
          return;
        this.Parent.Refresh();
      }
    }

    [Browsable(false)]
    public QAppearance Appearance
    {
      get
      {
        if (this.AppearanceType == QAppearanceType.Inherited)
        {
          if (this.Parent != null && !this.Parent.IsDisposed)
            return this.Parent.PanelAppearance;
        }
        else if (this.AppearanceType == QAppearanceType.Custom)
          return this.CustomAppearance;
        return (QAppearance) null;
      }
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual Size CalcuateSizeOfContents(Graphics graphicsToCalculate)
    {
      if (this.Parent == null || this.Parent.IsDisposed)
        return Size.Empty;
      StringFormat stringFormat = new StringFormat(StringFormat.GenericDefault);
      stringFormat.Alignment = this.Alignment;
      stringFormat.FormatFlags = StringFormatFlags.FitBlackBox | StringFormatFlags.NoWrap;
      stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      SizeF sizeF = graphicsToCalculate.MeasureString(this.Text, this.Parent.Font);
      Size size = new Size((int) Math.Ceiling((double) sizeF.Width), (int) Math.Ceiling((double) sizeF.Height));
      if (this.Icon != null)
      {
        if (this.IconAlignment == HorizontalAlignment.Left && this.Alignment == StringAlignment.Far || this.IconAlignment == HorizontalAlignment.Right && this.Alignment == StringAlignment.Near)
          size.Width += this.Icon.Width;
        else
          size.Width = Math.Max(size.Width, this.Icon.Width);
        size.Height = Math.Max(size.Height, this.Icon.Height);
      }
      size.Width += this.Padding.Left + this.Padding.Right;
      size.Height += this.Padding.Top + this.Padding.Bottom;
      stringFormat.Dispose();
      return size;
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void DrawBackGround(Graphics graphics)
    {
      if (this.Parent == null || this.Parent.IsDisposed || this.Appearance == null)
        return;
      QStatusBar parent = this.Parent;
      if (parent.ColorScheme == null || this.Bounds.Width - 1 <= 0 || this.Bounds.Height - 1 <= 0)
        return;
      QRectanglePainter.Default.Paint(new Rectangle(this.Bounds.Left, this.Bounds.Top, this.Bounds.Width - 1, this.Bounds.Height - 1), (IQAppearance) this.Appearance, new QColorSet((Color) parent.ColorScheme.StatusBarPanelBackground1, (Color) parent.ColorScheme.StatusBarPanelBackground2, (Color) parent.ColorScheme.StatusBarPanelBorder), QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, graphics);
    }

    [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "00240000048000009400000006020000002400005253413100040000010001008b5b8aca502cebae941f194bffa14be6e16725ebdafd43040a71ac4d95863f3a53d7a10b777b66efcbb0341adb7c52789cd2de3329409d21193868346d08788661aa142b4309fcdd56f4a660e33c4ffead58cfa62d91b438f6e89d8abffaf03c97ff50c1de956c095930dcae8c7dc5598d452c6f811f01c302af48d34e2317b8")]
    internal virtual void Draw(Graphics graphics)
    {
      if (this.Parent == null || this.Parent.IsDisposed || this.Appearance == null)
        return;
      QStatusBar parent = this.Parent;
      if (parent.ColorScheme == null)
        return;
      Rectangle layoutRectangle = new Rectangle(this.Bounds.Left + this.Padding.Left, this.Bounds.Top, this.Bounds.Width - this.Padding.Left - this.Padding.Right, this.Bounds.Height);
      if (layoutRectangle.Width <= 0 || layoutRectangle.Height <= 0)
        return;
      Brush brush = !this.m_bEnabled || !this.Parent.Enabled ? (Brush) new SolidBrush((Color) parent.ColorScheme.DisabledTextColor) : (Brush) new SolidBrush((Color) parent.ColorScheme.TextColor);
      StringFormat format = new StringFormat(StringFormat.GenericDefault);
      format.Alignment = this.Alignment;
      format.FormatFlags = StringFormatFlags.NoWrap;
      format.Trimming = StringTrimming.EllipsisCharacter;
      format.LineAlignment = StringAlignment.Center;
      graphics.DrawString(this.Text, this.Parent.Font, brush, (RectangleF) layoutRectangle, format);
      if (this.m_oIcon != null)
      {
        int top = (layoutRectangle.Height - this.m_oIcon.Height) / 2 + layoutRectangle.Top;
        switch (this.m_eIconAlignment)
        {
          case HorizontalAlignment.Left:
            QControlPaint.DrawIcon(this.m_oIcon, this.Bounds.Left + this.Padding.Left, top, graphics);
            break;
          case HorizontalAlignment.Right:
            QControlPaint.DrawIcon(this.m_oIcon, this.Bounds.Left + (layoutRectangle.Width - 1 - this.m_oIcon.Width) + this.Padding.Left, top, graphics);
            break;
          case HorizontalAlignment.Center:
            QControlPaint.DrawIcon(this.m_oIcon, this.Bounds.Left + (layoutRectangle.Width - this.m_oIcon.Width) / 2 + this.Padding.Left, top, graphics);
            break;
        }
      }
      format.Dispose();
      brush.Dispose();
    }

    private void CustomAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      if (this.m_eAppearanceType != QAppearanceType.Inherited || this.Parent == null || this.Parent.IsDisposed)
        return;
      this.Parent.Refresh();
    }

    protected override void Dispose(bool disposing)
    {
      this.m_bIsDisposed = true;
      base.Dispose(disposing);
    }
  }
}
