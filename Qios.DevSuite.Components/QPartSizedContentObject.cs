// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartSizedContentObject
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPartSizedContentObject : IQPartSizedContent
  {
    private object m_oContentObject;
    private Size m_oSize;

    public QPartSizedContentObject(object contentObject, Size size)
    {
      this.m_oContentObject = contentObject;
      this.m_oSize = size;
    }

    public Size Size => this.m_oSize;

    public virtual void CalculateSize(QPartLayoutContext layoutContext)
    {
    }

    public object ContentObject => this.m_oContentObject;
  }
}
