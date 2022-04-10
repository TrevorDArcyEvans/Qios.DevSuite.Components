// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeIcon
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeIconDesigner), typeof (IDesigner))]
  public class QCompositeIcon : QCompositeItemBase
  {
    private QIconContainer m_oIconContainer;
    private QIconContainer m_oDisabledIconContainer;

    protected QCompositeIcon(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeIcon()
      : base(QCompositeItemCreationOptions.CreateColorScheme | QCompositeItemCreationOptions.CreateConfiguration)
    {
      this.InternalConstruct((Icon) null, (Icon) null);
    }

    internal QCompositeIcon(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct((Icon) null, (Icon) null);
    }

    private void InternalConstruct(Icon defaultIcon, Icon defaultDisabledIcon)
    {
      if (this.m_oIconContainer == null)
        this.m_oIconContainer = new QIconContainer(defaultIcon);
      if (this.m_oDisabledIconContainer != null)
        return;
      this.m_oDisabledIconContainer = new QIconContainer(defaultDisabledIcon);
    }

    protected override IQPartObjectPainter[] CreatePainters(
      IQPartObjectPainter[] currentPainters)
    {
      currentPainters = base.CreatePainters(currentPainters);
      currentPainters = QPartObjectPainter.SetObjectPainter(currentPainters, QPartPaintLayer.Content, (IQPartObjectPainter) new QPartIconPainter()
      {
        ColorToReplace = Color.FromArgb((int) byte.MaxValue, 0, 0)
      });
      return currentPainters;
    }

    internal QIconContainer IconContainer => this.m_oIconContainer;

    internal QIconContainer DisabledIconContainer => this.m_oDisabledIconContainer;

    [Description("Gets or sets the QColorScheme that is used")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeText)]
    [Category("QAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Contains the Configuration.")]
    [Category("QAppearance")]
    public QCompositeIconConfiguration Configuration
    {
      get => base.Configuration as QCompositeIconConfiguration;
      set => this.Configuration = value;
    }

    public bool ShouldSerializeIcon() => this.m_oIconContainer.ShouldSerializeIcon();

    public void ResetIcon()
    {
      this.m_oIconContainer.ResetIcon();
      this.HandleChange(true);
    }

    [QXmlSave(QXmlSaveType.NeverSave)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the Icon of the QCompositeIcon")]
    public Icon Icon
    {
      get => this.m_oIconContainer.Icon;
      set => this.SetIcon(value, true);
    }

    public void SetIcon(Icon icon, bool notifyParent)
    {
      this.m_oIconContainer.Icon = icon;
      if (!notifyParent)
        return;
      this.HandleChange(true);
    }

    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("QAppearance")]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    public string IconResourceName
    {
      get => this.m_oIconContainer.ResourceName;
      set
      {
        this.m_oIconContainer.ResourceName = value;
        this.HandleChange(true);
      }
    }

    [Browsable(false)]
    [Description("Gets whether the Icon is loaded from a resource")]
    public bool IconLoadedFromResource => this.m_oIconContainer.LoadedFromResource;

    public bool ShouldSerializeDisabledIcon() => this.m_oDisabledIconContainer.ShouldSerializeIcon();

    public void ResetDisabledIcon()
    {
      this.m_oDisabledIconContainer.ResetIcon();
      this.HandleChange(true);
    }

    [Description("Gets or sets the Icon of the QCompositeIcon")]
    [QXmlSave(QXmlSaveType.NeverSave)]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Icon DisabledIcon
    {
      get => this.m_oDisabledIconContainer.Icon;
      set
      {
        this.m_oDisabledIconContainer.Icon = value;
        this.HandleChange(true);
      }
    }

    [Category("QAppearance")]
    [DefaultValue(null)]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string DisabledIconResourceName
    {
      get => this.m_oDisabledIconContainer.ResourceName;
      set
      {
        this.m_oDisabledIconContainer.ResourceName = value;
        this.HandleChange(true);
      }
    }

    [Description("Gets whether the Icon is loaded from a resource")]
    [Browsable(false)]
    public bool DisabledIconLoadedFromResource => this.m_oDisabledIconContainer.LoadedFromResource;

    [Browsable(false)]
    public virtual Icon UsedIcon => !this.IsEnabled && this.m_oDisabledIconContainer.UsedIcon != null ? this.m_oDisabledIconContainer.UsedIcon : this.m_oIconContainer.UsedIcon;

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QCompositeIconConfiguration();

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part != this || layoutStage != QPartLayoutStage.CalculatingSize)
        return;
      this.m_oIconContainer.CalculateIconSize(this.Configuration.IconSize);
      this.m_oDisabledIconContainer.CalculateIconSize(this.Configuration.IconSize);
      this.PutContentObject((object) new QPartSizedContentObject((object) this.UsedIcon, this.Configuration.IconSize));
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
        QPartIconPainter objectPainter = part.GetObjectPainter(QPartPaintLayer.Content, typeof (QPartIconPainter)) as QPartIconPainter;
        objectPainter.DrawDisabled = QItemStatesHelper.IsDisabled(this.ItemState);
        objectPainter.ColorToReplaceWith = qcolorSet1.Foreground;
      }
      QColorSet qcolorSet2 = base.HandlePaintStage(part, paintStage, paintContext);
      return qcolorSet1 ?? qcolorSet2;
    }
  }
}
