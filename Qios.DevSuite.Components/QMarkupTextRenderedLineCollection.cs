// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextRenderedLineCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextRenderedLineCollection : CollectionBase
  {
    private RectangleF m_oBounds;
    private QMarkupText m_oMarkupText;

    internal QMarkupTextRenderedLineCollection()
    {
    }

    internal QMarkupTextRenderedLineCollection(QMarkupText markupText)
    {
      this.m_oMarkupText = markupText;
      this.m_oBounds = RectangleF.Empty;
    }

    public QMarkupText MarkupText => this.m_oMarkupText;

    public RectangleF Bounds => this.m_oBounds;

    public SizeF Size => this.m_oBounds.Size;

    public PointF Location => this.m_oBounds.Location;

    internal void PutLocation(PointF value) => this.m_oBounds.Location = value;

    public float Left => this.m_oBounds.X;

    internal void PutLeft(float value) => this.m_oBounds.X = value;

    public float Top => this.m_oBounds.Y;

    internal void PutTop(float value) => this.m_oBounds.Y = value;

    public float Width => this.m_oBounds.Width;

    public float Height => this.m_oBounds.Height;

    internal void AlignLineCollection(ContentAlignment alignment, Rectangle availableBounds)
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].AlignLineHorizontal(alignment, this.Width);
      this.m_oBounds.Location = (PointF) QMath.AlignElement(new System.Drawing.Size((int) Math.Round((double) this.Width), (int) Math.Round((double) this.Height)), alignment, availableBounds, true).Location;
    }

    public PointF AbsoluteLocation => this.m_oMarkupText != null ? this.m_oMarkupText.CalculateAbsoluteLocation(this.m_oBounds.Location) : this.m_oBounds.Location;

    public PointF CalculateAbsoluteLocation(PointF childlocation)
    {
      PointF absoluteLocation = this.AbsoluteLocation;
      return new PointF(absoluteLocation.X + childlocation.X, absoluteLocation.Y + childlocation.Y);
    }

    internal void Add(QMarkupTextRenderedLine line) => this.InnerList.Add((object) line);

    internal void Remove(QMarkupTextRenderedLine line) => this.InnerList.Remove((object) line);

    public QMarkupTextRenderedLine this[int index] => (QMarkupTextRenderedLine) this.InnerList[index];

    public int IndexOf(QMarkupTextRenderedLine line) => this.InnerList.IndexOf((object) line);

    public QMarkupTextRenderedLine GetLineThatContainsAbsoluteY(
      float yLocation)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        PointF absoluteLocation = this[index].AbsoluteLocation;
        if ((double) absoluteLocation.Y <= (double) yLocation && (double) absoluteLocation.Y + (double) this[index].Height > (double) yLocation)
          return this[index];
      }
      return (QMarkupTextRenderedLine) null;
    }

    public QMarkupTextRenderedPart GetPartThatContainsAbsolutePoint(
      PointF point)
    {
      return this.GetLineThatContainsAbsoluteY(point.Y)?.Parts.GetPartThatContainsAbsolutePoint(point);
    }

    internal void CalculateSize()
    {
      this.m_oBounds.Size = SizeF.Empty;
      for (int index = 0; index < this.Count; ++index)
      {
        QMarkupTextRenderedLine textRenderedLine = this[index];
        this.m_oBounds.Width = Math.Max(textRenderedLine.Width, this.m_oBounds.Width);
        this.m_oBounds.Height += textRenderedLine.Height;
      }
    }
  }
}
