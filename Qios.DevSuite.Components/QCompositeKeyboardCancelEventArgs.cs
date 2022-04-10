// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QCompositeKeyboardCancelEventArgs
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QCompositeKeyboardCancelEventArgs : QCompositeCancelEventArgs
  {
    private Keys m_eKeys;

    public QCompositeKeyboardCancelEventArgs(QComposite composite, Keys keys, bool cancel)
      : base(composite, (QCompositeItemBase) null, QCompositeActivationType.Keyboard, cancel)
    {
      this.m_eKeys = keys;
    }

    public Keys Keys
    {
      get => this.m_eKeys;
      set => this.m_eKeys = value;
    }
  }
}
