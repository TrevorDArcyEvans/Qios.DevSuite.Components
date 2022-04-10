// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartSharedProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QPartSharedProperties : IQPartSharedProperties
  {
    private QPadding m_oPadding = QPadding.Empty;
    private QMargin m_oMargin = QMargin.Empty;
    private QPartAlignment m_eAlignmentHorizontal = QPartAlignment.Centered;
    private QPartAlignment m_eAlignmentVertical = QPartAlignment.Centered;
    private QPartAlignment m_eContentAlignmentHorizontal = QPartAlignment.Centered;
    private QPartAlignment m_eContentAlignmentVertical = QPartAlignment.Centered;
    private QPartDirection m_eDirection;
    private QTristateBool m_eVisible;
    private Size m_oMinimumSize = new Size(0, 0);
    private Size m_oMaximumSize = new Size(0, 0);
    private Size m_oPreferredSize = new Size(0, 0);
    private QPartOptions m_eOptions;

    [DefaultValue(typeof (QPadding), "0,0,0,0")]
    public QPadding Padding
    {
      get => this.m_oPadding;
      set => this.m_oPadding = value;
    }

    [DefaultValue(typeof (QMargin), "0,0,0,0")]
    public QMargin Margin
    {
      get => this.m_oMargin;
      set => this.m_oMargin = value;
    }

    [DefaultValue(QPartAlignment.Centered)]
    public QPartAlignment AlignmentHorizontal
    {
      get => this.m_eAlignmentHorizontal;
      set => this.m_eAlignmentHorizontal = value;
    }

    [DefaultValue(QPartAlignment.Centered)]
    public QPartAlignment AlignmentVertical
    {
      get => this.m_eAlignmentVertical;
      set => this.m_eAlignmentVertical = value;
    }

    [DefaultValue(QPartAlignment.Centered)]
    public QPartAlignment ContentAlignmentHorizontal
    {
      get => this.m_eContentAlignmentHorizontal;
      set => this.m_eContentAlignmentHorizontal = value;
    }

    [DefaultValue(QPartAlignment.Centered)]
    public QPartAlignment ContentAlignmentVertical
    {
      get => this.m_eContentAlignmentVertical;
      set => this.m_eContentAlignmentVertical = value;
    }

    [DefaultValue(QPartDirection.Horizontal)]
    public QPartDirection Direction
    {
      get => this.m_eDirection;
      set => this.m_eDirection = value;
    }

    [DefaultValue(true)]
    public QTristateBool Visible
    {
      get => this.m_eVisible;
      set => this.m_eVisible = value;
    }

    [DefaultValue(typeof (Size), "0,0")]
    public Size MinimumSize
    {
      get => this.m_oMinimumSize;
      set => this.m_oMinimumSize = value;
    }

    [DefaultValue(typeof (Size), "0,0")]
    public Size MaximumSize
    {
      get => this.m_oMaximumSize;
      set => this.m_oMaximumSize = value;
    }

    [DefaultValue(typeof (Size), "0,0")]
    public Size PreferredSize
    {
      get => this.m_oPreferredSize;
      set => this.m_oPreferredSize = value;
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool StretchHorizontal
    {
      get => (this.m_eOptions & QPartOptions.StretchHorizontal) == QPartOptions.StretchHorizontal;
      set
      {
        if (value)
          this.m_eOptions |= QPartOptions.StretchHorizontal;
        else
          this.m_eOptions &= ~QPartOptions.StretchHorizontal;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool StretchVertical
    {
      get => (this.m_eOptions & QPartOptions.StretchVertical) == QPartOptions.StretchVertical;
      set
      {
        if (value)
          this.m_eOptions |= QPartOptions.StretchVertical;
        else
          this.m_eOptions &= ~QPartOptions.StretchVertical;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShrinkHorizontal
    {
      get => (this.m_eOptions & QPartOptions.StretchHorizontal) == QPartOptions.StretchHorizontal;
      set
      {
        if (value)
          this.m_eOptions |= QPartOptions.ShrinkHorizontal;
        else
          this.m_eOptions &= ~QPartOptions.ShrinkHorizontal;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SrhinkVertical
    {
      get => (this.m_eOptions & QPartOptions.ShrinkVertical) == QPartOptions.ShrinkVertical;
      set
      {
        if (value)
          this.m_eOptions |= QPartOptions.ShrinkVertical;
        else
          this.m_eOptions &= ~QPartOptions.ShrinkVertical;
      }
    }

    [Editor(typeof (QFlagsEditor), typeof (UITypeEditor))]
    [DefaultValue(QPartOptions.None)]
    public QPartOptions Options
    {
      get => this.m_eOptions;
      set => this.m_eOptions = value;
    }

    public virtual QTristateBool GetVisible(IQPart part) => this.m_eVisible;

    public virtual IQPadding[] GetPaddings(IQPart part) => new IQPadding[1]
    {
      (IQPadding) this.m_oPadding
    };

    public virtual IQMargin[] GetMargins(IQPart part) => new IQMargin[1]
    {
      (IQMargin) this.m_oMargin
    };

    public virtual Size GetMinimumSize(IQPart part) => this.m_oMinimumSize;

    public virtual Size GetMaximumSize(IQPart part) => this.m_oMaximumSize;

    public virtual Size GetPreferredSize(IQPart part) => this.m_oPreferredSize;

    public virtual QPartDirection GetDirection(IQPart part) => this.m_eDirection;

    public virtual QPartAlignment GetAlignmentHorizontal(IQPart part) => this.m_eAlignmentHorizontal;

    public virtual QPartAlignment GetAlignmentVertical(IQPart part) => this.m_eAlignmentVertical;

    public virtual QPartAlignment GetContentAlignmentHorizontal(IQPart part) => this.m_eContentAlignmentHorizontal;

    public virtual QPartAlignment GetContentAlignmentVertical(IQPart part) => this.m_eContentAlignmentVertical;

    public virtual QPartOptions GetOptions(IQPart part) => this.m_eOptions;

    public virtual string GetContentLayoutOrder(IQPart part) => (string) null;
  }
}
