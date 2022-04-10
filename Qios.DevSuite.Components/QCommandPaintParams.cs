// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QCommandPaintParams
  {
    private Color m_oBackgroundColor1;
    private Color m_oBackgroundColor2;
    private Color m_oMoreItemsColor1;
    private Color m_oMoreItemsColor2;
    private Color m_oSizingGripDarkColor;
    private Color m_oSizingGripLightColor;
    private Color m_oTextColor;
    private Color m_oTextActiveColor;
    private Color m_oTextDisabledColor;
    private Color m_oExpandedItemBackground1Color;
    private Color m_oExpandedItemBackground2Color;
    private Color m_oExpandedItemBorderColor;
    private Color m_oHotItemBackground1Color;
    private Color m_oHotItemBackground2Color;
    private Color m_oHotItemBorderColor;
    private Color m_oPressedItemBackground1Color;
    private Color m_oPressedItemBackground2Color;
    private Color m_oPressedItemBorderColor;
    private Color m_oSeparatorColor;
    private Color m_oCheckedItemBackground1Color;
    private Color m_oCheckedItemBackground2Color;
    private Color m_oCheckedItemBorderColor;
    private int m_iItemGradientAngle;
    private StringFormat m_oStringFormat;
    private Font m_oFont;
    private Size m_oRequestedSize;
    private Size m_oMinimumSize;
    private Size m_oProposedSize;
    private int m_iFirstVisibleItem;
    private int m_iLastVisibleItem = -1;

    public Color BackgroundColor1
    {
      get => this.m_oBackgroundColor1;
      set => this.m_oBackgroundColor1 = value;
    }

    public Color BackgroundColor2
    {
      get => this.m_oBackgroundColor2;
      set => this.m_oBackgroundColor2 = value;
    }

    public Color MoreItemsColor1
    {
      get => this.m_oMoreItemsColor1;
      set => this.m_oMoreItemsColor1 = value;
    }

    public Color MoreItemsColor2
    {
      get => this.m_oMoreItemsColor2;
      set => this.m_oMoreItemsColor2 = value;
    }

    public Color SizingGripDarkColor
    {
      get => this.m_oSizingGripDarkColor;
      set => this.m_oSizingGripDarkColor = value;
    }

    public Color SizingGripLightColor
    {
      get => this.m_oSizingGripLightColor;
      set => this.m_oSizingGripLightColor = value;
    }

    public Color TextColor
    {
      get => this.m_oTextColor;
      set => this.m_oTextColor = value;
    }

    public Color TextActiveColor
    {
      get => this.m_oTextActiveColor;
      set => this.m_oTextActiveColor = value;
    }

    public Color TextDisabledColor
    {
      get => this.m_oTextDisabledColor;
      set => this.m_oTextDisabledColor = value;
    }

    public Color ExpandedItemBackground1Color
    {
      get => this.m_oExpandedItemBackground1Color;
      set => this.m_oExpandedItemBackground1Color = value;
    }

    public Color ExpandedItemBackground2Color
    {
      get => this.m_oExpandedItemBackground2Color;
      set => this.m_oExpandedItemBackground2Color = value;
    }

    public Color ExpandedItemBorderColor
    {
      get => this.m_oExpandedItemBorderColor;
      set => this.m_oExpandedItemBorderColor = value;
    }

    public Color HotItemBackground1Color
    {
      get => this.m_oHotItemBackground1Color;
      set => this.m_oHotItemBackground1Color = value;
    }

    public Color HotItemBackground2Color
    {
      get => this.m_oHotItemBackground2Color;
      set => this.m_oHotItemBackground2Color = value;
    }

    public Color PressedItemBorderColor
    {
      get => this.m_oPressedItemBorderColor;
      set => this.m_oPressedItemBorderColor = value;
    }

    public Color PressedItemBackground1Color
    {
      get => this.m_oPressedItemBackground1Color;
      set => this.m_oPressedItemBackground1Color = value;
    }

    public Color PressedItemBackground2Color
    {
      get => this.m_oPressedItemBackground2Color;
      set => this.m_oPressedItemBackground2Color = value;
    }

    public Color HotItemBorderColor
    {
      get => this.m_oHotItemBorderColor;
      set => this.m_oHotItemBorderColor = value;
    }

    public Color CheckedItemBackground1Color
    {
      get => this.m_oCheckedItemBackground1Color;
      set => this.m_oCheckedItemBackground1Color = value;
    }

    public Color CheckedItemBackground2Color
    {
      get => this.m_oCheckedItemBackground2Color;
      set => this.m_oCheckedItemBackground2Color = value;
    }

    public Color CheckedItemBorderColor
    {
      get => this.m_oCheckedItemBorderColor;
      set => this.m_oCheckedItemBorderColor = value;
    }

    public Color SeparatorColor
    {
      get => this.m_oSeparatorColor;
      set => this.m_oSeparatorColor = value;
    }

    public int ItemGradientAngle
    {
      get => this.m_iItemGradientAngle;
      set => this.m_iItemGradientAngle = value;
    }

    public StringFormat StringFormat
    {
      get => this.m_oStringFormat;
      set => this.m_oStringFormat = value;
    }

    public Font Font
    {
      get => this.m_oFont;
      set => this.m_oFont = value;
    }

    public int FirstVisibleItem
    {
      get => this.m_iFirstVisibleItem;
      set => this.m_iFirstVisibleItem = value;
    }

    public int LastVisibleItem
    {
      get => this.m_iLastVisibleItem;
      set => this.m_iLastVisibleItem = value;
    }

    public Size RequestedSize
    {
      get => this.m_oRequestedSize;
      set => this.m_oRequestedSize = value;
    }

    public Size MinimumSize
    {
      get => this.m_oMinimumSize;
      set => this.m_oMinimumSize = value;
    }

    public Size ProposedSize
    {
      get => this.m_oProposedSize;
      set => this.m_oProposedSize = value;
    }
  }
}
