// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QGroupPartConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Qios.DevSuite.Components
{
  public class QGroupPartConfiguration : QContentPartConfiguration
  {
    private QStringsOrderHelper m_oContentPartOrderHelper;
    private string m_sDefaultContentPartLayoutOrder;

    public QGroupPartConfiguration() => this.Properties.DefineProperty(12, (object) this.DefaultContentPartLayoutOrder);

    protected virtual string DefaultContentPartLayoutOrder => this.m_sDefaultContentPartLayoutOrder;

    internal void PutContentPartLayoutOrder(string value) => this.m_sDefaultContentPartLayoutOrder = value;

    [QPropertyIndex(13)]
    [Browsable(false)]
    public override QPartAlignment ContentAlignmentHorizontal
    {
      get => base.ContentAlignmentHorizontal;
      set => base.ContentAlignmentHorizontal = value;
    }

    [Browsable(false)]
    [QPropertyIndex(14)]
    public override QPartAlignment ContentAlignmentVertical
    {
      get => base.ContentAlignmentVertical;
      set => base.ContentAlignmentVertical = value;
    }

    [QPropertyIndex(11)]
    [Category("QAppearance")]
    [Description("Gets or sets the direction of the content of this item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual QPartDirection Direction
    {
      get => (QPartDirection) this.Properties.GetPropertyAsValueType(11);
      set => this.Properties.SetProperty(11, (object) value);
    }

    private void SecureContentLayoutOrderHelper()
    {
      if (this.m_oContentPartOrderHelper != null || this.DefaultContentPartLayoutOrder == null || this.DefaultContentPartLayoutOrder.Length <= 0)
        return;
      this.m_oContentPartOrderHelper = new QStringsOrderHelper(this.DefaultContentPartLayoutOrder);
    }

    [QPropertyIndex(12)]
    [Category("QAppearance")]
    [Description("Gets or sets the content layout order of the child parts. This defines when a part is layed out. The actual position is also determined by the alignment of a part. Set this string to String.Empty (not null) when you want to order manually or want to add parts.")]
    [Editor(typeof (QStringsOrderEditor), typeof (UITypeEditor))]
    public virtual string ContentLayoutOrder
    {
      get => this.Properties.GetProperty(12) as string;
      set
      {
        this.SecureContentLayoutOrderHelper();
        if (this.m_oContentPartOrderHelper != null)
          value = this.m_oContentPartOrderHelper.Validate(value);
        this.Properties.SetProperty(12, (object) value);
      }
    }
  }
}
