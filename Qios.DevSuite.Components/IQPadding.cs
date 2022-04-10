// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQPadding
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public interface IQPadding
  {
    Size InflateSize(Size size, bool inflate, bool horizontal);

    Rectangle InflateRectangle(Rectangle rectangle, bool inflate, DockStyle dockStyle);

    QPadding ToPadding();
  }
}
