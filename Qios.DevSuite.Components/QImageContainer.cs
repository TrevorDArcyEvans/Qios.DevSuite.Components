// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QImageContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QImageContainer : ICloneable
  {
    private Image m_oDefaultImage;
    private Image m_oImage;
    private string m_sResourceName;
    private bool m_bLoadedFromResource;

    public QImageContainer()
    {
    }

    public QImageContainer(Image defaultImage) => this.m_oDefaultImage = defaultImage;

    public bool ShouldSerializeImage() => this.m_oImage != null && !this.m_bLoadedFromResource;

    public object Clone() => QObjectCloner.CloneObject((object) this);

    public void ResetImage() => this.m_oImage = (Image) null;

    public Image Image
    {
      get => this.m_oImage;
      set
      {
        this.m_oImage = value;
        this.m_bLoadedFromResource = false;
        this.m_sResourceName = (string) null;
      }
    }

    public Image DefaultImage => this.m_oDefaultImage;

    public string ResourceName
    {
      get => this.m_sResourceName;
      set
      {
        if (this.m_sResourceName == value)
          return;
        this.m_sResourceName = value;
        this.m_bLoadedFromResource = true;
        this.m_oImage = QResources.LoadImageFromResource(this.m_sResourceName);
      }
    }

    public bool LoadedFromResource => this.m_bLoadedFromResource;

    public Image UsedImage => this.m_oImage == null ? this.m_oDefaultImage : this.m_oImage;
  }
}
