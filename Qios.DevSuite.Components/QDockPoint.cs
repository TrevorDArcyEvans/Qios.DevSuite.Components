// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockPoint
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QDockPoint
  {
    public Rectangle DockRectangle;
    public QDockPosition DockPosition;
    public int InsertIndex;
    public Control Parent;
    public QDockBar DockBar;
    public QDockControl ControlToPlaceOnContainer;
    public QDockOrientation DockContainerOrientation;

    public QDockPoint()
    {
    }

    public QDockPoint(
      Rectangle dockRectangle,
      QDockPosition dockPosition,
      Control parent,
      int insertIndex,
      QDockBar dockBar,
      QDockOrientation dockContainerOrientation,
      QDockControl controlToPlaceOnContainer)
    {
      this.DockRectangle = dockRectangle;
      this.DockPosition = dockPosition;
      this.Parent = parent;
      this.InsertIndex = insertIndex;
      this.DockBar = dockBar;
      this.DockContainerOrientation = dockContainerOrientation;
      this.ControlToPlaceOnContainer = controlToPlaceOnContainer;
    }

    public QDockPoint(
      int left,
      int top,
      int width,
      int height,
      QDockPosition dockPosition,
      Control parent,
      int insertIndex,
      QDockBar dockBar,
      QDockOrientation dockContainerOrientation,
      QDockControl controlToPlaceOnContainer)
      : this(new Rectangle(left, top, width, height), dockPosition, parent, insertIndex, dockBar, dockContainerOrientation, controlToPlaceOnContainer)
    {
    }

    public QDockPoint(QDockPoint basePoint)
      : this(basePoint.DockRectangle, basePoint.DockPosition, basePoint.Parent, basePoint.InsertIndex, basePoint.DockBar, basePoint.DockContainerOrientation, basePoint.ControlToPlaceOnContainer)
    {
    }

    public QDockContainer DockContainer => !(this.Parent is QDockContainer) ? (QDockContainer) null : (QDockContainer) this.Parent;
  }
}
