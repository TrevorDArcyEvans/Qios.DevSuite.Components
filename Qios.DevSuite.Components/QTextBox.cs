// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTextBox
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QTextBox), "Resources.ControlImages.QTextBox.bmp")]
  public class QTextBox : QInputBox
  {
    protected override QInputBoxConfiguration CreateConfigurationInstance() => (QInputBoxConfiguration) new QTextBoxConfiguration();

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.InputBox)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxAccelerationCollection Accelerations => base.Accelerations;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Decimal NumericValue
    {
      get => base.NumericValue;
      set => base.NumericValue = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Decimal Increment
    {
      get => base.Increment;
      set => base.Increment = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Decimal MaximumValue
    {
      get => base.MaximumValue;
      set => base.MaximumValue = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override Decimal MinimumValue
    {
      get => base.MinimumValue;
      set => base.MinimumValue = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string FormatString
    {
      get => base.FormatString;
      set => base.FormatString = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QInputBoxCompositeWindow CustomDropDownWindow
    {
      get => base.CustomDropDownWindow;
      set => base.CustomDropDownWindow = value;
    }

    [Category("Autocomplete")]
    public override object DataSource
    {
      get => base.DataSource;
      set => base.DataSource = value;
    }

    [Category("Autocomplete")]
    public override string DisplayMember
    {
      get => base.DisplayMember;
      set => base.DisplayMember = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override string ValueMember
    {
      get => base.ValueMember;
      set => base.ValueMember = value;
    }

    [Category("Autocomplete")]
    public override QInputBoxObjectCollection Items => base.Items;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool Sorted
    {
      get => base.Sorted;
      set => base.Sorted = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
    public override QColorScheme ChildCompositeColorScheme => base.ChildCompositeColorScheme;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QFloatingInputBoxWindowCompositeConfiguration ChildCompositeConfiguration => base.ChildCompositeConfiguration;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QCompositeItemConfiguration ItemConfiguration => base.ItemConfiguration;
  }
}
