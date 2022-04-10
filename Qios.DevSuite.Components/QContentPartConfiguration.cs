// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QContentPartConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QContentPartConfiguration : QPartConfigurationBase
  {
    [QPropertyIndex(0)]
    [Description("Gets or sets the margin.")]
    [Category("QAppearance")]
    public virtual QMargin Margin
    {
      get => (QMargin) this.Properties.GetPropertyAsValueType(0);
      set => this.Properties.SetProperty(0, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(1)]
    [Description("Gets or sets the padding.")]
    public virtual QPadding Padding
    {
      get => (QPadding) this.Properties.GetPropertyAsValueType(1);
      set => this.Properties.SetProperty(1, (object) value);
    }

    [Description("Gets or sets whether the item is visible. Undefined means that is is based on other properties. This means that it is most often visible.")]
    [QPropertyIndex(10)]
    [Category("QAppearance")]
    public virtual QTristateBool Visible
    {
      get => (QTristateBool) this.Properties.GetPropertyAsValueType(10);
      set => this.Properties.SetProperty(10, (object) value);
    }

    [QPropertyIndex(2)]
    [Description("Gets or sets whether the item can be horizontal shrinked.")]
    [Category("QAppearance")]
    public virtual bool ShrinkHorizontal
    {
      get => (bool) this.Properties.GetPropertyAsValueType(2);
      set => this.Properties.SetProperty(2, (object) value);
    }

    [Description("Gets or sets whether the item can be vertical shrinked.")]
    [QPropertyIndex(3)]
    [Category("QAppearance")]
    public virtual bool ShrinkVertical
    {
      get => (bool) this.Properties.GetPropertyAsValueType(3);
      set => this.Properties.SetProperty(3, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(4)]
    [Description("Gets or sets whether the item can be horizontal stretched.")]
    public virtual bool StretchHorizontal
    {
      get => (bool) this.Properties.GetPropertyAsValueType(4);
      set => this.Properties.SetProperty(4, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(5)]
    [Description("Gets or sets whether the item can be vertical stretched.")]
    public virtual bool StretchVertical
    {
      get => (bool) this.Properties.GetPropertyAsValueType(5);
      set => this.Properties.SetProperty(5, (object) value);
    }

    [QPropertyIndex(6)]
    [Category("QAppearance")]
    [Description("Gets or sets the alignment of this item horizontally.")]
    public virtual QPartAlignment AlignmentHorizontal
    {
      get => (QPartAlignment) this.Properties.GetPropertyAsValueType(6);
      set => this.Properties.SetProperty(6, (object) value);
    }

    [QPropertyIndex(7)]
    [Category("QAppearance")]
    [Description("Gets or sets the alignment of this item vertically.")]
    public virtual QPartAlignment AlignmentVertical
    {
      get => (QPartAlignment) this.Properties.GetPropertyAsValueType(7);
      set => this.Properties.SetProperty(7, (object) value);
    }

    [Category("QAppearance")]
    [Description("Gets or sets the alignment of the content of this item horizontally.")]
    [QPropertyIndex(13)]
    public virtual QPartAlignment ContentAlignmentHorizontal
    {
      get => (QPartAlignment) this.Properties.GetPropertyAsValueType(13);
      set => this.Properties.SetProperty(13, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(14)]
    [Description("Gets or sets the alignment of the content of this item vertically.")]
    public virtual QPartAlignment ContentAlignmentVertical
    {
      get => (QPartAlignment) this.Properties.GetPropertyAsValueType(14);
      set => this.Properties.SetProperty(14, (object) value);
    }

    [Category("QAppearance")]
    [QPropertyIndex(8)]
    [Description("Gets or sets the minimum size of the item.")]
    public virtual Size MinimumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(8);
      set => this.Properties.SetProperty(8, (object) value);
    }

    [Description("Gets or sets the minimum size of the item.")]
    [QPropertyIndex(9)]
    [Category("QAppearance")]
    public virtual Size MaximumSize
    {
      get => (Size) this.Properties.GetPropertyAsValueType(9);
      set => this.Properties.SetProperty(9, (object) value);
    }
  }
}
