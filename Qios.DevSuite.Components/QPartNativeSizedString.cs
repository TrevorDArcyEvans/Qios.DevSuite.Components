// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartNativeSizedString
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QPartNativeSizedString : IQPartSizedContent
  {
    private string m_sValue;
    private Size m_oSize;

    public QPartNativeSizedString()
    {
    }

    public QPartNativeSizedString(string stringValue) => this.m_sValue = stringValue;

    public string Value
    {
      get => this.m_sValue;
      set => this.m_sValue = value;
    }

    public void CalculateSize(QPartLayoutContext layoutContext) => this.m_oSize = NativeHelper.CalculateTextSize(this.m_sValue, layoutContext.Font, layoutContext.Graphics);

    public Size Size => this.m_oSize;

    public object ContentObject => (object) this.m_sValue;
  }
}
