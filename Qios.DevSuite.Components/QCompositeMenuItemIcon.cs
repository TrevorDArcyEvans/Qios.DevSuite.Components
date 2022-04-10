// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeMenuItemIcon
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;

namespace Qios.DevSuite.Components
{
  [Designer(typeof (QCompositeIconDesigner), typeof (IDesigner))]
  public class QCompositeMenuItemIcon : QCompositeIcon
  {
    private bool m_bChecked;
    private QIconContainer m_oCheckedIconContainer;

    protected QCompositeMenuItemIcon(object sourceObject, QObjectClonerConstructOptions options)
      : this(QCompositeItemCreationOptions.None)
    {
    }

    public QCompositeMenuItemIcon(QCompositeItemCreationOptions options)
      : base(options)
    {
      this.InternalConstruct();
    }

    private void InternalConstruct() => this.m_oCheckedIconContainer = new QIconContainer(new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Qios.DevSuite.Components.Resources.Icons.Checked.ico")));

    internal bool Checked
    {
      get => this.m_bChecked;
      set => this.m_bChecked = value;
    }

    internal QIconContainer CheckedIconContainer => this.m_oCheckedIconContainer;

    public bool ShouldSerializeCheckedIcon() => this.m_oCheckedIconContainer.ShouldSerializeIcon();

    public void ResetCheckedIcon() => this.m_oCheckedIconContainer.ResetIcon();

    [Category("QAppearance")]
    [QXmlSave(QXmlSaveType.NeverSave)]
    [Description("Gets or sets the Icon of the QCompositeIcon")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Icon CheckedIcon
    {
      get => this.m_oCheckedIconContainer.Icon;
      set
      {
        this.m_oCheckedIconContainer.Icon = value;
        this.HandleChange(true);
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets a possible resource name to load the Icon from. This must be in the format 'NameSpace.IconName.ico, AssemblyName'")]
    [Category("QAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string CheckedIconResourceName
    {
      get => this.m_oCheckedIconContainer.ResourceName;
      set
      {
        this.m_oCheckedIconContainer.ResourceName = value;
        this.HandleChange(true);
      }
    }

    [Description("Gets whether the Icon is loaded from a resource")]
    [Browsable(false)]
    public bool CheckedIconLoadedFromResource => this.m_oCheckedIconContainer.LoadedFromResource;

    [Browsable(false)]
    public override Icon UsedIcon
    {
      get
      {
        if (QItemStatesHelper.IsDisabled(this.ItemState) && this.DisabledIconContainer.UsedIcon != null)
          return this.DisabledIconContainer.UsedIcon;
        if (this.IconContainer.UsedIcon != null)
          return this.IconContainer.UsedIcon;
        return this.m_bChecked && this.m_oCheckedIconContainer.UsedIcon != null ? this.m_oCheckedIconContainer.UsedIcon : (Icon) null;
      }
    }

    protected override void HandleLayoutStage(
      IQPart part,
      QPartLayoutStage layoutStage,
      QPartLayoutContext layoutContext,
      QPartLayoutStageProperties additionalProperties)
    {
      if (part == this && layoutStage == QPartLayoutStage.CalculatingSize)
        this.m_oCheckedIconContainer.CalculateIconSize(this.Configuration.IconSize);
      base.HandleLayoutStage(part, layoutStage, layoutContext, additionalProperties);
    }
  }
}
