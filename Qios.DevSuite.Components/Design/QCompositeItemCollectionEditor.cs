// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QCompositeItemCollectionEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QCompositeItemCollectionEditor : QPartCollectionEditor
  {
    public QCompositeItemCollectionEditor(Type type)
      : base(type)
    {
    }

    protected override Type[] CreateNewItemTypes() => QCompositeDesignerHelper.DefaultCompositeCreationTypes;

    protected override Type CreateCollectionItemType() => typeof (QCompositeItemBase);
  }
}
