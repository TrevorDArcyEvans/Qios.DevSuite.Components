// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonSeparator
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;

namespace Qios.DevSuite.Components.Ribbon
{
  public class QRibbonSeparator : QCompositeSeparator
  {
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonPanel m_oCachedRibbonPanel;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonItemBar m_oCachedRibbonItemBar;
    [QCloneBehavior(QCloneBehaviorType.DoNotClone)]
    private QRibbonLaunchBar m_oCachedLaunchBar;

    protected QRibbonSeparator(object sourceObject, QObjectClonerConstructOptions options)
      : base(sourceObject, options)
    {
      this.InternalConstruct();
    }

    public QRibbonSeparator() => this.InternalConstruct();

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

    [Description("Gets or sets the QColorScheme that is used")]
    [Category("QAppearance")]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonSeparator)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    public override QColorSet GetItemColorSet(
      object destinationObject,
      QItemStates state,
      object additionalProperties)
    {
      if (destinationObject != this)
        return base.GetItemColorSet(destinationObject, state, additionalProperties);
      return new QColorSet()
      {
        Border = this.RetrieveFirstDefinedColor("RibbonSeparator1"),
        Background1 = this.RetrieveFirstDefinedColor("RibbonSeparator2")
      };
    }

    protected override void ClearCachedParents()
    {
      this.m_oCachedRibbonItemBar = (QRibbonItemBar) null;
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
