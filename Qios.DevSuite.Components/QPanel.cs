// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPanel
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QPanel), "Resources.ControlImages.QPanel.bmp")]
  public class QPanel : QContainerControl
  {
    private Container m_oComponents;
    private Rectangle m_oTextRectangle = Rectangle.Empty;
    private Rectangle m_oTextRegionRectangle = Rectangle.Empty;
    private int m_iClientAreaMarginTop;
    private int m_iClientAreaMarginBottom;
    private int m_iClientAreaMarginLeft;
    private int m_iClientAreaMarginRight;
    private int m_iClientAreaPaddingTop;
    private int m_iClientAreaPaddingBottom;
    private int m_iClientAreaPaddingLeft;
    private int m_iClientAreaPaddingRight;
    private EventHandler m_oConfigurationChangedEventHandler;
    private QPanelConfiguration m_oConfiguration;

    public QPanel()
    {
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.m_oComponents = new Container();
      this.m_oConfigurationChangedEventHandler = new EventHandler(this.Panel_ConfigurationChanged);
      this.m_oConfiguration = this.CreateConfigurationInstance();
      this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QAppearance();

    [Description("Gets or sets the QAppearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public virtual QAppearance Appearance => (QAppearance) base.Appearance;

    [Browsable(false)]
    [Obsolete("Obsolete since 1.0.7.20, use QAppearance.BorderWidth instead")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int BorderWidth
    {
      get => this.Appearance.BorderWidth;
      set => this.Appearance.BorderWidth = value;
    }

    [Category("QAppearance")]
    [Description("Gets or sets the QPanelConfiguration for this panel.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual QPanelConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set
      {
        if (this.m_oConfiguration == value)
          return;
        if (this.m_oConfiguration != null)
          this.m_oConfiguration.ConfigurationChanged -= this.m_oConfigurationChangedEventHandler;
        this.m_oConfiguration = value;
        if (this.m_oConfiguration == null)
          return;
        this.m_oConfiguration.ConfigurationChanged += this.m_oConfigurationChangedEventHandler;
        this.Panel_ConfigurationChanged((object) this.m_oConfiguration, EventArgs.Empty);
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    protected override int ClientAreaMarginTop => this.m_iClientAreaMarginTop + (!this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth);

    protected override int ClientAreaMarginLeft => this.m_iClientAreaMarginLeft + (!this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth);

    protected override int ClientAreaMarginRight => this.m_iClientAreaMarginRight + (!this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth);

    protected override int ClientAreaMarginBottom => this.m_iClientAreaMarginBottom + (!this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth);

    protected override int ClientAreaPaddingTop => this.m_iClientAreaPaddingTop;

    protected override int ClientAreaPaddingBottom => this.m_iClientAreaPaddingBottom;

    protected override int ClientAreaPaddingLeft => this.m_iClientAreaPaddingLeft;

    protected override int ClientAreaPaddingRight => this.m_iClientAreaPaddingRight;

    protected override string BackColorPropertyName => "PanelBackground1";

    protected override string BackColor2PropertyName => "PanelBackground2";

    protected override string BorderColorPropertyName => "PanelBorder";

    protected virtual QPanelConfiguration CreateConfigurationInstance() => new QPanelConfiguration();

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.PerformLayout();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.Configuration != null && this.Configuration.ShowText)
      {
        Graphics graphics = this.CreateGraphics();
        int angle = 0;
        if (this.Configuration.TextDock == DockStyle.Left || this.Configuration.TextDock == DockStyle.Right)
          angle = this.Configuration.VerticalTextOrientation != QVerticalTextOrientation.VerticalDown ? 90 : -90;
        Size textExtentPoint = NativeHelper.CalculateTextExtentPoint(this.Text, this.Font, angle, graphics);
        Rectangle rectangle1 = new Rectangle(Point.Empty, textExtentPoint);
        rectangle1.Width += this.Configuration.TextPadding.Horizontal;
        rectangle1.Height += Math.Max(0, this.Configuration.TextPadding.Vertical);
        this.ResetNonClientMarginPadding();
        int num1 = rectangle1.Height / 2;
        int num2 = 0;
        switch (this.Configuration.TextDock)
        {
          case DockStyle.Top:
            this.m_iClientAreaMarginTop = num1;
            this.m_iClientAreaPaddingTop = num1;
            num2 = this.CurrentBounds.Width;
            rectangle1.Y = 0;
            break;
          case DockStyle.Bottom:
            this.m_iClientAreaMarginBottom = num1;
            this.m_iClientAreaPaddingBottom = num1;
            num2 = this.CurrentBounds.Width;
            rectangle1.Y = this.CurrentBounds.Height - rectangle1.Height;
            break;
          case DockStyle.Left:
            this.m_iClientAreaMarginLeft = num1;
            this.m_iClientAreaPaddingLeft = num1;
            num2 = this.CurrentBounds.Height;
            rectangle1.Y = 0;
            break;
          case DockStyle.Right:
            this.m_iClientAreaMarginRight = num1;
            this.m_iClientAreaPaddingRight = num1;
            num2 = this.CurrentBounds.Height;
            rectangle1.Y = this.CurrentBounds.Width - rectangle1.Height;
            break;
        }
        Rectangle rectangle2 = rectangle1 with
        {
          Width = num2 - this.Configuration.TextSpacing.All
        };
        rectangle2.X += this.Configuration.TextSpacing.Before;
        switch (this.Configuration.TextAlignment)
        {
          case StringAlignment.Near:
            rectangle1.X = rectangle2.X;
            if (rectangle2.Right - rectangle1.Left < rectangle1.Width)
            {
              rectangle1.Width = rectangle2.Right - rectangle1.Left;
              break;
            }
            break;
          case StringAlignment.Center:
            rectangle1.X = (num2 - rectangle1.Width) / 2;
            if (rectangle1.X < rectangle2.X)
              rectangle1.X = rectangle2.X;
            if (rectangle2.Right - rectangle1.Left < rectangle1.Width)
            {
              rectangle1.Width = rectangle2.Right - rectangle1.Left;
              break;
            }
            break;
          case StringAlignment.Far:
            rectangle1.X = num2 - (rectangle1.Width + this.Configuration.TextSpacing.After);
            if (rectangle1.X < rectangle2.X)
              rectangle1.X = rectangle2.X;
            if (rectangle2.Right - rectangle1.Left < rectangle1.Width)
            {
              rectangle1.Width = rectangle2.Right - rectangle1.Left;
              break;
            }
            break;
        }
        Rectangle rectangle3 = rectangle1;
        rectangle1.Width -= this.Configuration.TextPadding.Horizontal;
        rectangle1.Height -= Math.Max(0, this.Configuration.TextPadding.Vertical);
        rectangle1.X += this.Configuration.TextPadding.Left;
        rectangle1.Y += Math.Max(0, this.Configuration.TextPadding.Top);
        if (this.Configuration.TextDock == DockStyle.Left || this.Configuration.TextDock == DockStyle.Right)
        {
          rectangle1 = this.RotateRectangle(rectangle1);
          rectangle3 = this.RotateRectangle(rectangle3);
        }
        graphics.Dispose();
        if (rectangle1 == this.m_oTextRectangle && rectangle3 == this.m_oTextRegionRectangle)
        {
          this.RefreshNoClientArea(false);
          return;
        }
        this.m_oTextRectangle = rectangle1;
        this.m_oTextRegionRectangle = rectangle3;
      }
      else
      {
        if (this.m_oTextRectangle.IsEmpty)
          return;
        this.ResetNonClientMarginPadding();
        this.m_oTextRectangle = Rectangle.Empty;
      }
      this.PerformNonClientAreaLayout();
      this.Invalidate();
    }

    private Rectangle RotateRectangle(Rectangle rectangle)
    {
      int y = rectangle.Y;
      rectangle.Y = rectangle.X;
      rectangle.X = y;
      int height = rectangle.Height;
      rectangle.Height = rectangle.Width;
      rectangle.Width = height;
      return rectangle;
    }

    private void ResetNonClientMarginPadding()
    {
      this.m_iClientAreaMarginTop = 0;
      this.m_iClientAreaMarginBottom = 0;
      this.m_iClientAreaMarginLeft = 0;
      this.m_iClientAreaMarginRight = 0;
      this.m_iClientAreaPaddingTop = 0;
      this.m_iClientAreaPaddingBottom = 0;
      this.m_iClientAreaPaddingLeft = 0;
      this.m_iClientAreaPaddingRight = 0;
    }

    protected override void OnPaintNonClientArea(PaintEventArgs e)
    {
      base.OnPaintNonClientArea(e);
      if (this.ClientAreaMarginTop > this.Appearance.BorderWidth || this.ClientAreaMarginBottom > this.Appearance.BorderWidth || this.ClientAreaMarginLeft > this.Appearance.BorderWidth || this.ClientAreaMarginRight > this.Appearance.BorderWidth)
        QControlPaint.PaintTransparentBackground((Control) this, e);
      QPaintBackgroundObjects currentObjects = new QPaintBackgroundObjects();
      Rectangle rectangle = new Rectangle(Point.Empty, this.CurrentBounds.Size);
      currentObjects.BackgroundBounds = rectangle;
      Rectangle bounds = rectangle with
      {
        X = this.ClientAreaMarginLeft,
        Y = this.ClientAreaMarginTop
      };
      bounds.Width -= this.ClientAreaMarginLeft + this.ClientAreaMarginRight;
      bounds.Height -= this.ClientAreaMarginTop + this.ClientAreaMarginBottom;
      int num1 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderLeft ? 0 : this.Appearance.BorderWidth;
      int num2 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderRight ? 0 : this.Appearance.BorderWidth;
      int num3 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderTop ? 0 : this.Appearance.BorderWidth;
      int num4 = !this.Appearance.ShowBorders || !this.Appearance.ShowBorderBottom ? 0 : this.Appearance.BorderWidth;
      bounds.X -= num1;
      bounds.Y -= num3;
      bounds.Width += num1 + num2;
      bounds.Height += num3 + num4;
      if (this.PaintBackgroundObjectsProvider != null)
        currentObjects = this.PaintBackgroundObjectsProvider.GetBackgroundObjects(currentObjects, (Control) this);
      QAppearanceFillerProperties fillerProperties = new QAppearanceFillerProperties();
      fillerProperties.AlternativeBoundsForBrushCreation = currentObjects.BackgroundBounds;
      QRectanglePainter.Default.FillBackground(bounds, (IQAppearance) this.Appearance, new QColorSet(this.BackColor, this.BackColor2, this.BorderColor), QRectanglePainterProperties.Default, fillerProperties, QPainterOptions.Default, e.Graphics);
      Region region = (Region) null;
      if (!this.m_oTextRectangle.IsEmpty)
      {
        region = e.Graphics.Clip;
        Region clip = e.Graphics.Clip;
        clip.Exclude(this.m_oTextRegionRectangle);
        e.Graphics.Clip = clip;
      }
      QRectanglePainter.Default.FillForeground(bounds, (IQAppearance) this.Appearance, new QColorSet(this.BackColor, this.BackColor2, this.BorderColor), QRectanglePainterProperties.Default, fillerProperties, QPainterOptions.Default, e.Graphics);
      if (!this.m_oTextRectangle.IsEmpty)
        e.Graphics.Clip = region;
      if (this.m_oTextRectangle.IsEmpty)
        return;
      int angle = 0;
      Rectangle oTextRectangle = this.m_oTextRectangle;
      if (this.Configuration.TextDock == DockStyle.Left || this.Configuration.TextDock == DockStyle.Right)
      {
        if (this.Configuration.VerticalTextOrientation == QVerticalTextOrientation.VerticalDown)
        {
          angle = -90;
          oTextRectangle.X += oTextRectangle.Width;
        }
        else
        {
          angle = 90;
          oTextRectangle.Y += oTextRectangle.Height;
        }
        oTextRectangle.Width = this.m_oTextRectangle.Height;
        oTextRectangle.Height = this.m_oTextRectangle.Width;
      }
      NativeHelper.DrawText(this.Text, this.Font, oTextRectangle, (Color) this.ColorScheme.TextColor, QDrawTextOptions.EndEllipsis, angle, e.Graphics);
    }

    private void Panel_ConfigurationChanged(object sender, EventArgs e) => this.PerformLayout();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.m_oComponents != null)
      {
        this.m_oComponents.Dispose();
        this.m_oComponents = (Container) null;
      }
      base.Dispose(disposing);
    }
  }
}
