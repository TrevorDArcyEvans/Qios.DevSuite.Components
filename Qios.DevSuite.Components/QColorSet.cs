// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QColorSet
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QColorSet
  {
    private const int Background1Index = 0;
    private const int Background2Index = 1;
    private const int ForegroundIndex = 2;
    private const int BorderIndex = 3;
    private const int InnerGlowIndex = 4;
    private Color[] m_aColors;
    private int InternalColorCount = 5;

    public QColorSet() => this.InternalConstruct();

    public QColorSet(Color background1, Color background2)
    {
      this.InternalConstruct();
      this.Background1 = background1;
      this.Background2 = background2;
    }

    public QColorSet(Color background1, Color background2, Color border)
    {
      this.InternalConstruct();
      this.Background1 = background1;
      this.Background2 = background2;
      this.Border = border;
    }

    public QColorSet(Color background1, Color background2, Color border, Color foreground)
    {
      this.InternalConstruct();
      this.Background1 = background1;
      this.Background2 = background2;
      this.Border = border;
      this.Foreground = foreground;
    }

    public QColorSet(QColorSet colorSet)
    {
      this.m_aColors = new Color[this.InternalColorCount];
      for (int index = 0; index < this.m_aColors.Length; ++index)
        this.m_aColors[index] = colorSet.m_aColors[index];
    }

    private void InternalConstruct()
    {
      this.m_aColors = new Color[this.InternalColorCount];
      for (int index = 0; index < this.m_aColors.Length; ++index)
        this.m_aColors[index] = Color.Empty;
    }

    public Color Background1
    {
      get => this.m_aColors[0];
      set => this.m_aColors[0] = value;
    }

    public Color Background2
    {
      get => this.m_aColors[1];
      set => this.m_aColors[1] = value;
    }

    public Color Border
    {
      get => this.m_aColors[3];
      set => this.m_aColors[3] = value;
    }

    public Color Foreground
    {
      get => this.m_aColors[2];
      set => this.m_aColors[2] = value;
    }

    public Color InnerGlow
    {
      get => this.m_aColors[4];
      set => this.m_aColors[4] = value;
    }

    public int NotEmptyColorCount
    {
      get
      {
        int notEmptyColorCount = 0;
        for (int index = 0; index < this.m_aColors.Length; ++index)
        {
          if (!this.m_aColors[index].IsEmpty)
            ++notEmptyColorCount;
        }
        return notEmptyColorCount;
      }
    }

    public int ColorCount => this.m_aColors.Length;

    public static QColorSet GetColorSetOnState(
      QColorSet normalSet,
      QColorSet hotSet,
      QColorSet activeSet,
      QButtonState state)
    {
      if (state == QButtonState.Pressed)
        return activeSet;
      return state == QButtonState.Hot ? hotSet : normalSet;
    }

    internal QColorSet MergeColorSet(QColorSet colorSet, float percentage) => new QColorSet(this.MergeColor(this.Background1, colorSet.Background1, percentage), this.MergeColor(this.Background2, colorSet.Background2, percentage), this.MergeColor(this.Border, colorSet.Border, percentage));

    private Color MergeColor(Color color1, Color color2, float percentage)
    {
      int num1 = (int) ((double) ((int) color1.R - (int) color2.R) * (double) percentage);
      int num2 = (int) ((double) ((int) color1.G - (int) color2.G) * (double) percentage);
      int num3 = (int) ((double) ((int) color1.B - (int) color2.B) * (double) percentage);
      return Color.FromArgb((int) color1.R - num1, (int) color1.G - num2, (int) color1.B - num3);
    }

    public bool EqualsColorSet(QColorSet colorSet)
    {
      for (int index = 0; index < this.m_aColors.Length; ++index)
      {
        if (this.m_aColors[index].ToArgb() != colorSet.m_aColors[index].ToArgb())
          return false;
      }
      return true;
    }
  }
}
