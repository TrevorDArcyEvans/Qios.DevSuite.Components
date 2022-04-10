// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Qios.DevSuite.Components.Ribbon
{
  [Designer(typeof (QRibbonItemDesigner), typeof (IDesigner))]
  public class QRibbonItem : QCompositeMenuItem
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonPanel m_oCachedRibbonPanel;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonItemBar m_oCachedRibbonItemBar;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonLaunchBar m_oCachedLaunchBar;

    protected QRibbonItem(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
      this.InternalConstruct();
    }

    public QRibbonItem() => this.InternalConstruct();

    private void InternalConstruct()
    {
    }

    [Browsable(false)]
    public QRibbonItemBar RibbonItemBar
    {
      get
      {
        if (this.m_oCachedRibbonItemBar == null)
          this.m_oCachedRibbonItemBar = QRibbonItemBar.FindItemBar((IQPart) this);
        return this.m_oCachedRibbonItemBar;
      }
    }

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

    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [Description("Gets or sets the QColorScheme that is used")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    public static QColorSet GetDefaultRibbonItemColorSet(
      object item,
      QItemStates state,
      QComposite parentComposite,
      IQColorRetriever retriever)
    {
      QColorSet ribbonItemColorSet = new QColorSet();
      if (QItemStatesHelper.IsDisabled(state))
      {
        ribbonItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemDisabledBackground1");
        ribbonItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemDisabledBackground2");
        ribbonItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemDisabledBorder");
        ribbonItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemTextDisabled");
      }
      else if (QItemStatesHelper.IsChecked(state) && QItemStatesHelper.IsHot(state))
      {
        float multiplyBrightness = 1.1f;
        ribbonItemColorSet.Background1 = QMath.ModifyColor(retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground1"), 1f, multiplyBrightness, 1f);
        ribbonItemColorSet.Background2 = QMath.ModifyColor(retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground2"), 1f, multiplyBrightness, 1f);
        ribbonItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBorder");
        ribbonItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemActiveText");
      }
      else if (QItemStatesHelper.IsPressed(state) || QItemStatesHelper.IsChecked(state))
      {
        ribbonItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground1");
        ribbonItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground2");
        ribbonItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBorder");
        ribbonItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemActiveText");
      }
      else if (QItemStatesHelper.IsExpanded(state) && parentComposite.PaintExpandedItem(item))
      {
        ribbonItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground1");
        ribbonItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground2");
        ribbonItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBorder");
        ribbonItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemActiveText");
      }
      else if (QItemStatesHelper.IsHot(state))
      {
        ribbonItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemHotBackground1");
        ribbonItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemHotBackground2");
        ribbonItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemHotBorder");
        ribbonItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemHotText");
      }
      else
      {
        ribbonItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemBackground1");
        ribbonItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemBackground2");
        ribbonItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBorder");
        ribbonItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemText");
      }
      return ribbonItemColorSet;
    }

    public static QColorSet GetDefaultRibbonItemBarItemColorSet(
      object item,
      QItemStates state,
      QComposite parentComposite,
      IQColorRetriever retriever)
    {
      QColorSet itemBarItemColorSet = new QColorSet();
      if (QItemStatesHelper.IsDisabled(state))
      {
        itemBarItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemDisabledBackground1");
        itemBarItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemDisabledBackground2");
        itemBarItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemDisabledBorder");
        itemBarItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemTextDisabled");
      }
      else if (QItemStatesHelper.IsChecked(state) && QItemStatesHelper.IsHot(state))
      {
        float multiplyBrightness = 1.1f;
        itemBarItemColorSet.Background1 = QMath.ModifyColor(retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground1"), 1f, multiplyBrightness, 1f);
        itemBarItemColorSet.Background2 = QMath.ModifyColor(retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground2"), 1f, multiplyBrightness, 1f);
        itemBarItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBorder");
        itemBarItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemActiveText");
      }
      else if (QItemStatesHelper.IsPressed(state) || QItemStatesHelper.IsChecked(state))
      {
        itemBarItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground1");
        itemBarItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground2");
        itemBarItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBorder");
        itemBarItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemActiveText");
      }
      else if (QItemStatesHelper.IsExpanded(state) && parentComposite.PaintExpandedItem(item))
      {
        itemBarItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground1");
        itemBarItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemActiveBackground2");
        itemBarItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBorder");
        itemBarItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemActiveText");
      }
      else if (QItemStatesHelper.IsHot(state))
      {
        itemBarItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemHotBackground1");
        itemBarItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemHotBackground2");
        itemBarItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBorder");
        itemBarItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemHotText");
      }
      else
      {
        itemBarItemColorSet.Background1 = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBackground1");
        itemBarItemColorSet.Background2 = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBackground2");
        itemBarItemColorSet.Border = retriever.RetrieveFirstDefinedColor("RibbonItemBarItemBorder");
        itemBarItemColorSet.Foreground = retriever.RetrieveFirstDefinedColor("RibbonItemText");
      }
      return itemBarItemColorSet;
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      if (destinationObject == this.DropDownSplitPart && (QItemStatesHelper.IsNormal(state) || QItemStatesHelper.IsDisabled(state)))
        return (QColorSet) null;
      return this.RibbonItemBar != null ? QRibbonItem.GetDefaultRibbonItemBarItemColorSet(destinationObject, state, this.Composite, (IQColorRetriever) this) : QRibbonItem.GetDefaultRibbonItemColorSet(destinationObject, state, this.Composite, (IQColorRetriever) this);
    }

    protected override void ClearCachedParents()
    {
      base.ClearCachedParents();
      this.m_oCachedRibbonItemBar = (QRibbonItemBar) null;
      this.m_oCachedRibbonPanel = (QRibbonPanel) null;
      this.m_oCachedLaunchBar = (QRibbonLaunchBar) null;
    }

    protected override IQPartConfigurationBase CreateConfiguration() => (IQPartConfigurationBase) new QRibbonItemConfiguration(QRibbonItemConfigurationType.Default);

    protected override void SetDisplayParent(IQManagedLayoutParent displayParent)
    {
      base.SetDisplayParent(displayParent);
      this.SetConfigurationBaseProperties(false);
    }

    protected virtual void SetConfigurationBaseProperties(bool raiseEvent)
    {
      if (this.RibbonItemBar != null)
        this.Configuration.Properties.SetBaseProperties(this.RibbonItemBar.Configuration.DefaultItemConfiguration.Properties, true, raiseEvent);
      else if (this.LaunchBar != null)
        this.Configuration.Properties.SetBaseProperties(this.LaunchBar.Configuration.DefaultItemConfiguration.Properties, true, raiseEvent);
      else if (this.RibbonPageComposite != null)
        this.Configuration.Properties.SetBaseProperties(this.RibbonPageComposite.Configuration.DefaultItemConfiguration.Properties, true, raiseEvent);
      else
        this.Configuration.Properties.SetBaseProperties((QFastPropertyBag) null, true, raiseEvent);
      if (this.RibbonItemBar != null)
      {
        if (this.ColorScheme == null)
          return;
        this.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.RibbonItemBar.ColorScheme, false);
      }
      else if (this.RibbonPanel != null)
      {
        if (this.ColorScheme == null)
          return;
        this.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.RibbonPanel.ColorScheme, false);
      }
      else
      {
        if (this.LaunchBar == null)
          return;
        this.Configuration.Properties.SetBaseProperties(this.LaunchBar.Configuration.DefaultItemConfiguration.Properties, true, raiseEvent);
        if (this.ColorScheme == null)
          return;
        this.ColorScheme.SetBaseColorScheme((QColorSchemeBase) this.LaunchBar.ColorScheme, false);
      }
    }
  }
}
