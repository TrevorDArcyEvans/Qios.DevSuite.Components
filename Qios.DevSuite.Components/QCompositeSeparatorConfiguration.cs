// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeSeparatorConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  public class QCompositeSeparatorConfiguration : QCompositeMaskConfiguration
  {
    private static Image m_oDefaultStaticMask;

    public QCompositeSeparatorConfiguration()
      : this((Image) null)
    {
    }

    public QCompositeSeparatorConfiguration(Image defaultMask)
      : base(defaultMask)
    {
      if (QCompositeSeparatorConfiguration.m_oDefaultStaticMask == null)
        QCompositeSeparatorConfiguration.m_oDefaultStaticMask = (Image) new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Images.CompositeSeperatorMask.png"));
      if (defaultMask == null)
        this.DefaultMask = QCompositeSeparatorConfiguration.m_oDefaultStaticMask;
      this.Properties.DefineProperty(0, (object) new QMargin(1, 1, 1, 1));
      this.Properties.DefineProperty(6, (object) QPartAlignment.Near);
      this.Properties.DefineProperty(7, (object) QPartAlignment.Near);
    }

    [QPropertyIndex(4)]
    [Browsable(false)]
    [Description("Gets or sets whether the item is horizontal stretched.")]
    [Category("QAppearance")]
    public override bool StretchHorizontal
    {
      get => base.StretchHorizontal;
      set => base.StretchHorizontal = value;
    }

    [Description("Gets or sets whether the item is vertical stretched.")]
    [QPropertyIndex(5)]
    [Browsable(false)]
    [Category("QAppearance")]
    public override bool StretchVertical
    {
      get => base.StretchVertical;
      set => base.StretchVertical = value;
    }

    [Category("QAppearance")]
    [Browsable(false)]
    [QPropertyIndex(2)]
    [Description("Gets or sets whether the item is horizontal shrinked.")]
    public override bool ShrinkHorizontal
    {
      get => base.ShrinkHorizontal;
      set => base.ShrinkHorizontal = value;
    }

    [Description("Gets or sets whether the item is vertical shrinked.")]
    [Browsable(false)]
    [Category("QAppearance")]
    [QPropertyIndex(3)]
    public override bool ShrinkVertical
    {
      get => base.ShrinkVertical;
      set => base.ShrinkVertical = value;
    }

    public override QPartOptions GetOptions(IQPart part)
    {
      QPartOptions options = base.GetOptions(part);
      if (part is QCompositeSeparator && part.ParentPart != null)
      {
        switch (part.ParentPart.Properties.GetDirection(part.ParentPart))
        {
          case QPartDirection.Horizontal:
            options = options | QPartOptions.StretchVertical | QPartOptions.ShrinkVertical;
            break;
          case QPartDirection.Vertical:
            options = options | QPartOptions.StretchHorizontal | QPartOptions.ShrinkHorizontal;
            break;
        }
      }
      return options;
    }
  }
}
