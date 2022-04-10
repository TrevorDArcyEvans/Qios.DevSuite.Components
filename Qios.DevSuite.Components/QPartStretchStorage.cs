// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPartStretchStorage
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QPartStretchStorage
  {
    private const int SHRINKSTRETCH_COLLECTIONSIZE = 10;
    public QPartDirection Direction;
    public IQPart LastCanStretchPart;
    public IQPart LastCanShrinkPart;
    public int NotShrinkSize;
    public int UsedShrinkSize;
    public int AvailableShrinkSize;
    public int NotStretchSize;
    public int UsedStretchSize;
    public int AvailableStretchSize;
    private ArrayList m_oCanShrinkParts;
    private ArrayList m_oCanStretchParts;

    public void AddCanShrinkPart(IQPart part)
    {
      if (this.m_oCanShrinkParts == null)
        this.m_oCanShrinkParts = new ArrayList(10);
      this.m_oCanShrinkParts.Add((object) part);
      this.LastCanShrinkPart = part;
    }

    public void AddCanStretchPart(IQPart part)
    {
      if (this.m_oCanStretchParts == null)
        this.m_oCanStretchParts = new ArrayList(10);
      this.m_oCanStretchParts.Add((object) part);
      this.LastCanStretchPart = part;
    }

    public void SortCanShrinkCollection()
    {
      if (this.m_oCanShrinkParts == null)
        return;
      bool flag = false;
      QPartStretchStorage.QPartShrinkComparer qpartShrinkComparer = new QPartStretchStorage.QPartShrinkComparer(this.Direction);
      for (int index = 0; index < this.m_oCanShrinkParts.Count - 1; ++index)
      {
        if (qpartShrinkComparer.Compare(this.m_oCanShrinkParts[index], this.m_oCanShrinkParts[index + 1]) < 0)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      this.m_oCanShrinkParts.Sort((IComparer) qpartShrinkComparer);
    }

    public int CanShrinkCount => this.m_oCanShrinkParts == null ? 0 : this.m_oCanShrinkParts.Count;

    public int CanStretchCount => this.m_oCanStretchParts == null ? 0 : this.m_oCanStretchParts.Count;

    public IQPart GetCanShrinkPart(int index) => this.m_oCanShrinkParts == null ? (IQPart) null : this.m_oCanShrinkParts[index] as IQPart;

    public IQPart GetCanStretchPart(int index) => this.m_oCanStretchParts == null ? (IQPart) null : this.m_oCanStretchParts[index] as IQPart;

    public bool ShouldShrink => this.CanShrinkCount > 0 && this.AvailableShrinkSize < this.UsedShrinkSize;

    public bool ShouldStretch => this.CanStretchCount > 0 && this.AvailableStretchSize > this.UsedStretchSize;

    private class QPartShrinkComparer : IComparer
    {
      private QPartDirection m_eDirection;

      public QPartShrinkComparer(QPartDirection direction) => this.m_eDirection = direction;

      public int Compare(object x, object y)
      {
        IQPart qpart1 = x as IQPart;
        IQPart qpart2 = y as IQPart;
        if (qpart1 == qpart2)
          return 0;
        if (qpart1 == null)
          return 1;
        if (qpart2 == null)
          return -1;
        return this.m_eDirection != QPartDirection.Horizontal ? qpart2.CalculatedProperties.OuterSize.Height - qpart1.CalculatedProperties.OuterSize.Height : qpart2.CalculatedProperties.OuterSize.Width - qpart1.CalculatedProperties.OuterSize.Width;
      }
    }
  }
}
