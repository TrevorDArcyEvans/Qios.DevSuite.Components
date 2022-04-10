// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QMarkupTextElementCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.Drawing;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  public class QMarkupTextElementCollection : CollectionBase
  {
    private QMarkupText m_oMarkupText;
    private QMarkupTextElement m_oParentElement;

    internal QMarkupTextElementCollection(QMarkupText markupText, QMarkupTextElement parentElement)
    {
      this.m_oMarkupText = markupText;
      this.m_oParentElement = parentElement;
    }

    internal void Add(QMarkupTextElement element)
    {
      this.InnerList.Add((object) element);
      element.PutParentElement(this.m_oParentElement);
    }

    internal void Remove(QMarkupTextElement element)
    {
      this.InnerList.Remove((object) element);
      if (element.ParentElement != this.m_oParentElement)
        return;
      element.PutParentElement((QMarkupTextElement) null);
    }

    public QMarkupTextElement this[int index] => (QMarkupTextElement) this.InnerList[index];

    public QMarkupTextElement this[string name] => this.FindElement(name, false);

    public int IndexOf(QMarkupTextElement element) => this.InnerList.IndexOf((object) element);

    internal QMarkupTextElement GetFocusableElement(bool forward, bool parents)
    {
      for (int index = forward ? 0 : this.Count - 1; (forward ? (index < this.Count ? 1 : 0) : (index >= 0 ? 1 : 0)) != 0; index += forward ? 1 : -1)
      {
        QMarkupTextElement focusableElement = this[index].GetFocusableElement(forward, parents);
        if (focusableElement != null)
          return focusableElement;
      }
      return (QMarkupTextElement) null;
    }

    public QMarkupTextElement GetElementThatContainsAbsolutePoint(PointF point)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        QMarkupTextElement containsAbsolutePoint = this[index].GetElementThatContainsAbsolutePoint(point);
        if (containsAbsolutePoint != null)
          return containsAbsolutePoint;
      }
      return (QMarkupTextElement) null;
    }

    public QMarkupTextElement FindElement(string name, bool deep)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        QMarkupTextElement element1 = this[index];
        if (string.Compare(element1.Name, name, true, CultureInfo.InvariantCulture) == 0)
          return element1;
        if (deep)
        {
          QMarkupTextElement element2 = element1.FindElement(name, true);
          if (element2 != null)
            return element2;
        }
      }
      return (QMarkupTextElement) null;
    }

    public void DisposeRenderedObjects(bool deep)
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].DisposeRenderedObjects(deep);
    }

    public void DisposeAttributes(bool deep)
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].DisposeAttributes(deep);
    }

    public void DisposeElements()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].Dispose();
    }
  }
}
