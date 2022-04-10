// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabControlPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QTabControlPaintParams
  {
    private Color m_oContentBorder;
    private Color m_oContentBackground1;
    private Color m_oContentBackground2;
    private Color m_oContentShade;

    internal QTabControlPaintParams()
    {
    }

    public Color ContentBorder
    {
      get => this.m_oContentBorder;
      set => this.m_oContentBorder = value;
    }

    public Color ContentBackground1
    {
      get => this.m_oContentBackground1;
      set => this.m_oContentBackground1 = value;
    }

    public Color ContentBackground2
    {
      get => this.m_oContentBackground2;
      set => this.m_oContentBackground2 = value;
    }

    public Color ContentShade
    {
      get => this.m_oContentShade;
      set => this.m_oContentShade = value;
    }
  }
}
