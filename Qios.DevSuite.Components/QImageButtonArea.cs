// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QImageButtonArea
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QImageButtonArea : QButtonArea
  {
    public QImageButtonArea(MouseButtons listensToButtons, Image image)
      : base(listensToButtons)
    {
      this.AdditionalData = (object) image;
      this.UpdateSize();
    }

    private void UpdateSize() => this.Size = this.Image == null ? Size.Empty : this.Image.Size;

    public Image Image
    {
      get => this.AdditionalData as Image;
      set
      {
        this.AdditionalData = (object) value;
        this.UpdateSize();
      }
    }
  }
}
