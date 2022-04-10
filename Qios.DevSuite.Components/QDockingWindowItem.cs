// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDockingWindowItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QDockingWindowItem : IQDockingWindowItem
  {
    public QDockingWindow Window;
    public Rectangle ButtonBounds;
    public bool IsSlideOutCandidate;
    public int LastActiveTickCount;
    private QDockingWindowGroup m_oGroup;
    private bool m_bExpanded;

    public QDockingWindowItem(QDockingWindow window, QDockingWindowGroup group)
    {
      this.m_oGroup = group;
      this.Window = window;
    }

    public bool IsExpanded
    {
      get => this.m_oGroup == null || this.m_bExpanded;
      set
      {
        if (this.m_bExpanded == value)
          return;
        if (value && this.m_oGroup != null)
          this.m_oGroup.SetAllButtonsToNotExpanded();
        this.m_bExpanded = value;
      }
    }

    public bool IsGroup => false;

    public int Count => 1;

    public bool IsInGroup => this.m_oGroup != null;

    public QDockingWindowGroup Group => this.m_oGroup;
  }
}
