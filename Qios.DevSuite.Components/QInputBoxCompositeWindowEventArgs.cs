// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxCompositeWindowEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  public class QInputBoxCompositeWindowEventArgs : QInputBoxEventArgs
  {
    private QInputBoxCompositeWindow m_oWindow;

    public QInputBoxCompositeWindowEventArgs()
    {
    }

    public QInputBoxCompositeWindowEventArgs(QInputBoxCompositeWindow window) => this.m_oWindow = window;

    public QInputBoxCompositeWindow Window
    {
      get => this.m_oWindow;
      set => this.m_oWindow = value;
    }
  }
}
