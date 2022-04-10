// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextElementEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextElementEventArgs : EventArgs
  {
    private QMarkupTextElement m_oElement;
    private MouseEventArgs m_oMouseArgs;

    public QMarkupTextElementEventArgs(QMarkupTextElement element, MouseEventArgs mouseArgs)
    {
      this.m_oElement = element;
      this.m_oMouseArgs = mouseArgs;
    }

    public QMarkupTextElement Element => this.m_oElement;

    public string HRef => !(this.m_oElement is QMarkupTextElementAnchor) ? string.Empty : ((QMarkupTextElementAnchor) this.m_oElement).HRef;

    public MouseEventArgs MouseArgs => this.m_oMouseArgs;
  }
}
