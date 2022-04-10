// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonCaptionButton
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonCaptionButton : QCompositeItem
  {
    internal QRibbonCaptionButton()
      : base(QCompositeItemCreationOptions.None)
    {
      this.SetObjectPainter(QPartPaintLayer.Content, (IQPartObjectPainter) new QPartImagePainter());
    }

    public QRibbonCaptionButtonConfiguration Configuration
    {
      get => base.Configuration as QRibbonCaptionButtonConfiguration;
      set => this.Configuration = value;
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      QColorSet itemColorSet = !QItemStatesHelper.IsDisabled(state) ? (!QItemStatesHelper.IsPressed(state) ? (!QItemStatesHelper.IsHot(state) ? (!QItemStatesHelper.IsNormal(state) ? base.GetItemColorSet(destinationObject, state, additionalProperties) : new QColorSet(this.RetrieveFirstDefinedColor("RibbonCaptionButtonBackground1"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonBackground2"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonBorder"))) : new QColorSet(this.RetrieveFirstDefinedColor("RibbonCaptionButtonHotBackground1"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonHotBackground2"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonHotBorder"))) : new QColorSet(this.RetrieveFirstDefinedColor("RibbonCaptionButtonPressedBackground1"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonPressedBackground2"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonPressedBorder"))) : new QColorSet(this.RetrieveFirstDefinedColor("RibbonCaptionButtonBackground1"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonBackground2"), this.RetrieveFirstDefinedColor("RibbonCaptionButtonBorder"));
      if (itemColorSet != null && this.ParentControl is QRibbonCaption parentControl)
        itemColorSet.Foreground = !parentControl.Active || QItemStatesHelper.IsDisabled(state) ? this.RetrieveFirstDefinedColor("RibbonCaptionButtonMaskInactive") : this.RetrieveFirstDefinedColor("RibbonCaptionButtonMask");
      return itemColorSet;
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (layoutStage == QPartLayoutStage.CalculatingSize && part == this)
        this.PutContentObject((object) this.Configuration.UsedButtonMask);
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet = base.HandlePaintStage(part, paintStage, paintContext);
      if (paintStage == QPartPaintStage.PaintingBackground && part == this)
      {
        QPartImagePainter objectPainter = (QPartImagePainter) this.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartImagePainter));
        objectPainter.ColorToReplace = Color.Red;
        objectPainter.ColorToReplaceWith = qcolorSet.Foreground;
      }
      return qcolorSet;
    }
  }
}
