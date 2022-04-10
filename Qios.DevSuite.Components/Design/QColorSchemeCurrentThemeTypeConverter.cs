// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorSchemeCurrentThemeTypeConverter
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System.Collections;
using System.ComponentModel;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  public class QColorSchemeCurrentThemeTypeConverter : StringConverter
  {
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => context != null && context.Instance is QColorSchemeBase;

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => false;

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      if (context == null || !(context.Instance is QColorSchemeBase))
        return (TypeConverter.StandardValuesCollection) null;
      QColorSchemeBase instance = (QColorSchemeBase) context.Instance;
      string[] values = new string[instance.Themes.Count];
      for (int index = 0; index < instance.Themes.Count; ++index)
        values[index] = instance.Themes[index].ThemeName;
      return new TypeConverter.StandardValuesCollection((ICollection) values);
    }
  }
}
