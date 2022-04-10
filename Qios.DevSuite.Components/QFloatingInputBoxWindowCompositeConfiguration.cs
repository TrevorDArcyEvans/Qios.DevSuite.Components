// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingInputBoxWindowCompositeConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QFloatingInputBoxWindowCompositeConfiguration : QCompositeConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 29;

    public QFloatingInputBoxWindowCompositeConfiguration()
    {
      this.Properties.DefineProperty(11, (object) QPartDirection.Vertical);
      this.Properties.DefineProperty(9, (object) new Size(0, 200));
    }

    protected override QShapeAppearance CreateAppearance() => (QShapeAppearance) new QFloatingInputBoxWindowCompositeAppearance();

    protected override QCompositeScrollConfiguration CreateScrollConfiguration() => (QCompositeScrollConfiguration) new QFloatingInputBoxWindowCompositeScrollConfiguration();

    [Browsable(false)]
    public override QPartAlignment AlignmentHorizontal
    {
      get => base.AlignmentHorizontal;
      set => base.AlignmentHorizontal = value;
    }

    [Browsable(false)]
    public override QPartAlignment AlignmentVertical
    {
      get => base.AlignmentVertical;
      set => base.AlignmentVertical = value;
    }

    [Browsable(false)]
    public override QCompositeExpandBehavior ExpandBehavior
    {
      get => base.ExpandBehavior;
      set => base.ExpandBehavior = value;
    }

    [Browsable(false)]
    public override QCompositeExpandDirection ExpandDirection
    {
      get => base.ExpandDirection;
      set => base.ExpandDirection = value;
    }

    [Browsable(false)]
    public override int ExpandingDelay
    {
      get => base.ExpandingDelay;
      set => base.ExpandingDelay = value;
    }

    [Browsable(false)]
    public override QHotkeyVisibilityType HotkeyPrefixVisibilityType
    {
      get => base.HotkeyPrefixVisibilityType;
      set => base.HotkeyPrefixVisibilityType = value;
    }

    [Browsable(false)]
    public override QHotkeyWindowConfiguration HotkeyWindowConfiguration => base.HotkeyWindowConfiguration;

    [Browsable(false)]
    public override QHotkeyWindowShowBehavior HotkeyWindowShowBehavior
    {
      get => base.HotkeyWindowShowBehavior;
      set => base.HotkeyWindowShowBehavior = value;
    }

    [Browsable(false)]
    public override QMargin IconBackgroundMargin
    {
      get => base.IconBackgroundMargin;
      set => base.IconBackgroundMargin = value;
    }

    [Browsable(false)]
    public override int IconBackgroundSize
    {
      get => base.IconBackgroundSize;
      set => base.IconBackgroundSize = value;
    }

    [Browsable(false)]
    public override bool IconBackgroundVisible
    {
      get => base.IconBackgroundVisible;
      set => base.IconBackgroundVisible = value;
    }

    [Browsable(false)]
    public override bool InheritWindowsSettings
    {
      get => base.InheritWindowsSettings;
      set => base.InheritWindowsSettings = value;
    }

    [Browsable(false)]
    public override QCompositeItemLayout Layout
    {
      get => base.Layout;
      set => base.Layout = value;
    }

    [Browsable(false)]
    public override QMargin Margin
    {
      get => base.Margin;
      set => base.Margin = value;
    }

    [Browsable(false)]
    public override bool ShrinkHorizontal
    {
      get => base.ShrinkHorizontal;
      set => base.ShrinkHorizontal = value;
    }

    [Browsable(false)]
    public override bool ShrinkVertical
    {
      get => base.ShrinkVertical;
      set => base.ShrinkVertical = value;
    }

    [Browsable(false)]
    public override bool StretchHorizontal
    {
      get => base.StretchHorizontal;
      set => base.StretchHorizontal = value;
    }

    [Browsable(false)]
    public override bool StretchVertical
    {
      get => base.StretchVertical;
      set => base.StretchVertical = value;
    }

    [Browsable(false)]
    public override QTristateBool Visible
    {
      get => base.Visible;
      set => base.Visible = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Size MinimumSize
    {
      get => base.MinimumSize;
      set => base.MinimumSize = value;
    }

    protected override int GetRequestedCount() => 29;
  }
}
