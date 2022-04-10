// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItemGroupBase
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonGroupDesigner), typeof (IDesigner))]
  public abstract class QRibbonItemGroupBase : QCompositeGroup
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonPanel m_oCachedRibbonPanel;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonLaunchBar m_oCachedLaunchBar;

    internal QRibbonItemGroupBase(QCompositeItemCreationOptions options)
      : base(options)
    {
    }

    [Category("QBehavior")]
    [Editor(typeof (QRibbonItemCollectionEditor), typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection of QRibbonItems of this QRibbonGroup")]
    public override QPartCollection Items => base.Items;

    [Browsable(false)]
    public QRibbonPanel RibbonPanel
    {
      get
      {
        if (this.m_oCachedRibbonPanel == null)
          this.m_oCachedRibbonPanel = QRibbonPanel.FindRibbonPanel((IQPart) this);
        return this.m_oCachedRibbonPanel;
      }
    }

    [Browsable(false)]
    public QRibbonPageComposite RibbonPageComposite => this.Composite as QRibbonPageComposite;

    [Browsable(false)]
    public QRibbonLaunchBar LaunchBar
    {
      get
      {
        if (this.m_oCachedLaunchBar == null)
          this.m_oCachedLaunchBar = QRibbonLaunchBar.FindLaunchBar((IQPart) this);
        return this.m_oCachedLaunchBar;
      }
    }

    protected override void ClearCachedParents()
    {
      base.ClearCachedParents();
      this.m_oCachedRibbonPanel = (QRibbonPanel) null;
      this.m_oCachedLaunchBar = (QRibbonLaunchBar) null;
    }

    protected override void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      base.SetDisplayParent(displayParent);
      this.SetConfigurationBaseProperties(false);
    }

    protected virtual void SetConfigurationBaseProperties(bool raiseEvent)
    {
    }
  }
}
