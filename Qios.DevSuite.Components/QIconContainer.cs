// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QIconContainer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Drawing;

namespace Qios.DevSuite.Components
{
  internal class QIconContainer : ICloneable
  {
    private Icon m_oDefaultIcon;
    private Icon m_oIcon;
    private string m_sResourceName;
    private Size m_oLastCalculatedSize;
    private bool m_bLoadedFromResource;

    public QIconContainer()
    {
    }

    public QIconContainer(Icon defaultIcon) => this.m_oDefaultIcon = defaultIcon;

    public bool ShouldSerializeIcon() => this.m_oIcon != null && !this.m_bLoadedFromResource;

    public object Clone() => QObjectCloner.CloneObject((object) this);

    public void ResetIcon() => this.m_oIcon = (Icon) null;

    public Icon Icon
    {
      get => this.m_oIcon;
      set
      {
        this.m_oIcon = value;
        this.m_bLoadedFromResource = false;
        this.m_sResourceName = (string) null;
        this.m_oLastCalculatedSize = Size.Empty;
      }
    }

    public Icon DefaultIcon => this.m_oDefaultIcon;

    public string ResourceName
    {
      get => this.m_sResourceName;
      set
      {
        if (this.m_sResourceName == value)
          return;
        this.m_sResourceName = value;
        this.m_bLoadedFromResource = true;
        this.m_oIcon = QResources.LoadIconFromResource(this.m_sResourceName);
        this.m_oLastCalculatedSize = Size.Empty;
      }
    }

    public bool LoadedFromResource => this.m_bLoadedFromResource;

    public Size CalculateIconSize(Size preferredSize)
    {
      if (this.m_oIcon != null)
      {
        if (this.m_oLastCalculatedSize != preferredSize && this.m_oIcon.Size != preferredSize)
        {
          this.m_oIcon = new Icon(this.m_oIcon, preferredSize);
          this.m_oLastCalculatedSize = preferredSize;
        }
        return preferredSize;
      }
      if (this.m_oDefaultIcon == null)
        return Size.Empty;
      if (this.m_oLastCalculatedSize != preferredSize && this.m_oDefaultIcon.Size != preferredSize)
      {
        this.m_oDefaultIcon = new Icon(this.m_oDefaultIcon, preferredSize);
        this.m_oLastCalculatedSize = preferredSize;
      }
      return preferredSize;
    }

    public Icon UsedIcon => this.m_oIcon == null ? this.m_oDefaultIcon : this.m_oIcon;
  }
}
