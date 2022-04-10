// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextStyleCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  [Editor(typeof (QMarkupTextStyleCollectionEditor), typeof (UITypeEditor))]
  public class QMarkupTextStyleCollection : CollectionBase, IList, ICollection, IEnumerable
  {
    private QMarkupText m_oParentMarkupText;

    internal QMarkupTextStyleCollection(QMarkupText parentMarkupText) => this.m_oParentMarkupText = parentMarkupText;

    public void Add(QMarkupTextStyle style)
    {
      this.InnerList.Add((object) style);
      if (this.m_oParentMarkupText != null)
        style.PutParentMarkupText(this.m_oParentMarkupText);
      this.NotifyParentMarkupTextOfChange(true, true);
    }

    public void Insert(int index, QMarkupTextStyle style)
    {
      this.InnerList.Insert(index, (object) style);
      if (this.m_oParentMarkupText != null)
        style.PutParentMarkupText(this.m_oParentMarkupText);
      this.NotifyParentMarkupTextOfChange(true, true);
    }

    public new void RemoveAt(int index)
    {
      QMarkupTextStyle qmarkupTextStyle = this[index];
      this.InnerList.RemoveAt(index);
      if (qmarkupTextStyle.ParentMarkupText == this.m_oParentMarkupText)
        qmarkupTextStyle.PutParentMarkupText((QMarkupText) null);
      this.NotifyParentMarkupTextOfChange(true, true);
    }

    public void Remove(QMarkupTextStyle style)
    {
      this.InnerList.Remove((object) style);
      if (style.ParentMarkupText == this.m_oParentMarkupText)
        style.PutParentMarkupText((QMarkupText) null);
      this.NotifyParentMarkupTextOfChange(true, true);
    }

    protected override void OnClear()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ParentMarkupText == this.m_oParentMarkupText)
          this[index].PutParentMarkupText((QMarkupText) null);
      }
      base.OnClear();
    }

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      this.NotifyParentMarkupTextOfChange(true, true);
    }

    public QMarkupTextStyle this[int index] => (QMarkupTextStyle) this.InnerList[index];

    public int IndexOf(QMarkupTextStyle style) => this.InnerList.IndexOf((object) style);

    private void NotifyParentMarkupTextOfChange() => this.NotifyParentMarkupTextOfChange(false, true);

    private void NotifyParentMarkupTextOfChange(bool processMarkup, bool applyAttributes)
    {
      if (this.m_oParentMarkupText == null)
        return;
      if (processMarkup)
        this.m_oParentMarkupText.ProcessMarkup();
      if (applyAttributes)
        this.m_oParentMarkupText.ApplyAttributes();
      this.m_oParentMarkupText.RaiseUpdateRequested(QCommandUIRequest.PerformLayout);
    }

    int IList.Add(object value)
    {
      QMarkupTextStyle style = value as QMarkupTextStyle;
      this.Add(style);
      return this.IndexOf(style);
    }

    void IList.Insert(int index, object value) => this.Insert(index, value as QMarkupTextStyle);

    void IList.Remove(object value) => this.Remove((QMarkupTextStyle) value);

    void IList.RemoveAt(int index) => this.RemoveAt(index);
  }
}
