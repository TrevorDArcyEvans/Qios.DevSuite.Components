// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QBalloonExtenderPair
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Design
{
  internal class QBalloonExtenderPair
  {
    public QBalloon Balloon;
    public IComponent Component;
    private string m_sText;

    public string Text
    {
      get => this.m_sText;
      set => this.m_sText = value;
    }

    internal QBalloonExtenderPair(QBalloon balloon, IComponent component, string text)
    {
      this.Balloon = balloon;
      this.Component = component;
      this.Text = text;
    }
  }
}
