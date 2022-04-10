// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarItem : QCompositeItem
  {
    public QRibbonLaunchBarItem()
      : base(QCompositeItemCreationOptions.CreateChildItemsCollection)
    {
      this.SetObjectPainter(QPartPaintLayer.Content, (IQPartObjectPainter) new QPartImagePainter());
    }

    public QRibbonLaunchBarItemConfiguration Configuration
    {
      get => base.Configuration as QRibbonLaunchBarItemConfiguration;
      set => this.Configuration = value;
    }

    [Browsable(false)]
    public QRibbonLaunchBar LaunchBar => QRibbonLaunchBar.FindLaunchBar((IQPart) this);

    public override Color RetrieveFirstDefinedColor(string colorName) => this.LaunchBar != null ? this.LaunchBar.RetrieveFirstDefinedColor(colorName) : base.RetrieveFirstDefinedColor(colorName);

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return QRibbonItem.GetDefaultRibbonItemColorSet(destinationObject, state, this.Composite, (IQColorRetriever) this);
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
