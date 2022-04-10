// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeGroup
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeGroupDesigner), typeof (IDesigner))]
  public class QCompositeGroup : QCompositeItemBase, IQScrollablePart, IQPart
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QScrollablePartData m_oScrollData;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private Region m_oSavedPaintRegion;

    protected QCompositeGroup(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
      this.InternalConstruct();
    }

    public QCompositeGroup(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct();
    }

    public QCompositeGroup()
      : this(QCompositeItemCreationOptions.Default)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct()
    {
    }

    protected virtual QScrollablePartData CreateScrollData() => new QScrollablePartData((IQScrollablePart) this, this.Configuration != null ? this.Configuration.ScrollConfiguration : (QCompositeScrollConfiguration) null);

    protected virtual void SynchronizeScrollData()
    {
      if (this.Configuration != null && this.Configuration.ScrollConfiguration != null && (this.Configuration.ScrollConfiguration.ScrollHorizontal != QCompositeScrollVisibility.None || this.Configuration.ScrollConfiguration.ScrollVertical != QCompositeScrollVisibility.None))
      {
        if (this.m_oScrollData != null)
          return;
        this.m_oScrollData = this.CreateScrollData();
      }
      else
        this.m_oScrollData = (QScrollablePartData) null;
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Background, (IQPartObjectPainter) new QPartShapePainter()
      {
        DrawOnBounds = QPartBoundsType.Bounds,
        Options = QPainterOptions.FillBackground,
        Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.ReturnDrawnShape | QShapePainterOptions.StayWithinBounds)
      });
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Foreground, (IQPartObjectPainter) new QPartShapePainter()
      {
        DrawOnBounds = QPartBoundsType.Bounds,
        Options = QPainterOptions.FillForeground,
        Properties = new QShapePainterProperties((Matrix) null, QShapePainterOptions.StayWithinBounds)
      });
      return currentPainters;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeGroupConfiguration();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeGroup)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeScroll)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QScrollablePartData ScrollData => this.m_oScrollData;

    public override IQPartLayoutEngine LayoutEngine
    {
      get
      {
        if (this.IsLayoutEngineSet)
          return base.LayoutEngine;
        if (this.Configuration.Layout == QCompositeItemLayout.Auto)
        {
          IQPart parentPart = this.ParentPart;
          return parentPart != null && parentPart.LayoutEngine is QPartTableLayoutEngine ? QPartTableRowLayoutEngine.Default : (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
        }
        if (this.Configuration.Layout == QCompositeItemLayout.Linear)
          return (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
        if (this.Configuration.Layout == QCompositeItemLayout.Flow)
          return (IQPartLayoutEngine) QPartFlowLayoutEngine.Default;
        return this.Configuration.Layout == QCompositeItemLayout.Table ? (IQPartLayoutEngine) QPartTableLayoutEngine.Default : base.LayoutEngine;
      }
      set => base.LayoutEngine = value;
    }

    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeGroupConfiguration Configuration
    {
      get => base.Configuration as QCompositeGroupConfiguration;
      set => this.Configuration = value;
    }

    [Browsable(true)]
    [Description("Gets the collection of CompositeItems of this QCompositeItem. These are the items that are displayed in this QCompositeItem.")]
    [Category("QBehavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor(typeof (QCompositeItemCollectionEditor), typeof (UITypeEditor))]
    public override QPartCollection Items => this.Parts;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected internal override QPartCollection DesignablePartsCollection => this.Items;

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return destinationObject == this && (QItemStatesHelper.IsDisabled(state) || QItemStatesHelper.IsNormal(state)) ? new QColorSet(this.RetrieveFirstDefinedColor("CompositeGroupBackground1"), this.RetrieveFirstDefinedColor("CompositeGroupBackground2"), this.RetrieveFirstDefinedColor("CompositeGroupBorder"), QItemStatesHelper.IsDisabled(state) ? this.RetrieveFirstDefinedColor("CompositeTextDisabled") : this.RetrieveFirstDefinedColor("CompositeText")) : base.GetItemColorSet(destinationObject, state, additionalProperties);
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
      if (part != this)
        return;
      if (layoutStage == QPartLayoutStage.PreparingForLayout)
        this.SynchronizeScrollData();
      if (this.m_oScrollData == null)
        return;
      this.m_oScrollData.HandleLayoutStage(layoutStage, layoutContext, additionalProperties);
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet1 = (QColorSet) null;
      if (part == this)
      {
        switch (paintStage)
        {
          case QPartPaintStage.PaintingBackground:
            if (this.Configuration.Appearance != null)
            {
              qcolorSet1 = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
              if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter1)
              {
                objectPainter1.Appearance = this.Configuration.Appearance;
                objectPainter1.ColorSet = qcolorSet1;
              }
              if (this.GetObjectPainter(QPartPaintLayer.Foreground, typeof (QPartShapePainter)) is QPartShapePainter objectPainter2)
              {
                objectPainter2.Appearance = this.Configuration.Appearance;
                objectPainter2.ColorSet = qcolorSet1;
                break;
              }
              break;
            }
            break;
          case QPartPaintStage.BackgroundPainted:
            if (this.GetObjectPainter(QPartPaintLayer.Background, typeof (QPartShapePainter)) is QPartShapePainter objectPainter)
              this.PutLastDrawnGraphicsPath(objectPainter.LastDrawnGraphicsPath);
            else
              this.PutLastDrawnGraphicsPath((GraphicsPath) null);
            this.m_oSavedPaintRegion = QPartHelper.AdjustPaintRegion((IQPart) this, this.LastDrawnGraphicsPath, paintContext);
            break;
          case QPartPaintStage.PaintingContent:
            if (this.m_oScrollData != null && this.m_oScrollData.ScrollOffset != Point.Empty)
            {
              paintContext.Graphics.TranslateTransform((float) this.m_oScrollData.ScrollOffset.X, (float) this.m_oScrollData.ScrollOffset.Y);
              break;
            }
            break;
          case QPartPaintStage.ContentPainted:
            if (this.m_oScrollData != null && this.m_oScrollData.ScrollOffset != Point.Empty)
            {
              paintContext.Graphics.TranslateTransform((float) -this.m_oScrollData.ScrollOffset.X, (float) -this.m_oScrollData.ScrollOffset.Y);
              break;
            }
            break;
          case QPartPaintStage.PaintingForeground:
            QPartHelper.RestorePaintRegion(this.m_oSavedPaintRegion, paintContext);
            this.m_oSavedPaintRegion = (Region) null;
            break;
          case QPartPaintStage.ForegroundPainted:
            if (this.m_oScrollData != null)
            {
              this.m_oScrollData.PaintScrollAreas(paintContext);
              break;
            }
            break;
          case QPartPaintStage.PaintFinished:
            this.PutContentClipRegion(QPartHelper.CreatePossibleContentClipRegion((IQPart) this, this.LastDrawnGraphicsPath));
            break;
        }
      }
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }

    protected internal override bool HandleMouseMove(MouseEventArgs e)
    {
      if (this.m_oScrollData != null)
        this.m_oScrollData.HandleMouseMove(e);
      return base.HandleMouseMove(e);
    }

    protected internal override bool HandleMouseDown(MouseEventArgs e)
    {
      if (this.m_oScrollData != null)
        this.m_oScrollData.HandleMouseDown(e);
      return base.HandleMouseDown(e);
    }

    protected internal override bool HandleMouseUp(MouseEventArgs e)
    {
      if (this.m_oScrollData != null)
        this.m_oScrollData.HandleMouseUp(e);
      return base.HandleMouseDown(e);
    }

    QScrollablePartData IQScrollablePart.ScrollData => this.m_oScrollData;

    void IQScrollablePart.CaptureMouse(QScrollablePartData scrollablePartData)
    {
      if (this.Composite == null)
        return;
      this.Composite.CaptureMouse((IQMouseHandler) scrollablePartData);
    }
  }
}
