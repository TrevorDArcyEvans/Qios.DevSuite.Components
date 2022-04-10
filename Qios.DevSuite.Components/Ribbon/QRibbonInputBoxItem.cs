// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonInputBoxItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonInputBoxItem : QRibbonItem
  {
    protected QRibbonInputBoxItem(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
    }

    public QRibbonInputBoxItem()
    {
    }

    protected override QCompositeMenuItemContentType DefaultContentType => QCompositeMenuItemContentType.Control;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DefaultValue(QCompositeMenuItemContentType.Control)]
    public override QCompositeMenuItemContentType ContentType
    {
      get => base.ContentType;
      set => base.ContentType = value;
    }

    protected override Control CreateControl() => (Control) new QRibbonInputBox();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Control Control
    {
      get => base.Control;
      set
      {
      }
    }

    [Description("Gets the inputBox")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QInputBox InputBox => base.Control as QInputBox;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }
  }
}
