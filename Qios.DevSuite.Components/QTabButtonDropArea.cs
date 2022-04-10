// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QTabButtonDropArea
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QTabButtonDropArea
  {
    private Rectangle m_oBounds;
    private QTabButton m_oTabButton;
    private QTabStrip m_oTabStrip;
    private QTabButtonDockStyle m_oDock;
    private bool m_bAllowDrop = true;
    private bool m_bFirstInRow;
    private bool m_bLastInRow;
    private bool m_bDockSet;

    public QTabButtonDropArea()
    {
    }

    public QTabButtonDropArea(Rectangle bounds, QTabButton button, QTabStrip tabStrip)
    {
      this.m_oBounds = bounds;
      this.m_oTabButton = button;
      this.m_oTabStrip = tabStrip;
    }

    public Rectangle Bounds
    {
      get => this.m_oBounds;
      set => this.m_oBounds = value;
    }

    public QTabButton Button
    {
      get => this.m_oTabButton;
      set => this.m_oTabButton = value;
    }

    public QTabStrip TabStrip
    {
      get => this.m_oTabStrip;
      set => this.m_oTabStrip = value;
    }

    public int DropButtonOrder
    {
      get
      {
        if (this.m_oTabButton == null)
          return 0;
        if (this.m_oDock == QTabButtonDockStyle.Top || this.m_oDock == QTabButtonDockStyle.Left)
          return this.m_oTabButton.ButtonOrder;
        QTabButton nextButton = this.m_oTabButton.TabStrip.GetNextButton(this.m_oTabButton, QTabButtonSelectionTypes.None, (QTabButtonRow) null, false);
        return nextButton != null ? nextButton.ButtonOrder : this.m_oTabButton.ButtonOrder + 1;
      }
    }

    public QTabButtonDockStyle Dock => this.m_oDock;

    public bool AllowDrop
    {
      get => this.m_bAllowDrop;
      set => this.m_bAllowDrop = value;
    }

    public bool FirstInRow
    {
      get => this.m_bFirstInRow;
      set => this.m_bFirstInRow = value;
    }

    public bool LastInRow
    {
      get => this.m_bLastInRow;
      set => this.m_bLastInRow = value;
    }

    public bool CalculateDock(Point mouseCoordinate, bool isHorizontal, QTabButton button)
    {
      if (isHorizontal)
      {
        QTabButtonDockStyle qtabButtonDockStyle;
        if (this.m_oTabButton != null)
        {
          int num = this.Button.BoundsToControl.X + this.Button.BoundsToControl.Width / 2;
          qtabButtonDockStyle = mouseCoordinate.X > num ? QTabButtonDockStyle.Right : QTabButtonDockStyle.Left;
        }
        else
          qtabButtonDockStyle = this.m_oTabStrip.Configuration.ButtonConfiguration.Alignment == QTabButtonAlignment.Near ? QTabButtonDockStyle.Left : QTabButtonDockStyle.Right;
        if (this.m_oDock != qtabButtonDockStyle || !this.m_bDockSet)
        {
          this.m_bDockSet = true;
          this.m_oDock = qtabButtonDockStyle;
          this.CalculateAllowDrop(button);
          return true;
        }
      }
      else
      {
        QTabButtonDockStyle qtabButtonDockStyle;
        if (this.m_oTabButton != null)
        {
          int num = this.Button.BoundsToControl.Y + this.Button.BoundsToControl.Height / 2;
          qtabButtonDockStyle = mouseCoordinate.Y > num ? QTabButtonDockStyle.Bottom : QTabButtonDockStyle.Top;
        }
        else
          qtabButtonDockStyle = this.m_oTabStrip.Configuration.ButtonConfiguration.Alignment == QTabButtonAlignment.Near ? QTabButtonDockStyle.Top : QTabButtonDockStyle.Bottom;
        if (this.m_oDock != qtabButtonDockStyle || !this.m_bDockSet)
        {
          this.m_bDockSet = true;
          this.m_oDock = qtabButtonDockStyle;
          this.CalculateAllowDrop(button);
          return true;
        }
      }
      return false;
    }

    public void CalculateAllowDrop(QTabButton button)
    {
      if (this.m_oTabButton == null)
      {
        this.m_bAllowDrop = true;
      }
      else
      {
        QTabButton nextButton = button.TabStrip.GetNextButton(button, QTabButtonSelectionTypes.None, (QTabButtonRow) null, false);
        QTabButton previousButton = button.TabStrip.GetPreviousButton(button, QTabButtonSelectionTypes.None, (QTabButtonRow) null, false);
        bool flag = this.m_oDock == QTabButtonDockStyle.Bottom || this.m_oDock == QTabButtonDockStyle.Right;
        if (button == this.Button)
          this.m_bAllowDrop = false;
        else if (!flag && this.Button == nextButton)
          this.m_bAllowDrop = false;
        else if (flag && this.Button == previousButton)
          this.m_bAllowDrop = false;
        else
          this.m_bAllowDrop = true;
      }
    }
  }
}
