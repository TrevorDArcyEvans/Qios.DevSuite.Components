// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QImageCacheKey
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Drawing;

namespace Qios.DevSuite.Components
{
  public struct QImageCacheKey
  {
    private QImageCacheType m_eConversionType;
    private int m_iSourceImageHash;
    private Icon m_oSourceIcon;
    private Image m_oSourceImage;
    private int[] m_aParameterHashes;
    private static QImageCacheKey m_oEmpty = new QImageCacheKey(QImageCacheType.None, (Icon) null, new object[0]);

    public QImageCacheKey(
      QImageCacheType conversionType,
      Icon sourceIcon,
      params object[] conversionParameters)
    {
      this.m_eConversionType = conversionType;
      this.m_oSourceImage = (Image) null;
      this.m_oSourceIcon = sourceIcon;
      this.m_iSourceImageHash = this.m_oSourceIcon == null ? 0 : this.m_oSourceIcon.GetHashCode();
      this.m_aParameterHashes = (int[]) null;
      this.FillParameterHashes(conversionParameters);
    }

    public QImageCacheKey(
      QImageCacheType conversionType,
      Image sourceImage,
      params object[] conversionParameters)
    {
      this.m_eConversionType = conversionType;
      this.m_oSourceIcon = (Icon) null;
      this.m_oSourceImage = sourceImage;
      this.m_iSourceImageHash = this.m_oSourceImage == null ? 0 : this.m_oSourceImage.GetHashCode();
      this.m_aParameterHashes = (int[]) null;
      this.FillParameterHashes(conversionParameters);
    }

    public QImageCacheKey(
      QImageCacheType conversionType,
      int sourceImageHash,
      params object[] conversionParameters)
    {
      this.m_eConversionType = conversionType;
      this.m_iSourceImageHash = sourceImageHash;
      this.m_oSourceImage = (Image) null;
      this.m_oSourceIcon = (Icon) null;
      this.m_aParameterHashes = (int[]) null;
      this.FillParameterHashes(conversionParameters);
    }

    public int SourceImageHash => this.m_iSourceImageHash;

    public Image SourceImage => this.m_oSourceImage;

    public Icon SourceIcon => this.m_oSourceIcon;

    public QImageCacheType ConversionType => this.m_eConversionType;

    public int[] ParametersHashes => this.m_aParameterHashes;

    public static QImageCacheKey Empty => QImageCacheKey.m_oEmpty;

    private void FillParameterHashes(object[] conversionParameters)
    {
      if (conversionParameters == null || conversionParameters.Length <= 0)
        return;
      this.m_aParameterHashes = new int[conversionParameters.Length];
      for (int index = 0; index < conversionParameters.Length; ++index)
      {
        if (conversionParameters[index] != null)
          this.m_aParameterHashes[index] = conversionParameters[index].GetHashCode();
        else
          conversionParameters[index] = (object) 0;
      }
    }

    public override int GetHashCode()
    {
      uint num1 = 0;
      uint num2 = (uint) (this.m_eConversionType.GetHashCode() ^ this.m_iSourceImageHash >> 16 ^ this.m_iSourceImageHash & (int) ushort.MaxValue);
      if (this.m_aParameterHashes != null)
      {
        for (int index = 0; index < this.m_aParameterHashes.Length; ++index)
          num1 ^= (uint) (this.m_aParameterHashes[index] >> 16 ^ this.m_aParameterHashes[index] & (int) ushort.MaxValue);
      }
      return (int) num2 << 16 | (int) num1 & (int) ushort.MaxValue;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is QImageCacheKey qimageCacheKey) || qimageCacheKey.m_eConversionType != this.m_eConversionType || qimageCacheKey.m_oSourceIcon != this.m_oSourceIcon || qimageCacheKey.m_oSourceImage != this.m_oSourceImage || qimageCacheKey.m_aParameterHashes == null != (this.m_aParameterHashes == null))
        return false;
      if (this.m_aParameterHashes != null)
      {
        if (qimageCacheKey.m_aParameterHashes.Length != this.m_aParameterHashes.Length)
          return false;
        for (int index = 0; index < this.m_aParameterHashes.Length; ++index)
        {
          if (this.m_aParameterHashes[index] != qimageCacheKey.m_aParameterHashes[index])
            return false;
        }
      }
      return true;
    }

    public static bool operator ==(QImageCacheKey operand1, QImageCacheKey operand2) => operand1.Equals((object) operand2);

    public static bool operator !=(QImageCacheKey operand1, QImageCacheKey operand2) => !operand1.Equals((object) operand2);
  }
}
