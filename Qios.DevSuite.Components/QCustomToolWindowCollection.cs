// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCustomToolWindowCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;

namespace Qios.DevSuite.Components
{
  public class QCustomToolWindowCollection : QWeakReferenceCollection
  {
    public void Add(QCustomToolWindow window)
    {
      if (this.ContainsObject((object) window, true))
        return;
      this.AddObject((object) window);
    }

    [Obsolete("Obsolete since version 1.1.0.50, Not implemented anymore. use the Add method")]
    public void Insert(int index, QCustomToolWindow window) => throw new NotImplementedException();

    public void Remove(QCustomToolWindow window) => this.RemoveObject((object) window, true);

    public QCustomToolWindow this[int index]
    {
      get
      {
        QWeakReference qweakReference = base[index];
        return qweakReference != null && !qweakReference.Finalized && qweakReference.IsAlive ? qweakReference.Target as QCustomToolWindow : (QCustomToolWindow) null;
      }
    }

    public bool Contains(QCustomToolWindow window) => this.ContainsObject((object) window, true);

    public int IndexOf(QCustomToolWindow window) => this.IndexOfObject((object) window, true);

    public void CopyTo(QCustomToolWindow[] windows, int index)
    {
      for (int index1 = index; index1 < this.Count; ++index1)
        windows[index1] = this[index1];
    }
  }
}
