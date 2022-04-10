// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStatusBarPanelCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  [Editor(typeof (QStatusBarPanelCollectionEditor), typeof (UITypeEditor))]
  [ToolboxItem(false)]
  public sealed class QStatusBarPanelCollection : CollectionBase, IList, ICollection, IEnumerable
  {
    private QStatusBar m_oStatusBar;

    public QStatusBarPanelCollection(QStatusBar statusBar) => this.m_oStatusBar = statusBar != null ? statusBar : throw new Exception(QResources.GetException("QStatusBarPanelCollection_NotNull", (object) nameof (statusBar)));

    public void Add(QStatusBarPanel panel)
    {
      if (panel == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (panel)));
      this.InnerList.Add((object) panel);
      panel.SetParent(this.m_oStatusBar);
      this.PerformStatusBarLayout();
    }

    public void Remove(QStatusBarPanel panel)
    {
      if (panel == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (panel)));
      this.InnerList.Remove((object) panel);
      panel.SetParent((QStatusBar) null);
      this.PerformStatusBarLayout();
    }

    public new void RemoveAt(int index)
    {
      ((QStatusBarPanel) this.List[index]).SetParent((QStatusBar) null);
      this.InnerList.RemoveAt(index);
      this.PerformStatusBarLayout();
    }

    public void Insert(int index, QStatusBarPanel panel)
    {
      if (panel == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (panel)));
      this.InnerList.Insert(index, (object) panel);
      panel.SetParent(this.m_oStatusBar);
      this.PerformStatusBarLayout();
    }

    public void AddRange(QStatusBarPanel[] panels)
    {
      if (panels == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (panels)));
      for (int index = 0; index < panels.Length; ++index)
        this.Add(panels[index]);
    }

    public void CopyTo(QStatusBarPanel[] panels, int index) => ((ICollection) this).CopyTo((Array) panels, index);

    public int IndexOf(QStatusBarPanel panel) => this.InnerList.Contains((object) panel) ? this.InnerList.IndexOf((object) panel) : -1;

    public bool Contains(QStatusBarPanel panel) => this.InnerList.Contains((object) panel);

    public QStatusBarPanel this[int index] => (QStatusBarPanel) this.InnerList[index];

    private void PerformStatusBarLayout()
    {
      if (this.m_oStatusBar == null || this.m_oStatusBar.PerformingLayout)
        return;
      this.m_oStatusBar.PerformLayout();
    }

    int IList.Add(object value)
    {
      QStatusBarPanel panel = (QStatusBarPanel) value;
      this.Add(panel);
      return this.IndexOf(panel);
    }

    void IList.Clear()
    {
      for (int index = this.Count - 1; index >= 0; --index)
        this.Remove(this[index]);
    }

    bool IList.Contains(object value) => this.Contains((QStatusBarPanel) value);

    int IList.IndexOf(object value) => this.IndexOf((QStatusBarPanel) value);

    void IList.Insert(int index, object value) => this.Insert(index, (QStatusBarPanel) value);

    void IList.Remove(object value) => this.Remove((QStatusBarPanel) value);

    void IList.RemoveAt(int index) => this.RemoveAt(index);

    bool IList.IsReadOnly => false;

    bool IList.IsFixedSize => false;

    object IList.this[int index]
    {
      get => (object) this[index];
      set
      {
      }
    }
  }
}
