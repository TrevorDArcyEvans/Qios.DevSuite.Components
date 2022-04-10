// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextRenderedPartCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextRenderedPartCollection : CollectionBase, IDisposable
  {
    private QMarkupTextRenderedLine m_oLine;
    private QMarkupTextElement m_oElement;

    internal QMarkupTextRenderedPartCollection(QMarkupTextRenderedLine line) => this.m_oLine = line;

    internal QMarkupTextRenderedPartCollection(QMarkupTextElement element) => this.m_oElement = element;

    public void Add(QMarkupTextRenderedPart part)
    {
      if (this.m_oLine != null)
        part.Line = this.m_oLine;
      if (this.m_oElement != null)
        part.Element = this.m_oElement;
      this.InnerList.Add((object) part);
      this.NotifyCollectionChanged();
    }

    public void Remove(QMarkupTextRenderedPart part)
    {
      if (this.m_oLine != null && part.Line == this.m_oLine)
        part.Line = (QMarkupTextRenderedLine) null;
      if (this.m_oElement != null && part.Element == this.m_oElement)
        part.Element = (QMarkupTextElement) null;
      this.InnerList.Remove((object) part);
      this.NotifyCollectionChanged();
    }

    public QMarkupTextRenderedPart this[int index] => (QMarkupTextRenderedPart) this.InnerList[index];

    public bool Contains(QMarkupTextRenderedPart part) => this.InnerList.Contains((object) part);

    public int IndexOf(QMarkupTextRenderedPart part) => this.InnerList.IndexOf((object) part);

    public QMarkupTextRenderedPart GetPartThatContainsAbsolutePoint(
      PointF point)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ContainsAbsolutePoint(point))
          return this[index];
      }
      return (QMarkupTextRenderedPart) null;
    }

    private void NotifyCollectionChanged()
    {
      if (this.m_oLine == null)
        return;
      this.m_oLine.HandlePartsCollectionChanged();
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
      for (int index = 0; index < this.Count; ++index)
        this[index].Dispose();
      this.Clear();
    }

    ~QMarkupTextRenderedPartCollection() => this.Dispose(false);
  }
}
