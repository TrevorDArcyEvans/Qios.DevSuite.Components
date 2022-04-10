// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QButton
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [DefaultEvent("Click")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QButton), "Resources.ControlImages.QButton.bmp")]
  public class QButton : QControl, IButtonControl
  {
    private DialogResult m_oDialogResult;
    private QButtonConfiguration m_oConfiguration;
    private QItemStates m_eStates;
    private bool m_bIsDefault;
    private QPart m_oButtonPart;
    private QPart m_oImagePart;
    private QPart m_oTextPart;
    private bool m_bPaintTransparentBackground;

    public QButton()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.m_oConfiguration = new QButtonConfiguration();
      this.m_oConfiguration.ConfigurationChanged += new EventHandler(this.Configuration_ConfigurationChanged);
      this.m_oImagePart = new QPart(nameof (Image));
      this.m_oImagePart.Properties = (IQPartSharedProperties) this.m_oConfiguration.ImageConfiguration;
      this.m_oImagePart.PutContentObject((object) this.Image, true);
      this.m_oImagePart.SetObjectPainter(QPartPaintLayer.Content, (IQPartObjectPainter) new QPartImagePainter());
      this.m_oTextPart = new QPart(nameof (Text));
      this.m_oTextPart.Properties = (IQPartSharedProperties) this.m_oConfiguration.TextConfiguration;
      this.m_oTextPart.PutContentObject((object) new QPartNativeSizedString(this.Text), true);
      this.m_oTextPart.SetObjectPainter(QPartPaintLayer.Content, (IQPartObjectPainter) new QPartTextPainter());
      this.m_oTextPart.LayoutEngine = (IQPartLayoutEngine) QPartTextLayoutEngine.Default;
      QPartShapePainter painter = new QPartShapePainter();
      painter.Appearance = (QShapeAppearance) this.Appearance;
      painter.DrawOnBounds = QPartBoundsType.Bounds;
      painter.Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds);
      this.m_oButtonPart = new QPart("Button", true, new IQPart[2]
      {
        (IQPart) this.m_oImagePart,
        (IQPart) this.m_oTextPart
      });
      this.m_oButtonPart.Properties = (IQPartSharedProperties) this.Configuration;
      this.m_oButtonPart.SetObjectPainter(QPartPaintLayer.Background, (IQPartObjectPainter) painter);
      this.m_oButtonPart.SetObjectPainter(QPartPaintLayer.Foreground, (IQPartObjectPainter) new QPartFocusRectanglePainter());
    }

    [Category("Appearance")]
    [Description("Gets or sets an image for this button.")]
    public Image Image
    {
      get => this.m_oImagePart.ContentObject as Image;
      set
      {
        this.m_oImagePart.PutContentObject((object) value, true);
        this.PerformLayout();
        this.Invalidate();
      }
    }

    public override string Text
    {
      get => base.Text;
      set
      {
        base.Text = value;
        ((QPartNativeSizedString) this.m_oTextPart.ContentObject).Value = value;
        this.PerformLayout();
        this.Invalidate();
      }
    }

    public bool ShouldSerializeConfiguration() => !this.Configuration.IsSetToDefaultValues();

    public void ResetConfiguration() => this.Configuration.SetToDefaultValues();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    public virtual QButtonConfiguration Configuration => this.m_oConfiguration;

    protected override string BackColorPropertyName => "ButtonBackground1";

    protected override string BackColor2PropertyName => "ButtonBackground2";

    protected override string BorderColorPropertyName => "ButtonBorder";

    public new bool ShouldSerializeAppearance() => !this.Appearance.IsSetToDefaultValues();

    public new void ResetAppearance() => this.Appearance.SetToDefaultValues();

    [Description("This appearance configures the shape, gradient or metallic properties.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QButtonAppearance Appearance => (QButtonAppearance) base.Appearance;

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QButtonAppearance();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    [Description("This appearance configures the shape, gradient or metallic properties.")]
    [DefaultValue(DialogResult.None)]
    public DialogResult DialogResult
    {
      get => this.m_oDialogResult;
      set => this.m_oDialogResult = value;
    }

    [Category("QBehavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether a transparent background must be painted. When this is false the background color of the parent is painted on this Control. If this is true the parent is painted on this control. Keeping this false increases performance. Set this to false when the Control is situated on a Parent with a solid background or when the control has a rectangular filled out shape.")]
    public virtual bool PaintTransparentBackground
    {
      get => this.m_bPaintTransparentBackground;
      set
      {
        this.m_bPaintTransparentBackground = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    public bool IsHot => QItemStatesHelper.IsHot(this.m_eStates);

    [Browsable(false)]
    public bool IsPressed => QItemStatesHelper.IsPressed(this.m_eStates);

    [Browsable(false)]
    public bool IsDefault => this.m_bIsDefault;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPartLayoutEngine PartLayoutEngine
    {
      get => this.m_oButtonPart.LayoutEngine;
      set => this.m_oButtonPart.LayoutEngine = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IQPartPaintEngine PaintEngine
    {
      get => this.m_oButtonPart.PaintEngine;
      set => this.m_oButtonPart.PaintEngine = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QPart ButtonPart => this.m_oButtonPart;

    public void NotifyDefault(bool value)
    {
      this.m_bIsDefault = value;
      this.Invalidate();
    }

    private void AdjustState(QItemStates state, bool setValue)
    {
      QItemStates qitemStates = QItemStatesHelper.AdjustState(this.m_eStates, state, setValue);
      if (qitemStates == this.m_eStates)
        return;
      this.m_eStates = qitemStates;
      this.Invalidate();
    }

    public void PerformClick()
    {
      if (!this.CanSelect)
        return;
      this.OnClick(EventArgs.Empty);
    }

    public QColorSet GetColorSetBasedOnState()
    {
      QColorSet colorSetBasedOnState;
      if (!this.Enabled)
        colorSetBasedOnState = new QColorSet((Color) this.ColorScheme.ButtonDisabledBackground1, (Color) this.ColorScheme.ButtonDisabledBackground2, (Color) this.ColorScheme.ButtonDisabledBorder, (Color) this.ColorScheme.ButtonDisabledText);
      else if (this.IsPressed)
        colorSetBasedOnState = new QColorSet((Color) this.ColorScheme.ButtonPressedBackground1, (Color) this.ColorScheme.ButtonPressedBackground2, (Color) this.ColorScheme.ButtonPressedBorder, (Color) this.ColorScheme.ButtonPressedText);
      else if (this.IsHot)
      {
        colorSetBasedOnState = new QColorSet((Color) this.ColorScheme.ButtonHotBackground1, (Color) this.ColorScheme.ButtonHotBackground2, (Color) this.ColorScheme.ButtonHotBorder, (Color) this.ColorScheme.ButtonHotText);
      }
      else
      {
        colorSetBasedOnState = new QColorSet((Color) this.ColorScheme.ButtonBackground1, (Color) this.ColorScheme.ButtonBackground2, (Color) this.ColorScheme.ButtonBorder, (Color) this.ColorScheme.ButtonText);
        if (this.Focused || this.IsDefault)
          colorSetBasedOnState.InnerGlow = (Color) this.ColorScheme.ButtonFocusedInnerGlow;
      }
      return colorSetBasedOnState;
    }

    public QFontDefinition GetFontDefinitionBasedOnState()
    {
      if (!this.Enabled)
        return this.Configuration.FontDefinition;
      if (this.IsPressed)
        return this.Configuration.FontDefinitionPressed;
      if (this.IsHot)
        return this.Configuration.FontDefinitionHot;
      return this.Focused || this.IsDefault ? this.Configuration.FontDefinitionFocused : this.Configuration.FontDefinition;
    }

    private Font GetMeasurementFont(Font baseFont, Graphics graphics)
    {
      Font fontFromCache1 = this.Configuration.FontDefinitionPressed.GetFontFromCache(baseFont);
      Font fontFromCache2 = this.Configuration.FontDefinitionHot.GetFontFromCache(baseFont);
      Font fontFromCache3 = this.Configuration.FontDefinition.GetFontFromCache(baseFont);
      Font fontFromCache4 = this.Configuration.FontDefinitionFocused.GetFontFromCache(baseFont);
      Font biggestFont1 = QControlPaint.GetBiggestFont(fontFromCache1, fontFromCache3, graphics);
      Font biggestFont2 = QControlPaint.GetBiggestFont(fontFromCache2, biggestFont1, graphics);
      return QControlPaint.GetBiggestFont(fontFromCache4, biggestFont2, graphics);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.AdjustState(QItemStates.Hot, true);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.AdjustState(QItemStates.Hot, false);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.AdjustState(QItemStates.Pressed, true);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.AdjustState(QItemStates.Pressed, false);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.AdjustState(QItemStates.Disabled, !this.Enabled);
    }

    protected override void OnClick(EventArgs e)
    {
      Form form = this.FindForm();
      if (form != null && this.DialogResult != DialogResult.None)
        form.DialogResult = this.DialogResult;
      base.OnClick(e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      using (QPartLayoutContext fromControl = QPartLayoutContext.CreateFromControl((Control) this))
      {
        fromControl.Font = this.GetMeasurementFont(this.Font, fromControl.Graphics);
        fromControl.StringFormat = QPartTextPainter.CreateStringFormat(fromControl.StringFormat, this.Configuration.DrawTextOptions, false, this.Configuration.WrapText);
        this.PartLayoutEngine.PerformLayout(this.ClientRectangle, (IQPart) this.m_oButtonPart, fromControl);
        fromControl.Dispose();
      }
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.Parent != null)
      {
        if (this.m_bPaintTransparentBackground)
          QControlPaint.PaintTransparentBackground((Control) this, pevent);
        else
          pevent.Graphics.Clear(this.Parent.BackColor);
      }
      if (this.BackgroundImage == null)
        return;
      QControlPaint.DrawImage(this.BackgroundImage, this.BackgroundImageAlign, this.BackgroundImageOffset, this.ClientRectangle, this.BackgroundImage.Size, pevent.Graphics, (ImageAttributes) null, false);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      QColorSet colorSetBasedOnState = this.GetColorSetBasedOnState();
      using (QPartPaintContext fromControl = QPartPaintContext.CreateFromControl((Control) this, e.Graphics))
      {
        if (this.m_oButtonPart.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter1)
          objectPainter1.ColorSet = colorSetBasedOnState;
        if (this.m_oTextPart.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartTextPainter)) is QPartTextPainter objectPainter2)
        {
          objectPainter2.Font = this.GetFontDefinitionBasedOnState().GetFontFromCache(this.Font);
          objectPainter2.StringFormat = QPartTextPainter.CreateStringFormat(StringFormat.GenericDefault, this.Configuration.DrawTextOptions, false, this.Configuration.WrapText);
          objectPainter2.TextColor = colorSetBasedOnState.Foreground;
        }
        if (this.m_oImagePart.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartImagePainter)) is QPartImagePainter objectPainter3)
        {
          int num = !this.Configuration.DrawImageGrayWhenDisabled ? 0 : (!this.Enabled ? 1 : 0);
          objectPainter3.DrawDisabled = num != 0;
        }
        if (this.m_oButtonPart.GetObjectPainter(QPartPaintLayer.Foreground, typeof (QPartFocusRectanglePainter)) is QPartFocusRectanglePainter objectPainter4)
        {
          objectPainter4.Enabled = this.Focused && this.ShowFocusCues;
          QMargin focusRectangleMargin = this.Configuration.FocusRectangleMargin;
          QPadding qpadding = QPadding.SumPaddings(this.Configuration.GetPaddings((IQPart) this.ButtonPart));
          objectPainter4.FocusRectanglePadding = qpadding;
          objectPainter4.FocusRectangleMargin = this.Configuration.FocusRectangleMargin;
        }
        this.PaintEngine.PerformPaint((IQPart) this.m_oButtonPart, fromControl);
      }
    }

    private void Configuration_ConfigurationChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Invalidate();
    }
  }
}
