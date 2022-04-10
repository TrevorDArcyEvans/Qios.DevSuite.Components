// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QPaintMenuItemEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  public class QPaintMenuItemEventArgs : EventArgs
  {
    private QMenuItem m_oMenuItem;
    private QCommandPaintOptions m_eFlags;
    private StringFormat m_oStringFormat;
    private Graphics m_oGraphics;

    public QPaintMenuItemEventArgs(
      QMenuItem menuItem,
      QCommandPaintOptions flags,
      StringFormat formatToUse,
      Graphics graphics)
    {
      this.m_oMenuItem = menuItem;
      this.m_eFlags = flags;
      this.m_oStringFormat = formatToUse;
      this.m_oGraphics = graphics;
    }

    public QMenuItem MenuItem => this.m_oMenuItem;

    public QCommandPaintOptions Flags => this.m_eFlags;

    public StringFormat StringFormat => this.m_oStringFormat;

    public Graphics Graphics => this.m_oGraphics;
  }
}
