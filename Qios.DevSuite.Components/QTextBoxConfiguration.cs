// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTextBoxConfiguration
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QTextBoxConfiguration : QInputBoxConfiguration
  {
    protected new const int CurrentPropertyCount = 0;
    protected new const int TotalPropertyCount = 18;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxButtonShapeAppearance ButtonAppearance
    {
      get => base.ButtonAppearance;
      set => base.ButtonAppearance = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QInputBoxButtonShapeAppearance ButtonAppearanceHot
    {
      get => base.ButtonAppearanceHot;
      set => base.ButtonAppearanceHot = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxButtonShapeAppearance ButtonAppearancePressed
    {
      get => base.ButtonAppearancePressed;
      set => base.ButtonAppearancePressed = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxButtonAlign ButtonAlign
    {
      get => base.ButtonAlign;
      set => base.ButtonAlign = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override QInputBoxButtonDrawType InputBoxButtonDrawFocused
    {
      get => base.InputBoxButtonDrawFocused;
      set => base.InputBoxButtonDrawFocused = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxButtonDrawType InputBoxButtonDrawHot
    {
      get => base.InputBoxButtonDrawHot;
      set => base.InputBoxButtonDrawHot = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QInputBoxButtonDrawType InputBoxButtonDrawNormal
    {
      get => base.InputBoxButtonDrawNormal;
      set => base.InputBoxButtonDrawNormal = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QPadding ButtonPadding
    {
      get => base.ButtonPadding;
      set => base.ButtonPadding = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override QMargin ButtonMargin
    {
      get => base.ButtonMargin;
      set => base.ButtonMargin = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Image ButtonMask
    {
      get => base.ButtonMask;
      set => base.ButtonMask = value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QImageAlign ButtonMaskAlign
    {
      get => base.ButtonMaskAlign;
      set => base.ButtonMaskAlign = value;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override QInputBoxStyle InputStyle
    {
      get => base.InputStyle;
      set => base.InputStyle = value;
    }

    protected override int GetRequestedCount() => 18;
  }
}
