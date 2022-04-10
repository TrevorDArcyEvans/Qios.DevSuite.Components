// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartImagePainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartImagePainter : QPartObjectPainter
  {
    private QPartBoundsType m_eDrawOnBounds;
    private bool m_bDrawDisabled;
    private Color m_oColorToReplace;
    private Color m_oColorToReplaceWith;
    private bool m_bKeepImageSize;
    private Image m_oImage;

    public QPartBoundsType DrawOnBounds
    {
      get => this.m_eDrawOnBounds;
      set => this.m_eDrawOnBounds = value;
    }

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

    public Image Image
    {
      get => this.m_oImage;
      set => this.m_oImage = value;
    }

    public bool KeepImageSize
    {
      get => this.m_bKeepImageSize;
      set => this.m_bKeepImageSize = value;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled || part == null)
        return;
      Image image = this.m_oImage ?? part.ContentObject as Image;
      if (image == null)
        return;
      Rectangle rectangle = part.CalculatedProperties.GetBounds(this.m_eDrawOnBounds);
      Size size = image.Size;
      if (rectangle.Width > image.Width)
        rectangle = QPartHelper.AlignPartSize(size, part.Properties.GetContentAlignmentHorizontal(part), rectangle, QPartDirection.Horizontal, false, true);
      if (rectangle.Height > image.Height)
        rectangle = QPartHelper.AlignPartSize(size, part.Properties.GetContentAlignmentVertical(part), rectangle, QPartDirection.Vertical, false, true);
      if (this.m_bKeepImageSize)
        rectangle.Size = image.Size;
      if (this.m_bDrawDisabled)
        QControlPaint.DrawImageDisabled(paintContext.Graphics, image, rectangle);
      else
        QControlPaint.DrawImage(paintContext.Graphics, image, this.m_oColorToReplace, this.m_oColorToReplaceWith, rectangle);
    }
  }
}
