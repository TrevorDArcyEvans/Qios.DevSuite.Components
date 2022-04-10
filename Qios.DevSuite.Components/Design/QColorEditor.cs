// Decompiled with JetBrains decompiler
// Type: Qios.DevSuite.Components.Design.QColorEditor
// Assembly: Qios.DevSuite.Components, Version=4.0.0.20, Culture=neutral, PublicKeyToken=3cc53827b79c92fa
// MVID: CBFDE16D-813F-43A0-8BDF-661C2FBF9FBE
// Assembly location: C:\dev\trevorde\WaveletStudio\trunk\res\libs\Qios\Qios.DevSuite.Components.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;

namespace Qios.DevSuite.Components.Design
{
  [ToolboxItem(false)]
  internal class QColorEditor : UITypeEditor
  {
    private UITypeEditor m_oColorEditor;

    public QColorEditor() => this.m_oColorEditor = (UITypeEditor) TypeDescriptor.GetEditor(typeof (Color), typeof (UITypeEditor));

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      if (context == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (context)));
      QColor qcolor = (QColor) value;
      Color color = (Color) QMisc.AsValueType(this.m_oColorEditor.EditValue(provider, (object) qcolor.Current));
      if (qcolor.Current != color)
      {
        context.OnComponentChanging();
        qcolor.Current = color;
        context.OnComponentChanged();
      }
      return (object) qcolor;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override bool GetPaintValueSupported(ITypeDescriptorContext context) => this.m_oColorEditor.GetPaintValueSupported(context);

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void PaintValue(PaintValueEventArgs e)
    {
      if (e == null)
        throw new InvalidOperationException(QResources.GetException("General_ParameterNull", (object) nameof (e)));
      this.m_oColorEditor.PaintValue(new PaintValueEventArgs(e.Context, (object) ((QColor) e.Value).Current, e.Graphics, e.Bounds));
    }
  }
}
