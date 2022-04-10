// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QAppStateIndicator
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  [ToolboxBitmap(typeof (QAppStateIndicator), "Resources.ControlImages.QAppStateIndicator.bmp")]
  [ToolboxItem(true)]
  public class QAppStateIndicator : Component
  {
    private string m_sMainToolTipText;
    private string m_sMainDoneText;
    private QProgressBar m_oMainProgressBar;
    private QStatusBarPanel m_oMainStatusBarPanel;
    private Control m_oStateImagesContainer;
    private int m_iMainState;
    private ArrayList m_aStateItems;
    private QBalloon m_oBalloon;
    private Container components;

    public QAppStateIndicator(IContainer container)
    {
      if (container == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (container)));
      container.Add((IComponent) this);
      this.InternalConstruct();
    }

    public QAppStateIndicator() => this.InternalConstruct();

    private void InternalConstruct()
    {
      this.components = new Container();
      this.m_aStateItems = new ArrayList();
      this.m_oBalloon = new QBalloon();
    }

    [Category("QAppearance")]
    [Description("Gets or sets the ToolTip text for the MainState")]
    [Localizable(true)]
    [DefaultValue(null)]
    public string MainToolTipText
    {
      get => this.m_sMainToolTipText;
      set
      {
        QXmlHelper.ValidateXmlFragment(value, true);
        this.m_sMainToolTipText = value;
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the main text when all the states are set to 0")]
    [Localizable(true)]
    [Category("QAppearance")]
    public string MainDoneText
    {
      get => this.m_sMainDoneText;
      set => this.m_sMainDoneText = value;
    }

    [Description("Gets or sets the StatusBarPanel that should display the main state")]
    [DefaultValue(null)]
    [Category("QBehavior")]
    public QStatusBarPanel MainStatusBarPanel
    {
      get => this.m_oMainStatusBarPanel;
      set
      {
        this.m_oMainStatusBarPanel = value;
        this.RefreshMainState();
      }
    }

    [DefaultValue(null)]
    [Category("QBehavior")]
    [Description("Gets or sets the Control that will show the Images set by the items")]
    public Control StateImagesContainer
    {
      get => this.m_oStateImagesContainer;
      set
      {
        this.m_oStateImagesContainer = value;
        this.RefreshMainState();
      }
    }

    [Category("QBehavior")]
    [Description("Gets or sets the QProgressBar that should display the main state")]
    [DefaultValue(null)]
    public QProgressBar MainProgressBar
    {
      get => this.m_oMainProgressBar;
      set
      {
        this.m_oMainProgressBar = value;
        this.RefreshMainState();
      }
    }

    public int this[string name]
    {
      get
      {
        QAppStateIndicatorItem stateIndicatorItem;
        return (stateIndicatorItem = this.GetItem(name)) != null ? stateIndicatorItem.State : 0;
      }
      set
      {
        QAppStateIndicatorItem stateIndicatorItem;
        if ((stateIndicatorItem = this.GetItem(name)) != null)
          stateIndicatorItem.State = value;
        else
          this.AddStateItem(name, (string) null, (string) null, value, QAppStateIndicatorType.AllInMain, (Bitmap) null);
      }
    }

    [Browsable(false)]
    public int MainState => this.m_iMainState;

    public void SetState(
      string name,
      string description,
      string toolTipText,
      int state,
      QAppStateIndicatorType stateType,
      Bitmap bitmap)
    {
      QAppStateIndicatorItem stateIndicatorItem;
      if ((stateIndicatorItem = this.GetItem(name)) != null)
      {
        stateIndicatorItem.State = state;
        stateIndicatorItem.Description = description;
        stateIndicatorItem.ToolTipText = toolTipText;
        if (stateIndicatorItem.State <= 0)
          this.RemoveStateItem(name);
        else
          this.SetToolTip(stateIndicatorItem);
      }
      else if (state > 0)
        this.SetToolTip(this.AddStateItem(name, description, toolTipText, state, stateType, bitmap));
      this.RefreshMainState();
    }

    private QAppStateIndicatorItem GetItem(string name)
    {
      for (int index = 0; index < this.m_aStateItems.Count; ++index)
      {
        if (string.Compare(((QAppStateIndicatorItem) this.m_aStateItems[index]).StateName, name, true, CultureInfo.InvariantCulture) == 0)
          return (QAppStateIndicatorItem) this.m_aStateItems[index];
      }
      return (QAppStateIndicatorItem) null;
    }

    private void RemoveStateItem(string name)
    {
      QAppStateIndicatorItem stateIndicatorItem;
      if ((stateIndicatorItem = this.GetItem(name)) == null)
        return;
      this.m_aStateItems.Remove((object) stateIndicatorItem);
      if (stateIndicatorItem.PictureBox == null || this.m_oStateImagesContainer == null)
        return;
      stateIndicatorItem.PictureBox.Dispose();
      this.m_oStateImagesContainer.Controls.Remove((Control) stateIndicatorItem.PictureBox);
    }

    private QAppStateIndicatorItem AddStateItem(
      string name,
      string description,
      string tooltipText,
      int state,
      QAppStateIndicatorType stateType,
      Bitmap bitmap)
    {
      if (state == 0)
        return (QAppStateIndicatorItem) null;
      PictureBox pictureBox = (PictureBox) null;
      if (bitmap != null && this.m_oStateImagesContainer != null)
      {
        pictureBox = new PictureBox();
        pictureBox.Image = (Image) bitmap;
        pictureBox.Width = bitmap.Width + 2;
        pictureBox.Height = this.m_oStateImagesContainer.Height;
        pictureBox.Dock = DockStyle.Right;
        pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        this.m_oStateImagesContainer.Controls.Add((Control) pictureBox);
      }
      QAppStateIndicatorItem stateIndicatorItem = new QAppStateIndicatorItem(name, description, tooltipText, state, stateType, pictureBox);
      this.m_aStateItems.Add((object) stateIndicatorItem);
      return stateIndicatorItem;
    }

    private void SetMainStateText(string text)
    {
      if (this.MainStatusBarPanel == null)
        return;
      this.MainStatusBarPanel.Text = text;
    }

    private void SetMainStateToolTip(string text)
    {
      if (this.MainStatusBarPanel == null)
        return;
      this.MainStatusBarPanel.ToolTipText = text;
    }

    private void SetMainStateProgressBar(int progress)
    {
      if (this.m_oMainProgressBar == null)
        return;
      this.m_oMainProgressBar.Value = progress;
    }

    private void SetToolTip(QAppStateIndicatorItem item)
    {
      if (item.PictureBox == null || item.ToolTipText == null)
        return;
      this.m_oBalloon.SetMarkupText((Control) item.PictureBox, QAppStateIndicator.FormatToolTipCaption(item.ToolTipText, item.State));
    }

    private static string FormatToolTipCaption(string caption, int status)
    {
      if (caption == null)
        return (string) null;
      string str = caption.Replace("%d", status.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      return status != 1 ? str.Replace("%e", "en") : str.Replace("%e", "");
    }

    private void RefreshMainState()
    {
      int num = 0;
      for (int index = 0; index < this.m_aStateItems.Count; ++index)
      {
        QAppStateIndicatorItem aStateItem = (QAppStateIndicatorItem) this.m_aStateItems[index];
        switch (aStateItem.StateType)
        {
          case QAppStateIndicatorType.OneInMain:
            ++num;
            break;
          case QAppStateIndicatorType.AllInMain:
            num += aStateItem.State;
            break;
        }
      }
      this.m_iMainState = num;
      if (this.m_aStateItems.Count > 0)
        this.SetMainStateText(((QAppStateIndicatorItem) this.m_aStateItems[this.m_aStateItems.Count - 1]).Description);
      else
        this.SetMainStateText(this.MainDoneText);
      this.SetMainStateProgressBar(this.MainState);
      this.SetMainStateToolTip(QAppStateIndicator.FormatToolTipCaption(this.MainToolTipText, this.MainState));
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (this.components != null)
          this.components.Dispose();
        if (this.m_oBalloon == null)
          return;
        this.m_oBalloon.Dispose();
        this.m_oBalloon = (QBalloon) null;
      }
      finally
      {
        base.Dispose(disposing);
      }
    }
  }
}
