// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextParams
  {
    private Graphics m_oGraphics;
    private Size m_oMaximumSize;
    private QMarkupTextRenderedLineCollection m_oLines;
    private QMarkupTextRenderedLine m_oCurrentLine;

    public QMarkupTextParams()
    {
    }

    public QMarkupTextParams(Graphics graphics) => this.m_oGraphics = graphics;

    public Graphics Graphics
    {
      get => this.m_oGraphics;
      set => this.m_oGraphics = value;
    }

    internal Size MaximumSize
    {
      get => this.m_oMaximumSize;
      set => this.m_oMaximumSize = value;
    }

    internal int MaximumWidth
    {
      get => this.m_oMaximumSize.Width;
      set => this.m_oMaximumSize.Width = value;
    }

    internal int MaximumHeight
    {
      get => this.m_oMaximumSize.Height;
      set => this.m_oMaximumSize.Height = value;
    }

    internal QMarkupTextRenderedLineCollection Lines
    {
      get => this.m_oLines;
      set => this.m_oLines = value;
    }

    public QMarkupTextRenderedLine CurrentLine
    {
      get
      {
        if (this.m_oCurrentLine == null)
          this.AddLine();
        return this.m_oCurrentLine;
      }
    }

    public void AddLine()
    {
      QMarkupTextRenderedLine line = new QMarkupTextRenderedLine(this.m_oLines);
      line.PutTop(this.FinishCurrentLineRendering());
      this.m_oCurrentLine = line;
      this.m_oLines.Add(line);
    }

    internal void SetCurrentLineToLastLine()
    {
      if (this.m_oLines == null || this.m_oLines.Count <= 0)
        return;
      this.m_oCurrentLine = this.m_oLines[this.m_oLines.Count - 1];
    }

    internal float FinishCurrentLineRendering()
    {
      if (this.m_oCurrentLine == null)
        return 0.0f;
      this.m_oCurrentLine.FinishLineRendering();
      return (float) Math.Ceiling((double) this.m_oCurrentLine.Top + (double) this.m_oCurrentLine.Height);
    }
  }
}
