// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabControlControlsCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Editor(typeof (QTabControlControlsCollectionEditor), typeof (UITypeEditor))]
  public sealed class QTabControlControlsCollection : 
    Control.ControlCollection,
    IList,
    ICollection,
    IEnumerable
  {
    private QTabControl m_oTabControl;

    public QTabControlControlsCollection(QTabControl tabControl)
      : base((Control) tabControl)
    {
      this.m_oTabControl = tabControl;
    }

    public int TabPagesCount
    {
      get
      {
        int tabPagesCount = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index] is QTabPage)
            ++tabPagesCount;
        }
        return tabPagesCount;
      }
    }

    public int AccessibleTabPagesCount
    {
      get
      {
        int accessibleTabPagesCount = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index] is QTabPage qtabPage && qtabPage.ButtonVisible && qtabPage.Enabled)
            ++accessibleTabPagesCount;
        }
        return accessibleTabPagesCount;
      }
    }

    public override void Add(Control value)
    {
      if (value is QTabPage qtabPage)
        qtabPage.PutTabControl(this.m_oTabControl);
      base.Add(value);
    }

    public void Insert(int index, Control value)
    {
      if (value is QTabPage qtabPage)
        qtabPage.PutTabControl(this.m_oTabControl);
      this.Add(value);
      this.SetChildIndex(value, index);
    }

    public override void AddRange(Control[] controls)
    {
      this.m_oTabControl.SuspendLayout();
      for (int index = 0; index < controls.Length; ++index)
      {
        if (controls[index] is QTabPage control)
          control.PutTabControl(this.m_oTabControl);
      }
      base.AddRange(controls);
      this.m_oTabControl.ResumeLayout(true);
    }

    public override void Remove(Control value)
    {
      if (value is QTabPage qtabPage)
        qtabPage.PutTabControl((QTabControl) null);
      base.Remove(value);
    }

    public override void Clear()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] is QTabPage qtabPage)
          qtabPage.PutTabControl((QTabControl) null);
      }
      base.Clear();
    }

    public void CopyTo(Control[] controls, int index) => this.CopyTo((Array) controls, index);

    int IList.Add(object value)
    {
      Control control = value as Control;
      this.Add(control);
      return this.IndexOf(control);
    }

    void IList.Insert(int index, object value)
    {
      Control control = value as Control;
      this.Insert(index, control);
    }

    void IList.Clear() => this.Clear();

    bool IList.Contains(object value) => this.Contains((Control) value);

    int IList.IndexOf(object value) => this.IndexOf((Control) value);

    void IList.Remove(object value) => this.Remove((Control) value);

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
