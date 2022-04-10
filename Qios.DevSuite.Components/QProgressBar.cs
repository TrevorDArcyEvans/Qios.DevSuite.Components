// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QProgressBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QProgressBar), "Resources.ControlImages.QProgressBar.bmp")]
  [ToolboxItem(true)]
  public class QProgressBar : QControl
  {
    private string m_sDisplayValueFormat;
    private bool m_bDisplayValueAsString;
    private float m_fDisplayValueMin;
    private float m_fDisplayValueMax = 100f;
    private QAppearanceBase m_oBlockAppearance;
    private int m_iBottomValue;
    private int m_iValue;
    private int m_iMinValue;
    private int m_iMaxValue = 100;
    private int m_iStepSize = 1;
    private int m_iBlockSize = 10;
    private int m_iBlockMargin = 1;
    private int m_iSpacing = 1;
    private bool m_bFullBlocks;
    private QProgressBarDirection m_eDirection;
    private EventHandler m_oBlockAppearanceChangedEventHandler;

    public QProgressBar()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.m_oBlockAppearanceChangedEventHandler = new EventHandler(this.BlockAppearance_AppearanceChanged);
      this.m_oBlockAppearance = this.CreateBlockAppearanceInstance();
      if (this.m_oBlockAppearance == null)
        return;
      this.m_oBlockAppearance.AppearanceChanged += this.m_oBlockAppearanceChangedEventHandler;
    }

    [Browsable(false)]
    public override string Text
    {
      get => base.Text;
      set => base.Text = value;
    }

    protected override QAppearanceBase CreateAppearanceInstance() => (QAppearanceBase) new QAppearance();

    protected virtual QAppearanceBase CreateBlockAppearanceInstance() => (QAppearanceBase) new QProgressBarBlockAppearance();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the QAppearance.")]
    [Category("QAppearance")]
    public virtual QAppearance Appearance => (QAppearance) base.Appearance;

    [DefaultValue(false)]
    [Category("QAppearance")]
    [Description("Gets or sets whether only full blocks are drawn.")]
    public bool FullBlocks
    {
      get => this.m_bFullBlocks;
      set
      {
        this.m_bFullBlocks = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets the ColorStyle.")]
    [DefaultValue(QColorStyle.Gradient)]
    public QColorStyle ColorStyle
    {
      get => this.m_oBlockAppearance.BackgroundStyle;
      set => this.m_oBlockAppearance.BackgroundStyle = value;
    }

    [Category("QBehavior")]
    [Description("Gets or sets the BottomValue. The bottom value is used to indicate the start of the progress.")]
    [DefaultValue(0)]
    public int BottomValue
    {
      get => this.m_iBottomValue;
      set
      {
        this.m_iBottomValue = value;
        this.Invalidate();
      }
    }

    [DefaultValue(0)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the Value. The range from BottomValue till Value will be drawn")]
    [Category("QBehavior")]
    public int Value
    {
      get => this.m_iValue;
      set
      {
        this.m_iValue = value;
        this.Refresh();
      }
    }

    [Description("Gets or sets the minimum value")]
    [Category("QBehavior")]
    [DefaultValue(0)]
    public int MinValue
    {
      get => this.m_iMinValue;
      set
      {
        this.m_iMinValue = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets the maximum value")]
    [DefaultValue(100)]
    [Category("QBehavior")]
    public int MaxValue
    {
      get => this.m_iMaxValue;
      set
      {
        this.m_iMaxValue = value;
        this.Invalidate();
      }
    }

    [DefaultValue(1)]
    [Category("QBehavior")]
    [Description("Gets or sets the size of one step.")]
    public int StepSize
    {
      get => this.m_iStepSize;
      set => this.m_iStepSize = value;
    }

    [Description("Gets or sets the direction of the bar")]
    [Category("QAppearance")]
    [DefaultValue(QProgressBarDirection.Horizontal)]
    public QProgressBarDirection Direction
    {
      get => this.m_eDirection;
      set
      {
        this.m_eDirection = value;
        this.Invalidate();
      }
    }

    [DefaultValue(10)]
    [Category("QAppearance")]
    [Description("Gets or sets the size of the blocks that are painted")]
    public int BlockSize
    {
      get => this.m_iBlockSize;
      set
      {
        this.m_iBlockSize = value;
        this.Invalidate();
      }
    }

    [DefaultValue(1)]
    [Description("Gets or sets the margin between the blocks. When this is set to 0 just one block is drawn")]
    [Category("QAppearance")]
    public int BlockMargin
    {
      get => this.m_iBlockMargin;
      set
      {
        this.m_iBlockMargin = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets the spacing between the blocks and the border of the QProgressBar")]
    [Category("QAppearance")]
    [DefaultValue(1)]
    public int Spacing
    {
      get => this.m_iSpacing;
      set
      {
        this.m_iSpacing = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets the displayed value as a float. The value is automatically calculated based on the DisplayValueMin and DisplayValueMax")]
    [Browsable(false)]
    [Category("QAppearance")]
    [DefaultValue(0.0f)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float DisplayValue
    {
      get => (double) this.m_fDisplayValueMin == (double) this.m_fDisplayValueMax || this.m_iMinValue == this.m_iMaxValue ? 0.0f : this.m_fDisplayValueMin + (float) ((double) (this.m_iValue - this.m_iMinValue) / (double) (this.m_iMaxValue - this.m_iMinValue) * ((double) this.m_fDisplayValueMax - (double) this.m_fDisplayValueMin));
      set
      {
        if ((double) this.m_fDisplayValueMin == (double) this.m_fDisplayValueMax || this.m_iMinValue == this.m_iMaxValue)
          this.Value = 0;
        else
          this.Value = this.m_iMinValue + (int) Math.Round(((double) value - (double) this.m_fDisplayValueMin) / ((double) this.m_fDisplayValueMax - (double) this.m_fDisplayValueMin) * ((double) this.m_iMaxValue - (double) this.m_iMinValue));
      }
    }

    [Description("Gets or sets the minimum displayed value. This value indicates what the minimum value is presented in the string.")]
    [Category("QBehavior")]
    [DefaultValue(0.0f)]
    public float DisplayValueMin
    {
      get => this.m_fDisplayValueMin;
      set
      {
        this.m_fDisplayValueMin = value;
        this.Invalidate();
      }
    }

    [Category("QBehavior")]
    [DefaultValue(100f)]
    [Description("Gets or sets the maximum displayed value. This value indicates what the maximum value is presented in the string.")]
    public float DisplayValueMax
    {
      get => this.m_fDisplayValueMax;
      set
      {
        this.m_fDisplayValueMax = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets whether the value must be displayed as a string inside the progress bar.")]
    [Category("QAppearance")]
    [DefaultValue(false)]
    public bool DisplayValueAsString
    {
      get => this.m_bDisplayValueAsString;
      set
      {
        this.m_bDisplayValueAsString = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets the format to display the value as when it is displayed as a string. The format must be structured so it can be used in String.Format. (so like '{0} Volt')")]
    [Category("QAppearance")]
    [DefaultValue(null)]
    public string DisplayValueFormat
    {
      get => this.m_sDisplayValueFormat;
      set
      {
        if (!QMisc.IsEmpty((object) value))
          string.Format(value, (object) this.DisplayValue);
        this.m_sDisplayValueFormat = value;
        this.Invalidate();
      }
    }

    public void Step()
    {
      if (this.StepSize > 0)
      {
        this.Value = Math.Min(this.MaxValue, this.Value + this.StepSize);
      }
      else
      {
        if (this.StepSize >= 0)
          return;
        this.Value = Math.Max(this.MinValue, this.Value + this.StepSize);
      }
    }

    protected override string BackColorPropertyName => "ProgressBarBackground1";

    protected override string BackColor2PropertyName => "ProgressBarBackground2";

    protected override string BorderColorPropertyName => "ProgressBarBorder";

    public bool ShouldSerializeBlockAppearance() => this.m_oBlockAppearance != null && !this.BlockAppearance.IsSetToDefaultValues();

    public void ResetBlockAppearance()
    {
      if (this.m_oBlockAppearance == null)
        return;
      this.m_oBlockAppearance.SetToDefaultValues();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QAppearance for the blocks of the QProgressBar")]
    public virtual QAppearanceBase BlockAppearance => this.m_oBlockAppearance;

    private void BlockAppearance_AppearanceChanged(object sender, EventArgs e)
    {
      this.PerformLayout();
      this.Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.IsDisposed)
        return;
      Rectangle rectangle = new Rectangle(this.ClientRectangle.Left + this.m_iSpacing, this.ClientRectangle.Top + this.m_iSpacing, this.ClientRectangle.Width - this.m_iSpacing * 2, this.ClientRectangle.Height - this.m_iSpacing * 2);
      if (rectangle.Height <= 0 || rectangle.Width <= 0 || this.m_iMaxValue <= this.m_iMinValue)
        return;
      if (this.m_iValue > this.m_iMinValue && this.m_iValue > this.m_iBottomValue)
      {
        QColorSet colors = new QColorSet((Color) this.ColorScheme.ProgressBarColor1, (Color) this.ColorScheme.ProgressBarColor2);
        if (this.m_eDirection == QProgressBarDirection.Horizontal)
        {
          int val1 = rectangle.Left + (int) Math.Ceiling((double) (this.m_iValue - this.m_iMinValue) / (double) (this.m_iMaxValue - this.m_iMinValue) * (double) rectangle.Width);
          int num1 = rectangle.Left + (int) Math.Ceiling((double) (this.m_iBottomValue - this.m_iMinValue) / (double) (this.m_iMaxValue - this.m_iMinValue) * (double) rectangle.Width);
          Region region1 = (Region) null;
          Region region2 = (Region) null;
          if (this.m_iBlockMargin > 0)
          {
            int left = rectangle.Left;
            int num2 = Math.Min(val1, rectangle.Right);
            int num3 = Math.Max(num1, rectangle.Left);
            for (; this.m_bFullBlocks && left + this.m_iBlockSize < num2 || !this.m_bFullBlocks && left < num2; left += this.m_iBlockSize + this.m_iBlockMargin)
            {
              if (left >= num3)
              {
                if (region1 == null)
                  region1 = new Region(new Rectangle(left, rectangle.Top, this.m_iBlockSize, rectangle.Height));
                else
                  region1.Union(new Rectangle(left, rectangle.Top, this.m_iBlockSize, rectangle.Height));
              }
            }
            if (region1 != null)
            {
              if (region2 == null)
                region2 = e.Graphics.Clip;
              e.Graphics.Clip = region1;
            }
          }
          QRectanglePainter.Default.Paint(new Rectangle(num1, rectangle.Top, val1 - num1, rectangle.Height), (IQAppearance) this.m_oBlockAppearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
          if (region2 != null)
            e.Graphics.Clip = region2;
          region1?.Dispose();
        }
        else if (this.m_eDirection == QProgressBarDirection.Vertical)
        {
          int num4 = rectangle.Bottom - (int) Math.Round((double) (this.m_iValue - this.m_iMinValue) / (double) (this.m_iMaxValue - this.m_iMinValue) * (double) rectangle.Height);
          int val1 = rectangle.Bottom - (int) Math.Ceiling((double) (this.m_iBottomValue - this.m_iMinValue) / (double) (this.m_iMaxValue - this.m_iMinValue) * (double) rectangle.Height);
          Region region3 = (Region) null;
          Region region4 = (Region) null;
          if (this.m_iBlockMargin > 0)
          {
            int bottom = rectangle.Bottom;
            int num5 = Math.Max(num4, rectangle.Top);
            int num6 = Math.Min(val1, rectangle.Bottom);
            for (; this.m_bFullBlocks && bottom - this.m_iBlockSize > num5 || !this.m_bFullBlocks && bottom > num5; bottom -= this.m_iBlockSize + this.m_iBlockMargin)
            {
              if (bottom <= num6)
              {
                if (region3 == null)
                  region3 = new Region(new Rectangle(rectangle.Left, bottom - this.m_iBlockSize, rectangle.Width, this.m_iBlockSize));
                else
                  region3.Union(new Rectangle(rectangle.Left, bottom - this.m_iBlockSize, rectangle.Width, this.m_iBlockSize));
              }
            }
            if (region3 != null)
            {
              if (region4 == null)
                region4 = e.Graphics.Clip;
              e.Graphics.Clip = region3;
            }
          }
          QRectanglePainter.Default.Paint(new Rectangle(rectangle.Left, num4, rectangle.Width, val1 - num4), (IQAppearance) this.m_oBlockAppearance, colors, QRectanglePainterProperties.Default, QAppearanceFillerProperties.Default, QPainterOptions.Default, e.Graphics);
          if (region4 != null)
            e.Graphics.Clip = region4;
          region3?.Dispose();
        }
      }
      if (!this.m_bDisplayValueAsString)
        return;
      string s = QMisc.IsEmpty((object) this.m_sDisplayValueFormat) ? this.DisplayValue.ToString() : string.Format(this.m_sDisplayValueFormat, (object) this.DisplayValue);
      Brush brush = (Brush) new SolidBrush((Color) this.ColorScheme.ProgressBarText);
      StringFormat format = new StringFormat(StringFormat.GenericDefault);
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      e.Graphics.DrawString(s, this.Font, brush, (RectangleF) this.ClientRectangle, format);
      brush.Dispose();
      format.Dispose();
    }
  }
}
