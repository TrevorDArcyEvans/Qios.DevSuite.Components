// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarShowMoreItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonLaunchBarShowMoreItem : QRibbonLaunchBarItem
  {
    public QRibbonLaunchBarShowMoreItemConfiguration Configuration
    {
      get => base.Configuration as QRibbonLaunchBarShowMoreItemConfiguration;
      set => this.Configuration = (QRibbonLaunchBarItemConfiguration) value;
    }

    public override bool CanExpand => true;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override QCompositeWindow CustomChildWindow
    {
      get => (QCompositeWindow) null;
      set
      {
      }
    }

    protected internal override void ConfigureChildWindow()
    {
      base.ConfigureChildWindow();
      this.ChildWindow.SuspendChangeNotification();
      this.ChildComposite.ColorHost = (IQItemColorHost) new QRibbonLaunchBarShowMoreItem.QRibbonLaunchBarMoreItemCompositeColorHost(this.LaunchBar, this.ChildComposite);
      this.ChildWindow.Configuration = this.Configuration.ChildWindowConfiguration;
      this.ChildComposite.Configuration = this.Configuration.ChildCompositeConfiguration;
      this.ChildWindow.ResumeChangeNotification(false);
      this.ChildItems.SuspendChangeNotification();
      this.ChildItems.AllowAddRemoveSystemParts = true;
      this.ChildItems.Clear();
      QRibbonLaunchBar launchBar = this.LaunchBar;
      for (int index = 0; index < launchBar.Parts.Count; ++index)
      {
        QCompositeItemBase part1 = launchBar.Parts[index] as QCompositeItemBase;
        if (part1.IsVisible(~QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints) && !part1.IsVisible(QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints))
        {
          QCompositeItemBase part2 = part1.Clone() as QCompositeItemBase;
          part2.HiddenBecauseOfConstraints = false;
          this.ChildItems.Add((IQPart) part2, false);
          part1.MoveUnclonablesToClone();
        }
      }
      this.ChildItems.AllowAddRemoveSystemParts = false;
      this.ChildItems.ResumeChangeNotification(false);
    }

    protected override void OnItemCollapsed(QCompositeEventArgs e)
    {
      if (e.Item == this)
      {
        QRibbonLaunchBar launchBar = this.LaunchBar;
        for (int index = 0; index < launchBar.Parts.Count; ++index)
        {
          if (launchBar.Parts[index] is QCompositeItemBase part && part.LastClonedItem != null)
          {
            part.RestoreUnclonablesFromClone();
            part.RemoveCloneLink();
          }
        }
      }
      base.OnItemCollapsed(e);
    }

    public class QRibbonLaunchBarMoreItemCompositeColorHost : IQItemColorHost
    {
      private QComposite m_oComposite;
      private QRibbonLaunchBar m_oLaunchBar;

      public QRibbonLaunchBarMoreItemCompositeColorHost(
        QRibbonLaunchBar launchBar,
        QComposite composite)
      {
        this.m_oLaunchBar = launchBar;
        this.m_oComposite = composite;
      }

      public QColorSet GetItemColorSet(
        object destinationObject,
        QItemStates state,
        object additionalProperties)
      {
        return destinationObject == this.m_oComposite ? new QColorSet(this.m_oLaunchBar.RetrieveFirstDefinedColor("RibbonLaunchBarHostBackground1"), this.m_oLaunchBar.RetrieveFirstDefinedColor("RibbonLaunchBarHostBackground2"), this.m_oLaunchBar.RetrieveFirstDefinedColor("RibbonLaunchBarHostBorder")) : this.m_oComposite.GetItemColorSet(destinationObject, state, additionalProperties);
      }
    }
  }
}
