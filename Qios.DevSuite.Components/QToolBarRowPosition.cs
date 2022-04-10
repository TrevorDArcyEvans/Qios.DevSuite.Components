// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarRowPosition
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

namespace Qios.DevSuite.Components
{
  internal class QToolBarRowPosition
  {
    private int m_iToolBarRowIndex = -1;
    private QToolBarRow m_oToolBarRow;
    private QToolBarRowPositionType m_ePositionType;

    public QToolBarRowPosition()
    {
    }

    public QToolBarRowPosition(
      int toolBarRowIndex,
      QToolBarRow toolBarRow,
      QToolBarRowPositionType positionType)
    {
      this.m_iToolBarRowIndex = toolBarRowIndex;
      this.m_oToolBarRow = toolBarRow;
      this.m_ePositionType = positionType;
    }

    public int ToolBarRowIndex
    {
      get => this.m_iToolBarRowIndex;
      set => this.m_iToolBarRowIndex = value;
    }

    public QToolBarRow ToolBarRow
    {
      get => this.m_oToolBarRow;
      set => this.m_oToolBarRow = value;
    }

    public QToolBarRowPositionType PositionType
    {
      get => this.m_ePositionType;
      set => this.m_ePositionType = value;
    }
  }
}
