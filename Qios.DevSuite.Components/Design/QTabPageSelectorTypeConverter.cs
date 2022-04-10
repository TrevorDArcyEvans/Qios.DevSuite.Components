// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QTabPageSelectorTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  internal class QTabPageSelectorTypeConverter : ComponentConverter
  {
    public QTabPageSelectorTypeConverter(System.Type type)
      : base(type)
    {
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => false;

    protected override bool IsValueAllowed(ITypeDescriptorContext context, object value)
    {
      Control instance = context.Instance as Control;
      QTabPage ctl = value as QTabPage;
      return instance != null && ctl != null && instance.Contains((Control) ctl);
    }
  }
}
