// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeSeparator
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QCompositeSeparator : QCompositeItemBase
  {
    private object m_oContentObject;

    protected QCompositeSeparator(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeSeparator()
      : base(QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct();
    }

    internal QCompositeSeparator(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct() => this.AutoSize = false;

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = base.CreatePainters(currentPainters);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Content, (IQPartObjectPainter) new QPartSeparatorPainter());
      return currentPainters;
    }

    public override IQPartLayoutEngine LayoutEngine
    {
      get
      {
        if (this.IsLayoutEngineSet)
          return base.LayoutEngine;
        IQPart parentPart = this.ParentPart;
        return parentPart != null && parentPart.LayoutEngine is QPartTableLayoutEngine ? QPartTableRowLayoutEngine.Default : (IQPartLayoutEngine) QPartLinearLayoutEngine.Default;
      }
      set => base.LayoutEngine = value;
    }

    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public QCompositeSeparatorConfiguration Configuration
    {
      get => base.Configuration as QCompositeSeparatorConfiguration;
      set => this.Configuration = (QContentPartConfiguration) value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the QColorScheme that is used")]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeSeparator)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeSeparatorConfiguration();

    internal bool AutoSize
    {
      get => (this.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartSeparatorPainter)) as QPartSeparatorPainter).AutoSize;
      set => (this.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartSeparatorPainter)) as QPartSeparatorPainter).AutoSize = value;
    }

    [Browsable(false)]
    public virtual Image UsedImage => this.Configuration.UsedMask;

    public override object ContentObject => this.m_oContentObject;

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      return new QColorSet()
      {
        Border = this.RetrieveFirstDefinedColor("CompositeSeparator1"),
        Background1 = this.RetrieveFirstDefinedColor("CompositeSeparator2")
      };
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this)
        return;
      if (layoutStage == QPartLayoutStage.CalculatingSize)
      {
        QPartDirection qpartDirection = QPartDirection.Horizontal;
        IQPart parentPart = this.ParentPart;
        if (parentPart != null)
          qpartDirection = parentPart.Properties.GetDirection(parentPart);
        if (qpartDirection == QPartDirection.Horizontal)
          this.m_oContentObject = (object) new Size(this.UsedImage.Height, 0);
        else
          this.m_oContentObject = (object) new Size(0, this.UsedImage.Height);
      }
    }

    protected override QColorSet HandlePaintStage(
      IQPart part,
      QPartPaintStage paintStage,
      QPartPaintContext paintContext)
    {
      QColorSet qcolorSet1 = (QColorSet) null;
      if (part == this && paintStage == QPartPaintStage.PaintingContent)
      {
        qcolorSet1 = this.ColorHost.GetItemColorSet((object) this, this.ItemState, (object) null);
        QPartSeparatorPainter objectPainter = this.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartSeparatorPainter)) as QPartSeparatorPainter;
        objectPainter.Mask = this.UsedImage;
        objectPainter.ColorSet = qcolorSet1;
      }
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }
  }
}
