// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxCompositeWindow
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QInputBoxCompositeWindow : QCompositeWindow
  {
    public QInputBoxCompositeWindow(IWin32Window ownerWindow)
      : base((IQPart) null, (QPartCollection) null, (QColorScheme) null, ownerWindow)
    {
    }

    public QInputBoxCompositeWindow()
    {
    }

    public virtual void ClearInputBoxItems() => this.Composite.Items.Clear();

    public virtual void AddInputBoxItem(QCompositeItemBase item) => this.Composite.Items.Add((IQPart) item, true);

    public virtual QCompositeItemBase CreateInputBoxItem(
      object item,
      QInputBox owner,
      bool selected)
    {
      if (item is QCompositeItemBase inputBoxItem1)
      {
        inputBoxItem1.SystemReference = (object) inputBoxItem1;
        if (selected)
          inputBoxItem1.AdjustState(QItemStates.Hot, true, QCompositeActivationType.None);
        return inputBoxItem1;
      }
      QCompositeInputBoxItem inputBoxItem2 = new QCompositeInputBoxItem(owner);
      inputBoxItem2.SystemReference = item;
      if (selected)
        inputBoxItem2.AdjustState(QItemStates.Hot, true, QCompositeActivationType.None);
      string itemText = owner.GetItemText(item);
      inputBoxItem2.Title = itemText == null || itemText.Length <= 0 ? " " : itemText;
      return (QCompositeItemBase) inputBoxItem2;
    }
  }
}
