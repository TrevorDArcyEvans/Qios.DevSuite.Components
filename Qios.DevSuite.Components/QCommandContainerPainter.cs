// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCommandContainerPainter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  internal class QCommandContainerPainter
  {
    private QCommandPainter m_oCommandPainter;

    public QCommandContainerPainter() => this.m_oCommandPainter = new QCommandPainter();

    public QCommandPainter CommandPainter
    {
      get => this.m_oCommandPainter;
      set => this.m_oCommandPainter = value != null ? value : throw new InvalidOperationException("CommandPainter cannot be null.");
    }

    public virtual void LayoutHorizontal(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
    }

    public virtual void LayoutVertical(
      Rectangle rectangle,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      Control destinationControl,
      QCommandCollection commands)
    {
    }

    public virtual void DrawItem(
      QCommand command,
      QCommandConfiguration configuration,
      QCommandPaintParams paintParams,
      QCommandContainer parentContainer,
      QCommandPaintOptions flags,
      Graphics graphics)
    {
    }

    public virtual void DrawControlBackgroundHorizontal(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
    }

    public virtual void DrawControlBackgroundVertical(
      Rectangle rectangle,
      QAppearanceBase appearance,
      QColorSchemeBase colorScheme,
      QCommandPaintParams paintParams,
      QCommandConfiguration configuration,
      Control destinationControl,
      Graphics graphics)
    {
    }
  }
}
