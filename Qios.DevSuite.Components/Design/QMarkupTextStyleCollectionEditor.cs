// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QMarkupTextStyleCollectionEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QMarkupTextStyleCollectionEditor : CollectionEditor
  {
    private System.Type[] m_aTypes = new System.Type[1]
    {
      typeof (QMarkupTextStyle)
    };

    public QMarkupTextStyleCollectionEditor(System.Type type)
      : base(type)
    {
    }

    protected override CollectionEditor.CollectionForm CreateCollectionForm()
    {
      CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
      PropertyGrid propertyGrid = QMisc.FindPropertyGrid((Control) collectionForm);
      if (propertyGrid != null)
        propertyGrid.HelpVisible = true;
      return collectionForm;
    }

    protected override System.Type[] CreateNewItemTypes() => this.m_aTypes;

    protected override System.Type CreateCollectionItemType() => typeof (QMarkupTextStyle);
  }
}
