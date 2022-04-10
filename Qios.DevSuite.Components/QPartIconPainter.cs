// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartIconPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartIconPainter : QPartObjectPainter
  {
    private bool m_bDrawDisabled;
    private Color m_oColorToReplace;
    private Color m_oColorToReplaceWith;

    public Color ColorToReplace
    {
      get => this.m_oColorToReplace;
      set => this.m_oColorToReplace = value;
    }

    public Color ColorToReplaceWith
    {
      get => this.m_oColorToReplaceWith;
      set => this.m_oColorToReplaceWith = value;
    }

    public bool DrawDisabled
    {
      get => this.m_bDrawDisabled;
      set => this.m_bDrawDisabled = value;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled || part == null || part.ContentObject == null)
        return;
      Size size;
      if (!(part.ContentObject is Icon contentObject1))
      {
        IQPartSizedContent contentObject = part.ContentObject as IQPartSizedContent;
        contentObject1 = contentObject.ContentObject as Icon;
        size = contentObject.Size;
      }
      else
        size = part.CalculatedProperties.InnerSize;
      if (contentObject1 == null)
        return;
      Rectangle rectangle = part.CalculatedProperties.InnerBounds;
      if (rectangle.Width > size.Width)
        rectangle = QPartHelper.AlignPartSize(size, part.Properties.GetContentAlignmentHorizontal(part), rectangle, QPartDirection.Horizontal, false, true);
      if (rectangle.Height > size.Height)
        rectangle = QPartHelper.AlignPartSize(size, part.Properties.GetContentAlignmentVertical(part), rectangle, QPartDirection.Vertical, false, true);
      if (this.m_bDrawDisabled)
        QControlPaint.DrawIconDisabled(paintContext.Graphics, contentObject1, rectangle);
      else
        QControlPaint.DrawIcon(paintContext.Graphics, contentObject1, this.m_oColorToReplace, this.m_oColorToReplaceWith, rectangle);
    }
  }
}
