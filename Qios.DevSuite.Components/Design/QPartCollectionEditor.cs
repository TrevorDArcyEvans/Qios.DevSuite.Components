// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QPartCollectionEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public abstract class QPartCollectionEditor : CollectionEditor
  {
    private CollectionEditor.CollectionForm m_oCollectionForm;

    public QPartCollectionEditor(Type type)
      : base(type)
    {
    }

    protected override CollectionEditor.CollectionForm CreateCollectionForm()
    {
      this.m_oCollectionForm = base.CreateCollectionForm();
      return this.m_oCollectionForm;
    }

    protected override object[] GetItems(object editValue)
    {
      ArrayList arrayList = new ArrayList();
      QPartCollection qpartCollection = editValue as QPartCollection;
      for (int index = 0; index < qpartCollection.Count; ++index)
      {
        IQPart qpart = qpartCollection[index];
        if (qpart != null && !qpart.IsSystemPart)
          arrayList.Add((object) qpart);
      }
      return (object[]) arrayList.ToArray(typeof (object));
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      ITypeDescriptorContext context1 = this.Context;
      try
      {
        return base.EditValue(context, provider, value);
      }
      finally
      {
        QDesignerHelper.SetCollectionEditorContext((CollectionEditor) this, context1);
      }
    }

    protected override object CreateInstance(Type itemType)
    {
      QDesignerHelper.NotifyDesignerOfCreation((object) this, true, (IServiceProvider) this.Context);
      try
      {
        return base.CreateInstance(itemType);
      }
      finally
      {
        QDesignerHelper.NotifyDesignerOfCreation((object) this, false, (IServiceProvider) this.Context);
      }
    }
  }
}
