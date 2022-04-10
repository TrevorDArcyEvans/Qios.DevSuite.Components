// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeItemInputBox
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeItemControlDesigner), typeof (IDesigner))]
  public class QCompositeItemInputBox : QCompositeItemControl
  {
    protected QCompositeItemInputBox(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
    }

    public QCompositeItemInputBox()
      : base(QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    internal QCompositeItemInputBox(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct() => base.Control = (Control) this.CreateInputBox();

    protected virtual QInputBox CreateInputBox() => new QInputBox();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Control Control
    {
      get => base.Control;
      set => base.Control = value;
    }

    [Category("QBehavior")]
    [Description("Contains the QInputBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QInputBox InputBox => base.Control as QInputBox;
  }
}
