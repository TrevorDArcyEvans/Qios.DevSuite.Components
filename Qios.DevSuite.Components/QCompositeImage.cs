// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeImage
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeImageDesigner), typeof (IDesigner))]
  public class QCompositeImage : QCompositeItemBase
  {
    private QImageContainer m_oImageContainer;

    protected QCompositeImage(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeImage()
      : base(QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct((Image) null);
    }

    internal QCompositeImage(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct((Image) null);
    }

    private void InternalConstruct(Image defaultImage)
    {
      if (this.m_oImageContainer != null)
        return;
      this.m_oImageContainer = new QImageContainer(defaultImage);
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = base.CreatePainters(currentPainters);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Content, (IQPartObjectPainter) new QPartImagePainter()
      {
        ColorToReplace = Color.FromArgb((int) byte.MaxValue, 0, 0)
      });
      return currentPainters;
    }

    internal QImageContainer ImageContainer => this.m_oImageContainer;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    public QCompositeImageConfiguration Configuration
    {
      get => base.Configuration as QCompositeImageConfiguration;
      set => this.Configuration = value;
    }

    public bool ShouldSerializeImage() => this.m_oImageContainer.ShouldSerializeImage();

    public void ResetImage() => this.m_oImageContainer.ResetImage();

    [Category("QAppearance")]
    [QXmlSave(QXmlSaveType.NeverSave)]
    [Description("Gets or sets the Image of the QCompositeImage")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Image Image
    {
      get => this.m_oImageContainer.Image;
      set => this.SetImage(value, true);
    }

    public void SetImage(Image image, bool notifyParent)
    {
      this.m_oImageContainer.Image = image;
      if (!notifyParent)
        return;
      this.HandleChange(true);
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a possible resource name to load the Image from. This must be in the format 'NameSpace.ImageName.ico, AssemblyName'")]
    [DefaultValue(null)]
    [Category("QAppearance")]
    public string ImageResourceName
    {
      get => this.m_oImageContainer.ResourceName;
      set
      {
        this.m_oImageContainer.ResourceName = value;
        this.HandleChange(true);
      }
    }

    [Browsable(false)]
    [Description("Gets whether the Image is loaded from a resource")]
    public bool ImageLoadedFromResource => this.m_oImageContainer.LoadedFromResource;

    [Browsable(false)]
    public Image UsedImage => this.m_oImageContainer.UsedImage;

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeImageConfiguration();

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this || layoutStage != QPartLayoutStage.CalculatingSize)
        return;
      this.PutContentObject((object) this.UsedImage);
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
        QPartImagePainter objectPainter = part.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartImagePainter)) as QPartImagePainter;
        objectPainter.DrawDisabled = QItemStatesHelper.IsDisabled(this.ItemState);
        objectPainter.ColorToReplaceWith = qcolorSet1.Foreground;
      }
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }
  }
}
