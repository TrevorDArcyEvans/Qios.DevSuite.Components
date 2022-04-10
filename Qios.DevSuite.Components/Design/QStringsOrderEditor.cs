// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QStringsOrderEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Qios.DevSuite.Components.Design
{
  public class QStringsOrderEditor : UITypeEditor
  {
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      if (context == null || context.Instance == null || context.PropertyDescriptor == null)
        return base.EditValue(context, provider, value);
      if (value == null)
        return (object) null;
      IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      QStringsOrderEditorControl orderEditorControl = new QStringsOrderEditorControl();
      orderEditorControl.ItemsString = Convert.ToString(value);
      service.DropDownControl((Control) orderEditorControl);
      return (object) orderEditorControl.ItemsString;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return context == null || context.Instance == null ? base.GetEditStyle(context) : UITypeEditorEditStyle.DropDown;
    }
  }
}
