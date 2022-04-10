// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QExplorerGroupItemCollectionEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QExplorerGroupItemCollectionEditor : CollectionEditor
  {
    private CollectionEditor.CollectionForm m_oCollectionForm;

    public QExplorerGroupItemCollectionEditor(Type type)
      : base(type)
    {
    }

    protected override CollectionEditor.CollectionForm CreateCollectionForm()
    {
      this.m_oCollectionForm = base.CreateCollectionForm();
      return this.m_oCollectionForm;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      return this.m_oCollectionForm != null && this.m_oCollectionForm.Visible ? new QExplorerItemCollectionEditor(this.CollectionType).EditValue(context, provider, value) : base.EditValue(context, provider, value);
    }

    protected override Type[] CreateNewItemTypes() => new Type[1]
    {
      typeof (QExplorerItem)
    };

    protected override Type CreateCollectionItemType() => typeof (QExplorerItem);

    protected override object CreateInstance(Type itemType) => base.CreateInstance(itemType);
  }
}
