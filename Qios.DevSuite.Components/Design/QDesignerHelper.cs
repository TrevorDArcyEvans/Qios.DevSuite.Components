// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QDesignerHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace Qios.DevSuite.Components.Design
{
  public static class QDesignerHelper
  {
    internal static void SetCollectionEditorContext(
      CollectionEditor editor,
      ITypeDescriptorContext context)
    {
      (typeof (CollectionEditor).GetField("currentContext", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException(QResources.GetException("General_ReturnValueNull", (object) "GetField(\"currentContext\")"))).SetValue((object) editor, (object) context);
    }

    public static void NotifyDesignerOfCreation(
      object creatingComponent,
      bool value,
      IServiceProvider provider)
    {
      if (provider == null)
        return;
      ISelectionService service1 = provider.GetService(typeof (ISelectionService)) as ISelectionService;
      IDesignerHost service2 = provider.GetService(typeof (IDesignerHost)) as IDesignerHost;
      if (service1 == null || service2 == null || !(service1.PrimarySelection is IComponent primarySelection) || !(service2.GetDesigner(primarySelection) is IQDesigner designer))
        return;
      designer.NotifyComponentCreation(creatingComponent, value);
    }
  }
}
