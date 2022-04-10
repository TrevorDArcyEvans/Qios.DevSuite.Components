// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.QThemeInfoCollection
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using Qios.DevSuite.Components.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Globalization;

namespace Qios.DevSuite.Components
{
  [Editor(typeof (QThemeInfoCollectionEditor), typeof (UITypeEditor))]
  [ToolboxItem(false)]
  [DesignerSerializer(typeof (QThemeInfoCollectionCodeSerializer), typeof (CodeDomSerializer))]
  public sealed class QThemeInfoCollection : CollectionBase, IList, ICollection, IEnumerable
  {
    private QColorSchemeBase m_oColorScheme;

    public QThemeInfoCollection(QColorSchemeBase colorScheme) => this.m_oColorScheme = colorScheme != null ? colorScheme : throw new Exception(QResources.GetException("QThemeInfoCollection_NotNull", (object) "ColorScheme"));

    public int Add(QThemeInfo info)
    {
      if (info == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (info)));
      int index;
      if ((index = this.IndexOf(info.ThemeName)) >= 0)
      {
        if (!this[index].IsSystemTheme)
          this.InnerList[index] = (object) info;
      }
      else
      {
        index = this.InnerList.Add((object) info);
        this.HandleCollectionChange();
      }
      return index;
    }

    public void Insert(int index, QThemeInfo info) => this.InnerList.Insert(index, (object) info);

    public void Remove(QThemeInfo info)
    {
      QThemeInfoCollection.AssertSystemTheme(info, "remove");
      if (this.IndexOf(info) < 0)
        return;
      this.InnerList.Remove((object) info);
      this.HandleCollectionChange();
    }

    public new void RemoveAt(int index)
    {
      QThemeInfoCollection.AssertSystemTheme(this[index], "remove");
      this.InnerList.RemoveAt(index);
      this.HandleCollectionChange();
    }

    public int IndexOf(QThemeInfo info) => this.InnerList.Contains((object) info) ? this.InnerList.IndexOf((object) info) : -1;

    public int IndexOf(string themeName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.Compare(this[index].ThemeName, themeName, true, CultureInfo.InvariantCulture) == 0)
          return index;
      }
      return -1;
    }

    public bool Contains(QThemeInfo info) => this.InnerList.Contains((object) info);

    public bool Contains(string themeName) => this.IndexOf(themeName) >= 0;

    public QThemeInfo this[int index] => (QThemeInfo) this.InnerList[index];

    public QThemeInfo this[string themeName]
    {
      get
      {
        int index = this.IndexOf(themeName);
        return index >= 0 ? this[index] : (QThemeInfo) null;
      }
    }

    public new void Clear()
    {
      for (int index = this.Count - 1; index >= 0; --index)
      {
        if (!this[index].IsSystemTheme)
          this.Remove(this[index]);
      }
    }

    public QThemeInfo FindMatchingTheme(QThemeInfo theme)
    {
      if (theme == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (theme)));
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].FileName != null && this[index].WindowsSchemeName != null && string.Compare(this[index].FileName, theme.FileName, true, CultureInfo.InvariantCulture) == 0 && string.Compare(this[index].WindowsSchemeName, theme.WindowsSchemeName, true, CultureInfo.InvariantCulture) == 0)
          return this[index];
      }
      return (QThemeInfo) null;
    }

    public void CopyTo(QThemeInfo[] themes, int index) => ((ICollection) this).CopyTo((Array) themes, index);

    private static void AssertSystemTheme(QThemeInfo theme, string procedure)
    {
      if (theme.IsSystemTheme)
        throw new InvalidOperationException(QResources.GetException("QThemeInfoCollection_NotSystemTheme", (object) procedure, (object) theme.ThemeName));
    }

    private void HandleCollectionChange() => this.m_oColorScheme.RaiseThemesChanged();

    int IList.Add(object value)
    {
      QThemeInfo info = (QThemeInfo) value;
      this.Add(info);
      return this.IndexOf(info);
    }

    void IList.Clear() => this.Clear();

    bool IList.Contains(object value) => this.Contains((QThemeInfo) value);

    int IList.IndexOf(object value) => this.IndexOf((QThemeInfo) value);

    void IList.Insert(int index, object value)
    {
    }

    void IList.Remove(object value) => this.Remove((QThemeInfo) value);

    void IList.RemoveAt(int index)
    {
      if (this[index].IsSystemTheme)
        return;
      this.RemoveAt(index);
    }

    bool IList.IsReadOnly => false;

    bool IList.IsFixedSize => false;

    object IList.this[int index]
    {
      get => (object) this[index];
      set
      {
      }
    }
  }
}
