// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQScrollBarHandlerParent
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQScrollBarHandlerParent
  {
    void Invalidate();

    bool IsScrollingAnimated { get; }

    void StopScrolling();

    void ScrollHorizontal(int xValue, QScrollablePartMethod method, bool animated, bool page);

    void ScrollVertical(int yValue, QScrollablePartMethod method, bool animated, bool page);

    void ScrollIntoView(Rectangle bounds, bool animated);

    bool IsAtHorizontalStart { get; }

    bool IsAtVerticalStart { get; }

    bool IsAtHorizontalEnd { get; }

    bool IsAtVerticalEnd { get; }

    Point ScrollOffset { get; }

    DockStyle Dock { get; }

    bool IsVisible { get; }

    Rectangle Bounds { get; }

    Size ButtonSize { get; }

    Size RequestedSize { get; }

    Rectangle ViewPort { get; }

    Size ContentSize { get; }

    int ScrollStepSize { get; }

    QScrollBarConfiguration Configuration { get; set; }

    QScrollBarAppearance Appearance { get; }

    IQItemColorHost ColorHost { get; }
  }
}
