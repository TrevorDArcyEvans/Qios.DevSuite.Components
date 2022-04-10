// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockingWindowGroup
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QDockingWindowGroup : IQDockingWindowItem
  {
    private QDockingWindowItemCollection m_aItems;

    public QDockingWindowGroup(QDockingWindow[] windows, QDockingWindow expandedWindow)
    {
      bool flag = false;
      this.m_aItems = new QDockingWindowItemCollection();
      for (int index = 0; index < windows.Length; ++index)
      {
        QDockingWindowItem windowItem = new QDockingWindowItem(windows[index], this);
        this.m_aItems.Add((IQDockingWindowItem) windowItem);
        if (expandedWindow != null && windows[index] == expandedWindow)
        {
          windowItem.IsExpanded = true;
          flag = true;
        }
      }
      if (flag || this.Count <= 0)
        return;
      this[0].IsExpanded = true;
    }

    public SizeF GetLargestTextSize(Graphics graphics, Font font, StringFormat format)
    {
      float width = 0.0f;
      float height = 0.0f;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this.m_aItems[index] is QDockingWindowItem)
        {
          QDockingWindowItem qdockingWindowItem = this[index];
          SizeF sizeF = graphics.MeasureString(qdockingWindowItem.Window.Text, font, PointF.Empty, format);
          if ((double) sizeF.Width > (double) width)
            width = sizeF.Width;
          if ((double) sizeF.Height > (double) height)
            height = sizeF.Height;
        }
      }
      return new SizeF(width, height);
    }

    public QDockingWindowItemCollection Items => this.m_aItems;

    public QDockingWindowItem this[int index] => (QDockingWindowItem) this.Items[index];

    public bool IsGroup => true;

    public int Count => this.Items.Count;

    public void SetAllButtonsToNotExpanded()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].IsExpanded = false;
    }
  }
}
