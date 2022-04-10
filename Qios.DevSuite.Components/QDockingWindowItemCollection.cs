// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockingWindowItemCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  internal class QDockingWindowItemCollection : CollectionBase
  {
    public void Add(IQDockingWindowItem windowItem) => this.List.Add((object) windowItem);

    public void Insert(int index, IQDockingWindowItem windowItem)
    {
      if (index >= this.List.Count)
        this.List.Add((object) windowItem);
      else
        this.List.Insert(index, (object) windowItem);
    }

    public void Remove(IQDockingWindowItem windowItem) => this.List.Remove((object) windowItem);

    public QDockingWindowItem GetWindowItem(QDockingWindow window)
    {
      for (int index = 0; index < this.List.Count; ++index)
      {
        if (this[index].IsGroup)
        {
          QDockingWindowItem windowItem = ((QDockingWindowGroup) this[index]).Items.GetWindowItem(window);
          if (windowItem != null)
            return windowItem;
        }
        else if (((QDockingWindowItem) this[index]).Window == window)
          return (QDockingWindowItem) this[index];
      }
      return (QDockingWindowItem) null;
    }

    public QDockingWindowItem GetFirstWindowItemWithGroupName(
      string windowGroupName)
    {
      switch (windowGroupName)
      {
        case "":
        case null:
          return (QDockingWindowItem) null;
        default:
          for (int index = 0; index < this.List.Count; ++index)
          {
            if (this[index].IsGroup)
            {
              QDockingWindowItem itemWithGroupName = ((QDockingWindowGroup) this[index]).Items.GetFirstWindowItemWithGroupName(windowGroupName);
              if (itemWithGroupName != null)
                return itemWithGroupName;
            }
            else
            {
              QDockingWindowItem itemWithGroupName = (QDockingWindowItem) this[index];
              if (itemWithGroupName.Window.WindowGroupName != null && string.Compare(itemWithGroupName.Window.WindowGroupName, windowGroupName, true, CultureInfo.InvariantCulture) == 0)
                return itemWithGroupName;
            }
          }
          return (QDockingWindowItem) null;
      }
    }

    public bool ContainsWindow(QDockingWindow window) => this.GetWindowItem(window) != null;

    public IQDockingWindowItem this[int index] => (IQDockingWindowItem) this.List[index];

    public int IndexOf(IQDockingWindowItem item) => this.InnerList.Contains((object) item) ? this.InnerList.IndexOf((object) item) : -1;
  }
}
