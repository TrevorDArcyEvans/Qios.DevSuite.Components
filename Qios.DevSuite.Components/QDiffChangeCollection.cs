// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDiffChangeCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;

namespace Qios.DevSuite.Components
{
  public class QDiffChangeCollection : ReadOnlyCollectionBase
  {
    internal int Add(QDiffChange change) => this.InnerList.Add((object) change);

    public void CopyTo(QDiffChange[] changes, int index) => ((ICollection) this).CopyTo((Array) changes, index);

    public QDiffChange this[int index]
    {
      get => (QDiffChange) this.InnerList[index];
      set => this.InnerList[index] = (object) value;
    }
  }
}
