// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeButton
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components
{
  public class QCompositeButton : QCompositeMenuItem
  {
    protected QCompositeButton(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
    }

    public QCompositeButton()
    {
    }

    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeButton)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeButtonConfiguration();

    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeButtonConfiguration Configuration
    {
      get => base.Configuration as QCompositeButtonConfiguration;
      set => this.Configuration = (QCompositeMenuItemConfiguration) value;
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      if (destinationObject == this)
      {
        if (QItemStatesHelper.IsDisabled(state))
          return new QColorSet(this.RetrieveFirstDefinedColor("CompositeButtonDisabledBackground1"), this.RetrieveFirstDefinedColor("CompositeButtonDisabledBackground2"), this.RetrieveFirstDefinedColor("CompositeButtonDisabledBorder"));
        if (QItemStatesHelper.IsNormal(state) || QItemStatesHelper.IsExpanded(state) && this.Composite.SelectedItem != null && this.Composite.SelectedItem != this && !this.Composite.PaintExpandedChildWhenHot((object) this))
          return new QColorSet(this.RetrieveFirstDefinedColor("CompositeButtonBackground1"), this.RetrieveFirstDefinedColor("CompositeButtonBackground2"), this.RetrieveFirstDefinedColor("CompositeButtonBorder"));
      }
      return base.GetItemColorSet(destinationObject, state, additionalProperties);
    }
  }
}
