// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartSeparatorPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Qios.DevSuite.Components
{
  public class QPartSeparatorPainter : QPartObjectPainter
  {
    private QPartBoundsType m_eDrawOnBounds = QPartBoundsType.Bounds;
    private Image m_oMask;
    private QColorSet m_oColorSet;
    private bool m_bAutoSize = true;

    public bool AutoSize
    {
      get => this.m_bAutoSize;
      set => this.m_bAutoSize = value;
    }

    public QPartBoundsType DrawOnBounds
    {
      get => this.m_eDrawOnBounds;
      set => this.m_eDrawOnBounds = value;
    }

    public QColorSet ColorSet
    {
      get => this.m_oColorSet;
      set => this.m_oColorSet = value;
    }

    public Image Mask
    {
      get => this.m_oMask;
      set => this.m_oMask = value;
    }

    protected ColorMap[] CreateReplaceColors(QColorSet colorSet)
    {
      if (colorSet.Border == Color.Empty && colorSet.Background1 == Color.Empty && colorSet.Background2 != Color.Empty)
        return (ColorMap[]) null;
      int index1 = 0;
      ColorMap[] replaceColors = new ColorMap[3];
      replaceColors[index1] = new ColorMap();
      replaceColors[index1].OldColor = Color.FromArgb((int) byte.MaxValue, 0, 0);
      replaceColors[index1].NewColor = colorSet.Border != Color.Empty ? colorSet.Border : Color.Transparent;
      int index2 = index1 + 1;
      replaceColors[index2] = new ColorMap();
      replaceColors[index2].OldColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
      replaceColors[index2].NewColor = colorSet.Background1 != Color.Empty ? colorSet.Background1 : Color.Transparent;
      int index3 = index2 + 1;
      replaceColors[index3] = new ColorMap();
      replaceColors[index3].OldColor = Color.FromArgb(0, 0, (int) byte.MaxValue);
      replaceColors[index3].NewColor = colorSet.Background2 != Color.Empty ? colorSet.Background2 : Color.Transparent;
      int num = index3 + 1;
      return replaceColors;
    }

    public override void PaintObject(IQPart part, QPartPaintContext paintContext)
    {
      if (!this.Enabled || part == null || part.ParentPart == null || this.m_oMask == null || this.m_oColorSet == null)
        return;
      Rectangle bounds1 = part.CalculatedProperties.GetBounds(this.m_eDrawOnBounds);
      Rectangle bounds2 = part.ParentPart.CalculatedProperties.GetBounds(QPartBoundsType.Bounds);
      bool flag = part.ParentPart.Properties.GetDirection(part.ParentPart) == QPartDirection.Vertical;
      ColorMap[] replaceColors = this.CreateReplaceColors(this.ColorSet);
      if (replaceColors == null)
        return;
      Image oMask = this.m_oMask;
      ImageAttributes imageAttr = new ImageAttributes();
      imageAttr.SetRemapTable(replaceColors);
      if (flag)
      {
        Rectangle rect = this.m_bAutoSize ? Rectangle.FromLTRB(bounds2.Left, bounds1.Top, bounds2.Right, bounds1.Bottom) : bounds1;
        int left = rect.Left;
        Region savedRegion = QControlPaint.AdjustClip(paintContext.Graphics, new Region(rect), CombineMode.Intersect);
        for (; left < rect.Right; left += oMask.Width)
          paintContext.Graphics.DrawImage(oMask, new Rectangle(left, rect.Top, oMask.Width, oMask.Height), 0, 0, oMask.Width, oMask.Height, GraphicsUnit.Pixel, imageAttr);
        QControlPaint.RestoreClip(paintContext.Graphics, savedRegion);
      }
      else
      {
        Image image = (Image) QControlPaint.RotateFlipImage(oMask, RotateFlipType.Rotate270FlipNone);
        Rectangle rect = this.m_bAutoSize ? Rectangle.FromLTRB(bounds1.Left, bounds2.Top, bounds1.Right, bounds2.Bottom) : bounds1;
        int top = rect.Top;
        Region savedRegion = QControlPaint.AdjustClip(paintContext.Graphics, new Region(rect), CombineMode.Intersect);
        for (; top < rect.Bottom; top += image.Height)
          paintContext.Graphics.DrawImage(image, new Rectangle(rect.Left, top, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
        QControlPaint.RestoreClip(paintContext.Graphics, savedRegion);
      }
    }
  }
}
