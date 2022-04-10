// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockControlCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QDockControlCollection : ReadOnlyCollectionBase
  {
    internal int Add(QDockControl dockControl) => this.InnerList.Add((object) dockControl);

    internal void Remove(QDockControl dockControl)
    {
      if (!this.InnerList.Contains((object) dockControl))
        return;
      this.InnerList.Remove((object) dockControl);
    }

    internal bool Contains(QDockControl dockControl) => this.InnerList.Contains((object) dockControl);

    public QDockControl this[int index] => (QDockControl) this.InnerList[index];

    public QDockControlCollection GetCurrentDockingWindows()
    {
      QDockControlCollection collectionToFill = new QDockControlCollection();
      for (int index = 0; index < this.Count; ++index)
        this[index].GetCurrentDockingWindows(collectionToFill);
      return collectionToFill;
    }
  }
}
