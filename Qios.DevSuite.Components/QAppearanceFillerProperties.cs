// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppearanceFillerProperties
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QAppearanceFillerProperties
  {
    private static QAppearanceFillerProperties m_oDefault;
    private DockStyle m_eDockStyle;
    private QAppearanceBackgroundOptions m_eBackgroundOptions;
    private Rectangle m_oAlternativeBoundsForBrushCreation = Rectangle.Empty;
    private bool m_bUseAlternativeBoundsForBrushCreation;
    private Color m_oAlternativeShineColor = Color.Empty;
    private bool m_bUseAlternativeShineColor;

    public Color AlternativeShineColor
    {
      get => this.m_oAlternativeShineColor;
      set => this.m_oAlternativeShineColor = value;
    }

    public bool UseAlternativeShineColor
    {
      get => this.m_bUseAlternativeShineColor;
      set => this.m_bUseAlternativeShineColor = value;
    }

    public Rectangle AlternativeBoundsForBrushCreation
    {
      get => this.m_oAlternativeBoundsForBrushCreation;
      set
      {
        this.m_oAlternativeBoundsForBrushCreation = value;
        this.m_bUseAlternativeBoundsForBrushCreation = value != Rectangle.Empty;
      }
    }

    public bool UseAlternativeBoundsForBrushCreation
    {
      get => this.m_bUseAlternativeBoundsForBrushCreation;
      set => this.m_bUseAlternativeBoundsForBrushCreation = value;
    }

    public DockStyle DockStyle
    {
      get => this.m_eDockStyle;
      set => this.m_eDockStyle = value;
    }

    public QAppearanceBackgroundOptions BackgroundOptions
    {
      get => this.m_eBackgroundOptions;
      set => this.m_eBackgroundOptions = value;
    }

    public static QAppearanceFillerProperties Default
    {
      get
      {
        if (QAppearanceFillerProperties.m_oDefault == null)
          QAppearanceFillerProperties.m_oDefault = new QAppearanceFillerProperties();
        return QAppearanceFillerProperties.m_oDefault;
      }
    }
  }
}
