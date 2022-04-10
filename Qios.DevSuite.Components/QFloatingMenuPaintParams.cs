// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QFloatingMenuPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QFloatingMenuPaintParams : QCommandPaintParams
  {
    private int m_iBorderWidth = 1;
    private QRange m_oIconRange = QRange.Empty;
    private QRange m_oIconBackgroundRange = QRange.Empty;
    private QRange m_oTitleRange = QRange.Empty;
    private QRange m_oShortcutRange = QRange.Empty;
    private QRange m_oHasChildItemsIconRange = QRange.Empty;
    private bool m_bShowDepersonalizeItem;
    private Rectangle m_oDepersonalizeItemBounds;
    private int m_iItemOuterBoundsWidth;
    private Rectangle m_oHasMoreItemsUpBounds = Rectangle.Empty;
    private Rectangle m_oHasMoreItemsDownBounds = Rectangle.Empty;
    private Rectangle m_oParentIntersectionBounds = Rectangle.Empty;
    private Color m_oParentMenuIntersectColor;
    private Color m_oIconBackground1Color;
    private Color m_oIconBackground2Color;
    private Color m_oDepersonalizeImageBackground;
    private Color m_oDepersonalizeImageForeground;
    private Color m_oIconBackgroundDepersonalized1Color;
    private Color m_oIconBackgroundDepersonalized2Color;

    public int BorderWidth
    {
      get => this.m_iBorderWidth;
      set => this.m_iBorderWidth = value;
    }

    public QRange IconBackgroundRange
    {
      get => this.m_oIconBackgroundRange;
      set => this.m_oIconBackgroundRange = value;
    }

    public QRange IconRange
    {
      get => this.m_oIconRange;
      set => this.m_oIconRange = value;
    }

    public QRange TitleRange
    {
      get => this.m_oTitleRange;
      set => this.m_oTitleRange = value;
    }

    public QRange ShortcutRange
    {
      get => this.m_oShortcutRange;
      set => this.m_oShortcutRange = value;
    }

    public QRange HasChildItemsIconRange
    {
      get => this.m_oHasChildItemsIconRange;
      set => this.m_oHasChildItemsIconRange = value;
    }

    public bool ShowDepersonalizeItem
    {
      get => this.m_bShowDepersonalizeItem;
      set => this.m_bShowDepersonalizeItem = value;
    }

    public Rectangle DepersonalizeItemBounds
    {
      get => this.m_oDepersonalizeItemBounds;
      set => this.m_oDepersonalizeItemBounds = value;
    }

    public int ItemOuterBoundsWidth
    {
      get => this.m_iItemOuterBoundsWidth;
      set => this.m_iItemOuterBoundsWidth = value;
    }

    public Rectangle HasMoreItemsUpBounds
    {
      get => this.m_oHasMoreItemsUpBounds;
      set => this.m_oHasMoreItemsUpBounds = value;
    }

    public Rectangle HasMoreItemsDownBounds
    {
      get => this.m_oHasMoreItemsDownBounds;
      set => this.m_oHasMoreItemsDownBounds = value;
    }

    public Rectangle ParentIntersectionBounds
    {
      get => this.m_oParentIntersectionBounds;
      set => this.m_oParentIntersectionBounds = value;
    }

    public Color ParentMenuIntersectColor
    {
      get => this.m_oParentMenuIntersectColor;
      set => this.m_oParentMenuIntersectColor = value;
    }

    public Color IconBackground1Color
    {
      get => this.m_oIconBackground1Color;
      set => this.m_oIconBackground1Color = value;
    }

    public Color IconBackground2Color
    {
      get => this.m_oIconBackground2Color;
      set => this.m_oIconBackground2Color = value;
    }

    public Color DepersonalizeImageBackground
    {
      get => this.m_oDepersonalizeImageBackground;
      set => this.m_oDepersonalizeImageBackground = value;
    }

    public Color DepersonalizeImageForeground
    {
      get => this.m_oDepersonalizeImageForeground;
      set => this.m_oDepersonalizeImageForeground = value;
    }

    public Color IconBackgroundDepersonalized1Color
    {
      get => this.m_oIconBackgroundDepersonalized1Color;
      set => this.m_oIconBackgroundDepersonalized1Color = value;
    }

    public Color IconBackgroundDepersonalized2Color
    {
      get => this.m_oIconBackgroundDepersonalized2Color;
      set => this.m_oIconBackgroundDepersonalized2Color = value;
    }
  }
}
