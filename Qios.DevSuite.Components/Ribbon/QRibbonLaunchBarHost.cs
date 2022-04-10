// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Ribbon.QRibbonLaunchBarHost
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components.Ribbon
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (QCompositeControl), "Resources.ControlImages.QRibbonLaunchBarHost.bmp")]
  [Designer(typeof (QRibbonLaunchBarHostDesigner), typeof (IDesigner))]
  public class QRibbonLaunchBarHost : QCompositeControl, IQComponentHost
  {
    private QRibbonLaunchBar m_oLaunchBar;
    private bool m_bChangingVisibility;
    private bool m_bAutoHide = true;

    public QRibbonLaunchBarHost()
    {
      this.PaintTransparentBackground = false;
      this.SuspendChangeNotification();
      this.ResumeChangeNotification(false);
    }

    protected override QComposite CreateComposite() => (QComposite) new QRibbonLaunchBarHostComposite(this);

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QRibbonLaunchBarHostComposite LaunchBarComposite => this.Composite as QRibbonLaunchBarHostComposite;

    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItemBar)]
    [Category("QAppearance")]
    [Description("The ColorScheme that is used.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Clear)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonLaunchBarHost)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonLaunchBar)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonPanel)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonItem)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.RibbonSeparator)]
    [QColorSchemeShowColors(QColorSchemeShowColorsMethod.Add, QColorCategory.CompositeBalloon)]
    public override QColorScheme ColorScheme
    {
      get => base.ColorScheme;
      set => base.ColorScheme = value;
    }

    [Description("Gets the configuration of the QRibbonLaunchBarHost")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("QAppearance")]
    public QRibbonLaunchBarHostConfiguration Configuration => base.Configuration as QRibbonLaunchBarHostConfiguration;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public QPart LaunchBarAreaPart => this.LaunchBarComposite.LaunchBarAreaPart;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public QPart ItemAreaPart => this.LaunchBarComposite.ItemAreaPart;

    [Description("Gets or sets whether this control must be hidden when there are no items on it.")]
    [DefaultValue(true)]
    [Category("QBehavior")]
    public virtual bool AutoHide
    {
      get => this.m_bAutoHide;
      set
      {
        this.m_bAutoHide = value;
        this.HandleChildObjectChanged(true, Rectangle.Empty);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    [Description("Gets or sets the LaunchBar that must be hosted on this QRibbonLaunchBarHost.")]
    [Category("QBehavior")]
    public QRibbonLaunchBar LaunchBar
    {
      get => this.m_oLaunchBar;
      set => this.SetLaunchBar(value, true);
    }

    internal void SetLaunchBar(QRibbonLaunchBar value, bool removeFromCurrentHost)
    {
      if (this.m_oLaunchBar == value)
        return;
      this.SuspendChangeNotification();
      if (this.m_oLaunchBar != null)
      {
        this.m_oLaunchBar.SetCustomComponentHost((IQComponentHost) null, false);
        this.LaunchBarAreaPart.Parts.Remove((IQPart) this.m_oLaunchBar);
      }
      this.m_oLaunchBar = value;
      if (this.m_oLaunchBar != null)
      {
        this.m_oLaunchBar.SetCustomComponentHost((IQComponentHost) this, removeFromCurrentHost);
        this.m_oLaunchBar.Active = true;
        this.m_oLaunchBar.DrawShape = false;
        this.LaunchBarAreaPart.Parts.Add((IQPart) this.m_oLaunchBar, false);
      }
      this.ResumeChangeNotification(true);
    }

    [DefaultValue(false)]
    public override bool PaintTransparentBackground
    {
      get => base.PaintTransparentBackground;
      set => base.PaintTransparentBackground = value;
    }

    public override QPartCollection Items => this.ItemAreaPart.Parts;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    protected override IList AssociatedComponents
    {
      get
      {
        ArrayList associatedComponents = new ArrayList((ICollection) this.ItemAreaPart.Parts);
        if (this.LaunchBar != null)
          associatedComponents.Add((object) this.LaunchBar);
        return (IList) associatedComponents;
      }
    }

    protected override string BackColorPropertyName => "RibbonLaunchBarBackground1";

    protected override string BackColor2PropertyName => "RibbonLaunchBarBackground2";

    protected override string BorderColorPropertyName => "RibbonLaunchBarBorder";

    private void SetVisibilityToAutoHide()
    {
      this.m_bChangingVisibility = true;
      if (!this.DesignMode && this.AutoHide && this.Visible != (this.LaunchBar != null))
        this.Visible = this.LaunchBar != null;
      this.m_bChangingVisibility = false;
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (this.m_bChangingVisibility)
        return;
      this.SetVisibilityToAutoHide();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.SetVisibilityToAutoHide();
    }

    void IQComponentHost.SetComponent(object previousValue, object newValue)
    {
      if (previousValue == null || previousValue != this.m_oLaunchBar)
        return;
      this.SetLaunchBar(newValue as QRibbonLaunchBar, true);
    }
  }
}
