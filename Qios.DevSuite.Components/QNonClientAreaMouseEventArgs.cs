// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QNonClientAreaMouseEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QNonClientAreaMouseEventArgs : MouseEventArgs
  {
    private QMouseAction m_eMouseAction;
    private QNonClientAreaLocation m_eNonClientAreaLocation;
    private QSizingAction m_eSizingAction;
    private bool m_bCancelDefaultAction;
    private bool m_bRedirectToNowhere;

    public QNonClientAreaMouseEventArgs(
      QMouseAction mouseAction,
      MouseButtons buttons,
      int clicks,
      int left,
      int top,
      QNonClientAreaLocation location,
      QSizingAction sizingAction)
      : base(buttons, clicks, left, top, 0)
    {
      this.m_eMouseAction = mouseAction;
      this.m_eSizingAction = sizingAction;
      this.m_eNonClientAreaLocation = location;
    }

    public QMouseAction MouseAction => this.m_eMouseAction;

    public QNonClientAreaLocation NonClientAreaLocation => this.m_eNonClientAreaLocation;

    public QSizingAction SizingAction => this.m_eSizingAction;

    public bool CancelDefaultAction
    {
      get => this.m_bCancelDefaultAction;
      set => this.m_bCancelDefaultAction = value;
    }

    internal bool RedirectToNowhere
    {
      get => this.m_bRedirectToNowhere;
      set => this.m_bRedirectToNowhere = value;
    }
  }
}
