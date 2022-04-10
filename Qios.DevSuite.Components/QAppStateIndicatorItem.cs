// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppStateIndicatorItem
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxItem(false)]
  public class QAppStateIndicatorItem
  {
    private string m_sStateName;
    private string m_sDescription;
    private string m_sToolTipText;
    private int m_iState;
    private PictureBox m_oPictureBox;
    private QAppStateIndicatorType m_eStateType = QAppStateIndicatorType.AllInMain;

    public QAppStateIndicatorItem(
      string name,
      string description,
      string toolTipText,
      int state,
      QAppStateIndicatorType stateType,
      PictureBox pictureBox)
    {
      this.m_sStateName = name;
      this.m_sDescription = description;
      this.m_eStateType = stateType;
      this.m_iState = state;
      this.m_oPictureBox = pictureBox;
      this.ToolTipText = toolTipText;
    }

    public string StateName => this.m_sStateName;

    public QAppStateIndicatorType StateType => this.m_eStateType;

    [Localizable(true)]
    public string Description
    {
      get => this.m_sDescription;
      set => this.m_sDescription = value;
    }

    [Localizable(true)]
    public string ToolTipText
    {
      get => this.m_sToolTipText;
      set
      {
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sToolTipText = value;
      }
    }

    public int State
    {
      get => this.m_iState;
      set => this.m_iState = value;
    }

    public PictureBox PictureBox => this.m_oPictureBox;
  }
}
