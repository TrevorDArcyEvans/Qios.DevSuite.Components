// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QWeakEventConsumerCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  internal class QWeakEventConsumerCollection : CollectionBase
  {
    public QWeakEventConsumer this[int index] => this.InnerList[index] as QWeakEventConsumer;

    public void Add(QWeakEventConsumer consumer) => this.InnerList.Add((object) consumer);

    public void Remove(QWeakEventConsumer consumer) => this.InnerList.Remove((object) consumer);

    public void DetachAndRemove(Delegate targetDelegate)
    {
      for (int index = this.Count - 1; index >= 0; --index)
      {
        if (this[index].TargetDelegate.EqualsDelegate(targetDelegate))
        {
          this[index].DetachEvent();
          this.RemoveAt(index);
        }
      }
    }

    public void DetachAndRemoveAll()
    {
      for (int index = this.Count - 1; index >= 0; --index)
        this[index].DetachEvent();
      this.Clear();
    }

    ~QWeakEventConsumerCollection() => this.DetachAndRemoveAll();
  }
}
