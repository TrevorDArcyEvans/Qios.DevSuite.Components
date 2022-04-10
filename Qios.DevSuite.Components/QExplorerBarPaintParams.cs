// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QExplorerBarPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QExplorerBarPaintParams : QCommandPaintParams
  {
    private StringFormat m_oItemStringFormat;
    private StringFormat m_oGroupItemStringFormat;
    private Color m_oGroupPanelBackground1Color;
    private Color m_oGroupPanelBackground2Color;
    private Color m_oGroupPanelBorderColor;
    private Color m_oGroupItemBackground1Color;
    private Color m_oGroupItemBackground2Color;
    private Color m_oGroupItemBorderColor;
    private Color m_oExpandedGroupItemBackground1Color;
    private Color m_oExpandedGroupItemBackground2Color;
    private Color m_oExpandedGroupItemBorderColor;
    private Color m_oHotGroupItemBackground1Color;
    private Color m_oHotGroupItemBackground2Color;
    private Color m_oHotGroupItemBorderColor;
    private Color m_oPressedGroupItemBackground1Color;
    private Color m_oPressedGroupItemBackground2Color;
    private Color m_oPressedGroupItemBorderColor;
    private Color m_oHasMoreChildItemsColor;
    private Color m_oCheckedGroupItemBackground1Color;
    private Color m_oCheckedGroupItemBackground2Color;
    private Color m_oCheckedGroupItemBorderColor;
    private Color m_oGroupItemShade;
    private Color m_oDepersonalizeImageForeground;
    private Color m_oDepersonalizeImageBackground;
    private QColorScheme m_oColorScheme;
    private QExplorerBarConfiguration m_oConfiguration;
    private QExplorerBarGroupItemConfiguration m_oGroupItemConfiguration;
    private QExplorerBarItemConfiguration m_oItemConfiguration;
    private Color m_oTextHotColor;
    private Color m_oTextPressedColor;
    private Color m_oTextExpandedColor;

    public Color GroupItemShade
    {
      get => this.m_oGroupItemShade;
      set => this.m_oGroupItemShade = value;
    }

    public Color DepersonalizeImageForeground
    {
      get => this.m_oDepersonalizeImageForeground;
      set => this.m_oDepersonalizeImageForeground = value;
    }

    public Color DepersonalizeImageBackground
    {
      get => this.m_oDepersonalizeImageBackground;
      set => this.m_oDepersonalizeImageBackground = value;
    }

    public Color GroupPanelBackground1Color
    {
      get => this.m_oGroupPanelBackground1Color;
      set => this.m_oGroupPanelBackground1Color = value;
    }

    public Color GroupPanelBackground2Color
    {
      get => this.m_oGroupPanelBackground2Color;
      set => this.m_oGroupPanelBackground2Color = value;
    }

    public Color GroupPanelBorderColor
    {
      get => this.m_oGroupPanelBorderColor;
      set => this.m_oGroupPanelBorderColor = value;
    }

    public Color GroupItemBackground1Color
    {
      get => this.m_oGroupItemBackground1Color;
      set => this.m_oGroupItemBackground1Color = value;
    }

    public Color GroupItemBackground2Color
    {
      get => this.m_oGroupItemBackground2Color;
      set => this.m_oGroupItemBackground2Color = value;
    }

    public Color GroupItemBorderColor
    {
      get => this.m_oGroupItemBorderColor;
      set => this.m_oGroupItemBorderColor = value;
    }

    public Color ExpandedGroupItemBackground1Color
    {
      get => this.m_oExpandedGroupItemBackground1Color;
      set => this.m_oExpandedGroupItemBackground1Color = value;
    }

    public Color ExpandedGroupItemBackground2Color
    {
      get => this.m_oExpandedGroupItemBackground2Color;
      set => this.m_oExpandedGroupItemBackground2Color = value;
    }

    public Color ExpandedGroupItemBorderColor
    {
      get => this.m_oExpandedGroupItemBorderColor;
      set => this.m_oExpandedGroupItemBorderColor = value;
    }

    public Color HotGroupItemBackground1Color
    {
      get => this.m_oHotGroupItemBackground1Color;
      set => this.m_oHotGroupItemBackground1Color = value;
    }

    public Color HotGroupItemBackground2Color
    {
      get => this.m_oHotGroupItemBackground2Color;
      set => this.m_oHotGroupItemBackground2Color = value;
    }

    public Color PressedGroupItemBorderColor
    {
      get => this.m_oPressedGroupItemBorderColor;
      set => this.m_oPressedGroupItemBorderColor = value;
    }

    public Color PressedGroupItemBackground1Color
    {
      get => this.m_oPressedGroupItemBackground1Color;
      set => this.m_oPressedGroupItemBackground1Color = value;
    }

    public Color PressedGroupItemBackground2Color
    {
      get => this.m_oPressedGroupItemBackground2Color;
      set => this.m_oPressedGroupItemBackground2Color = value;
    }

    public Color CheckedGroupItemBorderColor
    {
      get => this.m_oCheckedGroupItemBorderColor;
      set => this.m_oCheckedGroupItemBorderColor = value;
    }

    public Color CheckedGroupItemBackground1Color
    {
      get => this.m_oCheckedGroupItemBackground1Color;
      set => this.m_oCheckedGroupItemBackground1Color = value;
    }

    public Color CheckedGroupItemBackground2Color
    {
      get => this.m_oCheckedGroupItemBackground2Color;
      set => this.m_oCheckedGroupItemBackground2Color = value;
    }

    public Color HotGroupItemBorderColor
    {
      get => this.m_oHotGroupItemBorderColor;
      set => this.m_oHotGroupItemBorderColor = value;
    }

    public Color HasMoreChildItemsColor
    {
      get => this.m_oHasMoreChildItemsColor;
      set => this.m_oHasMoreChildItemsColor = value;
    }

    public Color TextHotColor
    {
      get => this.m_oTextHotColor;
      set => this.m_oTextHotColor = value;
    }

    public Color TextPressedColor
    {
      get => this.m_oTextPressedColor;
      set => this.m_oTextPressedColor = value;
    }

    public Color TextExpandedColor
    {
      get => this.m_oTextExpandedColor;
      set => this.m_oTextExpandedColor = value;
    }

    public StringFormat ItemStringFormat
    {
      get => this.m_oItemStringFormat;
      set => this.m_oItemStringFormat = value;
    }

    public StringFormat GroupItemStringFormat
    {
      get => this.m_oGroupItemStringFormat;
      set => this.m_oGroupItemStringFormat = value;
    }

    public QExplorerBarConfiguration Configuration
    {
      get => this.m_oConfiguration;
      set => this.m_oConfiguration = value;
    }

    public QExplorerBarItemConfiguration ItemConfiguration
    {
      get => this.m_oItemConfiguration;
      set => this.m_oItemConfiguration = value;
    }

    public QExplorerBarGroupItemConfiguration GroupItemConfiguration
    {
      get => this.m_oGroupItemConfiguration;
      set => this.m_oGroupItemConfiguration = value;
    }

    public QColorScheme ColorScheme
    {
      get => this.m_oColorScheme;
      set => this.m_oColorScheme = value;
    }

    internal void UpdateColors(QExplorerItem item)
    {
      if (item == null)
        return;
      this.ExpandedItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarExpandedItemBackground1", this.ColorScheme);
      this.ExpandedItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarExpandedItemBackground2", this.ColorScheme);
      this.ExpandedItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarExpandedItemBorder", this.ColorScheme);
      this.HotItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarHotItemBackground1", this.ColorScheme);
      this.HotItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarHotItemBackground2", this.ColorScheme);
      this.HotItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarHotItemBorder", this.ColorScheme);
      this.PressedItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarPressedItemBackground1", this.ColorScheme);
      this.PressedItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarPressedItemBackground2", this.ColorScheme);
      this.PressedItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarPressedItemBorder", this.ColorScheme);
      this.CheckedItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarCheckedItemBackground1", this.ColorScheme);
      this.CheckedItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarCheckedItemBackground2", this.ColorScheme);
      this.CheckedItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarCheckedItemBorder", this.ColorScheme);
      this.GroupPanelBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarGroupPanelBackground1", this.ColorScheme);
      this.GroupPanelBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarGroupPanelBackground2", this.ColorScheme);
      this.GroupPanelBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarGroupPanelBorder", this.ColorScheme);
      this.GroupItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarGroupItemBackground1", this.ColorScheme);
      this.GroupItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarGroupItemBackground2", this.ColorScheme);
      this.GroupItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarGroupItemBorder", this.ColorScheme);
      this.ExpandedGroupItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarExpandedGroupItemBackground1", this.ColorScheme);
      this.ExpandedGroupItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarExpandedGroupItemBackground2", this.ColorScheme);
      this.ExpandedGroupItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarExpandedGroupItemBorder", this.ColorScheme);
      this.HotGroupItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarHotGroupItemBackground1", this.ColorScheme);
      this.HotGroupItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarHotGroupItemBackground2", this.ColorScheme);
      this.HotGroupItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarHotGroupItemBorder", this.ColorScheme);
      this.PressedGroupItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarPressedGroupItemBackground1", this.ColorScheme);
      this.PressedGroupItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarPressedGroupItemBackground2", this.ColorScheme);
      this.PressedGroupItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarPressedGroupItemBorder", this.ColorScheme);
      this.CheckedGroupItemBackground1Color = item.RetrieveFirstDefinedColor("ExplorerBarCheckedGroupItemBackground1", this.ColorScheme);
      this.CheckedGroupItemBackground2Color = item.RetrieveFirstDefinedColor("ExplorerBarCheckedGroupItemBackground2", this.ColorScheme);
      this.CheckedGroupItemBorderColor = item.RetrieveFirstDefinedColor("ExplorerBarCheckedGroupItemBorder", this.ColorScheme);
      this.HasMoreChildItemsColor = item.RetrieveFirstDefinedColor("ExplorerBarHasMoreChildItemsColor", this.ColorScheme);
      this.GroupItemShade = item.RetrieveFirstDefinedColor("ExplorerBarGroupItemShade", this.ColorScheme);
      this.TextColor = item.RetrieveFirstDefinedColor("ExplorerBarText", this.ColorScheme);
      this.TextHotColor = item.RetrieveFirstDefinedColor("ExplorerBarTextHot", this.ColorScheme);
      this.TextPressedColor = item.RetrieveFirstDefinedColor("ExplorerBarTextPressed", this.ColorScheme);
      this.TextExpandedColor = item.RetrieveFirstDefinedColor("ExplorerBarTextExpanded", this.ColorScheme);
      this.TextDisabledColor = item.RetrieveFirstDefinedColor("ExplorerBarTextDisabled", this.ColorScheme);
    }
  }
}
