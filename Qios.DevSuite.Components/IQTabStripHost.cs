// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQTabStripHost
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal interface IQTabStripHost
  {
    QTabStripPaintParams RetrieveTabStripPaintParams();

    void HandleTabStripUiRequest(
      QTabStrip sender,
      QCommandUIRequest request,
      Rectangle invalidateBounds);

    void StartAnimateTimer(QTabStrip sender);

    void StopAnimateTimer(QTabStrip sender);

    bool UserIsDraggingTabButton { get; }

    bool UserIsDragging { get; }

    bool AllowDrag { get; set; }

    bool AllowDrop { get; set; }

    bool AllowExternalDrop { get; set; }
  }
}
