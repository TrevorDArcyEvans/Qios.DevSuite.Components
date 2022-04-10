// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMenuItemCollectionEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QMenuItemCollectionEditor : CollectionEditor
  {
    private CollectionEditor.CollectionForm m_oCollectionForm;

    public QMenuItemCollectionEditor(Type type)
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

    protected override Type[] CreateNewItemTypes() => new Type[1]
    {
      typeof (QMenuItem)
    };

    protected override Type CreateCollectionItemType() => typeof (QMenuItem);
  }
}
