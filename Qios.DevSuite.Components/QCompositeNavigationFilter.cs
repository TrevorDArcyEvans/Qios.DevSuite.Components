// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeNavigationFilter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QCompositeNavigationFilter
  {
    private QCompositeNavigationFilterOptions m_eOptions;
    private QPartVisibilitySelectionTypes m_ePartVisibilitySelection;
    private Keys m_eShortcut;

    public QCompositeNavigationFilter()
    {
    }

    public QCompositeNavigationFilter(QCompositeNavigationFilterOptions options)
    {
      this.m_eOptions = options;
      this.CalculatePartVisibilitySelection();
    }

    public QCompositeNavigationFilter(QCompositeNavigationFilterOptions options, Keys shortcut)
    {
      this.m_eOptions = options;
      this.m_eShortcut = shortcut;
      this.CalculatePartVisibilitySelection();
    }

    public QCompositeNavigationFilterOptions Options
    {
      get => this.m_eOptions;
      set
      {
        this.m_eOptions = value;
        this.CalculatePartVisibilitySelection();
      }
    }

    public QPartVisibilitySelectionTypes PartVisibilitySelection => this.m_ePartVisibilitySelection;

    public bool MustBeEnabled => (this.m_eOptions & QCompositeNavigationFilterOptions.Enabled) == QCompositeNavigationFilterOptions.Enabled;

    public bool MustHavePressedState => (this.m_eOptions & QCompositeNavigationFilterOptions.HasPressedState) == QCompositeNavigationFilterOptions.HasPressedState;

    public bool MustBeVisible => (this.m_eOptions & QCompositeNavigationFilterOptions.Visible) == QCompositeNavigationFilterOptions.Visible;

    public bool MustMatchShortcut => (this.m_eOptions & QCompositeNavigationFilterOptions.MatchShortcut) == QCompositeNavigationFilterOptions.MatchShortcut;

    public Keys Shortcut
    {
      get => this.m_eShortcut;
      set => this.m_eShortcut = value;
    }

    private void CalculatePartVisibilitySelection()
    {
      if ((this.m_eOptions & QCompositeNavigationFilterOptions.Visible) == QCompositeNavigationFilterOptions.Visible)
        this.m_ePartVisibilitySelection = QPartVisibilitySelectionTypes.IncludeAll;
      else if ((this.m_eOptions & QCompositeNavigationFilterOptions.VisibleForShortcut) == QCompositeNavigationFilterOptions.VisibleForShortcut)
        this.m_ePartVisibilitySelection = ~QPartVisibilitySelectionTypes.IncludeHiddenBecauseOfConstraints;
      else
        this.m_ePartVisibilitySelection = QPartVisibilitySelectionTypes.IncludeNone;
    }
  }
}
