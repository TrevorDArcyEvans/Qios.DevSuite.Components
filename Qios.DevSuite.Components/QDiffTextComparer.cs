// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QDiffTextComparer
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Qios.DevSuite.Components
{
  public class QDiffTextComparer : IDisposable
  {
    private bool m_bDisposed;
    private QDiffTextPart m_eInputTextPart = QDiffTextPart.Word;
    private bool m_bIncludeWhiteCharacters;
    private string m_sInputA = string.Empty;
    private string m_sInputB = string.Empty;

    public QDiffTextComparer() => this.InternalConstruct();

    public QDiffTextComparer(string textA, string textB, QDiffTextPart inputTextPart)
    {
      this.InternalConstruct();
      this.m_sInputA = textA == null ? "" : textA;
      this.m_sInputB = textB == null ? "" : textB;
      this.m_eInputTextPart = inputTextPart;
    }

    public QDiffTextComparer(
      string textA,
      string textB,
      QDiffTextPart inputTextPart,
      bool includeWhiteCharacters)
    {
      this.InternalConstruct();
      this.InputA = textA == null ? "" : textA;
      this.InputB = textB == null ? "" : textB;
      this.m_eInputTextPart = inputTextPart;
      this.m_bIncludeWhiteCharacters = includeWhiteCharacters;
    }

    ~QDiffTextComparer() => this.Dispose(false);

    public string InputA
    {
      get => this.m_sInputA;
      set => this.m_sInputA = value == null ? "" : value;
    }

    public string InputB
    {
      get => this.m_sInputB;
      set => this.m_sInputB = value == null ? "" : value;
    }

    public QDiffTextPart InputTextPart
    {
      get => this.m_eInputTextPart;
      set => this.m_eInputTextPart = value;
    }

    public bool IncludeWhiteCharacters
    {
      get => this.m_bIncludeWhiteCharacters;
      set => this.m_bIncludeWhiteCharacters = value;
    }

    public QDiffCompareResult Compare()
    {
      int[] indexes1;
      StringCollection textParts1 = this.GetTextParts(this.m_sInputA, out indexes1);
      int[] indexes2;
      StringCollection textParts2 = this.GetTextParts(this.m_sInputB, out indexes2);
      QDiff qdiff = new QDiff(QDiffTextComparer.HashStringList((IList) textParts1), QDiffTextComparer.HashStringList((IList) textParts2));
      return new QDiffCompareResult(textParts1, textParts2, indexes1, indexes2, qdiff.CalculateDifferences());
    }

    private void InternalConstruct()
    {
    }

    private StringCollection GetTextParts(string text, out int[] indexes)
    {
      StringCollection textParts = new StringCollection();
      ArrayList arrayList = new ArrayList();
      indexes = (int[]) null;
      if (this.m_eInputTextPart == QDiffTextPart.Line)
      {
        Regex regex = new Regex("\n");
        int num = 0;
        bool flag = true;
        while (flag)
        {
          Match match = regex.Match(text, num);
          flag = match.Success;
          if (match.Success)
          {
            string str = text.Substring(num, match.Index - num + 1);
            if (str != null && str.Length > 0)
            {
              textParts.Add(str);
              arrayList.Add((object) num);
            }
            num = match.Index + 1;
          }
        }
        textParts.Add(text.Substring(num));
        arrayList.Add((object) num);
      }
      else if (this.m_eInputTextPart == QDiffTextPart.Word)
      {
        MatchCollection matchCollection = (this.m_bIncludeWhiteCharacters ? new Regex("((\\S*)(\\s*))") : new Regex("\\S+")).Matches(text);
        for (int i = 0; i < matchCollection.Count; ++i)
        {
          textParts.Add(matchCollection[i].Value);
          arrayList.Add((object) matchCollection[i].Index);
        }
      }
      else if (this.m_eInputTextPart == QDiffTextPart.Character)
      {
        char[] charArray = text.ToCharArray();
        for (int index = 0; index < charArray.Length; ++index)
        {
          arrayList.Add((object) index);
          textParts.Add(charArray[index].ToString());
        }
      }
      indexes = (int[]) arrayList.ToArray(typeof (int));
      return textParts;
    }

    private static int[] HashStringList(IList oStringParts)
    {
      int[] numArray = new int[oStringParts.Count];
      for (int index = 0; index < oStringParts.Count; ++index)
        numArray[index] = ((string) oStringParts[index]).GetHashCode();
      return numArray;
    }

    public static void MarkDiffCompareResult(
      QDiffCompareResult result,
      RichTextBox inputA,
      RichTextBox inputB)
    {
      int length = 0;
      if (result == null || inputA == null || inputB == null)
        return;
      inputA.SelectAll();
      inputA.SelectionColor = SystemColors.ControlText;
      inputB.SelectAll();
      inputB.SelectionColor = SystemColors.ControlText;
      int[] indexA = result.GetIndexA();
      int[] indexB = result.GetIndexB();
      if (result.Changes == null)
        return;
      for (int index1 = 0; index1 < result.Changes.Count; ++index1)
      {
        if (result.Changes[index1].Type != QDiffChangeType.Insert)
        {
          int start = indexA[result.Changes[index1].StartA];
          for (int index2 = 1; index2 <= result.Changes[index1].LengthA; ++index2)
            length = index2 + result.Changes[index1].StartA >= indexA.Length ? inputA.Text.Length - start : indexA[result.Changes[index1].StartA + index2] - start;
          inputA.Select(start, length);
          inputA.SelectionColor = QDiffTextComparer.GetDiffChangeColor(result.Changes[index1]);
        }
        if (result.Changes[index1].Type != QDiffChangeType.Delete)
        {
          int start = indexB[result.Changes[index1].StartB];
          for (int index3 = 1; index3 <= result.Changes[index1].LengthB; ++index3)
            length = index3 + result.Changes[index1].StartB >= indexB.Length ? inputB.Text.Length - start : indexB[result.Changes[index1].StartB + index3] - start;
          inputB.Select(start, length);
          inputB.SelectionColor = QDiffTextComparer.GetDiffChangeColor(result.Changes[index1]);
        }
      }
    }

    private static Color GetDiffChangeColor(QDiffChange change)
    {
      if (change.Type == QDiffChangeType.Delete)
        return Color.Blue;
      if (change.Type == QDiffChangeType.Change)
        return Color.Red;
      return change.Type == QDiffChangeType.Insert ? Color.Green : SystemColors.ControlText;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.m_bDisposed)
        return;
      this.m_bDisposed = true;
    }
  }
}
