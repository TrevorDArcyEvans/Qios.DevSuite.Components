// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QShapeCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  public class QShapeCollection : CollectionBase, IQWeakEventPublisher
  {
    private bool m_bWeakEventHandlers = true;
    private QWeakDelegate m_oCollectionChangedDelegate;

    [QWeakEvent]
    public event EventHandler CollectionChanged
    {
      add => this.m_oCollectionChangedDelegate = QWeakDelegate.Combine(this.m_oCollectionChangedDelegate, (Delegate) value, this.m_bWeakEventHandlers);
      remove => this.m_oCollectionChangedDelegate = QWeakDelegate.Remove(this.m_oCollectionChangedDelegate, (Delegate) value);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool WeakEventHandlers
    {
      get => this.m_bWeakEventHandlers;
      set => this.m_bWeakEventHandlers = value;
    }

    public void Add(QShape shape)
    {
      this.InnerList.Add((object) shape);
      this.OnCollectionChanged(EventArgs.Empty);
    }

    public void Remove(QShape shape)
    {
      this.InnerList.Remove((object) shape);
      this.OnCollectionChanged(EventArgs.Empty);
    }

    public void Insert(int index, QShape shape)
    {
      this.InnerList.Insert(index, (object) shape);
      this.OnCollectionChanged(EventArgs.Empty);
    }

    public int IndexOf(string shapeName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.Compare(this[index].ShapeName, shapeName, true, CultureInfo.InvariantCulture) == 0)
          return index;
      }
      return -1;
    }

    public int IndexOf(QShape shape) => this.Contains(shape) ? this.InnerList.IndexOf((object) shape) : -1;

    public bool Contains(QShape shape) => this.InnerList.Contains((object) shape);

    public bool Contains(string shapeName) => this.IndexOf(shapeName) >= 0;

    public QShape this[int index] => (QShape) this.InnerList[index];

    public QShape this[string shapeName]
    {
      get
      {
        int index = this.IndexOf(shapeName);
        return index >= 0 ? this[index] : (QShape) null;
      }
    }

    public void CopyTo(QShape[] shapes, int index) => ((ICollection) this).CopyTo((Array) shapes, index);

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      this.OnCollectionChanged(EventArgs.Empty);
    }

    protected virtual void OnCollectionChanged(EventArgs e) => this.m_oCollectionChangedDelegate = QWeakDelegate.InvokeDelegate(this.m_oCollectionChangedDelegate, (object) this, (object) e);
  }
}
