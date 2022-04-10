// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QToolBarPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QToolBarPaintParams : QCommandPaintParams
  {
    private Color m_oShadedLineColor;
    private int m_iHasMoreItemsAreaWidth;
    private int m_iSizingGripSize;
    private int m_iCornerSize;
    private Bitmap m_oCustomizeImage;
    private Bitmap m_oHasMoreItemsImage;
    private QSizingGripStyle m_eSizingGripStyle;
    private QDrawToolBarFlags m_eFlags;
    private QToolBarLayoutFlags m_eLayoutFlags;
    private Rectangle m_oItemsBounds = Rectangle.Empty;
    private Rectangle m_oSizingGripBounds = Rectangle.Empty;
    private Rectangle m_oCustomizeBounds = Rectangle.Empty;
    private QToolBarLayoutType m_eLayoutType;
    private bool m_bShowCustomizeBar;
    private bool m_bShowCustomizeButton;
    private bool m_bShowSizingGrip;
    private int m_iRoundedBevelCornerSize;
    private int m_iSizingGripWidth;
    private QPadding m_oSizingGripPadding;
    private QSpacing m_oItemsSpacing;
    private QPadding m_oToolBarPadding;
    private QButtonState m_eCustomizeAreaState = QButtonState.Normal;
    private QButtonState m_eSizingGripState = QButtonState.Normal;
    private bool m_bShadeSeparator = true;
    private bool m_bShadeSizingGrip = true;
    private bool m_bShadeImages = true;
    private QToolBarStyle m_eToolBarStyle;
    private bool m_bStretched;
    private bool m_bExpandOnItemClick;
    private Icon m_oActiveMdiIcon;
    private QPadding m_oActiveMdiIconPadding;
    private Rectangle m_oActiveMdiIconBounds;
    private QMenuMdiButtons m_oMdiButtons;
    private QPadding m_oMdiButtonsPadding;
    private Size m_oMdiButtonsSize;
    private Rectangle m_oMdiButtonsBounds;

    public Color ShadedLineColor
    {
      get => this.m_oShadedLineColor;
      set => this.m_oShadedLineColor = value;
    }

    public int CornerSize
    {
      get => this.m_iCornerSize;
      set => this.m_iCornerSize = value;
    }

    public int HasMoreItemsAreaWidth
    {
      get => this.m_iHasMoreItemsAreaWidth;
      set => this.m_iHasMoreItemsAreaWidth = value;
    }

    public int SizingGripSize
    {
      get => this.m_iSizingGripSize;
      set => this.m_iSizingGripSize = value;
    }

    public Bitmap CustomizeImage
    {
      get => this.m_oCustomizeImage;
      set => this.m_oCustomizeImage = value;
    }

    public Bitmap HasMoreItemsImage
    {
      get => this.m_oHasMoreItemsImage;
      set => this.m_oHasMoreItemsImage = value;
    }

    public QSizingGripStyle SizingGripStyle
    {
      get => this.m_eSizingGripStyle;
      set => this.m_eSizingGripStyle = value;
    }

    public QDrawToolBarFlags Flags
    {
      get => this.m_eFlags;
      set => this.m_eFlags = value;
    }

    public QToolBarLayoutFlags LayoutFlags
    {
      get => this.m_eLayoutFlags;
      set => this.m_eLayoutFlags = value;
    }

    public Rectangle ItemsBounds
    {
      get => this.m_oItemsBounds;
      set => this.m_oItemsBounds = value;
    }

    public Rectangle SizingGripBounds
    {
      get => this.m_oSizingGripBounds;
      set => this.m_oSizingGripBounds = value;
    }

    public Rectangle CustomizeBounds
    {
      get => this.m_oCustomizeBounds;
      set => this.m_oCustomizeBounds = value;
    }

    public QToolBarLayoutType LayoutType
    {
      get => this.m_eLayoutType;
      set => this.m_eLayoutType = value;
    }

    public bool ShowCustomizeBar
    {
      get => this.m_bShowCustomizeBar;
      set => this.m_bShowCustomizeBar = value;
    }

    public bool ShowCustomizeButton
    {
      get => this.m_bShowCustomizeButton;
      set => this.m_bShowCustomizeButton = value;
    }

    public bool ShowSizingGrip
    {
      get => this.m_bShowSizingGrip;
      set => this.m_bShowSizingGrip = value;
    }

    public int RoundedBevelCornerSize
    {
      get => this.m_iRoundedBevelCornerSize;
      set => this.m_iRoundedBevelCornerSize = value;
    }

    public int SizingGripWidth
    {
      get => this.m_iSizingGripWidth;
      set => this.m_iSizingGripWidth = value;
    }

    public QPadding SizingGripPadding
    {
      get => this.m_oSizingGripPadding;
      set => this.m_oSizingGripPadding = value;
    }

    public virtual QSpacing ItemsSpacing
    {
      get => this.m_oItemsSpacing;
      set => this.m_oItemsSpacing = value;
    }

    public QPadding ToolBarPadding
    {
      get => this.m_oToolBarPadding;
      set => this.m_oToolBarPadding = value;
    }

    public bool ShadeSeparator
    {
      get => this.m_bShadeSeparator;
      set => this.m_bShadeSeparator = value;
    }

    public bool ShadeSizingGrip
    {
      get => this.m_bShadeSizingGrip;
      set => this.m_bShadeSizingGrip = value;
    }

    public bool ShadeImages
    {
      get => this.m_bShadeImages;
      set => this.m_bShadeImages = value;
    }

    public QToolBarStyle ToolBarStyle
    {
      get => this.m_eToolBarStyle;
      set => this.m_eToolBarStyle = value;
    }

    public bool Stretched
    {
      get => this.m_bStretched;
      set => this.m_bStretched = value;
    }

    public bool ExpandOnItemClick
    {
      get => this.m_bExpandOnItemClick;
      set => this.m_bExpandOnItemClick = value;
    }

    public QButtonState SizingGripState
    {
      get => this.m_eSizingGripState;
      set => this.m_eSizingGripState = value;
    }

    public QButtonState CustomizeAreaState
    {
      get => this.m_eCustomizeAreaState;
      set => this.m_eCustomizeAreaState = value;
    }

    public Icon ActiveMdiIcon
    {
      get => this.m_oActiveMdiIcon;
      set => this.m_oActiveMdiIcon = value;
    }

    public QPadding ActiveMdiIconPadding
    {
      get => this.m_oActiveMdiIconPadding;
      set => this.m_oActiveMdiIconPadding = value;
    }

    public Rectangle ActiveMdiIconBounds
    {
      get => this.m_oActiveMdiIconBounds;
      set => this.m_oActiveMdiIconBounds = value;
    }

    public QMenuMdiButtons MdiButtons
    {
      get => this.m_oMdiButtons;
      set => this.m_oMdiButtons = value;
    }

    public QPadding MdiButtonsPadding
    {
      get => this.m_oMdiButtonsPadding;
      set => this.m_oMdiButtonsPadding = value;
    }

    public Size MdiButtonsSize
    {
      get => this.m_oMdiButtonsSize;
      set => this.m_oMdiButtonsSize = value;
    }

    public Rectangle MdiButtonsBounds
    {
      get => this.m_oMdiButtonsBounds;
      set => this.m_oMdiButtonsBounds = value;
    }
  }
}
