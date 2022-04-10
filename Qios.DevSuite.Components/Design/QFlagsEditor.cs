// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QFlagsEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QFlagsEditor : UITypeEditor
  {
    private const int m_iExtraListItemSpace = 40;

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      if (context == null || context.Instance == null || context.PropertyDescriptor == null)
        return base.EditValue(context, provider, value);
      bool flag = false;
      int num1 = 0;
      int num2 = 0;
      IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      CheckedListBox checkedListBox = new CheckedListBox();
      checkedListBox.BorderStyle = BorderStyle.None;
      Graphics graphics = checkedListBox.CreateGraphics();
      checkedListBox.CheckOnClick = true;
      System.Type propertyType = context.PropertyDescriptor.PropertyType;
      string[] names = Enum.GetNames(propertyType);
      SizeF empty = SizeF.Empty;
      for (int index = 0; index < names.Length; ++index)
      {
        if ((int) Enum.Parse(propertyType, names[index]) == 0)
          flag = true;
        if (names[index] != null)
        {
          if (num2 < names[index].Length)
          {
            SizeF sizeF = graphics.MeasureString(names[index], checkedListBox.Font);
            if ((double) sizeF.Width > (double) num1)
              num1 = (int) sizeF.Width;
          }
          num2 = names[index].Length;
        }
        if ((int) Enum.Parse(propertyType, names[index]) != 0)
        {
          bool isChecked = ((int) value & (int) Enum.Parse(propertyType, names[index])) != 0;
          checkedListBox.Items.Add((object) names[index], isChecked);
        }
      }
      checkedListBox.Width = num1 + 40;
      service.DropDownControl((Control) checkedListBox);
      int num3 = 0;
      for (int index = 0; index < checkedListBox.CheckedItems.Count; ++index)
      {
        string str = Convert.ToString(checkedListBox.CheckedItems[index], (IFormatProvider) CultureInfo.InvariantCulture);
        num3 |= (int) Enum.Parse(propertyType, str);
      }
      graphics.Dispose();
      return num3 != 0 || flag ? Enum.ToObject(propertyType, num3) : throw new InvalidOperationException(QResources.GetException("QFlagsEditor_NoZeroValueInEnum"));
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return context == null || context.Instance == null ? base.GetEditStyle(context) : UITypeEditorEditStyle.DropDown;
    }
  }
}
