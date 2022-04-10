// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QInputBoxPaintParams
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QInputBoxPaintParams
  {
    private QMargin m_oClientAreaMargin = new QMargin();
    private Rectangle m_oUpButtonBounds = Rectangle.Empty;
    private Rectangle m_oUpImageBounds = Rectangle.Empty;
    private Rectangle m_oDownButtonBounds = Rectangle.Empty;
    private Rectangle m_oDownImageBounds = Rectangle.Empty;
    private Rectangle m_oDropDownButtonBounds = Rectangle.Empty;
    private Rectangle m_oDropDownImageBounds = Rectangle.Empty;
    private Rectangle m_oBackgroundBounds = Rectangle.Empty;
    private Rectangle m_oTextPaddingBounds = Rectangle.Empty;
    private bool m_bPaintTransparentBackground;

    public bool PaintTransparentBackground
    {
      get => this.m_bPaintTransparentBackground;
      set => this.m_bPaintTransparentBackground = value;
    }

    public Rectangle UpButtonBounds
    {
      get => this.m_oUpButtonBounds;
      set => this.m_oUpButtonBounds = value;
    }

    public Rectangle UpImageBounds
    {
      get => this.m_oUpImageBounds;
      set => this.m_oUpImageBounds = value;
    }

    public Rectangle DownButtonBounds
    {
      get => this.m_oDownButtonBounds;
      set => this.m_oDownButtonBounds = value;
    }

    public Rectangle DownImageBounds
    {
      get => this.m_oDownImageBounds;
      set => this.m_oDownImageBounds = value;
    }

    public Rectangle DropDownButtonBounds
    {
      get => this.m_oDropDownButtonBounds;
      set => this.m_oDropDownButtonBounds = value;
    }

    public Rectangle DropDownImageBounds
    {
      get => this.m_oDropDownImageBounds;
      set => this.m_oDropDownImageBounds = value;
    }

    public QMargin ClientAreaMargin
    {
      get => this.m_oClientAreaMargin;
      set => this.m_oClientAreaMargin = value;
    }

    public Rectangle BackgroundBounds
    {
      get => this.m_oBackgroundBounds;
      set => this.m_oBackgroundBounds = value;
    }

    internal Rectangle TextPaddingBounds
    {
      get => this.m_oTextPaddingBounds;
      set => this.m_oTextPaddingBounds = value;
    }
  }
}
