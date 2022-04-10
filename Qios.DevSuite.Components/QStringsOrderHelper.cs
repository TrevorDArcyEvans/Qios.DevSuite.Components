// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QStringsOrderHelper
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Text;

namespace Qios.DevSuite.Components
{
  internal class QStringsOrderHelper
  {
    private string[] m_aStrings;

    public QStringsOrderHelper(params string[] strings) => this.m_aStrings = strings;

    public QStringsOrderHelper(string commaSeperatedValue)
    {
      this.m_aStrings = commaSeperatedValue.Split(',');
      for (int index = 0; index < this.m_aStrings.Length; ++index)
        this.m_aStrings[index] = this.m_aStrings[index].Trim();
    }

    private int GetStringIndex(string value)
    {
      for (int stringIndex = 0; stringIndex < this.m_aStrings.Length; ++stringIndex)
      {
        if (string.Compare(this.m_aStrings[stringIndex], value, true) == 0)
          return stringIndex;
      }
      return -1;
    }

    public string Validate(string valueToValidate)
    {
      if (this.m_aStrings.Length == 0)
        return (string) null;
      switch (valueToValidate)
      {
        case "":
        case null:
          return string.Empty;
        default:
          string[] strArray = valueToValidate.Split(',');
          if (strArray.Length != this.m_aStrings.Length)
            throw new InvalidOperationException(QResources.GetException("QStringOrderHelper_InvalidCount"));
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < strArray.Length; ++index)
          {
            string str = strArray[index].Trim();
            int stringIndex = this.GetStringIndex(str);
            if (stringIndex < 0)
              throw new InvalidOperationException(QResources.GetException("QStringOrderHelper_ItemNotFound", (object) str));
            stringBuilder.Append(this.m_aStrings[stringIndex]);
            if (index < strArray.Length - 1)
              stringBuilder.Append(", ");
          }
          return stringBuilder.ToString();
      }
    }
  }
}
