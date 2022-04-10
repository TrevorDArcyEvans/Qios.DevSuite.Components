// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QTabControlControlsCollectionEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QTabControlControlsCollectionEditor : CollectionEditor
  {
    public QTabControlControlsCollectionEditor(Type type)
      : base(type)
    {
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      QTabControl instance1 = context.Instance as QTabControl;
      ISupportInitialize instance2 = context.Instance as ISupportInitialize;
      if (instance1 != null)
      {
        instance1.SuspendLayout();
        instance1.SuspendDraw();
      }
      instance2?.BeginInit();
      object obj = base.EditValue(context, provider, value);
      instance2?.EndInit();
      if (instance1 != null)
      {
        instance1.ResumeLayout(true);
        instance1.ResumeDraw(true);
      }
      return obj;
    }

    protected override Type[] CreateNewItemTypes() => new Type[1]
    {
      typeof (QTabPage)
    };

    protected override Type CreateCollectionItemType() => typeof (QTabPage);
  }
}
