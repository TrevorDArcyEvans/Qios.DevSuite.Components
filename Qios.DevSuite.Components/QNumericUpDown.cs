// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QNumericUpDown
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QNumericUpDown), "Resources.ControlImages.QNumericUpDown.bmp")]
  public class QNumericUpDown : QInputBox
  {
    protected override QInputBoxConfiguration CreateConfigurationInstance() => (QInputBoxConfiguration) new QNumericUpDownConfiguration();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QInputBoxCompositeWindow CustomDropDownWindow
    {
      get => base.CustomDropDownWindow;
      set => base.CustomDropDownWindow = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override object DataSource
    {
      get => base.DataSource;
      set => base.DataSource = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string DisplayMember
    {
      get => base.DisplayMember;
      set => base.DisplayMember = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override string ValueMember
    {
      get => base.ValueMember;
      set => base.ValueMember = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxObjectCollection Items => base.Items;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool Sorted
    {
      get => base.Sorted;
      set => base.Sorted = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override int SelectedIndex
    {
      get => base.SelectedIndex;
      set => base.SelectedIndex = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override object SelectedValue
    {
      get => base.SelectedValue;
      set => base.SelectedValue = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override object SelectedItem
    {
      get => base.SelectedItem;
      set => base.SelectedItem = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QFloatingInputBoxWindowConfiguration ChildWindowConfiguration => base.ChildWindowConfiguration;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QFloatingInputBoxWindowCompositeConfiguration ChildCompositeConfiguration => base.ChildCompositeConfiguration;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QColorScheme ChildCompositeColorScheme => base.ChildCompositeColorScheme;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QCompositeItemConfiguration ItemConfiguration => base.ItemConfiguration;
  }
}
