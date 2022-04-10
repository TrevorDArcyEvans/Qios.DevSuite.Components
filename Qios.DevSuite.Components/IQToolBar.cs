// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.IQToolBar
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal interface IQToolBar
  {
    Rectangle Bounds { get; }

    Rectangle ProposedBounds { get; set; }

    string Text { get; set; }

    Size MinimumSize { get; }

    int RowIndex { get; set; }

    IWin32Window OwnerWindow { get; set; }

    int ToolBarPositionIndex { get; }

    void SetToolBarPositionIndex(int positionIndex, bool updateParentRow);

    void DockToolBar(QToolBarHost host, int rowIndex, int toolBarIndex, int requestedPosition);

    void FloatToolBar(Point screenPoint);

    Size GetMinimumSizeForRow(QToolBarRow row, IQToolBar addingToolBar);

    Size RequestedSize { get; }

    QToolBarHost ToolBarHost { get; }

    bool Stretched { get; }

    bool IsFloating { get; }

    QToolBarForm ToolBarForm { get; set; }

    QToolBarRow ParentToolBarRow { get; set; }

    QToolBarRow OriginalToolBarRow { get; set; }

    QToolBarAction Action { get; }

    void StartMoving(Point startOffset);

    void StartCustomizing();

    void EndCustomizing();

    bool ShowMoreItemsButton { get; }

    int UserRequestedPosition { get; set; }

    int UserPriority { get; set; }

    bool IsVisible { get; }

    bool IsCustomizing { get; }

    bool Horizontal { get; }

    Rectangle CustomizeItemBounds { get; }

    QColorScheme ColorScheme { get; }

    QToolBarPaintParams PaintParams { get; }

    QToolBarConfiguration ToolBarConfiguration { get; }

    void LayoutToolBar(
      Rectangle rectangle,
      QCommandContainerOrientation orientation,
      QToolBarLayoutFlags layoutFlags);
  }
}
