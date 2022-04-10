// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextRenderedPart
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextRenderedPart : IDisposable
  {
    private QMarkupTextRenderedLine m_oLine;
    private QMarkupTextElement m_oElement;
    private float m_fLeft;
    private float m_fBaseLine;
    private SizeF m_oSize;

    public QMarkupTextRenderedPart(float baseLine, SizeF size)
    {
      this.m_fBaseLine = baseLine;
      this.m_oSize = size;
    }

    public QMarkupTextElement Element
    {
      get => this.m_oElement;
      set => this.m_oElement = value;
    }

    public QMarkupTextRenderedLine Line
    {
      get => this.m_oLine;
      set => this.m_oLine = value;
    }

    public QMarkupText MarkupText => this.Line != null && this.Line.ParentCollection != null && this.Line.ParentCollection.MarkupText != null ? this.Line.ParentCollection.MarkupText : (QMarkupText) null;

    public virtual bool TrimLeft() => false;

    public virtual bool TrimRight() => false;

    public float Left => this.m_fLeft;

    internal void PutLeft(float value) => this.m_fLeft = value;

    public SizeF Size => this.m_oSize;

    public float Width => this.m_oSize.Width;

    protected void PutWidth(float value) => this.m_oSize.Width = value;

    public float Height => this.m_oSize.Height;

    protected void PutHeight(float value) => this.m_oSize.Height = value;

    public float BaseLine => this.m_fBaseLine;

    public PointF AbsoluteLocation => this.m_oLine != null ? this.m_oLine.CalculateAbsoluteLocation(new PointF(this.m_fLeft, (float) Math.Floor((double) this.m_oLine.BaseLine - (double) this.BaseLine))) : new PointF(this.m_fLeft, 0.0f);

    public bool ContainsAbsolutePoint(PointF point)
    {
      if (this.Line == null)
        return false;
      PointF absoluteLocation = this.AbsoluteLocation;
      return (double) absoluteLocation.Y <= (double) point.Y && (double) point.Y <= (double) absoluteLocation.Y + (double) this.Line.Height && (double) absoluteLocation.X <= (double) point.X && (double) point.X <= (double) absoluteLocation.X + (double) this.Size.Width;
    }

    public virtual void Draw(Graphics graphics)
    {
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
    }

    ~QMarkupTextRenderedPart() => this.Dispose(false);
  }
}
