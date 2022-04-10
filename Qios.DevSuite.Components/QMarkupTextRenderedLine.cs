// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextRenderedLine
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextRenderedLine : IDisposable
  {
    private RectangleF m_oBounds;
    private float m_fBaseLine;
    private QMarkupTextRenderedPartCollection m_oParts;
    private QMarkupTextRenderedLineCollection m_oParentCollection;

    internal QMarkupTextRenderedLine(QMarkupTextRenderedLineCollection parentCollection)
    {
      this.m_oBounds = RectangleF.Empty;
      this.m_oParentCollection = parentCollection;
      this.m_oParts = new QMarkupTextRenderedPartCollection(this);
    }

    public QMarkupTextRenderedPartCollection Parts => this.m_oParts;

    public QMarkupTextRenderedLineCollection ParentCollection => this.m_oParentCollection;

    public int Index => this.m_oParentCollection == null ? -1 : this.m_oParentCollection.IndexOf(this);

    public RectangleF Bounds => this.m_oBounds;

    public SizeF Size => this.m_oBounds.Size;

    public PointF Location => this.m_oBounds.Location;

    internal void PutLocation(PointF value) => this.m_oBounds.Location = value;

    public float Left => this.m_oBounds.X;

    internal void PutLeft(float value) => this.m_oBounds.X = value;

    public float Top => this.m_oBounds.Y;

    internal void PutTop(float value) => this.m_oBounds.Y = value;

    public PointF AbsoluteLocation => this.m_oParentCollection != null ? this.m_oParentCollection.CalculateAbsoluteLocation(this.m_oBounds.Location) : this.m_oBounds.Location;

    public PointF CalculateAbsoluteLocation(PointF childlocation)
    {
      PointF absoluteLocation = this.AbsoluteLocation;
      return new PointF(absoluteLocation.X + childlocation.X, absoluteLocation.Y + childlocation.Y);
    }

    public float Width => this.m_oBounds.Width;

    public float Height => this.m_oBounds.Height;

    public float BaseLine => this.m_fBaseLine;

    internal void AlignLineHorizontal(ContentAlignment alignment, float availableWidth) => this.PutLeft((float) QMath.AlignElement(new System.Drawing.Size((int) Math.Round((double) this.Width), (int) Math.Round((double) this.Height)), alignment, new Rectangle(0, (int) Math.Round((double) this.Top), (int) Math.Round((double) availableWidth), (int) Math.Round((double) this.Height)), true).Left);

    internal void CalculateWidth()
    {
      this.m_oBounds.Width = 0.0f;
      float num = 0.0f;
      for (int index = 0; index < this.Parts.Count; ++index)
      {
        QMarkupTextRenderedPart part = this.Parts[index];
        part.PutLeft(num);
        this.m_oBounds.Width += part.Width;
        num += part.Width;
      }
    }

    internal void CalculateHeight()
    {
      this.m_oBounds.Height = 0.0f;
      this.m_fBaseLine = 0.0f;
      if (this.Parts.Count > 0)
      {
        for (int index = 0; index < this.Parts.Count; ++index)
        {
          QMarkupTextRenderedPart part = this.Parts[index];
          this.m_oBounds.Height = Math.Max(part.Height, this.m_oBounds.Height);
          this.m_fBaseLine = Math.Max(part.BaseLine, this.m_fBaseLine);
        }
      }
      else
        this.m_oBounds.Height = 0.0f;
    }

    internal void CalculateSize()
    {
      this.CalculateWidth();
      this.CalculateHeight();
    }

    internal void HandlePartsCollectionChanged() => this.CalculateSize();

    internal void FinishLineRendering()
    {
      this.TrimLine(false);
      this.CalculateSize();
    }

    internal void TrimLine(bool calculateWidth)
    {
      this.TrimLeft(false);
      this.TrimRight(false);
      if (!calculateWidth)
        return;
      this.CalculateWidth();
    }

    internal void TrimLeft(bool calculateWidth)
    {
      int index = 0;
      bool flag = false;
      while (!flag && index < this.m_oParts.Count)
      {
        if (this.m_oParts[index].TrimLeft())
          flag = true;
        else
          ++index;
      }
      if (!calculateWidth)
        return;
      this.CalculateWidth();
    }

    internal void TrimRight(bool calculateWidth)
    {
      int index = this.m_oParts.Count - 1;
      bool flag = false;
      while (!flag && index >= 0)
      {
        if (this.m_oParts[index].TrimRight())
          flag = true;
        else
          --index;
      }
      if (!calculateWidth)
        return;
      this.CalculateWidth();
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.m_oParts != null)
        this.Parts.Dispose();
      this.m_oParts = (QMarkupTextRenderedPartCollection) null;
    }

    ~QMarkupTextRenderedLine() => this.Dispose(false);
  }
}
