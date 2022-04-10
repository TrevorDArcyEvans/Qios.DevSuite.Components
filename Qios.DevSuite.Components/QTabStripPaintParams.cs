// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabStripPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QTabStripPaintParams
  {
    private Color m_oBorder;
    private Color m_oBackground1;
    private Color m_oBackground2;
    private Color m_oNavigationButtonBorder;
    private Color m_oNavigationButtonBorderHot;
    private Color m_oNavigationButtonBorderActive;
    private Color m_oNavigationButtonBackground1;
    private Color m_oNavigationButtonBackground1Hot;
    private Color m_oNavigationButtonBackground1Active;
    private Color m_oNavigationButtonBackground2;
    private Color m_oNavigationButtonBackground2Hot;
    private Color m_oNavigationButtonBackground2Active;
    private Color m_oNavigationButtonReplace;
    private Color m_oNavigationButtonReplaceWith;
    private Color m_oNavigationButtonReplaceWithHot;
    private Color m_oNavigationButtonReplaceWithActive;
    private Color m_oNavigationButtonReplaceWithDisabled;
    private Color m_oNavigationAreaBackground1;
    private Color m_oNavigationAreaBackground2;
    private Color m_oNavigationAreaBorder;
    private Color m_oDropIndicatorBackground;
    private Color m_oDropIndicatorBorder;

    internal QTabStripPaintParams()
    {
    }

    public Color Border
    {
      get => this.m_oBorder;
      set => this.m_oBorder = value;
    }

    public Color Background1
    {
      get => this.m_oBackground1;
      set => this.m_oBackground1 = value;
    }

    public Color Background2
    {
      get => this.m_oBackground2;
      set => this.m_oBackground2 = value;
    }

    public Color NavigationButtonBackground1
    {
      get => this.m_oNavigationButtonBackground1;
      set => this.m_oNavigationButtonBackground1 = value;
    }

    public Color NavigationButtonBackground1Hot
    {
      get => this.m_oNavigationButtonBackground1Hot;
      set => this.m_oNavigationButtonBackground1Hot = value;
    }

    public Color NavigationButtonBackground1Active
    {
      get => this.m_oNavigationButtonBackground1Active;
      set => this.m_oNavigationButtonBackground1Active = value;
    }

    public Color NavigationButtonBackground2
    {
      get => this.m_oNavigationButtonBackground2;
      set => this.m_oNavigationButtonBackground2 = value;
    }

    public Color NavigationButtonBackground2Hot
    {
      get => this.m_oNavigationButtonBackground2Hot;
      set => this.m_oNavigationButtonBackground2Hot = value;
    }

    public Color NavigationButtonBackground2Active
    {
      get => this.m_oNavigationButtonBackground2Active;
      set => this.m_oNavigationButtonBackground2Active = value;
    }

    public Color NavigationButtonBorder
    {
      get => this.m_oNavigationButtonBorder;
      set => this.m_oNavigationButtonBorder = value;
    }

    public Color NavigationButtonBorderHot
    {
      get => this.m_oNavigationButtonBorderHot;
      set => this.m_oNavigationButtonBorderHot = value;
    }

    public Color NavigationButtonBorderActive
    {
      get => this.m_oNavigationButtonBorderActive;
      set => this.m_oNavigationButtonBorderActive = value;
    }

    public Color NavigationButtonReplace
    {
      get => this.m_oNavigationButtonReplace;
      set => this.m_oNavigationButtonReplace = value;
    }

    public Color NavigationButtonReplaceWith
    {
      get => this.m_oNavigationButtonReplaceWith;
      set => this.m_oNavigationButtonReplaceWith = value;
    }

    public Color NavigationButtonReplaceWithHot
    {
      get => this.m_oNavigationButtonReplaceWithHot;
      set => this.m_oNavigationButtonReplaceWithHot = value;
    }

    public Color NavigationButtonReplaceWithActive
    {
      get => this.m_oNavigationButtonReplaceWithActive;
      set => this.m_oNavigationButtonReplaceWithActive = value;
    }

    public Color NavigationButtonReplaceWithDisabled
    {
      get => this.m_oNavigationButtonReplaceWithDisabled;
      set => this.m_oNavigationButtonReplaceWithDisabled = value;
    }

    public Color NavigationAreaBackground1
    {
      get => this.m_oNavigationAreaBackground1;
      set => this.m_oNavigationAreaBackground1 = value;
    }

    public Color NavigationAreaBackground2
    {
      get => this.m_oNavigationAreaBackground2;
      set => this.m_oNavigationAreaBackground2 = value;
    }

    public Color NavigationAreaBorder
    {
      get => this.m_oNavigationAreaBorder;
      set => this.m_oNavigationAreaBorder = value;
    }

    public Color DropIndicatorBorder
    {
      get => this.m_oDropIndicatorBorder;
      set => this.m_oDropIndicatorBorder = value;
    }

    public Color DropIndicatorBackground
    {
      get => this.m_oDropIndicatorBackground;
      set => this.m_oDropIndicatorBackground = value;
    }
  }
}
