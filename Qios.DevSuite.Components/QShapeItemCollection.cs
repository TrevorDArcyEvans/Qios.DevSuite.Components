// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeItemCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QShapeItemCollection : CollectionBase
  {
    private QShape m_oShape;
    private int m_iMinimumItemCount;

    public QShapeItemCollection(int minimumItemCount) => this.m_iMinimumItemCount = minimumItemCount;

    public QShapeItemCollection(QShape shape, int minimumItemCount)
    {
      this.m_oShape = shape;
      this.m_iMinimumItemCount = minimumItemCount;
    }

    public int MinimumItemCount => this.m_iMinimumItemCount;

    public void Add(QShapeItem item)
    {
      this.InnerList.Add((object) item);
      this.ChangeItemParentShape(item, this.m_oShape);
      this.NotifyShapeCollectionChanged();
    }

    public void Remove(QShapeItem item)
    {
      if (this.Count <= this.m_iMinimumItemCount)
        throw new InvalidOperationException(QResources.GetException("QShapeItemCollection_CountBelowMinimumItemCount", (object) this.m_iMinimumItemCount));
      this.InnerList.Remove((object) item);
      this.ChangeItemParentShape(item, (QShape) null);
      this.NotifyShapeCollectionChanged();
    }

    public void Insert(int index, QShapeItem item)
    {
      this.InnerList.Insert(index, (object) item);
      this.ChangeItemParentShape(item, this.m_oShape);
      this.NotifyShapeCollectionChanged();
    }

    public void InsertBefore(QShapeItem beforeItem, QShapeItem item)
    {
      int index = this.IndexOf(beforeItem);
      if (index < 0)
        return;
      this.Insert(index, item);
    }

    public void InsertAfter(QShapeItem afterItem, QShapeItem item)
    {
      int num = this.IndexOf(afterItem);
      if (num < 0)
        return;
      if (num + 1 < this.Count)
        this.Insert(num + 1, item);
      else
        this.Add(item);
    }

    public QShapeItem GetNextItem(QShapeItem item)
    {
      if (this.Count <= 1)
        return (QShapeItem) null;
      int num = this.IndexOf(item);
      return num >= 0 ? this[num + 1 < this.Count ? num + 1 : 0] : (QShapeItem) null;
    }

    public QShapeItem GetPreviousItem(QShapeItem item)
    {
      if (this.Count <= 1)
        return (QShapeItem) null;
      int num = this.IndexOf(item);
      return num >= 0 ? this[num - 1 >= 0 ? num - 1 : this.Count - 1] : (QShapeItem) null;
    }

    protected override void OnClear()
    {
      for (int index = 0; index < this.Count; ++index)
        this.ChangeItemParentShape(this[index], (QShape) null);
      base.OnClear();
    }

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      this.NotifyShapeCollectionChanged();
    }

    public int IndexOf(QShapeItem item) => this.Contains(item) ? this.InnerList.IndexOf((object) item) : -1;

    public bool Contains(QShapeItem item) => this.InnerList.Contains((object) item);

    public QShapeItem this[int index] => (QShapeItem) this.InnerList[index];

    public QShapeItem[] GetItemsInRectangle(RectangleF rectangle, QShapeItemParts parts)
    {
      ArrayList arrayList = (ArrayList) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].GetItemPartsInRectangle(rectangle, parts, false) != QShapeItemParts.None)
        {
          if (arrayList == null)
            arrayList = new ArrayList();
          arrayList.Add((object) this[index]);
        }
      }
      return arrayList != null ? (QShapeItem[]) arrayList.ToArray(typeof (QShapeItem)) : (QShapeItem[]) null;
    }

    public QShapeItem[] GetItemsOnPoint(
      PointF point,
      float margin,
      QShapeItemParts parts)
    {
      ArrayList arrayList = (ArrayList) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].GetItemPartsOnPoint(point, Rectangle.Empty, margin, parts, false) != QShapeItemParts.None)
        {
          if (arrayList == null)
            arrayList = new ArrayList();
          arrayList.Add((object) this[index]);
        }
      }
      return arrayList != null ? (QShapeItem[]) arrayList.ToArray(typeof (QShapeItem)) : (QShapeItem[]) null;
    }

    public void SelectShapeItemParts(QShapeItemParts parts)
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].SelectionParts = parts;
    }

    public QShapeItem[] GetSelectedItems(QShapeItemParts parts)
    {
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < this.Count; ++index)
      {
        if ((this[index].SelectionParts & parts) != QShapeItemParts.None)
          arrayList.Add((object) this[index]);
      }
      return (QShapeItem[]) arrayList.ToArray(typeof (QShapeItem));
    }

    public void MoveSelectedItemPartsRelativeToCache(PointF relativePoint)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SelectionParts != QShapeItemParts.None)
          this[index].MoveSelectedPartsRelativeToCache(relativePoint);
      }
    }

    public void CacheSelectedParts()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SelectionParts != QShapeItemParts.None)
          this[index].CacheCurrentLocationProperties();
      }
    }

    public void RestoreSelectedPartsFromCache()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SelectionParts != QShapeItemParts.None)
          this[index].RestorePropertiesFromCache();
      }
    }

    public void CopyTo(QShapeItem[] items, int index) => ((ICollection) this).CopyTo((Array) items, index);

    private void ChangeItemParentShape(QShapeItem item, QShape shape)
    {
      if (this.m_oShape == null)
        return;
      item.PutParentShape(shape);
    }

    private void NotifyShapeCollectionChanged()
    {
      if (this.m_oShape == null)
        return;
      this.m_oShape.HandleCollectionChanged();
    }
  }
}
